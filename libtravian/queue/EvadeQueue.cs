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

        public enum EvadeStatus
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
            			name = latest_toop.Owner;
            		}
            		return "检测到[" + name + "(" + p.X + "|" + p.Y + ")]的攻击";
            	}
            	else if (evade_status == EvadeStatus.ReadyForEvade)
            	{
            		if (tpEvadePoint == null)
            			return "未知的转移位置";
            		return "部队准备转移至(" + tpEvadePoint.X + "|" + tpEvadePoint.Y + ")";
            	}
            	else if (evade_status == EvadeStatus.Evaded)
            	{
            		if (tpEvadePoint == null)
            			return "未知的转移位置";
            		return "部队正转移前往(" + tpEvadePoint.X + "|" + tpEvadePoint.Y + ")";
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

            if (evade_status == EvadeStatus.NoAtkDetected 
               || evade_status == EvadeStatus.AtkDetected
               || evade_status == EvadeStatus.TroopsBack)
            {
            	CheckAttack();
            }
            else if (evade_status == EvadeStatus.ReadyForEvade)
            {
            	DoEvade();
            }
            else if (evade_status == EvadeStatus.Evaded
                    || evade_status == EvadeStatus.ReadyForBack)
            {
            	evade_status = EvadeStatus.ReadyForBack;
            	DoTroopsBack();
            }
            else
            {
            	evade_status = EvadeStatus.NoAtkDetected;
            	MinimumDelay = nMinInterval;
            }
            
            UpCall.TD.Dirty = true;
            UpCall.CallStatusUpdate(this, new Travian.StatusChanged() { ChangedData = Travian.ChangedType.Queue, VillageID = VillageID });
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
        
        static Random random = new Random();
        private int RandomDelay(int min, int max)
        {
            return EvadeQueue.random.Next(min, max);
        }
        
        private void CheckAttack()
        {
        	var CV = UpCall.TD.Villages[VillageID];
        	
        	UpCall.PageQuery(VillageID, "build.php?gid=16");
        	latest_toop = null;
        	foreach (TTInfo tt in CV.Troop.Troops)
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
        			UpCall.DebugLog("由于出现异常，重置回避攻击检查队列。", DebugLevel.II);
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
        }
        
        private void DoEvade()
        {
        	string sendTroopsUrl = String.Format("a2b.php?z={0}", tpEvadePoint.Z);
            string sendTroopForm = UpCall.PageQuery(VillageID, sendTroopsUrl);
            if (sendTroopForm == null 
               || !sendTroopForm.Contains("<form method=\"POST\" name=\"snd\" action=\"a2b.php\">"))
            {
                MinimumDelay = 0;
                return;
            }

            Dictionary<string, string> postData = RaidQueue.GetHiddenInputValues(sendTroopForm);
        	postData.Add("c", "2");
        	postData.Add("x", tpEvadePoint.X.ToString());
        	postData.Add("y", tpEvadePoint.Y.ToString());
        	ReinforceToop = RaidQueue.GetMaxTroops(sendTroopForm);
        	for (int i = 0; i < this.bTroopFilter.Length; i++)
        	{
        		if (bTroopFilter[i])
        			continue;
        		
        		ReinforceToop[i] = 0;
        	}
            for (int i = 0; i < ReinforceToop.Length; i++)
            {
                string troopKey = String.Format("t{0}", i + 1);
                string troopNumber = ReinforceToop[i] == 0 ? "" : ReinforceToop[i].ToString();
                postData.Add(troopKey, troopNumber);
            }
            
            string confirmUrl = "a2b.php";
            string confirmForm = UpCall.PageQuery(this.VillageID, confirmUrl, postData);
            if (confirmForm == null)
            {
                MinimumDelay = 0;
                return;
            }

            Match errorMatch = Regex.Match(confirmForm, "<p class=\"error\">(.+)</span>");
            if (errorMatch.Success)
            {
                string error = String.Format(
                    "Delete village {0}. Error: {1}",
                    tpEvadePoint,
                    errorMatch.Groups[1].Value);
                UpCall.DebugLog("增援位置不靠谱：" + error, DebugLevel.W);
                this.MarkDeleted = true;
                return;
            }
            
            if (!confirmForm.Contains("<form method=\"post\" action=\"a2b.php\">"))
            {
            	MinimumDelay = 0;
                return;
            }

            TimeSpan timeCost = RaidQueue.GetOneWayTimeCost(confirmForm);
            if (timeCost == TimeSpan.MinValue)
            {
            	MinimumDelay = nMinInterval;
        		evade_status = EvadeStatus.NoAtkDetected;
                return;
            }
            
            postData = RaidQueue.GetHiddenInputValues(confirmForm);

            string result = this.UpCall.PageQuery(this.VillageID, confirmUrl, postData);
            if (result == null)
            {
            	MinimumDelay = 0;
                return;
            }
            
            evade_status = EvadeStatus.Evaded;
            ReinforceTimeCost = (int)timeCost.TotalSeconds;
            MinimumDelay = ReinforceTimeCost + RandomDelay(5, 10);
            
            TTInfo troopInfo = new TTInfo()
            {
                Tribe = UpCall.TD.Tribe,
                Troops = ReinforceToop
            };
            string message = String.Format(
                "部队回避 {0} ({1}) => {2} {3}",
                this.UpCall.TD.Villages[this.VillageID].Coord,
                this.VillageID,
                this.tpEvadePoint,
                troopInfo.FriendlyName);

            this.UpCall.DebugLog(message, DebugLevel.I);
        }
        
        private void DoTroopsBack()
        {
            string data = UpCall.PageQuery(VillageID, "build.php?gid=16");
            if (data == null)
            {
                MinimumDelay = 0;
                return;
            }
            Match m = Regex.Match(data, 
                                  "<a href=\"karte\\.php\\?d=" + tpEvadePoint.Z + "\">[^<]*?</a>"
                                 	+ ".*?onclick=\"window\\.location\\.href = \'([^\']*?)\'",
                                 RegexOptions.Singleline);
            if (!m.Success)
            {
            	evade_status = EvadeStatus.NoAtkDetected;
            	MinimumDelay = nMinInterval;
            	return;
            }
            
            string url = m.Groups[1].Value.Replace("amp;", "");
            data = UpCall.PageQuery(VillageID, url);
            
            if (data == null)
            {
                MinimumDelay = 0;
                return;
            }
            MatchCollection mc = Regex.Matches(data, "<input type=\"hidden\" name=\"([^\"]*?)\" value=\"([^\"]*?)\">");
            Dictionary<string, string> postData = new Dictionary<string, string>();
            foreach (Match m1 in mc)
            {
            	postData.Add(m1.Groups[1].Value, m1.Groups[2].Value);
            }
            for (int i = 0; i < ReinforceToop.Length; i++)
            {
            	if (ReinforceToop[i] == 0)
            		continue;
                string troopKey = String.Format("t[{0}]", i + 1);
                string troopNumber = ReinforceToop[i].ToString();
                postData.Add(troopKey, troopNumber);
            }
            postData.Add("s1", "ok");
            UpCall.PageQuery(VillageID, "build.php", postData);
            
            evade_status = EvadeStatus.TroopsBack;
            MinimumDelay = ReinforceTimeCost + RandomDelay(5, 10);
        }
        
        [Json]
        public DateTime resumeTime = DateTime.Now;
        [Json]
        public EvadeStatus evade_status = EvadeStatus.NoAtkDetected;
        [Json]
        public TTInfo latest_toop;
        [Json]
        public int ReinforceTimeCost;
        [Json]
        public int[] ReinforceToop;
        
        [Json]
        public TPoint tpEvadePoint;
        [Json]
        public int nMinInterval;
        [Json]
        public int nLeadTime;
        [Json]
        public bool[] bTroopFilter;
	}
}
