using System;
using System.Runtime.InteropServices;

namespace SaveStruct
{
	[Serializable]
	public struct GlobalWork
	{
		public int language;

		public R r;

		public R r_bk;

		public byte Cursol;

		public byte system_language;

		public byte Mess_move_flag;

		public byte message_active_window;

		public byte event_flag;

		public ushort bk_start_mess;

		public ushort Bk_end_mess;

		public short bgm_vol_next;

		public ushort bgm_now;

		public byte sound_status;

		public short bgm_fade_time;

		public short bgm_vol;

		public ushort Random_seed;

		public byte get_note_id;

		public byte fade_time;

		public byte fade_speed;

		public ushort SpEf_status;

		public byte gauge_rno_0;

		public byte gauge_rno_1;

		public short gauge_hp;

		public short gauge_hp_disp;

		public short gauge_dmg_cnt;

		public short gauge_cnt_0;

		public short gauge_cnt_1;

		public short gauge_disp_flag;

		public short gauge_hp_scenario_end;

		public byte rest_old;

		public uint Room;

		public int title;

		public byte story;

		public byte scenario;

		public byte Scenario_enable;

		public sbyte rest;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
		public uint[] sce_flag;

		public uint status_flag;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
		public uint[] talk_end_flag;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
		public uint[] bg_first_flag;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
		public uint[] Map_data;

		public byte win_name_set;

		public byte timer;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
		public PsylockData[] psylock;

		public sbyte psy_no;

		public byte psy_menu_active_flag;

		public byte psy_unlock_not_unlock_message;

		public byte psy_unlock_success;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 60)]
		public byte[] sw_move_flag;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 25)]
		public byte[] roomseq;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
		public ushort[] lockdat;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
		public ushort[] talk_psy_data_;

		public ushort lock_max;

		public byte tanchiki_talk_selp;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 2048)]
		public byte[] inspect_readed_;

		public static void New(out GlobalWork global_work)
		{
			global_work = default(GlobalWork);
			global_work.r = default(R);
			global_work.r_bk = default(R);
			global_work.sce_flag = new uint[8];
			global_work.talk_end_flag = new uint[12];
			global_work.bg_first_flag = new uint[5];
			global_work.talk_psy_data_ = new ushort[20];
			global_work.Map_data = new uint[256];
			global_work.psylock = new PsylockData[4];
			for (int i = 0; i < global_work.psylock.Length; i++)
			{
				PsylockData.New(out global_work.psylock[i]);
			}
			global_work.sw_move_flag = new byte[60];
			global_work.roomseq = new byte[25];
			global_work.lockdat = new ushort[8];
			global_work.inspect_readed_ = new byte[2048];
		}

		public void CopyFrom(global::GlobalWork src)
		{
			if (src.r.no_0 == 11 || src.r.no_0 == 1)
			{
				GSScenario.sce_init sceInitProc = GSScenario.GetSceInitProc();
				sceInitProc(GSStatic.global_work_, true);
			}
			language = Language.languages.IndexOf(src.language);
			r.CopyFrom(ref src.r);
			r_bk.CopyFrom(ref src.r_bk);
			Mess_move_flag = src.Mess_move_flag;
			bk_start_mess = src.bk_start_mess;
			Bk_end_mess = src.Bk_end_mess;
			bgm_vol_next = src.bgm_vol_next;
			bgm_now = src.bgm_now;
			sound_status = src.sound_status;
			bgm_fade_time = src.bgm_fade_time;
			bgm_vol = src.bgm_vol;
			Random_seed = src.Random_seed;
			fade_time = src.fade_time;
			fade_speed = src.fade_speed;
			SpEf_status = src.SpEf_status;
			gauge_rno_0 = src.gauge_rno_0;
			gauge_rno_1 = src.gauge_rno_1;
			gauge_hp = src.gauge_hp;
			gauge_hp_disp = src.gauge_hp_disp;
			gauge_dmg_cnt = src.gauge_dmg_cnt;
			gauge_cnt_0 = src.gauge_cnt_0;
			gauge_cnt_1 = src.gauge_cnt_1;
			gauge_disp_flag = src.gauge_disp_flag;
			gauge_hp_scenario_end = src.gauge_hp_scenario_end;
			rest_old = src.rest_old;
			Room = src.Room;
			title = (int)src.title;
			story = src.story;
			scenario = src.scenario;
			Scenario_enable = src.Scenario_enable;
			rest = src.rest;
			Array.Copy(src.sce_flag, sce_flag, sce_flag.Length);
			status_flag = src.status_flag;
			Array.Copy(src.talk_end_flag, talk_end_flag, talk_end_flag.Length);
			Array.Copy(src.bg_first_flag, bg_first_flag, bg_first_flag.Length);
			for (int i = 0; i < 32; i++)
			{
				for (int j = 0; j < 8; j++)
				{
					Map_data[i * 8 + j] = src.Map_data[i][j];
				}
			}
			win_name_set = src.win_name_set;
			timer = src.timer;
			for (int k = 0; k < psylock.Length; k++)
			{
				psylock[k].CopyFrom(src.psylock[k]);
			}
			psy_no = src.psy_no;
			psy_menu_active_flag = src.psy_menu_active_flag;
			psy_unlock_not_unlock_message = src.psy_unlock_not_unlock_message;
			psy_unlock_success = src.psy_unlock_success;
			for (int l = 0; l < sw_move_flag.Length; l++)
			{
				sw_move_flag[l] = src.sw_move_flag[l];
			}
			Array.Copy(src.roomseq, roomseq, roomseq.Length);
			Array.Copy(src.lockdat, lockdat, lockdat.Length);
			Array.Copy(GSStatic.talk_psy_data_, talk_psy_data_, talk_psy_data_.Length);
			lock_max = src.lock_max;
			for (int m = 0; m < 2; m++)
			{
				for (int n = 0; n < 1024; n++)
				{
					inspect_readed_[m * 32 * 32 + n] = src.inspect_readed_[m, n];
				}
			}
			if (r.no_0 == 11 || r.no_0 == 1)
			{
				r.no_0 = GSScenario.GetScenarioPartData();
				r.no_1 = 0;
				r.no_2 = 0;
				r.no_3 = 0;
				r_bk.no_0 = 0;
				r_bk.no_1 = 0;
				r_bk.no_2 = 0;
				r_bk.no_3 = 0;
			}
		}

		public void CopyTo(global::GlobalWork dest)
		{
			dest.language = Language.languages[language];
			r.CopyTo(ref dest.r);
			r_bk.CopyTo(ref dest.r_bk);
			dest.Mess_move_flag = Mess_move_flag;
			dest.bk_start_mess = bk_start_mess;
			dest.Bk_end_mess = Bk_end_mess;
			dest.bgm_vol_next = bgm_vol_next;
			dest.bgm_now = bgm_now;
			dest.sound_status = sound_status;
			dest.bgm_fade_time = bgm_fade_time;
			dest.bgm_vol = bgm_vol;
			dest.Random_seed = Random_seed;
			dest.fade_time = fade_time;
			dest.fade_speed = fade_speed;
			dest.SpEf_status = SpEf_status;
			dest.gauge_rno_0 = gauge_rno_0;
			dest.gauge_rno_1 = gauge_rno_1;
			dest.gauge_hp = gauge_hp;
			dest.gauge_hp_disp = gauge_hp_disp;
			dest.gauge_dmg_cnt = gauge_dmg_cnt;
			dest.gauge_cnt_0 = gauge_cnt_0;
			dest.gauge_cnt_1 = gauge_cnt_1;
			dest.gauge_disp_flag = gauge_disp_flag;
			dest.gauge_hp_scenario_end = gauge_hp_scenario_end;
			dest.rest_old = rest_old;
			dest.Room = Room;
			dest.title = (TitleId)title;
			dest.story = story;
			dest.scenario = scenario;
			dest.Scenario_enable = Scenario_enable;
			dest.rest = rest;
			if (sce_flag == null)
			{
				sce_flag = new uint[8];
			}
			Array.Copy(sce_flag, dest.sce_flag, sce_flag.Length);
			dest.status_flag = status_flag;
			if (talk_end_flag == null)
			{
				talk_end_flag = new uint[12];
			}
			Array.Copy(talk_end_flag, dest.talk_end_flag, talk_end_flag.Length);
			if (bg_first_flag == null)
			{
				bg_first_flag = new uint[5];
			}
			Array.Copy(bg_first_flag, dest.bg_first_flag, bg_first_flag.Length);
			if (Map_data == null)
			{
				Map_data = new uint[256];
			}
			for (int i = 0; i < 32; i++)
			{
				for (int j = 0; j < 8; j++)
				{
					dest.Map_data[i][j] = Map_data[i * 8 + j];
				}
			}
			dest.win_name_set = win_name_set;
			dest.timer = timer;
			if (psylock == null)
			{
				psylock = new PsylockData[4];
			}
			for (int k = 0; k < psylock.Length; k++)
			{
				psylock[k].CopyTo(dest.psylock[k]);
			}
			dest.psy_no = psy_no;
			dest.psy_menu_active_flag = psy_menu_active_flag;
			dest.psy_unlock_not_unlock_message = psy_unlock_not_unlock_message;
			dest.psy_unlock_success = psy_unlock_success;
			if (sw_move_flag == null)
			{
				sw_move_flag = new byte[60];
			}
			for (int l = 0; l < 60; l++)
			{
				dest.sw_move_flag[l] = sw_move_flag[l];
			}
			if (roomseq == null)
			{
				roomseq = new byte[25];
			}
			Array.Copy(roomseq, dest.roomseq, roomseq.Length);
			if (lockdat == null)
			{
				lockdat = new ushort[8];
			}
			Array.Copy(lockdat, dest.lockdat, lockdat.Length);
			if (talk_psy_data_ == null)
			{
				talk_psy_data_ = new ushort[20];
			}
			Array.Copy(talk_psy_data_, GSStatic.talk_psy_data_, talk_psy_data_.Length);
			dest.lock_max = lock_max;
			if (inspect_readed_ == null)
			{
				inspect_readed_ = new byte[2048];
			}
			for (int m = 0; m < 2; m++)
			{
				for (int n = 0; n < 1024; n++)
				{
					dest.inspect_readed_[m, n] = inspect_readed_[m * 32 * 32 + n];
				}
			}
		}
	}
}
