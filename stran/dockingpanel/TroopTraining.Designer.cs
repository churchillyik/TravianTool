/*
 * Created by SharpDevelop.
 * User: Administrator
 * Date: 2011-4-10
 * Time: 23:38
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace Stran.DockingPanel
{
	partial class TroopTraining
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
			this.listViewTroopTraining = new System.Windows.Forms.ListView();
			this.columnHeader_TroopName = new System.Windows.Forms.ColumnHeader();
			this.columnHeader_CurrentAmount = new System.Windows.Forms.ColumnHeader();
			this.columnHeader_TrainAmount = new System.Windows.Forms.ColumnHeader();
			this.columnHeader_LeftTrainTime = new System.Windows.Forms.ColumnHeader();
			this.SuspendLayout();
			// 
			// listViewTroopTraining
			// 
			this.listViewTroopTraining.AllowColumnReorder = true;
			this.listViewTroopTraining.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
									this.columnHeader_TroopName,
									this.columnHeader_CurrentAmount,
									this.columnHeader_TrainAmount,
									this.columnHeader_LeftTrainTime});
			this.listViewTroopTraining.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listViewTroopTraining.FullRowSelect = true;
			this.listViewTroopTraining.GridLines = true;
			this.listViewTroopTraining.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.listViewTroopTraining.Location = new System.Drawing.Point(0, 0);
			this.listViewTroopTraining.Name = "listViewTroopTraining";
			this.listViewTroopTraining.Size = new System.Drawing.Size(490, 341);
			this.listViewTroopTraining.TabIndex = 0;
			this.listViewTroopTraining.UseCompatibleStateImageBehavior = false;
			this.listViewTroopTraining.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader_TroopName
			// 
			this.columnHeader_TroopName.Tag = "troopname";
			this.columnHeader_TroopName.Text = "兵种名称";
			this.columnHeader_TroopName.Width = 89;
			// 
			// columnHeader_CurrentAmount
			// 
			this.columnHeader_CurrentAmount.Tag = "currentamount";
			this.columnHeader_CurrentAmount.Text = "当前总数";
			this.columnHeader_CurrentAmount.Width = 117;
			// 
			// columnHeader_TrainAmount
			// 
			this.columnHeader_TrainAmount.Tag = "trainamount";
			this.columnHeader_TrainAmount.Text = "训练的数目";
			this.columnHeader_TrainAmount.Width = 108;
			// 
			// columnHeader_LeftTrainTime
			// 
			this.columnHeader_LeftTrainTime.Tag = "lefttraintime";
			this.columnHeader_LeftTrainTime.Text = "训练剩余时长";
			this.columnHeader_LeftTrainTime.Width = 134;
			// 
			// TroopTraining
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(490, 341);
			this.CloseButton = false;
			this.Controls.Add(this.listViewTroopTraining);
			this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)(((((WeifenLuo.WinFormsUI.Docking.DockAreas.DockLeft | WeifenLuo.WinFormsUI.Docking.DockAreas.DockRight) 
									| WeifenLuo.WinFormsUI.Docking.DockAreas.DockTop) 
									| WeifenLuo.WinFormsUI.Docking.DockAreas.DockBottom) 
									| WeifenLuo.WinFormsUI.Docking.DockAreas.Document)));
			this.Name = "TroopTraining";
			this.ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.Document;
			this.TabText = "军队训练";
			this.Tag = "trooptraining";
			this.Text = "TroopTraining";
			this.ResumeLayout(false);
		}
		public System.Windows.Forms.ListView listViewTroopTraining;
		private System.Windows.Forms.ColumnHeader columnHeader_LeftTrainTime;
		private System.Windows.Forms.ColumnHeader columnHeader_TrainAmount;
		private System.Windows.Forms.ColumnHeader columnHeader_CurrentAmount;
		private System.Windows.Forms.ColumnHeader columnHeader_TroopName;
	}
}
