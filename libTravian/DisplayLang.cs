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
using System.IO;
using System.Text;

namespace libTravian
{
    // 多国语言接口
    public class DisplayLang
    {
    	//	静态实例
        static public DisplayLang Instance;
        
        public Dictionary<int, string> GidLang;	//	建筑
        public Dictionary<int, string> AidLang;	//	兵种
        public List<string> AtkLang;				//	攻击
        public List<string> RaidLang;				//	抢夺
        public Dictionary<string, string> Tags;	//	

        public string Auther {get; private set;}	//	
        
        public string GetGidLang(int Gid)
        {
            if (GidLang.ContainsKey(Gid))
                return GidLang[Gid];
            else
                return "?";
        }
        
        public string GetAidLang(int Tribe, int Aid)
        {
            int key = (Tribe - 1) * 11 + Aid;
            if (AidLang.ContainsKey(key))
                return AidLang[key];
            else
                return "?";
        }
        
        public void SetAidLang(int Tribe, int Aid, string Value)
        {
            int key = (Tribe - 1) * 11 + Aid;
            AidLang[key] = Value;
        }
        
        public DisplayLang(string language)
        {
            GidLang = new Dictionary<int, string>(40);
            AidLang = new Dictionary<int, string>(33);
            AtkLang = new List<string>();
            RaidLang = new List<string>();
            Tags = new Dictionary<string, string>();
            
            string lang_file = string.Format("lang\\svr_{0}.txt", language);
            if (!File.Exists(lang_file))
                lang_file = "lang\\svr_cn.txt";
            if (!File.Exists(lang_file))
                return;
            
            //	载入建筑、兵种的多国语言
            string[] s = File.ReadAllLines(lang_file, Encoding.UTF8);
            foreach (var s1 in s)
            {
                var pairs = s1.Split('=');
                if (pairs.Length != 2)
                    continue;
                
                //	建筑以gid开头
                if (pairs[0] == "gid")
                {
                    try
                    {
                        string[] data = pairs[1].Split(',');
                        for (int i = 0; i < data.Length; i++)
                            GidLang[i + 1] = data[i];
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }
                //	兵种以aid开头
                else if (pairs[0].StartsWith("aid"))
                {
                    try
                    {
                        int Tribe = Convert.ToInt32(pairs[0].Substring(3));
                        string[] data = pairs[1].Split(',');
                        for (int i = 0; i < data.Length; i++)
                            SetAidLang(Tribe, i + 1, data[i]);
                    }
                    catch (Exception)
                    {
                        continue;
                    }
            	}
                //	攻击以attack开头
                else if (pairs[0].StartsWith("attack"))
                {
                    try
                    {
                        string[] data = pairs[1].Split(',');
                        for (int i = 0; i < data.Length; i++)
                        {
                        	AtkLang.Add(data[i]);
                        }
                    }
                    catch (Exception)
                    {
                        continue;
                    }
            	}
                //	抢夺以raid开头
                else if (pairs[0].StartsWith("raid"))
                {
                    try
                    {
                        string[] data = pairs[1].Split(',');
                        for (int i = 0; i < data.Length; i++)
                        {
                        	RaidLang.Add(data[i]);
                        }
                    }
                    catch (Exception)
                    {
                        continue;
                    }
            	}
                else
                {
                    Tags.Add(pairs[0], pairs[1]);
                }
            }
        }
    }
}
