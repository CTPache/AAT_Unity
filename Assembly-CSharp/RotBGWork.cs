using System;

[Serializable]
public class RotBGWork
{
	public byte rot_move_flag;

	public float rotate_add;

	public void init()
	{
		rot_move_flag = 0;
		rotate_add = 0f;
	}
}
