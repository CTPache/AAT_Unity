public static class SubWindow_Questioning
{
	private delegate void QuestProc(SubWindow sub_window);

	private static readonly QuestProc[] proc_table;

	static SubWindow_Questioning()
	{
		proc_table = new QuestProc[8] { Init, Appear0, Appear1, PreMain, Main, Leave, Exit, Return0 };
	}

	public static void Proc(SubWindow sub_window)
	{
		proc_table[sub_window.GetCurrentRoutine().r.no_1](sub_window);
	}

	private static void Init(SubWindow sub_window)
	{
		Routine currentRoutine = sub_window.GetCurrentRoutine();
		Routine3d[] routine_3d = currentRoutine.routine_3d;
		switch (currentRoutine.r.no_2)
		{
		case 0:
		{
			sub_window.SetObjDispFlag(240);
			sub_window.bar_req_ = SubWindow.BarReq.PLANE;
			for (int i = 0; i < routine_3d.Length; i++)
			{
				routine_3d[i].Clear();
			}
			routine_3d[0].disp_off = 1;
			routine_3d[0].x = -256;
			routine_3d[0].y = 54;
			routine_3d[1].disp_off = 0;
			routine_3d[1].x = 0;
			routine_3d[1].y = 54;
			routine_3d[1].dmy = 0;
			routine_3d[2].dmy = 0;
			routine_3d[3].dmy = 0;
			routine_3d[2].disp_off = 1;
			routine_3d[2].x = 256;
			routine_3d[2].y = 132;
			routine_3d[3].disp_off = 0;
			routine_3d[3].x = 0;
			routine_3d[3].y = 132;
			currentRoutine.r.no_2++;
			break;
		}
		case 1:
			currentRoutine.r.no_1++;
			currentRoutine.r.no_2 = 0;
			currentRoutine.r.no_3 = 0;
			break;
		}
	}

	private static void Appear0(SubWindow sub_window)
	{
		GlobalWork global_work_ = GSStatic.global_work_;
		Routine currentRoutine = sub_window.GetCurrentRoutine();
		Routine3d[] routine_3d = currentRoutine.routine_3d;
		switch (currentRoutine.r.no_2)
		{
		case 0:
			switch (currentRoutine.r.no_3)
			{
			case 0:
				routine_3d[0].x = (routine_3d[2].x = 0);
				currentRoutine.r.no_3++;
				break;
			case 1:
				routine_3d[1].disp_off = 1;
				routine_3d[3].disp_off = 1;
				routine_3d[4].disp_off = 1;
				routine_3d[5].disp_off = 1;
				routine_3d[0].timer = 0;
				routine_3d[1].scale = 0;
				routine_3d[0].dmy = 25;
				currentRoutine.r.no_3++;
				break;
			case 2:
				if (global_work_.r.no_2 == 4)
				{
					currentRoutine.r.no_2++;
				}
				routine_3d[0].timer += 4;
				routine_3d[1].scale += routine_3d[0].dmy;
				if (routine_3d[1].dmy == 0)
				{
					if (routine_3d[1].scale >= 221)
					{
						routine_3d[1].scale = 221;
						routine_3d[1].dmy = 1;
						routine_3d[0].dmy = 0;
					}
				}
				else
				{
					routine_3d[0].dmy = 0;
					routine_3d[2].dmy++;
					if (routine_3d[2].dmy % 6 == 0)
					{
						routine_3d[2].dmy = 0;
						routine_3d[0].dmy = 1;
					}
				}
				if (routine_3d[0].timer >= 24)
				{
					routine_3d[0].timer = 24;
				}
				if (routine_3d[1].scale >= 256)
				{
					routine_3d[1].scale = 256;
					routine_3d[3].dmy++;
					if (routine_3d[3].dmy >= 20)
					{
						currentRoutine.r.no_3++;
					}
				}
				routine_3d[0].y = (short)(50 - routine_3d[0].timer);
				routine_3d[2].y = (short)(128 - routine_3d[0].timer);
				routine_3d[1].y = (short)(routine_3d[0].y + routine_3d[0].timer * 2 + 4);
				routine_3d[3].y = (short)(routine_3d[2].y + routine_3d[0].timer * 2 + 4);
				break;
			case 3:
				if (global_work_.r.no_2 == 4)
				{
					currentRoutine.r.no_2++;
				}
				break;
			}
			if (routine_3d[0].disp_off != 0)
			{
			}
			if (routine_3d[2].disp_off != 0)
			{
			}
			if (routine_3d[1].disp_off != 0)
			{
			}
			if (routine_3d[3].disp_off != 0)
			{
			}
			if (routine_3d[4].disp_off == 0 || routine_3d[0].timer > 0)
			{
			}
			if (routine_3d[5].disp_off != 0 && routine_3d[1].scale <= 0)
			{
			}
			break;
		case 1:
			currentRoutine.r.no_2++;
			break;
		case 2:
			currentRoutine.r.no_2++;
			break;
		case 3:
			if (sub_window.CheckObjOut())
			{
				currentRoutine.r.no_1 = 3;
				currentRoutine.r.no_2 = 0;
			}
			break;
		}
	}

	private static void Appear1(SubWindow sub_window)
	{
		Routine currentRoutine = sub_window.GetCurrentRoutine();
		Routine3d[] routine_3d = currentRoutine.routine_3d;
		switch (currentRoutine.r.no_2)
		{
		case 0:
		{
			sub_window.SetObjDispFlag(240);
			sub_window.bar_req_ = SubWindow.BarReq.TANTEI;
			for (int i = 0; i < 4; i++)
			{
				routine_3d[i].Clear();
			}
			routine_3d[0].disp_off = 1;
			routine_3d[0].x = -160;
			routine_3d[0].y = 24;
			routine_3d[1].x = 288;
			routine_3d[1].y = 24;
			routine_3d[2].x = -128;
			routine_3d[2].y = 158;
			routine_3d[3].x = 256;
			routine_3d[3].y = 158;
			currentRoutine.r.no_2++;
			break;
		}
		case 1:
			if (sub_window.CheckObjOut())
			{
				sub_window.SetObjDispFlag(4);
				sub_window.bar_req_ = SubWindow.BarReq.QUESTIONING;
				routine_3d[4].Rno_0 = 0;
				currentRoutine.r.no_2++;
			}
			break;
		case 2:
			SubWindow_Questioning_Button.Proc(sub_window, 4);
			if (((uint)routine_3d[4].state & (true ? 1u : 0u)) != 0 && sub_window.CheckObjIn() && sub_window.bar_req_ == SubWindow.BarReq.NONE)
			{
				sub_window.busy_ = 0u;
				sub_window.req_ = SubWindow.Req.NONE;
				for (ushort num = 0; num < currentRoutine.tp_cnt; num++)
				{
				}
				currentRoutine.tp_cnt = 0;
				currentRoutine.r.no_1 = 4;
				currentRoutine.r.no_2 = 0;
			}
			break;
		}
	}

	private static void PreMain(SubWindow sub_window)
	{
		GlobalWork global_work_ = GSStatic.global_work_;
		Routine currentRoutine = sub_window.GetCurrentRoutine();
		Routine3d[] routine_3d = currentRoutine.routine_3d;
		switch (currentRoutine.r.no_2)
		{
		case 0:
			routine_3d[4].Rno_0 = 0;
			SubWindow_Houtei_BaseButton.Proc(sub_window, 4);
			currentRoutine.r.no_2++;
			break;
		case 1:
			SubWindow_Houtei_BaseButton.Proc(sub_window, 4);
			if (((uint)routine_3d[4].state & (true ? 1u : 0u)) != 0)
			{
				sub_window.busy_ = 0u;
				sub_window.req_ = SubWindow.Req.NONE;
				currentRoutine.r.no_2++;
			}
			break;
		case 2:
			SubWindow_Houtei_BaseButton.Proc(sub_window, 4);
			TouchSystem.instance.UpdateDownEvent();
			if (global_work_.r.no_0 == 11)
			{
				if (sub_window.req_ == SubWindow.Req.SAVE)
				{
					for (ushort num2 = 0; num2 < currentRoutine.tp_cnt; num2++)
					{
					}
					currentRoutine.tp_cnt = 0;
					sub_window.busy_ = 3u;
					currentRoutine.r.Set(4, 3, 5, 0);
					debugLogger.instance.Log("SubWindowStack", "++stack_");
					sub_window.stack_++;
					Routine currentRoutine2 = sub_window.GetCurrentRoutine();
					currentRoutine2.r.Set(15, 0, 0, 0);
					currentRoutine2.tex_no = currentRoutine.tex_no;
				}
			}
			else if (sub_window.CheckFastMessage() || (GSStatic.message_work_.status & MessageSystem.Status.LOOP) != 0)
			{
				sub_window.busy_ = 3u;
				routine_3d[4].Rno_0 = 3;
				currentRoutine.r.no_2++;
			}
			else if ((padCtrl.instance.GetKey(KeyType.B) && optionCtrl.instance.skip_type != 0) || ((padCtrl.instance.GetKey(KeyType.A) || padCtrl.instance.GetKeyDown(KeyType.A) || padCtrl.instance.GetKeyDown(KeyType.B) || MessageSystem.GetTouchStatus() == MessageSystem.TouchStatus.Left) && (GSStatic.message_work_.status & MessageSystem.Status.FAST_MESSAGE) == 0))
			{
				sub_window.busy_ = 3u;
				routine_3d[4].Rno_0 = 3;
				currentRoutine.r.no_2++;
			}
			break;
		case 3:
			SubWindow_Houtei_BaseButton.Proc(sub_window, 4);
			if ((routine_3d[4].state & 2u) != 0 && sub_window.CheckObjOut())
			{
				sub_window.SetObjDispFlag(4);
				sub_window.bar_req_ = SubWindow.BarReq.QUESTIONING;
				routine_3d[4].Rno_0 = 0;
				currentRoutine.r.no_2++;
			}
			break;
		case 4:
			SubWindow_Questioning_Button.Proc(sub_window, 4);
			if (((uint)routine_3d[4].state & (true ? 1u : 0u)) != 0 && sub_window.CheckObjIn() && sub_window.bar_req_ == SubWindow.BarReq.NONE)
			{
				sub_window.busy_ = 0u;
				if (sub_window.req_ != SubWindow.Req.QUESTIONING_EXIT)
				{
					sub_window.req_ = SubWindow.Req.NONE;
				}
				for (ushort num = 0; num < currentRoutine.tp_cnt; num++)
				{
				}
				currentRoutine.tp_cnt = 0;
				currentRoutine.r.no_1 = 4;
				currentRoutine.r.no_2 = 0;
			}
			break;
		case 5:
			sub_window.busy_ = 3u;
			routine_3d[4].Rno_0 = 0;
			SubWindow_Houtei_BaseButton.Proc(sub_window, 4);
			sub_window.bar_req_ = SubWindow.BarReq.PLANE;
			currentRoutine.r.no_2 = 1;
			break;
		}
	}

	private static void Main(SubWindow sub_window)
	{
		Routine currentRoutine = sub_window.GetCurrentRoutine();
		SubWindow_Questioning_Button.Proc(sub_window, 4);
		switch (currentRoutine.r.no_2)
		{
		case 0:
			if (sub_window.req_ != 0)
			{
				for (ushort num = 0; num < currentRoutine.tp_cnt; num++)
				{
				}
				currentRoutine.tp_cnt = 0;
				sub_window.busy_ = 3u;
				if (sub_window.req_ == SubWindow.Req.QUESTIONING_EXIT)
				{
					debugLogger.instance.Log("routine.flag", "routine.flag=0");
					currentRoutine.flag = 0;
					currentRoutine.r.no_1 = 5;
					currentRoutine.r.no_2 = 0;
				}
				else if (sub_window.req_ == SubWindow.Req.STATUS)
				{
					debugLogger.instance.Log("routine.flag", "routine.flag=1");
					currentRoutine.flag = 1;
					currentRoutine.r.no_1 = 5;
					currentRoutine.r.no_2 = 0;
				}
				else if (sub_window.req_ == SubWindow.Req.QUESTIONING_YUSABURU)
				{
					debugLogger.instance.Log("routine.flag", "routine.flag=2");
					currentRoutine.flag = 2;
					currentRoutine.r.no_1 = 5;
					currentRoutine.r.no_2 = 0;
				}
				else if (sub_window.req_ == SubWindow.Req.SAVE)
				{
					currentRoutine.r.Set(4, 2, 0, 0);
					debugLogger.instance.Log("SubWindowStack", "++stack_");
					sub_window.stack_++;
					Routine currentRoutine2 = sub_window.GetCurrentRoutine();
					currentRoutine2.r.Set(15, 0, 0, 0);
					currentRoutine2.tex_no = currentRoutine.tex_no;
				}
				else if (sub_window.req_ == SubWindow.Req.SELECT)
				{
					debugLogger.instance.Log("routine.flag", "routine.flag=3");
					currentRoutine.flag = 3;
					currentRoutine.r.no_1 = 5;
					currentRoutine.r.no_2 = 0;
				}
			}
			else if ((GSStatic.message_work_.status & MessageSystem.Status.LOOP) != 0)
			{
				if (padCtrl.instance.GetKeyDown(KeyType.Y) && (sub_window.obj_flag_[58] & SubWindow.ObjFlag.DISP) == 0)
				{
					sub_window.bar_req_ = SubWindow.BarReq.QUESTIONING_2;
					sub_window.SetObjDispFlag(21);
					sub_window.busy_ = 3u;
					currentRoutine.r.no_2++;
				}
				if (!padCtrl.instance.GetKeyDown(KeyType.Y) && (sub_window.obj_flag_[58] & SubWindow.ObjFlag.IN) != 0)
				{
					sub_window.bar_req_ = SubWindow.BarReq.QUESTIONING;
					sub_window.SetObjDispFlag(4);
					sub_window.busy_ = 3u;
					currentRoutine.r.no_2++;
				}
			}
			break;
		case 1:
			if (sub_window.CheckObjIn() && sub_window.bar_req_ == SubWindow.BarReq.NONE)
			{
				sub_window.busy_ = 0u;
				currentRoutine.r.no_2--;
			}
			break;
		}
	}

	private static void Leave(SubWindow sub_window)
	{
		Routine currentRoutine = sub_window.GetCurrentRoutine();
		Routine3d[] routine_3d = currentRoutine.routine_3d;
		switch (currentRoutine.r.no_2)
		{
		case 0:
			sub_window.SetObjDispFlag(240);
			routine_3d[4].Rno_0 = 3;
			currentRoutine.r.no_2++;
			break;
		case 1:
			SubWindow_Questioning_Button.Proc(sub_window, 4);
			if ((routine_3d[4].state & 2u) != 0 && sub_window.CheckObjOut())
			{
				currentRoutine.r.no_2++;
			}
			break;
		case 2:
			if (currentRoutine.flag == 1)
			{
				currentRoutine.r.Set(4, 2, 0, 0);
				debugLogger.instance.Log("SubWindowStack", "++stack_");
				sub_window.stack_++;
				Routine currentRoutine2 = sub_window.GetCurrentRoutine();
				currentRoutine2.r.Set(11, 0, 0, 0);
				currentRoutine2.tex_no = currentRoutine.tex_no;
			}
			else if (currentRoutine.flag == 2)
			{
				sub_window.stack_ = 0;
				Routine currentRoutine3 = sub_window.GetCurrentRoutine();
				currentRoutine3.r.Set(2, 0, 1, 0);
				sub_window.SetObjDispFlag(2);
				sub_window.bar_req_ = SubWindow.BarReq.TANTEI;
			}
			else if (currentRoutine.flag == 3)
			{
				currentRoutine.r.Set(4, 2, 0, 0);
				debugLogger.instance.Log("SubWindowStack", "++stack_");
				sub_window.stack_++;
				Routine currentRoutine4 = sub_window.GetCurrentRoutine();
				currentRoutine4.r.Set(3, 0, 0, 0);
				currentRoutine4.tex_no = currentRoutine.tex_no;
			}
			else
			{
				sub_window.stack_--;
			}
			break;
		}
	}

	private static void Exit(SubWindow sub_window)
	{
	}

	private static void Return0(SubWindow sub_window)
	{
	}
}
