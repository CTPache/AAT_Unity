using System;

[Serializable]
public class MovieWork
{
	public ushort status;

	public byte mov_no;

	public short frame_now;

	public short frame_max;

	public void init()
	{
		status = 0;
		mov_no = 0;
		frame_now = 0;
		frame_max = 0;
	}
}
