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
using System.Diagnostics;

namespace libTravian
{
	public class TDebugInfo
	{
		public DateTime Time { get; set; }
		public string MethodName { get; set; }
		public string Filename { get; set; }
		public int Line { get; set; }
		public libTravian.DebugLevel Level { get; set; }
		public string Text { get; set; }
	}
	public enum DebugLevel
	{
		I,
		W,
		E,
		F,
		II
	}
	public class LogArgs : EventArgs
	{
		public TDebugInfo DebugInfo { get; set; }
	}

	partial class Travian
	{
		public event EventHandler<LogArgs> OnError;
		public const int DebugCount = 16;
		public List<TDebugInfo> DebugList = new List<TDebugInfo>(DebugCount);
		public void DebugLog(string Text, DebugLevel Level)
		{
			StackFrame x = new StackTrace(true).GetFrame(1);
			string MethodName = x.GetMethod().Name;
			string Filename = x.GetFileName();
			int Line = x.GetFileLineNumber();
			TDebugInfo db = new TDebugInfo()
			{
				Filename = Filename,
				Level = Level,
				Line = Line,
				MethodName = MethodName,
				Text = Text,
				Time = DateTime.Now
			};
			if(DebugList.Count > DebugCount)
				DebugList.RemoveAt(0);
			DebugList.Add(db);

			if (this.OnError != null)
			{
				OnError(this, new LogArgs() { DebugInfo = db });
			}
		}
		public void DebugLog(Exception e)
		{
			DebugLog(e, DebugLevel.F);
		}
		public void DebugLog(Exception e, DebugLevel Level)
		{
			StackFrame x = new StackTrace(e).GetFrame(0);
			string MethodName = x.GetMethod().Name;
			string Filename = x.GetFileName();
			int Line = x.GetFileLineNumber();
			TDebugInfo db = new TDebugInfo()
			{
				Filename = Filename,
				Level = DebugLevel.F,
				Line = Line,
				MethodName = MethodName,
				Text = e.Message + Environment.NewLine + e.StackTrace,
				Time = DateTime.Now
			};
			if(DebugList.Count > DebugCount)
				DebugList.RemoveAt(0);
			DebugList.Add(db);
			OnError(this, new LogArgs() { DebugInfo = db });
		}

		[Obsolete("Not Implemented")]
		public string FetchDebugStack()
		{
			return "";
		}
	}
}
