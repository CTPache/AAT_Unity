using System;

public static class GSMain_TanteiPart
{
	public enum Rno1
	{
		INIT = 0,
		MAIN = 1,
		EXIT = 2,
		BG_WAIT = 3,
		MW_WAIT = 4,
		ROOM_INIT = 5,
		INSPECT = 6,
		MOVE = 7,
		TALK = 8,
		SHOW = 9,
		SUBW_WAIT = 10,
		PSYLOCK = 11,
		TANCHIKI = 12
	}

	public enum PsylockRno
	{
		INIT = 0,
		APPEAR_WAIT = 1,
		INPUT_INIT = 2,
		INPUT_WAIT = 3,
		BACK_FROM_STATUS_CORRECT = 4,
		BACK_FROM_STATUS_WRONG = 5,
		BACK_FROM_STATUS_CANCEL = 6,
		UNLOCK_ALL = 7,
		CANCEL = 8,
		SHOW_APPEAR_ONLY = 9,
		RESET_STATIC = 10,
		DIE = 11
	}

	private delegate void TanteiPartProc(GlobalWork global_work, TanteiWork tantei_work);

	private static TanteiPartProc[] proc_table;

	static GSMain_TanteiPart()
	{
		proc_table = new TanteiPartProc[13]
		{
			Init, Main, Exit, BgScrollWait, MwScrollWait, RoomInit, Inspect, Move, Talk, Show,
			SubWindowWait, Psylock, Tanchiki
		};
	}

	public static void room_seq_chg(uint room, uint seq)
	{
		GSStatic.global_work_.roomseq[room] = (byte)seq;
	}

	public static void Proc(GlobalWork global_work)
	{
		if (global_work.r.no_1 != 5)
		{
			GSScenario.GetSceLoopProc()(global_work);
		}
		proc_table[global_work.r.no_1](global_work, GSStatic.tantei_work_);
	}

	private static void Init(GlobalWork global_work, TanteiWork tantei_work)
	{
		GSStatic.saiban_work_.Clear();
		GSStatic.tantei_work_.Clear();
		if (global_work.title == TitleId.GS3)
		{
			GSScenario.GetSceInitProc()(global_work, true);
		}
		if (global_work.language == "JAPAN")
		{
		}
		GSMain_Status.Status_init(global_work, GSStatic.status_work_);
		if (GSStatic.global_work_.title == TitleId.GS1)
		{
			if ((uint)global_work.scenario < 17u)
			{
				Array.Clear(global_work.sce_flag, 0, global_work.sce_flag.Length);
				if (global_work.scenario > 1)
				{
					GSFlag.Set(0u, scenario.SCE1_CHIHIRO_DISCOVER, 1u);
				}
			}
			else
			{
				switch (global_work.scenario)
				{
				case 17:
				{
					for (uint num3 = scenario.SCE40_FLAG_ST_G; num3 < scenario.SCE40_FLAG_ST_G_MAX; num3++)
					{
						GSFlag.Set(0u, num3, 0u);
					}
					GSFlag.Set(0u, scenario.SCE4_FLAG_DEBUG_4_0A_END, 0u);
					GSFlag.Set(0u, scenario.SCE4_FLAG_STATUS_3D_ENABLE, 0u);
					GSFlag.Set(0u, scenario.SCE4_FLAG_ST_G_AKANE_JOIN, 0u);
					break;
				}
				case 22:
				{
					for (uint num2 = scenario.SCE42_FLAG_ST_G; num2 < scenario.SCE42_FLAG_ST_G_MAX; num2++)
					{
						GSFlag.Set(0u, num2, 0u);
					}
					GSFlag.Set(0u, scenario.SCE42_FLAG_BLOODSTAIN01, 0u);
					GSFlag.Set(0u, scenario.SCE42_FLAG_BLOODSTAIN02, 0u);
					GSFlag.Set(0u, scenario.SCE42_FLAG_BLOODSTAIN03, 0u);
					GSFlag.Set(0u, scenario.SCE42_FLAG_BLOODSTAIN04, 0u);
					GSFlag.Set(0u, scenario.SCE42_FLAG_BLOODSTAIN05, 0u);
					GSFlag.Set(0u, scenario.SCE42_FLAG_BLOODSTAIN06, 0u);
					GSFlag.Set(0u, scenario.SCE42_FLAG_BLOODSTAIN08, 0u);
					GSFlag.Set(0u, scenario.SCE4_FLAG_ST_G_FINGER_MES0, 0u);
					GSFlag.Set(0u, scenario.SCE4_FLAG_ST_G_FINGER_MES1, 0u);
					GSFlag.Set(0u, scenario.SCE4_FLAG_ST_G_FINGER_MES2, 0u);
					GSFlag.Set(0u, scenario.SCE4_FLAG_JAR_PUZZZLE, 0u);
					GSFlag.Set(0u, scenario.SCE4_FLAG_ST_G_AKANE_JOIN, 1u);
					GSFlag.Set(0u, scenario.SCE42_FLAG_BLOODSTAIN02_COMPLETE, 0u);
					GSFlag.Set(0u, scenario.SCE42_FLAG_BLOODSTAIN06_COMPLETE, 0u);
					break;
				}
				case 28:
				{
					for (uint num = scenario.SCE44_FLAG_ST_G; num < scenario.SCE44_FLAG_ST_G_MAX; num++)
					{
						GSFlag.Set(0u, num, 0u);
					}
					if (!GSFlag.Check(0u, scenario.SCE42_FLAG_BLOODSTAIN02_COMPLETE))
					{
						GSFlag.Set(0u, scenario.SCE42_FLAG_BLOODSTAIN02, 0u);
						GSFlag.Set(0u, scenario.SCE42_FLAG_BLOODSTAIN02_SET, 0u);
					}
					if (!GSFlag.Check(0u, scenario.SCE42_FLAG_BLOODSTAIN06_COMPLETE))
					{
						GSFlag.Set(0u, scenario.SCE42_FLAG_BLOODSTAIN06, 0u);
					}
					GSFlag.Set(0u, scenario.SCE42_FLAG_BLOODSTAIN07, 0u);
					GSFlag.Set(0u, scenario.SCE4_FLAG_ST_G_AKANE_JOIN, 1u);
					break;
				}
				}
				MessageSystem.ClearSceLocalFlg();
			}
			byte b = global_work.scenario;
			if (b == 1 || b == 5 || b == 11 || b == 17)
			{
				advCtrl.instance.sub_window_.SetReq(SubWindow.Req.BLANK);
			}
			else
			{
				advCtrl.instance.sub_window_.SetReq(SubWindow.Req.TANTEI);
			}
		}
		else if (GSStatic.global_work_.title == TitleId.GS2)
		{
			Array.Clear(global_work.sce_flag, 0, global_work.sce_flag.Length);
			byte b2 = global_work.scenario;
			if (b2 == 2 || b2 == 8 || b2 == 14)
			{
				advCtrl.instance.sub_window_.SetReq(SubWindow.Req.BLANK);
			}
			else
			{
				advCtrl.instance.sub_window_.SetReq(SubWindow.Req.TANTEI);
			}
		}
		else if (GSStatic.global_work_.title == TitleId.GS3)
		{
			Array.Clear(global_work.sce_flag, 0, global_work.sce_flag.Length);
			switch (global_work.scenario)
			{
			case 2:
			case 7:
			case 12:
			case 14:
				advCtrl.instance.sub_window_.SetReq(SubWindow.Req.BLANK);
				break;
			default:
				advCtrl.instance.sub_window_.SetReq(SubWindow.Req.TANTEI);
				break;
			}
		}
		global_work.status_flag = 0u;
		bgCtrl.instance.Bg256_SP_Flag = 0;
		global_work.Mess_move_flag = 1;
		GSStatic.message_work_.message_trans_flag = 1;
		GSStatic.message_work_2_.message_trans_flag = 0;
		advCtrl.instance.sub_window_.tantei_tukituke_ = 0;
		GSStatic.message_work_.now_no = ushort.MaxValue;
		advCtrl.instance.message_system_.SetMessage(128u);
		MessageSystem.Mess_window_set(3u);
		if (GSStatic.global_work_.title == TitleId.GS3)
		{
			if (global_work.scenario != 12 && global_work.scenario != 14)
			{
				MessageSystem.Mess_window_set(3u);
			}
			if (global_work.scenario == 14)
			{
				MessageSystem.Mess_window_set(7u);
				messageBoardCtrl.instance.board(false, false);
			}
			if (global_work.scenario == 7)
			{
				messageBoardCtrl.instance.board(false, false);
			}
		}
		MessageSystem.Set_scenario_enable();
		GSScenario.sce_init sceRoomInitData = GSScenario.GetSceRoomInitData();
		sceRoomInitData(GSStatic.global_work_, true);
		fadeCtrl.instance.play(1u, 1u, 1u);
		if (GSStatic.global_work_.title == TitleId.GS1)
		{
			global_work.rest = 5;
			if (global_work.scenario == 11 || global_work.scenario == 17)
			{
				messageBoardCtrl.instance.board(false, false);
			}
		}
		else if (GSStatic.global_work_.title == TitleId.GS2 && global_work.scenario == 8)
		{
			messageBoardCtrl.instance.board(false, false);
		}
		global_work.r.Set(5, 1, 0, 0);
		GS3DS_tantei_init();
	}

	private static void Main(GlobalWork global_work, TanteiWork tantei_work)
	{
		if (fadeCtrl.instance.status != 0 || advCtrl.instance.sub_window_.IsBusy() || tanteiMenu.instance.select_animation_playing || (advCtrl.instance.sub_window_.GetCurrentRoutine().r.no_0 == 5 && advCtrl.instance.sub_window_.GetCurrentRoutine().r.no_1 != 2 && !tanteiMenu.instance.is_play) || selectPlateCtrl.instance.select_animation_playing || (MessageSystem.GetActiveMessageWork().code == 8 && MessageSystem.GetActiveMessageWork().code == 9 && selectPlateCtrl.instance.is_end) || bgCtrl.instance.is_slider)
		{
			return;
		}
		if (padCtrl.instance.GetKeyDown(KeyType.Start) && (global_work.status_flag & 0x10) == 0 && (GSStatic.message_work_.status & (MessageSystem.Status.RT_WAIT | MessageSystem.Status.SELECT)) != 0 && bgCtrl.instance.bg_no_now != 254)
		{
			global_work.r_bk.CopyFrom(ref global_work.r);
			soundCtrl.instance.PlaySE(49);
			global_work.r.Set(17, 0, 0, 0);
		}
		else if (padCtrl.instance.GetKeyDown(KeyType.R) && (global_work.status_flag & 0x10) == 0 && !advCtrl.instance.sub_window_.IsBusy() && (GSStatic.message_work_.status & (MessageSystem.Status.RT_WAIT | MessageSystem.Status.SELECT)) != 0)
		{
			tanteiMenu.instance.close();
			global_work.r_bk.CopyFrom(ref global_work.r);
			global_work.r.Set(8, 0, 0, 0);
		}
		else
		{
			if (GSStatic.message_work_.mess_win_rno != 1 || (global_work.Mess_move_flag | GSStatic.message_work_.message_trans_flag) != 0)
			{
				return;
			}
			if (padCtrl.instance.GetKeyDown(KeyType.Start) && (global_work.status_flag & 0x10) == 0 && bgCtrl.instance.bg_no_now != 254)
			{
				global_work.r_bk.CopyFrom(ref global_work.r);
				soundCtrl.instance.PlaySE(49);
				global_work.r.Set(17, 0, 0, 0);
			}
			else if (padCtrl.instance.GetKeyDown(KeyType.R))
			{
				if ((global_work.status_flag & 0x10) == 0)
				{
					tanteiMenu.instance.close();
					global_work.r_bk.CopyFrom(ref global_work.r);
					global_work.r.Set(8, 0, 0, 0);
				}
			}
			else
			{
				uint bGType = bgData.instance.GetBGType(bgCtrl.instance.bg_no);
				if ((bGType == 1 || bGType == 2) && padCtrl.instance.GetKeyDown(KeyType.L) && IsBGSlide(bgCtrl.instance.bg_no))
				{
					soundCtrl.instance.PlaySE(43);
					coroutineCtrl.instance.Play(bgCtrl.instance.Slider());
				}
			}
		}
	}

	private static void Exit(GlobalWork global_work, TanteiWork tantei_work)
	{
		global_work.r.Set(11, 0, 0, 1);
		GS3DS_tantei_exit();
	}

	private static void BgScrollWait(GlobalWork global_work, TanteiWork tantei_work)
	{
		if (global_work.r.no_2 == 0)
		{
			if (!bgCtrl.instance.is_slider)
			{
				global_work.r.no_2++;
			}
		}
		else
		{
			global_work.r.CopyFrom(ref global_work.r_bk);
		}
	}

	private static void MwScrollWait(GlobalWork global_work, TanteiWork tantei_work)
	{
	}

	private static void RoomInit(GlobalWork global_work, TanteiWork tantei_work)
	{
		if (GSStatic.message_work_.mess_win_rno == 1 && fadeCtrl.instance.status == fadeCtrl.Status.NO_FADE)
		{
			uint data = GSStatic.global_work_.Map_data[global_work.Room][0];
			uint num = get_move_flag_id(data);
			GSStatic.global_work_.sw_move_flag[num] = 1;
			tanteiMenu.instance.cursor_no = 0;
			if (global_work.r.no_2 == 0)
			{
				global_work.r.no_2 = 1;
				return;
			}
			int num2 = (int)GSStatic.global_work_.Map_data[GSStatic.global_work_.Room][0];
			bgCtrl.instance.SetSprite(num2);
			GSDemo.CheckBGChange((uint)num2, 0u);
			AnimationSystem.Instance.StopCharacters();
			GSStatic.tantei_work_.person_flag = 0;
			GSScenario.sce_init sceRoomInitData = GSScenario.GetSceRoomInitData();
			sceRoomInitData(GSStatic.global_work_, true);
			fadeCtrl.instance.play(1u, 1u, 1u);
			global_work.r.Set(5, 1, 0, 0);
		}
	}

	private static void Inspect(GlobalWork global_work, TanteiWork tantei_work)
	{
		if (fadeCtrl.instance.status != 0 || advCtrl.instance.sub_window_.IsBusy() || bgCtrl.instance.is_slider || inspectCtrl.instance.select_animation_playing || (!inspectCtrl.instance.is_play && advCtrl.instance.sub_window_.GetCurrentRoutine().r.no_0 == 6 && advCtrl.instance.sub_window_.GetCurrentRoutine().r.no_1 == 2) || selectPlateCtrl.instance.select_animation_playing)
		{
			return;
		}
		if (padCtrl.instance.GetKeyDown(KeyType.Start))
		{
			if ((global_work.status_flag & 0x10) == 0 && (GSStatic.message_work_.mess_win_rno == 1 || (GSStatic.message_work_.status & (MessageSystem.Status.RT_WAIT | MessageSystem.Status.SELECT)) != 0) && bgCtrl.instance.bg_no_now != 254)
			{
				global_work.r_bk.CopyFrom(ref global_work.r);
				soundCtrl.instance.PlaySE(49);
				global_work.r.Set(17, 0, 0, 0);
			}
		}
		else if (padCtrl.instance.GetKeyDown(KeyType.R) && (global_work.status_flag & 0x10) == 0 && (GSStatic.message_work_.mess_win_rno == 1 || (GSStatic.message_work_.status & (MessageSystem.Status.RT_WAIT | MessageSystem.Status.SELECT)) != 0))
		{
			inspectCtrl.instance.stop();
			global_work.r_bk.CopyFrom(ref global_work.r);
			global_work.r.Set(8, 0, 0, 0);
		}
	}

	private static void Move(GlobalWork global_work, TanteiWork tantei_work)
	{
	}

	private static void Talk(GlobalWork global_work, TanteiWork tantei_work)
	{
		byte no_ = global_work.r.no_2;
		if (no_ != 3 || fadeCtrl.instance.status != 0 || advCtrl.instance.sub_window_.IsBusy() || selectPlateCtrl.instance.select_animation_playing || (selectPlateCtrl.instance.is_end && advCtrl.instance.sub_window_.GetCurrentRoutine().r.no_0 == 8 && advCtrl.instance.sub_window_.GetCurrentRoutine().r.no_1 == 2))
		{
			return;
		}
		if (padCtrl.instance.GetKeyDown(KeyType.Start))
		{
			if ((global_work.status_flag & 0x10) == 0 && (GSStatic.message_work_.status & (MessageSystem.Status.RT_WAIT | MessageSystem.Status.SELECT)) != 0 && bgCtrl.instance.bg_no_now != 254)
			{
				global_work.r_bk.CopyFrom(ref global_work.r);
				soundCtrl.instance.PlaySE(49);
				global_work.r.Set(17, 0, 0, 0);
			}
		}
		else if (padCtrl.instance.GetKeyDown(KeyType.R) && (global_work.status_flag & 0x10) == 0 && (GSStatic.message_work_.status & (MessageSystem.Status.RT_WAIT | MessageSystem.Status.SELECT)) != 0)
		{
			global_work.r_bk.CopyFrom(ref global_work.r);
			global_work.r.Set(8, 0, 0, 0);
		}
	}

	private static void Show(GlobalWork global_work, TanteiWork tantei_work)
	{
		switch (global_work.r.no_2)
		{
		case 0:
			global_work.r_bk.CopyFrom(ref global_work.r);
			global_work.r.Set(8, 0, 0, 2);
			global_work.r.no_2++;
			break;
		case 1:
			global_work.r.Set(5, 1, 0, 0);
			break;
		case 3:
			if (global_work.psy_menu_active_flag != 0)
			{
				global_work.r.Set(5, 11, 8, 0);
				break;
			}
			global_work.r.Set(5, 1, 0, 0);
			advCtrl.instance.sub_window_.SetReq(SubWindow.Req.ATTACK_EXIT);
			break;
		}
	}

	private static void SubWindowWait(GlobalWork global_work, TanteiWork tantei_work)
	{
	}

	private static void Psylock(GlobalWork global_work, TanteiWork tantei_work)
	{
		SubWindow sub_window_ = advCtrl.instance.sub_window_;
		MessageWork activeMessageWork = MessageSystem.GetActiveMessageWork();
		PsylockData psylockData = null;
		if (advCtrl.instance.sub_window_.IsBusy())
		{
			return;
		}
		if (global_work.r.no_2 != 9)
		{
			int num = global_work.psy_no;
			psylockData = global_work.psylock[num];
		}
		switch ((PsylockRno)global_work.r.no_2)
		{
		case PsylockRno.INIT:
			global_work.status_flag |= 16u;
			GSStatic.global_work_.gauge_dmg_cnt = 0;
			psylockData.level = psylockData.size;
			GSPsylock.PsylockDisp_init(psylockData.size);
			GSPsylock.PsylockDisp_appear();
			global_work.psy_menu_active_flag = 0;
			global_work.r.no_2++;
			goto case PsylockRno.APPEAR_WAIT;
		case PsylockRno.APPEAR_WAIT:
			GSPsylock.PsylockDisp_move();
			if (GSPsylock.PsylockDisp_is_wait())
			{
				global_work.r.no_2++;
			}
			break;
		case PsylockRno.INPUT_INIT:
			advCtrl.instance.message_system_.SetMessage(psylockData.start_message);
			global_work.Mess_move_flag = 1;
			global_work.r.no_2 = 3;
			global_work.status_flag &= 4294967279u;
			goto case PsylockRno.INPUT_WAIT;
		case PsylockRno.INPUT_WAIT:
			if ((global_work.psy_menu_active_flag & 2u) != 0 && padCtrl.instance.GetKeyDown(KeyType.B))
			{
				soundCtrl.instance.PlaySE(44);
				global_work.r.no_2 = 8;
				global_work.r.no_3 = 0;
				break;
			}
			if (global_work.psy_menu_active_flag == 0 && padCtrl.instance.GetKeyDown(KeyType.R) && (global_work.status_flag & 0x10) == 0 && (GSStatic.message_work_.status & (MessageSystem.Status.RT_WAIT | MessageSystem.Status.SELECT)) != 0)
			{
				global_work.r_bk.CopyFrom(ref global_work.r);
				global_work.r.Set(8, 0, 0, 4);
			}
			if (global_work.gauge_hp <= 0 && global_work.gauge_hp_disp <= 0 && !lifeGaugeCtrl.instance.is_lifegauge_moving())
			{
				global_work.r.no_2 = 11;
			}
			break;
		case PsylockRno.BACK_FROM_STATUS_CORRECT:
		{
			byte no_2 = global_work.r.no_3;
			if (no_2 != 0)
			{
				switch (no_2)
				{
				default:
					return;
				case 1:
					break;
				case 2:
					if (psylockData.level > 5)
					{
						psylockData.level = 5;
					}
					psylockData.level--;
					if (activeMessageWork.op_para == 69)
					{
						psylockData.level = 0;
					}
					if (psylockData.level <= 0 && global_work.psy_unlock_not_unlock_message == 0 && (activeMessageWork.status2 & MessageSystem.Status2.PSY_STOP_BREAK) == 0)
					{
						global_work.r.no_2 = 7;
						global_work.r.no_3 = 0;
						return;
					}
					global_work.Mess_move_flag = 1;
					if (global_work.psy_unlock_not_unlock_message == 0)
					{
						if (GSStatic.global_work_.title == TitleId.GS2)
						{
							MessageSystem.Mess_window_set(3u);
						}
						else
						{
							MessageSystem.Mess_window_set(3u);
						}
					}
					global_work.r.no_2 = 3;
					global_work.r.no_3 = 0;
					return;
				}
			}
			else
			{
				global_work.Mess_move_flag = 0;
				activeMessageWork.message_trans_flag = 0;
				messageBoardCtrl.instance.board(false, false);
				GSPsylock.PsylockDisp_unlock();
				global_work.r.no_3++;
			}
			GSPsylock.PsylockDisp_move();
			if (GSPsylock.PsylockDisp_is_wait())
			{
				global_work.r.no_3++;
			}
			break;
		}
		case PsylockRno.BACK_FROM_STATUS_WRONG:
			advCtrl.instance.message_system_.SetMessage(psylockData.wrong_message);
			MessageSystem.Mess_window_set(3u);
			global_work.r.no_2 = 3;
			break;
		case PsylockRno.BACK_FROM_STATUS_CANCEL:
			global_work.r.no_2 = 3;
			break;
		case PsylockRno.UNLOCK_ALL:
			if (global_work.language != "JAPAN")
			{
			}
			switch (global_work.r.no_3)
			{
			case 0:
				global_work.Mess_move_flag = 0;
				soundCtrl.instance.FadeOutBGM(30);
				GSPsylock.PsylockDisp_disappear();
				global_work.r.no_3++;
				goto case 1;
			case 1:
				GSPsylock.PsylockDisp_move();
				if (!GSPsylock.PsylockDisp_is_wait())
				{
					break;
				}
				GSPsylock.PsylockDisp_clear_all();
				if (global_work.gauge_hp < 80)
				{
					lifeGaugeCtrl.instance.is_recover_flag = true;
					lifeGaugeCtrl.instance.lifegauge_set_move(1);
					global_work.gauge_dmg_cnt = -40;
					if (GSStatic.global_work_.title == TitleId.GS2)
					{
						lifeGaugeCtrl.instance.lifegauge_set_move(3);
					}
					else
					{
						lifeGaugeCtrl.instance.lifegauge_set_move(4);
					}
				}
				global_work.r.no_3++;
				break;
			case 2:
				GSPsylock.PsylockDisp_move();
				if (GSPsylock.PsylockDisp_is_wait())
				{
					GSPsylock.PsylockDisp_unlock_message();
					global_work.r.no_3++;
				}
				break;
			case 3:
				GSPsylock.PsylockDisp_move();
				if (GSPsylock.PsylockDisp_is_wait() && !lifeGaugeCtrl.instance.is_lifegauge_moving())
				{
					lifeGaugeCtrl.instance.lifegauge_set_move(2);
					global_work.r.no_3++;
				}
				break;
			case 4:
				global_work.Mess_move_flag = 1;
				psylockData.status = 0u;
				global_work.r.Set(5, 1, 0, 0);
				break;
			}
			break;
		case PsylockRno.CANCEL:
			switch (global_work.r.no_3)
			{
			case 0:
				advCtrl.instance.message_system_.SetMessage(psylockData.cancel_message);
				if (GSStatic.message_work_.mess_win_rno == 1 || GSStatic.message_work_.mess_win_rno == 4)
				{
					MessageSystem.Mess_window_set(3u);
				}
				global_work.r.no_3++;
				break;
			case 1:
				if ((GSStatic.message_work_.status & MessageSystem.Status.LOOP) != 0)
				{
					global_work.r.no_3++;
					fadeCtrl.instance.play(2u, 4u, 1u);
				}
				break;
			case 2:
				if (fadeCtrl.instance.status == fadeCtrl.Status.NO_FADE)
				{
					global_work.r.no_3++;
				}
				break;
			case 3:
				GSPsylock.PsylockDisp_clear_all();
				global_work.r.no_3++;
				goto case 4;
			case 4:
				GSPsylock.PsylockDisp_move();
				if (GSPsylock.PsylockDisp_is_wait())
				{
					global_work.r.no_3++;
				}
				break;
			case 5:
				global_work.r.Set(5, 1, 0, 0);
				if (psylockData.cancel_bgm != ushort.MaxValue)
				{
					soundCtrl.instance.PlayBGM(psylockData.cancel_bgm);
				}
				fadeCtrl.instance.play(1u, 1u, 1u);
				break;
			}
			break;
		case PsylockRno.SHOW_APPEAR_ONLY:
		{
			byte no_ = global_work.r.no_3;
			if (no_ != 0)
			{
				switch (no_)
				{
				default:
					return;
				case 1:
					break;
				case 2:
					global_work.Mess_move_flag = 1;
					MessageSystem.Mess_window_set(3u);
					global_work.status_flag &= 4294967279u;
					global_work.r.CopyFrom(ref global_work.r_bk);
					return;
				}
			}
			else
			{
				global_work.status_flag |= 16u;
				global_work.Mess_move_flag = 0;
				MessageSystem.Mess_window_set(8u);
				GSPsylock.PsylockDisp_init(global_work.psy_no);
				GSPsylock.PsylockDisp_appear();
				global_work.r.no_3++;
			}
			GSPsylock.PsylockDisp_move();
			if (GSPsylock.PsylockDisp_is_wait())
			{
				global_work.r.no_3++;
			}
			break;
		}
		case PsylockRno.RESET_STATIC:
			break;
		case PsylockRno.DIE:
			switch (global_work.r.no_3)
			{
			case 0:
				advCtrl.instance.message_system_.SetMessage(psylockData.die_message);
				if (GSStatic.message_work_.mess_win_rno == 1 || GSStatic.message_work_.mess_win_rno == 4)
				{
					MessageSystem.Mess_window_set(3u);
				}
				global_work.r.no_3++;
				break;
			case 1:
				if ((GSStatic.message_work_.status & MessageSystem.Status.LOOP) != 0)
				{
					global_work.r.no_3++;
					fadeCtrl.instance.play(2u, 1u, 1u);
				}
				break;
			case 2:
				if (fadeCtrl.instance.status == fadeCtrl.Status.NO_FADE)
				{
					global_work.r.no_3++;
				}
				break;
			case 3:
				GSPsylock.PsylockDisp_clear_all();
				global_work.r.no_3++;
				goto case 4;
			case 4:
				GSPsylock.PsylockDisp_move();
				if (GSPsylock.PsylockDisp_is_wait())
				{
					global_work.r.no_3++;
				}
				break;
			case 5:
				global_work.gauge_hp = (global_work.gauge_hp_disp = 1);
				global_work.r.Set(5, 1, 0, 0);
				debugLogger.instance.Log("SubWindowStack", "--stack_");
				sub_window_.stack_--;
				sub_window_.tantei_tukituke_ = 0;
				if (psylockData.cancel_bgm != ushort.MaxValue)
				{
					soundCtrl.instance.PlayBGM(psylockData.cancel_bgm);
				}
				fadeCtrl.instance.play(1u, 1u, 1u);
				break;
			}
			break;
		}
	}

	private static void Tanchiki(GlobalWork global_work, TanteiWork tantei_work)
	{
		SubWindow sub_window_ = advCtrl.instance.sub_window_;
		Routine currentRoutine = sub_window_.GetCurrentRoutine();
		if (fadeCtrl.instance.status != 0 || sub_window_.IsBusy() || selectPlateCtrl.instance.select_animation_playing)
		{
			return;
		}
		if (padCtrl.instance.GetKeyDown(KeyType.Start))
		{
			if ((global_work.status_flag & 0x10) == 0 && currentRoutine.r.no_0 == 10 && currentRoutine.r.no_1 == 2 && (GSStatic.message_work_.mess_win_rno == 1 || (GSStatic.message_work_.status & (MessageSystem.Status.RT_WAIT | MessageSystem.Status.SELECT)) != 0))
			{
				global_work.r_bk.CopyFrom(ref global_work.r);
				soundCtrl.instance.PlaySE(49);
				global_work.r.Set(17, 0, 0, 0);
			}
		}
		else if (padCtrl.instance.GetKeyDown(KeyType.R) && (global_work.status_flag & 0x10) == 0 && currentRoutine.r.no_0 == 10 && currentRoutine.r.no_1 == 2 && (GSStatic.message_work_.mess_win_rno == 1 || (GSStatic.message_work_.status & (MessageSystem.Status.RT_WAIT | MessageSystem.Status.SELECT)) != 0))
		{
			global_work.r_bk.CopyFrom(ref global_work.r);
			global_work.r.Set(8, 0, 0, 0);
		}
	}

	public static void tantei_menu_recov(ushort knd, ushort curpos)
	{
		for (uint num = 0u; num < 8; num++)
		{
		}
		if (knd != 0 && knd == 1)
		{
			GSStatic.global_work_.r.no_1 = 8;
			GSStatic.global_work_.r.no_2 = 1;
			GSStatic.tantei_work_.def_place = (byte)curpos;
			if (GSStatic.global_work_.psylock[GSStatic.global_work_.psy_no].unlock_bgm != ushort.MaxValue)
			{
				soundCtrl.instance.PlayBGM(GSStatic.global_work_.psylock[GSStatic.global_work_.psy_no].unlock_bgm);
			}
			GSStatic.global_work_.psy_unlock_success = 1;
		}
	}

	public static void GS3DS_tantei_init()
	{
	}

	public static void GS3DS_tantei_exit()
	{
		GSMapIcon.instance.Terminate();
	}

	public static bool IsBGSlide(int in_bg_no)
	{
		if (bgData.instance.GetBGType(in_bg_no) != 1 && bgData.instance.GetBGType(in_bg_no) != 2)
		{
			return false;
		}
		switch (GSStatic.global_work_.title)
		{
		case TitleId.GS1:
			if ((long)in_bg_no == 115 && GSFlag.Check(0u, scenario.SCE4_PARKING_BG_SCROLL_OFF) && GSStatic.global_work_.scenario == 18)
			{
				return false;
			}
			break;
		}
		return true;
	}

	private static uint get_move_flag_id(uint data)
	{
		switch (GSStatic.global_work_.title)
		{
		case TitleId.GS1:
			switch (data)
			{
			case 0u:
				return 0u;
			case 9u:
				return 1u;
			case 30u:
				return 2u;
			case 39u:
				return 3u;
			case 31u:
				return 4u;
			case 32u:
				return 5u;
			case 1u:
				return 6u;
			case 25u:
				return 7u;
			case 12u:
				return 8u;
			case 24u:
				return 9u;
			case 11u:
				return 10u;
			case 26u:
				return 11u;
			case 44u:
				return 12u;
			case 19u:
				return 13u;
			case 79u:
				return 14u;
			case 80u:
				return 15u;
			case 70u:
				return 16u;
			case 71u:
				return 17u;
			case 73u:
				return 18u;
			case 77u:
				return 19u;
			case 78u:
				return 20u;
			case 115u:
				return 21u;
			case 114u:
				return 22u;
			case 113u:
				return 23u;
			case 116u:
				return 24u;
			case 118u:
				return 25u;
			case 117u:
				return 26u;
			case 40u:
				return 3u;
			case 72u:
				return 17u;
			default:
				return 0u;
			}
		case TitleId.GS2:
			switch (data)
			{
			case 18u:
				return 0u;
			case 17u:
				return 1u;
			case 34u:
				return 2u;
			case 28u:
				return 3u;
			case 29u:
				return 4u;
			case 30u:
				return 5u;
			case 31u:
				return 6u;
			case 33u:
				return 7u;
			case 62u:
				return 8u;
			case 59u:
				return 9u;
			case 64u:
				return 10u;
			case 60u:
				return 11u;
			case 61u:
				return 12u;
			case 65u:
				return 13u;
			case 66u:
				return 14u;
			case 19u:
				return 15u;
			case 90u:
				return 16u;
			case 91u:
				return 17u;
			case 89u:
				return 18u;
			case 88u:
				return 19u;
			case 93u:
				return 20u;
			case 94u:
				return 22u;
			case 96u:
				return 23u;
			case 95u:
				return 24u;
			case 32u:
				return 25u;
			default:
				return 0u;
			}
		case TitleId.GS3:
			switch (data)
			{
			case 19u:
				return 0u;
			case 18u:
				return 1u;
			case 24u:
				return 2u;
			case 22u:
				return 3u;
			case 21u:
				return 4u;
			case 26u:
				return 5u;
			case 28u:
				return 6u;
			case 25u:
				return 7u;
			case 29u:
				return 8u;
			case 30u:
				return 8u;
			case 31u:
				return 9u;
			case 33u:
				return 10u;
			case 34u:
				return 11u;
			case 32u:
				return 12u;
			case 20u:
				return 13u;
			case 35u:
				return 14u;
			case 38u:
				return 15u;
			case 39u:
				return 16u;
			case 40u:
				return 17u;
			case 45u:
				return 18u;
			case 49u:
				return 19u;
			case 54u:
				return 20u;
			case 44u:
				return 21u;
			case 23u:
				return 22u;
			case 27u:
				return 23u;
			case 36u:
				return 24u;
			case 37u:
				return 25u;
			case 43u:
				return 26u;
			case 41u:
				return 27u;
			case 42u:
				return 28u;
			case 46u:
				return 29u;
			case 52u:
				return 30u;
			case 50u:
				return 31u;
			case 51u:
				return 32u;
			case 53u:
				return 33u;
			default:
				return 0u;
			}
		default:
			return 0u;
		}
	}
}
