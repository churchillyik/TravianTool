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
 * Contributor(s): [MeteorRain], [jones125].
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Net;

namespace libTravian
{
	partial class Travian
	{
		private int UnixTime(DateTime time)
		{
			return Convert.ToInt32((DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds);
		}
		private bool Login()
		{
			string Username = TD.Username;
			string Password = TD.Password;
			if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
			{
				DebugLog("用户名和密码为空？", DebugLevel.F);
				return false;
			}
			try
			{
				//WriteInfo("Logging in as '" + Username + "', may take a few seconds...");
				string data = wc.DownloadString("/");
				if (!data.Contains("Travian"))
				{
					DebugLog("Cannot visit travian website!", DebugLevel.F);
					return false;
				}
				string userkey, passkey, alkey;
				Match m;
				m = Regex.Match(data, "type=\"text\" name=\"(\\S+?)\"");
				if (m.Success)
					userkey = m.Groups[1].Value;
				else
				{
					DebugLog("Parse userkey error!", DebugLevel.F);
					return false;
				}
				m = Regex.Match(data, "type=\"password\" name=\"(\\S+?)\"");
				if (m.Success)
					passkey = m.Groups[1].Value;
				else
				{
					DebugLog("Parse passkey error!", DebugLevel.F);
					return false;
				}
				MatchCollection ms = Regex.Matches(data, "<input type=\"hidden\" name=\"(\\S+?)\" value");
				if (ms.Count > 0)
					alkey = ms[ms.Count - 1].Groups[1].Value;
				else
				{
					DebugLog("Parse alkey error!", DebugLevel.F);
					return false;
				}
				Dictionary<string, string> PostData = new Dictionary<string, string>();
				PostData[userkey] = Username;
				PostData[passkey] = Password;
				PostData["w"] = @"1024:768";
				PostData["login"] = (UnixTime(DateTime.Now) - 10).ToString();
				//PostData[alkey] = "";
				PostData["s1.x"] = "0";
				PostData["s1.y"] = "0";

				string result = this.pageQuerier.PageQuery(0, "dorf1.php?ok", PostData, false, true);

				//if (result.Contains("login"))
				if (result == null || result.Contains("<span class=\"error\">"))
				{
					DebugLog("Username or Password error!", DebugLevel.F);
					//MessageBox.Show("Login failed.");
					return false;
				}

				m = Regex.Match(result, "spieler.php\\?uid=(\\d*)");
				if (m.Success)
					TD.UserID = Convert.ToInt32(m.Groups[1].Value);

				return true;
			}
			catch (Exception e)
			{
				DebugLog("扑捉到异常：" + e.ToString(), DebugLevel.F);
				DebugLog(e);
				return false;
			}
		}
	}
}
