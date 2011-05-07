using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using LitJson;

namespace libTravian
{
    public class AttackQueue : IQueue
    {
        #region IQueue 成员

        public Travian UpCall { get; set; }

        public string Title
        {
            get
            {
                return Targets[TargetID].ToString();
            }
        }

        public string Status
        {
            get
            {
            	if (!Settlers && (wTroops[Wave].Troops == null))
                    return "N/A";
                var s = string.Format("{4} ({0}/{1} - {2}/{3})", TargetID + 1, Targets.Count, Wave + 1, wWaves.Count, RaidTypeL[Raidtype]);
                if (UpCall != null)
                {
                    for (int i = 0; i < wTroops.Count; i++)
                    {
                        for (int j = 0; j < 11; j++)
                        {
                        	if (wTroops[i].Troops[j] > 0)
                                s += string.Format(" ({2})*{0}-{1}", wTroops[i].Troops[j], DisplayLang.Instance.GetAidLang(UpCall.TD.Tribe, j + 1), wWaves[i]);
                        }
                    }
                }
                return s;
            }
        }

        public int CountDown
        {
            get
            {
                int timecost = 0;
                if (NextExec != DateTime.MinValue && NextExec > DateTime.Now)
                    timecost = Math.Max(timecost, Convert.ToInt32(NextExec.Subtract(DateTime.Now).TotalSeconds));
                return timecost;
            }
        }

        public void Action()
        {
            if (CountDown > 0)
                return;
            if (Targets[TargetID].IsEmpty)
            {
                UpCall.DebugLog("Target Is Error!!", DebugLevel.F);
                MarkDeleted = true;
                return;
            }
            if (Settlers)
            {
                var result = UpCall.PageQuery(VillageID, "a2b.php?id=" + Targets[TargetID].Z.ToString() + "&s=1");
                Match m;
                m = Regex.Match(result, "type=\"submit\" value=\"ok\" name=\"s1\"");
                if (m.Success)
                {
                    Dictionary<string, string> PostDataST = new Dictionary<string, string>();
                    PostDataST["a"] = "1";
                    PostDataST["s"] = Targets[TargetID].Z.ToString();
                    PostDataST["id"] = "39";
                    PostDataST["s1.x"] = rand.Next(40, 70).ToString();
                    PostDataST["s1.y"] = rand.Next(3, 17).ToString();
                    PostDataST["s1"] = "ok";
                    var datast = UpCall.PageQuery(VillageID, "build.php", PostDataST);
                }
                else
                {
                    UpCall.DebugLog("Unable to Settlers", DebugLevel.F);
                }
                MarkDeleted = true;
                return;
            }
            List<Dictionary<string, string>> PostDataALL = new List<Dictionary<string, string>>();
            for (int w = 0; w < wWaves.Count; w++)
            {
            	for (int j = 0; j < wWaves[w]; j++)
            	{
            		Dictionary<string, string> PostData = new Dictionary<string, string>();
            		var data = UpCall.PageQuery(VillageID, "a2b.php");
            		Match m10;
            		m10 = Regex.Match(data, "type=\"hidden\" name=\"timestamp\" value=\"([^>]*?)\"");
            		string p_timestamp = m10.Groups[1].Value;
            		Match m11;
            		m11 = Regex.Match(data, "type=\"hidden\" name=\"timestamp_checksum\" value=\"([^>]*?)\"");
            		string p_timestamp_checksum = m11.Groups[1].Value;
            		PostData["timestamp"] = p_timestamp;
            		PostData["timestamp_checksum"] = p_timestamp_checksum;
            		PostData["b"] = "1";
            		PostData["dname"] ="";
            		PostData["x"] = Targets[TargetID].X.ToString();
            		PostData["y"] = Targets[TargetID].Y.ToString();
            		PostData["s1.x"] = rand.Next(40, 70).ToString();
            		PostData["s1.y"] = rand.Next(3, 17).ToString();
            		PostData["s1"] = "ok";
            		if (Raidtype > 4)
            		{
            			PostData["c"] = "4";
            		}
            		else
            		{
            			PostData["c"] = Raidtype.ToString();
            		}
            		for (int i = 1; i < 12; i++)
             			PostData["t" + i] = wTroops[w].Troops[i - 1].ToString();
						
            		Dictionary<string, string> PostDataF = new Dictionary<string, string>();
            		var result = UpCall.PageQuery(VillageID, "a2b.php", PostData);
            		if (result.Contains("<p class=\"error\">"))
            		{
            			UpCall.DebugLog("Target is Error!!", DebugLevel.W);
            			MarkDeleted = true;
            			return;
            		}
            		Match m20;
            		m20 = Regex.Match(result, "type=\"hidden\" name=\"timestamp\" value=\"([^>]*?)\"");
            		string p_timestampF = m20.Groups[1].Value;
            		Match m21;
            		m21 = Regex.Match(result, "type=\"hidden\" name=\"timestamp_checksum\" value=\"([^>]*?)\"");
            		string p_timestamp_checksumF = m21.Groups[1].Value;
            		Match m22;
            		m22 = Regex.Match(result, "type=\"hidden\" name=\"a\" value=\"(\\d+?)\"");
            		string p_aF = m22.Groups[1].Value;
            		PostDataF["timestamp"] = p_timestampF;
            		PostDataF["timestamp_checksum"] = p_timestamp_checksumF;
            		PostDataF["a"] = p_aF;
            		//2 RE,3 Nor,4 Raid, 5 SPY1, 6 SPY2
            		if (Raidtype > 4)
            		{
            			PostDataF["c"] = "4";
            			int Tribe = UpCall.TD.Tribe;
            			int spy = Raidtype - 4;
            			PostDataF["spy"] = spy.ToString();
            			if (Tribe == 3 && wTroops[w].Troops[2] != 0)
            				PostDataF["t3"] = wTroops[w].Troops[2].ToString();
            			else if ((Tribe == 1 || Tribe == 2) && wTroops[w].Troops[3] != 0)
            				PostDataF["t4"] = wTroops[w].Troops[3].ToString();
            			else
            			{
            				UpCall.DebugLog("NO SCOUT TROOPS", DebugLevel.W);
            				MarkDeleted = true;
            				return;
            			}
            		}
            		else
            		{
            			PostDataF["c"] = Raidtype.ToString();
            			for (int i = 1; i < 12; i++)
            				PostDataF["t" + i] = wTroops[w].Troops[i - 1].ToString();
            		}
            		if (Raidtype == 3 && (wTroops[w].Troops[7] != 0))
            		{
            			int RP = UpCall.TD.Villages[VillageID].Buildings[39].Level;
            			int tkata = GIDMap[kata];
            			if (tkata == 0 || RP < 10)
            				tkata = 99;
            			PostDataF["kata"] = tkata.ToString();
            			if ((wTroops[w].Troops[7] >= 20) && (RP == 20))
            			{
            				int tkata2 = GIDMap[kata2];
            				PostDataF["kata2"] = tkata2.ToString();
            			}
            		}
            		PostDataF["kid"] = Targets[TargetID].Z.ToString();
            		PostDataF["id"] = "39";
            		PostDataF["s1.x"] = rand.Next(40, 70).ToString();
            		PostDataF["s1.y"] = rand.Next(3, 17).ToString();
            		PostDataF["s1"] = "ok";
            		
            		PostDataALL.Add(PostDataF);
            	}
            }
            //POST ALL Attack
            for (int i = 0; i < PostDataALL.Count; i++)
            	UpCall.PageQuery(VillageID, "a2b.php", PostDataALL[i]);
            Wave++;
            if (TargetID == Targets.Count - 1)
            {
            	MarkDeleted = true;
            }
            else
            {
            	LastExec = DateTime.Now;
                NextExec = LastExec.AddSeconds(MinimumInterval);
                TargetID++;
            }
        }
        #endregion

        [Json]
        public List<TPoint> Targets { get; set; }
        [Json]
        public List<TTInfo> wTroops { get; set; }
//        [Json]
//        public List<int[]> wTroops { get; set; }
        [Json]
        public int Wave { get; set; }
        [Json]
        public int VillageID { get; set; }
        [Json]
        public int Count { get; set; }
//        [Json]
//        public int MaxCount { get; set; }
        [Json]
        public List<int> wWaves { get; set; }
        [Json]
        public int TargetID { get; set; }
        [Json]
        public int kata { get; set; }
        [Json]
        public int kata2 { get; set; }
        [Json]
        public int Raidtype { get; set; }
//        [Json]
//        public int[] Troops { get; set; }
        [Json]
        public bool Settlers { get; set; }
        [Json]
        public bool Paused { get; set; }
		[Json]
        public bool MarkDeleted { get; private set; }
        public DateTime LastExec = DateTime.MinValue;
        public DateTime NextExec = DateTime.MinValue;

        [Json]
        public int MinimumInterval { get; set; }

        private Random rand = new Random();
        /// <summary>
        /// Map KATAGID
        /// </summary>
        private static readonly int[] GIDMap = new int[] { 0, 
		1, 2, 3, 4, 5, 6, 7, 8, 9, 10,
		11, 12, 13, 14, 15, 16, 17, 18, 19, 20,
		21, 23, 24, 25, 26, 27, 28, 29, 34, 36,
		37, 38, 39, 40, 41, 99};
        private static readonly string[] RaidTypeL = new string[] {"0", "S", "R", "A:N", "A:R", "S:R", "S:D"};

        public int QueueGUID { get { return 9; } }
    }
}