/*
 * Created by SharpDevelop.
 * User: Administrator
 * Date: 2011-4-26
 * Time: 22:56
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace Stran.DockingPanel
{
	partial class HeroAdvanture
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.listViewHeroAdventure = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
			this.SuspendLayout();
			// 
			// listViewHeroAdventure
			// 
			this.listViewHeroAdventure.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
									this.columnHeader1,
									this.columnHeader2,
									this.columnHeader3,
									this.columnHeader4});
			this.listViewHeroAdventure.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listViewHeroAdventure.FullRowSelect = true;
			this.listViewHeroAdventure.GridLines = true;
			this.listViewHeroAdventure.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.listViewHeroAdventure.Location = new System.Drawing.Point(0, 0);
			this.listViewHeroAdventure.Name = "listViewHeroAdventure";
			this.listViewHeroAdventure.Size = new System.Drawing.Size(537, 309);
			this.listViewHeroAdventure.TabIndex = 0;
			this.listViewHeroAdventure.UseCompatibleStateImageBehavior = false;
			this.listViewHeroAdventure.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Tag = "colhdaxis";
			this.columnHeader1.Text = "坐标";
			this.columnHeader1.Width = 85;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Tag = "colhdduration";
			this.columnHeader2.Text = "行程时间";
			this.columnHeader2.Width = 90;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Tag = "colhddifficulty";
			this.columnHeader3.Text = "难易度";
			this.columnHeader3.Width = 66;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Tag = "colhdlefttime";
			this.columnHeader4.Text = "过期剩余时间";
			this.columnHeader4.Width = 110;
			// 
			// HeroAdvanture
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(537, 309);
			this.CloseButton = false;
			this.Controls.Add(this.listViewHeroAdventure);
			this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)(((((WeifenLuo.WinFormsUI.Docking.DockAreas.DockLeft | WeifenLuo.WinFormsUI.Docking.DockAreas.DockRight) 
									| WeifenLuo.WinFormsUI.Docking.DockAreas.DockTop) 
									| WeifenLuo.WinFormsUI.Docking.DockAreas.DockBottom) 
									| WeifenLuo.WinFormsUI.Docking.DockAreas.Document)));
			this.Name = "HeroAdvanture";
			this.ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.Document;
			this.TabText = "探险";
			this.Tag = "heroadvanture";
			this.Text = "探险";
			this.Load += new System.EventHandler(this.HeroAdvantureLoad);
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		public System.Windows.Forms.ListView listViewHeroAdventure;
	}
}
