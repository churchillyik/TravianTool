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
using System.IO;
using System.Threading;
using System.Text.RegularExpressions;

namespace libTravian
{
    partial class Travian
    {
        private object Level2Lock = new object();
        private void doFetchVillages()
        {
            lock (Level2Lock)
            {
                if (TD.Tribe == 0)
                {
                    TD.Dirty = true;
                    TD.Tribe = NewParseTribe();
                }
                
                if (TD.Tribe != 0)
                    PageQuery(0, "dorf1.php");
                
                StatusUpdate(this, new StatusChanged() { ChangedData = ChangedType.Villages, VillageID = TD.ActiveDid });
            }
        }

        private void doFetchVAllDetails(object o)
        {
        	int VillageID = (int)o;
        	TD.Villages[VillageID].isVillageInitialized = 1;
        	doFetchVBuilding(o);
        	doFetchVUpgrade(o);
        	doFetchVDestroy(o);
        	doFetchVMarket(o);
        	doFetchVTroop(o);
        	TD.Villages[VillageID].isVillageInitialized = 2;
        }
        
        private void doFetchVBuilding(object o)
        {
            lock (Level2Lock)
            {
                int VillageID = (int)o;
                TD.Villages[VillageID].isBuildingInitialized = 1;
                TD.Villages[VillageID].Buildings = new SortedDictionary<int, TBuilding>();
                PageQuery(VillageID, "dorf1.php");			//	资源田
                PageQuery(VillageID, "dorf2.php");			//	内城建筑
                TD.Dirty = true;

                TD.Villages[VillageID].isBuildingInitialized = 2;
                StatusUpdate(this, new StatusChanged() { ChangedData = ChangedType.Buildings, VillageID = VillageID });

                TD.Dirty = true;
            }
        }

        private void doFetchVUpgrade(object o)
        {
            lock (Level2Lock)
            {
                int VillageID = (int)o;
                TD.Villages[VillageID].isUpgradeInitialized = 1;
                PageQuery(VillageID, "build.php?gid=13");	//	铁匠铺
                PageQuery(VillageID, "build.php?gid=22");	//	研究院
                PageQuery(VillageID, "build.php?gid=24");	//	市政厅
                TD.Villages[VillageID].isUpgradeInitialized = 2;
                StatusUpdate(this, new StatusChanged() { ChangedData = ChangedType.Research, VillageID = VillageID });
                TD.Dirty = true;
            }
        }
        
        private void doFetchVDestroy(object o)
        {
            lock (Level2Lock)
            {
                int VillageID = (int)o;
                TD.Villages[VillageID].isDestroyInitialized = 1;
                PageQuery(VillageID, "build.php?gid=15");	//	中心大楼
                TD.Villages[VillageID].isDestroyInitialized = 2;
                TD.Dirty = true;
            }
        }
        
        private void doFetchVMarket(object o)
        {
            lock (Level2Lock)
            {
                int VillageID = (int)o;
                TD.Villages[VillageID].isMarketInitialized = 1;
                PageQuery(VillageID, "build.php?gid=17");	//	市场
                TD.Villages[VillageID].isMarketInitialized = 2;
                TD.Dirty = true;
            }
        }
        
        private void doFetchVTroop(object o)
        {
        	//	等待建筑完成初始化，才进行解析部队
        	DateTime start_time = DateTime.Now;
        	while (true)
        	{
        		int VillageID = (int)o;
        		if (TD.Villages[VillageID].isBuildingInitialized == 2)
        		{
        			break;
        		}
        		
        		if (DateTime.Now.Ticks - start_time.Ticks > (long)50000000)
        		{
        			DebugLog("村庄的建筑未初始化完成，无法解析部队，超时退出！",DebugLevel.E);
        			return;
        		}
        	}
        	
            lock (Level2Lock)
            {
                int VillageID = (int)o;
                TD.Villages[VillageID].isTroopInitialized = 1;
                PageQuery(VillageID, "build.php?gid=16");	//	集结点
                PageQuery(VillageID, "build.php?gid=19");	//	兵营
                PageQuery(VillageID, "build.php?gid=20");	//	马厩
                PageQuery(VillageID, "build.php?gid=21");	//	工场
                TD.Villages[VillageID].isTroopInitialized = 2;
                TD.Dirty = true;
            }
        }
        
        private void doFetchVTroopAll(object o)
        {
            lock (Level2Lock)
            {
                int VillageID = (int)o;
                TD.Villages[VillageID].isTroopInitialized = 1;
                string data = PageQuery(VillageID, "build.php?gid=16", null, true, true);	//	集结点

                if (string.IsNullOrEmpty(data))
                    return;

                Regex reg = new Regex("<p class=\"switch\"><a href=\"(build.php\\?id=39&k)\">");
                Match m = reg.Match(data);
                if (m.Success)
                {
                    PageQuery(VillageID, m.Groups[1].Value);
                }
                else
                {
                    NewParseEntry(VillageID, data);
                }

                TD.Villages[VillageID].isTroopInitialized = 2;
                TD.Dirty = true;
            }
        }
        
    }
}
