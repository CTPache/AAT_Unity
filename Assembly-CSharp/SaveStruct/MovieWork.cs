using System;

namespace SaveStruct
{
	[Serializable]
	public struct MovieWork
	{
		public ushort status;

		public byte mov_no;

		public short frame_now;

		public short frame_max;

		public static void New(out MovieWork movie_work)
		{
			movie_work = default(MovieWork);
		}

		public void CopyFrom(global::MovieWork src)
		{
			status = src.status;
			mov_no = src.mov_no;
			frame_now = src.frame_now;
			frame_max = src.frame_max;
		}

		public void CopyTo(global::MovieWork dest)
		{
			dest.status = status;
			dest.mov_no = mov_no;
			dest.frame_now = frame_now;
			dest.frame_max = frame_max;
		}
	}
}
