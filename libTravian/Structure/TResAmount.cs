using System;
using System.Collections.Generic;
using System.Text;
using LitJson;

namespace libTravian
{
	//	描述建筑资源量的类
	public class TResAmount
	{
		[Json]
		public int[] Resources;

		public TResAmount()
			: this(0, 0, 0, 0)
		{
		}

		public TResAmount(int r1, int r2, int r3, int r4)
			: this(new int[4] { r1, r2, r3, r4 })
		{
		}

		public TResAmount(int[] r)
		{
			this.Resources = r;
		}

		public TResAmount(TResAmount r)
		{
			this.Resources = new int[r.Resources.Length];
			for(int i = 0; i < this.Resources.Length; i++)
			{
				this.Resources[i] = r.Resources[i];
			}
		}

		//	4种资源总和的属性
		public int TotalAmount
		{
			get
			{
				int total = 0;
				for(int i = 0; i < this.Resources.Length; i++)
				{
					total += this.Resources[i];
				}

				return total;
			}
		}

		//	
		public double[] Proportions
		{
			get
			{
				double total = this.TotalAmount;
				double[] proportions = new double[this.Resources.Length];
				for(int i = 0; i < proportions.Length; i++)
				{
					proportions[i] = this.Resources[i] / total;
				}

				return proportions;
			}
		}

		public static TResAmount operator -(TResAmount r1, TResAmount r2)
		{
			int[] resources = new int[r1.Resources.Length];
			for(int i = 0; i < resources.Length; i++)
			{
				resources[i] = r1.Resources[i] - r2.Resources[i];
			}

			return new TResAmount(resources);
		}

		public static TResAmount operator +(TResAmount r1, TResAmount r2)
		{
			int[] resources = new int[r1.Resources.Length];
			for(int i = 0; i < resources.Length; i++)
			{
				resources[i] = r1.Resources[i] + r2.Resources[i];
			}

			return new TResAmount(resources);
		}

		public static TResAmount FromString(string s)
		{
			string[] values = s.Split('|');
			int[] resources = new int[values.Length];
			for(int i = 0; i < resources.Length; i++)
			{
				resources[i] = Int32.Parse(values[i]);
			}

			return new TResAmount(resources);
		}

		public override string ToString()
		{
			string rt = "";
			foreach(int x in Resources)
			{
				if(rt.Length != 0)
					rt += "|";
				rt += x.ToString();
			}
			return rt;
		}

		/// <summary>
		/// Convert all negative amounts to 0
		/// </summary>
		public void NoNegative()
		{
			for(int i = 0; i < this.Resources.Length; i++)
			{
				if(this.Resources[i] < 0)
				{
					this.Resources[i] = 0;
				}
			}
		}

		/// <summary>
		/// Set all resource amounts to 0
		/// </summary>
		public void Clear()
		{
			for(int i = 0; i < this.Resources.Length; i++)
			{
				this.Resources[i] = 0;
			}
		}

		/// <summary>
		/// Comparator for unit tests
		/// </summary>
		/// <param name="obj">Another resource amount to compare with</param>
		/// <returns>True if the two amounts are equal</returns>
		public override bool Equals(object obj)
		{
			TResAmount amount = obj as TResAmount;
			if(amount == null)
			{
				return false;
			}

			for(int i = 0; i < this.Resources.Length; i++)
			{
				if(this.Resources[i] != amount.Resources[i])
				{
					return false;
				}
			}

			return true;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public TResAmount Clone()
		{
			return (TResAmount)MemberwiseClone();
		}

		static public TResAmount operator * (TResAmount Res, int Time)
		{
			return new TResAmount(Res.Resources[0] * Time, Res.Resources[1] * Time, Res.Resources[2] * Time, Res.Resources[3] * Time);
		}
	}

}
