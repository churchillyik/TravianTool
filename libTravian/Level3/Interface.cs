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
		public void CachedFetchVillages()
		{
			if(TD.Villages.Count != 0)
			{
				StatusUpdate(this, new StatusChanged() { ChangedData = ChangedType.Villages, VillageID = TD.ActiveDid });
				return;
			}
			FetchVillages();
		}
		
		public void FetchVillages()
		{
			Thread t = new Thread(new ThreadStart(doFetchVillages));
			t.Name = "FetchVillages";
			t.Start();
		}
		
		public void FetchVillageAllDetails(int VillageID)
		{
			Thread t = new Thread(new ParameterizedThreadStart(doFetchVAllDetails));
			t.Name = "FetchVillageAllDetails";
			t.Start(VillageID);
		}
		
		public void FetchVillageBuilding(int VillageID)
		{
			Thread t = new Thread(new ParameterizedThreadStart(doFetchVBuilding));
			t.Name = "FetchVillageBuilding";
			t.Start(VillageID);
		}
		
		public void FetchVillageUpgrade(int VillageID)
		{
			Thread t = new Thread(new ParameterizedThreadStart(doFetchVUpgrade));
			t.Name = "FetchVillageUpgrade";
			t.Start(VillageID);
		}
		
		public void FetchVillageDestroy(int VillageID)
		{
			Thread t = new Thread(new ParameterizedThreadStart(doFetchVDestroy));
			t.Name = "FetchVillageDestroy";
			t.Start(VillageID);
		}
		
		public void FetchVillageMarket(int VillageID)
		{
			Thread t = new Thread(new ParameterizedThreadStart(doFetchVMarket));
			t.Name = "FetchVillageMarket";
			t.Start(VillageID);
		}
		
		public void FetchVillageTroop(int VillageID)
		{
			Thread t = new Thread(new ParameterizedThreadStart(doFetchVTroop));
			t.Name = "FetchVillageTroop";
			t.Start(VillageID);
		}
		
        public void FetchVillageTroopAll(int VillageID)
        {
            Thread t = new Thread(new ParameterizedThreadStart(doFetchVTroopAll));
            t.Name = "FetchVillageTroopAll";
            t.Start(VillageID);
        }
        
        public void FetchHeroAdvantures(int VillageID)
		{
			Thread t = new Thread(new ParameterizedThreadStart(doFetchHeroAdventures));
			t.Name = "FetchHeroAdventures";
			t.Start(VillageID);
		}
        
        public void ExecuteHeroAdvanture(int key)
		{
			Thread t = new Thread(new ParameterizedThreadStart(doHeroAdventure));
			t.Name = "ExecuteHeroAdvanture";
			t.Start(key);
		}
        
		public void Cancel(int VillageID, int Key)
		{
			Thread t = new Thread(new ParameterizedThreadStart(doCancelWrapper));
			t.Name = "Cancel";
			t.Start(new CancelOption() { VillageID = VillageID, Key = Key });
		}
		
		public void FindOasis(int VillageID, int x, int y, int num)
		{
			ThrdFindOasis = new Thread(new ParameterizedThreadStart(doFindOasis));
			ThrdFindOasis.Name = "FindOasis";
			ThrdFindOasis.Start(new FindOasisOption() {VillageID = VillageID, axis_x = x, axis_y = y, search_num = num});
		}
		
		public void StopFindOasis()
		{
			try
			{
				if (ThrdFindOasis != null)
				{
					ThrdFindOasis.Abort();
					OasisFoundLog("用户停止了搜田过程！");
				}
			}
			catch(Exception)
			{
				
			}
		}
		
		public void FindRaidTargets(int VillageID, int Range, int Population)
		{
			Thread t = new Thread(new ParameterizedThreadStart(doFindRaidTargets));
			t.Name = "FindRaidTargets";
			t.Start(new SearchingRaidTargetOption() {VillageID = VillageID, Range = Range, Population = Population});
		}
		
		public void FindAnimals(FindAnimalsOption option)
		{
			ThrdFindAnimals = new Thread(new ParameterizedThreadStart(doFindAnimals));
			ThrdFindAnimals.Name = "FindAnimals";
			ThrdFindAnimals.Start(option);
		}
		
		public void StopFindAnimals()
		{
			try
			{
				if (ThrdFindAnimals != null)
				{
					ThrdFindAnimals.Abort();
					AnimalsFoundLog("用户停止了搜田过程！");
				}
			}
			catch(Exception)
			{
				
			}
		}
	}
}
