using System;

[Serializable]
public class SubWindowNote
{
	public byte item_page;

	public byte item_page_max;

	public byte item_cursor;

	public byte man_page;

	public byte man_page_max;

	public byte man_cursor;

	public ushort rotate;

	public byte current_mode;

	public byte next_page;

	public byte next_cursor;

	public byte prev_page;

	public byte prev_cursor;

	public byte item_page_old;

	public byte item_cursor_old;

	public byte current_mode_old;

	public byte man_page_old;

	public byte man_cursor_old;

	public void init()
	{
		item_page = 0;
		item_page_max = 0;
		item_cursor = 0;
		man_page = 0;
		man_page_max = 0;
		man_cursor = 0;
		rotate = 0;
		current_mode = 0;
		next_page = 0;
		next_cursor = 0;
		prev_page = 0;
		prev_cursor = 0;
		item_page_old = 0;
		item_cursor_old = 0;
		current_mode_old = 0;
		man_page_old = 0;
		man_cursor_old = 0;
	}
}
