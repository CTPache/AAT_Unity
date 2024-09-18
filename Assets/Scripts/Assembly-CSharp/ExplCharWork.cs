using System;

[Serializable]
public class ExplCharWork
{
	public ExplCharData[] expl_char_data_ = new ExplCharData[8];

	public void init()
	{
		for (int i = 0; i < expl_char_data_.Length; i++)
		{
			expl_char_data_[i].init();
		}
	}
}
