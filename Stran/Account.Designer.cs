namespace Stran
{
	partial class NewAccount
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
			this.button1 = new System.Windows.Forms.Button();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.button2 = new System.Windows.Forms.Button();
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.textBox3 = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.textBox4 = new System.Windows.Forms.TextBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
			this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
			this.label7 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.button1.Location = new System.Drawing.Point(81, 294);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(80, 32);
			this.button1.TabIndex = 5;
			this.button1.Tag = "save";
			this.button1.Text = "保存";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(24, 96);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(93, 14);
			this.label4.TabIndex = 9;
			this.label4.Tag = "password";
			this.label4.Text = "密码";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(24, 61);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(93, 14);
			this.label5.TabIndex = 8;
			this.label5.Tag = "username";
			this.label5.Text = "用户名";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(24, 26);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(93, 14);
			this.label6.TabIndex = 7;
			this.label6.Tag = "server";
			this.label6.Text = "服务器";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(135, 93);
			this.textBox1.Name = "textBox1";
			this.textBox1.PasswordChar = 'X';
			this.textBox1.Size = new System.Drawing.Size(180, 22);
			this.textBox1.TabIndex = 2;
			this.textBox1.UseSystemPasswordChar = true;
			// 
			// textBox2
			// 
			this.textBox2.Location = new System.Drawing.Point(135, 58);
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new System.Drawing.Size(180, 22);
			this.textBox2.TabIndex = 1;
			// 
			// button2
			// 
			this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.button2.Location = new System.Drawing.Point(191, 294);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(80, 32);
			this.button2.TabIndex = 6;
			this.button2.Tag = "cancel";
			this.button2.Text = "取消";
			this.button2.UseVisualStyleBackColor = true;
			// 
			// comboBox1
			// 
			this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox1.FormattingEnabled = true;
			this.comboBox1.Location = new System.Drawing.Point(135, 128);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(180, 22);
			this.comboBox1.TabIndex = 3;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(24, 131);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(93, 14);
			this.label1.TabIndex = 10;
			this.label1.Tag = "tribe";
			this.label1.Text = "种族";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textBox3
			// 
			this.textBox3.Location = new System.Drawing.Point(135, 23);
			this.textBox3.Name = "textBox3";
			this.textBox3.Size = new System.Drawing.Size(180, 22);
			this.textBox3.TabIndex = 0;
			this.textBox3.Leave += new System.EventHandler(this.textBox3_Leave);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(24, 167);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(93, 14);
			this.label2.TabIndex = 11;
			this.label2.Tag = "displang";
			this.label2.Text = "显示语言";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textBox4
			// 
			this.textBox4.Location = new System.Drawing.Point(135, 164);
			this.textBox4.Name = "textBox4";
			this.textBox4.Size = new System.Drawing.Size(180, 22);
			this.textBox4.TabIndex = 4;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.numericUpDown2);
			this.groupBox1.Controls.Add(this.numericUpDown1);
			this.groupBox1.Controls.Add(this.label7);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Location = new System.Drawing.Point(12, 199);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(320, 89);
			this.groupBox1.TabIndex = 12;
			this.groupBox1.TabStop = false;
			this.groupBox1.Tag = "GrpBxSafety";
			this.groupBox1.Text = "防封号设置";
			// 
			// numericUpDown2
			// 
			this.numericUpDown2.Location = new System.Drawing.Point(197, 60);
			this.numericUpDown2.Name = "numericUpDown2";
			this.numericUpDown2.Size = new System.Drawing.Size(105, 22);
			this.numericUpDown2.TabIndex = 12;
			this.numericUpDown2.Value = new decimal(new int[] {
									20,
									0,
									0,
									0});
			// 
			// numericUpDown1
			// 
			this.numericUpDown1.Location = new System.Drawing.Point(198, 27);
			this.numericUpDown1.Name = "numericUpDown1";
			this.numericUpDown1.Size = new System.Drawing.Size(105, 22);
			this.numericUpDown1.TabIndex = 12;
			this.numericUpDown1.Value = new decimal(new int[] {
									1,
									0,
									0,
									0});
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(12, 62);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(179, 14);
			this.label7.TabIndex = 11;
			this.label7.Tag = "LBLGetOrPostDelaySeconds";
			this.label7.Text = "每段连续通讯之间的间隔(秒)";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(6, 27);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(185, 20);
			this.label3.TabIndex = 11;
			this.label3.Tag = "LBLGetOrPostTimesThreshold";
			this.label3.Text = "最多可连续执行通讯操作的次数";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// NewAccount
			// 
			this.AcceptButton = this.button1;
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.button2;
			this.ClientSize = new System.Drawing.Size(344, 348);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.textBox4);
			this.Controls.Add(this.textBox3);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.comboBox1);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.textBox2);
			this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "NewAccount";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Tag = "account";
			this.Text = "帐号";
			this.Load += new System.EventHandler(this.NewAccount_Load);
			this.groupBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.NumericUpDown numericUpDown1;
		private System.Windows.Forms.NumericUpDown numericUpDown2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.GroupBox groupBox1;

		#endregion

		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.TextBox textBox2;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.ComboBox comboBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBox3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textBox4;

	}
}