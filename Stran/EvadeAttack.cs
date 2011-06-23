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
        public EvadeQueue Return { get; set; }
        
		public EvadeAttack()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
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
			this.Return = new EvadeQueue()
			{
				nEvadePoint = new TPoint(x, y),
				nMinInterval = interval,
				nLeadTime = leadtime
			};
		}
		
		void EvadeAttackLoad(object sender, EventArgs e)
		{
			mui.RefreshLanguage(this);
		}
	}
}
