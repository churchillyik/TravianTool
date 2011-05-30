/*
 * Created by SharpDevelop.
 * User: Administrator
 * Date: 2011-4-21
 * Time: 13:52
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace Stran.DockingPanel
{
	partial class OasisSearching
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
			this.listView_SearchingResult = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.textBox_Log = new System.Windows.Forms.TextBox();
			this.textBox_AxisX = new System.Windows.Forms.TextBox();
			this.textBox_AxisY = new System.Windows.Forms.TextBox();
			this.textBox_SearchingNum = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.button2 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.flowLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// listView_SearchingResult
			// 
			this.listView_SearchingResult.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
									this.columnHeader1,
									this.columnHeader2});
			this.listView_SearchingResult.Dock = System.Windows.Forms.DockStyle.Left;
			this.listView_SearchingResult.FullRowSelect = true;
			this.listView_SearchingResult.GridLines = true;
			this.listView_SearchingResult.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.listView_SearchingResult.Location = new System.Drawing.Point(0, 0);
			this.listView_SearchingResult.Name = "listView_SearchingResult";
			this.listView_SearchingResult.Size = new System.Drawing.Size(224, 319);
			this.listView_SearchingResult.TabIndex = 0;
			this.listView_SearchingResult.UseCompatibleStateImageBehavior = false;
			this.listView_SearchingResult.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Tag = "ColHdoasisaxis";
			this.columnHeader1.Text = "坐标";
			// 
			// columnHeader2
			// 
			this.columnHeader2.Tag = "ColHdaddon";
			this.columnHeader2.Text = "绿洲加成";
			// 
			// textBox_Log
			// 
			this.textBox_Log.Dock = System.Windows.Forms.DockStyle.Top;
			this.textBox_Log.Location = new System.Drawing.Point(224, 0);
			this.textBox_Log.Multiline = true;
			this.textBox_Log.Name = "textBox_Log";
			this.textBox_Log.ReadOnly = true;
			this.textBox_Log.Size = new System.Drawing.Size(345, 131);
			this.textBox_Log.TabIndex = 1;
			// 
			// textBox_AxisX
			// 
			this.textBox_AxisX.Location = new System.Drawing.Point(300, 140);
			this.textBox_AxisX.Name = "textBox_AxisX";
			this.textBox_AxisX.Size = new System.Drawing.Size(43, 21);
			this.textBox_AxisX.TabIndex = 2;
			this.textBox_AxisX.Text = "400";
			// 
			// textBox_AxisY
			// 
			this.textBox_AxisY.Location = new System.Drawing.Point(349, 140);
			this.textBox_AxisY.Name = "textBox_AxisY";
			this.textBox_AxisY.Size = new System.Drawing.Size(42, 21);
			this.textBox_AxisY.TabIndex = 3;
			this.textBox_AxisY.Text = "400";
			// 
			// textBox_SearchingNum
			// 
			this.textBox_SearchingNum.Location = new System.Drawing.Point(300, 170);
			this.textBox_SearchingNum.Name = "textBox_SearchingNum";
			this.textBox_SearchingNum.Size = new System.Drawing.Size(52, 21);
			this.textBox_SearchingNum.TabIndex = 4;
			this.textBox_SearchingNum.Text = "3";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(241, 143);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(53, 14);
			this.label1.TabIndex = 3;
			this.label1.Tag = "lablestartaxis";
			this.label1.Text = "起始坐标";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(241, 173);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(59, 14);
			this.label2.TabIndex = 3;
			this.label2.Tag = "lablesearchingnum";
			this.label2.Text = "搜索重数";
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(3, 3);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(90, 33);
			this.button1.TabIndex = 5;
			this.button1.Tag = "startsearching";
			this.button1.Text = "开始搜索";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.Button1Click);
			// 
			// label3
			// 
			this.label3.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label3.Location = new System.Drawing.Point(397, 138);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(172, 89);
			this.label3.TabIndex = 5;
			this.label3.Tag = "labelinstruction";
			this.label3.Text = "本功能将按蜗牛式的方式搜索15田，起始坐标为中心坐标，搜索重数表示搜索的范围广度（推荐30重即可）";
			// 
			// button2
			// 
			this.button2.Enabled = false;
			this.button2.Location = new System.Drawing.Point(99, 3);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(87, 34);
			this.button2.TabIndex = 6;
			this.button2.Tag = "stopsearching";
			this.button2.Text = "停止搜索";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.Button2Click);
			// 
			// button3
			// 
			this.button3.Enabled = false;
			this.button3.Location = new System.Drawing.Point(192, 3);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(89, 34);
			this.button3.TabIndex = 6;
			this.button3.Tag = "fileout";
			this.button3.Text = "导出文件";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new System.EventHandler(this.Button3Click);
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.AutoSize = true;
			this.flowLayoutPanel1.Controls.Add(this.button1);
			this.flowLayoutPanel1.Controls.Add(this.button2);
			this.flowLayoutPanel1.Controls.Add(this.button3);
			this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.flowLayoutPanel1.Location = new System.Drawing.Point(224, 279);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(345, 40);
			this.flowLayoutPanel1.TabIndex = 7;
			// 
			// OasisSearching
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(569, 319);
			this.CloseButton = false;
			this.Controls.Add(this.flowLayoutPanel1);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.textBox_SearchingNum);
			this.Controls.Add(this.textBox_AxisY);
			this.Controls.Add(this.textBox_AxisX);
			this.Controls.Add(this.textBox_Log);
			this.Controls.Add(this.listView_SearchingResult);
			this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)(((((WeifenLuo.WinFormsUI.Docking.DockAreas.DockLeft | WeifenLuo.WinFormsUI.Docking.DockAreas.DockRight) 
									| WeifenLuo.WinFormsUI.Docking.DockAreas.DockTop) 
									| WeifenLuo.WinFormsUI.Docking.DockAreas.DockBottom) 
									| WeifenLuo.WinFormsUI.Docking.DockAreas.Document)));
			this.Name = "OasisSearching";
			this.ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.Document;
			this.TabText = "搜索15田";
			this.Tag = "OasisSearching";
			this.Text = "搜索15田";
			this.Load += new System.EventHandler(this.OasisSearchingLoad);
			this.flowLayoutPanel1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
		private System.Windows.Forms.Button button3;
		public System.Windows.Forms.ListView listView_SearchingResult;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		public System.Windows.Forms.TextBox textBox_Log;
		private System.Windows.Forms.TextBox textBox_AxisX;
		private System.Windows.Forms.TextBox textBox_AxisY;
		private System.Windows.Forms.TextBox textBox_SearchingNum;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
	}
}
