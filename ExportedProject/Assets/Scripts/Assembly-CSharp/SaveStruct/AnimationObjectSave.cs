using System;
using System.Runtime.InteropServices;

namespace SaveStruct
{
	[Serializable]
	public struct AnimationObjectSave
	{
		public bool exists;

		public float x;

		public float y;

		public float z;

		public int foa;

		public int characterID;

		public int beFlag;

		public int sequence;

		public int framesFromStarted;

		public int framesInSequence;

		public bool isFading;

		public bool isFadeIn;

		public int fadeFrame;

		public float alpha;

		public bool monochrome_fadein;

		public ushort monochrome_sw;

		public ushort monochrome_time;

		public ushort monochrome_speed;

		public const int BufferSize = 80;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 80)]
		public byte[] buffer;
	}
}
