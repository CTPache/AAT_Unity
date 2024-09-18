using System;

[Serializable]
public class ChouWork
{
	public short x;

	public short y;

	public ushort flg;

	public ushort num;

	public ushort work16;

	public short E;

	public short ax;

	public short ay;

	public short dx;

	public short dy;

	public byte[] work = new byte[4];

	public void init()
	{
		x = 0;
		y = 0;
		flg = 0;
		num = 0;
		work16 = 0;
		E = 0;
		ax = 0;
		ay = 0;
		dx = 0;
		dy = 0;
		work[0] = 0;
		work[1] = 0;
		work[2] = 0;
		work[3] = 0;
	}
}
