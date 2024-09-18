public static class SubWindow_Questioning_Button
{
	private delegate void ButtonProc(SubWindow sub_window, byte routine_no);

	private static readonly ButtonProc[] proc_table;

	private static readonly GSRect[] disp_table;

	static SubWindow_Questioning_Button()
	{
		disp_table = new GSRect[2]
		{
			new GSRect(16, 65, 106, 80),
			new GSRect(134, 65, 106, 80)
		};
		proc_table = new ButtonProc[5] { Init, Appear, Main, Leave, Press };
	}

	public static void Proc(SubWindow sub_window, byte routine_no)
	{
		Routine currentRoutine = sub_window.GetCurrentRoutine();
		OffsetedArray<Routine3d> offsetedArray = new OffsetedArray<Routine3d>(currentRoutine.routine_3d, routine_no);
		proc_table[offsetedArray[0].Rno_0](sub_window, routine_no);
		if (offsetedArray[0].disp_off == 0)
		{
			if (offsetedArray[0].pallet == 3 || (GSStatic.message_work_.status & MessageSystem.Status.LOOP) == 0 || offsetedArray[0].Rno_0 == 2)
			{
			}
			if (offsetedArray[1].pallet != 3 && (GSStatic.message_work_.status & MessageSystem.Status.LOOP) != 0 && offsetedArray[0].Rno_0 != 2)
			{
			}
		}
	}

	private static void Init(SubWindow sub_window, byte routine_no)
	{
		Routine currentRoutine = sub_window.GetCurrentRoutine();
		OffsetedArray<Routine3d> offsetedArray = new OffsetedArray<Routine3d>(currentRoutine.routine_3d, routine_no);
		offsetedArray[0].Clear();
		offsetedArray[1].Clear();
		offsetedArray[0].x = (short)(-disp_table[0].w);
		offsetedArray[0].y = disp_table[0].y;
		offsetedArray[0].scale = 100;
		offsetedArray[1].x = 256;
		offsetedArray[1].y = disp_table[0].y;
		offsetedArray[1].scale = 100;
		offsetedArray[0].disp_off = 1;
		offsetedArray[0].Rno_0 = 1;
		offsetedArray[0].Rno_1 = 0;
		if (GSStatic.global_work_.title != TitleId.GS3)
		{
		}
	}

	private static void Appear(SubWindow sub_window, byte routine_no)
	{
		Routine currentRoutine = sub_window.GetCurrentRoutine();
		OffsetedArray<Routine3d> offsetedArray = new OffsetedArray<Routine3d>(currentRoutine.routine_3d, routine_no);
		offsetedArray[0].disp_off = 0;
		if (offsetedArray[0].x < disp_table[0].x)
		{
			offsetedArray[0].x += 10;
		}
		else
		{
			offsetedArray[0].x = disp_table[0].x;
		}
		if (offsetedArray[1].x > disp_table[1].x)
		{
			offsetedArray[1].x -= 10;
		}
		else
		{
			offsetedArray[1].x = disp_table[1].x;
		}
		if (offsetedArray[0].x == disp_table[0].x && offsetedArray[1].x == disp_table[1].x)
		{
			offsetedArray[0].Rno_0 = 2;
			offsetedArray[0].Rno_1 = 0;
			offsetedArray[0].state |= 1;
		}
	}

	private static void Main(SubWindow sub_window, byte routine_no)
	{
		Routine currentRoutine = sub_window.GetCurrentRoutine();
		OffsetedArray<Routine3d> offsetedArray = new OffsetedArray<Routine3d>(currentRoutine.routine_3d, routine_no);
		if ((GSStatic.message_work_.status & MessageSystem.Status.LOOP) != 0)
		{
			for (ushort num = 0; num < 2; num++)
			{
			}
		}
		else if (sub_window.req_ == SubWindow.Req.QUESTIONING_NEXT)
		{
			sub_window.req_ = SubWindow.Req.NONE;
			offsetedArray[0].flag = 1;
			offsetedArray[0].Rno_0 = 4;
			offsetedArray[0].Rno_1 = 0;
			offsetedArray[1].pallet = 1;
		}
		else if (sub_window.req_ == SubWindow.Req.QUESTIONING_PREV)
		{
			sub_window.req_ = SubWindow.Req.NONE;
			offsetedArray[0].flag = 0;
			offsetedArray[0].Rno_0 = 4;
			offsetedArray[0].Rno_1 = 0;
			offsetedArray[0].pallet = 1;
		}
	}

	private static void Leave(SubWindow sub_window, byte routine_no)
	{
		Routine currentRoutine = sub_window.GetCurrentRoutine();
		OffsetedArray<Routine3d> offsetedArray = new OffsetedArray<Routine3d>(currentRoutine.routine_3d, routine_no);
		if (offsetedArray[0].x - 8 > -106)
		{
			offsetedArray[0].x -= 8;
		}
		else
		{
			offsetedArray[0].x = -106;
		}
		if (offsetedArray[1].x + 8 < 256)
		{
			offsetedArray[1].x += 8;
		}
		else
		{
			offsetedArray[1].x = 256;
		}
		if (offsetedArray[0].x == -106 && offsetedArray[1].x == 256)
		{
			offsetedArray[0].Rno_0 = 0;
			offsetedArray[0].Rno_1 = 0;
			offsetedArray[0].state |= 2;
		}
	}

	private static void Press(SubWindow sub_window, byte routine_no)
	{
		Routine currentRoutine = sub_window.GetCurrentRoutine();
		OffsetedArray<Routine3d> offsetedArray = new OffsetedArray<Routine3d>(currentRoutine.routine_3d, routine_no);
		switch (offsetedArray[0].Rno_1)
		{
		case 0:
			if (offsetedArray[offsetedArray[0].flag].scale - 4 > 95)
			{
				offsetedArray[offsetedArray[0].flag].scale -= 4;
				break;
			}
			offsetedArray[offsetedArray[0].flag].scale = 95;
			offsetedArray[0].Rno_1++;
			break;
		case 1:
			if (offsetedArray[offsetedArray[0].flag].scale + 8 < 100)
			{
				offsetedArray[offsetedArray[0].flag].scale += 8;
				break;
			}
			offsetedArray[offsetedArray[0].flag].scale = 100;
			offsetedArray[offsetedArray[0].flag].pallet = 0;
			offsetedArray[0].Rno_1++;
			break;
		case 2:
		{
			if ((GSStatic.message_work_.status & MessageSystem.Status.LOOP) != 0)
			{
				offsetedArray[0].Rno_0 = 2;
				offsetedArray[0].Rno_1 = 0;
				break;
			}
			for (ushort num = 0; num < 2; num++)
			{
			}
			break;
		}
		}
		if (offsetedArray[1 - offsetedArray[0].flag].scale + 8 < 100)
		{
			offsetedArray[1 - offsetedArray[0].flag].scale += 8;
		}
		else
		{
			offsetedArray[1 - offsetedArray[0].flag].scale = 100;
		}
	}
}
