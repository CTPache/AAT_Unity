public static class SubWindow_Luminol
{
	public delegate void LuminolProc(SubWindow sub_window);

	private static readonly LuminolProc[] proc_table;

	static SubWindow_Luminol()
	{
		proc_table = new LuminolProc[2] { Init, Wait };
	}

	public static void Proc(SubWindow sub_window)
	{
		Routine currentRoutine = sub_window.GetCurrentRoutine();
		proc_table[currentRoutine.r.no_1](sub_window);
	}

	private static void Init(SubWindow sub_window)
	{
		luminolMiniGame.instance.Init();
		Routine currentRoutine = sub_window.GetCurrentRoutine();
		sub_window.req_ = SubWindow.Req.NONE;
		sub_window.busy_ = 0u;
		currentRoutine.r.no_2 = 0;
		currentRoutine.r.no_1++;
	}

	private static void Wait(SubWindow sub_window)
	{
	}
}
