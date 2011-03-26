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
	public partial class QueueList : DockContent
	{
		public MainFrame UpCall { get; set; }

		public QueueList()
		{
			InitializeComponent();
		}

		private void QueueList_Load(object sender, EventArgs e)
		{
			listViewQueue.ContextMenuStrip = UpCall.contextMenuQueue;
			UpCall.mui.RefreshLanguage(this);
			TabText = Text;
		}
	}
}
