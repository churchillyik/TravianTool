using System;
using System.Collections.Generic;
using System.Text;
using LitJson;
using System.Text.RegularExpressions;

namespace libTravian
{
	public class ProduceTroopQueue : IQueue
	{
		#region IQueue 成员
		public Travian UpCall { get; set; }

		[Json]
		public int VillageID { get; set; }

		[Json]
		public bool Paused { get; set; }

		[Json]
		public int Aid { get; set; }

		[Json]
		public bool GRt { get; set; }

		[Json]
		public int Amount { get; set; }

		[Json]
		public bool MarkDeleted { get; private set; }

		public string Title
		{
			get
			{
				return string.Format("{0} {1} {2}", Amount, UpCall.GetAidLang(UpCall.TD.Tribe, Aid), GRt ? "GR" : "");
			}
		}

		public string Status
		{
			get
			{
				string count = this.Count.ToString() + "/";
				count += this.MaxCount == 0 ? "∞" : this.MaxCount.ToString();
				if(LastExec != DateTime.MinValue)
					return string.Format("{2}, Last: {0}, Next: After {1}", LastExec.ToShortTimeString(), NextExec.ToShortTimeString(), count);
				else
					return "";
			}
		}


		public int CountDown
		{
			get
			{
				var CV = UpCall.TD.Villages[VillageID];
				int key = (UpCall.TD.Tribe - 1) * 10 + Aid;
				int timecost;
                TResAmount TroopRes;
                TroopRes = Buildings.TroopCost[key] * Amount * (GRt ? 3 : 1);

                int[] adjustedResources = new int[TroopRes.Resources.Length];
                for (int i = 0; i < adjustedResources.Length; i++)
                {
                    adjustedResources[i] = TroopRes.Resources[i];
                    if (CV.Troop.ResLimit != null && adjustedResources[i] > 0)
                    {
                        adjustedResources[i] += CV.Troop.ResLimit.Resources[i];
                    }
                }

                timecost = CV.TimeCost(new TResAmount(adjustedResources));
				if (NextExec != DateTime.MinValue && NextExec > DateTime.Now)
					timecost = Math.Max(timecost, Convert.ToInt32(NextExec.Subtract(DateTime.Now).TotalSeconds));
				return timecost;
			}
		}

		public void Action()
		{
			if (CountDown > 0)
				return;
			int key = (UpCall.TD.Tribe - 1) * 10 + Aid;
			int Gid;
			Gid = GRt ? AIDMapg[key] : AIDMap[key];

			if (GRt && (Aid == 7 || Aid == 8) || Gid == 0)
			{
				UpCall.DebugLog("Not appropriate kind of troop to produce, deleted.", DebugLevel.W);
				MarkDeleted = true;
				return;
			}
			string url = "build.php?gid=" + Gid;
			if (Gid == 25)
			{
				url += "&s=1";
			}
			string data = UpCall.PageQuery(VillageID, url);
			if (data == null)
				return;
			if (!data.Contains(string.Format("name=\"t{0}\"", Aid)))
			{
				UpCall.DebugLog("Cannot produce this kind of troop before research it.", DebugLevel.W);
				MarkDeleted = true;
				return;
			}
			/*
			 * id	34
			 * z	10021
			 * a	2
			 * t1	2000
			 * t3	0
			 * s1.x	60
			 * s1.y	16
			 * s1	ok
			 * <input type="hidden" name="id" value="34">
			 * <input type="hidden" name="z" value="65535">
			 * <input type="hidden" name="a" value="2">

			 */
			string p_a, p_id, p_z;
			Match m;
			m = Regex.Match(data, "type=\"hidden\" name=\"id\" value=\"(\\d+?)\"");
			if (m.Success)
				p_id = m.Groups[1].Value;
			else
			{
				UpCall.DebugLog("Parse id error!", DebugLevel.F);
				MarkDeleted = true;
				return;
			}
			m = Regex.Match(data, "type=\"hidden\" name=\"z\" value=\"(\\w+?)\"");
			if (m.Success)
				p_z = m.Groups[1].Value;
			else
			{
				UpCall.DebugLog("Parse z error!", DebugLevel.F);
				MarkDeleted = true;
				return;
			}
			m = Regex.Match(data, "type=\"hidden\" name=\"a\" value=\"(\\d+?)\"");
			if (m.Success)
				p_a = m.Groups[1].Value;
			else
			{
				UpCall.DebugLog("Parse a error!", DebugLevel.F);
				MarkDeleted = true;
				return;
			}
			Dictionary<string, string> PostData = new Dictionary<string, string>();
			PostData["id"] = p_id;
			PostData["z"] = p_z;
			PostData["a"] = p_a;
			PostData["s1.x"] = rand.Next(10, 70).ToString();
			PostData["s1.y"] = rand.Next(3, 17).ToString();
			PostData["s1"] = "ok";
			PostData["t" + Aid] = Amount.ToString();
			data = UpCall.PageQuery(VillageID, "build.php", PostData);
			LastExec = DateTime.Now;
			NextExec = LastExec.AddSeconds(MinimumInterval);
			Count++;
			if (MaxCount != 0 && Count >= MaxCount)
				MarkDeleted = true;
		}

		public int QueueGUID { get { return 10; } }

		#endregion

		/// <summary>
		/// Map AID to GID
		/// </summary>
		private static readonly int[] AIDMap = new int[] { 0, 
		19, 19, 19, 20, 20, 20, 21, 21, 25, 25,
		19, 19, 19, 19, 20, 20, 21, 21, 25, 25,
		19, 19, 20, 20, 20, 20, 21, 21, 25, 25};

        private static readonly int[] AIDMapg = new int[] { 0, 
		29, 29, 29, 30, 30, 30, 21, 21, 26, 26,
		29, 29, 29, 29, 30, 30, 21, 21, 26, 26,
		29, 29, 30, 30, 30, 30, 21, 21, 26, 26};

		public DateTime LastExec = DateTime.MinValue;

		public DateTime NextExec = DateTime.MinValue;

		private Random rand = new Random();

		/// <summary>
		/// How many times left
		/// </summary>
		[Json]
		public int MaxCount { get; set; }

		/// <summary>
		/// How many transfers been done so far
		/// </summary>
		[Json]
		public int Count { get; set; }

		/// <summary>
		/// Minimum interval between two actions in seconds
		/// </summary>
		[Json]
		public int MinimumInterval { get; set; }
	}
}
