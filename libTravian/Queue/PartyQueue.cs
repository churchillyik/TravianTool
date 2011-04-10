using System;
using System.Collections.Generic;
using System.Text;
using LitJson;

namespace libTravian
{
	public class PartyQueue : IQueue
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
			get { return PartyType.ToString().Substring(1); }
		}

		public string Status
		{
			get
			{
				if(LastExec == DateTime.MinValue)
					return LastExec.ToShortTimeString();
				else
					return "";
			}
		}

		public int CountDown
		{
			get
			{
				var CV = UpCall.TD.Villages[VillageID];
				int timecost = CV.TimeCost(Buildings.PartyCos[(int)PartyType - 1]);
				if(NextExec != DateTime.MinValue && NextExec > DateTime.Now)
					timecost = Math.Max(timecost, Convert.ToInt32(NextExec.Subtract(DateTime.Now).TotalSeconds));
				if(CV.InBuilding[5] != null)
					return Math.Max(timecost, Convert.ToInt32(CV.InBuilding[5].FinishTime.Subtract(DateTime.Now).TotalSeconds) + 30);
				else
					return timecost;
			}
		}

		int retrycount = 0;

		public void Action()
		{
			var CV = UpCall.TD.Villages[VillageID];
			UpCall.PageQuery(VillageID, "build.php?gid=24&a=" + ((int)PartyType).ToString());
			LastExec = DateTime.Now;
			if(CV.InBuilding[5] == null || CV.InBuilding[5].FinishTime < DateTime.Now)
			{
				// error occurred!
				retrycount++;
				if(retrycount > 10)
				{
					UpCall.DebugLog("Error on party for several times! Delete the queue!", DebugLevel.W);
					MarkDeleted = true;
				}
				else
				{
					UpCall.DebugLog("Error on party! Will retry...", DebugLevel.I);
					NextExec = DateTime.Now.AddSeconds(rand.Next(500 + retrycount * 20, 800 + retrycount * 30));
				}
				UpCall.Dirty = true;
			}
			else
			{
				UpCall.BuildCount();
				retrycount = 0;
			}
		}

		#endregion

		private DateTime LastExec = DateTime.MinValue;

		private DateTime NextExec = DateTime.MinValue;

		[Json]
		public TPartyType PartyType { get; set; }

		private Random rand = new Random();

		public enum TPartyType
		{
			P500 = 1, P2000 = 2
		}

		public PartyQueue()
		{
		}
		public int QueueGUID { get { return 6; } }
	}
}
