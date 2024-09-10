public static class SubWindow_Kaiwa
{
	static SubWindow_Kaiwa()
	{
	}

	public static void Proc(SubWindow sub_window)
	{
		GlobalWork global_work_ = GSStatic.global_work_;
		Routine currentRoutine = sub_window.GetCurrentRoutine();
		Routine routine = sub_window.routine_[sub_window.stack_ - 1];
		Routine3d[] routine_3d = currentRoutine.routine_3d;
		switch (currentRoutine.r.no_1)
		{
		case 242:
			if (global_work_.r.no_0 == 5 && global_work_.r.no_1 == 11 && GSPsylock.PsylockDisp_is_wait())
			{
				if (currentRoutine.timer0 == 0)
				{
					if (global_work_.psy_menu_active_flag != 3)
					{
						GSStatic.message_work_.mess_win_rno = 0;
						currentRoutine.r.no_1 = 0;
					}
					else if (GSStatic.message_work_.mess_win_rno == 0)
					{
						currentRoutine.r.no_1 = 0;
					}
					messageBoardCtrl.instance.ActiveMessageBoardTouch();
				}
				else
				{
					currentRoutine.timer0--;
				}
			}
			else
			{
				sub_window.busy_ = 3u;
				currentRoutine.timer0 = 3;
			}
			break;
		case 0:
			if (MessageSystem.GetActiveMessageWork().mess_win_rno == 1)
			{
				sub_window.busy_ = 3u;
				break;
			}
			sub_window.SetObjDispFlag(10);
			currentRoutine.r.no_1 = 240;
			break;
		case 240:
			if (sub_window.CheckObjOut())
			{
				routine_3d[2].Rno_0 = 0;
				BaseButton.base_button_proc(sub_window, 2u);
				sub_window.bar_req_ = SubWindow.BarReq.TANTEI;
				currentRoutine.r.no_1 = 1;
			}
			break;
		case 1:
		{
			BaseButton.base_button_proc(sub_window, 2u);
			if ((routine_3d[2].state & 1) == 0 || !sub_window.CheckObjIn() || sub_window.bar_req_ != 0)
			{
				break;
			}
			sub_window.busy_ = 0u;
			if ((sub_window.req_ != SubWindow.Req.STATUS || global_work_.r.no_3 != 3) && sub_window.req_ != SubWindow.Req.RT_TMAIN)
			{
				sub_window.req_ = SubWindow.Req.NONE;
			}
			for (int i = 0; i < currentRoutine.tp_cnt; i++)
			{
			}
			currentRoutine.tp_cnt = 0;
			currentRoutine.r.no_1++;
			currentRoutine.r.no_2 = 0;
			if (GSStatic.global_work_.title == TitleId.GS2)
			{
				MessageWork activeMessageWork = GSStatic.message_work_;
				if (global_work_.scenario == 8 && activeMessageWork.code == 52)
				{
					sub_window.SetReq(SubWindow.Req.MOVE_GO);
				}
			}
			break;
		}
		case 2:
		{
			BaseButton.base_button_proc(sub_window, 2u);
			if (global_work_.r.no_0 == 8)
			{
				if (sub_window.req_ == SubWindow.Req.STATUS)
				{
					if (sub_window.tantei_tukituke_ != 0)
					{
						sub_window.tantei_tukituke_ = 2;
					}
					for (int i = 0; i < currentRoutine.tp_cnt; i++)
					{
					}
					currentRoutine.tp_cnt = 0;
					sub_window.busy_ = 3u;
					sub_window.SetObjDispFlag(37);
					currentRoutine.flag = 1;
					currentRoutine.r.no_1++;
					currentRoutine.r.no_2 = 0;
				}
				break;
			}
			if (global_work_.r.no_0 == 11)
			{
				if (sub_window.req_ == SubWindow.Req.SAVE)
				{
					for (int i = 0; i < currentRoutine.tp_cnt; i++)
					{
					}
					currentRoutine.tp_cnt = 0;
					sub_window.busy_ = 3u;
					sub_window.SetObjDispFlag(37);
					currentRoutine.r.Set(10, 7, 0, 0);
					sub_window.stack_++;
					Routine currentRoutine2 = sub_window.GetCurrentRoutine();
					currentRoutine2.r.Set(15, 0, 0, 0);
					currentRoutine2.tex_no = currentRoutine.tex_no;
				}
				break;
			}
			MessageWork activeMessageWork;
			if (sub_window.req_ != 0)
			{
				for (int i = 0; i < currentRoutine.tp_cnt; i++)
				{
				}
				currentRoutine.tp_cnt = 0;
				sub_window.busy_ = 3u;
				sub_window.SetObjDispFlag(37);
				if (sub_window.req_ == SubWindow.Req.MOVE_GO)
				{
					currentRoutine.r.no_1 = 6;
				}
				else if (sub_window.req_ == SubWindow.Req.STATUS)
				{
					currentRoutine.flag = 1;
					currentRoutine.r.no_1++;
					currentRoutine.r.no_2 = 0;
				}
				else if (sub_window.req_ == SubWindow.Req.SELECT)
				{
					currentRoutine.flag = 2;
					currentRoutine.r.no_1++;
					currentRoutine.r.no_2 = 0;
				}
				else if (sub_window.req_ == SubWindow.Req.POINT)
				{
					if (global_work_.r.no_0 == 5 && global_work_.r.no_1 == 11)
					{
						currentRoutine.flag = 11;
					}
					else
					{
						currentRoutine.flag = 3;
					}
					currentRoutine.r.no_1++;
					currentRoutine.r.no_2 = 0;
				}
				else if (sub_window.req_ == SubWindow.Req.RT_TMAIN)
				{
					currentRoutine.flag = 4;
					currentRoutine.r.no_1++;
					currentRoutine.r.no_2 = 0;
				}
				else if (sub_window.req_ == SubWindow.Req.MAGATAMA_MENU_ON)
				{
					activeMessageWork = MessageSystem.GetActiveMessageWork();
					if ((activeMessageWork.status & MessageSystem.Status.LOOP) != 0)
					{
						for (int i = 0; i < currentRoutine.tp_cnt; i++)
						{
						}
						currentRoutine.tp_cnt = 0;
						sub_window.busy_ = 3u;
						currentRoutine.flag = 10;
						currentRoutine.r.no_1++;
						currentRoutine.r.no_2 = 0;
					}
				}
				else if (sub_window.req_ == SubWindow.Req.LUMINOL_EXIT)
				{
					routine.r.no_1 = 8;
					routine.r.no_2 = 0;
					sub_window.busy_ = 3u;
					sub_window.stack_--;
				}
				else if (sub_window.req_ == SubWindow.Req.STATUS_3D)
				{
					currentRoutine.flag = 5;
					currentRoutine.r.no_1++;
					currentRoutine.r.no_2 = 0;
				}
				else if (sub_window.req_ == SubWindow.Req.FINGER)
				{
					currentRoutine.flag = 6;
					currentRoutine.r.no_1++;
					currentRoutine.r.no_2 = 0;
				}
				else if (sub_window.req_ == SubWindow.Req.VASE_PUZZLE)
				{
					currentRoutine.flag = 7;
					currentRoutine.r.no_1++;
					currentRoutine.r.no_2 = 0;
				}
				else if (sub_window.req_ == SubWindow.Req.SAFE_CRACKING)
				{
					currentRoutine.flag = 8;
					currentRoutine.r.no_1++;
					currentRoutine.r.no_2 = 0;
				}
				else if (sub_window.req_ == SubWindow.Req.HUMAN)
				{
					currentRoutine.flag = 9;
					currentRoutine.r.no_1++;
					currentRoutine.r.no_2 = 0;
				}
				break;
			}
			activeMessageWork = MessageSystem.GetActiveMessageWork();
			if (activeMessageWork.mess_win_rno == 1)
			{
				for (int i = 0; i < currentRoutine.tp_cnt; i++)
				{
				}
				currentRoutine.tp_cnt = 0;
				sub_window.busy_ = 3u;
				currentRoutine.flag = 0;
				currentRoutine.r.no_1++;
				currentRoutine.r.no_2 = 0;
				if (GSStatic.global_work_.title != TitleId.GS3)
				{
				}
			}
			break;
		}
		case 3:
		{
			MessageWork activeMessageWork = MessageSystem.GetActiveMessageWork();
			if (activeMessageWork.message_type == WindowType.SUB)
			{
				MessageSystem.SetActiveMessageWindow(WindowType.MAIN);
			}
			switch (currentRoutine.r.no_2)
			{
			default:
				return;
			case 0:
				routine_3d[2].Rno_0 = 3;
				currentRoutine.r.no_2++;
				break;
			case 1:
				break;
			}
			BaseButton.base_button_proc(sub_window, 2u);
			if ((routine_3d[2].state & 2) == 0)
			{
				break;
			}
			if (currentRoutine.flag != 1)
			{
				TrophyCtrl.check_trophy_by_mes_no();
			}
			if (currentRoutine.flag == 0)
			{
				if (sub_window.tantei_tukituke_ != 0)
				{
					if (global_work_.r.no_0 == 5 && global_work_.r.no_1 == 11)
					{
						currentRoutine.r.no_1 = 99;
					}
					else if (GSStatic.global_work_.psy_unlock_success != 0)
					{
						sub_window.tantei_tukituke_ = 0;
						sub_window.GetCurrentRoutine().r.Set(8, 0, 0, 0);
					}
					else
					{
						sub_window.GetCurrentRoutine().r.Set(11, 0, 0, 0);
						global_work_.r.Set(8, 0, 0, 2);
					}
				}
				else if (sub_window.tantei_tukituke_ == 0 && global_work_.r.no_0 == 5 && global_work_.r.no_1 == 11)
				{
					sub_window.busy_ = 0u;
					currentRoutine.r.no_1 = 9;
					currentRoutine.r.no_2 = 0;
				}
				else
				{
					sub_window.stack_--;
				}
			}
			else if (currentRoutine.flag == 1)
			{
				currentRoutine.r.Set(10, 4, 0, 0);
				sub_window.stack_++;
				Routine currentRoutine3 = sub_window.GetCurrentRoutine();
				currentRoutine3.r.Set(11, 0, 0, 0);
				currentRoutine3.tex_no = currentRoutine.tex_no;
			}
			else if (currentRoutine.flag == 2)
			{
				currentRoutine.r.Set(10, 5, 0, 0);
				sub_window.stack_++;
				Routine currentRoutine4 = sub_window.GetCurrentRoutine();
				currentRoutine4.r.Set(3, 0, 0, 0);
				currentRoutine4.tex_no = currentRoutine.tex_no;
			}
			else if (currentRoutine.flag == 3)
			{
				currentRoutine.r.Set(14, 0, 0, 0);
				sub_window.GetCurrentRoutine().tex_no = currentRoutine.tex_no;
			}
			else if (currentRoutine.flag == 4)
			{
				if (sub_window.tantei_tukituke_ != 0)
				{
					sub_window.tantei_tukituke_ = 0;
				}
				if (sub_window.routine_[sub_window.stack_ - 1].r.no_0 == 8)
				{
					sub_window.bg_return_ = 0;
				}
				if (sub_window.routine_[sub_window.stack_ - 1].r.no_0 == 6)
				{
				}
				sub_window.stack_ = 0;
				sub_window.GetCurrentRoutine().r.Set(5, 0, 0, 0);
			}
			else if (currentRoutine.flag == 5)
			{
				currentRoutine.r.Set(10, 4, 0, 0);
				sub_window.stack_++;
				Routine currentRoutine5 = sub_window.GetCurrentRoutine();
				currentRoutine5.r.Set(11, 0, 0, 0);
				if (GSStatic.message_work_.now_no == scenario.SC4_60840)
				{
					sub_window.tutorial_ = 10;
				}
				else if (GSStatic.message_work_.now_no == scenario.SC4_60260)
				{
					sub_window.tutorial_ = 1;
				}
				currentRoutine5.tex_no = currentRoutine.tex_no;
				global_work_.r_bk.CopyFrom(ref global_work_.r);
				global_work_.r.Set(5, 10, 0, global_work_.r.no_3);
			}
			else if (currentRoutine.flag == 6)
			{
				currentRoutine.r.Set(10, 4, 0, 0);
				sub_window.stack_++;
				Routine currentRoutine6 = sub_window.GetCurrentRoutine();
				currentRoutine6.r.Set(24, 0, 0, 0);
				currentRoutine6.tex_no = currentRoutine.tex_no;
				global_work_.r.Set(5, 10, 0, global_work_.r.no_3);
			}
			else if (currentRoutine.flag == 7)
			{
				sub_window.stack_ = 1;
				Routine currentRoutine7 = sub_window.GetCurrentRoutine();
				currentRoutine7.r.Set(26, 0, 0, 0);
			}
			else if (currentRoutine.flag == 8)
			{
				sub_window.routine_[0].r.Set(5, 0, 0, 0);
				sub_window.stack_ = 1;
				Routine currentRoutine8 = sub_window.GetCurrentRoutine();
				currentRoutine8.r.Set(28, 0, 0, 0);
				global_work_.r.Set(5, 10, 0, 0);
			}
			else if (currentRoutine.flag == 9)
			{
				if (sub_window.routine_[0].r.no_0 == 5)
				{
					global_work_.r_bk.CopyFrom(ref global_work_.r);
					global_work_.r.no_1 = 10;
				}
				currentRoutine.r.Set(10, 8, 0, 0);
				sub_window.stack_++;
				Routine currentRoutine9 = sub_window.GetCurrentRoutine();
				currentRoutine9.r.Set(16, 0, 0, global_work_.r.no_3);
				currentRoutine9.tex_no = currentRoutine.tex_no;
				global_work_.r.no_3 = 0;
			}
			else if (currentRoutine.flag == 10)
			{
				if (sub_window.tantei_tukituke_ != 0)
				{
					sub_window.GetCurrentRoutine().r.Set(11, 0, 0, 0);
					global_work_.r.Set(8, 0, 0, 2);
				}
				else
				{
					sub_window.stack_--;
				}
			}
			else if (currentRoutine.flag == 11)
			{
				currentRoutine.r.Set(10, 5, 0, 0);
				sub_window.stack_++;
				Routine currentRoutine10 = sub_window.GetCurrentRoutine();
				currentRoutine10.r.Set(14, 0, 0, 0);
				currentRoutine10.tex_no = currentRoutine.tex_no;
			}
			else if (currentRoutine.flag == 12)
			{
				sub_window.stack_--;
			}
			break;
		}
		case 4:
		{
			if (currentRoutine.r.no_3 != 0)
			{
				currentRoutine.r.no_3--;
				break;
			}
			if (sub_window.tantei_tukituke_ == 2)
			{
				sub_window.tantei_tukituke_ = 1;
			}
			MessageWork activeMessageWork = MessageSystem.GetActiveMessageWork();
			if (activeMessageWork.mess_win_rno == 1)
			{
				if (sub_window.tantei_tukituke_ != 0)
				{
					sub_window.GetCurrentRoutine().r.Set(11, 0, 0, 0);
					sub_window.SetObjDispFlag(9);
					sub_window.bar_req_ = SubWindow.BarReq.THRUST_0;
					global_work_.r.Set(8, 0, 0, 2);
				}
				else
				{
					sub_window.stack_--;
				}
			}
			else
			{
				sub_window.SetObjDispFlag(10);
				currentRoutine.r.Set(10, 240, 0, 0);
			}
			if (sub_window.note_add_ == 1)
			{
				if (global_work_.r.no_0 == 9)
				{
					global_work_.r.no_1 = 2;
				}
				sub_window.busy_ = 3u;
				sub_window.note_add_ = 0;
			}
			break;
		}
		case 5:
			sub_window.SetObjDispFlag(10);
			sub_window.bar_req_ = SubWindow.BarReq.TANTEI;
			currentRoutine.r.Set(10, 240, 0, 0);
			break;
		case 6:
			switch (currentRoutine.r.no_2)
			{
			case 0:
				sub_window.tantei_tukituke_ = 0;
				if (sub_window.routine_[0].r.no_0 == 2)
				{
					sub_window.stack_ = 0;
					sub_window.GetCurrentRoutine().r.Set(2, 0, 0, 0);
				}
				else if (sub_window.routine_[0].r.no_0 == 5)
				{
					GSStatic.message_work_.mdt_index++;
					GSStatic.global_work_.Room = GSStatic.message_work_.mdt_data.GetMessage(GSStatic.message_work_.mdt_index);
					GSStatic.message_work_.mdt_index++;
					fadeCtrl.instance.play(fadeCtrl.Status.FADE_OUT, 1u, 2u);
					global_work_.r.Set(5, 5, 0, 0);
					currentRoutine.r.no_2++;
				}
				break;
			case 1:
				if (GSStatic.global_work_.r.no_2 == 1 && fadeCtrl.instance.status == fadeCtrl.Status.NO_FADE)
				{
					sub_window.stack_ = 0;
					sub_window.GetCurrentRoutine().r.Set(5, 0, 0, 0);
					if (sub_window.bg_return_ == 1)
					{
						sub_window.bg_return_ = 0;
					}
				}
				break;
			}
			break;
		case 7:
			sub_window.SetObjDispFlag(10);
			sub_window.bar_req_ = SubWindow.BarReq.TANTEI;
			currentRoutine.r.Set(10, 240, 0, 0);
			break;
		case 8:
			sub_window.SetObjDispFlag(10);
			sub_window.bar_req_ = SubWindow.BarReq.TANTEI;
			currentRoutine.r.Set(10, 240, 0, 0);
			if (sub_window.routine_[0].r.no_0 == 5)
			{
				global_work_.r.CopyFrom(ref global_work_.r_bk);
			}
			break;
		case 9:
			if (global_work_.r.no_0 == 5 && global_work_.r.no_1 != 11)
			{
				sub_window.busy_ = 3u;
				sub_window.stack_--;
			}
			break;
		case 99:
			if (global_work_.r.no_0 == 5 && global_work_.gauge_hp == 0 && global_work_.gauge_hp_disp == 0)
			{
				sub_window.busy_ = 0u;
			}
			break;
		}
	}
}
