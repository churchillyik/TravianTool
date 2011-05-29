/*
 * Created by SharpDevelop.
 * User: Administrator
 * Date: 2011-4-21
 * Time: 13:52
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Stran.DockingPanel
{
	/// <summary>
	/// Description of OasisSearching.
	/// </summary>
	public partial class OasisSearching : DockContent
	{
		public MainFrame UpCall { get; set; }
		
		public OasisSearching()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		void OasisSearchingLoad(object sender, EventArgs e)
		{
			UpCall.mui.RefreshLanguage(this);
			TabText = Text;
		}
		
		
		void Button1Click(object sender, EventArgs e)
		{
			int x, y, num;
			try
			{
				x = Convert.ToInt32(this.textBox_AxisX.Text);
				y = Convert.ToInt32(this.textBox_AxisY.Text);
				num = Convert.ToInt32(this.textBox_SearchingNum.Text);
			}
			catch(Exception)
			{
				MessageBox.Show("填写的数据有误！");
				return;
			}
			UpCall.FindOasisClick(x, y, num);
			this.button1.Enabled = false;
			this.button2.Enabled = true;
			this.button3.Enabled = true;
		}
		
		void Button2Click(object sender, EventArgs e)
		{
			UpCall.StopFindOasisClick();
			this.button1.Enabled = true;
			this.button2.Enabled = false;
			this.button3.Enabled = false;
		}
		
		void Button3Click(object sender, EventArgs e)
		{
			UpCall.FileOutOasisClick();
		}
	}
}
