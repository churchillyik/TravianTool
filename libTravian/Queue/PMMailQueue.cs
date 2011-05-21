/*
 * Created by SharpDevelop.
 * User: Administrator
 * Date: 2011-5-16
 * Time: 9:55
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Text;
using LitJson;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading;
using System.ComponentModel;
using System.Globalization;
using System.Text.RegularExpressions;

namespace libTravian
{
	public class PMInfo
	{
		public string _subject;
		public string _content;
		public string _date;
	}
	
	public class PMSender
	{
		public int _uid;
		public string _name;
		public List<PMInfo> PMInfoList = new List<PMInfo>();
	}
	
	public class PMMailQueue : IQueue
	{
		#region IQueue 成员

		public Travian UpCall { get; set; }

		[Json]
		public int VillageID { get; set; }

		[Json]
		public bool Paused { get; set; }

		[Json]
		public bool MarkDeleted { get; private set; }

		public string Title
		{
			get 
			{ 
				if (To != null && To.Length > 0)
                    return "PM电邮到" + To[0];
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
			if (MinimumDelay > 0)
                return;
			
			int page = 1;
			string link, data;
			int result = -1;
			bool mail_sent = false;
			ClearPMSender();
			do
			{
				link = "nachrichten.php?&o=0&page=" + page.ToString();
				data = UpCall.PageQuery(VillageID, link);
				result = PMParse(data);
				if (result == 1)
				{
					page++;
				}
				
				if (result != -1 && SendMail() == true && mail_sent == false)
				{
					mail_sent = true;
				}
				
			} while (result == 1);
			
			if (mail_sent)
			{
				TotalCount++;
                this.MinimumDelay = this.MinimumInterval + new Random().Next(0, 60);
			}
			else
			{
				this.MinimumDelay = new Random().Next(60, 120);
			}
		}

		public int QueueGUID { get { return 12; } }
		
		#endregion
        
		#region mail fields
        [Json]
        public string From { set; get; }

        [Json]
        public string Password { set; get; }

        [Json]
        public string Host { set; get; }

        [Json]
        public bool SSLEnable { set; get; }

        [Json]
        public int Port { set; get; }

        [Json]
        public string[] To { set; get; }
        
		[Json]
        public int MinimumInterval { get; set; }
        
        int TotalCount = 0;
        DateTime resumeTime = DateTime.Now;
        
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
        
        string SmsBody
        {
            get
            {
                return "";
            }
        }
        #endregion
        
		public PMMailQueue()
		{
		}
		
		Dictionary<int, PMSender> DicPMSender = new Dictionary<int, PMSender>();
		private void ClearPMSender()
		{
			foreach (PMSender sender in DicPMSender.Values)
			{
				sender.PMInfoList.Clear();
			}
			DicPMSender.Clear();
		}
		
		private int PMParse(string data)
		{
			string strPMTitles = HtmlUtility.GetElement(data, "tbody");
			if (strPMTitles == null)
				return -1;
			string[] strPMTitleList = HtmlUtility.GetElements(strPMTitles, "tr");
			int len = strPMTitleList.Length;
			int cnt = 0;
			foreach (string eachPMTitle in strPMTitleList)
			{
				Match m = Regex.Match(
					eachPMTitle, "class=\"status \" title=\"unread\" alt=\"unread\" />");
				if (!m.Success)
					continue;
				
				m = Regex.Match(
					eachPMTitle, "<a href=\"spieler\\.php\\?uid=(\\d+)\">([^<]*?)</a>");
				if (!m.Success)
					continue;
				int uid = Convert.ToInt32(m.Groups[1].Value);
				string name = m.Groups[2].Value;
				if (!DicPMSender.ContainsKey(uid))
				{
					PMSender sender = new PMSender
					{
						_uid = uid,
						_name = name
					};
					DicPMSender.Add(uid, sender);
				}
				
				m = Regex.Match(
					eachPMTitle, "<a href=\"(nachrichten\\.php\\?id=\\d+)\">([^<]*?)</a>");
				if (!m.Success)
					continue;
				string link = m.Groups[1].Value;
				string subject = m.Groups[2].Value.Trim();
				
				m = Regex.Match(
					eachPMTitle, "<td class=\"dat\">([^<]*?)</td>");
				if (!m.Success)
					continue;
				string date = m.Groups[1].Value;
				
				PMInfo info = new PMInfo
				{
					_subject = subject,
					_date = date
				};
				
				string content = UpCall.PageQuery(VillageID, link);
				m = Regex.Match(content, @"<div id=""message"">(.+?)</div>", RegexOptions.Singleline);
				if (!m.Success)
					continue;
				info._content = m.Groups[1].Value;
				DicPMSender[uid].PMInfoList.Add(info);
				cnt++;
			}
			
			if (cnt > 0 && cnt == len)
				return 1;
			else if (cnt > 0 && cnt < len)
				return 0;
			else
				return -1;
		}
		
		private bool SendMail()
        {
            MailMessage msg = new MailMessage();
            msg.From = new MailAddress(From, UpCall.TD.Server, System.Text.Encoding.UTF8);
            msg.To.Add(To.Join(","));
            msg.Subject = string.Format("{0}@{1}", UpCall.TD.Server, UpCall.TD.Username);
            msg.SubjectEncoding = System.Text.Encoding.UTF8;
            msg.Body = this.SmsBody;
            msg.BodyEncoding = System.Text.Encoding.UTF8;
            msg.IsBodyHtml = false;
            msg.Priority = MailPriority.High;

            SmtpClient client = new SmtpClient(this.Host);
            client.Credentials = new System.Net.NetworkCredential(From, Password);
            client.Port = this.Port;
            client.EnableSsl = this.SSLEnable;

            try
            {
                client.Send(msg);
                UpCall.DebugLog("Message sent.", DebugLevel.II);
                return true;
            }
            catch (System.Net.Mail.SmtpException ex)
            {
                UpCall.DebugLog(ex);
                return false;
            }
        }
	}
}
