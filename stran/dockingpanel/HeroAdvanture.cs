/*
 * Created by SharpDevelop.
 * User: Administrator
 * Date: 2011-4-26
 * Time: 22:56
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Stran.DockingPanel
{
	/// <summary>
	/// Description of HeroAdvanture.
	/// </summary>
	public partial class HeroAdvanture : DockContent
	{
		public MainFrame UpCall { get; set; }
		
		public HeroAdvanture()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		void HeroAdvantureLoad(object sender, EventArgs e)
		{
			listViewHeroAdventure.ContextMenuStrip = UpCall.contextMenuHeroAdventure;
			UpCall.mui.RefreshLanguage(this);
			TabText = Text;
		}
	}
}
