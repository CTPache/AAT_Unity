using System;
using System.Runtime.InteropServices;

namespace SaveStruct
{
	[Serializable]
	public struct SubWindow
	{
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
		public Routine[] routine_;

		public byte cursor_Rno_0;

		public byte cursor_Rno_1;

		public byte req_;

		public byte stack_;

		public uint busy_;

		public byte tantei_tukituke_;

		public byte tutorial_;

		public byte status_force_;

		public byte point_3d_;

		public byte note_add_;

		public byte man_page_old;

		public byte man_cursor_old;

		public byte item_page_old;

		public byte item_cursor_old;

		public byte current_mode_old;

		public static void New(out SubWindow sub_window)
		{
			sub_window = default(SubWindow);
			sub_window.routine_ = new Routine[8];
		}

		public void CopyFrom(global::SubWindow src)
		{
			for (int i = 0; i < routine_.Length; i++)
			{
				routine_[i].CopyFrom(src.routine_[i]);
			}
			cursor_Rno_0 = src.cursor_.Rno_0;
			cursor_Rno_1 = src.cursor_.Rno_1;
			req_ = (byte)src.req_;
			stack_ = src.stack_;
			busy_ = src.busy_;
			tantei_tukituke_ = src.tantei_tukituke_;
			tutorial_ = src.tutorial_;
			status_force_ = src.status_force_;
			point_3d_ = src.point_3d_;
			note_add_ = src.note_add_;
			if (GSStatic.global_work_.r.no_0 == 11 || GSStatic.global_work_.r.no_0 == 1)
			{
				for (int j = 0; j < routine_.Length; j++)
				{
					routine_[j].r.no_0 = 0;
					routine_[j].r.no_1 = 0;
					routine_[j].r.no_2 = 0;
					routine_[j].r.no_3 = 0;
				}
				req_ = 0;
				stack_ = 0;
			}
			man_page_old = src.note_.man_page_old;
			man_cursor_old = src.note_.man_cursor_old;
			item_page_old = src.note_.item_page_old;
			item_cursor_old = src.note_.item_cursor_old;
			current_mode_old = src.note_.current_mode_old;
		}

		public void CopyTo(global::SubWindow dest)
		{
			if (routine_ == null)
			{
				routine_ = new Routine[8];
			}
			for (int i = 0; i < routine_.Length; i++)
			{
				routine_[i].CopyTo(dest.routine_[i]);
			}
			dest.cursor_.Rno_0 = cursor_Rno_0;
			dest.cursor_.Rno_1 = cursor_Rno_1;
			dest.req_ = (global::SubWindow.Req)req_;
			dest.stack_ = stack_;
			dest.busy_ = busy_;
			dest.tantei_tukituke_ = tantei_tukituke_;
			dest.tutorial_ = tutorial_;
			dest.status_force_ = status_force_;
			dest.point_3d_ = point_3d_;
			dest.note_add_ = note_add_;
			dest.note_.man_page_old = man_page_old;
			dest.note_.man_cursor_old = man_cursor_old;
			dest.note_.item_page_old = item_page_old;
			dest.note_.item_cursor_old = item_cursor_old;
			dest.note_.current_mode_old = current_mode_old;
		}
	}
}
