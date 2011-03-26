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
	public partial class BuildingList : DockContent
	{
		public MainFrame UpCall { get; set; }
		
		public BuildingList()
		{
			InitializeComponent();
		}

		private void BuildingList_Load(object sender, EventArgs e)
		{
			listViewBuilding.ContextMenuStrip = UpCall.contextMenuBuilding;
			UpCall.mui.RefreshLanguage(this);
			TabText = Text;
		}

		private void buttonParty_Click(object sender, EventArgs e)
		{
			UpCall.QPParty_Click(sender, e);
		}

		private void buttonAILevel_Click(object sender, EventArgs e)
		{
			UpCall.QPAILevel_Click(sender, e);
		}

		private void buttonUpTop_Click(object sender, EventArgs e)
		{
			UpCall.QPUpTop_Click(sender, e);
		}

		private void buttonRefreshRes_Click(object sender, EventArgs e)
		{
			UpCall.QPRefreshRes_Click(sender, e);
		}
	}
}
