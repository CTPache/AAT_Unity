public static class SubWindow_HumanDetail
{
	private delegate void HumanDetailProc(SubWindow sub_window);

	private static readonly HumanDetailProc[] proc_table;

	static SubWindow_HumanDetail()
	{
		proc_table = new HumanDetailProc[4] { Init, Main, Thrust, PageChange };
	}

	public static void Proc(SubWindow sub_window)
	{
		Routine currentRoutine = sub_window.GetCurrentRoutine();
		proc_table[currentRoutine.r.no_1](sub_window);
	}

	public static void Init(SubWindow sub_window)
	{
		Routine currentRoutine = sub_window.GetCurrentRoutine();
		switch (currentRoutine.r.no_2)
		{
		case 0:
			currentRoutine.r.no_2++;
			break;
		case 1:
			currentRoutine.r.no_2++;
			break;
		case 2:
			sub_window.busy_ = 0u;
			sub_window.req_ = SubWindow.Req.NONE;
			currentRoutine.r.no_1 = 1;
			currentRoutine.r.no_2 = 0;
			break;
		}
	}

	public static void Main(SubWindow sub_window)
	{
	}

	public static void Thrust(SubWindow sub_window)
	{
		Routine currentRoutine = sub_window.GetCurrentRoutine();
		switch (currentRoutine.r.no_2)
		{
		case 0:
			currentRoutine.r.no_2++;
			break;
		case 1:
			currentRoutine.r.no_2++;
			break;
		case 2:
			currentRoutine.r.no_2++;
			break;
		case 3:
			currentRoutine.r.no_2++;
			break;
		case 4:
			sub_window.req_ = SubWindow.Req.NONE;
			sub_window.busy_ = 0u;
			currentRoutine.r.no_2++;
			break;
		case 5:
			if (currentRoutine.r.no_3 == 0 || currentRoutine.r.no_3 == 1 || currentRoutine.r.no_3 == 2)
			{
				sub_window.stack_ -= 2;
			}
			else
			{
				sub_window.stack_ -= 2;
			}
			break;
		}
	}

	public static void PageChange(SubWindow sub_window)
	{
		Routine currentRoutine = sub_window.GetCurrentRoutine();
		switch (currentRoutine.r.no_2)
		{
		case 0:
			currentRoutine.r.no_2++;
			break;
		case 1:
			currentRoutine.r.no_2++;
			break;
		case 2:
			sub_window.busy_ = 0u;
			currentRoutine.r.no_1 = 1;
			currentRoutine.r.no_2 = 0;
			break;
		}
	}

	public static void disp_sw_human_detail(SubWindow sub_window)
	{
	}

	public static void disp_sw_human_detail_page_change(SubWindow sub_window)
	{
	}
}
