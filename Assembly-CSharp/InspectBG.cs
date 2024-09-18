using System;

[Serializable]
public class InspectBG
{
	public byte step0;

	public byte step1;

	public ushort timer0;

	public ushort ins_off_x;

	public ushort ins_off_y;

	public short ins_drag_x;

	public short ins_drag_y;

	public byte ins_hit_check;

	public byte ins_timer;

	public byte ins_flag;

	public byte tan_anm;

	public byte tan_flag;

	public byte tan_cnt;

	public byte tan_lamp;

	public byte tan_lv;

	public ushort tan_lampcnt;

	public ushort tan_lampanm;

	public ushort tan_posx;

	public ushort tan_posy;

	public void init()
	{
		step0 = 0;
		step1 = 0;
		timer0 = 0;
		ins_off_x = 0;
		ins_off_y = 0;
		ins_drag_x = 0;
		ins_drag_y = 0;
		ins_hit_check = 0;
		ins_timer = 0;
		ins_flag = 0;
		tan_anm = 0;
		tan_flag = 0;
		tan_cnt = 0;
		tan_lamp = 0;
		tan_lv = 0;
		tan_lampcnt = 0;
		tan_lampanm = 0;
		tan_posx = 0;
		tan_posy = 0;
	}
}
