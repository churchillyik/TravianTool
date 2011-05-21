/*
 * Created by SharpDevelop.
 * User: Administrator
 * Date: 2011-5-18
 * Time: 15:48
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using libTravian;

namespace Stran
{
	/// <summary>
	/// Description of PMMail.
	/// </summary>
	public partial class PMMail : Form
	{
		#region Properties
        public MUI mui { get; set; }
        public DisplayLang dl { get; set; }
        public PMMailQueue Return { get; set; }
        #endregion
        
		public PMMail()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		void PMMailLoad(object sender, EventArgs e)
		{
			mui.RefreshLanguage(this);
            SuspendLayout();

            numericUpDown1.Minimum = 1;
            numericUpDown1.Maximum = 360;
            numericUpDown1.Increment = 1;

            ResumeLayout();
		}
		
		void BtnOkClick(object sender, EventArgs e)
		{
			PMMailQueue queue = new PMMailQueue
            {
                From = tbFrom.Text + "@" + tbDomain.Text,
                Host = tbServer.Text,
                Port = int.Parse(tbPort.Text),
                Password = tbPass.Text,
                SSLEnable = checkBox_SSLEnable.Checked,
                MinimumInterval = int.Parse(numericUpDown1.Value.ToString("f0")) * 60,
                To = tbRecv.Text.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
            };

            if (queue.IsValid)
                this.Return = queue;
            else
                this.Return = null;
		}
		
		void TbDomainLeave(object sender, EventArgs e)
		{
			if (tbDomain.Text == "")
        	{
        		tbDomain.Text = "126.com";
        		tbServer.Text = "smtp.126.com";
        	}
        	else
        	{
        		tbServer.Text = "smtp." + tbDomain.Text;
        	}
		}
	}
}
