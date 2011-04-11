namespace Stran.DockingPanel
{
	partial class QueueList
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
			if (disposing && (components != null))
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
			this.listViewQueue = new System.Windows.Forms.ListView();
			this.columnHeader8 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader9 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
			this.SuspendLayout();
			// 
			// listViewQueue
			// 
			this.listViewQueue.AllowColumnReorder = true;
			this.listViewQueue.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
									this.columnHeader8,
									this.columnHeader9,
									this.columnHeader6,
									this.columnHeader7});
			this.listViewQueue.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listViewQueue.FullRowSelect = true;
			this.listViewQueue.GridLines = true;
			this.listViewQueue.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.listViewQueue.Location = new System.Drawing.Point(0, 0);
			this.listViewQueue.Margin = new System.Windows.Forms.Padding(0);
			this.listViewQueue.Name = "listViewQueue";
			this.listViewQueue.Size = new System.Drawing.Size(507, 418);
			this.listViewQueue.TabIndex = 1;
			this.listViewQueue.UseCompatibleStateImageBehavior = false;
			this.listViewQueue.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader8
			// 
			this.columnHeader8.Tag = "qtype";
			this.columnHeader8.Text = "类别";
			this.columnHeader8.Width = 100;
			// 
			// columnHeader9
			// 
			this.columnHeader9.Tag = "gname";
			this.columnHeader9.Text = "建筑名";
			this.columnHeader9.Width = 139;
			// 
			// columnHeader6
			// 
			this.columnHeader6.Tag = "status";
			this.columnHeader6.Text = "状态";
			this.columnHeader6.Width = 120;
			// 
			// columnHeader7
			// 
			this.columnHeader7.Tag = "qdelay";
			this.columnHeader7.Text = "延迟";
			this.columnHeader7.Width = 117;
			// 
			// QueueList
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.ClientSize = new System.Drawing.Size(507, 418);
			this.CloseButton = false;
			this.Controls.Add(this.listViewQueue);
			this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)(((((WeifenLuo.WinFormsUI.Docking.DockAreas.DockLeft | WeifenLuo.WinFormsUI.Docking.DockAreas.DockRight) 
									| WeifenLuo.WinFormsUI.Docking.DockAreas.DockTop) 
									| WeifenLuo.WinFormsUI.Docking.DockAreas.DockBottom) 
									| WeifenLuo.WinFormsUI.Docking.DockAreas.Document)));
			this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Name = "QueueList";
			this.ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.Document;
			this.TabText = "队列";
			this.Tag = "queue";
			this.Text = "队列";
			this.Load += new System.EventHandler(this.QueueList_Load);
			this.ResumeLayout(false);
		}

		#endregion

		public System.Windows.Forms.ListView listViewQueue;
		private System.Windows.Forms.ColumnHeader columnHeader8;
		private System.Windows.Forms.ColumnHeader columnHeader9;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		private System.Windows.Forms.ColumnHeader columnHeader7;
	}
}