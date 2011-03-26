using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
//using Yaowi.Common.Collections;
//using Yaowi.Common.Windows.Controls;

namespace Stran.DockingPanel
{
	public partial class VillageList : DockContent
	{
		public MainFrame UpCall { get; set; }
//		public ListViewSorter listviewsorter = new ListViewSorter();

		public VillageList()
		{
			InitializeComponent();
		}

		private void VillageList_Load(object sender, EventArgs e)
		{
//			listviewsorter.ListView = this.listViewVillage;
//			listviewsorter.ColumnComparerCollection[UpCall.mui._("vid")] = new NumericComparer();
//			listviewsorter.ColumnComparerCollection[UpCall.mui._("vname")] = new Yaowi.Common.Collections.StringComparer();
//			listviewsorter.ColumnComparerCollection[UpCall.mui._("coord")] = new GermanPostalCodeCityByPostalCodeComparer();
			listViewVillage.ContextMenuStrip = UpCall.contextMenuVillage;
			UpCall.mui.RefreshLanguage(this);
			TabText = Text;
		}

		private void listViewVillage_SelectedIndexChanged(object sender, EventArgs e)
		{
			UpCall.listViewVillage_Changed(sender, e);
		}

		private void listViewVillage_Click(object sender, EventArgs e)
		{
			UpCall.listViewVillage_Click(sender, e);
		}
	}
}
