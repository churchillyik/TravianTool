using System;
using System.Collections.Generic;
using System.Text;
using LitJson;
using System.Net.Mail;
using System.Globalization;
using System.Text.RegularExpressions;

namespace libTravian
{
    public class AlarmQueue : IQueue
    {
        #region IQueue 成员

        public Travian UpCall
        {
            set;
            get;
        }

        [Json]
        public int VillageID
        {
            set;
            get;
        }

        [Json]
        public bool MarkDeleted
        {
            get;
            private set;
        }

        [Json]
        public bool Paused
        {
            set;
            get;
        }

        public string Title
        {
            get
            {
                if (To != null && To.Length > 0)
                    return To[0] + "...";
                return string.Empty;
            }
        }

        public string Status
        {
            get
            {
                return TotalCount.ToString();
            }
        }

        public int CountDown
        {
            get
            {
                if (!UpCall.TD.Villages.ContainsKey(VillageID))
                {
                    MarkDeleted = true;
                    return 86400;
                }

                int value = 0;
                if (this.resumeTime > DateTime.Now)
                {
                    try
                    {
                        value = Convert.ToInt32((this.resumeTime - DateTime.Now).TotalSeconds);
                    }
                    catch (OverflowException)
                    {
                    }
                }

                return value;
            }
        }

        public void Action()
        {
        	//	当前时刻还未到唤醒时刻
            if (MinimumDelay > 0)
                return;

            var cv = UpCall.TD.Villages[VillageID];

            if (!HasGetAll)
            {
                cv.InitializeTroop();
                InitAttackers();
            }


            foreach (TTInfo tt in cv.Troop.Troops)
            {
                AddAtacker(tt);
            }

            if (BeAttacked)
            {
                AnalizeAttacker();

                if (SendMail())
                {
                    TotalCount++;
                    this.MinimumDelay = this.MinimumInterval + new Random().Next(60, 300);
                }
                else
                {
                    this.MinimumDelay = new Random().Next(300, 600);
                }
            }
            else
                this.MinimumDelay = this.MinimumInterval + new Random().Next(60, 300);

            //	延迟时间需要超过最小时间间隔，唤醒时刻到达后才会重新刷新集结点
            HasGetAll = !(this.MinimumDelay > this.MinimumInterval);
        }

        public int QueueGUID
        {
            get { return 11; }
        }

        public bool BeAttacked
        {
            get
            {
                return Waves > 0;
            }
        }

        public int Waves
        {
            get
            {
                int i = 0;
                foreach (TAttacker at in attackers.Values)
                {
                    i += at.troops.Count;
                }
                return i;
            }
        }

        #endregion

        #region fields

        #region mail fields
        /// <summary>
        /// mail sender from:gmail only up to now
        /// </summary>
        [Json]
        public string From { set; get; }

        /// <summary>
        /// mail sender pass
        /// </summary>
        [Json]
        public string Password { set; get; }

        /// <summary>
        /// smtp server
        /// </summary>
        [Json]
        public string Host { set; get; }
		/// <summary>
        /// Enable ssl or not
        /// </summary>
        [Json]
        public bool SSLEnable { set; get; }
        /// <summary>
        /// smtp port: defalt = 25
        /// </summary>
        [Json]
        public int Port { set; get; }

        /// <summary>
        /// mail receivers
        /// </summary>
        [Json]
        public string[] To { set; get; }

        /// <summary>
        /// sms body. at most 350 for sms
        /// </summary>
        string SmsBody
        {
            get
            {
                string format = "{0},{1},{2}({3}) is under attack. totally {4} waves, the latest wave is at {5} from: {6}(7)";
                var cv = UpCall.TD.Villages[VillageID];
                return string.Format(format,
                    UpCall.TD.Server,
                    UpCall.TD.Username,
                    cv.Name,
                    cv.Coord.ToString(),
                    Waves,
                    LatestIncoming.FinishTime.ToString(CultureInfo.CurrentCulture),
                    LatestIncoming.Owner,
                    (TPoint)LatestIncoming.OwnerVillageZ);
            }
        }
        #endregion

        public DisplayLang dl 
        {
        	get
        	{
        		if (DisplayLang.Instance != null)
        			return DisplayLang.Instance;
        		
        		return new DisplayLang("cn");
        	}
        }

        bool HasGetAll = false;

        Dictionary<int, TAttacker> attackers = new Dictionary<int, TAttacker>();

        [Json]
        public string TrustfulUsers
        {
            set;
            get;
        }

        [Json]
        public int MinimumInterval { get; set; }

        public bool IsValid
        {
            get
            {
                string reg = @"^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)+$";
                if (string.IsNullOrEmpty(From) && new Regex(reg).IsMatch(From))
                    return false;

                if (string.IsNullOrEmpty(To.Join(",")))
                    return false;

                if (string.IsNullOrEmpty(Password))
                    return false;

                return true;
            }
        }

        public int MinimumDelay
        {
            get
            {
                int value = 0;
                if (this.resumeTime > DateTime.Now)
                {
                    try
                    {
                        value = Convert.ToInt32((this.resumeTime - DateTime.Now).TotalSeconds);
                    }
                    catch (OverflowException)
                    {
                    }
                }

                return value;
            }
            set
            {
                this.resumeTime = DateTime.Now.AddSeconds(value);
            }
        }

        int TotalCount = 0;
        DateTime resumeTime = DateTime.Now;
        TTInfo LatestIncoming = null;
        #endregion

        #region methods
        void AnalizeAttacker()
        {
            //not ready
        }

        bool IsAttackType(TTInfo troop, string attType)
        {
            if (troop.TroopType != TTroopType.Incoming)
                return false;

            bool result = false;
            var CV = UpCall.TD.Villages[VillageID];
            int index = troop.VillageName.IndexOf(CV.Name);
            string test_str = troop.VillageName.Remove(index, CV.Name.Length);
            
            if (index > 0)
            {
            	if (attType == "attack")
            	{
            		foreach (string atk_lang in dl.AtkLang)
            		{
            			if (test_str.Contains(atk_lang))
            			{
            				result = true;
            				break;
            			}
            		}
            	}
                else if (attType == "raid")
            	{
            		foreach (string raid_lang in dl.RaidLang)
            		{
            			if (test_str.Contains(raid_lang))
            			{
            				result = true;
            				break;
            			}
            		}
            	}
            }

            return result;
        }

        bool IsRaid(TTInfo troop)
        {
            return IsAttackType(troop, "raid");
        }

        bool IsAttack(TTInfo troop)
        {
            return IsAttackType(troop, "attack");
        }

        void AddAtacker(TTInfo troop)
        {
            if (!IsAttack(troop) && !IsRaid(troop))
                return;

            if (!attackers.ContainsKey(troop.OwnerVillageZ))
            {
                TAttacker att = ParseAttacker(troop);
                if (att != null)
                    attackers.Add(troop.OwnerVillageZ, att);
                else
                    return;
            }

            if (IsTrustful(attackers[troop.OwnerVillageZ].Name))
            {
                return;
            }

            attackers[troop.OwnerVillageZ].troops.Add(troop);
            if (LatestIncoming == null || troop.FinishTime < LatestIncoming.FinishTime)
                LatestIncoming = troop;
        }

        TAttacker ParseAttacker(TTInfo troop)
        {
            string data = UpCall.PageQuery(VillageID, troop.OwnerVillageUrl, null, true, true);
            if (string.IsNullOrEmpty(data))
                return null;

            string name = "", ally = "";
            int uid = 0, popu = 0;

            string pattern = "allianz\\.php\\?aid=\\d+\">(.*?)</a></td>"+
            	"[^<]*?</tr>[^<]*?<tr>[^<]*?<th>[^<]*?</th>[^<]*?<td><a href=\"spieler\\.php\\?uid=(\\d+)\">(.*?)</a></td>"+
            	"[^<]*?</tr>[^<]*?<tr>[^<]*?<th>[^<]*?</th>[^<]*?<td>(\\d+)</td>";
            Regex reg = new Regex(pattern);
            Match m = reg.Match(data);
            if (m.Success)
            {
                ally = m.Groups[1].Value;
                uid = int.Parse(m.Groups[2].Value);
                name = m.Groups[3].Value;
                popu = int.Parse(m.Groups[4].Value);
            }
            else
                return null;

            TPoint point = new TPoint();
            point.Z = troop.OwnerVillageZ;
            TAttacker attacker = new TAttacker
            {
                Point = point,
                Tribe = troop.Tribe,
                troops = new List<TTInfo>(),
                VileageName = troop.Owner,
                Ally = ally,
                Name = name,
                Uid = uid,
                Population = popu,
            };
            return attacker;
        }

        void InitAttackers()
        {
            foreach (TAttacker ta in attackers.Values)
            {
                if (ta.troops != null)
                {
                    ta.troops.Clear();
                }
                else
                    ta.troops = new List<TTInfo>();
            }
        }

        bool IsTrustful(string user)
        {
            if (string.IsNullOrEmpty(TrustfulUsers))
                return false;

            return TrustfulUsers.Contains(user);
        }

        bool SendMail()
        {
            MailMessage msg = new MailMessage();
            msg.To.Add(To.Join(","));
            msg.From = new MailAddress(From, UpCall.TD.Server, Encoding.UTF8);
            msg.Subject = string.Format("{0}@{1}", UpCall.TD.Server, UpCall.TD.Username);
            msg.SubjectEncoding = System.Text.Encoding.UTF8;
            msg.Body = this.SmsBody;
            msg.BodyEncoding = System.Text.Encoding.UTF8;
            msg.IsBodyHtml = false;
            msg.Priority = MailPriority.High;

            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential(From, Password);
            client.Port = this.Port;
            client.Host = this.Host;
            client.EnableSsl = this.SSLEnable;
            object userState = msg;
            try
            {
                client.SendAsync(msg, userState);
                UpCall.DebugLog("已发送警报邮件:\r\n" + this.SmsBody, DebugLevel.II);
                return true;
            }
            catch (System.Net.Mail.SmtpException ex)
            {
                UpCall.DebugLog(ex);
                return false;
            }
        }
        #endregion
    }
}

