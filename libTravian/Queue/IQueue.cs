using System;
using System.Collections.Generic;
using System.Text;
using LitJson;

namespace libTravian
{
	public interface IQueue
	{
		/// <summary>
		/// UpCall for callback
		/// </summary>
		Travian UpCall { get; set; }

		/// <summary>
		/// Village ID
		/// </summary>
		[Json]
		int VillageID { get; set; }

		/// <summary>
		/// If this queue should be delete
		/// </summary>
		bool MarkDeleted { get; }

		/// <summary>
		/// When set to true, the corresponding task will be suspended
		/// </summary>
		[Json]
		bool Paused { get; set; }

		/// <summary>
		/// Brief information of the task
		/// </summary>
		string Title { get; }

		/// <summary>
		/// Detail information of the task
		/// </summary>
		string Status { get; }

		/// <summary>
		/// Seconds countdown to do the action
		/// </summary>
		int CountDown { get; }

		/// <summary>
		/// Do the action
		/// </summary>
		/// <returns></returns>
		void Action();

		int QueueGUID { get; }
	}
}
