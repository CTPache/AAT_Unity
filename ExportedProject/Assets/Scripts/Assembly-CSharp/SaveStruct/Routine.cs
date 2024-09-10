using System;

namespace SaveStruct
{
	[Serializable]
	public struct Routine
	{
		public R r;

		public byte flag;

		public static void New(out Routine routine)
		{
			routine = default(Routine);
		}

		public void CopyFrom(global::Routine src)
		{
			r.CopyFrom(ref src.r);
			flag = src.flag;
		}

		public void CopyTo(global::Routine dest)
		{
			r.CopyTo(ref dest.r);
			dest.flag = flag;
		}
	}
}
