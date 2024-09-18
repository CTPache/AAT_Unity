using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class MessageSystem
{
	[Flags]
	public enum Status : uint
	{
		RT_WAIT = 1u,
		RT_GO = 2u,
		SELECT = 4u,
		LOOP = 8u,
		NEXT_PULS = 0x10u,
		RT_END_WAIT = 0x20u,
		OBJ_MOSAIC = 0x40u,
		POINT_TO_START = 0x80u,
		POINT_TO = 0x100u,
		POINT_TO_1ST = 0x200u,
		POINT_CURSOL_ON = 0x400u,
		MOJI_SEMITRANS2 = 0x1000u,
		READ_MESSAGE = 0x800u,
		FAST_MESSAGE = 0x2000u,
		NEXT_MESSAGE = 0x4000u,
		LAST_MESSAGE = 0x8000u,
		FADE_OK = 0x10000u,
		NO_FADE = 0x20000u,
		NO_TALKENDFLG = 0x40000u,
		THREE_LINE = 0x80000u,
		CODE_SKIP = 0x80000000u
	}

	[Flags]
	public enum Status2
	{
		MOSAIC_MONO = 1,
		BG0_PROTECT = 2,
		PALESC_SP = 4,
		RESTORE_PSY = 8,
		BG2_H8 = 0x10,
		BG2_V8 = 0x20,
		WAIT_PSY = 0x40,
		QUAKE_F = 0x80,
		PSY_STOP_BREAK = 0x100,
		MV_MONO = 0x200,
		MV_BLACK = 0x400,
		STOP_EXPL = 0x800,
		MOSAIC_FLAG = 0x8000,
		WIN_NOCLR = 0x4000,
		MV_WHITE = 0x1000,
		DISABLE_FONT_ALPHA = 0x10000
	}

	[Flags]
	public enum TextFlag
	{
		POSITION_MASK = 0xF,
		POSITION_CENTER = 1,
		POSITION_CENTER_UP = 2,
		SIZE_MASK = 0xF0,
		SIZE_ZOOM = 0x10,
		GRADATION = 0x20
	}

	public enum ControlCode : ushort
	{
		CODE_00 = 0,
		CODE_01 = 1,
		CODE_02 = 2,
		CODE_03 = 3,
		CODE_04 = 4,
		CODE_05 = 5,
		CODE_06 = 6,
		CODE_07 = 7,
		CODE_08 = 8,
		CODE_09 = 9,
		CODE_0A = 10,
		CODE_0B = 11,
		CODE_0C = 12,
		CODE_0D = 13,
		CODE_0E = 14,
		CODE_0F = 15,
		CODE_10 = 16,
		CODE_11 = 17,
		CODE_12 = 18,
		CODE_13 = 19,
		CODE_14 = 20,
		CODE_15 = 21,
		CODE_16 = 22,
		CODE_17 = 23,
		CODE_18 = 24,
		CODE_19 = 25,
		CODE_1A = 26,
		CODE_1B = 27,
		CODE_1C = 28,
		CODE_1D = 29,
		CODE_1E = 30,
		CODE_1F = 31,
		CODE_20 = 32,
		CODE_21 = 33,
		CODE_22 = 34,
		CODE_23 = 35,
		CODE_24 = 36,
		CODE_25 = 37,
		CODE_26 = 38,
		CODE_27 = 39,
		CODE_28 = 40,
		CODE_29 = 41,
		CODE_2A = 42,
		CODE_2B = 43,
		CODE_2C = 44,
		CODE_2D = 45,
		CODE_2E = 46,
		CODE_2F = 47,
		CODE_30 = 48,
		CODE_31 = 49,
		CODE_32 = 50,
		CODE_33 = 51,
		CODE_34 = 52,
		CODE_35 = 53,
		CODE_36 = 54,
		CODE_37 = 55,
		CODE_38 = 56,
		CODE_39 = 57,
		CODE_3A = 58,
		CODE_3B = 59,
		CODE_3C = 60,
		CODE_3D = 61,
		CODE_3E = 62,
		CODE_3F = 63,
		CODE_40 = 64,
		CODE_41 = 65,
		CODE_42 = 66,
		CODE_43 = 67,
		CODE_44 = 68,
		CODE_45 = 69,
		CODE_46 = 70,
		CODE_47 = 71,
		CODE_48 = 72,
		CODE_49 = 73,
		CODE_4A = 74,
		CODE_4B = 75,
		CODE_4C = 76,
		CODE_4D = 77,
		CODE_4E = 78,
		CODE_4F = 79,
		CODE_50 = 80,
		CODE_51 = 81,
		CODE_52 = 82,
		CODE_53 = 83,
		CODE_54 = 84,
		CODE_55 = 85,
		CODE_56 = 86,
		CODE_57 = 87,
		CODE_58 = 88,
		CODE_59 = 89,
		CODE_5A = 90,
		CODE_5B = 91,
		CODE_5C = 92,
		CODE_5D = 93,
		CODE_5E = 94,
		CODE_5F = 95,
		CODE_60 = 96,
		CODE_61 = 97,
		CODE_62 = 98,
		CODE_63 = 99,
		CODE_64 = 100,
		CODE_65 = 101,
		CODE_66 = 102,
		CODE_67 = 103,
		CODE_68 = 104,
		CODE_69 = 105,
		CODE_6A = 106,
		CODE_6B = 107,
		CODE_6C = 108,
		CODE_6D = 109,
		CODE_6E = 110,
		CODE_6F = 111,
		CODE_70 = 112,
		CODE_71 = 113,
		CODE_72 = 114,
		CODE_73 = 115,
		CODE_74 = 116,
		CODE_75 = 117,
		CODE_76 = 118,
		CODE_77 = 119,
		CODE_78 = 120,
		CODE_79 = 121,
		CODE_7A = 122,
		CODE_7B = 123,
		CODE_7C = 124,
		CODE_7D = 125,
		CODE_7E = 126,
		CODE_7F = 127
	}

	[Flags]
	public enum SoundFlag
	{
		SE_OFF = 1,
		BGM_OFF = 2,
		MSE_OFF = 4
	}

	private delegate bool CodeProc(MessageWork message_work);

	public enum TouchStatus
	{
		None = 0,
		Left = 1,
		Right = 2
	}

	public const int DEFAULT_MESSAGE_SPEED = 3;

	private const int MESSAGE_LINE_CHARACTER_MAX = 32;

	private const int MESSAGE_SE_CHARACTER_COUNT_JAPAN = 1;

	private const int MESSAGE_SE_CHARACTER_COUNT_USA = 2;

	private const int EVENT_MESSAGE_OFFSET = 256;

	public const int ADD_SC_QUES = 61440;

	public const ushort MESSAGE_OFFSET = 128;

	public const uint START_MESSAGE = 128u;

	public const ushort CONTROL_CODE_OFFSET = 128;

	public const int RE_WAIT_TIME = 10;

	private static bool is_mosaic_run_;

	[SerializeField]
	private RectTransform text_area_;

	[SerializeField]
	private Text[] ui_texts_ = new Text[2];

	[SerializeField]
	private MessageWork active_work_;

	[SerializeField]
	private uint debug_skip_mdt_index_;

	[SerializeField]
	private uint debug_mdt_index_;

	[SerializeField]
	private ushort debug_next_no_;

	public GameObject psylock_destroy_effect_prefab_;

	[SerializeField]
	private bool debug_no_key_wait_;

	private static readonly CodeProc[] code_proc_table;

	public static readonly ushort[] code_proc_arg_count_table;

	private static readonly byte[] usa_message_time_table;

	private static int skip_time;

	private static bool is_end_;

	private bool debug_skip_;

	public Text[] ui_texts
	{
		get
		{
			return ui_texts_;
		}
	}

	public uint debug_skip_mdt_index
	{
		get
		{
			return debug_skip_mdt_index_;
		}
		set
		{
			debug_skip_mdt_index_ = value;
		}
	}

	public uint debug_mdt_index
	{
		get
		{
			return debug_mdt_index_;
		}
		set
		{
			debug_mdt_index_ = value;
		}
	}

	public bool is_end
	{
		get
		{
			return is_end_;
		}
		set
		{
			is_end_ = value;
		}
	}

	public bool debug_skip
	{
		get
		{
			return debug_skip_;
		}
		set
		{
			debug_skip_ = value;
		}
	}

	static MessageSystem()
	{
		code_proc_table = new CodeProc[128]
		{
			CodeProc_00, CodeProc_01, CodeProc_02, CodeProc_03, CodeProc_04, CodeProc_05, CodeProc_06, CodeProc_02, CodeProc_08, CodeProc_09,
			CodeProc_02, CodeProc_0b, CodeProc_0c, CodeProc_0d, CodeProc_0e, CodeProc_0f, CodeProc_10, CodeProc_11, CodeProc_12, CodeProc_13,
			CodeProc_14, CodeProc_15, CodeProc_16, CodeProc_17, CodeProc_18, CodeProc_19, CodeProc_1a, CodeProc_1b, CodeProc_1c, CodeProc_1d,
			CodeProc_1e, CodeProc_1f, CodeProc_20, CodeProc_21, CodeProc_22, CodeProc_23, CodeProc_24, CodeProc_25, CodeProc_26, CodeProc_27,
			CodeProc_28, CodeProc_29, CodeProc_2a, CodeProc_2b, CodeProc_2c, CodeProc_02, CodeProc_2e, CodeProc_2f, CodeProc_30, CodeProc_31,
			CodeProc_32, CodeProc_33, CodeProc_34, CodeProc_35, CodeProc_36, CodeProc_37, CodeProc_38, CodeProc_39, CodeProc_3a, CodeProc_3b,
			CodeProc_3c, CodeProc_3d, CodeProc_3e, CodeProc_3f, CodeProc_40, CodeProc_41, CodeProc_42, CodeProc_43, CodeProc_44, CodeProc_15,
			CodeProc_46, CodeProc_47, CodeProc_48, CodeProc_49, CodeProc_4a, CodeProc_4b, CodeProc_4c, CodeProc_4d, CodeProc_4e, CodeProc_4f,
			CodeProc_50, CodeProc_51, CodeProc_52, CodeProc_53, CodeProc_54, CodeProc_55, CodeProc_56, CodeProc_57, CodeProc_58, CodeProc_59,
			CodeProc_5a, CodeProc_5b, CodeProc_5c, CodeProc_5d, CodeProc_5e, CodeProc_5f, CodeProc_60, CodeProc_61, CodeProc_62, CodeProc_63,
			CodeProc_64, CodeProc_65, CodeProc_66, CodeProc_67, CodeProc_68, CodeProc_69, CodeProc_6a, CodeProc_6b, CodeProc_6c, CodeProc_6d,
			CodeProc_6e, CodeProc_6f, CodeProc_70, CodeProc_71, CodeProc_Dummy, CodeProc_Dummy, CodeProc_74, CodeProc_75, CodeProc_76, CodeProc_77,
			CodeProc_36, CodeProc_15, CodeProc_7a, CodeProc_7b, CodeProc_7c, CodeProc_7d, CodeProc_7e, CodeProc_7f
		};
		code_proc_arg_count_table = new ushort[128]
		{
			0, 0, 0, 1, 1, 2, 2, 0, 2, 3,
			1, 1, 1, 0, 1, 2, 1, 0, 3, 1,
			0, 0, 0, 1, 1, 2, 4, 1, 1, 1,
			3, 0, 1, 0, 2, 2, 0, 1, 1, 2,
			1, 1, 3, 0, 1, 0, 0, 2, 1, 2,
			2, 5, 1, 2, 1, 2, 1, 1, 3, 2,
			1, 1, 1, 0, 0, 0, 1, 1, 1, 0,
			1, 2, 2, 0, 1, 1, 0, 2, 1, 7,
			1, 2, 1, 0, 2, 1, 2, 1, 0, 1,
			1, 2, 3, 0, 0, 3, 4, 3, 0, 0,
			1, 2, 3, 0, 0, 4, 1, 3, 0, 1,
			1, 1, 3, 3, 0, 0, 2, 4, 2, 2,
			1, 0, 1, 2, 0, 1, 1, 1
		};
		usa_message_time_table = new byte[16]
		{
			0, 1, 1, 2, 2, 3, 3, 4, 4, 5,
			5, 6, 6, 7, 7, 8
		};
	}

	public void Initialize()
	{
		GSStatic.message_work_.message_type = WindowType.MAIN;
		GSStatic.message_work_2_.message_type = WindowType.SUB;
		GSStatic.message_work_.game_over = false;
		GSStatic.message_work_2_.game_over = false;
		for (int i = 0; i < GSStatic.obj_work_.Length; i++)
		{
			GSStatic.obj_work_[i] = new ObjWork();
		}
		for (int j = 0; j < GSStatic.inspect_data_.Length; j++)
		{
			GSStatic.inspect_data_[j] = new INSPECT_DATA();
		}
		for (int k = 0; k < GSStatic.talk_work_.talk_data_.Length; k++)
		{
			GSStatic.talk_work_.talk_data_[k] = new TALK_DATA(255u, 255u, 255u, 255u, 255u, 255u, 255u, 255u, 255u, 255u, 255u, 255u, 65535u, 65535u, 65535u, 65535u);
		}
	}

	public void end()
	{
		is_end_ = false;
		GSStatic.obj_work_[1].h_num = byte.MaxValue;
		GSStatic.obj_work_[1].foa = ushort.MaxValue;
		GSStatic.obj_work_[1].idlingFOA = ushort.MaxValue;
		GSStatic.global_work_.r.no_0 = 0;
		GSStatic.global_work_.r.no_1 = 0;
		GSStatic.global_work_.r.no_2 = 0;
		GSStatic.global_work_.r.no_3 = 0;
		GSStatic.global_work_.r_bk.no_0 = 0;
		GSStatic.global_work_.r_bk.no_1 = 0;
		GSStatic.global_work_.r_bk.no_2 = 0;
		GSStatic.global_work_.r_bk.no_3 = 0;
		for (int i = 0; i < 8; i++)
		{
			GSStatic.global_work_.sce_flag[i] = 0u;
		}
		for (int j = 0; j < 12; j++)
		{
			GSStatic.global_work_.talk_end_flag[j] = 0u;
		}
		for (int k = 0; k < 5; k++)
		{
			GSStatic.global_work_.bg_first_flag[k] = 0u;
		}
		for (int l = 0; l < 8; l++)
		{
			advCtrl.instance.sub_window_.routine_[l].r.no_0 = 0;
			advCtrl.instance.sub_window_.routine_[l].r.no_1 = 0;
			advCtrl.instance.sub_window_.routine_[l].r.no_2 = 0;
			advCtrl.instance.sub_window_.routine_[l].r.no_3 = 0;
		}
		advCtrl.instance.sub_window_.stack_ = 0;
	}

	public void Reset(bool is_reset)
	{
		if (is_reset)
		{
			GSStatic.message_work_.mdt_index = 0u;
			GSStatic.message_work_2_.mdt_index = 0u;
			GSStatic.global_work_.inspect_readed_ = new byte[2, 1024];
		}
		GSStatic.message_work_.game_over = false;
		GSStatic.message_work_2_.game_over = false;
		for (int i = 0; i < GSStatic.message_work_.now_no_bak.Length; i++)
		{
			GSStatic.message_work_.now_no_bak[i] = ushort.MaxValue;
		}
		for (int j = 0; j < GSStatic.message_work_2_.now_no_bak.Length; j++)
		{
			GSStatic.message_work_2_.now_no_bak[j] = ushort.MaxValue;
		}
		is_end_ = false;
		if (GSStatic.global_work_.title != TitleId.GS3)
		{
			GSScenario.sce_init sceInitProc = GSScenario.GetSceInitProc();
			sceInitProc(GSStatic.global_work_, is_reset);
		}
		GSStatic.message_work_.sound_flag &= ~SoundFlag.MSE_OFF;
		GSStatic.message_work_2_.sound_flag &= ~SoundFlag.MSE_OFF;
	}

	public static void SetActiveMessageWindow(WindowType type)
	{
		GSStatic.global_work_.message_active_window = (byte)type;
	}

	public static MessageWork GetActiveMessageWork()
	{
		return GetMessageWork((WindowType)GSStatic.global_work_.message_active_window);
	}

	public static MessageWork GetMessageWork(WindowType type)
	{
		return (type != 0) ? GSStatic.message_work_2_ : GSStatic.message_work_;
	}

	public static void setInspectTalkEndFlg(uint num, byte scenario)
	{
		if (num >= 1024)
		{
			return;
		}
		if (scenario == 0)
		{
			if (CheckInspectTalkEnd(num))
			{
				GSStatic.global_work_.inspect_readed_[scenario, num] = 1;
			}
		}
		else
		{
			GSStatic.global_work_.inspect_readed_[scenario, num] = 1;
		}
	}

	private static bool CheckInspectTalkEnd(uint num)
	{
		switch (GSStatic.global_work_.title)
		{
		case TitleId.GS1:
			if (GSStatic.global_work_.scenario == 3)
			{
				if (num == scenario.SC1_02210 || num == scenario.SC1_02250 || num == scenario.SC1_02280)
				{
					return false;
				}
				if (num == scenario.SC1_03450)
				{
					GSStatic.global_work_.inspect_readed_[0, scenario.SC1_03600] = 1;
					return false;
				}
			}
			else if (GSStatic.global_work_.scenario == 5)
			{
				if (num == scenario.SC2_01050)
				{
					return false;
				}
			}
			else if (GSStatic.global_work_.scenario == 7)
			{
				if (num == scenario.SC2_00950)
				{
					return false;
				}
			}
			else if (GSStatic.global_work_.scenario == 11)
			{
				if (num == scenario.SC3_00310)
				{
					return false;
				}
			}
			else if (GSStatic.global_work_.scenario == 18)
			{
				if (num == scenario.SC4_60820J)
				{
					return false;
				}
			}
			else if (GSStatic.global_work_.scenario == 24)
			{
				if (num == scenario.SC4_64360J)
				{
					return false;
				}
				if (num == scenario.SC4_64730)
				{
					return false;
				}
			}
			else if (GSStatic.global_work_.scenario == 30)
			{
				if (num == scenario.SC4_67600 && !GSFlag.Check(0u, scenario.SCE44_FLAG_MES67650))
				{
					return false;
				}
				if (num == scenario.SC4_67650)
				{
					GSStatic.global_work_.inspect_readed_[0, scenario.SC4_67600] = 1;
				}
			}
			break;
		}
		return true;
	}

	public void SetMessage(uint no)
	{
		SetMessage2(no, GSStatic.global_work_.message_active_window);
	}

	public void ScienceRecoverySetMessage(uint no)
	{
		SetMessage2(no, GSStatic.global_work_.message_active_window, true);
	}

	public void SetMessage2(uint no, uint type, bool science_recovery = false)
	{
		MessageWork messageWork = GetMessageWork((WindowType)type);
		ushort ev_temp = messageWork.ev_temp;
		if (GSStatic.global_work_.event_flag != 0)
		{
			messageWork.ev_temp = 256;
		}
		else
		{
			messageWork.ev_temp = 0;
		}
		if ((messageWork.status & Status.NO_TALKENDFLG) != 0)
		{
			messageWork.status &= ~Status.NO_TALKENDFLG;
		}
		else if (ev_temp == 0)
		{
		}
		if (!science_recovery)
		{
			TrophyCtrl.check_trophy_by_mes_no();
		}
		messageWork.now_no = (ushort)no;
		Message_init_sub(messageWork, science_recovery);
		messageWork.mdt_index++;
		messageWork.status |= Status.READ_MESSAGE;
		if (!TrophyCtrl.disable_check_trophy_by_mes_no)
		{
			GSStatic.message_work_.enable_message_trophy = true;
		}
		ushort num = messageWork.op_work[7];
		if (num == 65532)
		{
			messageWork.op_para = 101;
			messageWork.all_work[0] = 255;
			GSDemo.DemoProc_Special(messageWork);
			messageWork.op_work[7] = 0;
		}
	}

	public static void Message_init()
	{
		GSStatic.message_work_.sc_no = 0;
	}

	public void Message_main()
	{
		MessageWork activeMessageWork = GetActiveMessageWork();
		if (activeMessageWork.mdt_data != null)
		{
			if (GSStatic.global_work_.Mess_move_flag != 0 && GSStatic.global_work_.r.no_0 != 0 && GSStatic.global_work_.r.no_0 != 2 && GSStatic.global_work_.r.no_0 != 3)
			{
				Message_move(activeMessageWork);
			}
			else if (GSStatic.tantei_work_.tanchiki_demof != 0)
			{
				Message_move(activeMessageWork);
			}
			Message_trans(activeMessageWork);
		}
	}

	private void Message_init_sub(MessageWork message_work, bool science_recovery)
	{
		ClearText(message_work);
		message_work.code = 0;
		message_work.text_flag = (TextFlag)0;
		if (GSUtility.GetLanguageLayoutType(GSStatic.global_work_.language) != 0)
		{
			message_work.message_se_character_count = 2;
		}
		else
		{
			message_work.message_se_character_count = 1;
		}
		message_work.message_se = 0;
		message_work.next_no = (ushort)(message_work.now_no + 1);
		if (!science_recovery)
		{
			message_work.tukkomi_no = 0;
		}
		message_work.tukkomi_flag = 0;
		message_work.speaker_id = 0;
		message_work.status = (Status)0u;
		message_work.message_text.SetColor(0);
		message_work.message_time = 3;
		message_work.message_timer = 0;
		ushort num = message_work.now_no;
		ushort num2 = 0;
		MdtData mdtData;
		if (message_work.now_no >= 128)
		{
			mdtData = GSStatic.mdt_datas_[0];
			num -= 128;
			num2 = 0;
		}
		else
		{
			mdtData = GSStatic.mdt_datas_[1];
			num2 = 1;
		}
		message_work.mdt_datas_index_ = num2;
		if (!science_recovery)
		{
			message_work.mdt_index_top = mdtData.GetMessageOffset(num);
			message_work.mdt_index = message_work.mdt_index_top;
		}
		if (message_work.message_type == WindowType.MAIN && !science_recovery && !scienceInvestigationCtrl.instance.active)
		{
			Expl_init();
		}
	}

	public void Message_move(MessageWork message_work)
	{
		GSDemo.Play(message_work);
		if ((message_work.op_flg & 0xF) == 1 && !DebugSkipMdtIndex(message_work))
		{
			return;
		}
		byte message_time = 0;
		while (message_time == 0 && !lifeGaugeCtrl.instance.is_lifegauge_moving() && (fadeCtrl.instance.status == fadeCtrl.Status.NO_FADE || (message_work.status & Status.FADE_OK) != 0 || DebugSkipMdtIndex(message_work)) && message_work.mess_win_rno != 3)
		{
			if (debug_mdt_index_ != 0)
			{
				message_work.mdt_index = debug_mdt_index_;
				debug_mdt_index_ = 0u;
			}
			if (debug_next_no_ != 0)
			{
				message_work.next_no = debug_next_no_;
				debug_next_no_ = 0;
			}
			NextGuidSet(message_work);
			message_work.code = message_work.mdt_data.GetMessage(message_work.mdt_index);
			if (message_work.code < 128)
			{
				SetScript(message_work);
				if (code_proc_table[message_work.code](message_work))
				{
					break;
				}
				message_time = 0;
				continue;
			}
			if (MessageWait(message_work, ref message_time))
			{
				break;
			}
			message_work.code -= 128;
			if (message_work.message_text.character_count == 0 && (message_work.text_flag & TextFlag.POSITION_CENTER) != 0)
			{
				AdjustCentering(message_work);
			}
			SetCharacter(message_work);
			message_work.mdt_index++;
			if (message_work.code == 12288)
			{
				if (message_time != 0)
				{
					break;
				}
			}
			else if (message_time != 0)
			{
				MessageSE(message_work, message_time);
			}
		}
	}

	private bool MessageWait(MessageWork message_work, ref byte message_time)
	{
		if ((message_work.op_flg & 0xF0) >> 4 != 1)
		{
			message_work.status |= Status.READ_MESSAGE;
			if ((message_work.status & Status.READ_MESSAGE) != 0)
			{
				TouchSystem.instance.UpdateDownEvent();
				if ((padCtrl.instance.GetKey(KeyType.B) && optionCtrl.instance.skip_type != 0) || (padCtrl.instance.GetKeyDown(KeyType.A) && message_work.questioning_message_wait <= 0) || (GetTouchStatus() == TouchStatus.Right && optionCtrl.instance.skip_type != 0))
				{
					message_work.status |= Status.FAST_MESSAGE;
				}
			}
		}
		if (DebugInputSkip(message_work))
		{
			message_work.status |= Status.FAST_MESSAGE;
		}
		message_time = message_work.message_time;
		if ((message_work.status & Status.SELECT) == 0)
		{
			if ((message_work.status & Status.READ_MESSAGE) != 0 && (message_work.status & Status.FAST_MESSAGE) != 0)
			{
				message_time = 0;
			}
			else
			{
				message_work.status &= ~Status.FAST_MESSAGE;
			}
		}
		message_work.status |= Status.LAST_MESSAGE;
		message_work.message_timer++;
		if (DebugInputSkip(message_work))
		{
			message_time = 0;
		}
		if (GSUtility.GetLanguageLayoutType(GSStatic.global_work_.language) != 0)
		{
			if (message_time < 16)
			{
				message_time = usa_message_time_table[message_time];
			}
			if (message_work.message_timer < message_time)
			{
				return true;
			}
		}
		else if (message_work.message_timer < message_time)
		{
			return true;
		}
		message_work.message_timer = 0;
		return false;
	}

	private static void MessageSE(MessageWork message_work, byte message_time)
	{
		bool flag = false;
		if (GSUtility.GetLanguageLayoutType(GSStatic.global_work_.language) != 0)
		{
			if (message_work.message_se_character_count == 0 || (message_time >= 2 && message_work.message_se_character_count <= 1))
			{
				flag = true;
			}
			else
			{
				message_work.message_se_character_count--;
			}
		}
		else if (message_work.message_se_character_count == 0 || message_time >= 5)
		{
			flag = true;
		}
		else
		{
			message_work.message_se_character_count--;
		}
		if (!flag)
		{
			return;
		}
		if (GSUtility.GetLanguageLayoutType(GSStatic.global_work_.language) != 0)
		{
			message_work.message_se_character_count = 2;
		}
		else if (message_work.message_se != 2)
		{
			message_work.message_se_character_count = 1;
		}
		if ((message_work.sound_flag & SoundFlag.MSE_OFF) == 0)
		{
			if (message_work.message_se == 2)
			{
				soundCtrl.instance.PlaySE(68);
			}
			else if (message_work.message_se == 1)
			{
				soundCtrl.instance.PlaySE(46);
			}
			else
			{
				soundCtrl.instance.PlaySE(45);
			}
		}
	}

	private void SetScript(MessageWork message_work)
	{
		ushort code = message_work.code;
		if (message_work.code_no != code)
		{
			if (code != 12 && code != 78)
			{
				message_work.status &= ~Status.LAST_MESSAGE;
			}
			message_work.work = 0;
			message_work.code_no = code;
		}
	}

	private void AdjustCentering(MessageWork message_work)
	{
		if (message_work.message_line >= messageBoardCtrl.instance.line_list.Count)
		{
			return;
		}
		uint num = message_work.mdt_index;
		StringBuilder stringBuilder = new StringBuilder();
		while (true)
		{
			ushort message = message_work.mdt_data.GetMessage(num);
			num++;
			if (message >= 128)
			{
				stringBuilder.Append((char)(message - 128));
				continue;
			}
			if (message == 1 || message == 2 || message == 7 || message == 8 || message == 9 || message == 10 || message == 13 || message == 21 || message == 42 || message == 45 || message == 46 || message == 69)
			{
				break;
			}
			num += code_proc_arg_count_table[message];
		}
		Text text = messageBoardCtrl.instance.line_list[message_work.message_line];
		text.text = EnToHalf(stringBuilder.ToString(), GSStatic.global_work_.language);
		int num2 = (int)text.preferredWidth;
		text.text = string.Empty;
		RectTransform component = text.GetComponent<RectTransform>();
		component.anchoredPosition = new Vector2((component.sizeDelta.x - (float)num2) / 2f, component.anchoredPosition.y);
	}

	private void SetCharacter(MessageWork message_work)
	{
		SetCharacter(message_work, (char)message_work.code);
	}

	private void SetCharacter(MessageWork message_work, char character)
	{
		message_work.message_text.SetUSAFlag(GSUtility.GetLanguageLayoutType(GSStatic.global_work_.language) != Language.JAPAN);
		message_work.message_text.Append(character);
		if (message_work.message_line < messageBoardCtrl.instance.line_list.Count)
		{
			messageBoardCtrl.instance.line_list[message_work.message_line].text = EnToHalf(message_work.message_text.ToString(), GSStatic.global_work_.language);
		}
	}

	private void NextLine(MessageWork message_work)
	{
		message_work.message_line++;
		if (message_work.message_line < messageBoardCtrl.instance.line_list.Count)
		{
			messageBoardCtrl.instance.line_list[message_work.message_line].gameObject.SetActive(true);
		}
		message_work.message_text.Clear();
	}

	private void ClearText(MessageWork message_work)
	{
		message_work.message_text.Clear();
		message_work.message_line = 0;
		for (int i = 0; i < messageBoardCtrl.instance.line_list.Count; i++)
		{
			Text text = messageBoardCtrl.instance.line_list[i];
			text.text = string.Empty;
			RectTransform component = text.GetComponent<RectTransform>();
			component.anchoredPosition = new Vector2(0f, component.anchoredPosition.y);
			text.gameObject.SetActive(false);
		}
		messageBoardCtrl.instance.msg_key_icon.keyIconActiveSet(false);
		messageBoardCtrl.instance.line_list[0].gameObject.SetActive(true);
	}

	private static void Expl_init()
	{
		GSMapIcon.instance.Terminate();
	}

	private static void Message_trans(MessageWork message_work)
	{
		if ((message_work.status & Status.SELECT) != 0 || (message_work.status2 & Status2.STOP_EXPL) != 0)
		{
			return;
		}
		ExplCharData[] expl_char_data_ = GSStatic.expl_char_work_.expl_char_data_;
		for (uint num = 0u; num < expl_char_data_.Length; num++)
		{
			if (expl_char_data_[num].id == byte.MaxValue)
			{
				continue;
			}
			if (expl_char_data_[num].blink != 0)
			{
				expl_char_data_[num].timer++;
				expl_char_data_[num].timer &= 31;
				if (expl_char_data_[num].timer < 16)
				{
					GSMapIcon.instance.SetVisible(expl_char_data_[num].id, false);
				}
				else
				{
					GSMapIcon.instance.SetVisible(expl_char_data_[num].id, true);
				}
			}
			if ((expl_char_data_[num].status & 2) == 0)
			{
				continue;
			}
			if (expl_char_data_[num].move != 4)
			{
				expl_char_data_[num].dot_now += expl_char_data_[num].speed;
				if (expl_char_data_[num].dot_now >= expl_char_data_[num].dot)
				{
					expl_char_data_[num].speed -= (byte)(expl_char_data_[num].dot_now - expl_char_data_[num].dot);
					expl_char_data_[num].status &= 253;
				}
			}
			else
			{
				int num2 = expl_char_data_[num].para1 & 0x1FF;
				int num3 = expl_char_data_[num].para0 & 0xFF;
				num2 += message_work.all_work[0];
				num3 += message_work.all_work[1];
				message_work.all_work[2]--;
				if (message_work.all_work[2] == 0)
				{
					expl_char_data_[num].status &= 253;
				}
				expl_char_data_[num].para0 = (ushort)((expl_char_data_[num].para0 & 0xFF00u) | ((uint)num3 & 0xFFu));
				expl_char_data_[num].para1 = (ushort)((expl_char_data_[num].para1 & 0xFE00u) | ((uint)num2 & 0x1FFu));
			}
			switch (expl_char_data_[num].move)
			{
			case 0:
			{
				int num4 = expl_char_data_[num].para0 & 0xFF;
				expl_char_data_[num].para0 &= 65280;
				num4 -= expl_char_data_[num].speed;
				num4 &= 0xFF;
				expl_char_data_[num].para0 += (ushort)num4;
				break;
			}
			case 1:
			{
				int num4 = expl_char_data_[num].para0 & 0xFF;
				expl_char_data_[num].para0 &= 65280;
				num4 += expl_char_data_[num].speed;
				num4 &= 0xFF;
				expl_char_data_[num].para0 += (ushort)num4;
				break;
			}
			case 2:
			{
				int num4 = expl_char_data_[num].para1 & 0x1FF;
				expl_char_data_[num].para1 &= 65024;
				num4 -= expl_char_data_[num].speed;
				num4 &= 0x1FF;
				expl_char_data_[num].para1 += (ushort)num4;
				break;
			}
			case 3:
			{
				int num4 = expl_char_data_[num].para1 & 0x1FF;
				expl_char_data_[num].para1 &= 65024;
				num4 += expl_char_data_[num].speed;
				num4 &= 0x1FF;
				expl_char_data_[num].para1 += (ushort)num4;
				break;
			}
			}
			int x = expl_char_data_[num].para1 & 0x1FF;
			int y = expl_char_data_[num].para0 & 0xFF;
			GSMapIcon.instance.SetPosition(expl_char_data_[num].id, x, y);
		}
	}

	public void LoadSystemMdtFromStreamingAssets(string path)
	{
		string in_path = Application.streamingAssetsPath + "/" + path;
		byte[] bytes = decryptionCtrl.instance.load(in_path);
		GSStatic.message_work_2_.mdt_path = path;
		GSStatic.mdt_datas_[1] = new MdtData(bytes);
	}

	public void LoadScenarioMdtFromStreamingAssets(string path)
	{
		Debug.Log("LoadScenarioMdtFromStreamingAssets:" + path);
		string in_path = Application.streamingAssetsPath + "/" + path;
		byte[] bytes = decryptionCtrl.instance.load(in_path);
		GSStatic.message_work_.mdt_path = path;
		GSStatic.mdt_datas_[0] = new MdtData(bytes);
	}

	private bool DebugInputSkip(MessageWork message_work)
	{
		if (DebugSkipMdtIndex(message_work))
		{
			return true;
		}
		if (debug_skip_)
		{
			return true;
		}
		return false;
	}

	private bool DebugSkipMdtIndex(MessageWork message_work)
	{
		if (GSStatic.global_work_.r.no_0 == 7)
		{
			return false;
		}
		return message_work.message_type == WindowType.MAIN && message_work.mdt_index < debug_skip_mdt_index_;
	}

	private static bool CodeProc_Dummy(MessageWork message_work)
	{
		message_work.mdt_index++;
		return false;
	}

	private static bool CodeProc_00(MessageWork message_work)
	{
		message_work.mdt_index++;
		return false;
	}

	private static bool CodeProc_01(MessageWork message_work)
	{
		advCtrl.instance.message_system_.NextLine(message_work);
		if ((message_work.text_flag & TextFlag.GRADATION) != 0)
		{
			message_work.text_flag &= ~TextFlag.GRADATION;
			message_work.message_text.SetGradationColor(false);
			message_work.message_text.SetColor(0);
		}
		message_work.mdt_index++;
		return false;
	}

	private static bool CodeProc_02(MessageWork message_work)
	{
		if (advCtrl.instance.sub_window_.IsBusy())
		{
			return true;
		}
		message_work.status |= Status.RT_WAIT;
		if (GSStatic.global_work_.r.no_1 != 2 && GSStatic.global_work_.r.no_0 == 6)
		{
			return true;
		}
		if (GSStatic.global_work_.r.no_1 != 1 && GSStatic.global_work_.r.no_0 == 7)
		{
			return true;
		}
		if (4 <= GSStatic.global_work_.r.no_0 && GSStatic.global_work_.r.no_0 <= 7 && (lifeGaugeCtrl.instance.is_lifegauge_moving() || lifeGaugeCtrl.instance.gauge_mode == 7 || lifeGaugeCtrl.instance.gauge_mode == 0))
		{
			return true;
		}
		if ((message_work.work & 0xFF) == 0)
		{
			if (message_work.message_type == WindowType.MAIN)
			{
				AnimationSystem.Instance.GoIdle();
			}
			message_work.work++;
			if (!advCtrl.instance.message_system_.DebugSkipMdtIndex(message_work))
			{
				return true;
			}
		}
		if ((message_work.work & 0xF0) == 128)
		{
			message_work.work &= 65407;
			return true;
		}
		message_work.work++;
		message_work.status &= ~Status.FAST_MESSAGE;
		if ((message_work.status & Status.READ_MESSAGE) != 0 && ((padCtrl.instance.GetKey(KeyType.B) && optionCtrl.instance.skip_type == optionCtrl.SkipType.ALL_SKIP) || (GetTouchStatus() == TouchStatus.Right && optionCtrl.instance.skip_type == optionCtrl.SkipType.ALL_SKIP)))
		{
			message_work.status |= Status.FAST_MESSAGE;
		}
		if (advCtrl.instance.message_system_.DebugInputSkip(message_work))
		{
			message_work.status |= Status.FAST_MESSAGE;
		}
		if (advCtrl.instance.message_system_.DebugSkipMdtIndex(message_work))
		{
			message_work.work |= 255;
		}
		uint num = 12u;
		if ((message_work.status & Status.FAST_MESSAGE) != 0 && (message_work.work & 0xFF) < num)
		{
			return true;
		}
		if ((message_work.status & Status.FAST_MESSAGE) != 0)
		{
			advCtrl.instance.sub_window_.real_fast_mes_ = 1;
		}
		message_work.work--;
		if (padCtrl.instance.GetKey(KeyType.A) || GetTouchStatus() == TouchStatus.Left)
		{
			skip_time++;
			if (skip_time > 42)
			{
				message_work.status |= Status.FAST_MESSAGE;
				skip_time = 0;
			}
		}
		else
		{
			skip_time = 0;
		}
		if ((4 <= GSStatic.global_work_.r.no_0 && GSStatic.global_work_.r.no_0 <= 7) || GSStatic.global_work_.message_active_window == 1 || (message_work.status & Status.RT_GO) != 0 || (message_work.status & Status.CODE_SKIP) != 0 || !luminolMiniGame.instance.is_end || scienceInvestigationCtrl.instance.active_message)
		{
			if ((message_work.status & Status.RT_WAIT) != 0)
			{
				messageBoardCtrl.instance.arrow(true, 0);
			}
			messageBoardCtrl.instance.ActiveMessageBoardTouch();
			TouchSystem.instance.UpdateDownEvent();
			if (padCtrl.instance.GetKeyDown(KeyType.A) || advCtrl.instance.message_system_.debug_no_key_wait_ || padCtrl.instance.GetKeyDown(KeyType.B) || (message_work.status & Status.FAST_MESSAGE) != 0 || (message_work.status & Status.NEXT_MESSAGE) != 0 || (message_work.status & Status.RT_GO) != 0 || message_work.code == 7)
			{
				if (picePlateCtrl.instance.is_play)
				{
					picePlateCtrl.instance.closePice();
					return true;
				}
				messageBoardCtrl.instance.arrow(false, 0);
				if (message_work.code != 10)
				{
					if (message_work.code == 2)
					{
						messageBoardCtrl.instance.arrow(false, 0);
						if (message_work.message_type == WindowType.MAIN)
						{
							AnimationSystem.Instance.GoTalk();
						}
					}
					else if (message_work.code != 7)
					{
					}
				}
				messageBoardCtrl.instance.is_arrow = false;
				message_work.status &= ~Status.FAST_MESSAGE;
				message_work.status &= ~Status.NEXT_MESSAGE;
				message_work.status &= ~Status.RT_GO;
				message_work.status &= ~Status.RT_WAIT;
				message_work.status &= ~Status.LOOP;
				message_work.status &= ~Status.CODE_SKIP;
				if ((message_work.text_flag & TextFlag.GRADATION) != 0)
				{
					message_work.text_flag &= ~TextFlag.GRADATION;
					message_work.message_text.SetGradationColor(false);
					message_work.message_text.SetColor(0);
				}
				soundCtrl.instance.PlaySE(47);
				if (message_work.code == 10)
				{
					if (GSStatic.global_work_.title == TitleId.GS1)
					{
						if (GSStatic.global_work_.rest > 0)
						{
							advCtrl.instance.message_system_.SetMessage(message_work.mdt_data.GetMessage(message_work.mdt_index + 1));
							return true;
						}
						message_work.mdt_index++;
					}
					else if (GSStatic.global_work_.gauge_hp > 0)
					{
						advCtrl.instance.message_system_.SetMessage(message_work.mdt_data.GetMessage(message_work.mdt_index + 1));
						return true;
					}
				}
				else if (message_work.code == 2)
				{
					if (message_work.message_type == WindowType.MAIN)
					{
						AnimationSystem.Instance.GoTalk();
					}
				}
				else if (message_work.code == 7)
				{
					message_work.mdt_index++;
					message_work.status |= Status.SELECT;
					advCtrl.instance.sub_window_.SetReq(SubWindow.Req.SELECT);
					return false;
				}
				ushort work = message_work.work;
				message_work.work = (ushort)0;
				advCtrl.instance.message_system_.ClearText(message_work);
				message_work.mdt_index++;
				if (advCtrl.instance.message_system_.DebugSkipMdtIndex(message_work))
				{
					return false;
				}
				return true;
			}
		}
		return true;
	}

	private static bool CodeProc_03(MessageWork message_work)
	{
		message_work.mdt_index++;
		message_work.message_text.SetColor(message_work.mdt_data.GetMessage(message_work.mdt_index));
		message_work.mdt_index++;
		return false;
	}

	private static bool CodeProc_04(MessageWork message_work)
	{
		ushort message = message_work.mdt_data.GetMessage(message_work.mdt_index + 1);
		if (GSJoy.Trg(message))
		{
			message_work.mdt_index += 2u;
		}
		return true;
	}

	private static bool CodeProc_05(MessageWork message_work)
	{
		message_work.mdt_index++;
		ushort message = message_work.mdt_data.GetMessage(message_work.mdt_index);
		message_work.mdt_index++;
		soundCtrl.instance.PlayBGM(message, message_work.mdt_data.GetMessage(message_work.mdt_index));
		message_work.mdt_index++;
		return false;
	}

	private static bool CodeProc_06(MessageWork message_work)
	{
		message_work.mdt_index++;
		ushort message = message_work.mdt_data.GetMessage(message_work.mdt_index);
		message_work.mdt_index++;
		bool flag = (message_work.mdt_data.GetMessage(message_work.mdt_index) & 1) != 0;
		message_work.mdt_index++;
		if (GSStatic.global_work_.title == TitleId.GS1 && GS1_script.GS1_moji_code06_CheckSE(message_work, message))
		{
			return false;
		}
		if (flag)
		{
			soundCtrl.instance.PlaySE(message);
		}
		else if (GSStatic.global_work_.title == TitleId.GS3)
		{
			if (message == 380 || message == 391 || message == 378 || message == 374 || message == 164)
			{
				soundCtrl.instance.FadeOutSE(message, 3);
			}
			else
			{
				soundCtrl.instance.StopSE(message);
			}
		}
		else
		{
			soundCtrl.instance.StopSE(message);
		}
		return false;
	}

	private static bool CodeProc_08(MessageWork message_work)
	{
		if (advCtrl.instance.sub_window_.IsBusy())
		{
			return true;
		}
		if ((message_work.status & Status.RT_END_WAIT) != 0)
		{
			if (message_work.rt_wait_timer != 0)
			{
				message_work.rt_wait_timer--;
			}
			else
			{
				message_work.status &= ~Status.RT_END_WAIT;
				message_work.mdt_index += 3u;
			}
			return true;
		}
		if ((4 <= GSStatic.global_work_.r.no_0 && GSStatic.global_work_.r.no_0 <= 7) || advCtrl.instance.sub_window_.routine_[advCtrl.instance.sub_window_.stack_ - 1].r.no_0 == 23)
		{
			selectPlateCtrl instance = selectPlateCtrl.instance;
			if (instance.is_end)
			{
				message_work.cursor = (byte)instance.cursor_no;
				advCtrl.instance.sub_window_.SetReq(SubWindow.Req.SELECT_EXIT);
				return false;
			}
		}
		return true;
	}

	private static bool CodeProc_09(MessageWork message_work)
	{
		if (advCtrl.instance.sub_window_.IsBusy())
		{
			return true;
		}
		if ((message_work.status & Status.RT_END_WAIT) != 0)
		{
			if (message_work.rt_wait_timer != 0)
			{
				message_work.rt_wait_timer--;
			}
			else
			{
				message_work.status &= ~Status.RT_END_WAIT;
				message_work.mdt_index += 4u;
			}
			return true;
		}
		if ((4 <= GSStatic.global_work_.r.no_0 && GSStatic.global_work_.r.no_0 <= 7) || advCtrl.instance.sub_window_.routine_[advCtrl.instance.sub_window_.stack_ - 1].r.no_0 == 23)
		{
			selectPlateCtrl instance = selectPlateCtrl.instance;
			if (instance.is_end)
			{
				message_work.cursor = (byte)instance.cursor_no;
				advCtrl.instance.sub_window_.SetReq(SubWindow.Req.SELECT_EXIT);
				return false;
			}
		}
		return true;
	}

	private static bool CodeProc_0b(MessageWork message_work)
	{
		message_work.mdt_index++;
		message_work.message_time = (byte)message_work.mdt_data.GetMessage(message_work.mdt_index);
		if (message_work.message_time == byte.MaxValue)
		{
			message_work.message_time = 3;
		}
		message_work.mdt_index++;
		return false;
	}

	private static bool CodeProc_0c(MessageWork message_work)
	{
		if ((message_work.work & 0xFF) == 0)
		{
			message_work.work |= (ushort)(message_work.mdt_data.GetMessage(message_work.mdt_index + 1) & 0xFF);
			if (GSStatic.global_work_.title == TitleId.GS1 && lifeGaugeCtrl.instance.gauge_mode == 7)
			{
				message_work.work += 65;
			}
		}
		else
		{
			message_work.work--;
		}
		if ((message_work.status & Status.FAST_MESSAGE) != 0)
		{
			if ((message_work.status & Status.READ_MESSAGE) != 0 && (message_work.status & Status.FAST_MESSAGE) != 0 && (message_work.status & Status.LAST_MESSAGE) != 0)
			{
				message_work.work &= 65280;
			}
			else
			{
				message_work.status &= ~Status.FAST_MESSAGE;
			}
		}
		if (advCtrl.instance.message_system_.DebugInputSkip(message_work) && advCtrl.instance.sub_window_.req_ != SubWindow.Req.LUMINOL_SCENARIO && (GSStatic.global_work_.r.no_0 != 5 || GSStatic.global_work_.r.no_1 != 11 || GSStatic.global_work_.r.no_2 != 7))
		{
			message_work.work &= 65280;
		}
		if ((message_work.work & 0xFFu) != 0)
		{
			return true;
		}
		message_work.mdt_index += 2u;
		message_work.code_no = 0;
		return false;
	}

	private static bool CodeProc_0d(MessageWork message_work)
	{
		if (GSScenario.GetGameOverMesData() == message_work.next_no)
		{
			message_work.game_over = true;
			messageBoardCtrl.instance.guide_ctrl.next_guid = guideCtrl.GuideType.HOUTEI;
		}
		advCtrl.instance.message_system_.SetMessage(message_work.next_no);
		return false;
	}

	private static bool CodeProc_0e(MessageWork message_work)
	{
		message_work.mdt_index++;
		ushort message = message_work.mdt_data.GetMessage(message_work.mdt_index);
		message_work.speaker_id = (byte)((uint)(message >> 8) & 0x7Fu);
		GSStatic.global_work_.win_name_set = (byte)(message & 0xFFu);
		messageBoardCtrl.instance.name_plate(true, message_work.speaker_id, GSStatic.global_work_.win_name_set);
		if (GSStatic.global_work_.title == TitleId.GS3 && message_work.speaker_id == 21 && GSStatic.global_work_.scenario < 10 && !GSFlag.Check(0u, scenario_GS3.SCE2_2_SIBAKUZOU_KIKAN))
		{
			message_work.speaker_id = 2;
		}
		if (GSStatic.message_work_.message_trans_flag == 1)
		{
			messageBoardCtrl.instance.board(true, true);
			messageBoardCtrl.instance.name_plate(true, message_work.speaker_id, message & 0xFF);
		}
		byte[] getTalkSEDataTable = soundCtrl.instance.GetTalkSEDataTable;
		message_work.message_se = (byte)((message_work.speaker_id < getTalkSEDataTable.Length) ? getTalkSEDataTable[message_work.speaker_id] : 0);
		if (message_work.message_se == 2)
		{
			message_work.message_se_character_count = 0;
		}
		if ((message_work.mdt_data.GetMessage(message_work.mdt_index) & 0xFFu) != 0)
		{
			message_work.speaker_id |= 128;
		}
		else
		{
			message_work.speaker_id &= 127;
		}
		message_work.mdt_index++;
		return false;
	}

	private static bool CodeProc_0f(MessageWork message_work)
	{
		message_work.mdt_index++;
		message_work.tukkomi_no = message_work.mdt_data.GetMessage(message_work.mdt_index);
		message_work.mdt_index++;
		message_work.tukkomi_flag = (byte)message_work.mdt_data.GetMessage(message_work.mdt_index);
		message_work.mdt_index++;
		return false;
	}

	private static bool CodeProc_10(MessageWork message_work)
	{
		message_work.mdt_index++;
		uint num;
		uint message;
		uint num2 = (num = (message = message_work.mdt_data.GetMessage(message_work.mdt_index)));
		message_work.mdt_index++;
		num2 = (num2 & 0x7F00) >> 8;
		num &= 0xFFu;
		message >>= 15;
		GSFlag.Set(num2, num, message);
		return false;
	}

	private static bool CodeProc_11(MessageWork message_work)
	{
		message_work.mdt_index++;
		message_work.status |= Status.NEXT_PULS;
		GlobalWork global_work_ = GSStatic.global_work_;
		GSStatic.global_work_.status_flag |= 256u;
		global_work_.r_bk.CopyFrom(ref global_work_.r);
		global_work_.r.Set(8, 0, 0, 1);
		return false;
	}

	private static bool CodeProc_12(MessageWork message_work)
	{
		message_work.mdt_index++;
		uint message = message_work.mdt_data.GetMessage(message_work.mdt_index);
		uint num = message >> 8;
		uint in_time = message & 0xFFu;
		message_work.mdt_index++;
		uint message2 = message_work.mdt_data.GetMessage(message_work.mdt_index);
		message_work.mdt_index++;
		uint message3 = message_work.mdt_data.GetMessage(message_work.mdt_index);
		message_work.mdt_index++;
		if (GSStatic.global_work_.title == TitleId.GS3)
		{
			GlobalWork global_work_ = GSStatic.global_work_;
			if (global_work_.scenario == 7 || global_work_.scenario == 8 || global_work_.scenario == 9 || global_work_.scenario == 10 || global_work_.scenario == 11)
			{
				if (num == 2 && (fadeCtrl.instance.IsExecOut(2) || fadeCtrl.instance.IsOut(2)))
				{
					return false;
				}
			}
			else if (global_work_.scenario == 13 && num == 2 && message3 == 8 && (fadeCtrl.instance.IsExecOut(2) || fadeCtrl.instance.IsOut(2)))
			{
				fadeCtrl.instance.play(1u, in_time, message2, 23u);
				return false;
			}
		}
		if (message_work.message_type == WindowType.MAIN)
		{
			fadeCtrl.instance.play(num, in_time, message2, message3);
		}
		return false;
	}

	private static bool CodeProc_13(MessageWork message_work)
	{
		if ((message_work.work & 0xFF) == 0)
		{
			ushort message = message_work.mdt_data.GetMessage(message_work.mdt_index + 1);
			int in_id = message & 0xFF;
			int in_type = (message >> 8) & 0xF;
			itemPlateCtrl.instance.entryItem(in_id);
			itemPlateCtrl.instance.openItem(in_type, 1f);
			facePlateCtrl.instance.closeItem(true);
			if (((message >> 8) & 0xF0) == 0)
			{
				soundCtrl.instance.PlaySE(51);
			}
		}
		if (GSStatic.global_work_.title == TitleId.GS2)
		{
		}
		if ((message_work.work & 0xF0) == 0)
		{
			message_work.work++;
			return true;
		}
		message_work.mdt_index += 2u;
		return false;
	}

	private static bool CodeProc_14(MessageWork message_work)
	{
		if ((message_work.work & 0xFF) == 0)
		{
			itemPlateCtrl.instance.closeItem(false);
			if (GSStatic.global_work_.title == TitleId.GS3)
			{
				if (message_work.all_work[0] != 30000 || message_work.all_work[1] != 30000 || message_work.all_work[2] != 30000)
				{
					soundCtrl.instance.PlaySE(51);
				}
				else
				{
					message_work.all_work[0] = 0;
					message_work.all_work[1] = 0;
					message_work.all_work[2] = 0;
				}
			}
			else
			{
				soundCtrl.instance.PlaySE(51);
			}
		}
		if (GSStatic.global_work_.title == TitleId.GS2)
		{
		}
		if ((message_work.work & 0xF0) == 0)
		{
			message_work.work++;
			return true;
		}
		message_work.mdt_index++;
		return false;
	}

	private static bool CodeProc_15(MessageWork message_work)
	{
		ushort message = message_work.mdt_data.GetMessage(message_work.mdt_index);
		if (message == 121)
		{
			return true;
		}
		if ((message_work.work & 2) == 0)
		{
			messageBoardCtrl.instance.ActiveNormalMessageNextTouch();
			message_work.work++;
		}
		if ((message_work.status & Status.LOOP) == 0)
		{
			if (message == 21 && message_work.message_type == WindowType.MAIN)
			{
				AnimationSystem.Instance.GoIdle();
			}
			message_work.status &= ~Status.FAST_MESSAGE;
			message_work.status |= Status.LOOP;
		}
		return true;
	}

	private static bool CodeProc_16(MessageWork message_work)
	{
		GlobalWork global_work_ = GSStatic.global_work_;
		message_work.mdt_index++;
		global_work_.Mess_move_flag = 0;
		message_work.message_trans_flag = 0;
		messageBoardCtrl.instance.End();
		messageBoardCtrl.instance.Close();
		global_work_.gauge_hp_scenario_end = global_work_.gauge_hp;
		messageBoardCtrl.instance.InActiveNormalMessageNextTouch();
		if (GSStatic.global_work_.title == TitleId.GS3 && GSScenario.GetScenarioPartData(global_work_.scenario) == 4 && GSScenario.GetScenarioPartData((byte)(global_work_.scenario + 1)) == 5)
		{
			global_work_.gauge_hp += 40;
			if (global_work_.gauge_hp > 80)
			{
				global_work_.gauge_hp = 80;
			}
			global_work_.gauge_hp_disp = (global_work_.gauge_hp_scenario_end = global_work_.gauge_hp);
		}
		global_work_.r.Set(4, 2, 0, 0);
		if (advCtrl.instance.sub_window_.bg_return_ == 1)
		{
			advCtrl.instance.sub_window_.bg_return_ = 0;
		}
		switch (global_work_.title)
		{
		case TitleId.GS1:
		{
			uint num2 = global_work_.scenario;
			if (num2 == 0 || num2 == 4 || num2 == 10 || num2 == 16)
			{
				global_work_.story++;
			}
			break;
		}
		case TitleId.GS2:
		{
			uint num = global_work_.scenario;
			if (num == 1 || num == 7 || num == 13)
			{
				global_work_.story++;
			}
			break;
		}
		case TitleId.GS3:
			switch (global_work_.scenario)
			{
			case 1:
			case 6:
			case 11:
			case 13:
				global_work_.story++;
				break;
			}
			break;
		}
		if (GSStatic.global_work_.title == TitleId.GS1)
		{
			uint num3 = global_work_.scenario;
			if (num3 == 28)
			{
				global_work_.scenario = 31;
			}
			else
			{
				global_work_.scenario++;
			}
		}
		else
		{
			global_work_.scenario++;
		}
		return false;
	}

	private static bool CodeProc_17(MessageWork message_work)
	{
		Routine currentRoutine = advCtrl.instance.sub_window_.GetCurrentRoutine();
		if (currentRoutine.r.no_0 == 2 && currentRoutine.r.no_1 != 2)
		{
			return true;
		}
		message_work.mdt_index++;
		ushort message = message_work.mdt_data.GetMessage(message_work.mdt_index);
		message_work.mdt_index++;
		int in_id = message & 0x3FFF;
		int in_type = message & 0x8000;
		recordListCtrl.instance.addRecord(in_id);
		if ((message & 0x4000u) != 0)
		{
			picePlateCtrl.instance.entryPice(in_type, in_id);
			picePlateCtrl.instance.playPice(1f);
		}
		return true;
	}

	private static bool CodeProc_18(MessageWork message_work)
	{
		Routine currentRoutine = advCtrl.instance.sub_window_.GetCurrentRoutine();
		if (currentRoutine.r.no_0 == 2 && currentRoutine.r.no_1 != 2 && (GSStatic.global_work_.title != TitleId.GS2 || message_work.now_no != scenario_GS2.SC3_27440))
		{
			return true;
		}
		message_work.mdt_index++;
		ushort message = message_work.mdt_data.GetMessage(message_work.mdt_index);
		int in_id = message & 0x3FFF;
		recordListCtrl.instance.deleteRecord(in_id);
		if ((message & 0x4000u) != 0)
		{
			soundCtrl.instance.PlaySE(15);
		}
		message_work.mdt_index++;
		return false;
	}

	private static bool CodeProc_19(MessageWork message_work)
	{
		Routine currentRoutine = advCtrl.instance.sub_window_.GetCurrentRoutine();
		if (currentRoutine.r.no_0 == 2 && currentRoutine.r.no_1 != 2)
		{
			return true;
		}
		message_work.mdt_index++;
		ushort message = message_work.mdt_data.GetMessage(message_work.mdt_index);
		message_work.mdt_index++;
		ushort message2 = message_work.mdt_data.GetMessage(message_work.mdt_index);
		int in_id = message & 0x3FFF;
		int in_type = message & 0x8000;
		int num = message2 & 0x3FFF;
		recordListCtrl.instance.updateRecord(in_id, num);
		if ((message2 & 0x4000u) != 0)
		{
			picePlateCtrl.instance.entryPice(in_type, num);
			picePlateCtrl.instance.playPice(1f);
		}
		message_work.mdt_index++;
		return false;
	}

	private static bool CodeProc_1a(MessageWork message_work)
	{
		if ((message_work.work & 0xFF) == 0)
		{
			message_work.work |= 1;
			return true;
		}
		message_work.mdt_index++;
		uint message = message_work.mdt_data.GetMessage(message_work.mdt_index);
		message_work.mdt_index++;
		uint message2 = message_work.mdt_data.GetMessage(message_work.mdt_index);
		message_work.mdt_index++;
		uint message3 = message_work.mdt_data.GetMessage(message_work.mdt_index);
		message_work.mdt_index++;
		uint message4 = message_work.mdt_data.GetMessage(message_work.mdt_index);
		message_work.mdt_index++;
		uint in_start = 0u;
		if ((message2 & (true ? 1u : 0u)) != 0)
		{
			in_start = 30u;
		}
		bgCtrl.instance.CourtScrol(message, message2, message3, in_start, 31u, message4);
		if (GSStatic.global_work_.language == Language.JAPAN || GSStatic.global_work_.title == TitleId.GS3)
		{
		}
		message_work.work &= 65280;
		return false;
	}

	private static bool CodeProc_1b(MessageWork message_work)
	{
		if (bgCtrl.instance.is_scrolling_court)
		{
			return true;
		}
		message_work.mdt_index++;
		ushort message = message_work.mdt_data.GetMessage(message_work.mdt_index);
		message_work.mdt_index++;
		AnimationSystem.Instance.StopByChangingBG();
		VasePuzzleMiniGame.instance.DeleteEndBackGround();
		bgCtrl.instance.SetSprite(message);
		GSDemo.CheckBGChange(message, 0u);
		AnimationSystem.Instance.StopByBgChanged();
		if (GSStatic.global_work_.title == TitleId.GS1 && (message == 74 || message == 134))
		{
			bgCtrl.instance.Scroll(0f, 0.25f);
		}
		if (GSStatic.global_work_.title == TitleId.GS2)
		{
			if (message == 44 || message == 126)
			{
				bgCtrl.instance.SetSpriteDelay(12);
			}
			if (message == 36)
			{
				AnimationObject animationObject = AnimationSystem.Instance.PlayObject(0, 0, 82);
				animationObject.transform.localPosition = new Vector3(0f, -1080f, 0f);
				AnimationObject animationObject2 = AnimationSystem.Instance.PlayObject(0, 0, 83);
				animationObject2.transform.localPosition = new Vector3(0f, -1080f, 0f);
			}
		}
		return true;
	}

	private static bool CodeProc_1c(MessageWork message_work)
	{
		if ((message_work.work & 0xFF) == 0)
		{
			ushort message = message_work.mdt_data.GetMessage(message_work.mdt_index + 1);
			if (message == 1 && (message_work.status & Status.FAST_MESSAGE) != 0)
			{
				message_work.work |= 15;
			}
		}
		else
		{
			message_work.work--;
		}
		if ((message_work.work & 0xFFu) != 0)
		{
			return true;
		}
		message_work.status &= ~Status.FAST_MESSAGE;
		GlobalWork global_work_ = GSStatic.global_work_;
		message_work.mdt_index++;
		switch (message_work.mdt_data.GetMessage(message_work.mdt_index))
		{
		case 0:
			messageBoardCtrl.instance.board(true, true);
			messageBoardCtrl.instance.name_plate(true, message_work.speaker_id, GSStatic.global_work_.win_name_set);
			message_work.message_trans_flag = 1;
			break;
		case 1:
			message_work.message_trans_flag = 0;
			messageBoardCtrl.instance.board(false, false);
			break;
		case 2:
			if (GSStatic.global_work_.r.no_0 == 4)
			{
				AnimationSystem.Instance.StopCharacters();
			}
			Mess_window_set(3u);
			break;
		case 3:
			if (GSStatic.global_work_.r.no_0 == 4)
			{
				AnimationSystem.Instance.StopCharacters();
			}
			Mess_window_set(4u);
			messageBoardCtrl.instance.board(false, false);
			if (GSStatic.global_work_.r.no_0 == 5)
			{
				if (GSStatic.global_work_.r.no_1 == 6)
				{
				}
				if (GSStatic.global_work_.r.no_1 == 8)
				{
				}
				if (GSStatic.global_work_.r.no_1 != 9)
				{
				}
			}
			break;
		case 8:
			Mess_window_set(5u);
			message_work.mdt_index++;
			return true;
		case 16:
			Mess_window_set(6u);
			message_work.mdt_index++;
			return true;
		case 6:
			if (GSStatic.global_work_.r.no_0 != 5)
			{
				break;
			}
			if (GSStatic.global_work_.r.no_1 == 6)
			{
			}
			if (GSStatic.global_work_.r.no_1 == 8)
			{
				global_work_.Mess_move_flag = 0;
				message_work.message_trans_flag = 0;
				Mess_window_set(4u);
				messageBoardCtrl.instance.board(false, false);
				message_work.mess_win_rno = 4;
				break;
			}
			if (GSStatic.global_work_.r.no_1 == 9)
			{
			}
			global_work_.Mess_move_flag = 0;
			message_work.message_trans_flag = 0;
			Mess_window_set(4u);
			messageBoardCtrl.instance.board(false, false);
			message_work.mess_win_rno = 4;
			break;
		}
		message_work.mdt_index++;
		return false;
	}

	public static void Mess_window_set(uint type)
	{
		MessageWork activeMessageWork = GetActiveMessageWork();
		GSStatic.global_work_.Mess_move_flag = 0;
		activeMessageWork.message_trans_flag = 0;
		if (GSStatic.global_work_.language != 0)
		{
		}
		switch (type)
		{
		case 4u:
		case 6u:
			if (activeMessageWork.message_trans_flag == 0)
			{
				activeMessageWork.mess_win_rno = 1;
				break;
			}
			activeMessageWork.mess_win_rno = 4;
			messageBoardCtrl.instance.board(false, false);
			break;
		case 3u:
		case 5u:
			activeMessageWork.mess_win_rno = 3;
			messageBoardCtrl.instance.board(true, true);
			messageBoardCtrl.instance.name_plate(false, 0, 0);
			break;
		case 7u:
			activeMessageWork.mess_win_rno = 3;
			break;
		case 8u:
			activeMessageWork.mess_win_rno = 8;
			messageBoardCtrl.instance.board(false, false);
			break;
		case 10u:
			activeMessageWork.mess_win_rno = 10;
			GSStatic.global_work_.Mess_move_flag = 1;
			break;
		}
		if (GSStatic.message_work_.message_type != 0 && GSStatic.global_work_.title != 0)
		{
		}
	}

	private static bool CodeProc_1d(MessageWork message_work)
	{
		message_work.mdt_index++;
		ushort message = message_work.mdt_data.GetMessage(message_work.mdt_index);
		message_work.mdt_index++;
		int num = message & 0xFF;
		switch (message >> 8)
		{
		case 0:
			bgCtrl.instance.Scroll(num, 0f);
			break;
		case 1:
			bgCtrl.instance.Scroll(-num, 0f);
			break;
		case 2:
			bgCtrl.instance.Scroll(0f, -num);
			break;
		case 3:
			bgCtrl.instance.Scroll(0f, num);
			break;
		}
		return false;
	}

	private static bool CodeProc_1e(MessageWork message_work)
	{
		message_work.mdt_index++;
		AnimationSystem.Instance.CharFadeInit(message_work.mdt_data.GetMessage(message_work.mdt_index));
		uint message = message_work.mdt_data.GetMessage(message_work.mdt_index);
		message_work.mdt_index++;
		uint message2 = message_work.mdt_data.GetMessage(message_work.mdt_index);
		message_work.mdt_index++;
		if (GSStatic.global_work_.title == TitleId.GS3 || GSStatic.global_work_.title == TitleId.GS2)
		{
		}
		if (GSStatic.global_work_.language == Language.JAPAN || GSStatic.global_work_.title == TitleId.GS3)
		{
		}
		ushort message3 = message_work.mdt_data.GetMessage(message_work.mdt_index);
		message_work.mdt_index++;
		AnimationSystem instance = AnimationSystem.Instance;
		if (message != 0)
		{
			instance.StopCharacters(false);
			instance.PlayCharacter((int)GSStatic.global_work_.title, (int)message, (int)message2, message3);
			GSStatic.tantei_work_.person_flag = 1;
			GSStatic.obj_work_[1].h_num = (byte)message;
			GSStatic.obj_work_[1].foa = (ushort)message2;
			GSStatic.obj_work_[1].idlingFOA = message3;
		}
		else
		{
			if (GSStatic.global_work_.title == TitleId.GS3)
			{
				instance.CtrlChinamiObj(0);
			}
			instance.StopCharacters();
			GSStatic.tantei_work_.person_flag = 0;
			instance.CharacterAnimationObject.BeFlag = 0;
			instance.CharacterAnimationObject.gameObject.SetActive(true);
			GSStatic.obj_work_[1].h_num = 0;
			GSStatic.obj_work_[1].foa = 0;
			GSStatic.obj_work_[1].idlingFOA = 0;
		}
		if (GSStatic.global_work_.language == Language.JAPAN || GSStatic.global_work_.title == TitleId.GS3)
		{
		}
		return false;
	}

	private static bool CodeProc_1f(MessageWork message_work)
	{
		message_work.mdt_index++;
		cutInCtrl.instance.cutOut();
		bgCtrl.instance.StopCutUpScroll();
		return true;
	}

	private static bool CodeProc_20(MessageWork message_work)
	{
		message_work.mdt_index++;
		message_work.next_no = message_work.mdt_data.GetMessage(message_work.mdt_index);
		message_work.mdt_index++;
		return false;
	}

	private static bool CodeProc_21(MessageWork message_work)
	{
		message_work.mdt_index++;
		message_work.status |= Status.NEXT_PULS;
		GlobalWork global_work_ = GSStatic.global_work_;
		global_work_.status_flag |= 768u;
		global_work_.r_bk.CopyFrom(ref global_work_.r);
		global_work_.r.Set(8, 0, 0, 1);
		return false;
	}

	private static bool CodeProc_22(MessageWork message_work)
	{
		message_work.mdt_index++;
		message_work.mdt_index++;
		ushort message = message_work.mdt_data.GetMessage(message_work.mdt_index);
		if (message != 0)
		{
			soundCtrl.instance.FadeOutBGM(message);
		}
		else
		{
			soundCtrl.instance.AllStopBGM();
		}
		message_work.mdt_index++;
		return false;
	}

	private static bool CodeProc_23(MessageWork message_work)
	{
		message_work.mdt_index++;
		message_work.mdt_index++;
		if (message_work.mdt_data.GetMessage(message_work.mdt_index) != 0)
		{
			soundCtrl.instance.ReplayBGM();
		}
		else
		{
			soundCtrl.instance.PauseBGM();
		}
		message_work.mdt_index++;
		return false;
	}

	private static bool CodeProc_24(MessageWork message_work)
	{
		message_work.mdt_index++;
		TrophyCtrl.check_trophy_by_mes_no();
		GSStatic.global_work_.Mess_move_flag = 0;
		message_work.message_trans_flag = 0;
		GSStatic.global_work_.r.Set(3, 0, 0, 0);
		return true;
	}

	private static bool CodeProc_25(MessageWork message_work)
	{
		message_work.mdt_index++;
		message_work.mdt_index++;
		return false;
	}

	private static bool CodeProc_26(MessageWork message_work)
	{
		message_work.mdt_index++;
		if (message_work.mdt_data.GetMessage(message_work.mdt_index) != 0)
		{
			GSStatic.global_work_.status_flag |= 16u;
			recordListCtrl.instance.is_note_on = false;
		}
		else
		{
			GSStatic.global_work_.status_flag &= 4294967279u;
			recordListCtrl.instance.is_note_on = true;
		}
		message_work.mdt_index++;
		return false;
	}

	private static bool CodeProc_27(MessageWork message_work)
	{
		message_work.mdt_index++;
		if (!optionCtrl.instance.is_shake || (GSStatic.global_work_.status_flag & (true ? 1u : 0u)) != 0 || message_work.message_type == WindowType.MAIN)
		{
		}
		ushort message = message_work.mdt_data.GetMessage(message_work.mdt_index);
		message_work.mdt_index++;
		ushort message2 = message_work.mdt_data.GetMessage(message_work.mdt_index);
		if (optionCtrl.instance.is_shake)
		{
			ScreenCtrl.instance.quake_timer = message / 3 * 2;
			if (message > 0 && ScreenCtrl.instance.quake_timer == 0)
			{
				ScreenCtrl.instance.quake_timer = 1;
			}
			GSStatic.global_work_.status_flag |= 1u;
			ScreenCtrl.instance.quake_power = message_work.mdt_data.GetMessage(message_work.mdt_index);
			ScreenCtrl.instance.Quake();
		}
		if (message2 >= 1)
		{
			vibrationCtrl.instance.play(message);
		}
		message_work.mdt_index++;
		return false;
	}

	private static bool CodeProc_28(MessageWork message_work)
	{
		message_work.mdt_index++;
		GlobalWork global_work_ = GSStatic.global_work_;
		if (message_work.mdt_data.GetMessage(message_work.mdt_index) != 0)
		{
			global_work_.r_bk.CopyFrom(ref global_work_.r);
			global_work_.r.Set(6, 0, 0, 0);
		}
		else
		{
			GSStatic.global_work_.r.no_1++;
		}
		message_work.mdt_index++;
		return false;
	}

	private static bool CodeProc_29(MessageWork message_work)
	{
		message_work.mdt_index++;
		ushort message = message_work.mdt_data.GetMessage(message_work.mdt_index);
		message_work.mdt_index++;
		switch (message)
		{
		case 5:
			advCtrl.instance.sub_window_.SetReq(SubWindow.Req.QUESTIONING);
			GSStatic.global_work_.r.Set(7, 1, 0, 0);
			messageBoardCtrl.instance.ActiveNormalMessageNextTouch();
			messageBoardCtrl.instance.guide_set(false, guideCtrl.GuideType.NO_GUIDE);
			messageBoardCtrl.instance.guide_ctrl.next_guid = guideCtrl.GuideType.QUESTIONING;
			break;
		case 4:
			advCtrl.instance.sub_window_.SetReq(SubWindow.Req.QUESTIONING_EXIT);
			break;
		case 3:
			if (GSStatic.global_work_.title == TitleId.GS1)
			{
			}
			GSMain_Questioning.first_question = true;
			advCtrl.instance.sub_window_.SetReq(SubWindow.Req.QUESTIONING);
			GSStatic.global_work_.r.Set(7, 1, 0, 0);
			messageBoardCtrl.instance.guide_ctrl.next_guid = guideCtrl.GuideType.QUESTIONING;
			break;
		case 2:
			if (GSStatic.global_work_.title == TitleId.GS1)
			{
				if (GSStatic.global_work_.rest != 0)
				{
					lifeGaugeCtrl.instance.lifegauge_set_move(9);
				}
			}
			else if (GSStatic.global_work_.gauge_hp != 0)
			{
				lifeGaugeCtrl.instance.lifegauge_set_move(9);
			}
			advCtrl.instance.sub_window_.SetReq(SubWindow.Req.QUESTIONING);
			GSStatic.global_work_.r.Set(7, 1, 0, 0);
			messageBoardCtrl.instance.guide_ctrl.next_guid = guideCtrl.GuideType.QUESTIONING;
			break;
		default:
		{
			GlobalWork global_work_ = GSStatic.global_work_;
			global_work_.r_bk.CopyFrom(ref global_work_.r);
			global_work_.r.Set(7, 0, 0, 0);
			advCtrl.instance.sub_window_.SetReq(SubWindow.Req.QUESTIONING_FIRST);
			messageBoardCtrl.instance.guide_set(false, guideCtrl.GuideType.NO_GUIDE);
			messageBoardCtrl.instance.guide_ctrl.next_guid = guideCtrl.GuideType.QUESTIONING;
			break;
		}
		case 0:
			GSStatic.global_work_.r.Set(4, 1, 0, 0);
			advCtrl.instance.sub_window_.SetReq(SubWindow.Req.QUESTIONING_EXIT);
			break;
		}
		return false;
	}

	private static bool CodeProc_2a(MessageWork message_work)
	{
		message_work.mdt_index++;
		bool flag = GSFlag.Check(0u, message_work.mdt_data.GetMessage(message_work.mdt_index));
		message_work.next_no = message_work.mdt_data.GetMessage(message_work.mdt_index + (uint)(flag ? 1 : 2));
		message_work.mdt_index += 3u;
		return false;
	}

	private static bool CodeProc_2b(MessageWork message_work)
	{
		message_work.mdt_index++;
		if (lifeGaugeCtrl.instance.debug_instant_death && !lifeGaugeCtrl.instance.debug_no_damage)
		{
			GSStatic.global_work_.rest_old = (byte)GSStatic.global_work_.rest;
			GSStatic.global_work_.rest = 0;
			lifeGaugeCtrl.instance.ActionLifeGauge(lifeGaugeCtrl.Gauge_State.DAMAGE);
			GSStatic.message_work_.next_no = GSScenario.GetGameOverMesData();
			return false;
		}
		GSStatic.global_work_.rest_old = (byte)GSStatic.global_work_.rest;
		GlobalWork global_work_ = GSStatic.global_work_;
		global_work_.rest--;
		lifeGaugeCtrl.instance.ActionLifeGauge(lifeGaugeCtrl.Gauge_State.DAMAGE);
		if (GSStatic.global_work_.rest <= 0)
		{
			GSStatic.message_work_.next_no = GSScenario.GetGameOverMesData();
		}
		return false;
	}

	public static bool CodeProc_2c(MessageWork message_work)
	{
		message_work.mdt_index++;
		message_work.next_no = message_work.mdt_data.GetMessage(message_work.mdt_index);
		message_work.mdt_index++;
		advCtrl.instance.message_system_.ClearText(message_work);
		if (message_work.message_type == WindowType.MAIN)
		{
			AnimationSystem.Instance.GoIdle();
		}
		if (GSStatic.global_work_.title == TitleId.GS3)
		{
			if ((message_work.text_flag & TextFlag.GRADATION) != 0)
			{
				message_work.text_flag &= ~TextFlag.GRADATION;
				message_work.message_text.SetGradationColor(false);
				message_work.message_text.SetColor(0);
			}
			advCtrl.instance.message_system_.SetMessage(message_work.next_no);
		}
		return false;
	}

	private static bool CodeProc_2e(MessageWork message_work)
	{
		if ((message_work.work & 0xFF) == 0)
		{
			if ((message_work.status & Status.FAST_MESSAGE) != 0)
			{
				message_work.work |= 15;
			}
		}
		else
		{
			message_work.work--;
		}
		if ((message_work.work & 0xFFu) != 0)
		{
			return true;
		}
		message_work.status &= ~(Status.RT_WAIT | Status.RT_GO);
		message_work.status &= ~Status.FAST_MESSAGE;
		message_work.status &= ~Status.NEXT_MESSAGE;
		if ((message_work.text_flag & TextFlag.GRADATION) != 0)
		{
			message_work.text_flag &= ~TextFlag.GRADATION;
			message_work.message_text.SetGradationColor(false);
			message_work.message_text.SetColor(0);
		}
		message_work.mdt_index++;
		advCtrl.instance.message_system_.ClearText(message_work);
		if (advCtrl.instance.message_system_.DebugSkipMdtIndex(message_work))
		{
			return false;
		}
		return true;
	}

	private static bool CodeProc_2f(MessageWork message_work)
	{
		message_work.mdt_index++;
		uint message = message_work.mdt_data.GetMessage(message_work.mdt_index);
		message_work.mdt_index++;
		if (GSStatic.global_work_.title == TitleId.GS3 || GSStatic.global_work_.title == TitleId.GS2)
		{
		}
		if (message_work.mdt_data.GetMessage(message_work.mdt_index) != 0)
		{
			AnimationObject animationObject = null;
			if (message < objMoveCtrl.OBJ_MOVE_MAX)
			{
				objMoveCtrl.instance.play((int)message);
			}
			else
			{
				if (GSStatic.global_work_.title == TitleId.GS2)
				{
					int num = GS2_OpObjCtrl.instance.SearchMultipleAnimationName(message);
					if (num != 0)
					{
						animationObject = AnimationSystem.Instance.PlayObject(0, 0, num);
					}
					if (message == 82 || message == 83)
					{
						animationObject = AnimationSystem.Instance.FindObject(0, 0, (int)message);
					}
				}
				if (animationObject == null)
				{
					animationObject = AnimationSystem.Instance.PlayObject(1, 0, (int)message);
				}
				if (animationObject != null && GSStatic.global_work_.title == TitleId.GS3 && (message == 205 || message == 226))
				{
					Vector3 localPosition = animationObject.transform.localPosition;
					localPosition.x = 1920f;
					animationObject.transform.localPosition = localPosition;
				}
			}
			if (GSStatic.global_work_.title == TitleId.GS2)
			{
				GS2_OpObjCtrl.instance.CreateObj_GS2(animationObject, message);
			}
		}
		else if (message < objMoveCtrl.OBJ_MOVE_MAX)
		{
			objMoveCtrl.instance.stop((int)message);
		}
		else
		{
			int num2 = 0;
			if (GSStatic.global_work_.title == TitleId.GS2)
			{
				GS2_OpObjCtrl.instance.Remove((int)message);
				num2 = GS2_OpObjCtrl.instance.SearchMultipleAnimationName(message);
			}
			if (num2 != 0)
			{
				AnimationSystem.Instance.StopObject(0, 0, num2, true);
			}
			else
			{
				AnimationSystem.Instance.StopObject(1, 0, (int)message, true);
			}
		}
		message_work.mdt_index++;
		return false;
	}

	private static bool CodeProc_30(MessageWork message_work)
	{
		message_work.mdt_index++;
		message_work.message_se = (byte)message_work.mdt_data.GetMessage(message_work.mdt_index);
		if (message_work.message_se == 2)
		{
			message_work.message_se_character_count = 0;
		}
		message_work.mdt_index++;
		return false;
	}

	private static bool CodeProc_31(MessageWork message_work)
	{
		message_work.mdt_index++;
		int message = message_work.mdt_data.GetMessage(message_work.mdt_index);
		message_work.mdt_index++;
		int message2 = message_work.mdt_data.GetMessage(message_work.mdt_index);
		AnimationSystem.Instance.CharFade(message, message2);
		message_work.mdt_index++;
		if (GSStatic.global_work_.title == TitleId.GS3 && (long)AnimationSystem.Instance.IdlingCharacterMasked == 13 && GSStatic.global_work_.scenario == 5 && AnimationSystem.Instance.is_aiga_mozaic_anim_)
		{
			AnimationSystem.Instance.SetFadeAigaMozaicAnim(message, message2);
		}
		return false;
	}

	private static bool CodeProc_32(MessageWork message_work)
	{
		message_work.mdt_index++;
		uint message = message_work.mdt_data.GetMessage(message_work.mdt_index);
		message_work.mdt_index++;
		uint message2 = message_work.mdt_data.GetMessage(message_work.mdt_index);
		message_work.mdt_index++;
		GSStatic.global_work_.Map_data[message][0] = message2;
		return false;
	}

	private static bool CodeProc_33(MessageWork message_work)
	{
		message_work.mdt_index++;
		uint message = message_work.mdt_data.GetMessage(message_work.mdt_index);
		message_work.mdt_index++;
		GSStatic.global_work_.Map_data[message][4] = message_work.mdt_data.GetMessage(message_work.mdt_index);
		message_work.mdt_index++;
		GSStatic.global_work_.Map_data[message][5] = message_work.mdt_data.GetMessage(message_work.mdt_index);
		message_work.mdt_index++;
		GSStatic.global_work_.Map_data[message][6] = message_work.mdt_data.GetMessage(message_work.mdt_index);
		message_work.mdt_index++;
		GSStatic.global_work_.Map_data[message][7] = message_work.mdt_data.GetMessage(message_work.mdt_index);
		message_work.mdt_index++;
		return false;
	}

	private static bool CodeProc_34(MessageWork message_work)
	{
		if ((message_work.work & 0xFF) == 0)
		{
			advCtrl.instance.sub_window_.SetReq(SubWindow.Req.MOVE_GO);
			message_work.work++;
		}
		if (advCtrl.instance.sub_window_.IsBusy())
		{
			return true;
		}
		return false;
	}

	public static bool CodeProc_35(MessageWork message_work)
	{
		message_work.mdt_index++;
		ushort message = message_work.mdt_data.GetMessage(message_work.mdt_index);
		uint num = (uint)(message >> 8);
		if (((uint)message & (true ? 1u : 0u)) != 0)
		{
			if (!GSFlag.Check(0u, num))
			{
				message_work.mdt_index += 2u;
				return false;
			}
		}
		else if (GSFlag.Check(0u, num) && (GSStatic.global_work_.title != 0 || GSStatic.global_work_.scenario != 30 || message_work.now_no != scenario.SC4_66690 || num != scenario.SCE44_FLAG_ST_L_POLICE_SECTION_TYPE3 || (!GSFlag.Check(0u, scenario.SCE44_FLAG_MES67510) && (GSFlag.Check(0u, scenario.SCE44_FLAG_ST_L_POLICE_SECTION_TYPE4) || !GSFlag.Check(0u, scenario.SCE44_FLAG_MES67100)))))
		{
			message_work.mdt_index += 2u;
			return false;
		}
		message_work.mdt_index++;
		if ((message & 0x80u) != 0)
		{
			TrophyCtrl.check_trophy_by_mes_no();
			if ((message_work.status & Status.NO_TALKENDFLG) != 0)
			{
				message_work.status &= ~Status.NO_TALKENDFLG;
			}
			ushort message2 = message_work.mdt_data.GetMessage(message_work.mdt_index);
			uint message_top = message_work.mdt_index_top;
			message_work.mdt_index = message_work.mdt_data.GetLabelMessageOffset(message2, out message_work.now_no, out message_top);
			message_work.mdt_index_top = message_top;
			message_work.now_no += 128;
			if (!TrophyCtrl.disable_check_trophy_by_mes_no)
			{
				GSStatic.message_work_.enable_message_trophy = true;
			}
		}
		else
		{
			uint message3 = message_work.mdt_data.GetMessage(message_work.mdt_index);
			message3 /= 2;
			message_work.mdt_index = message_work.mdt_index_top + message3;
		}
		return false;
	}

	public static bool CodeProc_36(MessageWork message_work)
	{
		message_work.mdt_index++;
		if ((message_work.status & Status.NO_TALKENDFLG) != 0)
		{
			message_work.status &= ~Status.NO_TALKENDFLG;
		}
		else if (GSStatic.global_work_.title != TitleId.GS2)
		{
		}
		TrophyCtrl.check_trophy_by_mes_no();
		ushort message = message_work.mdt_data.GetMessage(message_work.mdt_index);
		uint message_top = message_work.mdt_index_top;
		message_work.mdt_index = message_work.mdt_data.GetLabelMessageOffset(message, out message_work.now_no, out message_top);
		message_work.mdt_index_top = message_top;
		message_work.now_no += 128;
		if (!TrophyCtrl.disable_check_trophy_by_mes_no)
		{
			GSStatic.message_work_.enable_message_trophy = true;
		}
		if (message_work.code == 120)
		{
			message_work.status |= Status.NO_TALKENDFLG;
		}
		return false;
	}

	private static bool CodeProc_37(MessageWork message_work)
	{
		message_work.mdt_index++;
		uint message = message_work.mdt_data.GetMessage(message_work.mdt_index);
		message_work.mdt_index++;
		GSStatic.talk_work_.talk_data_[message].sw = message_work.mdt_data.GetMessage(message_work.mdt_index);
		message_work.mdt_index++;
		return false;
	}

	private static bool CodeProc_38(MessageWork message_work)
	{
		message_work.mdt_index++;
		if (message_work.mdt_data.GetMessage(message_work.mdt_index) != 0)
		{
			if (AnimationSystem.Instance.CharacterAnimationObject.Exists)
			{
				AnimationSystem.Instance.CharacterAnimationObject.BeFlag |= 536870912;
				AnimationSystem.Instance.CharacterAnimationObject.BeFlag &= -134217729;
				AnimationSystem.Instance.CharacterAnimationObject.gameObject.SetActive(true);
			}
		}
		else if (AnimationSystem.Instance.CharacterAnimationObject.Exists)
		{
			AnimationSystem.Instance.CharacterAnimationObject.BeFlag &= -536870913;
			AnimationSystem.Instance.CharacterAnimationObject.BeFlag |= 134217728;
			AnimationSystem.Instance.CharacterAnimationObject.gameObject.SetActive(false);
		}
		message_work.mdt_index++;
		return false;
	}

	private static bool CodeProc_39(MessageWork message_work)
	{
		message_work.mdt_index++;
		ushort message = message_work.mdt_data.GetMessage(message_work.mdt_index);
		message_work.mdt_index++;
		uint num = (uint)message >> 8;
		bool flag = false;
		if (GSStatic.global_work_.title == TitleId.GS3)
		{
			uint num2 = num & 1u;
			if (num == 5)
			{
				flag = true;
			}
			if (GSStatic.global_work_.scenario == 13 && message_work.now_no == scenario_GS3.SC3_0_47732)
			{
				if (num == 7 && num2 == 0)
				{
					GSDemo.CheckBGChange(4095u, 0u);
				}
				flag = false;
			}
		}
		if (!flag)
		{
			if (((uint)message & (true ? 1u : 0u)) != 0)
			{
				GSMapIcon.instance.LoadSprite((int)num);
			}
			else
			{
				GSMapIcon.instance.UnloadSprite((int)num);
			}
		}
		return false;
	}

	private static bool CodeProc_3a(MessageWork message_work)
	{
		message_work.mdt_index++;
		uint message = message_work.mdt_data.GetMessage(message_work.mdt_index);
		int explCharWorkIndexById = GSMapIcon.GetExplCharWorkIndexById((int)message);
		if (explCharWorkIndexById != 255)
		{
			uint num = message_work.mdt_data.GetMessage(message_work.mdt_index + 1);
			uint message2 = message_work.mdt_data.GetMessage(message_work.mdt_index + 2);
			if (GSStatic.global_work_.title == TitleId.GS1)
			{
				if (GSStatic.global_work_.scenario == 6 && message == 1 && num == 258 && message2 == 256)
				{
					GSMapIcon.instance.is_expl_higaisha_move = true;
				}
				if ((long)bgCtrl.instance.bg_no == 119 && bgCtrl.instance.body.transform.localPosition.x <= -960f)
				{
					num -= 14;
				}
			}
			ExplCharData[] expl_char_data_ = GSStatic.expl_char_work_.expl_char_data_;
			expl_char_data_[explCharWorkIndexById].para1 = (ushort)num;
			expl_char_data_[explCharWorkIndexById].para0 = (ushort)message2;
			GSMapIcon.instance.SetPosition((int)message, (int)(num & 0x1FF), (int)(message2 & 0xFF));
		}
		message_work.mdt_index += 3u;
		return false;
	}

	private static bool CodeProc_3b(MessageWork message_work)
	{
		message_work.mdt_index++;
		ushort message = message_work.mdt_data.GetMessage(message_work.mdt_index);
		int id = message >> 8;
		int explCharWorkIndexById = GSMapIcon.GetExplCharWorkIndexById(id);
		if (explCharWorkIndexById != 255)
		{
			ExplCharData[] expl_char_data_ = GSStatic.expl_char_work_.expl_char_data_;
			expl_char_data_[explCharWorkIndexById].move = (byte)(message & 0xFFu);
			message = message_work.mdt_data.GetMessage(message_work.mdt_index + 1);
			expl_char_data_[explCharWorkIndexById].speed = (byte)(message >> 8);
			expl_char_data_[explCharWorkIndexById].dot = (byte)(message & 0xFFu);
			expl_char_data_[explCharWorkIndexById].status |= 2;
			expl_char_data_[explCharWorkIndexById].dot_now = 0;
			GSMapIcon.instance.SetVisible(id, true);
		}
		message_work.mdt_index += 2u;
		return false;
	}

	private static bool CodeProc_3c(MessageWork message_work)
	{
		message_work.mdt_index++;
		ushort message = message_work.mdt_data.GetMessage(message_work.mdt_index);
		message_work.mdt_index++;
		int id = message >> 8;
		int explCharWorkIndexById = GSMapIcon.GetExplCharWorkIndexById(id);
		if (explCharWorkIndexById != 255)
		{
			ExplCharData[] expl_char_data_ = GSStatic.expl_char_work_.expl_char_data_;
			expl_char_data_[explCharWorkIndexById].blink = (byte)(message & 0xFFu);
			GSMapIcon.instance.SetVisible(id, true);
			if (expl_char_data_[explCharWorkIndexById].blink == 0)
			{
				GSMapIcon.instance.SetVisible(id, false);
				expl_char_data_[explCharWorkIndexById].status |= 4;
			}
			if ((message & 1) == 0)
			{
				GSMapIcon.instance.SetVisible(id, true);
				expl_char_data_[explCharWorkIndexById].status &= 251;
			}
			expl_char_data_[explCharWorkIndexById].timer = 0;
		}
		return false;
	}

	private static bool CodeProc_3d(MessageWork message_work)
	{
		int id = message_work.mdt_data.GetMessage(message_work.mdt_index + 1) >> 8;
		int explCharWorkIndexById = GSMapIcon.GetExplCharWorkIndexById(id);
		if (explCharWorkIndexById != 255)
		{
			ExplCharData[] expl_char_data_ = GSStatic.expl_char_work_.expl_char_data_;
			if ((expl_char_data_[explCharWorkIndexById].status & 2u) != 0)
			{
				GSMapIcon.instance.SetVisible(id, true);
				return true;
			}
		}
		message_work.mdt_index += 2u;
		return false;
	}

	private static bool CodeProc_3e(MessageWork message_work)
	{
		message_work.mdt_index++;
		GSStatic.tantei_work_.siteki_no = (byte)message_work.mdt_data.GetMessage(message_work.mdt_index);
		message_work.status |= Status.POINT_TO_START | Status.POINT_TO_1ST;
		message_work.mdt_index++;
		return false;
	}

	private static bool CodeProc_3f(MessageWork message_work)
	{
		if ((message_work.status & Status.POINT_TO_START) != 0)
		{
			message_work.status &= ~Status.POINT_TO_START;
			message_work.status |= Status.LOOP | Status.POINT_TO;
			if (GSStatic.global_work_.title == TitleId.GS1)
			{
				if ((GSStatic.global_work_.scenario == 20 && GSStatic.tantei_work_.siteki_no == 8) || (GSStatic.global_work_.scenario == 20 && GSStatic.tantei_work_.siteki_no == 9))
				{
					advCtrl.instance.sub_window_.SetReq(SubWindow.Req._3D_POINT);
					advCtrl.instance.sub_window_.point_3d_ = 1;
				}
				else
				{
					advCtrl.instance.sub_window_.SetReq(SubWindow.Req.POINT);
				}
			}
			else
			{
				advCtrl.instance.sub_window_.SetReq(SubWindow.Req.POINT);
			}
			if ((message_work.status & Status.POINT_TO_1ST) != 0)
			{
				soundCtrl.instance.PlaySE(49);
				message_work.status &= ~Status.POINT_TO_1ST;
			}
		}
		else if (advCtrl.instance.sub_window_.point_3d_ == 1)
		{
			if ((message_work.status & Status.POINT_TO) != 0)
			{
				if (advCtrl.instance.sub_window_.IsBusy())
				{
					return true;
				}
				if (scienceInvestigationCtrl.instance.called_present)
				{
					message_work.status &= ~Status.POINT_TO;
					advCtrl.instance.sub_window_.SetReq(SubWindow.Req.POINT_EXIT);
					return false;
				}
				if ((message_work.status & Status.LOOP) == 0)
				{
					return false;
				}
			}
		}
		else if ((message_work.status & Status.POINT_TO) != 0)
		{
			if (advCtrl.instance.sub_window_.IsBusy())
			{
				return true;
			}
			PointMiniGame instance = PointMiniGame.instance;
			if (instance.is_running)
			{
				return true;
			}
			message_work.status &= ~Status.POINT_TO;
			return false;
		}
		return true;
	}

	private static bool CodeProc_40(MessageWork message_work)
	{
		message_work.mdt_index++;
		message_work.status &= ~Status.POINT_CURSOL_ON;
		return false;
	}

	private static bool CodeProc_41(MessageWork message_work)
	{
		message_work.mdt_index++;
		advCtrl.instance.sub_window_.SetReq(SubWindow.Req.RT_TMAIN);
		if (advCtrl.instance.sub_window_.GetCurrentRoutine().r.no_0 == 10)
		{
		}
		GSStatic.global_work_.r.Set(5, 1, 0, 0);
		return false;
	}

	private static bool CodeProc_42(MessageWork message_work)
	{
		message_work.mdt_index++;
		if (message_work.mdt_data.GetMessage(message_work.mdt_index) != 0)
		{
			message_work.sound_flag &= ~SoundFlag.MSE_OFF;
		}
		else
		{
			message_work.sound_flag |= SoundFlag.MSE_OFF;
		}
		message_work.mdt_index++;
		return false;
	}

	private static bool CodeProc_43(MessageWork message_work)
	{
		message_work.mdt_index++;
		switch (message_work.mdt_data.GetMessage(message_work.mdt_index))
		{
		case 0:
			if (lifeGaugeCtrl.instance.body_active && GSStatic.global_work_.rest > 0)
			{
				lifeGaugeCtrl.instance.ActionLifeGauge(lifeGaugeCtrl.Gauge_State.LIFE_OFF);
			}
			break;
		case 1:
			lifeGaugeCtrl.instance.ActionLifeGauge(lifeGaugeCtrl.Gauge_State.LIFE_ON);
			lifeGaugeCtrl.instance.ActionLifeGauge(lifeGaugeCtrl.Gauge_State.NOTICE_DAMAGE);
			break;
		case 2:
			lifeGaugeCtrl.instance.ActionLifeGauge(lifeGaugeCtrl.Gauge_State.LIFE_OFF);
			break;
		}
		message_work.mdt_index++;
		return false;
	}

	private static bool CodeProc_44(MessageWork message_work)
	{
		message_work.mdt_index++;
		GlobalWork global_work_ = GSStatic.global_work_;
		global_work_.r_bk.CopyFrom(ref global_work_.r);
		ushort message = message_work.mdt_data.GetMessage(message_work.mdt_index);
		if (message != 0)
		{
			global_work_.r.Set(10, 0, 0, 0);
		}
		else
		{
			global_work_.r.Set(10, 0, 0, 1);
		}
		message_work.mdt_index++;
		judgmentCtrl.instance.judgment(message);
		return false;
	}

	private static bool CodeProc_46(MessageWork message_work)
	{
		message_work.mdt_index++;
		ushort message = message_work.mdt_data.GetMessage(message_work.mdt_index);
		int num = message & 0xF;
		cutInCtrl.instance.cutIn(num);
		if (GSStatic.global_work_.title == TitleId.GS1 || GSStatic.global_work_.title == TitleId.GS2 || GSStatic.global_work_.title == TitleId.GS3)
		{
		}
		if ((message & 0x10) == 0)
		{
			bgCtrl.instance.StartCutUpScroll(num);
		}
		if ((message & 0x10) == 0)
		{
			int num2 = 0;
		}
		else if (GSStatic.global_work_.title == TitleId.GS3)
		{
			int num2;
			switch (num)
			{
			case 0:
				num2 = 60;
				break;
			case 1:
				num2 = 60;
				break;
			case 3:
				num2 = 60;
				break;
			case 2:
				num2 = -50;
				break;
			case 4:
				num2 = -50;
				break;
			default:
				num2 = 0;
				break;
			}
			cutInCtrl.instance.SetBodyPosition(num2);
			AnimationSystem.Instance.CharacterAnimationObject.transform.localPosition += Vector3.left * num2 * 6.75f;
		}
		if (GSStatic.global_work_.title == TitleId.GS1 || GSStatic.global_work_.title == TitleId.GS2 || GSStatic.global_work_.title == TitleId.GS3)
		{
		}
		if ((message & 0x20u) != 0)
		{
			bgCtrl.instance.setVisible(false);
		}
		message_work.mdt_index++;
		return false;
	}

	private static bool CodeProc_47(MessageWork message_work)
	{
		message_work.mdt_index++;
		ushort message = message_work.mdt_data.GetMessage(message_work.mdt_index);
		message_work.mdt_index++;
		ushort message2 = message_work.mdt_data.GetMessage(message_work.mdt_index);
		message_work.mdt_index++;
		soundCtrl.instance.VolumeChangeBGM(message, message2);
		return false;
	}

	private static bool CodeProc_48(MessageWork message_work)
	{
		message_work.mdt_index++;
		if (message_work.mdt_data.GetMessage(message_work.mdt_index) == ushort.MaxValue)
		{
			if (GSUtility.GetLanguageLayoutType(GSStatic.global_work_.language) == Language.JAPAN)
			{
				messageBoardCtrl.instance.SetPos(0f, 42f);
			}
			else
			{
				messageBoardCtrl.instance.SetPos(0f, 80f);
			}
		}
		else
		{
			messageBoardCtrl.instance.Close();
			short num = (short)message_work.mdt_data.GetMessage(message_work.mdt_index);
			short num2 = (short)message_work.mdt_data.GetMessage(message_work.mdt_index + 1);
			if (num == 0 && num2 == 142)
			{
				num = num;
			}
			else if (num > 0)
			{
				num = (short)((float)num * 6.75f);
				num = num;
			}
			num2 = (short)((float)(-num2 + 128) * 4.2f + 190f);
			if (GSUtility.GetLanguageLayoutType(GSStatic.global_work_.language) == Language.USA)
			{
				num2 += 38;
			}
			messageBoardCtrl.instance.SetPos(num, num2);
		}
		message_work.mdt_index += 2u;
		return false;
	}

	private static bool CodeProc_49(MessageWork message_work)
	{
		message_work.mdt_index++;
		GSStatic.global_work_.Mess_move_flag = 0;
		message_work.message_trans_flag = 0;
		GSMain_SaibanPart.GS3DS_saiban_exit();
		GSStatic.global_work_.r.Set(1, 0, 0, 0);
		return true;
	}

	private static bool CodeProc_4a(MessageWork message_work)
	{
		if (message_work.mdt_data.GetMessage(message_work.mdt_index + 1) != 0)
		{
			if (!judgmentCtrl.instance.is_guilty_play)
			{
				message_work.mdt_index += 2u;
				return false;
			}
		}
		else if (!judgmentCtrl.instance.is_not_guilty_play)
		{
			message_work.mdt_index += 2u;
			return false;
		}
		return true;
	}

	private static bool CodeProc_4b(MessageWork message_work)
	{
		message_work.mdt_index++;
		int message = message_work.mdt_data.GetMessage(message_work.mdt_index);
		message_work.mdt_index++;
		ushort message2 = message_work.mdt_data.GetMessage(message_work.mdt_index);
		message_work.mdt_index++;
		int explCharWorkIndexById = GSMapIcon.GetExplCharWorkIndexById(message);
		ExplCharData[] expl_char_data_ = GSStatic.expl_char_work_.expl_char_data_;
		if (explCharWorkIndexById != 255)
		{
			int num = (message2 & 3) << 12;
			expl_char_data_[explCharWorkIndexById].para1 &= 53247;
			expl_char_data_[explCharWorkIndexById].para1 += (ushort)num;
		}
		GSMapIcon.instance.IconSetChangeUV(explCharWorkIndexById, message2);
		expl_char_data_[explCharWorkIndexById].timer = 0;
		return false;
	}

	private static bool CodeProc_4c(MessageWork message_work)
	{
		if (bgCtrl.instance.is_scroll)
		{
			return true;
		}
		message_work.mdt_index++;
		return false;
	}

	private static bool CodeProc_4d(MessageWork message_work)
	{
		message_work.mdt_index++;
		int message = message_work.mdt_data.GetMessage(message_work.mdt_index);
		bgCtrl.instance.SetSprite(message);
		message_work.mdt_index++;
		ushort message2 = message_work.mdt_data.GetMessage(message_work.mdt_index);
		bool enable = message2 == 1;
		bgCtrl.instance.ChangeSubSpriteEnable(enable);
		if (message2 == 0)
		{
			bgCtrl.instance.Bg256_SP_Flag |= 1;
		}
		else
		{
			bgCtrl.instance.Bg256_SP_Flag &= 254;
		}
		message_work.mdt_index++;
		return false;
	}

	private static bool CodeProc_4e(MessageWork message_work)
	{
		if ((message_work.work & 0xFF) == 0)
		{
			AnimationSystem.Instance.GoIdle();
			message_work.work |= (ushort)(message_work.mdt_data.GetMessage(message_work.mdt_index + 1) & 0xFF);
		}
		else
		{
			message_work.work--;
		}
		if ((message_work.status & Status.FAST_MESSAGE) != 0)
		{
			if ((message_work.status & Status.READ_MESSAGE) != 0 && (message_work.status & Status.FAST_MESSAGE) != 0 && (message_work.status & Status.LAST_MESSAGE) != 0)
			{
				message_work.work &= 65280;
			}
			else
			{
				message_work.status &= ~Status.FAST_MESSAGE;
			}
		}
		if (advCtrl.instance.message_system_.DebugSkipMdtIndex(message_work))
		{
			message_work.work &= 65280;
		}
		if ((message_work.work & 0xFFu) != 0)
		{
			return true;
		}
		AnimationSystem.Instance.GoIdle();
		message_work.mdt_index += 2u;
		return false;
	}

	private static bool CodeProc_4f(MessageWork message_work)
	{
		message_work.mdt_index++;
		ushort message = message_work.mdt_data.GetMessage(message_work.mdt_index);
		GSStatic.global_work_.psy_no = (sbyte)message;
		PsylockData psylockData = GSStatic.global_work_.psylock[message];
		GSStatic.global_work_.psylock[message].status |= 1u;
		message_work.mdt_index++;
		ushort message2 = message_work.mdt_data.GetMessage(message_work.mdt_index);
		if (message2 != ushort.MaxValue)
		{
			psylockData.level = (psylockData.size = (byte)message2);
		}
		message_work.mdt_index++;
		ushort message3 = message_work.mdt_data.GetMessage(message_work.mdt_index);
		if (message3 != ushort.MaxValue)
		{
			psylockData.pl_id = message3;
		}
		message_work.mdt_index++;
		ushort message4 = message_work.mdt_data.GetMessage(message_work.mdt_index);
		if (message4 != ushort.MaxValue)
		{
			psylockData.room = message4;
		}
		message_work.mdt_index++;
		ushort message5 = message_work.mdt_data.GetMessage(message_work.mdt_index);
		if (message5 != ushort.MaxValue)
		{
			psylockData.start_message = message5;
		}
		message_work.mdt_index++;
		ushort message6 = message_work.mdt_data.GetMessage(message_work.mdt_index);
		if (message6 != ushort.MaxValue)
		{
			psylockData.cancel_message = message6;
		}
		message_work.mdt_index++;
		ushort message7 = message_work.mdt_data.GetMessage(message_work.mdt_index);
		if (message7 != ushort.MaxValue)
		{
			psylockData.die_message = message7;
		}
		message_work.mdt_index++;
		return false;
	}

	private static bool CodeProc_50(MessageWork message_work)
	{
		message_work.mdt_index++;
		GSStatic.global_work_.psylock[message_work.mdt_data.GetMessage(message_work.mdt_index)].Clear();
		message_work.mdt_index++;
		return false;
	}

	private static bool CodeProc_51(MessageWork message_work)
	{
		message_work.mdt_index++;
		ushort message = message_work.mdt_data.GetMessage(message_work.mdt_index);
		message_work.mdt_index++;
		ushort message2 = message_work.mdt_data.GetMessage(message_work.mdt_index);
		message_work.mdt_index++;
		GSMain_TanteiPart.room_seq_chg(message, message2);
		return false;
	}

	private static bool CodeProc_52(MessageWork message_work)
	{
		message_work.mdt_index++;
		int message = message_work.mdt_data.GetMessage(message_work.mdt_index);
		GSStatic.global_work_.psy_menu_active_flag |= (byte)message;
		advCtrl.instance.sub_window_.SetReq(SubWindow.Req.MAGATAMA_MENU_ON);
		message_work.mdt_index++;
		return false;
	}

	private static bool CodeProc_53(MessageWork message_work)
	{
		message_work.mdt_index++;
		GSStatic.global_work_.psy_menu_active_flag = 0;
		return false;
	}

	private static bool CodeProc_54(MessageWork message_work)
	{
		message_work.mdt_index++;
		ushort message = message_work.mdt_data.GetMessage(message_work.mdt_index);
		message_work.mdt_index++;
		ushort message2 = message_work.mdt_data.GetMessage(message_work.mdt_index);
		message_work.mdt_index++;
		switch (message)
		{
		case 0:
			lifeGaugeCtrl.instance.lifegauge_set_move(message2);
			break;
		case 1:
			GSStatic.global_work_.gauge_disp_flag = (short)message2;
			break;
		case 2:
			GSStatic.global_work_.gauge_dmg_cnt = (short)lifeGaugeCtrl.DamageAjust(message2);
			if (GSStatic.global_work_.gauge_dmg_cnt != 0)
			{
				lifeGaugeCtrl.instance.ActionLifeGauge(lifeGaugeCtrl.Gauge_State.NOTICE_DAMAGE);
			}
			else
			{
				lifeGaugeCtrl.instance.ActionLifeGauge(lifeGaugeCtrl.Gauge_State.STOP_NOTICE);
			}
			break;
		case 3:
			GSStatic.global_work_.gauge_disp_flag = 1;
			GSStatic.global_work_.gauge_rno_0 = 0;
			GSStatic.global_work_.lifegauge_init_hp();
			break;
		case 4:
			lifeGaugeCtrl.instance.ActionLifeGauge(lifeGaugeCtrl.Gauge_State.LIFE_OFF_MOMENT);
			break;
		case 5:
			lifeGaugeCtrl.instance.ActionLifeGauge(lifeGaugeCtrl.Gauge_State.LIFE_ON_MOMENT);
			break;
		}
		return true;
	}

	private static bool CodeProc_55(MessageWork message_work)
	{
		ushort message = message_work.mdt_data.GetMessage(message_work.mdt_index + 1);
		ushort message2 = message_work.mdt_data.GetMessage(message_work.mdt_index + 2);
		if ((message_work.work & 0xFF) == 0)
		{
			ushort num = message;
			switch (message2)
			{
			case 0:
			case 1:
				if (GSStatic.global_work_.title != TitleId.GS3 || GSStatic.global_work_.scenario != 11)
				{
					break;
				}
				if (message2 == 0)
				{
					if (num == 95)
					{
						num = 183;
						bgCtrl.instance.SetSprite(num);
						message_work.mdt_index += 3u;
						return true;
					}
					message_work.mdt_index += 3u;
					return false;
				}
				message_work.mdt_index += 3u;
				return false;
			default:
				bgCtrl.instance.SetSprite(num);
				break;
			case 2:
			case 3:
			case 5:
			case 6:
			case 9:
				bgCtrl.instance.bg_no_reserve = num;
				message_work.mdt_index += 3u;
				return false;
			}
			message_work.work |= 1;
			return true;
		}
		message_work.work &= 65280;
		message_work.mdt_index += 3u;
		return false;
	}

	private static bool CodeProc_56(MessageWork message_work)
	{
		return false;
	}

	private static bool CodeProc_57(MessageWork message_work)
	{
		message_work.mdt_index++;
		GlobalWork global_work_ = GSStatic.global_work_;
		GSStatic.global_work_.psy_no = (sbyte)message_work.mdt_data.GetMessage(message_work.mdt_index);
		global_work_.r_bk.CopyFrom(ref global_work_.r);
		global_work_.r.Set(5, 11, 9, 0);
		message_work.mdt_index++;
		return true;
	}

	private static bool CodeProc_58(MessageWork message_work)
	{
		message_work.mdt_index++;
		GSPsylock.PsylockDisp_reset_static();
		return false;
	}

	private static bool CodeProc_59(MessageWork message_work)
	{
		message_work.mdt_index++;
		GSStatic.global_work_.lockdat[GSStatic.global_work_.lock_max] = GSStatic.talk_psy_data_[message_work.mdt_data.GetMessage(message_work.mdt_index)];
		GSStatic.global_work_.lock_max++;
		message_work.mdt_index++;
		return false;
	}

	private static bool CodeProc_5a(MessageWork message_work)
	{
		message_work.mdt_index++;
		ushort message = message_work.mdt_data.GetMessage(message_work.mdt_index);
		message_work.mdt_index++;
		for (ushort num = 0; num < GSStatic.global_work_.lock_max; num++)
		{
			if (GSStatic.global_work_.lockdat[num] == GSStatic.talk_psy_data_[message])
			{
				GSStatic.global_work_.lockdat[num] = 0;
			}
		}
		for (ushort num2 = 0; num2 < GSStatic.global_work_.lock_max; num2++)
		{
			if (GSStatic.global_work_.lockdat[num2] == 0)
			{
				ushort num3 = (ushort)(num2 + 1);
				ushort num4 = num2;
				while (num3 < GSStatic.global_work_.lock_max)
				{
					GSStatic.global_work_.lockdat[num4] = GSStatic.global_work_.lockdat[num3];
					num3++;
					num4++;
				}
			}
		}
		GSStatic.global_work_.lock_max--;
		return false;
	}

	private static bool CodeProc_5b(MessageWork message_work)
	{
		message_work.mdt_index++;
		ushort message = message_work.mdt_data.GetMessage(message_work.mdt_index);
		message_work.mdt_index++;
		ushort message2 = message_work.mdt_data.GetMessage(message_work.mdt_index);
		message_work.mdt_index++;
		GSMain_TanteiPart.tantei_menu_recov(message, message2);
		return false;
	}

	private static bool CodeProc_5c(MessageWork message_work)
	{
		if (!is_mosaic_run_)
		{
			uint message = message_work.mdt_data.GetMessage(message_work.mdt_index + 2);
			uint message2 = message_work.mdt_data.GetMessage(message_work.mdt_index + 3);
			switch (message_work.mdt_data.GetMessage(message_work.mdt_index + 1))
			{
			case 1:
				fadeCtrl.instance.play(fadeCtrl.Status.FADE_IN, message * message2, message2);
				break;
			case 2:
				fadeCtrl.instance.play(fadeCtrl.Status.FADE_OUT, message * message2, message2);
				break;
			default:
				Debug.LogError("モザイクの設定 Error type : " + message_work.mdt_data.GetMessage(message_work.mdt_index + 1));
				break;
			}
			is_mosaic_run_ = true;
			return true;
		}
		if (!fadeCtrl.instance.is_end)
		{
			return true;
		}
		is_mosaic_run_ = false;
		message_work.mdt_index += 4u;
		return false;
	}

	private static bool CodeProc_5d(MessageWork message_work)
	{
		message_work.mdt_index++;
		switch (message_work.mdt_data.GetMessage(message_work.mdt_index))
		{
		case 0:
			message_work.text_flag &= ~TextFlag.POSITION_MASK;
			break;
		case 1:
			message_work.text_flag &= ~TextFlag.POSITION_MASK;
			message_work.text_flag |= TextFlag.POSITION_CENTER;
			break;
		case 2:
			message_work.text_flag &= ~TextFlag.POSITION_MASK;
			message_work.text_flag |= TextFlag.POSITION_CENTER_UP;
			break;
		case 3:
			message_work.text_flag |= TextFlag.SIZE_ZOOM;
			break;
		case 4:
			message_work.text_flag &= ~TextFlag.SIZE_MASK;
			break;
		case 5:
			message_work.text_flag |= TextFlag.GRADATION;
			message_work.message_text.SetGradationColor(true);
			break;
		}
		message_work.mdt_index++;
		return false;
	}

	private static bool CodeProc_5e(MessageWork message_work)
	{
		return false;
	}

	private static bool CodeProc_5f(MessageWork message_work)
	{
		message_work.mdt_index++;
		ushort message = message_work.mdt_data.GetMessage(message_work.mdt_index++);
		ushort message2 = message_work.mdt_data.GetMessage(message_work.mdt_index++);
		ushort message3 = message_work.mdt_data.GetMessage(message_work.mdt_index++);
		spefCtrl.instance.Monochrome_set(message, message2, message3);
		return false;
	}

	private static bool CodeProc_60(MessageWork message_work)
	{
		message_work.mdt_index++;
		PsylockData psylockData = GSStatic.global_work_.psylock[message_work.mdt_data.GetMessage(message_work.mdt_index)];
		message_work.mdt_index++;
		psylockData.item_size = 0u;
		psylockData.item_no[0] = (byte)message_work.mdt_data.GetMessage(message_work.mdt_index);
		message_work.mdt_index++;
		psylockData.item_correct_message[0] = message_work.mdt_data.GetMessage(message_work.mdt_index);
		message_work.mdt_index++;
		psylockData.wrong_message = message_work.mdt_data.GetMessage(message_work.mdt_index);
		message_work.mdt_index++;
		psylockData.item_size++;
		return false;
	}

	private static bool CodeProc_61(MessageWork message_work)
	{
		message_work.mdt_index++;
		PsylockData psylockData = GSStatic.global_work_.psylock[message_work.mdt_data.GetMessage(message_work.mdt_index)];
		message_work.mdt_index++;
		psylockData.item_no[psylockData.item_size] = (byte)message_work.mdt_data.GetMessage(message_work.mdt_index);
		message_work.mdt_index++;
		psylockData.item_correct_message[psylockData.item_size] = message_work.mdt_data.GetMessage(message_work.mdt_index);
		message_work.mdt_index++;
		psylockData.item_size++;
		return false;
	}

	private static bool CodeProc_62(MessageWork message_work)
	{
		GSPsylock.PsylockDisp_to_normal_bg();
		GSPsylock.PsylockDisp_move();
		message_work.mdt_index++;
		return false;
	}

	private static bool CodeProc_63(MessageWork message_work)
	{
		GSPsylock.PsylockDisp_redisp();
		GSPsylock.PsylockDisp_move();
		message_work.mdt_index++;
		return false;
	}

	private static bool CodeProc_64(MessageWork message_work)
	{
		GSStatic.global_work_.r.Set(5, 11, 4, 0);
		message_work.mdt_index += 2u;
		return true;
	}

	private static bool CodeProc_65(MessageWork message_work)
	{
		message_work.mdt_index++;
		ushort message = message_work.mdt_data.GetMessage(message_work.mdt_index);
		message_work.mdt_index++;
		ushort message2 = message_work.mdt_data.GetMessage(message_work.mdt_index);
		message_work.mdt_index++;
		bgCtrl.instance.SetParts(message, message2 == 1);
		return false;
	}

	private static bool CodeProc_66(MessageWork message_work)
	{
		message_work.mdt_index++;
		GSStatic.global_work_.psy_no = (sbyte)message_work.mdt_data.GetMessage(message_work.mdt_index);
		message_work.mdt_index++;
		PsylockData psylockData = GSStatic.global_work_.psylock[GSStatic.global_work_.psy_no];
		psylockData.cancel_bgm = message_work.mdt_data.GetMessage(message_work.mdt_index);
		message_work.mdt_index++;
		psylockData.unlock_bgm = message_work.mdt_data.GetMessage(message_work.mdt_index);
		message_work.mdt_index++;
		return false;
	}

	private static bool CodeProc_67(MessageWork message_work)
	{
		message_work.mdt_index++;
		Message_init();
		return false;
	}

	private static bool CodeProc_68(MessageWork message_work)
	{
		message_work.mdt_index++;
		AnimationSystem.Instance.Char_monochrome(1, 31, 0, true);
		AnimationSystem.Instance.CtrlChinamiObj(4);
		return false;
	}

	private static bool CodeProc_69(MessageWork message_work)
	{
		ushort message;
		ushort num = (message = message_work.mdt_data.GetMessage(message_work.mdt_index + 1));
		num >>= 8;
		message = (ushort)(message & 0xFFu);
		ushort message2;
		ushort num2 = (message2 = message_work.mdt_data.GetMessage(message_work.mdt_index + 2));
		num2 >>= 8;
		message2 = (ushort)(message2 & 0xFFu);
		if (num2 >> 3 > 0)
		{
			message_work.op_flg = (byte)((uint)((num2 & 0xF0) >> 1) | (message_work.op_flg & 0xFu));
			if ((message_work.op_flg & 0xF0) >> 4 == 1)
			{
			}
			message_work.mdt_index += 3u;
			if (GSStatic.global_work_.title == TitleId.GS1 && GSStatic.global_work_.scenario == 34 && message_work.now_no == scenario.SC4_70260)
			{
				GSStatic.global_work_.status_flag |= 2048u;
			}
			return false;
		}
		message_work.op_no = (byte)num;
		message_work.op_workno = (byte)message;
		message_work.op_flg = (byte)((message_work.op_flg & 0xF0u) | num2);
		message_work.op_para = (byte)message2;
		if (message_work.op_no == 0 && message_work.op_workno == 98 && message_work.op_para == 200)
		{
			num2 = 1;
			message_work.op_flg = (byte)((message_work.op_flg & 0xF0u) | num2);
		}
		switch ((byte)(message_work.op_flg & 0xF))
		{
		case 1:
			GSDemo.Play(message_work);
			return true;
		case 2:
			GSDemo.Play(message_work);
			break;
		}
		message_work.mdt_index += 3u;
		return false;
	}

	private static bool CodeProc_6a(MessageWork message_work)
	{
		message_work.mdt_index++;
		if (GSStatic.global_work_.title == TitleId.GS3)
		{
			advCtrl.instance.message_system_.AddScript(message_work.mdt_data.GetMessage(message_work.mdt_index));
			return false;
		}
		byte scenario_no = (byte)message_work.mdt_data.GetMessage(message_work.mdt_index);
		GSStatic.global_work_.scenario = scenario_no;
		string scenarioMdtPath = GSScenario.GetScenarioMdtPath(scenario_no);
		advCtrl.instance.message_system_.LoadScenarioMdtFromStreamingAssets(scenarioMdtPath);
		advCtrl.instance.message_system_.SetMessage(128u);
		return false;
	}

	private static bool CodeProc_6b(MessageWork message_work)
	{
		message_work.mdt_index++;
		message_work.all_work[0] = message_work.mdt_data.GetMessage(message_work.mdt_index);
		message_work.mdt_index++;
		message_work.all_work[1] = message_work.mdt_data.GetMessage(message_work.mdt_index);
		message_work.mdt_index++;
		message_work.all_work[2] = message_work.mdt_data.GetMessage(message_work.mdt_index);
		message_work.mdt_index++;
		return false;
	}

	private static bool CodeProc_6c(MessageWork message_work)
	{
		message_work.mdt_index++;
		if (GSStatic.global_work_.title == TitleId.GS2)
		{
			StatusWork status_work_ = GSStatic.status_work_;
			status_work_.page_status &= 251;
			status_work_.page_status |= 32;
			GlobalWork global_work_ = GSStatic.global_work_;
			global_work_.r_bk.CopyFrom(ref global_work_.r);
			global_work_.r.Set(8, 7, 0, 0);
		}
		else if (GSStatic.global_work_.title == TitleId.GS3)
		{
			ushort message = message_work.mdt_data.GetMessage(message_work.mdt_index);
			message_work.mdt_index++;
			if (message == 0)
			{
				if (GSStatic.talk_work_.talk_data_[scenario_GS3.SC1_0_TALK_YAHARI00].flag[2] == 255)
				{
					if (message_work.now_no == scenario_GS3.SC1_0_33660)
					{
						GSStatic.talk_work_.talk_data_[scenario_GS3.SC1_0_TALK_YAHARI00].tag[2] = 30u;
						GSStatic.talk_work_.talk_data_[scenario_GS3.SC1_0_TALK_YAHARI00].flag[2] = scenario_GS3.TDF_SC1_0_33670;
						GSStatic.talk_work_.talk_data_[scenario_GS3.SC1_0_TALK_YAHARI00].mess[2] = scenario_GS3.SC1_0_33670;
					}
					if (message_work.now_no == scenario_GS3.SC1_0_33690)
					{
						GSStatic.talk_work_.talk_data_[scenario_GS3.SC1_0_TALK_YAHARI00].tag[2] = 31u;
						GSStatic.talk_work_.talk_data_[scenario_GS3.SC1_0_TALK_YAHARI00].flag[2] = scenario_GS3.TDF_SC1_0_33680;
						GSStatic.talk_work_.talk_data_[scenario_GS3.SC1_0_TALK_YAHARI00].mess[2] = scenario_GS3.SC1_0_33680;
					}
				}
				else if (GSStatic.talk_work_.talk_data_[scenario_GS3.SC1_0_TALK_YAHARI00].flag[3] == 255)
				{
					if (message_work.now_no == scenario_GS3.SC1_0_33660 && GSStatic.talk_work_.talk_data_[scenario_GS3.SC1_0_TALK_YAHARI00].tag[2] == 30)
					{
						return false;
					}
					if (message_work.now_no == scenario_GS3.SC1_0_33690 && GSStatic.talk_work_.talk_data_[scenario_GS3.SC1_0_TALK_YAHARI00].tag[2] == 31)
					{
						return false;
					}
					GSStatic.talk_work_.talk_data_[scenario_GS3.SC1_0_TALK_YAHARI00].tag[2] = 30u;
					GSStatic.talk_work_.talk_data_[scenario_GS3.SC1_0_TALK_YAHARI00].flag[2] = scenario_GS3.TDF_SC1_0_33670;
					GSStatic.talk_work_.talk_data_[scenario_GS3.SC1_0_TALK_YAHARI00].mess[2] = scenario_GS3.SC1_0_33670;
					GSStatic.talk_work_.talk_data_[scenario_GS3.SC1_0_TALK_YAHARI00].tag[3] = 31u;
					GSStatic.talk_work_.talk_data_[scenario_GS3.SC1_0_TALK_YAHARI00].flag[3] = scenario_GS3.TDF_SC1_0_33680;
					GSStatic.talk_work_.talk_data_[scenario_GS3.SC1_0_TALK_YAHARI00].mess[3] = scenario_GS3.SC1_0_33680;
				}
			}
		}
		return false;
	}

	private static bool CodeProc_6d(MessageWork message_work)
	{
		message_work.mdt_index++;
		switch (message_work.mdt_data.GetMessage(message_work.mdt_index))
		{
		case 0:
			Mess_window_set(4u);
			GSStatic.global_work_.r.Set(5, 12, 0, 0);
			advCtrl.instance.sub_window_.GetCurrentRoutine().r.Set(22, 0, 0, 0);
			break;
		}
		message_work.mdt_index++;
		return false;
	}

	private static bool CodeProc_6e(MessageWork message_work)
	{
		message_work.mdt_index++;
		int message = message_work.mdt_data.GetMessage(message_work.mdt_index);
		if (message == 1)
		{
			message_work.status |= Status.THREE_LINE;
		}
		else
		{
			message_work.status &= ~Status.THREE_LINE;
		}
		message_work.mdt_index++;
		return false;
	}

	private static bool CodeProc_6f(MessageWork message_work)
	{
		message_work.mdt_index++;
		GSStatic.global_work_.Bk_end_mess = message_work.mdt_data.GetMessage(message_work.mdt_index);
		message_work.mdt_index++;
		return false;
	}

	private static bool CodeProc_70(MessageWork message_work)
	{
		return false;
	}

	private static bool CodeProc_71(MessageWork message_work)
	{
		message_work.mdt_index++;
		message_work.mdt_index++;
		int message = message_work.mdt_data.GetMessage(message_work.mdt_index);
		message_work.mdt_index++;
		message_work.mdt_index++;
		switch (message)
		{
		case 0:
			GSStatic.global_work_.r.Set(5, 11, 4, 0);
			GSStatic.global_work_.psy_unlock_not_unlock_message = 1;
			break;
		case 1:
			Mess_window_set(8u);
			GSStatic.global_work_.r.Set(5, 11, 7, 0);
			GSStatic.global_work_.psy_unlock_not_unlock_message = 0;
			break;
		}
		return true;
	}

	private static bool CodeProc_74(MessageWork message_work)
	{
		message_work.mdt_index++;
		sbyte b = (sbyte)message_work.mdt_data.GetMessage(message_work.mdt_index);
		message_work.mdt_index++;
		sbyte b2 = (sbyte)message_work.mdt_data.GetMessage(message_work.mdt_index);
		message_work.mdt_index++;
		SubWindow sub_window_ = advCtrl.instance.sub_window_;
		switch (b)
		{
		case 1:
			messageBoardCtrl.instance.guide_ctrl.next_guid = guideCtrl.GuideType.HOUTEI;
			sub_window_.SetReq(SubWindow.Req.QUESTIONING_EXIT);
			break;
		case 2:
			if (GSStatic.global_work_.r.no_0 == 5)
			{
				sub_window_.SetReq(SubWindow.Req.TANTEI);
			}
			else if (GSStatic.global_work_.r.no_0 == 4)
			{
				sub_window_.SetReq(SubWindow.Req.HOUTEI);
			}
			break;
		case 3:
			sub_window_.SetReq(SubWindow.Req.BLANK);
			break;
		case 4:
			sub_window_.SetReq(SubWindow.Req.TANTEI);
			break;
		case 5:
			if (FingerMiniGame.instance.is_running)
			{
				FingerMiniGame.instance.SetReq(33);
			}
			else
			{
				sub_window_.SetReq(SubWindow.Req.MESS_EXIT);
			}
			break;
		case 6:
			switch (b2)
			{
			case 0:
				sub_window_.SetReq(SubWindow.Req.LUMINOL);
				break;
			case 1:
				sub_window_.SetReq(SubWindow.Req.LUMINOL_ANM);
				break;
			}
			break;
		case 7:
			sub_window_.SetReq(SubWindow.Req.OPENING);
			break;
		case 8:
			if (b2 == 52)
			{
				KinkoMiniGame.instance.Init();
			}
			else if (b2 == 51)
			{
				VaseShowMiniGame.instance.Init();
			}
			else if (b2 == 62)
			{
				DyingMessageMiniGame.instance.StartDyingMessage();
			}
			else if (b2 == 50)
			{
				VasePuzzleMiniGame.instance.startVasePuzzle(GSFlag.Check(0u, scenario.SCE4_FLAG_JAR_PUZZZLE));
			}
			else if (FingerMiniGame.instance.is_running)
			{
				FingerMiniGame.instance.SetReq((byte)b2);
			}
			else if (b2 == 46)
			{
				sub_window_.SetReq(SubWindow.Req.STATUS_3D);
			}
			else if (b2 == 48)
			{
				coroutineCtrl.instance.Play(scienceInvestigationCtrl.instance.Play(InvestigateType.PRESENT, 11));
			}
			else if (b2 == 64)
			{
				staffrollCtrl.instance.play();
			}
			else if (b2 == 65)
			{
				staffrollCtrl.instance.stop();
			}
			else
			{
				sub_window_.SetReq((SubWindow.Req)b2);
			}
			break;
		case 9:
			sub_window_.SetReq(SubWindow.Req.FINGER);
			GSStatic.global_work_.r.no_3 = (byte)b2;
			break;
		case 10:
			sub_window_.SetReq(SubWindow.Req.HUMAN);
			GSStatic.global_work_.r.no_3 = (byte)b2;
			break;
		case 11:
			GSStatic.global_work_.r.Set(8, 0, 0, 0);
			sub_window_.status_force_ = 1;
			break;
		case 12:
			debugLogger.instance.Log("SubWindowStack", "++stack_");
			sub_window_.stack_++;
			sub_window_.GetCurrentRoutine().r.Set(19, 0, 0, 0);
			GSStatic.global_work_.r.no_1 = 10;
			staffrollCtrl.instance.init();
			break;
		case 13:
			sub_window_.GetCurrentRoutine().Rno_4 = (byte)b2;
			break;
		case 15:
			TrophyCtrl.set_flag(b2);
			break;
		}
		return false;
	}

	private static bool CodeProc_75(MessageWork message_work)
	{
		message_work.mdt_index++;
		ushort message = message_work.mdt_data.GetMessage(message_work.mdt_index);
		message_work.mdt_index++;
		ushort message2 = message_work.mdt_data.GetMessage(message_work.mdt_index);
		message_work.mdt_index++;
		ushort message3 = message_work.mdt_data.GetMessage(message_work.mdt_index);
		message_work.mdt_index++;
		ushort message4 = message_work.mdt_data.GetMessage(message_work.mdt_index);
		message_work.mdt_index++;
		switch (message)
		{
		case 0:
			Cinema.Cinema_init(message2, message3, message4);
			switch (message3)
			{
			case 2:
				GSStatic.cinema_work_.movie_type = 1;
				ConfrontWithMovie.instance.auto_play = true;
				ConfrontWithMovie.instance.controller.InitData(true);
				ConfrontWithMovie.instance.StartConfront();
				ConfrontWithMovie.instance.controller.SetAutoPlayStatus(message4);
				break;
			case 0:
				GSStatic.cinema_work_.movie_type = 2;
				ConfrontWithMovie.instance.controller.SetVideo();
				break;
			default:
				GSStatic.cinema_work_.movie_type = 0;
				MovieAccessor.Instance.Play(string.Format("film0{0}", message3), false);
				break;
			}
			break;
		case 1:
			Cinema.Cinema_set_status(message2);
			if (GSStatic.cinema_work_.movie_type != 0)
			{
				ConfrontWithMovie.instance.controller.SetAutoPlayStatus(message2);
				if (GSStatic.cinema_work_.movie_type == 1 && (message2 == 1 || message2 == 4) && ConfrontWithMovie.instance.controller.play_coroutine == null)
				{
					ConfrontWithMovie.instance.StartConfront();
					ConfrontWithMovie.instance.controller.SetAutoPlayStatus(message2);
				}
				break;
			}
			switch (message2)
			{
			case 2:
				MovieAccessor.Instance.ForcedStop();
				Cinema.cinema_end();
				break;
			case 16384:
				MovieAccessor.Instance.keepAsLastFrame_ = true;
				Cinema.cinema_end();
				break;
			case 32768:
				MovieAccessor.Instance.SetLoop = true;
				break;
			}
			break;
		case 2:
			Cinema.Cinema_set_frame(message2);
			if (GSStatic.cinema_work_.movie_type != 0)
			{
				ConfrontWithMovie.instance.controller.SetAutoPlayFrame(message2);
			}
			break;
		case 3:
			Cinema.Cinema_set_frame_add(message2);
			if (GSStatic.cinema_work_.movie_type != 0)
			{
				ConfrontWithMovie.instance.controller.SetAutoPlaySpeed(message2);
			}
			break;
		case 4:
			Cinema.Cinema_set_frame_top(message2);
			if (GSStatic.cinema_work_.movie_type != 0)
			{
				ConfrontWithMovie.instance.controller.SetAutoPlayStartFrame(message2);
			}
			break;
		case 5:
			Cinema.Cinema_set_frame_end(message2);
			if (GSStatic.cinema_work_.movie_type != 0)
			{
				ConfrontWithMovie.instance.controller.SetAutoPlayEndFrame(message2);
			}
			break;
		case 6:
			if (GSStatic.cinema_work_.movie_type != 0)
			{
				if (ConfrontWithMovie.instance.controller.Cinema_check_end())
				{
					message_work.mdt_index -= 5u;
					return true;
				}
			}
			else if (MovieAccessor.Instance.Status == MovieAccessor.AccessorStatus.Playing)
			{
				message_work.mdt_index -= 5u;
				return true;
			}
			break;
		case 7:
			Cinema.Cinema_clear_status(message2);
			if (GSStatic.cinema_work_.movie_type != 0)
			{
				ConfrontWithMovie.instance.controller.InitData(true);
			}
			break;
		}
		return false;
	}

	private static bool CodeProc_76(MessageWork message_work)
	{
		message_work.mdt_index++;
		ushort message = message_work.mdt_data.GetMessage(message_work.mdt_index);
		message_work.mdt_index++;
		ushort message2 = message_work.mdt_data.GetMessage(message_work.mdt_index);
		message_work.mdt_index++;
		switch (message)
		{
		case 0:
		case 1:
			facePlateCtrl.instance.entryItem(message2);
			facePlateCtrl.instance.openItem(message, 1f);
			itemPlateCtrl.instance.closeItem(true);
			break;
		case 2:
			facePlateCtrl.instance.closeItem(false);
			break;
		}
		return false;
	}

	private static bool CodeProc_77(MessageWork message_work)
	{
		message_work.mdt_index++;
		soundCtrl.instance.FadeOutSE(message_work.mdt_data.GetMessage(message_work.mdt_index), message_work.mdt_data.GetMessage(message_work.mdt_index + 1));
		message_work.mdt_index += 2u;
		return false;
	}

	private static bool CodeProc_7a(MessageWork message_work)
	{
		message_work.mdt_index++;
		if (GSStatic.global_work_.rest <= 0)
		{
			if ((message_work.status & Status.NO_TALKENDFLG) != 0)
			{
				message_work.status &= ~Status.NO_TALKENDFLG;
			}
			TrophyCtrl.check_trophy_by_mes_no();
			ushort message = message_work.mdt_data.GetMessage(message_work.mdt_index);
			uint message_top = message_work.mdt_index_top;
			message_work.mdt_index = message_work.mdt_data.GetLabelMessageOffset(message, out message_work.now_no, out message_top);
			message_work.mdt_index_top = message_top;
			message_work.now_no += 128;
			if (!TrophyCtrl.disable_check_trophy_by_mes_no)
			{
				GSStatic.message_work_.enable_message_trophy = true;
			}
			message_work.status |= Status.READ_MESSAGE;
		}
		else
		{
			message_work.mdt_index++;
		}
		return false;
	}

	private static bool CodeProc_7b(MessageWork message_work)
	{
		ushort message = message_work.mdt_data.GetMessage(message_work.mdt_index + 1);
		ushort message2 = message_work.mdt_data.GetMessage(message_work.mdt_index + 2);
		switch (message)
		{
		case 0:
			switch (message_work.all_work[0])
			{
			case 0:
				DIconController.instance.Dicon_rno_0 = 1;
				DIconController.instance.Dicon_id = (byte)message2;
				DIconController.instance.Dicon_pos_x = 400;
				DIconController.instance.Dicon_set();
				soundCtrl.instance.PlaySE(51);
				message_work.all_work[0]++;
				break;
			case 1:
				if (DIconController.instance.Dicon_rno_0 == 2)
				{
					message_work.all_work[0]++;
				}
				break;
			case 2:
				message_work.mdt_index += 3u;
				return true;
			}
			break;
		case 1:
			switch (message_work.all_work[0])
			{
			case 0:
				DIconController.instance.Dicon_rno_0 = 3;
				soundCtrl.instance.PlaySE(51);
				message_work.all_work[0]++;
				break;
			case 1:
				if (DIconController.instance.Dicon_rno_0 == 0)
				{
					message_work.all_work[0]++;
				}
				message_work.all_work[0]++;
				break;
			case 2:
				message_work.mdt_index += 3u;
				return true;
			}
			break;
		case 2:
			DIconController.instance.Dicon_rno_0 = 0;
			DIconController.instance.Dicon_disp(0);
			DIconController.instance.Terminate();
			message_work.mdt_index += 3u;
			return true;
		}
		return true;
	}

	private static bool CodeProc_7c(MessageWork message_work)
	{
		if (GS2_OpObjCtrl.instance.sc3_opening_tonosaman_update())
		{
			return true;
		}
		message_work.mdt_index++;
		return false;
	}

	private static bool CodeProc_7d(MessageWork message_work)
	{
		message_work.mdt_index++;
		switch (message_work.mdt_data.GetMessage(message_work.mdt_index))
		{
		case 1:
			GS2_OpObjCtrl.instance.hanabira_move_start();
			break;
		case 0:
			GS2_OpObjCtrl.instance.hanabira_move_end();
			break;
		}
		message_work.mdt_index++;
		return false;
	}

	private static bool CodeProc_7e(MessageWork message_work)
	{
		GS2_OpObjCtrl.instance.spotlight_move_focus();
		message_work.mdt_index += 2u;
		return false;
	}

	private static bool CodeProc_7f(MessageWork message_work)
	{
		int message = message_work.mdt_data.GetMessage(message_work.mdt_index + 1);
		int num = 0;
		num = 5;
		List<KeyType> list = new List<KeyType>();
		list.Add(KeyType.A);
		list.Add(KeyType.B);
		list.Add(KeyType.X);
		list.Add(KeyType.R);
		List<KeyType> list2 = list;
		int keyCodeSpriteNum = keyGuideBase.GuideIcon.GetKeyCodeSpriteNum(padCtrl.instance.GetKeyCode(list2[message]));
		string text = TextDataCtrl.GetText((TextDataCtrl.PlatformTextID)(num * 4 + keyCodeSpriteNum));
		textKeyIconCtrl msg_key_icon = messageBoardCtrl.instance.msg_key_icon;
		text = msg_key_icon.changeTextToIconSpase(text);
		byte message_time = 0;
		while (message_work.work < text.Length)
		{
			if (advCtrl.instance.message_system_.MessageWait(message_work, ref message_time))
			{
				return true;
			}
			msg_key_icon.oldTextWidthSet(messageBoardCtrl.instance.line_list[message_work.message_line]);
			advCtrl.instance.message_system_.SetCharacter(message_work, text[message_work.work]);
			if (message_work.work == text.Length / 2)
			{
				msg_key_icon.load(message_work.message_line);
				msg_key_icon.textIconSet(messageBoardCtrl.instance.line_list[message_work.message_line], list2[message], message_work.message_line);
			}
			message_work.work++;
			if (message_time != 0)
			{
				MessageSE(message_work, message_time);
				return true;
			}
		}
		message_work.work = 0;
		message_work.mdt_index += 2u;
		return false;
	}

	public static void NextGuidSet(MessageWork message_work)
	{
		if (!messageBoardCtrl.instance.is_guide_set())
		{
			if (messageBoardCtrl.instance.guide_ctrl.getEnables())
			{
				messageBoardCtrl.instance.guide_set(false, guideCtrl.GuideType.NO_GUIDE);
			}
			return;
		}
		if (GSStatic.global_work_.r.no_0 == 7)
		{
			if (messageBoardCtrl.instance.guide_ctrl.is_open_close_guid())
			{
				return;
			}
			if (messageBoardCtrl.instance.guide_ctrl.old_guid == guideCtrl.GuideType.QUESTIONING || messageBoardCtrl.instance.guide_ctrl.next_guid != 0)
			{
				messageBoardCtrl.instance.guide_ctrl.changeGuide(messageBoardCtrl.instance.guide_ctrl.old_guid);
				return;
			}
		}
		bool flag = true;
		ushort num = 0;
		while (true)
		{
			ushort message = message_work.mdt_data.GetMessage(message_work.mdt_index + num);
			if (message < 128)
			{
				if (message == 2 || message == 7 || message == 8 || message == 9 || message == 10 || message == 45)
				{
					flag = true;
					break;
				}
				if (message == 46 || message == 22 || message == 63 || message == 62)
				{
					flag = false;
					break;
				}
				if (message == 13 || message == 38 || message == 42)
				{
					return;
				}
				if (message == 116)
				{
					sbyte b = (sbyte)message_work.mdt_data.GetMessage((ushort)(message_work.mdt_index + num + 1));
					sbyte b2 = (sbyte)message_work.mdt_data.GetMessage((ushort)(message_work.mdt_index + num + 2));
					if (b == 8 && (b2 == 62 || b2 == 51))
					{
						flag = false;
						break;
					}
				}
				if (message == 105)
				{
					ushort message2;
					ushort num2 = (message2 = message_work.mdt_data.GetMessage((ushort)(message_work.mdt_index + num + 1)));
					num2 >>= 8;
					message2 = (ushort)(message2 & 0xFFu);
					ushort message3;
					ushort num3 = (message3 = message_work.mdt_data.GetMessage((ushort)(message_work.mdt_index + num + 2)));
					num3 >>= 8;
					message3 = (ushort)(message3 & 0xFFu);
					if (num3 >> 3 > 0 || (message2 == 98 && message3 == 58))
					{
						return;
					}
					num += 2;
				}
				else if (code_proc_arg_count_table[message] > 0)
				{
					num += code_proc_arg_count_table[message];
				}
			}
			num++;
		}
		if (flag)
		{
			if (!messageBoardCtrl.instance.guide_ctrl.getEnables())
			{
				messageBoardCtrl.instance.guide_ctrl.changeGuide();
			}
		}
		else if (messageBoardCtrl.instance.guide_ctrl.getEnables())
		{
			messageBoardCtrl.instance.guide_set(false, guideCtrl.GuideType.NO_GUIDE);
		}
	}

	public static string EnToHalf(string in_text, Language in_language)
	{
		Dictionary<string, string> dictionary = new Dictionary<string, string>();
		dictionary.Add("１", "1");
		dictionary.Add("２", "2");
		dictionary.Add("３", "3");
		dictionary.Add("４", "4");
		dictionary.Add("５", "5");
		dictionary.Add("６", "6");
		dictionary.Add("７", "7");
		dictionary.Add("８", "8");
		dictionary.Add("９", "9");
		dictionary.Add("０", "0");
		dictionary.Add("Ａ", "A");
		dictionary.Add("Ｂ", "B");
		dictionary.Add("Ｃ", "C");
		dictionary.Add("Ｄ", "D");
		dictionary.Add("Ｅ", "E");
		dictionary.Add("Ｆ", "F");
		dictionary.Add("Ｇ", "G");
		dictionary.Add("Ｈ", "H");
		dictionary.Add("Ｉ", "I");
		dictionary.Add("Ｊ", "J");
		dictionary.Add("Ｋ", "K");
		dictionary.Add("Ｌ", "L");
		dictionary.Add("Ｍ", "M");
		dictionary.Add("Ｎ", "N");
		dictionary.Add("Ｏ", "O");
		dictionary.Add("Ｐ", "P");
		dictionary.Add("Ｑ", "Q");
		dictionary.Add("Ｒ", "R");
		dictionary.Add("Ｓ", "S");
		dictionary.Add("Ｔ", "T");
		dictionary.Add("Ｕ", "U");
		dictionary.Add("Ｖ", "V");
		dictionary.Add("Ｗ", "W");
		dictionary.Add("Ｘ", "X");
		dictionary.Add("Ｙ", "Y");
		dictionary.Add("Ｚ", "Z");
		dictionary.Add("ａ", "a");
		dictionary.Add("ｂ", "b");
		dictionary.Add("ｃ", "c");
		dictionary.Add("ｄ", "d");
		dictionary.Add("ｅ", "e");
		dictionary.Add("ｆ", "f");
		dictionary.Add("ｇ", "g");
		dictionary.Add("ｈ", "h");
		dictionary.Add("ｉ", "i");
		dictionary.Add("ｊ", "j");
		dictionary.Add("ｋ", "k");
		dictionary.Add("ｌ", "l");
		dictionary.Add("ｍ", "m");
		dictionary.Add("ｎ", "n");
		dictionary.Add("ｏ", "o");
		dictionary.Add("ｐ", "p");
		dictionary.Add("ｑ", "q");
		dictionary.Add("ｒ", "r");
		dictionary.Add("ｓ", "s");
		dictionary.Add("ｔ", "t");
		dictionary.Add("ｕ", "u");
		dictionary.Add("ｖ", "v");
		dictionary.Add("ｗ", "w");
		dictionary.Add("ｘ", "x");
		dictionary.Add("ｙ", "y");
		dictionary.Add("ｚ", "z");
		dictionary.Add("\u3000", " ");
		dictionary.Add("．", ".");
		dictionary.Add("，", ",");
		dictionary.Add("＇", "'");
		dictionary.Add("！", "!");
		dictionary.Add("（", "(");
		dictionary.Add("）", ")");
		dictionary.Add("－", "-");
		dictionary.Add("／", "/");
		dictionary.Add("？", "?");
		dictionary.Add("∠", "_");
		dictionary.Add("［", "[");
		dictionary.Add("］", "]");
		dictionary.Add("“", "\"");
		dictionary.Add("”", "\"");
		dictionary.Add("＂", "\"");
		dictionary.Add("―", "-");
		dictionary.Add("‘", "'");
		dictionary.Add("’", "'");
		dictionary.Add("：", ":");
		dictionary.Add("＊", "*");
		dictionary.Add("；", ";");
		dictionary.Add("＄", "$");
		dictionary.Add("Ы", "©");
		dictionary.Add("∋", "è");
		dictionary.Add("∈", "é");
		dictionary.Add("∀", "á");
		dictionary.Add("∧", "à");
		dictionary.Add("⊆", "ç");
		dictionary.Add("⊂", "Ç");
		dictionary.Add("Ц", "û");
		dictionary.Add("↑", "î");
		dictionary.Add("α", "â");
		dictionary.Add("л", "ñ");
		dictionary.Add("↓", "ï");
		dictionary.Add("ε", "ê");
		Dictionary<string, string> dictionary2 = dictionary;
		switch (in_language)
		{
		case Language.KOREA:
			dictionary2.Add("＜", "<");
			dictionary2.Add("＞", ">");
			dictionary2.Add("・", ".");
			dictionary2.Add("‥", "..");
			dictionary2.Add("…", "...");
			dictionary2.Add("～", "~");
			break;
		case Language.CHINA_S:
		case Language.CHINA_T:
			dictionary = new Dictionary<string, string>();
			dictionary.Add("φ", " ");
			dictionary.Add("‧", " ・ ");
			dictionary2 = dictionary;
			if (in_language == Language.CHINA_S)
			{
				dictionary2.Add("？", " ?");
				dictionary2.Add("！", " !");
			}
			if (in_language == Language.CHINA_T)
			{
				dictionary2.Add("／", " ／ ");
			}
			break;
		}
		string text = string.Empty;
		switch (in_language)
		{
		case Language.JAPAN:
			text = in_text;
			break;
		case Language.USA:
		case Language.FRANCE:
		case Language.GERMAN:
		case Language.KOREA:
		case Language.CHINA_S:
		case Language.CHINA_T:
		{
			for (int i = 0; i < in_text.Length; i++)
			{
				text = ((!dictionary2.ContainsKey(in_text[i].ToString())) ? (text + in_text[i]) : (text + dictionary2[in_text[i].ToString()]));
			}
			break;
		}
		}
		return text;
	}

	private static string InsertSpace(string in_text, string target)
	{
		string text = string.Empty;
		for (int i = 0; i < in_text.Length; i++)
		{
			if (in_text[i].ToString() == target)
			{
				if (i != 0 && (in_text[i - 1] < '０' || in_text[i - 1] > '９'))
				{
					text += " ";
				}
				text += target;
				if (i < in_text.Length - 1 && (in_text[i + 1] < '０' || in_text[i + 1] > '９'))
				{
					text += " ";
				}
			}
			else
			{
				text += in_text[i];
			}
		}
		return text;
	}

	public void AddScript(uint no)
	{
		AddScript2(no, 1u);
	}

	public void AddScript2(uint no, uint flag)
	{
		TrophyCtrl.check_trophy_by_mes_no();
		if (GSStatic.global_work_.title == TitleId.GS3)
		{
			GS3_mess.GS3_AddScript2((ushort)no, (ushort)flag);
			return;
		}
		MessageWork activeMessageWork = GetActiveMessageWork();
		switch (GSStatic.global_work_.scenario)
		{
		case 17:
		case 19:
		case 22:
		case 25:
		case 28:
		case 31:
			activeMessageWork.now_no_bak[0] = activeMessageWork.now_no;
			activeMessageWork.mdt_index_bak[0] = activeMessageWork.mdt_index;
			break;
		case 18:
		case 20:
		case 23:
		case 26:
		case 29:
		case 32:
			activeMessageWork.now_no_bak[1] = activeMessageWork.now_no;
			activeMessageWork.mdt_index_bak[1] = activeMessageWork.mdt_index;
			break;
		case 21:
		case 24:
		case 27:
		case 30:
		case 33:
			activeMessageWork.now_no_bak[2] = activeMessageWork.now_no;
			activeMessageWork.mdt_index_bak[2] = activeMessageWork.mdt_index;
			break;
		case 34:
			activeMessageWork.now_no_bak[3] = activeMessageWork.now_no;
			activeMessageWork.mdt_index_bak[3] = activeMessageWork.mdt_index;
			break;
		case byte.MaxValue:
			activeMessageWork.now_no_bak[4] = activeMessageWork.now_no;
			activeMessageWork.mdt_index_bak[4] = activeMessageWork.mdt_index;
			break;
		}
		if (no != 65535)
		{
			GSStatic.global_work_.scenario = (byte)no;
		}
		GSStatic.global_work_.event_flag = 0;
		switch (no)
		{
		case 17u:
		case 19u:
		case 22u:
		case 25u:
		case 28u:
		case 31u:
			if (activeMessageWork.now_no_bak[0] != ushort.MaxValue)
			{
				activeMessageWork.now_no = activeMessageWork.now_no_bak[0];
				activeMessageWork.mdt_index = activeMessageWork.mdt_index_bak[0];
			}
			break;
		case 18u:
		case 20u:
		case 23u:
		case 26u:
		case 29u:
		case 32u:
			if (activeMessageWork.now_no_bak[1] != ushort.MaxValue)
			{
				activeMessageWork.now_no = activeMessageWork.now_no_bak[1];
				activeMessageWork.mdt_index = activeMessageWork.mdt_index_bak[1];
			}
			break;
		case 21u:
		case 24u:
		case 27u:
		case 30u:
		case 33u:
			if (activeMessageWork.now_no_bak[2] != ushort.MaxValue)
			{
				activeMessageWork.now_no = activeMessageWork.now_no_bak[2];
				activeMessageWork.mdt_index = activeMessageWork.mdt_index_bak[2];
			}
			break;
		case 34u:
			if (activeMessageWork.now_no_bak[3] != ushort.MaxValue)
			{
				activeMessageWork.now_no = activeMessageWork.now_no_bak[3];
				activeMessageWork.mdt_index = activeMessageWork.mdt_index_bak[3];
			}
			break;
		case 65535u:
			if (activeMessageWork.now_no_bak[4] != ushort.MaxValue)
			{
				activeMessageWork.now_no = activeMessageWork.now_no_bak[4];
				activeMessageWork.mdt_index = activeMessageWork.mdt_index_bak[4];
			}
			break;
		}
		switch (no)
		{
		case 17u:
		case 18u:
		case 22u:
		case 23u:
		case 24u:
		case 28u:
		case 29u:
		case 30u:
		{
			string scenarioMdtPath3 = GSScenario.GetScenarioMdtPath((int)no);
			advCtrl.instance.message_system_.LoadScenarioMdtFromStreamingAssets(scenarioMdtPath3);
			break;
		}
		case 19u:
		case 20u:
		case 21u:
		case 25u:
		case 26u:
		case 27u:
		case 31u:
		case 32u:
		case 33u:
		case 34u:
		{
			string scenarioMdtPath2 = GSScenario.GetScenarioMdtPath((int)no);
			advCtrl.instance.message_system_.LoadScenarioMdtFromStreamingAssets(scenarioMdtPath2);
			if (flag == 1)
			{
				Set_scenario_enable();
				advCtrl.instance.message_system_.SetMessage(128u);
			}
			break;
		}
		case 65535u:
		{
			GSStatic.global_work_.event_flag = 1;
			string scenarioMdtPath = GSScenario.GetScenarioMdtPath((int)no);
			advCtrl.instance.message_system_.LoadScenarioMdtFromStreamingAssets(scenarioMdtPath);
			break;
		}
		}
	}

	public static void Set_scenario_enable()
	{
		CheckScenario(GSStatic.global_work_.scenario);
	}

	private static void CheckScenario(byte no)
	{
		switch (GSStatic.global_work_.title)
		{
		case TitleId.GS1:
			GS1_mess.GS1_CheckScenario(no);
			break;
		case TitleId.GS2:
			GS2_mess.GS2_CheckScenario(no);
			break;
		case TitleId.GS3:
			GS3_mess.GS3_CheckScenario(no);
			break;
		}
	}

	public static void ClearSceLocalFlg()
	{
		if (GSStatic.global_work_.title == TitleId.GS1 && (uint)GSStatic.global_work_.scenario >= 17u)
		{
			for (uint num = scenario.SCE4_FLAG_ST_LOCAL; num < 255; num++)
			{
				GSFlag.Set(0u, num, 0u);
			}
		}
	}

	public static TouchStatus GetTouchStatus()
	{
		if (TouchUtility.GetTouchR() == TouchInfo.Move && !TouchSystem.instance.HitSomeCollider())
		{
			return TouchStatus.Right;
		}
		if (TouchUtility.GetTouch() == TouchInfo.Move && messageBoardCtrl.instance.touch_skip_flag && !TouchSystem.instance.HitSomeCollider())
		{
			return TouchStatus.Left;
		}
		return TouchStatus.None;
	}
}
