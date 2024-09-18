using System;
using System.Runtime.InteropServices;

namespace SaveStruct
{
	[Serializable]
	public struct TrophyWork
	{
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 96)]
		public int[] message_flag_;

		public static void New(out TrophyWork trophy_work)
		{
			trophy_work = default(TrophyWork);
			trophy_work.message_flag_ = new int[96];
		}

		public void CopyFrom(TrophyData src)
		{
			TrophyCtrl.MessageFlagCopyToIntArray(message_flag_);
		}

		public void CopyTo(TrophyData dest)
		{
			TrophyCtrl.MessageFlagCopyFromIntArray(message_flag_);
		}
	}
}
