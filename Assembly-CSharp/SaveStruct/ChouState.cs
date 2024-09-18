using System;
using System.Runtime.InteropServices;

namespace SaveStruct
{
	[Serializable]
	public struct ChouState
	{
		public ushort choustateBK;

		public ushort choustate;

		public byte chou_no;

		public byte chou_st;

		public ushort chou_cnt;

		public const int BufferSize = 12;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
		public byte[] buffer;
	}
}
