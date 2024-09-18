public static class SubWindow_Houtei_BaseButton
{
	private delegate void BaseButtonProc(SubWindow sub_window, byte routine_no);

	private static MessageSystem.Status base_mess_status;

	private static readonly BaseButtonProc[] proc_table;

	static SubWindow_Houtei_BaseButton()
	{
		proc_table = new BaseButtonProc[8] { Init, Appear0, Main, Leave0, Press, Appear1, Leave1, Wait };
	}

	public static void Proc(SubWindow sub_window, byte routine_no)
	{
		Routine currentRoutine = sub_window.GetCurrentRoutine();
		Routine3d routine3d = currentRoutine.routine_3d[routine_no];
		MessageWork activeMessageWork = MessageSystem.GetActiveMessageWork();
		proc_table[routine3d.Rno_0](sub_window, routine_no);
		if (routine3d.Rno_0 == 1 || routine3d.Rno_0 == 6 || routine3d.Rno_0 == 3)
		{
			if (routine3d.disp_off == 0 && (routine3d.flag != 0 || (activeMessageWork.status & (MessageSystem.Status.RT_WAIT | MessageSystem.Status.SELECT)) != 0 || routine3d.Rno_0 == 4) && routine3d.rotate[0] != 0)
			{
			}
		}
		else if (routine3d.disp_off == 0 && (routine3d.flag != 0 || (activeMessageWork.status & (MessageSystem.Status.RT_WAIT | MessageSystem.Status.SELECT)) != 0 || routine3d.Rno_0 == 4) && routine3d.rotate[0] != 0)
		{
		}
	}

	public static void Init(SubWindow sub_window, byte routine_no)
	{
		Routine currentRoutine = sub_window.GetCurrentRoutine();
		Routine3d routine3d = currentRoutine.routine_3d[routine_no];
		routine3d.Clear();
		routine3d.disp_off = 1;
		routine3d.rotate[0] = 16384;
		routine3d.scale = 100;
		routine3d.Rno_0 = 1;
		routine3d.Rno_1 = 0;
		if (GSStatic.global_work_.title != TitleId.GS3)
		{
		}
	}

	public static void Appear0(SubWindow sub_window, byte routine_no)
	{
		Routine currentRoutine = sub_window.GetCurrentRoutine();
		Routine3d routine3d = currentRoutine.routine_3d[routine_no];
		switch (routine3d.Rno_1)
		{
		case 0:
			routine3d.disp_off = 0;
			routine3d.rotate[0] = 49152;
			routine3d.Rno_1++;
			break;
		case 1:
			if (routine3d.rotate[0] + 2048 < 65535)
			{
				routine3d.rotate[0] += 2048;
				break;
			}
			routine3d.rotate[0] = 0;
			routine3d.Rno_0 = 2;
			routine3d.Rno_1 = 0;
			routine3d.state |= 1;
			break;
		}
	}

	public static void Main(SubWindow sub_window, byte routine_no)
	{
		Routine currentRoutine = sub_window.GetCurrentRoutine();
		Routine3d routine3d = currentRoutine.routine_3d[routine_no];
		MessageWork activeMessageWork = MessageSystem.GetActiveMessageWork();
		if (padCtrl.instance.GetKeyDown(KeyType.A) || padCtrl.instance.GetKeyDown(KeyType.B))
		{
			if (padCtrl.instance.GetKeyDown(KeyType.B))
			{
				if (sub_window.CheckFastMessage())
				{
					routine3d.pallet = 1;
					routine3d.flag = 11;
				}
				else if ((activeMessageWork.op_flg & 0xF0) >> 4 == 1 || (base_mess_status & MessageSystem.Status.READ_MESSAGE) == 0)
				{
					routine3d.pallet = 0;
					routine3d.flag = 0;
				}
				if (routine3d.pallet == 1)
				{
					if (routine3d.scale - 4 > 92)
					{
						routine3d.scale -= 4;
					}
					else
					{
						routine3d.scale = 92;
					}
				}
			}
			else if (padCtrl.instance.GetKeyDown(KeyType.A))
			{
				if ((base_mess_status & (MessageSystem.Status.RT_WAIT | MessageSystem.Status.SELECT)) != 0 || currentRoutine.r.no_0 == 4)
				{
					routine3d.Rno_0 = 4;
					routine3d.Rno_1 = 0;
					routine3d.pallet = 1;
				}
				else
				{
					routine3d.pallet = 0;
				}
			}
		}
		else
		{
			if (routine3d.scale + 8 < 100)
			{
				routine3d.scale += 8;
			}
			else
			{
				routine3d.scale = 100;
			}
			routine3d.timer = 0;
			routine3d.flag = 0;
			routine3d.pallet = 0;
			routine3d.flag = 0;
		}
		base_mess_status = activeMessageWork.status;
	}

	public static void Leave0(SubWindow sub_window, byte routine_no)
	{
		Routine currentRoutine = sub_window.GetCurrentRoutine();
		Routine3d routine3d = currentRoutine.routine_3d[routine_no];
		if (routine3d.rotate[0] + 2048 < 16384)
		{
			routine3d.rotate[0] += 2048;
			return;
		}
		routine3d.rotate[0] = 16384;
		routine3d.disp_off = 1;
		routine3d.state |= 2;
	}

	public static void Press(SubWindow sub_window, byte routine_no)
	{
		Routine currentRoutine = sub_window.GetCurrentRoutine();
		Routine3d routine3d = currentRoutine.routine_3d[routine_no];
		switch (routine3d.Rno_1)
		{
		case 0:
			if (routine3d.scale - 4 > 92)
			{
				routine3d.scale -= 4;
				break;
			}
			routine3d.scale = 92;
			routine3d.Rno_1++;
			break;
		case 1:
			if (routine3d.scale + 8 < 100)
			{
				routine3d.scale += 8;
				break;
			}
			routine3d.scale = 100;
			routine3d.pallet = 0;
			routine3d.Rno_0 = 2;
			routine3d.Rno_1 = 0;
			break;
		}
	}

	public static void Appear1(SubWindow sub_window, byte routine_no)
	{
		Routine currentRoutine = sub_window.GetCurrentRoutine();
		Routine3d routine3d = currentRoutine.routine_3d[routine_no];
		routine3d.disp_off = 0;
		if (routine3d.rotate[0] + 2048 < 65535)
		{
			routine3d.rotate[0] += 2048;
			return;
		}
		routine3d.rotate[0] = 0;
		routine3d.Rno_0 = 2;
		routine3d.Rno_1 = 0;
		routine3d.state |= 1;
	}

	public static void Leave1(SubWindow sub_window, byte routine_no)
	{
		Routine currentRoutine = sub_window.GetCurrentRoutine();
		Routine3d routine3d = currentRoutine.routine_3d[routine_no];
		switch (routine3d.Rno_1)
		{
		case 0:
			routine3d.rotate[0] = ushort.MaxValue;
			routine3d.Rno_1++;
			break;
		case 1:
			if (routine3d.rotate[0] - 2048 > 49152)
			{
				routine3d.rotate[0] -= 2048;
				break;
			}
			routine3d.rotate[0] = 49152;
			routine3d.disp_off = 1;
			routine3d.state |= 2;
			break;
		}
	}

	public static void Wait(SubWindow sub_window, byte routine_no)
	{
	}
}
