public static class SubWindow_StatusDetail
{
	private delegate void StatusDetailProc(SubWindow sub_window);

	private static readonly StatusDetailProc[] proc_table;

	static SubWindow_StatusDetail()
	{
		proc_table = new StatusDetailProc[9] { Init, Main, PageChange, ModeChange, Special, Thrust, Back, ThrustBack, Tutorial };
	}

	public static void Proc(SubWindow sub_window)
	{
		Routine currentRoutine = sub_window.GetCurrentRoutine();
		proc_table[currentRoutine.r.no_1](sub_window);
	}

	private static void Init(SubWindow sub_window)
	{
		GlobalWork global_work_ = GSStatic.global_work_;
		Routine currentRoutine = sub_window.GetCurrentRoutine();
		Routine3d[] routine_3d = currentRoutine.routine_3d;
		StatusWork status_work_ = GSStatic.status_work_;
		SubWindowNote note_ = sub_window.note_;
		switch (currentRoutine.r.no_2)
		{
		case 0:
		{
			for (int i = 0; i < 6; i++)
			{
				routine_3d[i].Clear();
			}
			routine_3d[1].disp_off = 1;
			routine_3d[3].disp_off = 0;
			if (sub_window.tantei_tukituke_ == 1)
			{
				note_.man_page = note_.man_page_old;
				note_.man_cursor = note_.man_cursor_old;
				note_.item_page = note_.item_page_old;
				note_.item_cursor = note_.item_cursor_old;
				if (note_.current_mode_old != 0)
				{
					short num = (short)(note_.man_page * 8 + note_.man_cursor);
					if (status_work_.name_file[num] == byte.MaxValue)
					{
						note_.man_page = 0;
						note_.man_cursor = 0;
					}
				}
				else
				{
					short num = (short)(note_.item_page * 8 + note_.item_cursor);
					if (status_work_.item_file[num] == byte.MaxValue)
					{
						note_.item_page = 0;
						note_.item_cursor = 0;
					}
				}
				note_.current_mode = note_.current_mode_old;
				note_.rotate = 0;
				routine_3d[0].x = 24;
				routine_3d[0].h = -48;
				routine_3d[1].x = 352;
				routine_3d[4].x = 352;
				global_work_.r.no_1 = 8;
			}
			currentRoutine.r.no_2++;
			break;
		}
		case 1:
			sub_window.SetObjDispPrevFlag();
			if (global_work_.psy_menu_active_flag != 0)
			{
				sub_window.SetObjFlagOne(2, 1, 0);
			}
			if ((global_work_.status_flag & 0x100u) != 0 || sub_window.status_force_ != 0)
			{
				sub_window.SetObjFlagOne(2, 1, 0);
			}
			SetBarReq(sub_window, 12);
			currentRoutine.r.no_2++;
			break;
		case 2:
			if (sub_window.CheckObjIn() && sub_window.bar_req_ == SubWindow.BarReq.NONE)
			{
				sub_window.busy_ = 0u;
				sub_window.req_ = SubWindow.Req.NONE;
				if (sub_window.tutorial_ != 0)
				{
					currentRoutine.r.no_1 = 8;
					currentRoutine.r.no_2 = 0;
				}
				else if (sub_window.tantei_tukituke_ == 1)
				{
					sub_window.tantei_tukituke_ = 0;
					currentRoutine.r.no_1 = 7;
					currentRoutine.r.no_2 = 0;
				}
				else
				{
					currentRoutine.r.no_1 = 1;
					currentRoutine.r.no_2 = 0;
				}
			}
			break;
		}
	}

	private static void Main(SubWindow sub_window)
	{
		GlobalWork global_work_ = GSStatic.global_work_;
		Routine currentRoutine = sub_window.GetCurrentRoutine();
		Routine3d[] routine_3d = currentRoutine.routine_3d;
		StatusWork status_work_ = GSStatic.status_work_;
		SubWindowNote note_ = sub_window.note_;
		switch (currentRoutine.r.no_2)
		{
		case 0:
		{
			short num;
			short num2;
			short num3;
			if (note_.current_mode == 0)
			{
				num = note_.item_cursor;
				num2 = (short)(status_work_.item_page_max - note_.item_page * 8);
				if (num2 > 8)
				{
					num2 = 8;
				}
				num3 = status_work_.item_page_max;
			}
			else
			{
				num = note_.man_cursor;
				num2 = (short)(status_work_.name_page_max - note_.man_page * 8);
				if (num2 > 8)
				{
					num2 = 8;
				}
				num3 = status_work_.name_page_max;
			}
			short num4 = num;
			if (num3 > 1 && sub_window.req_ == SubWindow.Req.NONE)
			{
				if (padCtrl.instance.GetKeyDown(KeyType.Right) || padCtrl.instance.GetKeyDown(KeyType.StickL_Right))
				{
					num++;
					routine_3d[1].flag = -1;
				}
				else if (padCtrl.instance.GetKeyDown(KeyType.Left) || padCtrl.instance.GetKeyDown(KeyType.StickL_Left))
				{
					num--;
					routine_3d[1].flag = 1;
				}
			}
			if (num4 != num)
			{
				if (num < 0)
				{
					if ((note_.current_mode == 0 && note_.item_page_max > 0) || (note_.current_mode == 1 && note_.man_page_max > 0))
					{
						if ((note_.current_mode == 0 && note_.item_page > 0) || (note_.current_mode == 1 && note_.man_page > 0))
						{
							debugLogger.instance.Log("routine.flag", "routine.flag=0x17");
							currentRoutine.flag = 23;
						}
						else
						{
							debugLogger.instance.Log("routine.flag", "routine.flag=0x87");
							currentRoutine.flag = 135;
						}
					}
					else if (num2 - 1 < 7)
					{
						currentRoutine.flag = (byte)(num2 - 1);
					}
					else
					{
						debugLogger.instance.Log("routine.flag", "routine.flag=7");
						currentRoutine.flag = 7;
					}
				}
				else if (num >= num2)
				{
					if ((note_.current_mode == 0 && note_.item_page_max > 0) || (note_.current_mode == 1 && note_.man_page_max > 0))
					{
						if ((note_.current_mode == 0 && note_.item_page < note_.item_page_max) || (note_.current_mode == 1 && note_.man_page < note_.man_page_max))
						{
							debugLogger.instance.Log("routine.flag", "routine.flag=0x20");
							currentRoutine.flag = 32;
						}
						else
						{
							debugLogger.instance.Log("routine.flag", "routine.flag=0x40");
							currentRoutine.flag = 64;
						}
					}
					else
					{
						debugLogger.instance.Log("routine.flag", "routine.flag=0x00");
						currentRoutine.flag = 0;
					}
				}
				else
				{
					debugLogger.instance.Log("routine.flag", "routine.flag=0x00 | cursor");
					currentRoutine.flag = (byte)(0u | (uint)num);
				}
				for (short num5 = 0; num5 < currentRoutine.tp_cnt; num5++)
				{
				}
				currentRoutine.tp_cnt = 0;
				sub_window.busy_ = 3u;
				currentRoutine.r.no_1 = 2;
				currentRoutine.r.no_2 = 0;
			}
			else if (sub_window.req_ != 0)
			{
				for (short num6 = 0; num6 < currentRoutine.tp_cnt; num6++)
				{
				}
				currentRoutine.tp_cnt = 0;
				sub_window.busy_ = 3u;
				if (sub_window.req_ == SubWindow.Req.STATUS_SETU_EXIT)
				{
					soundCtrl.instance.PlaySE(44);
					global_work_.r.no_1 = 1;
					debugLogger.instance.Log("SubWindowStack", "--stack_");
					sub_window.stack_--;
				}
				else if (sub_window.req_ == SubWindow.Req.STATUS_TUKITUKERU)
				{
					if (sub_window.routine_[sub_window.stack_ - 2].r.no_0 == 4)
					{
						debugLogger.instance.Log("SubWindowStack", "stack_=0");
						sub_window.stack_ = 0;
						Routine currentRoutine2 = sub_window.GetCurrentRoutine();
						currentRoutine2.r.no_1 = 6;
						currentRoutine2.r.no_2 = 0;
						currentRoutine2.r.no_3 = 0;
					}
					else if (sub_window.routine_[0].r.no_0 == 5)
					{
						if (global_work_.r.no_1 == 7)
						{
							currentRoutine.r.no_1 = 5;
							currentRoutine.r.no_2 = 0;
							currentRoutine.r.no_3 = 3;
						}
						else
						{
							currentRoutine.r.no_1 = 5;
							currentRoutine.r.no_2 = 0;
							currentRoutine.r.no_3 = 2;
						}
					}
					else if (sub_window.routine_[sub_window.stack_ - 2].r.no_0 == 2)
					{
						currentRoutine.r.no_1 = 5;
						currentRoutine.r.no_2 = 0;
						currentRoutine.r.no_3 = 1;
					}
				}
				else if (sub_window.req_ == SubWindow.Req.MAGATAMA_TUKITUKE || sub_window.req_ == SubWindow.Req.MAGATAMA_TUKITUKE2)
				{
					currentRoutine.r.no_1 = 5;
					currentRoutine.r.no_2 = 0;
					currentRoutine.r.no_3 = 2;
				}
			}
			else if (padCtrl.instance.GetKeyDown(KeyType.A) && (sub_window.obj_flag_[12] & SubWindow.ObjFlag.DISP) != 0)
			{
				soundCtrl.instance.PlaySE(43);
				if (note_.current_mode != 0)
				{
				}
			}
			else if (padCtrl.instance.GetKeyDown(KeyType.B))
			{
				for (short num7 = 0; num7 < currentRoutine.tp_cnt; num7++)
				{
				}
				currentRoutine.tp_cnt = 0;
				sub_window.busy_ = 3u;
				soundCtrl.instance.PlaySE(44);
				debugLogger.instance.Log("SubWindowStack", "--stack_");
				global_work_.r.no_1 = 1;
				sub_window.stack_--;
			}
			else if (global_work_.r.no_3 != 1 && global_work_.r.no_3 != 2)
			{
				if (padCtrl.instance.GetKeyDown(KeyType.R))
				{
					for (short num8 = 0; num8 < currentRoutine.tp_cnt; num8++)
					{
					}
					currentRoutine.tp_cnt = 0;
					sub_window.busy_ = 3u;
					currentRoutine.r.no_1 = 3;
					currentRoutine.r.no_2 = 0;
				}
				else if ((sub_window.obj_flag_[22] & SubWindow.ObjFlag.DISP) != 0 && GSStatic.global_work_.title == TitleId.GS1 && padCtrl.instance.GetKeyDown(KeyType.X))
				{
					soundCtrl.instance.PlaySE(43);
					for (short num9 = 0; num9 < currentRoutine.tp_cnt; num9++)
					{
					}
					currentRoutine.tp_cnt = 0;
					sub_window.busy_ = 3u;
					currentRoutine.r.no_1 = 5;
					currentRoutine.r.no_2 = 0;
					currentRoutine.r.no_3 = 4;
				}
			}
			else
			{
				if (global_work_.r.no_3 != 1 && global_work_.r.no_3 != 2)
				{
					break;
				}
				if (padCtrl.instance.GetKeyDown(KeyType.R) && GSStatic.global_work_.title != 0)
				{
					for (short num10 = 0; num10 < currentRoutine.tp_cnt; num10++)
					{
					}
					currentRoutine.tp_cnt = 0;
					sub_window.busy_ = 3u;
					currentRoutine.r.no_1 = 3;
					currentRoutine.r.no_2 = 0;
					break;
				}
				if (note_.current_mode == 0)
				{
					status_work_.now_file = status_work_.item_file;
					status_work_.page_now = (byte)(note_.item_page * 8 + note_.item_cursor);
				}
				else
				{
					status_work_.now_file = status_work_.name_file;
					status_work_.page_now = (byte)(note_.man_page * 8 + note_.man_cursor);
				}
				if ((global_work_.status_flag & 0x100u) != 0 || global_work_.psy_menu_active_flag != 0)
				{
					if (GSStatic.message_work_.op_work[7] == 65532)
					{
						GSMain_Status.Tukitukeru();
					}
				}
				else
				{
					GSMain_Status.Tukitukeru();
				}
				if ((GSStatic.message_work_.status & MessageSystem.Status.LOOP) != 0 && global_work_.r.no_3 == 1)
				{
					if (padCtrl.instance.GetKeyDown(KeyType.Y) && (sub_window.obj_flag_[59] & SubWindow.ObjFlag.DISP) == 0)
					{
						sub_window.busy_ = 3u;
						currentRoutine.r.no_2 = 2;
					}
					if (!padCtrl.instance.GetKeyDown(KeyType.Y) && (sub_window.obj_flag_[59] & SubWindow.ObjFlag.IN) != 0)
					{
						sub_window.SetObjDispFlag(9);
						sub_window.busy_ = 3u;
						currentRoutine.r.no_2 = 2;
					}
				}
			}
			break;
		}
		case 1:
		{
			currentRoutine.r.Set(12, 6, 0, 0);
			sub_window.stack_++;
			Routine currentRoutine3 = sub_window.GetCurrentRoutine();
			currentRoutine3.r.Set(13, 10, 0, 0);
			currentRoutine3.tex_no = currentRoutine.tex_no;
			currentRoutine3.flag = 0;
			break;
		}
		case 2:
			if (global_work_.r.no_3 == 1 || global_work_.r.no_3 == 2)
			{
				if ((global_work_.status_flag & 0x100u) != 0 || global_work_.psy_menu_active_flag != 0)
				{
					if (GSStatic.message_work_.op_work[7] == 65532)
					{
						GSMain_Status.Tukitukeru();
					}
				}
				else
				{
					GSMain_Status.Tukitukeru();
				}
			}
			if (sub_window.CheckObjIn() && sub_window.bar_req_ == SubWindow.BarReq.NONE)
			{
				sub_window.busy_ = 0u;
				currentRoutine.r.no_2 = 0;
			}
			break;
		}
	}

	private static void PageChange(SubWindow sub_window)
	{
		Routine currentRoutine = sub_window.GetCurrentRoutine();
		Routine3d[] routine_3d = currentRoutine.routine_3d;
		switch (currentRoutine.r.no_2)
		{
		case 0:
			routine_3d[1].x = routine_3d[2].h;
			if (routine_3d[1].flag == 1)
			{
			}
			currentRoutine.r.no_2++;
			break;
		case 1:
			routine_3d[1].disp_off = 0;
			if (routine_3d[1].flag == 1)
			{
				if (routine_3d[1].x < 256)
				{
					routine_3d[1].x += 16;
				}
				else
				{
					routine_3d[1].x = 256;
				}
				routine_3d[1].y = (short)(routine_3d[1].x - 256);
				if (routine_3d[1].x == 256)
				{
					currentRoutine.r.no_2++;
				}
				routine_3d[2].rotate[0] += 12;
				routine_3d[2].rotate[0] %= 16;
			}
			else
			{
				if (routine_3d[1].x > -256)
				{
					routine_3d[1].x -= 16;
				}
				else
				{
					routine_3d[1].x = -256;
				}
				routine_3d[1].y = (short)(routine_3d[1].x + 256);
				if (routine_3d[1].x == -256)
				{
					currentRoutine.r.no_2++;
				}
				routine_3d[2].rotate[0] += 65524;
				routine_3d[2].rotate[0] %= 16;
			}
			break;
		case 2:
			if (sub_window.CheckObjIn() && sub_window.bar_req_ == SubWindow.BarReq.NONE)
			{
				sub_window.busy_ = 0u;
				routine_3d[1].disp_off = 1;
				sub_window.SetObjDispPrevFlag();
				sub_window.SetObjFlagOne(22, 1, 1);
				sub_window.SetObjFlagOne(12, 1, 1);
				SetBarReq(sub_window, 12);
				for (int i = 0; i < 6; i++)
				{
					routine_3d[i].Clear();
				}
				routine_3d[1].disp_off = 1;
				currentRoutine.r.no_1 = 1;
				currentRoutine.r.no_2 = 0;
			}
			break;
		}
	}

	private static void ModeChange(SubWindow sub_window)
	{
		GlobalWork global_work_ = GSStatic.global_work_;
		Routine currentRoutine = sub_window.GetCurrentRoutine();
		SubWindowNote note_ = sub_window.note_;
		switch (currentRoutine.r.no_2)
		{
		case 0:
			if (note_.current_mode == 0)
			{
				sub_window.SetObjDispPrevFlag();
				sub_window.SetObjFlagOne(7, 1, 1);
				sub_window.SetObjFlagOne(20, 1, 0);
				sub_window.SetObjFlagOne(38, 1, 1);
				sub_window.SetObjFlagOne(41, 1, 1);
				sub_window.SetObjFlagOne(46, 1, 1);
				sub_window.SetObjFlagOne(43, 1, 1);
				sub_window.SetObjFlagOne(44, 1, 1);
				sub_window.SetObjFlagOne(45, 1, 1);
			}
			else
			{
				sub_window.SetObjDispPrevFlag();
				sub_window.SetObjFlagOne(7, 1, 0);
				sub_window.SetObjFlagOne(20, 1, 1);
				sub_window.SetObjFlagOne(37, 1, 1);
				sub_window.SetObjFlagOne(40, 1, 1);
				sub_window.SetObjFlagOne(46, 1, 1);
				sub_window.SetObjFlagOne(43, 1, 1);
				sub_window.SetObjFlagOne(44, 1, 1);
				sub_window.SetObjFlagOne(45, 1, 1);
			}
			note_.rotate = 0;
			currentRoutine.r.no_2++;
			break;
		case 1:
			note_.rotate += 2048;
			if (note_.rotate >= 32768)
			{
				note_.rotate = 0;
				note_.current_mode = (byte)(1 - note_.current_mode);
				if (note_.current_mode == 1)
				{
				}
				currentRoutine.r.no_2++;
			}
			break;
		case 2:
			if (sub_window.CheckObjOut())
			{
				sub_window.SetObjDispPrevFlag();
				if (global_work_.r.no_3 != 1 && global_work_.r.no_3 != 2 && sub_window.status_force_ == 0 && GSStatic.global_work_.title == TitleId.GS1)
				{
					sub_window.SetObjFlagOne(22, 1, 1);
				}
				sub_window.SetObjFlagOne(12, 1, 1);
				if (note_.current_mode == 1)
				{
					sub_window.SetObjFlagOne(37, 1, 0);
					sub_window.SetObjFlagOne(40, 1, 0);
					sub_window.SetObjFlagOne(46, 1, 0);
					sub_window.SetObjFlagOne(43, 1, 0);
					sub_window.SetObjFlagOne(44, 1, 0);
					sub_window.SetObjFlagOne(45, 1, 0);
				}
				else
				{
					sub_window.SetObjFlagOne(38, 1, 0);
					sub_window.SetObjFlagOne(41, 1, 0);
					sub_window.SetObjFlagOne(46, 1, 0);
					sub_window.SetObjFlagOne(43, 1, 0);
					sub_window.SetObjFlagOne(44, 1, 0);
					sub_window.SetObjFlagOne(45, 1, 0);
				}
				SetBarReq(sub_window, 12);
				currentRoutine.r.no_2++;
			}
			break;
		case 3:
			if (sub_window.CheckObjIn() && sub_window.bar_req_ == SubWindow.BarReq.NONE)
			{
				sub_window.busy_ = 0u;
				sub_window.req_ = SubWindow.Req.NONE;
				currentRoutine.r.no_1 = 1;
				currentRoutine.r.no_2 = 0;
			}
			break;
		}
	}

	private static void Special(SubWindow sub_window)
	{
		GlobalWork global_work_ = GSStatic.global_work_;
		Routine currentRoutine = sub_window.GetCurrentRoutine();
		Routine3d[] routine_3d = currentRoutine.routine_3d;
		SubWindowNote note_ = sub_window.note_;
		if (note_.current_mode == 0)
		{
		}
		switch (currentRoutine.r.no_2)
		{
		case 0:
			routine_3d[2].flag = 0;
			routine_3d[2].h = 0;
			sub_window.SetObjDispFlag(37);
			routine_3d[2].disp_off = 1;
			currentRoutine.r.no_2++;
			break;
		case 1:
			break;
		case 2:
			if (sub_window.CheckObjOut() && sub_window.bar_req_ == SubWindow.BarReq.NONE)
			{
				routine_3d[3].disp_off = 1;
				currentRoutine.r.no_2++;
			}
			break;
		case 3:
			sub_window.busy_ = 0u;
			if (sub_window.req_ == SubWindow.Req.STATUS_SETU_EXIT || padCtrl.instance.GetKeyDown(KeyType.B))
			{
				soundCtrl.instance.PlaySE(44);
				currentRoutine.tp_cnt = 0;
				sub_window.busy_ = 3u;
				currentRoutine.r.no_2++;
			}
			break;
		case 4:
			if (global_work_.r.no_3 == 1 || global_work_.r.no_3 == 2)
			{
				sub_window.SetObjDispFlag(9);
			}
			else
			{
				sub_window.SetObjDispFlag(11);
			}
			if ((global_work_.status_flag & 0x100u) != 0 || sub_window.status_force_ != 0)
			{
				sub_window.SetObjFlagOne(2, 1, 0);
			}
			if (global_work_.r.no_3 != 1 && global_work_.r.no_3 != 2)
			{
				if (note_.current_mode == 0)
				{
					sub_window.SetObjFlagOne(7, 1, 0);
					sub_window.SetObjFlagOne(20, 1, 1);
					sub_window.SetObjFlagOne(38, 1, 0);
					sub_window.SetObjFlagOne(41, 1, 0);
					sub_window.SetObjFlagOne(46, 1, 0);
					sub_window.SetObjFlagOne(43, 1, 0);
					sub_window.SetObjFlagOne(44, 1, 0);
					sub_window.SetObjFlagOne(45, 1, 0);
				}
				else
				{
					sub_window.SetObjFlagOne(7, 1, 1);
					sub_window.SetObjFlagOne(20, 1, 0);
					sub_window.SetObjFlagOne(37, 1, 0);
					sub_window.SetObjFlagOne(40, 1, 0);
					sub_window.SetObjFlagOne(46, 1, 0);
					sub_window.SetObjFlagOne(43, 1, 0);
					sub_window.SetObjFlagOne(44, 1, 0);
					sub_window.SetObjFlagOne(45, 1, 0);
				}
			}
			SetBarReq(sub_window, 12);
			if (GSStatic.global_work_.title == TitleId.GS1)
			{
			}
			currentRoutine.r.no_2++;
			break;
		case 5:
			if (sub_window.CheckObjOut())
			{
				if (global_work_.r.no_3 == 1 || global_work_.r.no_3 == 2)
				{
				}
				routine_3d[3].disp_off = 0;
				currentRoutine.r.no_2++;
			}
			break;
		case 6:
			if (sub_window.CheckObjIn() && sub_window.bar_req_ == SubWindow.BarReq.NONE)
			{
				routine_3d[2].disp_off = 0;
				sub_window.busy_ = 0u;
				sub_window.req_ = SubWindow.Req.NONE;
				currentRoutine.r.no_1 = 1;
				currentRoutine.r.no_2 = 0;
			}
			break;
		case 7:
			break;
		case 8:
			if (sub_window.CheckObjOut() && sub_window.bar_req_ == SubWindow.BarReq.NONE)
			{
				sub_window.busy_ = 0u;
				currentRoutine.r.no_2++;
			}
			break;
		case 9:
			if (sub_window.req_ == SubWindow.Req.STATUS_SETU_EXIT || padCtrl.instance.GetKeyDown(KeyType.B))
			{
				currentRoutine.r.no_2++;
			}
			break;
		case 10:
		{
			sub_window.scan_.flag = 1;
			for (short num = 0; num < currentRoutine.tp_cnt; num++)
			{
			}
			currentRoutine.tp_cnt = 0;
			sub_window.busy_ = 3u;
			sub_window.SetObjDispFlag(37);
			currentRoutine.r.no_2 = 4;
			break;
		}
		case 11:
			switch (GSStatic.global_work_.title)
			{
			default:
				currentRoutine.r.no_2 = 3;
				break;
			}
			break;
		}
	}

	private static void Thrust(SubWindow sub_window)
	{
		GlobalWork global_work_ = GSStatic.global_work_;
		Routine currentRoutine = sub_window.GetCurrentRoutine();
		Routine3d[] routine_3d = currentRoutine.routine_3d;
		SubWindowNote note_ = sub_window.note_;
		switch (currentRoutine.r.no_2)
		{
		case 0:
			if (global_work_.language != 0)
			{
				if (routine_3d[0].h - 16 > -48)
				{
					routine_3d[0].h -= 16;
				}
				else
				{
					routine_3d[0].h = -48;
				}
				if (routine_3d[0].x + 8 < 24)
				{
					routine_3d[0].x += 8;
				}
				else
				{
					routine_3d[0].x = 24;
				}
				if (routine_3d[0].h == -48 && routine_3d[0].x == 24)
				{
					routine_3d[0].timer = 0;
					currentRoutine.r.no_2++;
				}
			}
			else
			{
				if (routine_3d[0].x + 8 < 24)
				{
					routine_3d[0].x += 8;
				}
				else
				{
					routine_3d[0].x = 24;
				}
				if (routine_3d[0].x == 24)
				{
					routine_3d[0].timer = 0;
					currentRoutine.r.no_2++;
				}
			}
			break;
		case 1:
			if (routine_3d[0].timer == 0)
			{
				if (routine_3d[1].x + 16 < 352)
				{
					routine_3d[1].x += 16;
				}
				else
				{
					routine_3d[1].x = 352;
				}
				if (routine_3d[4].x + 16 < 80)
				{
					routine_3d[4].x += 16;
					break;
				}
				routine_3d[4].x = 80;
				routine_3d[0].timer = 10;
				currentRoutine.r.no_2++;
			}
			else
			{
				routine_3d[0].timer--;
			}
			break;
		case 2:
		{
			byte b = 1;
			if (routine_3d[0].timer == 0)
			{
				if (routine_3d[4].y - 16 > -128)
				{
					routine_3d[4].y -= 16;
					b = 0;
				}
				else
				{
					routine_3d[4].y = -128;
				}
			}
			else
			{
				routine_3d[0].timer--;
			}
			if (routine_3d[1].x + 16 < 352)
			{
				routine_3d[1].x += 16;
				b = 0;
			}
			else
			{
				routine_3d[1].x = 352;
			}
			if (b != 0)
			{
				currentRoutine.r.no_2++;
				if (sub_window.req_ == SubWindow.Req.MAGATAMA_TUKITUKE || sub_window.req_ == SubWindow.Req.MAGATAMA_TUKITUKE2)
				{
					currentRoutine.r.no_2 = 5;
				}
			}
			break;
		}
		case 3:
			if (currentRoutine.r.no_3 == 1)
			{
				debugLogger.instance.Log("SubWindowStack", "stack_=0");
				sub_window.stack_ = 0;
			}
			else if (currentRoutine.r.no_3 == 2)
			{
				note_.man_page_old = note_.man_page;
				note_.man_cursor_old = note_.man_cursor;
				note_.item_page_old = note_.item_page;
				note_.item_cursor_old = note_.item_cursor;
				note_.current_mode_old = note_.current_mode;
				sub_window.tantei_tukituke_ = 1;
				debugLogger.instance.Log("SubWindowStack", "--stack_");
				sub_window.stack_--;
				sub_window.GetCurrentRoutine().r.Set(10, 0, 0, 0);
				switch (GSStatic.global_work_.title)
				{
				case TitleId.GS1:
					break;
				case TitleId.GS2:
					break;
				case TitleId.GS3:
					break;
				}
			}
			else if (currentRoutine.r.no_3 == 3)
			{
				debugLogger.instance.Log("SubWindowStack", "stack_-2");
				sub_window.stack_ -= 2;
			}
			else if (currentRoutine.r.no_3 == 4)
			{
				currentRoutine.r.no_2++;
			}
			else
			{
				debugLogger.instance.Log("SubWindowStack", "--stack_");
				sub_window.stack_--;
				global_work_.r.no_1 = 1;
			}
			break;
		case 4:
		{
			routine_3d[2].disp_off = 1;
			debugLogger.instance.Log("SubWindowStack", "--stack_");
			sub_window.stack_--;
			Routine currentRoutine2 = sub_window.GetCurrentRoutine();
			currentRoutine2.r.Set(23, 0, 0, 0);
			currentRoutine2.tex_no = currentRoutine.tex_no;
			break;
		}
		case 5:
			note_.man_page_old = note_.man_page;
			note_.man_cursor_old = note_.man_cursor;
			note_.item_page_old = note_.item_page;
			note_.item_cursor_old = note_.item_cursor;
			note_.current_mode_old = note_.current_mode;
			sub_window.tantei_tukituke_ = 1;
			debugLogger.instance.Log("SubWindowStack", "--stack_");
			sub_window.stack_--;
			sub_window.GetCurrentRoutine().r.Set(10, 242, 0, 0);
			break;
		}
	}

	private static void Back(SubWindow sub_window)
	{
		GlobalWork global_work_ = GSStatic.global_work_;
		Routine currentRoutine = sub_window.GetCurrentRoutine();
		switch (currentRoutine.r.no_2)
		{
		case 0:
			if (global_work_.r.no_3 == 1 || global_work_.r.no_3 == 2)
			{
				sub_window.SetObjDispFlag(9);
			}
			else
			{
				sub_window.SetObjDispFlag(11);
			}
			SetBarReq(sub_window, 12);
			currentRoutine.r.no_2++;
			break;
		case 1:
			if (sub_window.CheckObjOut())
			{
				if (global_work_.r.no_3 == 1 || global_work_.r.no_3 == 2)
				{
				}
				currentRoutine.r.no_2++;
			}
			break;
		case 2:
			if (sub_window.CheckObjIn() && sub_window.bar_req_ == SubWindow.BarReq.NONE)
			{
				sub_window.busy_ = 0u;
				sub_window.req_ = SubWindow.Req.NONE;
				currentRoutine.r.no_1 = 1;
				currentRoutine.r.no_2 = 0;
			}
			break;
		}
	}

	private static void ThrustBack(SubWindow sub_window)
	{
		GlobalWork global_work_ = GSStatic.global_work_;
		Routine currentRoutine = sub_window.GetCurrentRoutine();
		Routine3d[] routine_3d = currentRoutine.routine_3d;
		switch (currentRoutine.r.no_2)
		{
		case 0:
			if (routine_3d[1].x - 16 > 0)
			{
				routine_3d[1].x -= 16;
				routine_3d[4].x = routine_3d[1].x;
			}
			else
			{
				routine_3d[1].x = (routine_3d[4].x = 0);
				routine_3d[0].timer = 5;
				currentRoutine.r.no_2++;
			}
			break;
		case 1:
			if (routine_3d[0].timer == 0)
			{
				if (global_work_.language != 0)
				{
					if (routine_3d[0].h + 16 < 0)
					{
						routine_3d[0].h += 16;
					}
					else
					{
						routine_3d[0].h = 0;
					}
					if (routine_3d[0].x - 8 > 0)
					{
						routine_3d[0].x -= 8;
					}
					else
					{
						routine_3d[0].x = 0;
					}
					if (routine_3d[0].h == 0 && routine_3d[0].x == 0)
					{
						routine_3d[0].timer = 0;
						currentRoutine.r.no_2++;
					}
				}
				else
				{
					if (routine_3d[0].x - 8 > 0)
					{
						routine_3d[0].x -= 8;
					}
					else
					{
						routine_3d[0].x = 0;
					}
					if (routine_3d[0].x == 0)
					{
						routine_3d[0].timer = 0;
						currentRoutine.r.no_2++;
					}
				}
			}
			else
			{
				routine_3d[0].timer--;
			}
			break;
		case 2:
			if (routine_3d[0].timer == 0)
			{
				currentRoutine.r.no_1 = 1;
				currentRoutine.r.no_2 = 0;
			}
			else
			{
				routine_3d[0].timer--;
			}
			break;
		}
	}

	private static void Tutorial(SubWindow sub_window)
	{
		Routine currentRoutine = sub_window.GetCurrentRoutine();
		Routine3d[] routine_3d = currentRoutine.routine_3d;
		switch (currentRoutine.r.no_2)
		{
		case 0:
			routine_3d[0].timer = 20;
			currentRoutine.r.no_2++;
			break;
		case 1:
			if (routine_3d[0].timer == 0)
			{
				for (short num = 0; num < currentRoutine.tp_cnt; num++)
				{
				}
				currentRoutine.tp_cnt = 0;
				soundCtrl.instance.PlaySE(43);
				currentRoutine.r.no_2++;
			}
			else
			{
				routine_3d[0].timer--;
			}
			break;
		case 2:
		{
			currentRoutine.r.Set(12, 6, 0, 0);
			debugLogger.instance.Log("SubWindowStack", "++stack_");
			sub_window.stack_++;
			Routine currentRoutine2 = sub_window.GetCurrentRoutine();
			currentRoutine2.r.Set(13, 0, 0, 0);
			currentRoutine2.tex_no = currentRoutine.tex_no;
			currentRoutine2.flag = 0;
			break;
		}
		}
	}

	private static void SetBarReq(SubWindow sub_window, int bar_req)
	{
		GlobalWork global_work_ = GSStatic.global_work_;
		if (bar_req == 12)
		{
			if (global_work_.r.no_3 == 1 || global_work_.r.no_3 == 2)
			{
				sub_window.bar_req_ = SubWindow.BarReq.THRUST_1;
			}
			else
			{
				sub_window.bar_req_ = SubWindow.BarReq.STATUS_DETAIL_1;
			}
		}
	}
}
