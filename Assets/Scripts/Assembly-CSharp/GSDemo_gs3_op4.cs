using UnityEngine;

public class GSDemo_gs3_op4
{
	private struct DSP
	{
		public ushort timer;

		public ushort wait;

		public ushort count;

		public ushort bcount;

		public ushort total;

		public ushort cursor;

		public ushort cwait;

		public ushort face_proc;

		public ushort com_proc;

		public ushort mess_flag;

		public ushort face_f;

		public ushort Bg2_f;

		public ushort Bg2_disp;

		public ushort[] work;

		public ushort ox;

		public ushort oy;

		public ushort write_x;

		public ushort code;

		public ushort line;

		public ushort[] moji_code_;

		public ushort[] buff;

		public ushort[] buff_code;
	}

	private const int CURSOR_WAIT = 20;

	private static DSP dsp = default(DSP);

	private static int mFaceNo;

	public static void DemoProcPCDataView(MessageWork message_work)
	{
		switch (message_work.op_para)
		{
		case 0:
			message_work.op_para = 50;
			break;
		case 50:
		{
			AnimationSystem.Instance.StopObject(1, 0, 50);
			fadeCtrl.instance.play(fadeCtrl.Status.FADE_IN, 0u, 1u);
			message_work.status2 |= MessageSystem.Status2.STOP_EXPL;
			PcViewCtrl.instance.LoadBackGround();
			fadeCtrl.instance.play(0, true);
			dsp.total = SetOp4SysMess(scenario_GS3.SYS_OP4_MES00);
			dsp.timer = 0;
			dsp.wait = 0;
			dsp.cwait = 0;
			dsp.count = 0;
			dsp.bcount = 0;
			dsp.cursor = 1;
			dsp.face_f = 0;
			dsp.Bg2_f = 1;
			dsp.Bg2_disp = 0;
			dsp.write_x = 0;
			dsp.line = 0;
			AnimationObject animationObject = AnimationSystem.Instance.PlayObject(1, 0, 51);
			animationObject.transform.localPosition = -Vector3.up * 1280f;
			animationObject.gameObject.SetActive(false);
			animationObject = AnimationSystem.Instance.PlayObject(1, 0, 53);
			animationObject.gameObject.SetActive(false);
			animationObject.transform.localPosition = Vector3.up * 304f;
			animationObject.transform.localPosition += PcViewCtrl.instance.Get_OBJ_OP4_008_DiffPosition();
			animationObject = AnimationSystem.Instance.PlayObject(1, 0, 54);
			animationObject.gameObject.SetActive(false);
			dsp.com_proc = 10;
			dsp.work = new ushort[4];
			dsp.work[0] = 0;
			dsp.work[1] = 0;
			dsp.work[2] = 0;
			dsp.mess_flag = 0;
			message_work.op_para = 1;
			break;
		}
		case 1:
			switch (dsp.com_proc)
			{
			case 0:
			{
				AnimationObject animationObject = AnimationSystem.Instance.FindObject(1, 0, 51);
				animationObject.gameObject.SetActive(true);
				animationObject.transform.localPosition += Vector3.up * 2f * 4.5f;
				if (animationObject.transform.localPosition.y >= -108f)
				{
					animationObject.transform.localPosition = new Vector3(animationObject.transform.localPosition.x, -108f, animationObject.transform.localPosition.z);
					animationObject = AnimationSystem.Instance.FindObject(1, 0, 53);
					if (animationObject != null)
					{
						animationObject.gameObject.SetActive(true);
					}
					animationObject = AnimationSystem.Instance.FindObject(1, 0, 54);
					if (animationObject != null)
					{
						animationObject.gameObject.SetActive(true);
					}
					else
					{
						animationObject = AnimationSystem.Instance.PlayObject(1, 0, 54);
						animationObject.gameObject.SetActive(true);
					}
					dsp.com_proc = 1;
				}
				break;
			}
			case 1:
			{
				AnimationObject animationObject = AnimationSystem.Instance.FindObject(1, 0, 53);
				animationObject.transform.localPosition -= Vector3.up * 2f * 4.5f;
				if (animationObject.transform.localPosition.y <= 0f)
				{
					animationObject.transform.localPosition = new Vector3(animationObject.transform.localPosition.x, 0f, animationObject.transform.localPosition.z);
					dsp.com_proc = 0;
					message_work.op_para = 10;
				}
				break;
			}
			case 10:
				mFaceNo = 0;
				dsp.com_proc = 0;
				break;
			}
			break;
		case 10:
			if (++dsp.timer >= 120)
			{
				dsp.wait = dsp.buff[dsp.bcount];
				if (GSUtility.GetLanguageLayoutType(GSStatic.global_work_.language) == "JAPAN")
				{
					dsp.bcount++;
					dsp.count++;
				}
				PcViewCtrl.instance.PcViewInitialize(0);
				dsp.timer = 0;
				message_work.op_para = 11;
			}
			else if (++dsp.cwait >= 20)
			{
				dsp.cwait = 0;
				if (dsp.cursor == 1)
				{
					PcViewCtrl.instance.CursorEnable(false);
				}
				else
				{
					PcViewCtrl.instance.CursorEnable(true);
				}
				dsp.cursor ^= 1;
			}
			break;
		case 11:
			if (++dsp.timer >= dsp.wait)
			{
				dsp.timer = 0;
				dsp.wait = dsp.buff[dsp.bcount];
				dsp.bcount++;
				if (dsp.wait != ushort.MaxValue && (dsp.wait & 0x8000) > 0)
				{
					dsp.wait &= 255;
					if (dsp.wait != 1)
					{
					}
					break;
				}
				if (dsp.wait != ushort.MaxValue && (dsp.wait & 0x4000) > 0)
				{
					dsp.face_f = (ushort)(dsp.wait - 16384);
					dsp.work[0] = 168;
					dsp.work[1] = 48;
					dsp.work[2] = 1;
					dsp.wait = 0;
					dsp.face_proc = 0;
					break;
				}
				dsp.write_x++;
				dsp.count++;
				if (dsp.wait == ushort.MaxValue)
				{
					if (dsp.mess_flag == 0)
					{
						dsp.mess_flag = 1;
						dsp.cwait = 0;
						AnimationSystem.Instance.StopObject(1, 0, 54);
						AnimationSystem.Instance.PlayObject(1, 0, 55);
						AnimationObject animationObject = AnimationSystem.Instance.PlayObject(1, 0, 52);
						animationObject.transform.localPosition = Vector3.up * -1188f;
						dsp.cursor = 0;
						message_work.op_para = 12;
					}
					else
					{
						fadeCtrl.instance.play(fadeCtrl.Status.FADE_OUT, 0u, 0u);
						message_work.op_para = 14;
					}
				}
				else
				{
					if (dsp.moji_code_[dsp.bcount - 1] != 0)
					{
						PcViewCtrl.instance.PcViewNext();
					}
					soundCtrl.instance.StopSE(386);
					soundCtrl.instance.PlaySE(386);
					dsp.cwait = 0;
					dsp.cursor = 1;
					PcViewCtrl.instance.CursorEnable(true);
				}
			}
			else if (++dsp.cwait >= 20)
			{
				dsp.cwait = 0;
				if (dsp.cursor == 1)
				{
					PcViewCtrl.instance.CursorEnable(false);
				}
				else
				{
					PcViewCtrl.instance.CursorEnable(true);
				}
				dsp.cursor ^= 1;
			}
			break;
		case 12:
		{
			for (int i = 0; i < 3; i++)
			{
				AnimationObject animationObject = AnimationSystem.Instance.FindObject(1, 0, 51 + i);
				if (animationObject != null)
				{
					animationObject.transform.localPosition += Vector3.up * 2f * 4.5f * 1.5f;
				}
			}
			PcViewCtrl.instance.rect_transform_.localPosition += Vector3.up * 2f * 4.5f * 1.5f;
			if (PcViewCtrl.instance.rect_transform_.localPosition.y >= 1670f)
			{
				PcViewCtrl.instance.rect_transform_.localPosition = Vector3.up * 1670f;
				AnimationSystem.Instance.StopObject(1, 0, 51);
				PcViewCtrl.instance.DellIcon();
				AnimationObject animationObject = AnimationSystem.Instance.FindObject(1, 0, 53);
				animationObject.transform.localPosition = Vector3.up * 304f;
				animationObject.transform.localPosition += PcViewCtrl.instance.Get_OBJ_OP4_008_DiffPosition();
				message_work.op_para = 13;
			}
			break;
		}
		case 13:
		{
			AnimationObject animationObject = AnimationSystem.Instance.FindObject(1, 0, 53);
			animationObject.transform.localPosition -= Vector3.up * 2f * 4.5f * 1.5f;
			if (animationObject.transform.localPosition.y <= 0f)
			{
				animationObject.transform.localPosition = new Vector3(animationObject.transform.localPosition.x, 0f, animationObject.transform.localPosition.z);
				dsp.com_proc = 0;
				PcViewCtrl.instance.PcViewInitialize(1);
				dsp.total = SetOp4SysMess(scenario_GS3.SYS_OP4_MES01);
				dsp.timer = 0;
				dsp.wait = 0;
				dsp.cwait = 0;
				dsp.count = 0;
				dsp.bcount = 0;
				dsp.cursor = 1;
				dsp.face_f = 0;
				dsp.write_x = 0;
				dsp.line = 0;
				message_work.op_para = 11;
			}
			break;
		}
		case 14:
			dsp.Bg2_f = 0;
			AnimationSystem.Instance.StopObject(1, 0, 55);
			AnimationSystem.Instance.StopObject(1, 0, 52);
			AnimationSystem.Instance.StopObject(1, 0, 53);
			PcViewCtrl.instance.EndView();
			message_work.status2 &= ~MessageSystem.Status2.STOP_EXPL;
			GSDemo.MoveNextScript(message_work);
			break;
		}
		if (dsp.face_f > 0)
		{
			Face_Disp(dsp.face_f);
		}
		if (dsp.Bg2_f == 1)
		{
			dsp.Bg2_disp ^= 1;
			PcViewCtrl.instance.PcLineUpdate(dsp.Bg2_disp);
		}
		else if (message_work.op_para == 50)
		{
		}
	}

	private static void Face_Disp(ushort type)
	{
		float num = 252f;
		if (GSUtility.GetLanguageLayoutType(GSStatic.global_work_.language) != "JAPAN")
		{
			num += 12f;
		}
		switch (dsp.face_proc)
		{
		case 0:
			PcViewCtrl.instance.init_icon(type);
			dsp.work[0] = 0;
			dsp.work[1] = 0;
			dsp.face_proc = 1;
			mFaceNo = type;
			break;
		case 1:
			if (++dsp.work[0] >= dsp.work[2])
			{
				dsp.work[0] = 0;
				if (++dsp.work[1] >= 64)
				{
					dsp.face_f = 0;
				}
				float fill = (float)(int)dsp.work[1] / 64f;
				PcViewCtrl.instance.icon_view(mFaceNo, fill);
			}
			break;
		}
	}

	private static ushort SetOp4SysMess(uint no)
	{
		int num = 0;
		ushort num2 = 8;
		ushort num3 = 0;
		dsp.buff = new ushort[1024];
		dsp.moji_code_ = new ushort[1024];
		dsp.buff_code = new ushort[1024];
		MessageSystem.SetActiveMessageWindow(WindowType.SUB);
		advCtrl.instance.message_system_.SetMessage(no);
		MessageWork activeMessageWork = MessageSystem.GetActiveMessageWork();
		while (activeMessageWork.code != 69)
		{
			activeMessageWork.code = activeMessageWork.mdt_data.GetMessage(activeMessageWork.mdt_index);
			if (activeMessageWork.code > 128)
			{
				num++;
				dsp.moji_code_[num3] = activeMessageWork.code;
				dsp.buff[num3++] = num2;
			}
			else
			{
				if (activeMessageWork.code == 46)
				{
				}
				if (activeMessageWork.code == 12)
				{
					activeMessageWork.mdt_index++;
					activeMessageWork.code = activeMessageWork.mdt_data.GetMessage(activeMessageWork.mdt_index);
					dsp.buff[num3++] = (ushort)(32768 + activeMessageWork.code);
				}
				else if (activeMessageWork.code == 11)
				{
					activeMessageWork.mdt_index++;
					activeMessageWork.code = activeMessageWork.mdt_data.GetMessage(activeMessageWork.mdt_index);
					num2 = activeMessageWork.code;
				}
				else if (activeMessageWork.code == 32)
				{
					activeMessageWork.mdt_index++;
					activeMessageWork.code = activeMessageWork.mdt_data.GetMessage(activeMessageWork.mdt_index);
					dsp.buff[num3++] = (ushort)(16384 + activeMessageWork.code);
				}
				else if (activeMessageWork.code == 45)
				{
					dsp.buff[num3++] = 32769;
				}
			}
			activeMessageWork.mdt_index++;
		}
		dsp.buff[num3++] = ushort.MaxValue;
		MessageSystem.SetActiveMessageWindow(WindowType.MAIN);
		return num3;
	}
}
