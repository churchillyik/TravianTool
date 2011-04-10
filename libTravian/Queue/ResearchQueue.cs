using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using LitJson;

namespace libTravian
{
	public class ResearchQueue : IQueue
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
			get { return UpCall.GetAidLang(UpCall.TD.Tribe, Aid); }
		}

		public string Status
		{
			get
			{
				string level, status;
				int timecost;
				if(!UpCall.TD.Villages.ContainsKey(VillageID))
				{
					UpCall.DebugLog("Unknown VillageID given in queue, cause to be deleted!", DebugLevel.W);
					MarkDeleted = true;
					return "UNKNOWN VID";
				}
				var CV = UpCall.TD.Villages[VillageID];
				TInBuilding x;

				if(ResearchType == TResearchType.UpTroopLevel)
				{
					if(TargetLevel == 0)
						level = "";
					else
						level = string.Format("{0}/{1}", CV.Upgrades[Aid].troop_lvl, TargetLevel);
					timecost = CV.TimeCost(Buildings.UpCost[(UpCall.TD.Tribe - 1) * 10 + Aid][CV.Upgrades[Aid].troop_lvl]);
					x = CV.InBuilding[3];
				}
				else
				{
					level = "";
					timecost = CV.TimeCost(Buildings.ResearchCost[(UpCall.TD.Tribe - 1) * 10 + Aid]);
					x = CV.InBuilding[4];
				}
				if(timecost != 0)
					status = "Lacking of resource";
				else if(x == null || x.FinishTime.AddSeconds(15) < DateTime.Now)
					status = "Starting";
				else
					status = "Waiting";
				return level + status;
			}
		}

		public int CountDown
		{
			get
			{
				int timecost;
				var CV = UpCall.TD.Villages[VillageID];
				TInBuilding x;
				if(ResearchType == TResearchType.UpTroopLevel)
				{
					timecost = CV.TimeCost(Buildings.UpCost[(UpCall.TD.Tribe - 1) * 10 + Aid][CV.Upgrades[Aid].troop_lvl]);
					x = CV.InBuilding[3];
				}
				else
				{
					timecost = CV.TimeCost(Buildings.ResearchCost[(UpCall.TD.Tribe - 1) * 10 + Aid]);
					x = CV.InBuilding[4];
				}
				if(x != null && x.FinishTime.AddSeconds(15) > DateTime.Now)
					timecost = Math.Max(timecost, Convert.ToInt32(x.FinishTime.Subtract(DateTime.Now).TotalSeconds) + 15);
				return timecost;
			}
		}

		public void Action()
		{
			var CV = UpCall.TD.Villages[VillageID];
			int GID;
			var Q = this;
			string mat_str, id, c;
			Match m;
			string result;
			switch(ResearchType)
			{
				case TResearchType.Research:
					if(!CV.Upgrades[Aid].CanResearch)
					{
						MarkDeleted = true;
						UpCall.Dirty = true;
						UpCall.CallStatusUpdate(this, new Travian.StatusChanged() { ChangedData = Travian.ChangedType.Queue, VillageID = VillageID });
						return;
					}
					GID = 22;
					result = UpCall.PageQuery(VillageID, "build.php?gid=" + GID.ToString());
					mat_str = "'build.php\\?id=(\\d+)&amp;a=" + Aid.ToString() + "&amp;c=([^']*?)'";
					m = Regex.Match(result, mat_str);
					if (!m.Success)
						return;
					id = m.Groups[1].Value;
					c = m.Groups[2].Value;
					result = UpCall.PageQuery(VillageID, "build.php?id=" + id + "&a=" + Aid.ToString() + "&c=" + c);
					break;
				case TResearchType.UpTroopLevel:
					if(TargetLevel != 0 && CV.Upgrades[Aid].troop_lvl >= TargetLevel || CV.Upgrades[Aid].troop_lvl >= CV.SmithyLevel)
					{
						MarkDeleted = true;
						UpCall.Dirty = true;
						UpCall.CallStatusUpdate(this, new Travian.StatusChanged() { ChangedData = Travian.ChangedType.Queue, VillageID = VillageID });
						return;
					}
					GID = 13;
					result = UpCall.PageQuery(VillageID, "build.php?gid=" + GID.ToString());
					mat_str = "'build.php\\?id=(\\d+)&amp;a=" + Aid.ToString() + "&amp;c=([^']*?)'";
					m = Regex.Match(result, mat_str, RegexOptions.Singleline);
					if (!m.Success)
						return;
					id = m.Groups[1].Value;
					c = m.Groups[2].Value;
					result = UpCall.PageQuery(VillageID, "build.php?id=" + id + "&a=" + Aid.ToString() + "&c=" + c);
					break;
				default:
					return;
			}
			UpCall.BuildCount();

			if(TargetLevel == 0 || ResearchType == TResearchType.Research)
			{
				MarkDeleted = true;
				UpCall.Dirty = true;
				UpCall.CallStatusUpdate(this, new Travian.StatusChanged() { ChangedData = Travian.ChangedType.Queue, VillageID = VillageID });
			}
			else if(ResearchType == TResearchType.UpTroopLevel)
			{
				if(CV.Upgrades[Aid].troop_lvl >= TargetLevel || CV.Upgrades[Aid].troop_lvl >= CV.SmithyLevel)
				{
					MarkDeleted = true;
					UpCall.Dirty = true;
					UpCall.CallStatusUpdate(this, new Travian.StatusChanged() { ChangedData = Travian.ChangedType.Queue, VillageID = VillageID });
				}
			}
			
			UpCall.CallStatusUpdate(this, new Travian.StatusChanged() { ChangedData = Travian.ChangedType.Research, VillageID = VillageID });
		}

		#endregion

		[Json]
		public TResearchType ResearchType { get; set; }

		public enum TResearchType
		{
			Research, UpTroopLevel
		}

		[Json]
		public int Aid { get; set; }

		/// <summary>
		/// Target level
		/// </summary>
		[Json]
		public int TargetLevel { get; set; }

		public int QueueGUID
		{
			get
			{
				switch(ResearchType)
				{
					case TResearchType.UpTroopLevel:
						return 3;
					case TResearchType.Research:
						return 5;
					default: // will not happened
						return -1;
				}
			}
		}
	}
}
