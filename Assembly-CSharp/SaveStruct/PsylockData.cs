using System;
using System.Runtime.InteropServices;

namespace SaveStruct
{
	[Serializable]
	public struct PsylockData
	{
		public uint status;

		public ushort room;

		public ushort pl_id;

		public byte level;

		public byte size;

		public ushort start_message;

		public ushort cancel_message;

		public ushort correct_message;

		public ushort wrong_message;

		public ushort die_message;

		public ushort cancel_bgm;

		public ushort unlock_bgm;

		public uint item_size;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
		public byte[] item_no;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
		public ushort[] item_correct_message;

		public static void New(out PsylockData psylock_data)
		{
			psylock_data = default(PsylockData);
			psylock_data.item_no = new byte[4];
			psylock_data.item_correct_message = new ushort[4];
		}

		public void CopyFrom(global::PsylockData src)
		{
			status = src.status;
			room = src.room;
			pl_id = src.pl_id;
			level = src.level;
			size = src.size;
			start_message = src.start_message;
			cancel_message = src.cancel_message;
			correct_message = src.correct_message;
			wrong_message = src.wrong_message;
			die_message = src.die_message;
			cancel_bgm = src.cancel_bgm;
			unlock_bgm = src.unlock_bgm;
			item_size = src.item_size;
			Array.Copy(src.item_no, item_no, item_no.Length);
			Array.Copy(src.item_correct_message, item_correct_message, item_correct_message.Length);
		}

		public void CopyTo(global::PsylockData dest)
		{
			dest.status = status;
			dest.room = room;
			dest.pl_id = pl_id;
			dest.level = level;
			dest.size = size;
			dest.start_message = start_message;
			dest.cancel_message = cancel_message;
			dest.correct_message = correct_message;
			dest.wrong_message = wrong_message;
			dest.die_message = die_message;
			dest.cancel_bgm = cancel_bgm;
			dest.unlock_bgm = unlock_bgm;
			dest.item_size = item_size;
			if (item_no == null)
			{
				item_no = new byte[4];
			}
			Array.Copy(item_no, dest.item_no, item_no.Length);
			if (item_correct_message == null)
			{
				item_correct_message = new ushort[4];
			}
			Array.Copy(item_correct_message, dest.item_correct_message, item_correct_message.Length);
		}
	}
}
