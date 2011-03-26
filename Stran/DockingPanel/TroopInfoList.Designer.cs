namespace Stran.DockingPanel
{
	partial class TroopInfoList
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
			this.listViewTroop = new System.Windows.Forms.ListView();
			this.columnHeader18 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader19 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader20 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader26 = new System.Windows.Forms.ColumnHeader();
			this.SuspendLayout();
			// 
			// listViewTroop
			// 
			this.listViewTroop.AllowColumnReorder = true;
			this.listViewTroop.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader18,
            this.columnHeader19,
            this.columnHeader20,
            this.columnHeader26});
			this.listViewTroop.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listViewTroop.FullRowSelect = true;
			this.listViewTroop.GridLines = true;
			this.listViewTroop.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.listViewTroop.Location = new System.Drawing.Point(0, 0);
			this.listViewTroop.Name = "listViewTroop";
			this.listViewTroop.Size = new System.Drawing.Size(492, 468);
			this.listViewTroop.TabIndex = 8;
			this.listViewTroop.UseCompatibleStateImageBehavior = false;
			this.listViewTroop.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader18
			// 
			this.columnHeader18.Tag = "lefttimetobuild";
			this.columnHeader18.Text = "剩余时间";
			this.columnHeader18.Width = 70;
			// 
			// columnHeader19
			// 
			this.columnHeader19.Tag = "troopamount";
			this.columnHeader19.Text = "军队数量";
			this.columnHeader19.Width = 210;
			// 
			// columnHeader20
			// 
			this.columnHeader20.Tag = "vname";
			this.columnHeader20.Text = "村名";
			this.columnHeader20.Width = 70;
			// 
			// columnHeader26
			// 
			this.columnHeader26.Tag = "trooptype";
			this.columnHeader26.Text = "类型";
			this.columnHeader26.Width = 70;
			// 
			// TroopInfoList
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(492, 468);
			this.CloseButton = false;
			this.Controls.Add(this.listViewTroop);
			this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)(((((WeifenLuo.WinFormsUI.Docking.DockAreas.DockLeft | WeifenLuo.WinFormsUI.Docking.DockAreas.DockRight)
									| WeifenLuo.WinFormsUI.Docking.DockAreas.DockTop)
									| WeifenLuo.WinFormsUI.Docking.DockAreas.DockBottom)
									| WeifenLuo.WinFormsUI.Docking.DockAreas.Document)));
			this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Name = "TroopInfoList";
			this.ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.Document;
			this.TabText = "TroopInfoList";
			this.Tag = "troopinfo";
			this.Text = "TroopInfoList";
			this.Load += new System.EventHandler(this.TroopInfoList_Load);
			this.ResumeLayout(false);

		}

		#endregion

		public System.Windows.Forms.ListView listViewTroop;
		private System.Windows.Forms.ColumnHeader columnHeader18;
		private System.Windows.Forms.ColumnHeader columnHeader19;
		private System.Windows.Forms.ColumnHeader columnHeader20;
		private System.Windows.Forms.ColumnHeader columnHeader26;
	}
}