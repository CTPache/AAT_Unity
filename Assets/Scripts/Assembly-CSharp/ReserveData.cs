using System;

[Serializable]
public class ReserveData
{
	public enum Reserve
	{
		ACCOUNT_ID = 1
	}

	public int[] reserve;

	public ReserveData()
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

	public void CopyTo(int[] in_reserve)
	{
		reserve[1] = SteamCtrl.GetAccountID();
		reserve.CopyTo(in_reserve, 0);
	}

	public void CopyFrom(int[] in_reserve)
	{
		in_reserve.CopyTo(reserve, 0);
	}
}
