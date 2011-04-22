namespace Stran
{
	partial class MainForm
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.textLog = new System.Windows.Forms.TextBox();
			this.listView1 = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader8 = new System.Windows.Forms.ColumnHeader();
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.loginToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.loginAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.addAccountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.editAccountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.deleteAccountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.timerIcon = new System.Windows.Forms.Timer(this.components);
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.contextMenuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Multiline = true;
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(942, 668);
			this.tabControl1.TabIndex = 1;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.textLog);
			this.tabPage1.Controls.Add(this.listView1);
			this.tabPage1.Location = new System.Drawing.Point(4, 23);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(934, 641);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Tag = "accmanager";
			this.tabPage1.Text = "帐号管理";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// textLog
			// 
			this.textLog.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textLog.Location = new System.Drawing.Point(402, 3);
			this.textLog.Multiline = true;
			this.textLog.Name = "textLog";
			this.textLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textLog.Size = new System.Drawing.Size(529, 635);
			this.textLog.TabIndex = 1;
			// 
			// listView1
			// 
			this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
									this.columnHeader1,
									this.columnHeader2,
									this.columnHeader8});
			this.listView1.ContextMenuStrip = this.contextMenuStrip1;
			this.listView1.Dock = System.Windows.Forms.DockStyle.Left;
			this.listView1.FullRowSelect = true;
			this.listView1.GridLines = true;
			this.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.listView1.Location = new System.Drawing.Point(3, 3);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(399, 635);
			this.listView1.TabIndex = 0;
			this.listView1.UseCompatibleStateImageBehavior = false;
			this.listView1.View = System.Windows.Forms.View.Details;
			this.listView1.DoubleClick += new System.EventHandler(this.CMenuLogin_Click);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Tag = "username";
			this.columnHeader1.Text = "用户名";
			this.columnHeader1.Width = 120;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Tag = "server";
			this.columnHeader2.Text = "服务器";
			this.columnHeader2.Width = 150;
			// 
			// columnHeader8
			// 
			this.columnHeader8.Tag = "tribe";
			this.columnHeader8.Text = "种族";
			this.columnHeader8.Width = 123;
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.loginToolStripMenuItem,
									this.loginAllToolStripMenuItem,
									this.toolStripSeparator1,
									this.addAccountToolStripMenuItem,
									this.editAccountToolStripMenuItem,
									this.deleteAccountToolStripMenuItem});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.ShowImageMargin = false;
			this.contextMenuStrip1.Size = new System.Drawing.Size(136, 120);
			this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
			// 
			// loginToolStripMenuItem
			// 
			this.loginToolStripMenuItem.Name = "loginToolStripMenuItem";
			this.loginToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
			this.loginToolStripMenuItem.Text = "&L. Login";
			this.loginToolStripMenuItem.Click += new System.EventHandler(this.CMenuLogin_Click);
			// 
			// loginAllToolStripMenuItem
			// 
			this.loginAllToolStripMenuItem.Name = "loginAllToolStripMenuItem";
			this.loginAllToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
			this.loginAllToolStripMenuItem.Tag = "loginall";
			this.loginAllToolStripMenuItem.Text = "&O. 登录所有帐号";
			this.loginAllToolStripMenuItem.Click += new System.EventHandler(this.CMenuLoginAll_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(132, 6);
			// 
			// addAccountToolStripMenuItem
			// 
			this.addAccountToolStripMenuItem.Name = "addAccountToolStripMenuItem";
			this.addAccountToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
			this.addAccountToolStripMenuItem.Tag = "addacc";
			this.addAccountToolStripMenuItem.Text = "&A. 添加帐号";
			this.addAccountToolStripMenuItem.Click += new System.EventHandler(this.CMenuAddAccount_Click);
			// 
			// editAccountToolStripMenuItem
			// 
			this.editAccountToolStripMenuItem.Name = "editAccountToolStripMenuItem";
			this.editAccountToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
			this.editAccountToolStripMenuItem.Tag = "editacc";
			this.editAccountToolStripMenuItem.Text = "&E. 编辑帐号";
			this.editAccountToolStripMenuItem.Click += new System.EventHandler(this.CMenuEditAccount_Click);
			// 
			// deleteAccountToolStripMenuItem
			// 
			this.deleteAccountToolStripMenuItem.Name = "deleteAccountToolStripMenuItem";
			this.deleteAccountToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
			this.deleteAccountToolStripMenuItem.Tag = "delacc";
			this.deleteAccountToolStripMenuItem.Text = "&D. 删除帐号";
			this.deleteAccountToolStripMenuItem.Click += new System.EventHandler(this.CMenuDeleteAccount_Click);
			// 
			// notifyIcon1
			// 
			this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
			this.notifyIcon1.Visible = true;
			this.notifyIcon1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseClick);
			// 
			// timer1
			// 
			this.timer1.Interval = 60000;
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// timerIcon
			// 
			this.timerIcon.Interval = 1000;
			this.timerIcon.Tick += new System.EventHandler(this.timerIcon_Tick);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(942, 668);
			this.Controls.Add(this.tabControl1);
			this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Form1";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
			this.Resize += new System.EventHandler(this.MainForm_Resize);
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage1.PerformLayout();
			this.contextMenuStrip1.ResumeLayout(false);
			this.ResumeLayout(false);
		}

		#endregion

		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader8;
		private System.Windows.Forms.TextBox textLog;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
		private System.Windows.Forms.ToolStripMenuItem loginToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem loginAllToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem addAccountToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem editAccountToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem deleteAccountToolStripMenuItem;
		private System.Windows.Forms.Timer timer1;
		public System.Windows.Forms.NotifyIcon notifyIcon1;
		public System.Windows.Forms.Timer timerIcon;
	}
}

