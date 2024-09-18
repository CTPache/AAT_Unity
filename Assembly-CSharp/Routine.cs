using System;

[Serializable]
public class Routine
{
	public R r;

	public byte tex_no;

	public byte tp_cnt;

	public sbyte[] tp_no = new sbyte[14];

	public Routine3d[] routine_3d = new Routine3d[6];

	public byte flag;

	public byte timer0;

	public byte Rno_4;

	public Routine()
	{
		GSStructUtility.FillArrayNewInstance(routine_3d);
	}

	public void init()
	{
		r.init();
		tex_no = 0;
		tp_cnt = 0;
		flag = 0;
		timer0 = 0;
		Rno_4 = 0;
		for (int i = 0; i < tp_no.Length; i++)
		{
			tp_no[i] = 0;
		}
		for (int j = 0; j < routine_3d.Length; j++)
		{
			routine_3d[j].init();
		}
	}
}
