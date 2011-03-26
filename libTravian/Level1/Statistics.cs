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

namespace libTravian
{
	partial class Travian
	{
		public static int Pagecount = 0, Buildcount = 0;
		public int pcount = 0, bcount = 0;
		private void FetchPageCount()
		{
			pcount++;
			Pagecount++;
		}
		public void BuildCount()
		{
			bcount++;
			Buildcount++;
		}
	}
}
