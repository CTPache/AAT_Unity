using System;

[Serializable]
public struct GSPoint4
{
	public ushort x0;

	public ushort y0;

	public ushort x1;

	public ushort y1;

	public ushort x2;

	public ushort y2;

	public ushort x3;

	public ushort y3;

	public GSPoint4(ushort x0, ushort y0, ushort x1, ushort y1, ushort x2, ushort y2, ushort x3, ushort y3)
	{
		this.x0 = x0;
		this.y0 = y0;
		this.x1 = x1;
		this.y1 = y1;
		this.x2 = x2;
		this.y2 = y2;
		this.x3 = x3;
		this.y3 = y3;
	}

	public GSPoint4(uint x0, uint y0, uint x1, uint y1, uint x2, uint y2, uint x3, uint y3)
	{
		this.x0 = (ushort)x0;
		this.y0 = (ushort)y0;
		this.x1 = (ushort)x1;
		this.y1 = (ushort)y1;
		this.x2 = (ushort)x2;
		this.y2 = (ushort)y2;
		this.x3 = (ushort)x3;
		this.y3 = (ushort)y3;
	}

	public GSPoint GetPoint(int index)
	{
		switch (index)
		{
		case 0:
			return new GSPoint(x0, y0);
		case 1:
			return new GSPoint(x1, y1);
		case 2:
			return new GSPoint(x2, y2);
		case 3:
			return new GSPoint(x3, y3);
		default:
			return default(GSPoint);
		}
	}

	public void init()
	{
		x0 = 0;
		y0 = 0;
		x1 = 0;
		y1 = 0;
		x2 = 0;
		y2 = 0;
		x3 = 0;
		y3 = 0;
	}
}
