public static class SubWindow_Move
{
	static SubWindow_Move()
	{
	}

	public static void Proc(SubWindow sub_window)
	{
		Routine currentRoutine = sub_window.GetCurrentRoutine();
		switch (currentRoutine.r.no_1)
		{
		case 0:
			switch (currentRoutine.r.no_2)
			{
			case 0:
			{
				currentRoutine.r.no_3 = 0;
				GlobalWork global_work_ = GSStatic.global_work_;
				for (int i = 0; i < 4; i++)
				{
					uint num = global_work_.Map_data[global_work_.Room][4 + i];
					if (num == 255)
					{
						continue;
					}
					if (GSStatic.global_work_.title == TitleId.GS1)
					{
						moveCtrl.instance.select_list[currentRoutine.r.no_3].bg_no_ = global_work_.Map_data[num][0];
						moveCtrl.instance.select_list[currentRoutine.r.no_3].thumbnail_ = num;
						switch (num)
						{
						case 0u:
							GSStatic.global_work_.sw_move_flag[num] = 1;
							break;
						case 1u:
						case 6u:
							GSStatic.global_work_.sw_move_flag[num] = 1;
							break;
						case 22u:
						case 23u:
						case 24u:
						case 25u:
						case 26u:
						case 27u:
							moveCtrl.instance.select_list[currentRoutine.r.no_3].thumbnail_ = num - 1;
							break;
						}
					}
					currentRoutine.r.no_3++;
				}
				if (currentRoutine.r.no_3 == 0)
				{
					MessageSystem.Mess_window_set(3u);
					switch (GSStatic.global_work_.title)
					{
					case TitleId.GS1:
						advCtrl.instance.message_system_.SetMessage(GS1_GetIdouKinshiMessage());
						break;
					case TitleId.GS2:
						advCtrl.instance.message_system_.SetMessage(GS2_GetIdouKinshiMessage());
						break;
					case TitleId.GS3:
						advCtrl.instance.message_system_.SetMessage(GS3_GetIdouKinshiMessage());
						break;
					}
					debugLogger.instance.Log("SubWindowStack", "--stack_");
					sub_window.stack_--;
					sub_window.routine_[sub_window.stack_].r.Set(5, 0, 0, 0);
					GSStatic.global_work_.r.Set(5, scenario.RNO1_TANTEI_MAIN, 0, 0);
				}
				else
				{
					currentRoutine.r.no_2++;
				}
				break;
			}
			case 1:
				sub_window.bar_req_ = SubWindow.BarReq.TALK;
				currentRoutine.r.no_2++;
				break;
			case 2:
				moveCtrl.instance.play(currentRoutine.r.no_3, 0);
				currentRoutine.r.no_2 = 0;
				currentRoutine.r.no_1++;
				break;
			}
			break;
		case 1:
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
				currentRoutine.r.no_2 = 0;
				currentRoutine.r.no_1++;
				break;
			}
			break;
		case 2:
			if (moveCtrl.instance.select_animation_playing)
			{
				break;
			}
			if (!moveCtrl.instance.is_play)
			{
				if (moveCtrl.instance.is_cancel)
				{
					sub_window.busy_ = 3u;
					currentRoutine.r.no_1 = 6;
					currentRoutine.r.no_2 = 0;
				}
				else
				{
					sub_window.busy_ = 3u;
					currentRoutine.r.no_1++;
					currentRoutine.r.no_2 = 0;
				}
				break;
			}
			if (GSStatic.global_work_.r.no_0 != 11)
			{
				if (padCtrl.instance.GetKeyDown(KeyType.Start))
				{
					if (moveCtrl.instance.select_animation_playing)
					{
						break;
					}
					GlobalWork global_work_2 = GSStatic.global_work_;
					if ((global_work_2.status_flag & 0x10) == 0)
					{
						global_work_2.r_bk.CopyFrom(ref global_work_2.r);
						global_work_2.r.Set(17, 0, 0, 0);
						soundCtrl.instance.PlaySE(49);
					}
				}
				else if (padCtrl.instance.GetKeyDown(KeyType.R) && (GSStatic.global_work_.status_flag & 0x10) == 0)
				{
					GlobalWork global_work_3 = GSStatic.global_work_;
					global_work_3.r.no_2 = 0;
					global_work_3.r_bk.CopyFrom(ref global_work_3.r);
					global_work_3.r.Set(8, 0, 0, 0);
					currentRoutine.tp_cnt = 0;
					sub_window.cursor_.Rno_0 = 3;
					sub_window.cursor_.disp_off = 1;
					currentRoutine.r.no_1 = 7;
					currentRoutine.r.no_2 = 0;
				}
			}
			if (sub_window.req_ == SubWindow.Req.MOVE_GO)
			{
				sub_window.busy_ = 3u;
				currentRoutine.r.no_1++;
				currentRoutine.r.no_2 = 0;
			}
			else if (sub_window.req_ == SubWindow.Req.MOVE_EXIT)
			{
				sub_window.busy_ = 3u;
				currentRoutine.r.no_1 = 6;
				currentRoutine.r.no_2 = 0;
			}
			break;
		case 3:
			switch (currentRoutine.r.no_2)
			{
			default:
				return;
			case 0:
				currentRoutine.r.no_2++;
				return;
			case 1:
				fadeCtrl.instance.play(2u, 1u, 1u);
				currentRoutine.r.no_2++;
				return;
			case 2:
				currentRoutine.r.no_2++;
				break;
			case 3:
				break;
			}
			soundCtrl.instance.FadeOutBGM(20);
			GSStatic.global_work_.Room = GSStatic.global_work_.Map_data[GSStatic.global_work_.Room][4 + moveCtrl.instance.cursor_no];
			GSStatic.global_work_.r.Set(5, scenario.RNO1_TANTEI_ROOM_INIT, 0, 0);
			currentRoutine.r.no_1++;
			currentRoutine.r.no_2 = 0;
			break;
		case 4:
			if (GSStatic.global_work_.r.no_1 == scenario.RNO1_TANTEI_MAIN)
			{
				sub_window.routine_[sub_window.stack_ - 1].r.Set(5, 0, 0, 0);
				debugLogger.instance.Log("SubWindowStack", "--stack_");
				sub_window.stack_--;
			}
			break;
		case 5:
			switch (currentRoutine.r.no_2)
			{
			case 0:
				sub_window.bar_req_ = SubWindow.BarReq.TALK;
				currentRoutine.r.no_2++;
				break;
			case 1:
				currentRoutine.r.no_2++;
				break;
			case 2:
				moveCtrl.instance.play(currentRoutine.r.no_3, moveCtrl.instance.bk_cursor_no);
				sub_window.busy_ = 0u;
				sub_window.req_ = SubWindow.Req.NONE;
				currentRoutine.r.no_1 = 2;
				currentRoutine.r.no_2 = 0;
				break;
			}
			break;
		case 6:
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
				GSStatic.global_work_.r.Set(5, scenario.RNO1_TANTEI_MAIN, 0, 0);
				debugLogger.instance.Log("SubWindowStack", "--stack_");
				sub_window.stack_--;
				break;
			}
			break;
		case 7:
			switch (currentRoutine.r.no_2)
			{
			case 0:
				currentRoutine.r.no_2++;
				break;
			case 1:
				currentRoutine.r.no_2++;
				break;
			case 2:
				moveCtrl.instance.BackupCursorNo();
				moveCtrl.instance.stop();
				sub_window.routine_[sub_window.stack_].r.Set(7, 5, 0, sub_window.routine_[sub_window.stack_].r.no_3);
				debugLogger.instance.Log("SubWindowStack", "++stack_");
				sub_window.stack_++;
				sub_window.routine_[sub_window.stack_].r.Set(11, 0, 0, 0);
				break;
			}
			break;
		}
	}

	private static uint GS1_GetIdouKinshiMessage()
	{
		return 194u;
	}

	private static uint GS2_GetIdouKinshiMessage()
	{
		return scenario_GS2.SC3_IDOU;
	}

	private static uint GS3_GetIdouKinshiMessage()
	{
		if (GSStatic.global_work_.scenario == 14)
		{
			return 151u;
		}
		if (GSStatic.global_work_.scenario == 15)
		{
			return scenario_GS3.SC4_0_NOEXIT_M;
		}
		return scenario_GS3.SYS_M0320;
	}
}
