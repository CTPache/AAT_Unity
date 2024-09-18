using System;

[Serializable]
public class OBJ_SCROLL
{
	public sbyte pos_y;

	public ushort now_obj;

	public uint vram_addr;

	public void init()
	{
		pos_y = 0;
		now_obj = 0;
		vram_addr = 0u;
	}
}
