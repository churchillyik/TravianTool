using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using libTravian;

namespace Stran
{
	public partial class AttackOptForm : Form
	{
		public AttackOptForm()
		{
			InitializeComponent();
		}
		
        public Travian UpCall { get; set; }
		public int Tribe { get; set; }
		public int[] Troops { get; set; }
        public int VillageID { get; set; }
		public MUI mui { get; set; }
		public DisplayLang dl { get; set; }
		
		public NumericUpDown[,] Nums = new NumericUpDown[5, 11];	//	5种出兵设置
        public NumericUpDown[] Waves = new NumericUpDown[5];		//	每种出兵设置的发兵波数
        
        private DateTime actionAt = DateTime.MinValue;				//	何时开始执行
        private int minimumInterval = 0;							//	最小发兵时间间隔
        public List<TPoint> iTargets = new List<TPoint>();			//	攻击目标坐标的集合
        public List<TTInfo> nTargets = new List<TTInfo>();			//	部队设置的集合
        public List<int> nWaves = new List<int>();				//	发兵波数的集合
        public bool tablelayoutdone = false;
        
        public AttackQueue Return { get; private set; }

		private void AttackOptForm_Load(object sender, EventArgs e)
		{
			mui.RefreshLanguage(this);
			
            this.tableLayoutPanel1.Enabled = true;
            this.numericUpDown1.Enabled = true;
            this.comboBox1.Enabled = true;
            this.comboBox2.Enabled = true;
            this.comboBox3.Enabled = true;
            this.textCoord.Enabled = true;

            //	攻击类型
            string[] raidtypelist = this.mui._("raidtypelist").Split(',');
            for (int i = 0; i < 5; i++)
            {
                this.comboBox3.Items.Add(raidtypelist[i]);
            }
            this.comboBox3.SelectedItem = this.comboBox3.Items[0];
            
            //	投石车目标
            this.comboBox1.Items.Add(raidtypelist[5]);
            this.comboBox2.Items.Add(raidtypelist[5]);
            for (int i = 0; i < 41; i++)
            {
                if (i == 22 || i == 30 || i == 31 || i == 32 || i == 33 || i ==35)
                    continue;
                else
                {
                    this.comboBox1.Items.Add(dl.GetGidLang(i + 1));
                    this.comboBox2.Items.Add(dl.GetGidLang(i + 1));
                }
            }
            this.comboBox1.Items.Add(raidtypelist[6]);
            this.comboBox2.Items.Add(raidtypelist[6]);
            this.comboBox1.SelectedItem = this.comboBox1.Items[36];
            this.comboBox2.SelectedItem = this.comboBox2.Items[0];
            if (Troops[7] == 0)
            {
                this.comboBox1.Enabled = false;
                this.comboBox2.Enabled = false;
            }
            //	根据集结点的等级和投石车的数量决定目标设置
            var RP = UpCall.TD.Villages[VillageID].Buildings[39].Level;
            if (Troops[7] < 20)
            {
                if (RP < 10)
                {
                    this.comboBox1.Enabled = false;
                    this.comboBox1.SelectedItem = this.comboBox1.Items[36];
                }
                this.comboBox2.Enabled = false;
            }
            if (Troops[7] >= 20)
            {
                if (RP < 10)
                {
                    this.comboBox1.Enabled = false;
                    this.comboBox2.Enabled = false;
                    this.comboBox1.SelectedItem = this.comboBox1.Items[36];
                }
                else if (RP < 20)
                {
                    this.comboBox2.Enabled = false;
                }
            }
            
            //	英雄和拓荒者
            this.checkBox1.Text = dl.GetAidLang(Tribe, 11);	//	英雄
            this.checkBox2.Text = dl.GetAidLang(Tribe, 10);	//	拓荒者
            if (Troops[10] == 0)
                this.checkBox1.Enabled = false;
            if (Troops[9] < 3)
                this.checkBox2.Enabled = false;
			
            //	应用于拓荒者复选框的点击响应
            if (this.checkBox2.Checked)
            {
                this.numericUpDown1.Value = 1;
                this.numericUpDown1.Enabled = false;
                this.tableLayoutPanel1.Enabled = false;
                this.comboBox1.Enabled = false;
                this.comboBox2.Enabled = false;
                this.comboBox3.Enabled = false;
                //	少于6个拓荒者，不能同时前往多个目标
                if (Troops[9] < 6)
                {
                    this.textCoord.Enabled = false;
                }
            }
            
			// 动态加入numericUpDown控件
            if (!tablelayoutdone)
            {
                SuspendLayout();
                
                //	5种出兵设置各自的波数设置
                for (int i = 0; i < 5; i++)
                {
                    NumericUpDown wave = new NumericUpDown();
                    Waves[i] = wave;
                    tableLayoutPanel1.Controls.Add(wave, i + 2, 0);
                    wave.Dock = DockStyle.Fill;
                    wave.Maximum = 50;
                    wave.Minimum = 0;
                    wave.Increment = 1;
                }
                
                for (int i = 0; i < 11; i++)
                {
                	//	兵种名次
                    Label l1 = new Label();
                    tableLayoutPanel1.Controls.Add(l1, 0, i + 1);
                    l1.Dock = DockStyle.Fill;
                    l1.Text = dl.GetAidLang(Tribe, i + 1);
                    l1.AutoSize = false;
                    l1.TextAlign = ContentAlignment.MiddleCenter;
                    
                    //	兵种数量
                    Label l2 = new Label();
                    tableLayoutPanel1.Controls.Add(l2, 1, i + 1);
                    l2.Dock = DockStyle.Fill;
                    l2.Text = Troops[i].ToString();
                    l2.AutoSize = false;
                    l2.TextAlign = ContentAlignment.MiddleCenter;
                    
                    //	增加5种出兵设置
                    for (int j = 0; j < 5; j++)
                    {
                        NumericUpDown nud = new NumericUpDown();
                        Nums[j, i] = nud;
                        tableLayoutPanel1.Controls.Add(nud, j + 2, i + 1);
                        nud.Dock = DockStyle.Fill;
                        nud.TabIndex = i;
                        nud.Maximum = Troops[i];
                        nud.Minimum = 0;
                        nud.ThousandsSeparator = true;
                        nud.Increment = Math.Max(1, (decimal)(Troops[i] / 20));
                    }
                }
                ResumeLayout();
                
                tablelayoutdone = true;
            }
        }

		private void button1_Click(object sender, EventArgs e)
		{
			//	解析攻击目标的坐标
            if (this.txtX.Text == "" && this.txtY.Text == "" && this.textCoord.Text == "")
                return;
            else if(this.txtX.Text != "" || this.txtY.Text != "")
            {
            	//	单目标情况
                int x = 0, y = 0;
                Int32.TryParse(this.txtX.Text, out x);
                Int32.TryParse(this.txtY.Text, out y);
                var aTargets = new TPoint(x, y);
                iTargets.Add(aTargets);
            }
            else
            {
            	//	多目标情况，格式："xxx|yyy,xxx|yyy"
                if (!this.textCoord.Text.Contains(","))
                    return;
                string[] multipletargets = this.textCoord.Text.Split(',');
                for (int i = 0; i < multipletargets.Length; i++)
                {
                    if (!multipletargets[i].Contains("|"))
                        continue;
                    string[] id = multipletargets[i].Split('|');
                    int x1 = 0, y1 = 0;
                    Int32.TryParse(id[0], out x1);
                    Int32.TryParse(id[1], out y1);
                    TPoint tid = new TPoint(x1, y1);
                    if (!tid.IsEmpty)
                    iTargets.Add(tid);
                }
            }

            //	检查部队设置的数量是否超过现有的数量
            for (int i = 0; i < 11; i++)
            {
            	int totalattack = Convert.ToInt32(this.numericUpDown1.Value) * iTargets.Count;
            	int[] totalraidtroops = new int[11];
            	int totaltroops = 0;
            	int[] raidtroops = new int[11];
                for (int j = 0; j < 5; j++)
                {
                	int wv = Convert.ToInt32(Waves[j].Value);
                    raidtroops[i] = Convert.ToInt32(Nums[j, i].Value);
                    totaltroops = totaltroops + (raidtroops[i] * wv);
                }
                totalraidtroops[i] = totaltroops;
                if (totalraidtroops[i] * totalattack > Troops[i])
                {
        			var Nt = MessageBox.Show("No Enough Troop");
        			return;
            	}
            }
            
            
            for (int j = 0; j < 5; j++)
            {
            	int wv = Convert.ToInt32(Waves[j].Value);
            	int[] raidtroops = new int[11];
            	int totaltroops = 0;
            	for (int i = 0; i < 11; i++)
            	{
            		raidtroops[i] = Convert.ToInt32(Nums[j, i].Value);
            		totaltroops = totaltroops + raidtroops[i];
            	}
            	
            	if (wv != 0 && totaltroops != 0)
            	{
            		TTInfo mTargets = new TTInfo
            		{
            			Troops = raidtroops
            		};
            		nWaves.Add(wv);
            		nTargets.Add(mTargets);
            	}
            }
            
            if (nWaves.Count == 0 || nTargets.Count == 0 || iTargets.Count == 0)
            {
            	if (!this.checkBox2.Checked)
            		return;
            }

            Return = new AttackQueue
			{
                Raidtype = this.checkBox2.Checked ? 1 : this.comboBox3.SelectedIndex + 2,
				Targets = iTargets,
                wTroops = nTargets,
                wWaves = nWaves,
                VillageID = VillageID,
                kata = this.comboBox1.SelectedIndex,
                kata2 = this.comboBox2.SelectedIndex,
                Settlers = this.checkBox2.Checked,
                MinimumInterval = minimumInterval,
				NextExec = actionAt,
			};
		}

        private void buttonTiming_Click(object sender, EventArgs e)
        {
            ActionTiming tt = new ActionTiming
            {
                ActionAt = actionAt,
                MinimumInterval = minimumInterval,
                ActionTime = 0,
                mui = mui
            };

            //	取得定时发兵的设置
            if (tt.ShowDialog() == DialogResult.OK)
            {
                actionAt = tt.ActionAt;
                minimumInterval = tt.MinimumInterval;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            AttackOptForm_Load(sender, e);
        }
	}
}
