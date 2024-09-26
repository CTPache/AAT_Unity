using System;
using System.Linq;
using UnityEngine;

[Serializable]
public class GlobalWork
{
	public enum Rno0
	{
		LOGO = 0,
		NEWTITLE = 1,
		TITLE = 2,
		GAME_OVER = 3,
		SAIBAN = 4,
		TANTEI = 5,
		TESTIMONY = 6,
		QUESTIONING = 7,
		STATUS = 8,
		NOTE_ADD_DISP = 9,
		DELIVER_JUDGMENT = 10,
		SAVE = 11,
		EPISODE_CLEAR = 12,
		EPISODE_SELECT = 13,
		EPISODE_CONTINUE = 14,
		FORMAT = 15,
		DEBUG_MENU = 16,
		OPTION = 17
	}

	public enum Rno1
	{
		INIT = 0,
		MAIN = 1,
		EXIT = 2
	}

	public enum StatusFlag
	{
		QUAKE_ON = 1,
		NOTE_OFF = 0x10,
		SITEKI = 0x100,
		SITEKI2 = 0x200,
		REST_DISP_ON = 0x400,
		SAVE_OFF = 0x800
	}

	private uint System_timer;

	public string language;

    public string languageFallback;

    private byte disp_select_flag;

	private byte old_Language_flag;

	public R r;

	public R r_bk;

	private byte V_blank_flag;

	private byte V_blank_ctr;

	private sbyte Quake_x;

	private sbyte Quake_y;

	private uint obj_foa_ptr;

	private uint obj_foa_ptr2;

	private uint obj_foa_ptr3;

	private ushort Quake_timer;

	private byte Quake_power;

	public byte Cursol;

	public SystemLanguage system_language;

	[SerializeField]
	private byte Mess_move_flag_;

	public byte message_active_window;

	private byte Bg3_freeze_flag;

	private byte Freez_bg_no;

	private short Freez_dir;

	public byte event_flag;

	private byte speech_reset_cancel;

	public ushort bk_start_mess;

	public ushort Bk_end_mess;

	public short bgm_vol_next;

	public ushort bgm_now;

	private byte first_ep_clear;

	public byte sound_status;

	private byte Obj_plt_use_flag;

	private byte Obj_flag;

	public short bgm_fade_time;

	public short bgm_vol;

	public ushort Random_seed;

	private byte get_note_file;

	public byte get_note_id;

	private ushort save_title_pos_x;

	private short Title_zoom;

	private ushort Fade_object;

	private ushort Fade_status;

	private ushort Fade_timer;

	public byte fade_time;

	public byte fade_speed;

	private byte Fade_sw;

	private byte Fade_flash;

	private ushort Fade_object2;

	private ushort Fade_status2;

	private ushort Fade_timer2;

	private byte Fade_time2;

	private byte Fade_speed2;

	private byte Fade_sw2;

	private byte Fade_flash2;

	public ushort SpEf_status;

	private ushort SpEf_sw;

	private ushort SpEf_timer;

	private byte SpEf_time;

	private byte SpEf_speed;

	private byte Dicon_rno_0;

	private byte Dicon_id;

	private short Dicon_pos_x;

	private ushort Dicon_tiemr;

	private ushort Dicon_dm00;

	public byte gauge_rno_0;

	public byte gauge_rno_1;

	public short gauge_hp;

	public short gauge_hp_disp;

	public short gauge_dmg_cnt;

	private short gauge_pos_x;

	private short gauge_pos_y;

	public short gauge_cnt_0;

	public short gauge_cnt_1;

	public short gauge_disp_flag;

	public short gauge_hp_scenario_end;

	private int gauge_hp_fixed;

	private int gauge_hp_fixed_diff;

	private byte Rest_type;

	private byte Rest_timer;

	public byte rest_old;

	private byte Rest_dm;

	public uint Room_;

	public TitleId title;

	public byte scenario;

	public byte story;

	public byte Scenario_enable;

	public sbyte rest;

	public ushort Def_talk_foa;

	public ushort Def_wait_foa;

	public uint[] sce_flag = new uint[8];

	public uint status_flag;

	public uint[] talk_end_flag = new uint[12];

	public uint[] bg_first_flag = new uint[5];

	public uint[][] Map_data = new uint[32][]
	{
		new uint[8],
		new uint[8],
		new uint[8],
		new uint[8],
		new uint[8],
		new uint[8],
		new uint[8],
		new uint[8],
		new uint[8],
		new uint[8],
		new uint[8],
		new uint[8],
		new uint[8],
		new uint[8],
		new uint[8],
		new uint[8],
		new uint[8],
		new uint[8],
		new uint[8],
		new uint[8],
		new uint[8],
		new uint[8],
		new uint[8],
		new uint[8],
		new uint[8],
		new uint[8],
		new uint[8],
		new uint[8],
		new uint[8],
		new uint[8],
		new uint[8],
		new uint[8]
	};

	private byte spotlight_command_status;

	private byte win_name_no;

	public byte win_name_set;

	private byte qta_pos_flag;

	private sbyte Mic_wait_timer;

	private uint Comeback_flag;

	private byte tbl_dsp_flg;

	private byte step0;

	private ushort cyn_debug01;

	private sbyte save_wait;

	public byte timer;

	public const int PSYLOCK_DATA_SIZE = 4;

	public PsylockData[] psylock = new PsylockData[4];

	public sbyte psy_no;

	private byte psy_bgm_backup;

	private ushort psy_menu_pos_diff;

	private byte psy_menu_rno_0;

	private byte psy_menu_rno_1;

	public byte psy_menu_active_flag;

	public byte psy_unlock_not_unlock_message;

	public byte psy_unlock_success;

	private ushort psy_acro_bird_id;

	public byte[] sw_move_flag = new byte[60];

	public byte[] roomseq = new byte[25];

	public ushort[] lockdat = new ushort[8];

	public ushort lock_max;

	private ushort se_no;

	private ushort recover_se_play_from_save_to_game;

	private byte chinami_sagesumi_f;

	private byte tantei_psy_point_m;

	public byte tanchiki_talk_selp;

	private byte demo_noise_disp_f;

	private uint saiban_expl_hoji_f;

	private uint debug_flg4;

	private byte debug_flg4_0;

	private byte debug_flg4_1;

	private byte debug_flg4_2;

	private byte debug_flg4_3;

	private uint op_priority_disable;

	private uint flash_flag;

	public byte[,] inspect_readed_ = new byte[2, 1024];

	private AnimationObject anime_obj;

	public byte Mess_move_flag
	{
		get
		{
			return Mess_move_flag_;
		}
		set
		{
			Mess_move_flag_ = value;
		}
	}

	public uint Room
	{
		get
		{
			return Room_;
		}
		set
		{
			Room_ = value;
		}
	}

	public GlobalWork()
	{
		GSStructUtility.FillArrayNewInstance(psylock);
	}

	public void lifegauge_init_hp()
	{
		gauge_hp = (gauge_hp_disp = (gauge_hp_scenario_end = 80));
	}

	public void reset()
	{
		r.init();
		r_bk.init();
		Cursol = 0;
		Mess_move_flag = 0;
		message_active_window = 0;
		event_flag = 0;
		bk_start_mess = 0;
		Bk_end_mess = 0;
		get_note_id = 0;
		fade_time = 0;
		fade_speed = 0;
		SpEf_status = 0;
		rest = 0;
		Def_talk_foa = 0;
		Def_wait_foa = 0;
		status_flag = 0u;
		win_name_set = 0;
		timer = 0;
		for (int i = 0; i < psylock.Length; i++)
		{
			psylock[i].init();
		}
		for (int j = 0; j < roomseq.Length; j++)
		{
			roomseq[j] = 0;
		}
		Array.Clear(lockdat, 0, lockdat.Length);
		lock_max = 0;
	}

	public void init()
    {
        language = "USA";
        languageFallback = Language.langFallback[language];
        r.init();
		r_bk.init();
		Cursol = 0;
		system_language = SystemLanguage.Afrikaans;
		Mess_move_flag = 0;
		message_active_window = 0;
		event_flag = 0;
		bk_start_mess = 0;
		Bk_end_mess = 0;
		bgm_vol_next = 0;
		bgm_now = 0;
		sound_status = 0;
		bgm_fade_time = 0;
		bgm_vol = 256;
		Random_seed = 0;
		get_note_id = 0;
		fade_time = 0;
		fade_speed = 0;
		SpEf_status = 0;
		gauge_rno_0 = 0;
		gauge_rno_1 = 0;
		gauge_hp = 0;
		gauge_hp_disp = 0;
		gauge_dmg_cnt = 0;
		gauge_cnt_0 = 0;
		gauge_cnt_1 = 0;
		gauge_disp_flag = 0;
		gauge_hp_scenario_end = 0;
		rest_old = 0;
		Room = 0u;
		title = TitleId.GS1;
		scenario = 0;
		story = 0;
		Scenario_enable = 0;
		rest = 0;
		Def_talk_foa = 0;
		Def_wait_foa = 0;
		status_flag = 0u;
		win_name_set = 0;
		timer = 0;
		psy_no = 0;
		psy_menu_active_flag = 0;
		psy_unlock_not_unlock_message = 0;
		psy_unlock_success = 0;
		lock_max = 0;
		tanchiki_talk_selp = 0;
		for (int i = 0; i < psylock.Length; i++)
		{
			psylock[i].init();
		}
		for (int j = 0; j < sce_flag.Length; j++)
		{
			sce_flag[j] = 0u;
		}
		for (int k = 0; k < talk_end_flag.Length; k++)
		{
			talk_end_flag[k] = 0u;
		}
		for (int l = 0; l < bg_first_flag.Length; l++)
		{
			bg_first_flag[l] = 0u;
		}
		for (int m = 0; m < Map_data.Length; m++)
		{
			for (int n = 0; n < Map_data[m].Length; n++)
			{
				Map_data[m][n] = 0u;
			}
		}
		for (int num = 0; num < sw_move_flag.Length; num++)
		{
			sw_move_flag[num] = 0;
		}
		for (int num2 = 0; num2 < roomseq.Length; num2++)
		{
			roomseq[num2] = 0;
		}
		for (int num3 = 0; num3 < lockdat.Length; num3++)
		{
			lockdat[num3] = 0;
		}
		for (int num4 = 0; num4 < inspect_readed_.GetLength(0); num4++)
		{
			for (int num5 = 0; num5 < inspect_readed_.GetLength(1); num5++)
			{
				inspect_readed_[num4, num5] = 0;
			}
		}
	}
}
