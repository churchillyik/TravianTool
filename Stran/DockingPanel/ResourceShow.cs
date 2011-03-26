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
	public partial class ResourceShow : DockContent
	{
		public MainFrame UpCall { get; set; }

		public ResourceShow()
		{
			InitializeComponent();
		}

		private void ResourceShow_Load(object sender, EventArgs e)
		{
			UpCall.mui.RefreshLanguage(this);
			TabText = Text;
		}
	}
}
