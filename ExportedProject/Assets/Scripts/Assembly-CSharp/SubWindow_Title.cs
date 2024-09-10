public class SubWindow_Title
{
	public struct CLEAR_TITLE_WK
	{
		private byte step0;

		private byte step1;

		private short timer0;

		private short state;

		private short stage_select_flag;

		private short episode;
	}

	private delegate void Title_Proc(SubWindow sub_window);

	private static readonly Title_Proc[] proc_table;

	static SubWindow_Title()
	{
		proc_table = new Title_Proc[6]
		{
			Init,
			Title_Menu,
			dummy_proc,
			Title_Select,
			Title_Continue,
			SubWindow_Title_EpisodeClear.Proc
		};
	}

	public static void Proc(SubWindow sub_window)
	{
		Routine currentRoutine = sub_window.GetCurrentRoutine();
		proc_table[currentRoutine.r.no_1](sub_window);
	}

	private static void Init(SubWindow sub_window)
	{
		Routine routine = sub_window.routine_[sub_window.stack_];
		switch (routine.r.no_2)
		{
		case 0:
			sub_window.req_ = SubWindow.Req.NONE;
			sub_window.busy_ = 0u;
			routine.r.no_2++;
			break;
		case 1:
			if (sub_window.req_ == SubWindow.Req.TITLE_START)
			{
				routine.r.no_1 = 1;
				routine.r.no_2 = 0;
			}
			break;
		}
	}

	private static void Title_Menu(SubWindow pwork)
	{
	}

	private static void Title_Select(SubWindow pwork)
	{
	}

	private int get_cl_state()
	{
		return 6;
	}

	private void clear_title_main()
	{
	}

	private void obj_disp_title(short x, short y, byte disp_off, byte prio)
	{
	}

	private static void Title_Continue(SubWindow pwork)
	{
	}

	private static void dummy_proc(SubWindow sub_window)
	{
	}
}
