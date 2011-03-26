using System;
using System.Collections.Generic;
using System.Text;
using LitJson;

namespace libTravian
{
	public class AIQueue : IQueue
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
			get { return AIType.ToString(); }
		}

		public string Status
		{
			get
			{
				if(Gid == 0)
					return "";
				else
					return UpCall.GetGidLang(Gid);
			}
		}

		public int CountDown
		{
			get
			{
				var CV = UpCall.TD.Villages[VillageID];
				var Cinb = CV.InBuilding;
				int delay = 0;
				if(Cinb[0] != null && Cinb[0].FinishTime != DateTime.MinValue)
					delay = Math.Max(delay, Convert.ToInt32(Cinb[0].FinishTime.Subtract(DateTime.Now).TotalSeconds));
				//if(Cinb[1] != null)
				//	delay = Math.Max(delay, Convert.ToInt32(Cinb[1].FinishTime.Subtract(DateTime.Now).TotalSeconds));
				if(NextExec != DateTime.MinValue)
					delay = Math.Max(delay, Convert.ToInt32(NextExec.Subtract(DateTime.Now).TotalSeconds));
				return delay;
			}
		}

		public void Action()
		{
			TVillage CV = this.UpCall.TD.Villages[VillageID];

			if(this.NextExec >= DateTime.Now)
				return;

			this.NextExec = DateTime.Now.AddSeconds(50);

			int bid = -1, gid = 0;
			double extrarate = (double)CV.Resource[0].Capacity / CV.Resource[3].Capacity;

			if(AIType == TAIType.Resource)
			{
				// by current warehouse amount
				int i;
				if(CV.isBuildingInitialized != 2)
					return;
                double min_ratio = double.MaxValue;
				for(i = 0; i < 3; i++)
                {
                    double ratio = CV.Resource[i].CurrAmount / Travian.resrate[i];
                    if (ratio < min_ratio)
                    {
                        gid = i + 1;
                        min_ratio = ratio;
                    }
                }

                // Crop
                if (CV.Resource[3].Produce <= 2 ||
                    CV.Resource[3].CurrAmount * extrarate / Travian.resrate[3] < min_ratio)
                {
                    gid = 4;
                }

                int min_level = Int32.MaxValue;
                for (i = 1; i <= 18; i++)
                {
                    if (CV.Buildings[i].Gid == gid)
                    {
                        if (CV.Buildings[i].Level < min_level)
                        {
                            bid = i;
                            min_level = CV.Buildings[i].Level;
                        }
                    }
                }
			}
			else
			{
				// by resource field level
				int i;
				int minlevel = 10;
				int[] buildpriority = new int[5];
				for(i = 1; i <= 18; i++)
				{
					if(!CV.Buildings.ContainsKey(i))
						continue;
					var tlevel = CV.Buildings[i].Gid != 4 ? CV.Buildings[i].Level : CV.Buildings[i].Level + 1;
					if(tlevel < minlevel)
						minlevel = tlevel;
				}
				int min = 1;
				for(i = 1; i <= 18; i++)
				{
					if(!CV.Buildings.ContainsKey(i))
						continue;
					var tlevel = CV.Buildings[i].Gid != 4 ? CV.Buildings[i].Level : CV.Buildings[i].Level + 1;
					if(tlevel == minlevel && buildpriority[CV.Buildings[i].Gid] == 0)
					{
						min = CV.Buildings[i].Gid - 1;
						buildpriority[CV.Buildings[i].Gid] = i;
					}
				}
				for(i = 0; i < 4; i++)
					if(CV.Resource[min].CurrAmount / Travian.resrate[min] > CV.Resource[i].CurrAmount / Travian.resrate[i] && buildpriority[i + 1] != 0)
						min = i;
				gid = min + 1;
				bid = buildpriority[gid];

				if(minlevel == 10)
				{
                    bool croop = false;
                    for (i = 1; i <= 18; i++)
                    {
                        if (!CV.Buildings.ContainsKey(i))
                            continue;
                        var tlevel = CV.Buildings[i].Level;
                        if (tlevel < 10 && croop == false)
                        {
                            gid = CV.Buildings[i].Gid;
                            bid = i;
                            croop = true;
                        }
                    }
                    if (croop == false && CV.Queue.Contains(this))
                    {
                        MarkDeleted = true;
                        UpCall.Dirty = true;
                        UpCall.CallStatusUpdate(this, new Travian.StatusChanged() { ChangedData = Travian.ChangedType.Queue, VillageID = VillageID });
                        return;
                    }
				}
			}
			// balance on warehouse and resource field:
			int[] rate2;
			if(UpCall.TD.Villages.Count > 20)
				rate2 = new int[3] { 7, 8, 1000 };
			else if(UpCall.TD.Villages.Count > 5)
				rate2 = new int[3] { 3, 4, 3000 };
			else
				rate2 = new int[3] { 2, 3, 3000 };

			// check warehouse/granary
			int tgid, tbid;

			// romans double-way build
			TInBuilding[] Cinb = CV.InBuilding;
            int[] costs = Buildings.Cost(gid, CV.Buildings[bid].Level + 1).Resources;
			if(Cinb[1] == null || Convert.ToInt32(Cinb[1].FinishTime.Subtract(DateTime.Now).TotalSeconds) <= 0)
			{
                int max_cost = 0;
                for (int i = 0; i < 3; i++)
                {
                    if (costs[i] > max_cost)
                    {
                        max_cost = costs[i];
                    }
                }

				if(CV.Resource[0].Capacity < max_cost * rate2[0])
				{
					tgid = 10;
					tbid = findDorf2Building(CV.Buildings, tgid);
					if(tbid != -1)
					{
						gid = tgid;
						bid = tbid;
					}
				}
				else if(CV.Resource[3].Capacity < costs[3] * rate2[1])
				{
					tgid = 11;
					tbid = findDorf2Building(CV.Buildings, tgid);
					if(tbid != -1)
					{
						gid = tgid;
						bid = tbid;
					}
				}
				else if(!Travian.NoMB) // check main building
				{
					tgid = 15;
					tbid = findDorf2Building(CV.Buildings, tgid);
					if(tbid != -1 && CV.Buildings[tbid].Level < 20 && CV.Buildings[tbid].Level * rate2[2] < CV.Resource[0].Capacity)
					{
						gid = tgid;
						bid = tbid;
					}
					// if nothing match, build the resource field
				}
			}
			var BQ = new BuildingQueue()
			{
				Bid = bid,
				Gid = gid,
				UpCall = UpCall,
				VillageID = VillageID
			};
			Gid = gid;
			if(BQ.CountDown <= 0)
				BQ.Action();
			//Q.Status = UpCall.GetGidLang(gid);
			/*
			int timecost;
			if(CV.Buildings.ContainsKey(bid))
				timecost = CV.TimeCost(Buildings.Cost(gid, CV.Buildings[bid].Level + 1));
			else
				timecost = CV.TimeCost(Buildings.Cost(gid, 1));
			if(timecost > 0)
				Q.NextExec = DateTime.Now.AddSeconds(Math.Min(timecost, rand.Next(500, 1000)));
			else
				;//doBuild(VillageID, BQ);
			 */
		}

		#endregion

		[Json]
		public DateTime NextExec { get; set; }

		private Random rand = new Random();

		[Json]
		public TAIType AIType { get; set; }

		public enum TAIType
		{
			Resource = 0, Level = 1
		}

		public int findDorf2Building(SortedDictionary<int, TBuilding> b, int gid)
		{
			int k;
			int tlvl = 20, tid = 0;
			for(k = 19; k <= 40; k++)
				if(b.ContainsKey(k) && b[k].Gid == gid)
					if(tlvl > b[k].Level)
					{
						tlvl = b[k].Level;
						tid = k;
					}
			if(tid != 0)
				return tid;
			for(k = 0; k < Buildings.PreferPos[gid].Length; k++)
				if(!b.ContainsKey(Buildings.PreferPos[gid][k]))
					return Buildings.PreferPos[gid][k];
			//for(k = 19; k < b.Length; k++)

			return -1;
		}

		private int Gid = 0;
		public int QueueGUID { get { return 0; } }
	}
}
