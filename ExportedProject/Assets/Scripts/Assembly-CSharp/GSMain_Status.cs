using UnityEngine;

public static class GSMain_Status
{
	public enum Rno1
	{
		INIT = 0,
		MAIN = 1,
		EXIT = 2,
		WAIT = 3,
		MENU_CHG = 4,
		EXCEPTION = 5,
		MELT = 6,
		EFFECT = 7,
		SUB_WINDOW_WAIT = 8
	}

	private delegate void StatusProc(GlobalWork global_work, StatusWork status_work);

	public const int ST_PS_PAGE_ITEM = 1;

	public const int ST_PS_PAGE_CHAR_DISP = 2;

	public const int ST_PS_ARROW_DISP = 4;

	public const int ST_PS_MAGADAMA_TUKITUKE = 8;

	public const int ST_PS_PSYLOCK_TUKITUKE = 16;

	public const int ST_04_PHONE_KURAE = 32;

	public static int loop_se_;

	private static StatusProc[] proc_table;

	static GSMain_Status()
	{
		loop_se_ = 268435455;
		proc_table = new StatusProc[9] { Init, Main, Exit, Wait, MenuChange, StatusException, StatusMelt, StatusEffect, StatusSubWindowWait };
	}

	public static void Status_init(GlobalWork global_work, StatusWork status_work)
	{
		recordListCtrl.instance.setPartRecord(global_work.scenario);
	}

	public static void Proc(GlobalWork global_work)
	{
		proc_table[global_work.r.no_1](global_work, GSStatic.status_work_);
	}

	private static void Init(GlobalWork global_work, StatusWork status_work)
	{
		if (soundCtrl.instance.se_no[0] != 268435455)
		{
			ushort in_se_no = (ushort)soundCtrl.instance.se_no[0];
			soundCtrl.instance.StopSE(in_se_no);
			loop_se_ = in_se_no;
		}
		status_work.page_status = 0;
		status_work.page_now = 0;
		status_work.page_now_max = status_work.item_page_max;
		status_work.now_file = status_work.item_file;
		messageBoardCtrl.instance.arrow(false, 0);
		global_work.r.no_1 = 1;
		advCtrl.instance.sub_window_.SetReq(SubWindow.Req.STATUS);
	}

	private static void Main(GlobalWork global_work, StatusWork status_work)
	{
		if (!advCtrl.instance.sub_window_.IsBusy())
		{
			if (!recordListCtrl.instance.is_open && recordListCtrl.instance.is_info)
			{
				global_work.r.no_1 = 8;
				advCtrl.instance.sub_window_.SetReq(SubWindow.Req.STATUS_SETU);
			}
			else if (global_work.r.no_3 == 1 && global_work.r_bk.no_0 == 5)
			{
			}
		}
	}

	private static void Exit(GlobalWork global_work, StatusWork status_work)
	{
		global_work.r.CopyFrom(ref global_work.r_bk);
		if (loop_se_ != 268435455)
		{
			soundCtrl.instance.PlaySE(loop_se_);
			loop_se_ = 268435455;
		}
	}

	private static void Wait(GlobalWork global_work, StatusWork status_work)
	{
		if (global_work.r.no_3 != 2)
		{
		}
	}

	private static void MenuChange(GlobalWork global_work, StatusWork status_work)
	{
	}

	private static void StatusException(GlobalWork global_work, StatusWork status_work)
	{
	}

	private static void StatusMelt(GlobalWork global_work, StatusWork status_work)
	{
		global_work.r.no_1 = 3;
	}

	private static void StatusEffect(GlobalWork global_work, StatusWork status_work)
	{
		MessageWork activeMessageWork = MessageSystem.GetActiveMessageWork();
		switch (global_work.r.no_2)
		{
		case 0:
			if ((activeMessageWork.status2 & MessageSystem.Status2.WAIT_PSY) != 0)
			{
				if (!bgCtrl.instance.is_slider)
				{
					activeMessageWork.status2 &= ~MessageSystem.Status2.WAIT_PSY;
					soundCtrl.instance.FadeOutBGM(30);
				}
				break;
			}
			switch (GSStatic.global_work_.title)
			{
			case TitleId.GS1:
				Balloon.PlayTakeThat();
				break;
			case TitleId.GS2:
				GS2_status_effect_case0();
				break;
			case TitleId.GS3:
				GS3_status_effect_case0();
				break;
			}
			GSStatic.saiban_work_.wait_timer = 6;
			global_work.Mess_move_flag = 0;
			activeMessageWork.message_trans_flag = 0;
			global_work.timer = 0;
			global_work.r.no_2++;
			global_work.r.no_3 = 0;
			break;
		case 1:
		case 2:
			if (global_work.timer >= 8)
			{
				fadeCtrl.instance.play(3u, 1u, 6u);
				global_work.timer = 0;
				global_work.r.no_2++;
				global_work.r.no_3 = 0;
			}
			else
			{
				global_work.timer++;
			}
			break;
		case 3:
			if (global_work.timer >= 40)
			{
				switch (GSStatic.global_work_.title)
				{
				case TitleId.GS1:
					GS1_status_effect_case3(global_work, status_work);
					break;
				case TitleId.GS2:
					GS2_status_effect_case3(global_work, status_work);
					break;
				case TitleId.GS3:
					GS3_status_effect_case3(global_work, status_work);
					break;
				}
			}
			else
			{
				global_work.timer++;
			}
			break;
		case 4:
			itemPlateCtrl.instance.Load_sStatusEffectSprite();
			activeMessageWork.Item_zoom = 256;
			global_work.r.no_2 = 5;
			break;
		case 5:
		{
			activeMessageWork.Item_zoom -= 8;
			if (activeMessageWork.Item_zoom < 5)
			{
				itemPlateCtrl.instance.Terminate_sStatusEffectSprite();
				global_work.r.no_2 = 6;
				break;
			}
			float num = (float)activeMessageWork.Item_zoom / 256f;
			float num2 = 1f - num;
			float z = 540f * num2;
			float num3 = num;
			itemPlateCtrl.instance.sStatusEffectSprite.transform.localRotation = Quaternion.Euler(0f, 0f, z);
			itemPlateCtrl.instance.sStatusEffectSprite.transform.localScale = new Vector3(num3, num3, 1f);
			break;
		}
		case 6:
		{
			global_work.Mess_move_flag = 1;
			GSStatic.message_work_.message_trans_flag = 1;
			status_work.page_status &= 223;
			uint now_no = GSStatic.message_work_.now_no;
			GSStatic.message_work_.status |= MessageSystem.Status.NO_TALKENDFLG;
			advCtrl.instance.message_system_.SetMessage(now_no);
			global_work.r.CopyFrom(ref global_work.r_bk);
			break;
		}
		}
	}

	private static void StatusSubWindowWait(GlobalWork global_work, StatusWork status_work)
	{
	}

	public static void Tukitukeru()
	{
		padCtrl instance = padCtrl.instance;
		StatusWork status_work_ = GSStatic.status_work_;
		GlobalWork global_work_ = GSStatic.global_work_;
		MessageWork activeMessageWork = MessageSystem.GetActiveMessageWork();
		if (global_work_.r.no_3 == 1)
		{
			if (!recordListCtrl.instance.is_select)
			{
				return;
			}
			advCtrl.instance.sub_window_.SetReq(SubWindow.Req.STATUS_TUKITUKERU);
			if (GSStatic.global_work_.title == TitleId.GS3 && (global_work_.status_flag & 0x100) == 0 && (long)bgCtrl.instance.bg_no != 6)
			{
				AnimationSystem.Instance.CharacterAnimationObject.gameObject.SetActive(true);
				bgCtrl.instance.SetSprite(6);
			}
			if ((global_work_.status_flag & 0x200u) != 0)
			{
				global_work_.r.Set(8, 7, 0, 0);
				activeMessageWork.message_trans_flag = 0;
			}
			else
			{
				if ((global_work_.status_flag & 0x100u) != 0)
				{
					switch (GSStatic.global_work_.title)
					{
					case TitleId.GS1:
						Balloon.PlayTakeThat();
						break;
					case TitleId.GS2:
						GS2_status_tukitukeru_KyouseiShiteki();
						break;
					case TitleId.GS3:
						GS3_status_tukitukeru_KyouseiShiteki();
						break;
					}
				}
				else
				{
					TitleId title = GSStatic.global_work_.title;
					if (title == TitleId.GS1 || title == TitleId.GS2 || title == TitleId.GS3)
					{
						Balloon.PlayObjection();
					}
				}
				fadeCtrl.instance.play(3u, 1u, 4u);
				GSStatic.saiban_work_.wait_timer = 10;
				global_work_.Mess_move_flag = 0;
				activeMessageWork.message_trans_flag = 0;
				if ((global_work_.status_flag & (true ? 1u : 0u)) != 0 || activeMessageWork.message_type == WindowType.MAIN)
				{
				}
				global_work_.status_flag |= 1u;
				uint num = CheckMujyun(GSStatic.message_work_.now_no, (uint)recordListCtrl.instance.selectNoteID());
				if (num != 0)
				{
					switch (GSStatic.global_work_.title)
					{
					case TitleId.GS1:
						GS1_status_tukitukeru_Mujyun(global_work_, num);
						break;
					case TitleId.GS2:
						GS2_status_tukitukeru_Mujyun(global_work_, num);
						break;
					case TitleId.GS3:
						GS3_status_tukitukeru_Mujyun(global_work_, num);
						break;
					}
				}
				else
				{
					switch (GSStatic.global_work_.title)
					{
					case TitleId.GS1:
						GS1_status_tukitukeru_NotMujyun(global_work_);
						break;
					case TitleId.GS2:
						GS2_status_tukitukeru_NotMujyun(global_work_);
						break;
					case TitleId.GS3:
						GS3_status_tukitukeru_NotMujyun(global_work_);
						break;
					}
				}
				global_work_.r.Set(7, 5, 0, 0);
			}
			messageBoardCtrl.instance.board(false, false);
			global_work_.status_flag &= 4294966527u;
		}
		else
		{
			if (global_work_.r.no_3 != 2 || !recordListCtrl.instance.is_select)
			{
				return;
			}
			if (global_work_.psy_menu_active_flag == 3)
			{
				global_work_.r.Set(8, 7, 0, 0);
				status_work_.page_status |= 16;
				if (GSPsylock.is_psylock_correct_item(global_work_.psylock[global_work_.psy_no], (ushort)recordListCtrl.instance.selectNoteID()) != -1)
				{
					if (GSStatic.global_work_.title == TitleId.GS3)
					{
						lifeGaugeCtrl.instance.lifegauge_set_move(3);
						global_work_.gauge_dmg_cnt = 0;
					}
					soundCtrl.instance.AllStopBGM();
				}
				else if (GSStatic.global_work_.title == TitleId.GS3)
				{
					lifeGaugeCtrl.instance.lifegauge_set_move(8);
				}
				advCtrl.instance.sub_window_.SetReq(SubWindow.Req.MAGATAMA_TUKITUKE2);
			}
			else if (IsMagatama((byte)recordListCtrl.instance.selectNoteID()) && GSPsylock.is_on_psylock_flag_in_room((ushort)global_work_.Room, (ushort)AnimationSystem.Instance.IdlingCharacterMasked) >= 0)
			{
				status_work_.page_status |= 8;
				activeMessageWork.status2 &= ~MessageSystem.Status2.WAIT_PSY;
				if (AnimationSystem.Instance.CharacterAnimationObject.transform.localPosition.x != 0f)
				{
					bgCtrl.instance.Slider();
					activeMessageWork.status2 |= MessageSystem.Status2.WAIT_PSY;
				}
				else
				{
					soundCtrl.instance.FadeOutBGM(30);
				}
				global_work_.r.Set(8, 7, 0, 0);
				advCtrl.instance.sub_window_.SetReq(SubWindow.Req.MAGATAMA_TUKITUKE);
			}
			else
			{
				soundCtrl.instance.PlaySE(43);
				global_work_.r.Set(5, 1, 0, 0);
				advCtrl.instance.sub_window_.SetReq(SubWindow.Req.STATUS_TUKITUKERU);
				messageBoardCtrl.instance.guide_set(false, guideCtrl.GuideType.NO_GUIDE);
				messageBoardCtrl.instance.guide_ctrl.next_guid = guideCtrl.GuideType.HOUTEI;
			}
		}
	}

	public static uint CheckMujyun(uint mess, uint item)
	{
		MessageWork activeMessageWork = MessageSystem.GetActiveMessageWork();
		MUJYUN_CK_DATA[] mujyunCkData = GSScenario.GetMujyunCkData();
		int num = 0;
		while (num < mujyunCkData.Length && mujyunCkData[num].keyhole != 65535)
		{
			if (mujyunCkData[num].sce_flag == 255 || GSFlag.Check(0u, mujyunCkData[num].sce_flag))
			{
				if (activeMessageWork.sc_no != 0)
				{
					if ((mujyunCkData[num].keyhole & 0xF000) == 0)
					{
						num++;
						continue;
					}
				}
				else if ((mujyunCkData[num].keyhole & 0xF000u) != 0)
				{
					num++;
					continue;
				}
				if ((mujyunCkData[num].keyhole & 0xFFF) == mess && (mujyunCkData[num].key == item || mujyunCkData[num].key == 255))
				{
					if (mujyunCkData[num].win_flag != 0)
					{
						GSStatic.message_work_.desk_attack = 0;
					}
					else
					{
						GSStatic.message_work_.desk_attack = 1;
					}
					return mujyunCkData[num].jump;
				}
			}
			num++;
		}
		GSStatic.message_work_.desk_attack = 0;
		return 0u;
	}

	public static uint tantei_show_check(GlobalWork global_work, uint item, byte itorhum)
	{
		SHOW_DATA[] tanteiShowData = GSScenario.GetTanteiShowData();
		int i = 0;
		uint mess_false = tanteiShowData[i].mess_false;
		if (GSStatic.global_work_.title == TitleId.GS1)
		{
			for (; tanteiShowData[i].end != 255; i++)
			{
				if (AnimationSystem.Instance.IdlingCharacterMasked == tanteiShowData[i].pl_id && global_work.Room == tanteiShowData[i].room)
				{
					mess_false = tanteiShowData[i].mess_false;
					if (item == tanteiShowData[i].item)
					{
						return tanteiShowData[i].mess_true;
					}
				}
			}
			return mess_false;
		}
		for (; tanteiShowData[i].end != 255 && tanteiShowData[i].room != global_work.Room; i++)
		{
		}
		for (; tanteiShowData[i].room == global_work.Room; i++)
		{
			if (AnimationSystem.Instance.IdlingCharacterMasked == tanteiShowData[i].pl_id && itorhum == tanteiShowData[i].end && (global_work.roomseq[global_work.Room] == tanteiShowData[i].roomseq || tanteiShowData[i].roomseq == 255))
			{
				if (item == tanteiShowData[i].item || tanteiShowData[i].item == 255)
				{
					return tanteiShowData[i].mess_true;
				}
				mess_false = tanteiShowData[i].mess_false;
			}
		}
		return mess_false;
	}

	private static void GS1_status_tukitukeru_Mujyun(GlobalWork global_work, uint ret)
	{
		soundCtrl.instance.AllStopBGM();
		advCtrl.instance.message_system_.SetMessage(ret);
		uint num = 4u;
		switch (global_work.scenario)
		{
		case 19:
			if (ret == 151 || ret == 170)
			{
				num = 7u;
			}
			break;
		case 20:
			if (ret == 189 || ret == 188)
			{
				num = 7u;
			}
			break;
		case 21:
			if (ret == 169)
			{
				num = 7u;
			}
			break;
		case 25:
			if (ret == 155)
			{
				num = 7u;
			}
			break;
		case 32:
			if (ret == 144)
			{
				num = 7u;
			}
			break;
		}
		global_work.r_bk.Set((byte)num, 1, 0, 0);
	}

	private static void GS1_status_tukitukeru_NotMujyun(GlobalWork global_work)
	{
		uint now_no = GSStatic.message_work_.now_no;
		if ((GSStatic.message_work_.status & MessageSystem.Status.NEXT_PULS) != 0)
		{
			now_no++;
			advCtrl.instance.message_system_.SetMessage(now_no);
		}
		else
		{
			switch ((uint)(GSUtility.Rnd() & 3))
			{
			case 0u:
				advCtrl.instance.message_system_.SetMessage(scenario.SYS_M0100);
				GSStatic.message_work_.desk_attack = 1;
				break;
			case 1u:
				advCtrl.instance.message_system_.SetMessage(scenario.SYS_M0110);
				GSStatic.message_work_.desk_attack = 1;
				break;
			case 2u:
				advCtrl.instance.message_system_.SetMessage(scenario.SYS_M0120);
				break;
			case 3u:
				advCtrl.instance.message_system_.SetMessage(scenario.SYS_M0130);
				break;
			}
			GSStatic.message_work_.next_no = (ushort)now_no;
		}
		GSStatic.message_work_.status &= ~MessageSystem.Status.NEXT_PULS;
		global_work.r_bk.Set(4, 1, 0, 0);
	}

	private static void GS1_status_effect_case3(GlobalWork global_work, StatusWork status_work)
	{
		MessageWork activeMessageWork = MessageSystem.GetActiveMessageWork();
		global_work.Mess_move_flag = 1;
		activeMessageWork.message_trans_flag = 1;
		uint num = CheckMujyun(GSStatic.message_work_.now_no, (uint)recordListCtrl.instance.selectNoteID());
		if (num != 0)
		{
			advCtrl.instance.message_system_.SetMessage(num);
		}
		else
		{
			uint now_no = GSStatic.message_work_.now_no;
			if ((GSStatic.message_work_.status & MessageSystem.Status.NEXT_PULS) != 0)
			{
				now_no++;
				advCtrl.instance.message_system_.SetMessage(now_no);
			}
			else
			{
				switch ((uint)(GSUtility.Rnd() & 3))
				{
				case 0u:
					advCtrl.instance.message_system_.SetMessage(scenario.SYS_M0100);
					break;
				case 1u:
					advCtrl.instance.message_system_.SetMessage(scenario.SYS_M0110);
					break;
				case 2u:
					advCtrl.instance.message_system_.SetMessage(scenario.SYS_M0120);
					break;
				case 3u:
					advCtrl.instance.message_system_.SetMessage(scenario.SYS_M0130);
					break;
				}
				GSStatic.message_work_.next_no = (ushort)now_no;
			}
			GSStatic.message_work_.status &= ~MessageSystem.Status.NEXT_PULS;
		}
		global_work.r.CopyFrom(ref global_work.r_bk);
	}

	private static bool GS1_IsMagatama(byte item_id)
	{
		return false;
	}

	private static void GS2_status_tukitukeru_Mujyun(GlobalWork global_work, uint ret)
	{
		soundCtrl.instance.AllStopBGM();
		advCtrl.instance.message_system_.SetMessage(ret);
		uint num = 4u;
		global_work.r_bk.Set((byte)num, 1, 0, 0);
	}

	private static void GS2_status_tukitukeru_NotMujyun(GlobalWork global_work)
	{
		uint now_no = GSStatic.message_work_.now_no;
		if ((GSStatic.message_work_.status & MessageSystem.Status.NEXT_PULS) != 0)
		{
			now_no++;
			advCtrl.instance.message_system_.SetMessage(now_no);
		}
		else
		{
			switch ((uint)(GSUtility.Rnd() & 3))
			{
			case 0u:
				advCtrl.instance.message_system_.SetMessage(scenario_GS2.SYS_M0100);
				GSStatic.message_work_.desk_attack = 1;
				break;
			case 1u:
				advCtrl.instance.message_system_.SetMessage(scenario_GS2.SYS_M0110);
				GSStatic.message_work_.desk_attack = 1;
				break;
			case 2u:
				advCtrl.instance.message_system_.SetMessage(scenario_GS2.SYS_M0120);
				break;
			case 3u:
				advCtrl.instance.message_system_.SetMessage(scenario_GS2.SYS_M0130);
				break;
			}
			GSStatic.message_work_.next_no = (ushort)now_no;
		}
		GSStatic.message_work_.status &= ~MessageSystem.Status.NEXT_PULS;
		global_work.r_bk.Set(4, 1, 0, 0);
	}

	private static void GS2_status_effect_case3(GlobalWork global_work, StatusWork status_work)
	{
		MessageWork activeMessageWork = MessageSystem.GetActiveMessageWork();
		if ((status_work.page_status & 8u) != 0)
		{
			status_work.page_status &= 247;
			global_work.psy_no = (sbyte)GSPsylock.is_on_psylock_flag_in_room((ushort)global_work.Room, (ushort)AnimationSystem.Instance.IdlingCharacterMasked);
			global_work.r.Set(5, 11, 0, 0);
			return;
		}
		if ((status_work.page_status & 0x10u) != 0)
		{
			status_work.page_status &= 239;
			int num = GSPsylock.is_psylock_correct_item(global_work.psylock[global_work.psy_no], (ushort)recordListCtrl.instance.selectNoteID());
			if (num >= 0)
			{
				global_work.psylock[global_work.psy_no].correct_message = global_work.psylock[global_work.psy_no].item_correct_message[num];
				advCtrl.instance.message_system_.SetMessage(global_work.psylock[global_work.psy_no].correct_message);
			}
			else
			{
				advCtrl.instance.message_system_.SetMessage(global_work.psylock[global_work.psy_no].wrong_message);
			}
			MessageSystem.Mess_window_set(3u);
			global_work.r.Set(5, 11, 3, 0);
			return;
		}
		if ((status_work.page_status & 0x20u) != 0)
		{
			global_work.r.no_2 = 4;
			return;
		}
		global_work.Mess_move_flag = 1;
		activeMessageWork.message_trans_flag = 1;
		uint num2 = CheckMujyun(GSStatic.message_work_.now_no, (uint)recordListCtrl.instance.selectNoteID());
		if (num2 != 0)
		{
			advCtrl.instance.message_system_.SetMessage(num2);
		}
		else
		{
			uint now_no = GSStatic.message_work_.now_no;
			if ((GSStatic.message_work_.status & MessageSystem.Status.NEXT_PULS) != 0)
			{
				now_no++;
				advCtrl.instance.message_system_.SetMessage(now_no);
			}
			else
			{
				switch ((uint)(GSUtility.Rnd() & 3))
				{
				case 0u:
					advCtrl.instance.message_system_.SetMessage(scenario_GS2.SYS_M0100);
					break;
				case 1u:
					advCtrl.instance.message_system_.SetMessage(scenario_GS2.SYS_M0110);
					break;
				case 2u:
					advCtrl.instance.message_system_.SetMessage(scenario_GS2.SYS_M0120);
					break;
				case 3u:
					advCtrl.instance.message_system_.SetMessage(scenario_GS2.SYS_M0130);
					break;
				}
				GSStatic.message_work_.next_no = (ushort)now_no;
			}
			GSStatic.message_work_.status &= ~MessageSystem.Status.NEXT_PULS;
		}
		global_work.r.CopyFrom(ref global_work.r_bk);
	}

	private static void GS2_status_tukitukeru_KyouseiShiteki()
	{
		Balloon.PlayTakeThat();
		messageBoardCtrl.instance.board(false, false);
	}

	private static bool GS2_IsMagatama(byte item_id)
	{
		if (item_id == scenario_GS2.NOTE_JEWEL2)
		{
			return true;
		}
		return false;
	}

	private static void GS2_status_effect_case0()
	{
		GS2_status_tukitukeru_KyouseiShiteki();
	}

	private static void GS3_status_tukitukeru_Mujyun(GlobalWork global_work, uint ret)
	{
		if (lifeGaugeCtrl.instance.is_lifegauge_disp())
		{
			lifeGaugeCtrl.instance.lifegauge_set_move(3);
			GSStatic.global_work_.gauge_dmg_cnt = 0;
		}
		soundCtrl.instance.AllStopBGM();
		advCtrl.instance.message_system_.SetMessage(ret);
		uint num = 4u;
		global_work.r_bk.Set((byte)num, 1, 0, 0);
	}

	private static void GS3_status_tukitukeru_NotMujyun(GlobalWork global_work)
	{
		if (lifeGaugeCtrl.instance.is_lifegauge_disp())
		{
			lifeGaugeCtrl.instance.lifegauge_set_move(8);
		}
		uint now_no = GSStatic.message_work_.now_no;
		if ((GSStatic.message_work_.status & MessageSystem.Status.NEXT_PULS) != 0)
		{
			now_no++;
			advCtrl.instance.message_system_.SetMessage(now_no);
		}
		else if (global_work.scenario == 0 || global_work.scenario == 1)
		{
			switch ((uint)(GSUtility.Rnd() & 3))
			{
			default:
				advCtrl.instance.message_system_.SetMessage(scenario_GS3.SYS_C0100);
				GSStatic.message_work_.desk_attack = 1;
				break;
			case 1u:
				advCtrl.instance.message_system_.SetMessage(scenario_GS3.SYS_C0110);
				break;
			case 2u:
				advCtrl.instance.message_system_.SetMessage(scenario_GS3.SYS_C0120);
				break;
			case 3u:
				advCtrl.instance.message_system_.SetMessage(scenario_GS3.SYS_C0130);
				GSStatic.message_work_.desk_attack = 1;
				break;
			}
			GSStatic.message_work_.next_no = (ushort)now_no;
		}
		else if (global_work.scenario == 12 || global_work.scenario == 13)
		{
			switch ((uint)(GSUtility.Rnd() & 3))
			{
			default:
				advCtrl.instance.message_system_.SetMessage(scenario_GS3.SYS_C0101);
				GSStatic.message_work_.desk_attack = 1;
				break;
			case 1u:
				advCtrl.instance.message_system_.SetMessage(scenario_GS3.SYS_C0111);
				break;
			case 2u:
				advCtrl.instance.message_system_.SetMessage(scenario_GS3.SYS_C0121);
				break;
			}
			GSStatic.message_work_.next_no = (ushort)now_no;
		}
		else if (global_work.scenario == 16 || global_work.scenario == 17)
		{
			switch ((uint)(GSUtility.Rnd() & 3))
			{
			default:
				advCtrl.instance.message_system_.SetMessage(scenario_GS3.SYS_M0101);
				break;
			case 1u:
				advCtrl.instance.message_system_.SetMessage(scenario_GS3.SYS_M0111);
				break;
			case 2u:
				advCtrl.instance.message_system_.SetMessage(scenario_GS3.SYS_M0121);
				break;
			}
			GSStatic.message_work_.next_no = (ushort)now_no;
		}
		else
		{
			switch ((uint)(GSUtility.Rnd() & 3))
			{
			case 0u:
				advCtrl.instance.message_system_.SetMessage(scenario_GS3.SYS_M0100);
				GSStatic.message_work_.desk_attack = 1;
				break;
			case 1u:
				advCtrl.instance.message_system_.SetMessage(scenario_GS3.SYS_M0110);
				GSStatic.message_work_.desk_attack = 1;
				break;
			case 2u:
				advCtrl.instance.message_system_.SetMessage(scenario_GS3.SYS_M0120);
				break;
			case 3u:
				advCtrl.instance.message_system_.SetMessage(scenario_GS3.SYS_M0130);
				break;
			}
			GSStatic.message_work_.next_no = (ushort)now_no;
		}
		GSStatic.message_work_.status &= ~MessageSystem.Status.NEXT_PULS;
		global_work.r_bk.Set(4, 1, 0, 0);
	}

	private static void GS3_status_effect_case3(GlobalWork global_work, StatusWork status_work)
	{
		MessageWork activeMessageWork = MessageSystem.GetActiveMessageWork();
		if ((status_work.page_status & 8u) != 0)
		{
			status_work.page_status &= 247;
			global_work.psy_no = (sbyte)GSPsylock.is_on_psylock_flag_in_room((ushort)global_work.Room, (ushort)AnimationSystem.Instance.IdlingCharacterMasked);
			global_work.r.Set(5, 11, 0, 0);
			return;
		}
		if ((status_work.page_status & 0x10u) != 0)
		{
			status_work.page_status &= 239;
			int num = GSPsylock.is_psylock_correct_item(global_work.psylock[global_work.psy_no], (ushort)recordListCtrl.instance.selectNoteID());
			if (num >= 0)
			{
				global_work.psylock[global_work.psy_no].correct_message = global_work.psylock[global_work.psy_no].item_correct_message[num];
				advCtrl.instance.message_system_.SetMessage(global_work.psylock[global_work.psy_no].correct_message);
			}
			else
			{
				advCtrl.instance.message_system_.SetMessage(global_work.psylock[global_work.psy_no].wrong_message);
			}
			MessageSystem.Mess_window_set(3u);
			global_work.r.Set(5, 11, 3, 0);
			return;
		}
		global_work.Mess_move_flag = 1;
		activeMessageWork.message_trans_flag = 1;
		uint num2 = CheckMujyun(GSStatic.message_work_.now_no, (uint)recordListCtrl.instance.selectNoteID());
		if (num2 != 0)
		{
			if (lifeGaugeCtrl.instance.is_lifegauge_disp())
			{
				lifeGaugeCtrl.instance.lifegauge_set_move(3);
				GSStatic.global_work_.gauge_dmg_cnt = 0;
			}
			advCtrl.instance.message_system_.SetMessage(num2);
		}
		else
		{
			if (lifeGaugeCtrl.instance.is_lifegauge_disp())
			{
				lifeGaugeCtrl.instance.lifegauge_set_move(8);
			}
			uint now_no = GSStatic.message_work_.now_no;
			if ((GSStatic.message_work_.status & MessageSystem.Status.NEXT_PULS) != 0)
			{
				now_no++;
				advCtrl.instance.message_system_.SetMessage(now_no);
			}
			else
			{
				switch ((uint)(GSUtility.Rnd() & 3))
				{
				case 0u:
					advCtrl.instance.message_system_.SetMessage(scenario_GS3.SYS_M0100);
					break;
				case 1u:
					advCtrl.instance.message_system_.SetMessage(scenario_GS3.SYS_M0110);
					break;
				case 2u:
					advCtrl.instance.message_system_.SetMessage(scenario_GS3.SYS_M0120);
					break;
				case 3u:
					advCtrl.instance.message_system_.SetMessage(scenario_GS3.SYS_M0130);
					break;
				}
				GSStatic.message_work_.next_no = (ushort)now_no;
			}
			GSStatic.message_work_.status &= ~MessageSystem.Status.NEXT_PULS;
		}
		global_work.r.CopyFrom(ref global_work.r_bk);
	}

	private static void GS3_status_tukitukeru_KyouseiShiteki()
	{
		Balloon.PlayTakeThat();
	}

	private static bool GS3_IsMagatama(byte item_id)
	{
		if (item_id == scenario_GS3.NOTE_JEWEL || item_id == scenario_GS3.NOTE_JEWEL2 || item_id == scenario_GS3.NOTE_JEWEL3)
		{
			return true;
		}
		return false;
	}

	private static void GS3_status_effect_case0()
	{
		GS3_status_tukitukeru_KyouseiShiteki();
	}

	private static void st_eff_meswin_set()
	{
		MessageWork activeMessageWork = MessageSystem.GetActiveMessageWork();
		activeMessageWork.message_trans_flag = 0;
		if (activeMessageWork.message_type != 0)
		{
		}
	}

	private static bool IsMagatama(byte item_id)
	{
		switch (GSStatic.global_work_.title)
		{
		case TitleId.GS1:
			return GS1_IsMagatama(item_id);
		case TitleId.GS2:
			return GS2_IsMagatama(item_id);
		case TitleId.GS3:
			return GS3_IsMagatama(item_id);
		default:
			return false;
		}
	}
}
