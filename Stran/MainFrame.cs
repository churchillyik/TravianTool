/*
 * The contents of this file are subject to the Mozilla Public License
 * Version 1.1 (the "License"); you may not use this file except in
 * compliance with the License. You may obtain a copy of the License at
 * http://www.mozilla.org/MPL/
 * 
 * Software distributed under the License is distributed on an "AS IS"
 * basis, WITHOUT WARRANTY OF ANY KIND, either express or implied. See the
 * License for the specific language governing rights and limitations
 * under the License.
 * 
 * The Initial Developer of the Original Code is [MeteorRain <msg7086@gmail.com>].
 * Copyright (C) MeteorRain 2007, 2008. All Rights Reserved.
 * Contributor(s): [MeteorRain].
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using libTravian;
using System.Threading;
using System.Net;
using Stran.DockingPanel;
using System.IO;
using WeifenLuo.WinFormsUI.Docking;

namespace Stran
{
    public partial class MainFrame : UserControl
    {
        public TLoginInfo LoginInfo { get; set; }
        public TabPage UpTP { get; set; }
        public Data TravianData;

        private BuildingList m_buildinglist = new BuildingList();
        private InBuildingList m_inbuildinglist = new InBuildingList();
        private QueueList m_queuelist = new QueueList();
        private ResearchStatus m_researchstatus = new ResearchStatus();
        private ResourceShow m_resourceshow = new ResourceShow();
        private TransferStatus m_transferstatus = new TransferStatus();
        private VillageList m_villagelist = new VillageList();
        private TroopInfoList m_troopinfolist = new TroopInfoList();

        private delegate void StatusEvent_d(object sender, Travian.StatusChanged e);
        private delegate void LogEvent_d(TDebugInfo e);
        private delegate void Void_d();

        private static Color[] ResColor = new Color[] { Color.ForestGreen, Color.Chocolate, Color.SlateGray, Color.Gold };
        private static readonly Color RedBGColor = Color.FromArgb(255, 192, 192);
        private static readonly Color YellowBGColor = Color.FromArgb(255, 255, 192);
        public static string[] typelist = new string[] { "资源田", "建筑", "拆除", "攻击", "防御", "研究", "活动", "运输", "平仓" };

        private static object QueueLock = new object();

        private DisplayLang dl;
        public MUI mui { get; set; }

        ResourceLabel[] reslabel;
        Travian tr = null;
        int QueueCount = 0;
        int SelectVillage = 0;

        public MainFrame()
        {
            InitializeComponent();
            reslabel = new ResourceLabel[] { m_resourceshow.resourceLabel1, m_resourceshow.resourceLabel2, m_resourceshow.resourceLabel3, m_resourceshow.resourceLabel4 };
            //AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(UnhandledException);
            //Thread.GetDomain().UnhandledException += new UnhandledExceptionEventHandler(UnhandledException);
            for (int i = 0; i < 7; i++)
            {
                var lvi = m_inbuildinglist.listViewInBuilding.Items.Add(typelist[i]);
                lvi.SubItems.Add("");
            }
            CMBEnableCoin_CheckedChanged(null, null);
        }

        public void Login()
        {
            if (tr != null)
                tr = null;
            TravianData = DB.Instance.RestoreData(LoginInfo.GetKey());
            if (TravianData == null)
                TravianData = new Data();
            TravianData.Username = LoginInfo.Username;
            TravianData.Password = LoginInfo.Password;
            TravianData.Tribe = LoginInfo.Tribe;
            TravianData.Server = LoginInfo.Server;
            if (MainForm.Options.ContainsKey("proxy"))
            {
                string proxy = MainForm.Options["proxy"];
                TravianData.Proxy = new WebProxy(proxy);
            }
            tr = DB.Instance.RestoreTravian(LoginInfo.Server);
            if (tr == null)
                tr = new Travian(TravianData, MainForm.Options);
            else
            {
                tr.TD = TravianData;
                tr.LoadOptions(MainForm.Options);
            }
            foreach (var v in TravianData.Villages)
            {
                v.Value.UpCall = tr;
                foreach (var q in v.Value.Queue)
                    q.UpCall = tr;
            }
            dl = new DisplayLang(LoginInfo.Language);
            DisplayLang.Instance = dl;
            tr.StatusUpdate += new EventHandler<Travian.StatusChanged>(tr_StatusUpdate);
            tr.OnError += new EventHandler<LogArgs>(tr_OnError);

            m_villagelist.listViewVillage.Items.Clear();
            m_buildinglist.listViewBuilding.Items.Clear();
            tr.CachedFetchVillages();
            UpTP.Text = string.Format("{0} @ {1}", LoginInfo.Username, LoginInfo.Server.Replace("travian.", ""));
        }

        public void RefreshLanguage()
        {
            foreach (Control x in this.Controls)
            {
                if (x.Tag is string)
                    x.Text = mui._(x.Tag as string);
            }
        }

        void tr_OnError(object sender, LogArgs e)
        {
            try
            {
                Invoke(new LogEvent_d(DebugWriteError), new object[] { e.DebugInfo });
            }
            catch (Exception)
            { }
        }

        void tr_StatusUpdate(object sender, Travian.StatusChanged e)
        {
            try
            {
                Invoke(new StatusEvent_d(Local_StatusUpdate), new object[] { sender, e });
            }
            catch (Exception)
            { }
        }

        void Local_StatusUpdate(object sender, Travian.StatusChanged e)
        {
            if (e.ChangedData == Travian.ChangedType.Villages)
            {
                if (e.VillageID == -1)
                {
                    MessageBox.Show("Login failed.", "Stran");
                    return;
                }
                if (LoginInfo.Tribe == 0 && LoginInfo.Tribe != TravianData.Tribe)
                    LoginInfo.Tribe = TravianData.Tribe;
				if (m_villagelist.listViewVillage.Items.Count != TravianData.Villages.Count)
					m_villagelist.listViewVillage.Items.Clear();
				else
				{
					bool newv = false;
					for (int j = 0; j < m_villagelist.listViewVillage.Items.Count - 1; j++)
					{
						int m = Convert.ToInt32(m_villagelist.listViewVillage.Items[j].SubItems[0].Text);
						int n = TravianData.Villages[m].Sort;
						if (n != j)
							newv = true;
					}
					if (newv == true)
						m_villagelist.listViewVillage.Items.Clear();
				}
				
				int index = 0;
				if (m_villagelist.listViewVillage.SelectedIndices.Count == 1 && m_villagelist.listViewVillage.Items.Count != 0)
					index = m_villagelist.listViewVillage.SelectedIndices[0];
                int i = 0;
                List<int> f = new List<int>();
                foreach (ListViewItem x in m_villagelist.listViewVillage.Items)
                {
                    f.Add(Convert.ToInt32(x.SubItems[0].Text));
                }

                foreach (var x in TravianData.Villages)
                {
                    if (f.Contains(x.Key))
                    {
                        var xkey = m_villagelist.listViewVillage.Items[f.IndexOf(x.Value.ID)];
                        if (xkey.SubItems[2].Text != x.Value.Name)
                            xkey.SubItems[2].Text = x.Value.Name;
						xkey.BackColor = SystemColors.Window;
						if(TravianData.Villages[x.Key].Troop.GetTroopsIsAttackMe == true)
							xkey.BackColor = Color.Salmon;
						m_villagelist.listViewVillage.Items[index].Selected = true;
                    }
                    else
                    {
                        var lvi = m_villagelist.listViewVillage.Items.Add(x.Value.ID.ToString());
                        string qcount = x.Value.GetStatus();
                        lvi.SubItems.Add(qcount);
                        lvi.SubItems.Add(x.Value.Name);
                        lvi.SubItems.Add(x.Value.Coord.ToString());
                        lvi.SubItems.Add("");
                        lvi.BackColor = SystemColors.Window;
                        if(TravianData.Villages[x.Key].Troop.GetTroopsIsAttackMe == true)
							lvi.BackColor = Color.Salmon;
                        if (e.VillageID == x.Key)
                            i = m_villagelist.listViewVillage.Items.Count - 1;
                    }
                }
                if (m_villagelist.listViewVillage.SelectedIndices.Count <= 0 && i >= 0)
                    m_villagelist.listViewVillage.Items[i].Selected = true;
            }
            else if (e.ChangedData == Travian.ChangedType.Stop)
            {
                m_resourceshow.label5.BackColor = e.Param == 0 ? Color.Ivory : RedBGColor;
            }
            else if (e.ChangedData == Travian.ChangedType.Queue && e.Param == -1)
            {
                //RestoreQueue(e.VillageID);

            }
            else if (e.ChangedData == Travian.ChangedType.PageCount)
            {
                lock (MainForm.Instance)
                {
                    MainForm.Instance.notifyIcon1.Icon = Properties.Resources.full;
                    MainForm.Instance.timerIcon.Enabled = false;
                    MainForm.Instance.timerIcon.Enabled = true;
                }
            }
            else if (e.VillageID == SelectVillage)
                switch (e.ChangedData)
                {
                    case Travian.ChangedType.Buildings:
                        DisplayBuildings();
                        DisplayResource();
                        DisplayInBuilding();
                        RefreshBuildings();
                        break;
                    case Travian.ChangedType.Research:
                        DisplayUpgrade();
                        break;
                    case Travian.ChangedType.Queue:
                        RefreshQueue(e.Param);
                        break;
                    case Travian.ChangedType.OasisFound:
                        DisplayOasisFound();
                        break;
                }
        }

        private void RefreshBuildings()
        {
            if (!TravianData.Villages.ContainsKey(SelectVillage))
                return;
            Color c_color;
            string c_text;
            var CV = TravianData.Villages[SelectVillage];
            if (CV.isBuildingInitialized == 2)
            {
                foreach (ListViewItem lvi in m_buildinglist.listViewBuilding.Items)
                {
                    int Bid = Convert.ToInt32(lvi.SubItems[0].Text);
                    if (!CV.Buildings.ContainsKey(Bid))
                        continue;
                    var y = CV.Buildings[Bid];
                    if (y == null)
                        continue;
                    if (!Buildings.CheckLevelFull(y.Gid, y.Level, CV.isCapital))
                    {
                        var timecost = CV.TimeCost(Buildings.Cost(y.Gid, y.Level + 1));
                        if (timecost > 0)
                        {
                            c_color = RedBGColor;
                            c_text = TimeToString(timecost);
                        }
                        else
                        {
                            c_color = YellowBGColor;
                            c_text = mui._("available");
                        }
                        if (lvi.SubItems[1].BackColor != c_color)
                            lvi.SubItems[1].BackColor = lvi.SubItems[2].BackColor = c_color;
                        if (lvi.SubItems[2].Text != c_text)
                            lvi.SubItems[2].Text = c_text;
                    }
                }
            }
        }
        
        private void DisplayBuildings()
        {
            if (!TravianData.Villages.ContainsKey(SelectVillage))
                return;
            var CV = TravianData.Villages[SelectVillage];

            if (CV.isBuildingInitialized == 2)
            {
                m_buildinglist.listViewBuilding.Items.Clear();
                // Parse queue onto building
                int[] TL = new int[45]; // Less than 0 => CurrLevel-TL[], Greater than 0 => TL[], 0 => Destroy

                foreach (var Q in CV.Queue)
                    if (Q is BuildingQueue) // Not AI
                    {
                        BuildingQueue q = Q as BuildingQueue;
                        if (TL[q.Bid] == -1024)
                            continue;
                        else if (q.TargetLevel == 0)
                            if (TL[q.Bid] > 0)
                                TL[q.Bid]++;
                            else
                                TL[q.Bid]--;
                        else // build to level
                            TL[q.Bid] = q.TargetLevel;
                    }
                    else if (Q is DestroyQueue)
                    {
                        DestroyQueue q = Q as DestroyQueue;
                        TL[q.Bid] = -1024;
                    }
                SortedDictionary<int, ListViewItem> lvid = new SortedDictionary<int, ListViewItem>();
                foreach (var x in CV.Buildings)
                {
                    if (x.Value.Gid > 4)
                        break;
                    if (!Buildings.CheckLevelFull(x.Value.Gid, x.Value.Level, CV.isCapital))
                    {
                        var lvi = new ListViewItem(x.Key.ToString());
                        lvi.UseItemStyleForSubItems = false;
                        if (TL[x.Key] < 0 && TL[x.Key] > -1000)
                            TL[x.Key] = x.Value.Level - TL[x.Key];
                        string text;
                        if (TL[x.Key] == 0)
                            text = string.Format("{0} {1}", dl.GetGidLang(x.Value.Gid), x.Value.Level);
                        //destroy
                        else
                        {
                            if (TL[x.Key] == -1024)
                                TL[x.Key] = 0;
                            else if (TL[x.Key] < 0)
                                TL[x.Key] = x.Value.Level - TL[x.Key];
                            text = string.Format("{0} {1} => {2}", dl.GetGidLang(x.Value.Gid), x.Value.Level, TL[x.Key]);
                        }
                        if (x.Value.InBuilding)
                            text += " <--";
                        lvi.SubItems.Add(text);
                        lvi.SubItems.Add("");
                        if (x.Value.Gid <= 4)
                        {
                            lvi.SubItems[0].BackColor = ResColor[x.Value.Gid - 1];
                            lvi.SubItems[0].ForeColor = Color.White;
                        }
                        lvid.Add((x.Value.Gid << 16) + (x.Value.Level << 8) + x.Key, lvi);
                    }
                }
                foreach (var x in lvid)
                    m_buildinglist.listViewBuilding.Items.Add(x.Value);
                foreach (var x in CV.Buildings)
                {
                    if (x.Value.Gid <= 4)
                        continue;
                    var lvi = m_buildinglist.listViewBuilding.Items.Add(x.Key.ToString());
                    lvi.UseItemStyleForSubItems = false;
                    if (TL[x.Key] < 0 && TL[x.Key] > -1000)
                        TL[x.Key] = x.Value.Level - TL[x.Key];
                    string text;
                    if (TL[x.Key] == 0)
                        text = string.Format("{0} {1}", dl.GetGidLang(x.Value.Gid), x.Value.Level);
                    //destroy
                    else
                    {
                        if (TL[x.Key] == -1024)
                            TL[x.Key] = 0;
                        else if (TL[x.Key] < 0)
                            TL[x.Key] = x.Value.Level - TL[x.Key];
                        text = string.Format("{0} {1} => {2}", dl.GetGidLang(x.Value.Gid), x.Value.Level, TL[x.Key]);
                    }
                    if (x.Value.InBuilding)
                        text += " <--";
                    lvi.SubItems.Add(text);
                    lvi.SubItems.Add("");
                }
            }
        }
        
        private void RefreshQueue(int QueueID)
        {
            if (!TravianData.Villages.ContainsKey(SelectVillage))
                return;
            var CV = TravianData.Villages[SelectVillage];
            if (CV.isBuildingInitialized == 2)
            {
                lock (QueueLock)
                {
                    if (CV.Queue.Count == m_queuelist.listViewQueue.Items.Count - 1)
                        m_queuelist.listViewQueue.Items.RemoveAt(QueueID);
                }
            }
        }

        /// <summary>
        /// Update Queue list view
        /// </summary>
        private void DisplayQueue()
        {
            if (!TravianData.Villages.ContainsKey(SelectVillage))
                return;

            var CV = TravianData.Villages[SelectVillage];
            lock (QueueLock)
            {
                if (CV.Queue.Count != m_queuelist.listViewQueue.Items.Count)
                {
                    m_queuelist.listViewQueue.Items.Clear();
                    foreach (var x in CV.Queue)
                    {
                        ListViewItem lvi;
                        lvi = m_queuelist.listViewQueue.Items.Add(x.GetType().Name);

                        lvi.SubItems.Add(x.Title);
                        lvi.SubItems.Add(x.Status);
                        lvi.SubItems.Add("");//tr.GetDelay(SelectVillage, x).ToString());
                    }
                }
                else
                {
                    List<int> status = new List<int>();
                    for (int i = 0; i < CV.Queue.Count; i++)
                    {
                        var x = CV.Queue[i];
                        var lvi = m_queuelist.listViewQueue.Items[i];
                        if (lvi.SubItems[2].Text != x.Status)
                            lvi.SubItems[2].Text = x.Status;

                        int ntype = 0;
                        if (x is BuildingQueue)
                        {
                            ntype = (x as BuildingQueue).Bid > 19 && TravianData.isRomans ? 1 : 0;
                        }
                        else
                            ntype = x.QueueGUID;

                        string delayStr = String.Empty;
                        if (x.Paused)
                        {
                            delayStr = "||";
                        }
                        else if (!status.Contains(ntype))
                        {
                            int n = x.CountDown;
                            if (n > 0)
                            {
                                delayStr = this.TimeToString(n);
                            }
                            if (ntype < 7)
                                status.Add(ntype);
                        }

                        if (lvi.SubItems[3].Text != delayStr)
                        {
                            lvi.SubItems[3].Text = delayStr;
                        }
                    }
                }
            }
        }

        private void DisplayInBuilding()
        {
            if (!TravianData.Villages.ContainsKey(SelectVillage))
                return;
            string c_text;
            var CV = TravianData.Villages[SelectVillage];
            if (CV.isBuildingInitialized == 2)
            {
                for (int i = 0; i < CV.InBuilding.Length; i++)
                {
                    var x = TravianData.isRomans || i >= 2 ? CV.InBuilding[i] : CV.InBuilding[0];
                    if (x == null || x.FinishTime < DateTime.Now || !TravianData.isRomans && i < 2 && i == (x.Gid > 5 ? 0 : 1))
                        c_text = "";
                    else
                    {
                        TimeSpan ts = x.FinishTime.Subtract(DateTime.Now);

                        if (i < 3)
                        {
                            c_text = dl.GetGidLang(x.Gid);
                        }
                        else if (i < 6)
                            c_text = dl.GetAidLang(TravianData.Tribe, x.ABid);
                        else
                            c_text = "";
                        if (i != 6)
                        {
	                        c_text += string.Format(" {0} {1:0}:{2:00}:{3:00} -> {4}",
	                            x.Level,
	                            Math.Floor(ts.TotalHours), ts.Minutes, ts.Seconds,
	                            x.FinishTime.ToLongTimeString());
                        }
                        else
                        {
                        	c_text = string.Format(" {0} {1:0}:{2:00}:{3:00} -> {4}",
	                            x.PartyDesc,
	                            Math.Floor(ts.TotalHours), ts.Minutes, ts.Seconds,
	                            x.FinishTime.ToLongTimeString());
                        }
                    }

                    if (m_inbuildinglist.listViewInBuilding.Items[i].SubItems[1].Text != c_text)
                    {
                        m_inbuildinglist.listViewInBuilding.Items[i].SubItems[1].Text = c_text;
                    }
                }
            }
        }
        
        private void DisplayResource()
        {
            if (!TravianData.Villages.ContainsKey(SelectVillage))
                return;
            var CVRes = TravianData.Villages[SelectVillage].Resource;
            if (TravianData.Villages[SelectVillage].isBuildingInitialized == 2)
                for (int i = 0; i < 4; i++)
                {
                    reslabel[i].Display(CVRes[i]);
                }
        }
        
        private void DisplayUpgrade()
        {
            if (!TravianData.Villages.ContainsKey(SelectVillage))
                return;
            var CV = TravianData.Villages[SelectVillage];
            m_researchstatus.listViewUpgrade.Items.Clear();
            if (CV.isUpgradeInitialized != 2)
                return;
            foreach (var x in CV.Upgrades)
            {
                var lvi = m_researchstatus.listViewUpgrade.Items.Add(dl.GetAidLang(TravianData.Tribe, x.Key));
                lvi.UseItemStyleForSubItems = false;
                #region Research
                if (x.Value.Researched)
                {
                    lvi.SubItems.Add(mui._("finished"));
                    lvi.SubItems[1].BackColor = Color.White;
                }
                else if (x.Value.CanResearch)
                {
                    if (x.Value.InUpgrading)
                    {
                        lvi.SubItems.Add(mui._("upgrading"));
                        lvi.SubItems[1].BackColor = Color.White;
                    }
                    else
                    {
                        int TimeCost = CV.TimeCost(Buildings.ResearchCost[(TravianData.Tribe - 1) * 10 + x.Key]);
                        if (TimeCost > 0)
                        {
                            lvi.SubItems.Add(TimeToString(TimeCost));
                            lvi.SubItems[1].BackColor = RedBGColor;
                        }
                        else
                        {
                            lvi.SubItems.Add(mui._("available"));
                            lvi.SubItems[1].BackColor = YellowBGColor;
                        }
                    }
                }
                else
                {
                    lvi.SubItems.Add(mui._("notavailable"));
                    lvi.SubItems[1].BackColor = Color.White;
                    lvi.SubItems[1].ForeColor = Color.DarkRed;
                }
                #endregion
                #region Attack
                if (x.Value.AttackLevel >= 20)
                {
                    lvi.SubItems.Add(mui._("finished"));
                    lvi.SubItems[2].BackColor = Color.White;
                }
                else if (x.Value.Researched && x.Value.AttackLevel < CV.BlacksmithLevel && x.Value.AttackLevel >= 0)
                {
                    int TimeCost = CV.TimeCost(Buildings.UpCost[(TravianData.Tribe - 1) * 10 + x.Key][x.Value.AttackLevel]);
                    if (TimeCost > 0)
                    {
                        lvi.SubItems.Add(TimeToString(TimeCost));
                        lvi.SubItems[2].BackColor = RedBGColor;
                    }
                    else
                    {
                        lvi.SubItems.Add(mui._("available"));
                        lvi.SubItems[2].BackColor = YellowBGColor;
                    }
                }
                else
                {
                    lvi.SubItems.Add(mui._("notavailable"));
                    lvi.SubItems[2].BackColor = Color.White;
                    lvi.SubItems[2].ForeColor = Color.DarkRed;
                }
                if (x.Value.AttackLevel >= 0 && x.Value.AttackLevel < 20)
                    lvi.SubItems[2].Text = x.Value.AttackLevel.ToString() + " " + lvi.SubItems[2].Text;
                #endregion
                #region Defense
                if (x.Value.DefenceLevel >= 20)
                {
                    lvi.SubItems.Add(mui._("finished"));
                    lvi.SubItems[3].BackColor = Color.White;
                }
                else if (x.Value.Researched && x.Value.DefenceLevel < CV.ArmouryLevel && x.Value.DefenceLevel >= 0)
                {
                    int TimeCost = CV.TimeCost(Buildings.UpCost[(TravianData.Tribe - 1) * 10 + x.Key][x.Value.DefenceLevel]);
                    if (TimeCost > 0)
                    {
                        lvi.SubItems.Add(TimeToString(TimeCost));
                        lvi.SubItems[3].BackColor = RedBGColor;
                    }
                    else
                    {
                        lvi.SubItems.Add(mui._("available"));
                        lvi.SubItems[3].BackColor = YellowBGColor;
                    }
                }
                else
                {
                    lvi.SubItems.Add(mui._("notavailable"));
                    lvi.SubItems[3].BackColor = Color.White;
                    lvi.SubItems[3].ForeColor = Color.DarkRed;
                }
                if (x.Value.DefenceLevel >= 0 && x.Value.DefenceLevel < 20)
                    lvi.SubItems[3].Text = x.Value.DefenceLevel.ToString() + " " + lvi.SubItems[3].Text;
                #endregion
            }

            if (this.m_researchstatus.listViewUpgrade.Items.Count > 9)
            {
                this.m_researchstatus.listViewUpgrade.Items.RemoveAt(9);
            }
        }
        
        private void DisplayMarket()
        {
            if (!TravianData.Villages.ContainsKey(SelectVillage))
                return;
            var CV = TravianData.Villages[SelectVillage];
            if (CV.isBuildingInitialized != 2)
                return;
            m_transferstatus.listViewMarket.Items.Clear();
            m_transferstatus.listViewMarket.SuspendLayout();
            foreach (var x in CV.Market.MarketInfo)
            {
                var lvi = m_transferstatus.listViewMarket.Items.Add(TimeToString(Convert.ToInt32(x.FinishTime.Subtract(DateTime.Now).TotalSeconds) + 5));
                lvi.SubItems.Add(x.CarryAmount.ToString());
                lvi.SubItems.Add(x.VillageName);
                lvi.SubItems.Add(x.Coord.ToString());
                lvi.SubItems.Add(x.MType.ToString());
            }
            m_transferstatus.listViewMarket.ResumeLayout();
        }
        
        private void DisplayTroop()
        {
            if (!TravianData.Villages.ContainsKey(SelectVillage))
                return;
            var CV = TravianData.Villages[SelectVillage];
            if (CV.isTroopInitialized != 2)
                return;
            m_troopinfolist.listViewTroop.Items.Clear();
            m_troopinfolist.listViewTroop.SuspendLayout();
            foreach (var x in CV.Troop.Troops)
            {
                var lvi = m_troopinfolist.listViewTroop.Items.Add("-");
                if (x.FinishTime != DateTime.MinValue)
                {
                	TimeSpan ts = new TimeSpan((long)x.FinishTime.Subtract(DateTime.Now).TotalSeconds * 10000000 + 5);
                	if (ts.Days == 0)
                	{
                		lvi.Text = string.Format(
                			"{0}:{1}:{2}", ts.Hours, ts.Minutes, ts.Seconds);
                	}
                	else
                	{
	            		lvi.Text = string.Format(
                			"{0}天{1}:{2}:{3}", ts.Days, ts.Hours, ts.Minutes, ts.Seconds);
                	}
                	
                }
                lvi.SubItems.Add(x.FriendlyName);
                lvi.SubItems.Add(x.VillageName);
                lvi.SubItems.Add(x.TroopType.ToString());
            }
            m_troopinfolist.listViewTroop.ResumeLayout();
        }
        
        private void DisplayOasisFound()
        {
        	if (!TravianData.Villages.ContainsKey(SelectVillage))
                return;
        	var CV = TravianData.Villages[SelectVillage];
            if (CV.isOasisFoundInitialized != 2)
                return;
            
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("这是已经找到的绿洲之情况：");
        	sb.AppendLine("X轴坐标\tY轴坐标\t绿洲加成");
        	
        	foreach (var o in CV.OasisInfo)
        	{
        		TOasisInfo oasis = o as TOasisInfo;
        		sb.AppendLine("");
        		sb.Append(oasis.axis_x);
        		sb.Append("\t");
        		sb.Append(oasis.axis_y);
        		sb.Append("\t");
        		sb.Append(oasis.addon);
        		sb.Append("%");
        	}

            MsgBox mb = new MsgBox() { message = sb.ToString() };
            mb.ShowDialog();
        }

        private string TimeToString(long timecost)
        {
            if (timecost >= 86400)
                return "∞";
            TimeSpan ts = new TimeSpan(timecost * 10000000);
            return ts.ToString();
        }

        public void listViewVillage_Changed(object sender, EventArgs e)
        {
            if (tr == null)
                return;
            if (m_villagelist.listViewVillage.SelectedIndices.Count == 1)
                TravianData.ActiveDid = SelectVillage = Convert.ToInt32(m_villagelist.listViewVillage.SelectedItems[0].Text);
            if (TravianData.Villages == null)
                return;
            if (!TravianData.Villages.ContainsKey(SelectVillage))
                return;
            m_buildinglist.listViewBuilding.Items.Clear();
            m_queuelist.listViewQueue.Items.Clear();
            foreach (var l in reslabel)
                l.Clear();
            var CV = TravianData.Villages[SelectVillage];
            m_resourceshow.label5.Text = string.Format("{0} ({1}|{2})", CV.Name, CV.Coord.X, CV.Coord.Y);
            
            if (CV.isVillageInitialized == 0)
            {
            	CV.InitializeVillage();
            }
            
            DisplayBuildings();
            DisplayResource();
            DisplayInBuilding();
            RefreshBuildings();
            DisplayQueue();
            DisplayUpgrade();
            DisplayMarket();
            DisplayTroop();
        }

        public void listViewVillage_Click(object sender, EventArgs e)
        {
            if (tr == null)
                return;
            if (SelectVillage < 0)
                return;
            if (!TravianData.Villages.ContainsKey(SelectVillage))
                return;
            listViewVillage_Changed(sender, e);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (tr != null)
            {
                DisplayInBuilding();
                DisplayResource();
                RefreshBuildings();
                DisplayQueue();
                DisplayMarket();
                DisplayTroop();
                tr.Tick();
                int QCount = 0;
                foreach (var x in TravianData.Villages)
                    if (x.Value.isBuildingInitialized > 1)
                        QCount += x.Value.Queue.Count;
                if (QueueCount != QCount)
                {
                    QueueCount = QCount;
                    UpTP.Text = string.Format("{0} @ {1} ({2})", LoginInfo.Username, LoginInfo.Server.Replace("travian.", ""), QueueCount);
                }
            }

            foreach (ListViewItem x in m_villagelist.listViewVillage.Items)
            {
                int index = Convert.ToInt32(x.SubItems[0].Text);
                if (TravianData.Villages.ContainsKey(index))
                {
                    string qcount = this.TravianData.Villages[index].GetStatus();
                    if (TravianData.Villages[index].isBuildingInitialized != 2)
                        qcount += " -";
                    if (x.SubItems[1].Text != qcount)
                        x.SubItems[1].Text = qcount;
                    if (TravianData.Villages[index].isBuildingInitialized == 2)
                    {
                        string resstat = "";
                        foreach (var res in TravianData.Villages[index].Resource)
                            if (res.isFull)
                                resstat += "F";
                            else
                                resstat += res.CurrAmount * 10 / res.Capacity;
                        x.SubItems[4].Text = resstat;
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (tr == null ||
                QueueCount == 0 ||
                MessageBox.Show(mui._("reallyclosetext"), mui._("reallyclosecap"),
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                (UpTP.Parent as TabControl).TabPages.Remove(UpTP);
                timer1.Enabled = false;
                UpTP.Dispose();
            }
        }

        private void DebugWriteError(TDebugInfo DB)
        {
            string str = string.Format("[{2,-3}{0} {1}]{3,20}@{4,-35}:{5,-3} {6}",
                DB.Time.Day,
                DB.Time.ToLongTimeString(),
                DB.Level.ToString(),
                DB.MethodName,
                DB.Filename.Substring(13),
                DB.Line,
                DB.Text);
            if (checkBoxVerbose.Checked || DB.Level != DebugLevel.II)
                textBox1.AppendText(str + "\r\n");
            LastDebug.Text = str;
        }

        string GetStyleFilename()
        {
            string fn = "style\\" + LoginInfo.GetKey() + "!style.xml";
            return fn;
        }

        void UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = e.ExceptionObject as Exception;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("请您复制错误信息并稍后张贴到论坛，谢谢。");
            sb.AppendLine("Please copy the error message and post to developers' forum later.");
            sb.AppendLine(DateTime.Now.ToString());
            sb.AppendLine(ex.Message);
            sb.AppendLine(ex.StackTrace);
            sb.AppendLine(MainForm.VERSION);

            foreach (var DB in tr.DebugList)
            {
                sb.AppendFormat("[{0}][{1}]{2} of {3}:{4}\t{5}\r\n",
                    DB.Time.ToString(),
                    DB.Level,
                    DB.MethodName,
                    DB.Filename.Replace(@"H:\app\Stravian\", ""),
                    DB.Line,
                    DB.Text);
            }

            MsgBox fe = new MsgBox() { message = sb.ToString() };
            fe.ShowDialog();
        }

        private void MainFrame_Load(object sender, EventArgs e)
        {
            mui.RefreshLanguage(this);
            //throw new Exception("a");

            m_buildinglist.UpCall =
                m_queuelist.UpCall =
                m_researchstatus.UpCall =
                m_transferstatus.UpCall =
                m_resourceshow.UpCall =
                m_inbuildinglist.UpCall =
                m_villagelist.UpCall =
                m_troopinfolist.UpCall = this;


            string fn = GetStyleFilename();
            if (!File.Exists(fn))
                fn = "style\\default!style.xml";
            SuspendLayout();
            if (File.Exists(fn))
                dockPanel1.LoadFromXml(fn, new DeserializeDockContent(FindDocument));
            else
            {
                m_resourceshow.Show(dockPanel1);
                m_inbuildinglist.Show(dockPanel1);
                m_queuelist.Show(dockPanel1);
                m_buildinglist.Show(dockPanel1);
                m_transferstatus.Show(dockPanel1);
                m_researchstatus.Show(dockPanel1);
                m_villagelist.Show(dockPanel1);
                m_troopinfolist.Show(dockPanel1);
            }
            if (!dockPanel1.Contains(m_troopinfolist))
                m_troopinfolist.Show(dockPanel1);
            m_buildinglist.Activate();
            ResumeLayout();
        }

        private IDockContent FindDocument(string text)
        {
            foreach (var x in new DockContent[] { m_buildinglist, m_inbuildinglist, m_queuelist, m_researchstatus, m_resourceshow, m_transferstatus, m_villagelist })
            {
                if (text == x.GetType().ToString())
                    return x;
            }
            return null;
        }

        #region CMV
        private void CMV_Opening(object sender, CancelEventArgs e)
        {
            bool Enabled = CMVSnapshot.Enabled = CMVRefresh.Enabled = m_villagelist.listViewVillage.SelectedItems.Count == 1;
        }

        private void CMVRefresh_Click(object sender, EventArgs e)
        {
            if (!TravianData.Villages.ContainsKey(SelectVillage))
                return;

            TravianData.Villages[SelectVillage].InitializeVillage();
        }

        /// <summary>
        /// Set village resource low bound for outgoing transportation
        /// </summary>
        private void CMVLowerLimit_Click(object sender, EventArgs e)
        {
            if (!TravianData.Villages.ContainsKey(SelectVillage))
            {
                return;
            }

            TVillage village = this.TravianData.Villages[SelectVillage];
            if (village.isBuildingInitialized != 2)
            {
                return;
            }

            ResourceLimit limit = new ResourceLimit()
            {
                Village = village,
                Description = mui._("lowerlimit"),
                Limit = village.Market.LowerLimit == null ? new TResAmount(0, 0, 0, 0) : village.Market.LowerLimit,
                mui = this.mui
            };

            if (limit.ShowDialog() == DialogResult.OK)
            {
                village.Market.LowerLimit = limit.Return;
                TravianData.Dirty = true;
            }
        }

        /// <summary>
        /// Set village resource upper bound for incoming transportation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CMVUpperLimit_Click(object sender, EventArgs e)
        {
            if (!TravianData.Villages.ContainsKey(SelectVillage))
            {
                return;
            }

            TVillage village = this.TravianData.Villages[SelectVillage];
            if (village.isBuildingInitialized != 2)
            {
                return;
            }

            ResourceLimit limit = new ResourceLimit()
            {
                Village = village,
                Description = mui._("upperlimit"),
                Limit = village.Market.UpperLimit == null ? village.ResourceCapacity : village.Market.UpperLimit,
                mui = this.mui
            };

            if (limit.ShowDialog() == DialogResult.OK)
            {
                village.Market.UpperLimit = limit.Return;
                TravianData.Dirty = true;
            }
        }

        private void CMVSnapshot_Click(object sender, EventArgs e)
        {
            if (!TravianData.Villages.ContainsKey(SelectVillage))
                return;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("这是一份村庄快照。如有需要，请您复制并稍后张贴到论坛，谢谢。");
            sb.AppendLine("This is a snapshot of your village. If necessary, please copy this and post to developers' forum later.");
            sb.AppendLine(TravianData.Villages[SelectVillage].Snapshot());
            MsgBox mb = new MsgBox() { message = sb.ToString() };
            mb.ShowDialog();
        }
        private void CMVSnapAll_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("这是一份村庄快照。如有需要，请您复制并稍后张贴到论坛，谢谢。");
            sb.AppendLine("This is a snapshot of your village. If necessary, please copy this and post to developers' forum later.");
            foreach (var v in TravianData.Villages)
                sb.AppendLine(v.Value.Snapshot());
            MsgBox mb = new MsgBox() { message = sb.ToString() };
            mb.ShowDialog();
        }
        private void CMBNewCap_Click(object sender, EventArgs e)
        {
        	/*
            if (!TravianData.Villages.ContainsKey(SelectVillage))
                return;
            var dr = MessageBox.Show("这将在本程序中设置此村庄为主村。不影响游戏中的主村设定。\r\n\r\n确定设置主村吗？", "注意！", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (dr == DialogResult.OK)
            {
                foreach (var v in TravianData.Villages)
                    v.Value.isCapital = false;
                TravianData.Villages[SelectVillage].isCapital = true;
            }
        	*/
        	var dr = MessageBox.Show("此功能仅能刷新实际主村的位置，无法设定村庄为主村。", "注意！", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
        	if (dr == DialogResult.OK)
        	{
        		tr.PageQuery(0, "dorf1.php");
        	}
        }
        #endregion

        #region CMB
        private void CMBUp_Click(object sender, EventArgs e)
        {
            if (!TravianData.Villages.ContainsKey(SelectVillage))
                return;
            if (m_buildinglist.listViewBuilding.SelectedItems.Count == 0)
                return;
            var CV = TravianData.Villages[SelectVillage];
            if (CV.isBuildingInitialized == 2)
            {
                for (int i = 0; i < m_buildinglist.listViewBuilding.SelectedItems.Count; i++)
                {
                    int temp;
                    if (!int.TryParse(m_buildinglist.listViewBuilding.SelectedItems[i].Text, out temp))
                        continue;
                    int Bid = Convert.ToInt32(m_buildinglist.listViewBuilding.SelectedItems[i].Text);
                    if (Buildings.CheckLevelFull(CV.Buildings[Bid].Gid, CV.Buildings[Bid].Level, CV.isCapital))
                        continue;
                    var Q = new BuildingQueue()
                    {
                        UpCall = tr,
                        VillageID = SelectVillage,
                        Bid = Bid,
                        Gid = CV.Buildings[Bid].Gid,
                    };
                    CV.Queue.Add(Q);
                    lvi(Q);

                    m_buildinglist.listViewBuilding.Items[m_buildinglist.listViewBuilding.SelectedIndices[i]].SubItems[1].Text += "*";
                }
            }
        }
        private void CMBUp2_Click(object sender, EventArgs e)
        {
            CMBUp_Click(sender, e);
            CMBUp_Click(sender, e);
        }
        private void CMBUp5_Click(object sender, EventArgs e)
        {
            CMBUp_Click(sender, e);
            CMBUp_Click(sender, e);
            CMBUp_Click(sender, e);
            CMBUp_Click(sender, e);
            CMBUp_Click(sender, e);
        }
        private void CMBUp9_Click(object sender, EventArgs e)
        {
            CMBUp_Click(sender, e);
            CMBUp_Click(sender, e);
            CMBUp_Click(sender, e);
            CMBUp_Click(sender, e);
            CMBUp_Click(sender, e);
            CMBUp_Click(sender, e);
            CMBUp_Click(sender, e);
            CMBUp_Click(sender, e);
            CMBUp_Click(sender, e);
        }
        private void CMBUpTo_Click(object sender, EventArgs e)
        {
            if (!TravianData.Villages.ContainsKey(SelectVillage))
                return;
            if (m_buildinglist.listViewBuilding.SelectedItems.Count == 0)
                return;
            var CV = TravianData.Villages[SelectVillage];
            if (TravianData.Villages[SelectVillage].isBuildingInitialized == 2)
            {
                for (int i = 0; i < m_buildinglist.listViewBuilding.SelectedItems.Count; i++)
                {
                    int temp;
                    if (!int.TryParse(m_buildinglist.listViewBuilding.SelectedItems[i].Text, out temp))
                        continue;
                    int Bid = Convert.ToInt32(m_buildinglist.listViewBuilding.SelectedItems[i].Text);
                    int Gid = CV.Buildings[Bid].Gid;
                    if (CV.Buildings[Bid].Level >= Buildings.BuildingCost[Gid].data.Length - 1)
                        continue;
                    BuildToLevel btl = new BuildToLevel()
                    {
                        BuildingName = tr.GetGidLang(Gid),
                        DisplayName = dl.GetGidLang(Gid),
                        CurrentLevel = CV.Buildings[Bid].Level,
                        TargetLevel = Buildings.BuildingCost[Gid].data.Length - 1,
                        mui = mui
                    };
                    if (btl.ShowDialog() == DialogResult.OK)
                    {
                        if (btl.Return < 0)
                            continue;
                        var Q = new BuildingQueue()
                        {
                            UpCall = tr,
                            VillageID = SelectVillage,
                            Bid = Bid,
                            Gid = CV.Buildings[Bid].Gid,
                            TargetLevel = btl.Return
                        };
                        CV.Queue.Add(Q);
                        lvi(Q);
                        if (m_buildinglist.listViewBuilding.SelectedItems.Count > i)
                            m_buildinglist.listViewBuilding.SelectedItems[i].SubItems[1].Text += "!";
                    }
                }
            }
        }
        private void CMBDestroy_Click(object sender, EventArgs e)
        {
            if (!TravianData.Villages.ContainsKey(SelectVillage))
                return;
            if (m_buildinglist.listViewBuilding.SelectedItems.Count == 0)
                return;
            var CV = TravianData.Villages[SelectVillage];
            if (TravianData.Villages[SelectVillage].isBuildingInitialized == 2)
            {
                for (int i = 0; i < m_buildinglist.listViewBuilding.SelectedItems.Count; i++)
                {
                    int temp;
                    if (!int.TryParse(m_buildinglist.listViewBuilding.SelectedItems[i].Text, out temp))
                        continue;
                    int Bid = Convert.ToInt32(m_buildinglist.listViewBuilding.SelectedItems[i].Text);
                    if (Bid < 19)
                        continue;
                    var Q = new DestroyQueue
                    {
                        UpCall = tr,
                        VillageID = SelectVillage,
                        Bid = Bid,
                        Gid = CV.Buildings[Bid].Gid
                    };
                    CV.Queue.Add(Q);
                    lvi(Q);
                    m_buildinglist.listViewBuilding.Items[m_buildinglist.listViewBuilding.SelectedIndices[i]].SubItems[1].Text += "X";
                }
            }
        }
        private void CMBNew_Click(object sender, EventArgs e)
        {
            if (!TravianData.Villages.ContainsKey(SelectVillage))
                return;
            var CV = TravianData.Villages[SelectVillage];
            if (TravianData.Villages[SelectVillage].isBuildingInitialized == 2)
            {
                NewBuilding nb = new NewBuilding(TravianData, SelectVillage) { mui = mui };
                if (nb.ShowDialog() == DialogResult.OK)
                {
                    var Q = new BuildingQueue()
                    {
                        UpCall = tr,
                        VillageID = SelectVillage,
                        Bid = nb.OutBid,
                        Gid = nb.OutGid,
                        TargetLevel = nb.OutTop ? Buildings.BuildingCost[nb.OutGid].data.Length - 1 : 0
                    };
                    CV.Queue.Add(Q);
                    lvi(Q);

                    CV.Buildings[nb.OutBid] = new TBuilding() { Gid = nb.OutGid };
                }
            }
        }
        private void CMBAI_C_Click(object sender, EventArgs e)
        {
            if (!TravianData.Villages.ContainsKey(SelectVillage))
                return;
            var CV = TravianData.Villages[SelectVillage];
            if (CV.isBuildingInitialized == 2)
            {
                var Q = new AIQueue
                {
                    AIType = AIQueue.TAIType.Resource,
                    UpCall = tr,
                    VillageID = SelectVillage
                };
                CV.Queue.Add(Q);
                lvi(Q);
            }

        }
        private void CMBAI_L_Click(object sender, EventArgs e)
        {
            if (!TravianData.Villages.ContainsKey(SelectVillage))
                return;
            var CV = TravianData.Villages[SelectVillage];
            if (CV.isBuildingInitialized == 2)
            {
                var Q = new AIQueue
                {
                    AIType = AIQueue.TAIType.Level,
                    UpCall = tr,
                    VillageID = SelectVillage
                };
                CV.Queue.Add(Q);
                lvi(Q);
            }

        }
        
        private void CMBRefresh_Click(object sender, EventArgs e)
        {
            if (!TravianData.Villages.ContainsKey(SelectVillage))
                return;
            TravianData.Villages[SelectVillage].InitializeBuilding();
        }
        
        private void CMBRefreshDestroy_Click(object sender, EventArgs e)
        {
            if (!TravianData.Villages.ContainsKey(SelectVillage))
                return;
            TravianData.Villages[SelectVillage].InitializeDestroy();
        }
        
        private void CMBParty500_Click(object sender, EventArgs e)
        {
            if (!TravianData.Villages.ContainsKey(SelectVillage))
                return;
            var CV = TravianData.Villages[SelectVillage];
            var Q = new PartyQueue
            {
                PartyType = PartyQueue.TPartyType.P500,
                UpCall = tr,
                VillageID = SelectVillage
            };
            CV.Queue.Add(Q);
            lvi(Q);
        }
        
        private void CMBParty2000_Click(object sender, EventArgs e)
        {
            if (!TravianData.Villages.ContainsKey(SelectVillage))
                return;
            var CV = TravianData.Villages[SelectVillage];
            var Q = new PartyQueue
            {
                PartyType = PartyQueue.TPartyType.P2000,
                UpCall = tr,
                VillageID = SelectVillage
            };
            CV.Queue.Add(Q);
            lvi(Q);
        }
        private void CMBRaid_Click(object sender, EventArgs e)
        {
            // MessageBox.Show("尚未完成此功能");
            // return;
            TVillage village = this.GetSelectedVillage();
            if (village == null)
            {
                return;
            }

            RaidQueue task = new RaidQueue()
            {
                UpCall = this.tr,
                Tribe = TravianData.Tribe,
                VillageID = this.SelectVillage,
                RaidType = RaidType.AttackRaid,
                SpyOption = SpyOption.Resource,
                MaxCount = 1,
            };

            if (this.EditRaidQueue(village, task))
            {
                village.Queue.Add(task);
                this.lvi(task);
            }
        }

        private void CMBEnableCoin_CheckedChanged(object sender, EventArgs e)
        {
            CMMNpcTrade.Enabled = CMMNpcTrade2.Enabled = CMBEnableCoin.Checked;
            CMBEnableCoin.ForeColor = CMBEnableCoin.Checked ? Color.DarkRed : Color.DarkBlue;
            if (mui != null)
                CMBEnableCoin.Text = mui._("CMBEnableCoin") + (CMBEnableCoin.Checked ? mui._("enabled") : "");
        }
        #endregion

        #region CMQ
        private void CMQEdit_Click(object sender, EventArgs e)
        {
            TVillage village = this.GetSelectedVillage();
            if (village == null)
            {
                return;
            }

            lock (QueueLock)
            {
                IQueue task = this.GetSelectedTask(village);
                if (task is RaidQueue)
                {
                    this.EditRaidQueue(village, (RaidQueue)task);
                }
            }
        }

        private bool EditRaidQueue(TVillage village, RaidQueue task)
        {
            if (village.isTroopInitialized != 2)
            {
                village.InitializeTroop();
                return false;
            }

            RaidOptForm rof = new RaidOptForm()
            {
                mui = this.mui,
                dl = this.dl,
                TroopsAtHome = village.Troop.GetTroopsAtHome(village),
                Village = village,
                Return = task,
            };

            if (rof.ShowDialog() != DialogResult.OK)
            {
                return false;
            }

            if (rof.Return == null || !rof.Return.IsValid)
            {
                return false;
            }

            task.CopySettings(rof.Return);
            return true;
        }

        private IQueue GetSelectedTask(TVillage village)
        {
            if (this.m_queuelist.listViewQueue.SelectedIndices.Count == 0)
            {
                return null;
            }

            int index = this.m_queuelist.listViewQueue.SelectedIndices[0];
            if (index >= village.Queue.Count)
            {
                return null;
            }

            return village.Queue[index];
        }

        private TVillage GetSelectedVillage()
        {
            if (this.TravianData.Villages.ContainsKey(this.SelectVillage))
            {
                TVillage village = this.TravianData.Villages[this.SelectVillage];
                if (village.isBuildingInitialized == 2)
                {
                    return village;
                }
            }

            return null;
        }

        private void CMQDel_Click(object sender, EventArgs e)
        {
            if (!TravianData.Villages.ContainsKey(SelectVillage))
                return;
            if (m_queuelist.listViewQueue.SelectedIndices.Count == 0)
                return;
            lock (QueueLock)
            {
                var CV = TravianData.Villages[SelectVillage];
                if (CV.isBuildingInitialized == 2)
                {
                    for (int i = m_queuelist.listViewQueue.SelectedIndices.Count - 1; i >= 0; i--)
                    {
                        int QID = m_queuelist.listViewQueue.SelectedIndices[i];
                        if (CV.Queue.Count > QID)
                        {
                            var q = CV.Queue[QID];
                            if (q is BuildingQueue)
                            {
                                var Q = q as BuildingQueue;
                                if (CV.Buildings.ContainsKey(Q.Bid) && CV.Buildings[Q.Bid].Level == 0)
                                    CV.Buildings.Remove(Q.Bid);
                            }
                            if (q is AlarmQueue)
                            {
                                CV.GetAllTroop = false;
                            }
                            CV.Queue.RemoveAt(QID);
                            TravianData.Dirty = true;
                        }
                        if (m_queuelist.listViewQueue.Items.Count > QID)
                            m_queuelist.listViewQueue.Items.RemoveAt(QID);
                    }
                }
            }
        }
        private void CMQClear_Click(object sender, EventArgs e)
        {
            if (!TravianData.Villages.ContainsKey(SelectVillage))
                return;
            lock (QueueLock)
            {
                var CV = TravianData.Villages[SelectVillage];
                if (CV.isBuildingInitialized == 2)
                {
                    foreach (var q in CV.Queue)
                    {
                        if (q is BuildingQueue)
                        {
                            var Q = q as BuildingQueue;
                            if (CV.Buildings.ContainsKey(Q.Bid) && CV.Buildings[Q.Bid].Level == 0)
                                CV.Buildings.Remove(Q.Bid);
                        }
                    }
                    CV.Queue.Clear();
                    TravianData.Dirty = true;
                    m_queuelist.listViewQueue.Items.Clear();
                }
            }
        }
        private void CMQUp_Click(object sender, EventArgs e)
        {
            if (!TravianData.Villages.ContainsKey(SelectVillage))
                return;
            if (m_queuelist.listViewQueue.SelectedIndices.Count == 0 || m_queuelist.listViewQueue.SelectedIndices[0] == 0)
                return;
            lock (QueueLock)
            {
                var CV = TravianData.Villages[SelectVillage];
                if (CV.isBuildingInitialized == 2)
                {
                    for (int i = 0; i < m_queuelist.listViewQueue.SelectedIndices.Count; i++)
                    {
                        int n = m_queuelist.listViewQueue.SelectedIndices[i];
                        CV.Queue.Reverse(n - 1, 2);
                        ListViewItem tlvi = m_queuelist.listViewQueue.Items[n - 1];
                        m_queuelist.listViewQueue.Items.RemoveAt(n - 1);
                        m_queuelist.listViewQueue.Items.Insert(n, tlvi);
                    }
                    TravianData.Dirty = true;
                }
            }
        }
        private void CMQDown_Click(object sender, EventArgs e)
        {
            if (!TravianData.Villages.ContainsKey(SelectVillage))
                return;
            if (m_queuelist.listViewQueue.SelectedIndices.Count == 0 || m_queuelist.listViewQueue.SelectedIndices[m_queuelist.listViewQueue.SelectedIndices.Count - 1] == m_queuelist.listViewQueue.Items.Count - 1)
                return;
            lock (QueueLock)
            {
                var CV = TravianData.Villages[SelectVillage];
                if (CV.isBuildingInitialized == 2)
                {
                    for (int i = m_queuelist.listViewQueue.SelectedIndices.Count - 1; i >= 0; i--)
                    {
                        int n = m_queuelist.listViewQueue.SelectedIndices[i];
                        CV.Queue.Reverse(n, 2);
                        ListViewItem tlvi = m_queuelist.listViewQueue.Items[n + 1];
                        m_queuelist.listViewQueue.Items.RemoveAt(n + 1);
                        m_queuelist.listViewQueue.Items.Insert(n, tlvi);
                    }
                    TravianData.Dirty = true;
                }
            }
        }

        /// <summary>
        /// Pause/resume the selected task
        /// </summary>
        private void CMQPause_Click(object sender, EventArgs e)
        {
            if (!this.TravianData.Villages.ContainsKey(this.SelectVillage))
            {
                return;
            }

            if (m_queuelist.listViewQueue.SelectedIndices.Count == 0)
            {
                return;
            }

            TVillage village = this.TravianData.Villages[this.SelectVillage];
            if (village.isBuildingInitialized == 2)
            {
                lock (QueueLock)
                {
                    foreach (int i in m_queuelist.listViewQueue.SelectedIndices)
                    {
                        var task = village.Queue[i];
                        task.Paused = !task.Paused;
                    }

                    TravianData.Dirty = true;
                }
            }

            this.DisplayQueue();
        }

        private void CMQTimer_Click(object sender, EventArgs e)
        {
            CMQTimer.Checked = !CMQTimer.Checked;
            timer1.Enabled = CMQTimer.Checked;
        }

        /// <summary>
        /// Export the villag task queue to a text file
        /// </summary>
        private void CMQImport_Click(object sender, EventArgs e)
        {
            if (!this.TravianData.Villages.ContainsKey(this.SelectVillage))
            {
                return;
            }

            TVillage village = this.TravianData.Villages[this.SelectVillage];
            if (village.isBuildingInitialized != 2)
            {
                return;
            }

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.RestoreDirectory = true;
            openFileDialog.Filter = "Stran task queue|*.stq";
            openFileDialog.Title = this.mui._("ImportTaskQueue");
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                lock (QueueLock)
                {
                    village.RestoreQueue(openFileDialog.FileName);
                }
            }
        }

        /// <summary>
        /// Import the villag task queue from a previously saved text file
        /// </summary>
        private void CMQExport_Click(object sender, EventArgs e)
        {
            if (!this.TravianData.Villages.ContainsKey(this.SelectVillage))
            {
                return;
            }

            TVillage village = this.TravianData.Villages[this.SelectVillage];
            if (village.isBuildingInitialized != 2)
            {
                return;
            }

            lock (QueueLock)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.RestoreDirectory = true;
                saveFileDialog.Filter = "Stran task queue|*.stq";
                saveFileDialog.Title = this.mui._("ExportTaskQueue");
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                //if(saveFileDialog.FileName != "")
                {
                    village.SaveQueue(saveFileDialog.FileName);
                }
            }
        }
        #endregion

        #region CMR
        private void CMR_Opening(object sender, CancelEventArgs e)
        {
            if (!TravianData.Villages.ContainsKey(SelectVillage))
                return;
            CMRResearch.Enabled = false;
            CMRUpgradeAtk.Enabled = false;
            CMRUpgradeAtkTo.Enabled = false;
            CMRUpgradeDef.Enabled = false;
            CMRUpgradeDefTo.Enabled = false;
            foreach (ListViewItem x in m_researchstatus.listViewUpgrade.SelectedItems)
            {
                CMRResearch.Enabled |= x.SubItems[1].BackColor != Color.White;
                CMRUpgradeAtkTo.Enabled = CMRUpgradeAtk.Enabled |= x.SubItems[2].BackColor != Color.White;
                CMRUpgradeDefTo.Enabled = CMRUpgradeDef.Enabled |= x.SubItems[3].BackColor != Color.White;
            }
        }
        private void CMRResearch_Click(object sender, EventArgs e)
        {
            if (!TravianData.Villages.ContainsKey(SelectVillage))
                return;
            foreach (ListViewItem x in m_researchstatus.listViewUpgrade.SelectedItems)
            {
                if (x.SubItems[1].BackColor != Color.White)
                {
                    var Q = new ResearchQueue
                    {
                        UpCall = tr,
                        VillageID = SelectVillage,
                        ResearchType = ResearchQueue.TResearchType.Research,
                        Aid = m_researchstatus.listViewUpgrade.Items.IndexOf(x) + 1
                    };
                    TravianData.Villages[SelectVillage].Queue.Add(Q);
                    lvi(Q);
                }
            }
        }
        private void CMRUpgradeAtk_Click(object sender, EventArgs e)
        {
            if (!TravianData.Villages.ContainsKey(SelectVillage))
                return;
            foreach (ListViewItem x in m_researchstatus.listViewUpgrade.SelectedItems)
            {
                if (x.SubItems[2].BackColor != Color.White)
                {
                    var Q = new ResearchQueue
                    {
                        UpCall = tr,
                        VillageID = SelectVillage,
                        ResearchType = ResearchQueue.TResearchType.UpAttack,
                        Aid = m_researchstatus.listViewUpgrade.Items.IndexOf(x) + 1
                    };
                    TravianData.Villages[SelectVillage].Queue.Add(Q);
                    lvi(Q);
                }
            }
        }
        private void CMRUpgradeDef_Click(object sender, EventArgs e)
        {
            if (!TravianData.Villages.ContainsKey(SelectVillage))
                return;
            foreach (ListViewItem x in m_researchstatus.listViewUpgrade.SelectedItems)
            {
                if (x.SubItems[3].BackColor != Color.White)
                {
                    var Q = new ResearchQueue
                    {
                        UpCall = tr,
                        VillageID = SelectVillage,
                        ResearchType = ResearchQueue.TResearchType.UpDefence,
                        Aid = m_researchstatus.listViewUpgrade.Items.IndexOf(x) + 1
                    };
                    TravianData.Villages[SelectVillage].Queue.Add(Q);
                    lvi(Q);
                }
            }
        }
        private void CMRUpgradeAtkTo_Click(object sender, EventArgs e)
        {
            if (!TravianData.Villages.ContainsKey(SelectVillage))
                return;
            var CV = TravianData.Villages[SelectVillage];
            foreach (ListViewItem x in m_researchstatus.listViewUpgrade.SelectedItems)
            {
                if (x.SubItems[2].BackColor != Color.White)
                {
                    int Bid = m_researchstatus.listViewUpgrade.Items.IndexOf(x) + 1;

                    BuildToLevel btl = new BuildToLevel()
                    {
                        BuildingName = tr.GetAidLang(TravianData.Tribe, Bid),
                        DisplayName = dl.GetAidLang(TravianData.Tribe, Bid),
                        CurrentLevel = CV.Upgrades[Bid].AttackLevel,
                        TargetLevel = CV.BlacksmithLevel,
                        mui = mui
                    };
                    if (btl.ShowDialog() == DialogResult.OK)
                    {
                        if (btl.Return < 0)
                            continue;

                        var Q = new ResearchQueue
                        {
                            UpCall = tr,
                            VillageID = SelectVillage,
                            TargetLevel = btl.Return,
                            ResearchType = ResearchQueue.TResearchType.UpAttack,
                            Aid = m_researchstatus.listViewUpgrade.Items.IndexOf(x) + 1
                        };
                        CV.Queue.Add(Q);
                        lvi(Q);
                    }
                }
            }
        }
        private void CMRUpgradeDefTo_Click(object sender, EventArgs e)
        {
            if (!TravianData.Villages.ContainsKey(SelectVillage))
                return;
            var CV = TravianData.Villages[SelectVillage];
            foreach (ListViewItem x in m_researchstatus.listViewUpgrade.SelectedItems)
            {
                if (x.SubItems[3].BackColor != Color.White)
                {
                    int Bid = m_researchstatus.listViewUpgrade.Items.IndexOf(x) + 1;

                    BuildToLevel btl = new BuildToLevel()
                    {
                        BuildingName = tr.GetAidLang(TravianData.Tribe, Bid),
                        DisplayName = dl.GetAidLang(TravianData.Tribe, Bid),
                        CurrentLevel = CV.Upgrades[Bid].DefenceLevel,
                        TargetLevel = CV.ArmouryLevel,
                        mui = mui
                    };
                    if (btl.ShowDialog() == DialogResult.OK)
                    {
                        if (btl.Return < 0)
                            continue;

                        var Q = new ResearchQueue
                        {
                            UpCall = tr,
                            VillageID = SelectVillage,
                            TargetLevel = btl.Return,
                            ResearchType = ResearchQueue.TResearchType.UpDefence,
                            Aid = m_researchstatus.listViewUpgrade.Items.IndexOf(x) + 1
                        };
                        CV.Queue.Add(Q);
                        lvi(Q);
                    }
                }
            }
        }
        private void CMRRefresh_Click(object sender, EventArgs e)
        {
            if (!TravianData.Villages.ContainsKey(SelectVillage))
                return;
            TravianData.Villages[SelectVillage].InitializeUpgrade();
        }
        #endregion

        #region CMM
        private void CMMNew_Click(object sender, EventArgs e)
        {
            if (!TravianData.Villages.ContainsKey(SelectVillage))
                return;

            TVillage CV = TravianData.Villages[SelectVillage];
            if (CV.isBuildingInitialized == 2)
            {
                TransferSetting ts = new TransferSetting(tr)
                {
                    FromVillageID = this.SelectVillage,
                    TravianData = this.TravianData,
                    mui = this.mui
                };

                if (ts.ShowDialog() == DialogResult.OK && ts.Return != null)
                {
                    CV.Queue.Add(ts.Return);
                    lvi(ts.Return);
                }
            }
        }


        private void CMMNpcTrade_Click(object sender, EventArgs e)
        {
            if (!TravianData.Villages.ContainsKey(SelectVillage))
                return;

            TVillage CV = TravianData.Villages[SelectVillage];
            if (CV.isBuildingInitialized != 2)
                return;

            NpcTradeSetting setting = new NpcTradeSetting()
            {
                Village = CV,
                mui = this.mui
            };

            if (setting.ShowDialog() == DialogResult.OK && setting.Return != null)
            {
                var Q = setting.Return;
                Q.UpCall = tr;
                CV.Queue.Add(Q);
                lvi(Q);
            }
        }
        #endregion

        private void dockPanel1_Resize(object sender, EventArgs e)
        {
            if (dockPanel1.Contents.Count != 0)
            {
                string fn = GetStyleFilename();
                dockPanel1.SaveAsXml(fn);
            }
        }

        //public int timeoffset = 0;

        private void timersec1_Tick(object sender, EventArgs e)
        {
            LCLTime.Text = LCLTime.ToolTipText + " " + DateTime.Now.ToLongTimeString();
            SVRTime.Text = SVRTime.ToolTipText + " " + DateTime.Now.AddSeconds(TravianData.ServerTimeOffset).ToLongTimeString();
            if (tr != null)
            {
                PageCount.Text = "Page: " + tr.pcount;
                ActionCount.Text = "Action: " + tr.bcount;
            }
        }

        private void CMICancel_Click(object sender, EventArgs e)
        {
            if (!TravianData.Villages.ContainsKey(SelectVillage))
                return;
            if (m_inbuildinglist.listViewInBuilding.SelectedIndices.Count == 1)
            {
                var CV = TravianData.Villages[SelectVillage];
                int key = m_inbuildinglist.listViewInBuilding.SelectedIndices[0];
                if (CV.InBuilding[key] != null && CV.InBuilding[key].Cancellable)
                    tr.Cancel(SelectVillage, key);
            }
        }

        private void contextMenuInbuilding_Opening(object sender, CancelEventArgs e)
        {
            if (!TravianData.Villages.ContainsKey(SelectVillage))
                return;
            if (m_inbuildinglist.listViewInBuilding.SelectedIndices.Count == 1)
            {
                var CV = TravianData.Villages[SelectVillage];
                int key = m_inbuildinglist.listViewInBuilding.SelectedIndices[0];
                CMICancel.Enabled = CV.InBuilding[key] != null && CV.InBuilding[key].Cancellable && CV.InBuilding[key].FinishTime > DateTime.Now;

            }
        }

        private void lvi(IQueue Q)
        {
            ListViewItem lvi = m_queuelist.listViewQueue.Items.Add(Q.GetType().Name);
            lvi.SubItems.Add(Q.Title);
            lvi.SubItems.Add(Q.Status);
            lvi.SubItems.Add("");
            TravianData.Dirty = true;
        }

        public void QPParty_Click(object sender, EventArgs e)
        {
            if (!TravianData.Villages.ContainsKey(SelectVillage))
                return;
            var CV = TravianData.Villages[SelectVillage];
            int G24Level = 0;
            foreach (var b in CV.Buildings)
            {
                if (b.Value.Gid == 24)
                {
                    G24Level = b.Value.Level;
                    break;
                }
            }
            if (G24Level > 0)
            {
                var Q = new PartyQueue
                {
                    PartyType = G24Level > 10 ? PartyQueue.TPartyType.P2000 : PartyQueue.TPartyType.P500,
                    UpCall = tr,
                    VillageID = SelectVillage
                };
                CV.Queue.Add(Q);
                lvi(Q);
            }
        }

        public void QPAILevel_Click(object sender, EventArgs e)
        {
            CMBAI_L_Click(sender, e);
        }

        public void QPUpTop_Click(object sender, EventArgs e)
        {
            if (!TravianData.Villages.ContainsKey(SelectVillage))
                return;
            if (m_buildinglist.listViewBuilding.SelectedItems.Count == 0)
                return;
            var CV = TravianData.Villages[SelectVillage];
            if (TravianData.Villages[SelectVillage].isBuildingInitialized == 2)
            {
                for (int i = 0; i < m_buildinglist.listViewBuilding.SelectedItems.Count; i++)
                {
                    int temp;
                    if (!int.TryParse(m_buildinglist.listViewBuilding.SelectedItems[i].Text, out temp))
                        continue;
                    int Bid = Convert.ToInt32(m_buildinglist.listViewBuilding.SelectedItems[i].Text);
                    int Gid = CV.Buildings[Bid].Gid;
                    if (CV.Buildings[Bid].Level >= Buildings.BuildingCost[Gid].data.Length - 1)
                        continue;
                    var Q = new BuildingQueue()
                    {
                        UpCall = tr,
                        VillageID = SelectVillage,
                        Bid = Bid,
                        Gid = CV.Buildings[Bid].Gid,
                        TargetLevel = Buildings.BuildingCost[Gid].data.Length - 1
                    };
                    CV.Queue.Add(Q);
                    lvi(Q);
                    if (m_buildinglist.listViewBuilding.SelectedItems.Count > i)
                        m_buildinglist.listViewBuilding.SelectedItems[i].SubItems[1].Text += "!";
                }
            }
        }

        public void QPRefreshRes_Click(object sender, EventArgs e)
        {
            if (!TravianData.Villages.ContainsKey(SelectVillage))
                return;
            tr.FetchVillageMarket(SelectVillage);
        }

        private void CMBProduceTroop_Click(object sender, EventArgs e)
        {
            if (!TravianData.Villages.ContainsKey(SelectVillage))
                return;
            var CV = TravianData.Villages[SelectVillage];
            if (CV.isUpgradeInitialized < 2)
            {
                CV.InitializeUpgrade();
                MessageBox.Show("程序正在读取研发信息，请重新操作一次");
                return;
            }
            List<TroopInfo> CanProduce = new List<TroopInfo>();
            foreach (var x in CV.Upgrades)
                //if(x.Value.Researched)
                if (x.Key <= 10)
                    CanProduce.Add(new TroopInfo { Aid = x.Key, Name = dl.GetAidLang(TravianData.Tribe, x.Key), Researched = x.Value.Researched });
            ProduceTroopSetting pts = new ProduceTroopSetting
            {
                RUVillageID = this.SelectVillage,
                TravianData = this.TravianData,
                mui = mui,
                CanProduce = CanProduce
            };
            if (pts.ShowDialog() == DialogResult.OK && pts.Result != null)
            {
                // continue
                pts.Result.VillageID = CV.ID;
                pts.Result.UpCall = tr;
                CV.Queue.Add(pts.Result);
                lvi(pts.Result);
            }
        }

        private void CMTRefresh_Click(object sender, EventArgs e)
        {
            if (!TravianData.Villages.ContainsKey(SelectVillage))
                return;
            TravianData.Villages[SelectVillage].InitializeTroop();
        }
        
        private void CMVTlimit_Click(object sender, EventArgs e)
        {
            if (!TravianData.Villages.ContainsKey(SelectVillage))
            {
                return;
            }

            TVillage village = this.TravianData.Villages[SelectVillage];
            if (village.isBuildingInitialized != 2)
            {
                return;
            }

            ResourceLimit limit = new ResourceLimit()
            {
                Village = village,
                Description = mui._("TResourceLimit"),
                Limit = village.Troop.ResLimit == null ? new TResAmount(0, 0, 0, 0) : village.Troop.ResLimit,
                mui = this.mui
            };

            if (limit.ShowDialog() == DialogResult.OK)
            {
                village.Troop.ResLimit = limit.Return;
                TravianData.Dirty = true;
            }
        }

        private void CMVRename_Click(object sender, EventArgs e)
        {
            if (!TravianData.Villages.ContainsKey(SelectVillage))
                return;
            var CV = TravianData.Villages[SelectVillage];
            Rename newname = new Rename()
            {
                ReVillageID = CV.ID,
                oldVillagename = CV.Name.ToString(),
                mui = mui,
                UpCall = tr
            };

            if (newname.ShowDialog() == DialogResult.OK)
            {
                return;
            }
        }

        void CMBAttackClick(object sender, EventArgs e)
        {
            if (!TravianData.Villages.ContainsKey(SelectVillage))
                return;

            TVillage CV = TravianData.Villages[SelectVillage];
            if (CV.isTroopInitialized == 2)
            {
                TTInfo Troop = CV.Troop.GetTroopsAtHome(CV);
                if (Troop == null)
                    return;
                AttackOptForm rof = new AttackOptForm()
                {
                    mui = this.mui,
                    Troops = Troop.Troops,
                    dl = this.dl,
                    Tribe = Troop.Tribe,
                    VillageID = CV.ID,
                    UpCall = tr
                };

                if (rof.ShowDialog() == DialogResult.OK && rof.Return != null)
                {
                    rof.Return.VillageID = CV.ID;
                    rof.Return.UpCall = tr;
                    CV.Queue.Add(rof.Return);
                    lvi(rof.Return);
                }
            }
            else
                CV.InitializeTroop();
        }

        private void CMBAlarm_Click(object sender, EventArgs e)
        {
            var CV = TravianData.Villages[SelectVillage];
            if (CV.isTroopInitialized != 2)
            {
                CV.InitializeTroop();
                MessageBox.Show("读取军队信息，重新操作一次");
                return;
            }
            Alarm a = new Alarm()
            {
                mui = this.mui,
                dl = this.dl,
            };
            if (a.ShowDialog() == DialogResult.OK && a.Return != null)
            {
                var q = a.Return;
                q.UpCall = tr;
                q.VillageID = CV.ID;
                q.dl = this.dl;
                CV.Queue.Add(q);
                lvi(q);

                CV.GetAllTroop = true;
            }
        }
        
		private void CMVSaveRESClick(object sender, EventArgs e)
		{
			if(!this.TravianData.Villages.ContainsKey(this.SelectVillage))
			{
				return;
			}

			TVillage village = this.TravianData.Villages[this.SelectVillage];
			if (village.isBuildingInitialized != 2)
			{
				return;
			}

			lock (QueueLock)
			{
				SaveFileDialog saveFileDialog = new SaveFileDialog();
				saveFileDialog.RestoreDirectory = true;
				saveFileDialog.Filter = "Stran ResLimit queue|*.srq";
				saveFileDialog.Title = this.mui._("ExportTaskQueue");
				if(saveFileDialog.ShowDialog() == DialogResult.OK)
				{
					village.SaveQueueRes(saveFileDialog.FileName);
				}
			}
		}
		
		private void CMVRestoreRESClick(object sender, EventArgs e)
		{
			if(!this.TravianData.Villages.ContainsKey(this.SelectVillage))
			{
				return;
			}

			TVillage village = this.TravianData.Villages[this.SelectVillage];
			if (village.isBuildingInitialized != 2)
			{
				return;
			}

			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.RestoreDirectory = true;
			openFileDialog.Filter = "Stran ResLimit queue|*.srq";
			openFileDialog.Title = this.mui._("ImportTaskQueue");
			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				lock (QueueLock)
				{
					village.RestoreQueueRes(openFileDialog.FileName);
					TravianData.Dirty = true;
				}
			}
		}
		
		private void CMVDelVClick(object sender, EventArgs e)
		{
            MessageBox.Show("尚未完成此功能");
            return;
			if(!TravianData.Villages.ContainsKey(SelectVillage) || TravianData.Villages.Count <= 1)
				return;
			
			TVillage CV = TravianData.Villages[SelectVillage];
			
			if (MessageBox.Show("这将会将此村庄从资料库中删除", "强制删除村庄", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
			{
				lock (QueueLock)
				{
					TravianData.Villages.Remove(SelectVillage);
					TravianData.Dirty = true;
					Local_StatusUpdate(sender, new Travian.StatusChanged() { ChangedData = Travian.ChangedType.Villages });
				}
			}
		}
		private void CMVRefreshAll_Click(object sender, EventArgs e)
        {
			foreach (var v in TravianData.Villages)
            {
        		TravianData.Villages[v.Key].InitializeVillage();
            }
        }
		
		void CMVFindOasisClick(object sender, EventArgs e)
		{
			if (!TravianData.Villages.ContainsKey(SelectVillage))
                return;
            
			TravianData.Villages[SelectVillage].FindOasis();
		}
    }
}
