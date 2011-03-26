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
using System.Collections;
using System.Net;
using System.Web;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace libTravian
{
	/// <summary>
	/// Abstract page query interface to enable unit test
	/// </summary>
	public interface IPageQuerier
	{
		string PageQuery(int VillageID, string Uri, Dictionary<string, string> Data, bool CheckLogin, bool NoParser);
	}

	public partial class Travian
	{
		public string PageQuery(int VillageID, string Uri)
		{
			return this.pageQuerier.PageQuery(VillageID, Uri, null, true, false);
		}
		public string PageQuery(int VillageID, string Uri, Dictionary<string, string> Data)
		{
			return this.pageQuerier.PageQuery(VillageID, Uri, Data, true, false);
		}
		private string AddNewdid(int VillageID, string Uri)
		{
			if(VillageID == 0)
				return Uri;
			if(Uri.Contains("?"))
				return Uri + "&newdid=" + VillageID;
			else
				return Uri + "?newdid=" + VillageID;
		}

		private void PageQueryDebugLog(int VillageID, string Uri)
		{
			var st = new StackTrace(true);
			StackFrame x = null;
			string MethodName = null;
			int i;
			for(i = 2; i < st.FrameCount; i++)
			{
				x = st.GetFrame(i);
				MethodName = x.GetMethod().Name;
				if(MethodName != "PageQuery")
					break;
			}
			if(i == st.FrameCount)
			{
				x = st.GetFrame(3);
				MethodName = x.GetMethod().Name;
			}
			string Filename = x.GetFileName();
			int Line = x.GetFileLineNumber();
			TDebugInfo db = new TDebugInfo()
			{
				Filename = Filename,
				Level = DebugLevel.II,
				Line = Line,
				MethodName = MethodName,
				Text = "Page: " + Uri + " (" + VillageID.ToString() + ")",
				Time = DateTime.Now
			};
			OnError(this, new LogArgs() { DebugInfo = db });
		}
		
		string _LastQueryPageURI = null;
		public string PageQuery(int VillageID, string Uri, Dictionary<string, string> Data, bool CheckLogin, bool NoParser)
		{
			try
			{
				PageQueryDebugLog(VillageID, Uri);
				if(wc == null)
				{
					wc = new WebClient();
					wc.BaseAddress = string.Format("http://{0}/", TD.Server);
					wc.Encoding = Encoding.UTF8;
					wc.Headers[HttpRequestHeader.Cookie] = TD.Cookie;
					if(TD.Proxy != null)
						wc.Proxy = TD.Proxy;
					_LastQueryPageURI = wc.BaseAddress;
				}
				wc.Headers[HttpRequestHeader.Referer] = _LastQueryPageURI;
				_LastQueryPageURI = wc.BaseAddress + Uri;

				wc.Headers[HttpRequestHeader.UserAgent] = "Mozilla/5.0 (Windows; U; Windows NT 5.1; zh-CN; rv:1.9.2.13) Gecko/20101203 Firefox/3.6.13";
				if(TD.Cookie == null)
					if(CheckLogin && !Login())
						return null;
				Uri = AddNewdid(VillageID, Uri);
				string QueryString = null;
				string result;
				if(Data != null)
				{
					StringBuilder sb = new StringBuilder();
					foreach(var x in Data)
					{
						if(sb.Length != 0)
							sb.Append("&");

						// Got to support some weired form data, like arrays
						if (x.Key == "!!!RawData!!!")
						{
							sb.Append(x.Value);
							continue;
						}

						sb.Append(HttpUtility.UrlEncode(x.Key));
						sb.Append("=");
						sb.Append(HttpUtility.UrlEncode(x.Value));
					}
					QueryString = sb.ToString();
					wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
					result = wc.UploadString(Uri, QueryString);
				}
				else
					result = wc.DownloadString(Uri);
				string[] t = wc.ResponseHeaders.GetValues("Set-Cookie");
				if(t != null)
				{
					foreach(string t1 in t)
						if(t1.Contains("T3E"))
						{
							TD.Dirty = true;
							TD.Cookie = t1.Split(';')[0];
						}
					wc.Headers[HttpRequestHeader.Cookie] = TD.Cookie;
				}

				if(!CheckLogin)
					return result;

				if(result.Contains("login"))
				{
					if(!Login())
						return null;
					if(Data != null)
					{
						wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
						result = wc.UploadString(Uri, QueryString);
					}
					else
						result = wc.DownloadString(Uri);
				}
				if(result.Contains(".php?ok"))
				{
					wc.DownloadString("dorf1.php?ok");
					if(Data != null)
					{
						wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
						result = wc.UploadString(Uri, QueryString);
					}
					else
						result = wc.DownloadString(Uri);
				}
				FetchPageCount();
				StatusUpdate(this, new StatusChanged { ChangedData = ChangedType.PageCount });

				var m = Regex.Match(result, "<span id=\"tp1\">([0-9:]+)</span>");
				if(m.Success)
				{
					var time = DateTime.Parse(m.Groups[1].Value);
					var timeoff = time.Subtract(DateTime.Now);
					if(timeoff < new TimeSpan(-12, 0, 0))
						timeoff.Add(new TimeSpan(24, 0, 0));
					else if(timeoff > new TimeSpan(12, 0, 0))
						timeoff.Subtract(new TimeSpan(-24, 0, 0));
					TD.ServerTimeOffset = Convert.ToInt32(timeoff.TotalSeconds);
				}
				if(!NoParser)
					NewParseEntry(VillageID, result);
				return result;
			}
			catch(Exception e)
			{
				DebugLog(e);
			}
			return null;
		}
	}
}
