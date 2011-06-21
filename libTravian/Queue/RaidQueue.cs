using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using LitJson;

namespace libTravian
{
    #region Enums
    public enum RaidType
    {
        Reinforce = 2,
        AttackNormal = 3,
        AttackRaid = 4
    }

    public enum SpyOption
    {
        Resource = 1,
        Defense = 2
    }
    #endregion

    /// <summary>
    /// Attacking, scouting and reinforcement
    /// </summary>
    public class RaidQueue : IQueue
    {
        #region Fields
        /// <summary>
        /// When the troops will return
        /// </summary>
        [Json]
        public DateTime resumeTime = DateTime.MinValue;

        #endregion

        #region Properties
        #region IQueue Members
        public Travian UpCall { get; set; }

        [Json]
        public bool MarkDeleted { get; set; }

        [Json]
        public bool Paused { get; set; }

        [Json]
        public int VillageID { get; set; }

        public int QueueGUID { get { return 9; } }

        public string Title
        {
            get
            {
                TTInfo troopInfo = new TTInfo()
                {
                    Tribe = this.Tribe,
                    Troops = this.Troops
                };

                return String.Format("{0}{1} {2}", 
                    this.MultipeRaids ? "MR " : "",
                    troopInfo.FriendlyName, 
                    this.RaidType);
            }
        }

        public string Status
        {
            get
            {
                if (this.MaxCount == 1)
                {
                    return string.Format(
                        "{0}/{1} {2} {3}",
                        this.TargetID + 1,
                        this.Targets.Count,
                        this.Targets[this.TargetID],
                        this.Description.IsEmpty() ? "-" : this.Description);
                }
                else
                {
                    return string.Format(
                        "{0}:{1}/{2}:{3} {4} {5}",
                        this.Count + 1,
                        this.TargetID + 1,
                        this.MaxCount == 0 ? "∞" : this.MaxCount.ToString(),
                        this.Targets.Count,
                        this.Targets[this.TargetID],
                        this.Description.IsEmpty() ? "-" : this.Description);
                }
            }
        }

        public int CountDown
        {
            get
            {
                if (!UpCall.TD.Villages.ContainsKey(this.VillageID))
                {
                    MarkDeleted = true;
                    return 86400;
                }

                TVillage village = UpCall.TD.Villages[this.VillageID];
                if (village.isTroopInitialized != 2)
                {
                    return 86400;
                }

                if (this.MaxSlots != 0 && village.Troop.GetUsedSlots(village) >= this.MaxSlots)
                {
                    return 86400;
                }

                this.Observe2To7Rule();
                double delay = this.MinimumDelay;
                if (!village.Troop.HasEnoughTroops(village, this.Troops))
                {
                    DateTime refreshTime = village.Troop.RefreshTime;
                    delay = Math.Max(delay, (refreshTime - DateTime.Now).TotalSeconds + 5);
                }

                return (int)Math.Min(delay, 86400);
            }
        }
        #endregion

        /// <summary>
        /// Short Description
        /// </summary>
        [Json]
        public string Description { set; get; }

        [Json]
        public int Tribe { get; set; }

        [Json]
        public int[] Troops { get; set; }

        [Json]
        public List<TPoint> Targets { get; set; }

        [Json]
        public RaidType RaidType { get; set; }

        [Json]
        public SpyOption SpyOption { get; set; }

        [Json]
        public int MaxCount { get; set; }

        [Json]
        public int MaxSlots { get; set; }

        [Json]
        public bool MultipeRaids { get; set; }

        [Json]
        public int Count { get; set; }

        [Json]
        public int TargetID { get; set; }

        public bool IsValid
        {
            get
            {
                if (this.Troops == null)
                {
                    return false;
                }

                int totalTroops = 0;
                foreach (int troop in this.Troops)
                {
                    totalTroops += troop;
                }

                if (totalTroops < 1)
                {
                    return false;
                }

                if (this.Targets == null || this.Targets.Count < 1)
                {
                    return false;
                }

                return true;
            }
        }

        private int MinimumDelay
        {
            get
            {
                int value = 0;
                if (this.resumeTime > DateTime.Now)
                {
                    value = (int)Math.Min(86400, (this.resumeTime - DateTime.Now).TotalSeconds);
                }

                return value;
            }
            set
            {
                DateTime newResumeTime = DateTime.Now.AddSeconds(value);
                if (newResumeTime > this.resumeTime)
                {
                    this.resumeTime = newResumeTime;
                }
            }
        }
        #endregion

        #region Methods
        #region IQueue Members
        public void Action()
        {
            if (this.MarkDeleted)
            {
                return;
            }

            DoRaidResult result = this.DoRaid();
            switch (result)
            {
                case DoRaidResult.Success:
                    this.NextTarget();
                    break;
                case DoRaidResult.Postpone:
                    this.UpCall.FetchVillageTroop(this.VillageID);
                    break;
                case DoRaidResult.SkipVillage:
                    this.UpCall.FetchVillageTroop(this.VillageID);
                    this.NextTarget();
                    break;
                case DoRaidResult.DeleteVillage:
                    this.Targets.RemoveAt(this.TargetID);
                    if (this.Targets.Count == 0)
                    {
                        this.Delete();
                    }
                    else
                    {
                        this.TargetID--;
                        this.NextTarget();
                    }
                    break;
                default:
                    throw new Exception(String.Format("Unhandled DoRaidResult {0}", result)); 
            }

            //this.MinimumDelay = this.RandomDelay(30, 120);
            this.MinimumDelay = 0;
        }
        #endregion

        public void CopySettings(RaidQueue settings)
        {
            this.UpCall.TD.Dirty = true;

            this.Troops = settings.Troops;
            this.Targets = settings.Targets;
            this.RaidType = settings.RaidType;
            this.SpyOption = settings.SpyOption;
            this.MaxCount = settings.MaxCount;
            this.MaxSlots = settings.MaxSlots;
            this.MultipeRaids = settings.MultipeRaids;
            this.Description = settings.Description;

            if (this.TargetID >= this.Targets.Count)
            {
                this.NextTarget();
            }
        }

        private enum DoRaidResult
        {
            Success,
            Postpone,
            SkipVillage,
            DeleteVillage,
        }

        private DoRaidResult DoRaid()
        {
            if (this.MinimumDelay > 0)
            {
                return DoRaidResult.Postpone;
            }

            string sendTroopsUrl = String.Format("a2b.php?z={0}", this.Targets[this.TargetID].Z);
            string sendTroopForm = UpCall.PageQuery(this.VillageID, sendTroopsUrl);
            if (sendTroopForm == null)
            {
                return DoRaidResult.SkipVillage;
            }

            if (!sendTroopForm.Contains("<form method=\"POST\" name=\"snd\" action=\"a2b.php\">"))
            {
                return DoRaidResult.SkipVillage;
            }
            
            if (!UpCall.NoAnimals(VillageID, Targets[this.TargetID].X, Targets[this.TargetID].Y))
            {
            	UpCall.DebugLog("跳过有野怪的绿洲", DebugLevel.II);
            	return DoRaidResult.SkipVillage;
            }

            Dictionary<string, string> postData = RaidQueue.GetHiddenInputValues(sendTroopForm);
            postData.Add("c", ((int)this.RaidType).ToString());
            postData.Add("x", this.Targets[this.TargetID].X.ToString());
            postData.Add("y", this.Targets[this.TargetID].Y.ToString());
            int[] maxTroops = RaidQueue.GetMaxTroops(sendTroopForm);
            for (int i = 0; i < this.Troops.Length; i++)
            {
                if (this.Troops[i] > maxTroops[i])
                {
                    return DoRaidResult.SkipVillage;
                }

                string troopKey = String.Format("t{0}", i + 1);
                string troopNumber = this.Troops[i] == 0 ? "" : this.Troops[i].ToString();
                postData.Add(troopKey, troopNumber);
            }

            string confirmUrl = "a2b.php";
            string confirmForm = UpCall.PageQuery(this.VillageID, confirmUrl, postData);
            if (confirmForm == null)
            {
                return DoRaidResult.SkipVillage;
            }

            Match errorMatch = Regex.Match(confirmForm, "<p class=\"error\">(.+)</p>");
            if (errorMatch.Success)
            {
                string error = String.Format(
                    "Delete village {0}. Error: {1}",
                    this.Targets[this.TargetID],
                    errorMatch.Groups[1].Value);
                this.UpCall.DebugLog(error, DebugLevel.W);
                return DoRaidResult.DeleteVillage;
            }

            if (!confirmForm.Contains("<form method=\"post\" action=\"a2b.php\">"))
            {
                return DoRaidResult.SkipVillage;
            }

            TimeSpan timeCost = RaidQueue.GetOneWayTimeCost(confirmForm);
            if (timeCost == TimeSpan.MinValue)
            {
                return DoRaidResult.SkipVillage;
            }

            postData = RaidQueue.GetHiddenInputValues(confirmForm);
            if (RaidQueue.HasRadioInput("spy", confirmForm))
            {
                postData.Add("spy", ((int)this.SpyOption).ToString());
            }

            string result = this.UpCall.PageQuery(this.VillageID, confirmUrl, postData);
            if (result == null)
            {
                return DoRaidResult.SkipVillage;
            }

            if (!this.MultipeRaids)
            {
                this.MinimumDelay = (int)timeCost.TotalSeconds * 2 + this.RandomDelay(30, 180);
            }

            TTInfo troopInfo = new TTInfo()
            {
                Tribe = this.UpCall.TD.Tribe,
                Troops = this.Troops
            };
            string message = String.Format(
                "{0} {1} ({2}) => {3} {4}",
                this.RaidType,
                this.UpCall.TD.Villages[this.VillageID].Coord,
                this.VillageID,
                this.Targets[this.TargetID],
                troopInfo.FriendlyName);

            this.UpCall.DebugLog(message, DebugLevel.I);
            return DoRaidResult.Success;
        }

        public static bool NoRaid2To7 { get; set; }
        private void Observe2To7Rule()
        {
            if (NoRaid2To7)
            {
                int hour = DateTime.Now.Hour;
                if (hour >= 2 && hour < 7)
                {
                    DateTime seven = DateTime.Today.AddHours(7);
                    this.MinimumDelay = (int)(seven - DateTime.Now).TotalSeconds;
                }
            }
        }

        static Random random = new Random();
        private int RandomDelay(int min, int max)
        {
            return RaidQueue.random.Next(min, max);
        }

        /// <summary>
        /// Move TargetID to the next village
        /// </summary>
        /// <returns>True if there are more villages to attack</returns>
        private void NextTarget()
        {
            this.UpCall.TD.Dirty = true;
            if (++this.TargetID >= this.Targets.Count)
            {
                this.TargetID = 0;
                if (++this.Count >= this.MaxCount && this.MaxCount != 0)
                {
                    this.Delete();
                }
            }
        }

        private void Delete()
        {
            this.MarkDeleted = true;
            this.UpCall.CallStatusUpdate(this, new Travian.StatusChanged() { ChangedData = Travian.ChangedType.Queue, VillageID = this.VillageID });
        }

        private static Regex hiddenInputPattern = new Regex(@"<input\s+type=""hidden""\s+name=""(\w+)""\s+value=""(.+?)""\s+/>");
        private static Dictionary<string, string> GetHiddenInputValues(string data)
        {
            Dictionary<string, string> values = new Dictionary<string, string>();
            MatchCollection matches = hiddenInputPattern.Matches(data);
            foreach (Match match in matches)
            {
                values.Add(match.Groups[1].Value, match.Groups[2].Value);
            }

            return values;
        }

        private static Regex maxTroopPattern = new Regex(@"document.snd.t(\d+).value=(\d+);");
        private static int[] GetMaxTroops(string sendTroopForm)
        {
            int[] maxTroops = new int[11];
            MatchCollection matches = maxTroopPattern.Matches(sendTroopForm);
            foreach (Match match in matches)
            {
                int troopIndex = Convert.ToInt32(match.Groups[1].Value);
                int maxTroop = Convert.ToInt32(match.Groups[2].Value);
                maxTroops[troopIndex - 1] = maxTroop;
            }

            return maxTroops;
        }

        private static Regex timeCostPattern = new Regex(@"<div class=""in"">\D*(\d+):(\d+):(\d+)\D*</div>");
        private static TimeSpan GetOneWayTimeCost(string confirmForm)
        {
            Match match = timeCostPattern.Match(confirmForm);
            if (match.Success)
            {
                int hours = Convert.ToInt32(match.Groups[1].Value);
                int minutes = Convert.ToInt32(match.Groups[2].Value);
                int seconds = Convert.ToInt32(match.Groups[3].Value);
                return new TimeSpan(hours, minutes, seconds);
            }

            return TimeSpan.MinValue;
        }

        private static Regex radioInputPattern = new Regex(@"<input class=""radio"" type=""radio"" name=""(\w+)"" value=""(\d+)"" (checked)? />");
        private static bool HasRadioInput(string name, string data)
        {
            MatchCollection matches = radioInputPattern.Matches(data);
            foreach (Match match in matches)
            {
                if (match.Groups[1].Value == name)
                {
                    return true;
                }
            }

            return false;
        }
        #endregion
    }
}
