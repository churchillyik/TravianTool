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
	public partial class ResearchStatus : DockContent
	{
		public MainFrame UpCall { get; set; }

		public ResearchStatus()
		{
			InitializeComponent();
		}

		private void ResearchStatus_Load(object sender, EventArgs e)
		{
			listViewUpgrade.ContextMenuStrip = UpCall.contextMenuResearch;
			UpCall.mui.RefreshLanguage(this);
			TabText = Text;
		}
	}
}
