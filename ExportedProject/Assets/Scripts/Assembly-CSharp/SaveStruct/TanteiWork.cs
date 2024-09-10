using System;

namespace SaveStruct
{
	[Serializable]
	public struct TanteiWork
	{
		public byte person_flag;

		public byte tantei_cursor;

		public float inspect_cursor_x;

		public float inspect_cursor_y;

		public byte talk_cursor;

		public byte talk_cursor_num;

		public byte select_cursor;

		public byte move_cursor;

		public static void New(out TanteiWork tantei_work)
		{
			tantei_work = default(TanteiWork);
		}

		public void CopyFrom(global::TanteiWork src)
		{
			person_flag = src.person_flag;
			tantei_cursor = (byte)tanteiMenu.instance.cursor_no;
			inspect_cursor_x = inspectCtrl.instance.pos_x_;
			inspect_cursor_y = inspectCtrl.instance.pos_y_;
			talk_cursor = (byte)selectPlateCtrl.instance.old_cursor_no;
			talk_cursor_num = (byte)selectPlateCtrl.instance.old_cursor_num;
			select_cursor = (byte)selectPlateCtrl.instance.cursor_no;
			move_cursor = (byte)moveCtrl.instance.cursor_no;
		}

		public void CopyTo(global::TanteiWork dest)
		{
			dest.person_flag = person_flag;
			dest.tantei_cursor = tantei_cursor;
			dest.inspect_cursor_x = inspect_cursor_x;
			dest.inspect_cursor_y = inspect_cursor_y;
			dest.talk_cursor = talk_cursor;
			dest.talk_cursor_num = talk_cursor_num;
			dest.select_cursor = select_cursor;
			dest.move_cursor = move_cursor;
		}
	}
}
