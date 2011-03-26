namespace Stran
{
	partial class ActionTiming
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
			this.radioImmediate = new System.Windows.Forms.RadioButton();
			this.radioDelayed = new System.Windows.Forms.RadioButton();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.buttonOK = new System.Windows.Forms.Button();
			this.lableInterval = new System.Windows.Forms.Label();
			this.dateTimeTransferAt = new System.Windows.Forms.DateTimePicker();
			this.label1 = new System.Windows.Forms.Label();
			this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
			this.label2 = new System.Windows.Forms.Label();
			this.labelDetail = new System.Windows.Forms.TextBox();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
			this.SuspendLayout();
			// 
			// radioImmediate
			// 
			this.radioImmediate.AutoSize = true;
			this.radioImmediate.Checked = true;
			this.radioImmediate.Location = new System.Drawing.Point(36, 23);
			this.radioImmediate.Name = "radioImmediate";
			this.radioImmediate.Size = new System.Drawing.Size(128, 18);
			this.radioImmediate.TabIndex = 0;
			this.radioImmediate.TabStop = true;
			this.radioImmediate.Tag = "ImmediateAction";
			this.radioImmediate.Text = "ImmediateTransfer";
			this.radioImmediate.UseVisualStyleBackColor = true;
			this.radioImmediate.CheckedChanged += new System.EventHandler(this.radioDelayed_CheckedChanged);
			// 
			// radioDelayed
			// 
			this.radioDelayed.AutoSize = true;
			this.radioDelayed.Location = new System.Drawing.Point(36, 47);
			this.radioDelayed.Name = "radioDelayed";
			this.radioDelayed.Size = new System.Drawing.Size(113, 18);
            this.radioDelayed.TabIndex = 6;
			this.radioDelayed.Tag = "DelayedAction";
			this.radioDelayed.Text = "DelayedTransfer";
			this.radioDelayed.UseVisualStyleBackColor = true;
			this.radioDelayed.CheckedChanged += new System.EventHandler(this.radioDelayed_CheckedChanged);
			// 
			// buttonCancel
			// 
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new System.Drawing.Point(164, 205);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(80, 34);
            this.buttonCancel.TabIndex = 5;
			this.buttonCancel.Tag = "cancel";
			this.buttonCancel.Text = "取消";
			this.buttonCancel.UseVisualStyleBackColor = true;
			// 
			// buttonOK
			// 
			this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonOK.Location = new System.Drawing.Point(51, 205);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new System.Drawing.Size(80, 34);
            this.buttonOK.TabIndex = 4;
			this.buttonOK.Tag = "ok";
			this.buttonOK.Text = "确定";
			this.buttonOK.UseVisualStyleBackColor = true;
			// 
			// lableInterval
			// 
			this.lableInterval.AutoSize = true;
			this.lableInterval.Location = new System.Drawing.Point(33, 143);
			this.lableInterval.Name = "lableInterval";
			this.lableInterval.Size = new System.Drawing.Size(93, 14);
            this.lableInterval.TabIndex = 8;
			this.lableInterval.Tag = "ActionInterval";
			this.lableInterval.Text = "TransferInterval";
			// 
			// dateTimeTransferAt
			// 
			this.dateTimeTransferAt.CustomFormat = "yyyy-MM-dd  HH:mm:ss";
			this.dateTimeTransferAt.Enabled = false;
			this.dateTimeTransferAt.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dateTimeTransferAt.Location = new System.Drawing.Point(55, 71);
			this.dateTimeTransferAt.MaxDate = new System.DateTime(2050, 12, 31, 0, 0, 0, 0);
			this.dateTimeTransferAt.MinDate = new System.DateTime(2008, 1, 1, 0, 0, 0, 0);
			this.dateTimeTransferAt.Name = "dateTimeTransferAt";
			this.dateTimeTransferAt.ShowUpDown = true;
			this.dateTimeTransferAt.Size = new System.Drawing.Size(193, 22);
            this.dateTimeTransferAt.TabIndex = 1;
			this.dateTimeTransferAt.ValueChanged += new System.EventHandler(this.dateTimeTransferAt_ValueChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(127, 162);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(49, 14);
            this.label1.TabIndex = 9;
			this.label1.Tag = "Minutes";
			this.label1.Text = "Minutes";
			// 
			// numericUpDown1
			// 
			this.numericUpDown1.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
			this.numericUpDown1.Location = new System.Drawing.Point(55, 160);
			this.numericUpDown1.Maximum = new decimal(new int[] {
            1440,
            0,
            0,
            0});
			this.numericUpDown1.Name = "numericUpDown1";
			this.numericUpDown1.Size = new System.Drawing.Size(66, 22);
            this.numericUpDown1.TabIndex = 3;
			this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(33, 104);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(96, 14);
            this.label2.TabIndex = 7;
			this.label2.Tag = "ActionArriveAt";
			this.label2.Text = "TransferArriveAt";
			// 
			// labelDetail
			// 
			this.labelDetail.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.labelDetail.Location = new System.Drawing.Point(55, 121);
			this.labelDetail.Name = "labelDetail";
			this.labelDetail.ReadOnly = true;
			this.labelDetail.Size = new System.Drawing.Size(193, 15);
            this.labelDetail.TabIndex = 2;
			this.labelDetail.TabStop = false;
			this.labelDetail.Text = "2008-11-12 14:01:42";
			// 
			// ActionTiming
			// 
			this.AcceptButton = this.buttonOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.buttonCancel;
			this.ClientSize = new System.Drawing.Size(294, 270);
			this.Controls.Add(this.labelDetail);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.numericUpDown1);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.dateTimeTransferAt);
			this.Controls.Add(this.lableInterval);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.buttonOK);
			this.Controls.Add(this.radioDelayed);
			this.Controls.Add(this.radioImmediate);
			this.Font = new System.Drawing.Font("Tahoma", 9F);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ActionTiming";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Tag = "ActionTiming";
			this.Text = "TransferTiming";
			this.Load += new System.EventHandler(this.TransferTiming_Load);
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.RadioButton radioImmediate;
		private System.Windows.Forms.RadioButton radioDelayed;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.Label lableInterval;
		private System.Windows.Forms.DateTimePicker dateTimeTransferAt;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.NumericUpDown numericUpDown1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox labelDetail;
	}
}