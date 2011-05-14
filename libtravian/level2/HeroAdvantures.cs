/*
 * Created by SharpDevelop.
 * User: Administrator
 * Date: 2011-5-1
 * Time: 11:18
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace libTravian
{
	public class HeroAdvantureOption
	{
		public int VillageID { get; set; }
		public int Key { get; set; }
	}


	partial class Travian
	{
		private void doFetchHeroAdventures(object o)
        {
        	lock (Level2Lock)
            {
                int VillageID = (int)o;
                string data = PageQuery(VillageID, "hero_inventory.php");	//	查询英雄状态
                
                if (string.IsNullOrEmpty(data))
                    return;
                
                string hero_status = HtmlUtility.GetElementWithClass(
                	data, "div", "attribute heroStatus");
                Match m = Regex.Match(hero_status, "karte.php\\?d=(\\d+)");
                int hero_loc = 0;
                if (m.Success)
                {
                	int z = Convert.ToInt32(m.Groups[1].Value);
                	foreach (var x in TD.Villages)
                	{
                		TVillage v = x.Value;
                		if (v.Z == z)
                		{
                			hero_loc = x.Key;
                			TD.Adv_Sta.HeroLocate = hero_loc;
                			break;
                		}
                	}
                }
                else
                {
                	hero_loc = (TD.Adv_Sta.HeroLocate == 0 ? VillageID : TD.Adv_Sta.HeroLocate);
                }
                
                data = PageQuery(hero_loc, "hero_adventure.php");	//	查询探险地点
                string[] places = HtmlUtility.GetElements(data, "tr");
                if (places.Length <= 1)
                	return;
				
                int coord_x, coord_y;
                string dur, dgr, lnk;
                DateTime fin;
                TD.Adv_Sta.HeroAdventures.Clear();
                for (int i = 1; i < places.Length; i++)
                {
                	//	坐标
                	string coords = HtmlUtility.GetElementWithClass(
                		places[i], "td", "coords");
                	if (coords == null)
                		continue;
                	m = Regex.Match(coords, "karte.php\\?x=(\\-?\\d+)&amp;y=(\\-?\\d+)");
                	if (!m.Success)
                		continue;
                	coord_x = Convert.ToInt32(m.Groups[1].Value);
                	coord_y = Convert.ToInt32(m.Groups[2].Value);
                	
                	//	持续时间
                	string move_time = HtmlUtility.GetElementWithClass(
                		places[i], "td", "moveTime");
                	if (move_time == null)
                		continue;
                	m = Regex.Match(move_time, "\\d+:\\d+:\\d+");
                	if (!m.Success)
                		continue;
                	dur = m.Groups[0].Value;
                	
                	//	难度
                	string difficulty = HtmlUtility.GetElementWithClass(
                		places[i], "td", "difficulty");
                	if (difficulty == null)
                		continue;
                	m = Regex.Match(difficulty, "alt=\"([^\"]*?)\"");
                	if (!m.Success)
                		continue;
                	dgr = m.Groups[1].Value;
                	
                	//	难度
                	string timeLeft = HtmlUtility.GetElementWithClass(
                		places[i], "td", "timeLeft");
                	if (timeLeft == null)
                		continue;
                	m = Regex.Match(timeLeft, "\\d+:\\d+:\\d+");
                	if (!m.Success)
                		continue;
                	fin = DateTime.Now.Add(TimeSpanParse(m.Groups[0].Value));
                	
                	//	链接
                	string goTo = HtmlUtility.GetElementWithClass(
                		places[i], "td", "goTo");
                	if (goTo == null)
                		continue;
                	m = Regex.Match(goTo, "href=\"([^\"]*?)\"");
                	if (!m.Success)
                		continue;
                	lnk = m.Groups[1].Value;
                	
                	//	增加新的探险地点
                	HeroAdventureInfo adv_info = new HeroAdventureInfo()
                	{
                		axis_x = coord_x,
                		axis_y = coord_y,
                		duration = dur,
                		danger = dgr,
                		finish_time = fin,
                		link = lnk
                	};
                	TD.Adv_Sta.HeroAdventures.Add(adv_info);
                }
                
                TD.Dirty = true;
        	}
        }
		
		private void doHeroAdventure(object o)
		{
			lock (Level2Lock)
            {
				int HeroLoc = TD.Adv_Sta.HeroLocate;
				int Key = (int)o;

				TPoint tp = new TPoint(TD.Adv_Sta.HeroAdventures[Key].axis_x, TD.Adv_Sta.HeroAdventures[Key].axis_y);
				string data = PageQuery(HeroLoc, "a2b.php?id=" + tp.Z.ToString() + "&h=1");
				
                Match m_test = Regex.Match(data, "type=\"submit\" value=\"ok\" name=\"h1\"");
                if (!m_test.Success)
                {
                	DebugLog("英雄目前还无法进行探险！", DebugLevel.II);
					return;
                }
				Dictionary<string, string> PostData = new Dictionary<string, string>();
				MatchCollection mc = Regex.Matches(
					data, "<input type=\"hidden\" name=\"([^\"]*?)\" value=\"([^\"]*?)\" />");
				string key, val;
				foreach (Match m in mc)
				{
					key = m.Groups[1].Value;
					val = m.Groups[2].Value;
					PostData[key] = val;
				}
				PostData["h1"] = "ok";
				PageQuery(HeroLoc, "a2b.php", PostData);
			}
		}
	}
}