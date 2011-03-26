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
	public partial class TransferStatus : DockContent
	{
		public MainFrame UpCall { get; set; }

		public TransferStatus()
		{
			InitializeComponent();
		}

		private void TransferStatus_Load(object sender, EventArgs e)
		{
			listViewMarket.ContextMenuStrip = UpCall.contextMenuMarket;
			UpCall.mui.RefreshLanguage(this);
			TabText = Text;
		}
	}
}
