using System;

namespace SaveStruct
{
	[Serializable]
	public struct MenuWork
	{
		public bool tantei_menu_is_play;

		public int tantei_menu_setting;

		public bool select_plate_is_select;

		public bool select_plate_is_talk;

		public bool inspect_is_play;

		public bool move_is_play;

		public int life_gauge;

		public static void New(out MenuWork menu_work)
		{
			menu_work = default(MenuWork);
		}

		public void CopyFrom()
		{
			tantei_menu_is_play = tanteiMenu.instance.is_play;
			tantei_menu_setting = tanteiMenu.instance.setting;
			select_plate_is_select = selectPlateCtrl.instance.is_select;
			select_plate_is_talk = selectPlateCtrl.instance.is_talk;
			inspect_is_play = inspectCtrl.instance.is_play;
			move_is_play = moveCtrl.instance.is_play;
			life_gauge = lifeGaugeCtrl.instance.gauge_mode;
		}

		public void CopyTo(global::MenuWork dest)
		{
			dest.tantei_menu_is_play = tantei_menu_is_play;
			dest.tantei_menu_setting = tantei_menu_setting;
			dest.select_plate_is_select = select_plate_is_select;
			dest.select_plate_is_talk = select_plate_is_talk;
			dest.inspect_is_play = inspect_is_play;
			dest.move_is_play = move_is_play;
			dest.life_gauge = life_gauge;
		}
	}
}
