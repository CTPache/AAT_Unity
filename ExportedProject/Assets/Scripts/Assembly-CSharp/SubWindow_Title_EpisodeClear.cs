public class SubWindow_Title_EpisodeClear
{
	private delegate void Title_EpisodeClear_Proc(SubWindow sub_window);

	private static readonly Title_EpisodeClear_Proc[] proc_table;

	static SubWindow_Title_EpisodeClear()
	{
		proc_table = new Title_EpisodeClear_Proc[4] { Back, Appear, Sc4_00, Sc4_01 };
	}

	public static void Proc(SubWindow sub_window)
	{
		Routine currentRoutine = sub_window.GetCurrentRoutine();
		proc_table[currentRoutine.r.no_2](sub_window);
		Main(sub_window);
	}

	private static void Main(SubWindow sub_window)
	{
	}

	private static void Back(SubWindow sub_window)
	{
		GlobalWork global_work_ = GSStatic.global_work_;
		MessageWork activeMessageWork = MessageSystem.GetActiveMessageWork();
		Routine routine = sub_window.routine_[sub_window.stack_];
		Routine3d[] routine_3d = routine.routine_3d;
		if (global_work_.title == TitleId.GS1 && global_work_.scenario == 17)
		{
			routine.r.no_2 = 2;
			return;
		}
		switch (routine.r.no_3)
		{
		case 0:
			global_work_.Cursol = 0;
			routine_3d[0].disp_off = 1;
			routine_3d[1].disp_off = 1;
			routine_3d[2].disp_off = 1;
			routine_3d[5].disp_off = 1;
			switch (global_work_.title)
			{
			case TitleId.GS1:
				switch (global_work_.scenario)
				{
				case 1:
					routine.flag = 1;
					break;
				case 5:
					routine.flag = 2;
					break;
				case 11:
					routine.flag = 3;
					break;
				case 17:
					routine.flag = 4;
					break;
				default:
					routine.flag = 0;
					break;
				}
				break;
			case TitleId.GS2:
				routine.flag = 0;
				break;
			case TitleId.GS3:
				routine.flag = 0;
				break;
			}
			episodeReleaseCtrl.instance.play();
			routine.r.no_3 = 8;
			break;
		case 1:
			routine_3d[0].x = 87;
			routine_3d[0].y = 56;
			routine_3d[0].scale = 100;
			routine_3d[0].disp_off = 1;
			routine_3d[1].x = 296;
			routine_3d[1].y = 56;
			routine_3d[1].scale = 100;
			routine_3d[1].disp_off = 0;
			routine_3d[2].x = 505;
			routine_3d[2].y = 56;
			routine_3d[2].scale = 100;
			routine_3d[2].disp_off = 0;
			routine.r.no_3++;
			break;
		case 2:
			if (global_work_.Cursol < routine.flag)
			{
				routine_3d[0].x -= 27;
				routine_3d[1].x -= 27;
				routine_3d[2].x -= 27;
			}
			else
			{
				routine_3d[0].x -= 13;
				routine_3d[1].x -= 13;
				routine_3d[2].x -= 13;
			}
			if (routine_3d[1].x <= 40)
			{
				routine_3d[0].x = -169;
				routine_3d[1].x = 40;
				routine_3d[2].x = 249;
				routine_3d[5].x = 96;
				routine.timer0 = 0;
				routine.r.no_3++;
			}
			break;
		case 3:
			routine.timer0 = 0;
			routine_3d[0].x = -169;
			routine_3d[1].x = 40;
			routine_3d[2].x = 249;
			routine_3d[0].disp_off = 0;
			routine_3d[1].disp_off = 0;
			routine_3d[2].disp_off = 0;
			routine_3d[3].disp_off = 0;
			routine_3d[4].disp_off = 0;
			if (global_work_.Cursol == routine.flag)
			{
				routine.r.no_3 += 2;
				break;
			}
			global_work_.Cursol++;
			routine.r.no_3++;
			if (global_work_.Cursol < routine.flag)
			{
				routine_3d[0].x -= 27;
				routine_3d[1].x -= 27;
				routine_3d[2].x -= 27;
			}
			else
			{
				routine_3d[0].x -= 13;
				routine_3d[1].x -= 13;
				routine_3d[2].x -= 13;
			}
			break;
		case 4:
			if (global_work_.Cursol < routine.flag)
			{
				routine_3d[0].x -= 27;
				routine_3d[1].x -= 27;
				routine_3d[2].x -= 27;
			}
			else
			{
				routine_3d[0].x -= 13;
				routine_3d[1].x -= 13;
				routine_3d[2].x -= 13;
			}
			if (routine_3d[0].x < -176)
			{
				routine_3d[0].x += 627;
				if (global_work_.Cursol == routine.flag)
				{
					routine_3d[0].disp_off = 1;
				}
				else
				{
					routine_3d[0].disp_off = 0;
				}
			}
			if (routine_3d[2].x <= 40)
			{
				routine_3d[0].x = 249;
				routine_3d[1].x = -169;
				routine_3d[2].x = 40;
				routine.r.no_3--;
			}
			break;
		case 5:
			routine_3d[1].rotate[0] = 0;
			routine.r.no_3++;
			break;
		case 6:
			routine_3d[1].rotate[0] += 3;
			if (routine_3d[1].rotate[0] >= 90)
			{
				routine_3d[1].rotate[0] += 180;
				routine.r.no_3++;
			}
			break;
		case 7:
			routine_3d[1].rotate[0] += 3;
			if (routine_3d[1].rotate[0] >= 360)
			{
				routine_3d[1].rotate[0] = 0;
				routine.r.no_3++;
			}
			break;
		case 8:
			if (!episodeReleaseCtrl.instance.is_scroll)
			{
				routine.r.no_2++;
				routine.r.no_3 = 0;
			}
			break;
		}
		if (!episodeReleaseCtrl.instance.is_scroll)
		{
			global_work_.Mess_move_flag = 1;
			activeMessageWork.message_trans_flag = 1;
			activeMessageWork.now_no = ushort.MaxValue;
		}
	}

	private static void Appear(SubWindow sub_window)
	{
		GlobalWork global_work_ = GSStatic.global_work_;
		Routine routine = sub_window.routine_[sub_window.stack_];
		Routine3d[] routine_3d = routine.routine_3d;
		MessageWork activeMessageWork = MessageSystem.GetActiveMessageWork();
		switch (routine.r.no_3)
		{
		case 0:
			if (activeMessageWork.status == MessageSystem.Status.LOOP)
			{
				return;
			}
			if (!episodeReleaseCtrl.instance.is_play)
			{
				global_work_.Mess_move_flag = 0;
				activeMessageWork.message_trans_flag = 0;
				routine_3d[0].disp_off = 1;
				routine_3d[1].disp_off = 1;
				routine_3d[5].disp_off = 1;
				routine.timer0 = 32;
				routine.r.no_3++;
			}
			break;
		case 1:
			routine.r.no_3++;
			if (routine.timer0 != 0)
			{
				routine.timer0--;
			}
			break;
		case 2:
			routine.r.no_3++;
			break;
		case 3:
			global_work_.r.Set(11, 0, 2, 1);
			routine.r.no_3++;
			break;
		case 4:
			if (sub_window.req_ == SubWindow.Req.SAVE)
			{
				sub_window.routine_[sub_window.stack_].r.Set(15, 0, 0, 0);
			}
			break;
		}
		if (routine.r.no_3 >= 1)
		{
			routine_3d[2].rotate[0] += 10;
			if (routine_3d[2].rotate[0] >= 90)
			{
				routine_3d[2].disp_off = 1;
			}
		}
	}

	private static void Sc4_00(SubWindow sub_window)
	{
		GlobalWork global_work_ = GSStatic.global_work_;
		MessageWork activeMessageWork = MessageSystem.GetActiveMessageWork();
		Routine routine = sub_window.routine_[sub_window.stack_];
		Routine3d[] routine_3d = routine.routine_3d;
		switch (routine.r.no_3)
		{
		case 0:
			routine.Rno_4 = 0;
			global_work_.Cursol = 0;
			routine_3d[0].disp_off = 1;
			routine_3d[1].disp_off = 1;
			routine_3d[2].disp_off = 1;
			routine_3d[5].disp_off = 1;
			Balloon.PlayHoldIt();
			soundCtrl.instance.AllStopBGM();
			bgCtrl.instance.SetSprite(4095);
			routine.Rno_4 = 1;
			routine.r.no_3++;
			break;
		case 1:
			if (!objMoveCtrl.instance.is_play)
			{
				episodeReleaseCtrl.instance.play();
				routine.r.no_3 = 9;
			}
			break;
		case 2:
			routine_3d[0].x = 87;
			routine_3d[0].y = 56;
			routine_3d[0].scale = 100;
			routine_3d[0].disp_off = 1;
			routine_3d[1].x = 296;
			routine_3d[1].y = 56;
			routine_3d[1].scale = 100;
			routine_3d[1].disp_off = 0;
			routine_3d[2].x = 505;
			routine_3d[2].y = 56;
			routine_3d[2].scale = 100;
			routine_3d[2].disp_off = 0;
			routine.r.no_3++;
			break;
		case 3:
			if (global_work_.Cursol < routine.flag)
			{
				routine_3d[0].x -= 54;
				routine_3d[1].x -= 54;
				routine_3d[2].x -= 54;
			}
			else
			{
				routine_3d[0].x -= 13;
				routine_3d[1].x -= 13;
				routine_3d[2].x -= 13;
			}
			if (routine_3d[1].x <= 40)
			{
				routine_3d[0].x = -169;
				routine_3d[1].x = 40;
				routine_3d[2].x = 249;
				routine_3d[5].x = 96;
				routine.timer0 = 0;
				routine.r.no_3++;
			}
			break;
		case 4:
			routine.timer0 = 0;
			routine_3d[0].x = -169;
			routine_3d[1].x = 40;
			routine_3d[2].x = 249;
			routine_3d[0].disp_off = 0;
			routine_3d[2].disp_off = 0;
			routine_3d[3].disp_off = 0;
			routine_3d[4].disp_off = 0;
			routine_3d[1].disp_off = 0;
			if (global_work_.Cursol == routine.flag)
			{
				routine.timer0 = 60;
				routine.r.no_3 += 2;
				break;
			}
			global_work_.Cursol++;
			routine.r.no_3++;
			if (global_work_.Cursol < routine.flag)
			{
				routine_3d[0].x -= 54;
				routine_3d[1].x -= 54;
				routine_3d[2].x -= 54;
			}
			else
			{
				routine_3d[0].x -= 13;
				routine_3d[1].x -= 13;
				routine_3d[2].x -= 13;
			}
			break;
		case 5:
			if (global_work_.Cursol < routine.flag)
			{
				routine_3d[0].x -= 54;
				routine_3d[1].x -= 54;
				routine_3d[2].x -= 54;
			}
			else
			{
				routine_3d[0].x -= 13;
				routine_3d[1].x -= 13;
				routine_3d[2].x -= 13;
			}
			if (routine_3d[0].x < -176)
			{
				routine_3d[0].x += 627;
				if (global_work_.Cursol == routine.flag)
				{
					routine_3d[0].disp_off = 1;
				}
				else
				{
					routine_3d[0].disp_off = 0;
				}
			}
			if (routine_3d[2].x <= 40)
			{
				routine_3d[0].x = 249;
				routine_3d[1].x = -169;
				routine_3d[2].x = 40;
				routine.r.no_3--;
			}
			break;
		case 6:
			if (routine.timer0-- == 0)
			{
				routine.r.no_3++;
			}
			break;
		case 7:
			routine_3d[1].rotate[0] = 0;
			routine.r.no_3++;
			break;
		case 8:
			routine_3d[1].rotate[0] += 3;
			if (routine_3d[1].rotate[0] >= 90)
			{
				routine_3d[1].rotate[0] += 180;
				routine.r.no_3++;
			}
			break;
		case 9:
			if (!episodeReleaseCtrl.instance.is_scroll)
			{
				fadeCtrl.instance.play(fadeCtrl.Status.WFADE_IN, 1u, 8u);
				soundCtrl.instance.PlaySE(14);
				soundCtrl.instance.PlayBGM(12);
				routine_3d[1].rotate[0] = 0;
				routine.timer0 = 60;
				routine.r.no_3++;
			}
			break;
		case 10:
			if (routine.timer0-- == 0)
			{
				routine.Rno_4 = 2;
				routine.r.no_3++;
			}
			break;
		case 11:
			if (!episodeReleaseCtrl.instance.is_scroll)
			{
				routine.r.no_2++;
				routine.r.no_3 = 0;
			}
			break;
		}
		if (!episodeReleaseCtrl.instance.is_scroll)
		{
			global_work_.Mess_move_flag = 1;
			activeMessageWork.message_trans_flag = 1;
			activeMessageWork.now_no = ushort.MaxValue;
		}
	}

	private static void Sc4_01(SubWindow sub_window)
	{
		GlobalWork global_work_ = GSStatic.global_work_;
		MessageWork activeMessageWork = MessageSystem.GetActiveMessageWork();
		Routine routine = sub_window.routine_[sub_window.stack_];
		Routine3d[] routine_3d = routine.routine_3d;
		switch (routine.r.no_3)
		{
		case 0:
			if (activeMessageWork.status == MessageSystem.Status.LOOP)
			{
				return;
			}
			if (padCtrl.instance.GetKeyDown(KeyType.Start) || padCtrl.instance.GetKeyDown(KeyType.Select) || padCtrl.instance.GetKeyDown(KeyType.A) || padCtrl.instance.GetKeyDown(KeyType.B))
			{
				global_work_.Mess_move_flag = 0;
				activeMessageWork.message_trans_flag = 0;
				routine_3d[0].disp_off = 1;
				routine_3d[1].disp_off = 1;
				routine_3d[5].disp_off = 1;
				routine.timer0 = 32;
				routine.r.no_3++;
			}
			break;
		case 1:
			if (routine.timer0 == 0)
			{
				routine.r.no_3++;
			}
			if (routine.timer0 != 0)
			{
				routine.timer0--;
			}
			break;
		case 2:
			routine.r.no_3++;
			break;
		case 3:
			global_work_.r.Set(11, 0, 2, 1);
			routine.r.no_3++;
			break;
		case 4:
			if (sub_window.req_ == SubWindow.Req.SAVE)
			{
				sub_window.routine_[sub_window.stack_].r.Set(15, 0, 0, 0);
			}
			break;
		}
		if (routine.r.no_3 >= 1)
		{
			routine_3d[2].rotate[0] += 10;
			if (routine_3d[2].rotate[0] >= 90)
			{
				routine_3d[2].disp_off = 1;
			}
		}
	}
}
