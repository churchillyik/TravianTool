/*
 * Created by SharpDevelop.
 * User: Administrator
 * Date: 2011-6-23
 * Time: 17:05
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace Stran
{
	partial class EvadeAttack
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
			this.lblY = new System.Windows.Forms.Label();
			this.txtY = new System.Windows.Forms.TextBox();
			this.lblX = new System.Windows.Forms.Label();
			this.txtX = new System.Windows.Forms.TextBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label9 = new System.Windows.Forms.Label();
			this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
			this.label8 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
			this.label2 = new System.Windows.Forms.Label();
			this.buttonOK = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.CBT11 = new System.Windows.Forms.CheckBox();
			this.CBT10 = new System.Windows.Forms.CheckBox();
			this.CBT9 = new System.Windows.Forms.CheckBox();
			this.CBT6 = new System.Windows.Forms.CheckBox();
			this.CBT3 = new System.Windows.Forms.CheckBox();
			this.CBT8 = new System.Windows.Forms.CheckBox();
			this.CBT5 = new System.Windows.Forms.CheckBox();
			this.CBT2 = new System.Windows.Forms.CheckBox();
			this.CBT7 = new System.Windows.Forms.CheckBox();
			this.CBT4 = new System.Windows.Forms.CheckBox();
			this.CBT1 = new System.Windows.Forms.CheckBox();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// lblY
			// 
			this.lblY.AutoSize = true;
			this.lblY.Location = new System.Drawing.Point(106, 26);
			this.lblY.Name = "lblY";
			this.lblY.Size = new System.Drawing.Size(11, 12);
			this.lblY.TabIndex = 69;
			this.lblY.Text = "Y";
			// 
			// txtY
			// 
			this.txtY.Location = new System.Drawing.Point(130, 23);
			this.txtY.Name = "txtY";
			this.txtY.Size = new System.Drawing.Size(48, 21);
			this.txtY.TabIndex = 2;
			this.txtY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// lblX
			// 
			this.lblX.AutoSize = true;
			this.lblX.Location = new System.Drawing.Point(17, 26);
			this.lblX.Name = "lblX";
			this.lblX.Size = new System.Drawing.Size(11, 12);
			this.lblX.TabIndex = 67;
			this.lblX.Text = "X";
			// 
			// txtX
			// 
			this.txtX.Location = new System.Drawing.Point(40, 22);
			this.txtX.Name = "txtX";
			this.txtX.Size = new System.Drawing.Size(48, 21);
			this.txtX.TabIndex = 1;
			this.txtX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.txtY);
			this.groupBox1.Controls.Add(this.lblY);
			this.groupBox1.Controls.Add(this.txtX);
			this.groupBox1.Controls.Add(this.lblX);
			this.groupBox1.Location = new System.Drawing.Point(12, 24);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(204, 68);
			this.groupBox1.TabIndex = 70;
			this.groupBox1.TabStop = false;
			this.groupBox1.Tag = "GBReinforce";
			this.groupBox1.Text = "增援坐标";
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(449, 29);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(17, 12);
			this.label9.TabIndex = 73;
			this.label9.Tag = "Seconds";
			this.label9.Text = "秒";
			// 
			// numericUpDown1
			// 
			this.numericUpDown1.Location = new System.Drawing.Point(354, 24);
			this.numericUpDown1.Maximum = new decimal(new int[] {
									3600,
									0,
									0,
									0});
			this.numericUpDown1.Name = "numericUpDown1";
			this.numericUpDown1.Size = new System.Drawing.Size(89, 21);
			this.numericUpDown1.TabIndex = 3;
			this.numericUpDown1.Value = new decimal(new int[] {
									1200,
									0,
									0,
									0});
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(235, 31);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(101, 12);
			this.label8.TabIndex = 72;
			this.label8.Tag = "LBLChkInterval";
			this.label8.Text = "多少秒检查一次：";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(235, 73);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(113, 12);
			this.label1.TabIndex = 72;
			this.label1.Tag = "LBLEvadeLeadTime";
			this.label1.Text = "受攻前多少秒回避：";
			// 
			// numericUpDown2
			// 
			this.numericUpDown2.Location = new System.Drawing.Point(354, 71);
			this.numericUpDown2.Maximum = new decimal(new int[] {
									3600,
									0,
									0,
									0});
			this.numericUpDown2.Name = "numericUpDown2";
			this.numericUpDown2.Size = new System.Drawing.Size(89, 21);
			this.numericUpDown2.TabIndex = 4;
			this.numericUpDown2.Value = new decimal(new int[] {
									180,
									0,
									0,
									0});
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(449, 73);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(17, 12);
			this.label2.TabIndex = 73;
			this.label2.Tag = "Seconds";
			this.label2.Text = "秒";
			// 
			// buttonOK
			// 
			this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonOK.Location = new System.Drawing.Point(115, 242);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new System.Drawing.Size(75, 23);
			this.buttonOK.TabIndex = 5;
			this.buttonOK.Tag = "BTNOK";
			this.buttonOK.Text = "确定";
			this.buttonOK.UseVisualStyleBackColor = true;
			this.buttonOK.Click += new System.EventHandler(this.ButtonOKClick);
			// 
			// buttonCancel
			// 
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new System.Drawing.Point(240, 242);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(75, 23);
			this.buttonCancel.TabIndex = 6;
			this.buttonCancel.Tag = "BTNCancel";
			this.buttonCancel.Text = "取消";
			this.buttonCancel.UseVisualStyleBackColor = true;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.CBT11);
			this.groupBox2.Controls.Add(this.CBT10);
			this.groupBox2.Controls.Add(this.CBT9);
			this.groupBox2.Controls.Add(this.CBT6);
			this.groupBox2.Controls.Add(this.CBT3);
			this.groupBox2.Controls.Add(this.CBT8);
			this.groupBox2.Controls.Add(this.CBT5);
			this.groupBox2.Controls.Add(this.CBT2);
			this.groupBox2.Controls.Add(this.CBT7);
			this.groupBox2.Controls.Add(this.CBT4);
			this.groupBox2.Controls.Add(this.CBT1);
			this.groupBox2.Location = new System.Drawing.Point(12, 108);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(453, 111);
			this.groupBox2.TabIndex = 74;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "选择需要回避的部队";
			// 
			// CBT11
			// 
			this.CBT11.Location = new System.Drawing.Point(351, 80);
			this.CBT11.Name = "CBT11";
			this.CBT11.Size = new System.Drawing.Size(89, 24);
			this.CBT11.TabIndex = 0;
			this.CBT11.Text = "T11";
			this.CBT11.UseVisualStyleBackColor = true;
			// 
			// CBT10
			// 
			this.CBT10.Checked = true;
			this.CBT10.CheckState = System.Windows.Forms.CheckState.Checked;
			this.CBT10.Location = new System.Drawing.Point(351, 50);
			this.CBT10.Name = "CBT10";
			this.CBT10.Size = new System.Drawing.Size(89, 24);
			this.CBT10.TabIndex = 0;
			this.CBT10.Text = "T10";
			this.CBT10.UseVisualStyleBackColor = true;
			// 
			// CBT9
			// 
			this.CBT9.Checked = true;
			this.CBT9.CheckState = System.Windows.Forms.CheckState.Checked;
			this.CBT9.Location = new System.Drawing.Point(351, 21);
			this.CBT9.Name = "CBT9";
			this.CBT9.Size = new System.Drawing.Size(89, 24);
			this.CBT9.TabIndex = 0;
			this.CBT9.Text = "T9";
			this.CBT9.UseVisualStyleBackColor = true;
			// 
			// CBT6
			// 
			this.CBT6.Checked = true;
			this.CBT6.CheckState = System.Windows.Forms.CheckState.Checked;
			this.CBT6.Location = new System.Drawing.Point(130, 82);
			this.CBT6.Name = "CBT6";
			this.CBT6.Size = new System.Drawing.Size(106, 24);
			this.CBT6.TabIndex = 0;
			this.CBT6.Text = "T6";
			this.CBT6.UseVisualStyleBackColor = true;
			// 
			// CBT3
			// 
			this.CBT3.Checked = true;
			this.CBT3.CheckState = System.Windows.Forms.CheckState.Checked;
			this.CBT3.Location = new System.Drawing.Point(16, 80);
			this.CBT3.Name = "CBT3";
			this.CBT3.Size = new System.Drawing.Size(101, 24);
			this.CBT3.TabIndex = 0;
			this.CBT3.Text = "T3";
			this.CBT3.UseVisualStyleBackColor = true;
			// 
			// CBT8
			// 
			this.CBT8.Checked = true;
			this.CBT8.CheckState = System.Windows.Forms.CheckState.Checked;
			this.CBT8.Location = new System.Drawing.Point(242, 51);
			this.CBT8.Name = "CBT8";
			this.CBT8.Size = new System.Drawing.Size(103, 24);
			this.CBT8.TabIndex = 0;
			this.CBT8.Text = "T8";
			this.CBT8.UseVisualStyleBackColor = true;
			// 
			// CBT5
			// 
			this.CBT5.Checked = true;
			this.CBT5.CheckState = System.Windows.Forms.CheckState.Checked;
			this.CBT5.Location = new System.Drawing.Point(130, 51);
			this.CBT5.Name = "CBT5";
			this.CBT5.Size = new System.Drawing.Size(106, 24);
			this.CBT5.TabIndex = 0;
			this.CBT5.Text = "T5";
			this.CBT5.UseVisualStyleBackColor = true;
			// 
			// CBT2
			// 
			this.CBT2.Checked = true;
			this.CBT2.CheckState = System.Windows.Forms.CheckState.Checked;
			this.CBT2.Location = new System.Drawing.Point(16, 50);
			this.CBT2.Name = "CBT2";
			this.CBT2.Size = new System.Drawing.Size(101, 24);
			this.CBT2.TabIndex = 0;
			this.CBT2.Text = "T2";
			this.CBT2.UseVisualStyleBackColor = true;
			// 
			// CBT7
			// 
			this.CBT7.Checked = true;
			this.CBT7.CheckState = System.Windows.Forms.CheckState.Checked;
			this.CBT7.Location = new System.Drawing.Point(242, 21);
			this.CBT7.Name = "CBT7";
			this.CBT7.Size = new System.Drawing.Size(103, 24);
			this.CBT7.TabIndex = 0;
			this.CBT7.Text = "T7";
			this.CBT7.UseVisualStyleBackColor = true;
			// 
			// CBT4
			// 
			this.CBT4.Checked = true;
			this.CBT4.CheckState = System.Windows.Forms.CheckState.Checked;
			this.CBT4.Location = new System.Drawing.Point(130, 21);
			this.CBT4.Name = "CBT4";
			this.CBT4.Size = new System.Drawing.Size(106, 24);
			this.CBT4.TabIndex = 0;
			this.CBT4.Text = "T4";
			this.CBT4.UseVisualStyleBackColor = true;
			// 
			// CBT1
			// 
			this.CBT1.Checked = true;
			this.CBT1.CheckState = System.Windows.Forms.CheckState.Checked;
			this.CBT1.Location = new System.Drawing.Point(16, 21);
			this.CBT1.Name = "CBT1";
			this.CBT1.Size = new System.Drawing.Size(101, 24);
			this.CBT1.TabIndex = 0;
			this.CBT1.Text = "T1";
			this.CBT1.UseVisualStyleBackColor = true;
			// 
			// EvadeAttack
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(480, 277);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.buttonOK);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.numericUpDown2);
			this.Controls.Add(this.numericUpDown1);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.groupBox1);
			this.Name = "EvadeAttack";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Tag = "FormEvadeAttack";
			this.Text = "回避攻击";
			this.Load += new System.EventHandler(this.EvadeAttackLoad);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
			this.groupBox2.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.CheckBox CBT10;
		private System.Windows.Forms.CheckBox CBT11;
		private System.Windows.Forms.CheckBox CBT9;
		private System.Windows.Forms.CheckBox CBT6;
		private System.Windows.Forms.CheckBox CBT3;
		private System.Windows.Forms.CheckBox CBT8;
		private System.Windows.Forms.CheckBox CBT5;
		private System.Windows.Forms.CheckBox CBT2;
		private System.Windows.Forms.CheckBox CBT7;
		private System.Windows.Forms.CheckBox CBT4;
		private System.Windows.Forms.CheckBox CBT1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.NumericUpDown numericUpDown2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.NumericUpDown numericUpDown1;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TextBox txtX;
		private System.Windows.Forms.Label lblX;
		private System.Windows.Forms.TextBox txtY;
		private System.Windows.Forms.Label lblY;
	}
}
