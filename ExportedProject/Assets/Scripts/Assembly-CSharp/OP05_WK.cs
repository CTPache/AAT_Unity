using System;

[Serializable]
public class OP05_WK
{
	public ushort BG_rno;

	public ushort BG_no;

	public ushort BG_status;

	public ushort OBJ_scroll;

	public ushort wait;

	public ushort speed;

	public ushort fade;

	public ushort next_obj;

	public ushort[] dPal = new ushort[224];

	public void Clear()
	{
		BG_rno = 0;
		BG_no = 0;
		BG_status = 0;
		OBJ_scroll = 0;
		wait = 0;
		speed = 0;
		fade = 0;
		next_obj = 0;
	}

	public void init()
	{
		BG_rno = 0;
		BG_no = 0;
		BG_status = 0;
		OBJ_scroll = 0;
		wait = 0;
		speed = 0;
		fade = 0;
		next_obj = 0;
		for (int i = 0; i < dPal.Length; i++)
		{
			dPal[i] = 0;
		}
	}
}
