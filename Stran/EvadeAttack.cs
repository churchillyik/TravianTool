/*
 * Created by SharpDevelop.
 * User: Administrator
 * Date: 2011-6-23
 * Time: 17:05
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using libTravian;

namespace Stran
{
	/// <summary>
	/// Description of EvadeAttack.
	/// </summary>
	public partial class EvadeAttack : Form
	{
		
		public MUI mui { get; set; }
		public int Tribe { get; set; }
        public EvadeQueue Return { get; set; }
        private CheckBox[] CBTroops;
        
		public EvadeAttack()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
			
			CreateControlArrays();
		}
		
		void CreateControlArrays()
		{
			this.CBTroops = new CheckBox[]
			{
				this.CBT1,
				this.CBT2,
				this.CBT3,
				this.CBT4,
				this.CBT5,
				this.CBT6,
				this.CBT7,
				this.CBT8,
				this.CBT9,
				this.CBT10,
				this.CBT11,
			};
		}
		
		void ButtonOKClick(object sender, EventArgs e)
		{
			int x, y, interval, leadtime;
			try
			{
				x = Convert.ToInt32(this.txtX.Text);
				y = Convert.ToInt32(this.txtY.Text);
				interval = Convert.ToInt32(this.numericUpDown1.Value);
				leadtime = Convert.ToInt32(this.numericUpDown2.Value);
			}
			catch
			{
				this.Return = null;
				return;
			}
			bool[] troop_filter = new bool[CBTroops.Length];
			for (int i = 0; i < CBTroops.Length; i++)
			{
				troop_filter[i] = CBTroops[i].Checked;
			}
			this.Return = new EvadeQueue()
			{
				tpEvadePoint = new TPoint(x, y),
				nMinInterval = interval,
				nLeadTime = leadtime,
				bTroopFilter = troop_filter,
			};
		}
		
		void EvadeAttackLoad(object sender, EventArgs e)
		{
			mui.RefreshLanguage(this);
			InitTroopTexts();
			InitTroopFilter();
		}
		
		void InitTroopTexts()
		{
			for (int i = 0; i < this.CBTroops.Length; i++)
			{
				CBTroops[i].Text = DisplayLang.Instance.GetAidLang(Tribe, i + 1);
			}
		}
		
		void InitTroopFilter()
		{
			switch (Tribe)
			{
				case 1:
					CBTroops[1].Checked = false;
					CBTroops[5].Checked = false;
					break;
				case 2:
					CBTroops[1].Checked = false;
					CBTroops[4].Checked = false;
					break;
				case 3:
					CBTroops[0].Checked = false;
					CBTroops[4].Checked = false;
					CBTroops[5].Checked = false;
					break;
			}
		}
	}
}
