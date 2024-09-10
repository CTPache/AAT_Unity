public static class SubWindow_Inspect
{
	public class RECT
	{
		public int x;

		public int y;

		public int w;

		public int h;

		public POINT point
		{
			get
			{
				return new POINT(x, y);
			}
			set
			{
				x = value.x;
				y = value.y;
			}
		}

		public RECT(int in_x, int in_y, int in_w, int in_h)
		{
			x = in_x;
			y = in_y;
			w = in_w;
			h = in_h;
		}
	}

	public class POINT
	{
		public int x;

		public int y;

		public POINT(int in_x, int in_y)
		{
			x = in_x;
			y = in_y;
		}
	}

	public class POINT4
	{
		public int x0;

		public int y0;

		public int x1;

		public int y1;

		public int x2;

		public int y2;

		public int x3;

		public int y3;

		public POINT4(int in_x0, int in_y0, int in_x1, int in_y1, int in_x2, int in_y2, int in_x3, int in_y3)
		{
			x0 = in_x0;
			y0 = in_y0;
			x1 = in_x1;
			y1 = in_y1;
			x2 = in_x2;
			y2 = in_y2;
			x3 = in_x3;
			y3 = in_y3;
		}

		public POINT point(uint in_no)
		{
			switch (in_no)
			{
			case 0u:
				return new POINT(x0, y0);
			case 1u:
				return new POINT(x1, y1);
			case 2u:
				return new POINT(x2, y2);
			default:
				return new POINT(x3, y3);
			}
		}
	}

	private delegate void InspectProc(SubWindow sub_window);

	private static readonly InspectProc[] proc_table;

	static SubWindow_Inspect()
	{
		proc_table = new InspectProc[7] { Init, Appear, Main, LrScroll, Leave, Decide, Back };
	}

	public static void Proc(SubWindow sub_window)
	{
		Routine currentRoutine = sub_window.GetCurrentRoutine();
		proc_table[currentRoutine.r.no_1](sub_window);
	}

	private static void Init(SubWindow sub_window)
	{
		Routine routine = sub_window.routine_[sub_window.stack_];
		switch (routine.r.no_2)
		{
		case 0:
			if (!AnimationSystem.Instance.CheckCharFade())
			{
				AnimationSystem.Instance.CharFade(4, 1);
			}
			routine.r.no_2++;
			break;
		case 1:
			if (!AnimationSystem.Instance.isFade(4))
			{
				routine.r.no_1 = 1;
				routine.r.no_2 = 0;
			}
			break;
		}
	}

	private static void Appear(SubWindow sub_window)
	{
		Routine routine = sub_window.routine_[sub_window.stack_];
		switch (routine.r.no_2)
		{
		case 0:
			routine.r.no_2++;
			break;
		case 1:
		{
			uint bg_no = (uint)bgCtrl.instance.bg_no;
			GSDemo.CheckBGChange(bg_no, 0u);
			inspectCtrl.instance.play();
			if (sub_window.req_ == SubWindow.Req.SELECT)
			{
				sub_window.req_ = SubWindow.Req.NONE;
			}
			sub_window.busy_ = 0u;
			routine.r.no_1 = 2;
			routine.r.no_2 = 0;
			break;
		}
		}
	}

	private static void Main(SubWindow sub_window)
	{
		GlobalWork global_work_ = GSStatic.global_work_;
		Routine routine = sub_window.routine_[sub_window.stack_];
		if (global_work_.r.no_0 == 8 && sub_window.req_ == SubWindow.Req.STATUS)
		{
			routine.tp_cnt = 0;
			sub_window.busy_ = 3u;
			routine.r.Set(6, 1, 0, 0);
			sub_window.stack_++;
			sub_window.routine_[sub_window.stack_].r.Set(11, 0, 0, 0);
			sub_window.routine_[sub_window.stack_].tex_no = routine.tex_no;
		}
		switch (routine.r.no_2)
		{
		case 0:
			if (inspectCtrl.instance.is_play)
			{
				break;
			}
			if (inspectCtrl.instance.is_cancel)
			{
				if (AnimationSystem.Instance.CheckCharFade())
				{
					AnimationSystem.Instance.CharFade(3, 1);
				}
				inspectCtrl.instance.reset();
				routine.r.no_2++;
			}
			else
			{
				sub_window.busy_ = 3u;
				routine.r.no_1 = 5;
				routine.r.no_2 = 0;
			}
			break;
		case 1:
			if (!AnimationSystem.Instance.isFade(4))
			{
				routine.r.no_2 = 0;
				debugLogger.instance.Log("SubWindowStack", "--stack_");
				sub_window.stack_--;
				GSStatic.global_work_.r.Set(5, scenario.RNO1_TANTEI_MAIN, 0, 0);
			}
			break;
		}
	}

	private static void LrScroll(SubWindow sub_window)
	{
	}

	private static void Leave(SubWindow sub_window)
	{
		Routine routine = sub_window.routine_[sub_window.stack_];
		switch (routine.r.no_2)
		{
		case 0:
			routine.r.no_2++;
			break;
		case 1:
			debugLogger.instance.Log("SubWindowStack", "--stack_");
			sub_window.stack_--;
			break;
		}
	}

	private static void Decide(SubWindow sub_window)
	{
		Routine currentRoutine = sub_window.GetCurrentRoutine();
		switch (currentRoutine.r.no_2)
		{
		case 0:
			currentRoutine.r.no_2++;
			break;
		case 1:
			currentRoutine.r.no_2++;
			break;
		case 2:
			currentRoutine.r.no_2++;
			break;
		case 3:
			currentRoutine.r.no_2++;
			break;
		case 4:
		{
			uint num = finger_pos_check();
			if (num == 65535 || num == scenario.SYS_M0320)
			{
				uint sYS_M = scenario.SYS_M0320;
				switch (GSStatic.global_work_.title)
				{
				case TitleId.GS1:
					sYS_M = scenario.SYS_M0320;
					break;
				case TitleId.GS2:
					sYS_M = scenario_GS2.SYS_M0320;
					break;
				case TitleId.GS3:
					sYS_M = scenario_GS3.SYS_M0320;
					break;
				}
				advCtrl.instance.message_system_.SetMessage(sYS_M);
			}
			else
			{
				MessageSystem.setInspectTalkEndFlg(inspectCtrl.GetNextInspectNumber(num), 0);
				inspectCtrl.InspectCashReset();
				advCtrl.instance.message_system_.SetMessage(num);
			}
			MessageSystem.Mess_window_set(5u);
			messageBoardCtrl.instance.guide_ctrl.next_guid = guideCtrl.GuideType.HOUTEI;
			currentRoutine.r.Set(6, 0, 0, 1);
			debugLogger.instance.Log("SubWindowStack", "++stack_");
			sub_window.stack_++;
			Routine currentRoutine2 = sub_window.GetCurrentRoutine();
			currentRoutine2.r.Set(10, 0, 0, 0);
			currentRoutine2.tex_no = currentRoutine.tex_no;
			break;
		}
		}
	}

	private static void Back(SubWindow sub_window)
	{
		Routine currentRoutine = sub_window.GetCurrentRoutine();
		byte no_ = currentRoutine.r.no_3;
		if (no_ != 0 && no_ == 1)
		{
			switch (currentRoutine.r.no_2)
			{
			case 0:
				currentRoutine.r.no_2++;
				break;
			case 1:
				currentRoutine.r.no_2++;
				break;
			case 2:
				sub_window.busy_ = 0u;
				sub_window.req_ = SubWindow.Req.NONE;
				currentRoutine.r.no_1 = 2;
				currentRoutine.r.no_2 = 0;
				break;
			}
		}
	}

	public static uint finger_pos_check_interrupt()
	{
		uint result = 0u;
		switch (GSStatic.global_work_.title)
		{
		case TitleId.GS1:
			result = GS1_finger_pos_check_00();
			break;
		case TitleId.GS2:
			result = GS2_finger_pos_check_00();
			break;
		case TitleId.GS3:
			result = GS3_finger_pos_check_00();
			break;
		}
		return result;
	}

	public static uint finger_pos_check(bool inspect_check = false)
	{
		uint num = 0u;
		if (!inspect_check)
		{
			num = finger_pos_check_interrupt();
		}
		if (num != 0)
		{
			return num;
		}
		int num2 = (int)(inspectCtrl.instance.pos_x + bgCtrl.instance.bg_pos_x + 960f);
		int num3 = (int)(540f - inspectCtrl.instance.pos_y);
		RECT rect = ((GSStatic.global_work_.title != 0 || (uint)GSStatic.global_work_.scenario < 17u) ? new RECT(num2, num3, 4, 16) : new RECT(num2 - 3, num3 - 3, 6, 6));
		for (int i = 0; i < GSStatic.inspect_data_.Length; i++)
		{
			INSPECT_DATA iNSPECT_DATA = GSStatic.inspect_data_[i];
			if (iNSPECT_DATA.place == uint.MaxValue)
			{
				break;
			}
			if (iNSPECT_DATA.place != 253)
			{
				continue;
			}
			POINT4 point = new POINT4((int)iNSPECT_DATA.x0, (int)iNSPECT_DATA.y0, (int)iNSPECT_DATA.x1, (int)iNSPECT_DATA.y1, (int)iNSPECT_DATA.x2, (int)iNSPECT_DATA.y2, (int)iNSPECT_DATA.x3, (int)iNSPECT_DATA.y3);
			if (!ObjHitCheck2(rect, point))
			{
				continue;
			}
			switch (GSStatic.global_work_.title)
			{
			case TitleId.GS1:
				switch (iNSPECT_DATA.item)
				{
				case 13u:
					if (GSFlag.Check(0u, scenario.SCE1_SITAI))
					{
						return iNSPECT_DATA.message;
					}
					break;
				case 14u:
					if (!GSFlag.Check(0u, scenario.SCE2_BOY_DEPOSITION_1))
					{
						return iNSPECT_DATA.message;
					}
					break;
				case 17u:
					if (!GSFlag.Check(0u, scenario.SCE30_GET_CRACKER))
					{
						return iNSPECT_DATA.message;
					}
					break;
				case 16u:
					return iNSPECT_DATA.message;
				case 18u:
					if ((uint)GSStatic.global_work_.scenario >= 17u && (uint)GSStatic.global_work_.scenario <= 18u && !GSFlag.Check(0u, scenario.SCE4_GET_TADASIKI_ID))
					{
						return iNSPECT_DATA.message;
					}
					break;
				case 19u:
					if ((uint)GSStatic.global_work_.scenario >= 17u && (uint)GSStatic.global_work_.scenario <= 18u && !GSFlag.Check(0u, scenario.SCE4_GET_TOMOE_PHONE))
					{
						return iNSPECT_DATA.message;
					}
					break;
				case 186u:
					if (GSFlag.Check(0u, scenario.SCE44_FLAG_DISP_KENJI_JIHYO))
					{
						return iNSPECT_DATA.message;
					}
					break;
				}
				break;
			case TitleId.GS2:
				switch (iNSPECT_DATA.item)
				{
				case 15u:
					if (GSFlag.Check(0u, scenario_GS2.SCE2_MARI) && GSFlag.Check(0u, scenario_GS2.SCE2_BLKEY) && GSFlag.Check(0u, scenario_GS2.SCE2_SYOUZOKU) && GSFlag.Check(0u, scenario_GS2.SCE2_KURAIN_2ND) && !GSFlag.Check(0u, scenario_GS2.SCE2_MARI2))
					{
						return iNSPECT_DATA.message;
					}
					break;
				case 28u:
					if (GSFlag.Check(0u, scenario_GS2.SCE401_SHIKISHIOBJ_FLG))
					{
						return iNSPECT_DATA.message;
					}
					break;
				case 27u:
					if (GSFlag.Check(0u, scenario_GS2.SCE401_SAZAE))
					{
						return iNSPECT_DATA.message;
					}
					break;
				case 32u:
					if (GSFlag.Check(0u, scenario_GS2.SCE421_TANAKA) && !GSFlag.Check(0u, scenario_GS2.SCE421_ACCE))
					{
						return iNSPECT_DATA.message;
					}
					break;
				case 30u:
					if (GSStatic.global_work_.scenario == 19 && !GSFlag.Check(0u, scenario_GS2.SCE421_PHOTO))
					{
						return iNSPECT_DATA.message;
					}
					break;
				}
				break;
			case TitleId.GS3:
				if (iNSPECT_DATA.item == 188 && GSStatic.global_work_.Room == 8 && GSStatic.global_work_.scenario == 7 && !GSFlag.Check(0u, scenario_GS3.SCE2_0_PASS_SNEWS_MAKO) && !GSFlag.Check(0u, scenario_GS3.SCE2_0_GET_SNEWS))
				{
					return iNSPECT_DATA.message;
				}
				break;
			}
		}
		for (int j = 0; j < GSStatic.inspect_data_.Length; j++)
		{
			INSPECT_DATA iNSPECT_DATA2 = GSStatic.inspect_data_[j];
			if (iNSPECT_DATA2.place == uint.MaxValue)
			{
				break;
			}
			if (iNSPECT_DATA2.place != 254 && iNSPECT_DATA2.place != 253)
			{
				POINT4 point2 = new POINT4((int)iNSPECT_DATA2.x0, (int)iNSPECT_DATA2.y0, (int)iNSPECT_DATA2.x1, (int)iNSPECT_DATA2.y1, (int)iNSPECT_DATA2.x2, (int)iNSPECT_DATA2.y2, (int)iNSPECT_DATA2.x3, (int)iNSPECT_DATA2.y3);
				if (ObjHitCheck2(rect, point2))
				{
					return iNSPECT_DATA2.message;
				}
			}
		}
		switch (GSStatic.global_work_.title)
		{
		case TitleId.GS1:
			return scenario.SYS_M0320;
		case TitleId.GS2:
			return GS2_finger_pos_check_02();
		case TitleId.GS3:
			return GS3_finger_pos_check_02();
		default:
			return 65535u;
		}
	}

	private static uint GS1_finger_pos_check_00()
	{
		if (GSStatic.global_work_.scenario < 17 && !GSFlag.Check(0u, scenario.SCE1_CHIHIRO_DISCOVER))
		{
			return scenario.SC1_00285;
		}
		return 0u;
	}

	private static uint GS2_finger_pos_check_00()
	{
		if (GSStatic.global_work_.scenario == 2)
		{
			if (GSStatic.global_work_.Room == 5 && GSFlag.Check(0u, scenario_GS2.SCE1_0_WATARI_NATSUMI) && !GSFlag.Check(0u, scenario_GS2.SCE1_0_RYUUCHIJO_MAYOI))
			{
				return scenario_GS2.SC1_02130;
			}
		}
		else if (GSStatic.global_work_.scenario == 14)
		{
			if (GSStatic.global_work_.Room == 0)
			{
				return 199u;
			}
		}
		else if (GSStatic.global_work_.scenario == 15)
		{
			if (GSStatic.global_work_.Room == 0 && GSFlag.Check(0u, scenario_GS2.SCE401_KIRIO_OUT))
			{
				return scenario_GS2.SC3_19080;
			}
		}
		else if (GSStatic.global_work_.scenario == 18 && GSStatic.global_work_.Room == 20 && !GSFlag.Check(0u, scenario_GS2.SCE420_MITSURUGI))
		{
			return 165u;
		}
		return 0u;
	}

	public static uint GS2_finger_pos_check_02()
	{
		if (GSStatic.global_work_.scenario == 15 && GSStatic.global_work_.Room == 23)
		{
			return scenario_GS2.SC3_19125;
		}
		if ((GSStatic.global_work_.scenario == 18 && GSStatic.global_work_.Room == 25) || (GSStatic.global_work_.scenario == 18 && GSStatic.global_work_.Room == 21))
		{
			return 268u;
		}
		return scenario_GS2.SYS_M0320;
	}

	private static uint GS3_finger_pos_check_00()
	{
		if (GSStatic.global_work_.scenario == 2)
		{
			if (GSStatic.global_work_.Room == 3 && !GSFlag.Check(0u, scenario_GS3.SCE1_0_AIGA_PUSH) && GSFlag.Check(0u, scenario_GS3.SCE1_0_KIRIO_DINNER))
			{
				return 298u;
			}
		}
		else if (GSStatic.global_work_.scenario == 4 && GSStatic.global_work_.Room == 2)
		{
			return 225u;
		}
		return 0u;
	}

	private static uint GS3_finger_pos_check_02()
	{
		if (GSStatic.global_work_.scenario == 15)
		{
			return scenario_GS3.SYS_M0330;
		}
		return scenario_GS3.SYS_M0320;
	}

	private static bool ObjHitCheck2(RECT rect, POINT4 point)
	{
		if (Hit_ck_point4(rect.point, point))
		{
			return true;
		}
		POINT4 pOINT = new POINT4(rect.x, rect.y, rect.x + rect.w, rect.y, rect.x + rect.w, rect.y + rect.h, rect.x, rect.y + rect.h);
		for (uint num = 0u; num < 3; num++)
		{
			for (uint num2 = 0u; num2 < 3; num2++)
			{
				if (CkTwoLine(point.point(num2), point.point(num2 + 1), pOINT.point(num), pOINT.point(num + 1)))
				{
					return true;
				}
			}
			if (CkTwoLine(point.point(3u), point.point(0u), pOINT.point(num), pOINT.point(num + 1)))
			{
				return true;
			}
		}
		for (uint num3 = 0u; num3 < 3; num3++)
		{
			if (CkTwoLine(point.point(num3), point.point(num3 + 1), pOINT.point(3u), pOINT.point(0u)))
			{
				return true;
			}
		}
		if (CkTwoLine(point.point(3u), point.point(0u), pOINT.point(3u), pOINT.point(0u)))
		{
			return true;
		}
		return false;
	}

	private static bool CkTwoLine(POINT pt0, POINT pt1, POINT pt2, POINT pt3)
	{
		int num = pt0.x - pt1.x;
		int num2 = pt0.y - pt1.y;
		int num3 = pt2.x - pt3.x;
		int num4 = pt2.y - pt3.y;
		int num5 = num * num4 - num2 * num3;
		if (num5 != 0)
		{
			return false;
		}
		int num6 = pt1.x - pt3.x;
		int num7 = pt1.y - pt3.y;
		int num8 = num7 * num3 - num6 * num4;
		int num9 = num7 * num - num6 * num2;
		if (((num5 > 0 && num8 >= 0 && num8 <= num5) || (num5 < 0 && num8 >= num5 && num8 <= 0)) && ((num5 > 0 && num9 >= 0 && num9 <= num5) || (num5 < 0 && num9 >= num5 && num9 <= 0)))
		{
			return true;
		}
		return false;
	}

	private static bool Hit_ck_point4(POINT p, POINT4 cp)
	{
		int x = cp.x0;
		int y = cp.y0;
		int num = p.x - x;
		int num2 = p.y - y;
		int num3 = cp.x1 - x;
		int num4 = cp.y1 - y;
		int num5 = cp.x3 - x;
		int num6 = cp.y3 - y;
		if (num3 * num2 < num4 * num || num5 * num2 > num6 * num)
		{
			return false;
		}
		num -= cp.x2 - x;
		num2 -= cp.y2 - y;
		num3 -= cp.x2 - x;
		num4 -= cp.y2 - y;
		num5 -= cp.x2 - x;
		num6 -= cp.y2 - y;
		if (num3 * num2 > num4 * num || num5 * num2 < num6 * num)
		{
			return false;
		}
		return true;
	}
}
