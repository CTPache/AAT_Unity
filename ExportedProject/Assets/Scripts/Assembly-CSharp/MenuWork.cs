using System;

[Serializable]
public class MenuWork
{
	public bool tantei_menu_is_play;

	public int tantei_menu_setting;

	public bool select_plate_is_select;

	public bool select_plate_is_talk;

	public bool inspect_is_play;

	public bool move_is_play;

	public int life_gauge;

	public void init()
	{
		tantei_menu_is_play = false;
		tantei_menu_setting = 0;
		select_plate_is_select = false;
		select_plate_is_talk = false;
		inspect_is_play = false;
		move_is_play = false;
		life_gauge = 0;
	}
}
