/*
 * Created by SharpDevelop.
 * User: Administrator
 * Date: 2011-5-18
 * Time: 15:48
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace Stran
{
	partial class PMMail
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
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.checkBox_SSLEnable = new System.Windows.Forms.CheckBox();
			this.tbPort = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.tbServer = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.tbPass = new System.Windows.Forms.TextBox();
			this.tbDomain = new System.Windows.Forms.TextBox();
			this.tbFrom = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
			this.grpTargets = new System.Windows.Forms.GroupBox();
			this.label8 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.tbRecv = new System.Windows.Forms.TextBox();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOk = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
			this.grpTargets.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.checkBox_SSLEnable);
			this.groupBox1.Controls.Add(this.tbPort);
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Controls.Add(this.tbServer);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.tbPass);
			this.groupBox1.Controls.Add(this.tbDomain);
			this.groupBox1.Controls.Add(this.tbFrom);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Location = new System.Drawing.Point(12, 21);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(358, 159);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Tag = "mailsender";
			this.groupBox1.Text = "邮件服务器";
			// 
			// checkBox_SSLEnable
			// 
			this.checkBox_SSLEnable.Location = new System.Drawing.Point(27, 121);
			this.checkBox_SSLEnable.Name = "checkBox_SSLEnable";
			this.checkBox_SSLEnable.Size = new System.Drawing.Size(254, 25);
			this.checkBox_SSLEnable.TabIndex = 17;
			this.checkBox_SSLEnable.Tag = "sslenable";
			this.checkBox_SSLEnable.Text = "此SMTP服务器要求安全连接(SSL)";
			this.checkBox_SSLEnable.UseVisualStyleBackColor = true;
			// 
			// tbPort
			// 
			this.tbPort.Location = new System.Drawing.Point(283, 90);
			this.tbPort.Name = "tbPort";
			this.tbPort.Size = new System.Drawing.Size(49, 21);
			this.tbPort.TabIndex = 16;
			this.tbPort.Text = "25";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(236, 93);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(29, 12);
			this.label6.TabIndex = 19;
			this.label6.Tag = "serverport";
			this.label6.Text = "端口";
			// 
			// tbServer
			// 
			this.tbServer.Location = new System.Drawing.Point(106, 90);
			this.tbServer.Name = "tbServer";
			this.tbServer.Size = new System.Drawing.Size(124, 21);
			this.tbServer.TabIndex = 15;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(25, 93);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(41, 12);
			this.label5.TabIndex = 18;
			this.label5.Tag = "smtpserver";
			this.label5.Text = "服务器";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(25, 31);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(41, 12);
			this.label2.TabIndex = 9;
			this.label2.Tag = "mailuser";
			this.label2.Text = "发件人";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(25, 62);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(29, 12);
			this.label3.TabIndex = 14;
			this.label3.Tag = "mailpass";
			this.label3.Text = "密码";
			// 
			// tbPass
			// 
			this.tbPass.Location = new System.Drawing.Point(106, 59);
			this.tbPass.Name = "tbPass";
			this.tbPass.PasswordChar = '*';
			this.tbPass.Size = new System.Drawing.Size(124, 21);
			this.tbPass.TabIndex = 13;
			// 
			// tbDomain
			// 
			this.tbDomain.Location = new System.Drawing.Point(253, 24);
			this.tbDomain.Name = "tbDomain";
			this.tbDomain.Size = new System.Drawing.Size(79, 21);
			this.tbDomain.TabIndex = 12;
			this.tbDomain.Leave += new System.EventHandler(this.TbDomainLeave);
			// 
			// tbFrom
			// 
			this.tbFrom.Location = new System.Drawing.Point(106, 24);
			this.tbFrom.Name = "tbFrom";
			this.tbFrom.Size = new System.Drawing.Size(124, 21);
			this.tbFrom.TabIndex = 10;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(236, 28);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(11, 12);
			this.label4.TabIndex = 11;
			this.label4.Text = "@";
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(132, 96);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(29, 12);
			this.label9.TabIndex = 68;
			this.label9.Tag = "Minutes";
			this.label9.Text = "分钟";
			// 
			// numericUpDown1
			// 
			this.numericUpDown1.Location = new System.Drawing.Point(7, 90);
			this.numericUpDown1.Name = "numericUpDown1";
			this.numericUpDown1.Size = new System.Drawing.Size(120, 21);
			this.numericUpDown1.TabIndex = 9;
			// 
			// grpTargets
			// 
			this.grpTargets.Controls.Add(this.label9);
			this.grpTargets.Controls.Add(this.numericUpDown1);
			this.grpTargets.Controls.Add(this.label8);
			this.grpTargets.Controls.Add(this.label1);
			this.grpTargets.Controls.Add(this.tbRecv);
			this.grpTargets.Location = new System.Drawing.Point(12, 196);
			this.grpTargets.Name = "grpTargets";
			this.grpTargets.Size = new System.Drawing.Size(358, 128);
			this.grpTargets.TabIndex = 70;
			this.grpTargets.TabStop = false;
			this.grpTargets.Tag = "recvsettings";
			this.grpTargets.Text = "发送设置";
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(5, 75);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(65, 12);
			this.label8.TabIndex = 66;
			this.label8.Tag = "ActionInterval";
			this.label8.Text = "时间间隔：";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(7, 21);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(41, 12);
			this.label1.TabIndex = 63;
			this.label1.Tag = "recvaccounts";
			this.label1.Text = "收件人";
			// 
			// tbRecv
			// 
			this.tbRecv.Location = new System.Drawing.Point(6, 39);
			this.tbRecv.Name = "tbRecv";
			this.tbRecv.Size = new System.Drawing.Size(346, 21);
			this.tbRecv.TabIndex = 7;
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(225, 333);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(80, 30);
			this.btnCancel.TabIndex = 73;
			this.btnCancel.Tag = "cancel";
			this.btnCancel.Text = "cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// btnOk
			// 
			this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOk.Location = new System.Drawing.Point(79, 332);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(80, 30);
			this.btnOk.TabIndex = 72;
			this.btnOk.Tag = "ok";
			this.btnOk.Text = "ok";
			this.btnOk.UseVisualStyleBackColor = true;
			this.btnOk.Click += new System.EventHandler(this.BtnOkClick);
			// 
			// PMMail
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(383, 375);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.grpTargets);
			this.Controls.Add(this.groupBox1);
			this.Name = "PMMail";
			this.Tag = "PMMail";
			this.Text = "自动发送PM";
			this.Load += new System.EventHandler(this.PMMailLoad);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
			this.grpTargets.ResumeLayout(false);
			this.grpTargets.PerformLayout();
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.TextBox tbRecv;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.GroupBox grpTargets;
		private System.Windows.Forms.NumericUpDown numericUpDown1;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox tbFrom;
		private System.Windows.Forms.TextBox tbDomain;
		private System.Windows.Forms.TextBox tbPass;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox tbServer;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox tbPort;
		private System.Windows.Forms.CheckBox checkBox_SSLEnable;
		private System.Windows.Forms.GroupBox groupBox1;
	}
}
