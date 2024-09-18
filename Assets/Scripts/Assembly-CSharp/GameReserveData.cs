using System;

[Serializable]
public class GameReserveData
{
	public int[] reserve;

	public GameReserveData()
	{
		reserve = new int[200];
	}

	public void init()
	{
		for (int i = 0; i < reserve.Length; i++)
		{
			reserve[i] = 0;
		}
	}
}
