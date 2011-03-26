using System;
using System.Windows.Forms;
using libTravian;

namespace Stran
{
	public partial class ActionTiming : Form
	{
		/// <summary>
		/// In/Out Action start time, default to DateTime.Now
		/// </summary>
		public DateTime ActionAt { get; set; }

		/// <summary>
		/// In/Out Minimum interval between actions, default to 0
		/// </summary>
		public int MinimumInterval { get; set; }

		/// <summary>
		/// In Action time cost. 0 for instant action
		/// </summary>
		public int ActionTime { get; set; }

		public MUI mui { get; set; }

		public ActionTiming()
		{
			InitializeComponent();
			ActionAt = DateTime.Now;
			ActionTime = 0;
		}

		private void TransferTiming_Load(object sender, EventArgs e)
		{
			mui.RefreshLanguage(this);
			if (this.ActionAt > DateTime.Now)
			{
				this.radioDelayed.Checked = true;
				this.dateTimeTransferAt.Value = this.ActionAt;
			}
			else
			{
				this.radioImmediate.Checked = true;
				this.dateTimeTransferAt.Value = DateTime.Now;
			}

			this.numericUpDown1.Value = this.MinimumInterval / 60;
			this.CalculateArrivalTime();
		}

		private void radioDelayed_CheckedChanged(object sender, EventArgs e)
		{
			this.dateTimeTransferAt.Enabled  = this.radioDelayed.Checked;
			if (this.radioImmediate.Checked)
			{
				this.dateTimeTransferAt.Value = DateTime.Now;
			}
		}

		private void dateTimeTransferAt_ValueChanged(object sender, EventArgs e)
		{
			this.ActionAt = this.dateTimeTransferAt.Value;
			this.CalculateArrivalTime();
		}

		private void numericUpDown1_ValueChanged(object sender, EventArgs e)
		{
			this.MinimumInterval = Convert.ToInt32(this.numericUpDown1.Value) * 60;
		}

		private void CalculateArrivalTime()
		{
			if(this.ActionTime > 0)
			{
				this.labelDetail.Text = this.ActionAt.AddSeconds(this.ActionTime).ToString("yyyy-MM-dd  HH:mm:ss");
				if(this.ActionTime < 86400)
					labelDetail.Text += " (+" + DateTime.MinValue.AddSeconds(this.ActionTime).ToLongTimeString() + ")";
				else
					labelDetail.Text += " ( > 1 day )";
			}
			else
			{
				this.labelDetail.Text = "Instant";
			}
		}
	}
}
