public class GSMain_Questioning
{
	private delegate void QuestioningProc(GlobalWork global_work);

	private static QuestioningProc[] proc_table;

	private static bool first_question_;

	private static ushort old_tukkomi_no_;

	public static bool first_question
	{
		set
		{
			first_question_ = value;
		}
	}

	static GSMain_Questioning()
	{
		proc_table = new QuestioningProc[6] { Init, Main, Exit, ObjSet, Yusaburi, Tukitukeru };
	}

	public static void Proc(GlobalWork global_work)
	{
		proc_table[global_work.r.no_1](global_work);
	}

	private static void Init(GlobalWork global_work)
	{
		global_work.bk_start_mess = GSStatic.message_work_.now_no;
		first_question_ = true;
		old_tukkomi_no_ = 0;
		lifeGaugeCtrl.instance.ActionLifeGauge(lifeGaugeCtrl.Gauge_State.UPDATE_REST);
		global_work.r.no_1 = 3;
		messageBoardCtrl.instance.SetArrowPosition(true, 0);
		messageBoardCtrl.instance.SetArrowPosition(true, 1);
	}

	private static void Main(GlobalWork global_work)
	{
		MessageWork activeMessageWork = MessageSystem.GetActiveMessageWork();
		SubWindow sub_window_ = advCtrl.instance.sub_window_;
		Routine currentRoutine = sub_window_.GetCurrentRoutine();
		if ((activeMessageWork.status & MessageSystem.Status.POINT_TO_START) != 0 || (activeMessageWork.status & MessageSystem.Status.POINT_TO) != 0 || GSStatic.message_work_.questioning_message_wait > 0)
		{
			return;
		}
		if (fadeCtrl.instance.status == fadeCtrl.Status.NO_FADE && (activeMessageWork.status & (MessageSystem.Status.RT_WAIT | MessageSystem.Status.SELECT | MessageSystem.Status.LOOP)) != 0 && ((activeMessageWork.status & MessageSystem.Status.LOOP) == 0 || (activeMessageWork.work & 1) == 0) && !sub_window_.IsBusy())
		{
			if (selectPlateCtrl.instance.select_animation_playing || fadeCtrl.instance.status != 0)
			{
				return;
			}
			if (padCtrl.instance.GetKeyDown(KeyType.Start))
			{
				if ((global_work.status_flag & 0x10) == 0 && (GSStatic.message_work_.status & (MessageSystem.Status.RT_WAIT | MessageSystem.Status.SELECT | MessageSystem.Status.LOOP)) != 0 && GSMain_SaibanPart.CheckSaveKey())
				{
					global_work.r_bk.CopyFrom(ref global_work.r);
					soundCtrl.instance.PlaySE(49);
					global_work.r.Set(17, 0, 0, 0);
				}
			}
			else if ((GSStatic.message_work_.status & MessageSystem.Status.LOOP) != 0)
			{
				if (activeMessageWork.next_no != global_work.Bk_end_mess)
				{
					messageBoardCtrl.instance.arrow(true, 0);
					messageBoardCtrl.instance.SetArrowPosition(true, 0);
				}
				else
				{
					messageBoardCtrl.instance.arrow(false, 0);
					messageBoardCtrl.instance.SetArrowPosition(false, 0);
				}
				if (activeMessageWork.now_no - 1 != global_work.bk_start_mess)
				{
					messageBoardCtrl.instance.arrow(true, 1);
					messageBoardCtrl.instance.SetArrowPosition(true, 1);
				}
				else
				{
					messageBoardCtrl.instance.arrow(false, 1);
					messageBoardCtrl.instance.SetArrowPosition(false, 1);
				}
				messageBoardCtrl.instance.ActiveMessageBoardTouch();
				int num = 0;
				uint message = 0u;
				bool flag = false;
				if (padCtrl.instance.IsNextMove())
				{
					if (padCtrl.instance.GetKeyDown(KeyType.StickL_Right) || padCtrl.instance.GetKeyDown(KeyType.Right) || padCtrl.instance.GetKeyDown(KeyType.A) || padCtrl.instance.GetWheelMoveDown())
					{
						message = GSStatic.message_work_.next_no;
						flag = true;
						messageBoardCtrl.instance.arrow(false, 0);
						messageBoardCtrl.instance.arrow(false, 1);
					}
					else if ((padCtrl.instance.GetKeyDown(KeyType.StickL_Left) || padCtrl.instance.GetKeyDown(KeyType.Left) || padCtrl.instance.GetKeyDown(KeyType.B) || padCtrl.instance.GetWheelMoveUp()) && GSStatic.message_work_.now_no - 1 != global_work.bk_start_mess)
					{
						message = (uint)(GSStatic.message_work_.now_no - 1);
						flag = true;
						messageBoardCtrl.instance.arrow(false, 0);
						messageBoardCtrl.instance.arrow(false, 1);
					}
				}
				padCtrl.instance.WheelMoveValUpdate();
				if (flag)
				{
					soundCtrl.instance.PlaySE(43);
					advCtrl.instance.message_system_.SetMessage(message);
					advCtrl.instance.message_system_.Message_main();
					GSStatic.message_work_.questioning_message_wait = 5;
				}
				else if (padCtrl.instance.GetKeyDown(KeyType.L) || num != 0)
				{
					messageBoardCtrl.instance.arrow(false, 0);
					messageBoardCtrl.instance.arrow(false, 1);
					messageBoardCtrl.instance.SetArrowPosition(false, 0);
					messageBoardCtrl.instance.SetArrowPosition(false, 1);
					messageBoardCtrl.instance.ActiveNormalMessageNextTouch();
					if (GSStatic.message_work_.tukkomi_no != 0)
					{
						if (GSStatic.global_work_.title == TitleId.GS3 && (long)bgCtrl.instance.bg_no != 6)
						{
							global_work.r.no_1 = 4;
							global_work.r.no_2 = 2;
							return;
						}
						fadeCtrl.instance.play(3u, 1u, 4u);
						messageBoardCtrl.instance.board(false, false);
						Balloon.PlayHoldIt();
						GSStatic.saiban_work_.wait_timer = 57;
						global_work.Mess_move_flag = 0;
						activeMessageWork.message_trans_flag = 0;
						global_work.r.no_1 = 4;
						global_work.r.no_2 = 0;
						sub_window_.SetReq(SubWindow.Req.QUESTIONING_YUSABURU);
						return;
					}
				}
				else if (padCtrl.instance.GetKeyDown(KeyType.R))
				{
					global_work.r_bk.CopyFrom(ref global_work.r);
					global_work.r.Set(8, 0, 0, 1);
					messageBoardCtrl.instance.arrow(false, 0);
					messageBoardCtrl.instance.arrow(false, 1);
					messageBoardCtrl.instance.SetArrowPosition(false, 0);
					messageBoardCtrl.instance.SetArrowPosition(false, 1);
					return;
				}
			}
			else if (padCtrl.instance.GetKeyDown(KeyType.R) && (GSStatic.message_work_.status & (MessageSystem.Status.RT_WAIT | MessageSystem.Status.SELECT)) != 0 && GSMain_SaibanPart.CheckNoteKey())
			{
				global_work.r_bk.CopyFrom(ref global_work.r);
				global_work.r.Set(8, 0, 0, 0);
				messageBoardCtrl.instance.arrow(false, 0);
				messageBoardCtrl.instance.arrow(false, 1);
				messageBoardCtrl.instance.SetArrowPosition(false, 0);
				messageBoardCtrl.instance.SetArrowPosition(false, 1);
			}
		}
		if (global_work.title == TitleId.GS2 && (global_work.scenario == 0 || global_work.scenario == 1) && old_tukkomi_no_ != activeMessageWork.tukkomi_no)
		{
			old_tukkomi_no_ = activeMessageWork.tukkomi_no;
		}
		else if (activeMessageWork.tukkomi_no > 1)
		{
			if (global_work.gauge_disp_flag == 1 && first_question_)
			{
				lifeGaugeCtrl.instance.ActionLifeGauge(lifeGaugeCtrl.Gauge_State.LIFE_ON);
				first_question_ = false;
			}
		}
		else if (!first_question_)
		{
			lifeGaugeCtrl.instance.ActionLifeGauge(lifeGaugeCtrl.Gauge_State.LIFE_OFF);
			first_question_ = true;
		}
		if ((global_work.status_flag & 0x400u) != 0)
		{
		}
		if ((GSStatic.message_work_.status & MessageSystem.Status.LOOP) != 0 && currentRoutine.r.no_0 != 14 && activeMessageWork.now_no - 1 != global_work.bk_start_mess && global_work.language != "JAPAN")
		{
		}
	}

	private static void Exit(GlobalWork global_work)
	{
	}

	private static void ObjSet(GlobalWork global_work)
	{
		AnimationSystem instance = AnimationSystem.Instance;
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		switch (global_work.title)
		{
		case TitleId.GS1:
			num = 21;
			num2 = 23;
			num3 = 25;
			break;
		case TitleId.GS2:
			num = 36;
			num2 = 38;
			num3 = 40;
			break;
		case TitleId.GS3:
			num = 66;
			num2 = 68;
			num3 = 70;
			break;
		}
		if (GSStatic.global_work_.title == TitleId.GS1 || GSStatic.global_work_.title == TitleId.GS2)
		{
		}
		switch (global_work.r.no_2)
		{
		case 0:
			if (advCtrl.instance.sub_window_.GetCurrentRoutine().r.no_0 == 4)
			{
				instance.PlayObject((int)global_work.title, 0, num);
				soundCtrl.instance.PlaySE(83);
				global_work.r.no_2++;
			}
			break;
		case 1:
			if (!instance.IsPlayingObject((int)global_work.title, 0, num))
			{
				fadeCtrl.instance.play(3u, 1u, 8u);
				global_work.r.no_2++;
			}
			break;
		case 2:
			if (fadeCtrl.instance.status == fadeCtrl.Status.NO_FADE)
			{
				instance.StopObject((int)global_work.title, 0, num);
				instance.PlayObject((int)global_work.title, 0, num2);
				global_work.r.no_2++;
			}
			break;
		case 3:
			if (!instance.IsPlayingObject((int)global_work.title, 0, num2))
			{
				instance.StopObject((int)global_work.title, 0, num2);
				instance.PlayObject((int)global_work.title, 0, num3);
				global_work.r.no_2++;
			}
			break;
		case 4:
			if (!instance.IsPlayingObject((int)global_work.title, 0, num3))
			{
				instance.StopObject((int)global_work.title, 0, num3);
				global_work.r.no_1 = 1;
				global_work.r.no_2 = 0;
			}
			break;
		}
	}

	private static void Yusaburi(GlobalWork global_work)
	{
		MessageWork activeMessageWork = MessageSystem.GetActiveMessageWork();
		switch (global_work.r.no_2)
		{
		case 0:
			if (GSStatic.saiban_work_.wait_timer == 0)
			{
				switch (GSStatic.global_work_.title)
				{
				case TitleId.GS1:
					GS1_questioning_yusaburi_case0();
					break;
				case TitleId.GS2:
					GS2_questioning_yusaburi_case0();
					break;
				case TitleId.GS3:
					GS3_questioning_yusaburi_case0();
					break;
				}
				MessageSystem.Mess_window_set(4u);
				global_work.r.no_2++;
			}
			else
			{
				GSStatic.saiban_work_.wait_timer--;
			}
			if (!objMoveCtrl.instance.is_play)
			{
				objMoveCtrl.instance.stop(1);
				switch (GSStatic.global_work_.title)
				{
				case TitleId.GS1:
					GS1_questioning_yusaburi_case0();
					break;
				case TitleId.GS2:
					GS2_questioning_yusaburi_case0();
					break;
				case TitleId.GS3:
					GS3_questioning_yusaburi_case0();
					break;
				}
				MessageSystem.Mess_window_set(4u);
				global_work.r.no_2++;
			}
			break;
		case 1:
			if (!bgCtrl.instance.is_scrolling_court)
			{
				if (GSStatic.message_work_.tukkomi_flag != 0)
				{
					global_work.Mess_move_flag = 1;
					activeMessageWork.message_trans_flag = 1;
				}
				else
				{
					MessageSystem.Mess_window_set(3u);
				}
				advCtrl.instance.message_system_.SetMessage(activeMessageWork.tukkomi_no);
				lifeGaugeCtrl.instance.ActionLifeGauge(lifeGaugeCtrl.Gauge_State.LIFE_OFF);
				messageBoardCtrl.instance.guide_ctrl.next_guid = guideCtrl.GuideType.HOUTEI;
				global_work.r.no_1 = 1;
				global_work.r.no_2 = 0;
			}
			break;
		case 2:
			if (GSStatic.global_work_.title == TitleId.GS3)
			{
				AnimationSystem.Instance.CharacterAnimationObject.gameObject.SetActive(true);
				bgCtrl.instance.SetSprite(6);
				Balloon.PlayHoldIt();
				fadeCtrl.instance.play(3u, 1u, 4u);
				GSStatic.saiban_work_.wait_timer = 57;
				global_work.Mess_move_flag = 0;
				activeMessageWork.message_trans_flag = 0;
				messageBoardCtrl.instance.board(false, false);
				global_work.r.no_1 = 4;
				global_work.r.no_2 = 0;
				advCtrl.instance.sub_window_.SetReq(SubWindow.Req.QUESTIONING_YUSABURU);
			}
			break;
		}
	}

	private static void Tukitukeru(GlobalWork global_work)
	{
		MessageWork activeMessageWork = MessageSystem.GetActiveMessageWork();
		switch (global_work.r.no_2)
		{
		case 0:
			if (GSStatic.saiban_work_.wait_timer == 0)
			{
				fadeCtrl.instance.play(3u, 1u, 4u);
				GSStatic.saiban_work_.wait_timer = 46;
				global_work.r.no_2++;
			}
			else
			{
				GSStatic.saiban_work_.wait_timer--;
			}
			break;
		case 1:
			if (GSStatic.saiban_work_.wait_timer == 0)
			{
				switch (GSStatic.global_work_.title)
				{
				case TitleId.GS1:
					GS1_questioning_tukitukeru_case1();
					break;
				case TitleId.GS2:
					GS2_questioning_tukitukeru_case1();
					break;
				case TitleId.GS3:
					GS3_questioning_tukitukeru_case1();
					break;
				}
				MessageSystem.Mess_window_set(4u);
				global_work.r.no_2++;
			}
			else
			{
				GSStatic.saiban_work_.wait_timer--;
			}
			break;
		case 2:
			if (!bgCtrl.instance.is_scrolling_court)
			{
				GSStatic.saiban_work_.wait_timer = 20;
				global_work.r.no_2++;
			}
			break;
		case 3:
			if (GSStatic.saiban_work_.wait_timer == 0)
			{
				if (GSStatic.message_work_.desk_attack != 0)
				{
					GSStatic.global_work_.Mess_move_flag = 1;
					activeMessageWork.message_trans_flag = 1;
					activeMessageWork.mess_win_rno = 0;
				}
				else
				{
					MessageSystem.Mess_window_set(3u);
				}
				messageBoardCtrl.instance.guide_ctrl.next_guid = guideCtrl.GuideType.HOUTEI;
				if (GSStatic.global_work_.title == TitleId.GS1)
				{
					MUJYUN_CK_DATA[] mujyunCkData = GSScenario.GetMujyunCkData();
					for (int i = 0; i <= mujyunCkData.Length; i++)
					{
						if (i == mujyunCkData.Length)
						{
							lifeGaugeCtrl.instance.ActionLifeGauge(lifeGaugeCtrl.Gauge_State.NOTICE_DAMAGE);
							break;
						}
						if (mujyunCkData[i].jump == GSStatic.message_work_.now_no)
						{
							lifeGaugeCtrl.instance.ActionLifeGauge(lifeGaugeCtrl.Gauge_State.LIFE_OFF);
							break;
						}
					}
				}
				global_work.r.CopyFrom(ref global_work.r_bk);
			}
			else
			{
				GSStatic.saiban_work_.wait_timer--;
			}
			break;
		}
	}

	private static void GS1_questioning_yusaburi_case0()
	{
		MessageWork activeMessageWork = MessageSystem.GetActiveMessageWork();
		if (GSStatic.global_work_.scenario != 31 || activeMessageWork.now_no == scenario.SC4_68240)
		{
		}
		bgCtrl.instance.CourtScrol(0u, 1u, 2u, 30u, 31u, 1u);
	}

	private static void GS1_questioning_tukitukeru_case1()
	{
		MessageWork activeMessageWork = MessageSystem.GetActiveMessageWork();
		if (GSStatic.global_work_.scenario == 31 && (activeMessageWork.next_no == scenario.SC4_68330 || activeMessageWork.next_no == scenario.SC4_68240) && (activeMessageWork.now_no == scenario.SC4_68300 || activeMessageWork.now_no == scenario.SYS_M0100 || activeMessageWork.now_no == scenario.SYS_M0110 || activeMessageWork.now_no == scenario.SYS_M0120 || activeMessageWork.now_no == scenario.SYS_M0130))
		{
			bgCtrl.instance.SetSprite(5);
			GSStatic.global_work_.Def_talk_foa = 376;
			GSStatic.global_work_.Def_wait_foa = 376;
			AnimationSystem.Instance.PlayCharacter((int)GSStatic.global_work_.title, 44, 376, 376);
			GSStatic.tantei_work_.person_flag = 1;
			GSStatic.obj_work_[1].h_num = 44;
			GSStatic.obj_work_[1].foa = 376;
			GSStatic.obj_work_[1].idlingFOA = 376;
		}
		bgCtrl.instance.CourtScrol(0u, 1u, 2u, 30u, 31u, 11u);
	}

	private static void GS2_questioning_yusaburi_case0()
	{
		bgCtrl.instance.CourtScrol(0u, 1u, 3u, 30u, 31u, 1u);
	}

	private static void GS2_questioning_tukitukeru_case1()
	{
		bgCtrl.instance.CourtScrol(0u, 1u, 3u, 30u, 31u, 11u);
	}

	private static void GS3_questioning_yusaburi_case0()
	{
		if (GSStatic.global_work_.scenario == 0 || GSStatic.global_work_.scenario == 1 || GSStatic.global_work_.scenario == 12 || GSStatic.global_work_.scenario == 13)
		{
			bgCtrl.instance.CourtScrol(0u, 1u, 7u, 30u, 31u, 117u);
		}
		else if (GSStatic.global_work_.scenario == 14 || GSStatic.global_work_.scenario == 15 || GSStatic.global_work_.scenario == 16 || GSStatic.global_work_.scenario == 17)
		{
			bgCtrl.instance.CourtScrol(0u, 1u, 31u, 30u, 31u, 444u);
		}
		else
		{
			bgCtrl.instance.CourtScrol(0u, 1u, 3u, 30u, 31u, 1u);
		}
	}

	private static void GS3_questioning_tukitukeru_case1()
	{
		if (GSStatic.global_work_.scenario == 0 || GSStatic.global_work_.scenario == 1 || GSStatic.global_work_.scenario == 12 || GSStatic.global_work_.scenario == 13)
		{
			bgCtrl.instance.CourtScrol(0u, 1u, 7u, 30u, 31u, 117u);
		}
		else if (GSStatic.global_work_.scenario == 14 || GSStatic.global_work_.scenario == 15 || GSStatic.global_work_.scenario == 16 || GSStatic.global_work_.scenario == 17)
		{
			bgCtrl.instance.CourtScrol(0u, 1u, 31u, 30u, 31u, 447u);
		}
		else
		{
			bgCtrl.instance.CourtScrol(0u, 1u, 3u, 30u, 31u, 12u);
		}
	}
}
