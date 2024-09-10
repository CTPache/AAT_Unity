using System;

[Serializable]
public class EffectWork
{
	public SubWindowEffectInfo[] info = new SubWindowEffectInfo[4];

	public void init()
	{
		for (int i = 0; i < info.Length; i++)
		{
			info[i].init();
		}
	}
}
