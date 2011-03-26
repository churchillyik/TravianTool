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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Stran
{
	public partial class BuildToLevel : Form
	{
		public int Return { private set; get; }
		public string BuildingName { set; private get; }
		public string DisplayName { set; private get; }
		public int CurrentLevel { set; private get; }
		public int TargetLevel { set; private get; }
		public MUI mui { get; set; }
		public BuildToLevel()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			Return = (int)comboBox1.SelectedItem;
		}

		private void BuildToLevel_Load(object sender, EventArgs e)
		{
			mui.RefreshLanguage(this);
			if(CurrentLevel >= TargetLevel)
			{
				Return = -1;
				this.Close();
				return;
			}
			label1.Text = BuildingName;
			label2.Text = DisplayName;
			for(int i = CurrentLevel; i < TargetLevel; i++)
			{
				comboBox1.Items.Add(i + 1);
			}
			comboBox1.SelectedIndex = 0;
		}
	}
}
