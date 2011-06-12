/*
 * Created by SharpDevelop.
 * User: Administrator
 * Date: 2011-5-29
 * Time: 8:13
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Text;
using LitJson;
using System.Text.RegularExpressions;

namespace libTravian
{
	/// <summary>
	/// Description of AdventureQueue.
	/// </summary>
	public class AdventureQueue : IQueue
	{
		#region IQueue 成员
	
		public Travian UpCall { get; set; }

		[Json]
		public int VillageID { get; set; }

		[Json]
		public bool Paused { get; set; }

		[Json]
		public bool MarkDeleted { get; private set; }

		public string Title
		{
			get 
			{ 
				return "探险：共" + total_adv_pt + "处";
			}
		}

		public string Status
		{
			get
			{
				if (hero_status == -2)
					return "英雄的状况未知";
				else if (hero_status == -1)
					return "英雄不属于本村";
				else if (hero_status == 1)
					return "英雄目前不在家";
				else if (hero_status == 0)
					return "正在调查可探险的目标...";
				else if (hero_status == 2 && !cur_adv_pt.IsEmpty)
					return "前往(" + cur_adv_pt.X + "|" + cur_adv_pt.Y + ")探险";
				else if (hero_status == 2 && cur_adv_pt.IsEmpty)
					return "探险位置不存在或行程过长";
				else
					return "未知错误";
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
			if (MinimumDelay > 0)
				return;

			string data = UpCall.CheckBuildingExistAndQuery(VillageID, 16);
			if (data == null)
			{
				MinimumDelay = 60;
				return;
			}
			
			hero_status = CheckIfHeroHome();
			if (hero_status == -1)
			{
				UpCall.CallStatusUpdate(this, new Travian.StatusChanged() 
			                        { 
			                        	ChangedData = Travian.ChangedType.Queue, 
			                        	VillageID = VillageID 
			                        });
				UpCall.DebugLog("英雄不属于该村，无法探险！", DebugLevel.II);
				MarkDeleted = true;
				return;
			}
			else if (hero_status == 1)
			{
				UpCall.CallStatusUpdate(this, new Travian.StatusChanged() 
			                        { 
			                        	ChangedData = Travian.ChangedType.Queue, 
			                        	VillageID = VillageID 
			                        });
				UpCall.DebugLog("英雄目前不在家，等待英雄返回！", DebugLevel.II);
				return;
			}
			
			bool bAvail = false;
			List<HeroAdventureInfo> InfoList = UpCall.TD.Adv_Sta.HeroAdventures;
			for (int i = 0; i < InfoList.Count; i++)
			{
				TimeSpan ts = CheckDurAvail(InfoList[i].duration);
				if (ts == TimeSpan.MinValue)
					continue;
				bAvail = true;
				break;
			}
			
			//	如果当前的探险地方尚未初始化或全部都不合适，则尝试重新抓取
			if (!UpCall.TD.Adv_Sta.bIsHeroAdventureInitialize || !bAvail)
			{
				UpCall.doFetchHeroAdventures(VillageID);
			}

			cur_adv_pt = new TPoint(0, 0);
			int HeroLoc = UpCall.TD.Adv_Sta.HeroLocate;
			
			for (int i = 0; i < InfoList.Count; i++)
			{
				TimeSpan ts = CheckDurAvail(InfoList[i].duration);
				if (ts == TimeSpan.MinValue)
				{
					UpCall.DebugLog(
						"跳过(" + InfoList[i].axis_x + "|" + InfoList[i].axis_y + ")的探险",
						DebugLevel.II);
					continue;
				}
				
				TPoint tp = new TPoint(InfoList[i].axis_x, InfoList[i].axis_y);
				data = UpCall.PageQuery(HeroLoc, "a2b.php?id=" + tp.Z.ToString() + "&h=1");
				if (data == null)
					continue;
                Match m_test = Regex.Match(data, "type=\"submit\" value=\"ok\" name=\"h1\"");
                if (!m_test.Success)
                	continue;
				
                Dictionary<string, string> PostData = new Dictionary<string, string>();
				MatchCollection mc = Regex.Matches(
					data, "<input type=\"hidden\" name=\"([^\"]*?)\" value=\"([^\"]*?)\" />");
				string key, val;
				foreach (Match m in mc)
				{
					key = m.Groups[1].Value;
					val = m.Groups[2].Value;
					PostData[key] = val;
				}
				PostData["h1"] = "ok";
				UpCall.PageQuery(HeroLoc, "a2b.php", PostData);
				
				MinimumDelay = Convert.ToInt32(ts.TotalSeconds);
				cur_adv_pt = tp;
				break;
			}
			
			if (cur_adv_pt.IsEmpty)
			{
				MinimumDelay = 3600;
				UpCall.DebugLog("探险位置不存在或行程过长", DebugLevel.II);
			}
			else
			{
				UpCall.DebugLog("前往(" + cur_adv_pt.X + "|" + cur_adv_pt.Y + ")探险", DebugLevel.II);
			}
			hero_status = 2;
			UpCall.CallStatusUpdate(this, new Travian.StatusChanged() 
	                { 
	                	ChangedData = Travian.ChangedType.Queue, 
	                	VillageID = VillageID 
	                });
		}

		public int QueueGUID { get { return 13; } }
		
		#endregion
		
		private TimeSpan CheckDurAvail(string dur)
		{
			TimeSpan ts = UpCall.TimeSpanParse(dur);
			if (ts.TotalSeconds <= 3600 * 3)
				return ts;
			else
				return TimeSpan.MinValue;
		}
		
		private int CheckIfHeroHome()
		{
			TVillage CV = UpCall.TD.Villages[VillageID];
			foreach (TTInfo info in CV.Troop.Troops)
			{
				if (info.OwnerVillageZ != CV.Z || info.Troops[10] != 1)
					continue;
				resumeTime = info.FinishTime;
				if (info.TroopType == TTroopType.InVillage)
				{
					return 0;
				}
				else
				{
					if (info.FinishTime == DateTime.MinValue)
						MinimumDelay = 3600;
					return 1;
				}
			}
			
			return -1;
		}
		
		private int hero_status { get; set; }
		
		private TPoint cur_adv_pt { get; set; }
		
		private int total_adv_pt
		{
			get
			{
				return UpCall.TD.Adv_Sta.HeroAdventures.Count;
			}
		}
		
		private DateTime resumeTime = DateTime.Now;
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
		
		public AdventureQueue()
		{
			hero_status = -2;
		}
	}
}
