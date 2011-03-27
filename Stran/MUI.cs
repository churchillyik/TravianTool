﻿/*
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
using System.Windows.Forms;
using System.Diagnostics;

namespace Stran
{
	public class MUI
	{
		public Dictionary<string, string> Lang;
		public string _(string tag)
		{
			if(Lang.ContainsKey(tag))
				return Lang[tag];
			else
				return "?" + tag;
		}
		public MUI(string language)
		{
			Lang = new Dictionary<string, string>(64);
			string lang_file = string.Format("lang\\cli_{0}.txt", language);
			if(!File.Exists(lang_file))
				lang_file = "lang\\cli_cn.txt";
			if(!File.Exists(lang_file))
				return;
			string[] s = File.ReadAllLines(lang_file, Encoding.UTF8);
			foreach(var s1 in s)
			{
				var pairs = s1.Split(new char[] { '=' }, 2);
				if(pairs.Length != 2)
					continue;
				try
				{
					Lang[pairs[0]] = pairs[1];
				}
				catch(Exception)
				{
					continue;
				}
			}
		}
		public void RefreshLanguage(Control t)
		{
			Queue<Control> qc = new Queue<Control>();
			Queue<ToolStripItem> qt = new Queue<ToolStripItem>();
			qc.Enqueue(t);
			while(qc.Count != 0)
			{
				var x = qc.Dequeue();
				if(x.Tag is string)
					x.Text = _(x.Tag as string);
				foreach(Control y in x.Controls)
				{
					qc.Enqueue(y);
				}
				if(x is ListView)
				{
					foreach(ColumnHeader y in (x as ListView).Columns)
					{
						if(y.Tag is string)
							y.Text = _(y.Tag as string);
					}
					var n = x as ListView;
					if(n.ContextMenuStrip != null)
						foreach(ToolStripItem y in n.ContextMenuStrip.Items)
							if(y is ToolStripMenuItem)
								qt.Enqueue(y);
				}
			}
			while(qt.Count != 0)
			{
				var x = qt.Dequeue();
				if(x.Tag is string)
					x.Text = _(x.Tag as string);
				if(x is ToolStripMenuItem && (x as ToolStripMenuItem).DropDownItems != null)
					foreach(ToolStripItem y in (x as ToolStripMenuItem).DropDownItems)
						qt.Enqueue(y);
			}
			MainForm.tribelist = _("tribelist").Split(',');
			MainFrame.typelist = _("typelist").Split(',');
		}
	}
}
