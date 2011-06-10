using System.Windows.Forms;
namespace Stran
{
	partial class AttackOptForm
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
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.label4 = new System.Windows.Forms.Label();
			this.buttonTiming = new System.Windows.Forms.Button();
			this.txtY = new System.Windows.Forms.TextBox();
			this.txtX = new System.Windows.Forms.TextBox();
			this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
			this.label5 = new System.Windows.Forms.Label();
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.comboBox2 = new System.Windows.Forms.ComboBox();
			this.label6 = new System.Windows.Forms.Label();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.comboBox3 = new System.Windows.Forms.ComboBox();
			this.label7 = new System.Windows.Forms.Label();
			this.checkBox2 = new System.Windows.Forms.CheckBox();
			this.textCoord = new System.Windows.Forms.TextBox();
			this.tableLayoutPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.BackColor = System.Drawing.Color.Cornsilk;
			this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
			this.tableLayoutPanel1.ColumnCount = 7;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 55F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 55F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 55F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 55F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 59F));
			this.tableLayoutPanel1.Controls.Add(this.label2, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
			this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 8);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 12;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(408, 380);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// label2
			// 
			this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label2.Location = new System.Drawing.Point(66, 1);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(54, 37);
			this.label2.TabIndex = 1;
			this.label2.Tag = "troopamount";
			this.label2.Text = "troopavailable";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label1
			// 
			this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label1.Location = new System.Drawing.Point(4, 1);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(55, 37);
			this.label1.TabIndex = 0;
			this.label1.Tag = "troopname";
			this.label1.Text = "troopname troopname";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// button1
			// 
			this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.button1.Location = new System.Drawing.Point(426, 358);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(80, 30);
			this.button1.TabIndex = 11;
			this.button1.Tag = "ok";
			this.button1.Text = "ok";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.button2.Location = new System.Drawing.Point(512, 358);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(80, 30);
			this.button2.TabIndex = 12;
			this.button2.Tag = "cancel";
			this.button2.Text = "cancel";
			this.button2.UseVisualStyleBackColor = true;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(426, 68);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(38, 14);
			this.label4.TabIndex = 22;
			this.label4.Tag = "coord";
			this.label4.Text = "label4";
			// 
			// buttonTiming
			// 
			this.buttonTiming.Location = new System.Drawing.Point(426, 306);
			this.buttonTiming.Name = "buttonTiming";
			this.buttonTiming.Size = new System.Drawing.Size(80, 32);
			this.buttonTiming.TabIndex = 10;
			this.buttonTiming.Tag = "Timing";
			this.buttonTiming.Text = "定时";
			this.buttonTiming.UseVisualStyleBackColor = true;
			this.buttonTiming.Click += new System.EventHandler(this.buttonTiming_Click);
			// 
			// txtY
			// 
			this.txtY.Location = new System.Drawing.Point(485, 85);
			this.txtY.Name = "txtY";
			this.txtY.Size = new System.Drawing.Size(50, 22);
			this.txtY.TabIndex = 3;
			// 
			// txtX
			// 
			this.txtX.Location = new System.Drawing.Point(426, 85);
			this.txtX.Name = "txtX";
			this.txtX.Size = new System.Drawing.Size(50, 22);
			this.txtX.TabIndex = 2;
			// 
			// numericUpDown1
			// 
			this.numericUpDown1.Location = new System.Drawing.Point(426, 33);
			this.numericUpDown1.Minimum = new decimal(new int[] {
									1,
									0,
									0,
									0});
			this.numericUpDown1.Name = "numericUpDown1";
			this.numericUpDown1.Size = new System.Drawing.Size(100, 22);
			this.numericUpDown1.TabIndex = 1;
			this.numericUpDown1.ThousandsSeparator = true;
			this.numericUpDown1.Value = new decimal(new int[] {
									1,
									0,
									0,
									0});
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(426, 8);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(100, 22);
			this.label5.TabIndex = 31;
			this.label5.Tag = "transfercnt";
			this.label5.Text = "transfercnt";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// comboBox1
			// 
			this.comboBox1.FormattingEnabled = true;
			this.comboBox1.Location = new System.Drawing.Point(426, 213);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(140, 22);
			this.comboBox1.TabIndex = 6;
			// 
			// comboBox2
			// 
			this.comboBox2.FormattingEnabled = true;
			this.comboBox2.Location = new System.Drawing.Point(426, 238);
			this.comboBox2.Name = "comboBox2";
			this.comboBox2.Size = new System.Drawing.Size(140, 22);
			this.comboBox2.TabIndex = 7;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(423, 196);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(31, 14);
			this.label6.TabIndex = 37;
			this.label6.Tag = "katatarget";
			this.label6.Text = "Kata";
			// 
			// checkBox1
			// 
			this.checkBox1.AutoSize = true;
			this.checkBox1.Location = new System.Drawing.Point(426, 267);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(52, 18);
			this.checkBox1.TabIndex = 8;
			this.checkBox1.Text = "Hero";
			this.checkBox1.UseVisualStyleBackColor = true;
			// 
			// comboBox3
			// 
			this.comboBox3.FormattingEnabled = true;
			this.comboBox3.Location = new System.Drawing.Point(426, 171);
			this.comboBox3.Name = "comboBox3";
			this.comboBox3.Size = new System.Drawing.Size(140, 22);
			this.comboBox3.TabIndex = 5;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(426, 154);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(54, 14);
			this.label7.TabIndex = 40;
			this.label7.Tag = "raidtype";
			this.label7.Text = "Raidtype";
			// 
			// checkBox2
			// 
			this.checkBox2.AutoSize = true;
			this.checkBox2.Location = new System.Drawing.Point(514, 267);
			this.checkBox2.Name = "checkBox2";
			this.checkBox2.Size = new System.Drawing.Size(68, 18);
			this.checkBox2.TabIndex = 9;
			this.checkBox2.Text = "Settlers";
			this.checkBox2.UseVisualStyleBackColor = true;
			this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
			// 
			// textCoord
			// 
			this.textCoord.Location = new System.Drawing.Point(426, 114);
			this.textCoord.Name = "textCoord";
			this.textCoord.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
			this.textCoord.Size = new System.Drawing.Size(140, 22);
			this.textCoord.TabIndex = 4;
			// 
			// AttackOptForm
			// 
			this.AcceptButton = this.button1;
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.button2;
			this.ClientSize = new System.Drawing.Size(594, 392);
			this.Controls.Add(this.textCoord);
			this.Controls.Add(this.checkBox2);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.comboBox3);
			this.Controls.Add(this.checkBox1);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.comboBox2);
			this.Controls.Add(this.comboBox1);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.numericUpDown1);
			this.Controls.Add(this.txtY);
			this.Controls.Add(this.txtX);
			this.Controls.Add(this.buttonTiming);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "AttackOptForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Tag = "SendTroopsAttack";
			this.Text = "AttackOptForm";
			this.Load += new System.EventHandler(this.AttackOptForm_Load);
			this.tableLayoutPanel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();
		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private Button button1;
        private Button button2;
		private Label label2;
		private Label label1;
        private Label label4;
        private Button buttonTiming;
        private TextBox txtY;
        private TextBox txtX;
        private NumericUpDown numericUpDown1;
        private Label label5;
        private ComboBox comboBox1;
        private ComboBox comboBox2;
        private Label label6;
        private CheckBox checkBox1;
        private ComboBox comboBox3;
        private Label label7;
        private CheckBox checkBox2;
        private TextBox textCoord;
	}
}