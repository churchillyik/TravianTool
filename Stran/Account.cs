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
using System.Text.RegularExpressions;

namespace Stran
{
	public partial class NewAccount : Form
	{
		bool _ismodify;
		public TLoginInfo accountresult;
		public MUI mui { get; set; }
		public TLoginInfo logininfo { get; set; }
		public NewAccount(bool ismodify)
		{
			_ismodify = ismodify;
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			if(textBox3.Text != "" && textBox2.Text != "" && (_ismodify || textBox1.Text != ""))
			{
				accountresult = new TLoginInfo()
				{
					Server = textBox3.Text,
					Username = textBox2.Text,
					Password = textBox1.Text,
					Tribe = comboBox1.SelectedIndex,
					Language = textBox4.Text
				};
			}
		}

		private void textBox3_Leave(object sender, EventArgs e)
		{
			var m = Regex.Match(textBox3.Text.ToLower(), "^([a-zA-Z]{2,3})(\\d{1,3}|x)$");
			if(m.Success)
			{
				if(m.Groups[1].Value == "de")
					textBox3.Text = string.Format("welt{0}.travian.{1}", m.Groups[2].Value[0] == 'x' ? "peed" : m.Groups[2].Value, m.Groups[1].Value);
				else
					textBox3.Text = string.Format("s{0}.travian.{1}", m.Groups[2].Value[0] == 'x' ? "peed" : m.Groups[2].Value, m.Groups[1].Value);
				if(textBox4.Text == "")
					textBox4.Text = m.Groups[1].Value;
				return;
			}
			m = Regex.Match(textBox3.Text.ToLower(), "http://(.*)");
			if(m.Success)
			{
				textBox3.Text = m.Groups[1].Value;
			}
			if(textBox4.Text == "")
			{
				m = Regex.Match(textBox3.Text.ToLower(), "travian.(.*)");
				if(m.Success)
					textBox4.Text = m.Groups[1].Value;
			}
		}

		private void NewAccount_Load(object sender, EventArgs e)
		{
			mui.RefreshLanguage(this);
			comboBox1.Items.AddRange(MainForm.tribelist);
			if(_ismodify)
				Text = mui._("edit") + Text;
			else
				Text = mui._("add") + Text;
			comboBox1.SelectedIndex = 0;
			if(logininfo != null)
			{
				textBox3.Text = logininfo.Server;
				textBox2.Text = logininfo.Username;
				comboBox1.SelectedIndex = logininfo.Tribe;
				textBox4.Text = logininfo.Language;
			}
		}

	}
}
