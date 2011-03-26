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
using System.Text.RegularExpressions;

namespace libTravian
{
	partial class Travian
	{
		private DateTime NextExec = DateTime.MinValue;
		private DateTime NextRead = DateTime.MinValue;
		private string RemoteStopWord = "stop";
		private void CheckPause(int VillageID, string data)
		{
			var m = Regex.Match(data, "l/m(1|2)\\.gif");
			if(!m.Success)
			{
				StatusUpdate(this, new StatusChanged() { ChangedData = ChangedType.Stop, Param = 0 });
				return; // no igm found, go on processing.
			}
			if(NextRead > DateTime.Now)
				return; // too short interval, go on pausing.
			data = this.pageQuerier.PageQuery(0, "nachrichten.php", null, true, true);
			if(data == null)
				return; // cannot read... network problem?
			NextRead = DateTime.Now.AddMinutes(15);
			if(Regex.Match(data, "<a href=\"nachrichten.php[^\"]+\">" + RemoteStopWord + "</a>", RegexOptions.IgnoreCase).Success)
			{
				NextExec = DateTime.Now.AddMinutes(15);
				DebugLog("Pause: To " + NextExec.ToLongTimeString(), DebugLevel.I);
				StatusUpdate(this, new StatusChanged() { ChangedData = ChangedType.Stop, Param = 1 });
			}
			else
				StatusUpdate(this, new StatusChanged() { ChangedData = ChangedType.Stop, Param = 0 });
		}
	}
}
