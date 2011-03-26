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
using System.Windows.Forms;
using libTravian;
using System.Collections.Generic;

namespace Stran
{
	public partial class NewBuilding : Form
	{
		public int OutBid { get; set; }
		public int OutGid { get; set; }
		public bool OutTop { get; set; }
		DisplayLang dl;
		public MUI mui { get; set; }

		public NewBuilding(Data TD, int VillageID)
		{
			InitializeComponent();
			this.dl = DisplayLang.Instance;
			for(int i = 5; i <= dl.GidLang.Count; i++)
			{
				if(i >= 31 && i <= 33 || i == 16)
					continue;
				int possible = Travian.testPossibleNewNow(TD.Tribe, TD.Villages, TD.Villages[VillageID], i, 0);
				if(possible > 0)
					comboBox1.Items.Add(i + ". " + dl.GetGidLang(i));
				else if(possible == 0)
					comboBox1.Items.Add(i + ". " + dl.GetGidLang(i) + " <--");
			}
			var Buildings = TD.Villages[VillageID].Buildings;
			for(int i = 19; i < 39; i++)
				if(!Buildings.ContainsKey(i))
					comboBox2.Items.Add(i);
			if(comboBox2.Items.Count != 0)
				comboBox2.SelectedIndex = 0;
			else
				button1.Enabled = false;
		}
		/*
		public static int B2I(TBuilding b)
		{
			if(b != null)
				return b.Gid;
			else
				return 0;
		}
		 */
		private void button1_Click(object sender, EventArgs e)
		{
			OutGid = Convert.ToInt32(comboBox1.SelectedItem.ToString().Split('.')[0]);
			OutBid = (int)comboBox2.SelectedItem;
			OutTop = checkBox1.Checked;
		}

		private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			int gid = Convert.ToInt32(comboBox1.SelectedItem.ToString().Split('.')[0]);
			if(!Buildings.PreferPos.ContainsKey(gid))
				return;
			int[] preferpos = Buildings.PreferPos[gid];
			if(preferpos != null)
				foreach(int pos in preferpos)
					if(comboBox2.Items.Contains(pos))
					{
						comboBox2.SelectedIndex = comboBox2.Items.IndexOf(pos);
						return;
					}
			if(comboBox2.Items.Count != 0)
				comboBox2.SelectedIndex = 0;
		}

		private void NewBuilding_Load(object sender, EventArgs e)
		{
			mui.RefreshLanguage(this);
		}
	}
}
