public static class SubWindow_Select
{
	private delegate void SelectProc(SubWindow sub_window);

	private const int SELECT_MOVE_SPEED = 10;

	private static readonly SelectProc[] proc_table;

	private static readonly GSRect[] tp_table;

	static SubWindow_Select()
	{
		proc_table = new SelectProc[7] { Init, Appear, Main, Decide, ToStatus, FromStatus, FromSave };
		tp_table = new GSRect[6]
		{
			new GSRect(0, 57, 255, 32),
			new GSRect(0, 119, 255, 32),
			new GSRect(0, 0, 0, 0),
			new GSRect(0, 44, 255, 32),
			new GSRect(0, 88, 255, 32),
			new GSRect(0, 132, 255, 32)
		};
	}

	public static void Proc(SubWindow sub_window)
	{
		Routine currentRoutine = sub_window.GetCurrentRoutine();
		proc_table[currentRoutine.r.no_1](sub_window);
		sub_window.cursor_.Proc();
	}

	private static void Init(SubWindow sub_window)
	{
		Routine currentRoutine = sub_window.GetCurrentRoutine();
		Routine3d[] routine_3d = currentRoutine.routine_3d;
		MessageWork message_work_ = GSStatic.message_work_;
		switch (currentRoutine.r.no_2)
		{
		case 0:
		{
			ushort[] selectTextIds = SelectTable.GetSelectTextIds(GSStatic.global_work_.scenario, message_work_.now_no);
			selectPlateCtrl instance = selectPlateCtrl.instance;
			if (selectTextIds != null)
			{
				instance.entryCursor(selectTextIds.Length, selectPlateCtrl.FromEntryRequest.SELECT);
				for (int i = 0; i < selectTextIds.Length; i++)
				{
					instance.setText(i, advCtrl.instance.cho_data_.GetText(selectTextIds[i]));
				}
			}
			instance.playCursor(0);
			for (int j = 0; j < 6; j++)
			{
				routine_3d[j].Clear();
			}
			routine_3d[0].y = (routine_3d[1].y = (routine_3d[2].y = -100));
			for (ushort num = 0; num < 3; num++)
			{
			}
			sub_window.cursor_.Rno_0 = 0;
			message_work_.cursor = 0;
			sub_window.cursor_.Set((ushort)(tp_table[currentRoutine.r.no_3 * 3 + message_work_.cursor].x + 8 + 1), (ushort)(tp_table[currentRoutine.r.no_3 * 3 + message_work_.cursor].y + 1), 238, 30, 0);
			if (sub_window.routine_[sub_window.stack_ - 1].r.no_0 != 23)
			{
				sub_window.SetObjDispFlag(3);
				sub_window.bar_req_ = SubWindow.BarReq.SELECT;
			}
			else
			{
				sub_window.SetObjDispFlag(37);
				sub_window.bar_req_ = SubWindow.BarReq.MESS;
			}
			currentRoutine.r.no_2++;
			break;
		}
		case 1:
			if (sub_window.CheckObjOut())
			{
				currentRoutine.r.no_1 = 1;
				currentRoutine.r.no_2 = 0;
			}
			break;
		}
	}

	private static void Appear(SubWindow sub_window)
	{
		Routine currentRoutine = sub_window.GetCurrentRoutine();
		Routine3d[] routine_3d = currentRoutine.routine_3d;
		MessageWork message_work_ = GSStatic.message_work_;
		byte b = 1;
		switch (currentRoutine.r.no_2)
		{
		case 0:
		{
			for (ushort num2 = 0; num2 < 3; num2++)
			{
				if (routine_3d[num2].y + 10 < tp_table[currentRoutine.r.no_3 * 3 + num2].y)
				{
					routine_3d[num2].y += 10;
					b = 0;
				}
				else
				{
					routine_3d[num2].y = tp_table[currentRoutine.r.no_3 * 3 + num2].y;
				}
			}
			if (b == 1)
			{
				currentRoutine.r.no_2++;
			}
			break;
		}
		case 1:
			if (sub_window.CheckObjIn() && sub_window.bar_req_ == SubWindow.BarReq.NONE)
			{
				sub_window.busy_ = 0u;
				sub_window.req_ = SubWindow.Req.NONE;
				for (ushort num = 0; num < currentRoutine.tp_cnt; num++)
				{
				}
				currentRoutine.tp_cnt = 0;
				sub_window.cursor_.Rno_1 = 1;
				sub_window.cursor_.Set((ushort)(tp_table[currentRoutine.r.no_3 * 3 + message_work_.cursor].x + 8 + 1), (ushort)(tp_table[currentRoutine.r.no_3 * 3 + message_work_.cursor].y + 1), 238, 30, 0);
				currentRoutine.r.no_1 = 2;
				currentRoutine.r.no_2 = 0;
			}
			break;
		}
	}

	private static void Main(SubWindow sub_window)
	{
		Routine currentRoutine = sub_window.GetCurrentRoutine();
		Routine3d[] routine_3d = currentRoutine.routine_3d;
		MessageWork message_work_ = GSStatic.message_work_;
		GlobalWork global_work_ = GSStatic.global_work_;
		if (global_work_.r.no_0 == 11)
		{
			for (ushort num = 0; num < currentRoutine.tp_cnt; num++)
			{
			}
			currentRoutine.tp_cnt = 0;
			sub_window.busy_ = 3u;
			if (sub_window.req_ == SubWindow.Req.SAVE)
			{
				sub_window.cursor_.Rno_0 = 3;
				sub_window.cursor_.disp_off = 1;
				currentRoutine.r.Set(3, 6, 0, currentRoutine.r.no_3);
				debugLogger.instance.Log("SubWindowStack", "++stack_");
				sub_window.stack_++;
				Routine currentRoutine2 = sub_window.GetCurrentRoutine();
				currentRoutine2.r.Set(15, 0, 0, 0);
				currentRoutine2.tex_no = currentRoutine.tex_no;
			}
		}
		else if (global_work_.r.no_0 == 8)
		{
			for (ushort num2 = 0; num2 < currentRoutine.tp_cnt; num2++)
			{
			}
			currentRoutine.tp_cnt = 0;
			sub_window.busy_ = 3u;
			if (sub_window.req_ == SubWindow.Req.STATUS)
			{
				sub_window.cursor_.Rno_0 = 3;
				sub_window.cursor_.disp_off = 1;
				currentRoutine.r.no_1 = 4;
				currentRoutine.r.no_2 = 0;
			}
		}
		else if (sub_window.req_ != 0)
		{
			for (ushort num3 = 0; num3 < currentRoutine.tp_cnt; num3++)
			{
			}
			currentRoutine.tp_cnt = 0;
			sub_window.busy_ = 3u;
			if (sub_window.req_ == SubWindow.Req.SELECT_EXIT)
			{
				sub_window.busy_ = 3u;
				routine_3d[message_work_.cursor].pallet = 1;
				currentRoutine.r.no_1 = 3;
				currentRoutine.r.no_2 = 0;
			}
		}
		if (sub_window.cursor_.Rno_0 == 2)
		{
			sub_window.cursor_.Set((ushort)(tp_table[currentRoutine.r.no_3 * 3 + message_work_.cursor].x + 8 + 1), (ushort)(tp_table[currentRoutine.r.no_3 * 3 + message_work_.cursor].y + 1), 238, 30, 1);
		}
	}

	private static void Decide(SubWindow sub_window)
	{
		Routine currentRoutine = sub_window.GetCurrentRoutine();
		Routine3d[] routine_3d = currentRoutine.routine_3d;
		MessageWork activeMessageWork = MessageSystem.GetActiveMessageWork();
		MessageWork message_work_ = GSStatic.message_work_;
		switch (currentRoutine.r.no_2)
		{
		case 0:
			if (sub_window.cursor_.Rno_0 == 2)
			{
				routine_3d[message_work_.cursor].timer = 0;
				currentRoutine.r.no_2++;
			}
			break;
		case 1:
			if (routine_3d[message_work_.cursor].timer < 20)
			{
				if (routine_3d[message_work_.cursor].timer++ % 10 > 5)
				{
					routine_3d[message_work_.cursor].disp_off = 0;
				}
				else
				{
					routine_3d[message_work_.cursor].disp_off = 1;
				}
			}
			else
			{
				currentRoutine.r.no_2++;
			}
			break;
		case 2:
		{
			for (ushort num = 0; num < 3; num++)
			{
				routine_3d[num].disp_off = 1;
			}
			routine_3d[message_work_.cursor].disp_off = 0;
			sub_window.cursor_.disp_off = 1;
			currentRoutine.r.no_2++;
			break;
		}
		case 3:
		{
			if (routine_3d[message_work_.cursor].y - 10 > -32)
			{
				routine_3d[message_work_.cursor].y -= 10;
				break;
			}
			activeMessageWork.rt_wait_timer = 10;
			activeMessageWork.status |= MessageSystem.Status.RT_END_WAIT;
			uint message = ((activeMessageWork.cursor == 0) ? activeMessageWork.mdt_data.GetMessage(activeMessageWork.mdt_index + 1) : ((activeMessageWork.cursor != 1) ? activeMessageWork.mdt_data.GetMessage(activeMessageWork.mdt_index + 3) : activeMessageWork.mdt_data.GetMessage(activeMessageWork.mdt_index + 2)));
			advCtrl.instance.message_system_.SetMessage(message);
			debugLogger.instance.Log("SubWindowStack", "--stack_");
			sub_window.stack_--;
			break;
		}
		}
	}

	private static void ToStatus(SubWindow sub_window)
	{
		Routine currentRoutine = sub_window.GetCurrentRoutine();
		Routine3d[] routine_3d = currentRoutine.routine_3d;
		MessageWork message_work_ = GSStatic.message_work_;
		byte b = 1;
		for (ushort num = 0; num < 3; num++)
		{
			if (routine_3d[num].y > -50)
			{
				routine_3d[num].y -= 20;
				b = 0;
			}
			else
			{
				routine_3d[num].y = -50;
			}
			if (message_work_.cursor == num)
			{
			}
		}
		if (b == 1)
		{
			if (sub_window.tantei_tukituke_ != 0)
			{
				sub_window.tantei_tukituke_ = 2;
			}
			currentRoutine.r.Set(3, 5, 0, currentRoutine.r.no_3);
			debugLogger.instance.Log("SubWindowStack", "++stack_");
			sub_window.stack_++;
			Routine currentRoutine2 = sub_window.GetCurrentRoutine();
			currentRoutine2.r.Set(11, 0, 0, 0);
			currentRoutine2.tex_no = currentRoutine.tex_no;
		}
	}

	private static void FromStatus(SubWindow sub_window)
	{
		Routine currentRoutine = sub_window.GetCurrentRoutine();
		Routine3d[] routine_3d = currentRoutine.routine_3d;
		MessageWork message_work_ = GSStatic.message_work_;
		byte b = 1;
		switch (currentRoutine.r.no_2)
		{
		case 0:
		{
			for (ushort num = 0; num < 3; num++)
			{
				if (routine_3d[num].y + 10 < tp_table[currentRoutine.r.no_3 * 3 + num].y)
				{
					routine_3d[num].y += 20;
					b = 0;
				}
				else
				{
					routine_3d[num].y = tp_table[currentRoutine.r.no_3 * 3 + num].y;
				}
			}
			if (b == 1)
			{
				sub_window.SetObjDispFlag(3);
				sub_window.bar_req_ = SubWindow.BarReq.SELECT;
				currentRoutine.r.no_2++;
			}
			break;
		}
		case 1:
			if (sub_window.CheckObjOut())
			{
				sub_window.cursor_.Rno_0 = 0;
				sub_window.cursor_.Set((ushort)(tp_table[currentRoutine.r.no_3 * 3 + message_work_.cursor].x + 8 + 1), (ushort)(tp_table[currentRoutine.r.no_3 * 3 + message_work_.cursor].y + 1), 238, 30, 0);
				currentRoutine.r.no_2++;
			}
			break;
		case 2:
			if (sub_window.CheckObjIn() && sub_window.bar_req_ == SubWindow.BarReq.NONE)
			{
				sub_window.busy_ = 0u;
				sub_window.req_ = SubWindow.Req.NONE;
				sub_window.cursor_.Rno_1 = 1;
				sub_window.cursor_.Set((ushort)(tp_table[currentRoutine.r.no_3 * 3 + message_work_.cursor].x + 8 + 1), (ushort)(tp_table[currentRoutine.r.no_3 * 3 + message_work_.cursor].y + 1), 238, 30, 0);
				if (sub_window.tantei_tukituke_ == 2)
				{
					sub_window.tantei_tukituke_ = 1;
				}
				currentRoutine.r.no_1 = 2;
				currentRoutine.r.no_2 = 0;
			}
			break;
		}
	}

	private static void FromSave(SubWindow sub_window)
	{
		Routine currentRoutine = sub_window.GetCurrentRoutine();
		MessageWork message_work_ = GSStatic.message_work_;
		switch (currentRoutine.r.no_2)
		{
		case 0:
			sub_window.SetObjDispFlag(3);
			sub_window.bar_req_ = SubWindow.BarReq.SELECT;
			sub_window.cursor_.Rno_0 = 0;
			sub_window.cursor_.Set((ushort)(tp_table[currentRoutine.r.no_3 * 3 + message_work_.cursor].x + 8 + 1), (ushort)(tp_table[currentRoutine.r.no_3 * 3 + message_work_.cursor].y + 1), 238, 30, 0);
			currentRoutine.r.no_2++;
			break;
		case 1:
			if (sub_window.CheckObjOut())
			{
				currentRoutine.r.no_2++;
			}
			break;
		case 2:
			if (sub_window.CheckObjIn() && sub_window.bar_req_ == SubWindow.BarReq.NONE)
			{
				sub_window.busy_ = 0u;
				sub_window.req_ = SubWindow.Req.NONE;
				for (ushort num = 0; num < currentRoutine.tp_cnt; num++)
				{
				}
				sub_window.cursor_.Rno_1 = 1;
				sub_window.cursor_.Set((ushort)(tp_table[currentRoutine.r.no_3 * 3 + message_work_.cursor].x + 8 + 1), (ushort)(tp_table[currentRoutine.r.no_3 * 3 + message_work_.cursor].y + 1), 238, 30, 0);
				currentRoutine.tp_cnt = 0;
				currentRoutine.r.no_1 = 2;
				currentRoutine.r.no_2 = 0;
			}
			break;
		}
	}
}
