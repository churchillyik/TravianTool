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
            if (mainuser == null)
            	return;
            Match mu = Regex.Match(mainuser, "<a\\s*?href=\"spieler.php\\?s=1\"", RegexOptions.Singleline);
            if (!mu.Success)
            {
                UpCall.DebugLog("You are not owner of this accounts.", DebugLevel.W);
            }
            else
            {
                if (newVillagename != oldVillagename && newVillagename != null)
                {
                	//	cmd=changeVillageName&name=%E5%8A%A8%E5%A6%82%E8%84%B1%E5%85%94&did=63043
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
