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
	public partial class TroopInfoList : DockContent
	{
		public MainFrame UpCall { get; set; }

		public TroopInfoList()
		{
			InitializeComponent();
		}

		private void TroopInfoList_Load(object sender, EventArgs e)
		{
            listViewTroop.ContextMenuStrip = UpCall.contextMenuTroop;
            UpCall.mui.RefreshLanguage(this);
			TabText = Text;
		}
	}
}
