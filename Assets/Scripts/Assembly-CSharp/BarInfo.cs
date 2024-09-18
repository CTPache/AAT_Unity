using System;

[Serializable]
public class BarInfo
{
	public sbyte sprite_id;

	public short x;

	public short y;

	public byte v_flip;

	public byte h_flip;

	public BarInfo(SpriteDataTable.MenuSpriteNo sprite_id, short x, short y, byte v_flip, byte h_flip)
	{
		this.sprite_id = (sbyte)sprite_id;
		this.x = x;
		this.y = y;
		this.v_flip = v_flip;
		this.h_flip = h_flip;
	}

	public void init()
	{
		sprite_id = 0;
		x = 0;
		y = 0;
		v_flip = 0;
		h_flip = 0;
	}
}
