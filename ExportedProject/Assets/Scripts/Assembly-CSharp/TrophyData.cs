using System;

[Serializable]
public class TrophyData
{
	public int[] message_flag_;

	public TrophyData()
	{
		message_flag_ = new int[96];
	}

	public void init()
	{
		for (int i = 0; i < message_flag_.Length; i++)
		{
			message_flag_[i] = 0;
		}
	}
}
