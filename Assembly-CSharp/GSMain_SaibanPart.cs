using System;

public static class GSMain_SaibanPart
{
	private delegate void SaibanPartProc(GlobalWork global_work);

	private static SaibanPartProc[] proc_table;

	static GSMain_SaibanPart()
	{
		proc_table = new SaibanPartProc[3] { Init, Main, Exit };
	}

	public static void Proc(GlobalWork global_work)
	{
		if (global_work.r.no_1 != 10)
		{
			proc_table[global_work.r.no_1](global_work);
		}
	}

	public static bool CheckSaveKey()
	{
		if ((GSStatic.global_work_.status_flag & 0x800u) != 0)
		{
			return false;
		}
		return true;
	}

	public static bool CheckNoteKey()
	{
		if ((GSStatic.global_work_.status_flag & 0x10u) != 0)
		{
			return false;
		}
		return true;
	}

	private static void Init(GlobalWork global_work)
	{
		MessageSystem.Message_init();
		GSMain_Status.Status_init(global_work, GSStatic.status_work_);
		if (GSStatic.global_work_.title != 0)
		{
			Array.Clear(global_work.sce_flag, 0, global_work.sce_flag.Length);
			Array.Clear(global_work.roomseq, 0, global_work.roomseq.Length);
			Array.Clear(global_work.lockdat, 0, global_work.lockdat.Length);
			global_work.lock_max = 0;
		}
		else
		{
			GS1_saiban_init_case0(global_work);
		}
		bgCtrl.instance.Bg256_SP_Flag = 0;
		global_work.Mess_move_flag = 1;
		GSStatic.message_work_.message_trans_flag = 1;
		GSStatic.message_work_2_.message_trans_flag = 0;
		advCtrl.instance.sub_window_.tantei_tukituke_ = 0;
		advCtrl.instance.message_system_.SetMessage(128u);
		global_work.rest = 5;
		global_work.rest_old = byte.MaxValue;
		MessageSystem.Set_scenario_enable();
		if (global_work.scenario > 18)
		{
			GSFlag.Set(0u, scenario.SCE4_FLAG_STATUS_3D_ENABLE, 1u);
		}
		global_work.r.Set(4, 1, 0, 0);
		GS3DS_saiban_init();
	}

	private static void Main(GlobalWork global_work)
	{
		if (fadeCtrl.instance.status != 0)
		{
			return;
		}
		if (!advCtrl.instance.sub_window_.IsBusy())
		{
			if (selectPlateCtrl.instance.select_animation_playing || lifeGaugeCtrl.instance.is_lifegauge_moving() || lifeGaugeCtrl.instance.gauge_mode == 7 || lifeGaugeCtrl.instance.gauge_mode == 0)
			{
				return;
			}
			if (padCtrl.instance.GetKeyDown(KeyType.Start) && (global_work.status_flag & 0x10) == 0 && (GSStatic.message_work_.status & (MessageSystem.Status.RT_WAIT | MessageSystem.Status.SELECT)) != 0 && CheckSaveKey())
			{
				global_work.r_bk.CopyFrom(ref global_work.r);
				soundCtrl.instance.PlaySE(49);
				global_work.r.Set(17, 0, 0, 0);
			}
			else if (padCtrl.instance.GetKeyDown(KeyType.R) && (GSStatic.message_work_.status & (MessageSystem.Status.RT_WAIT | MessageSystem.Status.SELECT)) != 0 && CheckNoteKey())
			{
				global_work.r_bk.CopyFrom(ref global_work.r);
				global_work.r.Set(8, 0, 0, 0);
			}
		}
		if (GSStatic.global_work_.title == TitleId.GS1 && (global_work.status_flag & 0x400) == 0)
		{
		}
	}

	private static void Exit(GlobalWork global_work)
	{
		GS3DS_saiban_exit();
		if (global_work.r.no_2 == 0)
		{
			for (int i = 0; i < 12; i++)
			{
				global_work.talk_end_flag[i] = 0u;
			}
		}
		int[] array = new int[3] { 16, 230, 331 };
		SaveSys save_sys_ = GSStatic.save_sys_;
		switch (global_work.r.no_2)
		{
		case 0:
		{
			bool flag = false;
			switch (GSStatic.global_work_.title)
			{
			case TitleId.GS1:
				switch (global_work.scenario)
				{
				case 1:
					if (global_work.Scenario_enable >> 4 == 1)
					{
						save_sys_.Scenario_enable = 32;
						global_work.Scenario_enable = save_sys_.Scenario_enable;
						global_work.r.no_2++;
						global_work.r.no_3 = 1;
						flag = true;
					}
					break;
				case 5:
					if (global_work.Scenario_enable >> 4 == 2)
					{
						save_sys_.Scenario_enable = 48;
						global_work.Scenario_enable = save_sys_.Scenario_enable;
						global_work.r.no_2++;
						global_work.r.no_3 = 2;
						flag = true;
					}
					break;
				case 11:
					if (global_work.Scenario_enable >> 4 == 3)
					{
						save_sys_.Scenario_enable = 64;
						global_work.Scenario_enable = save_sys_.Scenario_enable;
						global_work.r.no_2++;
						global_work.r.no_3 = 3;
						flag = true;
					}
					break;
				case 17:
					if (global_work.Scenario_enable >> 4 == 4)
					{
						soundCtrl.instance.FadeOutBGM(20);
						save_sys_.Scenario_enable = 80;
						global_work.Scenario_enable = save_sys_.Scenario_enable;
						global_work.r.no_2++;
						global_work.r.no_3 = 4;
						flag = true;
					}
					else
					{
						soundCtrl.instance.FadeOutBGM(250);
						global_work.r.no_2 += 2;
						global_work.r.no_3 = 150;
						flag = true;
					}
					break;
				default:
					flag = false;
					break;
				}
				break;
			case TitleId.GS2:
				switch (global_work.scenario)
				{
				case 2:
					if (global_work.Scenario_enable >> 4 == 1)
					{
						save_sys_.Scenario_enable = 32;
						global_work.Scenario_enable = save_sys_.Scenario_enable;
						global_work.r.no_2++;
						global_work.r.no_3 = 1;
						flag = true;
					}
					break;
				case 8:
					if (global_work.Scenario_enable >> 4 == 2)
					{
						save_sys_.Scenario_enable = 48;
						global_work.Scenario_enable = save_sys_.Scenario_enable;
						global_work.r.no_2++;
						global_work.r.no_3 = 2;
						flag = true;
					}
					break;
				case 14:
					if (global_work.Scenario_enable >> 4 == 3)
					{
						save_sys_.Scenario_enable = 64;
						global_work.Scenario_enable = save_sys_.Scenario_enable;
						global_work.r.no_2++;
						global_work.r.no_3 = 3;
						flag = true;
					}
					break;
				default:
					flag = false;
					break;
				}
				break;
			case TitleId.GS3:
				switch (global_work.scenario)
				{
				case 2:
					if (global_work.Scenario_enable >> 4 == 1)
					{
						save_sys_.Scenario_enable = 32;
						global_work.Scenario_enable = save_sys_.Scenario_enable;
						global_work.r.no_2++;
						global_work.r.no_3 = 1;
						flag = true;
					}
					break;
				case 7:
					if (global_work.Scenario_enable >> 4 == 2)
					{
						save_sys_.Scenario_enable = 48;
						global_work.Scenario_enable = save_sys_.Scenario_enable;
						global_work.r.no_2++;
						global_work.r.no_3 = 2;
						flag = true;
					}
					break;
				case 12:
					if (global_work.Scenario_enable >> 4 == 3)
					{
						save_sys_.Scenario_enable = 64;
						global_work.Scenario_enable = save_sys_.Scenario_enable;
						global_work.r.no_2++;
						global_work.r.no_3 = 3;
						flag = true;
					}
					break;
				case 14:
					if (global_work.Scenario_enable >> 4 == 4)
					{
						save_sys_.Scenario_enable = 80;
						global_work.Scenario_enable = save_sys_.Scenario_enable;
						global_work.r.no_2++;
						global_work.r.no_3 = 4;
						flag = true;
					}
					break;
				default:
					flag = false;
					break;
				}
				break;
			}
			TrophyCtrl.check_trophy_scenario_clear(false);
			GSStatic.global_work_.inspect_readed_ = new byte[2, 1024];
			if (tanteiMenu.instance != null)
			{
				tanteiMenu.instance.cursor_no = 0;
			}
			GSStatic.open_sce_.CurrentSave(GSStatic.save_sys_, GSStatic.global_work_.title);
			if (!flag)
			{
				global_work.r.no_2++;
				global_work.r.no_3 = byte.MaxValue;
			}
			break;
		}
		case 1:
			switch (global_work.r.no_3)
			{
			case 1:
			case 2:
			case 3:
			case 4:
				advCtrl.instance.sub_window_.SetReq(SubWindow.Req.EPISODE_CLEAR);
				global_work.r.Set(12, 0, 0, global_work.r.no_3);
				soundCtrl.instance.PlaySE(array[(int)GSStatic.global_work_.title]);
				break;
			case 5:
				advCtrl.instance.sub_window_.SetReq(SubWindow.Req.EPISODE_CLEAR);
				global_work.r.Set(12, 2, 0, 0);
				break;
			case byte.MaxValue:
				global_work.r.Set(11, 0, 1, 1);
				soundCtrl.instance.PlaySE(array[(int)GSStatic.global_work_.title]);
				break;
			}
			break;
		case 2:
			if (global_work.r.no_3-- == 0)
			{
				global_work.r.no_3 = 150;
				global_work.r.no_2++;
			}
			break;
		case 3:
			if (global_work.r.no_3-- == 0)
			{
				fadeCtrl.instance.play(2u, 4u, 1u);
				global_work.r.no_3 = 30;
				global_work.r.no_2++;
			}
			break;
		case 4:
			if (fadeCtrl.instance.status == fadeCtrl.Status.NO_FADE && global_work.r.no_3-- == 0)
			{
				bgCtrl.instance.SetSprite(4095);
				global_work.r.Set(11, 0, 1, 1);
				soundCtrl.instance.PlaySE(array[(int)GSStatic.global_work_.title]);
			}
			break;
		}
	}

	public static void GS3DS_saiban_init()
	{
	}

	public static void GS3DS_saiban_exit()
	{
		GSMapIcon.instance.Terminate();
		messageBoardCtrl.instance.board(false, false);
	}

	private static void GS1_saiban_init_case0(GlobalWork global_work)
	{
		if ((uint)global_work.scenario < 17u)
		{
			Array.Clear(global_work.sce_flag, 0, global_work.sce_flag.Length);
			if (global_work.scenario > 1)
			{
				GSFlag.Set(0u, scenario.SCE1_CHIHIRO_DISCOVER, 1u);
			}
			return;
		}
		switch (global_work.scenario)
		{
		case 19:
		{
			for (uint num3 = scenario.SCE41_FLAG_ST_G; num3 < scenario.SCE41_FLAG_ST_G_MAX; num3++)
			{
				GSFlag.Set(0u, num3, 0u);
			}
			GSFlag.Set(0u, scenario.SCE4_FLAG_DEBUG_4_1A_END, 0u);
			GSFlag.Set(0u, scenario.SCE4_FLAG_DEBUG_4_1B_END, 0u);
			GSFlag.Set(0u, scenario.SCE4_FLAG_ST_G_AKANE_JOIN, 1u);
			break;
		}
		case 25:
		{
			for (uint num2 = scenario.SCE43_FLAG_ST_G; num2 < scenario.SCE43_FLAG_ST_G_MAX; num2++)
			{
				GSFlag.Set(0u, num2, 0u);
			}
			GSFlag.Set(0u, scenario.SCE4_FLAG_DEBUG_4_3A_END, 0u);
			GSFlag.Set(0u, scenario.SCE4_FLAG_DEBUG_4_3B_END, 0u);
			GSFlag.Set(0u, scenario.SCE4_FLAG_ST_G_AKANE_JOIN, 1u);
			break;
		}
		case 31:
		{
			for (uint num = scenario.SCE45_FLAG_ST_G; num < scenario.SCE45_FLAG_ST_G_MAX; num++)
			{
				GSFlag.Set(0u, num, 0u);
			}
			GSFlag.Set(0u, scenario.SCE4_FLAG_DEBUG_4_5A_END, 0u);
			GSFlag.Set(0u, scenario.SCE4_FLAG_DEBUG_4_5B_END, 0u);
			GSFlag.Set(0u, scenario.SCE4_FLAG_DEBUG_4_5C_END, 0u);
			GSFlag.Set(0u, scenario.SCE4_FLAG_ST_G_AKANE_JOIN, 0u);
			break;
		}
		}
		MessageSystem.ClearSceLocalFlg();
	}
}
