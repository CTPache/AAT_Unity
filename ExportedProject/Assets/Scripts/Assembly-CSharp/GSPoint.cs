using System;

[Serializable]
public struct GSPoint
{
	public ushort x;

	public ushort y;

	public GSPoint(ushort x, ushort y)
	{
		this.x = x;
		this.y = y;
	}

	public void init()
	{
		x = 0;
		y = 0;
	}
}
