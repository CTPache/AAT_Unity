using System;

[Serializable]
public struct SubWindowObjInfo
{
	public int sp_id;

	public byte plt;

	public short x;

	public short y;

	public GSRect rect;

	public byte dir;

	public sbyte speed;

	public byte time;

	public Action proc;

	public byte proc_trg;

	public ushort reaction_btn;

	public SubWindowObjInfo(SpriteDataTable.MenuSpriteNo sp_id, int plt, short x, short y, GSRect rect, SubWindow.ObjMove dir, int speed, int time, Action proc, GSTouch.Check proc_trg, ushort reaction_btn)
	{
		this.sp_id = (int)sp_id;
		this.plt = (byte)plt;
		this.x = x;
		this.y = y;
		this.rect = rect;
		this.dir = (byte)dir;
		this.speed = (sbyte)speed;
		this.time = (byte)time;
		this.proc = proc;
		this.proc_trg = (byte)proc_trg;
		this.reaction_btn = reaction_btn;
	}

	public void init()
	{
		sp_id = 0;
		plt = 0;
		x = 0;
		y = 0;
		rect.init();
		dir = 0;
		speed = 0;
		time = 0;
		proc = null;
		proc_trg = 0;
		reaction_btn = 0;
	}
}
