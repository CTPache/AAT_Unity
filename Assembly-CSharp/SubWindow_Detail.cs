public static class SubWindow_Detail
{
	private delegate void DetailProc(SubWindow sub_window);

	private static readonly DetailProc[] proc_table;

	static SubWindow_Detail()
	{
		proc_table = new DetailProc[9] { Init, Main, PageChange, ModeChange, Special, Thrust, ThrustBack, Back, Tutorial };
	}

	public static void Proc(SubWindow sub_window)
	{
		Routine currentRoutine = sub_window.GetCurrentRoutine();
		proc_table[currentRoutine.r.no_1](sub_window);
	}

	private static void Init(SubWindow sub_window)
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
		{
			sub_window.busy_ = 0u;
			sub_window.req_ = SubWindow.Req.NONE;
			if (recordListCtrl.instance.detail_obj_id != 0)
			{
				currentRoutine.r.no_1 = 1;
				currentRoutine.r.no_2 = 1;
				break;
			}
			int detail_data_id = recordListCtrl.instance.detail_data_id;
			if (piceDataCtrl.instance.status_ext_bg_tbl[detail_data_id].page_num == 255)
			{
				ConfrontWithMovie.instance.StartDetail();
				currentRoutine.r.no_2 = 9;
			}
			else
			{
				recordListCtrl.instance.detailPlay();
				currentRoutine.r.no_1 = 1;
				currentRoutine.r.no_2 = 0;
			}
			break;
		}
		case 9:
			if (padCtrl.instance.GetKeyDown(KeyType.B))
			{
				ConfrontWithMovie.instance.StopDetail();
				currentRoutine.r.no_2++;
			}
			break;
		case 10:
			if (!ConfrontWithMovie.instance.controller.Cinema_check_end())
			{
				currentRoutine.r.no_1 = 1;
				currentRoutine.r.no_2 = 0;
			}
			break;
		}
	}

	private static void Main(SubWindow sub_window)
	{
		Routine currentRoutine = sub_window.GetCurrentRoutine();
		switch (currentRoutine.r.no_2)
		{
		case 0:
			if (!ConfrontWithMovie.instance.IsDetailing && !recordListCtrl.instance.detail_open)
			{
				sub_window.busy_ = 3u;
				GSStatic.global_work_.r.no_1 = 1;
				sub_window.stack_--;
			}
			break;
		case 1:
			currentRoutine.r.Set(12, 6, 0, 0);
			sub_window.stack_++;
			sub_window.routine_[sub_window.stack_].r.Set(13, 0, 0, 0);
			sub_window.routine_[sub_window.stack_].flag = 0;
			break;
		}
	}

	private static void PageChange(SubWindow sub_window)
	{
		Routine currentRoutine = sub_window.GetCurrentRoutine();
		switch (currentRoutine.r.no_2)
		{
		}
	}

	private static void ModeChange(SubWindow sub_window)
	{
		Routine currentRoutine = sub_window.GetCurrentRoutine();
		switch (currentRoutine.r.no_2)
		{
		case 0:
			break;
		case 1:
			break;
		case 2:
			break;
		case 3:
			break;
		}
	}

	private static void Special(SubWindow sub_window)
	{
		Routine currentRoutine = sub_window.GetCurrentRoutine();
		switch (currentRoutine.r.no_2)
		{
		case 0:
			break;
		case 1:
			break;
		case 2:
			break;
		case 3:
			break;
		case 4:
			break;
		case 5:
			break;
		case 6:
			break;
		case 7:
			break;
		case 8:
			break;
		case 9:
			break;
		case 10:
			break;
		}
	}

	private static void Thrust(SubWindow sub_window)
	{
		Routine currentRoutine = sub_window.GetCurrentRoutine();
		switch (currentRoutine.r.no_2)
		{
		case 0:
			break;
		case 1:
			break;
		case 2:
			break;
		case 3:
			break;
		case 4:
			break;
		}
	}

	private static void Back(SubWindow sub_window)
	{
		Routine currentRoutine = sub_window.GetCurrentRoutine();
		switch (currentRoutine.r.no_2)
		{
		}
	}

	private static void ThrustBack(SubWindow sub_window)
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
			currentRoutine.r.no_1 = 1;
			currentRoutine.r.no_2 = 0;
			break;
		}
	}

	private static void Tutorial(SubWindow sub_window)
	{
		Routine currentRoutine = sub_window.GetCurrentRoutine();
		switch (currentRoutine.r.no_2)
		{
		}
	}

	private static void disp_sw_status_detail(SubWindow sub_window)
	{
	}
}
