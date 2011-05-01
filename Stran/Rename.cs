using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using libTravian;

namespace Stran
{
    public partial class Rename : Form
    {
        public MUI mui { get; set; }
        public string oldVillagename { get; set; }
        private string newVillagename;
        public int ReVillageID { get; set; }
        public Travian UpCall { get; set; }

        public Rename()
        {
            InitializeComponent();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            newVillagename = this.tbnewVillagename.Text;
            // Get if possible Rename
            string mainuser = UpCall.PageQuery(ReVillageID, "spieler.php");
            Match mu = Regex.Match(mainuser, "<a href=\"spieler.php\\?s=1\">", RegexOptions.Singleline);
            if (!mu.Success)
            {
                UpCall.DebugLog("You are not owner of this accounts.", DebugLevel.W);
            }
            else
            {
                if (newVillagename != oldVillagename && newVillagename != null)
                {
                    Dictionary<string, string> PostData = new Dictionary<string, string>();
                    PostData["cmd"] = "changeVillageName";
                    PostData["name"] = newVillagename;
                    PostData["did"] = ReVillageID.ToString();
                    UpCall.PageQuery(ReVillageID, "ajax.php?cmd=changeVillageName", PostData);
                }
                else
                {
                    this.Close();
                }
            }
            this.Close();
        }

        private void Rename_Load(object sender, EventArgs e)
        {
            mui.RefreshLanguage(this);
            this.lboldVillagename.Text = this.tbnewVillagename.Text = oldVillagename;
        }
    }
}
