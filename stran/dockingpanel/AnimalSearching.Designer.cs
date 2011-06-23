/*
 * Created by SharpDevelop.
 * User: Administrator
 * Date: 2011-6-17
 * Time: 20:24
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace Stran.DockingPanel
{
	partial class AnimalSearching
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
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
			this.label2 = new System.Windows.Forms.Label();
			this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
			this.label3 = new System.Windows.Forms.Label();
			this.numericUpDown3 = new System.Windows.Forms.NumericUpDown();
			this.button3 = new System.Windows.Forms.Button();
			this.button4 = new System.Windows.Forms.Button();
			this.listView1 = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
			this.flowLayoutPanel5 = new System.Windows.Forms.FlowLayoutPanel();
			this.flowLayoutPanel6 = new System.Windows.Forms.FlowLayoutPanel();
			this.flowLayoutPanel4 = new System.Windows.Forms.FlowLayoutPanel();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.checkBox10 = new System.Windows.Forms.CheckBox();
			this.checkBox5 = new System.Windows.Forms.CheckBox();
			this.checkBox9 = new System.Windows.Forms.CheckBox();
			this.checkBox4 = new System.Windows.Forms.CheckBox();
			this.checkBox8 = new System.Windows.Forms.CheckBox();
			this.checkBox3 = new System.Windows.Forms.CheckBox();
			this.checkBox7 = new System.Windows.Forms.CheckBox();
			this.checkBox2 = new System.Windows.Forms.CheckBox();
			this.checkBox6 = new System.Windows.Forms.CheckBox();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.checkBox20 = new System.Windows.Forms.CheckBox();
			this.checkBox11 = new System.Windows.Forms.CheckBox();
			this.checkBox15 = new System.Windows.Forms.CheckBox();
			this.checkBox16 = new System.Windows.Forms.CheckBox();
			this.checkBox19 = new System.Windows.Forms.CheckBox();
			this.checkBox12 = new System.Windows.Forms.CheckBox();
			this.checkBox14 = new System.Windows.Forms.CheckBox();
			this.checkBox17 = new System.Windows.Forms.CheckBox();
			this.checkBox18 = new System.Windows.Forms.CheckBox();
			this.checkBox13 = new System.Windows.Forms.CheckBox();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).BeginInit();
			this.flowLayoutPanel3.SuspendLayout();
			this.flowLayoutPanel5.SuspendLayout();
			this.flowLayoutPanel6.SuspendLayout();
			this.flowLayoutPanel4.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(264, 79);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.ReadOnly = true;
			this.textBox1.Size = new System.Drawing.Size(157, 173);
			this.textBox1.TabIndex = 10;
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(3, 3);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(87, 32);
			this.button1.TabIndex = 4;
			this.button1.Tag = "BTNFindAnimals";
			this.button1.Text = "开始搜索";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.Button1Click);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(96, 3);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(86, 33);
			this.button2.TabIndex = 5;
			this.button2.Tag = "BTNStopFindAnimal";
			this.button2.Text = "停止搜索";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.Button2Click);
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label1.Location = new System.Drawing.Point(3, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(53, 26);
			this.label1.TabIndex = 4;
			this.label1.Tag = "LALRange";
			this.label1.Text = "搜索重数";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// numericUpDown1
			// 
			this.numericUpDown1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.numericUpDown1.Location = new System.Drawing.Point(62, 3);
			this.numericUpDown1.Maximum = new decimal(new int[] {
									50,
									0,
									0,
									0});
			this.numericUpDown1.Name = "numericUpDown1";
			this.numericUpDown1.Size = new System.Drawing.Size(45, 21);
			this.numericUpDown1.TabIndex = 1;
			this.numericUpDown1.Value = new decimal(new int[] {
									5,
									0,
									0,
									0});
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label2.Location = new System.Drawing.Point(3, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(53, 26);
			this.label2.TabIndex = 4;
			this.label2.Tag = "LALAxisX";
			this.label2.Text = "X轴坐标";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// numericUpDown2
			// 
			this.numericUpDown2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.numericUpDown2.Location = new System.Drawing.Point(62, 3);
			this.numericUpDown2.Maximum = new decimal(new int[] {
									400,
									0,
									0,
									0});
			this.numericUpDown2.Minimum = new decimal(new int[] {
									400,
									0,
									0,
									-2147483648});
			this.numericUpDown2.Name = "numericUpDown2";
			this.numericUpDown2.Size = new System.Drawing.Size(45, 21);
			this.numericUpDown2.TabIndex = 2;
			this.numericUpDown2.Value = new decimal(new int[] {
									100,
									0,
									0,
									0});
			// 
			// label3
			// 
			this.label3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label3.Location = new System.Drawing.Point(3, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(53, 26);
			this.label3.TabIndex = 4;
			this.label3.Tag = "LALAxisY";
			this.label3.Text = "Y轴坐标";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// numericUpDown3
			// 
			this.numericUpDown3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.numericUpDown3.Location = new System.Drawing.Point(62, 3);
			this.numericUpDown3.Maximum = new decimal(new int[] {
									400,
									0,
									0,
									0});
			this.numericUpDown3.Minimum = new decimal(new int[] {
									400,
									0,
									0,
									-2147483648});
			this.numericUpDown3.Name = "numericUpDown3";
			this.numericUpDown3.Size = new System.Drawing.Size(45, 21);
			this.numericUpDown3.TabIndex = 3;
			this.numericUpDown3.Value = new decimal(new int[] {
									100,
									0,
									0,
									0});
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(188, 3);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(81, 33);
			this.button3.TabIndex = 6;
			this.button3.Tag = "BTNClearLog";
			this.button3.Text = "清空日志";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new System.EventHandler(this.Button3Click);
			// 
			// button4
			// 
			this.button4.Location = new System.Drawing.Point(275, 3);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(146, 33);
			this.button4.TabIndex = 7;
			this.button4.Tag = "BTNOutFile";
			this.button4.Text = "导出文件到[野兽.txt]";
			this.button4.UseVisualStyleBackColor = true;
			this.button4.Click += new System.EventHandler(this.Button4Click);
			// 
			// listView1
			// 
			this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
									this.columnHeader1,
									this.columnHeader2});
			this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listView1.FullRowSelect = true;
			this.listView1.GridLines = true;
			this.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.listView1.Location = new System.Drawing.Point(0, 0);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(680, 354);
			this.listView1.TabIndex = 8;
			this.listView1.UseCompatibleStateImageBehavior = false;
			this.listView1.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Tag = "COLHDAxis";
			this.columnHeader1.Text = "坐标";
			// 
			// columnHeader2
			// 
			this.columnHeader2.Tag = "COLHDAnimals";
			this.columnHeader2.Text = "野兽";
			this.columnHeader2.Width = 185;
			// 
			// flowLayoutPanel3
			// 
			this.flowLayoutPanel3.Controls.Add(this.button1);
			this.flowLayoutPanel3.Controls.Add(this.button2);
			this.flowLayoutPanel3.Controls.Add(this.button3);
			this.flowLayoutPanel3.Controls.Add(this.button4);
			this.flowLayoutPanel3.Controls.Add(this.flowLayoutPanel5);
			this.flowLayoutPanel3.Controls.Add(this.flowLayoutPanel6);
			this.flowLayoutPanel3.Controls.Add(this.flowLayoutPanel4);
			this.flowLayoutPanel3.Controls.Add(this.groupBox1);
			this.flowLayoutPanel3.Controls.Add(this.groupBox2);
			this.flowLayoutPanel3.Controls.Add(this.textBox1);
			this.flowLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Right;
			this.flowLayoutPanel3.Location = new System.Drawing.Point(215, 0);
			this.flowLayoutPanel3.Name = "flowLayoutPanel3";
			this.flowLayoutPanel3.Size = new System.Drawing.Size(465, 354);
			this.flowLayoutPanel3.TabIndex = 12;
			// 
			// flowLayoutPanel5
			// 
			this.flowLayoutPanel5.Controls.Add(this.label2);
			this.flowLayoutPanel5.Controls.Add(this.numericUpDown2);
			this.flowLayoutPanel5.Location = new System.Drawing.Point(3, 42);
			this.flowLayoutPanel5.Name = "flowLayoutPanel5";
			this.flowLayoutPanel5.Size = new System.Drawing.Size(117, 31);
			this.flowLayoutPanel5.TabIndex = 0;
			// 
			// flowLayoutPanel6
			// 
			this.flowLayoutPanel6.Controls.Add(this.label3);
			this.flowLayoutPanel6.Controls.Add(this.numericUpDown3);
			this.flowLayoutPanel6.Location = new System.Drawing.Point(126, 42);
			this.flowLayoutPanel6.Name = "flowLayoutPanel6";
			this.flowLayoutPanel6.Size = new System.Drawing.Size(117, 31);
			this.flowLayoutPanel6.TabIndex = 0;
			// 
			// flowLayoutPanel4
			// 
			this.flowLayoutPanel4.Controls.Add(this.label1);
			this.flowLayoutPanel4.Controls.Add(this.numericUpDown1);
			this.flowLayoutPanel4.Location = new System.Drawing.Point(249, 42);
			this.flowLayoutPanel4.Name = "flowLayoutPanel4";
			this.flowLayoutPanel4.Size = new System.Drawing.Size(117, 31);
			this.flowLayoutPanel4.TabIndex = 0;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.checkBox10);
			this.groupBox1.Controls.Add(this.checkBox5);
			this.groupBox1.Controls.Add(this.checkBox9);
			this.groupBox1.Controls.Add(this.checkBox4);
			this.groupBox1.Controls.Add(this.checkBox8);
			this.groupBox1.Controls.Add(this.checkBox3);
			this.groupBox1.Controls.Add(this.checkBox7);
			this.groupBox1.Controls.Add(this.checkBox2);
			this.groupBox1.Controls.Add(this.checkBox6);
			this.groupBox1.Controls.Add(this.checkBox1);
			this.groupBox1.Location = new System.Drawing.Point(3, 79);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(124, 173);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "必须包含";
			// 
			// checkBox10
			// 
			this.checkBox10.Location = new System.Drawing.Point(65, 140);
			this.checkBox10.Name = "checkBox10";
			this.checkBox10.Size = new System.Drawing.Size(53, 24);
			this.checkBox10.TabIndex = 0;
			this.checkBox10.Text = "大象";
			this.checkBox10.UseVisualStyleBackColor = true;
			// 
			// checkBox5
			// 
			this.checkBox5.Location = new System.Drawing.Point(5, 140);
			this.checkBox5.Name = "checkBox5";
			this.checkBox5.Size = new System.Drawing.Size(53, 24);
			this.checkBox5.TabIndex = 0;
			this.checkBox5.Text = "野猪";
			this.checkBox5.UseVisualStyleBackColor = true;
			// 
			// checkBox9
			// 
			this.checkBox9.Location = new System.Drawing.Point(65, 110);
			this.checkBox9.Name = "checkBox9";
			this.checkBox9.Size = new System.Drawing.Size(53, 24);
			this.checkBox9.TabIndex = 0;
			this.checkBox9.Text = "老虎";
			this.checkBox9.UseVisualStyleBackColor = true;
			// 
			// checkBox4
			// 
			this.checkBox4.Location = new System.Drawing.Point(6, 110);
			this.checkBox4.Name = "checkBox4";
			this.checkBox4.Size = new System.Drawing.Size(53, 24);
			this.checkBox4.TabIndex = 0;
			this.checkBox4.Text = "蝙蝠";
			this.checkBox4.UseVisualStyleBackColor = true;
			// 
			// checkBox8
			// 
			this.checkBox8.Location = new System.Drawing.Point(65, 80);
			this.checkBox8.Name = "checkBox8";
			this.checkBox8.Size = new System.Drawing.Size(53, 24);
			this.checkBox8.TabIndex = 0;
			this.checkBox8.Text = "鳄鱼";
			this.checkBox8.UseVisualStyleBackColor = true;
			// 
			// checkBox3
			// 
			this.checkBox3.Location = new System.Drawing.Point(6, 80);
			this.checkBox3.Name = "checkBox3";
			this.checkBox3.Size = new System.Drawing.Size(53, 24);
			this.checkBox3.TabIndex = 0;
			this.checkBox3.Text = "蛇";
			this.checkBox3.UseVisualStyleBackColor = true;
			// 
			// checkBox7
			// 
			this.checkBox7.Location = new System.Drawing.Point(65, 50);
			this.checkBox7.Name = "checkBox7";
			this.checkBox7.Size = new System.Drawing.Size(53, 24);
			this.checkBox7.TabIndex = 0;
			this.checkBox7.Text = "熊";
			this.checkBox7.UseVisualStyleBackColor = true;
			// 
			// checkBox2
			// 
			this.checkBox2.Location = new System.Drawing.Point(6, 50);
			this.checkBox2.Name = "checkBox2";
			this.checkBox2.Size = new System.Drawing.Size(53, 24);
			this.checkBox2.TabIndex = 0;
			this.checkBox2.Text = "蜘蛛";
			this.checkBox2.UseVisualStyleBackColor = true;
			// 
			// checkBox6
			// 
			this.checkBox6.Location = new System.Drawing.Point(65, 20);
			this.checkBox6.Name = "checkBox6";
			this.checkBox6.Size = new System.Drawing.Size(53, 24);
			this.checkBox6.TabIndex = 0;
			this.checkBox6.Text = "狼";
			this.checkBox6.UseVisualStyleBackColor = true;
			// 
			// checkBox1
			// 
			this.checkBox1.Location = new System.Drawing.Point(6, 20);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(53, 24);
			this.checkBox1.TabIndex = 0;
			this.checkBox1.Text = "老鼠";
			this.checkBox1.UseVisualStyleBackColor = true;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.checkBox20);
			this.groupBox2.Controls.Add(this.checkBox11);
			this.groupBox2.Controls.Add(this.checkBox15);
			this.groupBox2.Controls.Add(this.checkBox16);
			this.groupBox2.Controls.Add(this.checkBox19);
			this.groupBox2.Controls.Add(this.checkBox12);
			this.groupBox2.Controls.Add(this.checkBox14);
			this.groupBox2.Controls.Add(this.checkBox17);
			this.groupBox2.Controls.Add(this.checkBox18);
			this.groupBox2.Controls.Add(this.checkBox13);
			this.groupBox2.Location = new System.Drawing.Point(133, 79);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(125, 173);
			this.groupBox2.TabIndex = 0;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "必须不含";
			// 
			// checkBox20
			// 
			this.checkBox20.Location = new System.Drawing.Point(65, 140);
			this.checkBox20.Name = "checkBox20";
			this.checkBox20.Size = new System.Drawing.Size(53, 24);
			this.checkBox20.TabIndex = 0;
			this.checkBox20.Text = "大象";
			this.checkBox20.UseVisualStyleBackColor = true;
			// 
			// checkBox11
			// 
			this.checkBox11.Location = new System.Drawing.Point(6, 20);
			this.checkBox11.Name = "checkBox11";
			this.checkBox11.Size = new System.Drawing.Size(53, 24);
			this.checkBox11.TabIndex = 0;
			this.checkBox11.Text = "老鼠";
			this.checkBox11.UseVisualStyleBackColor = true;
			// 
			// checkBox15
			// 
			this.checkBox15.Location = new System.Drawing.Point(6, 140);
			this.checkBox15.Name = "checkBox15";
			this.checkBox15.Size = new System.Drawing.Size(53, 24);
			this.checkBox15.TabIndex = 0;
			this.checkBox15.Text = "野猪";
			this.checkBox15.UseVisualStyleBackColor = true;
			// 
			// checkBox16
			// 
			this.checkBox16.Location = new System.Drawing.Point(65, 20);
			this.checkBox16.Name = "checkBox16";
			this.checkBox16.Size = new System.Drawing.Size(53, 24);
			this.checkBox16.TabIndex = 0;
			this.checkBox16.Text = "狼";
			this.checkBox16.UseVisualStyleBackColor = true;
			// 
			// checkBox19
			// 
			this.checkBox19.Location = new System.Drawing.Point(65, 110);
			this.checkBox19.Name = "checkBox19";
			this.checkBox19.Size = new System.Drawing.Size(53, 24);
			this.checkBox19.TabIndex = 0;
			this.checkBox19.Text = "老虎";
			this.checkBox19.UseVisualStyleBackColor = true;
			// 
			// checkBox12
			// 
			this.checkBox12.Location = new System.Drawing.Point(6, 50);
			this.checkBox12.Name = "checkBox12";
			this.checkBox12.Size = new System.Drawing.Size(53, 24);
			this.checkBox12.TabIndex = 0;
			this.checkBox12.Text = "蜘蛛";
			this.checkBox12.UseVisualStyleBackColor = true;
			// 
			// checkBox14
			// 
			this.checkBox14.Location = new System.Drawing.Point(6, 110);
			this.checkBox14.Name = "checkBox14";
			this.checkBox14.Size = new System.Drawing.Size(53, 24);
			this.checkBox14.TabIndex = 0;
			this.checkBox14.Text = "蝙蝠";
			this.checkBox14.UseVisualStyleBackColor = true;
			// 
			// checkBox17
			// 
			this.checkBox17.Location = new System.Drawing.Point(65, 50);
			this.checkBox17.Name = "checkBox17";
			this.checkBox17.Size = new System.Drawing.Size(53, 24);
			this.checkBox17.TabIndex = 0;
			this.checkBox17.Text = "熊";
			this.checkBox17.UseVisualStyleBackColor = true;
			// 
			// checkBox18
			// 
			this.checkBox18.Location = new System.Drawing.Point(65, 80);
			this.checkBox18.Name = "checkBox18";
			this.checkBox18.Size = new System.Drawing.Size(53, 24);
			this.checkBox18.TabIndex = 0;
			this.checkBox18.Text = "鳄鱼";
			this.checkBox18.UseVisualStyleBackColor = true;
			// 
			// checkBox13
			// 
			this.checkBox13.Location = new System.Drawing.Point(6, 80);
			this.checkBox13.Name = "checkBox13";
			this.checkBox13.Size = new System.Drawing.Size(53, 24);
			this.checkBox13.TabIndex = 0;
			this.checkBox13.Text = "蛇";
			this.checkBox13.UseVisualStyleBackColor = true;
			// 
			// AnimalSearching
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(680, 354);
			this.CloseButton = false;
			this.Controls.Add(this.flowLayoutPanel3);
			this.Controls.Add(this.listView1);
			this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)(((((WeifenLuo.WinFormsUI.Docking.DockAreas.DockLeft | WeifenLuo.WinFormsUI.Docking.DockAreas.DockRight) 
									| WeifenLuo.WinFormsUI.Docking.DockAreas.DockTop) 
									| WeifenLuo.WinFormsUI.Docking.DockAreas.DockBottom) 
									| WeifenLuo.WinFormsUI.Docking.DockAreas.Document)));
			this.Name = "AnimalSearching";
			this.ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.Document;
			this.TabText = "搜索野兽";
			this.Tag = "AnimalSearching";
			this.Text = "搜索野兽";
			this.Load += new System.EventHandler(this.AnimalSearchingLoad);
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).EndInit();
			this.flowLayoutPanel3.ResumeLayout(false);
			this.flowLayoutPanel3.PerformLayout();
			this.flowLayoutPanel5.ResumeLayout(false);
			this.flowLayoutPanel6.ResumeLayout(false);
			this.flowLayoutPanel4.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.CheckBox checkBox15;
		private System.Windows.Forms.CheckBox checkBox16;
		private System.Windows.Forms.CheckBox checkBox14;
		private System.Windows.Forms.CheckBox checkBox17;
		private System.Windows.Forms.CheckBox checkBox13;
		private System.Windows.Forms.CheckBox checkBox18;
		private System.Windows.Forms.CheckBox checkBox12;
		private System.Windows.Forms.CheckBox checkBox19;
		private System.Windows.Forms.CheckBox checkBox11;
		private System.Windows.Forms.CheckBox checkBox20;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.CheckBox checkBox6;
		private System.Windows.Forms.CheckBox checkBox7;
		private System.Windows.Forms.CheckBox checkBox8;
		private System.Windows.Forms.CheckBox checkBox9;
		private System.Windows.Forms.CheckBox checkBox10;
		private System.Windows.Forms.CheckBox checkBox2;
		private System.Windows.Forms.CheckBox checkBox1;
		private System.Windows.Forms.CheckBox checkBox3;
		private System.Windows.Forms.CheckBox checkBox4;
		private System.Windows.Forms.CheckBox checkBox5;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel6;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel5;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel4;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
		private System.Windows.Forms.NumericUpDown numericUpDown3;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.NumericUpDown numericUpDown2;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.NumericUpDown numericUpDown1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		public System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button1;
		public System.Windows.Forms.TextBox textBox1;
	}
}
