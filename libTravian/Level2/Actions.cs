/*
 * The contents of this file are subject to the Mozilla Public License
 * Version 1.1 (the "License"); you may not use this file except in
 * compliance with the License. You may obtain a copy of the License at
 * http://www.mozilla.org/MPL/
 * 
 * Software distributed under the License is distributed on an "AS IS"
 * basis, WITHOUT WARRANTY OF ANY KIND, either express or implied. See the
 * License for the specific language governing rights and limitations
 * under the License.
 * 
 * The Initial Developer of the Original Code is [MeteorRain <msg7086@gmail.com>].
 * Copyright (C) MeteorRain 2007, 2008. All Rights Reserved.
 * Contributor(s): [MeteorRain].
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace libTravian
{
	partial class Travian
	{
		public void Tick()
		{
			try
			{
				foreach (var vid in TD.Villages.Keys)
				{
					var CV = TD.Villages[vid];
					
					if (CV.isVillageInitialized != 2)
						continue;
					
					try
					{
						CV.Market.tick(CV, TD.MarketSpeed);
					}
					catch (Exception ex)
					{
						DebugLog(ex, DebugLevel.W);
					}
					try
					{
						CV.Troop.tick(CV);
						if(CV.Troop.ShouldRefresh)
						{
							CV.Troop.ShouldRefresh = false;
							FetchVillageTroop(vid);
						}
					}
					catch(Exception ex)
					{
						DebugLog(ex, DebugLevel.W);
					}
				}
			}
			catch (InvalidOperationException e)
			{
				// Good bye! Collection was modified; enumeration operation may not execute.
				DebugLog(e, DebugLevel.I);
			}
			
			if (NextExec > DateTime.Now)
				return;
			Thread t = new Thread(new ThreadStart(doTick));
			t.Start();
		}

		public void doTick()
		{
			if(!Monitor.TryEnter(Level2Lock))
				return;
			try
			{
				foreach(var vid in TD.Villages.Keys)
				{
					TVillage CV = TD.Villages[vid];
					
					if (CV.isVillageInitialized != 2)
						continue;
					
					List<int> status = new List<int>();

					foreach(var task in CV.Queue)
					{
						if(task.Paused)
						{
							continue;
						}
						if(status.Contains(task.QueueGUID))
							continue;
						else if(task.QueueGUID < 7)
							status.Add(task.QueueGUID);
						if (task.QueueGUID <= 1 && !TD.isRomans)
							status.Add(1 - task.QueueGUID);
						switch(task.GetType().Name)
						{
							case "BuildingQueue":
								if(CV.isBuildingInitialized == 0)
								{
									CV.InitializeBuilding();
									continue;
								}
								else if(CV.isBuildingInitialized == 1)
									continue;
								break;
							case "DestroyQueue":
								if(CV.isDestroyInitialized == 0)
								{
									CV.InitializeDestroy();
									continue;
								}
								else if(CV.isDestroyInitialized == 1)
									continue;
								break;
							case "ResearchQueue":
								if(CV.isUpgradeInitialized == 0)
								{
									CV.InitializeUpgrade();
									continue;
								}
								else if(CV.isUpgradeInitialized == 1)
									continue;
								break;
							case "PartyQueue":
								break;
							case "TransferQueue":
								break;
							case "NpcTradeQueue":
								break;
							case "RaidQueue":
                                if (CV.isTroopInitialized == 0)
                                {
                                    CV.InitializeTroop();
                                    continue;
                                }
								break;
							case "AttackQueue":
                                if (CV.isTroopInitialized == 0)
                                {
                                    CV.InitializeTroop();
                                    continue;
                                }
								break;
						}
						if(task.CountDown <= 0)
							task.Action();
					}
					for(int i = CV.Queue.Count - 1; i >= 0; i--)
						if(CV.Queue[i].MarkDeleted)
							CV.Queue.RemoveAt(i);
				}
			}
			catch(InvalidOperationException e)
			{
				// Good bye! Collection was modified; enumeration operation may not execute.
				DebugLog(e, DebugLevel.I);
			}
			try 
			{
				if(Dirty)
					DB.Instance.Snapshot(this);
				if(TD.Dirty)
					DB.Instance.Snapshot(TD); 
			}
			catch { }
			Monitor.Exit(Level2Lock);
		}

		/// <summary>
		/// Returns the minmum delay before a queued task can start running
		/// </summary>
		/// <param name="VillageID">which village the queued task belongs to</param>
		/// <param name="Q">A queued task</param>
		/// <returns>Delay in seconds</returns>
		/*
		public int GetDelay(int VillageID, TQueue Q)
		{
			var CV = TD.Villages[VillageID];
			int timecost;
			switch (Q.Type)
			{
				case 0:
				case 1:
					if (Q.Bid == TQueue.AIBID)
						return AIDelay(VillageID, Q);
					if (CV.Buildings.ContainsKey(Q.Bid))
						timecost = CV.TimeCost(Buildings.Cost(CV.Buildings[Q.Bid].Gid, CV.Buildings[Q.Bid].Level + 1));
					else
						timecost = CV.TimeCost(Buildings.Cost(Q.Gid, 1));
					var x = CV.InBuilding[TD.isRomans ? Q.Type : 0];
					if (timecost != 0)
						return timecost;
					else if (x == null || x.FinishTime.AddSeconds(15) < DateTime.Now)
						return 0;
					else
						return Convert.ToInt32(x.FinishTime.Subtract(DateTime.Now).TotalSeconds) + 5;
				//break;
				case 2:
					if (CV.InBuilding[Q.Type] != null)
						return Convert.ToInt32(CV.InBuilding[Q.Type].FinishTime.Subtract(DateTime.Now).TotalSeconds) + 5;
					else
						return 0;
				case 3:
					if (CV.isUpgradeInitialized == 0)
						doFetchVUpgrade(VillageID);
					if (CV.isUpgradeInitialized != 2)
						break;
					timecost = CV.TimeCost(Buildings.UpCost[(TD.Tribe - 1) * 10 + Q.Bid][CV.Upgrades[Q.Bid].AttackLevel]);
					if (CV.InBuilding[Q.Type] != null)
						return Math.Max(timecost, Convert.ToInt32(CV.InBuilding[Q.Type].FinishTime.Subtract(DateTime.Now).TotalSeconds) + 5);
					else
						return timecost;
				case 4:
					if (CV.isUpgradeInitialized == 0)
						doFetchVUpgrade(VillageID);
					if (CV.isUpgradeInitialized != 2)
						break;
					timecost = CV.TimeCost(Buildings.UpCost[(TD.Tribe - 1) * 10 + Q.Bid][CV.Upgrades[Q.Bid].DefenceLevel]);
					if (CV.InBuilding[Q.Type] != null)
						return Math.Max(timecost, Convert.ToInt32(CV.InBuilding[Q.Type].FinishTime.Subtract(DateTime.Now).TotalSeconds) + 5);
					else
						return timecost;
				case 5:
					if (CV.isUpgradeInitialized == 0)
						doFetchVUpgrade(VillageID);
					if (CV.isUpgradeInitialized != 2)
						break;
					timecost = CV.TimeCost(Buildings.ResearchCost[(TD.Tribe - 1) * 10 + Q.Bid]);
					if (CV.InBuilding[Q.Type] != null)
						return Math.Max(timecost, Convert.ToInt32(CV.InBuilding[Q.Type].FinishTime.Subtract(DateTime.Now).TotalSeconds) + 5);
					else
						return timecost;
				case 6:
					timecost = CV.TimeCost(Buildings.PartyCos[Q.Bid - 1]);
					if (CV.InBuilding[Q.Type] != null)
						return Math.Max(timecost, Convert.ToInt32(CV.InBuilding[Q.Type].FinishTime.Subtract(DateTime.Now).TotalSeconds) + 5);
					else
						return timecost;

				case (int)TQueueType.Transfer:
					TransferOption transferOption = TransferOption.FromString(Q.ExtraOptions);
					transferOption.CalculateResourceAmount(TD, VillageID);
					return transferOption.GetDelay(this.TD, VillageID);

				case (int) TQueueType.NpcTrade:
					NpcTradeOption npcTradeOption = NpcTradeOption.FromString(Q.ExtraOptions);
					return npcTradeOption.GetDelay(this.TD, VillageID);

				default:
					return 1;
			}
			return 1;
		}
		*/
	}
}
