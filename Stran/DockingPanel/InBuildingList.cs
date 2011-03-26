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
	public partial class InBuildingList : DockContent
	{
		public MainFrame UpCall { get; set; }

		public InBuildingList()
		{
			InitializeComponent();
		}

		private void InBuildingList_Load(object sender, EventArgs e)
		{
			listViewInBuilding.ContextMenuStrip = UpCall.contextMenuInbuilding;
			UpCall.mui.RefreshLanguage(this);
			TabText = Text;
		}
	}
}
