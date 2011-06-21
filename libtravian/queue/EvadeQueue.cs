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
            		return "检测到攻击部队来自[" + name + "(" + p.X + "|" + p.Y + ")]";
            	}
            	else if (evade_status == EvadeStatus.ReadyForEvade)
            	{
            		if (nEvadePoint == null)
            			return "未知的转移位置";
            		return "准备把部队转移至(" + nEvadePoint.X + "|" + nEvadePoint.Y + ")";
            	}
            	else if (evade_status == EvadeStatus.Evaded)
            	{
            		if (nEvadePoint == null)
            			return "未知的转移位置";
            		return "部队正转移前往(" + nEvadePoint.X + "|" + nEvadePoint.Y + ")";
            	}
            	else if (evade_status == EvadeStatus.ReadyForBack)
            	{
            		return "准备把部队撤回";
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

        private DateTime resumeTime = DateTime.Now;
        private EvadeStatus evade_status = EvadeStatus.NoAtkDetected;
        private TTInfo latest_toop = null;
        
        public TPoint nEvadePoint;
        public int nMinInterval;
        public int nLeadTime;
	}
}
