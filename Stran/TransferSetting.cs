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
using System.Text;
using System.Windows.Forms;
using libTravian;
using System.IO;

namespace Stran
{
	public partial class TransferSetting : Form
	{
		private TVillage CV = null;
		private TVillage TV = null;
		private int targetVillageID = 0;
		private Travian UpCall = null;

		private DateTime transferAt = DateTime.Now;
		private int minimumInterval = 0;

		public Data TravianData { get; set; }
		public int FromVillageID { get; set; }
		public MUI mui { get; set; }
		public TransferQueue Return { get; private set; }

		public TransferSetting(Travian UpCall)
		{
			InitializeComponent();
			this.UpCall = UpCall;
		}

		private void TransferSetting_Load(object sender, EventArgs e)
		{
			mui.RefreshLanguage(this);
			if (TravianData == null)
				return;
			CV = TravianData.Villages[FromVillageID];
			foreach (var v in TravianData.Villages)
				if (v.Key != FromVillageID)
					comboBoxTargetVillage.Items.Add(v.Key + " " + v.Value.Coord + " " + v.Value.Name);
				else
					comboBoxTargetVillage.SelectedIndex = comboBoxTargetVillage.Items.Count - 1;

			if (File.Exists("Transfer"))
			{
				var ts = File.ReadAllLines("Transfer");
				foreach (var t in ts)
				{
					string[] opt = t.Split(new char[] { '|' }, 3);
					if (opt.Length < 3)
						continue;
					int x = 0, y = 0;
					if(int.TryParse(opt[0], out x) && int.TryParse(opt[1], out y))
						if(Math.Abs(x) <= 400 && Math.Abs(y) <= 400)
							comboBoxTargetVillage2.Items.Add(opt[0] + "|" + opt[1] + " " + opt[2]);
				}
			}

			numericUpDown1.Increment =
				numericUpDown2.Increment =
				numericUpDown3.Increment =
				numericUpDown4.Increment = CV.Market.SingleCarry;
			numericUpDown1.Maximum =
				numericUpDown2.Maximum =
				numericUpDown3.Maximum =
				numericUpDown4.Maximum = CV.Market.SingleCarry * CV.Market.MaxMerchant;
			numericUpDownMerchantCount.Maximum = CV.Market.MaxMerchant;
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			this.Return = this.GetTransferOption();
			if (!this.Return.IsValid)
			{
				this.Return = null;
			}
		}

		private void comboBoxTargetVillage_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.TV = null;
			if (comboBoxTargetVillage.SelectedIndex == 0)
			{
				this.txtX.Enabled = this.txtY.Enabled = true;
				targetVillageID = 0;
			}
			else
			{
				this.comboBoxTargetVillage2.SelectedIndex = 0;
				comboBoxTargetVillage2_SelectedIndexChanged(this, e);
				this.txtX.Enabled = this.txtY.Enabled = false;
				this.targetVillageID = Convert.ToInt32((comboBoxTargetVillage.SelectedItem as string).Split(' ')[0]);
				if (TravianData.Villages.ContainsKey(this.targetVillageID))
				{
					TVillage village = TravianData.Villages[this.targetVillageID];
					this.txtX.Text = village.X.ToString();
					this.txtY.Text = village.Y.ToString();
					if (village.isBuildingInitialized == 2)
					{
						this.TV = village;
					}
				}
			}

			if (this.TV == null)
			{
				this.radioNormalTarget.Enabled = false;
				this.buttonTarget.Enabled = false;
				if (this.radioNormalTarget.Checked)
				{
					this.radioNormalMe.Checked = true;
				}
			}
			else
			{
				this.radioNormalTarget.Enabled = true;
				this.buttonTarget.Enabled = true;
			}

			numericUpDownMechantCount_ValueChanged(sender, e);
		}

		private void numericUpDownMechantCount_ValueChanged(object sender, EventArgs e)
		{
			var option = this.GetTransferOption();
			if (option != null && option.Distribution != ResourceDistributionType.None)
			{
				int total = this.CV.Market.SingleCarry * Convert.ToInt32(this.numericUpDownMerchantCount.Value);
				option.ResourceAmount = new TResAmount(0, 0, 0, total);
				option.CalculateResourceAmount(this.TravianData, this.CV.ID);
				this.numericUpDown1.Value = option.ResourceAmount.Resources[0];
				this.numericUpDown2.Value = option.ResourceAmount.Resources[1];
				this.numericUpDown3.Value = option.ResourceAmount.Resources[2];
				this.numericUpDown4.Value = option.ResourceAmount.Resources[3];
			}
		}

		private void checkBoxNoCrop_CheckedChanged(object sender, EventArgs e)
		{
			comboBoxTargetVillage_SelectedIndexChanged(sender, e);
		}

		private void numericUpDown1234_ValueChanged(object sender, EventArgs e)
		{
			var num = new int[]{
					Convert.ToInt32(numericUpDown1.Value),
					Convert.ToInt32(numericUpDown2.Value),
					Convert.ToInt32(numericUpDown3.Value),
					Convert.ToInt32(numericUpDown4.Value)};

			StringBuilder sb = new StringBuilder();
			if (TV != null)
			{
				int max = Math.Max(num[0],
					Math.Max(num[1],
					Math.Max(num[2], num[3])));

				int length1 = Math.Max(CV.Resource[0].Capacity, CV.Resource[3].Capacity).ToString().Length;
				int length2 = max.ToString().Length;
				int length3 = Math.Max(TV.Resource[0].Capacity, TV.Resource[3].Capacity).ToString().Length;

				string format = "{0," + length1.ToString() + "}/{1," + length1.ToString() + "} {2,3}% -> {3," + length2.ToString() + "} -> {4," + length3.ToString() + "}/{5," + length3.ToString() + "} {6,3}%";
				for (int i = 0; i < 4; i++)
				{
					sb.AppendFormat(format, //"{0}/{1} {2}% ->\t{3} ->\t{4}/{5} {6}%",
						CV.Resource[i].CurrAmount,
						CV.Resource[i].Capacity,
						(CV.Resource[i].CurrAmount - num[i]) * 100 / CV.Resource[i].Capacity,
						num[i],
						TV.Resource[i].CurrAmount,
						TV.Resource[i].Capacity,
						(num[i] + TV.Resource[i].CurrAmount) * 100 / TV.Resource[i].Capacity
						);
					sb.AppendLine();
				}
			}
			int all = 0;
			for (int i = 0; i < 4; i++)
				all += num[i];
			if (CV.Market.SingleCarry == 0)
				CV.Market.SingleCarry = 750;
			sb.AppendFormat(mui._("merchantsformat"), Convert.ToInt32(Math.Ceiling(Convert.ToDouble(all) / CV.Market.SingleCarry)), CV.Market.ActiveMerchant);

			var option = this.GetTransferOption();
			if (option != null)
			{
				sb.AppendFormat(" {0}", option.Status);
				if (option.Distribution == ResourceDistributionType.None)
				{
					this.numericUpDownMerchantCount.Value = (option.ResourceAmount.TotalAmount - 1) / this.CV.Market.SingleCarry + 1;
				}
			}

			buttonOK.Enabled = option != null;

			labelDetail.Text = sb.ToString();
			
		}

		private void radio_CheckedChanged(object sender, EventArgs e)
		{
			comboBoxTargetVillage_SelectedIndexChanged(sender, e);
		}

		private void buttonTiming_Click(object sender, EventArgs e)
		{
			ActionTiming tt = new ActionTiming
			{
				ActionAt = this.transferAt,
				MinimumInterval = this.minimumInterval,
				mui = this.mui
			};

			var option = this.GetTransferOption();
			if (option != null && !option.TargetPos.IsEmpty)
			{
				int speed = this.TravianData.MarketSpeed == 0 ? 24 : this.TravianData.MarketSpeed;
				tt.ActionTime = (int) (this.CV.Coord * option.TargetPos * 3600 / speed);
			}

			if (tt.ShowDialog() == DialogResult.OK)
			{
				this.transferAt = tt.ActionAt;
				this.minimumInterval = tt.MinimumInterval;
			}
		}

		/// <summary>
		/// Assemble a TransferOption object using current control values
		/// </summary>
		/// <returns></returns>
		private TransferQueue GetTransferOption()
		{
			TransferQueue option = new TransferQueue { UpCall = UpCall, VillageID = CV.ID };

			option.MaxCount = Convert.ToInt32(this.numericUpDownTransferCount.Value);
			option.TargetVillageID = this.targetVillageID;
			int x = 0, y = 0;
			Int32.TryParse(this.txtX.Text, out x);
			Int32.TryParse(this.txtY.Text, out y);
			option.TargetPos = new TPoint(x, y);
			option.ForceGo = checkBoxForce.Checked;

			option.ResourceAmount = new TResAmount(
				 Convert.ToInt32(this.numericUpDown1.Value),
				 Convert.ToInt32(this.numericUpDown2.Value),
				 Convert.ToInt32(this.numericUpDown3.Value),
				 Convert.ToInt32(this.numericUpDown4.Value));
			if (option.ResourceAmount.TotalAmount > this.numericUpDownMerchantCount.Maximum * this.CV.Market.SingleCarry)
			{
				return null;
			}

			if(this.radioNormalTarget.Checked)
			{
				option.Distribution = ResourceDistributionType.BalanceTarget;
				if(this.TV == null)
				{
					return null;
				}
			}
			else if(radioNormalMe.Checked)
			{
				option.Distribution = ResourceDistributionType.BalanceSource;
			}
			else if(radioUniform.Checked)
			{
				option.Distribution = ResourceDistributionType.Uniform;
			}
			else if(radioNormalMeTime.Checked)
				option.Distribution = ResourceDistributionType.BalanceSourceTime;

			if (checkBoxNoCrop.Checked)
			{
				option.NoCrop = true;
			}

			option.MinimumInterval = this.minimumInterval;
			if (this.transferAt > DateTime.Now)
			{
				option.MinimumDelay = Convert.ToInt32((this.transferAt - DateTime.Now).TotalSeconds);
			}

			option.LimitRate = Convert.ToInt32(this.numericUpDown5.Value);
			return option;
		}

		private void numericUpDown1234_Enter(object sender, EventArgs e)
		{
			this.radioNoNormal.Checked = true;
		}

		private void buttonSource_Click(object sender, EventArgs e)
		{
			ResourceLimit limit = new ResourceLimit()
			{
				Village = this.CV,
				Description = this.mui._("lowerlimit"),
				Limit = this.CV.Market.LowerLimit == null ? new TResAmount(0, 0, 0, 0) : this.CV.Market.LowerLimit,
				mui = this.mui
			};

			if (limit.ShowDialog() == DialogResult.OK && limit.Return != null)
			{
				this.CV.Market.LowerLimit = limit.Return;
				UpCall.Dirty = true;
			}
		}

		private void buttonTarget_Click(object sender, EventArgs e)
		{
			if (this.TV == null || this.TV.isBuildingInitialized != 2)
			{
				return;
			}

			ResourceLimit limit = new ResourceLimit()
			{
				Village = this.TV,
				Description = this.mui._("upperlimit"),
				Limit = this.TV.Market.UpperLimit == null ? this.TV.ResourceCapacity : this.TV.Market.UpperLimit,
				mui = this.mui
			};

			if (limit.ShowDialog() == DialogResult.OK && limit.Return != null)
			{
				this.TV.Market.UpperLimit = limit.Return;
				UpCall.Dirty = true;
			}
		}

		private void comboBoxTargetVillage2_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBoxTargetVillage2.SelectedIndex == 0)
			{
				this.txtX.Enabled = this.txtY.Enabled = true;
			}
			else
			{
				this.comboBoxTargetVillage.SelectedIndex = 0;
				comboBoxTargetVillage_SelectedIndexChanged(this, e);
				this.txtX.Enabled = this.txtY.Enabled = false;
				string selfVillageID = (comboBoxTargetVillage2.SelectedItem as string).Split(' ')[0];
				string Xc = selfVillageID.Split('|')[0];
				string Yc = selfVillageID.Split('|')[1];
				this.txtX.Text = Xc;
				this.txtY.Text = Yc;
				this.radioNormalTarget.Enabled = false;
				this.buttonTarget.Enabled = false;
				if (this.radioNormalTarget.Checked)
				{
					this.radioNormalMe.Checked = true;
				}
			}

			numericUpDownMechantCount_ValueChanged(sender, e);
		}
	}
}
