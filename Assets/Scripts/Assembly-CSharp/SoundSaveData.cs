using System;

[Serializable]
public class SoundSaveData
{
	public int playBgmNo;

	public int stopBgmNo;

	public int[] se_no = new int[10];

	public bool[] pause_bgm = new bool[3];

	public void init()
	{
		playBgmNo = 254;
		stopBgmNo = 254;
		for (int i = 0; i < se_no.Length; i++)
		{
			se_no[i] = 0;
		}
		for (int j = 0; j < pause_bgm.Length; j++)
		{
			pause_bgm[j] = false;
		}
	}
}
