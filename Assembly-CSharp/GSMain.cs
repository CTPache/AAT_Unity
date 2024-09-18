using System;
using UnityEngine;

public static class GSMain
{
	private delegate void GSProc(GlobalWork global_work);

	private static readonly GSProc[] proc_table;

	public static bool is_active;

	public static bool is_active_
	{
		get
		{
			return is_active;
		}
		set
		{
			is_active = value;
		}
	}

	static GSMain()
	{
		proc_table = new GSProc[18]
		{
			Logo,
			NewTitle,
			GsTitle,
			GameOver,
			GSMain_SaibanPart.Proc,
			GSMain_TanteiPart.Proc,
			GSMain_Testimony.Proc,
			GSMain_Questioning.Proc,
			GSMain_Status.Proc,
			NoteAddDisp,
			DeliverJudgment,
			Save,
			EpisodeClear,
			EpisodeSelect,
			EpisodeContinue,
			Format,
			DebugMenu,
			Option
		};
	}

	public static void MainLoop()
	{
		if (is_active_)
		{
			if (GSStatic.message_work_.questioning_message_wait > 0)
			{
				GSStatic.message_work_.questioning_message_wait--;
			}
			MessageWork activeMessageWork = MessageSystem.GetActiveMessageWork();
			ushort num = activeMessageWork.op_work[7];
			if (num == 65533)
			{
				spefCtrl.instance.Monochrome_set(4, 1, 1);
				activeMessageWork.op_work[7] = 0;
			}
			rotBG.instance.Rot_bg_main();
			advCtrl.instance.message_system_.Message_main();
			Mess_window_main();
			DIconController.instance.Dicon_disp_main();
			Proc();
			GSObj_Sub.Instance.Obj_main();
			bgCtrl.instance.BG256_main();
			if (GSStatic.global_work_.title == TitleId.GS2)
			{
				GS2_OpObjCtrl.instance.Process();
			}
			advCtrl.instance.sub_window_.Process();
			if (GSStatic.global_work_.title == TitleId.GS3)
			{
				butterflyCtrl.instance.Butterfly();
			}
		}
	}

	public static void Proc()
	{
		GlobalWork global_work_ = GSStatic.global_work_;
		if (GSStatic.global_work_.title == TitleId.GS2)
		{
		}
		proc_table[global_work_.r.no_0](global_work_);
		if (GSStatic.global_work_.title != 0)
		{
		}
	}

	private static void Logo(GlobalWork global_work)
	{
	}

	private static void NewTitle(GlobalWork global_work)
	{
		switch (global_work.r.no_1)
		{
		case 0:
			loadingCtrl.instance.play(loadingCtrl.Type.SAVEING);
			GSStatic.message_work_.mdt_path = GSScenario.GetScenarioMdtPath(GSStatic.global_work_.scenario);
			SaveControl.SaveSystemDataRequest();
			global_work.r.no_1++;
			break;
		case 1:
			if (SaveControl.is_save_)
			{
				SaveControl.SaveSystemData();
				loadingCtrl.instance.wait_start();
				global_work.r.no_1++;
			}
			break;
		case 2:
			if (!loadingCtrl.instance.is_wait)
			{
				loadingCtrl.instance.stop();
				if (SaveControl.is_save_error)
				{
					messageBoxCtrl.instance.init();
					messageBoxCtrl.instance.SetWindowSize(new Vector2(1200f, 360f));
					messageBoxCtrl.instance.SetText(TextDataCtrl.GetTexts(TextDataCtrl.SaveTextID.SAVE_ERROR));
					messageBoxCtrl.instance.SetTextPosCenter();
					messageBoxCtrl.instance.OpenWindow();
				}
				global_work.r.no_1++;
			}
			break;
		case 3:
			if (SaveControl.is_save_error && messageBoxCtrl.instance.active)
			{
				if (padCtrl.instance.GetKeyDown(KeyType.A))
				{
					messageBoxCtrl.instance.CloseWindow();
				}
			}
			else
			{
				bgCtrl.instance.SetSprite(4095);
				advCtrl.instance.message_system_.is_end = true;
				global_work.r.no_1++;
			}
			break;
		}
	}

	private static void GsTitle(GlobalWork global_work)
	{
	}

	private static void GameOver(GlobalWork global_work)
	{
		gameoverCtrl.instance.Play();
		if (!gameoverCtrl.instance.is_play)
		{
			bgCtrl.instance.SetSprite(4095);
			bgCtrl.instance.GameOverInactiveAnmChild();
			fadeCtrl.instance.play(1, true);
			End();
			titleCtrlRoot.instance.active = true;
			titleCtrlRoot.instance.Scene(titleCtrlRoot.SceneType.Top);
			GSStatic.global_work_.r.Set(2, 0, 0, 0);
		}
	}

	private static void NoteAddDisp(GlobalWork global_work)
	{
	}

	private static void DeliverJudgment(GlobalWork global_work)
	{
		if (!judgmentCtrl.instance.is_guilty_play)
		{
			global_work.r.CopyFrom(ref global_work.r_bk);
		}
	}

	private static void Save(GlobalWork global_work)
	{
		switch (global_work.r.no_1)
		{
		case 0:
			global_work.status_flag &= 4294965247u;
			loadingCtrl.instance.play(loadingCtrl.Type.SAVEING);
			GSStatic.message_work_.mdt_path = GSScenario.GetScenarioMdtPath(GSStatic.global_work_.scenario);
			soundCtrl.instance.playBgmNo = 254;
			soundCtrl.instance.stopBgmNo = 254;
			SaveControl.SaveSystemDataRequest();
			global_work.r.no_1++;
			break;
		case 1:
			if (SaveControl.is_save_)
			{
				SaveControl.SaveSystemData();
				loadingCtrl.instance.wait_start();
				global_work.r.no_1++;
			}
			break;
		case 2:
			if (!loadingCtrl.instance.is_wait)
			{
				loadingCtrl.instance.stop();
				if (SaveControl.is_save_error)
				{
					messageBoxCtrl.instance.init();
					messageBoxCtrl.instance.SetWindowSize(new Vector2(1200f, 360f));
					messageBoxCtrl.instance.SetText(TextDataCtrl.GetTexts(TextDataCtrl.SaveTextID.SAVE_ERROR));
					messageBoxCtrl.instance.SetTextPosCenter();
					messageBoxCtrl.instance.OpenWindow();
				}
				global_work.r.no_1++;
			}
			break;
		case 3:
			if (SaveControl.is_save_error && messageBoxCtrl.instance.active)
			{
				if (padCtrl.instance.GetKeyDown(KeyType.A))
				{
					messageBoxCtrl.instance.CloseWindow();
				}
			}
			else
			{
				TouchSystem.TouchInActive();
				SaveLoadUICtrl.instance.SaveConfirmation();
				global_work.r.no_1++;
			}
			break;
		case 4:
			if (!SaveLoadUICtrl.instance.is_input_wait)
			{
				SaveLoadUICtrl.instance.ClearSaveOpen();
				global_work.r.no_1++;
			}
			break;
		case 5:
			if (!SaveLoadUICtrl.instance.is_open)
			{
				global_work.r.no_1++;
			}
			break;
		case 6:
			advCtrl.instance.message_system_.is_end = true;
			global_work.r.no_1++;
			break;
		}
	}

	private static void EpisodeClear(GlobalWork global_work)
	{
		switch (global_work.r.no_1)
		{
		case 0:
		{
			global_work.Mess_move_flag = 0;
			MessageSystem.GetActiveMessageWork().message_trans_flag = 0;
			messageBoardCtrl.instance.board(false, false);
			fadeCtrl.instance.play(0, false);
			for (int i = 0; i < GSStatic.global_work_.sw_move_flag.Length; i++)
			{
				GSStatic.global_work_.sw_move_flag[i] = 0;
			}
			global_work.r.no_1++;
			break;
		}
		case 1:
			global_work.r.no_1++;
			break;
		case 2:
			soundCtrl.instance.PlaySE(49);
			fadeCtrl.instance.play(0, true);
			global_work.r.no_1++;
			break;
		case 3:
			break;
		}
	}

	private static void EpisodeSelect(GlobalWork global_work)
	{
	}

	private static void EpisodeContinue(GlobalWork global_work)
	{
	}

	private static void Format(GlobalWork global_work)
	{
	}

	private static void DebugMenu(GlobalWork global_work)
	{
	}

	private static void Option(GlobalWork global_work)
	{
		switch (global_work.r.no_1)
		{
		case 0:
			optionCtrl.instance.Open(optionCtrl.OptionType.IN_GAME);
			global_work.r.no_1++;
			advCtrl.instance.sub_window_.stack_++;
			advCtrl.instance.sub_window_.routine_[advCtrl.instance.sub_window_.stack_].r.Set(30, 0, 0, 0);
			if (lifeGaugeCtrl.instance.gauge_active)
			{
				lifeGaugeCtrl.instance.ActionLifeGauge(lifeGaugeCtrl.Gauge_State.LIFE_OFF_MOMENT);
			}
			break;
		case 1:
			if (!optionCtrl.instance.is_open)
			{
				global_work.r.no_1++;
				lifeGaugeCtrl.instance.ResumeFromPause();
			}
			break;
		case 2:
			global_work.r.CopyFrom(ref global_work.r_bk);
			messageBoardCtrl.instance.guide_ctrl.guideIconSet(false, messageBoardCtrl.instance.guide_ctrl.GetChangeGuideType());
			advCtrl.instance.sub_window_.routine_[advCtrl.instance.sub_window_.stack_].r.Set(0, 0, 0, 0);
			advCtrl.instance.sub_window_.stack_--;
			if (!selectPlateCtrl.instance.body_active && messageBoardCtrl.instance.body_active)
			{
				messageBoardCtrl.instance.ActiveMessageBoardTouch();
			}
			if (messageBoardCtrl.instance.body_active)
			{
				messageBoardCtrl.instance.guide_ctrl.ActiveTouch();
			}
			break;
		}
	}

	private static void Mess_window_main()
	{
		MessageWork activeMessageWork = MessageSystem.GetActiveMessageWork();
		if (GSStatic.global_work_.language != 0)
		{
		}
		if (GSStatic.global_work_.title == TitleId.GS1)
		{
		}
		switch (activeMessageWork.mess_win_rno)
		{
		case 0:
		case 1:
		case 2:
		case 9:
		case 10:
			if (GSStatic.global_work_.title == TitleId.GS1 && activeMessageWork.message_type != WindowType.SUB)
			{
			}
			break;
		case 3:
		case 5:
			GSStatic.global_work_.Mess_move_flag = 1;
			activeMessageWork.message_trans_flag = 1;
			activeMessageWork.mess_win_rno = 0;
			break;
		case 4:
		case 6:
			activeMessageWork.mess_win_rno = 1;
			break;
		case 8:
			activeMessageWork.mess_win_rno = 9;
			break;
		case 7:
			break;
		}
	}

	public static void End()
	{
		GSStatic.mdt_datas_[0] = null;
		GlobalWork global_work_ = GSStatic.global_work_;
		GSStatic.global_work_.reset();
		GSStatic.status_work_.init();
		GSStatic.movie_work_.init();
		GSStatic.cinema_work_.init();
		GSStatic.rot_bg_work_.init();
		GSStatic.saiban_work_.init();
		GSStatic.tantei_work_.init();
		GSStatic.expl_char_work_.init();
		GSStatic.obj_work_[1].init();
		GSStatic.furiko_wk_.init();
		GSStatic.op5_wk_.init();
		GSStatic.menu_work.init();
		GSStatic.sound_save_data.init();
		GSStatic.bg_save_data.init();
		global_work_.r.Set(0, 0, 0, 0);
		if (is_active_)
		{
			AnimationSystem.Instance.StopAll();
			AnimationSystem.Instance.Holder.UnlaodAll();
			advCtrl.instance.end();
			SubWindow sub_window_ = advCtrl.instance.sub_window_;
			sub_window_.routine_[sub_window_.stack_].r.Set(0, 0, 0, 0);
		}
		else
		{
			Debug.Log("GSMain.End() | GSMain.is_active_ == false");
		}
		GSStatic.message_work_.init();
		GSStatic.message_work_2_.init();
		GSStatic.message_work_2_.message_type = WindowType.SUB;
		GSStatic.message_work_.message_trans_flag = 0;
		GSStatic.message_work_2_.message_trans_flag = 0;
		GC.Collect();
	}
}
