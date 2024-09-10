using System;

[Serializable]
public struct ExplCharData
{
	[Flags]
	public enum Status
	{
		EXPL_ST_BLINK = 1,
		EXPL_ST_MOVE = 2,
		EXPL_ST_NO_DISP = 4
	}

	public byte id;

	public byte blink;

	public byte timer;

	public byte speed;

	public byte move;

	public byte status;

	public byte dot;

	public byte dot_now;

	public ushort para0;

	public ushort para1;

	public float move_x;

	public float move_y;

	public ushort para2;

	public ushort oam;

	public uint vram_addr;

	public void Reset()
	{
		id = byte.MaxValue;
		blink = 0;
		status = 0;
		para0 = 512;
	}

	public void init()
	{
		id = byte.MaxValue;
		blink = 0;
		timer = 0;
		speed = 0;
		move = 0;
		status = 0;
		dot = 0;
		dot_now = 0;
		para0 = 512;
		para1 = 0;
		move_x = 0f;
		move_y = 0f;
		para2 = 0;
		oam = 0;
		vram_addr = 0u;
	}
}
