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
using System.Runtime.InteropServices;

namespace Stran
{
	public partial class MsgBox : Form
	{
		public string message { get; set; }
		public MUI mui { get; set; }
		public MsgBox()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			Clipboard.SetDataObject(textBox1.Text);
			MessageBox.Show("Message copied to clipboard.", Text);
		}

		private void FatalError_Load(object sender, EventArgs e)
		{
			mui.RefreshLanguage(this);
			textBox1.Text = message;
		}

		private void button2_Click(object sender, EventArgs e)
		{
			Close();
		}
	}
}
