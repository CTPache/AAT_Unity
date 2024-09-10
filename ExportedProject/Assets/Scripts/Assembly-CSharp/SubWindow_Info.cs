public static class SubWindow_Info
{
	private delegate void InvestigateProc(SubWindow sub_window, Routine routine);

	private static readonly InvestigateProc[] proc_main_table;

	static SubWindow_Info()
	{
		proc_main_table = new InvestigateProc[7] { Init, Main, GetRecord, BackRecordList, Message, Tutorial, PresentMain };
	}

	public static void Proc(SubWindow sub_window)
	{
		Routine currentRoutine = sub_window.GetCurrentRoutine();
		proc_main_table[currentRoutine.r.no_1](sub_window, sub_window.GetCurrentRoutine());
	}

	private static void Init(SubWindow sub_window, Routine routine)
	{
		switch (routine.r.no_2)
		{
		case 0:
			messageBoardCtrl.instance.guide_set(false, guideCtrl.GuideType.NO_GUIDE);
			if (sub_window.point_3d_ == 1)
			{
				scienceInvestigationCtrl.instance.mode_type = InvestigateType.PRESENT;
				scienceInvestigationCtrl.instance.poly_obj_id = 11;
			}
			else if (sub_window.tutorial_ == 1)
			{
				scienceInvestigationCtrl.instance.mode_type = InvestigateType.FORCE;
				scienceInvestigationCtrl.instance.poly_obj_id = 2;
			}
			else if (sub_window.tutorial_ == 20)
			{
				scienceInvestigationCtrl.instance.mode_type = InvestigateType.FORCE;
				scienceInvestigationCtrl.instance.poly_obj_id = 13;
			}
			else if (sub_window.tutorial_ == 30)
			{
				scienceInvestigationCtrl.instance.mode_type = InvestigateType.ENDING;
				scienceInvestigationCtrl.instance.poly_obj_id = 30;
			}
			else
			{
				scienceInvestigationCtrl.instance.mode_type = InvestigateType.NORMAL;
				scienceInvestigationCtrl.instance.poly_obj_id = recordListCtrl.instance.detail_obj_id;
			}
			if (sub_window.point_3d_ != 1 && routine.flag != 1)
			{
				if (GSStatic.tantei_work_.person_flag != 1 || sub_window.tutorial_ != 30)
				{
				}
				MessageWork activeMessageWork = MessageSystem.GetActiveMessageWork();
				if (activeMessageWork.mess_win_rno == 0)
				{
					activeMessageWork.message_trans_flag = 0;
					scienceInvestigationCtrl.instance.mess_win_out = true;
				}
				else
				{
					scienceInvestigationCtrl.instance.mess_win_out = false;
				}
			}
			if (TestimonyRoot.instance.TestimonyIconEnabled)
			{
				TestimonyRoot.instance.TestimonyIconEnabled = false;
			}
			routine.r.no_2++;
			break;
		case 1:
			if (!recordListCtrl.instance.detail_open)
			{
				routine.r.no_2++;
			}
			break;
		case 2:
			scienceInvestigationCtrl.instance.Play();
			routine.r.no_2++;
			break;
		case 3:
			if (sub_window.tutorial_ == 1)
			{
				routine.r.no_1 = 5;
				routine.r.no_2 = 0;
			}
			else
			{
				routine.r.no_2++;
			}
			break;
		case 4:
			if (scienceInvestigationCtrl.instance.active_cursor)
			{
				if (sub_window.point_3d_ == 0)
				{
					routine.r.no_1++;
					routine.r.no_2 = 0;
					break;
				}
				routine.r.no_1 = 6;
				routine.r.no_2 = 0;
				sub_window.req_ = SubWindow.Req.NONE;
				sub_window.busy_ = 0u;
			}
			break;
		}
	}

	private static void Main(SubWindow sub_window, Routine routine)
	{
		if (scienceInvestigationCtrl.instance.is_ending_failed && !scienceInvestigationCtrl.instance.is_play)
		{
			sub_window.stack_ = 0;
			sub_window.routine_[sub_window.stack_].r.Set(0, 0, 0, 0);
			GSStatic.global_work_.Mess_move_flag = 1;
			MessageSystem.GetActiveMessageWork().message_trans_flag = 1;
			scienceInvestigationCtrl.instance.SetNextScenario();
			return;
		}
		int tutorial_ = sub_window.tutorial_;
		bool flag = false;
		if (tutorial_ == 1 && scienceInvestigationCtrl.instance.hit_point_index >= 0)
		{
			routine.r.no_1 = 5;
			routine.r.no_2 = 2;
			flag = true;
		}
		if (scienceInvestigationCtrl.instance.called_back)
		{
			switch (tutorial_)
			{
			case 0:
				routine.r.no_1 = 3;
				routine.r.no_2 = 0;
				break;
			case 10:
				routine.r.no_1 = 5;
				routine.r.no_2 = 5;
				break;
			case 1:
			case 2:
			case 20:
			case 30:
				return;
			}
			routine.tp_cnt = 0;
			sub_window.busy_ = 3u;
			routine.r.no_3 = 1;
		}
		else if (!flag && scienceInvestigationCtrl.instance.called_check)
		{
			routine.r.no_1 = 2;
			routine.r.no_2 = 0;
			routine.tp_cnt = 0;
			sub_window.busy_ = 3u;
		}
	}

	private static void GetRecord(SubWindow sub_window, Routine routine)
	{
		switch (routine.r.no_2)
		{
		case 0:
			if (sub_window.tutorial_ == 30)
			{
				MessageSystem.GetActiveMessageWork().message_trans_flag = 0;
				GSStatic.global_work_.Mess_move_flag = 0;
				routine.r.no_2 = 4;
			}
			else
			{
				MessageSystem.GetActiveMessageWork().message_trans_flag = 0;
				GSStatic.global_work_.Mess_move_flag = 0;
				routine.r.no_2++;
			}
			break;
		case 1:
			if (scienceInvestigationCtrl.instance.check_state < 2 || scienceInvestigationCtrl.instance.check_state == 4)
			{
				break;
			}
			if (!scienceInvestigationCtrl.instance.is_exist_mesasge)
			{
				if (sub_window.status_force_ == 1 && scienceInvestigationCtrl.instance.poly_obj_id == 28)
				{
					routine.r.no_1 = 3;
					routine.r.no_2 = 0;
				}
				else
				{
					routine.flag = 1;
					routine.r.no_2 = 5;
				}
			}
			else
			{
				routine.r.no_2++;
			}
			break;
		case 2:
			routine.r.no_2++;
			GetRecord(sub_window, routine);
			break;
		case 3:
			sub_window.busy_ = 0u;
			MessageSystem.GetActiveMessageWork().message_trans_flag = 1;
			GSStatic.global_work_.Mess_move_flag = 1;
			routine.r.no_1 = 4;
			routine.r.no_2 = 0;
			break;
		case 4:
			if (!scienceInvestigationCtrl.instance.is_play)
			{
				sub_window.busy_ = 0u;
				sub_window.stack_ = 0;
				sub_window.routine_[sub_window.stack_].r.Set(0, 0, 0, 0);
				GSStatic.global_work_.Mess_move_flag = 1;
				MessageSystem.GetActiveMessageWork().message_trans_flag = 1;
				scienceInvestigationCtrl.instance.SetNextScenario();
			}
			break;
		case 5:
			if (scienceInvestigationCtrl.instance.check_state == 0)
			{
				routine.r.no_1 = 0;
				routine.r.no_2 = 3;
			}
			else if (scienceInvestigationCtrl.instance.check_state == 4)
			{
				routine.r.no_1 = 0;
				routine.r.no_2 = 4;
			}
			else if (scienceInvestigationCtrl.instance.check_state == 5)
			{
				routine.r.no_1 = 3;
				routine.r.no_2 = 0;
			}
			break;
		case 6:
			routine.r.no_1 = 0;
			routine.r.no_2 = 0;
			break;
		}
	}

	private static void BackRecordList(SubWindow sub_window, Routine routine)
	{
		switch (routine.r.no_2)
		{
		case 0:
			if (!scienceInvestigationCtrl.instance.is_play)
			{
				routine.r.no_2++;
			}
			break;
		case 1:
		{
			routine.flag = 0;
			MessageWork activeMessageWork = MessageSystem.GetActiveMessageWork();
			if (sub_window.tutorial_ == 2)
			{
				activeMessageWork.message_trans_flag = 1;
				GSStatic.global_work_.Mess_move_flag = 1;
				sub_window.busy_ = 3u;
				sub_window.stack_ = 1;
				sub_window.routine_[sub_window.stack_].r.Set(10, 0, 0, 0);
				GSStatic.global_work_.r.Set(5, scenario.RNO1_TANTEI_MAIN, 0, 0);
			}
			else if (sub_window.tutorial_ == 10)
			{
				if (GSFlag.Check(0u, scenario.SCE40_FLAG_ST_G_KEITAI_MES))
				{
					activeMessageWork.message_trans_flag = 1;
					GSStatic.global_work_.Mess_move_flag = 1;
					sub_window.busy_ = 3u;
					sub_window.stack_ = 1;
					sub_window.routine_[sub_window.stack_].r.Set(10, 0, 0, 0);
					GSStatic.global_work_.r.Set(5, scenario.RNO1_TANTEI_MAIN, 0, 0);
				}
				else
				{
					GSStatic.global_work_.Mess_move_flag = 0;
					sub_window.busy_ = 3u;
					sub_window.stack_ -= 3;
					GSStatic.global_work_.r.CopyFrom(ref GSStatic.global_work_.r_bk);
				}
			}
			else if (sub_window.tutorial_ == 20)
			{
				activeMessageWork.message_trans_flag = 1;
				GSStatic.global_work_.Mess_move_flag = 1;
				sub_window.stack_ -= 3;
			}
			else if (sub_window.status_force_ == 1 && scienceInvestigationCtrl.instance.poly_obj_id == 28 && routine.r.no_3 == 0)
			{
				activeMessageWork.message_trans_flag = 1;
				GSStatic.global_work_.Mess_move_flag = 1;
				sub_window.stack_ = 0;
				GSStatic.global_work_.r.Set(4, 1, 0, 0);
			}
			else
			{
				activeMessageWork.message_trans_flag = 0;
				activeMessageWork.mess_win_rno = 1;
				sub_window.stack_--;
				if (GSStatic.global_work_.r_bk.no_0 == 6)
				{
					TestimonyRoot.instance.TestimonyIconEnabled = true;
				}
			}
			scienceInvestigationCtrl.instance.SetNextScenario();
			if (scienceInvestigationCtrl.instance.mess_win_out)
			{
				GSStatic.global_work_.Mess_move_flag = 1;
				activeMessageWork.message_trans_flag = 1;
				activeMessageWork.mess_win_rno = 0;
			}
			break;
		}
		}
	}

	private static void Message(SubWindow sub_window, Routine routine)
	{
		switch (routine.r.no_2)
		{
		case 0:
			if (scienceInvestigationCtrl.instance.active_message)
			{
				break;
			}
			routine.tp_cnt = 0;
			sub_window.busy_ = 3u;
			sub_window.req_ = SubWindow.Req.NONE;
			if (sub_window.tutorial_ == 10)
			{
				MessageSystem.GetActiveMessageWork().message_trans_flag = 0;
				GSStatic.global_work_.Mess_move_flag = 0;
				if (GSFlag.Check(0u, scenario.SCE40_FLAG_ST_G_KEITAI_MES))
				{
					routine.r.no_1 = 3;
					routine.r.no_2 = 0;
				}
				else
				{
					routine.r.no_2++;
				}
			}
			else if (sub_window.tutorial_ == 20)
			{
				if (GSFlag.Check(0u, scenario.SCE41_FLAG_MES61930))
				{
					routine.r.no_1 = 3;
					routine.r.no_2 = 0;
				}
				else
				{
					MessageSystem.GetActiveMessageWork().message_trans_flag = 0;
					routine.r.no_2++;
				}
			}
			else if (sub_window.tutorial_ != 0)
			{
				MessageSystem.GetActiveMessageWork().message_trans_flag = 0;
				routine.r.no_1 = 3;
				routine.r.no_2 = 0;
			}
			else if (sub_window.status_force_ == 1 && scienceInvestigationCtrl.instance.poly_obj_id == 28)
			{
				routine.r.no_1 = 3;
				routine.r.no_2 = 0;
			}
			else
			{
				routine.r.no_2++;
			}
			break;
		case 1:
			if (scienceInvestigationCtrl.instance.check_state == 0)
			{
				routine.flag = 1;
				routine.r.no_1 = 0;
				routine.r.no_2 = 3;
			}
			else if (scienceInvestigationCtrl.instance.check_state == 4)
			{
				routine.flag = 1;
				routine.r.no_1 = 0;
				routine.r.no_2 = 4;
			}
			else if (scienceInvestigationCtrl.instance.check_state == 5)
			{
				routine.flag = 1;
				routine.r.no_1 = 3;
				routine.r.no_2 = 0;
			}
			break;
		}
	}

	private static void Tutorial(SubWindow sub_window, Routine routine)
	{
		MessageSystem message_system_ = advCtrl.instance.message_system_;
		switch (routine.r.no_2)
		{
		case 0:
			if (scienceInvestigationCtrl.instance.is_play_ready)
			{
				MessageSystem.GetActiveMessageWork().message_trans_flag = 0;
				MessageSystem.GetActiveMessageWork().message_trans_flag = 1;
				GSStatic.global_work_.Mess_move_flag = 1;
				message_system_.SetMessage(scenario.SC4_60261);
				sub_window.req_ = SubWindow.Req.NONE;
				sub_window.busy_ = 0u;
				routine.r.no_2++;
			}
			break;
		case 1:
			if (sub_window.req_ == SubWindow.Req.MESS_EXIT)
			{
				sub_window.bar_req_ = SubWindow.BarReq.STATUS_3D;
				sub_window.req_ = SubWindow.Req.NONE;
				routine.tp_cnt = 0;
				routine.r.no_1 = 0;
				routine.r.no_2 = 4;
			}
			break;
		case 2:
			sub_window.bar_req_ = SubWindow.BarReq.IDLE;
			routine.r.no_2++;
			break;
		case 3:
			if (sub_window.bar_req_ == SubWindow.BarReq.NONE)
			{
				MessageSystem.GetActiveMessageWork().message_trans_flag = 0;
				MessageSystem.GetActiveMessageWork().message_trans_flag = 1;
				GSStatic.global_work_.Mess_move_flag = 1;
				message_system_.SetMessage(scenario.SC4_60262);
				sub_window.req_ = SubWindow.Req.NONE;
				routine.r.no_2++;
			}
			break;
		case 4:
			if (sub_window.req_ == SubWindow.Req.MESS_EXIT)
			{
				sub_window.req_ = SubWindow.Req.NONE;
				routine.tp_cnt = 0;
				sub_window.tutorial_ = 2;
				routine.r.no_1 = 1;
				routine.r.no_2 = 0;
			}
			break;
		case 5:
			sub_window.bar_req_ = SubWindow.BarReq.IDLE;
			routine.r.no_2++;
			break;
		case 6:
			if (sub_window.bar_req_ == SubWindow.BarReq.NONE)
			{
				sub_window.busy_ = 0u;
				routine.r.no_2++;
			}
			break;
		case 7:
			if (!scienceInvestigationCtrl.instance.active_message)
			{
				routine.tp_cnt = 0;
				routine.r.no_1 = 3;
				routine.r.no_2 = 0;
			}
			break;
		}
	}

	private static void PresentMain(SubWindow sub_window, Routine routine)
	{
		switch (routine.r.no_2)
		{
		case 0:
			if (sub_window.req_ == SubWindow.Req.POINT_EXIT)
			{
				routine.tp_cnt = 0;
				sub_window.busy_ = 3u;
				routine.r.no_2++;
			}
			break;
		case 1:
			if (!scienceInvestigationCtrl.instance.is_play)
			{
				routine.timer0 = 15;
				routine.r.no_2++;
			}
			break;
		case 2:
			if (routine.timer0-- == 0)
			{
				routine.r.no_2++;
			}
			break;
		case 3:
		{
			sub_window.point_3d_ = 0;
			bool flag = false;
			int hit_point_index = scienceInvestigationCtrl.instance.hit_point_index;
			if (hit_point_index >= 0)
			{
				polyData polyData2 = polyDataCtrl.instance.obj_table[scienceInvestigationCtrl.instance.poly_obj_id - 1];
				flag = polyData2.event_tbl[hit_point_index] == 20;
			}
			MessageSystem message_system_ = advCtrl.instance.message_system_;
			EXPL_CK_DATA eXPL_CK_DATA = GSScenario.GS1_expl_ck_data_tbl[GSStatic.tantei_work_.siteki_no];
			message_system_.SetMessage((!flag) ? eXPL_CK_DATA.falseMes1 : eXPL_CK_DATA.trueMes);
			GSStatic.global_work_.Mess_move_flag = 1;
			MessageSystem.GetActiveMessageWork().message_trans_flag = 1;
			sub_window.stack_ = 0;
			routine.r.no_1 = 0;
			routine.r.no_2 = 1;
			break;
		}
		}
	}
}
