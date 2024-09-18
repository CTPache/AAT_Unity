public static class GSMain_Testimony
{
	private delegate void TestimonyProc(GlobalWork global_work);

	private static TestimonyProc[] proc_table;

	static GSMain_Testimony()
	{
		proc_table = new TestimonyProc[4] { ObjSet, Init, Main, Exit };
	}

	public static void Proc(GlobalWork global_work)
	{
		proc_table[global_work.r.no_1](global_work);
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
			num = 20;
			num2 = 22;
			num3 = 24;
			break;
		case TitleId.GS2:
			num = 35;
			num2 = 37;
			num3 = 39;
			break;
		case TitleId.GS3:
			num = 65;
			num2 = 67;
			num3 = 69;
			break;
		}
		switch (global_work.r.no_2)
		{
		case 0:
			soundCtrl.instance.PlaySE(83);
			instance.PlayObject((int)global_work.title, 0, num);
			global_work.r.no_2++;
			break;
		case 1:
			if (!instance.IsPlayingObject((int)global_work.title, 0, num))
			{
				fadeCtrl.instance.play(fadeCtrl.Status.WFADE_IN, 1u, 8u);
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
			}
			break;
		}
	}

	private static void Init(GlobalWork global_work)
	{
		GSStatic.saiban_work_.wait_timer = 0;
		global_work.r.no_1 = 2;
	}

	private static void Main(GlobalWork global_work)
	{
		if (!TestimonyRoot.instance.TestimonyIconEnabled)
		{
			TestimonyRoot.instance.TestimonyIconEnabled = true;
		}
		if (fadeCtrl.instance.status == fadeCtrl.Status.NO_FADE && !advCtrl.instance.sub_window_.IsBusy())
		{
			if (selectPlateCtrl.instance.select_animation_playing)
			{
				return;
			}
			if (padCtrl.instance.GetKeyDown(KeyType.Start) && (global_work.status_flag & 0x10) == 0 && (GSStatic.message_work_.status & (MessageSystem.Status.RT_WAIT | MessageSystem.Status.SELECT)) != 0 && GSMain_SaibanPart.CheckSaveKey())
			{
				global_work.r_bk.CopyFrom(ref global_work.r);
				soundCtrl.instance.PlaySE(49);
				global_work.r.Set(17, 0, 0, 0);
			}
			else if (padCtrl.instance.GetKeyDown(KeyType.R) && (GSStatic.message_work_.status & (MessageSystem.Status.RT_WAIT | MessageSystem.Status.SELECT)) != 0 && GSMain_SaibanPart.CheckNoteKey())
			{
				global_work.r_bk.CopyFrom(ref global_work.r);
				global_work.r.Set(8, 0, 0, 0);
				Disp(1);
				return;
			}
		}
		Disp(0);
	}

	private static void Exit(GlobalWork global_work)
	{
		TestimonyRoot.instance.TestimonyIconEnabled = false;
		global_work.r.Set(4, 1, 0, 0);
	}

	private static void Disp(byte flag)
	{
		switch (flag)
		{
		case 0:
			GSStatic.saiban_work_.wait_timer++;
			if (GSStatic.saiban_work_.wait_timer > 100)
			{
				GSStatic.saiban_work_.wait_timer = 0;
			}
			if (GSStatic.saiban_work_.wait_timer > 80)
			{
			}
			break;
		case 1:
			GSStatic.saiban_work_.wait_timer = 0;
			break;
		case 2:
			GSStatic.saiban_work_.wait_timer = 0;
			break;
		}
	}
}
