using System;
using System.Runtime.InteropServices;

namespace SaveStruct
{
	[Serializable]
	public struct SaveData
	{
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 40)]
		public string time;

		public ushort title;

		public ushort scenario;

		public ushort progress;

		public ushort in_data;

		public void CopyFrom(ref global::SaveData src)
		{
			time = src.time;
			title = src.title;
			scenario = src.scenario;
			progress = src.progress;
			in_data = src.in_data;
		}

		public void CopyTo(ref global::SaveData dest)
		{
			dest.time = time;
			dest.title = title;
			dest.scenario = scenario;
			dest.progress = progress;
			dest.in_data = in_data;
		}
	}
}
