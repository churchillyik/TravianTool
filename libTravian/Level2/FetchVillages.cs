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
        
        //	搜田功能
        private void doFindOasis(object o)
        {
        	lock (Level2Lock)
            {
                int VillageID = (int)o;
                TD.Villages[VillageID].isOasisFoundInitialized = 0;
                TD.Villages[VillageID].OasisInfo.Clear();
                
                Dictionary<string, string> Options = new Dictionary<string, string>();
                if(File.Exists("搜田设置"))
				{
					FileStream fs = new FileStream("搜田设置", FileMode.Open, FileAccess.Read);
					StreamReader sr = new StreamReader(fs, Encoding.UTF8);
					while(!sr.EndOfStream)
					{
						string[] opt = sr.ReadLine().Split('=');
						if(opt.Length == 2)
							Options.Add(opt[0], opt[1]);
					}
					sr.Close();
				}
                
                if (!Options.ContainsKey("center_axis_x")
                   || !Options.ContainsKey("center_axis_y")
                  || !Options.ContainsKey("total_times"))
                	return;
                
                string data;
                int axis_x, axis_y, total_times;
                axis_x = Convert.ToInt32(Options["center_axis_x"]);
                axis_y = Convert.ToInt32(Options["center_axis_y"]);
                total_times = Convert.ToInt32(Options["total_times"]);
                data = FetchBlockMap(VillageID, axis_x, axis_y);

                for (int i = 1; i <= total_times; i++)
                {
                	DebugLog("正在进行第" + i + "重扫描：", DebugLevel.II);
                	for (int j = 0; j < i; j++)
                	{
                		if (i % 2 == 1)
                		{
                			axis_y = CalcAxisTran(axis_y, 9);
                			data = FetchBlockMap(VillageID, axis_x, axis_y);
                		}
                		else
                		{
                			axis_y = CalcAxisTran(axis_y, -9);
                			data = FetchBlockMap(VillageID, axis_x, axis_y);
                		}
                	}
                		
                	for (int j = 0; j < i; j++)
                	{
                		if (i % 2 == 1)
                		{
                			axis_x = CalcAxisTran(axis_x, 11);
                			data = FetchBlockMap(VillageID, axis_x, axis_y);
                		}
                		else
                		{
                			axis_x = CalcAxisTran(axis_x, -11);
                			data = FetchBlockMap(VillageID, axis_x, axis_y);
                		}
                	}
                }
                
                DebugLog("总共扫描到" + TD.Villages[VillageID].OasisInfo.Count + "个１５田。", DebugLevel.II);
                
                TD.Villages[VillageID].isOasisFoundInitialized = 1;
                foreach (var oas in TD.Villages[VillageID].OasisInfo)
                {
                	TOasisInfo oasis = oas as TOasisInfo;
                	data = FetchBlockMap(VillageID, oasis.axis_x, oasis.axis_y);
                	oasis.addon = GetOasisAddon(data, oasis.axis_x, oasis.axis_y);
                }
                
                TD.Villages[VillageID].isOasisFoundInitialized = 2;
                StatusUpdate(this, new StatusChanged() { ChangedData = ChangedType.OasisFound, VillageID = VillageID });
                TD.Dirty = true;
            }
        }
        
        private string FetchBlockMap(int VillageID, int center_axis_x, int center_axis_y)
        {
        	Dictionary<string, string> PostData = new Dictionary<string, string>(4);
        	PostData["cmd"] = "mapPositionData";
        	PostData["data[x]"] = center_axis_x.ToString();
			PostData["data[y]"] = center_axis_y.ToString();
			PostData["data[zoomLevel]"] = "1";
			
			return PageQuery(VillageID, "/ajax.php?cmd=mapPositionData", PostData);
        }
        
        private int CalcAxisTran(int axis, int offset)
        {
        	axis += offset;
        	if (axis > 400)
        	{
        		return axis -= 801;
        	}
        	else if (axis < -400)
        	{
        		return axis +=801;
        	}
        	else
        	{
        		return axis;
        	}
        }
        
        private int CalcAxisOffset(int axis_1, int axis_2)
        {
        	if (Math.Abs(axis_1 - axis_2) <= 400)
        	{
        		return Math.Abs(axis_1 - axis_2);
        	}
        	else
        	{
        		return 801 - Math.Abs(axis_1 - axis_2);
        	}
        }
        
        private int GetOasisAddon(string data, int center_axis_x, int center_axis_y)
        {
        	if (data == null)
        		return 0;
        	
        	Match m = Regex.Match(data, "\"error\":false,\"errorMsg\":null,\"data\":{\"tiles\":" +
    	                      "\\[(.*?\\])}}");
        	if (!m.Success)
        		return 0;
        	
        	MatchCollection mc_cell;
            mc_cell = Regex.Matches(m.Groups[1].Value, "{(.*?)}[,\\]]");
        	
            if(mc_cell.Count != 99)
            {
            	DebugLog("本次搜索只返回" + mc_cell.Count + "组数据。", DebugLevel.E);
            	return 0;
            }
            
            int axis_x, axis_y, bigcnt, smallcnt, res_rate;
            string cell, resource_info, res_type;
            MatchCollection mc_res;
            
            bigcnt = 0;
        	smallcnt = 0;
        	foreach (Match pm_cell in mc_cell)
        	{         	
        		cell = pm_cell.Groups[1].Value;
        		m = Regex.Match(cell, "\"x\":\"(\\-?\\d+)\",\"y\":\"(\\-?\\d+)\"," +
        		                "\"d\":\\-?\\d+,\"c\":\"{([^}]*?)}\",\"t\":\"\\-?\\d+\\|\\-?\\d+([^\"]*?)\"");
        	
        		if(!m.Success)
        			continue;
        		
        		axis_x = Convert.ToInt32(m.Groups[1].Value);
        		axis_y = Convert.ToInt32(m.Groups[2].Value);
        		if (CalcAxisOffset(axis_x, center_axis_x) >= 4
        		    || CalcAxisOffset(axis_y, center_axis_y) >= 4
        		    || m.Groups[3].Value != "k.fo")
        			continue;
        		
        		resource_info = m.Groups[4].Value;
        		mc_res = Regex.Matches(resource_info, "<.*?>{[^}]*?}\\s{([^}]*?)}\\s(\\d+)%");
        		
        		
        		foreach (Match pm_res in mc_res)
        		{
        			res_type = pm_res.Groups[1].Value;
        			res_rate = Convert.ToInt32(pm_res.Groups[2].Value);
        			if (res_type == "a.r4" && res_rate == 50)
        			{
        				bigcnt++;
        			}
        			else if (res_type == "a.r4" && res_rate == 25)
        			{
        				smallcnt++;
        			}
        		}
        	}
        	
        	return Math.Min(bigcnt, 3) * 50 + Math.Min(smallcnt, 3 - Math.Min(bigcnt, 3)) * 25;
        	
        }
        
    }
}
