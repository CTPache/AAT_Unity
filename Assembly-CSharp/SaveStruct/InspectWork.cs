using System;
using System.Runtime.InteropServices;

namespace SaveStruct
{
	[Serializable]
	public struct InspectWork
	{
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
		public InspectData[] inspect_data_;

		public static void New(out InspectWork inspect_work)
		{
			inspect_work = default(InspectWork);
			inspect_work.inspect_data_ = new InspectData[32];
		}

		public void CopyFrom(global::InspectWork src)
		{
			for (int i = 0; i < inspect_data_.Length; i++)
			{
				inspect_data_[i].CopyFrom(ref src.inspect_data_[i]);
			}
		}

		public void CopyTo(global::InspectWork dest)
		{
			if (inspect_data_ == null)
			{
				inspect_data_ = new InspectData[32];
			}
			for (int i = 0; i < inspect_data_.Length; i++)
			{
				inspect_data_[i].CopyTo(ref dest.inspect_data_[i]);
			}
		}
	}
}
