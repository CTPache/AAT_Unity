public static class GSFlag
{
	public const int SCENARIO = 0;

	public const int STATUS = 1;

	public const int TALK_END = 2;

	public static void Set(uint flag_id, uint flag_no, uint set)
	{
		GlobalWork global_work_ = GSStatic.global_work_;
		switch (flag_id)
		{
		case 0u:
			Set(ref global_work_.sce_flag[flag_no >> 5], flag_no, set);
			break;
		case 1u:
			Set(ref global_work_.status_flag, flag_no, set);
			break;
		case 2u:
			Set(ref global_work_.talk_end_flag[flag_no >> 5], flag_no, set);
			break;
		case 3u:
			Set(ref global_work_.bg_first_flag[flag_no >> 5], flag_no, set);
			break;
		}
	}

	private static void Set(ref uint flag, uint flag_no, uint set)
	{
		uint num = (uint)(1 << (int)flag_no);
		if (set != 0)
		{
			flag |= num;
		}
		else
		{
			flag &= ~num;
		}
	}

	public static bool Check(uint flag_id, uint flag_no)
	{
		GlobalWork global_work_ = GSStatic.global_work_;
		uint num = 0u;
		switch (flag_id)
		{
		case 0u:
			num = global_work_.sce_flag[flag_no >> 5];
			break;
		case 1u:
			num = global_work_.status_flag;
			break;
		case 2u:
			num = global_work_.talk_end_flag[flag_no >> 5];
			break;
		case 3u:
			num = global_work_.bg_first_flag[flag_no >> 5];
			break;
		}
		return ((num >> (int)(flag_no & 0x1F)) & 1) != 0;
	}
}
