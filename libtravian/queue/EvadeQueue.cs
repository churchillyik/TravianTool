/*
 * Created by SharpDevelop.
 * User: Administrator
 * Date: 2011-6-21
 * Time: 22:14
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Text;
using LitJson;
using System.Threading;
using System.Text.RegularExpressions;

namespace libTravian
{
	/// <summary>
	/// Description of EvadeQueue.
	/// </summary>
	public class EvadeQueue : IQueue
	{
		#region IQueue 成员

        public Travian UpCall { set; get; }

        [Json]
        public int VillageID { set; get; }

        [Json]
        public bool MarkDeleted { get; private set; }

        [Json]
        public bool Paused { set; get; }

        public string Title
        {
            get
            {
                return "回避攻击";
            }
        }

        private enum EvadeStatus
        {
        	NoAtkDetected,
        	AtkDetected,
        	ReadyForEvade,
        	Evaded,
        	ReadyForBack,
        	TroopsBack
        };
        
        public string Status
        {
            get
            {
            	if (evade_status == EvadeStatus.NoAtkDetected)
            	{
            		return "未检测到攻击部队";
            	}
            	else if (evade_status == EvadeStatus.AtkDetected)
            	{
            		TPoint p = new TPoint(0, 0);
            		string name = "未知";
            		if (this.latest_toop != null)
            		{
            			p.Z = latest_toop.OwnerVillageZ;
            			name = latest_toop.VillageName;
            		}
            		return "检测到[" + name + "(" + p.X + "|" + p.Y + ")]的攻击";
            	}
            	else if (evade_status == EvadeStatus.ReadyForEvade)
            	{
            		if (nEvadePoint == null)
            			return "未知的转移位置";
            		return "部队准备转移至(" + nEvadePoint.X + "|" + nEvadePoint.Y + ")";
            	}
            	else if (evade_status == EvadeStatus.Evaded)
            	{
            		if (nEvadePoint == null)
            			return "未知的转移位置";
            		return "部队正转移前往(" + nEvadePoint.X + "|" + nEvadePoint.Y + ")";
            	}
            	else if (evade_status == EvadeStatus.ReadyForBack)
            	{
            		return "部队准备撤回";
            	}
            	else if (evade_status == EvadeStatus.TroopsBack)
            	{
            		return "部队正在撤回途中";
            	}
            	else
            	{
            		return "出现未知错误";
            	}
            }
        }

        public int CountDown
        {
            get
            {
                if (!UpCall.TD.Villages.ContainsKey(VillageID))
                {
                    MarkDeleted = true;
                    return 86400;
                }

                int value = 0;
                if (this.resumeTime > DateTime.Now)
                {
                    try
                    {
                        value = Convert.ToInt32((this.resumeTime - DateTime.Now).TotalSeconds);
                    }
                    catch (OverflowException)
                    {
                    }
                }

                return value;
            }
        }

        public void Action()
        {
        	//	当前时刻还未到唤醒时刻
            if (MinimumDelay > 0)
                return;

            var cv = UpCall.TD.Villages[VillageID];
            
            if (evade_status == EvadeStatus.NoAtkDetected 
               || evade_status == EvadeStatus.AtkDetected)
            {
            	UpCall.PageQuery(VillageID, "build.php?gid=16");
            	foreach (TTInfo tt in cv.Troop.Troops)
		        {
		            AnalizeAttacker(tt);
		        }
            	
            	if (latest_toop != null)
            	{
            		int latest_atk_delay = 
            			(int)latest_toop.FinishTime.Subtract(DateTime.Now).TotalSeconds;
            		if (latest_atk_delay > nMinInterval + nLeadTime)
            		{
            			MinimumDelay = nMinInterval;
            			evade_status = EvadeStatus.AtkDetected;
            		}
            		else if (latest_atk_delay > nLeadTime 
            		         && latest_atk_delay <= nMinInterval + nLeadTime)
            		{
            			MinimumDelay = latest_atk_delay - nLeadTime;
            			evade_status = EvadeStatus.ReadyForEvade;
            		}
            		else if (latest_atk_delay > 0 
            		         && latest_atk_delay <= nLeadTime)
            		{
            			MinimumDelay = 0;
            			evade_status = EvadeStatus.ReadyForEvade;
            		}
            		else
            		{
            			UpCall.DebugLog("由于出现异常，重置回避攻击检查队列。");
            			latest_toop = null;
            			MinimumDelay = nMinInterval;
						evade_status = EvadeStatus.NoAtkDetected;
            		}
            		
            	}
            	else
            	{
            		MinimumDelay = nMinInterval;
            		evade_status = EvadeStatus.NoAtkDetected;
            	}
            	
            	return;
            }
            else if (evade_status = EvadeStatus.ReadyForEvade)
            {
            	
            }
        }

        public int QueueGUID { get { return 15; } }

        #endregion
        
		public EvadeQueue()
		{
		}
		
		public int MinimumDelay
        {
            get
            {
                int value = 0;
                if (this.resumeTime > DateTime.Now)
                {
                    try
                    {
                        value = Convert.ToInt32((this.resumeTime - DateTime.Now).TotalSeconds);
                    }
                    catch (OverflowException)
                    {
                    }
                }

                return value;
            }
            set
            {
                this.resumeTime = DateTime.Now.AddSeconds(value);
            }
        }
		
		public DisplayLang dl 
        {
        	get
        	{
        		if (DisplayLang.Instance != null)
        			return DisplayLang.Instance;
        		
        		return new DisplayLang("cn");
        	}
        }
		
		bool IsAttackType(TTInfo troop, string attType)
        {
            if (troop.TroopType != TTroopType.Incoming)
                return false;

            bool result = false;
            var CV = UpCall.TD.Villages[VillageID];
            int index = troop.VillageName.IndexOf(CV.Name);
            
            if (index > 0)
            {
            	string test_str = troop.VillageName.Remove(index, CV.Name.Length);
            	if (attType == "attack")
            	{
            		foreach (string atk_lang in dl.AtkLang)
            		{
            			if (test_str.Contains(atk_lang))
            			{
            				result = true;
            				break;
            			}
            		}
            	}
                else if (attType == "raid")
            	{
            		foreach (string raid_lang in dl.RaidLang)
            		{
            			if (test_str.Contains(raid_lang))
            			{
            				result = true;
            				break;
            			}
            		}
            	}
            }

            return result;
        }

        bool IsRaid(TTInfo troop)
        {
            return IsAttackType(troop, "raid");
        }

        bool IsAttack(TTInfo troop)
        {
            return IsAttackType(troop, "attack");
        }

        void AnalizeAttacker(TTInfo troop)
        {
            if (!IsAttack(troop) && !IsRaid(troop))
                return;

            if (latest_toop == null || troop.FinishTime < latest_toop.FinishTime)
                latest_toop = troop;
        }

        private DateTime resumeTime = DateTime.Now;
        private EvadeStatus evade_status = EvadeStatus.NoAtkDetected;
        private TTInfo latest_toop = null;
        
        public TPoint nEvadePoint;
        public int nMinInterval;
        public int nLeadTime;
	}
}
