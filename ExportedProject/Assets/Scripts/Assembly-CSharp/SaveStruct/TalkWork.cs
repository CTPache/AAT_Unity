using System;
using System.Runtime.InteropServices;

namespace SaveStruct
{
	[Serializable]
	public struct TalkWork
	{
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
		public TALK_DATA[] talk_data;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)]
		public uint[] flag;

		public static void New(out TalkWork talk_work)
		{
			talk_work = default(TalkWork);
			talk_work.talk_data = new TALK_DATA[32];
			for (int i = 0; i < talk_work.talk_data.Length; i++)
			{
				TALK_DATA.New(out talk_work.talk_data[i]);
			}
			talk_work.flag = new uint[128];
		}

		public void CopyFrom(global::TalkWork src)
		{
			for (int i = 0; i < talk_data.Length; i++)
			{
				talk_data[i].CopyFrom(src.talk_data_[i]);
			}
			for (int j = 0; j < 32; j++)
			{
				for (int k = 0; k < 4; k++)
				{
					flag[j * 4 + k] = src.talk_data_[j].flag[k];
				}
			}
		}

		public void CopyTo(global::TalkWork dest)
		{
			if (talk_data == null)
			{
				talk_data = new TALK_DATA[32];
			}
			for (int i = 0; i < talk_data.Length; i++)
			{
				talk_data[i].CopyTo(dest.talk_data_[i]);
			}
			for (int j = 0; j < 32; j++)
			{
				for (int k = 0; k < 4; k++)
				{
					dest.talk_data_[j].flag[k] = flag[j * 4 + k];
				}
			}
		}
	}
}
