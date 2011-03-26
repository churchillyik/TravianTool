namespace Stran.DockingPanel
{
	partial class InBuildingList
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
			this.listViewInBuilding = new System.Windows.Forms.ListView();
			this.columnHeader11 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader17 = new System.Windows.Forms.ColumnHeader();
			this.SuspendLayout();
			// 
			// listViewInBuilding
			// 
			this.listViewInBuilding.AllowColumnReorder = true;
			this.listViewInBuilding.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader11,
            this.columnHeader17});
			this.listViewInBuilding.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listViewInBuilding.FullRowSelect = true;
			this.listViewInBuilding.GridLines = true;
			this.listViewInBuilding.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.listViewInBuilding.Location = new System.Drawing.Point(0, 0);
			this.listViewInBuilding.Margin = new System.Windows.Forms.Padding(0);
			this.listViewInBuilding.MultiSelect = false;
			this.listViewInBuilding.Name = "listViewInBuilding";
			this.listViewInBuilding.Size = new System.Drawing.Size(342, 138);
			this.listViewInBuilding.TabIndex = 19;
			this.listViewInBuilding.UseCompatibleStateImageBehavior = false;
			this.listViewInBuilding.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader11
			// 
			this.columnHeader11.Tag = "ibtype";
			this.columnHeader11.Text = "项目";
			this.columnHeader11.Width = 100;
			// 
			// columnHeader17
			// 
			this.columnHeader17.Tag = "ibitem";
			this.columnHeader17.Text = "建筑和时间";
			this.columnHeader17.Width = 200;
			// 
			// InBuildingList
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(342, 138);
			this.CloseButton = false;
			this.Controls.Add(this.listViewInBuilding);
			this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)(((((WeifenLuo.WinFormsUI.Docking.DockAreas.DockLeft | WeifenLuo.WinFormsUI.Docking.DockAreas.DockRight)
									| WeifenLuo.WinFormsUI.Docking.DockAreas.DockTop)
									| WeifenLuo.WinFormsUI.Docking.DockAreas.DockBottom)
									| WeifenLuo.WinFormsUI.Docking.DockAreas.Document)));
			this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.MinimumSize = new System.Drawing.Size(300, 170);
			this.Name = "InBuildingList";
			this.ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.DockBottom;
			this.TabText = "进行中的任务";
			this.Tag = "inbuilding";
			this.Text = "进行中的任务";
			this.Load += new System.EventHandler(this.InBuildingList_Load);
			this.ResumeLayout(false);

		}

		#endregion

		public System.Windows.Forms.ListView listViewInBuilding;
		private System.Windows.Forms.ColumnHeader columnHeader11;
		private System.Windows.Forms.ColumnHeader columnHeader17;
	}
}