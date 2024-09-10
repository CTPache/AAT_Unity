using System;
using System.Runtime.InteropServices;

namespace SaveStruct
{
	[Serializable]
	public struct AnimationSystemSave
	{
		public int character_id;

		public int idling_foa;

		public int talking_foa;

		public int playing_foa;

		public const int BufferSize = 40;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 40)]
		public byte[] buffer;
	}
}
