using System;
using System.Runtime.InteropServices;

namespace SaveStruct
{
	[Serializable]
	public struct SoundWork
	{
		public int playBgmNo;

		public int stopBgmNo;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
		public int[] se_no;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
		public bool[] pause_bgm;

		public static void New(out SoundWork sound_work)
		{
			sound_work = default(SoundWork);
			sound_work.se_no = new int[10];
			sound_work.pause_bgm = new bool[3];
		}

		public void CopyFrom(SoundSaveData src)
		{
			playBgmNo = soundCtrl.instance.playBgmNo;
			stopBgmNo = soundCtrl.instance.stopBgmNo;
			pause_bgm[0] = soundCtrl.instance.pause_bgm[0];
			pause_bgm[1] = soundCtrl.instance.pause_bgm[1];
			pause_bgm[2] = soundCtrl.instance.pause_bgm[2];
			for (int i = 0; i < se_no.Length; i++)
			{
				se_no[i] = soundCtrl.instance.se_no[i];
			}
		}

		public void CopyTo(SoundSaveData dest)
		{
			dest.playBgmNo = playBgmNo;
			dest.stopBgmNo = stopBgmNo;
			if (dest.se_no == null)
			{
				dest.se_no = new int[10];
			}
			if (dest.pause_bgm == null)
			{
				dest.pause_bgm = new bool[3];
			}
			Array.Copy(se_no, dest.se_no, se_no.Length);
			Array.Copy(pause_bgm, dest.pause_bgm, pause_bgm.Length);
		}
	}
}
