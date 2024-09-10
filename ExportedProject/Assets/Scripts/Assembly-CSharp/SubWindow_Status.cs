public static class SubWindow_Status
{
	private delegate void StatusProc(SubWindow sub_window);

	private static readonly StatusProc[] proc_table;

	private static bool from_detail_;

	static SubWindow_Status()
	{
		proc_table = new StatusProc[11]
		{
			Init, Appear, Main, Leave, PageChange, ModeChange, Add, ToDetail, FromDetail, Thrust,
			Tutorial
		};
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
			if (global_work_.r.no_3 == 3)
			{
				note_.current_mode = 1;
				note_.item_page = (note_.man_page = (note_.item_cursor = (note_.man_cursor = 0)));
				note_.item_page_max = (byte)((status_work_.item_page_max - 1) / 8);
				note_.man_page_max = (byte)((status_work_.name_page_max - 1) / 8);
				ushort num;
				for (num = 0; num < status_work_.item_page_max; num++)
				{
					if (global_work_.get_note_id == status_work_.item_file[num])
					{
						note_.item_page = (byte)(num / 8);
						int num2 = status_work_.item_page_max - note_.item_page * 8;
						if (num2 > 8)
						{
							num2 = 8;
						}
						note_.item_cursor = (byte)(num % 8);
						break;
					}
				}
				if (num == status_work_.item_page_max)
				{
					note_.item_page = note_.item_page_max;
					int num2 = status_work_.item_page_max - note_.item_page * 8;
					if (num2 > 8)
					{
						num2 = 8;
					}
					note_.item_cursor = (byte)(num2 - 1);
				}
				currentRoutine.r.no_3 = 3;
				sub_window.note_add_ = 1;
			}
			else
			{
				note_.current_mode = 1;
				note_.item_page = (note_.man_page = (note_.item_cursor = (note_.man_cursor = 0)));
				note_.item_page_max = (byte)((status_work_.item_page_max - 1) / 8);
				note_.man_page_max = (byte)((status_work_.name_page_max - 1) / 8);
				if (sub_window.tantei_tukituke_ == 1)
				{
					note_.man_page = note_.man_page_old;
					note_.man_cursor = note_.man_cursor_old;
					note_.item_page = note_.item_page_old;
					note_.item_cursor = note_.item_cursor_old;
					if (note_.current_mode_old != 0)
					{
						int num3 = note_.man_page * 8 + note_.man_cursor;
						if (status_work_.name_file[num3] == byte.MaxValue)
						{
							note_.man_page = 0;
							note_.man_cursor = 0;
						}
					}
					else
					{
						int num3 = note_.item_page * 8 + note_.item_cursor;
						if (status_work_.item_file[num3] == byte.MaxValue)
						{
							note_.item_page = 0;
							note_.item_cursor = 0;
						}
					}
					note_.current_mode = note_.current_mode_old;
					sub_window.tantei_tukituke_ = 0;
				}
				else if (from_detail_)
				{
					note_.man_page = note_.man_page_old;
					note_.man_cursor = note_.man_cursor_old;
					note_.item_page = note_.item_page_old;
					note_.item_cursor = note_.item_cursor_old;
					note_.current_mode = note_.current_mode_old;
					from_detail_ = false;
				}
				else
				{
					note_.current_mode_old = 0;
					note_.man_page_old = 0;
					note_.man_cursor_old = 0;
					note_.item_page_old = 0;
					note_.item_cursor_old = 0;
				}
			}
			for (ushort num4 = 0; num4 < currentRoutine.tp_cnt; num4++)
			{
			}
			currentRoutine.tp_cnt = 0;
			if (sub_window.status_force_ != 0)
			{
				note_.current_mode = 0;
				sub_window.SetObjDispFlag(11);
				note_.current_mode = 1;
				sub_window.SetObjFlagOne(2, 1, 1);
				sub_window.bar_req_ = SubWindow.BarReq.STATUS_FORCE;
			}
			else if (global_work_.r.no_3 == 1 || global_work_.r.no_3 == 2)
			{
				note_.current_mode = 0;
				if (global_work_.r.no_3 == 2)
				{
					note_.current_mode = note_.current_mode_old;
				}
				if (global_work_.psy_menu_active_flag != 0)
				{
					sub_window.SetObjDispFlag(35);
				}
				else
				{
					sub_window.SetObjDispFlag(9);
				}
				note_.current_mode = 1;
				if (global_work_.r.no_3 == 2)
				{
					note_.current_mode = (byte)(note_.current_mode_old ^ 1u);
				}
				sub_window.bar_req_ = SubWindow.BarReq.THRUST_2;
			}
			else if (global_work_.r.no_3 == 3)
			{
				note_.current_mode = 0;
				sub_window.SetObjDispFlag(11);
				sub_window.SetObjFlagOne(2, 1, 1);
				sub_window.SetObjFlagOne(7, 1, 1);
				note_.current_mode = 1;
				sub_window.bar_req_ = SubWindow.BarReq.STATUS_ADD;
			}
			else
			{
				note_.current_mode = 0;
				sub_window.SetObjDispFlag(11);
				note_.current_mode = 1;
				sub_window.bar_req_ = SubWindow.BarReq.STATUS;
			}
			if ((global_work_.status_flag & 0x100u) != 0)
			{
				sub_window.SetObjFlagOne(2, 1, 1);
				sub_window.bar_req_ = SubWindow.BarReq.THRUST_3;
			}
			for (int i = 0; i < 6; i++)
			{
				routine_3d[i].Clear();
			}
			currentRoutine.r.no_2++;
			break;
		}
		case 1:
		{
			if (!sub_window.CheckObjOut())
			{
				break;
			}
			recordListCtrl.instance.is_change = recordListCtrl.instance.scenarioChange();
			bool is_back = (global_work_.status_flag & 0x100) == 0;
			if (sub_window.tutorial_ != 0)
			{
				bool seal_key = GetTutorialItemId(sub_window) > 0;
				recordListCtrl.instance.select_type = false;
				recordListCtrl.instance.is_back = false;
				recordListCtrl.instance.noteOpen(seal_key);
			}
			else if (global_work_.r.no_3 == 0)
			{
				recordListCtrl.instance.select_type = false;
				if (advCtrl.instance.sub_window_.status_force_ == 0)
				{
					recordListCtrl.instance.is_back = true;
				}
				else
				{
					recordListCtrl.instance.is_back = false;
				}
				recordListCtrl.instance.noteOpen(false, note_.current_mode_old);
			}
			else if (global_work_.r.no_3 == 1)
			{
				recordListCtrl.instance.select_type = true;
				recordListCtrl.instance.is_back = is_back;
				recordListCtrl.instance.noteOpen(false, note_.current_mode_old);
			}
			else if (global_work_.r.no_3 == 2)
			{
				recordListCtrl.instance.select_type = true;
				recordListCtrl.instance.is_back = is_back;
				recordListCtrl.instance.noteOpen(false, note_.current_mode_old);
			}
			else if (global_work_.r.no_3 == 4)
			{
				recordListCtrl.instance.select_type = false;
				recordListCtrl.instance.is_back = true;
				recordListCtrl.instance.noteOpen(false, note_.current_mode_old);
			}
			currentRoutine.r.no_1 = 1;
			currentRoutine.r.no_2 = 0;
			break;
		}
		}
	}

	private static void Appear(SubWindow sub_window)
	{
		GlobalWork global_work_ = GSStatic.global_work_;
		Routine currentRoutine = sub_window.GetCurrentRoutine();
		SubWindowNote note_ = sub_window.note_;
		switch (currentRoutine.r.no_2)
		{
		case 0:
			note_.current_mode = 0;
			if (global_work_.r.no_3 == 2)
			{
				note_.current_mode = note_.current_mode_old;
			}
			note_.rotate = 0;
			currentRoutine.r.no_2++;
			break;
		case 1:
			if (!sub_window.CheckObjIn() || sub_window.bar_req_ != 0)
			{
				break;
			}
			if (global_work_.r.no_3 == 3)
			{
				currentRoutine.r.no_1 = 6;
				currentRoutine.r.no_2 = 0;
				break;
			}
			if (GSStatic.global_work_.title == TitleId.GS1 && sub_window.tutorial_ != 0)
			{
				currentRoutine.r.no_1 = 10;
				currentRoutine.r.no_2 = 0;
				break;
			}
			sub_window.busy_ = 0u;
			if (sub_window.req_ == SubWindow.Req.SELECT)
			{
				debugLogger.instance.Log("SubWindowStack", "--stack_");
				sub_window.stack_--;
			}
			else
			{
				sub_window.req_ = SubWindow.Req.NONE;
				currentRoutine.r.no_1 = 2;
				currentRoutine.r.no_2 = 0;
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
			if (currentRoutine.r.no_1 != 2)
			{
				break;
			}
			if (sub_window.req_ == SubWindow.Req.STATUS_TUKITUKERU)
			{
				for (ushort num = 0; num < currentRoutine.tp_cnt; num++)
				{
				}
				currentRoutine.tp_cnt = 0;
				sub_window.busy_ = 3u;
				routine_3d[1].disp_off = 0;
				if (sub_window.routine_[sub_window.stack_ - 1].r.no_0 == 4)
				{
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
						currentRoutine.r.no_1 = 9;
						currentRoutine.r.no_2 = 0;
						currentRoutine.r.no_3 = 3;
					}
					else
					{
						currentRoutine.r.no_1 = 9;
						currentRoutine.r.no_2 = 0;
						currentRoutine.r.no_3 = 2;
					}
				}
				else if (sub_window.routine_[sub_window.stack_ - 1].r.no_0 == 2)
				{
					currentRoutine.r.no_1 = 9;
					currentRoutine.r.no_2 = 0;
					currentRoutine.r.no_3 = 1;
				}
				break;
			}
			if (sub_window.req_ == SubWindow.Req.STATUS_SETU)
			{
				sub_window.busy_ = 3u;
				soundCtrl.instance.PlaySE(43);
				if (global_work_.psy_menu_active_flag != 0)
				{
					sub_window.SetObjFlagOne(69, 1, 1);
				}
				note_.man_page_old = note_.man_page;
				note_.man_cursor_old = note_.man_cursor;
				note_.item_page_old = note_.item_page;
				note_.item_cursor_old = note_.item_cursor;
				note_.current_mode_old = note_.current_mode;
				currentRoutine.r.no_1 = 7;
				currentRoutine.r.no_2 = 0;
				break;
			}
			if (sub_window.req_ == SubWindow.Req.MAGATAMA_TUKITUKE || sub_window.req_ == SubWindow.Req.MAGATAMA_TUKITUKE2)
			{
				for (ushort num2 = 0; num2 < currentRoutine.tp_cnt; num2++)
				{
				}
				currentRoutine.tp_cnt = 0;
				sub_window.busy_ = 3u;
				routine_3d[1].disp_off = 0;
				currentRoutine.r.no_1 = 9;
				currentRoutine.r.no_2 = 0;
				currentRoutine.r.no_3 = 2;
				break;
			}
			if (sub_window.req_ == SubWindow.Req.STATUS_EXIT || (recordListCtrl.instance.KeyDownClose() && (global_work_.status_flag & 0x100) == 0 && sub_window.status_force_ == 0))
			{
				for (ushort num3 = 0; num3 < currentRoutine.tp_cnt; num3++)
				{
				}
				currentRoutine.tp_cnt = 0;
				sub_window.busy_ = 3u;
				if (global_work_.r.no_3 == 2)
				{
					global_work_.r_bk.Set(5, 9, 3, 0);
				}
				global_work_.r.CopyFrom(ref global_work_.r_bk);
				soundCtrl.instance.PlaySE(44);
				currentRoutine.r.no_1 = 3;
				currentRoutine.r.no_2 = 0;
				if (global_work_.psy_menu_active_flag != 0)
				{
					currentRoutine.r.no_3 = 1;
				}
				else
				{
					currentRoutine.r.no_3 = 0;
				}
				break;
			}
			if (!padCtrl.instance.GetKeyDown(KeyType.R) && CheckFukitukeru(sub_window) == 1 && GSStatic.global_work_.title == TitleId.GS1 && global_work_.r.no_3 != 1 && global_work_.r.no_3 != 2 && padCtrl.instance.GetKeyDown(KeyType.X))
			{
				soundCtrl.instance.PlaySE(43);
				for (ushort num4 = 0; num4 < currentRoutine.tp_cnt; num4++)
				{
				}
				currentRoutine.tp_cnt = 0;
				sub_window.busy_ = 3u;
				recordListCtrl.instance.Close();
				currentRoutine.r.no_1 = 9;
				currentRoutine.r.no_2 = 0;
				currentRoutine.r.no_3 = 4;
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
			break;
		case 1:
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
			recordListCtrl.instance.Close();
			routine_3d[1].disp_off = 1;
			currentRoutine.r.no_2++;
			break;
		case 1:
			if (!recordListCtrl.instance.is_open)
			{
				currentRoutine.r.no_2++;
			}
			break;
		case 2:
			if (currentRoutine.r.no_3 == 1)
			{
				currentRoutine.r.Set(10, 242, 0, 0);
				break;
			}
			debugLogger.instance.Log("SubWindowStack", "--stack_");
			sub_window.stack_--;
			if (sub_window.note_add_ == 1)
			{
				GSStatic.message_work_.status |= MessageSystem.Status.RT_GO;
				sub_window.routine_[sub_window.stack_].r.no_3 = 3;
			}
			if (GSMain_Status.loop_se_ != 268435455)
			{
				soundCtrl.instance.PlaySE(GSMain_Status.loop_se_);
				GSMain_Status.loop_se_ = 268435455;
			}
			break;
		}
	}

	private static void PageChange(SubWindow sub_window)
	{
		GlobalWork global_work_ = GSStatic.global_work_;
		Routine currentRoutine = sub_window.GetCurrentRoutine();
		Routine3d[] routine_3d = currentRoutine.routine_3d;
		SubWindowNote note_ = sub_window.note_;
		EffectWork[] effect_work_ = GSStatic.effect_work_;
		switch (currentRoutine.r.no_2)
		{
		case 0:
		{
			if (note_.current_mode == 0)
			{
				if ((currentRoutine.flag & 0x10u) != 0)
				{
					note_.next_page = (byte)(note_.item_page - 1);
				}
				else if ((currentRoutine.flag & 0x20u) != 0)
				{
					note_.next_page = (byte)(note_.item_page + 1);
				}
				else if ((currentRoutine.flag & 0x40u) != 0)
				{
					note_.next_page = 0;
				}
				else if ((currentRoutine.flag & 0x80u) != 0)
				{
					note_.next_page = note_.item_page_max;
				}
			}
			else if ((currentRoutine.flag & 0x10u) != 0)
			{
				note_.next_page = (byte)(note_.man_page - 1);
			}
			else if ((currentRoutine.flag & 0x20u) != 0)
			{
				note_.next_page = (byte)(note_.man_page + 1);
			}
			else if ((currentRoutine.flag & 0x40u) != 0)
			{
				note_.next_page = 0;
			}
			else if ((currentRoutine.flag & 0x80u) != 0)
			{
				note_.next_page = note_.man_page_max;
			}
			if ((currentRoutine.flag & 0x10u) != 0 || (currentRoutine.flag & 0x80u) != 0)
			{
				effect_work_[0].info[0].x = 0;
				effect_work_[0].info[1].x = -256;
			}
			else
			{
				effect_work_[0].info[0].x = 0;
				effect_work_[0].info[1].x = 256;
			}
			routine_3d[1].disp_off = 1;
			for (ushort num = 0; num < currentRoutine.tp_cnt; num++)
			{
			}
			currentRoutine.tp_cnt = 0;
			sub_window.busy_ = 3u;
			currentRoutine.r.no_2++;
			break;
		}
		case 1:
			if ((currentRoutine.flag & 0x10u) != 0 || (currentRoutine.flag & 0x80u) != 0)
			{
				if (effect_work_[0].info[1].x + 16 < 0)
				{
					effect_work_[0].info[0].x += 16;
					effect_work_[0].info[1].x += 16;
				}
				else
				{
					effect_work_[0].info[0].x = 256;
					effect_work_[0].info[1].x = 0;
					currentRoutine.r.no_2++;
				}
			}
			else if (effect_work_[0].info[1].x - 16 > 0)
			{
				effect_work_[0].info[0].x -= 16;
				effect_work_[0].info[1].x -= 16;
			}
			else
			{
				effect_work_[0].info[0].x = -256;
				effect_work_[0].info[1].x = 0;
				currentRoutine.r.no_2++;
			}
			if (currentRoutine.r.no_2 == 2 && (currentRoutine.flag & 0x10) == 0 && (currentRoutine.flag & 0x20) == 0 && (currentRoutine.flag & 0x40) == 0 && (currentRoutine.flag & 0x80) == 0)
			{
			}
			break;
		case 2:
			if (!sub_window.CheckObjIn() || sub_window.bar_req_ != 0)
			{
				break;
			}
			if (note_.current_mode == 0)
			{
			}
			sub_window.busy_ = 0u;
			if (sub_window.tutorial_ != 0)
			{
				currentRoutine.r.no_1 = 10;
				currentRoutine.r.no_2 = 1;
			}
			else
			{
				currentRoutine.r.no_1 = 2;
				currentRoutine.r.no_2 = 0;
			}
			if (GSStatic.global_work_.title == TitleId.GS1 && global_work_.r.no_3 != 1 && global_work_.r.no_3 != 2 && sub_window.status_force_ == 0)
			{
				if (CheckFukitukeru(sub_window) == 1)
				{
					sub_window.SetObjFlagOne(22, 1, 0);
					sub_window.bar_req_ = SubWindow.BarReq.THRUST_2;
				}
				else
				{
					sub_window.SetObjFlagOne(22, 1, 1);
					sub_window.bar_req_ = SubWindow.BarReq.STATUS;
				}
			}
			break;
		}
	}

	private static void ModeChange(SubWindow sub_window)
	{
		GlobalWork global_work_ = GSStatic.global_work_;
		Routine currentRoutine = sub_window.GetCurrentRoutine();
		Routine3d[] routine_3d = currentRoutine.routine_3d;
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
			routine_3d[1].disp_off = 1;
			note_.rotate = 0;
			currentRoutine.r.no_2++;
			break;
		case 1:
			note_.rotate += 10;
			if (note_.rotate >= 180)
			{
				note_.rotate = 0;
				note_.current_mode = (byte)(1 - note_.current_mode);
				currentRoutine.r.no_2++;
			}
			break;
		case 2:
			if (sub_window.CheckObjOut())
			{
				sub_window.SetObjDispPrevFlag();
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
				currentRoutine.r.no_2++;
			}
			break;
		case 3:
			if (!sub_window.CheckObjIn() || sub_window.bar_req_ != 0)
			{
				break;
			}
			sub_window.busy_ = 0u;
			if (note_.current_mode == 1)
			{
			}
			if (GSStatic.global_work_.title == TitleId.GS1 && global_work_.r.no_3 != 1 && global_work_.r.no_3 != 2 && sub_window.status_force_ == 0)
			{
				if (CheckFukitukeru(sub_window) == 1)
				{
					sub_window.SetObjFlagOne(22, 1, 0);
					sub_window.bar_req_ = SubWindow.BarReq.THRUST_2;
				}
				else
				{
					sub_window.SetObjFlagOne(22, 1, 1);
					sub_window.bar_req_ = SubWindow.BarReq.STATUS;
				}
			}
			currentRoutine.r.no_1 = 2;
			currentRoutine.r.no_2 = 0;
			break;
		}
	}

	private static void Add(SubWindow sub_window)
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
			sub_window.busy_ = 0u;
			sub_window.req_ = SubWindow.Req.NONE;
			currentRoutine.r.no_2++;
			break;
		case 3:
			soundCtrl.instance.PlaySE(43);
			currentRoutine.r.no_2++;
			break;
		case 4:
			currentRoutine.r.no_1 = 3;
			currentRoutine.r.no_2 = 0;
			currentRoutine.r.no_3 = 0;
			break;
		}
	}

	private static void ToDetail(SubWindow sub_window)
	{
		GlobalWork global_work_ = GSStatic.global_work_;
		Routine currentRoutine = sub_window.GetCurrentRoutine();
		if (global_work_.psy_menu_active_flag != 0)
		{
			if (global_work_.language != 0)
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
				{
					currentRoutine.r.Set(11, 8, 0, 0);
					debugLogger.instance.Log("SubWindowStack", "++stack_");
					sub_window.stack_++;
					Routine currentRoutine2 = sub_window.GetCurrentRoutine();
					currentRoutine2.r.Set(12, 0, 0, 0);
					currentRoutine2.tex_no = currentRoutine.tex_no;
					break;
				}
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
				{
					currentRoutine.r.Set(11, 8, 0, 0);
					debugLogger.instance.Log("SubWindowStack", "++stack_");
					sub_window.stack_++;
					Routine currentRoutine3 = sub_window.GetCurrentRoutine();
					currentRoutine3.r.Set(12, 0, 0, 0);
					currentRoutine3.tex_no = currentRoutine.tex_no;
					break;
				}
				}
			}
		}
		else if (global_work_.language != 0)
		{
			switch (currentRoutine.r.no_2)
			{
			case 0:
				currentRoutine.r.no_2++;
				break;
			case 1:
			{
				currentRoutine.r.Set(11, 8, 0, 0);
				debugLogger.instance.Log("SubWindowStack", "++stack_");
				sub_window.stack_++;
				Routine currentRoutine4 = sub_window.GetCurrentRoutine();
				currentRoutine4.r.Set(12, 0, 0, 0);
				currentRoutine4.tex_no = currentRoutine.tex_no;
				break;
			}
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
			{
				currentRoutine.r.Set(11, 8, 0, 0);
				debugLogger.instance.Log("SubWindowStack", "++stack_");
				sub_window.stack_++;
				Routine currentRoutine5 = sub_window.GetCurrentRoutine();
				currentRoutine5.r.Set(12, 0, 0, 0);
				currentRoutine5.tex_no = currentRoutine.tex_no;
				break;
			}
			}
		}
	}

	private static void FromDetail(SubWindow sub_window)
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
			currentRoutine.r.no_2++;
			break;
		case 3:
			currentRoutine.r.no_2++;
			break;
		case 4:
			currentRoutine.r.no_2++;
			break;
		case 5:
			currentRoutine.r.no_2++;
			break;
		case 6:
			currentRoutine.r.no_2++;
			break;
		case 7:
			currentRoutine.r.no_2++;
			break;
		case 8:
			currentRoutine.r.no_1 = 0;
			currentRoutine.r.no_2 = 0;
			from_detail_ = true;
			break;
		}
	}

	private static void Thrust(SubWindow sub_window)
	{
		GlobalWork global_work_ = GSStatic.global_work_;
		Routine currentRoutine = sub_window.GetCurrentRoutine();
		Routine3d[] routine_3d = currentRoutine.routine_3d;
		SubWindowNote note_ = sub_window.note_;
		byte b = 1;
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
		case 3:
			if (currentRoutine.r.no_3 == 1)
			{
				debugLogger.instance.Log("SubWindowStack", "--stack_");
				sub_window.stack_--;
			}
			else if (currentRoutine.r.no_3 == 2)
			{
				note_.man_page_old = note_.man_page;
				note_.man_cursor_old = note_.man_cursor;
				note_.item_page_old = note_.item_page;
				note_.item_cursor_old = note_.item_cursor;
				note_.current_mode_old = note_.current_mode;
				sub_window.tantei_tukituke_ = 1;
				currentRoutine.r.Set(10, 0, 0, 0);
				uint num = GSMain_Status.tantei_show_check(global_work_, (uint)recordListCtrl.instance.selectNoteID(), (byte)recordListCtrl.instance.record_type);
				advCtrl.instance.message_system_.SetMessage(num);
				switch (GSStatic.global_work_.title)
				{
				case TitleId.GS1:
					GS1_status_list_thrust_00(global_work_.scenario, num);
					break;
				case TitleId.GS2:
					GS2_status_list_thrust_00(global_work_.scenario, num);
					break;
				case TitleId.GS3:
					GS3_status_list_thrust_00(global_work_.scenario, num);
					break;
				}
			}
			else if (currentRoutine.r.no_3 == 3)
			{
				debugLogger.instance.Log("SubWindowStack", "--stack_");
				sub_window.stack_--;
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
			routine_3d[2].disp_off = 1;
			sub_window.stack_--;
			sub_window.routine_[sub_window.stack_].r.Set(23, 0, 0, 0);
			sub_window.routine_[sub_window.stack_].tex_no = currentRoutine.tex_no;
			break;
		case 5:
			sub_window.req_ = SubWindow.Req.NONE;
			sub_window.busy_ = 0u;
			note_.man_page_old = note_.man_page;
			note_.man_cursor_old = note_.man_cursor;
			note_.item_page_old = note_.item_page;
			note_.item_cursor_old = note_.item_cursor;
			note_.current_mode_old = note_.current_mode;
			sub_window.tantei_tukituke_ = 1;
			currentRoutine.r.Set(10, 242, 0, 0);
			break;
		}
	}

	private static void GS1_status_list_thrust_00(byte scenarioID, uint messID)
	{
		if (scenarioID == 28)
		{
			if (messID == 148)
			{
				MessageSystem.Mess_window_set(10u);
			}
			else
			{
				MessageSystem.Mess_window_set(5u);
			}
		}
		else
		{
			MessageSystem.Mess_window_set(5u);
		}
	}

	private static void GS2_status_list_thrust_00(byte scenarioID, uint messID)
	{
		switch (scenarioID)
		{
		case 11:
			switch (messID)
			{
			case 164u:
			case 166u:
			case 181u:
			case 194u:
				MessageSystem.Mess_window_set(10u);
				break;
			default:
				MessageSystem.Mess_window_set(5u);
				break;
			}
			break;
		case 18:
			if (messID == 242 || messID == 243)
			{
				MessageSystem.Mess_window_set(10u);
			}
			else
			{
				MessageSystem.Mess_window_set(5u);
			}
			break;
		case 19:
			if (messID == 150)
			{
				MessageSystem.Mess_window_set(10u);
			}
			else
			{
				MessageSystem.Mess_window_set(5u);
			}
			break;
		default:
			MessageSystem.Mess_window_set(5u);
			break;
		}
	}

	private static void GS3_status_list_thrust_00(byte scenarioID, uint messID)
	{
		MessageSystem.Mess_window_set(5u);
	}

	public static uint CheckFukitukeru(SubWindow sub_window)
	{
		if (GSStatic.global_work_.title != 0)
		{
			return 0u;
		}
		SubWindowNote note_ = sub_window.note_;
		MessageWork activeMessageWork = MessageSystem.GetActiveMessageWork();
		GlobalWork global_work_ = GSStatic.global_work_;
		if (note_.current_mode != 0)
		{
			return 0u;
		}
		if (recordListCtrl.instance.current_pice_.no == 144 && global_work_.r_bk.no_0 == 5 && activeMessageWork.mess_win_rno == 1 && global_work_.r.no_3 != 2)
		{
			return 1u;
		}
		return 0u;
	}

	private static void Tutorial(SubWindow sub_window)
	{
		Routine currentRoutine = sub_window.GetCurrentRoutine();
		int target_item_id = GetTutorialItemId(sub_window);
		switch (currentRoutine.r.no_2)
		{
		case 0:
		{
			if (sub_window.tutorial_ == 1)
			{
				GSFlag.Set(0u, scenario.SCE4_FLAG_STATUS_3D_ENABLE, 1u);
			}
			if (target_item_id < 0)
			{
				currentRoutine.r.no_2 = 6;
				break;
			}
			int num = recordListCtrl.instance.record_data_[0].note_list_.FindIndex((int n) => piceDataCtrl.instance.note_data[n].no == target_item_id);
			if (num < 0)
			{
				currentRoutine.r.no_2 = 6;
				break;
			}
			currentRoutine.timer0 = 30;
			currentRoutine.routine_3d[1].disp_off = 0;
			currentRoutine.routine_3d[1].flag = 0;
			currentRoutine.routine_3d[5].flag = 0;
			currentRoutine.r.no_2++;
			break;
		}
		case 1:
			if (currentRoutine.timer0 == 0)
			{
				currentRoutine.timer0 = 30;
				currentRoutine.r.no_2++;
				TouchSystem.TouchInActive();
			}
			else
			{
				currentRoutine.timer0--;
			}
			break;
		case 2:
			currentRoutine.r.no_2++;
			break;
		case 3:
			if (currentRoutine.timer0 == 0)
			{
				currentRoutine.timer0 = 25;
				int index = recordListCtrl.instance.selectNoteID();
				if (piceDataCtrl.instance.note_data[index].no == target_item_id)
				{
					currentRoutine.r.no_2++;
					break;
				}
				recordListCtrl.instance.record_data_[0].cursor_no_++;
				recordListCtrl.instance.is_cursor_update = true;
				soundCtrl.instance.PlaySE(42);
			}
			else if (!recordListCtrl.instance.is_page_changing)
			{
				currentRoutine.timer0--;
			}
			break;
		case 4:
			currentRoutine.r.no_2++;
			break;
		case 5:
			if (currentRoutine.timer0 == 0)
			{
				soundCtrl.instance.PlaySE(51);
				recordListCtrl.instance.Decide();
				currentRoutine.r.no_2++;
			}
			else
			{
				currentRoutine.timer0--;
			}
			break;
		case 6:
			if (!recordListCtrl.instance.is_open && recordListCtrl.instance.is_info)
			{
				currentRoutine.r.no_1 = 7;
				currentRoutine.r.no_2 = 0;
			}
			break;
		}
	}

	private static int GetTutorialItemId(SubWindow sub_window)
	{
		int result = -1;
		if (sub_window.tutorial_ == 1)
		{
			result = 125;
		}
		else if (sub_window.tutorial_ == 10)
		{
			result = 208;
		}
		else if (sub_window.tutorial_ == 20)
		{
			result = 196;
		}
		return result;
	}
}
