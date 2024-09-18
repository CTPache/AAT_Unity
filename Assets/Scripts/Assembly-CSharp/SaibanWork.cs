using System;

[Serializable]
public class SaibanWork
{
	public byte wait_timer;

	public void Clear()
	{
	}

	public void init()
	{
		wait_timer = 0;
	}
}
