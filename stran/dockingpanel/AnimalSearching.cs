/*
 * Created by SharpDevelop.
 * User: Administrator
 * Date: 2011-6-17
 * Time: 20:24
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using WeifenLuo.WinFormsUI.Docking;
using libTravian;

namespace Stran.DockingPanel
{
	/// <summary>
	/// Description of AnimalSearching.
	/// </summary>
	public partial class AnimalSearching : DockContent
	{
		public MainFrame UpCall { get; set; }
		private CheckBox[] CBAnimalsInclude;
		private CheckBox[] CBAnimalsExclude;
		
		public AnimalSearching()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		void AnimalSearchingLoad(object sender, EventArgs e)
		{
			UpCall.mui.RefreshLanguage(this);
			TabText = Text;
			CreateControlArrays();
		}
		
		void CreateControlArrays()
		{
			this.CBAnimalsInclude = new CheckBox[]
			{
				this.checkBox1,
				this.checkBox2,
				this.checkBox3,
				this.checkBox4,
				this.checkBox5,
				this.checkBox6,
				this.checkBox7,
				this.checkBox8,
				this.checkBox9,
				this.checkBox10,
			};
			this.CBAnimalsExclude = new CheckBox[]
			{
				this.checkBox11,
				this.checkBox12,
				this.checkBox13,
				this.checkBox14,
				this.checkBox15,
				this.checkBox16,
				this.checkBox17,
				this.checkBox18,
				this.checkBox19,
				this.checkBox20,
			};
		}
		
		private bool bIsInSearching = false;
		void Button1Click(object sender, EventArgs e)
		{
			if (bIsInSearching)
				return;
			int range, axis_x, axis_y;
			range = axis_x = axis_y = 0;
			try
			{
				range = Convert.ToInt32(this.numericUpDown1.Value);
				axis_x = Convert.ToInt32(this.numericUpDown2.Value);
				axis_y = Convert.ToInt32(this.numericUpDown3.Value);
			}
			catch
			{
				return;
			}
			
			FindAnimalsOption option = new FindAnimalsOption()
			{
				Range = range,
				AxisX = axis_x,
				AxisY = axis_y,
			};
			for (int i = 0; i < CBAnimalsInclude.Length; i++)
			{
				if (CBAnimalsInclude[i].Checked)
				{
					option.lstIncl.Add(i + 1 + 30);
				}
			}
			
			for (int i = 0; i < CBAnimalsExclude.Length; i++)
			{
				if (CBAnimalsExclude[i].Checked)
				{
					option.lstExcl.Add(i + 1 + 30);
				}
			}
			this.listView1.Items.Clear();
			UpCall.FindAnimalsClick(option);
		}
		
		void Button2Click(object sender, EventArgs e)
		{
			UpCall.StopFindAnimalsClick();
		}
		
		void Button3Click(object sender, EventArgs e)
		{
			textBox1.Text = "";
		}
		
		void Button4Click(object sender, EventArgs e)
		{
			UpCall.SaveAnimalsInfoClick();
		}
	}
}
