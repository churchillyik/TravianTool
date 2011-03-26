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
        private Random rand = new Random();
        private string p_e, p_uid, p_jahr, p_monat, p_tag, p_be1, p_mw, p_ort, p_be2;

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
                    // Prepare data
                    string data = UpCall.PageQuery(ReVillageID, "spieler.php?s=1");
                    Match m;
                    m = Regex.Match(data, "type=\"hidden\" name=\"e\" value=\"(\\d+?)\"");
                    if (m.Success)
                        p_e = m.Groups[1].Value;
                    m = Regex.Match(data, "type=\"hidden\" name=\"uid\" value=\"(\\d+?)\"");
                    if (m.Success)
                        p_uid = m.Groups[1].Value;
                    m = Regex.Match(data, "tabindex=\"3\" type=\"text\" name=\"jahr\" value=\"(.*?)\" maxlength=\"4\"");
                    if (m.Success)
                        p_jahr = m.Groups[1].Value;
                    m = Regex.Match(data, "<option value=\"(\\d+?)\" selected=\"selected\">");
                    if (m.Success)
                        p_monat = m.Groups[1].Value;
                    else
                    {
                        p_monat = "0";
                    }
                    m = Regex.Match(data, "tabindex=\"1\" class=\"text day\" type=\"text\" name=\"tag\" value=\"(.*?)\" maxlength=\"2\"");
                    if (m.Success)
                        p_tag = m.Groups[1].Value;
                    m = Regex.Match(data, "type=\"radio\" name=\"mw\" value=\"(\\d+?)\" checked tabindex=\"4\"");
                    if (m.Success)
                        p_mw = m.Groups[1].Value;
                    m = Regex.Match(data, "tabindex=\"5\" type=\"text\" name=\"ort\" value=\"(.*?)\" maxlength=\"30\"");
                    if (m.Success)
                        p_ort = m.Groups[1].Value;
                    //                    m = Regex.Match(data, "tabindex=\"6\" type=\"text\" name=\"dname\" value=\"(.*?)\" maxlength=\"20\"");
                    //                    if (m.Success)
                    //                        p_dname = m.Groups[1].Value;
                    m = Regex.Match(data, "tabindex=\"7\" name=\"be1\">([^<]*?)</textarea>");
                    if (m.Success)
                        p_be1 = m.Groups[1].Value;
                    m = Regex.Match(data, "tabindex=\"8\" name=\"be2\">([^<]*?)</textarea>");
                    if (m.Success)
                        p_be2 = m.Groups[1].Value;

                    Dictionary<string, string> PostData = new Dictionary<string, string>();
                    PostData["e"] = p_e;
                    PostData["uid"] = p_uid;
                    PostData["jahr"] = p_jahr;
                    PostData["monat"] = p_monat;
                    PostData["tag"] = p_tag;
                    PostData["be1"] = p_be1;
                    PostData["mw"] = p_mw;
                    PostData["ort"] = p_ort;
                    PostData["dname"] = newVillagename;
                    PostData["be2"] = p_be2;
                    PostData["s1.x"] = rand.Next(10, 70).ToString();
                    PostData["s1.y"] = rand.Next(3, 17).ToString();
                    string result = UpCall.PageQuery(ReVillageID, "spieler.php", PostData);
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
            // Get Rename form
//            string mainuser = UpCall.PageQuery(ReVillageID, "spieler.php");
//           Match m = Regex.Match(mainuser, "<a href=\"spieler.php\\?s=1\">");
//            if (!m.Success)
//            {
//                this.tbnewVillagename.Enabled = false;
//                this.buttonOK.Enabled = false;
//            }
        }
    }
}
