public static class BaseButton
{
	private delegate void BaseProc(SubWindow sub_window, uint rtn_no);

	private static readonly BaseProc[] base_button_proc_tbl;

	static BaseButton()
	{
		base_button_proc_tbl = new BaseProc[8] { base_button_init, base_button_appear0, base_button_main, base_button_leave0, base_button_press, base_button_appear1, base_button_leave1, base_button_wait };
	}

	public static void base_button_proc(SubWindow sub_window, uint rtn_no)
	{
		Routine3d routine3d = sub_window.routine_[sub_window.stack_].routine_3d[rtn_no];
		base_button_proc_tbl[routine3d.Rno_0](sub_window, rtn_no);
	}

	private static void base_button_init(SubWindow sub_window, uint rtn_no)
	{
		Routine3d routine3d = sub_window.routine_[sub_window.stack_].routine_3d[rtn_no];
		routine3d.disp_off = 1;
		routine3d.scale = 100;
		routine3d.Rno_0 = 1;
		routine3d.Rno_1 = 0;
	}

	private static void base_button_appear0(SubWindow sub_window, uint rtn_no)
	{
		Routine3d routine3d = sub_window.routine_[sub_window.stack_].routine_3d[rtn_no];
		switch (routine3d.Rno_1)
		{
		case 0:
			routine3d.disp_off = 0;
			routine3d.Rno_1++;
			break;
		case 1:
			routine3d.Rno_0 = 2;
			routine3d.Rno_1 = 0;
			routine3d.state |= 1;
			break;
		}
	}

	private static void base_button_appear1(SubWindow sub_window, uint rtn_no)
	{
		Routine3d routine3d = sub_window.routine_[sub_window.stack_].routine_3d[rtn_no];
		routine3d.disp_off = 0;
		routine3d.Rno_0 = 2;
		routine3d.Rno_1 = 0;
		routine3d.state |= 1;
	}

	private static void base_button_main(SubWindow sub_window, uint rtn_no)
	{
		Routine3d routine3d = sub_window.routine_[sub_window.stack_].routine_3d[rtn_no];
		routine3d.timer = 0;
		routine3d.flag = 0;
		routine3d.pallet = 0;
		routine3d.flag = 0;
	}

	private static void base_button_leave0(SubWindow sub_window, uint rtn_no)
	{
		Routine3d routine3d = sub_window.routine_[sub_window.stack_].routine_3d[rtn_no];
		routine3d.disp_off = 1;
		routine3d.state |= 2;
	}

	private static void base_button_leave1(SubWindow sub_window, uint rtn_no)
	{
		Routine3d routine3d = sub_window.routine_[sub_window.stack_].routine_3d[rtn_no];
		switch (routine3d.Rno_1)
		{
		case 0:
			routine3d.Rno_1++;
			break;
		case 1:
			routine3d.disp_off = 1;
			routine3d.state |= 2;
			break;
		}
	}

	private static void base_button_press(SubWindow sub_window, uint rtn_no)
	{
		Routine3d routine3d = sub_window.routine_[sub_window.stack_].routine_3d[rtn_no];
		switch (routine3d.Rno_1)
		{
		case 0:
			routine3d.Rno_1++;
			break;
		case 1:
			routine3d.scale = 100;
			routine3d.pallet = 0;
			routine3d.Rno_0 = 2;
			routine3d.Rno_1 = 0;
			break;
		}
	}

	private static void base_button_wait(SubWindow sub_window, uint rtn_no)
	{
	}
}
