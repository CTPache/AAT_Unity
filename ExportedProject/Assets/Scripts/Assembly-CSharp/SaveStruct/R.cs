using System;

namespace SaveStruct
{
	[Serializable]
	public struct R
	{
		public byte no_0;

		public byte no_1;

		public byte no_2;

		public byte no_3;

		public void CopyFrom(ref global::R src)
		{
			no_0 = src.no_0;
			no_1 = src.no_1;
			no_2 = src.no_2;
			no_3 = src.no_3;
		}

		public void CopyTo(ref global::R dest)
		{
			dest.no_0 = no_0;
			dest.no_1 = no_1;
			dest.no_2 = no_2;
			dest.no_3 = no_3;
		}
	}
}
