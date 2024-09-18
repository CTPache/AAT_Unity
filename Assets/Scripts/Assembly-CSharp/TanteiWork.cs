using System;

[Serializable]
public class TanteiWork
{
	public byte sel_place;

	public byte def_place;

	public byte person_flag_;

	public byte siteki_no;

	public byte menu;

	public byte[] sel_place_be = new byte[4];

	public byte yubi_timer;

	public ushort tanchiki_demof;

	public byte tantei_cursor;

	public float inspect_cursor_x;

	public float inspect_cursor_y;

	public byte talk_cursor;

	public byte talk_cursor_num;

	public byte select_cursor;

	public byte move_cursor;

	public byte person_flag
	{
		get
		{
			return person_flag_;
		}
		set
		{
			person_flag_ = value;
		}
	}

	public void Clear()
	{
		sel_place = 0;
		def_place = 0;
		person_flag = 0;
		siteki_no = 0;
		menu = 0;
		tanchiki_demof = 0;
		tantei_cursor = 0;
		inspect_cursor_x = 0f;
		inspect_cursor_y = 0f;
		talk_cursor = 0;
		talk_cursor_num = 0;
		select_cursor = 0;
		move_cursor = 0;
	}

	public void init()
	{
		sel_place = 0;
		def_place = 0;
		person_flag_ = 0;
		siteki_no = 0;
		menu = 0;
		yubi_timer = 0;
		tanchiki_demof = 0;
		tantei_cursor = 0;
		inspect_cursor_x = 0f;
		inspect_cursor_y = 0f;
		talk_cursor = 0;
		talk_cursor_num = 0;
		select_cursor = 0;
		move_cursor = 0;
		for (int i = 0; i < sel_place_be.Length; i++)
		{
			sel_place_be[i] = 0;
		}
	}
}
