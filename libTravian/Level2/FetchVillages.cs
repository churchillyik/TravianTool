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
        	TD.Villages[VillageID].isVillageInitializing = false;
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
				
                if (TD.Villages.ContainsKey(VillageID))
                {
                	TD.Villages[VillageID].isBuildingInitialized = 2;
                }
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
                CheckBuildingExistAndQuery(VillageID, 13);	//	铁匠铺
                CheckBuildingExistAndQuery(VillageID, 22);	//	研究院
                CheckBuildingExistAndQuery(VillageID, 24);	//	市政厅
                if (TD.Villages.ContainsKey(VillageID))
                {
                	TD.Villages[VillageID].isUpgradeInitialized = 2;
                }
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
                CheckBuildingExistAndQuery(VillageID, 15);	//	中心大楼
                if (TD.Villages.ContainsKey(VillageID))
                {
                	TD.Villages[VillageID].isDestroyInitialized = 2;
                }
                TD.Dirty = true;
            }
        }
        
        private void doFetchVMarket(object o)
        {
            lock (Level2Lock)
            {
                int VillageID = (int)o;
                TD.Villages[VillageID].isMarketInitialized = 1;
                CheckBuildingExistAndQuery(VillageID, 17);	//	市场
                if (TD.Villages.ContainsKey(VillageID))
                {
                	TD.Villages[VillageID].isMarketInitialized = 2;
                }
                TD.Dirty = true;
            }
        }
        
        private void doFetchVTroop(object o)
        {
            lock (Level2Lock)
            {
                int VillageID = (int)o;
                TD.Villages[VillageID].isTroopInitialized = 1;
                CheckBuildingExistAndQuery(VillageID, 16);	//	集结点
                CheckBuildingExistAndQuery(VillageID, 19);	//	兵营
                CheckBuildingExistAndQuery(VillageID, 20);	//	马厩
                CheckBuildingExistAndQuery(VillageID, 21);	//	工场
                if (TD.Villages.ContainsKey(VillageID))
                {
                	TD.Villages[VillageID].isTroopInitialized = 2;
                }
                TD.Dirty = true;
            }
        }
        
        private void doFetchVTroopAll(object o)
        {
            lock (Level2Lock)
            {
                int VillageID = (int)o;
                TD.Villages[VillageID].isTroopInitialized = 1;
                string data = PageQuery(VillageID, "build.php?gid=16&tt=1", null, true, true);	//	集结点

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
				if (TD.Villages.ContainsKey(VillageID))
				{
                	TD.Villages[VillageID].isTroopInitialized = 2;
				}
                TD.Dirty = true;
            }
        }
        
        public string CheckBuildingExistAndQuery(int VillageID, int gid)
        {
        	int bid = -1;
        	if (TD.Villages[VillageID].isBuildingInitialized == 2)
        	{
	        	foreach (KeyValuePair<int, TBuilding> pair in TD.Villages[VillageID].Buildings)
	        	{
	        		TBuilding tb = pair.Value;
	        		if (tb.Gid == gid && tb.Level != 0)
	        			bid = pair.Key;
	        	}
        	}
        	
        	if (bid != -1)
        	{
        		if (gid == 17)
        		{
        			//	市场页面需要查询第二页
        			return PageQuery(VillageID, "build.php?gid=" + gid.ToString() + "&t=5&id=" + bid);
        		}
        		else if (gid == 16)
        		{
        			return PageQuery(VillageID, "build.php?gid=" + gid.ToString() + "&tt=1&id=" + bid);
        		}
        		else
        		{
        			return PageQuery(VillageID, "build.php?gid=" + gid.ToString() + "&id=" + bid);
        		}
        	}
        	else
        	{
        		string building_name = DisplayLang.Instance.GetGidLang(gid);
        		DebugLog("因找不到" + building_name
        		         + "(gid=" + gid.ToString() + ")，跳过抓取该建筑的网页。",
        		         DebugLevel.II);
        		return null;
        	}
        }
    }
}
