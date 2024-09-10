using System;

[Serializable]
public class ObjRoutine
{
	public R r;

	public short x;

	public short y;

	public short ratex;

	public short ratey;

	public byte reaction_flag;

	public void init()
	{
		r.init();
		x = 0;
		y = 0;
		ratex = 0;
		ratey = 0;
		reaction_flag = 0;
	}
}
