public static class GS1_script
{
	public static bool GS1_moji_code06_CheckSE(MessageWork pMess, ushort _arg1)
	{
		bool result = false;
		if (GSStatic.global_work_.scenario == 18 && pMess.now_no == scenario.SC4_60870 && _arg1 == 92)
		{
			result = true;
		}
		return result;
	}
}
