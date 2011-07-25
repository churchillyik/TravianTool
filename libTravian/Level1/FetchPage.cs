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
using System.IO.Compression;
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
		
		private void CheckForSafety(int VillageID, string Uri)
		{
			if (VillageID != 0 && TD.LastGetOrPost != DateTime.MinValue)
			{
				int rand_delay = TD.GetOrPostDelaySeconds + new Random().Next(1, 10);
				if (TD.CurGetOrPostTimes > 0 && 
				    DateTime.Now.Subtract(TD.LastGetOrPost).TotalSeconds
				    >= rand_delay)
				{
					TD.CurGetOrPostTimes = 1;
				}
				else
				{
					TD.CurGetOrPostTimes ++;
				}
				if (TD.GetOrPostTimesThreshold != 0 
				    && TD.CurGetOrPostTimes > TD.GetOrPostTimesThreshold)
				{
					DateTime resume_time = TD.LastGetOrPost.AddSeconds(rand_delay);
					DebugLog("为防封号延迟至（延迟" + rand_delay + "秒）："
					         + resume_time.Hour + ":" 
					         + resume_time.Minute + ":"
					         + resume_time.Second + "...", DebugLevel.II);
					Thread.Sleep(rand_delay * 1000);
					TD.CurGetOrPostTimes = 1;
					DebugLog("恢复工作，访问" + Uri + "(" + VillageID.ToString() + ")"
					         , DebugLevel.II);
				}
			}
			TD.LastGetOrPost = DateTime.Now;
		}
		
		string _LastQueryPageURI = null;
		private void CreateRequest(string Uri)
		{
			request = (HttpWebRequest)WebRequest.Create(Uri);
			if (TD.Proxy != null)
				request.Proxy = TD.Proxy;
			if (cookies == null)
				cookies = new CookieContainer();
			request.CookieContainer = cookies;
			if (_LastQueryPageURI != null)
				request.Referer = _LastQueryPageURI;
			_LastQueryPageURI = Uri;
			request.Timeout = 30000;
			request.UserAgent = "Mozilla/5.0 (Windows NT 5.1; rv:5.0) Gecko/20100101 Firefox/5.0";
			request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
			request.Headers.Add("Accept-Language", "zh-cn,zh;q=0.5");
			request.Headers.Add("Accept-Encoding", "gzip, deflate");
			request.Headers.Add("Accept-Charset", "GB2312,utf-8;q=0.7,*;q=0.7");
			request.ServicePoint.Expect100Continue = false;
			request.KeepAlive = true;
		}
		
		private string HttpQuery(int VillageID, string Uri, Dictionary<string, string> Data)
		{
			string BaseAddress = string.Format("http://{0}/", TD.Server);
			Uri = AddNewdid(VillageID, Uri);
			CreateRequest(BaseAddress + Uri);
			if(Data == null)
			{
				return HttpGet();
			}
			else
			{
				return HttpPost(Data);
			}
		}
		
		private string HttpGet()
		{
			request.Method = "GET";
			return FetchResponse();
		}
		
		private string HttpPost(Dictionary<string, string> Data)
		{
			request.Method = "POST";
			
			string QueryString = null;
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
			request.ContentType = "application/x-www-form-urlencoded";
			
			ASCIIEncoding encoding = new ASCIIEncoding ();
    		byte[] qry_bytes = encoding.GetBytes(QueryString);
    		request.ContentLength = qry_bytes.Length;
    		
			Stream newStream = request.GetRequestStream();
			newStream.Write(qry_bytes, 0, qry_bytes.Length);
			newStream.Close();
			
			return FetchResponse(); 
		}
		
		private string FetchResponse()
		{
			string result = null;
			HttpWebResponse response = (HttpWebResponse)request.GetResponse();
			response.Cookies = cookies.GetCookies(request.RequestUri);
			
			foreach (Cookie cook in response.Cookies)
			{
				if (cook.Name == "T3E")
				{
					TD.Dirty = true;
					TD.Cookie = cook.Value;
				}
			}
			
			if (response.ContentEncoding == "gzip")
			{
				using(Stream streamReceive = response.GetResponseStream())
				{
					using(GZipStream zipStream = new GZipStream(streamReceive, CompressionMode.Decompress))
				        using (StreamReader sr = new StreamReader(zipStream, Encoding.UTF8))
				            result = sr.ReadToEnd();
				}
			}
			else
			{
				using(Stream streamReceive = response.GetResponseStream())
				{
					using(StreamReader sr = new StreamReader(streamReceive, Encoding.UTF8))
						result = sr.ReadToEnd();
				}
			}
			
			
			return result;
		}
		
		public string PageQuery(int VillageID, string Uri, Dictionary<string, string> Data, bool CheckLogin, bool NoParser)
		{
			try
			{
				CheckForSafety(VillageID, Uri);
				PageQueryDebugLog(VillageID, Uri);
				
				if(TD.Cookie == null)
					if(CheckLogin && !Login())
						return null;
				
				string result = HttpQuery(VillageID, Uri, Data);

				if(!CheckLogin)
					return result;

				if(result.Contains("login"))
				{
					if(!Login())
					{
						DebugLog("无法抓取网页：" + _LastQueryPageURI, DebugLevel.II);
						return null;
					}
					result = HttpQuery(VillageID, Uri, Data);
				}
				if(result.Contains(".php?ok"))
				{
					HttpQuery(0, "dorf1.php?ok", null);
					result = HttpQuery(VillageID, Uri, Data);
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
				{
					NewParseEntry(VillageID, result);
				}
				return result;
			}
			catch(Exception e)
			{
				DebugLog("扑捉到异常：" + e.ToString(), DebugLevel.F);
				DebugLog(e);
			}
			DebugLog("无法抓取网页：" + _LastQueryPageURI, DebugLevel.II);
			return null;
		}
	}
}
