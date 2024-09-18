using System;

[Serializable]
public class CinemaWork
{
	public ushort set_type;

	public ushort film_no;

	public byte bg_no;

	public byte sw;

	public byte step0;

	public byte step1;

	public byte plt;

	public byte win_type;

	public short frame_add;

	public short frame_top;

	public short frame_end;

	public float frame_now;

	public short frame_set;

	public uint status;

	public int movie_type;

	public void init()
	{
		set_type = 0;
		film_no = 0;
		bg_no = 0;
		sw = 0;
		step0 = 0;
		step1 = 0;
		plt = 0;
		win_type = 0;
		frame_add = 0;
		frame_top = 0;
		frame_end = 0;
		frame_now = 0f;
		frame_set = 0;
		status = 0u;
		movie_type = 0;
	}
}
