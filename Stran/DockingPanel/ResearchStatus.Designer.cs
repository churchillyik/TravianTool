namespace Stran.DockingPanel
{
	partial class ResearchStatus
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if(disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.listViewUpgrade = new System.Windows.Forms.ListView();
			this.columnHeader22 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader23 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader24 = new System.Windows.Forms.ColumnHeader();
			this.SuspendLayout();
			// 
			// listViewUpgrade
			// 
			this.listViewUpgrade.AllowColumnReorder = true;
			this.listViewUpgrade.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
									this.columnHeader22,
									this.columnHeader23,
									this.columnHeader24});
			this.listViewUpgrade.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listViewUpgrade.FullRowSelect = true;
			this.listViewUpgrade.GridLines = true;
			this.listViewUpgrade.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.listViewUpgrade.Location = new System.Drawing.Point(0, 0);
			this.listViewUpgrade.Name = "listViewUpgrade";
			this.listViewUpgrade.Size = new System.Drawing.Size(472, 448);
			this.listViewUpgrade.TabIndex = 6;
			this.listViewUpgrade.UseCompatibleStateImageBehavior = false;
			this.listViewUpgrade.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader22
			// 
			this.columnHeader22.Tag = "troopname";
			this.columnHeader22.Text = "兵种";
			this.columnHeader22.Width = 140;
			// 
			// columnHeader23
			// 
			this.columnHeader23.Tag = "research";
			this.columnHeader23.Text = "研发";
			this.columnHeader23.Width = 95;
			// 
			// columnHeader24
			// 
			this.columnHeader24.Tag = "troop_lvl";
			this.columnHeader24.Text = "兵种等级";
			this.columnHeader24.Width = 95;
			// 
			// ResearchStatus
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(472, 448);
			this.CloseButton = false;
			this.Controls.Add(this.listViewUpgrade);
			this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)(((((WeifenLuo.WinFormsUI.Docking.DockAreas.DockLeft | WeifenLuo.WinFormsUI.Docking.DockAreas.DockRight) 
									| WeifenLuo.WinFormsUI.Docking.DockAreas.DockTop) 
									| WeifenLuo.WinFormsUI.Docking.DockAreas.DockBottom) 
									| WeifenLuo.WinFormsUI.Docking.DockAreas.Document)));
			this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Name = "ResearchStatus";
			this.ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.Document;
			this.TabText = "研发状态";
			this.Tag = "upgradestatus";
			this.Text = "研发状态";
			this.Load += new System.EventHandler(this.ResearchStatus_Load);
			this.ResumeLayout(false);
		}

		#endregion

		public System.Windows.Forms.ListView listViewUpgrade;
		private System.Windows.Forms.ColumnHeader columnHeader22;
		private System.Windows.Forms.ColumnHeader columnHeader23;
		private System.Windows.Forms.ColumnHeader columnHeader24;
	}
}