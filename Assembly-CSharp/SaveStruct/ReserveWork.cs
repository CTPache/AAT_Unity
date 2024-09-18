using System;
using System.Runtime.InteropServices;

namespace SaveStruct
{
	[Serializable]
	public struct ReserveWork
	{
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 200)]
		public int[] reserve;

		public static void New(out ReserveWork reserve_work)
		{
			reserve_work = default(ReserveWork);
			reserve_work.reserve = new int[200];
		}

		public void CopyFrom(ReserveData src)
		{
			src.CopyTo(reserve);
		}

		public void CopyTo(ReserveData dest)
		{
			dest.CopyFrom(reserve);
		}
	}
}
