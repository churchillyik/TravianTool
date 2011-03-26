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
using System.Reflection;
using LitJson;

namespace libTravian
{
	// for saving server related data into cache database
	public class DB
	{
		static public DB Instance = new DB();
		private DB()
		{
		}

		string invalidchar = "\\/:*?\"<>|";
		public string GetKey(string Username, string Server)
		{
			string str = string.Format("{0}@{1}", Username, Server);
			foreach (var x in invalidchar)
			{
				str = str.Replace(x, '-');
			}
			return str;
		}

		public void Snapshot(Data data)
		{
			var fn = Filename(data.key);
			string jsondata = JsonMapper.ToJson(data);
			File.WriteAllText(fn, jsondata);
			data.Dirty = false;
		}

		public void Snapshot(Travian tr)
		{
			var fn = Filename(tr.TD.Server);
			string jsondata = JsonMapper.ToJson(tr);
			File.WriteAllText(fn, jsondata);
			tr.Dirty = false;
		}

		public Travian RestoreTravian(string key)
		{
			var fn = Filename(key);
			if(!File.Exists(fn))
				return null;
			string jsondata = File.ReadAllText(fn);
			return JsonMapper.ToObject<Travian>(jsondata);
		}

		public Data RestoreData(string key)
		{
			var fn = Filename(key);
			if(!File.Exists(fn))
				return null;
			string jsondata = File.ReadAllText(fn);
			return JsonMapper.ToObject<Data>(jsondata);
		}

		private string Filename(string key)
		{
			if(!Directory.Exists("db"))
				Directory.CreateDirectory("db");
			return "db\\" + key + ".json";
		}

	}

	public class JsonDataWrapper
	{		
		public string xData;
		public string[] xVillages;
	}
}
