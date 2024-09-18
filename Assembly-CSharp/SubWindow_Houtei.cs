public static class SubWindow_Houtei
{
	private delegate void HouteiProc(SubWindow sub_window);

	private static readonly HouteiProc[] proc_table;

	static SubWindow_Houtei()
	{
		proc_table = new HouteiProc[8] { Init, Appear, Main, Leave, Back, Go3dPoint, Thrust, Last };
	}

	public static void Proc(SubWindow sub_window)
	{
		Routine currentRoutine = sub_window.GetCurrentRoutine();
		proc_table[currentRoutine.r.no_1](sub_window);
	}

	private static void Init(SubWindow sub_window)
	{
		Routine currentRoutine = sub_window.GetCurrentRoutine();
		Routine3d[] routine_3d = currentRoutine.routine_3d;
		Routine scan_ = sub_window.scan_;
		switch (currentRoutine.r.no_2)
		{
		case 0:
			scan_.flag = 1;
			if (sub_window.CheckObjOut())
			{
				routine_3d[0].Clear();
				routine_3d[1].Clear();
				routine_3d[3].Clear();
				routine_3d[4].Clear();
				routine_3d[5].Clear();
				routine_3d[0].disp_off = 1;
				routine_3d[1].disp_off = 1;
				routine_3d[1].x = 0;
				routine_3d[1].y = 18;
				routine_3d[2].Rno_0 = 0;
				SubWindow_Houtei_BaseButton.Proc(sub_window, 2);
				currentRoutine.r.no_1 = 1;
				currentRoutine.r.no_2 = 0;
			}
			else
			{
				currentRoutine.r.no_2++;
			}
			sub_window.SetObjDispFlag(2);
			sub_window.bar_req_ = SubWindow.BarReq.TANTEI;
			break;
		case 1:
			if (sub_window.CheckObjOut())
			{
				routine_3d[0].Clear();
				routine_3d[1].Clear();
				routine_3d[3].Clear();
				routine_3d[4].Clear();
				routine_3d[5].Clear();
				routine_3d[0].disp_off = 1;
				routine_3d[1].disp_off = 1;
				routine_3d[1].x = 0;
				routine_3d[1].y = 18;
				routine_3d[2].Rno_0 = 0;
				SubWindow_Houtei_BaseButton.Proc(sub_window, 2);
				currentRoutine.r.no_1 = 1;
				currentRoutine.r.no_2 = 0;
			}
			break;
		}
	}

	private static void Appear(SubWindow sub_window)
	{
		Routine currentRoutine = sub_window.GetCurrentRoutine();
		Routine3d[] routine_3d = currentRoutine.routine_3d;
		switch (currentRoutine.r.no_2)
		{
		case 0:
			SubWindow_Houtei_BaseButton.Proc(sub_window, 2);
			if (((uint)routine_3d[2].state & (true ? 1u : 0u)) != 0)
			{
				currentRoutine.r.no_2++;
			}
			break;
		case 1:
			SubWindow_Houtei_BaseButton.Proc(sub_window, 2);
			if (sub_window.CheckObjIn() || sub_window.bar_req_ == SubWindow.BarReq.NONE)
			{
				sub_window.busy_ = 0u;
				if (sub_window.req_ != SubWindow.Req.SELECT && sub_window.req_ != SubWindow.Req.STATUS_3D)
				{
					sub_window.req_ = SubWindow.Req.NONE;
				}
				for (ushort num = 0; num < currentRoutine.tp_cnt; num++)
				{
				}
				currentRoutine.tp_cnt = 0;
				currentRoutine.r.no_1 = 2;
				currentRoutine.r.no_2 = 0;
			}
			break;
		}
	}

	private static void Main(SubWindow sub_window)
	{
		Routine currentRoutine = sub_window.GetCurrentRoutine();
		Routine3d[] routine_3d = currentRoutine.routine_3d;
		SubWindow_Houtei_BaseButton.Proc(sub_window, 2);
		if (sub_window.req_ == SubWindow.Req.NONE)
		{
			return;
		}
		debugLogger.instance.Log("routine.flag", "routine.flag=0");
		currentRoutine.tp_cnt = 0;
		sub_window.SetObjDispFlag(240);
		sub_window.busy_ = 3u;
		if (sub_window.req_ == SubWindow.Req.HOUTEI_EXIT || sub_window.req_ == SubWindow.Req.IDLE)
		{
			currentRoutine.flag = 0;
			routine_3d[2].Rno_0 = 3;
			currentRoutine.r.no_1 = 3;
			currentRoutine.r.no_2 = 0;
		}
		else if (sub_window.req_ == SubWindow.Req.SELECT)
		{
			debugLogger.instance.Log("routine.flag", "routine.flag=1");
			currentRoutine.flag = 1;
			routine_3d[2].Rno_0 = 3;
			currentRoutine.r.no_1 = 3;
			currentRoutine.r.no_2 = 0;
		}
		else if (sub_window.req_ == SubWindow.Req.QUESTIONING_FIRST)
		{
			debugLogger.instance.Log("routine.flag", "routine.flag=2");
			currentRoutine.flag = 2;
			routine_3d[2].Rno_0 = 3;
			currentRoutine.r.no_1 = 3;
			currentRoutine.r.no_2 = 0;
		}
		else if (sub_window.req_ == SubWindow.Req.QUESTIONING)
		{
			debugLogger.instance.Log("routine.flag", "routine.flag=3");
			currentRoutine.flag = 3;
			routine_3d[2].Rno_0 = 3;
			currentRoutine.r.no_1 = 3;
			currentRoutine.r.no_2 = 0;
		}
		else if (sub_window.req_ == SubWindow.Req.STATUS)
		{
			debugLogger.instance.Log("routine.flag", "routine.flag=4");
			currentRoutine.flag = 4;
			routine_3d[2].Rno_0 = 3;
			currentRoutine.r.no_1 = 3;
			currentRoutine.r.no_2 = 0;
		}
		else if (sub_window.req_ == SubWindow.Req.BLANK)
		{
			debugLogger.instance.Log("routine.flag", "routine.flag=5");
			currentRoutine.flag = 5;
			routine_3d[2].Rno_0 = 3;
			currentRoutine.r.no_1 = 3;
			currentRoutine.r.no_2 = 0;
		}
		else if (sub_window.req_ == SubWindow.Req.POINT)
		{
			debugLogger.instance.Log("routine.flag", "routine.flag=6");
			currentRoutine.flag = 6;
			routine_3d[2].Rno_0 = 3;
			currentRoutine.r.no_1 = 3;
			currentRoutine.r.no_2 = 0;
		}
		else if (sub_window.req_ == SubWindow.Req._3D_POINT)
		{
			debugLogger.instance.Log("routine.flag", "routine.flag=7");
			currentRoutine.flag = 7;
			currentRoutine.r.no_1 = 5;
			currentRoutine.r.no_2 = 0;
		}
		else if (sub_window.req_ == SubWindow.Req.SAVE)
		{
			debugLogger.instance.Log("routine.flag", "routine.flag=8");
			currentRoutine.flag = 8;
			currentRoutine.r.Set(2, 4, 0, currentRoutine.flag);
			debugLogger.instance.Log("SubWindowStack", "++stack_");
			sub_window.stack_++;
			Routine currentRoutine2 = sub_window.GetCurrentRoutine();
			currentRoutine2.r.Set(15, 0, 0, 0);
			currentRoutine2.tex_no = currentRoutine.tex_no;
		}
		else if (sub_window.req_ == SubWindow.Req.EPISODE_CLEAR)
		{
			sub_window.bar_req_ = SubWindow.BarReq.IDLE;
			debugLogger.instance.Log("SubWindowStack", "stack_=0");
			sub_window.stack_ = 0;
			sub_window.GetCurrentRoutine().r.Set(1, 5, 0, 0);
		}
		else if (sub_window.req_ == SubWindow.Req.STATUS_3D)
		{
			currentRoutine.r.Set(2, 4, 0, 0);
			debugLogger.instance.Log("SubWindowStack", "++stack_");
			sub_window.stack_++;
			Routine currentRoutine3 = sub_window.GetCurrentRoutine();
			currentRoutine3.r.Set(11, 0, 0, 0);
			MessageWork activeMessageWork = MessageSystem.GetActiveMessageWork();
			if (activeMessageWork.now_no == 129)
			{
				sub_window.tutorial_ = 20;
			}
			currentRoutine3.tex_no = currentRoutine.tex_no;
		}
		else if (sub_window.req_ == SubWindow.Req.MOVIE_THRUST)
		{
			debugLogger.instance.Log("routine.flag", "routine.flag=11");
			currentRoutine.flag = 11;
			routine_3d[2].Rno_0 = 3;
			currentRoutine.r.no_1 = 3;
			currentRoutine.r.no_2 = 0;
		}
		else if (sub_window.req_ == SubWindow.Req.VASE_SHOW)
		{
			debugLogger.instance.Log("routine.flag", "routine.flag=12");
			currentRoutine.flag = 12;
			routine_3d[2].Rno_0 = 3;
			currentRoutine.r.no_1 = 3;
			currentRoutine.r.no_2 = 0;
		}
		else if (sub_window.req_ == SubWindow.Req.HUMAN)
		{
			debugLogger.instance.Log("routine.flag", "routine.flag=13");
			currentRoutine.flag = 13;
			routine_3d[2].Rno_0 = 3;
			currentRoutine.r.no_1 = 3;
			currentRoutine.r.no_2 = 0;
		}
		else if (sub_window.req_ == SubWindow.Req.DIE_MES)
		{
			debugLogger.instance.Log("routine.flag", "routine.flag=14");
			currentRoutine.flag = 14;
			routine_3d[2].Rno_0 = 3;
			currentRoutine.r.no_1 = 3;
			currentRoutine.r.no_2 = 0;
		}
		else if (sub_window.req_ == SubWindow.Req.GBA_STAFF)
		{
			sub_window.SetObjDispFlag(37);
			sub_window.bar_req_ = SubWindow.BarReq.PLANE;
			debugLogger.instance.Log("routine.flag", "routine.flag=15");
			currentRoutine.flag = 15;
			routine_3d[2].Rno_0 = 3;
			currentRoutine.r.no_1 = 3;
			currentRoutine.r.no_2 = 0;
		}
		else if (sub_window.req_ == SubWindow.Req.LAST_OBJECTION)
		{
			debugLogger.instance.Log("routine.flag", "routine.flag=16");
			currentRoutine.flag = 16;
			routine_3d[2].Rno_0 = 3;
			currentRoutine.r.no_1 = 3;
			currentRoutine.r.no_2 = 0;
		}
	}

	private static void Leave(SubWindow sub_window)
	{
		Routine currentRoutine = sub_window.GetCurrentRoutine();
		Routine3d[] routine_3d = currentRoutine.routine_3d;
		SubWindow_Houtei_BaseButton.Proc(sub_window, 2);
		if ((routine_3d[2].state & 2u) != 0 && sub_window.CheckObjIn())
		{
			sub_window.req_ = SubWindow.Req.NONE;
			if (currentRoutine.flag == 0)
			{
				currentRoutine.r.Set(0, 0, 0, 0);
			}
			else if (currentRoutine.flag == 1)
			{
				currentRoutine.r.Set(2, 4, currentRoutine.flag, 0);
				debugLogger.instance.Log("SubWindowStack", "++stack_");
				sub_window.stack_++;
				Routine currentRoutine2 = sub_window.GetCurrentRoutine();
				currentRoutine2.r.Set(3, 0, 0, 0);
				currentRoutine2.tex_no = currentRoutine.tex_no;
			}
			else if (currentRoutine.flag == 2)
			{
				currentRoutine.r.Set(2, 4, currentRoutine.flag, 0);
				debugLogger.instance.Log("SubWindowStack", "++stack_");
				sub_window.stack_++;
				Routine currentRoutine3 = sub_window.GetCurrentRoutine();
				currentRoutine3.r.Set(4, 0, 0, 0);
				currentRoutine3.tex_no = currentRoutine.tex_no;
			}
			else if (currentRoutine.flag == 3)
			{
				currentRoutine.r.Set(2, 4, currentRoutine.flag, 0);
				debugLogger.instance.Log("SubWindowStack", "++stack_");
				sub_window.stack_++;
				Routine currentRoutine4 = sub_window.GetCurrentRoutine();
				currentRoutine4.r.Set(4, 2, 0, 0);
				currentRoutine4.tex_no = currentRoutine.tex_no;
			}
			else if (currentRoutine.flag == 4)
			{
				currentRoutine.r.Set(2, 4, currentRoutine.flag, 0);
				debugLogger.instance.Log("SubWindowStack", "++stack_");
				sub_window.stack_++;
				Routine currentRoutine5 = sub_window.GetCurrentRoutine();
				currentRoutine5.r.Set(11, 0, 0, 0);
				currentRoutine5.tex_no = currentRoutine.tex_no;
			}
			else if (currentRoutine.flag == 5)
			{
				currentRoutine.r.Set(20, 0, 0, 0);
			}
			else if (currentRoutine.flag == 6)
			{
				currentRoutine.r.Set(2, 4, currentRoutine.flag, 0);
				debugLogger.instance.Log("SubWindowStack", "++stack_");
				sub_window.stack_++;
				Routine currentRoutine6 = sub_window.GetCurrentRoutine();
				currentRoutine6.r.Set(14, 0, 0, 0);
				currentRoutine6.tex_no = currentRoutine.tex_no;
			}
			else if (currentRoutine.flag == 10)
			{
				sub_window.bar_req_ = SubWindow.BarReq.IDLE;
				debugLogger.instance.Log("SubWindowStack", "stack_=0");
				sub_window.stack_ = 0;
				sub_window.GetCurrentRoutine().r.Set(1, 5, 2, 0);
			}
			else if (currentRoutine.flag == 11)
			{
				currentRoutine.r.Set(2, 4, currentRoutine.flag, 0);
				debugLogger.instance.Log("SubWindowStack", "++stack_");
				sub_window.stack_++;
				Routine currentRoutine7 = sub_window.GetCurrentRoutine();
				currentRoutine7.r.Set(29, 0, 0, 0);
				currentRoutine7.tex_no = currentRoutine.tex_no;
				ConfrontWithMovie.instance.pSmt.Clear();
				ConfrontWithMovie.instance.pSmt.atari_no = SubWindow.GS1_GetMovieProcNo(GSStatic.message_work_.now_no);
			}
			else if (currentRoutine.flag == 12)
			{
				currentRoutine.r.Set(2, 4, currentRoutine.flag, 0);
				debugLogger.instance.Log("SubWindowStack", "++stack_");
				sub_window.stack_++;
				Routine currentRoutine8 = sub_window.GetCurrentRoutine();
				currentRoutine8.r.Set(27, 0, 0, 0);
				currentRoutine8.tex_no = currentRoutine.tex_no;
			}
			else if (currentRoutine.flag == 13)
			{
				currentRoutine.r.Set(2, 4, currentRoutine.flag, 0);
				debugLogger.instance.Log("SubWindowStack", "++stack_");
				sub_window.stack_++;
				Routine currentRoutine9 = sub_window.GetCurrentRoutine();
				currentRoutine9.r.Set(16, 0, 0, GSStatic.global_work_.r.no_3);
				currentRoutine9.tex_no = currentRoutine.tex_no;
				GSStatic.global_work_.r.no_3 = 0;
			}
			else if (currentRoutine.flag == 14)
			{
				currentRoutine.r.Set(2, 4, currentRoutine.flag, 0);
				sub_window.stack_++;
				Routine currentRoutine10 = sub_window.GetCurrentRoutine();
				currentRoutine10.r.Set(25, 0, 0, 0);
				currentRoutine10.tex_no = currentRoutine.tex_no;
				GSStatic.global_work_.r.no_3 = 0;
			}
			else if (currentRoutine.flag == 15)
			{
				currentRoutine.r.no_1 = 2;
			}
			else if (currentRoutine.flag == 16)
			{
				currentRoutine.r.no_1 = 7;
				currentRoutine.r.no_2 = 0;
			}
		}
	}

	private static void Back(SubWindow sub_window)
	{
		Routine currentRoutine = sub_window.GetCurrentRoutine();
		byte no_ = currentRoutine.r.no_2;
		if (sub_window.note_add_ == 1)
		{
			if (GSStatic.global_work_.r.no_0 == 9)
			{
				GSStatic.global_work_.r.no_1 = 2;
			}
			sub_window.busy_ = 3u;
			sub_window.note_add_ = 0;
		}
		sub_window.SetObjDispFlag(2);
		sub_window.bar_req_ = SubWindow.BarReq.TANTEI;
		currentRoutine.r.Set(2, 0, 1, 0);
	}

	private static void Go3dPoint(SubWindow sub_window)
	{
		Routine currentRoutine = sub_window.GetCurrentRoutine();
		switch (currentRoutine.r.no_2)
		{
		case 0:
			currentRoutine.r.no_2++;
			break;
		case 1:
		{
			currentRoutine.r.Set(2, 4, 0, 0);
			debugLogger.instance.Log("SubWindowStack", "++stack_");
			sub_window.stack_++;
			Routine currentRoutine2 = sub_window.GetCurrentRoutine();
			currentRoutine2.r.Set(13, 0, 0, 0);
			currentRoutine2.tex_no = currentRoutine.tex_no;
			break;
		}
		}
	}

	private static void Thrust(SubWindow sub_window)
	{
		Routine currentRoutine = sub_window.GetCurrentRoutine();
		Routine3d[] routine_3d = currentRoutine.routine_3d;
		switch (currentRoutine.r.no_2)
		{
		case 0:
			sub_window.SetObjDispFlag(240);
			sub_window.bar_req_ = SubWindow.BarReq.PLANE;
			routine_3d[1].x = -256;
			routine_3d[1].y = 0;
			routine_3d[1].disp_off = 0;
			currentRoutine.r.no_2++;
			break;
		case 1:
			if (routine_3d[1].x + 16 < 0)
			{
				routine_3d[1].x += 16;
			}
			else
			{
				routine_3d[1].x = 0;
			}
			currentRoutine.r.no_2++;
			break;
		case 2:
			routine_3d[1].disp_off = 1;
			currentRoutine.r.no_2++;
			break;
		case 3:
			currentRoutine.r.no_1 = 4;
			currentRoutine.r.no_2 = 0;
			break;
		}
	}

	private static void Last(SubWindow sub_window)
	{
		if (GSStatic.global_work_.title != TitleId.GS3)
		{
			LastObjection(sub_window);
		}
		else
		{
			LastSketch(sub_window);
		}
	}

	private static void LastObjection(SubWindow sub_window)
	{
		Routine currentRoutine = sub_window.GetCurrentRoutine();
		GlobalWork global_work_ = GSStatic.global_work_;
		switch (currentRoutine.r.no_2)
		{
		case 0:
			sub_window.SetObjDispFlag(34);
			sub_window.bar_req_ = SubWindow.BarReq.LAST_OBJECTION;
			global_work_.r.no_1 = 10;
			currentRoutine.r.no_2++;
			break;
		case 1:
			if (sub_window.CheckObjOut())
			{
				currentRoutine.r.no_2++;
			}
			break;
		case 2:
			if (sub_window.CheckObjIn() && sub_window.bar_req_ == SubWindow.BarReq.NONE)
			{
				sub_window.req_ = SubWindow.Req.NONE;
				sub_window.busy_ = 0u;
				currentRoutine.r.no_2++;
			}
			break;
		case 3:
			if (padCtrl.instance.GetKeyDown(KeyType.Y) && (sub_window.obj_flag_[59] & SubWindow.ObjFlag.DISP) == 0)
			{
				sub_window.SetObjFlagOne(48, 1, 1);
				sub_window.SetObjFlagOne(59, 1, 0);
			}
			if (!padCtrl.instance.GetKeyDown(KeyType.Y) && (sub_window.obj_flag_[59] & SubWindow.ObjFlag.IN) != 0)
			{
				sub_window.SetObjFlagOne(48, 1, 0);
				sub_window.SetObjFlagOne(59, 1, 1);
			}
			if (sub_window.req_ == SubWindow.Req.SAVE)
			{
				currentRoutine.r.Set(2, 7, 0, 0);
				debugLogger.instance.Log("SubWindowStack", "++stack_");
				sub_window.stack_++;
				Routine currentRoutine2 = sub_window.GetCurrentRoutine();
				currentRoutine2.r.Set(15, 0, 0, 0);
				currentRoutine2.tex_no = currentRoutine.tex_no;
			}
			break;
		case 4:
			break;
		}
	}

	private static void LastSketch(SubWindow sub_window)
	{
		Routine currentRoutine = sub_window.GetCurrentRoutine();
		switch (currentRoutine.r.no_2)
		{
		case 0:
			sub_window.bar_req_ = SubWindow.BarReq.IDLE;
			sub_window.SetObjDispFlag(37);
			GS1_sce4_opening.instance.GS3LastSketchInit();
			currentRoutine.r.no_2++;
			break;
		case 1:
			sub_window.scan_.flag = 0;
			currentRoutine.r.no_2++;
			break;
		case 2:
			currentRoutine.r.no_2++;
			break;
		case 3:
		case 4:
			currentRoutine.r.no_2++;
			break;
		case 5:
			currentRoutine.r.no_2++;
			break;
		case 6:
			if (GSFlag.Check(0u, scenario_GS3.SCF_33_EPILOGUE))
			{
				currentRoutine.r.no_2++;
			}
			break;
		case 7:
			GS1_sce4_opening.instance.GS3LastSketchScroll();
			if (GSFlag.Check(0u, scenario_GS3.SCF_33_EPILOGUE2))
			{
				GS1_sce4_opening.instance.GS3LastSketchExit();
				currentRoutine.r.no_1 = 4;
				currentRoutine.r.no_2 = 0;
			}
			break;
		}
	}
}
