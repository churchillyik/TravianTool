using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using LitJson;

namespace libTravian
{
	public class NpcTradeQueue : IQueue
	{
		#region IQueue 成员

		public Travian UpCall { get; set; }

		[Json]
		public int VillageID { get; set; }

		[Json]
		public bool Paused { get; set; }

		[Json]
		public bool MarkDeleted { get; private set; }

		/// <summary>
		/// Task title for queue display (threashold->distribution)
		/// </summary>
		public string Title
		{
			get
			{
				return String.Format("{0}=>{1}%", this.Threshold, this.MinTradeRatio);
			}
		}

		public string Status
		{
			get
			{
				string maxCount = this.MaxCount == 0 ? "inf" : this.MaxCount.ToString();
				return String.Format("{0}/{1}=>{2}", this.Count, maxCount, this.Distribution);
			}
		}

		public int CountDown
		{
			get
			{
				if(!UpCall.TD.Villages.ContainsKey(VillageID))
				{
					return 86400;
				}

				TVillage village = UpCall.TD.Villages[VillageID];
				if(village.isBuildingInitialized != 2)
				{
					return 86400;
				}

				int sum = 0;
				for(int i = 0; i < village.Resource.Length; i++)
				{
					sum += village.Resource[i].CurrAmount;
				}

				if(this.RedistributeResources(UpCall.TD, VillageID, sum) == null)
				{
					return 86400;
				}

				int timecost = Math.Max(this.MinimumDelay, village.TimeCost(this.Threshold));

				return timecost;
			}
		}

		public void Action()
		{
			TVillage village = UpCall.TD.Villages[VillageID];

			if (!IsValid)
			{
				UpCall.DebugLog("Invalid NPC trade task discarded: " + ToString(), DebugLevel.W);
				RemoveQueuedTask();
				return;
			}

			if (CountDown > 0)
			{
				return;
			}

			NpcTradeResult returnCode = doNpcTrade();
			switch (returnCode)
			{
				case NpcTradeResult.Failure:
					UpCall.DebugLog("NPC trade task has failed: " + ToString(), DebugLevel.W);
					RemoveQueuedTask();
					return;

				case NpcTradeResult.Delay:
					// Wait at least 10 minutes before retrying
					MinimumDelay = 600;
					break;

				case NpcTradeResult.Success:
					Count++;
					if (MaxCount != 0 & Count >= MaxCount)
					{
						RemoveQueuedTask();
						return;
					}

					// This is an unfinished multiple NPC trade task, wait at least 1 hr
					MinimumDelay = 3600;
					break;
			}
		}
		#endregion

		public NpcTradeQueue()
		{
			this.Threshold = new TResAmount();
			this.Distribution = new TResAmount();
			this.MaxCount = 1;
			this.MinTradeRatio = 50;
		}

		/// <summary>
		/// Return value of doNpcTrade(int, TResAmount, TResAmount)
		/// </summary>
		public enum NpcTradeResult
		{
			/// <summary>
			/// The trade is complete
			/// </summary>
			Success,

			/// <summary>
			/// A parsing error has happened
			/// </summary>
			Failure,

			/// <summary>
			/// Some trading condition doesn't apply
			/// </summary>
			Delay
		}

		/// <summary>
		/// Trade resource with NPC with 1:1 rate (after paying 3 gold)
		/// </summary>
		public NpcTradeResult doNpcTrade()
		{
			// Get NPC trade form
			string result = UpCall.PageQuery(VillageID, "build.php?gid=17&t=3");
			if (result == null)
			{
				return NpcTradeResult.Failure;
			}

			// Parse capacity and sum
			Match match = Regex.Match(result, "var summe=(?<summe>\\d+);var max123=(?<max123>\\d+);var max4=(?<max4>\\d+);");
			if (!match.Success)
			{
				return NpcTradeResult.Failure;
			}

			int sum = Int32.Parse(match.Groups["summe"].Value);

			// Parse id
			match = Regex.Match(result, "<input type=\"hidden\" name=\"id\" value=\"(?<id>\\d+)\"");
			if (!match.Success)
			{
				return NpcTradeResult.Failure;
			}

			string id = match.Groups["id"].Value;

            // Parse c
            match = Regex.Match(result, "<input type=\"hidden\" name=\"c\" value=\"(?<c>[^>]*?)\"");
            if (!match.Success)
            {
                return NpcTradeResult.Failure;
            }

            string c = match.Groups["c"].Value;

			// Parse m1[] and m2[]
			MatchCollection matches = Regex.Matches(result, "<input type=\"hidden\" name=\"m1\\[\\]\" value=\"(?<m1>\\d+)\"");
			if (matches.Count != 4)
			{
				return NpcTradeResult.Failure;
			}

			TResAmount m1 = new TResAmount(new int[matches.Count]);
			for (int i = 0; i < matches.Count; i ++)
			{
				m1.Resources[i] = Int32.Parse(matches[i].Groups["m1"].Value);
			}

			// Does m1 exceeds threshold?
			for (int i = 0; i < m1.Resources.Length; i ++)
			{
				if (m1.Resources[i] < Threshold.Resources[i])
				{
					return NpcTradeResult.Delay;
				}
			}

			// Compute m2
			TResAmount m2 = RedistributeResources(UpCall.TD, VillageID, sum);
			if (m2 == null)
			{
				return NpcTradeResult.Delay;
			}

			// Prepare data
			Dictionary<string, string> postData = new Dictionary<string, string>();
			postData["id"] = id;
			postData["t"] = "3";
			postData["a"] = "6";
            postData["c"] = c;

			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < m2.Resources.Length; i++)
			{
				if (i > 0)
				{
					sb.Append("&");
				}

				sb.AppendFormat("m2[]={0}&m1[]={1}", m2.Resources[i], m1.Resources[i]);
			}

			postData["!!!RawData!!!"] = sb.ToString();

			// Post form
			result = UpCall.PageQuery(VillageID, "build.php", postData);
			if (result == null)
			{
				return NpcTradeResult.Failure;
			}

			match = Regex.Match(result, "<b>3</b>[^<]*?</p><script language=\"JavaScript\">var summe=");
			if (!match.Success)
			{
				return NpcTradeResult.Failure;
			}

			UpCall.DebugLog(string.Format("NPC trade {0} -> {1} ({2}) ", m1, m2, VillageID), DebugLevel.I);
			UpCall.BuildCount();
			return NpcTradeResult.Success;
		}

		/// <summary>
		/// Remove a item from the village task queue
		/// </summary>
		/// <param name="villageID">Which village the task queue belongs to</param>
		/// <param name="task">The item to delete from the task queue</param>
		private void RemoveQueuedTask()
		{
			MarkDeleted = true;
			UpCall.CallStatusUpdate(this, new Travian.StatusChanged() { ChangedData = Travian.ChangedType.Queue, VillageID = VillageID });
		}

		/// <summary>
		/// When can we retry the NPC trade task
		/// </summary>
		[Json]
		public DateTime resumeTime;

		/// <summary>
		/// Start NPC trading after resources has exceeded all thresholds, e.g.,
		/// 100|100|100|1800 exceeds 0|0|0|1000, but not 10|10|10|1000
		/// </summary>
		[Json]
		public TResAmount Threshold { get; set; }

		/// <summary>
		/// Target resource distribution ratio
		/// </summary>
		[Json]
		public TResAmount Distribution { get; set; }

		/// <summary>
		/// Maximum number of repeats (0 = inf)
		/// </summary>
		[Json]
		public int MaxCount { get; set; }

		/// <summary>
		/// Current number of repeats
		/// </summary>
		[Json]
		public int Count { get; set; }

		/// <summary>
		/// A percentage p: an NPC trade won't happen unless at least p%
		/// of resource has been transfered from one type to other type(s)
		/// </summary>
		[Json]
		public int MinTradeRatio { get; set; }

		/// <summary>
		/// Minimum seconds to wait until the mechant resturns
		/// </summary>
		public int MinimumDelay
		{
			get
			{
				int value = 0;
				if(this.resumeTime > DateTime.Now)
				{
					try
					{
						value = Convert.ToInt32((this.resumeTime - DateTime.Now).TotalSeconds);
					}
					catch(OverflowException)
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

		/// <summary>
		/// Distribute a total amount based on the distribution ratio
		/// </summary>
		public TResAmount RedistributeResources(Data travianData, int villageID, int sum)
		{
			if(!travianData.Villages.ContainsKey(villageID))
			{
				return null;
			}

			TVillage village = travianData.Villages[villageID];
			if(village.isBuildingInitialized != 2)
			{
				return null;
			}

			TResAmount target = new TResAmount(this.Distribution);
			TResAmount distribution = new TResAmount();

			// Allocate by proportion
			int residual = sum - distribution.TotalAmount;
			while(target.TotalAmount > 0 && residual > 10)
			{
				double[] proportions = target.Proportions;
				for(int i = 0; i < proportions.Length; i++)
				{
					distribution.Resources[i] += (int)(residual * proportions[i]);
					if(distribution.Resources[i] > village.Resource[i].Capacity)
					{
						distribution.Resources[i] = village.Resource[i].Capacity;
						target.Resources[i] = 0;
					}
				}

				residual = sum - distribution.TotalAmount;
			}

			// Don't trade if residual exceeds 50% threshold
			bool tooManyResidual = true;
			for(int i = 0; i < this.Threshold.Resources.Length; i++)
			{
				if(residual < this.Threshold.Resources[i] * (100 - this.MinTradeRatio) / 100)
				{
					tooManyResidual = false;
					break;
				}
			}

			if(tooManyResidual)
			{
				return null;
			}

			// Allocate by capacity
			for(int i = 0; i < distribution.Resources.Length; i++)
			{
				distribution.Resources[i] = Math.Min(
					distribution.Resources[i] + residual,
					village.Resource[i].Capacity);
				residual = sum - distribution.TotalAmount;
			}

			return distribution;
		}


		/// <summary>
		/// A valid task should have non-zero thresholds and non-zero distribution
		/// </summary>
		public bool IsValid
		{
			get { return this.Threshold.TotalAmount > 0 && this.Distribution.TotalAmount > 0; }
		}

		public int QueueGUID { get { return 8; } }
	}
}
