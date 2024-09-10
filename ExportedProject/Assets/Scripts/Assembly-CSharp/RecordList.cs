using System;

[Serializable]
public class RecordList
{
	public int[] record = new int[80];

	public void init()
	{
		for (int i = 0; i < record.Length; i++)
		{
			record[i] = 0;
		}
	}
}
