using System;

[Serializable]
public class Routine3d
{
	public byte Rno_0_;

	public byte Rno_1_;

	public sbyte flag;

	public byte timer;

	public byte pallet;

	public byte disp_off;

	public byte state;

	public short x;

	public short y;

	public short w;

	public short h;

	public ushort[] rotate = new ushort[3];

	public ushort scale;

	public byte dmy;

	public byte Rno_0
	{
		get
		{
			return Rno_0_;
		}
		set
		{
			Rno_0_ = value;
		}
	}

	public byte Rno_1
	{
		get
		{
			return Rno_1_;
		}
		set
		{
			Rno_1_ = value;
		}
	}

	public void Clear()
	{
		init();
	}

	public void init()
	{
		Rno_0 = 0;
		Rno_1 = 0;
		flag = 0;
		timer = 0;
		pallet = 0;
		disp_off = 0;
		state = 0;
		x = 0;
		y = 0;
		w = 0;
		h = 0;
		rotate[0] = 0;
		rotate[1] = 0;
		rotate[2] = 0;
		scale = 0;
		dmy = 0;
	}
}
