/*
 * Created by SharpDevelop.
 * User: Administrator
 * Date: 2011-4-21
 * Time: 16:06
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Globalization;

namespace libTravian
{
	public class FindOasisOption
	{
		public int VillageID { get; set; }
		public int axis_x { get; set; }
		public int axis_y { get; set; }
		public int search_num { get; set; }
	}

	public class OasisFoundLogArgs : EventArgs
	{
		public string arg_log { get; set; }
	}
	
	public class SearchingRaidTargetOption
	{
		public int VillageID { get; set; }
		public int Range { get; set; }
		public int Population { get; set; }
	}
	
	public class RaidTargetFoundLogArgs : EventArgs
	{
		public string arg_log { get; set; }
	}
	
	public class RaidTargetInfo
    {
    	public TPoint loc_pt;
    	public int type;
    	public int population;
    	public string race;
    	
    	public override string ToString()
		{
			return (loc_pt.X.ToString(CultureInfo.CurrentCulture) + "|" 
    		        + loc_pt.Y.ToString(CultureInfo.CurrentCulture)
    		        + (type == 1 ? "; 绿洲":"; 死羊; 人口：" + population
    		        + " " + race));
		}
    }
	
	public class RaidTargetListArgs : EventArgs
	{
		public List<RaidTargetInfo> info_lst { get; set; }
	}
	
	public class FindAnimalsOption
	{
		public int VillageID { get; set; }
		public int Range { get; set; }
		public int AxisX { get; set; }
		public int AxisY { get; set; }
		public List<int> lstIncl = new List<int>();
		public List<int> lstExcl = new List<int>();
	}
	
	public class AnimalsFoundLogArgs : EventArgs
	{
		public string arg_log { get; set; }
	}
	
	public class AnimalsInfo
	{
		public TPoint loc_pt;
		public int[] Troops = new int[10];
		public string FriendlyName
		{
			get
	        {
	            StringBuilder sb = new StringBuilder();
	            for (int i = 0; i < Troops.Length; i++)
	            {
	                if (Troops[i] != 0)
	                {
	                    string troopName = String.Format("T{0}", i + 1);
	                    if (DisplayLang.Instance != null)
	                    {
	                        troopName = DisplayLang.Instance.GetAidLang(4, i + 1);
	                    }
	
	                    sb.AppendFormat("{0} {1}, ", troopName, Troops[i]);
	                }
	            }
	            if (sb.Length > 2)
	                sb.Remove(sb.Length - 2, 2);
	            else
	                return "None";
	            return sb.ToString();
	        }
		}
	}
	
	public class AnimalsInfoArgs : EventArgs
	{
		public AnimalsInfo info { get; set; }
	}
	
	partial class Travian
	{
		//	搜田功能
		
		public event EventHandler<OasisFoundLogArgs> OnOasisFoundLog;
		public Thread ThrdFindOasis;
		
		private void OasisFoundLog(string log)
		{
			if (this.OnOasisFoundLog != null)
			{
				OnOasisFoundLog(this, new OasisFoundLogArgs {arg_log = log});
			}
		}
		
        private void doFindOasis(object o)
        {
        	lock (Level2Lock)
            {
        		FindOasisOption to = o as FindOasisOption;
                int VillageID = to.VillageID;
                TD.Villages[VillageID].OasisInfo.Clear();
				TD.Villages[VillageID].isOasisFoundComplete = false;
					
                int axis_x, axis_y, total_times;
                axis_x = to.axis_x;
                axis_y = to.axis_y;
                total_times = to.search_num;
                
                string data;
                data = FetchBlockMap(VillageID, axis_x, axis_y);
				OasisFoundLog("[" + 1 + " / " + 1 + "] 搜索以(" 
                              + axis_x + "|" + axis_y + ")为中心的地图块");
                
                for (int i = 1; i <= total_times; i++)
                {
                	OasisFoundLog("正在进行第" + i + "重扫描：");
                	for (int j = 0; j < i; j++)
                	{
                		if (i % 2 == 1)
                		{
                			axis_y = CalcAxisTran(axis_y, 9);
                			OasisFoundLog("[" + (j + 1) + " / " + (2 * i) + "] 搜索以("
                			              + axis_x + "|" + axis_y + ")为中心的地图块");
                			data = FetchBlockMap(VillageID, axis_x, axis_y);
                		}
                		else
                		{
                			axis_y = CalcAxisTran(axis_y, -9);
                			OasisFoundLog("[" + (j + 1) + " / " + (2 * i) + "] 搜索以(" 
                			              + axis_x + "|" + axis_y + ")为中心的地图块");
                			data = FetchBlockMap(VillageID, axis_x, axis_y);
                		}
                	}
                		
                	for (int j = 0; j < i; j++)
                	{
                		if (i % 2 == 1)
                		{
                			axis_x = CalcAxisTran(axis_x, 11);
                			OasisFoundLog("[" + (i + j + 1) + " / " + (2 * i) + "] 搜索以(" 
                			              + axis_x + "|" + axis_y + ")为中心的地图块");
                			data = FetchBlockMap(VillageID, axis_x, axis_y);
                		}
                		else
                		{
                			axis_x = CalcAxisTran(axis_x, -11);
                			OasisFoundLog("[" + (i + j + 1) + " / " + (2 * i) + "] 搜索以(" 
                			              + axis_x + "|" + axis_y + ")为中心的地图块");
                			data = FetchBlockMap(VillageID, axis_x, axis_y);
                		}
                	}
                }
                
                int max_cnt = TD.Villages[VillageID].OasisInfo.Count;
                OasisFoundLog("总共扫描到" + max_cnt + "个１５田。");
                TD.Villages[VillageID].isOasisFoundComplete = true;
                
                int cnt = 0;
                foreach (var oas in TD.Villages[VillageID].OasisInfo)
                {
                	cnt ++;
                	TOasisInfo oasis = oas as TOasisInfo;
                	OasisFoundLog("[" + cnt + " / " + max_cnt + "] 搜索以(" 
                	              + oasis.axis_x + "|" + oasis.axis_y + ")为中心的地图块");
                	data = FetchBlockMap(VillageID, oasis.axis_x, oasis.axis_y);
                	oasis.addon = GetOasisAddon(data, oasis.axis_x, oasis.axis_y);
                }
                OasisFoundLog("打印搜索结果。");
                StatusUpdate(this, new StatusChanged() { ChangedData = ChangedType.OasisFound, VillageID = VillageID });
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
        		return axis += 801;
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
        		                "\"d\":\\-?\\d+,\"c\":\"{([^}]*?)}\",\"t\":\"[^\\)]*?\\)<\\\\/span><\\\\/span>([^\"]*?)\"");
        	
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
        
        //	搜索抢劫目标功能
        
        public event EventHandler<RaidTargetFoundLogArgs> OnRaidTargetFoundLog;
        private void RaidTargetFoundLog(string log)
		{
			if (this.OnRaidTargetFoundLog != null)
			{
				OnRaidTargetFoundLog(this, new RaidTargetFoundLogArgs {arg_log = log});
			}
		}
        
        private void doFindRaidTargets(object o)
        {
        	lock (Level2Lock)
        	{
	        	SearchingRaidTargetOption search_option = o as SearchingRaidTargetOption;
	        	int VillageID = search_option.VillageID;
	        	int Range = search_option.Range;
	        	int popu_limit = search_option.Population;
                
	        	int axis_x = TD.Villages[VillageID].Coord.X;
	        	int axis_y = TD.Villages[VillageID].Coord.Y;
	        	RaidTargetFoundLog("开始以村子【" + TD.Villages[VillageID].Name 
	        	                   + "("  + axis_x + "|" + axis_y
	        	                   + ")】为中心，在" + Range + "范围内查找绿洲和人口低于"
	        	                   + popu_limit + "的死羊。");
	        	
	        	List<RaidTargetInfo> info_lst = new List<RaidTargetInfo>();
	        	RaidTargetFoundLog("[" + 1 + " / " + 1 + "] 搜索以(" 
	        	                   + axis_x + "|" + axis_y + ")为中心的地图块");
	        	string data = FetchBlockMap(VillageID, axis_x, axis_y);
				if (data != null)
        		{
        			ParseRaidTarget(VillageID, data, info_lst, popu_limit);
        		}
				
                for (int i = 1; i <= Range; i++)
                {
                	RaidTargetFoundLog("正在进行第" + i + "重扫描：");
                	for (int j = 0; j < i; j++)
                	{
                		if (i % 2 == 1)
                		{
                			axis_y = CalcAxisTran(axis_y, 9);
                			RaidTargetFoundLog("[" + (j + 1) + " / " + (2 * i) + "] 搜索以(" 
                			                   + axis_x + "|" + axis_y + ")为中心的地图块");
                			data = FetchBlockMap(VillageID, axis_x, axis_y);
                		}
                		else
                		{
                			axis_y = CalcAxisTran(axis_y, -9);
                			RaidTargetFoundLog("[" + (j + 1) + " / " + (2 * i) + "] 搜索以(" 
                			                   + axis_x + "|" + axis_y + ")为中心的地图块");
                			data = FetchBlockMap(VillageID, axis_x, axis_y);
                		}
                		
                		if (data != null)
                		{
                			ParseRaidTarget(VillageID, data, info_lst, popu_limit);
                		}
                	}
                		
                	for (int j = 0; j < i; j++)
                	{
                		if (i % 2 == 1)
                		{
                			axis_x = CalcAxisTran(axis_x, 11);
                			RaidTargetFoundLog("[" + (i + j + 1) + " / " + (2 * i) + "] 搜索以(" 
                			                   + axis_x + "|" + axis_y + ")为中心的地图块");
                			data = FetchBlockMap(VillageID, axis_x, axis_y);
                		}
                		else
                		{
                			axis_x = CalcAxisTran(axis_x, -11);
                			RaidTargetFoundLog("[" + (i + j + 1) + " / " + (2 * i) + "] 搜索以(" 
                			                   + axis_x + "|" + axis_y + ")为中心的地图块");
                			data = FetchBlockMap(VillageID, axis_x, axis_y);
                		}
                		
                		if (data != null)
                		{
                			ParseRaidTarget(VillageID, data, info_lst, popu_limit);
                		}
                	}
                }
                
                RaidTargetFoundLog("共搜索到" + info_lst.Count + "个目标。");
                
                CallRaidTargetsListUpdate(info_lst);
        	}
        }
        
        public bool NoAnimals(int VillageID, int axis_x, int axis_y)
        {
        	Dictionary<string, string> PostData = new Dictionary<string, string>(3);
        	PostData["cmd"] = "viewTileDetails";
        	PostData["x"] = axis_x.ToString();
			PostData["y"] = axis_y.ToString();

			string data = PageQuery(VillageID, "/ajax.php?cmd=viewTileDetails", PostData);
			
			if (data == null)
				return false;
			
			Match m = Regex.Match(data, "unit u(\\d+)");
			if (m.Success)
				return false;
			
			return true;
        }
        
        private void ParseRaidTarget(int VillageID, string data, List<RaidTargetInfo> info_lst, int popu_limit)
        {
        	Match m = Regex.Match(data, "\"error\":false,\"errorMsg\":null,\"data\":{\"tiles\":" +
    	                      "\\[(.*?\\])}}");
        	if (!m.Success)
        		return;
        	
        	MatchCollection mc_cell;
            mc_cell = Regex.Matches(m.Groups[1].Value, "{(.*?)}[,\\]]");
        	
            if(mc_cell.Count != 99)
            {
            	RaidTargetFoundLog("本次搜索只返回" + mc_cell.Count + "组数据。");
            	return;
            }
            
            int axis_x, axis_y, popul;
            string cell, resource_info;
            MatchCollection mc_res;
            Match m1, m2;
            foreach (Match pm_cell in mc_cell)
        	{         	
        		cell = pm_cell.Groups[1].Value;
        		m1 = Regex.Match(cell, "\"x\":\"(\\-?\\d+)\",\"y\":\"(\\-?\\d+)\"," +
        		                "\"d\":\\-?\\d+,\"c\":\"{([^}]*?)}\",\"t\":\"[^\\)]*?\\)<\\\\/span><\\\\/span>([^\"]*?)\"");
        	
        		m2 = Regex.Match(cell, "\"x\":\"(\\-?\\d+)\",\"y\":\"(\\-?\\d+)\"," + 
        		                ".*?\"c\":\"{([^}]*?)}.*?{k\\.einwohner}\\s(\\d+)" + 
        		                ".*?{k\\.allianz}\\s([^<]*?)<br\\s\\\\/>" +
        		                "{k.volk}\\s{([^}]*?)}\"", RegexOptions.Singleline);
        		if(m1.Success)
        		{
	        		axis_x = Convert.ToInt32(m1.Groups[1].Value);
	        		axis_y = Convert.ToInt32(m1.Groups[2].Value);
	        		if (m1.Groups[3].Value != "k.fo")
	        			continue;
	        		
	        		resource_info = m1.Groups[4].Value;
	        		mc_res = Regex.Matches(resource_info, "<.*?>{[^}]*?}\\s{([^}]*?)}\\s(\\d+)%");
	        		
	        		if (mc_res.Count == 2 || mc_res.Count == 1 
	        		    && Convert.ToInt32(mc_res[0].Groups[2].Value) == 50)
	        		{
	        			if (!NoAnimals(VillageID, axis_x, axis_y))
	        				continue;
	        			
	        			RaidTargetInfo info = new RaidTargetInfo()
	        			{
	        				loc_pt = new TPoint(axis_x, axis_y),
	        				type = 1,
	        				population = 0,
	        				race = ""
	        			};
	        			info_lst.Add(info);
	        		}
        		}
        		else if (m2.Success)
        		{
        			axis_x = Convert.ToInt32(m2.Groups[1].Value);
	        		axis_y = Convert.ToInt32(m2.Groups[2].Value);
	        		if (m2.Groups[3].Value != "k.dt")
	        			continue;
	        		popul = Convert.ToInt32(m2.Groups[4].Value);
	        		//	不攻击人口高于预设的
	        		if (popul > popu_limit)
	        			continue;
	        		//	不攻击有联盟的
	        		if (m2.Groups[5].Value.Length > 0)
	        			continue;

	        		RaidTargetInfo info = new RaidTargetInfo()
        			{
        				loc_pt = new TPoint(axis_x, axis_y),
        				type = 2,
        				population = popul
        			};
	        		if (m2.Groups[6].Value == "a.v1")
	        			info.race = "罗马";
	        		else if (m2.Groups[6].Value == "a.v2")
	        			info.race = "日族";
	        		else if (m2.Groups[6].Value == "a.v3")
	        			info.race = "高卢";
	        		else
	        			info.race = "纳塔";
        			info_lst.Add(info);
        		}
        	}
        }
        
        public event EventHandler<RaidTargetListArgs> OnRaidTargetListUpdate;
        private void CallRaidTargetsListUpdate(List<RaidTargetInfo> info_lst)
		{
			if (this.OnRaidTargetListUpdate != null)
			{
				OnRaidTargetListUpdate(this, new RaidTargetListArgs {info_lst = info_lst});
			}
		}
        
        
        //	搜索野兽功能
        public Thread ThrdFindAnimals;
        
        public event EventHandler<AnimalsFoundLogArgs> OnAnimalsFoundLog;
        private void AnimalsFoundLog(string log)
		{
			if (this.OnAnimalsFoundLog != null)
			{
				OnAnimalsFoundLog(this, new AnimalsFoundLogArgs {arg_log = log});
			}
		}
        
        private void doFindAnimals(object o)
        {
        	lock (Level2Lock)
        	{
        		FindAnimalsOption option = o as FindAnimalsOption;
        		int VillageID = option.VillageID;
        		int Range = option.Range;
        		int axis_x = option.AxisX;
        		int axis_y = option.AxisY;
        		List<int> lstIncl = option.lstIncl;
        		List<int> lstExcl = option.lstExcl;
        		
        		AnimalsFoundLog("开始以村子【" + TD.Villages[VillageID].Name 
	        	                   + "("  + axis_x + "|" + axis_x
	        	                   + ")】为中心，在" + Range + "范围内搜索野兽。");
        		
        		AnimalsFoundLog("[" + 1 + " / " + 1 + "] 搜索以(" 
	        	                   + axis_x + "|" + axis_y + ")为中心的地图块");
	        	string data = FetchBlockMap(VillageID, axis_x, axis_y);
				if (data != null)
        		{
        			ParseAnimalAreas(VillageID, data, lstIncl, lstExcl);
        		}
				
				for (int i = 1; i <= Range; i++)
                {
                	AnimalsFoundLog("正在进行第" + i + "重扫描：");
                	for (int j = 0; j < i; j++)
                	{
                		if (i % 2 == 1)
                		{
                			axis_y = CalcAxisTran(axis_y, 9);
                			AnimalsFoundLog("[" + (j + 1) + " / " + (2 * i) + "] 搜索以(" 
                			                   + axis_x + "|" + axis_y + ")为中心的地图块");
                			data = FetchBlockMap(VillageID, axis_x, axis_y);
                		}
                		else
                		{
                			axis_y = CalcAxisTran(axis_y, -9);
                			AnimalsFoundLog("[" + (j + 1) + " / " + (2 * i) + "] 搜索以(" 
                			                   + axis_x + "|" + axis_y + ")为中心的地图块");
                			data = FetchBlockMap(VillageID, axis_x, axis_y);
                		}
                		
                		if (data != null)
                		{
                			ParseAnimalAreas(VillageID, data, lstIncl, lstExcl);
                		}
                	}
                		
                	for (int j = 0; j < i; j++)
                	{
                		if (i % 2 == 1)
                		{
                			axis_x = CalcAxisTran(axis_x, 11);
                			AnimalsFoundLog("[" + (i + j + 1) + " / " + (2 * i) + "] 搜索以(" 
                			                   + axis_x + "|" + axis_y + ")为中心的地图块");
                			data = FetchBlockMap(VillageID, axis_x, axis_y);
                		}
                		else
                		{
                			axis_x = CalcAxisTran(axis_x, -11);
                			AnimalsFoundLog("[" + (i + j + 1) + " / " + (2 * i) + "] 搜索以(" 
                			                   + axis_x + "|" + axis_y + ")为中心的地图块");
                			data = FetchBlockMap(VillageID, axis_x, axis_y);
                		}
                		
                		if (data != null)
                		{
                			ParseAnimalAreas(VillageID, data, lstIncl, lstExcl);
                		}
                	}
                }
        	}
        }
        
        private void ParseAnimalAreas(int VillageID, string data,
                                      List<int> lstIncl, List<int> lstExcl)
        {
        	Match m = Regex.Match(data, "\"error\":false,\"errorMsg\":null,\"data\":{\"tiles\":" +
    	                      "\\[(.*?\\])}}");
        	if (!m.Success)
        		return;
        	
        	MatchCollection mc_cell;
            mc_cell = Regex.Matches(m.Groups[1].Value, "{(.*?)}[,\\]]");
        	
            if(mc_cell.Count != 99)
            {
            	RaidTargetFoundLog("本次搜索只返回" + mc_cell.Count + "组数据。");
            	return;
            }
            
            int axis_x, axis_y;
            string cell;
            foreach (Match pm_cell in mc_cell)
        	{         	
        		cell = pm_cell.Groups[1].Value;
        		m = Regex.Match(cell, "\"x\":\"(\\-?\\d+)\",\"y\":\"(\\-?\\d+)\"," +
        		                "\"d\":\\-?\\d+,\"c\":\"{([^}]*?)}\",\"t\":\"[^\\)]*?\\)<\\\\/span><\\\\/span>([^\"]*?)\"");
        	
        		if(!m.Success)
					continue;
        		
        		axis_x = Convert.ToInt32(m.Groups[1].Value);
        		axis_y = Convert.ToInt32(m.Groups[2].Value);
        		if (m.Groups[3].Value != "k.fo")
        			continue;
        		
	        	ParseAnimals(VillageID, axis_x, axis_y, lstIncl, lstExcl);
        	}
        }
        
        private void ParseAnimals(int VillageID, int axis_x, int axis_y,
                                      List<int> lstIncl, List<int> lstExcl)
        {
        	Dictionary<string, string> PostData = new Dictionary<string, string>(3);
        	PostData["cmd"] = "viewTileDetails";
        	PostData["x"] = axis_x.ToString();
			PostData["y"] = axis_y.ToString();

			string data = PageQuery(VillageID, "/ajax.php?cmd=viewTileDetails", PostData);
			
			if (data == null)
				return;
			
			MatchCollection mc = Regex.Matches(
				data, "unit u(\\d+).*?<td\\sclass=[^\\d]*?(\\d+)",
				RegexOptions.Singleline);
			if (mc.Count == 0)
				return;
			foreach (int aid in lstExcl)
			{
				Match m = Regex.Match(data, "unit u" + aid, RegexOptions.Singleline);
				if (m.Success)
					return;
			}
			
			foreach (int aid in lstIncl)
			{
				Match m = Regex.Match(data, "unit u" + aid, RegexOptions.Singleline);
				if (m.Success)
					break;
			}
			
			AnimalsInfo info = new AnimalsInfo()
			{
				loc_pt = new TPoint(axis_x, axis_y)
			};
			foreach (Match m in mc)
			{
				int aid = Convert.ToInt32(m.Groups[1].Value) - 30;
				int ammount = Convert.ToInt32(m.Groups[2].Value);
				if (aid - 1 >= 0 && aid - 1 < info.Troops.Length)
					info.Troops[aid - 1] = ammount;
			}
			CallAnimalsInfoUpdate(info);
        }
        
        public event EventHandler<AnimalsInfoArgs> OnAnimalsInfoUpdate;
        private void CallAnimalsInfoUpdate(AnimalsInfo info)
		{
			if (this.OnAnimalsInfoUpdate != null)
			{
				OnAnimalsInfoUpdate(this, new AnimalsInfoArgs {info = info});
			}
		}
	}
}