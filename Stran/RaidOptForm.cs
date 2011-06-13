using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using libTravian;

namespace Stran
{
    public partial class RaidOptForm : Form
    {
        #region Fields
        private TextBox[] txtTroops;
        private Label[] lblTroops;
        private Label[] lblMaxTroops;
        private RadioButton[] rdbRaidTypes;
        private RadioButton[] rdbSpyOptions;
        #endregion

        #region Constructor
        public RaidOptForm()
        {
            InitializeComponent();
            this.CreateControlArrays();
            if (MainForm.Options.ContainsKey("AllowMultipleRaids"))
            {
                this.grpSchedule.Visible = true;
            }
        }

        private void LoadTroopIcons()
        {
            Bitmap troopIcons = Properties.Resources.TroopIcons;
            for (int i = 0; i < this.lblTroops.Length; i++)
            {
                Rectangle rect = new Rectangle(
                    20 * i,
                    (this.Return.Tribe - 1) * 20,
                    18,
                    16);
                Bitmap icon = troopIcons.Clone(rect, troopIcons.PixelFormat);
                icon.MakeTransparent();
                this.lblTroops[i].Image = icon;
                this.lblTroops[i].Text = "   ";
            }
        }
        #endregion

        #region Properties
        public MUI mui { get; set; }
        public DisplayLang dl { get; set; }
        public NumericUpDown[] Nums = new NumericUpDown[11];
        public TTInfo TroopsAtHome { get; set; }
        public RaidQueue Return { get; set; }
        public TVillage Village { get; set; }
        #endregion

        #region Methods
        private void RaidOptForm_Load(object sender, EventArgs e)
        {
            mui.RefreshLanguage(this);
            SuspendLayout();
            if (this.Return != null)
            {
                this.LoadTroopIcons();
                if (this.Return.Troops != null)
                {
                    for (int i = 0; i < this.Return.Troops.Length; i++)
                    {
                        this.txtTroops[i].Text = this.Return.Troops[i].ToString();
                    }
                }

                if (this.TroopsAtHome != null)
                {
                    for (int i = 0; i < this.TroopsAtHome.Troops.Length; i++)
                    {
                        this.lblMaxTroops[i].Text = string.Format(
                            "({0})",
                            this.TroopsAtHome.Troops[i]);
                    }
                }

                this.rdbRaidTypes[this.Return.RaidType - RaidType.Reinforce].Select();
                this.rdbSpyOptions[this.Return.SpyOption - SpyOption.Resource].Select();
                this.nudCount.Value = this.Return.MaxCount;
                this.nudMaxSlots.Value = this.Return.MaxSlots;
                this.ckbMultipleRaids.Checked = this.Return.MultipeRaids;
                this.tbDesc.Text = this.Return.Description;

                if (this.Return.Targets != null)
                {
                    foreach (TPoint village in this.Return.Targets)
                    {
                        this.lstTargets.Items.Add(village);
                    }
                }
            }

            ResumeLayout();
            
            this.Village.UpCall.OnRaidTargetFoundLog += 
            	new EventHandler<RaidTargetFoundLogArgs>(tr_OnRaidTargetFoundLog);
        }

        private void CreateControlArrays()
        {
            this.txtTroops = new TextBox[]
            {
                this.txtT1,
                this.txtT2,
                this.txtT3,
                this.txtT4,
                this.txtT5,
                this.txtT6,
                this.txtT7,
                this.txtT8,
                this.txtT9,
                this.txtT10,
                this.txtT11,
            };

            this.lblTroops = new Label[]
            {
                this.lblT1,
                this.lblT2,
                this.lblT3,
                this.lblT4,
                this.lblT5,
                this.lblT6,
                this.lblT7,
                this.lblT8,
                this.lblT9,
                this.lblT10,
                this.lblT11,
            };

            this.lblMaxTroops = new Label[]
            {
                this.lblT1Max,
                this.lblT2Max,
                this.lblT3Max,
                this.lblT4Max,
                this.lblT5Max,
                this.lblT6Max,
                this.lblT7Max,
                this.lblT8Max,
                this.lblT9Max,
                this.lblT10Max,
                this.lblT11Max,
            };

            this.rdbRaidTypes = new RadioButton[]
            {
                this.rdbTypeReinforce,
                this.rdbAttackNormal,
                this.rdbAttackRaid,
            };

            this.rdbSpyOptions = new RadioButton[]
            {
                this.rdbSpyResource,
                this.rdbSpyDefense,
            };
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            RaidQueue task = new RaidQueue();
            task.Troops = new int[11];
            for (int i = 0; i < 11; i++)
            {
                Int32.TryParse(this.txtTroops[i].Text, out task.Troops[i]);
            }

            task.Targets = new List<TPoint>();
            foreach (object village in this.lstTargets.Items)
            {
                task.Targets.Add((TPoint)village);
            }

            task.RaidType = RaidType.Reinforce + this.SelectedRadioButtonIndex(this.rdbRaidTypes);
            task.SpyOption = SpyOption.Resource + this.SelectedRadioButtonIndex(this.rdbSpyOptions);
            task.MaxCount = Convert.ToInt32(this.nudCount.Value);
            task.MaxSlots = Convert.ToInt32(this.nudMaxSlots.Value);
            task.MultipeRaids = this.ckbMultipleRaids.Checked;
            task.Description = tbDesc.Text;

            this.Return = task;
        }

        private int SelectedRadioButtonIndex(RadioButton[] radioButtons)
        {
            for (int i = 0; i < radioButtons.Length; i++)
            {
                if (radioButtons[i].Checked)
                {
                    return i;
                }
            }

            return -1;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            int x, y;
            if (!Int32.TryParse(this.txtX.Text, out x))
            {
                return;
            }

            if (!Int32.TryParse(this.txtY.Text, out y))
            {
                return;
            }

            TPoint village = new TPoint(x, y);
            if (this.lstTargets.SelectedIndex < 0)
            {
                this.lstTargets.Items.Add(village);
            }
            else
            {
                this.lstTargets.Items.Insert(this.lstTargets.SelectedIndex, village);
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (this.lstTargets.SelectedIndex < 0)
            {
                return;
            }

            TPoint village = (TPoint)this.lstTargets.SelectedItem;
            this.txtX.Text = village.X.ToString();
            this.txtY.Text = village.Y.ToString();

            this.lstTargets.Items.RemoveAt(this.lstTargets.SelectedIndex);
        }

        private void lblMaxTroops_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.lblMaxTroops.Length; i++)
            {
                if (this.lblMaxTroops[i] == sender)
                {
                    string text = this.lblMaxTroops[i].Text;
                    this.txtTroops[i].Text = text.Substring(1, text.Length - 2);
                }
            }
        }

        private void btnSort_Click(object sender, EventArgs e)
        {
            if (this.Village == null)
            {
                return;
            }

            TPoint origin = this.Village.Coord;
            int count = this.lstTargets.Items.Count;
            double[] dist = new double[count];
            for (int i = 0; i < count; i++)
            {
                dist[i] = origin * (TPoint)this.lstTargets.Items[i];
            }

            for (int i = 0; i < count - 1; i++)
            {
                for (int j = i + 1; j < count; j++)
                {
                    if (dist[i] > dist[j])
                    {
                        double tempDist = dist[i];
                        TPoint tempVillage = (TPoint) this.lstTargets.Items[i];

                        dist[i] = dist[j];
                        this.lstTargets.Items[i] = this.lstTargets.Items[j];

                        dist[j] = tempDist;
                        this.lstTargets.Items[j] = tempVillage;
                    }
                }
            }
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            foreach (TPoint village in this.lstTargets.Items)
            {
                sb.AppendLine(village.ToString());
            }

            if (sb.Length > 0)
            {
                Clipboard.SetText(sb.ToString());
            }
        }

        private void btnPaste_Click(object sender, EventArgs e)
        {
            string clipboardContent = Clipboard.GetText();
            string[] lines = clipboardContent.Split('\n');
            foreach (string line in lines)
            {
                if (line.StartsWith(";"))
                {
                    continue;
                }

                TPoint village = TPoint.FromString(line);
                if (!village.IsEmpty)
                {
                    this.lstTargets.Items.Add(village);
                }
            }
        }
        #endregion
		
        private bool bIsInSearching = false;
		void Button1Click(object sender, EventArgs e)
		{
			bIsInSearching = true;
			int Range, Population;
			try 
			{
				Range = Convert.ToInt32(this.NUD_Range.Value);
				Population = Convert.ToInt32(this.NUDPopulation.Value); 
			}
            catch
            {
            	return;
            }
			this.Village.UpCall.FindRaidTargets(this.Village.ID, Range, Population);
		}
		
		void Button2Click(object sender, EventArgs e)
		{
			
		}
		
		private delegate void RaidTargetLogEvent_d(string e);
		void tr_OnRaidTargetFoundLog(object sender, RaidTargetFoundLogArgs e)
        {
            try
            {
                Invoke(new RaidTargetLogEvent_d(DisplayRaidTargetFoundLog), 
            	       new object[] { e.arg_log });
            }
            catch (Exception)
            { }
        }
		
		void DisplayRaidTargetFoundLog(string log)
		{
			textBoxSearchingLog.AppendText(log + "\r\n");
		}
    }
}
