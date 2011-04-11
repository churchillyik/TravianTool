using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Stran.DockingPanel
{
	/// <summary>
	/// Description of TroopTraining.
	/// </summary>
	public partial class TroopTraining : DockContent
	{
		public MainFrame UpCall { get; set; }
		
		public TroopTraining()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		void TroopTrainingLoad(object sender, EventArgs e)
		{
			listViewTroopTraining.ContextMenuStrip = UpCall.contextMenuTroopTraining;
			UpCall.mui.RefreshLanguage(this);
			TabText = Text;
		}
	}
}
