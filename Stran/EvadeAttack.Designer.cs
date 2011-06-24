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
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
			this.SuspendLayout();
			// 
			// lblY
			// 
			this.lblY.AutoSize = true;
			this.lblY.Location = new System.Drawing.Point(142, 26);
			this.lblY.Name = "lblY";
			this.lblY.Size = new System.Drawing.Size(11, 12);
			this.lblY.TabIndex = 69;
			this.lblY.Text = "Y";
			// 
			// txtY
			// 
			this.txtY.Location = new System.Drawing.Point(166, 23);
			this.txtY.Name = "txtY";
			this.txtY.Size = new System.Drawing.Size(48, 21);
			this.txtY.TabIndex = 2;
			this.txtY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// lblX
			// 
			this.lblX.AutoSize = true;
			this.lblX.Location = new System.Drawing.Point(29, 27);
			this.lblX.Name = "lblX";
			this.lblX.Size = new System.Drawing.Size(11, 12);
			this.lblX.TabIndex = 67;
			this.lblX.Text = "X";
			// 
			// txtX
			// 
			this.txtX.Location = new System.Drawing.Point(52, 23);
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
			this.groupBox1.Location = new System.Drawing.Point(25, 22);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(256, 61);
			this.groupBox1.TabIndex = 70;
			this.groupBox1.TabStop = false;
			this.groupBox1.Tag = "GBReinforce";
			this.groupBox1.Text = "增援坐标";
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(264, 99);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(17, 12);
			this.label9.TabIndex = 73;
			this.label9.Tag = "Seconds";
			this.label9.Text = "秒";
			// 
			// numericUpDown1
			// 
			this.numericUpDown1.Location = new System.Drawing.Point(169, 94);
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
			this.label8.Location = new System.Drawing.Point(27, 99);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(101, 12);
			this.label8.TabIndex = 72;
			this.label8.Tag = "LBLChkInterval";
			this.label8.Text = "多少秒检查一次：";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(27, 131);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(113, 12);
			this.label1.TabIndex = 72;
			this.label1.Tag = "LBLEvadeLeadTime";
			this.label1.Text = "受攻前多少秒回避：";
			// 
			// numericUpDown2
			// 
			this.numericUpDown2.Location = new System.Drawing.Point(169, 126);
			this.numericUpDown2.Maximum = new decimal(new int[] {
									1200,
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
			this.label2.Location = new System.Drawing.Point(264, 131);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(17, 12);
			this.label2.TabIndex = 73;
			this.label2.Tag = "Seconds";
			this.label2.Text = "秒";
			// 
			// buttonOK
			// 
			this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonOK.Location = new System.Drawing.Point(65, 169);
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
			this.buttonCancel.Location = new System.Drawing.Point(169, 169);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(75, 23);
			this.buttonCancel.TabIndex = 6;
			this.buttonCancel.Tag = "BTNCancel";
			this.buttonCancel.Text = "取消";
			this.buttonCancel.UseVisualStyleBackColor = true;
			// 
			// EvadeAttack
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(311, 204);
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
			this.ResumeLayout(false);
			this.PerformLayout();
		}
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
