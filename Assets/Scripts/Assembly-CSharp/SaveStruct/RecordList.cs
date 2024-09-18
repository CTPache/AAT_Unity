using System;
using System.Runtime.InteropServices;

namespace SaveStruct
{
	[Serializable]
	public struct RecordList
	{
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 80)]
		public int[] record;

		public static void New(out RecordList record_list)
		{
			record_list = default(RecordList);
			record_list.record = new int[80];
			for (int i = 0; i < record_list.record.Length; i++)
			{
				record_list.record[i] = -1;
			}
		}

		public void CopyFrom(recordListCtrl src)
		{
			int num = 0;
			foreach (recordListCtrl.RecodeData item in src.record_data_)
			{
				foreach (int item2 in item.note_list_)
				{
					record[num++] = item2;
				}
			}
		}

		public void CopyTo(global::RecordList dest)
		{
			if (record == null)
			{
				record = new int[80];
				for (int i = 0; i < record.Length; i++)
				{
					record[i] = -1;
				}
			}
			for (int j = 0; j < record.Length; j++)
			{
				dest.record[j] = record[j];
			}
		}
	}
}
