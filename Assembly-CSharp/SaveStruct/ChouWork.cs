using System;
using System.Runtime.InteropServices;

namespace SaveStruct
{
	[Serializable]
	public struct ChouWork
	{
		public short x;

		public short y;

		public ushort flg;

		public ushort num;

		public ushort work16;

		public short E;

		public short ax;

		public short ay;

		public short dx;

		public short dy;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
		public byte[] work;

		public const int BufferSize = 8;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
		public byte[] buffer;
	}
}
