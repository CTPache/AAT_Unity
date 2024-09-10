using System;
using System.Runtime.InteropServices;

namespace SaveStruct
{
	[Serializable]
	public struct PresideData
	{
		public SystemData system_data_;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 100)]
		public GameData[] slot_list_;

		public const int MAX_SLOT = 10;

		public const int REGION_MAX = 10;

		public static void New(out PresideData preside_data)
		{
			preside_data = default(PresideData);
			SystemData.New(out preside_data.system_data_);
			preside_data.slot_list_ = new GameData[100];
			for (int i = 0; i < preside_data.slot_list_.Length; i++)
			{
				GameData.New(out preside_data.slot_list_[i]);
			}
		}
	}
}
