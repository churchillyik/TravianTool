namespace Stran
{
	partial class ResourceLimit
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
			this.lblDescription = new System.Windows.Forms.Label();
			this.lblVillage = new System.Windows.Forms.Label();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOK = new System.Windows.Forms.Button();
			this.nudLimit1 = new System.Windows.Forms.NumericUpDown();
			this.nudLimit2 = new System.Windows.Forms.NumericUpDown();
			this.nudLimit3 = new System.Windows.Forms.NumericUpDown();
			this.nudLimit4 = new System.Windows.Forms.NumericUpDown();
			this.button1 = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.nudLimit1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nudLimit2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nudLimit3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nudLimit4)).BeginInit();
			this.SuspendLayout();
			// 
			// lblDescription
			// 
			this.lblDescription.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblDescription.Location = new System.Drawing.Point(30, 56);
			this.lblDescription.Name = "lblDescription";
			this.lblDescription.Size = new System.Drawing.Size(270, 40);
			this.lblDescription.TabIndex = 8;
			this.lblDescription.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lblVillage
			// 
			this.lblVillage.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblVillage.Location = new System.Drawing.Point(30, 26);
			this.lblVillage.Name = "lblVillage";
			this.lblVillage.Size = new System.Drawing.Size(270, 30);
			this.lblVillage.TabIndex = 7;
			this.lblVillage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(125, 163);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(80, 32);
            this.btnCancel.TabIndex = 5;
			this.btnCancel.Tag = "cancel";
			this.btnCancel.Text = "取消";
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// btnOK
			// 
			this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOK.Location = new System.Drawing.Point(39, 163);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(80, 32);
            this.btnOK.TabIndex = 4;
			this.btnOK.Tag = "ok";
			this.btnOK.Text = "确定";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// nudLimit1
			// 
			this.nudLimit1.Location = new System.Drawing.Point(16, 112);
			this.nudLimit1.Name = "nudLimit1";
			this.nudLimit1.Size = new System.Drawing.Size(70, 22);
            this.nudLimit1.TabIndex = 0;
			this.nudLimit1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// nudLimit2
			// 
			this.nudLimit2.Location = new System.Drawing.Point(92, 112);
			this.nudLimit2.Name = "nudLimit2";
			this.nudLimit2.Size = new System.Drawing.Size(70, 22);
            this.nudLimit2.TabIndex = 1;
			this.nudLimit2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// nudLimit3
			// 
			this.nudLimit3.Location = new System.Drawing.Point(168, 112);
			this.nudLimit3.Name = "nudLimit3";
			this.nudLimit3.Size = new System.Drawing.Size(70, 22);
            this.nudLimit3.TabIndex = 2;
			this.nudLimit3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// nudLimit4
			// 
			this.nudLimit4.Location = new System.Drawing.Point(244, 112);
			this.nudLimit4.Name = "nudLimit4";
			this.nudLimit4.Size = new System.Drawing.Size(70, 22);
            this.nudLimit4.TabIndex = 3;
			this.nudLimit4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// button1
			// 
			this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.button1.Location = new System.Drawing.Point(211, 163);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(80, 32);
            this.button1.TabIndex = 6;
			this.button1.Tag = "nolimit";
			this.button1.Text = "无限制";
			this.button1.UseVisualStyleBackColor = true;
			// 
			// ResourceLimit
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(331, 230);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.nudLimit4);
			this.Controls.Add(this.nudLimit3);
			this.Controls.Add(this.nudLimit2);
			this.Controls.Add(this.nudLimit1);
			this.Controls.Add(this.lblDescription);
			this.Controls.Add(this.lblVillage);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.Font = new System.Drawing.Font("Tahoma", 9F);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ResourceLimit";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Tag = "ResourceLimit";
			this.Text = "交易限制";
			this.Load += new System.EventHandler(this.ResourceLimit_Load);
			((System.ComponentModel.ISupportInitialize)(this.nudLimit1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudLimit2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudLimit3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudLimit4)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label lblDescription;
		private System.Windows.Forms.Label lblVillage;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.NumericUpDown nudLimit1;
		private System.Windows.Forms.NumericUpDown nudLimit2;
		private System.Windows.Forms.NumericUpDown nudLimit3;
		private System.Windows.Forms.NumericUpDown nudLimit4;
		private System.Windows.Forms.Button button1;
	}
}