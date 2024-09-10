using UnityEngine;

public static class SubWindow_Tanchiki
{
	public delegate void TanchikiProc(SubWindow sub_window);

	private static readonly TanchikiProc[] proc_table;

	static SubWindow_Tanchiki()
	{
		proc_table = new TanchikiProc[9] { Init, Appear, Main, Scroll, Leave, Decide, Back, MainDemo, AppearDemo };
	}

	public static void Proc(SubWindow sub_window)
	{
		Routine routine = sub_window.routine_[sub_window.stack_];
		proc_table[routine.r.no_1](sub_window);
	}

	private static void Init(SubWindow sub_window)
	{
		Routine routine = sub_window.routine_[sub_window.stack_];
		switch (routine.r.no_2)
		{
		case 0:
			if (sub_window.inspect_.tan_flag != 99)
			{
				sub_window.inspect_.tan_flag = 0;
				sub_window.inspect_.tan_posx = 896;
				sub_window.inspect_.tan_posy = 480;
			}
			sub_window.bar_req_ = SubWindow.BarReq.TANCHIKI_LR;
			TanchikiMiniGame.instance.init();
			routine.r.no_2++;
			break;
		case 1:
			sub_window.inspect_.step0 = 0;
			routine.r.no_1 = 1;
			routine.r.no_2 = 0;
			break;
		}
	}

	private static void Appear(SubWindow sub_window)
	{
		TanteiWork tantei_work_ = GSStatic.tantei_work_;
		Routine routine = sub_window.routine_[sub_window.stack_];
		switch (routine.r.no_2)
		{
		case 0:
			TanchikiMiniGame.instance.EnabledCursor(true);
			MiniGameCursor.instance.cursor_position = new Vector3(0f, 135f, 0f);
			TanchikiMiniGame.instance.UpdateWavePos();
			tantei_work_.yubi_timer = 14;
			routine.r.no_2++;
			if (tantei_work_.tanchiki_demof != 0)
			{
				MiniGameCursor.instance.cursor_position = new Vector3(0f, 146.25f, 0f);
				TanchikiMiniGame.instance.UpdateWavePos();
				routine.r.no_2 = 2;
			}
			if (!GSMain_TanteiPart.IsBGSlide(bgCtrl.instance.bg_no))
			{
				MiniGameCursor.instance.cursor_exception_limit = new Vector2(systemCtrl.instance.ScreenWidth, systemCtrl.instance.ScreenHeight);
			}
			break;
		case 1:
			if (tantei_work_.yubi_timer != 0)
			{
				Vector3 cursor_position = MiniGameCursor.instance.cursor_position;
				MiniGameCursor.instance.cursor_position = cursor_position + new Vector3(((float)(int)sub_window.inspect_.tan_posx - cursor_position.x) / (float)(int)tantei_work_.yubi_timer, ((float)(int)sub_window.inspect_.tan_posy - cursor_position.y) / (float)(int)tantei_work_.yubi_timer, 0f);
				TanchikiMiniGame.instance.UpdateWavePos();
				tantei_work_.yubi_timer--;
				break;
			}
			MiniGameCursor.instance.cursor_position = new Vector3((int)sub_window.inspect_.tan_posx, (int)sub_window.inspect_.tan_posy, 0f);
			TanchikiMiniGame.instance.UpdateWavePos();
			sub_window.busy_ = 0u;
			if (sub_window.req_ == SubWindow.Req.SELECT)
			{
				sub_window.req_ = SubWindow.Req.NONE;
			}
			routine.tp_cnt = 0;
			if (GSMain_TanteiPart.IsBGSlide(bgCtrl.instance.bg_no))
			{
				keyGuideCtrl.instance.open(keyGuideBase.Type.TANCHIKI_SLIDE);
			}
			else
			{
				keyGuideCtrl.instance.open(keyGuideBase.Type.TANCHIKI);
			}
			routine.r.no_1 = 2;
			routine.r.no_2 = 0;
			break;
		case 2:
			tantei_work_.tanchiki_demof = 2;
			sub_window.busy_ = 0u;
			if (sub_window.req_ == SubWindow.Req.SELECT)
			{
				sub_window.req_ = SubWindow.Req.NONE;
			}
			routine.tp_cnt = 0;
			routine.r.no_1 = 7;
			routine.r.no_2 = 0;
			break;
		}
	}

	private static void Main(SubWindow sub_window)
	{
		GlobalWork global_work_ = GSStatic.global_work_;
		Routine routine = sub_window.routine_[sub_window.stack_];
		sub_window.inspect_.ins_hit_check = 0;
		int num = TanchikiMiniGame.instance.hit_check();
		if (num != TanchikiMiniGame.find_target.Length - 1)
		{
			sub_window.inspect_.ins_hit_check = 1;
		}
		sub_window.inspect_.ins_hit_check = 0;
		TanchikiMiniGame.instance.UpdateCursor();
		num = TanchikiMiniGame.instance.hit_check();
		if (num != TanchikiMiniGame.find_target.Length - 1)
		{
			sub_window.inspect_.ins_hit_check = 1;
		}
		if (padCtrl.instance.GetKeyDown(KeyType.A))
		{
			keyGuideCtrl.instance.close();
			TanchikiMiniGame.instance.EnabledCursor(false);
			soundCtrl.instance.PlaySE(43);
			routine.tp_cnt = 0;
			sub_window.busy_ = 3u;
			if (num == 0)
			{
				routine.r.no_1 = 4;
				routine.r.no_2 = 0;
				sub_window.inspect_.tan_flag = 0;
			}
			else
			{
				routine.r.no_1 = 5;
				routine.r.no_2 = 0;
				sub_window.inspect_.tan_posx = (ushort)MiniGameCursor.instance.cursor_position.x;
				sub_window.inspect_.tan_posy = (ushort)MiniGameCursor.instance.cursor_position.y;
				sub_window.inspect_.tan_flag = 99;
			}
		}
		else if (padCtrl.instance.GetKeyDown(KeyType.L) && (bgData.instance.GetBGType(bgCtrl.instance.bg_no) == 1 || bgData.instance.GetBGType(bgCtrl.instance.bg_no) == 2))
		{
			TanchikiMiniGame.instance.EnabledWave(false);
			soundCtrl.instance.PlaySE(43);
			bgCtrl.instance.Slider();
			global_work_.r_bk.CopyFrom(ref global_work_.r);
			global_work_.r.no_1 = 3;
			global_work_.r.no_2 = 0;
			global_work_.r.no_3 = 0;
			sub_window.busy_ = 3u;
			routine.r.no_1 = 3;
			routine.r.no_2 = 0;
		}
	}

	private static void Scroll(SubWindow sub_window)
	{
		Routine routine = sub_window.routine_[sub_window.stack_];
		if (!bgCtrl.instance.is_slider)
		{
			TanchikiMiniGame.instance.EnabledWave(true);
			routine.r.no_1 = 2;
			sub_window.busy_ = 0u;
		}
	}

	private static void Leave(SubWindow sub_window)
	{
		GlobalWork global_work_ = GSStatic.global_work_;
		Routine routine = sub_window.routine_[sub_window.stack_];
		switch (routine.r.no_2)
		{
		case 0:
			TanchikiMiniGame.instance.end();
			routine.r.no_2++;
			break;
		case 1:
			routine.r.no_2++;
			break;
		case 2:
			routine.r.no_2++;
			break;
		case 3:
			fadeCtrl.instance.play(fadeCtrl.Status.FADE_IN, 1u, 1u);
			routine.r.no_2++;
			break;
		case 4:
			MessageSystem.Mess_window_set(5u);
			advCtrl.instance.message_system_.SetMessage(TanchikiMiniGame.find_message[0]);
			if (global_work_.title == TitleId.GS2)
			{
				global_work_.r.Set(5, 1, 0, 0);
				routine.r.Set(5, 0, 0, 0);
				sub_window.stack_++;
				sub_window.routine_[sub_window.stack_].r.Set(10, 0, 0, 0);
				sub_window.routine_[sub_window.stack_].tex_no = routine.tex_no;
			}
			else
			{
				global_work_.r.Set(5, 8, 3, 0);
				sub_window.routine_[sub_window.stack_].r.Set(10, 0, 0, 0);
				sub_window.routine_[sub_window.stack_].tex_no = routine.tex_no;
			}
			break;
		}
	}

	private static void Decide(SubWindow sub_window)
	{
		GlobalWork global_work_ = GSStatic.global_work_;
		Routine routine = sub_window.routine_[sub_window.stack_];
		switch (routine.r.no_2)
		{
		case 0:
			routine.r.no_2++;
			break;
		case 1:
			routine.r.no_2++;
			break;
		case 2:
			MessageSystem.Mess_window_set(5u);
			routine.r.no_2++;
			break;
		case 3:
			routine.r.no_2++;
			break;
		case 4:
		{
			int num = TanchikiMiniGame.instance.hit_check();
			if (num == 0)
			{
				num = TanchikiMiniGame.find_target.Length - 1;
			}
			advCtrl.instance.message_system_.SetMessage(TanchikiMiniGame.find_message[num]);
			global_work_.status_flag &= 4294967279u;
			routine.r.Set(22, 0, 0, 1);
			sub_window.stack_++;
			sub_window.routine_[sub_window.stack_].r.Set(10, 0, 0, 0);
			sub_window.routine_[sub_window.stack_].tex_no = routine.tex_no;
			break;
		}
		}
	}

	private static void Back(SubWindow sub_window)
	{
		Routine routine = sub_window.routine_[sub_window.stack_];
		byte no_ = routine.r.no_3;
		if (no_ != 0 && no_ != 1)
		{
			return;
		}
		switch (routine.r.no_2)
		{
		case 0:
			sub_window.bar_req_ = SubWindow.BarReq.TANCHIKI_LR;
			routine.r.no_2++;
			break;
		case 1:
			routine.r.no_2++;
			break;
		case 2:
			if (sub_window.bar_req_ == SubWindow.BarReq.NONE)
			{
				sub_window.busy_ = 0u;
				sub_window.req_ = SubWindow.Req.NONE;
				TanchikiMiniGame.instance.EnabledCursor(true);
				routine.r.no_1 = 2;
				routine.r.no_2 = 0;
			}
			break;
		}
	}

	private static void MainDemo(SubWindow sub_window)
	{
		TanteiWork tantei_work_ = GSStatic.tantei_work_;
		Routine routine = sub_window.routine_[sub_window.stack_];
		if (tantei_work_.tanchiki_demof != 3)
		{
			if (tantei_work_.tanchiki_demof == 0)
			{
				routine.r.no_1 = 8;
				routine.r.no_2 = 0;
			}
			sub_window.inspect_.ins_hit_check = 0;
			int num = TanchikiMiniGame.instance.hit_check();
			if (num != TanchikiMiniGame.find_target.Length - 1)
			{
				sub_window.inspect_.ins_hit_check = 1;
			}
			TanchikiMiniGame.instance.UpdateWave();
		}
	}

	private static void AppearDemo(SubWindow sub_window)
	{
		TanteiWork tantei_work_ = GSStatic.tantei_work_;
		Routine routine = sub_window.routine_[sub_window.stack_];
		switch (routine.r.no_2)
		{
		case 0:
			sub_window.inspect_.tan_posx = 1260;
			sub_window.inspect_.tan_posy = 260;
			MiniGameCursor.instance.cursor_position = new Vector3(0f, 135f, 0f);
			TanchikiMiniGame.instance.UpdateWavePos();
			tantei_work_.yubi_timer = 14;
			routine.r.no_2++;
			break;
		case 1:
			if (tantei_work_.yubi_timer != 0)
			{
				Vector3 cursor_position = MiniGameCursor.instance.cursor_position;
				MiniGameCursor.instance.cursor_position = cursor_position + new Vector3(((float)(int)sub_window.inspect_.tan_posx - cursor_position.x) / (float)(int)tantei_work_.yubi_timer, ((float)(int)sub_window.inspect_.tan_posy - cursor_position.y) / (float)(int)tantei_work_.yubi_timer, 0f);
				TanchikiMiniGame.instance.UpdateWavePos();
				tantei_work_.yubi_timer--;
				break;
			}
			MiniGameCursor.instance.cursor_position = new Vector3((int)sub_window.inspect_.tan_posx, (int)sub_window.inspect_.tan_posy, 0f);
			TanchikiMiniGame.instance.UpdateWavePos();
			sub_window.busy_ = 0u;
			if (sub_window.req_ == SubWindow.Req.SELECT)
			{
				sub_window.req_ = SubWindow.Req.NONE;
			}
			routine.tp_cnt = 0;
			routine.r.no_1 = 2;
			routine.r.no_2 = 0;
			break;
		}
	}

	private static uint tanchiki_BG(InspectBG inspect_bg)
	{
		return 0u;
	}
}
