using System;
using System.Runtime.InteropServices;

namespace SaveStruct
{
	[Serializable]
	public struct TALK_DATA
	{
		public uint room;

		public uint pl_id;

		public uint dm;

		public uint sw;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
		public uint[] tag;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
		public uint[] flag;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
		public uint[] mess;

		public static void New(out TALK_DATA talk_data)
		{
			talk_data = default(TALK_DATA);
			talk_data.tag = new uint[4];
			talk_data.flag = new uint[4];
			talk_data.mess = new uint[4];
		}

		public void CopyFrom(global::TALK_DATA src)
		{
			room = src.room;
			pl_id = src.pl_id;
			dm = src.dm;
			sw = src.sw;
			Array.Copy(src.tag, tag, tag.Length);
			Array.Copy(src.flag, flag, flag.Length);
			Array.Copy(src.mess, mess, mess.Length);
		}

		public void CopyTo(global::TALK_DATA dest)
		{
			dest.room = room;
			dest.pl_id = pl_id;
			dest.dm = dm;
			dest.sw = sw;
			Array.Copy(tag, dest.tag, tag.Length);
			Array.Copy(flag, dest.flag, flag.Length);
			Array.Copy(mess, dest.mess, mess.Length);
		}
	}
}
