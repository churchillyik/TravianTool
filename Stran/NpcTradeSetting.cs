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
	public partial class NpcTradeSetting : Form
	{
		public NpcTradeQueue Return { get; private set; }
		public TVillage Village { private get; set; }
		public MUI mui { private get; set; }

		private NumericUpDown[] nudThreshold;
		private NumericUpDown[] nudDistribution;

		public NpcTradeSetting()
		{
			InitializeComponent();
		}

		private void NpcTradeSetting_Load(object sender, EventArgs e)
		{
			this.mui.RefreshLanguage(this);
			this.nudThreshold = new NumericUpDown[]
			{
				this.nudThreshold1,
				this.nudThreshold2,
				this.nudThreshold3,
				this.nudThreshold4
			};

			this.nudDistribution = new NumericUpDown[]
			{
				this.nudDistribution1,
				this.nudDistribution2,
				this.nudDistribution3,
				this.nudDistribution4
			};

			TResAmount capacity = this.Village.ResourceCapacity;
			TResAmount threshold = new TResAmount(0, 0, 0, capacity.Resources[3]);
			this.SetResources(this.nudThreshold, capacity, threshold);

			TResAmount distribution = new TResAmount(capacity);
			distribution.Resources[3] = 0;
			this.SetResources(this.nudDistribution, capacity, distribution);
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			var option = new NpcTradeQueue()
			{
				Threshold = this.GetResources(this.nudThreshold),
				Distribution = this.GetResources(this.nudDistribution),
				MinTradeRatio = Convert.ToInt32(this.nudMinTradeRatio.Value),
				MaxCount = Convert.ToInt32(this.nudMaxCount.Value),
				VillageID = Village.ID
			};

			if (option.IsValid)
			{
				this.Return = option;
			}
		}

		#region Helper Methods
		private void SetResources(NumericUpDown[] nuds, TResAmount capacity, TResAmount value)
		{
			for (int i = 0; i < nuds.Length; i++)
			{
				nuds[i].Minimum = 0;
				nuds[i].Maximum = capacity.Resources[i];
				nuds[i].Increment = capacity.Resources[i] / 20;
				nuds[i].Value = value.Resources[i];
			}
		}

		private TResAmount GetResources(NumericUpDown[] nuds)
		{
			int[] resources = new int[nuds.Length];

			for (int i = 0; i < resources.Length; i++)
			{
				resources[i] = Convert.ToInt32(nuds[i].Value);
			}

			return new TResAmount(resources);
		}
		#endregion
	}
}
