using System;

[Serializable]
public class INSPECT_DATA
{
	public uint message;

	public uint place;

	public uint item;

	public uint x0;

	public uint y0;

	public uint x1;

	public uint y1;

	public uint x2;

	public uint y2;

	public uint x3;

	public uint y3;

	public INSPECT_DATA()
	{
		message = uint.MaxValue;
		place = uint.MaxValue;
		item = uint.MaxValue;
		x0 = uint.MaxValue;
		y0 = uint.MaxValue;
		x1 = uint.MaxValue;
		y1 = uint.MaxValue;
		x2 = uint.MaxValue;
		y2 = uint.MaxValue;
		x3 = uint.MaxValue;
		y3 = uint.MaxValue;
	}

	public INSPECT_DATA(uint in_message, uint in_place, uint in_item, uint in_x0, uint in_y0, uint in_x1, uint in_y1, uint in_x2, uint in_y2, uint in_x3, uint in_y3)
	{
		message = in_message;
		place = in_place;
		item = in_item;
		x0 = in_x0;
		y0 = in_y0;
		x1 = in_x1;
		y1 = in_y1;
		x2 = in_x2;
		y2 = in_y2;
		x3 = in_x3;
		y3 = in_y3;
	}

	public void set(INSPECT_DATA in_data)
	{
		message = in_data.message;
		place = in_data.place;
		item = in_data.item;
		x0 = in_data.x0;
		y0 = in_data.y0;
		x1 = in_data.x1;
		y1 = in_data.y1;
		x2 = in_data.x2;
		y2 = in_data.y2;
		x3 = in_data.x3;
		y3 = in_data.y3;
	}
}
