using System;
using System.Runtime.InteropServices;

namespace SaveStruct
{
	[Serializable]
	public struct GameReserveWork
	{
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 200)]
		public int[] reserve;

		public static void New(out GameReserveWork reserve_work)
		{
			reserve_work = default(GameReserveWork);
			reserve_work.reserve = new int[200];
		}

		public void CopyFrom(GameReserveData src)
		{
		}

		public void CopyTo(GameReserveData dest)
		{
		}
	}
}
