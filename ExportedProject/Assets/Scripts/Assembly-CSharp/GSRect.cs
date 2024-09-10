using System;

[Serializable]
public struct GSRect
{
	public short x;

	public short y;

	public short w;

	public short h;

	public GSRect(short x, short y, short w, short h)
	{
		this.x = x;
		this.y = y;
		this.w = w;
		this.h = h;
	}

	public void init()
	{
		x = 0;
		y = 0;
		w = 0;
		h = 0;
	}
}
