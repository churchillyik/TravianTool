using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using LitJson;
using System.Diagnostics;

namespace libTravian
{
	public class TransferQueue : IQueue
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
				string pos = this.TargetPos.ToString();
				if(UpCall.TD != null && UpCall.TD.Villages.ContainsKey(this.TargetVillageID))
				{
					pos = pos + " " + UpCall.TD.Villages[this.TargetVillageID].Name;
				}

				return pos;
			}
		}

		public string Status
		{
			get
			{
				if(MaxCount != 0 && Count >= MaxCount)
					RemoveQueuedTask();
				string count = this.Count.ToString() + "/";
				string mask = (this.NoCrop ? "NC " : "") + (this.ForceGo ? "FG " : "");
				count += this.MaxCount == 0 ? "∞" : this.MaxCount.ToString();
				CalculateResourceAmount(UpCall.TD, VillageID);
				string limit_rate = "(Tg" + this.LimitRate + "%)";
				return mask + count + DistributionShortName[(int)this.Distribution]
					+ this.ResourceAmount.ToString() + limit_rate;
			}
		}

		public int CountDown
		{
			get
			{
				CalculateResourceAmount(UpCall.TD, VillageID);
				if(!UpCall.TD.Villages.ContainsKey(VillageID))
				{
					MarkDeleted = true;
					return 86400;
				}

				TVillage village = UpCall.TD.Villages[VillageID];
				if(village.isBuildingInitialized != 2)
				{
					return 86400;
				}

				int[] adjustedResources = new int[this.ResourceAmount.Resources.Length];
				for(int i = 0; i < adjustedResources.Length; i++)
				{
					adjustedResources[i] = this.ResourceAmount.Resources[i];
					if(village.Market.LowerLimit != null && adjustedResources[i] > 0)
					{
						adjustedResources[i] += village.Market.LowerLimit.Resources[i];
					}
				}

				int timecost = Math.Max(this.MinimumDelay, village.TimeCost(new TResAmount(adjustedResources)));
				if(this.ExceedTargetCapacity(UpCall.TD))
				{
					timecost = Math.Max(timecost, 86400);
				}

				if(this.ResourceAmount.TotalAmount > village.Market.SingleCarry * village.Market.ActiveMerchant)
				{
					timecost = Math.Max(timecost, village.Market.MinimumDelay + 5);
				}

				return timecost;
			}
		}

		/// <summary>
		/// Wrapper of the real doTransfer function, which
		/// 1) Verify that the tranfer amount is valid (non-zero)
		/// 2) Verify that merchant dispatched for the same mission has returned.
		/// 3) Recalculate the transfer amount for dynamic resource balance
		/// 4) Verify that the transfer won't overflow the arrival village's warehouse/granary
		/// 5) Update task status after a successful merchan dispatch
		/// 6) Remove the task from the village queue when it's no longer valid/needed
		/// </summary>
		public void Action()
		{
			if(!IsValid)
			{
				UpCall.DebugLog("Invalid transfer task discarded: " + Title, DebugLevel.W);
				this.RemoveQueuedTask();
				return;
			}

			if(MinimumDelay > 0)
			{
				return;
			}

			TResAmount toTransfer = new TResAmount(ResourceAmount);

			if(TargetVillageID != 0 &&
				UpCall.TD.Villages.ContainsKey(TargetVillageID) &&
				UpCall.TD.Villages[TargetVillageID].isBuildingInitialized == 2)
			{
				UpCall.PageQuery(TargetVillageID, "build.php?gid=17");
				CalculateResourceAmount(UpCall.TD, VillageID);
				
				// check if it's a crop transfer, and crop is seriously needed from target village:
				if (UpCall.TD.Villages[TargetVillageID].Resource[3].Produce < 0)
				{
					var temp = NeedCrop(UpCall.TD);
					if(temp != null)
					{
						UpCall.DebugLog("NeedCrop rule on use. Force crop transfer.", DebugLevel.I);
						toTransfer = temp;
					}
					else if(ExceedTargetCapacity(UpCall.TD))
					{
						return;
					}
				}
			}
			else
			{
				CalculateResourceAmount(UpCall.TD, VillageID);
			}

			int timeCost = doTransfer(toTransfer, TargetPos);
			if(timeCost >= 0)
			{
				var CV = UpCall.TD.Villages[VillageID];
				MinimumDelay = Math.Max(MinimumInterval, timeCost * 2 + 30);
				Count++;
				if(MaxCount != 0 && Count >= MaxCount)
					RemoveQueuedTask();
			}
		}

		#endregion

		public TransferQueue()
		{
			this.resumeTime = DateTime.MinValue;
			this.ResourceAmount = new TResAmount(0, 0, 0, 0);
		}

		int retrycount = 0;

		/// <summary>
		/// Dispatch a transportation of a given amount of resource from one village to a given destiantion
		/// </summary>
		/// <param name="VillageID">Unique ID of the departure village</param>
		/// <param name="Amount">Amounts of resources to transport</param>
		/// <param name="TargetPos">Position of the arrival village</param>
		/// <returns>Error return minus number. Succeed return single way transfer time cost.</returns>
		public int doTransfer(TResAmount Amount, TPoint TargetPos)
		{
			string result = UpCall.PageQuery(VillageID, "build.php?gid=17");
			if(result == null)
				return -1;
			var CV = UpCall.TD.Villages[VillageID];
			Dictionary<string, string> PostData = new Dictionary<string, string>();
			var m = Regex.Match(result, "name=\"id\" value=\"(\\d+)\"");
			if(!m.Success)
				return -1;
			PostData["id"] = m.Groups[1].Value;

			if(result.Contains("Popup(2,5)") && Amount.TotalAmount > CV.Market.SingleCarry * CV.Market.ActiveMerchant)
			{
				resumeTime = DateTime.Now.AddSeconds(rand.Next(200 + retrycount * 20, 300 + retrycount * 30));
				UpCall.DebugLog("0:00:0?, Will retry...", DebugLevel.I);
				return -2;
			}
			if(Amount.TotalAmount > CV.Market.SingleCarry * CV.Market.ActiveMerchant)
			{
				retrycount++;
				if(retrycount > 5)
				{
					UpCall.DebugLog(string.Format("Transfer cannot go on: MCarry({0}) * MCount({1}) < Amount({2})", CV.Market.SingleCarry, CV.Market.ActiveMerchant, Amount.TotalAmount), DebugLevel.W);
					return -2; // Beyond transfer ability
				}
				else
				{
					UpCall.DebugLog("Error on 'ActiveMerchant'! Will retry...", DebugLevel.I);
					resumeTime = DateTime.Now.AddSeconds(rand.Next(500 + retrycount * 20, 800 + retrycount * 30));
					CV.Market.ActiveMerchant = Math.Min(Amount.TotalAmount / CV.Market.SingleCarry + 1, CV.Market.MaxMerchant);
					return -2;
				}
			}
			retrycount = 0;
			for(int i = 0; i < 4; i++)
			{
				PostData["r" + (i + 1).ToString()] = Amount.Resources[i].ToString();
			}

			PostData["dname"] = "";
			PostData["x"] = TargetPos.X.ToString();
			PostData["y"] = TargetPos.Y.ToString();
			PostData["s1.x"] = "0";
			PostData["s1.y"] = "0";

			result = UpCall.PageQuery(VillageID, "build.php", PostData);

			if(result == null)
				return -1;
			PostData.Clear();
			MatchCollection matches = Regex.Matches(result, "name=\"(\\w+)\" value=\"(\\w+)\"");
			for (int i = 0; i < matches.Count; i ++)
            {
				PostData[matches[i].Groups[1].Value] = matches[i].Groups[2].Value;
            }
			for(int i = 0; i < 4; i++)
			{
				PostData["r" + (i + 1).ToString()] = Amount.Resources[i].ToString();
			}
			PostData["s1.x"] = "0";
			PostData["s1.y"] = "0";
			m = Regex.Match(result, "<td>([0-9:]{6,})</td>");
			if(!m.Success)
				return -1; // Parse error!
			int TimeCost = Convert.ToInt32(UpCall.TimeSpanParse(m.Groups[1].Value).TotalSeconds);
			if(UpCall.TD.MarketSpeed != 0)
			{
				// calc market speed
				var distance = CV.Coord * TargetPos;
				UpCall.TD.Dirty = true;
				UpCall.TD.MarketSpeed = Convert.ToInt32(Math.Round(distance * 3600 / TimeCost));
			}

			result = UpCall.PageQuery(VillageID, "build.php", PostData);
			UpCall.BuildCount();

			// write data into target village if it's my village.
			foreach(var x in UpCall.TD.Villages)
			{
				if(x.Value == CV)
					continue;
				if(x.Value.Coord == TargetPos)
				{
					if(x.Value.isBuildingInitialized == 2)
						x.Value.Market.MarketInfo.Add(new TMInfo()
						{
							CarryAmount = Amount.Clone(),
							Coord = CV.Coord,
							FinishTime = DateTime.Now.AddSeconds(TimeCost),
							MType = TMType.OtherCome,
							VillageName = CV.Name
						});
					break;
				}
			}
			UpCall.DebugLog(string.Format("Transfer {0}({1}) => {2} {3}", CV.Coord.ToString(), VillageID, TargetPos.ToString(),
				Amount.ToString()), DebugLevel.I);
			return TimeCost;
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
		/// Short names for distribution type None, Source and Destination
		/// </summary>
		private static readonly string[] DistributionShortName = new string[] { "=>", "=>", "=S=>", "=T=>", "=SS>" };

		/// <summary>
		/// When the mechant (occupied by the previous transfer) will return
		/// </summary>
		//[Json]
		public DateTime resumeTime;

		/// <summary>
		/// The destination village (where the resource is going)
		/// </summary>
		[Json]
		public int TargetVillageID { get; set; }

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
		/// Resource distribution options
		/// </summary>
		[Json]
		public ResourceDistributionType Distribution { get; set; }

		/// <summary>
		/// Do not transport
		/// </summary>
		[Json]
		public bool NoCrop { get; set; }

		/// <summary>
		/// Destination village
		/// </summary>
		[Json]
		public TPoint TargetPos { get; set; }

		/// <summary>
		/// Resource amount
		/// </summary>
		[Json]
		public TResAmount ResourceAmount { get; set; }

		/// <summary>
		/// Minimum interval between two consequential transfers in seconds
		/// </summary>
		[Json]
		public int MinimumInterval { get; set; }

		/// <summary>
		/// Limit Rate of the amount for every kind of resources
		/// </summary>
		[Json]
		public int LimitRate { get; set; }
		
		/// <summary>
		/// Return false if the total resource amount is 0
		/// </summary>
		public bool IsValid
		{
			get { return !this.TargetPos.IsEmpty && this.ResourceAmount.TotalAmount > 0; }
		}

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
		/// Test if the target village have enought storage capacity for the to-be-transfered resources
		/// </summary>
		/// <param name="travianData">User game info, including target village distance and storage capacity</param>
		/// <returns>True if the transportation will overflow the target village</returns>
		public bool ExceedTargetCapacity(Data travianData)
		{
			if(ForceGo)
				return false;
			TResAmount targetCapacity = this.GetTargetCapacity(travianData, VillageID);
			if(targetCapacity == null)
			{
				if(this.Distribution == ResourceDistributionType.BalanceTarget)
				{
					return true;
				}
			}
			else
			{
				for(int i = 0; i < targetCapacity.Resources.Length; i++)
				{
					if(this.ResourceAmount.Resources[i] > targetCapacity.Resources[i])
					{
						return true;
					}
				}
			}

			return false;
		}
		
		[Obsolete("Don't use this before fixing")]
		private TResAmount NeedCrop(Data travianData)
		{
			return null;
			if(NoCrop)
				return null;
			if(Distribution == ResourceDistributionType.Uniform ||
				Distribution == ResourceDistributionType.BalanceSource ||
				Distribution == ResourceDistributionType.BalanceSourceTime)
				return null;
			if(Distribution == ResourceDistributionType.None &&
				ResourceAmount.Resources[3] <= 0)
				return null;
			if(travianData == null ||
			!travianData.Villages.ContainsKey(TargetVillageID) ||
			!travianData.Villages.ContainsKey(VillageID))
				return null;

			TVillage source = travianData.Villages[VillageID];
			TVillage destination = travianData.Villages[TargetVillageID];
			if(destination.isBuildingInitialized != 2)
				return null;

			if(destination.Resource[3].Produce >= 0)
			{
				if(UpCall != null)
					UpCall.DebugLog("Target Produce >= 0, no need crop rule.", DebugLevel.I);
				return null;
			}

			int speed = travianData.MarketSpeed == 0 ? 24 : travianData.MarketSpeed;
			int timecost = Convert.ToInt32(source.Coord * destination.Coord * 3600 / speed) + 30;

			int cropcap = destination.Resource[3].CurrAmount + timecost * destination.Resource[3].Produce / 3600;

			foreach(TMInfo transfer in destination.Market.MarketInfo)
			{
				if(transfer.MType == TMType.OtherCome && transfer.FinishTime < DateTime.Now.AddSeconds(timecost))
				{
					cropcap += transfer.CarryAmount.Resources[3];
				}
			}

			if(cropcap <= 0)
			{
				return new TResAmount(0, 0, 0, ResourceAmount.TotalAmount);
			}
			if(UpCall != null)
				UpCall.DebugLog("Target village don't need crop, no need crop rule.", DebugLevel.I);

			return null;
		}

		/// <summary>
		/// Distribute transported resource amount 
		/// </summary>
		/// <param name="travianData">Game info of the current user</param>
		public void CalculateResourceAmount(Data travianData, int VillageID)
		{
			switch(this.Distribution)
			{
				case ResourceDistributionType.None:
					break;
				case ResourceDistributionType.Uniform:
					this.EvenlyDistibuteResource();
					break;
				case ResourceDistributionType.BalanceSource:
					this.BalanceSourceResource(travianData, VillageID);
					break;
				case ResourceDistributionType.BalanceTarget:
					this.BalanceDestinationResource(travianData, VillageID);
					break;
				case ResourceDistributionType.BalanceSourceTime:
					this.BalanceSourceTime(travianData, VillageID);
					break;

			}
		}

		[Obsolete("Not implemented well")]
		private void BalanceSourceTime(Data travianData, int VillageID)
		{
			// TODO: FIXME
			int total = ResourceAmount.TotalAmount;
			int slots = NoCrop ? 3 : 4;
			TResAmount targetAmount = new TResAmount(0, 0, 0, 0);
			if(travianData != null &&
				travianData.Villages.ContainsKey(VillageID) &&
				travianData.Villages[VillageID].isBuildingInitialized == 2)
			{
				TVillage TV = travianData.Villages[VillageID];
				int[] fulltime = new int[slots];
				int maxtime = 0, total2 = 0, totalproduce = 0, total3 = 0;

				for(int i = 0; i < fulltime.Length; i++)
				{
					fulltime[i] = Convert.ToInt32(TV.Resource[i].LeftTime.TotalSeconds);

					maxtime = Math.Max(maxtime, fulltime[i]);
				}

				for(int i = 0; i < fulltime.Length; i++)
				{
					targetAmount.Resources[i] = Convert.ToInt32(Convert.ToInt64(TV.Resource[i].Produce) * (maxtime - fulltime[i]) / 3600);

					total2 += targetAmount.Resources[i];
					totalproduce += TV.Resource[i].Produce;
				}
				if(total2 > total)
				{
					for(int i = 0; i < fulltime.Length; i++)
					{
						targetAmount.Resources[i] = Convert.ToInt32(targetAmount.Resources[i] * Convert.ToDouble(total) / total2);
						total3 += targetAmount.Resources[i];
					}
				}
				else
				{

					double seconds = Convert.ToDouble(total - total2) / totalproduce;
					for(int i = 0; i < fulltime.Length; i++)
					{
						targetAmount.Resources[i] += Convert.ToInt32(TV.Resource[i].Produce * seconds);
						total3 += targetAmount.Resources[i];
					}
				}
				targetAmount.Resources[slots - 1] += total - total3;
				targetAmount.NoNegative();

			}
			ResourceAmount = targetAmount;
		}

		private void EvenlyDistibuteResource()
		{
			int total = this.ResourceAmount.TotalAmount;
			int slots = this.NoCrop ? 3 : 4;

			this.ResourceAmount = new TResAmount(0, 0, 0, 0);
			for(int i = 0; i < slots; i++)
			{
				this.ResourceAmount.Resources[i] = total / slots;
			}

			this.ResourceAmount.Resources[0] += total - this.ResourceAmount.TotalAmount;
		}

		private void BalanceSourceResource(Data travianData, int VillageID)
		{
			TResAmount targetAmount = new TResAmount(0, 0, 0, 0);
			if(travianData != null &&
				travianData.Villages.ContainsKey(VillageID) &&
				travianData.Villages[VillageID].isBuildingInitialized == 2)
			{
				TVillage village = travianData.Villages[VillageID];
				for(int i = 0; i < targetAmount.Resources.Length; i++)
				{
					targetAmount.Resources[i] = village.Resource[i].CurrAmount;
					if(village.Market.LowerLimit != null)
					{
						targetAmount.Resources[i] -= village.Market.LowerLimit.Resources[i];
					}
				}

				targetAmount.NoNegative();
			}

			this.DoBalance(targetAmount);
		}

		private void BalanceDestinationResource(Data travianData, int VillageID)
		{
			TResAmount targetAmount = this.GetTargetCapacity(travianData, VillageID);
			if(targetAmount == null)
			{
				targetAmount = new TResAmount(0, 0, 0, 0);
			}

			this.DoBalance(targetAmount);
		}

		/// <summary>
		/// Estimate the target village capacity when transportantion arrives, based on 
		/// its current resource amount, production rate, distance, and merchant speed.
		/// </summary>
		/// <param name="travianData">Contains game info</param>
		/// <param name="VillageID">Where the merchant starts, for computing distance</param>
		/// <returns>Estimated capacity</returns>
		private TResAmount GetTargetCapacity(Data travianData, int VillageID)
		{
			if(travianData != null &&
				travianData.Villages.ContainsKey(this.TargetVillageID) &&
				travianData.Villages.ContainsKey(VillageID))
			{
				TVillage source = travianData.Villages[VillageID];
				TVillage destination = travianData.Villages[this.TargetVillageID];
				if(destination.isBuildingInitialized == 2)
				{
					TResource[] VR = destination.Resource;
					int[] resources = new int[VR.Length];
					int speed = travianData.MarketSpeed == 0 ? 24 : travianData.MarketSpeed;
					double timecost = source.Coord * destination.Coord / speed;
					for(int i = 0; i < resources.Length; i++)
					{
						resources[i] = VR[i].Capacity * this.LimitRate / 100;
						if(destination.Market.UpperLimit != null)
						{
							resources[i] = destination.Market.UpperLimit.Resources[i];
						}
						
						resources[i] -= VR[i].CurrAmount + (int)(VR[i].Produce * timecost);
					}

					TResAmount capacity = new TResAmount(resources);
					foreach(TMInfo transfer in destination.Market.MarketInfo)
					{
						if(transfer.MType == TMType.OtherCome)
						{
							capacity -= transfer.CarryAmount;
						}
					}

					capacity.NoNegative();
					return capacity;
				}
			}

			return null;
		}

		private void DoBalance(TResAmount targetAmount)
		{
			int total = this.ResourceAmount.TotalAmount;
			int slots = this.NoCrop ? 3 : 4;

			// Sort targetAmount by desc order
			int[] ranks = new int[] { 0, 1, 2, 3 };
			for(int i = 0; i < slots - 1; i++)
			{
				for(int j = i + 1; j < slots; j++)
				{
					if(targetAmount.Resources[ranks[i]] < targetAmount.Resources[ranks[j]])
					{
						int temp = ranks[i];
						ranks[i] = ranks[j];
						ranks[j] = temp;
					}
				}
			}

			// Allocate by rank
			this.ResourceAmount.Clear();
			for(int i = 1; i < slots; i++)
			{
				int inc = targetAmount.Resources[ranks[i - 1]] - targetAmount.Resources[ranks[i]];
				if(total < this.ResourceAmount.TotalAmount + inc * i)
				{
					inc = (total - this.ResourceAmount.TotalAmount) / i;
				}

				for(int j = 0; j < i; j++)
				{
					this.ResourceAmount.Resources[ranks[j]] += inc;
				}
			}

			// Allocate remaining resources and round up with unit of 50
			int bonus = (total - this.ResourceAmount.TotalAmount) / slots;
			for(int i = 0; i < slots; i++)
			{
				this.ResourceAmount.Resources[i] += bonus;
				this.ResourceAmount.Resources[i] = (this.ResourceAmount.Resources[i] / 50) * 50;
			}

			// If we have anything left, give it to a lucky guy
			int luckyOne = ranks[0];
			this.ResourceAmount.Resources[luckyOne] += total - this.ResourceAmount.TotalAmount;
		}

		private Random rand = new Random();

		public int QueueGUID { get { return 7; } }

		[Json]
		public bool ForceGo { get; set; }
	}
	/// <summary>
	/// Automatic resource balance flavors in resource transportations
	/// </summary>
	public enum ResourceDistributionType
	{
		/// <summary>
		/// Always transport fixed amounts of resources
		/// </summary>
		None = 0,

		/// <summary>
		/// Distribute the same amount of resource among all categories
		/// </summary>
		Uniform,

		/// <summary>
		/// Evenly distribute the source village's remaining resources
		/// </summary>
		BalanceSource,

		/// <summary>
		/// Evenly distribute destination village's resources wrt storage capacity
		/// </summary>
		BalanceTarget,

		/// <summary>
		/// Same as BalanceSource except use remaining time instead of remaining space
		/// </summary>
		BalanceSourceTime
	}

}
