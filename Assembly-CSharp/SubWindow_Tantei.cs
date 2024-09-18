public static class SubWindow_Tantei
{
	static SubWindow_Tantei()
	{
	}

	public static void Proc(SubWindow sub_window)
	{
		Routine routine = sub_window.routine_[sub_window.stack_];
		switch (routine.r.no_1)
		{
		case 0:
			switch (routine.r.no_2)
			{
			case 0:
				if (sub_window.note_add_ == 1)
				{
					if (routine.r.no_3 != 0)
					{
						routine.r.no_3--;
						break;
					}
					if (GSStatic.global_work_.r.no_0 == 9)
					{
						routine.r.no_1 = 2;
					}
					sub_window.busy_ = 3u;
					sub_window.note_add_ = 0;
				}
				sub_window.scan_.flag = 1;
				debugLogger.instance.Log("routine.flag", "routine.flag=0");
				routine.flag = 0;
				if (GSStatic.message_work_.mess_win_rno == 1)
				{
					sub_window.busy_ = 3u;
					sub_window.cursor_.Rno_0 = 3;
					routine.r.no_1 = 2;
					routine.r.no_2 = 2;
					sub_window.req_ = SubWindow.Req.NONE;
				}
				else
				{
					GSStatic.tantei_work_.sel_place = 0;
					GSStatic.tantei_work_.menu = 0;
					sub_window.cursor_.Rno_0 = 3;
					sub_window.bar_req_ = SubWindow.BarReq.TANTEI;
					routine.r.no_2++;
				}
				break;
			case 1:
				if (sub_window.CheckObjOut())
				{
					routine.routine_3d[4].Rno_0 = 0;
					BaseButton.base_button_proc(sub_window, 4u);
					routine.r.no_1++;
					routine.r.no_2 = 0;
				}
				break;
			case 2:
				if (sub_window.CheckObjOut())
				{
					routine.routine_3d[4].Rno_0 = 0;
					BaseButton.base_button_proc(sub_window, 4u);
					routine.r.no_1 = 3;
					routine.r.no_2 = 0;
				}
				break;
			}
			break;
		case 1:
			BaseButton.base_button_proc(sub_window, 4u);
			if (((uint)routine.routine_3d[4].state & (true ? 1u : 0u)) != 0 && sub_window.CheckObjIn() && sub_window.bar_req_ == SubWindow.BarReq.NONE)
			{
				sub_window.busy_ = 0u;
				sub_window.req_ = SubWindow.Req.NONE;
				routine.tp_cnt = 0;
				routine.r.Set(5, 2, 0, 0);
			}
			break;
		case 2:
			switch (routine.r.no_2)
			{
			case 0:
				BaseButton.base_button_proc(sub_window, 4u);
				if (sub_window.req_ != 0)
				{
					routine.tp_cnt = 0;
					sub_window.busy_ = 3u;
					if (sub_window.req_ == SubWindow.Req.SELECT)
					{
						routine.routine_3d[4].Rno_0 = 3;
						debugLogger.instance.Log("routine.flag", "routine.flag=1");
						routine.flag = 1;
						routine.r.no_2++;
					}
					else if (sub_window.req_ == SubWindow.Req.STATUS)
					{
						routine.routine_3d[4].Rno_0 = 3;
						debugLogger.instance.Log("routine.flag", "routine.flag=2");
						routine.flag = 2;
						routine.r.no_2++;
					}
					else if (sub_window.req_ == SubWindow.Req.MOVE_GO)
					{
						routine.r.no_1 = 7;
						routine.r.no_2 = 0;
						routine.routine_3d[4].Rno_0 = 3;
					}
					else if (sub_window.req_ == SubWindow.Req.SAVE)
					{
						routine.r.Set(5, 0, 0, 0);
						debugLogger.instance.Log("SubWindowStack", "++stack_");
						sub_window.stack_++;
						sub_window.routine_[sub_window.stack_].r.Set(15, 0, 0, 0);
						sub_window.routine_[sub_window.stack_].tex_no = routine.tex_no;
					}
					else if (sub_window.req_ == SubWindow.Req.LUMINOL)
					{
						routine.routine_3d[4].Rno_0 = 6;
						debugLogger.instance.Log("routine.flag", "routine.flag=3");
						routine.flag = 3;
						routine.r.no_2++;
					}
					else if (sub_window.req_ == SubWindow.Req.FINGER)
					{
						routine.routine_3d[4].Rno_0 = 6;
						debugLogger.instance.Log("routine.flag", "routine.flag=4");
						routine.flag = 4;
						routine.r.no_2++;
					}
					else if (sub_window.req_ == SubWindow.Req.VASE_PUZZLE)
					{
						routine.routine_3d[4].Rno_0 = 6;
						debugLogger.instance.Log("routine.flag", "routine.flag=5");
						routine.flag = 5;
						routine.r.no_2++;
					}
					else if (sub_window.req_ == SubWindow.Req.SAFE_CRACKING)
					{
						routine.routine_3d[4].Rno_0 = 6;
						debugLogger.instance.Log("routine.flag", "routine.flag=6");
						routine.flag = 6;
						routine.r.no_2++;
					}
					else if (sub_window.req_ == SubWindow.Req.BLANK)
					{
						sub_window.req_ = SubWindow.Req.NONE;
						sub_window.bar_req_ = SubWindow.BarReq.IDLE;
						sub_window.scan_.flag = 0;
						routine.r.Set(20, 0, 0, 0);
					}
					else if (sub_window.req_ == SubWindow.Req.RT_TMAIN)
					{
						routine.r.Set(5, 0, 0, 0);
					}
				}
				else if (GSStatic.message_work_.mess_win_rno == 1)
				{
					sub_window.busy_ = 3u;
					TrophyCtrl.check_trophy_by_mes_no();
					routine.routine_3d[4].Rno_0 = 3;
					routine.r.no_2++;
				}
				break;
			case 1:
				BaseButton.base_button_proc(sub_window, 4u);
				if ((routine.routine_3d[4].state & 2u) != 0)
				{
					if (routine.flag == 1)
					{
						routine.r.Set(5, 0, 0, 0);
						debugLogger.instance.Log("SubWindowStack", "++stack_");
						sub_window.stack_++;
						sub_window.routine_[sub_window.stack_].r.Set(3, 0, 0, 0);
						sub_window.routine_[sub_window.stack_].tex_no = routine.tex_no;
					}
					else if (routine.flag == 2)
					{
						routine.r.Set(5, 0, 0, 0);
						debugLogger.instance.Log("SubWindowStack", "++stack_");
						sub_window.stack_++;
						sub_window.routine_[sub_window.stack_].r.Set(11, 0, 0, 0);
						sub_window.routine_[sub_window.stack_].tex_no = routine.tex_no;
					}
					else if (routine.flag == 3)
					{
						routine.r.Set(5, 0, 0, 0);
						debugLogger.instance.Log("SubWindowStack", "++stack_");
						sub_window.stack_++;
						sub_window.routine_[sub_window.stack_].r.Set(23, 0, 0, 1);
						GSStatic.global_work_.r.Set(5, 10, 0, GSStatic.global_work_.r.no_3);
					}
					else if (routine.flag == 4)
					{
						routine.r.Set(5, 0, 0, 0);
						debugLogger.instance.Log("SubWindowStack", "++stack_");
						sub_window.stack_++;
						sub_window.routine_[sub_window.stack_].r.Set(24, 0, 0, 0);
						GSStatic.global_work_.r.Set(5, 10, 0, GSStatic.global_work_.r.no_3);
					}
					else if (routine.flag == 5)
					{
						debugLogger.instance.Log("SubWindowStack", "stack_=1");
						sub_window.stack_ = 1;
						sub_window.routine_[sub_window.stack_].r.Set(26, 0, 0, 0);
						sub_window.routine_[sub_window.stack_].tex_no = 4;
						GSStatic.global_work_.r.Set(5, 10, 0, GSStatic.global_work_.r.no_3);
					}
					else if (routine.flag == 6)
					{
						sub_window.routine_[0].r.Set(5, 0, 0, 0);
						debugLogger.instance.Log("SubWindowStack", "stack_=1");
						sub_window.stack_ = 1;
						sub_window.routine_[sub_window.stack_].r.Set(28, 0, 0, 0);
						sub_window.routine_[sub_window.stack_].tex_no = 4;
						GSStatic.global_work_.r.Set(5, 10, 0, 0);
					}
					else
					{
						routine.r.no_2++;
					}
				}
				break;
			case 2:
				routine.tp_cnt = 0;
				sub_window.bar_req_ = SubWindow.BarReq.TANTEI;
				if (GSStatic.tantei_work_.person_flag == 0 && GSStatic.tantei_work_.menu >= 2)
				{
					GSStatic.tantei_work_.menu = 0;
				}
				routine.r.no_2++;
				break;
			case 3:
				if (sub_window.CheckObjOut())
				{
					routine.r.no_1++;
					routine.r.no_2 = 0;
				}
				break;
			}
			break;
		case 3:
		{
			int in_type = 0;
			if (GSStatic.tantei_work_.person_flag == 1)
			{
				in_type = 1;
			}
			tanteiMenu.instance.play(in_type);
			routine.r.no_1++;
			if (sub_window.req_ != SubWindow.Req.SAVE && sub_window.req_ != SubWindow.Req.STATUS)
			{
				sub_window.req_ = SubWindow.Req.NONE;
			}
			sub_window.busy_ = 0u;
			break;
		}
		case 4:
			if (GSStatic.global_work_.r.no_0 == 8)
			{
				if (sub_window.req_ == SubWindow.Req.STATUS)
				{
					routine.tp_cnt = 0;
					sub_window.busy_ = 3u;
					routine.routine_3d[0].Rno_0 = 5;
					routine.routine_3d[0].Rno_1 = 0;
					routine.routine_3d[0].state |= 2;
					sub_window.cursor_.disp_off = 1;
					routine.r.no_1 = 8;
					routine.r.no_2 = 0;
				}
			}
			else if (GSStatic.global_work_.r.no_0 == 15 && sub_window.req_ == SubWindow.Req.SAVE)
			{
				routine.tp_cnt = 0;
				sub_window.busy_ = 3u;
				routine.r.Set(5, 2, 2, 0);
				debugLogger.instance.Log("SubWindowStack", "++stack_");
				sub_window.stack_++;
				sub_window.routine_[sub_window.stack_].r.Set(15, 0, 0, 0);
				sub_window.routine_[sub_window.stack_].tex_no = routine.tex_no;
			}
			if (!tanteiMenu.instance.is_play)
			{
				GSStatic.tantei_work_.menu = (byte)tanteiMenu.instance.cursor_no;
				routine.r.no_1++;
				routine.r.no_2 = 0;
			}
			break;
		case 5:
			switch (routine.r.no_2)
			{
			case 0:
				routine.routine_3d[0].Rno_0 = 7;
				routine.routine_3d[0].Rno_1 = 0;
				sub_window.cursor_.timer = 10;
				routine.r.no_2++;
				break;
			case 1:
			{
				if (sub_window.cursor_.timer == 0)
				{
					sub_window.cursor_.disp_off = 1;
				}
				else
				{
					sub_window.cursor_.timer--;
				}
				GSStatic.global_work_.r.no_1 = (byte)(GSStatic.tantei_work_.menu + 6);
				if (GSStatic.global_work_.r.no_1 == 6)
				{
					inspectCtrl.instance.reset();
				}
				//ref R r = ref routine.r;
				byte b = 0;
				GSStatic.global_work_.r.no_3 = b;
				routine.r.no_2 = b;
				routine.r.Set(5, 2, 2, 0);
				if (GSStatic.tantei_work_.menu != 3)
				{
					debugLogger.instance.Log("SubWindowStack", "++stack_");
					sub_window.stack_++;
					sub_window.routine_[sub_window.stack_].r.Set((byte)(6 + GSStatic.tantei_work_.menu), 0, 0, 0);
				}
				else
				{
					routine.r.no_1 = 6;
					GSStatic.global_work_.r.no_3 = 2;
				}
				sub_window.routine_[sub_window.stack_].tex_no = routine.tex_no;
				break;
			}
			}
			break;
		case 6:
			if (sub_window.req_ == SubWindow.Req.STATUS)
			{
				routine.tp_cnt = 0;
				routine.r.Set(5, 2, 2, 0);
				debugLogger.instance.Log("SubWindowStack", "++stack_");
				sub_window.stack_++;
				sub_window.routine_[sub_window.stack_].r.Set(11, 0, 0, 0);
				sub_window.routine_[sub_window.stack_].tex_no = routine.tex_no;
			}
			break;
		case 7:
			switch (routine.r.no_2)
			{
			case 0:
				BaseButton.base_button_proc(sub_window, 4u);
				if ((routine.routine_3d[4].state & 2u) != 0)
				{
					if (sub_window.routine_[0].r.no_0 == 2)
					{
						debugLogger.instance.Log("SubWindowStack", "stack_=0");
						sub_window.stack_ = 0;
						sub_window.routine_[sub_window.stack_].r.Set(11, 0, 0, 0);
					}
					else if (sub_window.routine_[0].r.no_0 == 5)
					{
						MessageWork message_work_ = GSStatic.message_work_;
						message_work_.mdt_index++;
						GSStatic.global_work_.Room = message_work_.mdt_data.GetMessage(message_work_.mdt_index);
						message_work_.mdt_index++;
						GSStatic.global_work_.r.Set(5, 5, 0, 0);
						routine.r.no_2++;
					}
				}
				break;
			case 1:
				if (GSStatic.global_work_.r.no_2 == 1)
				{
					debugLogger.instance.Log("SubWindowStack", "stack_=0");
					sub_window.stack_ = 0;
					sub_window.routine_[sub_window.stack_].r.Set(5, 0, 0, 0);
				}
				else if (GSStatic.global_work_.title == TitleId.GS2)
				{
					GS2_tantei_kyousei_idou_01(sub_window);
				}
				break;
			}
			break;
		case 8:
			if ((routine.routine_3d[0].state & 2u) != 0)
			{
				routine.r.Set(5, 0, 0, 0);
				debugLogger.instance.Log("SubWindowStack", "++stack_");
				sub_window.stack_++;
				sub_window.routine_[sub_window.stack_].r.Set(11, 0, 0, 0);
				sub_window.routine_[sub_window.stack_].tex_no = routine.tex_no;
			}
			break;
		}
	}

	private static void GS2_tantei_kyousei_idou_01(SubWindow sub_window)
	{
		GlobalWork global_work_ = GSStatic.global_work_;
		if (global_work_.scenario == 2 && GSStatic.message_work_.now_no == scenario_GS2.SC1_02000 && GSStatic.global_work_.Room == 5)
		{
			sub_window.stack_ = 0;
			sub_window.GetCurrentRoutine().r.Set(5, 0, 0, 0);
		}
	}
}
