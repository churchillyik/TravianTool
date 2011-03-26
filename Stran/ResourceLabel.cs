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
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using libTravian;

namespace Stran
{
	public partial class ResourceLabel : UserControl
	{
		public TResource Res { get; set; }
		public ResourceLabel()
		{
			InitializeComponent();
		}

		private void ResourceLabel_Load(object sender, EventArgs e)
		{
			/*
			reslabel[i].Text = string.Format("{0}/{1}\n({2:0}, {3}:{4:00}:{5:00})\n({6}, {7:F2}%)",
				Res.CurrAmount,
				Res.Capacity,
				Res.Produce,
				Math.Floor(Res.LeftTime.TotalHours),
				Res.LeftTime.Minutes,
				Res.LeftTime.Seconds,
				Res.Capacity - Res.CurrAmount,
				Res.CurrAmount * 100.0 / Res.Capacity
				);
			 */
			Clear();

		}
		public void Display(TResource Res)
		{
			if(Res == null)
				return;
			label1.Text = string.Format("{0}/{1}", Res.CurrAmount, Res.Capacity);
			label3.Text = string.Format("({0:0}, {1}:{2:00}:{3:00})", Res.Produce,
				Math.Floor(Res.LeftTime.TotalHours),
				Res.LeftTime.Minutes, Res.LeftTime.Seconds);
			int color = Math.Abs(Convert.ToInt32(Res.LeftTime.TotalHours * 10));
			if(color > 255)
				color = 255;

			label3.ForeColor = Color.FromArgb(255 - color, 0, 0);
			label5.Text = string.Format("({0}, {1:F2}%)", Res.Capacity - Res.CurrAmount, Res.CurrAmount * 100.0 / Res.Capacity);
			color = Math.Abs(Res.CurrAmount * 255 / Res.Capacity);
			label5.ForeColor = Color.FromArgb(color, 0, 255 - color);
		}
		public void Clear()
		{
			label1.Text = label3.Text = label5.Text = "";
		}
	}
}
