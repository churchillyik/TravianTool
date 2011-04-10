namespace Stran.DockingPanel
{
	partial class VillageList
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
			this.listViewVillage = new System.Windows.Forms.ListView();
			this.columnHeader14 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader15 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader12 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader13 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.SuspendLayout();
			// 
			// listViewVillage
			// 
			this.listViewVillage.AllowColumnReorder = true;
			this.listViewVillage.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
									this.columnHeader14,
									this.columnHeader15,
									this.columnHeader12,
									this.columnHeader13,
									this.columnHeader1});
			this.listViewVillage.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listViewVillage.Font = new System.Drawing.Font("Tahoma", 8F);
			this.listViewVillage.FullRowSelect = true;
			this.listViewVillage.GridLines = true;
			this.listViewVillage.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.listViewVillage.HideSelection = false;
			this.listViewVillage.Location = new System.Drawing.Point(0, 0);
			this.listViewVillage.Margin = new System.Windows.Forms.Padding(0);
			this.listViewVillage.MultiSelect = false;
			this.listViewVillage.Name = "listViewVillage";
			this.listViewVillage.ShowItemToolTips = true;
			this.listViewVillage.Size = new System.Drawing.Size(342, 368);
			this.listViewVillage.TabIndex = 16;
			this.listViewVillage.UseCompatibleStateImageBehavior = false;
			this.listViewVillage.View = System.Windows.Forms.View.Details;
			this.listViewVillage.SelectedIndexChanged += new System.EventHandler(this.listViewVillage_SelectedIndexChanged);
			this.listViewVillage.Click += new System.EventHandler(this.listViewVillage_Click);
			// 
			// columnHeader14
			// 
			this.columnHeader14.Tag = "vid";
			this.columnHeader14.Text = "ID";
			this.columnHeader14.Width = 45;
			// 
			// columnHeader15
			// 
			this.columnHeader15.Tag = "qcount";
			this.columnHeader15.Text = "队列";
			this.columnHeader15.Width = 39;
			// 
			// columnHeader12
			// 
			this.columnHeader12.Tag = "vname";
			this.columnHeader12.Text = "村名";
			this.columnHeader12.Width = 86;
			// 
			// columnHeader13
			// 
			this.columnHeader13.Tag = "coord";
			this.columnHeader13.Text = "坐标";
			// 
			// columnHeader1
			// 
			this.columnHeader1.Tag = "vres";
			this.columnHeader1.Text = "资源";
			this.columnHeader1.Width = 49;
			// 
			// VillageList
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(342, 368);
			this.CloseButton = false;
			this.Controls.Add(this.listViewVillage);
			this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)((WeifenLuo.WinFormsUI.Docking.DockAreas.DockLeft | WeifenLuo.WinFormsUI.Docking.DockAreas.DockRight)));
			this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Name = "VillageList";
			this.ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.DockLeft;
			this.TabText = "VillageList";
			this.Tag = "villagelist";
			this.Text = "VillageList";
			this.Load += new System.EventHandler(this.VillageList_Load);
			this.ResumeLayout(false);
		}

		#endregion

		public System.Windows.Forms.ListView listViewVillage;
		private System.Windows.Forms.ColumnHeader columnHeader14;
		private System.Windows.Forms.ColumnHeader columnHeader15;
		private System.Windows.Forms.ColumnHeader columnHeader12;
		private System.Windows.Forms.ColumnHeader columnHeader13;
        private System.Windows.Forms.ColumnHeader columnHeader1;
	}
}