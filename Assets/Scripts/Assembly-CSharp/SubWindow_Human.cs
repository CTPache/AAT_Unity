public static class SubWindow_Human
{
	public enum SW_HUMAN
	{
		AKANE = 0,
		TOMOE = 1,
		ZAIMON = 2,
		KYOUKA = 3,
		TADASIKI = 4,
		HARABAI = 5,
		GANTO = 6,
		ITONOKO = 7
	}

	public class HUMAN_INFO
	{
		public uint id;

		public uint correct_no;

		public uint[] dmy = new uint[2];

		public uint mes_no_true;

		public uint mes_no_false;

		public HUMAN_INFO(uint in_id, uint in_correct_no, uint in_dmy00, uint in_dmy01, uint in_mes_no_true, uint in_mes_no_false)
		{
			id = in_id;
			correct_no = in_correct_no;
			dmy[0] = in_dmy00;
			dmy[1] = in_dmy01;
			mes_no_true = in_mes_no_true;
			mes_no_false = in_mes_no_false;
		}
	}

	private delegate void HumanProc(SubWindow sub_window);

	public static uint[] human_idx_tbl;

	public static HUMAN_INFO[] human_info;

	private static readonly HumanProc[] proc_table;

	static SubWindow_Human()
	{
		human_idx_tbl = new uint[8] { 6u, 5u, 2u, 4u, 7u, 1u, 0u, 3u };
		human_info = new HUMAN_INFO[9]
		{
			new HUMAN_INFO(0u, 65535u, 0u, 0u, 0u, 0u),
			new HUMAN_INFO(1u, 65535u, 0u, 0u, 0u, 0u),
			new HUMAN_INFO(2u, 65535u, 0u, 0u, 0u, 0u),
			new HUMAN_INFO(3u, scenario.NOTE_ZAIMON, 0u, 0u, 184u, 183u),
			new HUMAN_INFO(4u, scenario.NOTE_GANTO, 0u, 0u, 171u, 169u),
			new HUMAN_INFO(5u, scenario.NOTE_GANTO, 0u, 0u, 132u, 131u),
			new HUMAN_INFO(6u, scenario.NOTE_TOMOE2, 0u, 0u, 169u, 168u),
			new HUMAN_INFO(7u, scenario.NOTE_TOMOE2, 0u, 0u, 187u, 186u),
			new HUMAN_INFO(8u, scenario.NOTE_AKANE2, 0u, 0u, scenario.SC4_69800, scenario.SC4_69790)
		};
		proc_table = new HumanProc[8] { Init, Appear, Main, Leave, ToDetail, FromDetail, Thrust, ToDetail2 };
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
		{
			SubWindowNote note_ = advCtrl.instance.sub_window_.note_;
			note_.current_mode = 1;
			note_.current_mode_old = 0;
			note_.item_cursor = 0;
			note_.item_cursor_old = 0;
			note_.man_cursor = 0;
			note_.man_cursor_old = 0;
			note_.item_page = 0;
			note_.item_page_old = 0;
			note_.man_page = 0;
			note_.man_page_old = 0;
			recordListCtrl.instance.record_type = 1;
			recordListCtrl.instance.is_change = false;
			recordListCtrl.instance.select_type = true;
			recordListCtrl.instance.is_back = false;
			recordListCtrl.instance.noteOpen(false, recordListCtrl.instance.record_type, currentRoutine.r.no_3 >= 3);
			currentRoutine.r.no_2++;
			break;
		}
		case 1:
			currentRoutine.r.no_1 = 1;
			currentRoutine.r.no_2 = 0;
			break;
		}
	}

	public static void Appear(SubWindow sub_window)
	{
		Routine currentRoutine = sub_window.GetCurrentRoutine();
		switch (currentRoutine.r.no_2)
		{
		case 0:
			currentRoutine.r.no_2++;
			break;
		case 1:
			sub_window.busy_ = 0u;
			sub_window.req_ = SubWindow.Req.NONE;
			currentRoutine.r.no_1 = 2;
			currentRoutine.r.no_2 = 0;
			break;
		}
	}

	public static void Main(SubWindow sub_window)
	{
		Routine currentRoutine = sub_window.GetCurrentRoutine();
		if (currentRoutine.r.no_2 == 0 && !recordListCtrl.instance.is_open)
		{
			sub_window.busy_ = 3u;
			currentRoutine.r.no_1 = 6;
			currentRoutine.r.no_2 = 0;
		}
	}

	public static void Leave(SubWindow sub_window)
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
			sub_window.stack_--;
			sub_window.routine_[sub_window.stack_].r.no_2 = 3;
			break;
		}
	}

	public static void ToDetail(SubWindow sub_window)
	{
		Routine currentRoutine = sub_window.GetCurrentRoutine();
		switch (currentRoutine.r.no_2)
		{
		case 0:
			currentRoutine.r.no_2++;
			break;
		case 1:
			currentRoutine.r.Set(16, 5, 0, currentRoutine.r.no_3);
			sub_window.stack_++;
			sub_window.routine_[sub_window.stack_].r.Set(17, 0, 0, currentRoutine.r.no_3);
			break;
		}
	}

	public static void FromDetail(SubWindow sub_window)
	{
		Routine currentRoutine = sub_window.GetCurrentRoutine();
		if (GSUtility.GetLanguageLayoutType(GSStatic.global_work_.language) == "USA" && currentRoutine.r.no_3 > 2)
		{
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
				currentRoutine.r.no_2++;
				break;
			case 5:
				sub_window.busy_ = 0u;
				sub_window.req_ = SubWindow.Req.NONE;
				currentRoutine.r.no_1 = 2;
				currentRoutine.r.no_2 = 0;
				break;
			}
		}
		else
		{
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
				currentRoutine.r.no_2++;
				break;
			case 5:
				currentRoutine.r.no_1 = 2;
				currentRoutine.r.no_2 = 0;
				break;
			}
		}
	}

	public static void Thrust(SubWindow sub_window)
	{
		Routine currentRoutine = sub_window.GetCurrentRoutine();
		switch (currentRoutine.r.no_2)
		{
		case 0:
			if (currentRoutine.r.no_3 > 2)
			{
				Balloon.PlayTakeThat();
				fadeCtrl.instance.play(3u, 1u, 4u, 31u);
			}
			currentRoutine.r.no_2++;
			goto case 1;
		case 1:
			if (fadeCtrl.instance.status == fadeCtrl.Status.NO_FADE)
			{
				currentRoutine.r.no_2++;
			}
			break;
		case 2:
			currentRoutine.r.no_2++;
			break;
		case 3:
			if (!objMoveCtrl.instance.is_play)
			{
				currentRoutine.r.no_2++;
			}
			break;
		case 4:
		{
			int num = recordListCtrl.instance.selectNoteID();
			if (human_info[currentRoutine.r.no_3].correct_no == num)
			{
				advCtrl.instance.message_system_.SetMessage(human_info[currentRoutine.r.no_3].mes_no_true);
			}
			else
			{
				advCtrl.instance.message_system_.SetMessage(human_info[currentRoutine.r.no_3].mes_no_false);
			}
			sub_window.stack_--;
			break;
		}
		}
	}

	public static void ToDetail2(SubWindow sub_window)
	{
		Routine currentRoutine = sub_window.GetCurrentRoutine();
		switch (currentRoutine.r.no_2)
		{
		case 0:
			currentRoutine.r.no_2++;
			break;
		case 1:
			currentRoutine.r.Set(16, 5, 0, currentRoutine.r.no_3);
			sub_window.stack_++;
			sub_window.routine_[sub_window.stack_].r.Set(17, 0, 0, currentRoutine.r.no_3);
			break;
		}
		switch (currentRoutine.r.no_2)
		{
		case 0:
			currentRoutine.r.no_2++;
			break;
		case 1:
			currentRoutine.r.Set(16, 5, 0, currentRoutine.r.no_3);
			sub_window.stack_++;
			sub_window.routine_[sub_window.stack_].r.Set(17, 0, 0, currentRoutine.r.no_3);
			break;
		}
	}

	public static void disp_sw_human_list(SubWindow sub_window)
	{
	}

	public static void disp_sw_human_list2(SubWindow sub_window)
	{
	}
}
