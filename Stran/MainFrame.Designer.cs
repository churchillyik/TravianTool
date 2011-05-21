namespace Stran
{
	partial class MainFrame
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
			if(dockPanel1.Contents.Count != 0)
			{
				string fn = "style\\" + LoginInfo.GetKey() + "!style.xml";
				dockPanel1.SaveAsXml(fn);
			}
			if(disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.tabControl2 = new System.Windows.Forms.TabControl();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.dockPanel1 = new WeifenLuo.WinFormsUI.Docking.DockPanel();
			this.panel2 = new System.Windows.Forms.Panel();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.checkBoxVerbose = new System.Windows.Forms.CheckBox();
			this.contextMenuEvent = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.contextMenuBuilding = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.CMBUp = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.CMBUp2 = new System.Windows.Forms.ToolStripMenuItem();
			this.CMBUp5 = new System.Windows.Forms.ToolStripMenuItem();
			this.CMBUp9 = new System.Windows.Forms.ToolStripMenuItem();
			this.CMBUpTo = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.CMBDestroy = new System.Windows.Forms.ToolStripMenuItem();
			this.CMBNew = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.CMBAI = new System.Windows.Forms.ToolStripMenuItem();
			this.CMBAI_C = new System.Windows.Forms.ToolStripMenuItem();
			this.CMBAI_L = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.CMBRefresh = new System.Windows.Forms.ToolStripMenuItem();
			this.CMBRefreshDestroy = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
			this.CMMNew2 = new System.Windows.Forms.ToolStripMenuItem();
			this.CMMNpcTrade2 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
			this.CMBParty = new System.Windows.Forms.ToolStripMenuItem();
			this.CMBParty500 = new System.Windows.Forms.ToolStripMenuItem();
			this.CMBParty2000 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator13 = new System.Windows.Forms.ToolStripSeparator();
			this.CMBRaid = new System.Windows.Forms.ToolStripMenuItem();
			this.CMBAttack = new System.Windows.Forms.ToolStripMenuItem();
			this.CMBAlarm = new System.Windows.Forms.ToolStripMenuItem();
			this.CMB_PMMail = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator15 = new System.Windows.Forms.ToolStripSeparator();
			this.CMBEnableCoin = new System.Windows.Forms.ToolStripMenuItem();
			this.contextMenuResearch = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.CMRResearch = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
			this.CMRUpgradeTroopLvl = new System.Windows.Forms.ToolStripMenuItem();
			this.CMRUpgradeTroopLvlTo = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
			this.CMRRefresh = new System.Windows.Forms.ToolStripMenuItem();
			this.contextMenuQueue = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.CMQDel = new System.Windows.Forms.ToolStripMenuItem();
			this.CMQClear = new System.Windows.Forms.ToolStripMenuItem();
			this.CMQEdit = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.CMQUp = new System.Windows.Forms.ToolStripMenuItem();
			this.CMQDown = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			this.CMQPause = new System.Windows.Forms.ToolStripMenuItem();
			this.CMQTimer = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator14 = new System.Windows.Forms.ToolStripSeparator();
			this.CMQExport = new System.Windows.Forms.ToolStripMenuItem();
			this.CMQImport = new System.Windows.Forms.ToolStripMenuItem();
			this.contextMenuVillage = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.CMVRefresh = new System.Windows.Forms.ToolStripMenuItem();
			this.CMVRefreshAll = new System.Windows.Forms.ToolStripMenuItem();
			this.CMBNewCap = new System.Windows.Forms.ToolStripMenuItem();
			this.CMVRename = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
			this.CMVLowerLimit = new System.Windows.Forms.ToolStripMenuItem();
			this.CMVUpperLimit = new System.Windows.Forms.ToolStripMenuItem();
			this.CMVTlimit = new System.Windows.Forms.ToolStripMenuItem();
			this.CMVSaveRES = new System.Windows.Forms.ToolStripMenuItem();
			this.CMVRestoreRES = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
			this.CMVSnapshot = new System.Windows.Forms.ToolStripMenuItem();
			this.CMVSnapAll = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator17 = new System.Windows.Forms.ToolStripSeparator();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.button1 = new System.Windows.Forms.Button();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.LastDebug = new System.Windows.Forms.ToolStripStatusLabel();
			this.PageCount = new System.Windows.Forms.ToolStripStatusLabel();
			this.ActionCount = new System.Windows.Forms.ToolStripStatusLabel();
			this.SVRTime = new System.Windows.Forms.ToolStripStatusLabel();
			this.LCLTime = new System.Windows.Forms.ToolStripStatusLabel();
			this.timersec1 = new System.Windows.Forms.Timer(this.components);
			this.CMMNew = new System.Windows.Forms.ToolStripMenuItem();
			this.contextMenuMarket = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.CMMRefresh = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
			this.CMMNpcTrade = new System.Windows.Forms.ToolStripMenuItem();
			this.contextMenuInbuilding = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.CMICancel = new System.Windows.Forms.ToolStripMenuItem();
			this.contextMenuTroop = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.CMTRaid = new System.Windows.Forms.ToolStripMenuItem();
			this.CMTAttack = new System.Windows.Forms.ToolStripMenuItem();
			this.CMTAlarm = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator16 = new System.Windows.Forms.ToolStripSeparator();
			this.CMTRefresh = new System.Windows.Forms.ToolStripMenuItem();
			this.contextMenuTroopTraining = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.trainingTroopsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.refreshTroopTrainingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.contextMenuHeroAdventure = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.refreshAdvanturesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.startAdvantureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.tabControl2.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.panel1.SuspendLayout();
			this.contextMenuBuilding.SuspendLayout();
			this.contextMenuResearch.SuspendLayout();
			this.contextMenuQueue.SuspendLayout();
			this.contextMenuVillage.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.contextMenuMarket.SuspendLayout();
			this.contextMenuInbuilding.SuspendLayout();
			this.contextMenuTroop.SuspendLayout();
			this.contextMenuTroopTraining.SuspendLayout();
			this.contextMenuHeroAdventure.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl2
			// 
			this.tabControl2.Controls.Add(this.tabPage2);
			this.tabControl2.Controls.Add(this.tabPage1);
			this.tabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl2.Location = new System.Drawing.Point(0, 0);
			this.tabControl2.Name = "tabControl2";
			this.tabControl2.SelectedIndex = 0;
			this.tabControl2.Size = new System.Drawing.Size(934, 612);
			this.tabControl2.TabIndex = 15;
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.dockPanel1);
			this.tabPage2.Controls.Add(this.panel2);
			this.tabPage2.Location = new System.Drawing.Point(4, 23);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(926, 585);
			this.tabPage2.TabIndex = 0;
			this.tabPage2.Tag = "villagebuilding";
			this.tabPage2.Text = "村庄建设";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// dockPanel1
			// 
			this.dockPanel1.ActiveAutoHideContent = null;
			this.dockPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dockPanel1.DocumentStyle = WeifenLuo.WinFormsUI.Docking.DocumentStyle.DockingWindow;
			this.dockPanel1.Location = new System.Drawing.Point(3, 3);
			this.dockPanel1.Name = "dockPanel1";
			this.dockPanel1.Size = new System.Drawing.Size(919, 579);
			this.dockPanel1.TabIndex = 22;
			this.dockPanel1.Resize += new System.EventHandler(this.dockPanel1_Resize);
			// 
			// panel2
			// 
			this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
			this.panel2.Location = new System.Drawing.Point(922, 3);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(1, 579);
			this.panel2.TabIndex = 21;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.textBox1);
			this.tabPage1.Controls.Add(this.panel1);
			this.tabPage1.Location = new System.Drawing.Point(4, 23);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(926, 585);
			this.tabPage1.TabIndex = 2;
			this.tabPage1.Tag = "log";
			this.tabPage1.Text = "日志";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// textBox1
			// 
			this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textBox1.Font = new System.Drawing.Font("Lucida Console", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.textBox1.Location = new System.Drawing.Point(3, 33);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBox1.Size = new System.Drawing.Size(920, 549);
			this.textBox1.TabIndex = 0;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.checkBoxVerbose);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(3, 3);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(920, 30);
			this.panel1.TabIndex = 1;
			// 
			// checkBoxVerbose
			// 
			this.checkBoxVerbose.AutoSize = true;
			this.checkBoxVerbose.Checked = true;
			this.checkBoxVerbose.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBoxVerbose.Location = new System.Drawing.Point(3, 6);
			this.checkBoxVerbose.Name = "checkBoxVerbose";
			this.checkBoxVerbose.Size = new System.Drawing.Size(71, 18);
			this.checkBoxVerbose.TabIndex = 0;
			this.checkBoxVerbose.Text = "Verbose";
			this.checkBoxVerbose.UseVisualStyleBackColor = true;
			// 
			// contextMenuEvent
			// 
			this.contextMenuEvent.Name = "contextMenuEvent";
			this.contextMenuEvent.ShowImageMargin = false;
			this.contextMenuEvent.Size = new System.Drawing.Size(36, 4);
			// 
			// contextMenuBuilding
			// 
			this.contextMenuBuilding.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.CMBUp,
									this.toolStripMenuItem1,
									this.CMBUpTo,
									this.toolStripSeparator3,
									this.CMBDestroy,
									this.CMBNew,
									this.toolStripSeparator2,
									this.CMBAI,
									this.toolStripSeparator4,
									this.CMBRefresh,
									this.CMBRefreshDestroy,
									this.toolStripSeparator12,
									this.CMMNew2,
									this.CMMNpcTrade2,
									this.toolStripSeparator9,
									this.CMBParty,
									this.toolStripSeparator13,
									this.CMBRaid,
									this.CMBAttack,
									this.CMBAlarm,
									this.CMB_PMMail,
									this.toolStripSeparator15,
									this.CMBEnableCoin});
			this.contextMenuBuilding.Name = "contextMenuStrip2";
			this.contextMenuBuilding.ShowImageMargin = false;
			this.contextMenuBuilding.Size = new System.Drawing.Size(190, 420);
			this.contextMenuBuilding.Text = "添加到队列";
			// 
			// CMBUp
			// 
			this.CMBUp.Name = "CMBUp";
			this.CMBUp.Size = new System.Drawing.Size(189, 22);
			this.CMBUp.Tag = "cmbup";
			this.CMBUp.Text = "&A. 升级建筑 - 添加到队列";
			this.CMBUp.Click += new System.EventHandler(this.CMBUp_Click);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.CMBUp2,
									this.CMBUp5,
									this.CMBUp9});
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(189, 22);
			this.toolStripMenuItem1.Tag = "cmpupmulti";
			this.toolStripMenuItem1.Text = "&U. 升级建筑 - 多次";
			// 
			// CMBUp2
			// 
			this.CMBUp2.Name = "CMBUp2";
			this.CMBUp2.Size = new System.Drawing.Size(142, 22);
			this.CMBUp2.Tag = "cmpup2";
			this.CMBUp2.Text = "&2. 添加 2 次";
			this.CMBUp2.Click += new System.EventHandler(this.CMBUp2_Click);
			// 
			// CMBUp5
			// 
			this.CMBUp5.Name = "CMBUp5";
			this.CMBUp5.Size = new System.Drawing.Size(142, 22);
			this.CMBUp5.Tag = "cmpup5";
			this.CMBUp5.Text = "&5. 添加 5 次";
			this.CMBUp5.Click += new System.EventHandler(this.CMBUp5_Click);
			// 
			// CMBUp9
			// 
			this.CMBUp9.Name = "CMBUp9";
			this.CMBUp9.Size = new System.Drawing.Size(142, 22);
			this.CMBUp9.Tag = "cmpup9";
			this.CMBUp9.Text = "&9. 添加 9 次";
			this.CMBUp9.Click += new System.EventHandler(this.CMBUp9_Click);
			// 
			// CMBUpTo
			// 
			this.CMBUpTo.Name = "CMBUpTo";
			this.CMBUpTo.Size = new System.Drawing.Size(189, 22);
			this.CMBUpTo.Tag = "cmpupto";
			this.CMBUpTo.Text = "&L. 升级建筑到指定等级...";
			this.CMBUpTo.Click += new System.EventHandler(this.CMBUpTo_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(186, 6);
			// 
			// CMBDestroy
			// 
			this.CMBDestroy.Name = "CMBDestroy";
			this.CMBDestroy.Size = new System.Drawing.Size(189, 22);
			this.CMBDestroy.Tag = "cmpdestroy";
			this.CMBDestroy.Text = "&D. 拆除建筑";
			this.CMBDestroy.Click += new System.EventHandler(this.CMBDestroy_Click);
			// 
			// CMBNew
			// 
			this.CMBNew.Name = "CMBNew";
			this.CMBNew.Size = new System.Drawing.Size(189, 22);
			this.CMBNew.Tag = "cmbnew";
			this.CMBNew.Text = "&N. 新建建筑";
			this.CMBNew.Click += new System.EventHandler(this.CMBNew_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(186, 6);
			// 
			// CMBAI
			// 
			this.CMBAI.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.CMBAI_C,
									this.CMBAI_L});
			this.CMBAI.Name = "CMBAI";
			this.CMBAI.Size = new System.Drawing.Size(189, 22);
			this.CMBAI.Tag = "cmbai";
			this.CMBAI.Text = "&E. 启用人工智能模块";
			// 
			// CMBAI_C
			// 
			this.CMBAI_C.Name = "CMBAI_C";
			this.CMBAI_C.Size = new System.Drawing.Size(172, 22);
			this.CMBAI_C.Tag = "cmbaic";
			this.CMBAI_C.Text = "&C. 根据资源储备";
			this.CMBAI_C.Click += new System.EventHandler(this.CMBAI_C_Click);
			// 
			// CMBAI_L
			// 
			this.CMBAI_L.Name = "CMBAI_L";
			this.CMBAI_L.Size = new System.Drawing.Size(172, 22);
			this.CMBAI_L.Tag = "cmbail";
			this.CMBAI_L.Text = "&L. 根据资源田等级";
			this.CMBAI_L.Click += new System.EventHandler(this.CMBAI_L_Click);
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(186, 6);
			// 
			// CMBRefresh
			// 
			this.CMBRefresh.Name = "CMBRefresh";
			this.CMBRefresh.Size = new System.Drawing.Size(189, 22);
			this.CMBRefresh.Tag = "refreshbuilding";
			this.CMBRefresh.Text = "刷新建筑列表";
			this.CMBRefresh.Click += new System.EventHandler(this.CMBRefresh_Click);
			// 
			// CMBRefreshDestroy
			// 
			this.CMBRefreshDestroy.Name = "CMBRefreshDestroy";
			this.CMBRefreshDestroy.Size = new System.Drawing.Size(189, 22);
			this.CMBRefreshDestroy.Tag = "refreshdestroy";
			this.CMBRefreshDestroy.Text = "刷新拆除项目";
			this.CMBRefreshDestroy.Click += new System.EventHandler(this.CMBRefreshDestroy_Click);
			// 
			// toolStripSeparator12
			// 
			this.toolStripSeparator12.Name = "toolStripSeparator12";
			this.toolStripSeparator12.Size = new System.Drawing.Size(186, 6);
			// 
			// CMMNew2
			// 
			this.CMMNew2.Name = "CMMNew2";
			this.CMMNew2.Size = new System.Drawing.Size(189, 22);
			this.CMMNew2.Tag = "newtransfer";
			this.CMMNew2.Text = "新运输";
			this.CMMNew2.Click += new System.EventHandler(this.CMMNew_Click);
			// 
			// CMMNpcTrade2
			// 
			this.CMMNpcTrade2.Name = "CMMNpcTrade2";
			this.CMMNpcTrade2.Size = new System.Drawing.Size(189, 22);
			this.CMMNpcTrade2.Tag = "npctrade";
			this.CMMNpcTrade2.Text = "自动平仓";
			this.CMMNpcTrade2.Click += new System.EventHandler(this.CMMNpcTrade_Click);
			// 
			// toolStripSeparator9
			// 
			this.toolStripSeparator9.Name = "toolStripSeparator9";
			this.toolStripSeparator9.Size = new System.Drawing.Size(186, 6);
			// 
			// CMBParty
			// 
			this.CMBParty.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.CMBParty500,
									this.CMBParty2000});
			this.CMBParty.Name = "CMBParty";
			this.CMBParty.Size = new System.Drawing.Size(189, 22);
			this.CMBParty.Tag = "cmbparty";
			this.CMBParty.Text = "启用自动派对模块";
			// 
			// CMBParty500
			// 
			this.CMBParty500.Name = "CMBParty500";
			this.CMBParty500.Size = new System.Drawing.Size(124, 22);
			this.CMBParty500.Tag = "cmbparty500";
			this.CMBParty500.Text = "5. 小派对";
			this.CMBParty500.Click += new System.EventHandler(this.CMBParty500_Click);
			// 
			// CMBParty2000
			// 
			this.CMBParty2000.Name = "CMBParty2000";
			this.CMBParty2000.Size = new System.Drawing.Size(124, 22);
			this.CMBParty2000.Tag = "cmbparty2000";
			this.CMBParty2000.Text = "2. 大派对";
			this.CMBParty2000.Click += new System.EventHandler(this.CMBParty2000_Click);
			// 
			// toolStripSeparator13
			// 
			this.toolStripSeparator13.Name = "toolStripSeparator13";
			this.toolStripSeparator13.Size = new System.Drawing.Size(186, 6);
			// 
			// CMBRaid
			// 
			this.CMBRaid.Name = "CMBRaid";
			this.CMBRaid.Size = new System.Drawing.Size(189, 22);
			this.CMBRaid.Tag = "CMBRaid";
			this.CMBRaid.Text = "出兵";
			this.CMBRaid.Click += new System.EventHandler(this.CMBRaid_Click);
			// 
			// CMBAttack
			// 
			this.CMBAttack.Name = "CMBAttack";
			this.CMBAttack.Size = new System.Drawing.Size(189, 22);
			this.CMBAttack.Tag = "CMBRaid";
			this.CMBAttack.Text = "出兵";
			this.CMBAttack.Click += new System.EventHandler(this.CMBAttackClick);
			// 
			// CMBAlarm
			// 
			this.CMBAlarm.Name = "CMBAlarm";
			this.CMBAlarm.Size = new System.Drawing.Size(189, 22);
			this.CMBAlarm.Tag = "CMBAlarm";
			this.CMBAlarm.Text = "自动报警";
			this.CMBAlarm.Click += new System.EventHandler(this.CMBAlarm_Click);
			// 
			// CMB_PMMail
			// 
			this.CMB_PMMail.Name = "CMB_PMMail";
			this.CMB_PMMail.Size = new System.Drawing.Size(189, 22);
			this.CMB_PMMail.Tag = "CMB_PMMail";
			this.CMB_PMMail.Text = "自动发送PM";
			this.CMB_PMMail.Click += new System.EventHandler(this.CMB_PMMailClick);
			// 
			// toolStripSeparator15
			// 
			this.toolStripSeparator15.Name = "toolStripSeparator15";
			this.toolStripSeparator15.Size = new System.Drawing.Size(186, 6);
			// 
			// CMBEnableCoin
			// 
			this.CMBEnableCoin.CheckOnClick = true;
			this.CMBEnableCoin.ForeColor = System.Drawing.Color.DarkBlue;
			this.CMBEnableCoin.Name = "CMBEnableCoin";
			this.CMBEnableCoin.Size = new System.Drawing.Size(189, 22);
			this.CMBEnableCoin.Tag = "CMBEnableCoin";
			this.CMBEnableCoin.Text = "启用金币功能";
			this.CMBEnableCoin.CheckedChanged += new System.EventHandler(this.CMBEnableCoin_CheckedChanged);
			// 
			// contextMenuResearch
			// 
			this.contextMenuResearch.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.CMRResearch,
									this.toolStripSeparator6,
									this.CMRUpgradeTroopLvl,
									this.CMRUpgradeTroopLvlTo,
									this.toolStripSeparator7,
									this.toolStripSeparator10,
									this.CMRRefresh});
			this.contextMenuResearch.Name = "contextMenuResearch";
			this.contextMenuResearch.ShowImageMargin = false;
			this.contextMenuResearch.Size = new System.Drawing.Size(148, 110);
			this.contextMenuResearch.Tag = "mresearch";
			this.contextMenuResearch.Opening += new System.ComponentModel.CancelEventHandler(this.CMR_Opening);
			// 
			// CMRResearch
			// 
			this.CMRResearch.Name = "CMRResearch";
			this.CMRResearch.Size = new System.Drawing.Size(147, 22);
			this.CMRResearch.Tag = "mresearch";
			this.CMRResearch.Text = "research";
			this.CMRResearch.Click += new System.EventHandler(this.CMRResearch_Click);
			// 
			// toolStripSeparator6
			// 
			this.toolStripSeparator6.Name = "toolStripSeparator6";
			this.toolStripSeparator6.Size = new System.Drawing.Size(144, 6);
			// 
			// CMRUpgradeTroopLvl
			// 
			this.CMRUpgradeTroopLvl.Name = "CMRUpgradeTroopLvl";
			this.CMRUpgradeTroopLvl.Size = new System.Drawing.Size(147, 22);
			this.CMRUpgradeTroopLvl.Tag = "upgradetrooplvl";
			this.CMRUpgradeTroopLvl.Text = "upgradetrooplvl";
			this.CMRUpgradeTroopLvl.Click += new System.EventHandler(this.CMRUpgradeTroopLvl_Click);
			// 
			// CMRUpgradeTroopLvlTo
			// 
			this.CMRUpgradeTroopLvlTo.Name = "CMRUpgradeTroopLvlTo";
			this.CMRUpgradeTroopLvlTo.Size = new System.Drawing.Size(147, 22);
			this.CMRUpgradeTroopLvlTo.Tag = "upgradetrooplvlto";
			this.CMRUpgradeTroopLvlTo.Text = "upgradetrooplvlto";
			this.CMRUpgradeTroopLvlTo.Click += new System.EventHandler(this.CMRUpgradeTroopLvlTo_Click);
			// 
			// toolStripSeparator7
			// 
			this.toolStripSeparator7.Name = "toolStripSeparator7";
			this.toolStripSeparator7.Size = new System.Drawing.Size(144, 6);
			// 
			// toolStripSeparator10
			// 
			this.toolStripSeparator10.Name = "toolStripSeparator10";
			this.toolStripSeparator10.Size = new System.Drawing.Size(144, 6);
			// 
			// CMRRefresh
			// 
			this.CMRRefresh.Name = "CMRRefresh";
			this.CMRRefresh.Size = new System.Drawing.Size(147, 22);
			this.CMRRefresh.Tag = "refreshupgrade";
			this.CMRRefresh.Text = "refreshupgrade";
			this.CMRRefresh.Click += new System.EventHandler(this.CMRRefresh_Click);
			// 
			// contextMenuQueue
			// 
			this.contextMenuQueue.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.CMQDel,
									this.CMQClear,
									this.CMQEdit,
									this.toolStripSeparator1,
									this.CMQUp,
									this.CMQDown,
									this.toolStripSeparator5,
									this.CMQPause,
									this.CMQTimer,
									this.toolStripSeparator14,
									this.CMQExport,
									this.CMQImport});
			this.contextMenuQueue.Name = "contextMenuStrip3";
			this.contextMenuQueue.ShowCheckMargin = true;
			this.contextMenuQueue.ShowImageMargin = false;
			this.contextMenuQueue.Size = new System.Drawing.Size(197, 220);
			this.contextMenuQueue.Tag = "";
			// 
			// CMQDel
			// 
			this.CMQDel.Name = "CMQDel";
			this.CMQDel.Size = new System.Drawing.Size(196, 22);
			this.CMQDel.Tag = "cmqdel";
			this.CMQDel.Text = "&D. 删除";
			this.CMQDel.Click += new System.EventHandler(this.CMQDel_Click);
			// 
			// CMQClear
			// 
			this.CMQClear.Name = "CMQClear";
			this.CMQClear.Size = new System.Drawing.Size(196, 22);
			this.CMQClear.Tag = "cmqclear";
			this.CMQClear.Text = "&C. 清空";
			this.CMQClear.Click += new System.EventHandler(this.CMQClear_Click);
			// 
			// CMQEdit
			// 
			this.CMQEdit.Name = "CMQEdit";
			this.CMQEdit.Size = new System.Drawing.Size(196, 22);
			this.CMQEdit.Tag = "cmqedit";
			this.CMQEdit.Text = "&E.  编辑";
			this.CMQEdit.Click += new System.EventHandler(this.CMQEdit_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(193, 6);
			// 
			// CMQUp
			// 
			this.CMQUp.Name = "CMQUp";
			this.CMQUp.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Up)));
			this.CMQUp.Size = new System.Drawing.Size(196, 22);
			this.CMQUp.Tag = "cmqup";
			this.CMQUp.Text = "上移";
			this.CMQUp.Click += new System.EventHandler(this.CMQUp_Click);
			// 
			// CMQDown
			// 
			this.CMQDown.Name = "CMQDown";
			this.CMQDown.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Down)));
			this.CMQDown.Size = new System.Drawing.Size(196, 22);
			this.CMQDown.Tag = "cmqdown";
			this.CMQDown.Text = "下移";
			this.CMQDown.Click += new System.EventHandler(this.CMQDown_Click);
			// 
			// toolStripSeparator5
			// 
			this.toolStripSeparator5.Name = "toolStripSeparator5";
			this.toolStripSeparator5.Size = new System.Drawing.Size(193, 6);
			// 
			// CMQPause
			// 
			this.CMQPause.Name = "CMQPause";
			this.CMQPause.Size = new System.Drawing.Size(196, 22);
			this.CMQPause.Tag = "cmqpause";
			this.CMQPause.Text = "&P. 暂停";
			this.CMQPause.Click += new System.EventHandler(this.CMQPause_Click);
			// 
			// CMQTimer
			// 
			this.CMQTimer.Checked = true;
			this.CMQTimer.CheckState = System.Windows.Forms.CheckState.Checked;
			this.CMQTimer.Name = "CMQTimer";
			this.CMQTimer.Size = new System.Drawing.Size(196, 22);
			this.CMQTimer.Tag = "cmqtimer";
			this.CMQTimer.Text = "&T. 延迟倒计时正在工作";
			this.CMQTimer.Click += new System.EventHandler(this.CMQTimer_Click);
			// 
			// toolStripSeparator14
			// 
			this.toolStripSeparator14.Name = "toolStripSeparator14";
			this.toolStripSeparator14.Size = new System.Drawing.Size(193, 6);
			// 
			// CMQExport
			// 
			this.CMQExport.Name = "CMQExport";
			this.CMQExport.Size = new System.Drawing.Size(196, 22);
			this.CMQExport.Tag = "cmqexport";
			this.CMQExport.Text = "&X. 导出";
			this.CMQExport.Click += new System.EventHandler(this.CMQExport_Click);
			// 
			// CMQImport
			// 
			this.CMQImport.Name = "CMQImport";
			this.CMQImport.Size = new System.Drawing.Size(196, 22);
			this.CMQImport.Tag = "cmqimport";
			this.CMQImport.Text = "&I. 导入";
			this.CMQImport.Click += new System.EventHandler(this.CMQImport_Click);
			// 
			// contextMenuVillage
			// 
			this.contextMenuVillage.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.CMVRefresh,
									this.CMVRefreshAll,
									this.CMBNewCap,
									this.CMVRename,
									this.toolStripSeparator8,
									this.CMVLowerLimit,
									this.CMVUpperLimit,
									this.CMVTlimit,
									this.CMVSaveRES,
									this.CMVRestoreRES,
									this.toolStripSeparator11,
									this.CMVSnapshot,
									this.CMVSnapAll,
									this.toolStripSeparator17});
			this.contextMenuVillage.Name = "contextMenuStrip1";
			this.contextMenuVillage.ShowImageMargin = false;
			this.contextMenuVillage.Size = new System.Drawing.Size(166, 264);
			this.contextMenuVillage.Opening += new System.ComponentModel.CancelEventHandler(this.CMV_Opening);
			// 
			// CMVRefresh
			// 
			this.CMVRefresh.Name = "CMVRefresh";
			this.CMVRefresh.Size = new System.Drawing.Size(165, 22);
			this.CMVRefresh.Tag = "refresh";
			this.CMVRefresh.Text = "刷新";
			this.CMVRefresh.Click += new System.EventHandler(this.CMVRefresh_Click);
			// 
			// CMVRefreshAll
			// 
			this.CMVRefreshAll.Name = "CMVRefreshAll";
			this.CMVRefreshAll.Size = new System.Drawing.Size(165, 22);
			this.CMVRefreshAll.Tag = "refreshall";
			this.CMVRefreshAll.Text = "全部刷新";
			this.CMVRefreshAll.Click += new System.EventHandler(this.CMVRefreshAll_Click);
			// 
			// CMBNewCap
			// 
			this.CMBNewCap.Name = "CMBNewCap";
			this.CMBNewCap.Size = new System.Drawing.Size(165, 22);
			this.CMBNewCap.Tag = "newcap";
			this.CMBNewCap.Text = "&X. 设为新主村";
			this.CMBNewCap.Click += new System.EventHandler(this.CMBNewCap_Click);
			// 
			// CMVRename
			// 
			this.CMVRename.Name = "CMVRename";
			this.CMVRename.Size = new System.Drawing.Size(165, 22);
			this.CMVRename.Tag = "Rename";
			this.CMVRename.Text = "Rename Village";
			this.CMVRename.Click += new System.EventHandler(this.CMVRename_Click);
			// 
			// toolStripSeparator8
			// 
			this.toolStripSeparator8.Name = "toolStripSeparator8";
			this.toolStripSeparator8.Size = new System.Drawing.Size(162, 6);
			// 
			// CMVLowerLimit
			// 
			this.CMVLowerLimit.Name = "CMVLowerLimit";
			this.CMVLowerLimit.Size = new System.Drawing.Size(165, 22);
			this.CMVLowerLimit.Tag = "ResourceLowerLimit";
			this.CMVLowerLimit.Text = "Minimum Resource";
			this.CMVLowerLimit.Click += new System.EventHandler(this.CMVLowerLimit_Click);
			// 
			// CMVUpperLimit
			// 
			this.CMVUpperLimit.Name = "CMVUpperLimit";
			this.CMVUpperLimit.Size = new System.Drawing.Size(165, 22);
			this.CMVUpperLimit.Tag = "ResourceUpperLimit";
			this.CMVUpperLimit.Text = "Maximum Resource";
			this.CMVUpperLimit.Click += new System.EventHandler(this.CMVUpperLimit_Click);
			// 
			// CMVTlimit
			// 
			this.CMVTlimit.Name = "CMVTlimit";
			this.CMVTlimit.Size = new System.Drawing.Size(165, 22);
			this.CMVTlimit.Tag = "TResLimit";
			this.CMVTlimit.Text = "Troop Resource Limit";
			this.CMVTlimit.Click += new System.EventHandler(this.CMVTlimit_Click);
			// 
			// CMVSaveRES
			// 
			this.CMVSaveRES.Name = "CMVSaveRES";
			this.CMVSaveRES.Size = new System.Drawing.Size(165, 22);
			this.CMVSaveRES.Tag = "saveRES";
			this.CMVSaveRES.Text = "Save Res Limit";
			this.CMVSaveRES.Click += new System.EventHandler(this.CMVSaveRESClick);
			// 
			// CMVRestoreRES
			// 
			this.CMVRestoreRES.Name = "CMVRestoreRES";
			this.CMVRestoreRES.Size = new System.Drawing.Size(165, 22);
			this.CMVRestoreRES.Tag = "restoreRES";
			this.CMVRestoreRES.Text = "Restore Res Limit";
			this.CMVRestoreRES.Click += new System.EventHandler(this.CMVRestoreRESClick);
			// 
			// toolStripSeparator11
			// 
			this.toolStripSeparator11.Name = "toolStripSeparator11";
			this.toolStripSeparator11.Size = new System.Drawing.Size(162, 6);
			// 
			// CMVSnapshot
			// 
			this.CMVSnapshot.Name = "CMVSnapshot";
			this.CMVSnapshot.Size = new System.Drawing.Size(165, 22);
			this.CMVSnapshot.Tag = "snapshot";
			this.CMVSnapshot.Text = "snapshot";
			this.CMVSnapshot.Click += new System.EventHandler(this.CMVSnapshot_Click);
			// 
			// CMVSnapAll
			// 
			this.CMVSnapAll.Name = "CMVSnapAll";
			this.CMVSnapAll.Size = new System.Drawing.Size(165, 22);
			this.CMVSnapAll.Tag = "snapall";
			this.CMVSnapAll.Text = "snapall";
			this.CMVSnapAll.Click += new System.EventHandler(this.CMVSnapAll_Click);
			// 
			// toolStripSeparator17
			// 
			this.toolStripSeparator17.Name = "toolStripSeparator17";
			this.toolStripSeparator17.Size = new System.Drawing.Size(162, 6);
			// 
			// timer1
			// 
			this.timer1.Enabled = true;
			this.timer1.Interval = 5000;
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// button1
			// 
			this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.button1.Location = new System.Drawing.Point(912, 0);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(22, 22);
			this.button1.TabIndex = 14;
			this.button1.Text = "X";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// statusStrip1
			// 
			this.statusStrip1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.LastDebug,
									this.PageCount,
									this.ActionCount,
									this.SVRTime,
									this.LCLTime});
			this.statusStrip1.Location = new System.Drawing.Point(0, 612);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(934, 30);
			this.statusStrip1.TabIndex = 16;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// LastDebug
			// 
			this.LastDebug.AutoSize = false;
			this.LastDebug.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
									| System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
									| System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
			this.LastDebug.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.LastDebug.Name = "LastDebug";
			this.LastDebug.Size = new System.Drawing.Size(479, 25);
			this.LastDebug.Spring = true;
			this.LastDebug.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// PageCount
			// 
			this.PageCount.AutoSize = false;
			this.PageCount.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
									| System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
									| System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
			this.PageCount.Name = "PageCount";
			this.PageCount.Size = new System.Drawing.Size(80, 25);
			this.PageCount.Text = "Page:";
			this.PageCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// ActionCount
			// 
			this.ActionCount.AutoSize = false;
			this.ActionCount.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
									| System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
									| System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
			this.ActionCount.Name = "ActionCount";
			this.ActionCount.Size = new System.Drawing.Size(80, 25);
			this.ActionCount.Text = "Action:";
			this.ActionCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// SVRTime
			// 
			this.SVRTime.AutoSize = false;
			this.SVRTime.AutoToolTip = true;
			this.SVRTime.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
									| System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
									| System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
			this.SVRTime.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.SVRTime.Name = "SVRTime";
			this.SVRTime.Size = new System.Drawing.Size(140, 25);
			this.SVRTime.ToolTipText = "服务器时间";
			// 
			// LCLTime
			// 
			this.LCLTime.AutoSize = false;
			this.LCLTime.AutoToolTip = true;
			this.LCLTime.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
									| System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
									| System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
			this.LCLTime.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.LCLTime.Name = "LCLTime";
			this.LCLTime.Size = new System.Drawing.Size(140, 25);
			this.LCLTime.ToolTipText = "本地时间";
			// 
			// timersec1
			// 
			this.timersec1.Enabled = true;
			this.timersec1.Interval = 1000;
			this.timersec1.Tick += new System.EventHandler(this.timersec1_Tick);
			// 
			// CMMNew
			// 
			this.CMMNew.Name = "CMMNew";
			this.CMMNew.Size = new System.Drawing.Size(117, 22);
			this.CMMNew.Tag = "newtransfer";
			this.CMMNew.Text = "新运输";
			this.CMMNew.Click += new System.EventHandler(this.CMMNew_Click);
			// 
			// contextMenuMarket
			// 
			this.contextMenuMarket.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.CMMNew,
									this.CMMRefresh,
									this.toolStripMenuItem2,
									this.CMMNpcTrade});
			this.contextMenuMarket.Name = "contextMenuMarket";
			this.contextMenuMarket.ShowImageMargin = false;
			this.contextMenuMarket.Size = new System.Drawing.Size(118, 76);
			// 
			// CMMRefresh
			// 
			this.CMMRefresh.Name = "CMMRefresh";
			this.CMMRefresh.Size = new System.Drawing.Size(117, 22);
			this.CMMRefresh.Tag = "QPRefreshRes";
			this.CMMRefresh.Text = "QPRefreshRes";
			this.CMMRefresh.Click += new System.EventHandler(this.QPRefreshRes_Click);
			// 
			// toolStripMenuItem2
			// 
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new System.Drawing.Size(114, 6);
			// 
			// CMMNpcTrade
			// 
			this.CMMNpcTrade.Name = "CMMNpcTrade";
			this.CMMNpcTrade.Size = new System.Drawing.Size(117, 22);
			this.CMMNpcTrade.Tag = "npctrade";
			this.CMMNpcTrade.Text = "自动平仓";
			this.CMMNpcTrade.Click += new System.EventHandler(this.CMMNpcTrade_Click);
			// 
			// contextMenuInbuilding
			// 
			this.contextMenuInbuilding.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.CMICancel});
			this.contextMenuInbuilding.Name = "contextMenuInbuilding";
			this.contextMenuInbuilding.ShowImageMargin = false;
			this.contextMenuInbuilding.Size = new System.Drawing.Size(70, 26);
			this.contextMenuInbuilding.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuInbuilding_Opening);
			// 
			// CMICancel
			// 
			this.CMICancel.Name = "CMICancel";
			this.CMICancel.Size = new System.Drawing.Size(69, 22);
			this.CMICancel.Tag = "cmicancel";
			this.CMICancel.Text = "取消";
			this.CMICancel.Click += new System.EventHandler(this.CMICancel_Click);
			// 
			// contextMenuTroop
			// 
			this.contextMenuTroop.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.CMTRaid,
									this.CMTAttack,
									this.CMTAlarm,
									this.toolStripSeparator16,
									this.CMTRefresh});
			this.contextMenuTroop.Name = "contextMenuTroop";
			this.contextMenuTroop.ShowImageMargin = false;
			this.contextMenuTroop.Size = new System.Drawing.Size(130, 98);
			this.contextMenuTroop.Tag = "mtroop";
			// 
			// CMTRaid
			// 
			this.CMTRaid.Name = "CMTRaid";
			this.CMTRaid.Size = new System.Drawing.Size(129, 22);
			this.CMTRaid.Tag = "CMBRaid";
			this.CMTRaid.Text = "send troops";
			this.CMTRaid.Click += new System.EventHandler(this.CMBRaid_Click);
			// 
			// CMTAttack
			// 
			this.CMTAttack.Name = "CMTAttack";
			this.CMTAttack.Size = new System.Drawing.Size(129, 22);
			this.CMTAttack.Tag = "CMBRaid";
			this.CMTAttack.Text = "send troops";
			this.CMTAttack.Click += new System.EventHandler(this.CMBAttackClick);
			// 
			// CMTAlarm
			// 
			this.CMTAlarm.Name = "CMTAlarm";
			this.CMTAlarm.Size = new System.Drawing.Size(129, 22);
			this.CMTAlarm.Tag = "CMBAlarm";
			this.CMTAlarm.Text = "auto alarm";
			this.CMTAlarm.Click += new System.EventHandler(this.CMBAlarm_Click);
			// 
			// toolStripSeparator16
			// 
			this.toolStripSeparator16.Name = "toolStripSeparator16";
			this.toolStripSeparator16.Size = new System.Drawing.Size(126, 6);
			// 
			// CMTRefresh
			// 
			this.CMTRefresh.Name = "CMTRefresh";
			this.CMTRefresh.Size = new System.Drawing.Size(129, 22);
			this.CMTRefresh.Tag = "refreshtroop";
			this.CMTRefresh.Text = "refresh troops";
			this.CMTRefresh.Click += new System.EventHandler(this.CMTRefresh_Click);
			// 
			// contextMenuTroopTraining
			// 
			this.contextMenuTroopTraining.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.trainingTroopsToolStripMenuItem,
									this.refreshTroopTrainingToolStripMenuItem});
			this.contextMenuTroopTraining.Name = "contextMenuTroopTraining";
			this.contextMenuTroopTraining.ShowImageMargin = false;
			this.contextMenuTroopTraining.Size = new System.Drawing.Size(178, 48);
			// 
			// trainingTroopsToolStripMenuItem
			// 
			this.trainingTroopsToolStripMenuItem.Name = "trainingTroopsToolStripMenuItem";
			this.trainingTroopsToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
			this.trainingTroopsToolStripMenuItem.Tag = "trainingtroops";
			this.trainingTroopsToolStripMenuItem.Text = "training troops";
			this.trainingTroopsToolStripMenuItem.Click += new System.EventHandler(this.TrainingTroopsToolStripMenuItemClick);
			// 
			// refreshTroopTrainingToolStripMenuItem
			// 
			this.refreshTroopTrainingToolStripMenuItem.Name = "refreshTroopTrainingToolStripMenuItem";
			this.refreshTroopTrainingToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
			this.refreshTroopTrainingToolStripMenuItem.Tag = "refreshtrooptraining";
			this.refreshTroopTrainingToolStripMenuItem.Text = "refresh troop training";
			this.refreshTroopTrainingToolStripMenuItem.Click += new System.EventHandler(this.RefreshTroopTrainingToolStripMenuItemClick);
			// 
			// contextMenuHeroAdventure
			// 
			this.contextMenuHeroAdventure.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.refreshAdvanturesToolStripMenuItem,
									this.startAdvantureToolStripMenuItem});
			this.contextMenuHeroAdventure.Name = "contextMenuHeroAdventure";
			this.contextMenuHeroAdventure.ShowImageMargin = false;
			this.contextMenuHeroAdventure.Size = new System.Drawing.Size(118, 48);
			// 
			// refreshAdvanturesToolStripMenuItem
			// 
			this.refreshAdvanturesToolStripMenuItem.Name = "refreshAdvanturesToolStripMenuItem";
			this.refreshAdvanturesToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
			this.refreshAdvanturesToolStripMenuItem.Tag = "CMSrefreshadvantures";
			this.refreshAdvanturesToolStripMenuItem.Text = "刷新探险位置";
			this.refreshAdvanturesToolStripMenuItem.Click += new System.EventHandler(this.RefreshAdvanturesToolStripMenuItemClick);
			// 
			// startAdvantureToolStripMenuItem
			// 
			this.startAdvantureToolStripMenuItem.Name = "startAdvantureToolStripMenuItem";
			this.startAdvantureToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
			this.startAdvantureToolStripMenuItem.Tag = "CMSstartadvanture";
			this.startAdvantureToolStripMenuItem.Text = "开始探险";
			this.startAdvantureToolStripMenuItem.Click += new System.EventHandler(this.StartAdvantureToolStripMenuItemClick);
			// 
			// MainFrame
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.button1);
			this.Controls.Add(this.tabControl2);
			this.Controls.Add(this.statusStrip1);
			this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Name = "MainFrame";
			this.Size = new System.Drawing.Size(934, 642);
			this.Load += new System.EventHandler(this.MainFrame_Load);
			this.tabControl2.ResumeLayout(false);
			this.tabPage2.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage1.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.contextMenuBuilding.ResumeLayout(false);
			this.contextMenuResearch.ResumeLayout(false);
			this.contextMenuQueue.ResumeLayout(false);
			this.contextMenuVillage.ResumeLayout(false);
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.contextMenuMarket.ResumeLayout(false);
			this.contextMenuInbuilding.ResumeLayout(false);
			this.contextMenuTroop.ResumeLayout(false);
			this.contextMenuTroopTraining.ResumeLayout(false);
			this.contextMenuHeroAdventure.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.ToolStripMenuItem CMB_PMMail;
		private System.Windows.Forms.ToolStripMenuItem startAdvantureToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem refreshAdvanturesToolStripMenuItem;
		public System.Windows.Forms.ContextMenuStrip contextMenuHeroAdventure;
		private System.Windows.Forms.ToolStripMenuItem refreshTroopTrainingToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem trainingTroopsToolStripMenuItem;
		public System.Windows.Forms.ContextMenuStrip contextMenuTroopTraining;
		private System.Windows.Forms.ToolStripMenuItem CMRUpgradeTroopLvlTo;
		private System.Windows.Forms.ToolStripMenuItem CMRUpgradeTroopLvl;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator17;
		private System.Windows.Forms.ToolStripMenuItem CMTAlarm;
		private System.Windows.Forms.ToolStripMenuItem CMVRefreshAll;
		private System.Windows.Forms.ToolStripMenuItem CMVRestoreRES;
		private System.Windows.Forms.ToolStripMenuItem CMVSaveRES;
		private System.Windows.Forms.ToolStripMenuItem CMMRefresh;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem CMTAttack;
		private System.Windows.Forms.ToolStripMenuItem CMBAttack;

		#endregion

		private System.Windows.Forms.TabControl tabControl2;
		private System.Windows.Forms.ToolStripMenuItem CMVRefresh;
		private System.Windows.Forms.ToolStripMenuItem CMBUp;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem CMBUp2;
		private System.Windows.Forms.ToolStripMenuItem CMBUp5;
		private System.Windows.Forms.ToolStripMenuItem CMBUp9;
		private System.Windows.Forms.ToolStripMenuItem CMBUpTo;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripMenuItem CMBDestroy;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private System.Windows.Forms.ToolStripMenuItem CMBNew;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem CMBAI;
		private System.Windows.Forms.ContextMenuStrip contextMenuEvent;
		public System.Windows.Forms.Timer timer1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.ToolStripMenuItem CMQDel;
		private System.Windows.Forms.ToolStripMenuItem CMQClear;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem CMQUp;
		private System.Windows.Forms.ToolStripMenuItem CMQDown;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
		private System.Windows.Forms.ToolStripMenuItem CMQTimer;
		private System.Windows.Forms.ToolStripMenuItem CMVSnapshot;
		private System.Windows.Forms.ToolStripMenuItem CMVSnapAll;
		private System.Windows.Forms.ToolStripMenuItem CMRResearch;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
		private System.Windows.Forms.ToolStripMenuItem CMBAI_C;
		private System.Windows.Forms.ToolStripMenuItem CMBAI_L;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
		private System.Windows.Forms.ToolStripMenuItem CMMNew2;
		public System.Windows.Forms.ContextMenuStrip contextMenuVillage;
		public System.Windows.Forms.ContextMenuStrip contextMenuBuilding;
		public System.Windows.Forms.ContextMenuStrip contextMenuQueue;
		public System.Windows.Forms.ContextMenuStrip contextMenuResearch;
		private System.Windows.Forms.TabPage tabPage2;
		private WeifenLuo.WinFormsUI.Docking.DockPanel dockPanel1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel LastDebug;
		private System.Windows.Forms.ToolStripStatusLabel SVRTime;
		private System.Windows.Forms.ToolStripStatusLabel LCLTime;
		private System.Windows.Forms.Timer timersec1;
		private System.Windows.Forms.ToolStripMenuItem CMBRefresh;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
		private System.Windows.Forms.ToolStripMenuItem CMRRefresh;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
		private System.Windows.Forms.ToolStripMenuItem CMBRefreshDestroy;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator12;
		private System.Windows.Forms.ToolStripMenuItem CMBParty;
		private System.Windows.Forms.ToolStripMenuItem CMBParty500;
		private System.Windows.Forms.ToolStripMenuItem CMBParty2000;
		private System.Windows.Forms.ToolStripMenuItem CMMNew;
		public System.Windows.Forms.ContextMenuStrip contextMenuMarket;
		private System.Windows.Forms.ToolStripMenuItem CMICancel;
		public System.Windows.Forms.ContextMenuStrip contextMenuInbuilding;
		private System.Windows.Forms.ToolStripMenuItem CMVLowerLimit;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
		private System.Windows.Forms.ToolStripMenuItem CMVUpperLimit;
		private System.Windows.Forms.ToolStripMenuItem CMMNpcTrade2;
		private System.Windows.Forms.ToolStripMenuItem CMMNpcTrade;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator13;
		private System.Windows.Forms.ToolStripMenuItem CMBRaid;
		private System.Windows.Forms.ToolStripMenuItem CMQPause;
		private System.Windows.Forms.ToolStripMenuItem CMQExport;
		private System.Windows.Forms.ToolStripMenuItem CMQImport;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator14;
		private System.Windows.Forms.ToolStripStatusLabel PageCount;
		private System.Windows.Forms.ToolStripStatusLabel ActionCount;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.CheckBox checkBoxVerbose;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator15;
		private System.Windows.Forms.ToolStripMenuItem CMBEnableCoin;
		private System.Windows.Forms.ToolStripMenuItem CMBNewCap;
        public System.Windows.Forms.ContextMenuStrip contextMenuTroop;
        private System.Windows.Forms.ToolStripMenuItem CMTRaid;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator16;
        private System.Windows.Forms.ToolStripMenuItem CMTRefresh;
        private System.Windows.Forms.ToolStripMenuItem CMQEdit;
        private System.Windows.Forms.ToolStripMenuItem CMVTlimit;
        private System.Windows.Forms.ToolStripMenuItem CMVRename;
        private System.Windows.Forms.ToolStripMenuItem CMBAlarm;
	}
}
