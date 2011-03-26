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
	public partial class ProduceTroopSetting : Form
	{
		private DateTime actionAt = DateTime.MinValue;
		private int minimumInterval = 0;
        private TVillage CV = null;
        
        public List<TroopInfo> CanProduce { get; set; }
		public MUI mui { get; set; }

        public Data TravianData { get; set; }
        public int RUVillageID { get; set; }
        public ProduceTroopQueue Result;

		public ProduceTroopSetting()
		{
			InitializeComponent();
		}

		private void buttonTiming_Click(object sender, EventArgs e)
		{
			ActionTiming tt = new ActionTiming
			{
				ActionAt = actionAt,
				MinimumInterval = minimumInterval,
				ActionTime = 0,
				mui = mui
			};

			if(tt.ShowDialog() == DialogResult.OK)
			{
				actionAt = tt.ActionAt;
				minimumInterval = tt.MinimumInterval;
			}

		}

		private void ProduceTroopSetting_Load(object sender, EventArgs e)
		{
            mui.RefreshLanguage(this);
            if (TravianData == null)
                return;
            CV = TravianData.Villages[RUVillageID];
            listBox1.Items.Clear();
            if (checkBox3.Checked)
            {
                numericUpDown1.Value = 1;
                numericUpDownTransferCount.Value = 0;
                numericUpDown1.Enabled = false;
                numericUpDownTransferCount.Enabled = false;
                checkBox1.Enabled = false;
                listBox1.Enabled = false;
            }
            else
            {
                numericUpDown1.Value = 0;
                numericUpDownTransferCount.Value = 1;
                numericUpDown1.Enabled = numericUpDownTransferCount.Enabled = checkBox1.Enabled = listBox1.Enabled = true;
                if (CanProduce != null)
                    foreach (var p in CanProduce)
                        if (p.Researched | checkBox1.Checked)
                            listBox1.Items.Add(p);
            }
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			if (numericUpDown1.Value == 0)
				return;
            if (listBox1.SelectedItem == null && checkBox3.Checked == false)
				return;
			bool St = checkBox3.Checked;
            Result = new ProduceTroopQueue
			{
				Aid = St ? 10 : (listBox1.SelectedItem as TroopInfo).Aid,
                GRt = checkBox2.Checked,
                Amount = Convert.ToInt32(numericUpDown1.Value),
				MaxCount = Convert.ToInt32(numericUpDownTransferCount.Value),
				MinimumInterval = minimumInterval,
				NextExec = actionAt,
			};
		}									 

		private void checkBox1_CheckedChanged(object sender, EventArgs e)
		{
			ProduceTroopSetting_Load(sender, e);
		}

        private void buttonlimit_Click(object sender, EventArgs e)
        {
            ResourceLimit limit = new ResourceLimit()
            {
                Village = this.CV,
                Description = this.mui._("TResourceLimit"),
                Limit = this.CV.Troop.ResLimit == null ? new TResAmount(0, 0, 0, 0) : this.CV.Troop.ResLimit,
                mui = this.mui
            };

            if (limit.ShowDialog() == DialogResult.OK && limit.Return != null)
            {
                this.CV.Troop.ResLimit = limit.Return;
                TravianData.Dirty = true;
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            ProduceTroopSetting_Load(sender, e);
        }
	}

	public class TroopInfo
	{
		public int Aid { get; set; }
		public string Name { get; set; }
		public bool Researched { get; set; }
		public override string ToString()
		{
			return string.Format("{0} - {1}", Aid, Name);
		}
	}
}
