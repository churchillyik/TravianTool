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
    public partial class Alarm : Form
    {
        #region Properties
        public MUI mui { get; set; }
        public DisplayLang dl { get; set; }
        public AlarmQueue Return { get; set; }
        #endregion

        public Alarm()
        {
            InitializeComponent();
        }

        private void Alarm_Load(object sender, EventArgs e)
        {
            mui.RefreshLanguage(this);
            SuspendLayout();

            numericUpDown1.Minimum = 30;
            numericUpDown1.Maximum = 360;
            numericUpDown1.Increment = (360 - 30) / 20;

            ResumeLayout();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            AlarmQueue queue = new AlarmQueue
            {
                From = tbFrom.Text + "@" + tbDomain.Text,
                Host = tbServer.Text,
                Port = int.Parse(tbPort.Text),
                Password = tbPass.Text,
                SSLEnable = checkBox_SSLEnable.Checked,
                MinimumInterval = int.Parse(numericUpDown1.Value.ToString("f0")) * 60,
                To = tbRecv.Text.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries),
                TrustfulUsers = tbTrust.Text,
            };

            if (queue.IsValid)
                this.Return = queue;
            else
                this.Return = null;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

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
