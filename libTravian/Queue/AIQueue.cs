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
				{
					delay = Math.Max(delay, Convert.ToInt32(
						Cinb[0].FinishTime.Subtract(DateTime.Now).TotalSeconds) + 10);
				}
				//------------------------------------------------------------------------
				//	对罗马以外不能双建的种族，需要等待内城建筑建造完毕
				if(!UpCall.TD.isRomans)
				{
					if (Cinb[1] != null && Cinb[1].FinishTime != DateTime.MinValue)
					{
						delay = Math.Max(delay, Convert.ToInt32(
							Cinb[1].FinishTime.Subtract(DateTime.Now).TotalSeconds) + 10);
					}
				}
				else
				{
					if (Cinb[1] != null && Cinb[1].FinishTime != DateTime.MinValue)
					{
						delay = Math.Min(delay, Convert.ToInt32(
							Cinb[1].FinishTime.Subtract(DateTime.Now).TotalSeconds) + 10);
					}
					else
					{
						delay = 0;
					}
				}
				if(NextExec != DateTime.MinValue)
				{
					delay = Math.Max(delay, Convert.ToInt32(
						NextExec.Subtract(DateTime.Now).TotalSeconds));
				}
				return delay;
			}
		}

		public void Action()
		{
			TVillage CV = this.UpCall.TD.Villages[VillageID];

			if(this.NextExec >= DateTime.Now)
				return;

			this.NextExec = DateTime.Now.AddSeconds(50);

			if(CV.isBuildingInitialized != 2)
				return;
						
			int gid, bid;
			
			if(AIType == TAIType.Resource)
			{
				if (!FetchAvailGidAndBidWithRes(CV, out gid, out bid))
					return;
			}
			else
			{
				if (!FetchAvailGidAndBidWithlvl(CV, out gid, out bid))
					return;
			}
			
			int[] costs = Buildings.Cost(gid, CV.Buildings[bid].Level + 1).Resources;
			int inside_gid, insid_bid;
			if (FetchAvailInsideBuilding(CV, costs, out inside_gid, out insid_bid))
			{
				gid = inside_gid;
				bid = insid_bid;
			}
			
			UpCall.DebugLog("AIQueue准备建造 " + DisplayLang.Instance.GetGidLang(gid) 
			                + " @" + bid, DebugLevel.II);
			var BQ = new BuildingQueue()
			{
				Bid = bid,
				Gid = gid,
				UpCall = UpCall,
				VillageID = VillageID
			};
			Gid = gid;

			int cd = BQ.CountDown;
			if(cd <= 0)
			{
				UpCall.DebugLog("AIQueue建筑队列启动！", DebugLevel.II);
				BQ.Action();
			}
			else
			{
				int delay = Math.Min(15 * 60, cd);
				UpCall.DebugLog("AIQueue建筑队列未能启动，建造该单位尚需等待" + cd 
				                + "秒；将于" + delay + "秒后再检查一次。"
				                , DebugLevel.II);
				this.NextExec = DateTime.Now.AddSeconds(delay);
				//	为了防止资源未刷新而引起的问题
				UpCall.PageQuery(VillageID, "dorf1.php");
			}
		}
		
		private bool FetchAvailGidAndBidWithRes(TVillage CV, out int gid, out int bid)
		{
			bid = -1;
			gid = 0;
			
			//	计算仓库容量和粮仓容量的比值
			double extrarate = (double)CV.Resource[0].Capacity / CV.Resource[3].Capacity;
			
			//	按资源总量
			int i;
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
            	if (!CV.Buildings.ContainsKey(i))
            		continue;
                if (CV.Buildings[i].Gid == gid)
                {
                    if (CV.Buildings[i].Level < min_level)
                    {
                        bid = i;
                        min_level = CV.Buildings[i].Level;
                    }
                }
            }
            
            return true;
		}
		
		private bool FetchAvailGidAndBidWithlvl(TVillage CV, out int gid, out int bid)
		{
			bid = -1;
			gid = 0;
			
			//	按资源田等级
			int i;
			int minlevel = 10;
			int[] buildpriority = new int[5];
			bool bIsCroopsAbove5 = false;
			
			//	以粮食的最低等级划分判定区域，避免初期粮食过少
			for(i = 1; i <= 18; i++)
			{
				if(!CV.Buildings.ContainsKey(i) || CV.Buildings[i].Gid != 4)
					continue;
				var tlevel = CV.Buildings[i].Level;
				if(tlevel < minlevel)
					minlevel = tlevel;
			}
			
			if (minlevel >= 5)
				bIsCroopsAbove5 = true;
			
			minlevel = 10;
			
			//	找到所有资源田的最低等级
			for(i = 1; i <= 18; i++)
			{
				if(!CV.Buildings.ContainsKey(i))
					continue;
				
				var tlevel = 0;
				if (bIsCroopsAbove5)
					tlevel = CV.Buildings[i].Gid != 4 ? CV.Buildings[i].Level : CV.Buildings[i].Level + 1;
				else
					tlevel = CV.Buildings[i].Level;
				if(tlevel < minlevel)
					minlevel = tlevel;
			}
			
			//	找到等级最低资源田的种类和坑号
			int min = 1;
			for(i = 1; i <= 18; i++)
			{
				if(!CV.Buildings.ContainsKey(i))
					continue;

				var tlevel = 0;
				if (bIsCroopsAbove5)
					tlevel = CV.Buildings[i].Gid != 4 ? CV.Buildings[i].Level : CV.Buildings[i].Level + 1;
				else
					tlevel = CV.Buildings[i].Level;
				if(tlevel == minlevel && buildpriority[CV.Buildings[i].Gid] == 0)
				{
					min = CV.Buildings[i].Gid - 1;
					buildpriority[CV.Buildings[i].Gid] = i;
				}
			}
			
			//	如果有多种资源田同级，则按资源的评估比率确定升级的资源田
			for(i = 0; i < 4; i++)
				if(CV.Resource[min].CurrAmount / Travian.resrate[min] > 
				   CV.Resource[i].CurrAmount / Travian.resrate[i] 
				   && buildpriority[i + 1] != 0)
					min = i;
			gid = min + 1;
			bid = buildpriority[gid];

			//	如果最低等级为10级，则查找等级低于10级的粮食
			if(minlevel == 10)
			{
                bool croop = false;
                for (i = 1; i <= 18; i++)
                {
                    if (!CV.Buildings.ContainsKey(i))
                        continue;
                    var tlevel = CV.Buildings[i].Level;
                    if (tlevel < 10)
                    {
                        gid = CV.Buildings[i].Gid;
                        bid = i;
                        croop = true;
                        break;
                    }
                }
                
               	int finish_cnt = 0;
               	for (i = 19; i <= 40; i++)
               	{
               		if (!CV.Buildings.ContainsKey(i))
                        continue;
               		
               		for (int j = 0; j < Goal.Length; j++)
               		{
               			if (CV.Buildings[i].Gid == Goal[j].gid 
               			    && CV.Buildings[i].Level == Goal[j].limit_lvl)
               			{
               				finish_cnt++;
               				break;
               			}
               		}
               	}
                
                //	如果所有资源田都满级了，那么删除掉该任务
                if (croop == false && finish_cnt == Goal.Length && CV.Queue.Contains(this))
                {
                    MarkDeleted = true;
                    UpCall.TD.Dirty = true;
                    UpCall.CallStatusUpdate(this, new Travian.StatusChanged() 
                                            { 
                                            	ChangedData = Travian.ChangedType.Queue, 
                                            	VillageID = VillageID 
                                            });
                    return false;
                }
			}
			
			return true;
		}
		
		private bool FetchAvailInsideBuilding(TVillage CV, int[] costs, out int gid, out int bid)
		{
			bid = -1;
			gid = 0;
			//	根据当前村庄的数量平衡建造资源田所需的资源和仓库容量
			//	村数	仓库容量 / 需要资源的最高项		粮仓容量 / 需要粮食量	仓库容量 / 中心大楼等级
			//	1 ~ 5		2							3						3000（中心大楼9到10级，需要仓库先到27000）
			//	6 ~ 20		3							4						3000（中心大楼9到10级，需要仓库先到27000）
			//	20以上		7							8						1000（中心大楼9到10级，需要仓库先到9000）
			int[] rate2;
			if(UpCall.TD.Villages.Count > 20)
				rate2 = new int[3] { 7, 8, 1000 };
			else if(UpCall.TD.Villages.Count > 5)
				rate2 = new int[3] { 3, 4, 3000 };
			else
				rate2 = new int[3] { 2, 3, 3000 };	

			//	检查仓库和粮仓
			int tgid, tbid;
			//	罗马双建：
			//	在内城建筑为空的情况下，优先建造内城建筑，
			//	那么在下轮再Tick的时候，将会跳过下面这块而开始造外城资源田
			TInBuilding[] Cinb = CV.InBuilding;
			if(Cinb[1] == null || Convert.ToInt32(Cinb[1].FinishTime.Subtract(DateTime.Now).TotalSeconds) <= 0)
			{
				//	找到建造当前建筑需要最多的资源
                int max_cost = 0;
                for (int i = 0; i < 3; i++)
                {
                    if (costs[i] > max_cost)
                    {
                        max_cost = costs[i];
                    }
                }

                //	如果仓库的总容量低于总需求的rate2[0]倍，则需要升级仓库
				if(CV.Resource[0].Capacity < max_cost * rate2[0])
				{
					tgid = 10;
					tbid = findDorf2Building(CV.Buildings, tgid);
					if(tbid != -1)
					{
						gid = tgid;
						bid = tbid;
						return true;
					}
				}
				//	如果粮仓的总容量低于粮食需求的rate2[1]倍，则需要升级粮仓
				else if(CV.Resource[3].Capacity < costs[3] * rate2[1])
				{
					tgid = 11;
					tbid = findDorf2Building(CV.Buildings, tgid);
					if(tbid != -1)
					{
						gid = tgid;
						bid = tbid;
						return true;
					}
				}
				//	如果中心大楼等级的rate2[2]倍低于仓库容量，则需要升级中心大楼
				else if(!Travian.NoMB)
				{
					tgid = 15;
					tbid = findDorf2Building(CV.Buildings, tgid);
					if(tbid != -1 && (!CV.Buildings.ContainsKey(tbid) ||
					   CV.Buildings.ContainsKey(tbid) && CV.Buildings[tbid].Level < 10
					   && CV.Buildings[tbid].Level * rate2[2] < CV.Resource[0].Capacity))
					{
						gid = tgid;
						bid = tbid;
						return true;
					}
				}
				
				if (FetchAdditionalConstruction(CV, out gid, out bid))
					return true;
			}
			
			return false;
		}
		
		private class ConstructGoal
		{
			public bool bPossible;
			public int req_gid;
			public int req_lvl;
			public int gid;
			public int limit_lvl;
			public ConstructGoal(bool bPossible, int req_gid, int req_lvl, int gid, int limit_lvl)
			{
				this.bPossible = bPossible;
				this.req_gid = req_gid;
				this.req_lvl = req_lvl;
				this.gid = gid;
				this.limit_lvl = limit_lvl;
			}
		};
		
		//					0		1		2		3		4		5		6
		//	建造优先级：木柴厂 > 砖块厂 > 铸造厂 > 行宫 > 市场 > 磨坊 > 面包房
		private static ConstructGoal[] Goal = new ConstructGoal[7]
		{
			//	木柴厂需要10级伐木场，造到5级
			new ConstructGoal(false, 1, 10, 5, 5),
			//	砖块厂需要10级粘土矿，造到5级
			new ConstructGoal(false, 2, 10, 6, 5),
			//	铸造厂需要10级铁矿，造到5级
			new ConstructGoal(false, 3, 10, 7, 5),
			//	行宫在铸造厂5级以后开始建造，造到10级
			new ConstructGoal(false, 7, 5, 25, 10),
			//	市场在行宫10级以后开始建造，造到10级
			new ConstructGoal(false, 25, 10, 17, 10),
			//	磨坊在市场10级以后开始建造，造到5级
			new ConstructGoal(false, 17, 10, 8, 5),
			//	面包房在磨坊5级以后开始建造，造到5级
			new ConstructGoal(false, 8, 5, 9, 5)
		};
		
		private bool FetchAdditionalConstruction(TVillage CV, out int gid, out int bid)
		{
			bid = -1;
			gid = 0;
			
			int tgid, tbid;
			
			for (int i = 0; i < Goal.Length; i++)
			{
				Goal[i].bPossible = false;
			}
			
			for (int i = 1; i <= 40; i++)
			{
				if(!CV.Buildings.ContainsKey(i))
					continue;
				
				for (int j = 0; j < Goal.Length; j++)
				{
					if (CV.Buildings[i].Gid == Goal[j].req_gid 
					    && CV.Buildings[i].Level >= Goal[j].req_lvl)
					{
						Goal[j].bPossible = true;
					}
				}
			}
			
			for (int i = 0; i < Goal.Length; i++)
			{
				if (!Goal[i].bPossible)
					continue;
				tgid = Goal[i].gid;
				tbid = findDorf2Building(CV.Buildings, tgid);
				if (tbid != -1 && (!CV.Buildings.ContainsKey(tbid) ||
				                  CV.Buildings.ContainsKey(tbid) && CV.Buildings[tbid].Level < Goal[i].limit_lvl))
				{
					gid = tgid;
					bid = tbid;
					return true;
				}
			}
			
			return false;
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
			//	遍历内城建筑，找到gid相符的最低等级的建筑
			for(k = 19; k <= 40; k++)
				if(b.ContainsKey(k) && b[k].Gid == gid)
					if(tlvl > b[k].Level)
					{
						tlvl = b[k].Level;
						tid = k;
					}
			if(tid != 0)
				return tid;
			//	如果内城不存在该gid的建筑，或改建筑已经满级，那么寻找一缺省的坑位
			for(k = 0; k < Buildings.PreferPos[gid].Length; k++)
				if(!b.ContainsKey(Buildings.PreferPos[gid][k]))
					return Buildings.PreferPos[gid][k];

			return -1;
		}

		private int Gid = 0;
		public int QueueGUID { get { return 0; } }
	}
}
