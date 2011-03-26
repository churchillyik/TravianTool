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
using LitJson;

namespace libTravian
{
	partial class Travian
	{
		[Json]
		public Dictionary<int, string> GidLang = new Dictionary<int, string>(40);
		[Json]
		public Dictionary<int, string> AidLang = new Dictionary<int, string>(30);
		[Json]
		public string[] Market = new string[2];

		public bool Dirty;

		public string GetGidLang(int Gid)
		{
			if(GidLang.ContainsKey(Gid))
				return GidLang[Gid];
			else
				return "?";
		}
		public void SetGidLang(int Gid, string Value)
		{
			if(GidLang.ContainsKey(Gid) && GidLang[Gid] == Value)
				return;
			GidLang[Gid] = Value;
			Dirty = true;

			//TODO:DB.Instance.SetString(TD.Server, "gid" + Gid.ToString(), Value);
		}
		public string GetAidLang(int Tribe, int Aid)
		{
			int key = (Tribe - 1) * 10 + Aid;
			if(AidLang.ContainsKey(key))
				return AidLang[key];
			else
				return "?";
		}
		public void SetAidLang(int Tribe, int Aid, string Value)
		{
			int key = (Tribe - 1) * 10 + Aid;
			AidLang[key] = Value;
			Dirty = true;
		}
		public void SetAidLang(int Aid, string Value)
		{
			if(AidLang.ContainsKey(Aid) && AidLang[Aid] == Value)
				return;
			AidLang[Aid] = Value;
			Dirty = true;
			//TODO:DB.Instance.SetString(TD.Server, "aid" + Aid.ToString(), Value);
		}
	}
}
