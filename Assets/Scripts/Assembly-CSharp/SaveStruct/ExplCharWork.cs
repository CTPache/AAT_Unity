using System;
using System.Runtime.InteropServices;

namespace SaveStruct
{
	[Serializable]
	public struct ExplCharWork
	{
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
		public ExplCharData[] expl_char_data_;

		public static void New(out ExplCharWork expl_char_work)
		{
			expl_char_work = default(ExplCharWork);
			expl_char_work.expl_char_data_ = new ExplCharData[8];
		}

		public void CopyFrom(global::ExplCharWork src)
		{
			for (int i = 0; i < expl_char_data_.Length; i++)
			{
				expl_char_data_[i].CopyFrom(ref src.expl_char_data_[i]);
			}
		}

		public void CopyTo(global::ExplCharWork dest)
		{
			for (int i = 0; i < expl_char_data_.Length; i++)
			{
				expl_char_data_[i].CopyTo(ref dest.expl_char_data_[i]);
			}
		}
	}
}
