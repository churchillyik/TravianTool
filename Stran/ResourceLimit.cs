using System;
using System.Windows.Forms;
using libTravian;

namespace Stran
{
	public partial class ResourceLimit : Form
	{
		public TResAmount Return { private set; get; }
		public TVillage Village { set; private get; }
		public string Description { set; private get; }
		public TResAmount Limit { set; private get; }
		public MUI mui { get; set; }

		private NumericUpDown[] nudLimits;

		public ResourceLimit()
		{
			InitializeComponent();
			Return = null;
		}

		private void ResourceLimit_Load(object sender, EventArgs e)
		{
			this.mui.RefreshLanguage(this);
			this.lblVillage.Text = String.Format("{0} ({1})", this.Village.Name, this.Village.Coord);
			this.lblDescription.Text = this.Description;
			this.nudLimits = new NumericUpDown[]
			{
				this.nudLimit1,
				this.nudLimit2,
				this.nudLimit3,
				this.nudLimit4
			};

			TResAmount capacity = this.Village.ResourceCapacity;
			for (int i = 0; i < this.nudLimits.Length; i++)
			{
				this.nudLimits[i].Minimum = 0;
				this.nudLimits[i].Maximum = capacity.Resources[i];
				this.nudLimits[i].Increment = capacity.Resources[i] / 10;
				this.nudLimits[i].Value = this.Limit.Resources[i];
			}
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			int[] limits = new int[this.nudLimits.Length];
			for (int i = 0; i < limits.Length; i++)
			{
				limits[i] = Convert.ToInt32(nudLimits[i].Value);
			}

			TResAmount newLimit = new TResAmount(limits);
			if (newLimit != this.Limit)
			{
				this.Return = newLimit;
			}
		}
	}
}
