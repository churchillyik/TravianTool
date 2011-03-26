namespace Stran.DockingPanel
{
	partial class BuildingList
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
			this.listViewBuilding = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.buttonParty = new System.Windows.Forms.Button();
			this.buttonAILevel = new System.Windows.Forms.Button();
			this.buttonUpTop = new System.Windows.Forms.Button();
			this.buttonRefreshRes = new System.Windows.Forms.Button();
			this.flowLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// listViewBuilding
			// 
			this.listViewBuilding.AllowColumnReorder = true;
			this.listViewBuilding.BackColor = System.Drawing.SystemColors.Window;
			this.listViewBuilding.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader4});
			this.listViewBuilding.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listViewBuilding.FullRowSelect = true;
			this.listViewBuilding.GridLines = true;
			this.listViewBuilding.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.listViewBuilding.Location = new System.Drawing.Point(0, 0);
			this.listViewBuilding.Name = "listViewBuilding";
			this.listViewBuilding.Size = new System.Drawing.Size(392, 330);
			this.listViewBuilding.TabIndex = 4;
			this.listViewBuilding.UseCompatibleStateImageBehavior = false;
			this.listViewBuilding.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Tag = "bid";
			this.columnHeader1.Text = "坑号";
			this.columnHeader1.Width = 50;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Tag = "gname";
			this.columnHeader2.Text = "建筑名";
			this.columnHeader2.Width = 200;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Tag = "lefttimetobuild";
			this.columnHeader4.Text = "剩余时间";
			this.columnHeader4.Width = 100;
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.AutoSize = true;
			this.flowLayoutPanel1.Controls.Add(this.buttonParty);
			this.flowLayoutPanel1.Controls.Add(this.buttonAILevel);
			this.flowLayoutPanel1.Controls.Add(this.buttonUpTop);
			this.flowLayoutPanel1.Controls.Add(this.buttonRefreshRes);
			this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 330);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(392, 38);
			this.flowLayoutPanel1.TabIndex = 5;
			// 
			// buttonParty
			// 
			this.buttonParty.AutoSize = true;
			this.buttonParty.Location = new System.Drawing.Point(3, 3);
			this.buttonParty.Name = "buttonParty";
			this.buttonParty.Padding = new System.Windows.Forms.Padding(2);
			this.buttonParty.Size = new System.Drawing.Size(80, 32);
			this.buttonParty.TabIndex = 0;
			this.buttonParty.Tag = "QPParty";
			this.buttonParty.Text = "Party";
			this.buttonParty.UseVisualStyleBackColor = true;
			this.buttonParty.Click += new System.EventHandler(this.buttonParty_Click);
			// 
			// buttonAILevel
			// 
			this.buttonAILevel.AutoSize = true;
			this.buttonAILevel.Location = new System.Drawing.Point(89, 3);
			this.buttonAILevel.Name = "buttonAILevel";
			this.buttonAILevel.Padding = new System.Windows.Forms.Padding(2);
			this.buttonAILevel.Size = new System.Drawing.Size(80, 32);
			this.buttonAILevel.TabIndex = 1;
			this.buttonAILevel.Tag = "QPAILevel";
			this.buttonAILevel.Text = "AILevel";
			this.buttonAILevel.UseVisualStyleBackColor = true;
			this.buttonAILevel.Click += new System.EventHandler(this.buttonAILevel_Click);
			// 
			// buttonUpTop
			// 
			this.buttonUpTop.AutoSize = true;
			this.buttonUpTop.Location = new System.Drawing.Point(175, 3);
			this.buttonUpTop.Name = "buttonUpTop";
			this.buttonUpTop.Padding = new System.Windows.Forms.Padding(2);
			this.buttonUpTop.Size = new System.Drawing.Size(80, 32);
			this.buttonUpTop.TabIndex = 2;
			this.buttonUpTop.Tag = "QPUpTop";
			this.buttonUpTop.Text = "UpTop";
			this.buttonUpTop.UseVisualStyleBackColor = true;
			this.buttonUpTop.Click += new System.EventHandler(this.buttonUpTop_Click);
			// 
			// buttonRefreshRes
			// 
			this.buttonRefreshRes.AutoSize = true;
			this.buttonRefreshRes.Location = new System.Drawing.Point(261, 3);
			this.buttonRefreshRes.Name = "buttonRefreshRes";
			this.buttonRefreshRes.Padding = new System.Windows.Forms.Padding(2);
			this.buttonRefreshRes.Size = new System.Drawing.Size(81, 32);
			this.buttonRefreshRes.TabIndex = 3;
			this.buttonRefreshRes.Tag = "QPRefreshRes";
			this.buttonRefreshRes.Text = "RefreshRes";
			this.buttonRefreshRes.UseVisualStyleBackColor = true;
			this.buttonRefreshRes.Click += new System.EventHandler(this.buttonRefreshRes_Click);
			// 
			// BuildingList
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(392, 368);
			this.CloseButton = false;
			this.Controls.Add(this.listViewBuilding);
			this.Controls.Add(this.flowLayoutPanel1);
			this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)(((((WeifenLuo.WinFormsUI.Docking.DockAreas.DockLeft | WeifenLuo.WinFormsUI.Docking.DockAreas.DockRight)
									| WeifenLuo.WinFormsUI.Docking.DockAreas.DockTop)
									| WeifenLuo.WinFormsUI.Docking.DockAreas.DockBottom)
									| WeifenLuo.WinFormsUI.Docking.DockAreas.Document)));
			this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Name = "BuildingList";
			this.ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.Document;
			this.TabText = "村庄建筑";
			this.Tag = "villagestatus";
			this.Text = "村庄建筑";
			this.Load += new System.EventHandler(this.BuildingList_Load);
			this.flowLayoutPanel1.ResumeLayout(false);
			this.flowLayoutPanel1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		public System.Windows.Forms.ListView listViewBuilding;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
		private System.Windows.Forms.Button buttonParty;
		private System.Windows.Forms.Button buttonAILevel;
		private System.Windows.Forms.Button buttonUpTop;
		private System.Windows.Forms.Button buttonRefreshRes;
	}
}