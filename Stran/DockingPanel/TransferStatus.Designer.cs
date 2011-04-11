namespace Stran.DockingPanel
{
	partial class TransferStatus
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
			this.listViewMarket = new System.Windows.Forms.ListView();
			this.columnHeader18 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader19 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader20 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader21 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader26 = new System.Windows.Forms.ColumnHeader();
			this.SuspendLayout();
			// 
			// listViewMarket
			// 
			this.listViewMarket.AllowColumnReorder = true;
			this.listViewMarket.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
									this.columnHeader18,
									this.columnHeader19,
									this.columnHeader20,
									this.columnHeader21,
									this.columnHeader26});
			this.listViewMarket.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listViewMarket.FullRowSelect = true;
			this.listViewMarket.GridLines = true;
			this.listViewMarket.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.listViewMarket.Location = new System.Drawing.Point(0, 0);
			this.listViewMarket.Name = "listViewMarket";
			this.listViewMarket.Size = new System.Drawing.Size(492, 468);
			this.listViewMarket.TabIndex = 7;
			this.listViewMarket.UseCompatibleStateImageBehavior = false;
			this.listViewMarket.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader18
			// 
			this.columnHeader18.Tag = "lefttimetobuild";
			this.columnHeader18.Text = "剩余时间";
			this.columnHeader18.Width = 87;
			// 
			// columnHeader19
			// 
			this.columnHeader19.Tag = "resamount";
			this.columnHeader19.Text = "资源量";
			this.columnHeader19.Width = 100;
			// 
			// columnHeader20
			// 
			this.columnHeader20.Tag = "vname";
			this.columnHeader20.Text = "村名";
			this.columnHeader20.Width = 108;
			// 
			// columnHeader21
			// 
			this.columnHeader21.Tag = "coord";
			this.columnHeader21.Text = "坐标";
			this.columnHeader21.Width = 89;
			// 
			// columnHeader26
			// 
			this.columnHeader26.Tag = "mtype";
			this.columnHeader26.Text = "运输类型";
			this.columnHeader26.Width = 100;
			// 
			// TransferStatus
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(492, 468);
			this.CloseButton = false;
			this.Controls.Add(this.listViewMarket);
			this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)(((((WeifenLuo.WinFormsUI.Docking.DockAreas.DockLeft | WeifenLuo.WinFormsUI.Docking.DockAreas.DockRight) 
									| WeifenLuo.WinFormsUI.Docking.DockAreas.DockTop) 
									| WeifenLuo.WinFormsUI.Docking.DockAreas.DockBottom) 
									| WeifenLuo.WinFormsUI.Docking.DockAreas.Document)));
			this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Name = "TransferStatus";
			this.ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.Document;
			this.TabText = "市场运输";
			this.Tag = "market";
			this.Text = "市场运输";
			this.Load += new System.EventHandler(this.TransferStatus_Load);
			this.ResumeLayout(false);
		}

		#endregion

		public System.Windows.Forms.ListView listViewMarket;
		private System.Windows.Forms.ColumnHeader columnHeader18;
		private System.Windows.Forms.ColumnHeader columnHeader19;
		private System.Windows.Forms.ColumnHeader columnHeader20;
		private System.Windows.Forms.ColumnHeader columnHeader21;
		private System.Windows.Forms.ColumnHeader columnHeader26;
	}
}