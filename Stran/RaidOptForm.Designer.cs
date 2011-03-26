using System.Windows.Forms;
namespace Stran
{
	partial class RaidOptForm
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
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.txtT1 = new System.Windows.Forms.TextBox();
            this.txtT2 = new System.Windows.Forms.TextBox();
            this.txtT3 = new System.Windows.Forms.TextBox();
            this.txtT6 = new System.Windows.Forms.TextBox();
            this.txtT5 = new System.Windows.Forms.TextBox();
            this.txtT4 = new System.Windows.Forms.TextBox();
            this.lblT1 = new System.Windows.Forms.Label();
            this.lblT2 = new System.Windows.Forms.Label();
            this.lblT3 = new System.Windows.Forms.Label();
            this.lblT6 = new System.Windows.Forms.Label();
            this.lblT5 = new System.Windows.Forms.Label();
            this.lblT4 = new System.Windows.Forms.Label();
            this.lblT1Max = new System.Windows.Forms.Label();
            this.lblT2Max = new System.Windows.Forms.Label();
            this.lblT3Max = new System.Windows.Forms.Label();
            this.lblT6Max = new System.Windows.Forms.Label();
            this.lblT5Max = new System.Windows.Forms.Label();
            this.lblT4Max = new System.Windows.Forms.Label();
            this.lblT8Max = new System.Windows.Forms.Label();
            this.lblT7Max = new System.Windows.Forms.Label();
            this.lblT8 = new System.Windows.Forms.Label();
            this.lblT7 = new System.Windows.Forms.Label();
            this.txtT8 = new System.Windows.Forms.TextBox();
            this.txtT7 = new System.Windows.Forms.TextBox();
            this.lblT11Max = new System.Windows.Forms.Label();
            this.lblT10Max = new System.Windows.Forms.Label();
            this.lblT9Max = new System.Windows.Forms.Label();
            this.lblT11 = new System.Windows.Forms.Label();
            this.lblT10 = new System.Windows.Forms.Label();
            this.lblT9 = new System.Windows.Forms.Label();
            this.txtT11 = new System.Windows.Forms.TextBox();
            this.txtT10 = new System.Windows.Forms.TextBox();
            this.txtT9 = new System.Windows.Forms.TextBox();
            this.rdbTypeReinforce = new System.Windows.Forms.RadioButton();
            this.rdbAttackNormal = new System.Windows.Forms.RadioButton();
            this.rdbAttackRaid = new System.Windows.Forms.RadioButton();
            this.grpTroops = new System.Windows.Forms.GroupBox();
            this.grpRaidType = new System.Windows.Forms.GroupBox();
            this.txtX = new System.Windows.Forms.TextBox();
            this.lblX = new System.Windows.Forms.Label();
            this.lblY = new System.Windows.Forms.Label();
            this.txtY = new System.Windows.Forms.TextBox();
            this.lstTargets = new System.Windows.Forms.ListBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.grpTargets = new System.Windows.Forms.GroupBox();
            this.btnPaste = new System.Windows.Forms.Button();
            this.btnCopy = new System.Windows.Forms.Button();
            this.btnSort = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.grpSpyOptions = new System.Windows.Forms.GroupBox();
            this.rdbSpyDefense = new System.Windows.Forms.RadioButton();
            this.rdbSpyResource = new System.Windows.Forms.RadioButton();
            this.nudCount = new System.Windows.Forms.NumericUpDown();
            this.lblCount = new System.Windows.Forms.Label();
            this.grpSchedule = new System.Windows.Forms.GroupBox();
            this.ckbMultipleRaids = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbDesc = new System.Windows.Forms.TextBox();
            this.lblMaxSlots = new System.Windows.Forms.Label();
            this.nudMaxSlots = new System.Windows.Forms.NumericUpDown();
            this.grpTroops.SuspendLayout();
            this.grpRaidType.SuspendLayout();
            this.grpTargets.SuspendLayout();
            this.grpSpyOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCount)).BeginInit();
            this.grpSchedule.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxSlots)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(12, 384);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(80, 30);
            this.btnOk.TabIndex = 20;
            this.btnOk.Tag = "ok";
            this.btnOk.Text = "ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(98, 384);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 30);
            this.btnCancel.TabIndex = 21;
            this.btnCancel.Tag = "cancel";
            this.btnCancel.Text = "cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // txtT1
            // 
            this.txtT1.Location = new System.Drawing.Point(35, 26);
            this.txtT1.Name = "txtT1";
            this.txtT1.Size = new System.Drawing.Size(66, 26);
            this.txtT1.TabIndex = 22;
            this.txtT1.Text = "0";
            this.txtT1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtT2
            // 
            this.txtT2.Location = new System.Drawing.Point(35, 58);
            this.txtT2.Name = "txtT2";
            this.txtT2.Size = new System.Drawing.Size(66, 26);
            this.txtT2.TabIndex = 23;
            this.txtT2.Text = "0";
            this.txtT2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtT3
            // 
            this.txtT3.Location = new System.Drawing.Point(35, 90);
            this.txtT3.Name = "txtT3";
            this.txtT3.Size = new System.Drawing.Size(66, 26);
            this.txtT3.TabIndex = 24;
            this.txtT3.Text = "0";
            this.txtT3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtT6
            // 
            this.txtT6.Location = new System.Drawing.Point(205, 90);
            this.txtT6.Name = "txtT6";
            this.txtT6.Size = new System.Drawing.Size(66, 26);
            this.txtT6.TabIndex = 27;
            this.txtT6.Text = "0";
            this.txtT6.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtT5
            // 
            this.txtT5.Location = new System.Drawing.Point(205, 58);
            this.txtT5.Name = "txtT5";
            this.txtT5.Size = new System.Drawing.Size(66, 26);
            this.txtT5.TabIndex = 26;
            this.txtT5.Text = "0";
            this.txtT5.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtT4
            // 
            this.txtT4.Location = new System.Drawing.Point(205, 26);
            this.txtT4.Name = "txtT4";
            this.txtT4.Size = new System.Drawing.Size(66, 26);
            this.txtT4.TabIndex = 25;
            this.txtT4.Text = "0";
            this.txtT4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblT1
            // 
            this.lblT1.AutoSize = true;
            this.lblT1.Location = new System.Drawing.Point(3, 29);
            this.lblT1.Name = "lblT1";
            this.lblT1.Size = new System.Drawing.Size(26, 18);
            this.lblT1.TabIndex = 28;
            this.lblT1.Text = "T1";
            // 
            // lblT2
            // 
            this.lblT2.AutoSize = true;
            this.lblT2.Location = new System.Drawing.Point(3, 61);
            this.lblT2.Name = "lblT2";
            this.lblT2.Size = new System.Drawing.Size(26, 18);
            this.lblT2.TabIndex = 29;
            this.lblT2.Text = "T2";
            // 
            // lblT3
            // 
            this.lblT3.AutoSize = true;
            this.lblT3.Location = new System.Drawing.Point(3, 93);
            this.lblT3.Name = "lblT3";
            this.lblT3.Size = new System.Drawing.Size(26, 18);
            this.lblT3.TabIndex = 30;
            this.lblT3.Text = "T3";
            // 
            // lblT6
            // 
            this.lblT6.AutoSize = true;
            this.lblT6.Location = new System.Drawing.Point(173, 93);
            this.lblT6.Name = "lblT6";
            this.lblT6.Size = new System.Drawing.Size(26, 18);
            this.lblT6.TabIndex = 33;
            this.lblT6.Text = "T6";
            // 
            // lblT5
            // 
            this.lblT5.AutoSize = true;
            this.lblT5.Location = new System.Drawing.Point(173, 61);
            this.lblT5.Name = "lblT5";
            this.lblT5.Size = new System.Drawing.Size(26, 18);
            this.lblT5.TabIndex = 32;
            this.lblT5.Text = "T5";
            // 
            // lblT4
            // 
            this.lblT4.AutoSize = true;
            this.lblT4.Location = new System.Drawing.Point(173, 29);
            this.lblT4.Name = "lblT4";
            this.lblT4.Size = new System.Drawing.Size(26, 18);
            this.lblT4.TabIndex = 31;
            this.lblT4.Text = "T4";
            // 
            // lblT1Max
            // 
            this.lblT1Max.AutoSize = true;
            this.lblT1Max.ForeColor = System.Drawing.Color.Green;
            this.lblT1Max.Location = new System.Drawing.Point(107, 29);
            this.lblT1Max.Name = "lblT1Max";
            this.lblT1Max.Size = new System.Drawing.Size(28, 18);
            this.lblT1Max.TabIndex = 34;
            this.lblT1Max.Text = "(0)";
            this.lblT1Max.Click += new System.EventHandler(this.lblMaxTroops_Click);
            // 
            // lblT2Max
            // 
            this.lblT2Max.AutoSize = true;
            this.lblT2Max.ForeColor = System.Drawing.Color.Green;
            this.lblT2Max.Location = new System.Drawing.Point(107, 61);
            this.lblT2Max.Name = "lblT2Max";
            this.lblT2Max.Size = new System.Drawing.Size(28, 18);
            this.lblT2Max.TabIndex = 35;
            this.lblT2Max.Text = "(0)";
            this.lblT2Max.Click += new System.EventHandler(this.lblMaxTroops_Click);
            // 
            // lblT3Max
            // 
            this.lblT3Max.AutoSize = true;
            this.lblT3Max.ForeColor = System.Drawing.Color.Green;
            this.lblT3Max.Location = new System.Drawing.Point(107, 93);
            this.lblT3Max.Name = "lblT3Max";
            this.lblT3Max.Size = new System.Drawing.Size(28, 18);
            this.lblT3Max.TabIndex = 36;
            this.lblT3Max.Text = "(0)";
            this.lblT3Max.Click += new System.EventHandler(this.lblMaxTroops_Click);
            // 
            // lblT6Max
            // 
            this.lblT6Max.AutoSize = true;
            this.lblT6Max.ForeColor = System.Drawing.Color.Green;
            this.lblT6Max.Location = new System.Drawing.Point(277, 93);
            this.lblT6Max.Name = "lblT6Max";
            this.lblT6Max.Size = new System.Drawing.Size(28, 18);
            this.lblT6Max.TabIndex = 39;
            this.lblT6Max.Text = "(0)";
            this.lblT6Max.Click += new System.EventHandler(this.lblMaxTroops_Click);
            // 
            // lblT5Max
            // 
            this.lblT5Max.AutoSize = true;
            this.lblT5Max.ForeColor = System.Drawing.Color.Green;
            this.lblT5Max.Location = new System.Drawing.Point(277, 61);
            this.lblT5Max.Name = "lblT5Max";
            this.lblT5Max.Size = new System.Drawing.Size(28, 18);
            this.lblT5Max.TabIndex = 38;
            this.lblT5Max.Text = "(0)";
            this.lblT5Max.Click += new System.EventHandler(this.lblMaxTroops_Click);
            // 
            // lblT4Max
            // 
            this.lblT4Max.AutoSize = true;
            this.lblT4Max.ForeColor = System.Drawing.Color.Green;
            this.lblT4Max.Location = new System.Drawing.Point(277, 29);
            this.lblT4Max.Name = "lblT4Max";
            this.lblT4Max.Size = new System.Drawing.Size(28, 18);
            this.lblT4Max.TabIndex = 37;
            this.lblT4Max.Text = "(0)";
            this.lblT4Max.Click += new System.EventHandler(this.lblMaxTroops_Click);
            // 
            // lblT8Max
            // 
            this.lblT8Max.AutoSize = true;
            this.lblT8Max.ForeColor = System.Drawing.Color.Green;
            this.lblT8Max.Location = new System.Drawing.Point(447, 61);
            this.lblT8Max.Name = "lblT8Max";
            this.lblT8Max.Size = new System.Drawing.Size(28, 18);
            this.lblT8Max.TabIndex = 47;
            this.lblT8Max.Text = "(0)";
            this.lblT8Max.Click += new System.EventHandler(this.lblMaxTroops_Click);
            // 
            // lblT7Max
            // 
            this.lblT7Max.AutoSize = true;
            this.lblT7Max.ForeColor = System.Drawing.Color.Green;
            this.lblT7Max.Location = new System.Drawing.Point(447, 29);
            this.lblT7Max.Name = "lblT7Max";
            this.lblT7Max.Size = new System.Drawing.Size(28, 18);
            this.lblT7Max.TabIndex = 46;
            this.lblT7Max.Text = "(0)";
            this.lblT7Max.Click += new System.EventHandler(this.lblMaxTroops_Click);
            // 
            // lblT8
            // 
            this.lblT8.AutoSize = true;
            this.lblT8.Location = new System.Drawing.Point(343, 61);
            this.lblT8.Name = "lblT8";
            this.lblT8.Size = new System.Drawing.Size(26, 18);
            this.lblT8.TabIndex = 44;
            this.lblT8.Text = "T8";
            // 
            // lblT7
            // 
            this.lblT7.AutoSize = true;
            this.lblT7.Location = new System.Drawing.Point(343, 29);
            this.lblT7.Name = "lblT7";
            this.lblT7.Size = new System.Drawing.Size(26, 18);
            this.lblT7.TabIndex = 43;
            this.lblT7.Text = "T7";
            // 
            // txtT8
            // 
            this.txtT8.Location = new System.Drawing.Point(375, 58);
            this.txtT8.Name = "txtT8";
            this.txtT8.Size = new System.Drawing.Size(66, 26);
            this.txtT8.TabIndex = 41;
            this.txtT8.Text = "0";
            this.txtT8.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtT7
            // 
            this.txtT7.Location = new System.Drawing.Point(375, 26);
            this.txtT7.Name = "txtT7";
            this.txtT7.Size = new System.Drawing.Size(66, 26);
            this.txtT7.TabIndex = 40;
            this.txtT7.Text = "0";
            this.txtT7.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblT11Max
            // 
            this.lblT11Max.AutoSize = true;
            this.lblT11Max.ForeColor = System.Drawing.Color.Green;
            this.lblT11Max.Location = new System.Drawing.Point(617, 93);
            this.lblT11Max.Name = "lblT11Max";
            this.lblT11Max.Size = new System.Drawing.Size(28, 18);
            this.lblT11Max.TabIndex = 56;
            this.lblT11Max.Text = "(0)";
            this.lblT11Max.Click += new System.EventHandler(this.lblMaxTroops_Click);
            // 
            // lblT10Max
            // 
            this.lblT10Max.AutoSize = true;
            this.lblT10Max.ForeColor = System.Drawing.Color.Green;
            this.lblT10Max.Location = new System.Drawing.Point(617, 61);
            this.lblT10Max.Name = "lblT10Max";
            this.lblT10Max.Size = new System.Drawing.Size(28, 18);
            this.lblT10Max.TabIndex = 55;
            this.lblT10Max.Text = "(0)";
            this.lblT10Max.Click += new System.EventHandler(this.lblMaxTroops_Click);
            // 
            // lblT9Max
            // 
            this.lblT9Max.AutoSize = true;
            this.lblT9Max.ForeColor = System.Drawing.Color.Green;
            this.lblT9Max.Location = new System.Drawing.Point(617, 29);
            this.lblT9Max.Name = "lblT9Max";
            this.lblT9Max.Size = new System.Drawing.Size(28, 18);
            this.lblT9Max.TabIndex = 54;
            this.lblT9Max.Text = "(0)";
            this.lblT9Max.Click += new System.EventHandler(this.lblMaxTroops_Click);
            // 
            // lblT11
            // 
            this.lblT11.AutoSize = true;
            this.lblT11.Location = new System.Drawing.Point(513, 93);
            this.lblT11.Name = "lblT11";
            this.lblT11.Size = new System.Drawing.Size(34, 18);
            this.lblT11.TabIndex = 53;
            this.lblT11.Text = "T11";
            // 
            // lblT10
            // 
            this.lblT10.AutoSize = true;
            this.lblT10.Location = new System.Drawing.Point(513, 61);
            this.lblT10.Name = "lblT10";
            this.lblT10.Size = new System.Drawing.Size(34, 18);
            this.lblT10.TabIndex = 52;
            this.lblT10.Text = "T10";
            // 
            // lblT9
            // 
            this.lblT9.AutoSize = true;
            this.lblT9.Location = new System.Drawing.Point(513, 29);
            this.lblT9.Name = "lblT9";
            this.lblT9.Size = new System.Drawing.Size(26, 18);
            this.lblT9.TabIndex = 51;
            this.lblT9.Text = "T9";
            // 
            // txtT11
            // 
            this.txtT11.Location = new System.Drawing.Point(545, 90);
            this.txtT11.Name = "txtT11";
            this.txtT11.Size = new System.Drawing.Size(66, 26);
            this.txtT11.TabIndex = 50;
            this.txtT11.Text = "0";
            this.txtT11.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtT10
            // 
            this.txtT10.Location = new System.Drawing.Point(545, 58);
            this.txtT10.Name = "txtT10";
            this.txtT10.Size = new System.Drawing.Size(66, 26);
            this.txtT10.TabIndex = 49;
            this.txtT10.Text = "0";
            this.txtT10.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtT9
            // 
            this.txtT9.Location = new System.Drawing.Point(545, 26);
            this.txtT9.Name = "txtT9";
            this.txtT9.Size = new System.Drawing.Size(66, 26);
            this.txtT9.TabIndex = 48;
            this.txtT9.Text = "0";
            this.txtT9.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // rdbTypeReinforce
            // 
            this.rdbTypeReinforce.AutoSize = true;
            this.rdbTypeReinforce.Location = new System.Drawing.Point(12, 22);
            this.rdbTypeReinforce.Name = "rdbTypeReinforce";
            this.rdbTypeReinforce.Size = new System.Drawing.Size(89, 22);
            this.rdbTypeReinforce.TabIndex = 57;
            this.rdbTypeReinforce.Text = "Reinforce";
            this.rdbTypeReinforce.UseVisualStyleBackColor = true;
            // 
            // rdbAttackNormal
            // 
            this.rdbAttackNormal.AutoSize = true;
            this.rdbAttackNormal.Location = new System.Drawing.Point(12, 46);
            this.rdbAttackNormal.Name = "rdbAttackNormal";
            this.rdbAttackNormal.Size = new System.Drawing.Size(126, 22);
            this.rdbAttackNormal.TabIndex = 58;
            this.rdbAttackNormal.Text = "Attack: Normal";
            this.rdbAttackNormal.UseVisualStyleBackColor = true;
            // 
            // rdbAttackRaid
            // 
            this.rdbAttackRaid.AutoSize = true;
            this.rdbAttackRaid.Checked = true;
            this.rdbAttackRaid.Location = new System.Drawing.Point(12, 70);
            this.rdbAttackRaid.Name = "rdbAttackRaid";
            this.rdbAttackRaid.Size = new System.Drawing.Size(107, 22);
            this.rdbAttackRaid.TabIndex = 59;
            this.rdbAttackRaid.TabStop = true;
            this.rdbAttackRaid.Text = "Attack: Raid";
            this.rdbAttackRaid.UseVisualStyleBackColor = true;
            // 
            // grpTroops
            // 
            this.grpTroops.Controls.Add(this.lblT11Max);
            this.grpTroops.Controls.Add(this.lblT10Max);
            this.grpTroops.Controls.Add(this.lblT9Max);
            this.grpTroops.Controls.Add(this.lblT11);
            this.grpTroops.Controls.Add(this.lblT10);
            this.grpTroops.Controls.Add(this.lblT9);
            this.grpTroops.Controls.Add(this.txtT11);
            this.grpTroops.Controls.Add(this.txtT10);
            this.grpTroops.Controls.Add(this.txtT9);
            this.grpTroops.Controls.Add(this.lblT8Max);
            this.grpTroops.Controls.Add(this.lblT7Max);
            this.grpTroops.Controls.Add(this.lblT8);
            this.grpTroops.Controls.Add(this.lblT7);
            this.grpTroops.Controls.Add(this.txtT8);
            this.grpTroops.Controls.Add(this.txtT7);
            this.grpTroops.Controls.Add(this.lblT6Max);
            this.grpTroops.Controls.Add(this.lblT5Max);
            this.grpTroops.Controls.Add(this.lblT4Max);
            this.grpTroops.Controls.Add(this.lblT3Max);
            this.grpTroops.Controls.Add(this.lblT2Max);
            this.grpTroops.Controls.Add(this.lblT1Max);
            this.grpTroops.Controls.Add(this.lblT6);
            this.grpTroops.Controls.Add(this.lblT5);
            this.grpTroops.Controls.Add(this.lblT4);
            this.grpTroops.Controls.Add(this.lblT3);
            this.grpTroops.Controls.Add(this.lblT2);
            this.grpTroops.Controls.Add(this.lblT1);
            this.grpTroops.Controls.Add(this.txtT6);
            this.grpTroops.Controls.Add(this.txtT5);
            this.grpTroops.Controls.Add(this.txtT4);
            this.grpTroops.Controls.Add(this.txtT3);
            this.grpTroops.Controls.Add(this.txtT2);
            this.grpTroops.Controls.Add(this.txtT1);
            this.grpTroops.Location = new System.Drawing.Point(12, 12);
            this.grpTroops.Name = "grpTroops";
            this.grpTroops.Size = new System.Drawing.Size(700, 129);
            this.grpTroops.TabIndex = 60;
            this.grpTroops.TabStop = false;
            this.grpTroops.Text = "Troops";
            // 
            // grpRaidType
            // 
            this.grpRaidType.Controls.Add(this.rdbAttackRaid);
            this.grpRaidType.Controls.Add(this.rdbAttackNormal);
            this.grpRaidType.Controls.Add(this.rdbTypeReinforce);
            this.grpRaidType.Location = new System.Drawing.Point(12, 147);
            this.grpRaidType.Name = "grpRaidType";
            this.grpRaidType.Size = new System.Drawing.Size(147, 95);
            this.grpRaidType.TabIndex = 61;
            this.grpRaidType.TabStop = false;
            this.grpRaidType.Text = "Raid Type";
            // 
            // txtX
            // 
            this.txtX.Location = new System.Drawing.Point(30, 25);
            this.txtX.Name = "txtX";
            this.txtX.Size = new System.Drawing.Size(48, 26);
            this.txtX.TabIndex = 62;
            this.txtX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblX
            // 
            this.lblX.AutoSize = true;
            this.lblX.Location = new System.Drawing.Point(7, 29);
            this.lblX.Name = "lblX";
            this.lblX.Size = new System.Drawing.Size(17, 18);
            this.lblX.TabIndex = 63;
            this.lblX.Text = "X";
            // 
            // lblY
            // 
            this.lblY.AutoSize = true;
            this.lblY.Location = new System.Drawing.Point(7, 57);
            this.lblY.Name = "lblY";
            this.lblY.Size = new System.Drawing.Size(18, 18);
            this.lblY.TabIndex = 65;
            this.lblY.Text = "Y";
            // 
            // txtY
            // 
            this.txtY.Location = new System.Drawing.Point(31, 54);
            this.txtY.Name = "txtY";
            this.txtY.Size = new System.Drawing.Size(48, 26);
            this.txtY.TabIndex = 64;
            this.txtY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lstTargets
            // 
            this.lstTargets.ColumnWidth = 80;
            this.lstTargets.FormattingEnabled = true;
            this.lstTargets.ItemHeight = 18;
            this.lstTargets.Location = new System.Drawing.Point(191, 25);
            this.lstTargets.MultiColumn = true;
            this.lstTargets.Name = "lstTargets";
            this.lstTargets.Size = new System.Drawing.Size(160, 184);
            this.lstTargets.TabIndex = 66;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(95, 26);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 67;
            this.btnAdd.Text = ">>";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // grpTargets
            // 
            this.grpTargets.Controls.Add(this.btnPaste);
            this.grpTargets.Controls.Add(this.btnCopy);
            this.grpTargets.Controls.Add(this.btnSort);
            this.grpTargets.Controls.Add(this.btnRemove);
            this.grpTargets.Controls.Add(this.btnAdd);
            this.grpTargets.Controls.Add(this.lstTargets);
            this.grpTargets.Controls.Add(this.lblY);
            this.grpTargets.Controls.Add(this.txtY);
            this.grpTargets.Controls.Add(this.lblX);
            this.grpTargets.Controls.Add(this.txtX);
            this.grpTargets.Location = new System.Drawing.Point(344, 147);
            this.grpTargets.Name = "grpTargets";
            this.grpTargets.Size = new System.Drawing.Size(368, 223);
            this.grpTargets.TabIndex = 68;
            this.grpTargets.TabStop = false;
            this.grpTargets.Text = "Villages";
            // 
            // btnPaste
            // 
            this.btnPaste.Location = new System.Drawing.Point(95, 142);
            this.btnPaste.Name = "btnPaste";
            this.btnPaste.Size = new System.Drawing.Size(75, 23);
            this.btnPaste.TabIndex = 71;
            this.btnPaste.Text = "Paste";
            this.btnPaste.UseVisualStyleBackColor = true;
            this.btnPaste.Click += new System.EventHandler(this.btnPaste_Click);
            // 
            // btnCopy
            // 
            this.btnCopy.Location = new System.Drawing.Point(95, 113);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(75, 23);
            this.btnCopy.TabIndex = 70;
            this.btnCopy.Text = "Copy";
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // btnSort
            // 
            this.btnSort.Location = new System.Drawing.Point(95, 84);
            this.btnSort.Name = "btnSort";
            this.btnSort.Size = new System.Drawing.Size(75, 23);
            this.btnSort.TabIndex = 69;
            this.btnSort.Text = "Sort";
            this.btnSort.UseVisualStyleBackColor = true;
            this.btnSort.Click += new System.EventHandler(this.btnSort_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(95, 55);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(75, 23);
            this.btnRemove.TabIndex = 68;
            this.btnRemove.Text = "<<";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // grpSpyOptions
            // 
            this.grpSpyOptions.Controls.Add(this.rdbSpyDefense);
            this.grpSpyOptions.Controls.Add(this.rdbSpyResource);
            this.grpSpyOptions.Location = new System.Drawing.Point(173, 147);
            this.grpSpyOptions.Name = "grpSpyOptions";
            this.grpSpyOptions.Size = new System.Drawing.Size(158, 81);
            this.grpSpyOptions.TabIndex = 69;
            this.grpSpyOptions.TabStop = false;
            this.grpSpyOptions.Text = "Spy Options";
            // 
            // rdbSpyDefense
            // 
            this.rdbSpyDefense.AutoSize = true;
            this.rdbSpyDefense.Checked = true;
            this.rdbSpyDefense.Location = new System.Drawing.Point(12, 50);
            this.rdbSpyDefense.Name = "rdbSpyDefense";
            this.rdbSpyDefense.Size = new System.Drawing.Size(129, 22);
            this.rdbSpyDefense.TabIndex = 58;
            this.rdbSpyDefense.TabStop = true;
            this.rdbSpyDefense.Text = "Defense,troops";
            this.rdbSpyDefense.UseVisualStyleBackColor = true;
            // 
            // rdbSpyResource
            // 
            this.rdbSpyResource.AutoSize = true;
            this.rdbSpyResource.Location = new System.Drawing.Point(12, 25);
            this.rdbSpyResource.Name = "rdbSpyResource";
            this.rdbSpyResource.Size = new System.Drawing.Size(135, 22);
            this.rdbSpyResource.TabIndex = 57;
            this.rdbSpyResource.Text = "Resource,troops";
            this.rdbSpyResource.UseVisualStyleBackColor = true;
            // 
            // nudCount
            // 
            this.nudCount.Location = new System.Drawing.Point(117, 25);
            this.nudCount.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.nudCount.Name = "nudCount";
            this.nudCount.Size = new System.Drawing.Size(31, 26);
            this.nudCount.TabIndex = 70;
            this.nudCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblCount
            // 
            this.lblCount.Location = new System.Drawing.Point(12, 26);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(104, 22);
            this.lblCount.TabIndex = 71;
            this.lblCount.Tag = "transfercnt";
            this.lblCount.Text = "Repeat";
            this.lblCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // grpSchedule
            // 
            this.grpSchedule.Controls.Add(this.lblMaxSlots);
            this.grpSchedule.Controls.Add(this.ckbMultipleRaids);
            this.grpSchedule.Controls.Add(this.nudMaxSlots);
            this.grpSchedule.Controls.Add(this.lblCount);
            this.grpSchedule.Controls.Add(this.nudCount);
            this.grpSchedule.Location = new System.Drawing.Point(173, 247);
            this.grpSchedule.Name = "grpSchedule";
            this.grpSchedule.Size = new System.Drawing.Size(158, 123);
            this.grpSchedule.TabIndex = 72;
            this.grpSchedule.TabStop = false;
            this.grpSchedule.Text = "Schedule";
            // 
            // ckbMultipleRaids
            // 
            this.ckbMultipleRaids.AutoSize = true;
            this.ckbMultipleRaids.Location = new System.Drawing.Point(15, 91);
            this.ckbMultipleRaids.Name = "ckbMultipleRaids";
            this.ckbMultipleRaids.Size = new System.Drawing.Size(116, 22);
            this.ckbMultipleRaids.TabIndex = 72;
            this.ckbMultipleRaids.Text = "Multiple Raids";
            this.ckbMultipleRaids.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbDesc);
            this.groupBox1.Location = new System.Drawing.Point(13, 249);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(146, 59);
            this.groupBox1.TabIndex = 73;
            this.groupBox1.TabStop = false;
            this.groupBox1.Tag = "Description";
            this.groupBox1.Text = "Description";
            // 
            // tbDesc
            // 
            this.tbDesc.Location = new System.Drawing.Point(11, 23);
            this.tbDesc.Name = "tbDesc";
            this.tbDesc.Size = new System.Drawing.Size(129, 26);
            this.tbDesc.TabIndex = 0;
            // 
            // lblMaxSlots
            // 
            this.lblMaxSlots.Location = new System.Drawing.Point(12, 60);
            this.lblMaxSlots.Name = "lblMaxSlots";
            this.lblMaxSlots.Size = new System.Drawing.Size(82, 22);
            this.lblMaxSlots.TabIndex = 74;
            this.lblMaxSlots.Text = "Max Slots";
            // 
            // nudMaxSlots
            // 
            this.nudMaxSlots.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudMaxSlots.Location = new System.Drawing.Point(100, 59);
            this.nudMaxSlots.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.nudMaxSlots.Name = "nudMaxSlots";
            this.nudMaxSlots.Size = new System.Drawing.Size(48, 26);
            this.nudMaxSlots.TabIndex = 73;
            this.nudMaxSlots.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // RaidOptForm
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(730, 428);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.grpSchedule);
            this.Controls.Add(this.grpSpyOptions);
            this.Controls.Add(this.grpTargets);
            this.Controls.Add(this.grpRaidType);
            this.Controls.Add(this.grpTroops);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "RaidOptForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Tag = "SendTroops";
            this.Text = "Send troops";
            this.Load += new System.EventHandler(this.RaidOptForm_Load);
            this.grpTroops.ResumeLayout(false);
            this.grpTroops.PerformLayout();
            this.grpRaidType.ResumeLayout(false);
            this.grpRaidType.PerformLayout();
            this.grpTargets.ResumeLayout(false);
            this.grpTargets.PerformLayout();
            this.grpSpyOptions.ResumeLayout(false);
            this.grpSpyOptions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCount)).EndInit();
            this.grpSchedule.ResumeLayout(false);
            this.grpSchedule.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxSlots)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

        private Button btnOk;
        private Button btnCancel;
        private TextBox txtT1;
        private TextBox txtT2;
        private TextBox txtT3;
        private TextBox txtT6;
        private TextBox txtT5;
        private TextBox txtT4;
        private Label lblT1;
        private Label lblT2;
        private Label lblT3;
        private Label lblT6;
        private Label lblT5;
        private Label lblT4;
        private Label lblT1Max;
        private Label lblT2Max;
        private Label lblT3Max;
        private Label lblT6Max;
        private Label lblT5Max;
        private Label lblT4Max;
        private Label lblT8Max;
        private Label lblT7Max;
        private Label lblT8;
        private Label lblT7;
        private TextBox txtT8;
        private TextBox txtT7;
        private Label lblT11Max;
        private Label lblT10Max;
        private Label lblT9Max;
        private Label lblT11;
        private Label lblT10;
        private Label lblT9;
        private TextBox txtT11;
        private TextBox txtT10;
        private TextBox txtT9;
        private RadioButton rdbTypeReinforce;
        private RadioButton rdbAttackNormal;
        private RadioButton rdbAttackRaid;
        private GroupBox grpTroops;
        private GroupBox grpRaidType;
        private TextBox txtX;
        private Label lblX;
        private Label lblY;
        private TextBox txtY;
        private ListBox lstTargets;
        private Button btnAdd;
        private GroupBox grpTargets;
        private Button btnRemove;
        private Button btnSort;
        private GroupBox grpSpyOptions;
        private RadioButton rdbSpyDefense;
        private RadioButton rdbSpyResource;
        private NumericUpDown nudCount;
        private Label lblCount;
        private GroupBox grpSchedule;
        private GroupBox groupBox1;
        private TextBox tbDesc;
        private CheckBox ckbMultipleRaids;
        private Button btnPaste;
        private Button btnCopy;
        private Label lblMaxSlots;
        private NumericUpDown nudMaxSlots;
	}
}