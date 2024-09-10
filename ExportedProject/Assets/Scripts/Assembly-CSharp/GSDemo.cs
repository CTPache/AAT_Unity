using UnityEngine;

public static class GSDemo
{
	private class FURIKO
	{
		public ushort time;

		public sbyte wait;

		public sbyte dir;
	}

	public class QuakeWork
	{
		public ushort time;

		public sbyte Qx;

		public sbyte Qy;
	}

	private class FlashWork
	{
		public ushort time;

		public ushort sw;
	}

	private struct OP03_WORK
	{
		public ushort proc;

		public ushort[] Lspeed;

		public ushort[] Lframe;

		public ushort[] Rspeed;

		public ushort[] Rframe;

		public ushort CrossTime;

		public ushort FadeSpeed;

		public ushort CrossTime2;

		public ushort FadeSpeed2;

		public ushort FadeinSpeed;

		public ushort Fade;

		public ushort speed;

		public ushort timer;

		public int count;

		public int line;

		public int wait;

		public ushort[] work;

		public ushort init;
	}

	private delegate void DemoProc(MessageWork message_work);

	public const float TO_UNTIY_RATE = 4.5f;

	private const int MOSAIC_SPEED = 17;

	private const int MOSAIC_WAIT = 2;

	private const int MOSAIC_COUNT = 30;

	private const int SHAWL_FADE_SPEED = 1;

	private const int OBJ_FADE_LIMIT = 15;

	private static float TinamiOfs0;

	private static float TinamiOfs1;

	private static readonly float[] sTamashiPosX;

	private static readonly float[] sTamashiPosY;

	private static readonly float[,] s_SprA;

	private static int TinamiTimer;

	private static FURIKO[] furiko;

	private static readonly ushort[] scroll_para;

	private static readonly ushort[,] BG_Data;

	private static QuakeWork[] Qdata;

	private static readonly FlashWork[] Fdata;

	private static OP03_WORK op3_work;

	private const int OBJ_FOUT_SPEED = 2;

	private const int OBJ_FIN_SPEED = 2;

	private static readonly DemoProc[] proc_table;

	static GSDemo()
	{
		TinamiOfs0 = 0f;
		TinamiOfs1 = 0f;
		sTamashiPosX = new float[11]
		{
			-29.564972f, 19.44003f, -174.68997f, -294.97498f, -228.14998f, 28.35003f, 284.85004f, 421.06503f, 329.40005f, 157.54503f,
			37.26003f
		};
		sTamashiPosY = new float[11]
		{
			-330.88504f, -103.680016f, 52.244987f, 237.465f, 511.785f, 634.635f, 520.695f, 268.65f, -63.585014f, -250.69502f,
			290.925f
		};
		s_SprA = new float[30, 3]
		{
			{ 1f, 0.5f, 0.5f },
			{ 1f, 0.6f, 0.5f },
			{ 1f, 0.7f, 0.5f },
			{ 1f, 0.8f, 0.5f },
			{ 1f, 0.9f, 0.5f },
			{ 1f, 1f, 0.5f },
			{ 0.9f, 1f, 0.5f },
			{ 0.8f, 1f, 0.5f },
			{ 0.7f, 1f, 0.5f },
			{ 0.6f, 1f, 0.5f },
			{ 0.5f, 1f, 0.5f },
			{ 0.5f, 1f, 0.6f },
			{ 0.5f, 1f, 0.7f },
			{ 0.5f, 1f, 0.8f },
			{ 0.5f, 1f, 0.9f },
			{ 0.5f, 1f, 1f },
			{ 0.5f, 0.9f, 1f },
			{ 0.5f, 0.8f, 1f },
			{ 0.5f, 0.7f, 1f },
			{ 0.5f, 0.6f, 1f },
			{ 0.5f, 0.5f, 1f },
			{ 0.6f, 0.5f, 1f },
			{ 0.7f, 0.5f, 1f },
			{ 0.8f, 0.5f, 1f },
			{ 0.9f, 0.5f, 1f },
			{ 1f, 0.5f, 1f },
			{ 1f, 0.5f, 0.9f },
			{ 1f, 0.5f, 0.8f },
			{ 1f, 0.5f, 0.7f },
			{ 1f, 0.5f, 0.6f }
		};
		TinamiTimer = 0;
		furiko = new FURIKO[28]
		{
			new FURIKO
			{
				time = 0,
				wait = 1,
				dir = 1
			},
			new FURIKO
			{
				time = 12,
				wait = 2,
				dir = 3
			},
			new FURIKO
			{
				time = 24,
				wait = 2,
				dir = 3
			},
			new FURIKO
			{
				time = 36,
				wait = 2,
				dir = 3
			},
			new FURIKO
			{
				time = 48,
				wait = 2,
				dir = 3
			},
			new FURIKO
			{
				time = 60,
				wait = 1,
				dir = 1
			},
			new FURIKO
			{
				time = 72,
				wait = 1,
				dir = 1
			},
			new FURIKO
			{
				time = 84,
				wait = 2,
				dir = 1
			},
			new FURIKO
			{
				time = 96,
				wait = 3,
				dir = 1
			},
			new FURIKO
			{
				time = 108,
				wait = 6,
				dir = 1
			},
			new FURIKO
			{
				time = 120,
				wait = 1,
				dir = 0
			},
			new FURIKO
			{
				time = 142,
				wait = 1,
				dir = 0
			},
			new FURIKO
			{
				time = 154,
				wait = 6,
				dir = -1
			},
			new FURIKO
			{
				time = 166,
				wait = 4,
				dir = -1
			},
			new FURIKO
			{
				time = 178,
				wait = 4,
				dir = -1
			},
			new FURIKO
			{
				time = 190,
				wait = 3,
				dir = -1
			},
			new FURIKO
			{
				time = 202,
				wait = 2,
				dir = -1
			},
			new FURIKO
			{
				time = 214,
				wait = 2,
				dir = -1
			},
			new FURIKO
			{
				time = 226,
				wait = 1,
				dir = -1
			},
			new FURIKO
			{
				time = 238,
				wait = 1,
				dir = -1
			},
			new FURIKO
			{
				time = 250,
				wait = 1,
				dir = -1
			},
			new FURIKO
			{
				time = 262,
				wait = 1,
				dir = -1
			},
			new FURIKO
			{
				time = 274,
				wait = 1,
				dir = -1
			},
			new FURIKO
			{
				time = 286,
				wait = 1,
				dir = -1
			},
			new FURIKO
			{
				time = 298,
				wait = 1,
				dir = -1
			},
			new FURIKO
			{
				time = 307,
				wait = 1,
				dir = -1
			},
			new FURIKO
			{
				time = 322,
				wait = 1,
				dir = 0
			},
			new FURIKO
			{
				time = 330,
				wait = -1,
				dir = 0
			}
		};
		scroll_para = new ushort[22]
		{
			12, 12, 12, 12, 8, 4, 3, 3, 2, 2,
			2, 2, 1, 1, 1, 1, 3, 2, 2, 1,
			1, 1
		};
		BG_Data = new ushort[7, 3]
		{
			{ 119, 1, 20 },
			{ 108, 1, 0 },
			{ 118, 2, 0 },
			{ 117, 2, 0 },
			{ 61, 3, 0 },
			{ 60, 3, 0 },
			{ 65535, 0, 0 }
		};
		Qdata = new QuakeWork[12]
		{
			new QuakeWork
			{
				time = 12,
				Qx = 0,
				Qy = -4
			},
			new QuakeWork
			{
				time = 78,
				Qx = 0,
				Qy = 2
			},
			new QuakeWork
			{
				time = 150,
				Qx = 0,
				Qy = -4
			},
			new QuakeWork
			{
				time = 214,
				Qx = 0,
				Qy = 2
			},
			new QuakeWork
			{
				time = 288,
				Qx = 0,
				Qy = 4
			},
			new QuakeWork
			{
				time = 345,
				Qx = 0,
				Qy = -4
			},
			new QuakeWork
			{
				time = 320,
				Qx = 0,
				Qy = 2
			},
			new QuakeWork
			{
				time = 488,
				Qx = 0,
				Qy = -3
			},
			new QuakeWork
			{
				time = 512,
				Qx = 0,
				Qy = 2
			},
			new QuakeWork
			{
				time = 588,
				Qx = 0,
				Qy = -2
			},
			new QuakeWork
			{
				time = 640,
				Qx = 0,
				Qy = 3
			},
			new QuakeWork
			{
				time = 670,
				Qx = 0,
				Qy = 0
			}
		};
		Fdata = new FlashWork[21]
		{
			new FlashWork
			{
				time = 25,
				sw = 1
			},
			new FlashWork
			{
				time = 30,
				sw = 0
			},
			new FlashWork
			{
				time = 47,
				sw = 1
			},
			new FlashWork
			{
				time = 50,
				sw = 0
			},
			new FlashWork
			{
				time = 100,
				sw = 1
			},
			new FlashWork
			{
				time = 105,
				sw = 0
			},
			new FlashWork
			{
				time = 110,
				sw = 1
			},
			new FlashWork
			{
				time = 115,
				sw = 0
			},
			new FlashWork
			{
				time = 120,
				sw = 1
			},
			new FlashWork
			{
				time = 122,
				sw = 0
			},
			new FlashWork
			{
				time = 124,
				sw = 1
			},
			new FlashWork
			{
				time = 126,
				sw = 0
			},
			new FlashWork
			{
				time = 128,
				sw = 1
			},
			new FlashWork
			{
				time = 130,
				sw = 0
			},
			new FlashWork
			{
				time = 132,
				sw = 1
			},
			new FlashWork
			{
				time = 134,
				sw = 0
			},
			new FlashWork
			{
				time = 136,
				sw = 1
			},
			new FlashWork
			{
				time = 138,
				sw = 0
			},
			new FlashWork
			{
				time = 140,
				sw = 1
			},
			new FlashWork
			{
				time = 142,
				sw = 0
			},
			new FlashWork
			{
				time = 999,
				sw = 0
			}
		};
		op3_work = default(OP03_WORK);
		proc_table = new DemoProc[7] { Op0DemoProc, Op2DemoProc, Op3DemoProc, Op4DemoProc, Op5DemoProc, OpFirstDemoProc, OpSecondDemoProc };
	}

	public static void Play(MessageWork message_work)
	{
		proc_table[message_work.op_no](message_work);
	}

	private static void Op0DemoProc(MessageWork message_work)
	{
		GlobalWork global_work_ = GSStatic.global_work_;
		switch (message_work.op_workno)
		{
		case 1:
			switch (message_work.op_para)
			{
			case 0:
			case 2:
			{
				AnimationObject animationObject2 = AnimationSystem.Instance.FindObject(1, 0, 13);
				if (animationObject2 == null)
				{
					message_work.status |= MessageSystem.Status.FADE_OK;
					message_work.status |= MessageSystem.Status.NO_FADE;
					animationObject2 = AnimationSystem.Instance.PlayObject(1, 0, 13);
					if (message_work.op_para == 0)
					{
						AnimationSystem.Instance.CharFade(3329, 6);
					}
				}
				animationObject2 = AnimationSystem.Instance.FindObject(1, 0, 19);
				if (animationObject2 == null)
				{
					animationObject2 = AnimationSystem.Instance.PlayObject(1, 0, 19);
					animationObject2.SetPriority(AnimationRenderTarget.BehingBg);
					animationObject2.Fade(false, 0);
				}
				if (message_work.op_para == 2)
				{
				}
				Exit(message_work);
				break;
			}
			case 1:
				if (message_work.all_work[0] == 0)
				{
					AnimationSystem.Instance.StopObject(1, 0, 13, true);
					message_work.status &= ~MessageSystem.Status.FADE_OK;
					message_work.status &= ~MessageSystem.Status.NO_FADE;
					message_work.all_work[0] = 1;
					AnimationObject animationObject = AnimationSystem.Instance.FindObject(1, 0, 19);
					if (animationObject != null)
					{
						animationObject.Fade(false, 16);
					}
				}
				else if (message_work.all_work[0] == 1 && fadeCtrl.instance.status == fadeCtrl.Status.NO_FADE)
				{
					Exit(message_work);
				}
				break;
			case 3:
				AnimationSystem.Instance.CharFade(3332, 6);
				Exit(message_work);
				break;
			}
			break;
		case 2:
			if (message_work.op_para == 0)
			{
				AnimationObject animationObject12 = AnimationSystem.Instance.FindObject(1, 0, 14);
				if (animationObject12 == null)
				{
					animationObject12 = AnimationSystem.Instance.PlayObject(1, 0, 14);
				}
				Exit(message_work);
			}
			break;
		case 3:
			if (message_work.op_para == 0)
			{
				AnimationObject animationObject10 = AnimationSystem.Instance.FindObject(1, 0, 14);
				if (animationObject10 == null)
				{
					animationObject10 = AnimationSystem.Instance.PlayObject(1, 0, 14);
				}
				Transform transform = animationObject10.transform;
				if (message_work.op_work[0] == 0)
				{
					message_work.status &= ~MessageSystem.Status.FADE_OK;
					message_work.status &= ~MessageSystem.Status.NO_FADE;
					message_work.op_work[0] = 1;
					message_work.op_work[1] = 0;
					message_work.op_work[2] = 0;
					message_work.op_work[3] = 0;
					transform.eulerAngles = Vector3.zero;
				}
				message_work.op_work[1]++;
				float num = 1f + (float)(int)message_work.op_work[1] / 2048f;
				transform.localScale = new Vector3(num, num, 1f);
				Vector3 eulerAngles = transform.eulerAngles;
				eulerAngles.z += -0.0146484375f;
				transform.eulerAngles = eulerAngles;
			}
			else
			{
				AnimationObject animationObject11 = AnimationSystem.Instance.FindObject(1, 0, 14);
				Transform transform2 = animationObject11.transform;
				transform2.localScale = Vector3.one;
				transform2.eulerAngles = Vector3.zero;
				AnimationSystem.Instance.StopObject(1, 0, 14);
				Exit(message_work);
			}
			break;
		case 4:
			if (message_work.op_para == 0)
			{
				AnimationObject animationObject13 = AnimationSystem.Instance.FindObject(1, 0, 15);
				if (animationObject13 == null)
				{
					animationObject13 = AnimationSystem.Instance.PlayObject(1, 0, 15);
				}
				Exit(message_work);
			}
			else
			{
				AnimationSystem.Instance.StopObject(1, 0, 15);
				Exit(message_work);
			}
			break;
		case 5:
			if (message_work.op_para == 0)
			{
				Exit(message_work);
			}
			else if (message_work.op_para == 1)
			{
				bgCtrl.instance.DetentionBgFadeInit(1u, 16u, 1u);
				message_work.op_para = 2;
				message_work.all_work[2] = 16;
				AnimationObject animationObject8 = AnimationSystem.Instance.FindObject(1, 0, 19);
				if (animationObject8 != null)
				{
					animationObject8.Fade(true, 96);
				}
			}
			else
			{
				message_work.all_work[2]--;
				if (0 >= message_work.all_work[2])
				{
					message_work.all_work[2] = 0;
					Exit(message_work);
				}
			}
			break;
		case 6:
			if (message_work.op_para == 0)
			{
				AnimationObject animationObject7 = AnimationSystem.Instance.FindObject(1, 0, 16);
				if (animationObject7 == null)
				{
					animationObject7 = AnimationSystem.Instance.PlayObject(1, 0, 16);
				}
				Exit(message_work);
			}
			else
			{
				AnimationSystem.Instance.StopObject(1, 0, 16, true);
				Exit(message_work);
			}
			break;
		case 7:
			if (message_work.op_para == 0)
			{
				bgCtrl.instance.SetWhiteBG();
				Exit(message_work);
			}
			break;
		case 8:
			if (message_work.op_para == 0)
			{
				AnimationObject animationObject9 = AnimationSystem.Instance.FindObject(1, 0, 98);
				if (animationObject9 == null)
				{
					animationObject9 = AnimationSystem.Instance.PlayObject(1, 0, 98);
				}
				Exit(message_work);
			}
			else
			{
				AnimationSystem.Instance.StopObject(1, 0, 98);
				Exit(message_work);
			}
			break;
		case 9:
			switch (message_work.op_para)
			{
			case 0:
				if (!fadeCtrl.instance.IsExecOut(1))
				{
					fadeCtrl.instance.SetFade(1, 14u, 16u, false, Color.black, false);
					message_work.status2 |= MessageSystem.Status2.DISABLE_FONT_ALPHA;
				}
				message_work.status |= MessageSystem.Status.OBJ_MOSAIC;
				if (message_work.op_work[5] == 14)
				{
					bgCtrl.instance.SetReserveSprite();
				}
				if (++message_work.op_work[5] > 15)
				{
					message_work.op_work[5] = 14;
					message_work.op_para = 1;
					fadeCtrl.instance.SetFade(1, 15u, 16u, true, Color.black, false);
				}
				break;
			case 1:
				if (message_work.op_work[5] > 0)
				{
					message_work.op_work[5]--;
				}
				else
				{
					message_work.op_para = 2;
				}
				break;
			case 2:
				message_work.status &= ~MessageSystem.Status.OBJ_MOSAIC;
				message_work.status2 &= ~MessageSystem.Status2.DISABLE_FONT_ALPHA;
				message_work.op_flg = 0;
				message_work.mdt_index += 3u;
				Exit(message_work);
				break;
			}
			break;
		case 10:
			Demo01_ObjProc(message_work);
			break;
		case 11:
		{
			AnimationObject animationObject6 = AnimationSystem.Instance.FindObject(1, 0, 19);
			if (animationObject6 != null)
			{
				animationObject6.Fade(true, 0);
			}
			message_work.all_work[0] = 0;
			Exit(message_work);
			break;
		}
		case 12:
			switch (message_work.op_para)
			{
			case 0:
			{
				AnimationSystem.Instance.StopObject(1, 0, 13, true);
				message_work.status &= ~MessageSystem.Status.FADE_OK;
				message_work.status &= ~MessageSystem.Status.NO_FADE;
				message_work.op_work[1] = message_work.all_work[0];
				fadeCtrl.instance.status = fadeCtrl.Status.NO_FADE;
				AnimationObject animationObject4 = AnimationSystem.Instance.FindObject(1, 0, 19);
				if (animationObject4 == null)
				{
					animationObject4 = AnimationSystem.Instance.PlayObject(1, 0, 19);
					animationObject4.SetPriority(AnimationRenderTarget.BehingBg);
					animationObject4.Fade(true, 0);
				}
				animationObject4 = AnimationSystem.Instance.FindObject(1, 0, 18);
				if (animationObject4 == null)
				{
					animationObject4 = AnimationSystem.Instance.PlayObject(1, 0, 18);
					animationObject4.transform.localPosition = Vector3.up * 857f;
					animationObject4.SetPriority(AnimationRenderTarget.BehingBg);
					animationObject4.Fade(true, 0);
				}
				animationObject4 = AnimationSystem.Instance.FindObject(1, 0, 17);
				if (animationObject4 == null)
				{
					animationObject4 = AnimationSystem.Instance.PlayObject(1, 0, 17);
					animationObject4.transform.localPosition = Vector3.up * 1289f;
					animationObject4.SetPriority(AnimationRenderTarget.BehingBg);
					animationObject4.Fade(true, 0);
				}
				message_work.all_work[0] = 0;
				message_work.all_work[1] = 0;
				message_work.all_work[2] = 0;
				bgCtrl instance = bgCtrl.instance;
				instance.SetBodyPosition(Vector3.zero);
				message_work.op_para = 11;
				break;
			}
			case 11:
				if (++message_work.all_work[0] > 10)
				{
					message_work.all_work[0] = 0;
					message_work.op_para = 1;
				}
				break;
			case 1:
			{
				message_work.all_work[2]++;
				if (message_work.all_work[2] > 1)
				{
					message_work.all_work[0]++;
					bgCtrl instance2 = bgCtrl.instance;
					Vector3 localPosition = instance2.body.transform.localPosition;
					localPosition.y -= 4f;
					instance2.SetBodyPosition(localPosition);
					if (message_work.all_work[0] == 120)
					{
						Exit(message_work);
					}
					message_work.all_work[2] = 0;
				}
				Vector3 vector = Vector3.up * 504f / 120f;
				AnimationObject animationObject5 = AnimationSystem.Instance.FindObject(1, 0, 17);
				animationObject5.transform.localPosition -= vector;
				animationObject5 = AnimationSystem.Instance.FindObject(1, 0, 18);
				animationObject5.transform.localPosition -= vector;
				animationObject5 = AnimationSystem.Instance.FindObject(1, 0, 19);
				animationObject5.transform.localPosition -= vector;
				break;
			}
			case 2:
				AnimationSystem.Instance.StopObject(1, 0, 13);
				AnimationSystem.Instance.StopObject(1, 0, 17);
				AnimationSystem.Instance.StopObject(1, 0, 18);
				AnimationSystem.Instance.StopObject(1, 0, 19);
				Exit(message_work);
				break;
			case 3:
			{
				AnimationObject animationObject3 = AnimationSystem.Instance.FindObject(1, 0, 19);
				if (animationObject3 != null)
				{
					animationObject3.Fade(true, 0);
				}
				Exit(message_work);
				break;
			}
			}
			break;
		case 13:
			if (message_work.op_para == 0)
			{
				global_work_.status_flag |= 1u;
				message_work.status2 |= MessageSystem.Status2.QUAKE_F;
			}
			else
			{
				global_work_.status_flag &= 4294967294u;
				message_work.status2 &= ~MessageSystem.Status2.QUAKE_F;
				Exit(message_work);
			}
			break;
		case 98:
			DemoProc_Special(message_work);
			break;
		case 0:
			Exit(message_work);
			break;
		case 128:
			Exit(message_work);
			break;
		}
	}

	private static void Demo01_ObjProc(MessageWork message_work)
	{
		switch (message_work.op_para)
		{
		case 0:
			AnimationSystem.Instance.PlayObject(2, 0, 93);
			Exit(message_work);
			break;
		case 1:
		{
			AnimationObject animationObject5 = AnimationSystem.Instance.FindObject((int)GSStatic.global_work_.title, 0, 93);
			if (animationObject5 != null)
			{
				animationObject5.Stop(true);
			}
			Exit(message_work);
			break;
		}
		case 2:
			AnimationSystem.Instance.PlayObject(2, 0, 94);
			Exit(message_work);
			break;
		case 3:
		{
			AnimationObject animationObject7 = AnimationSystem.Instance.FindObject((int)GSStatic.global_work_.title, 0, 94);
			if (animationObject7 != null)
			{
				animationObject7.Stop(true);
			}
			Exit(message_work);
			break;
		}
		case 4:
			AnimationSystem.Instance.PlayObject(2, 0, 95);
			Exit(message_work);
			break;
		case 5:
		{
			AnimationObject animationObject2 = AnimationSystem.Instance.FindObject((int)GSStatic.global_work_.title, 0, 95);
			if (animationObject2 != null)
			{
				animationObject2.Stop(true);
			}
			Exit(message_work);
			break;
		}
		case 6:
			switch (message_work.op_work[0])
			{
			default:
				return;
			case 0:
				message_work.op_work[1] = 0;
				message_work.op_work[0] = 1;
				break;
			case 1:
				break;
			}
			FadeOutObj(message_work, 94);
			break;
		case 7:
			switch (message_work.op_work[0])
			{
			default:
				return;
			case 0:
			{
				AnimationObject animationObject6 = AnimationSystem.Instance.PlayObject(2, 0, 95);
				animationObject6.Alpha = 0f;
				message_work.op_work[1] = 0;
				message_work.op_work[0] = 1;
				break;
			}
			case 1:
				break;
			}
			FadeInObj(message_work, 95);
			break;
		case 8:
			switch (message_work.op_work[0])
			{
			default:
				return;
			case 0:
				message_work.op_work[1] = 0;
				message_work.op_work[0] = 1;
				break;
			case 1:
				break;
			}
			FadeOutObj(message_work, 95);
			break;
		case 9:
			switch (message_work.op_work[0])
			{
			default:
				return;
			case 0:
				message_work.op_work[1] = 0;
				message_work.op_work[0] = 1;
				break;
			case 1:
				break;
			}
			FadeOutObj(message_work, 93);
			break;
		case 10:
			switch (message_work.op_work[0])
			{
			case 0:
			{
				AnimationObject animationObject4 = AnimationSystem.Instance.PlayObject(2, 0, 96);
				animationObject4.Alpha = 0f;
				message_work.op_work[1] = 0;
				message_work.op_work[0] = 1;
				break;
			}
			case 1:
				FadeInObj(message_work, 96);
				break;
			}
			break;
		case 11:
			Exit(message_work);
			break;
		case 12:
			switch (message_work.op_work[0])
			{
			default:
				return;
			case 0:
			{
				AnimationObject animationObject3 = AnimationSystem.Instance.PlayObject(2, 0, 97);
				animationObject3.Alpha = 0f;
				message_work.op_work[1] = 0;
				message_work.op_work[0] = 1;
				break;
			}
			case 1:
				break;
			}
			FadeInObj(message_work, 97);
			break;
		case 13:
		{
			for (uint num = 0u; num < 6; num++)
			{
				AnimationObject animationObject = AnimationSystem.Instance.FindObject(2, 0, (int)(93 + num));
				if (animationObject != null)
				{
					animationObject.Stop(true);
				}
			}
			Exit(message_work);
			break;
		}
		}
	}

	private static void Op2DemoProc(MessageWork message_work)
	{
		GlobalWork global_work_ = GSStatic.global_work_;
		switch (message_work.op_workno)
		{
		case 1:
			if (message_work.op_para == 0)
			{
				int num2 = 0;
				AnimationObject animationObject2 = AnimationSystem.Instance.PlayObject(0, 1, 20);
				animationObject2.transform.localPosition = Vector3.forward * (-30f - (float)num2++);
				animationObject2 = AnimationSystem.Instance.PlayObject(0, 1, 27);
				animationObject2.transform.localPosition = Vector3.forward * (-30f - (float)num2++);
				animationObject2 = AnimationSystem.Instance.PlayObject(0, 1, 26);
				animationObject2.transform.localPosition = Vector3.forward * (-30f - (float)num2++);
				animationObject2 = AnimationSystem.Instance.PlayObject(0, 1, 25);
				animationObject2.transform.localPosition = Vector3.forward * (-30f - (float)num2++);
				bgCtrl.instance.ChangeSubSpriteEnable(false);
				Exit(message_work);
			}
			break;
		case 2:
			switch (message_work.op_para)
			{
			default:
				return;
			case 0:
				if (global_work_.scenario == 21)
				{
					bgCtrl.instance.SetSprite(bgCtrl.instance.bg_no_old);
				}
				message_work.op_para = 1;
				break;
			case 1:
				break;
			}
			if (global_work_.scenario != 7)
			{
			}
			Exit(message_work);
			break;
		case 3:
			switch (message_work.op_para)
			{
			case 0:
				GSStatic.bg_work_.Bg256_scroll2_x = 0;
				GSStatic.bg_work_.Bg256_scroll2_y = 240;
				bgCtrl.instance.ChangeSubSpriteEnable(true);
				bgCtrl.instance.ChangeSubSpritePosition(new Vector3(0f, -1080f, -20f));
				message_work.op_para = 10;
				break;
			case 1:
				message_work.op_para = 20;
				break;
			case 2:
				message_work.all_work[0] = (message_work.all_work[1] = (message_work.all_work[2] = 0));
				message_work.op_para = 3;
				break;
			case 3:
				GSStatic.bg_work_.Bg256_scroll2_y -= 4;
				if (GSStatic.bg_work_.Bg256_scroll2_y <= 0)
				{
					GSStatic.bg_work_.Bg256_scroll2_y = 0;
					MoveNextScript(message_work);
				}
				bgCtrl.instance.ChangeSubSpritePosition(new Vector3(0f, (float)(-GSStatic.bg_work_.Bg256_scroll2_y) * 4.5f, -20f));
				break;
			case 10:
				message_work.op_para = 1;
				break;
			case 20:
				message_work.op_para = 2;
				break;
			}
			break;
		case 4:
			if (message_work.op_para == 0)
			{
				bgCtrl.instance.DetentionBgFade(0f);
			}
			else
			{
				bgCtrl.instance.DetentionBgFade(1f);
			}
			Exit(message_work);
			break;
		case 5:
			Exit(message_work);
			break;
		case 6:
			switch (message_work.op_para)
			{
			case 0:
				message_work.all_work[0] = (message_work.all_work[1] = (message_work.all_work[2] = 0));
				message_work.op_para = 1;
				break;
			case 1:
				GSStatic.bg_work_.Bg256_scroll2_y += 4;
				if (GSStatic.bg_work_.Bg256_scroll2_y >= 240)
				{
					GSStatic.bg_work_.Bg256_scroll2_y = 240;
					message_work.all_work[0] = 0;
					message_work.all_work[1] = 0;
					message_work.op_para = 2;
				}
				bgCtrl.instance.ChangeSubSpritePosition(new Vector3(0f, (float)(-GSStatic.bg_work_.Bg256_scroll2_y) * 4.5f, -20f));
				break;
			case 2:
				if (++message_work.all_work[0] <= 40)
				{
					return;
				}
				message_work.all_work[0] = 0;
				AnimationSystem.Instance.StopObject(0, 1, 20);
				AnimationSystem.Instance.PlayObject(0, 1, 21);
				message_work.op_para = 3;
				break;
			case 3:
				if (++message_work.all_work[0] <= 139)
				{
					if (message_work.all_work[0] == 30 || message_work.all_work[0] == 37 || message_work.all_work[0] == 44 || message_work.all_work[0] == 51 || message_work.all_work[0] == 58 || message_work.all_work[0] == 65 || message_work.all_work[0] == 72)
					{
						soundCtrl.instance.PlaySE(363);
					}
					if (message_work.all_work[0] == 92)
					{
						soundCtrl.instance.PlaySE(364);
					}
					return;
				}
				AnimationSystem.Instance.StopObject(0, 1, 21);
				AnimationSystem.Instance.PlayObject(0, 1, 28);
				AnimationSystem.Instance.PlayObject(0, 1, 20);
				message_work.all_work[0] = 0;
				message_work.op_para = 4;
				break;
			case 4:
			{
				if (++message_work.all_work[0] <= 45)
				{
					return;
				}
				AnimationSystem.Instance.StopObject(0, 1, 20);
				AnimationSystem.Instance.PlayObject(0, 1, 22);
				AnimationObject animationObject11 = AnimationSystem.Instance.FindObject(1, 0, 27);
				animationObject11.SetCentor(new Vector3(347f, -336f, -30f));
				message_work.all_work[0] = 0;
				soundCtrl.instance.PlaySE(382);
				message_work.op_para = 5;
				break;
			}
			case 5:
				if (++message_work.all_work[0] >= 40)
				{
					message_work.all_work[0] = 0;
					message_work.op_para = 6;
				}
				else
				{
					AnimationObject animationObject15 = AnimationSystem.Instance.FindObject(1, 0, 27);
					animationObject15.transform.localEulerAngles -= Vector3.forward * 7.875f;
				}
				break;
			case 6:
				if (++message_work.all_work[0] >= 10)
				{
					message_work.all_work[0] = 0;
					message_work.op_para = 7;
					soundCtrl.instance.PlaySE(383);
				}
				break;
			case 7:
				if (++message_work.all_work[0] >= 30)
				{
					message_work.all_work[0] = 0;
					message_work.op_para = 8;
				}
				else
				{
					AnimationObject animationObject10 = AnimationSystem.Instance.FindObject(1, 0, 27);
					animationObject10.transform.localEulerAngles += Vector3.forward * 7.5f;
				}
				break;
			case 8:
				if (++message_work.all_work[0] >= 10)
				{
					message_work.all_work[0] = 0;
					message_work.op_para = 9;
					soundCtrl.instance.PlaySE(384);
				}
				break;
			case 9:
				if (++message_work.all_work[0] >= 20)
				{
					message_work.all_work[0] = 0;
					message_work.op_para = 10;
				}
				else
				{
					AnimationObject animationObject14 = AnimationSystem.Instance.FindObject(1, 0, 27);
					animationObject14.transform.localEulerAngles -= Vector3.forward * 6.25f;
				}
				break;
			case 10:
				if (++message_work.all_work[0] >= 10)
				{
					message_work.all_work[0] = 0;
					message_work.op_para = 11;
					soundCtrl.instance.PlaySE(385);
				}
				break;
			case 11:
				if (++message_work.all_work[0] >= 50)
				{
					message_work.op_work[0] = 0;
					message_work.all_work[0] = 0;
					AnimationObject animationObject12 = AnimationSystem.Instance.FindObject(1, 0, 27);
					animationObject12.transform.localEulerAngles = Vector3.zero;
					Vector3 centorPos = AnimationSystem.Instance.FindObject(1, 0, 25).GetCentorPos();
					animationObject12 = AnimationSystem.Instance.FindObject(1, 0, 26);
					animationObject12.SetCentor(centorPos);
					soundCtrl.instance.PlaySE(364);
					fadeCtrl.instance.play(fadeCtrl.Status.WFADE_IN, 1u, 8u);
					message_work.op_para = 12;
				}
				else
				{
					AnimationObject animationObject13 = AnimationSystem.Instance.FindObject(1, 0, 27);
					animationObject13.transform.localEulerAngles += Vector3.forward * 11.5f;
				}
				break;
			case 12:
				if (++message_work.all_work[0] >= 120)
				{
					message_work.all_work[0] = 0;
					message_work.op_para = 13;
				}
				else if (message_work.all_work[0] > 60)
				{
					AnimationObject animationObject8 = AnimationSystem.Instance.FindObject(1, 0, 26);
					animationObject8.transform.localEulerAngles -= Vector3.forward * 1.4f;
				}
				break;
			case 13:
				if (++message_work.all_work[0] < 40)
				{
					return;
				}
				message_work.all_work[0] = 0;
				message_work.op_para = 14;
				AnimationSystem.Instance.FindObject(1, 0, 26).transform.localEulerAngles = Vector3.back * 90f;
				break;
			case 14:
			{
				for (ushort num = 0; num < 3; num++)
				{
				}
				soundCtrl.instance.PlaySE(388);
				message_work.op_para = 15;
				ScreenCtrl.instance.quake_power = 15u;
				ScreenCtrl.instance.quake_timer = 15;
				ScreenCtrl.instance.Quake();
				break;
			}
			case 15:
				if (++message_work.all_work[0] < 15)
				{
					uint num7 = GSUtility.Rnd() & 0xFu;
					if (num7 >= 8)
					{
					}
					num7 = GSUtility.Rnd() & 0xFu;
					if (num7 < 8)
					{
					}
				}
				else
				{
					message_work.all_work[0] = 0;
					AnimationSystem.Instance.StopObject(0, 1, 22);
					AnimationSystem.Instance.PlayObject(0, 1, 23);
					message_work.op_para = 16;
				}
				break;
			case 16:
				if (++message_work.all_work[0] < 121)
				{
					if (message_work.all_work[0] == 42 || message_work.all_work[0] == 52 || message_work.all_work[0] == 62 || message_work.all_work[0] == 70 || message_work.all_work[0] == 75 || message_work.all_work[0] == 80 || message_work.all_work[0] == 85 || message_work.all_work[0] == 90 || message_work.all_work[0] == 95 || message_work.all_work[0] == 100 || message_work.all_work[0] == 105 || message_work.all_work[0] == 109)
					{
						soundCtrl.instance.PlaySE(365);
					}
					return;
				}
				message_work.all_work[0] = 0;
				AnimationSystem.Instance.StopObject(0, 1, 23);
				AnimationSystem.Instance.PlayObject(0, 1, 24);
				message_work.all_work[1] = 0;
				message_work.op_para = 17;
				break;
			case 17:
				if (++message_work.all_work[0] < 86)
				{
					if (message_work.all_work[0] == 40)
					{
						soundCtrl.instance.PlaySE(389);
					}
					if (message_work.all_work[0] == 66)
					{
						soundCtrl.instance.PlaySE(390);
					}
					if (message_work.all_work[0] == 63)
					{
						ScreenCtrl.instance.quake_power = 15u;
						ScreenCtrl.instance.quake_timer = 10;
						ScreenCtrl.instance.Quake();
					}
					return;
				}
				AnimationSystem.Instance.StopObject(0, 1, 24);
				AnimationSystem.Instance.PlayObject(0, 1, 29);
				message_work.all_work[0] = 0;
				message_work.all_work[0] = 0;
				message_work.op_para = 18;
				break;
			case 18:
				if (++message_work.all_work[0] < 15)
				{
					return;
				}
				message_work.all_work[0] = 0;
				bgCtrl.instance.SetSprite(73);
				bgCtrl.instance.ChangeSubSpritePosition(new Vector3(0f, 0f, -4f));
				message_work.op_para = 21;
				break;
			case 19:
			{
				GSStatic.bg_work_.Bg256_scroll2_x -= 3;
				if (GSStatic.bg_work_.Bg256_scroll2_x <= -400)
				{
					GSStatic.bg_work_.Bg256_scroll2_x = -400;
					message_work.op_para = 20;
				}
				float x = (float)GSStatic.bg_work_.Bg256_scroll2_x * 4.5f;
				Vector3 vector = Vector3.left * 3f * 4.5f;
				AnimationObject animationObject9 = AnimationSystem.Instance.FindObject(1, 0, 26);
				animationObject9.transform.localPosition += vector;
				animationObject9 = AnimationSystem.Instance.FindObject(1, 0, 28);
				animationObject9.transform.localPosition += vector;
				animationObject9 = AnimationSystem.Instance.FindObject(1, 0, 29);
				animationObject9.transform.localPosition += vector;
				animationObject9 = AnimationSystem.Instance.FindObject(1, 0, 25);
				animationObject9.transform.localPosition += vector;
				animationObject9 = AnimationSystem.Instance.FindObject(1, 0, 27);
				animationObject9.transform.localPosition += vector;
				bgCtrl.instance.ChangeSubSpritePosition(new Vector3(x, 0f, -4f));
				break;
			}
			case 20:
			{
				for (ushort num = 0; num < 3; num++)
				{
				}
				AnimationSystem.Instance.StopObject(0, 1, 28);
				AnimationSystem.Instance.StopObject(0, 1, 29);
				AnimationSystem.Instance.StopObject(0, 1, 25);
				AnimationSystem.Instance.StopObject(0, 1, 26);
				AnimationSystem.Instance.StopObject(0, 1, 27);
				MoveNextScript(message_work);
				break;
			}
			case 21:
				if (++message_work.all_work[0] < 15)
				{
					return;
				}
				message_work.all_work[0] = 0;
				soundCtrl.instance.PlaySE(360);
				message_work.op_para = 19;
				break;
			}
			if (message_work.op_para <= 14 && message_work.op_para > 3 && message_work.op_para >= 12 && message_work.op_para > 14)
			{
			}
			break;
		case 7:
			if (message_work.op_para == 0)
			{
				AnimationSystem.Instance.PlayObject(0, 1, 38);
				Exit(message_work);
			}
			break;
		case 8:
			switch (message_work.op_para)
			{
			case 0:
				fadeCtrl.instance.play(fadeCtrl.Status.WFADE_OUT, 0u, 0u);
				AnimationSystem.Instance.StopObject(0, 1, 38);
				message_work.all_work[0] = 0;
				message_work.op_para = 1;
				break;
			case 1:
				if (++message_work.all_work[0] > 4)
				{
					message_work.all_work[0] = 0;
					message_work.op_para = 2;
				}
				break;
			case 2:
				if (++message_work.all_work[0] > 16)
				{
					GS1_sce4_opening.instance.gs3_sc1_set_biru_obj();
					message_work.op_para = 3;
				}
				break;
			case 3:
				message_work.op_para = 4;
				break;
			case 4:
			{
				int num6 = 0;
				AnimationObject animationObject7 = AnimationSystem.Instance.PlayObject(0, 1, 32);
				animationObject7.transform.localPosition = Vector3.forward * (-30f - (float)num6++);
				animationObject7 = AnimationSystem.Instance.PlayObject(0, 1, 33);
				animationObject7.transform.localPosition = Vector3.forward * (-30f - (float)num6++);
				animationObject7 = AnimationSystem.Instance.PlayObject(0, 1, 30);
				animationObject7.transform.localPosition = Vector3.forward * (-30f - (float)num6++);
				animationObject7 = AnimationSystem.Instance.PlayObject(0, 1, 31);
				animationObject7.transform.localPosition = Vector3.forward * (-30f - (float)num6++);
				animationObject7 = AnimationSystem.Instance.PlayObject(0, 1, 35);
				animationObject7.transform.localPosition = Vector3.forward * (-30f - (float)num6++);
				animationObject7.transform.localPosition += Vector3.right * 80f;
				animationObject7 = AnimationSystem.Instance.PlayObject(0, 1, 36);
				animationObject7.transform.localPosition = Vector3.forward * (-30f - (float)num6++);
				animationObject7 = AnimationSystem.Instance.PlayObject(0, 1, 34);
				animationObject7.transform.localPosition = Vector3.forward * (-30f - (float)num6++);
				message_work.all_work[0] = 0;
				message_work.op_para = 5;
				break;
			}
			case 5:
				if (message_work.all_work[0] == 0)
				{
					fadeCtrl.instance.play(fadeCtrl.Status.WFADE_IN, 4u, 1u);
					message_work.op_para = 6;
				}
				else if (fadeCtrl.instance.status == fadeCtrl.Status.NO_FADE)
				{
					message_work.op_para = 6;
				}
				break;
			case 6:
			{
				for (ushort num5 = 0; num5 < 32; num5++)
				{
					for (ushort num = 0; num < 32; num++)
					{
					}
				}
				message_work.all_work[0] = (message_work.all_work[1] = (message_work.all_work[2] = 0));
				message_work.op_work[0] = 0;
				message_work.op_work[1] = 1;
				message_work.op_para = 10;
				break;
			}
			case 10:
			{
				message_work.all_work[0]++;
				float num3 = 0f;
				if (message_work.op_work[1] != 0)
				{
					if (++message_work.op_work[0] >= 6)
					{
						message_work.op_work[0] = 0;
					}
					message_work.op_work[1] = 0;
				}
				GS1_sce4_opening.instance.play_gs3_biru_scroll(message_work.all_work[0] >= 435);
				switch (message_work.all_work[1])
				{
				case 0:
				{
					Transform transform = AnimationSystem.Instance.FindObject(1, 0, 36).transform;
					if (++message_work.all_work[2] >= 10)
					{
						message_work.all_work[2] = 0;
						transform.localPosition += Vector3.up * 4.5f * 3f / 2f;
					}
					if (message_work.all_work[0] >= 150)
					{
						message_work.Item_zoom = 4096;
						message_work.all_work[2] = 0;
						message_work.all_work[1] = 1;
						message_work.op_work[2] = 0;
					}
					break;
				}
				case 1:
					if (++message_work.op_work[2] >= 285)
					{
						AnimationObject animationObject4 = AnimationSystem.Instance.FindObject(1, 0, 36);
						Vector3 localPosition = animationObject4.transform.localPosition;
						AnimationSystem.Instance.StopObject(1, 0, 36);
						animationObject4 = AnimationSystem.Instance.PlayObject(1, 0, 37);
						animationObject4.transform.localPosition = localPosition;
						message_work.op_work[2] = 0;
						message_work.all_work[1] = 2;
						message_work.op_work[5] = 0;
						message_work.op_work[6] = 5;
					}
					break;
				case 2:
				{
					AnimationObject animationObject3 = AnimationSystem.Instance.FindObject(1, 0, 37);
					if (animationObject3 == null)
					{
						break;
					}
					if (message_work.Item_zoom > 24)
					{
						if (message_work.Item_zoom > 1536)
						{
							message_work.Item_zoom -= 16;
						}
						else
						{
							message_work.Item_zoom -= 8;
						}
						if (message_work.Item_zoom < 24)
						{
							message_work.Item_zoom = 24;
						}
						float num4 = (float)message_work.Item_zoom / 4096f;
						animationObject3.transform.localScale = new Vector3(num4, num4, 1f);
					}
					else
					{
						animationObject3.transform.localScale = Vector3.one;
						AnimationSystem.Instance.StopObject(1, 0, 37);
						message_work.Item_zoom = 0;
						animationObject3 = null;
					}
					if (message_work.op_work[6] > 0)
					{
						num3 = (int)message_work.op_work[6];
						if (++message_work.op_work[5] >= 5)
						{
							message_work.op_work[5] = 0;
							message_work.op_work[6]--;
						}
					}
					else if (++message_work.op_work[5] >= 4)
					{
						message_work.op_work[5] = 0;
						num3 = -1f;
					}
					if (animationObject3 != null)
					{
						animationObject3.transform.localPosition += Vector3.up * num3 * 4.5f * 1.5f;
					}
					break;
				}
				}
				for (int i = 0; i < 2; i++)
				{
					num3 = 1f + 3f * (float)i;
					AnimationObject animationObject5 = AnimationSystem.Instance.FindObject(1, 0, 34 + i);
					if (!(animationObject5 != null))
					{
						continue;
					}
					Transform transform2 = animationObject5.transform;
					transform2.localPosition += Vector3.down * num3 * 4.5f;
					if (transform2.localPosition.y <= -750f)
					{
						if (++message_work.op_work[3 + i] >= 2 + 3 * i)
						{
							AnimationSystem.Instance.StopObject(1, 0, 34 + i);
							message_work.op_work[3 + i] = 0;
						}
						else
						{
							transform2.localPosition = new Vector3(transform2.localPosition.x, 560f, transform2.localPosition.z);
						}
					}
				}
				num3 = 1f;
				if (message_work.all_work[0] <= 435)
				{
					if (message_work.all_work[0] % 6 != 0)
					{
						break;
					}
				}
				else
				{
					num3 = 2f;
				}
				for (int j = 0; j < 4; j++)
				{
					AnimationObject animationObject6 = AnimationSystem.Instance.FindObject(1, 0, 30 + j);
					if (animationObject6 != null)
					{
						animationObject6.transform.localPosition += Vector3.down * num3 * 3f / 2f * 4.5f;
					}
				}
				break;
			}
			case 7:
			case 8:
			case 9:
				break;
			}
			break;
		case 9:
		{
			message_work.op_work[6] = 0;
			for (ushort num = 0; num < 8; num++)
			{
				AnimationObject animationObject = AnimationSystem.Instance.FindObject(1, 0, 30 + num);
				if (animationObject != null)
				{
					AnimationSystem.Instance.StopObject(1, 0, 30 + num);
				}
			}
			GS1_sce4_opening.instance.end_biru_scroll();
			Exit(message_work);
			break;
		}
		case 98:
			DemoProc_Special(message_work);
			break;
		case 0:
			Exit(message_work);
			break;
		}
	}

	private static OP03_WORK InitOP03_WORK()
	{
		op3_work = default(OP03_WORK);
		op3_work.Lspeed = new ushort[6];
		op3_work.Lframe = new ushort[6];
		op3_work.Rspeed = new ushort[6];
		op3_work.Rframe = new ushort[6];
		op3_work.work = new ushort[8];
		op3_work.init = 1;
		return op3_work;
	}

	private static void Op3DemoProc(MessageWork message_work)
	{
		if (op3_work.init == 0)
		{
			InitOP03_WORK();
		}
		switch (message_work.op_workno)
		{
		case 1:
			if (message_work.op_para == 0)
			{
				fadeCtrl.instance.play(fadeCtrl.Status.FADE_IN, 0u, 1u);
				op3_work.proc = 0;
				op3_work.timer = 0;
				op3_work.count = 80;
				op3_work.line = 14;
				op3_work.work[0] = 0;
				op3_work.work[1] = 0;
				op3_work.work[2] = 0;
				message_work.op_para = 1;
				bgCtrl.instance.ChangeSubSpritePosition(Vector3.left * 960f);
				bgCtrl.instance.SetBodyPosition(Vector3.up * 92f + Vector3.left * (((float)(op3_work.line * 24 - op3_work.work[0] * 3) - 48f) * 4.5f + 405f));
				break;
			}
			if (++op3_work.work[2] > op3_work.Lframe[op3_work.proc])
			{
				if (op3_work.proc < 5)
				{
					op3_work.proc++;
				}
				op3_work.work[2] = 0;
				op3_work.work[1] = 0;
			}
			if ((op3_work.Lspeed[op3_work.proc] & 0x8000u) != 0)
			{
				op3_work.wait = (ushort)(op3_work.Lspeed[op3_work.proc] ^ 0xFFFF);
				op3_work.wait++;
				op3_work.speed = 1;
			}
			else
			{
				op3_work.speed = op3_work.Lspeed[op3_work.proc];
				op3_work.wait = 0;
			}
			if (++op3_work.work[1] >= op3_work.wait)
			{
				op3_work.work[1] = 0;
				op3_work.work[0] += op3_work.speed;
				op3_work.count -= (short)op3_work.speed;
				while (op3_work.work[0] >= 8)
				{
					op3_work.work[0] -= 8;
					if (op3_work.line < 0)
					{
						op3_work.line = 0;
						op3_work.count = 0;
						op3_work.Fade = 0;
						op3_work.proc = 0;
						op3_work.work[0] = 0;
						op3_work.work[1] = 0;
						op3_work.work[2] = 0;
						op3_work.work[3] = 0;
						message_work.op_workno = 2;
						message_work.op_para = 0;
						bgCtrl.instance.ChangeSubSpriteAlpha(0.5f);
					}
					else
					{
						op3_work.line--;
					}
				}
			}
			if (message_work.op_workno != 2)
			{
				bgCtrl.instance.SetBodyPosition(Vector3.up * 92f + Vector3.left * (((float)(op3_work.line * 24 - op3_work.work[0] * 3) - 48f) * 4.5f + 405f));
			}
			if (message_work.op_workno == 1 && op3_work.Fade > 0 && op3_work.timer++ >= op3_work.FadeinSpeed)
			{
				op3_work.timer = 0;
				op3_work.Fade--;
			}
			break;
		case 2:
			op3_work.count++;
			if (++op3_work.work[2] > op3_work.Rframe[op3_work.proc])
			{
				if (op3_work.proc < 5)
				{
					op3_work.proc++;
				}
				op3_work.work[2] = 0;
				op3_work.work[1] = 0;
			}
			if ((op3_work.Rspeed[op3_work.proc] & 0x8000u) != 0)
			{
				op3_work.wait = op3_work.Rspeed[op3_work.proc] ^ 0xFFFF;
				op3_work.wait++;
				op3_work.speed = 1;
			}
			else
			{
				op3_work.speed = op3_work.Rspeed[op3_work.proc];
				op3_work.wait = 0;
			}
			if (++op3_work.work[1] >= op3_work.wait)
			{
				op3_work.work[1] = 0;
				op3_work.work[0] += op3_work.speed;
				while (op3_work.work[0] >= 8)
				{
					op3_work.work[0] -= 8;
					if (op3_work.line > 1)
					{
						op3_work.work[0] = 0;
						message_work.op_workno = 3;
						message_work.op_para = 0;
					}
					else
					{
						op3_work.line++;
					}
				}
			}
			if (message_work.op_workno == 2)
			{
				bgCtrl.instance.ChangeSubSpritePosition(Vector3.left * (960f + (float)(op3_work.line * 16 + op3_work.work[0] * 2) * 4.5f) + Vector3.back * 20f);
			}
			goto case 3;
		case 3:
			op3_work.count++;
			if (op3_work.Fade >= 16 && message_work.op_workno == 3)
			{
				Exit(message_work);
			}
			if (op3_work.count > op3_work.CrossTime && op3_work.Fade < 16 && op3_work.work[3]++ > op3_work.FadeSpeed)
			{
				op3_work.work[3] = 0;
				op3_work.Fade++;
				bgCtrl.instance.ChangeSubSpriteAlpha((float)(16 - op3_work.Fade) / 32f);
			}
			break;
		case 10:
			op3_work.proc = 0;
			op3_work.timer = 0;
			op3_work.count = 0;
			op3_work.line = 8;
			op3_work.work[0] = 0;
			op3_work.work[1] = 0;
			op3_work.work[2] = 0;
			op3_work.Fade = 0;
			fadeCtrl.instance.play(fadeCtrl.Status.FADE_IN, 0u, 1u);
			bgCtrl.instance.DetentionBgFade(1f);
			bgCtrl.instance.ChangeSubSpritePosition(new Vector3(-960f, 0f, -20f));
			bgCtrl.instance.ChangeSubSpriteAlpha(1f);
			bgCtrl.instance.SetBodyPosition(new Vector3(0f, bgCtrl.instance.body.transform.localPosition.y, 0f));
			message_work.op_workno = 11;
			break;
		case 11:
			if (++op3_work.work[2] > op3_work.Lframe[op3_work.proc])
			{
				if (op3_work.proc < 5)
				{
					op3_work.proc++;
				}
				op3_work.work[2] = 0;
				op3_work.work[1] = 0;
			}
			if ((op3_work.Lspeed[op3_work.proc] & 0x8000u) != 0)
			{
				op3_work.wait = (short)(op3_work.Lspeed[op3_work.proc] ^ 0xFFFF);
				op3_work.wait++;
				op3_work.speed = 1;
			}
			else
			{
				op3_work.speed = op3_work.Lspeed[op3_work.proc];
				op3_work.wait = 0;
			}
			if (++op3_work.work[1] >= op3_work.wait)
			{
				op3_work.work[1] = 0;
				op3_work.work[0] += op3_work.speed;
				while (op3_work.work[0] >= 8)
				{
					op3_work.work[0] -= 8;
					if (op3_work.line < 0)
					{
						op3_work.proc = 0;
						op3_work.timer = 0;
						op3_work.count = 0;
						op3_work.line = 4;
						op3_work.work[0] = 0;
						op3_work.work[1] = 0;
						op3_work.work[2] = 0;
						op3_work.Fade = 0;
						message_work.op_workno = 12;
					}
					else
					{
						if (op3_work.line > 2)
						{
						}
						op3_work.line--;
					}
				}
				bgCtrl.instance.SetBodyPosition(new Vector3(0f, bgCtrl.instance.body.transform.localPosition.y + -6.75f, 0f));
			}
			op3_work.count++;
			if (op3_work.count > op3_work.CrossTime && op3_work.Fade < 16 && op3_work.work[3]++ > op3_work.FadeSpeed)
			{
				op3_work.work[3] = 0;
				op3_work.Fade++;
				bgCtrl.instance.ChangeSubSpriteAlpha((float)(16 - op3_work.Fade) / 32f);
			}
			break;
		case 12:
			if (++op3_work.work[2] > op3_work.Rframe[op3_work.proc])
			{
				if (op3_work.proc < 5)
				{
					op3_work.proc++;
				}
				op3_work.work[2] = 0;
				op3_work.work[1] = 0;
			}
			if ((op3_work.Rspeed[op3_work.proc] & 0x8000u) != 0)
			{
				op3_work.wait = (short)(op3_work.Rspeed[op3_work.proc] ^ 0xFFFF);
				op3_work.wait++;
				op3_work.speed = 1;
			}
			else
			{
				op3_work.speed = op3_work.Rspeed[op3_work.proc];
				op3_work.wait = 0;
			}
			if (++op3_work.work[1] >= op3_work.wait)
			{
				bgCtrl.instance.ChangeSubSpritePosition(new Vector3(bgCtrl.instance.SubSpritePosition().x, bgCtrl.instance.SubSpritePosition().y + 9f, 0f));
				op3_work.work[1] = 0;
				op3_work.work[0] += op3_work.speed;
				while (op3_work.work[0] >= 8)
				{
					op3_work.work[0] -= 8;
					if (op3_work.line < 0)
					{
						message_work.op_workno = 13;
						op3_work.work[0] = 0;
					}
					else
					{
						op3_work.line--;
					}
				}
			}
			goto case 13;
		case 13:
			op3_work.count++;
			if (op3_work.Fade == 16 && message_work.op_workno == 13)
			{
				MoveNextScript(message_work);
			}
			if (op3_work.count > op3_work.CrossTime2 && op3_work.Fade < 16 && op3_work.work[3]++ > op3_work.FadeSpeed2)
			{
				op3_work.work[3] = 0;
				op3_work.Fade++;
				bgCtrl.instance.ChangeSubSpriteAlpha((float)(16 - op3_work.Fade) / 32f);
			}
			break;
		case 14:
			bgCtrl.instance.setNegaPosi(true, false);
			Exit(message_work);
			break;
		case 16:
			switch (message_work.op_para)
			{
			case 0:
				op3_work.FadeSpeed = message_work.all_work[0];
				op3_work.timer = 0;
				op3_work.Fade = 0;
				message_work.op_para = 1;
				break;
			case 1:
				if (++op3_work.timer >= op3_work.FadeSpeed)
				{
					op3_work.timer = 0;
					op3_work.Fade++;
					bgCtrl.instance.setAlpha((float)(16 - op3_work.Fade) / 32f);
					bgCtrl.instance.ChangeSubSpriteAlpha((float)(int)op3_work.Fade / 16f);
				}
				if (op3_work.Fade == 16)
				{
					MoveNextScript(message_work);
				}
				break;
			}
			break;
		case 17:
		{
			bgCtrl.instance.setNegaPosi(false, false);
			AnimationSystem.Instance.PlayObject(1, 0, 39);
			AnimationObject animationObject = AnimationSystem.Instance.PlayObject(1, 0, 40);
			animationObject.PlayMonochrome(0, 32, 0, true);
			AnimationSystem.Instance.PlayObject(1, 0, 43);
			op3_work.Lspeed[0] = message_work.all_work[0];
			op3_work.Rspeed[0] = message_work.all_work[1];
			op3_work.count = 192;
			op3_work.work[0] = (op3_work.work[1] = 0);
			op3_work.work[2] = 0;
			op3_work.line = 0;
			Vector3 localPosition2 = Vector3.right * op3_work.count * 1.5f * 4.5f;
			localPosition2.z = -30f;
			animationObject = AnimationSystem.Instance.FindObject(1, 0, 39);
			animationObject.transform.localPosition = localPosition2;
			animationObject = AnimationSystem.Instance.FindObject(1, 0, 40);
			localPosition2.z -= 1f;
			animationObject.transform.localPosition = localPosition2;
			animationObject = AnimationSystem.Instance.FindObject(1, 0, 43);
			localPosition2.z -= 1f;
			animationObject.transform.localPosition = localPosition2;
			message_work.op_workno = 18;
			break;
		}
		case 26:
			op3_work.work[2] = message_work.all_work[0];
			if (op3_work.work[2] == 1)
			{
				op3_work.FadeSpeed = message_work.all_work[1];
				op3_work.Fade = 0;
				fadeCtrl.instance.play(2, true);
			}
			message_work.op_workno = 18;
			goto case 18;
		case 18:
		case 20:
			if ((op3_work.Lspeed[0] & 0x8000u) != 0)
			{
				op3_work.wait = op3_work.Lspeed[0] ^ 0xFFFF;
				op3_work.wait++;
				op3_work.speed = 1;
			}
			else
			{
				op3_work.speed = op3_work.Lspeed[0];
				op3_work.wait = 0;
			}
			if (++op3_work.work[0] >= op3_work.wait)
			{
				op3_work.work[0] = 0;
				op3_work.count -= op3_work.speed;
				if (op3_work.count < 72)
				{
					op3_work.count = 72;
				}
				AnimationObject animationObject = AnimationSystem.Instance.FindObject(1, 0, 39);
				if (animationObject != null)
				{
					animationObject.transform.localPosition = new Vector3((float)(op3_work.count - 128) * 1.5f * 4.5f, animationObject.transform.localPosition.y, animationObject.transform.localPosition.z);
				}
				animationObject = AnimationSystem.Instance.FindObject(1, 0, 40);
				if (animationObject != null)
				{
					animationObject.transform.localPosition = new Vector3((float)(op3_work.count - 128) * 1.5f * 4.5f, animationObject.transform.localPosition.y, animationObject.transform.localPosition.z);
				}
				animationObject = AnimationSystem.Instance.FindObject(1, 0, 43);
				if (animationObject != null)
				{
					animationObject.transform.localPosition = new Vector3((float)(op3_work.count - 128) * 1.5f * 4.5f, animationObject.transform.localPosition.y, animationObject.transform.localPosition.z);
				}
				if (op3_work.count == 72)
				{
					if (message_work.op_workno == 18)
					{
						Exit(message_work);
					}
					else
					{
						MoveNextScript(message_work);
					}
				}
				else if (op3_work.count == 74)
				{
					op3_work.Lspeed[0]--;
					op3_work.Rspeed[0]--;
				}
				else if (op3_work.count == 78)
				{
					op3_work.Lspeed[0]--;
					op3_work.Rspeed[0]--;
				}
				else if (op3_work.count == 84)
				{
					op3_work.Lspeed[0]--;
					op3_work.Rspeed[0]--;
				}
			}
			if ((op3_work.Rspeed[0] & 0x8000u) != 0)
			{
				op3_work.wait = op3_work.Rspeed[0] ^ 0xFFFF;
				op3_work.wait++;
				op3_work.speed = 1;
			}
			else
			{
				op3_work.speed = op3_work.Rspeed[0];
				op3_work.wait = 0;
			}
			if (++op3_work.work[1] >= op3_work.wait)
			{
				op3_work.work[1] = 0;
				op3_work.line += op3_work.speed;
				while (op3_work.line >= 8)
				{
					op3_work.line -= 8;
				}
			}
			bgCtrl.instance.ChangeSubSpritePosition(new Vector3(bgCtrl.instance.SubSpritePosition().x + 2.25f, bgCtrl.instance.SubSpritePosition().y, -20f));
			if (op3_work.work[2] == 1)
			{
				if (++op3_work.Fade >= op3_work.FadeSpeed)
				{
					op3_work.Fade = 0;
					op3_work.work[2] = 0;
					message_work.op_workno = 65;
				}
			}
			else if (op3_work.work[2] == 2 && ++op3_work.Fade >= op3_work.FadeSpeed)
			{
				op3_work.Fade = 0;
				op3_work.work[2] = 0;
			}
			break;
		case 19:
		{
			bgCtrl.instance.ChangeSubSpriteEnable(true);
			AnimationObject animationObject = AnimationSystem.Instance.FindObject(1, 0, 39);
			animationObject.gameObject.SetActive(true);
			animationObject = AnimationSystem.Instance.FindObject(1, 0, 40);
			animationObject.gameObject.SetActive(true);
			animationObject = AnimationSystem.Instance.FindObject(1, 0, 43);
			animationObject.gameObject.SetActive(true);
			op3_work.work[2] = message_work.all_work[0];
			if (message_work.all_work[1] == 0)
			{
				op3_work.FadeSpeed = 0;
			}
			else
			{
				op3_work.FadeSpeed = message_work.all_work[1];
			}
			op3_work.Fade = 0;
			message_work.op_workno = 20;
			break;
		}
		case 21:
			if (message_work.op_para == 0)
			{
				op3_work.timer = 0;
				op3_work.wait = message_work.all_work[0];
				op3_work.count = 0;
				message_work.op_para = 1;
				AnimationObject animationObject2 = AnimationSystem.Instance.FindObject(1, 0, 40);
				animationObject2.SetCentor(animationObject2.GetCentorPos() + Vector3.down * 20f);
				animationObject2.PlayMonochrome((ushort)op3_work.wait, 32, 0, false);
			}
			else if (message_work.op_para == 1)
			{
				if (++op3_work.timer >= op3_work.wait)
				{
					op3_work.timer = 0;
					if (++op3_work.count > 32)
					{
						bgCtrl.instance.DetentionBgFade(0f);
						message_work.op_para = 2;
						break;
					}
					float alpha = 1f - (float)op3_work.count / 32f;
					AnimationObject animationObject = AnimationSystem.Instance.FindObject(1, 0, 39);
					animationObject.Alpha = alpha;
					animationObject = AnimationSystem.Instance.FindObject(1, 0, 43);
					animationObject.Alpha = alpha;
					bgCtrl.instance.ChangeSubSpriteAlpha(alpha);
				}
			}
			else
			{
				message_work.all_work[0] = 0;
				MoveNextScript(message_work);
			}
			break;
		case 22:
			switch (message_work.op_para)
			{
			case 0:
				if (message_work.all_work[0] != ushort.MaxValue)
				{
					bgCtrl.instance.DetentionBgFade(0f);
				}
				AnimationSystem.Instance.StopObject(1, 0, 39);
				AnimationSystem.Instance.StopObject(1, 0, 43);
				message_work.op_para = 8;
				break;
			case 8:
				message_work.op_para = 9;
				break;
			case 9:
				bgCtrl.instance.setNegaPosi(false);
				bgCtrl.instance.SetSprite(92);
				bgCtrl.instance.SetBodyPosition(Vector3.down * 1080f);
				bgCtrl.instance.ChangeSubSpritePosition(new Vector3(0f, 1080f, -20f));
				if (message_work.all_work[0] != ushort.MaxValue)
				{
					bgCtrl.instance.DetentionBgFade(0f);
				}
				else
				{
					bgCtrl.instance.DetentionBgFade(1f);
				}
				message_work.op_para = 10;
				break;
			case 10:
				if (message_work.all_work[0] != ushort.MaxValue)
				{
				}
				message_work.op_para = 1;
				break;
			case 1:
				if (message_work.all_work[0] != ushort.MaxValue)
				{
					message_work.Item_zoom = 4096;
				}
				if (message_work.all_work[0] != ushort.MaxValue)
				{
					op3_work.Fade = 0;
					op3_work.count = 0;
					op3_work.work[3] = 0;
					op3_work.Lspeed[0] = message_work.all_work[0];
					op3_work.CrossTime2 = message_work.all_work[1];
					op3_work.FadeSpeed2 = message_work.all_work[2];
					message_work.op_para = 2;
				}
				else
				{
					MoveNextScript(message_work);
				}
				break;
			case 2:
				if (++op3_work.count >= op3_work.CrossTime2 && op3_work.Fade < 16 && op3_work.work[3]++ > op3_work.FadeSpeed2)
				{
					op3_work.work[3] = 0;
					op3_work.Fade += 2;
					AnimationObject animationObject = AnimationSystem.Instance.FindObject(1, 0, 40);
					animationObject.Alpha = 16f - (float)(int)op3_work.Fade / 16f;
					bgCtrl.instance.DetentionBgFade((float)(int)op3_work.Fade / 10f);
					if (op3_work.Fade == 16)
					{
						animationObject = AnimationSystem.Instance.FindObject(1, 0, 40);
						if (animationObject != null)
						{
							animationObject.transform.localScale = Vector3.one;
						}
						AnimationSystem.Instance.StopObject(1, 0, 40);
						MoveNextScript(message_work);
					}
				}
				if (message_work.Item_zoom < 8064)
				{
					message_work.Item_zoom += (short)op3_work.Lspeed[0];
					if (message_work.Item_zoom > 8064)
					{
						message_work.Item_zoom = 8064;
					}
					float num = (float)message_work.Item_zoom / 4096f;
					AnimationObject animationObject = AnimationSystem.Instance.FindObject(1, 0, 40);
					if (animationObject != null)
					{
						animationObject.transform.localScale = Vector3.one * num;
					}
					if (message_work.Item_zoom == 8064)
					{
						animationObject.transform.localScale = Vector3.one;
						AnimationSystem.Instance.StopObject(1, 0, 40);
					}
				}
				break;
			}
			break;
		case 23:
			switch (message_work.op_para)
			{
			case 0:
			{
				bgCtrl.instance.ChangeSubSpritePosition(new Vector3(0f, 1080f, -20f));
				AnimationObject animationObject = AnimationSystem.Instance.PlayObject(1, 0, 41);
				animationObject.transform.localPosition = new Vector3(0f, -608f, -20f);
				animationObject = AnimationSystem.Instance.PlayObject(1, 0, 42);
				animationObject.transform.localPosition = new Vector3(0f, -864f, -20f);
				op3_work.Lspeed[0] = message_work.all_work[0];
				op3_work.count = 0;
				op3_work.timer = 0;
				op3_work.line = 1;
				op3_work.work[0] = 0;
				message_work.op_para = 1;
				break;
			}
			case 1:
			{
				if ((op3_work.Lspeed[0] & 0x8000u) != 0)
				{
					op3_work.wait = op3_work.Lspeed[0] ^ 0xFFFF;
					op3_work.wait++;
					op3_work.speed = 1;
				}
				else
				{
					op3_work.speed = op3_work.Lspeed[0];
					op3_work.wait = 0;
				}
				if (++op3_work.count >= op3_work.wait)
				{
					op3_work.count = 0;
					op3_work.work[0] += op3_work.speed;
					while (op3_work.work[0] >= 8)
					{
						op3_work.work[0] -= 8;
						op3_work.line++;
						if (op3_work.line > 24)
						{
							op3_work.work[0] = 0;
							MoveNextScript(message_work);
						}
					}
					AnimationObject animationObject = AnimationSystem.Instance.FindObject(1, 0, 41);
					animationObject.transform.localPosition += Vector3.up * (int)op3_work.speed * 4.5f;
					if (animationObject.transform.localPosition.y > 256f)
					{
						animationObject.transform.localPosition = new Vector3(0f, 256f, -20f);
					}
					animationObject = AnimationSystem.Instance.FindObject(1, 0, 42);
					animationObject.transform.localPosition += Vector3.up * (int)op3_work.speed * 4.5f;
					if (animationObject.transform.localPosition.y > -256f)
					{
						animationObject.transform.localPosition = new Vector3(0f, -256f, -20f);
					}
				}
				Vector3 bodyPosition = Vector3.down * 1080f * (200 - (op3_work.line * 8 + op3_work.work[0])) / 192f;
				bgCtrl.instance.SetBodyPosition(bodyPosition);
				break;
			}
			}
			break;
		case 24:
			if (message_work.all_work[0] != ushort.MaxValue)
			{
			}
			bgCtrl.instance.ChangeSubSpriteEnable(false);
			AnimationSystem.Instance.StopObject(1, 0, 39, true);
			AnimationSystem.Instance.StopObject(1, 0, 40, true);
			AnimationSystem.Instance.StopObject(1, 0, 41, true);
			AnimationSystem.Instance.StopObject(1, 0, 42, true);
			AnimationSystem.Instance.StopObject(1, 0, 43, true);
			MoveNextScript(message_work);
			break;
		case 27:
			Exit(message_work);
			break;
		case 28:
			Exit(message_work);
			break;
		case 29:
			bgCtrl.instance.setColor(Color.black);
			Exit(message_work);
			break;
		case 30:
			if (message_work.op_para == 0)
			{
				bgCtrl.instance.body.SetActive(false);
				bgCtrl.instance.parts_parent.SetActive(false);
				AnimationSystem.Instance.CharacterAnimationObject.gameObject.SetActive(false);
			}
			else
			{
				bgCtrl.instance.body.SetActive(true);
				bgCtrl.instance.parts_parent.SetActive(true);
				AnimationSystem.Instance.CharacterAnimationObject.gameObject.SetActive(true);
			}
			Exit(message_work);
			break;
		case 31:
			if (message_work.op_para == 0)
			{
				message_work.all_work[0] = 0;
				message_work.op_para = 1;
				bgCtrl.instance.darkness_scroll();
				break;
			}
			message_work.all_work[0] ^= 1;
			if (message_work.all_work[0] != 0 && !bgCtrl.instance.is_scrolling_court)
			{
				MoveNextScript(message_work);
			}
			break;
		case 89:
		{
			if (message_work.op_para == 0)
			{
				bgCtrl.instance.SetSubSprite("bg309");
				bgCtrl.instance.ChangeSubSpritePosition(new Vector3(-483f, -160f, 3f));
				message_work.op_para = 1;
				break;
			}
			op3_work.Lspeed[0] = message_work.all_work[0];
			op3_work.Rspeed[0] = message_work.all_work[1];
			op3_work.count = 192;
			op3_work.work[0] = (op3_work.work[1] = 0);
			op3_work.line = 0;
			AnimationSystem.Instance.PlayObject(1, 0, 39);
			AnimationObject animationObject = AnimationSystem.Instance.PlayObject(1, 0, 40);
			animationObject.PlayMonochrome(0, 32, 0, true);
			AnimationSystem.Instance.PlayObject(1, 0, 43);
			Vector3 localPosition = Vector3.right * op3_work.count * 1.5f * 4.5f;
			localPosition.z = -30f;
			animationObject = AnimationSystem.Instance.FindObject(1, 0, 39);
			animationObject.transform.localPosition = localPosition;
			localPosition.z -= 1f;
			animationObject = AnimationSystem.Instance.FindObject(1, 0, 40);
			animationObject.transform.localPosition = localPosition;
			localPosition.z -= 1f;
			animationObject = AnimationSystem.Instance.FindObject(1, 0, 43);
			animationObject.transform.localPosition = localPosition;
			MoveNextScript(message_work);
			break;
		}
		case 90:
			op3_work.Lspeed[message_work.all_work[0]] = message_work.all_work[1];
			op3_work.Lframe[message_work.all_work[0]] = message_work.all_work[2];
			Exit(message_work);
			break;
		case 91:
			op3_work.Rspeed[message_work.all_work[0]] = message_work.all_work[1];
			op3_work.Rframe[message_work.all_work[0]] = message_work.all_work[2];
			Exit(message_work);
			break;
		case 92:
			op3_work.CrossTime = message_work.all_work[0];
			op3_work.FadeSpeed = message_work.all_work[1];
			Exit(message_work);
			break;
		case 95:
			op3_work.CrossTime2 = message_work.all_work[0];
			op3_work.FadeSpeed2 = message_work.all_work[1];
			Exit(message_work);
			break;
		case 96:
			op3_work.FadeinSpeed = message_work.all_work[0];
			op3_work.Fade = 16;
			op3_work.timer = 0;
			Exit(message_work);
			break;
		case 93:
			bgCtrl.instance.SetBodyPosition(Vector3.up * 92f);
			bgCtrl.instance.ChangeSubSpritePosition(new Vector3(-960f, 0f, 0f));
			message_work.op_workno = 94;
			break;
		case 94:
			message_work.op_workno = 60;
			break;
		case 60:
			message_work.op_workno = 61;
			break;
		case 61:
			MoveNextScript(message_work);
			break;
		case 97:
			if (message_work.op_para == 0)
			{
				bgCtrl.instance.SetSubSprite("bg309");
				bgCtrl.instance.ChangeSubSpritePosition(new Vector3(-483f, 220f, 3f));
				message_work.op_para = 1;
			}
			else
			{
				MoveNextScript(message_work);
			}
			break;
		case 65:
		{
			AnimationObject animationObject = AnimationSystem.Instance.FindObject(1, 0, 39);
			if (animationObject != null)
			{
				animationObject.gameObject.SetActive(false);
			}
			animationObject = AnimationSystem.Instance.FindObject(1, 0, 40);
			if (animationObject != null)
			{
				animationObject.gameObject.SetActive(false);
			}
			animationObject = AnimationSystem.Instance.FindObject(1, 0, 41);
			if (animationObject != null)
			{
				animationObject.gameObject.SetActive(false);
			}
			animationObject = AnimationSystem.Instance.FindObject(1, 0, 42);
			if (animationObject != null)
			{
				animationObject.gameObject.SetActive(false);
			}
			animationObject = AnimationSystem.Instance.FindObject(1, 0, 43);
			if (animationObject != null)
			{
				animationObject.gameObject.SetActive(false);
			}
			bgCtrl.instance.ChangeSubSpriteEnable(false);
			MoveNextScript(message_work);
			break;
		}
		case 98:
			DemoProc_Special(message_work);
			break;
		case 0:
			Exit(message_work);
			break;
		}
	}

	private static void Op4DemoProc(MessageWork message_work)
	{
		switch (message_work.op_workno)
		{
		case 1:
			switch (message_work.op_para)
			{
			case 0:
				NoiseCtrl.instance.FILM_EffectInit();
				switch (NoiseCtrl.instance.NoiseSW)
				{
				case 0:
				case 3:
				case 4:
					spefCtrl.instance.Monochrome_set(23, 0, 31);
					message_work.op_para = 10;
					break;
				case 1:
				case 2:
					spefCtrl.instance.Monochrome_set(23, 0, 0);
					message_work.all_work[0] = (message_work.all_work[2] = 0);
					message_work.op_para = 3;
					return;
				}
				goto case 10;
			case 10:
				spefCtrl.instance.Monochrome_set(23, 0, 31);
				NoiseCtrl.instance.FILM_Effect(0);
				break;
			case 1:
			{
				for (ushort num2 = 0; num2 < 64; num2++)
				{
					NoiseCtrl.instance.noise_list_[num2].gameObject.SetActive(false);
				}
				message_work.op_para = 99;
				break;
			}
			case 2:
			{
				for (ushort num2 = 0; num2 < 3; num2++)
				{
					NoiseCtrl.instance.line_list_[num2].gameObject.SetActive(false);
				}
				spefCtrl.instance.Monochrome_set(16, 1, 0);
				NoiseCtrl.instance.NoiseDelete();
				Exit(message_work);
				break;
			}
			case 3:
				if (++message_work.all_work[2] >= message_work.all_work[1])
				{
					message_work.all_work[2] = 0;
					message_work.all_work[0]++;
					if (message_work.all_work[0] >= 31)
					{
						NoiseCtrl.instance.FILM_Effect(0);
						message_work.op_para = 10;
					}
				}
				break;
			}
			break;
		case 4:
			GSDemo_gs3_op4.DemoProcPCDataView(message_work);
			break;
		case 5:
		{
			byte op_para5 = message_work.op_para;
			if (op_para5 != 0)
			{
				if (op_para5 != 1 && op_para5 != 2)
				{
					goto IL_02e6;
				}
			}
			else
			{
				message_work.all_work[1] = 0;
				if (message_work.all_work[0] == 0)
				{
					message_work.op_para = 2;
				}
				else
				{
					message_work.op_para = 1;
				}
				fadeCtrl.instance.play(fadeCtrl.Status.FADE_IN, message_work.all_work[0], 1u);
			}
			if (fadeCtrl.instance.status == fadeCtrl.Status.NO_FADE)
			{
				message_work.op_workno = 1;
				message_work.op_para = 10;
			}
			goto IL_02e6;
		}
		case 6:
		{
			byte op_para3 = message_work.op_para;
			if (op_para3 != 0)
			{
				if (op_para3 != 1 && op_para3 != 2)
				{
					goto IL_0384;
				}
			}
			else
			{
				message_work.all_work[1] = 0;
				if (message_work.all_work[0] == 0)
				{
					message_work.op_para = 2;
				}
				else
				{
					message_work.op_para = 1;
				}
				fadeCtrl.instance.play(fadeCtrl.Status.FADE_OUT, message_work.all_work[0], 1u);
			}
			if (fadeCtrl.instance.status == fadeCtrl.Status.NO_FADE)
			{
				message_work.op_workno = 1;
				message_work.op_para = 10;
			}
			goto IL_0384;
		}
		case 7:
			switch (message_work.op_para)
			{
			case 0:
				if ((long)bgCtrl.instance.bg_no_reserve == 109)
				{
					bgCtrl.instance.SetSubSprite(109);
					bgCtrl.instance.ChangeSubSpriteAlpha(0f);
					bgCtrl.instance.ChangeSubSpriteEnable(true);
				}
				else if ((long)bgCtrl.instance.bg_no_reserve == 108)
				{
					bgCtrl.instance.SetSubSprite(108);
					bgCtrl.instance.ChangeSubSpriteAlpha(0f);
					bgCtrl.instance.ChangeSubSpriteEnable(true);
				}
				if (bgCtrl.instance.bg_no_reserve != 65535)
				{
					bgCtrl.instance.bg_no_reserve = 65535;
				}
				message_work.op_para = 50;
				break;
			case 50:
				message_work.all_work[0] = 0;
				message_work.all_work[2] = 0;
				message_work.op_para = 1;
				break;
			case 1:
				if (++message_work.all_work[0] > message_work.all_work[1])
				{
					message_work.all_work[0] = 0;
					message_work.all_work[2]++;
					bgCtrl.instance.ChangeSubSpriteAlpha((float)(int)message_work.all_work[2] / 16f);
				}
				if (message_work.all_work[2] == 16)
				{
					message_work.op_para = 2;
				}
				break;
			case 2:
				message_work.op_para = 3;
				break;
			case 3:
				message_work.op_workno = 1;
				message_work.op_para = 10;
				break;
			}
			NoiseCtrl.instance.FILM_Effect(0);
			break;
		case 8:
			switch (message_work.op_para)
			{
			case 0:
			{
				AnimationObject animationObject3 = AnimationSystem.Instance.PlayObject(1, 0, 48);
				animationObject3.transform.localPosition = new Vector3(0f, -440f);
				animationObject3.Alpha = 0.8f;
				animationObject3.PlayMonochrome(0, 32, 6, true);
				bgCtrl.instance.SetSubSprite("bg409");
				bgCtrl.instance.ChangeSubSpritePosition(new Vector3(0f, 317.5f, -20f));
				message_work.all_work[0] = (message_work.all_work[1] = (message_work.all_work[2] = 0));
				message_work.op_para = 101;
				break;
			}
			case 101:
				if (++message_work.all_work[0] >= 4)
				{
					message_work.all_work[0] = 0;
					message_work.op_para = 1;
				}
				break;
			case 1:
				if (++message_work.all_work[0] >= 94)
				{
					message_work.all_work[0] = 0;
					AnimationSystem.Instance.ResetSpefObj();
					AnimationSystem.Instance.StopObject(1, 0, 48);
					AnimationObject animationObject3 = AnimationSystem.Instance.PlayObject(1, 0, 49);
					animationObject3.Alpha = 0.8f;
					animationObject3.PlayMonochrome(0, 32, 6, true);
					message_work.op_para = 2;
				}
				else if (message_work.all_work[0] > 32)
				{
					AnimationObject animationObject3 = AnimationSystem.Instance.FindObject(1, 0, 48);
					if (animationObject3 != null)
					{
						animationObject3.transform.localPosition += Vector3.down * 2f * 4.5f;
					}
				}
				break;
			case 2:
				if (++message_work.all_work[0] >= 60)
				{
					AnimationSystem.Instance.ResetSpefObj();
					AnimationSystem.Instance.StopObject(1, 0, 49);
					message_work.op_workno = 1;
					message_work.op_para = 10;
				}
				else if (message_work.all_work[2] < 24)
				{
					AnimationSystem.Instance.ResetSpefObj();
					AnimationSystem.Instance.StopObject(1, 0, 49);
				}
				break;
			}
			if (message_work.all_work[1] != 0 || message_work.all_work[2] != 0)
			{
				float num = 8f - (float)(int)message_work.all_work[1];
				num -= (float)(int)message_work.all_work[2] * 8f;
				num *= 1.3033334f;
				bgCtrl.instance.SetBodyPosition(Vector3.down * (num * 4.5f));
			}
			if (message_work.all_work[2] < 24)
			{
				message_work.all_work[1] += 2;
				while (message_work.all_work[1] >= 8)
				{
					message_work.all_work[1] -= 8;
					message_work.all_work[2]++;
					if (message_work.all_work[2] >= 24)
					{
						message_work.all_work[2] = 24;
						message_work.all_work[1] = 0;
					}
				}
			}
			NoiseCtrl.instance.FILM_Effect(1);
			break;
		case 9:
			switch (message_work.op_para)
			{
			case 0:
			{
				AnimationObject animationObject9 = AnimationSystem.Instance.FindObject(1, 0, 50);
				if (animationObject9 == null)
				{
					animationObject9 = AnimationSystem.Instance.PlayObject(1, 0, 50);
				}
				break;
			}
			case 1:
			{
				AnimationObject animationObject9 = AnimationSystem.Instance.FindObject(1, 0, 50);
				if (animationObject9 != null)
				{
					AnimationSystem.Instance.StopObject(1, 0, 50);
				}
				message_work.status2 &= ~MessageSystem.Status2.STOP_EXPL;
				break;
			}
			}
			Exit(message_work);
			break;
		case 10:
			switch (message_work.op_para)
			{
			case 0:
			{
				message_work.op_work[1] = message_work.all_work[0];
				AnimationObject animationObject8 = AnimationSystem.Instance.PlayObject(1, 0, 19);
				animationObject8.SetPriority(AnimationRenderTarget.BehingBg);
				animationObject8.PlayMonochrome(1, 31, 6, true);
				message_work.all_work[0] = 3;
				NoiseCtrl.instance.FILM_EffectInit();
				message_work.all_work[0] = 0;
				message_work.all_work[1] = 0;
				message_work.all_work[2] = 0;
				message_work.op_work[2] = 0;
				message_work.op_para = 1;
				fadeCtrl.instance.play(fadeCtrl.Status.WFADE_IN, 1u, 1u);
				break;
			}
			case 1:
				if (++message_work.all_work[0] >= 70)
				{
					message_work.all_work[0] = 0;
					message_work.op_para = 2;
				}
				break;
			case 2:
				if (++message_work.all_work[1] <= message_work.op_work[1])
				{
					break;
				}
				message_work.all_work[1] = 0;
				message_work.all_work[2]++;
				if (message_work.all_work[2] > 1)
				{
					message_work.all_work[0]++;
					if (message_work.all_work[0] == 80)
					{
						message_work.all_work[0] = 0;
						message_work.op_para = 3;
					}
					message_work.all_work[2] = 0;
				}
				break;
			case 3:
				message_work.op_workno = 1;
				message_work.op_para = 10;
				break;
			}
			NoiseCtrl.instance.FILM_Effect(1);
			break;
		case 11:
		{
			byte op_para = message_work.op_para;
			if (op_para != 0)
			{
				if (op_para != 50)
				{
					if (op_para != 1)
					{
						goto IL_0b46;
					}
				}
				else
				{
					AnimationSystem.Instance.StopObject(0, 0, 19, true);
					if (bgCtrl.instance.bg_no_reserve != 65535)
					{
						bgCtrl.instance.SetReserveSprite();
						spefCtrl.instance.Monochrome_set(23, 1, 31);
					}
					message_work.all_work[1] = 0;
					message_work.op_para = 1;
					fadeCtrl.instance.play(3u, 1u, 1u, 31u);
				}
				if (fadeCtrl.instance.status == fadeCtrl.Status.NO_FADE)
				{
					message_work.op_workno = 1;
					message_work.op_para = 10;
				}
			}
			else
			{
				message_work.op_para = 50;
			}
			goto IL_0b46;
		}
		case 12:
			switch (message_work.op_para)
			{
			case 0:
				message_work.all_work[0] = 3;
				NoiseCtrl.instance.FILM_EffectInit();
				message_work.op_para = 50;
				break;
			case 50:
				if (bgCtrl.instance.bg_no_reserve != 65535)
				{
					bgCtrl.instance.SetReserveSprite();
					spefCtrl.instance.Monochrome_set(23, 1, 31);
				}
				message_work.op_para = 51;
				break;
			case 51:
			{
				int foreSprite = 0;
				switch (GSStatic.global_work_.title)
				{
				case TitleId.GS1:
					foreSprite = 28;
					break;
				case TitleId.GS2:
					foreSprite = 13;
					break;
				case TitleId.GS3:
					foreSprite = 11;
					break;
				}
				bgCtrl.instance.SetForeSprite(foreSprite);
				bgCtrl.instance.Bg256_monochrome(1, 31, 6, true);
				AnimationObject animationObject2 = AnimationSystem.Instance.PlayObject(0, 0, 203);
				animationObject2.PlayMonochrome(1, 31, 6, true);
				message_work.op_work[1] = 0;
				message_work.op_para = 1;
				break;
			}
			case 1:
				if (++message_work.op_work[1] >= 22)
				{
					soundCtrl.instance.PlaySE(111);
					ScreenCtrl.instance.Quake(10, 1u);
					message_work.op_flg &= 240;
					message_work.op_workno = 1;
					message_work.op_para = 10;
				}
				break;
			case 2:
			{
				bgCtrl.instance.DeleteForeSprite();
				AnimationObject animationObject = AnimationSystem.Instance.FindObject(0, 0, 203);
				if (animationObject != null)
				{
					animationObject.Stop(true);
				}
				message_work.op_workno = 1;
				message_work.op_para = 10;
				break;
			}
			}
			NoiseCtrl.instance.FILM_Effect(1);
			break;
		case 13:
		{
			if (message_work.op_para == 0)
			{
				AnimationObject animationObject6 = null;
				animationObject6 = ((message_work.all_work[0] != 0) ? AnimationSystem.Instance.FindObject((int)GSStatic.global_work_.title, 0, 176) : AnimationSystem.Instance.FindObject((int)GSStatic.global_work_.title, 0, 159));
				if (animationObject6 != null)
				{
					animationObject6.PlayMonochrome(0, 31, 6, true);
				}
				spefCtrl.instance.Monochrome_set(23, 0, 31);
				message_work.all_work[2] = message_work.all_work[0];
				message_work.all_work[0] = 3;
				NoiseCtrl.instance.FILM_EffectInit();
				if (message_work.all_work[2] == 0)
				{
				}
				message_work.op_workno = 1;
				message_work.op_para = 10;
				break;
			}
			AnimationObject animationObject7 = AnimationSystem.Instance.FindObject((int)GSStatic.global_work_.title, 0, 159);
			if (animationObject7 != null)
			{
				animationObject7.Stop(true);
			}
			animationObject7 = AnimationSystem.Instance.FindObject((int)GSStatic.global_work_.title, 0, 176);
			if (animationObject7 != null)
			{
				animationObject7.Stop(true);
			}
			for (ushort num2 = 0; num2 < 3; num2++)
			{
				animationObject7 = AnimationSystem.Instance.FindObject((int)GSStatic.global_work_.title, 0, 45 + num2);
				if (animationObject7 != null)
				{
					animationObject7.Stop(true);
				}
			}
			NoiseCtrl.instance.NoiseDelete();
			Exit(message_work);
			break;
		}
		case 14:
			switch (message_work.op_para)
			{
			case 0:
			{
				message_work.all_work[1] = 0;
				if (message_work.all_work[0] == 0)
				{
					message_work.op_para = 2;
				}
				else
				{
					message_work.op_para = 1;
				}
				AnimationObject animationObject5 = AnimationSystem.Instance.FindObject(0, 0, 159);
				if (animationObject5 != null)
				{
					animationObject5.SetPriority(AnimationRenderTarget.Default);
				}
				fadeCtrl.instance.play(2u, message_work.all_work[0], 1u, 8u);
				goto case 1;
			}
			case 1:
				NoiseCtrl.instance.SetLineColor(Color.gray);
				goto case 2;
			case 2:
				if (fadeCtrl.instance.status == fadeCtrl.Status.NO_FADE)
				{
					bgCtrl.instance.SetSprite(4095);
					message_work.op_para = 3;
					message_work.all_work[0] = 0;
				}
				break;
			case 3:
				if (++message_work.all_work[0] >= 3)
				{
					message_work.all_work[0] = 0;
					AnimationObject animationObject4 = AnimationSystem.Instance.FindObject((int)GSStatic.global_work_.title, 0, 159);
					Vector3 localPosition = animationObject4.transform.localPosition;
					localPosition.y += 7.03125f;
					animationObject4.transform.localPosition = localPosition;
					if (localPosition.y >= 360f)
					{
						animationObject4.transform.localPosition = localPosition;
						message_work.op_workno = 1;
						message_work.op_para = 10;
					}
				}
				break;
			}
			if (message_work.op_para < 3)
			{
				NoiseCtrl.instance.FILM_Effect(0);
			}
			else
			{
				NoiseCtrl.instance.FILM_Effect(1);
			}
			break;
		case 15:
			switch (message_work.op_para)
			{
			case 0:
				message_work.all_work[0] = 0;
				message_work.op_para = 1;
				break;
			case 1:
				if (++message_work.all_work[0] >= 4)
				{
					Exit(message_work);
				}
				break;
			}
			break;
		case 16:
			switch (message_work.op_para)
			{
			case 0:
			{
				if (message_work.all_work[0] == 0)
				{
					message_work.op_para = 2;
				}
				else
				{
					message_work.op_para = 1;
				}
				bgCtrl.instance.Bg256_monochrome(1, 31, 6, true);
				ushort in_time = message_work.all_work[0];
				message_work.all_work[0] = 0;
				NoiseCtrl.instance.FILM_EffectInit();
				if (message_work.all_work[1] == 1)
				{
				}
				message_work.all_work[1] = 0;
				fadeCtrl.instance.play(fadeCtrl.Status.WFADE_IN, in_time, 1u);
				goto case 1;
			}
			case 1:
			case 2:
				if (fadeCtrl.instance.status == fadeCtrl.Status.NO_FADE)
				{
					message_work.op_workno = 1;
					message_work.op_para = 10;
				}
				break;
			case 4:
				message_work.all_work[1] = 0;
				if (message_work.all_work[0] == 0)
				{
					message_work.op_para = 2;
				}
				else
				{
					message_work.op_para = 1;
				}
				message_work.all_work[0] = 2;
				NoiseCtrl.instance.FILM_EffectInit();
				break;
			}
			NoiseCtrl.instance.FILM_Effect(0);
			break;
		case 17:
		{
			byte op_para4 = message_work.op_para;
			if (op_para4 != 0)
			{
				if (op_para4 != 1 && op_para4 != 2)
				{
					goto IL_125e;
				}
			}
			else
			{
				message_work.all_work[1] = 0;
				if (message_work.all_work[0] == 0)
				{
					message_work.op_para = 2;
				}
				else
				{
					message_work.op_para = 1;
				}
				fadeCtrl.instance.play(fadeCtrl.Status.WFADE_OUT, message_work.all_work[0], 1u);
			}
			if (fadeCtrl.instance.status == fadeCtrl.Status.NO_FADE)
			{
				message_work.op_workno = 1;
				message_work.op_para = 10;
			}
			goto IL_125e;
		}
		case 18:
		{
			byte op_para2 = message_work.op_para;
			if (op_para2 != 0)
			{
				if (op_para2 != 1 && op_para2 != 2)
				{
					goto IL_1324;
				}
			}
			else
			{
				message_work.all_work[1] = 0;
				if (message_work.all_work[0] == 0)
				{
					message_work.op_para = 2;
				}
				else
				{
					message_work.op_para = 1;
				}
				GSStatic.global_work_.fade_time = (byte)message_work.all_work[0];
				GSStatic.global_work_.fade_speed = (byte)message_work.all_work[1];
				fadeCtrl.instance.play(fadeCtrl.Status.FADE_OUT, GSStatic.global_work_.fade_time, 1u);
				message_work.op_work[0] = 1;
			}
			if (fadeCtrl.instance.status == fadeCtrl.Status.NO_FADE)
			{
				MoveNextScript(message_work);
			}
			goto IL_1324;
		}
		case 19:
			switch (message_work.op_para)
			{
			default:
				return;
			case 0:
				message_work.all_work[1] = 0;
				message_work.all_work[2] = 0;
				message_work.op_para = 1;
				bgCtrl.instance.Bg256_monochrome(message_work.all_work[0], 0, 0, true);
				foreach (AnimationObject item in AnimationSystem.Instance.AllAnimationObject)
				{
					if (item.Exists && item.CharacterID == 0)
					{
						item.PlayMonochrome(message_work.all_work[0], 0, 0, true);
					}
				}
				break;
			case 1:
				break;
			}
			if (++message_work.all_work[1] >= message_work.all_work[0])
			{
				message_work.all_work[1] = 0;
				message_work.all_work[2]++;
				if (message_work.all_work[2] >= 32)
				{
					MoveNextScript(message_work);
				}
			}
			break;
		case 20:
			switch (message_work.op_para)
			{
			case 0:
				message_work.op_para = 50;
				message_work.all_work[0] = 2;
				NoiseCtrl.instance.FILM_EffectInit();
				break;
			case 50:
				message_work.all_work[1] = 0;
				if (message_work.all_work[0] == 0)
				{
					message_work.op_para = 2;
				}
				else
				{
					message_work.op_para = 1;
				}
				GSStatic.global_work_.fade_time = (byte)message_work.all_work[0];
				GSStatic.global_work_.fade_speed = 1;
				AnimationSystem.Instance.Char_monochrome(1, 31, 6, true);
				fadeCtrl.instance.play(fadeCtrl.Status.FADE_IN, GSStatic.global_work_.fade_time, GSStatic.global_work_.fade_speed);
				message_work.op_para = 1;
				goto case 1;
			case 1:
			case 2:
				if (fadeCtrl.instance.status == fadeCtrl.Status.NO_FADE)
				{
					message_work.op_work[1] = 0;
					message_work.all_work[1] = 1;
					message_work.op_para = 3;
				}
				break;
			case 3:
				if (++message_work.op_work[1] >= 7 * message_work.all_work[1])
				{
					message_work.op_workno = 1;
					message_work.op_para = 10;
				}
				break;
			}
			NoiseCtrl.instance.FILM_Effect(0);
			break;
		case 21:
			message_work.all_work[1] = 0;
			AnimationSystem.Instance.Char_monochrome(1, 31, 6, true);
			message_work.op_workno = 1;
			message_work.op_para = 10;
			NoiseCtrl.instance.FILM_Effect(0);
			break;
		case 22:
			switch (message_work.op_para)
			{
			default:
				return;
			case 0:
				message_work.all_work[1] = 0;
				if (message_work.all_work[0] == 0)
				{
					message_work.op_para = 2;
				}
				else
				{
					message_work.op_para = 1;
				}
				AnimationSystem.Instance.Char_monochrome(1, 31, 6, true);
				GSStatic.global_work_.fade_time = (byte)message_work.all_work[0];
				GSStatic.global_work_.fade_speed = 1;
				message_work.all_work[0] = 2;
				NoiseCtrl.instance.FILM_EffectInit();
				message_work.op_para = 1;
				GSStatic.global_work_.fade_time = (byte)message_work.all_work[0];
				message_work.all_work[0] = 2;
				NoiseCtrl.instance.FILM_EffectInit();
				fadeCtrl.instance.play(fadeCtrl.Status.WFADE_IN, GSStatic.global_work_.fade_time, 1u);
				break;
			case 1:
			case 2:
				break;
			case 4:
				return;
			case 3:
				return;
			}
			if (fadeCtrl.instance.status == fadeCtrl.Status.NO_FADE)
			{
				message_work.op_workno = 1;
				message_work.op_para = 10;
			}
			break;
		case 98:
			DemoProc_Special(message_work);
			break;
		case 0:
			{
				Exit(message_work);
				break;
			}
			IL_125e:
			NoiseCtrl.instance.FILM_Effect(0);
			break;
			IL_0384:
			NoiseCtrl.instance.FILM_Effect(0);
			break;
			IL_0b46:
			NoiseCtrl.instance.FILM_Effect(1);
			break;
			IL_1324:
			NoiseCtrl.instance.FILM_Effect(0);
			break;
			IL_02e6:
			NoiseCtrl.instance.FILM_Effect(0);
			break;
		}
	}

	private static void Op5DemoProc(MessageWork message_work)
	{
		if (GSStatic.global_work_.title == TitleId.GS3)
		{
			Op5DemoProc_GS3(message_work);
			return;
		}
		switch (message_work.op_workno)
		{
		case 1:
			message_work.op_workno = 0;
			break;
		case 2:
			GS1_sce4_opening.instance.play_rain(true, 100);
			message_work.op_workno = 0;
			break;
		case 3:
			switch (message_work.op_para)
			{
			case 0:
				fadeCtrl.instance.play(message_work.all_work[0], message_work.all_work[1], message_work.all_work[2]);
				message_work.op_para++;
				break;
			case 1:
				if (fadeCtrl.instance.is_end)
				{
					message_work.op_workno = 0;
				}
				break;
			}
			break;
		case 4:
			GS1_sce4_opening.instance.set_biru_obj(true);
			GS1_sce4_opening.instance.play_biru_scroll(100);
			message_work.op_workno = 0;
			break;
		case 9:
			GS1_sce4_opening.instance.play_rain(false);
			message_work.op_workno = 0;
			break;
		case 12:
			GS1_sce4_opening.instance.play_rain(true);
			GS1_sce4_opening.instance.set_biru_obj(false);
			GS1_sce4_opening.instance.play_biru_scroll();
			bgCtrl.instance.SetSprite(4095);
			message_work.op_workno = 0;
			break;
		case 10:
			GS1_sce4_opening.instance.stop_biru_scroll();
			message_work.op_workno = 0;
			break;
		case 11:
			message_work.op_workno = 0;
			break;
		case 8:
			GS1_sce4_opening.instance.stop_biru_scroll();
			message_work.op_workno = 0;
			break;
		case 5:
			switch (message_work.op_para)
			{
			case 0:
				GS1_sce4_opening.instance.play_black_knife();
				message_work.op_para++;
				break;
			case 1:
				message_work.op_para++;
				break;
			case 2:
				if (!GS1_sce4_opening.instance.is_black_knife_animation)
				{
					message_work.op_para++;
				}
				break;
			case 3:
				if (!GS1_sce4_opening.instance.is_movie)
				{
					message_work.op_flg &= 240;
					message_work.mdt_index++;
					message_work.mdt_index++;
					message_work.mdt_index++;
					message_work.op_workno = 0;
					bgCtrl.instance.SetSprite(228);
				}
				break;
			}
			break;
		case 6:
			switch (message_work.op_para)
			{
			case 0:
				soundCtrl.instance.AllStopBGM();
				GS1_sce4_opening.instance.play_flash_knife();
				GS1_sce4_opening.instance.op4_taimar = 0;
				message_work.op_para++;
				break;
			case 1:
				if (GS1_sce4_opening.instance.op4_taimar >= 10)
				{
					GS1_sce4_opening.instance.stop_flash_knife();
					fadeCtrl.instance.play(fadeCtrl.Status.WFADE_OUT, 1u, 8u);
					GS1_sce4_opening.instance.op4_taimar = 0;
					message_work.op_para++;
				}
				GS1_sce4_opening.instance.op4_taimar++;
				break;
			case 2:
				if (fadeCtrl.instance.is_end)
				{
					message_work.op_para++;
				}
				break;
			case 3:
				message_work.op_flg &= 240;
				message_work.mdt_index++;
				message_work.mdt_index++;
				message_work.mdt_index++;
				message_work.op_workno = 0;
				break;
			}
			break;
		case 7:
			switch (message_work.op_para)
			{
			case 0:
				GS1_sce4_opening.instance.set_tomoe_obj();
				GS1_sce4_opening.instance.play_tomoe_scroll();
				message_work.op_para++;
				break;
			}
			break;
		case 99:
			GS1_sce4_opening.instance.stop_tomoe_scroll();
			GS1_sce4_opening.instance.end();
			message_work.op_no = 0;
			message_work.op_workno = 0;
			break;
		}
	}

	private static void Op5DemoProc_GS3(MessageWork message_work)
	{
		FURIKO_WK furiko_wk_ = GSStatic.furiko_wk_;
		OP05_WK op5_wk_ = GSStatic.op5_wk_;
		float num = 0f;
		switch (message_work.op_workno)
		{
		case 1:
			switch (message_work.op_para)
			{
			case 0:
				bgCtrl.instance.SetSprite(message_work.all_work[0], false);
				op5_wk_.BG_rno = 0;
				message_work.all_work[2] = 0;
				op5_wk_.fade = 31;
				op5_wk_.wait = 0;
				message_work.op_para = 1;
				break;
			case 1:
				if (op5_wk_.BG_rno != 0)
				{
					break;
				}
				bgCtrl.instance.Bg256_monochrome(1, 31, 0, true);
				if (message_work.all_work[1] != 0)
				{
					if (++message_work.all_work[2] >= 2)
					{
						message_work.all_work[2] = 0;
						op5_wk_.fade--;
						if (op5_wk_.fade == 0)
						{
							Exit(message_work);
							message_work.op_workno = 198;
						}
						else
						{
							float alpha4 = (float)(255 - 2 * (31 - op5_wk_.fade)) / 255f;
							endingMaskCtrl.instance.SetAlpha(alpha4);
						}
					}
				}
				else
				{
					Exit(message_work);
					message_work.op_workno = 198;
				}
				break;
			}
			break;
		case 2:
			switch (message_work.op_para)
			{
			case 0:
				GSDemo_gs3_op5_mask.instance.Init();
				op5_wk_.speed = message_work.all_work[0];
				op5_wk_.next_obj = 4;
				op5_wk_.OBJ_scroll = 0;
				op5_wk_.fade = 0;
				message_work.all_work[2] = 0;
				message_work.op_para = 2;
				endingMaskCtrl.instance.SetMask();
				endingMaskCtrl.instance.SetAlpha(1f);
				fadeCtrl.instance.play(fadeCtrl.Status.FADE_IN, 16u, 4u);
				break;
			case 2:
				if (++message_work.all_work[2] >= message_work.all_work[1])
				{
					message_work.all_work[2] = 0;
					op5_wk_.fade++;
					if (op5_wk_.fade == 16)
					{
						Exit(message_work);
					}
				}
				break;
			case 3:
				op5_wk_.OBJ_scroll = 1;
				Exit(message_work);
				message_work.op_workno = 198;
				break;
			}
			break;
		case 3:
		{
			byte op_para3 = message_work.op_para;
			if (op_para3 != 0)
			{
				if (op_para3 != 1)
				{
					break;
				}
			}
			else
			{
				op5_wk_.fade = 0;
				message_work.all_work[0] = 0;
				message_work.op_para = 1;
			}
			if (++message_work.all_work[0] >= 2)
			{
				message_work.all_work[0] = 0;
				op5_wk_.fade++;
				float alpha3 = (float)(256 - 2 * (31 - op5_wk_.fade)) / 255f;
				endingMaskCtrl.instance.SetAlpha(alpha3);
				if (op5_wk_.fade >= 31)
				{
					endingMaskCtrl.instance.SetAlpha(1f);
					Exit(message_work);
					message_work.op_workno = 198;
				}
			}
			break;
		}
		case 4:
			switch (message_work.op_para)
			{
			case 0:
				GSStatic.ClearDemo_buff();
				AnimationSystem.Instance.PlayObject((int)GSStatic.global_work_.title, 0, 98);
				message_work.op_para = 1;
				break;
			case 1:
			{
				AnimationObject animationObject27 = AnimationSystem.Instance.FindObject((int)GSStatic.global_work_.title, 0, 98);
				if (animationObject27 == null)
				{
					Exit(message_work);
				}
				break;
			}
			}
			break;
		case 5:
			switch (message_work.op_para)
			{
			case 0:
				message_work.all_work[1] = 0;
				message_work.all_work[2] = 0;
				op5_wk_.fade = 12;
				message_work.op_para = 1;
				fadeCtrl.instance.play(fadeCtrl.Status.WFADE_OUT, 31u, 3u);
				break;
			case 1:
				if (fadeCtrl.instance.status == fadeCtrl.Status.NO_FADE)
				{
					op5_wk_.fade = 0;
					message_work.op_para = 2;
				}
				break;
			case 2:
				op5_wk_.OBJ_scroll = 0;
				bgCtrl.instance.Bg256_monochrome(1, 1, 0, false);
				bgCtrl.instance.SetSprite(4095);
				GSDemo_gs3_op5_mask.instance.Exit();
				endingMaskCtrl.instance.ResetMask();
				MoveNextScript(message_work);
				break;
			}
			break;
		case 100:
			switch (message_work.op_para)
			{
			case 0:
				message_work.op_para = 1;
				break;
			case 1:
				message_work.op_work[0] = 1;
				message_work.op_work[1] = 0;
				message_work.op_work[2] = 0;
				message_work.op_work[3] = 0;
				GS1_sce4_opening.instance.GS3YahariInit();
				message_work.op_workno = 101;
				message_work.op_para = 0;
				break;
			}
			break;
		case 101:
			switch (message_work.op_para)
			{
			case 1:
				GSStatic.expl_char_work_.expl_char_data_[0].vram_addr = 98304u;
				GSStatic.expl_char_work_.expl_char_data_[0].para0 = message_work.all_work[0];
				GSStatic.expl_char_work_.expl_char_data_[0].para1 = 0;
				GSStatic.expl_char_work_.expl_char_data_[0].para2 = 1;
				message_work.all_work[1] = 0;
				message_work.all_work[2] = 0;
				GS1_sce4_opening.instance.GS3YahariFadeOutStart();
				message_work.op_para = 10;
				break;
			case 11:
				if (++message_work.all_work[1] >= 5)
				{
					message_work.all_work[1] = (message_work.all_work[2] = 0);
					message_work.op_para = 10;
					GSStatic.expl_char_work_.expl_char_data_[0].vram_addr = 98304u;
					GSStatic.expl_char_work_.expl_char_data_[0].para0 = message_work.all_work[0];
					GSStatic.expl_char_work_.expl_char_data_[0].para1 = 0;
					GSStatic.expl_char_work_.expl_char_data_[0].para2 = 1;
				}
				break;
			case 2:
				message_work.all_work[1] = 4;
				message_work.op_para = 10;
				goto case 10;
			case 10:
			{
				ExplCharData explCharData = GSStatic.expl_char_work_.expl_char_data_[0];
				if (message_work.all_work[1] != 0)
				{
					message_work.all_work[1]--;
					if (message_work.all_work[1] != 0)
					{
					}
				}
				if (explCharData.para1 != 0 && ++message_work.all_work[2] >= message_work.all_work[0])
				{
					message_work.all_work[2] = 0;
					explCharData.para1++;
					if (explCharData.para1 > 31)
					{
						explCharData.para1 = 0;
					}
				}
				if (explCharData.para2 != 0)
				{
					explCharData.para2 = GS1_sce4_opening.instance.GS3Yahari_screen_rotate(message_work);
					GS1_sce4_opening.instance.GS3YahariScale();
					if (explCharData.para2 == 0)
					{
						explCharData.vram_addr = 0u;
					}
				}
				break;
			}
			case 4:
				message_work.all_work[2] = 0;
				GSStatic.expl_char_work_.expl_char_data_[0].para1 = 1;
				message_work.op_para = 10;
				break;
			case 5:
				GS1_sce4_opening.instance.GS3YahariExit();
				message_work.op_para = 6;
				break;
			case 6:
				Exit(message_work);
				break;
			}
			break;
		case 102:
			switch (message_work.op_para)
			{
			case 0:
				message_work.all_work[1] = 0;
				message_work.all_work[2] = 31;
				fadeCtrl.instance.play(1u, message_work.all_work[2], 4u, 8u);
				message_work.op_para = 1;
				break;
			case 1:
				if (++message_work.all_work[1] >= message_work.all_work[0])
				{
					message_work.all_work[1] = 0;
					if (message_work.all_work[2] == 0)
					{
						AnimationSystem.Instance.CharacterAnimationObject.BeFlag &= -1025;
						AnimationSystem.Instance.ResetSpefChara();
						MoveNextScript(message_work);
					}
					else
					{
						message_work.all_work[2]--;
						AnimationSystem.Instance.CharacterAnimationObject.SetSpefVolume((float)(int)message_work.all_work[2] / 31f);
						AnimationSystem.Instance.CharacterAnimationObject.BeFlag &= -1025;
					}
				}
				break;
			}
			break;
		case 103:
			switch (message_work.op_para)
			{
			case 0:
			{
				AnimationObject animationObject16 = AnimationSystem.Instance.PlayObject((int)GSStatic.global_work_.title, 35, 211);
				Vector3 localPosition16 = animationObject16.transform.localPosition;
				animationObject16 = AnimationSystem.Instance.FindObject((int)GSStatic.global_work_.title, 35, 211);
				AnimationSystem.Instance.CharacterAnimationObject.BeFlag &= -16777217;
				AnimationSystem.Instance.CharacterAnimationObject.BeFlag |= 128;
				if (animationObject16 != null)
				{
					localPosition16.z = -4f;
					localPosition16.x += (float)(message_work.all_work[0] - 128) * 6.75f;
					animationObject16.transform.localPosition = localPosition16;
					animationObject16.ResetSpef();
					animationObject16.PlayMonochrome(1, 31, 0, true);
					animationObject16.ObjColor = new Color(0.6666666f, 0.6666666f, 0.6666666f, 0f);
				}
				message_work.all_work[0] = 0;
				message_work.all_work[2] = 0;
				message_work.op_para = 1;
				break;
			}
			case 1:
			{
				AnimationObject animationObject16;
				if (message_work.all_work[2] == 15)
				{
					animationObject16 = AnimationSystem.Instance.FindObject((int)GSStatic.global_work_.title, 35, 211);
					AnimationSystem.Instance.CharacterAnimationObject.BeFlag &= -129;
					AnimationSystem.Instance.CharacterAnimationObject.BeFlag &= -65;
					AnimationSystem.Instance.CharacterAnimationObject.BeFlag &= -16777217;
					AnimationSystem.Instance.CharacterAnimationObject.BeFlag &= -33554433;
					MoveNextScript(message_work);
					if (animationObject16 != null)
					{
						animationObject16.Alpha = 1f;
					}
					break;
				}
				if (++message_work.all_work[0] >= message_work.all_work[1])
				{
					message_work.all_work[0] = 0;
					message_work.all_work[2]++;
				}
				animationObject16 = AnimationSystem.Instance.FindObject((int)GSStatic.global_work_.title, 35, 211);
				if (animationObject16 != null)
				{
					float num4 = 15 * message_work.all_work[1];
					float num5 = message_work.all_work[2] * message_work.all_work[1] + message_work.all_work[0];
					float alpha = num5 / num4;
					animationObject16.Alpha = alpha;
				}
				break;
			}
			case 2:
			{
				AnimationObject animationObject16 = AnimationSystem.Instance.FindObject((int)GSStatic.global_work_.title, 35, 211);
				animationObject16.ObjColor = Color.white;
				animationObject16.ResetSpef();
				AnimationSystem.Instance.StopObject(1, 0, 211);
				Exit(message_work);
				break;
			}
			}
			break;
		case 104:
			switch (message_work.op_para)
			{
			case 0:
				bgCtrl.instance.SetSprite(143);
				message_work.op_para = 1;
				break;
			case 1:
				AnimationSystem.Instance.PlayObject(0, 0, 194);
				message_work.all_work[0] = 0;
				message_work.all_work[1] = 0;
				message_work.all_work[2] = 0;
				furiko_wk_.total = 0;
				furiko_wk_.status = 0;
				message_work.op_para = 2;
				break;
			case 2:
				if (++message_work.all_work[0] >= 1)
				{
					message_work.all_work[0] = 0;
					AnimationObject animationObject15 = AnimationSystem.Instance.FindObject(0, 0, 194);
					animationObject15.Stop(true);
					animationObject15 = AnimationSystem.Instance.PlayObject(0, 0, 195);
					Vector3 localPosition15 = animationObject15.transform.localPosition;
					localPosition15.x = 70f;
					animationObject15.transform.localPosition = localPosition15;
					message_work.op_para = 3;
				}
				break;
			case 3:
				if (++furiko_wk_.total >= furiko[furiko_wk_.status + 1].time)
				{
					furiko_wk_.status++;
					if (furiko[furiko_wk_.status].dir == 0)
					{
						message_work.all_work[0] = 0;
						message_work.all_work[2] = 1;
						bgCtrl instance2 = bgCtrl.instance;
						Vector3 localPosition12 = instance2.body.transform.localPosition;
						localPosition12.y += (float)(message_work.all_work[0] * 1080) / 192f;
						instance2.SetBodyPosition(localPosition12);
						message_work.op_para = 4;
						break;
					}
				}
				if (++message_work.all_work[1] >= furiko[furiko_wk_.status].wait)
				{
					message_work.all_work[1] = 0;
					message_work.all_work[0] = (ushort)((short)message_work.all_work[0] + furiko[furiko_wk_.status].dir);
					while (message_work.all_work[0] >= 8)
					{
						message_work.all_work[0] -= 8;
					}
					bgCtrl instance3 = bgCtrl.instance;
					Vector3 localPosition13 = instance3.body.transform.localPosition;
					localPosition13.y += (float)(int)message_work.all_work[0] * 1080f / 192f;
					if (localPosition13.y > 900f)
					{
						localPosition13.y = 900f;
					}
					instance3.SetBodyPosition(localPosition13);
					AnimationObject animationObject14 = AnimationSystem.Instance.FindObject(0, 0, 195);
					Vector3 localPosition14 = animationObject14.transform.localPosition;
					localPosition14.y = -480f + localPosition13.y;
					animationObject14.transform.localPosition = localPosition14;
				}
				break;
			case 4:
				if (++furiko_wk_.total >= furiko[furiko_wk_.status + 1].time)
				{
					furiko_wk_.status++;
					if (furiko[furiko_wk_.status].wait == -1)
					{
						MoveNextScript(message_work);
						break;
					}
				}
				if (furiko[furiko_wk_.status].wait != 0 && ++message_work.all_work[1] >= furiko[furiko_wk_.status].wait)
				{
					message_work.all_work[1] = 0;
					message_work.all_work[0] = (ushort)((short)message_work.all_work[0] - furiko[furiko_wk_.status].dir);
					while (message_work.all_work[0] >= 8)
					{
						message_work.all_work[0] -= 8;
					}
					bgCtrl instance = bgCtrl.instance;
					Vector3 localPosition10 = instance.body.transform.localPosition;
					if (message_work.all_work[0] > 0)
					{
						localPosition10.y -= (float)((message_work.all_work[0] - 1) * 1080) / 192f;
					}
					else if (message_work.all_work[0] >= 5)
					{
						localPosition10.y -= (float)((message_work.all_work[0] - 2) * 1080) / 192f;
					}
					else
					{
						localPosition10.y -= (float)(message_work.all_work[0] * 1080) / 192f;
					}
					if (localPosition10.y < 0f)
					{
						localPosition10.y = 0f;
					}
					instance.SetBodyPosition(localPosition10);
					AnimationObject animationObject13 = AnimationSystem.Instance.FindObject(0, 0, 195);
					Vector3 localPosition11 = animationObject13.transform.localPosition;
					localPosition11.y = -480f + localPosition10.y;
					animationObject13.transform.localPosition = localPosition11;
				}
				break;
			case 5:
			{
				AnimationObject animationObject12 = AnimationSystem.Instance.FindObject(0, 0, 195);
				animationObject12.Stop(true);
				Exit(message_work);
				break;
			}
			}
			break;
		case 105:
			switch (message_work.op_para)
			{
			case 0:
				message_work.op_para = 50;
				break;
			case 50:
				bgCtrl.instance.SetSprite(154);
				bgCtrl.instance.DetentionBgFade(0f);
				message_work.op_para = 51;
				break;
			case 51:
				message_work.op_para = 1;
				break;
			case 1:
			{
				AnimationObject animationObject6 = AnimationSystem.Instance.PlayObject(2, 0, 123);
				Vector3 localPosition2 = animationObject6.transform.localPosition;
				localPosition2.y = -892f;
				animationObject6.transform.localPosition = localPosition2;
				animationObject6 = AnimationSystem.Instance.PlayObject(2, 0, 124);
				localPosition2 = animationObject6.transform.localPosition;
				localPosition2.y = -892f;
				localPosition2.z += 1f;
				animationObject6.transform.localPosition = localPosition2;
				GS1_sce4_opening.instance.ChinamiBreak_ReloadBG04();
				furiko_wk_.QuakeTime = 0;
				furiko_wk_.QuakeCnt = 0;
				furiko_wk_.FlashCnt = 0;
				furiko_wk_.work[0] = 0;
				furiko_wk_.work[1] = 0;
				message_work.all_work[0] = 24;
				message_work.all_work[1] = 0;
				message_work.all_work[2] = 0;
				soundCtrl.instance.FadeOutBGM(120);
				soundCtrl.instance.PlaySE(400);
				message_work.op_para = 2;
				break;
			}
			case 2:
			{
				AnimationObject animationObject7 = AnimationSystem.Instance.FindObject(2, 0, 123);
				AnimationObject animationObject8 = AnimationSystem.Instance.FindObject(2, 0, 124);
				Vector3 localPosition3 = animationObject7.transform.localPosition;
				Vector3 localPosition4 = animationObject8.transform.localPosition;
				if (animationObject7 != null)
				{
					if (++furiko_wk_.work[0] >= 4)
					{
						if (++message_work.all_work[2] >= 4)
						{
							message_work.all_work[2] = 0;
							if (++message_work.all_work[1] > 4)
							{
								message_work.all_work[1] = 4;
							}
						}
						localPosition3.y += (float)(int)message_work.all_work[1] * 6.75f;
						GS1_sce4_opening.instance.ChinamiBreak_AddBGPosY(0, (float)(int)message_work.all_work[1] * 6.75f);
					}
					else if (++furiko_wk_.work[1] >= 2)
					{
						furiko_wk_.work[1] = 0;
						localPosition3.y += 6.75f;
						GS1_sce4_opening.instance.ChinamiBreak_AddBGPosY(0, 6.75f);
					}
					localPosition4.y = localPosition3.y;
					if ((double)localPosition3.y >= (double)(-message_work.all_work[0] * 9 + 64) * 6.75)
					{
						if (message_work.all_work[0] == 0)
						{
							animationObject7.Stop(true);
							animationObject8.Stop(true);
							GS1_sce4_opening.instance.ChinamiBreakBGEnabled(0, false);
							GS1_sce4_opening.instance.ChinamiBreakBGEnabled(1, false);
							GS1_sce4_opening.instance.ChinamiBreakBGEnabled(2, false);
							GS1_sce4_opening.instance.ChinamiBreakBGEnabled(0, false, true);
							message_work.op_para = 3;
						}
						else
						{
							message_work.all_work[0]--;
							if (message_work.all_work[0] == 0)
							{
								localPosition3.y += 40.5f;
								localPosition4.y = localPosition3.y;
								GS1_sce4_opening.instance.ChinamiBreak_AddBGPosY(0, 40.5f);
							}
						}
					}
				}
				animationObject7 = AnimationSystem.Instance.FindObject(2, 0, 123);
				animationObject8 = AnimationSystem.Instance.FindObject(2, 0, 124);
				if (animationObject7 != null)
				{
					animationObject7.transform.localPosition = localPosition3;
				}
				if (animationObject8 != null)
				{
					animationObject8.transform.localPosition = localPosition4;
				}
				break;
			}
			case 3:
				if (++furiko_wk_.FlashCnt < 20 || ++furiko_wk_.QuakeTime < 10)
				{
					break;
				}
				furiko_wk_.QuakeTime = 0;
				if (furiko_wk_.QuakeCnt != 0)
				{
					if (furiko_wk_.QuakeCnt < 11)
					{
						AnimationObject animationObject10 = AnimationSystem.Instance.PlayObject(2, 0, 125 + furiko_wk_.QuakeCnt - 1);
						Vector3 localPosition5 = animationObject10.transform.localPosition;
						localPosition5.x = sTamashiPosX[furiko_wk_.QuakeCnt - 1];
						localPosition5.y = sTamashiPosY[furiko_wk_.QuakeCnt - 1];
						animationObject10.transform.localPosition = localPosition5;
					}
					else if (furiko_wk_.QuakeCnt == 14)
					{
						AnimationObject animationObject11 = AnimationSystem.Instance.PlayObject(2, 0, 135);
						Vector3 localPosition6 = animationObject11.transform.localPosition;
						localPosition6.x = sTamashiPosX[10];
						localPosition6.y = sTamashiPosY[10];
						animationObject11.transform.localPosition = localPosition6;
					}
				}
				if (++furiko_wk_.QuakeCnt == 15)
				{
					furiko_wk_.QuakeTime = 0;
					furiko_wk_.QuakeCnt = 0;
					furiko_wk_.FlashCnt = 0;
					message_work.op_para = 4;
					soundCtrl.instance.PlayBGM(339, 80);
				}
				break;
			case 4:
				if (++furiko_wk_.FlashCnt <= 90)
				{
					if (furiko_wk_.FlashCnt == 89)
					{
						fadeCtrl.instance.play(fadeCtrl.Status.WFADE_OUT, 0u, 0u);
					}
					else if (furiko_wk_.FlashCnt == 90)
					{
						fadeCtrl.instance.play(fadeCtrl.Status.WFADE_IN, 31u, 4u);
						bgCtrl.instance.DetentionBgFade(1f);
						soundCtrl.instance.PlaySE(87);
					}
				}
				else if (++furiko_wk_.QuakeTime >= 3)
				{
					furiko_wk_.QuakeTime = 0;
					if (++furiko_wk_.QuakeCnt >= 31)
					{
						furiko_wk_.QuakeTime = 0;
						furiko_wk_.QuakeCnt = 31;
						furiko_wk_.FlashCnt = 0;
						message_work.op_para = 52;
					}
				}
				break;
			case 52:
				if (fadeCtrl.instance.status == fadeCtrl.Status.NO_FADE)
				{
					message_work.op_para = 5;
				}
				break;
			case 5:
				if (furiko_wk_.QuakeCnt != 0 && ++furiko_wk_.QuakeTime >= 3)
				{
					furiko_wk_.QuakeTime = 0;
					furiko_wk_.QuakeCnt--;
					bgCtrl.instance.DetentionBgFade((float)(int)furiko_wk_.QuakeCnt * 8.2f / 255f);
					if (furiko_wk_.QuakeCnt == 0)
					{
						bgCtrl.instance.DetentionBgFade(0f);
					}
				}
				furiko_wk_.FlashCnt++;
				switch (furiko_wk_.FlashCnt)
				{
				case 10:
					op4_105_flame_end(new int[3] { 0, 4, 5 });
					break;
				case 20:
					op4_105_flame_end(new int[3] { 1, 7, 8 });
					break;
				case 30:
					op4_105_flame_end(new int[2] { 2, 6 });
					break;
				case 40:
					op4_105_flame_end(new int[2] { 3, 9 });
					break;
				}
				if (furiko_wk_.QuakeCnt == 0 && furiko_wk_.FlashCnt > 40)
				{
					GSStatic.ClearDemo_buff();
					op5_wk_.next_obj = 0;
					message_work.all_work[0] = 0;
					message_work.op_para = 20;
				}
				break;
			case 6:
				if (BG_Data[op5_wk_.next_obj, 0] != ushort.MaxValue)
				{
					op5_wk_.BG_no = BG_Data[op5_wk_.next_obj, 0];
					op5_wk_.BG_rno = 1;
					op5_wk_.BG_status = 0;
					bgCtrl.instance.SetSprite(op5_wk_.BG_no);
					message_work.op_para = 7;
				}
				else
				{
					bgCtrl.instance.SetSprite(4095);
					op5_wk_.fade = 31;
					op5_wk_.wait = 0;
					fadeCtrl.instance.play(fadeCtrl.Status.WFADE_OUT, 0u, 0u);
					message_work.op_para = 101;
				}
				break;
			case 101:
			{
				fadeCtrl.instance.play(fadeCtrl.Status.WFADE_IN, 4u, 1u);
				AnimationObject animationObject9 = AnimationSystem.Instance.FindObject(2, 0, 135);
				animationObject9.Alpha = 1f;
				message_work.op_para = 10;
				break;
			}
			case 7:
				if (op5_wk_.BG_rno != 0)
				{
					op5_wk_.fade = 32;
					op5_wk_.wait = 0;
					message_work.op_para = 8;
					soundCtrl.instance.PlaySE(87);
					fadeCtrl.instance.play(1u, 0u, 0u, 8u);
				}
				break;
			case 8:
				if (op5_wk_.fade == 32)
				{
					fadeCtrl.instance.play(2u, (uint)(4 - BG_Data[op5_wk_.next_obj, 1]), 1u, 8u);
				}
				if (op5_wk_.fade >= BG_Data[op5_wk_.next_obj, 1])
				{
					op5_wk_.fade -= BG_Data[op5_wk_.next_obj, 1];
				}
				else
				{
					op5_wk_.fade = 0;
				}
				if (op5_wk_.fade == 0 && fadeCtrl.instance.status == fadeCtrl.Status.NO_FADE)
				{
					if (BG_Data[op5_wk_.next_obj, 2] != 0)
					{
						op5_wk_.fade = 0;
						message_work.op_para = 9;
					}
					else
					{
						op5_wk_.next_obj++;
						message_work.op_para = 6;
					}
				}
				break;
			case 9:
				if (++op5_wk_.fade >= BG_Data[op5_wk_.next_obj, 2])
				{
					op5_wk_.fade = 0;
					op5_wk_.next_obj++;
					message_work.op_para = 6;
				}
				break;
			case 10:
				if (++op5_wk_.wait >= 3)
				{
					op5_wk_.wait = 0;
					op5_wk_.fade--;
					if (op5_wk_.fade == 0)
					{
						op5_wk_.fade = 0;
						message_work.op_para = 11;
					}
				}
				break;
			case 11:
				if (++op5_wk_.fade >= 60)
				{
					AnimationObject animationObject4 = AnimationSystem.Instance.FindObject(2, 0, 135);
					AnimationObject animationObject5 = AnimationSystem.Instance.PlayObject(2, 0, 146);
					animationObject5.transform.localPosition = animationObject4.transform.localPosition;
					animationObject4.Stop(true);
					soundCtrl.instance.FadeOutBGM(120);
					message_work.op_para = 12;
				}
				break;
			case 12:
			{
				AnimationObject animationObject3 = AnimationSystem.Instance.FindObject(2, 0, 146);
				if (animationObject3 == null)
				{
					MoveNextScript(message_work);
				}
				break;
			}
			case 20:
				if (++message_work.all_work[0] >= 4)
				{
					message_work.all_work[0] = 0;
					AnimationObject animationObject2 = AnimationSystem.Instance.FindObject(2, 0, 135);
					Vector3 localPosition = animationObject2.transform.localPosition;
					localPosition.y -= 6.75f;
					animationObject2.transform.localPosition = localPosition;
					if (localPosition.y <= 0f)
					{
						message_work.op_para = 21;
					}
				}
				break;
			case 21:
				if (++message_work.all_work[0] >= 40)
				{
					message_work.all_work[0] = 0;
					message_work.op_para = 6;
					AnimationObject animationObject = AnimationSystem.Instance.FindObject(2, 0, 135);
					animationObject.Alpha = 0.6f;
				}
				break;
			}
			break;
		case 106:
			switch (message_work.op_para)
			{
			case 0:
				message_work.op_para = 1;
				break;
			case 1:
			{
				message_work.all_work[0] = 0;
				message_work.all_work[1] = 1;
				message_work.all_work[2] = 0;
				furiko_wk_.total = 0;
				furiko_wk_.status = 1;
				furiko_wk_.cnt = 0;
				furiko_wk_.work[0] = 0;
				furiko_wk_.work[1] = 0;
				furiko_wk_.work[2] = 0;
				furiko_wk_.work[3] = 0;
				furiko_wk_.work[4] = 0;
				furiko_wk_.work[5] = 0;
				furiko_wk_.work[6] = 0;
				furiko_wk_.work[7] = 0;
				furiko_wk_.work[9] = 0;
				furiko_wk_.work[10] = 1;
				furiko_wk_.work[11] = 2;
				furiko_wk_.QuakeTime = 0;
				furiko_wk_.QuakeCnt = 0;
				furiko_wk_.FlashCnt = 0;
				furiko_wk_.ScrollFlag = 1;
				furiko_wk_.para[3] = 0;
				for (ushort num9 = 0; num9 < 3; num9++)
				{
					furiko_wk_.sc_cnt[num9] = 0;
					furiko_wk_.sc_line[num9] = furiko_wk_.work[8];
					furiko_wk_.sc_over[num9] = 0;
					furiko_wk_.para[num9] = 0;
				}
				message_work.op_para = 10;
				break;
			}
			case 2:
			{
				if (++message_work.all_work[0] >= 1)
				{
					message_work.all_work[0] = 0;
					message_work.all_work[1]++;
				}
				AnimationObject characterAnimationObject8 = AnimationSystem.Instance.CharacterAnimationObject;
				if (characterAnimationObject8.IsPlaying)
				{
					Vector3 localPosition25 = characterAnimationObject8.transform.localPosition;
					localPosition25.y -= message_work.all_work[1] * 4;
					characterAnimationObject8.transform.localPosition = localPosition25;
					AnimationObject animationObject28 = AnimationSystem.Instance.FindObject(2, 0, 220);
					if (animationObject28 != null)
					{
						animationObject28.transform.localPosition = localPosition25;
						if (localPosition25.y >= 240f)
						{
							animationObject28.Stop(true);
						}
					}
					if (localPosition25.y <= -1012f)
					{
						AnimationSystem.Instance.StopCharacters();
					}
				}
				Transform partsTransform3 = bgCtrl.instance.GetPartsTransform(0);
				Vector3 localPosition26 = partsTransform3.localPosition;
				localPosition26.y -= message_work.all_work[1] * 4;
				partsTransform3.localPosition = localPosition26;
				TinamiOfs0 += (float)(int)scroll_para[++furiko_wk_.para[3]] * 0.7f;
				TinamiOfs1 += (float)(int)scroll_para[furiko_wk_.para[3]] * 0.8f;
				GS1_sce4_opening.instance.ChinamiBreak_BG_Scroll(TinamiOfs0, TinamiOfs1);
				furiko_wk_.QuakeTime++;
				if (furiko_wk_.QuakeTime == 10)
				{
					soundCtrl.instance.PlaySE(404);
				}
				if (furiko_wk_.QuakeTime >= 13)
				{
					furiko_wk_.work[7] = 0;
					furiko_wk_.work[5] = 0;
					message_work.all_work[0] = (message_work.all_work[1] = (message_work.all_work[2] = 0));
					furiko_wk_.QuakeTime = 0;
					characterAnimationObject8 = AnimationSystem.Instance.CharacterAnimationObject;
					if (characterAnimationObject8.IsPlaying)
					{
						AnimationSystem.Instance.StopCharacters();
					}
					characterAnimationObject8 = AnimationSystem.Instance.FindObject(2, 0, 220);
					if (characterAnimationObject8 != null)
					{
						characterAnimationObject8.transform.localPosition = Vector3.zero;
						characterAnimationObject8.Z = characterAnimationObject8.DefaultZ;
						characterAnimationObject8.Stop(true);
					}
					furiko_wk_.work[4] = 31;
					message_work.op_para = 3;
				}
				break;
			}
			case 3:
				TinamiOfs0 -= 0.4f;
				TinamiOfs1 -= 1f;
				GS1_sce4_opening.instance.ChinamiBreak_BG_Scroll(TinamiOfs0, TinamiOfs1);
				furiko_wk_.QuakeTime++;
				if (furiko_wk_.work[4] != 0 && furiko_wk_.QuakeTime >= 1)
				{
					furiko_wk_.work[4]--;
					GS1_sce4_opening.instance.SetFrontAlpha(1, (float)(int)furiko_wk_.work[4] / 31f);
				}
				goto case 4;
			case 4:
				if (furiko_wk_.work[4] == 0 && furiko_wk_.QuakeTime >= 31)
				{
					fadeCtrl.instance.play(fadeCtrl.Status.WFADE_OUT, 4u, 1u);
					message_work.op_para = 5;
				}
				break;
			case 5:
				if (fadeCtrl.instance.status == fadeCtrl.Status.NO_FADE)
				{
					GS1_sce4_opening.instance.ChinamiBreakBGEnabled(0, false, true);
					GS1_sce4_opening.instance.ChinamiBreakBGEnabled(1, false, true);
					soundCtrl.instance.PlayBGM(338, 60);
					message_work.op_para = 6;
				}
				break;
			case 6:
				GS1_sce4_opening.instance.ChinamiBreakBGEnabled(0, true);
				GS1_sce4_opening.instance.ChinamiBreakBGEnabled(1, true);
				GS1_sce4_opening.instance.ChinamiBreakBGEnabled(2, true);
				GS1_sce4_opening.instance.SetAlpha(0, 0.5f);
				GS1_sce4_opening.instance.SetAlpha(1, 0.5f);
				GS1_sce4_opening.instance.SetAlpha(2, 0.5f);
				GS1_sce4_opening.instance.SetDepth(0, 0f);
				GS1_sce4_opening.instance.SetDepth(1, 0f);
				GS1_sce4_opening.instance.SetDepth(2, 0f);
				TinamiTimer = 0;
				message_work.op_para = 66;
				break;
			case 66:
				furiko_wk_.QuakeCnt = 650;
				furiko_wk_.FlashCnt = 10;
				message_work.op_para = 14;
				break;
			case 7:
				furiko_wk_.QuakeCnt += 2;
				if (furiko_wk_.QuakeCnt >= Qdata[furiko_wk_.FlashCnt].time)
				{
					num = Qdata[furiko_wk_.FlashCnt].Qy * -1;
					if (++furiko_wk_.FlashCnt > 11)
					{
						furiko_wk_.FlashCnt = 0;
						furiko_wk_.QuakeCnt = 0;
					}
				}
				else
				{
					num = 0f;
				}
				GS1_sce4_opening.instance.ChinamiBreak_SetBGPosX(0, num);
				GS1_sce4_opening.instance.ChinamiBreak_SetBGPosX(1, num);
				GS1_sce4_opening.instance.ChinamiBreak_SetBGPosX(2, num);
				GS1_sce4_opening.instance.SetAlpha(0, s_SprA[TinamiTimer % 30, 0]);
				GS1_sce4_opening.instance.SetAlpha(1, s_SprA[TinamiTimer % 30, 1]);
				GS1_sce4_opening.instance.SetAlpha(2, s_SprA[TinamiTimer % 30, 2]);
				TinamiTimer++;
				if (TinamiTimer >= 30)
				{
					message_work.all_work[0] = 0;
					furiko_wk_.QuakeCnt = 0;
					furiko_wk_.FlashCnt = 0;
					MoveNextScript(message_work);
				}
				break;
			case 10:
			{
				if (++message_work.all_work[2] < 64)
				{
					GS1_sce4_opening.instance.SetAlpha(0, 1f - (float)(int)message_work.all_work[2] / 63f);
					break;
				}
				GS1_sce4_opening.instance.ChinamiBreak_ReloadBG01();
				GS1_sce4_opening.instance.ChinamiBreak_YureiInit();
				TinamiOfs0 = 0f;
				TinamiOfs1 = 0f;
				AnimationObject animationObject29 = AnimationSystem.Instance.FindObject(2, 0, 220);
				if (animationObject29 != null)
				{
					animationObject29.Alpha = 1f;
				}
				animationObject29 = AnimationSystem.Instance.CharacterAnimationObject;
				animationObject29.Alpha = 1f;
				message_work.all_work[0] = 0;
				message_work.all_work[2] = 0;
				furiko_wk_.work[0] = 0;
				furiko_wk_.FlashCnt = 0;
				message_work.op_para = 11;
				break;
			}
			case 11:
			{
				if (++furiko_wk_.QuakeCnt > 11)
				{
					furiko_wk_.QuakeCnt = 0;
				}
				float num10 = Qdata[furiko_wk_.QuakeCnt].Qy;
				GS1_sce4_opening.instance.ChinamiBreak_SetBGPosY(0, -338f - num10 * 6.75f);
				GS1_sce4_opening.instance.ChinamiBreak_SetBGPosY(1, -338f - num10 * 6.75f);
				if (message_work.all_work[2] < 16)
				{
					if (++message_work.all_work[0] >= 5)
					{
						message_work.all_work[0] = 0;
						message_work.all_work[2]++;
						GS1_sce4_opening.instance.SetFrontAlpha(0, (float)(int)message_work.all_work[2] / 63f);
						GS1_sce4_opening.instance.SetFrontAlpha(1, (float)(int)message_work.all_work[2] / 63f);
						AnimationObject characterAnimationObject7 = AnimationSystem.Instance.CharacterAnimationObject;
						characterAnimationObject7.Alpha = 1f - (float)(int)message_work.all_work[2] / 31f;
						characterAnimationObject7 = AnimationSystem.Instance.FindObject(2, 0, 220);
						if (characterAnimationObject7 != null)
						{
							characterAnimationObject7.Alpha = 1f - (float)(int)message_work.all_work[2] / 31f;
						}
					}
				}
				else if (++furiko_wk_.work[0] <= 143)
				{
					if (furiko_wk_.work[0] >= Fdata[furiko_wk_.FlashCnt].time)
					{
						if (Fdata[furiko_wk_.FlashCnt].sw == 1)
						{
							GS1_sce4_opening.instance.SetFrontDepth(0, -31f);
							GS1_sce4_opening.instance.SetFrontDepth(1, -32f);
						}
						else
						{
							GS1_sce4_opening.instance.SetFrontDepth(0, -4f);
							GS1_sce4_opening.instance.SetFrontDepth(1, -3f);
							GS1_sce4_opening.instance.SetFrontAlpha(0, 0.25f);
							GS1_sce4_opening.instance.SetFrontAlpha(1, 0.25f);
						}
						furiko_wk_.FlashCnt++;
					}
				}
				else
				{
					message_work.all_work[0] = 0;
					message_work.op_para = 12;
				}
				break;
			}
			case 12:
				if (++message_work.all_work[0] >= 5)
				{
					soundCtrl.instance.FadeOutSE(391, 5);
					furiko_wk_.QuakeTime = 0;
					ScreenCtrl.instance.Quake(1, 1u);
					message_work.op_para = 13;
					soundCtrl.instance.FadeOutSE(407, 5);
				}
				break;
			case 13:
				furiko_wk_.QuakeTime = 0;
				furiko_wk_.QuakeCnt = 0;
				message_work.all_work[0] = 0;
				message_work.all_work[2] = 0;
				soundCtrl.instance.PlaySE(375);
				soundCtrl.instance.PlaySE(116);
				message_work.op_para = 2;
				break;
			case 14:
				fadeCtrl.instance.play(fadeCtrl.Status.WFADE_IN, 31u, 4u);
				message_work.op_para = 7;
				break;
			}
			break;
		case 107:
			switch (message_work.op_para)
			{
			case 0:
				GS1_sce4_opening.instance.ChinamiBreakInit();
				message_work.op_para = 50;
				break;
			case 50:
				message_work.op_para = 51;
				break;
			case 51:
				message_work.op_para = 52;
				break;
			case 52:
				message_work.op_para = 1;
				break;
			case 1:
				message_work.op_para = 2;
				break;
			case 2:
			{
				bgCtrl.instance.DetentionBgFade(0f);
				GS1_sce4_opening.instance.ChinamiBreakBGEnabled(0, true);
				AnimationObject animationObject17 = AnimationSystem.Instance.FindObject(2, 0, 220);
				if (animationObject17 != null)
				{
					animationObject17.transform.localPosition = AnimationSystem.Instance.CharacterAnimationObject.transform.localPosition;
				}
				MoveNextScript(message_work);
				break;
			}
			}
			break;
		case 120:
			GS1_sce4_opening.instance.ChinamiBreakEnd();
			Exit(message_work);
			break;
		case 108:
			switch (message_work.op_para)
			{
			case 0:
				message_work.op_para = 50;
				break;
			case 50:
				message_work.op_para = 51;
				break;
			case 51:
				furiko_wk_.work[0] = 1;
				furiko_wk_.work[1] = 0;
				furiko_wk_.work[2] = 2;
				furiko_wk_.work[3] = 1;
				furiko_wk_.work[4] = 0;
				furiko_wk_.now_bg = 0;
				furiko_wk_.next_bg = 1;
				furiko_wk_.QuakeCnt = 0;
				furiko_wk_.FlashCnt = 0;
				furiko_wk_.QuakeTime = 0;
				furiko_wk_.DispFlag = 1;
				message_work.all_work[0] = 0;
				message_work.all_work[1] = 0;
				message_work.op_para = 1;
				break;
			}
			furiko_wk_.QuakeCnt += 2;
			if (furiko_wk_.QuakeCnt >= Qdata[furiko_wk_.FlashCnt].time)
			{
				num = Qdata[furiko_wk_.FlashCnt].Qy * -1;
				if (++furiko_wk_.FlashCnt > 11)
				{
					furiko_wk_.FlashCnt = 0;
					furiko_wk_.QuakeCnt = 0;
				}
			}
			else
			{
				num = 0f;
			}
			GS1_sce4_opening.instance.ChinamiBreak_SetBGPosX(0, num);
			GS1_sce4_opening.instance.ChinamiBreak_SetBGPosX(1, num);
			GS1_sce4_opening.instance.ChinamiBreak_SetBGPosX(2, num);
			GS1_sce4_opening.instance.SetAlpha(0, s_SprA[TinamiTimer / 4 % 30, 0]);
			GS1_sce4_opening.instance.SetAlpha(1, s_SprA[TinamiTimer / 4 % 30, 1]);
			GS1_sce4_opening.instance.SetAlpha(2, s_SprA[TinamiTimer / 4 % 30, 2]);
			TinamiTimer++;
			break;
		case 109:
			switch (message_work.op_para)
			{
			case 0:
			{
				bgCtrl.instance.SetSprite(6);
				bgCtrl.instance.DetentionBgFade(0f);
				AnimationObject animationObject21 = AnimationSystem.Instance.PlayCharacter(2, 33, 513, 513);
				Vector3 localPosition19 = animationObject21.transform.localPosition;
				localPosition19.y = -1620f;
				animationObject21.transform.localPosition = localPosition19;
				Transform partsTransform2 = bgCtrl.instance.GetPartsTransform(0);
				localPosition19 = partsTransform2.localPosition;
				localPosition19.y -= 1620f;
				partsTransform2.localPosition = localPosition19;
				furiko_wk_.work[0] = 0;
				message_work.all_work[0] = (message_work.all_work[1] = 0);
				message_work.all_work[2] = 1;
				message_work.op_para = 1;
				break;
			}
			case 1:
			{
				AnimationObject characterAnimationObject4 = AnimationSystem.Instance.CharacterAnimationObject;
				Vector3 localPosition17 = characterAnimationObject4.transform.localPosition;
				if (localPosition17.y >= -520f)
				{
					message_work.all_work[2] = 0;
				}
				if (message_work.all_work[2] == 0)
				{
					if (++message_work.all_work[1] >= 2)
					{
						message_work.all_work[1] = 0;
						localPosition17.y += 13.5f;
					}
				}
				else
				{
					localPosition17.y += (float)(message_work.all_work[2] * 2) * 6.75f;
				}
				if (localPosition17.y > 0f)
				{
					localPosition17.y = 0f;
				}
				AnimationObject animationObject20 = AnimationSystem.Instance.FindObject(2, 0, 215);
				if (animationObject20 == null)
				{
					animationObject20 = AnimationSystem.Instance.PlayObject(2, 0, 215);
				}
				else
				{
					Transform partsTransform = bgCtrl.instance.GetPartsTransform(0);
					Vector3 localPosition18 = partsTransform.localPosition;
					localPosition18.y = localPosition17.y - 540f;
					partsTransform.localPosition = localPosition18;
				}
				if (localPosition17.y >= 0f)
				{
					message_work.all_work[0] = 0;
					message_work.all_work[1] = 0;
					message_work.op_para = 2;
				}
				characterAnimationObject4.transform.localPosition = localPosition17;
				if (animationObject20 != null)
				{
					localPosition17.z = animationObject20.transform.localPosition.z;
					animationObject20.transform.localPosition = localPosition17;
				}
				break;
			}
			case 2:
				if (++message_work.all_work[0] >= 4)
				{
					message_work.all_work[0] = 0;
					message_work.all_work[1]++;
					bgCtrl.instance.DetentionBgFade((float)(int)message_work.all_work[1] * 8.2f / 255f);
					if (message_work.all_work[1] == 31)
					{
						MoveNextScript(message_work);
					}
				}
				break;
			}
			break;
		case 110:
			switch (message_work.op_para)
			{
			case 0:
				message_work.all_work[0] = 0;
				message_work.all_work[1] = 16;
				message_work.op_para = 1;
				break;
			case 1:
				if (++message_work.all_work[0] < 2)
				{
					break;
				}
				message_work.all_work[0] = 0;
				message_work.all_work[1]--;
				if (message_work.all_work[1] == 0)
				{
					AnimationSystem.Instance.StopCharacters();
					AnimationSystem.Instance.CharacterAnimationObject.Alpha = 1f;
					AnimationObject animationObject18 = AnimationSystem.Instance.FindObject(2, 0, 215);
					if (animationObject18 != null)
					{
						animationObject18.Stop(true);
						animationObject18.Alpha = 1f;
					}
					MoveNextScript(message_work);
				}
				else
				{
					AnimationObject characterAnimationObject3 = AnimationSystem.Instance.CharacterAnimationObject;
					float alpha2 = (characterAnimationObject3.Alpha = (float)(int)message_work.all_work[1] / 16f);
					AnimationObject animationObject19 = AnimationSystem.Instance.FindObject(2, 0, 215);
					if (animationObject19 != null)
					{
						animationObject19.Alpha = alpha2;
					}
				}
				break;
			}
			break;
		case 111:
		{
			byte op_para4 = message_work.op_para;
			if (op_para4 != 0)
			{
				if (op_para4 != 1)
				{
					break;
				}
			}
			else
			{
				message_work.all_work[1] = (message_work.all_work[2] = 0);
				message_work.op_para = 1;
			}
			if (++message_work.all_work[2] < message_work.all_work[0])
			{
				break;
			}
			message_work.all_work[2] = 0;
			GS3_BGChange.GS3_MapData gS3_MapData = GS3_BGChange.MapDemo[message_work.all_work[1]];
			ushort num9;
			if (0 < gS3_MapData.st)
			{
				for (num9 = 0; num9 < gS3_MapData.num; num9++)
				{
					bgCtrl.instance.SetSeal(gS3_MapData.st + num9);
				}
			}
			num9 = 0;
			if (GS3_BGChange.MapDemo[++message_work.all_work[1]].num < 0)
			{
				MoveNextScript(message_work);
			}
			break;
		}
		case 112:
		{
			byte op_para2 = message_work.op_para;
			if (op_para2 != 0)
			{
				if (op_para2 != 1)
				{
					break;
				}
			}
			else
			{
				AnimationObject animationObject25 = AnimationSystem.Instance.PlayObject((int)GSStatic.global_work_.title, 0, 210);
				animationObject25.Alpha = 0f;
				Vector3 localPosition24 = animationObject25.transform.localPosition;
				localPosition24.z = AnimationSystem.Instance.CharacterAnimationObject.transform.localPosition.z;
				animationObject25.transform.localPosition = localPosition24;
				message_work.all_work[1] = 0;
				message_work.all_work[2] = 0;
				message_work.op_para = 1;
			}
			if (++message_work.all_work[1] >= message_work.all_work[0])
			{
				message_work.all_work[1] = 0;
				message_work.all_work[2]++;
				AnimationObject animationObject26 = AnimationSystem.Instance.FindObject((int)GSStatic.global_work_.title, 0, 210);
				if (animationObject26 != null)
				{
					animationObject26.Alpha = (float)(int)message_work.all_work[2] / 16f;
				}
				if (message_work.all_work[2] == 16)
				{
					MoveNextScript(message_work);
				}
			}
			break;
		}
		case 113:
			switch (message_work.op_para)
			{
			case 0:
			{
				GS1_sce4_opening.instance.GodoFadeInit();
				AnimationObject animationObject24 = AnimationSystem.Instance.FindObject((int)GSStatic.global_work_.title, 0, 210);
				animationObject24.Z = animationObject24.DefaultZ;
				animationObject24.Stop(true);
				message_work.op_para = 1;
				break;
			}
			case 1:
			{
				bgCtrl.instance.SetSprite(5);
				AnimationObject animationObject23 = AnimationSystem.Instance.PlayObject((int)GSStatic.global_work_.title, 0, 223);
				Vector3 localPosition23 = animationObject23.transform.localPosition;
				localPosition23.z = AnimationSystem.Instance.CharacterAnimationObject.transform.localPosition.z;
				animationObject23.transform.localPosition = localPosition23;
				message_work.all_work[1] = 0;
				message_work.all_work[2] = 0;
				message_work.op_para = 2;
				break;
			}
			case 2:
				if (++message_work.all_work[1] >= message_work.all_work[0])
				{
					message_work.all_work[1] = 0;
					message_work.all_work[2]++;
					GS1_sce4_opening.instance.SetAlpha(0, 1f - (float)(int)message_work.all_work[2] / 31f);
					GS1_sce4_opening.instance.SetAlpha(1, 1f - (float)(int)message_work.all_work[2] / 31f);
					if (message_work.all_work[2] == 31)
					{
						GS1_sce4_opening.instance.GodoFadeExit();
						AnimationObject animationObject22 = AnimationSystem.Instance.FindObject((int)GSStatic.global_work_.title, 0, 223);
						animationObject22.Z = animationObject22.DefaultZ;
						animationObject22.Stop(true);
						AnimationSystem.Instance.PlayCharacter((int)GSStatic.global_work_.title, 17, 257, 257);
						MoveNextScript(message_work);
					}
				}
				break;
			}
			break;
		case 150:
			switch (message_work.op_para)
			{
			case 0:
			{
				fadeCtrl.instance.play(fadeCtrl.Status.FADE_IN, 0u, 0u);
				AnimationObject characterAnimationObject5 = AnimationSystem.Instance.CharacterAnimationObject;
				Vector3 localPosition20 = characterAnimationObject5.transform.localPosition;
				localPosition20.x = -1980f;
				characterAnimationObject5.transform.localPosition = localPosition20;
				bgCtrl.instance.SetEndingParts(1);
				endingMaskCtrl.instance.SetMask();
				message_work.op_para = 1;
				goto case 1;
			}
			case 1:
			{
				float num7 = 0f;
				float num8 = (float)(int)message_work.all_work[2] * 6.75f;
				AnimationObject characterAnimationObject6 = AnimationSystem.Instance.CharacterAnimationObject;
				Vector3 localPosition21 = characterAnimationObject6.transform.localPosition;
				Vector3 localPosition22 = bgCtrl.instance.ending_table_trans.localPosition;
				if (localPosition21.x + num8 > num7)
				{
					localPosition21.x = 0f;
					localPosition22.x = 0f;
					message_work.op_para = 20;
				}
				else
				{
					localPosition21.x += num8;
					localPosition22.x += num8;
				}
				characterAnimationObject6.transform.localPosition = localPosition21;
				bgCtrl.instance.ending_table_trans.localPosition = localPosition22;
				break;
			}
			case 20:
				MoveNextScript(message_work);
				break;
			case 2:
				endingMaskCtrl.instance.ResetMask();
				bgCtrl.instance.ResetEndingParts();
				Exit(message_work);
				break;
			case 3:
				Exit(message_work);
				break;
			}
			break;
		case 151:
		{
			byte op_para = message_work.op_para;
			if (op_para != 0)
			{
				if (op_para != 1)
				{
					if (op_para == 20)
					{
						MoveNextScript(message_work);
					}
					break;
				}
			}
			else
			{
				fadeCtrl.instance.play(fadeCtrl.Status.FADE_IN, 0u, 0u);
				AnimationObject characterAnimationObject = AnimationSystem.Instance.CharacterAnimationObject;
				Vector3 localPosition7 = characterAnimationObject.transform.localPosition;
				localPosition7.x = 1980f;
				characterAnimationObject.transform.localPosition = localPosition7;
				bgCtrl.instance.SetEndingParts(2);
				endingMaskCtrl.instance.SetMask();
				message_work.op_para = 1;
			}
			float num2 = 0f;
			float num3 = (float)(int)message_work.all_work[2] * 6.75f;
			AnimationObject characterAnimationObject2 = AnimationSystem.Instance.CharacterAnimationObject;
			Vector3 localPosition8 = characterAnimationObject2.transform.localPosition;
			Vector3 localPosition9 = bgCtrl.instance.ending_table_trans.localPosition;
			if (localPosition8.x - num3 < num2)
			{
				localPosition8.x = 0f;
				localPosition9.x = 0f;
				message_work.op_para = 20;
			}
			else
			{
				localPosition8.x -= num3;
				localPosition9.x -= num3;
			}
			characterAnimationObject2.transform.localPosition = localPosition8;
			bgCtrl.instance.ending_table_trans.localPosition = localPosition9;
			break;
		}
		case 152:
			switch (message_work.op_para)
			{
			case 0:
				fadeCtrl.instance.play(fadeCtrl.Status.FADE_OUT, message_work.all_work[0], message_work.all_work[1]);
				message_work.all_work[0] = 0;
				message_work.all_work[1] = 31;
				message_work.op_para = 1;
				break;
			case 1:
				if (fadeCtrl.instance.status == fadeCtrl.Status.NO_FADE)
				{
					AnimationSystem.Instance.StopCharacters();
					MoveNextScript(message_work);
				}
				break;
			}
			break;
		case 153:
			switch (message_work.op_para)
			{
			case 0:
				bgCtrl.instance.SetReserveSprite();
				if (AnimationSystem.Instance.CharacterAnimationObject.IsMonochrom())
				{
					spefCtrl.instance.Monochrome_set(3, 1, 31);
				}
				fadeCtrl.instance.play(fadeCtrl.Status.FADE_IN, message_work.all_work[0], message_work.all_work[1]);
				message_work.all_work[0] = 0;
				message_work.all_work[1] = 0;
				message_work.op_para = 1;
				break;
			case 1:
				if (fadeCtrl.instance.status == fadeCtrl.Status.NO_FADE)
				{
					MoveNextScript(message_work);
				}
				break;
			}
			break;
		case 154:
			switch (message_work.op_para)
			{
			case 0:
				message_work.all_work[0] = 0;
				message_work.all_work[1] = 0;
				message_work.all_work[2] = 0;
				fadeCtrl.instance.play(fadeCtrl.Status.WFADE_OUT, message_work.all_work[0], message_work.all_work[1]);
				message_work.op_para = 1;
				break;
			case 1:
				if (fadeCtrl.instance.status == fadeCtrl.Status.NO_FADE)
				{
					MoveNextScript(message_work);
				}
				break;
			}
			break;
		case 155:
			switch (message_work.op_para)
			{
			case 0:
				bgCtrl.instance.SetReserveSprite();
				spefCtrl.instance.Monochrome_set(3, 1, 31);
				fadeCtrl.instance.play(fadeCtrl.Status.WFADE_IN, message_work.all_work[0], message_work.all_work[1]);
				message_work.all_work[0] = 0;
				message_work.all_work[1] = 31;
				message_work.op_para = 1;
				break;
			case 1:
				if (fadeCtrl.instance.status == fadeCtrl.Status.NO_FADE)
				{
					MoveNextScript(message_work);
				}
				break;
			}
			break;
		case 156:
			if (message_work.op_para == 0)
			{
				AnimationSystem.Instance.Char_monochrome(1, 31, 0, true);
				Exit(message_work);
			}
			break;
		case 157:
			bgCtrl.instance.SetSprite(4095);
			Exit(message_work);
			break;
		case 158:
			switch (message_work.op_para)
			{
			case 0:
				if (message_work.all_work[2] == 0)
				{
					GSStatic.tantei_work_.tanchiki_demof = 1;
					break;
				}
				GSStatic.tantei_work_.tanchiki_demof = 0;
				message_work.op_flg = 0;
				Exit(message_work);
				break;
			case 1:
			{
				if (GSStatic.tantei_work_.tanchiki_demof != 2)
				{
					return;
				}
				Vector3 cursor_position = MiniGameCursor.instance.cursor_position;
				switch (message_work.all_work[0])
				{
				case 0:
					cursor_position.x -= 30f;
					MiniGameCursor.instance.cursor_position = cursor_position;
					break;
				case 1:
					cursor_position.x += 30f;
					MiniGameCursor.instance.cursor_position = cursor_position;
					break;
				case 2:
					cursor_position.y -= 22.5f;
					MiniGameCursor.instance.cursor_position = cursor_position;
					break;
				case 3:
					cursor_position.y += 22.5f;
					MiniGameCursor.instance.cursor_position = cursor_position;
					break;
				}
				switch (message_work.all_work[0])
				{
				case 0:
					if (cursor_position.x > (float)(int)message_work.all_work[1] * 7.5f)
					{
						return;
					}
					cursor_position.x = (float)(int)message_work.all_work[1] * 7.5f;
					MiniGameCursor.instance.cursor_position = cursor_position;
					break;
				case 1:
					if (cursor_position.x < (float)(int)message_work.all_work[1] * 7.5f)
					{
						return;
					}
					cursor_position.x = (float)(int)message_work.all_work[1] * 7.5f;
					MiniGameCursor.instance.cursor_position = cursor_position;
					break;
				case 2:
					if (cursor_position.y > (float)(int)message_work.all_work[1] * 5.625f)
					{
						return;
					}
					cursor_position.y = (float)(int)message_work.all_work[1] * 5.625f;
					MiniGameCursor.instance.cursor_position = cursor_position;
					break;
				case 3:
					if (cursor_position.y < (float)(int)message_work.all_work[1] * 5.625f)
					{
						return;
					}
					cursor_position.y = (float)(int)message_work.all_work[1] * 5.625f;
					MiniGameCursor.instance.cursor_position = cursor_position;
					break;
				}
				if (1920f < cursor_position.x)
				{
					GSStatic.tantei_work_.tanchiki_demof = 3;
				}
				message_work.op_flg &= 240;
				message_work.mdt_index_ += 3u;
				message_work.op_para = 90;
				message_work.all_work[0] = 50;
				break;
			}
			case 4:
				message_work.all_work[2]++;
				if (message_work.all_work[0] > 0)
				{
					if (message_work.all_work[2] == 3)
					{
						message_work.all_work[0]--;
						message_work.all_work[2] = 0;
						MiniGameCursor.instance.cursor_position += new Vector3(7.5f, 0f, 0f);
					}
				}
				else
				{
					message_work.op_flg &= 240;
					message_work.mdt_index_ += 3u;
					message_work.op_para = 90;
					message_work.all_work[0] = 50;
				}
				break;
			}
			break;
		case 159:
			MoveNextScript(message_work);
			break;
		case 98:
			DemoProc_Special(message_work);
			break;
		case 0:
			GSStatic.ClearDemo_buff();
			Exit(message_work);
			break;
		}
		if ((message_work.op_workno < 99 || message_work.op_workno == 198) && op5_wk_.OBJ_scroll != 0)
		{
			GSDemo_gs3_op5_mask.instance.op05_scroll_down(op5_wk_);
		}
	}

	private static void OpFirstDemoProc(MessageWork message_work)
	{
	}

	private static void OpSecondDemoProc(MessageWork message_work)
	{
	}

	public static void Exit(MessageWork message_work)
	{
		message_work.op_work[0] = (message_work.op_work[1] = (message_work.op_work[2] = (message_work.op_work[3] = (message_work.op_work[4] = (message_work.op_work[5] = (message_work.op_work[6] = (message_work.op_work[7] = 0)))))));
		message_work.op_workno = 99;
	}

	public static void MoveNextScript(MessageWork message_work)
	{
		message_work.op_work[0] = 0;
		message_work.op_work[1] = 0;
		Exit(message_work);
		message_work.op_flg &= 240;
		message_work.mdt_index += 3u;
	}

	private static void FadeOutObj(MessageWork message_work, ushort no)
	{
		AnimationObject animationObject = null;
		if (++message_work.op_work[1] > 30)
		{
			animationObject = AnimationSystem.Instance.FindObject((int)GSStatic.global_work_.title, 0, no);
			if (animationObject != null)
			{
				animationObject.Stop(true);
			}
			MoveNextScript(message_work);
			return;
		}
		uint num = (uint)message_work.op_work[1] / 2u;
		if (num > 15)
		{
			num = 15u;
		}
		animationObject = AnimationSystem.Instance.FindObject((int)GSStatic.global_work_.title, 0, no);
		if (animationObject != null)
		{
			animationObject.Alpha = (float)(15 - num) / 15f;
		}
	}

	private static void FadeInObj(MessageWork message_work, ushort no)
	{
		AnimationObject animationObject = null;
		if (++message_work.op_work[1] > 30)
		{
			MoveNextScript(message_work);
			return;
		}
		uint num = (uint)message_work.op_work[1] / 2u;
		if (num > 15)
		{
			num = 15u;
		}
		animationObject = AnimationSystem.Instance.FindObject((int)GSStatic.global_work_.title, 0, no);
		if (animationObject != null)
		{
			animationObject.Alpha = (float)num / 15f;
		}
	}

	private static void Character_MoveH(MessageWork message_work)
	{
		AnimationObject characterAnimationObject = AnimationSystem.Instance.CharacterAnimationObject;
		if (characterAnimationObject == null)
		{
			MoveNextScript(message_work);
		}
		Vector3 localPosition = characterAnimationObject.transform.localPosition;
		float num = (float)(message_work.all_work[0] - 128) * 5.625f;
		ushort num2 = message_work.all_work[1];
		float num3 = (float)(int)message_work.all_work[2] * 5.625f;
		if (num2 != 0)
		{
			if (localPosition.x + num3 > num)
			{
				localPosition.x = num;
				MoveNextScript(message_work);
			}
			else
			{
				localPosition.x += num3;
			}
		}
		else if (localPosition.x - num3 < num)
		{
			localPosition.x = num;
			MoveNextScript(message_work);
		}
		else
		{
			localPosition.x -= num3;
		}
		characterAnimationObject.transform.localPosition = localPosition;
	}

	public static void DemoProc_Special(MessageWork message_work)
	{
		GlobalWork global_work_ = GSStatic.global_work_;
		ObjWork objWork = null;
		switch (message_work.op_para)
		{
		case 0:
		{
			AnimationObject characterAnimationObject2 = AnimationSystem.Instance.CharacterAnimationObject;
			uint num18 = 0u;
			switch (characterAnimationObject2.ObjectFOA)
			{
			case 448:
				num18 = 447u;
				break;
			case 154:
				num18 = 144u;
				break;
			case 144:
				num18 = 154u;
				break;
			}
			if (num18 != 0)
			{
				AnimationSystem.Instance.PlayTalkCharacter((int)num18);
			}
			Exit(message_work);
			break;
		}
		case 1:
			Exit(message_work);
			break;
		case 2:
			Exit(message_work);
			break;
		case 3:
		{
			if (GSStatic.global_work_.title == TitleId.GS3 && global_work_.scenario == 6)
			{
				Exit(message_work);
				break;
			}
			AnimationObject characterAnimationObject = AnimationSystem.Instance.CharacterAnimationObject;
			Vector3 localPosition11 = characterAnimationObject.transform.localPosition;
			localPosition11.x = (float)(message_work.all_work[0] - 128) * 5.625f;
			localPosition11.y = (float)(-(message_work.all_work[1] - 96)) * 5.625f;
			if (GSStatic.global_work_.title == TitleId.GS3 && GSStatic.global_work_.r.no_0 == 4 && (long)AnimationSystem.Instance.CharacterAnimationObject.CharacterID == 32 && localPosition11.y < 0f)
			{
				localPosition11.y += 35f;
			}
			characterAnimationObject.transform.localPosition = localPosition11;
			Exit(message_work);
			break;
		}
		case 4:
			Character_MoveH(message_work);
			break;
		case 5:
			break;
		case 6:
			switch (message_work.op_work[0])
			{
			case 0:
				endingMaskCtrl.instance.SetAlpha(0f);
				message_work.op_work[1] = 0;
				message_work.op_work[0] = 1;
				break;
			case 1:
			{
				int num11 = 15 * message_work.all_work[0];
				if (++message_work.op_work[1] > num11)
				{
					MoveNextScript(message_work);
					break;
				}
				float num12 = (float)(int)message_work.op_work[1] / (float)num11;
				if (num12 > 1f)
				{
					num12 = 1f;
				}
				endingMaskCtrl.instance.SetAlpha(num12);
				break;
			}
			}
			break;
		case 7:
			Exit(message_work);
			break;
		case 8:
			Exit(message_work);
			break;
		case 9:
			message_work.status &= ~MessageSystem.Status.SELECT;
			Exit(message_work);
			break;
		case 10:
			messageBoardCtrl.instance.SetAlphaText(1f);
			message_work.status |= MessageSystem.Status.MOJI_SEMITRANS2;
			Exit(message_work);
			break;
		case 11:
			messageBoardCtrl.instance.SetAlphaText(1f);
			message_work.status &= ~MessageSystem.Status.MOJI_SEMITRANS2;
			Exit(message_work);
			break;
		case 12:
			switch (message_work.op_work[0])
			{
			case 0:
				message_work.op_work[1] = 0;
				message_work.op_work[0] = 1;
				break;
			case 1:
			{
				int num5 = 15 * message_work.all_work[0];
				if (++message_work.op_work[1] > num5)
				{
					MoveNextScript(message_work);
					break;
				}
				float num6 = 1f - (float)(int)message_work.op_work[1] / (float)num5;
				if (num6 < 0f)
				{
					num6 = 0f;
				}
				break;
			}
			}
			break;
		case 13:
			switch (message_work.op_work[0])
			{
			case 0:
				messageBoardCtrl.instance.SetAlphaText(0f);
				message_work.op_work[1] = 0;
				message_work.op_work[0] = 1;
				break;
			case 1:
			{
				int num = 15 * message_work.all_work[0];
				if (++message_work.op_work[1] > num)
				{
					MoveNextScript(message_work);
					break;
				}
				float num2 = (float)(int)message_work.op_work[1] / (float)num;
				if (num2 > 1f)
				{
					num2 = 1f;
				}
				messageBoardCtrl.instance.SetAlphaText(num2);
				break;
			}
			}
			break;
		case 14:
			switch (message_work.op_work[0])
			{
			case 0:
				messageBoardCtrl.instance.SetAlphaText(1f);
				message_work.op_work[1] = 0;
				message_work.op_work[0] = 1;
				break;
			case 1:
			{
				int num3 = 15 * message_work.all_work[0];
				if (++message_work.op_work[1] > num3)
				{
					MoveNextScript(message_work);
					break;
				}
				float num4 = 1f - (float)(int)message_work.op_work[1] / (float)num3;
				if (num4 < 0f)
				{
					num4 = 0f;
				}
				messageBoardCtrl.instance.SetAlphaText(num4);
				break;
			}
			}
			break;
		case 15:
			switch (message_work.op_work[0])
			{
			case 0:
				message_work.op_work[1] = 0;
				message_work.op_work[0] = 1;
				global_work_.fade_time = (byte)message_work.all_work[0];
				global_work_.fade_speed = (byte)message_work.all_work[1];
				message_work.all_work[0] = 1;
				break;
			case 1:
				if (++message_work.op_work[1] > 24 * message_work.all_work[0])
				{
					message_work.op_work[0] = 2;
					fadeCtrl.instance.play(2u, global_work_.fade_time, global_work_.fade_speed);
				}
				else
				{
					uint num15 = (uint)(message_work.op_work[1] / message_work.all_work[0]);
				}
				break;
			case 2:
				if (fadeCtrl.instance.status != 0)
				{
					break;
				}
				switch (GSStatic.global_work_.title)
				{
				case TitleId.GS1:
					if (objWork != null)
					{
					}
					if (objWork == null)
					{
					}
					break;
				case TitleId.GS2:
					if (objWork != null)
					{
					}
					if (objWork == null)
					{
					}
					break;
				case TitleId.GS3:
					if (objWork != null)
					{
					}
					if (objWork == null)
					{
					}
					break;
				}
				MoveNextScript(message_work);
				break;
			}
			break;
		case 16:
			switch (message_work.op_work[0])
			{
			case 0:
			{
				if ((message_work.status2 & MessageSystem.Status2.MOSAIC_FLAG) == 0)
				{
					fadeCtrl.instance.play(fadeCtrl.Status.FADE_OUT, 17u, 17u);
				}
				message_work.status2 |= MessageSystem.Status2.MOSAIC_FLAG;
				ushort num16 = (ushort)(message_work.op_work[5] / 2 * 17);
				if (num16 > 255)
				{
					num16 = 255;
				}
				message_work.status |= MessageSystem.Status.OBJ_MOSAIC;
				message_work.op_work[5]++;
				if (message_work.op_work[5] == 30)
				{
					if (bgCtrl.instance.bg_no_reserve != 65535)
					{
						bgCtrl.instance.SetReserveSprite(false);
					}
				}
				else
				{
					if (message_work.op_work[5] <= 30)
					{
						break;
					}
					if (message_work.all_work[0] != 0)
					{
						AnimationSystem instance3 = AnimationSystem.Instance;
						instance3.StopCharacters();
						instance3.PlayCharacter((int)GSStatic.global_work_.title, message_work.all_work[0], message_work.all_work[1], message_work.all_work[1]);
						GSStatic.tantei_work_.person_flag = 1;
						GSStatic.obj_work_[1].h_num = (byte)message_work.all_work[0];
						GSStatic.obj_work_[1].foa = message_work.all_work[1];
						GSStatic.obj_work_[1].idlingFOA = 0;
						global_work_.Def_talk_foa = message_work.all_work[1];
						global_work_.Def_wait_foa = message_work.all_work[1];
						if ((message_work.all_work[2] & 0xF0u) != 0)
						{
							num16 = (ushort)((message_work.all_work[2] & 0xF0) >> 4);
							if (num16 == 1)
							{
								AnimationSystem.Instance.Char_monochrome(1, 31, 0, true);
							}
						}
					}
					else
					{
						AnimationSystem.Instance.StopCharacters();
					}
					if ((message_work.status2 & MessageSystem.Status2.RESTORE_PSY) != 0)
					{
						message_work.status2 &= ~MessageSystem.Status2.RESTORE_PSY;
						GSPsylock.Set_Psylock_Mode(9, 0);
						GSPsylock.PsylockDisp_move();
					}
					else
					{
						GSPsyLockControl.instance.ChainAnimationDelete();
						if ((message_work.status2 & MessageSystem.Status2.BG0_PROTECT) != 0)
						{
							if (GSStatic.global_work_.r.no_0 == 5 && GSStatic.global_work_.r.no_1 == 11)
							{
								GSPsylock.KeyObjDelete();
							}
							message_work.status2 &= ~MessageSystem.Status2.BG0_PROTECT;
						}
						if ((message_work.all_work[2] & 0xFu) != 0)
						{
							num16 = (ushort)(message_work.all_work[2] & 0xFu);
							if (num16 == 1)
							{
								spefCtrl.instance.Monochrome_set(7, 1, 31);
							}
						}
						else if ((ushort)(message_work.all_work[2] & 0xF) == 0)
						{
							spefCtrl.instance.Monochrome_set(8, 1, 1);
						}
						if ((message_work.all_work[2] & 0xF00u) != 0)
						{
							switch ((ushort)((message_work.all_work[2] & 0xF00) >> 8))
							{
							case 4:
								if (((uint)bgCtrl.instance.Bg256_SP_Flag & (true ? 1u : 0u)) != 0)
								{
									bgCtrl.instance.Bg256_SP_Flag &= 254;
								}
								bgCtrl.instance.ChangeSubSpriteEnable(true);
								break;
							}
						}
						ushort num17 = (ushort)((message_work.all_work[2] & 0xF000) >> 12);
						if (num17 != 2 && num17 == 4)
						{
							bgCtrl.instance.Bg256_set_ex2((uint)bgCtrl.instance.bg_no, 16u);
							CheckBGChange((uint)bgCtrl.instance.bg_no, 0u);
						}
					}
					message_work.op_work[5] = 29;
					message_work.op_work[0] = 1;
					fadeCtrl.instance.play(fadeCtrl.Status.FADE_IN, 17u, 17u);
				}
				break;
			}
			case 1:
			{
				ushort num16 = (ushort)(message_work.op_work[5] / 2 * 17);
				if (num16 > 255)
				{
					num16 = 255;
				}
				if (message_work.op_work[5] > 0)
				{
					message_work.op_work[5]--;
				}
				else
				{
					message_work.op_work[0] = 2;
				}
				break;
			}
			case 2:
				message_work.status &= ~MessageSystem.Status.OBJ_MOSAIC;
				message_work.status2 &= ~MessageSystem.Status2.MOSAIC_FLAG;
				message_work.status2 &= ~MessageSystem.Status2.DISABLE_FONT_ALPHA;
				message_work.op_flg &= 240;
				message_work.mdt_index += 3u;
				Exit(message_work);
				break;
			}
			break;
		case 17:
			switch (message_work.op_work[0])
			{
			case 0:
				if ((message_work.op_flg & 0xF0) == 0 || GSStatic.global_work_.title != TitleId.GS3 || objWork != null)
				{
				}
				message_work.op_work[0] = 1;
				message_work.op_work[1] = 0;
				if (GSStatic.global_work_.title != TitleId.GS2)
				{
				}
				break;
			case 1:
				if (++message_work.op_work[1] >= 22)
				{
					if (message_work.all_work[0] != 0)
					{
						soundCtrl.instance.PlaySE(111);
					}
					else
					{
						soundCtrl.instance.PlaySE(58);
					}
					if ((global_work_.status_flag & (true ? 1u : 0u)) != 0 || message_work.message_type == WindowType.MAIN)
					{
					}
					global_work_.status_flag |= 1u;
					message_work.op_flg &= 240;
					message_work.mdt_index += 3u;
					message_work.op_work[0] = (message_work.op_work[1] = (message_work.op_work[2] = (message_work.op_work[3] = (message_work.op_work[4] = (message_work.op_work[5] = (message_work.op_work[6] = 0))))));
					message_work.op_work[7] = 65534;
					message_work.op_workno = 99;
					int foreSprite4 = 0;
					switch (GSStatic.global_work_.title)
					{
					case TitleId.GS1:
						foreSprite4 = 28;
						break;
					case TitleId.GS2:
						foreSprite4 = 13;
						break;
					case TitleId.GS3:
						foreSprite4 = 11;
						break;
					}
					bgCtrl.instance.SetForeSprite(foreSprite4);
				}
				break;
			}
			break;
		case 18:
			if (GSStatic.global_work_.title == TitleId.GS2)
			{
			}
			bgCtrl.instance.DeleteForeSprite();
			switch (GSStatic.global_work_.title)
			{
			case TitleId.GS1:
				if (objWork != null)
				{
				}
				if (objWork == null)
				{
				}
				break;
			case TitleId.GS2:
				if (objWork != null)
				{
				}
				if (objWork == null)
				{
				}
				break;
			case TitleId.GS3:
				if (objWork != null)
				{
				}
				if (objWork == null)
				{
				}
				break;
			}
			Exit(message_work);
			break;
		case 19:
			switch (message_work.op_work[0])
			{
			case 0:
				message_work.op_work[0] = 1;
				message_work.op_work[1] = 0;
				break;
			case 1:
				if (++message_work.op_work[1] >= 22)
				{
					if (message_work.all_work[0] == 0)
					{
						soundCtrl.instance.PlaySE(58);
					}
					if ((global_work_.status_flag & (true ? 1u : 0u)) != 0 || message_work.message_type == WindowType.MAIN)
					{
					}
					global_work_.status_flag |= 1u;
					message_work.op_work[0] = 2;
					int foreSprite2 = 0;
					switch (GSStatic.global_work_.title)
					{
					case TitleId.GS1:
						foreSprite2 = 28;
						break;
					case TitleId.GS2:
						foreSprite2 = 13;
						break;
					case TitleId.GS3:
						foreSprite2 = 11;
						break;
					}
					bgCtrl.instance.SetForeSprite(foreSprite2);
				}
				break;
			case 2:
				if (++message_work.op_work[1] >= 32)
				{
					message_work.op_work[0] = 3;
					bgCtrl.instance.DeleteForeSprite();
				}
				break;
			case 3:
				if (++message_work.op_work[1] >= 44)
				{
					if (message_work.all_work[0] == 0)
					{
						soundCtrl.instance.PlaySE(58);
					}
					if ((global_work_.status_flag & (true ? 1u : 0u)) != 0 || message_work.message_type == WindowType.MAIN)
					{
					}
					global_work_.status_flag |= 1u;
					message_work.op_work[0] = 4;
					int foreSprite3 = 0;
					switch (GSStatic.global_work_.title)
					{
					case TitleId.GS1:
						foreSprite3 = 28;
						break;
					case TitleId.GS2:
						foreSprite3 = 13;
						break;
					case TitleId.GS3:
						foreSprite3 = 11;
						break;
					}
					bgCtrl.instance.SetForeSprite(foreSprite3);
				}
				break;
			case 4:
				if (++message_work.op_work[1] >= 54)
				{
					message_work.op_work[0] = 5;
					bgCtrl.instance.DeleteForeSprite();
				}
				break;
			case 5:
				if (++message_work.op_work[1] >= 67)
				{
					if (message_work.all_work[0] == 0)
					{
						soundCtrl.instance.PlaySE(58);
					}
					if ((global_work_.status_flag & (true ? 1u : 0u)) != 0 || message_work.message_type == WindowType.MAIN)
					{
					}
					global_work_.status_flag |= 1u;
					message_work.op_flg = 0;
					message_work.mdt_index += 3u;
					message_work.op_work[0] = (message_work.op_work[1] = (message_work.op_work[2] = (message_work.op_work[3] = (message_work.op_work[4] = (message_work.op_work[5] = (message_work.op_work[6] = 0))))));
					message_work.op_work[7] = 65534;
					message_work.op_workno = 99;
					int foreSprite = 0;
					switch (GSStatic.global_work_.title)
					{
					case TitleId.GS1:
						foreSprite = 28;
						break;
					case TitleId.GS2:
						foreSprite = 13;
						break;
					case TitleId.GS3:
						foreSprite = 11;
						break;
					}
					bgCtrl.instance.SetForeSprite(foreSprite);
				}
				break;
			}
			break;
		case 21:
			switch (message_work.op_work[0])
			{
			case 0:
				message_work.op_work[1] = 2;
				message_work.op_work[0] = 1;
				if (GSStatic.global_work_.title == TitleId.GS3)
				{
					for (uint num15 = 0u; num15 < 3; num15++)
					{
						AnimationObject animationObject12 = AnimationSystem.Instance.FindObject(2, 0, (int)(114 + num15));
						if (animationObject12 != null)
						{
							animationObject12.gameObject.SetActive(false);
						}
					}
				}
				AnimationSystem.Instance.CharFade(4, 2);
				break;
			case 1:
				if (!AnimationSystem.Instance.isFade(4))
				{
					AnimationSystem.Instance.ChangeInstantlyAlpha(true);
					AnimationSystem.Instance.CtrlChinamiObj(0);
					AnimationSystem.Instance.StopCharacters();
					if (message_work.choustate != 5 && message_work.choustate != 6)
					{
						message_work.choustateBK = message_work.choustate;
						message_work.choustate = 6;
					}
					MoveNextScript(message_work);
				}
				break;
			}
			break;
		case 22:
			switch (message_work.op_work[0])
			{
			case 0:
				message_work.op_work[1] = 2;
				message_work.op_work[0] = 1;
				AnimationSystem.Instance.PlayCharacter(2, message_work.all_work[1], message_work.all_work[2], message_work.all_work[2]);
				GSStatic.obj_work_[1].h_num = 0;
				GSStatic.obj_work_[1].foa = message_work.all_work[2];
				GSStatic.obj_work_[1].idlingFOA = message_work.all_work[2];
				AnimationSystem.Instance.ChangeInstantlyAlpha(false);
				AnimationSystem.Instance.CharFade(1, 2);
				break;
			case 1:
				if (!AnimationSystem.Instance.isFade(1))
				{
					AnimationSystem.Instance.ChangeInstantlyAlpha(true);
					message_work.op_work[0] = 2;
					message_work.op_work[1] = 0;
					message_work.all_work[0] = 1;
				}
				else
				{
					if (GSStatic.global_work_.title != TitleId.GS3)
					{
						break;
					}
					for (uint num15 = 0u; num15 < 3; num15++)
					{
						AnimationObject animationObject11 = AnimationSystem.Instance.FindObject(2, 0, (int)(114 + num15));
						if (animationObject11 != null)
						{
							animationObject11.Alpha = 0f;
						}
					}
				}
				break;
			case 2:
				if (++message_work.op_work[1] < 7 * message_work.all_work[0])
				{
					break;
				}
				if (GSStatic.global_work_.title == TitleId.GS3 && message_work.choustate == 6)
				{
					message_work.choustate = message_work.choustateBK;
					for (uint num15 = 0u; num15 < 3; num15++)
					{
						AnimationObject animationObject10 = AnimationSystem.Instance.FindObject(2, 0, (int)(114 + num15));
						if (animationObject10 != null)
						{
							animationObject10.gameObject.SetActive(true);
							animationObject10.Alpha = 1f;
						}
					}
				}
				MoveNextScript(message_work);
				break;
			}
			break;
		case 23:
			switch (message_work.op_work[0])
			{
			case 0:
				global_work_.fade_time = (byte)message_work.all_work[0];
				global_work_.fade_speed = (byte)message_work.all_work[1];
				fadeCtrl.instance.play(2u, global_work_.fade_time, global_work_.fade_speed);
				message_work.op_work[0] = 1;
				break;
			case 1:
				if (fadeCtrl.instance.status == fadeCtrl.Status.NO_FADE)
				{
					if ((message_work.all_work[2] & 0x10u) != 0)
					{
					}
					MoveNextScript(message_work);
				}
				break;
			}
			break;
		case 24:
			switch (message_work.op_work[0])
			{
			case 0:
				global_work_.fade_time = (byte)message_work.all_work[0];
				global_work_.fade_speed = (byte)message_work.all_work[1];
				fadeCtrl.instance.play(1u, global_work_.fade_time, global_work_.fade_speed);
				message_work.op_work[0] = 1;
				break;
			case 1:
				if (fadeCtrl.instance.status == fadeCtrl.Status.NO_FADE)
				{
					message_work.op_work[1] = 0;
					message_work.all_work[1] = 1;
					message_work.op_work[0] = 2;
				}
				break;
			case 2:
				if (++message_work.op_work[1] >= 7 * message_work.all_work[1])
				{
					MoveNextScript(message_work);
				}
				else
				{
					uint num15 = (ushort)(message_work.op_work[1] / message_work.all_work[1]);
				}
				break;
			}
			break;
		case 25:
			switch (message_work.op_work[0])
			{
			case 0:
				fadeCtrl.instance.play(4u, message_work.all_work[1], message_work.all_work[0]);
				message_work.op_work[0] = 1;
				break;
			case 1:
				if (fadeCtrl.instance.status == fadeCtrl.Status.NO_FADE)
				{
					MoveNextScript(message_work);
				}
				break;
			}
			break;
		case 26:
			switch (message_work.op_work[0])
			{
			case 0:
				fadeCtrl.instance.play(3u, message_work.all_work[1], message_work.all_work[0]);
				message_work.op_work[0] = 1;
				break;
			case 1:
				if (fadeCtrl.instance.status == fadeCtrl.Status.NO_FADE)
				{
					MoveNextScript(message_work);
				}
				break;
			case 2:
				if (++message_work.op_work[1] >= 7 * message_work.all_work[1])
				{
					MoveNextScript(message_work);
				}
				else
				{
					uint num15 = (ushort)(message_work.op_work[1] / message_work.all_work[1]);
				}
				break;
			}
			break;
		case 27:
			message_work.choustate = 1;
			Exit(message_work);
			break;
		case 28:
			message_work.choustate = 3;
			Exit(message_work);
			break;
		case 29:
			message_work.choustate = 8;
			Exit(message_work);
			break;
		case 30:
			message_work.choustate = 4;
			Exit(message_work);
			break;
		case 32:
			message_work.op_work[0] = (message_work.op_work[1] = (message_work.op_work[2] = (message_work.op_work[3] = (message_work.op_work[4] = (message_work.op_work[5] = 0)))));
			message_work.op_work[6] = message_work.all_work[0];
			message_work.op_work[7] = ushort.MaxValue;
			message_work.op_workno = 99;
			break;
		case 33:
			if (GSStatic.global_work_.title == TitleId.GS3)
			{
				AnimationSystem.Instance.Char_monochrome(1, 31, 3, true);
			}
			Exit(message_work);
			break;
		case 34:
			if (GSStatic.global_work_.title == TitleId.GS3 && (long)AnimationSystem.Instance.IdlingCharacterMasked == 13)
			{
				AnimationSystem.Instance.is_aiga_mozaic_anim_ = true;
			}
			Exit(message_work);
			break;
		case 35:
			Exit(message_work);
			break;
		case 36:
			if (GSStatic.global_work_.title == TitleId.GS3)
			{
				AnimationSystem.Instance.is_aiga_mozaic_anim_ = false;
			}
			Exit(message_work);
			break;
		case 37:
			if (GSStatic.global_work_.title == TitleId.GS3)
			{
			}
			message_work.op_flg &= 240;
			MoveNextScript(message_work);
			Exit(message_work);
			break;
		case 38:
			message_work.status2 |= MessageSystem.Status2.MOSAIC_MONO;
			Exit(message_work);
			break;
		case 39:
			message_work.status2 |= MessageSystem.Status2.BG0_PROTECT;
			Exit(message_work);
			break;
		case 40:
			message_work.status2 |= MessageSystem.Status2.PALESC_SP;
			Exit(message_work);
			break;
		case 41:
			message_work.status2 |= MessageSystem.Status2.RESTORE_PSY;
			Exit(message_work);
			break;
		case 42:
			message_work.status2 |= MessageSystem.Status2.BG2_H8;
			message_work.status2 |= MessageSystem.Status2.BG2_V8;
			Exit(message_work);
			break;
		case 43:
			message_work.status2 &= ~MessageSystem.Status2.BG2_H8;
			message_work.status2 &= ~MessageSystem.Status2.BG2_V8;
			Exit(message_work);
			break;
		case 44:
			message_work.status2 |= MessageSystem.Status2.BG2_H8;
			Exit(message_work);
			break;
		case 45:
			message_work.status2 |= MessageSystem.Status2.BG2_V8;
			Exit(message_work);
			break;
		case 46:
			GSPsylock.KeyObjDelete();
			Exit(message_work);
			break;
		case 47:
			demo_shut_door();
			if (message_work.all_work[0] == 99)
			{
				message_work.op_flg &= 240;
				MoveNextScript(message_work);
			}
			break;
		case 48:
		{
			byte[] array = new byte[5] { 72, 32, 32, 32, 80 };
			switch (message_work.op_work[0])
			{
			case 0:
			{
				AnimationSystem.Instance.StopCharacters();
				AnimationSystem.Instance.PlayCharacter(0, 13, 213, 213);
				Vector3 localPosition4 = AnimationSystem.Instance.CharacterAnimationObject.transform.localPosition;
				localPosition4.y = -324f;
				AnimationSystem.Instance.CharacterAnimationObject.transform.localPosition = localPosition4;
				if (message_work.all_work[0] != 0)
				{
					AnimationSystem.Instance.Char_monochrome(1, 31, 0, true);
				}
				message_work.op_work[1] = (message_work.op_work[2] = 0);
				message_work.op_work[0]++;
				break;
			}
			case 1:
				if (++message_work.op_work[1] >= array[message_work.op_work[2]])
				{
					message_work.op_work[1] = 0;
					Vector3 localPosition3 = AnimationSystem.Instance.CharacterAnimationObject.transform.localPosition;
					localPosition3.y += 81f;
					AnimationSystem.Instance.CharacterAnimationObject.transform.localPosition = localPosition3;
					if (++message_work.op_work[2] > 4)
					{
						MoveNextScript(message_work);
					}
				}
				break;
			}
			break;
		}
		case 49:
			message_work.choustate = 7;
			Exit(message_work);
			break;
		case 50:
			message_work.all_work[0] = (ushort)(message_work.all_work[0] & 3u);
			Exit(message_work);
			break;
		case 51:
			if (message_work.all_work[0] != 0)
			{
				message_work.status2 &= ~MessageSystem.Status2.PSY_STOP_BREAK;
				global_work_.r.no_2 = 7;
				global_work_.r.no_3 = 0;
			}
			else
			{
				message_work.status2 |= MessageSystem.Status2.PSY_STOP_BREAK;
			}
			Exit(message_work);
			break;
		case 52:
			AnimationSystem.Instance.OBJ_monochrome2(message_work.all_work[0], 1, 31, 0, true);
			Exit(message_work);
			break;
		case 53:
			message_work.status2 |= MessageSystem.Status2.MV_MONO;
			Exit(message_work);
			break;
		case 54:
			message_work.status2 |= MessageSystem.Status2.MV_BLACK;
			Exit(message_work);
			break;
		case 55:
			switch (message_work.op_work[0])
			{
			case 0:
				Debug.Log("message_work.all_work[0] " + message_work.all_work[0]);
				switch (GSStatic.global_work_.title)
				{
				case TitleId.GS1:
				case TitleId.GS2:
					bgCtrl.instance.SetSubSprite(message_work.all_work[0]);
					break;
				case TitleId.GS3:
				{
					uint num19 = message_work.all_work[0];
					if (num19 == 114)
					{
						bgCtrl.instance.SetSubSprite(185);
					}
					else
					{
						bgCtrl.instance.SetSubSprite(message_work.all_work[0]);
					}
					break;
				}
				}
				bgCtrl.instance.ChangeSubSpritePosition(new Vector3(bgCtrl.instance.bg_pos_x, bgCtrl.instance.bg_pos_y, -1f));
				bgCtrl.instance.ChangeSubSpriteAlpha(0f);
				message_work.op_work[0] = 1;
				if (GSStatic.global_work_.title == TitleId.GS3 && global_work_.scenario == 10)
				{
					message_work.op_work[1] = (ushort)bgCtrl.instance.bg_no;
					message_work.op_work[2] = message_work.all_work[0];
				}
				break;
			case 1:
				message_work.all_work[0] = 0;
				message_work.all_work[2] = 0;
				message_work.op_work[0] = 2;
				break;
			case 2:
				if (++message_work.all_work[0] > message_work.all_work[1])
				{
					message_work.all_work[0] = 0;
					message_work.all_work[2]++;
					bgCtrl.instance.ChangeSubSpriteAlpha((float)(int)message_work.all_work[2] / 16f);
				}
				if (message_work.all_work[2] == 16)
				{
					message_work.op_work[0] = 3;
				}
				break;
			case 3:
				bgCtrl.instance.ChangeSubSpritePosition(Vector3.forward * -20f);
				bgCtrl.instance.ChangeSubSpriteEnable(false);
				if (GSStatic.global_work_.title == TitleId.GS3 && global_work_.scenario == 10)
				{
					bgCtrl.instance.SetSprite(message_work.op_work[2]);
					bgCtrl.instance.bg_no_old = message_work.op_work[1];
				}
				message_work.op_flg &= 240;
				message_work.mdt_index += 3u;
				Exit(message_work);
				break;
			}
			break;
		case 56:
			Exit(message_work);
			break;
		case 57:
			Exit(message_work);
			break;
		case 58:
		{
			uint status_flag = global_work_.status_flag;
			if (message_work.all_work[0] != 1)
			{
				global_work_.status_flag &= 4294965247u;
			}
			else
			{
				global_work_.status_flag |= 2048u;
			}
			if (status_flag != global_work_.status_flag)
			{
				messageBoardCtrl instance2 = messageBoardCtrl.instance;
				instance2.guide_ctrl.changeSaveOffGuide(instance2.guide_ctrl.current_guide);
			}
			break;
		}
		case 59:
			if (message_work.all_work[0] != 1)
			{
				GSStatic.global_work_.psy_unlock_not_unlock_message = 0;
			}
			else
			{
				GSStatic.global_work_.psy_unlock_not_unlock_message = 1;
			}
			Exit(message_work);
			break;
		case 60:
			switch (message_work.op_work[0])
			{
			case 0:
				bgCtrl.instance.DetentionBgFadeInit(false);
				message_work.all_work[1] = 0;
				message_work.all_work[2] = 0;
				message_work.op_work[0] = 1;
				break;
			case 1:
				if (++message_work.all_work[2] >= message_work.all_work[0])
				{
					message_work.all_work[2] = 0;
					message_work.all_work[1]++;
					float rate2 = 1f - (float)(int)message_work.all_work[1] / 31f;
					bgCtrl.instance.DetentionBgFade(rate2);
					if (message_work.all_work[1] == 31)
					{
						MoveNextScript(message_work);
					}
				}
				break;
			}
			break;
		case 61:
			message_work.status |= MessageSystem.Status.FADE_OK;
			Exit(message_work);
			break;
		case 62:
			message_work.status &= ~MessageSystem.Status.FADE_OK;
			Exit(message_work);
			break;
		case 63:
			switch (message_work.op_work[0])
			{
			case 0:
				bgCtrl.instance.DetentionBgFade(0f);
				message_work.op_work[0] = 1;
				break;
			case 1:
				message_work.all_work[1] = 0;
				message_work.all_work[2] = 0;
				message_work.op_work[0] = 2;
				fadeCtrl.instance.play(1u, 0u, 0u, 8u);
				break;
			case 2:
				if (++message_work.all_work[2] >= message_work.all_work[0])
				{
					message_work.all_work[2] = 0;
					message_work.all_work[1]++;
					float rate = (float)(int)message_work.all_work[1] / 31f;
					bgCtrl.instance.DetentionBgFade(rate);
					if (message_work.all_work[1] == 31)
					{
						MoveNextScript(message_work);
					}
				}
				break;
			}
			break;
		case 64:
			soundCtrl.instance.FadeOutSE(message_work.all_work[0], message_work.all_work[1] / 16);
			Exit(message_work);
			break;
		case 65:
			switch (message_work.op_work[0])
			{
			default:
				return;
			case 0:
				if (message_work.all_work[1] == 0)
				{
					message_work.all_work[2] = 31;
				}
				else
				{
					message_work.all_work[2] = 0;
				}
				fadeCtrl.instance.play(2u, message_work.all_work[0], message_work.all_work[1], 8u);
				message_work.all_work[1] = 0;
				message_work.op_work[0] = 1;
				break;
			case 1:
				break;
			}
			if (fadeCtrl.instance.status == fadeCtrl.Status.NO_FADE)
			{
				MoveNextScript(message_work);
			}
			break;
		case 66:
			switch (message_work.op_work[0])
			{
			default:
				return;
			case 0:
				bgCtrl.instance.SetSprite(message_work.all_work[2]);
				if (message_work.all_work[1] == 0)
				{
					message_work.all_work[2] = 30;
					message_work.all_work[1] = message_work.all_work[0];
				}
				else
				{
					message_work.all_work[2] = 0;
					message_work.all_work[1] = 0;
				}
				message_work.op_work[0] = 1;
				return;
			case 1:
				fadeCtrl.instance.play(1u, message_work.all_work[0], 1u, 8u);
				message_work.op_work[0] = 2;
				break;
			case 2:
				break;
			}
			if (fadeCtrl.instance.status == fadeCtrl.Status.NO_FADE)
			{
				MoveNextScript(message_work);
			}
			break;
		case 67:
		{
			MessageWork activeMessageWork2 = MessageSystem.GetActiveMessageWork();
			if (activeMessageWork2.message_type == WindowType.MAIN)
			{
			}
			itemPlateCtrl.instance.closeItem(true);
			facePlateCtrl.instance.closeItem(true);
			Exit(message_work);
			break;
		}
		case 68:
		{
			ushort num14 = message_work.op_work[0];
			if (num14 != 0)
			{
				switch (num14)
				{
				default:
					return;
				case 1:
					break;
				case 2:
					MoveNextScript(message_work);
					return;
				}
			}
			else
			{
				global_work_.psy_no = (sbyte)message_work.all_work[0];
				GSPsylock.Set_Psylock_Mode(11, global_work_.psy_no);
				GSPsylock.Set_Psylock_Mode(12, 0);
				GSPsylock.Set_Psylock_Mode(13, 0);
				message_work.op_work[0]++;
			}
			GSPsylock.PsylockDisp_move();
			if (GSPsylock.PsylockDisp_is_wait())
			{
				message_work.op_work[0]++;
			}
			break;
		}
		case 69:
			if (message_work.all_work[0] != 0)
			{
				Exit(message_work);
				message_work.op_para = 67;
			}
			break;
		case 70:
		{
			ushort num13 = message_work.op_work[0];
			if (num13 != 0)
			{
				switch (num13)
				{
				default:
					return;
				case 1:
					break;
				case 2:
					message_work.status2 &= ~MessageSystem.Status2.STOP_EXPL;
					Exit(message_work);
					return;
				}
			}
			else
			{
				message_work.status2 |= MessageSystem.Status2.STOP_EXPL;
				message_work.op_work[0]++;
			}
			message_work.op_work[1] += message_work.all_work[1];
			if (message_work.op_work[1] > 15)
			{
				message_work.op_work[1] = 15;
				message_work.op_work[0]++;
			}
			break;
		}
		case 71:
			if (message_work.all_work[0] != 0)
			{
				message_work.status |= MessageSystem.Status.OBJ_MOSAIC;
			}
			else
			{
				message_work.status &= ~MessageSystem.Status.OBJ_MOSAIC;
			}
			Exit(message_work);
			break;
		case 72:
			message_work.status2 |= MessageSystem.Status2.MV_WHITE;
			Exit(message_work);
			break;
		case 73:
			switch (message_work.op_work[0])
			{
			case 0:
			{
				bgCtrl.instance.SetSprite(4095);
				bgCtrl.instance.SetColorImage(Color.white);
				bgCtrl.instance.SetAlphaImage(0f);
				AnimationObject animationObject9 = AnimationSystem.Instance.PlayObject((int)GSStatic.global_work_.title, 0, 222);
				Vector3 localPosition10 = animationObject9.transform.localPosition;
				localPosition10.z = -4f;
				animationObject9.transform.localPosition = localPosition10;
				message_work.all_work[0] = 0;
				message_work.all_work[1] = 0;
				message_work.all_work[2] = 0;
				message_work.op_work[0] = 0;
				message_work.op_work[1] = 0;
				message_work.op_work[2] = 0;
				message_work.op_work[0]++;
				goto case 1;
			}
			case 1:
			{
				AnimationObject animationObject8 = AnimationSystem.Instance.FindObject((int)GSStatic.global_work_.title, 0, 222);
				Vector3 localPosition9 = animationObject8.transform.localPosition;
				if (localPosition9.x < 202.5f)
				{
					localPosition9.x += 13.5f;
				}
				if (message_work.all_work[1] == 0)
				{
					localPosition9.x += 6.75f;
				}
				if (localPosition9.x > 337.5f)
				{
					localPosition9.x = 337.5f;
				}
				animationObject8.transform.localPosition = localPosition9;
				if (++message_work.all_work[0] > 17)
				{
					message_work.all_work[0] = 0;
					message_work.op_work[1]++;
					if (message_work.op_work[1] > 7)
					{
						message_work.op_work[1] = 8;
						message_work.all_work[0] = 0;
					}
				}
				if (++message_work.all_work[1] > 4)
				{
					message_work.all_work[1] = 0;
					message_work.op_work[2]++;
					bgCtrl.instance.SetAlphaImage((float)(int)message_work.op_work[2] * 8.2f / 255f);
				}
				animationObject8.Alpha = (float)(int)message_work.op_work[2] / 48f;
				if (message_work.op_work[2] > 30)
				{
					message_work.all_work[0] = 0;
					message_work.op_work[0]++;
				}
				break;
			}
			case 2:
				message_work.all_work[0]++;
				if (message_work.all_work[0] > 40)
				{
					message_work.op_work[0]++;
				}
				break;
			case 3:
			{
				message_work.all_work[0] = 0;
				message_work.all_work[1] = 0;
				message_work.all_work[2] = 0;
				AnimationSystem.Instance.PlayCharacter((int)GSStatic.global_work_.title, 3, 14, 15);
				AnimationObject animationObject7 = AnimationSystem.Instance.FindObject((int)GSStatic.global_work_.title, 0, 222);
				Vector3 localPosition8 = animationObject7.transform.localPosition;
				animationObject7.Stop(true);
				animationObject7 = AnimationSystem.Instance.PlayObject((int)GSStatic.global_work_.title, 0, 221);
				animationObject7.transform.localPosition = localPosition8;
				animationObject7.Alpha = 0.75f;
				MoveNextScript(message_work);
				break;
			}
			}
			break;
		case 74:
			switch (message_work.op_work[0])
			{
			case 0:
				message_work.op_work[0]++;
				message_work.all_work[1] = 0;
				break;
			case 1:
			{
				if (++message_work.all_work[1] > 2)
				{
					message_work.all_work[1] = 0;
					message_work.all_work[0]--;
				}
				AnimationObject animationObject6 = AnimationSystem.Instance.FindObject((int)GSStatic.global_work_.title, 0, 222);
				animationObject6.Alpha = (float)(int)message_work.all_work[0] / 24f;
				if (message_work.all_work[0] == 0)
				{
					message_work.op_work[0]++;
				}
				break;
			}
			case 2:
			{
				message_work.all_work[0] = 0;
				message_work.all_work[1] = 0;
				message_work.all_work[2] = 0;
				AnimationObject animationObject5 = AnimationSystem.Instance.FindObject((int)GSStatic.global_work_.title, 0, 222);
				animationObject5.Alpha = 1f;
				animationObject5.Stop(true);
				MoveNextScript(message_work);
				break;
			}
			}
			break;
		case 75:
		{
			message_work.all_work[0] = 11;
			AnimationObject animationObject4 = AnimationSystem.Instance.PlayObject((int)GSStatic.global_work_.title, 0, 222);
			Vector3 localPosition7 = animationObject4.transform.localPosition;
			localPosition7.x = -6648.5f;
			localPosition7.z = -4f;
			animationObject4.transform.localPosition = localPosition7;
			animationObject4.Alpha = 0.75f;
			Exit(message_work);
			break;
		}
		case 76:
			switch (message_work.op_work[0])
			{
			case 0:
			{
				bgCtrl.instance.SetSprite(4095);
				bgCtrl.instance.SetColorImage(Color.white);
				AnimationObject animationObject3 = AnimationSystem.Instance.PlayObject((int)GSStatic.global_work_.title, 0, 222);
				Vector3 localPosition6 = animationObject3.transform.localPosition;
				localPosition6.x = 405f;
				localPosition6.y = -60.75f;
				localPosition6.z = -4f;
				animationObject3.transform.localPosition = localPosition6;
				animationObject3.PlayMonochrome(1, 31, 0, true);
				animationObject3 = AnimationSystem.Instance.CharacterAnimationObject;
				animationObject3.PlayMonochrome(1, 31, 0, true);
				message_work.op_work[0] = 1;
				break;
			}
			case 1:
				fadeCtrl.instance.play(fadeCtrl.Status.WFADE_IN, 8u, 2u);
				message_work.all_work[0] = 11;
				message_work.op_work[0] = 2;
				message_work.all_work[0] = 31;
				break;
			case 2:
				message_work.all_work[0]--;
				if (message_work.all_work[0] == 0)
				{
					message_work.op_work[0] = 99;
				}
				break;
			case 99:
			{
				AnimationSystem.Instance.PlayCharacter((int)GSStatic.global_work_.title, 3, 14, 14);
				AnimationObject animationObject2 = AnimationSystem.Instance.FindObject((int)GSStatic.global_work_.title, 0, 222);
				Vector3 localPosition5 = animationObject2.transform.localPosition;
				animationObject2.Stop(true);
				animationObject2 = AnimationSystem.Instance.PlayObject((int)GSStatic.global_work_.title, 0, 221);
				animationObject2.transform.localPosition = localPosition5;
				animationObject2.PlayMonochrome(1, 31, 0, true);
				bgCtrl.instance.Bg256_monochrome(1, 31, 0, true);
				MoveNextScript(message_work);
				break;
			}
			}
			break;
		case 77:
			switch (message_work.op_work[0])
			{
			case 0:
				fadeCtrl.instance.play(fadeCtrl.Status.WFADE_OUT, 8u, 2u);
				message_work.op_work[0] = 1;
				break;
			case 1:
				if (fadeCtrl.instance.status == fadeCtrl.Status.NO_FADE)
				{
					bgCtrl.instance.Bg256_monochrome(1, 1, 0, false);
					message_work.op_work[0] = 99;
				}
				break;
			case 99:
			{
				AnimationObject animationObject = AnimationSystem.Instance.FindObject((int)GSStatic.global_work_.title, 0, 221);
				animationObject.Stop(true);
				MoveNextScript(message_work);
				break;
			}
			}
			break;
		case 78:
			Exit(message_work);
			break;
		case 79:
			if (bgCtrl.instance.bg_no_reserve != 65535)
			{
				bgCtrl.instance.SetReserveSprite();
			}
			MoveNextScript(message_work);
			break;
		case 80:
			Exit(message_work);
			break;
		case 83:
			AnimationSystem.Instance.OBJ_monochrome2(159u, 1, 31, 6, true);
			AnimationSystem.Instance.OBJ_monochrome2(185u, 1, 31, 6, true);
			AnimationSystem.Instance.OBJ_monochrome2(152u, 1, 31, 6, true);
			AnimationSystem.Instance.OBJ_monochrome2(153u, 1, 31, 6, true);
			Exit(message_work);
			break;
		case 84:
			if (message_work.op_work[3] == 0)
			{
				message_work.op_work[3] = 1;
			}
			if (message_work.op_work[3] != 0 && fadeCtrl.instance.status == fadeCtrl.Status.NO_FADE)
			{
				Exit(message_work);
			}
			break;
		case 85:
			message_work.op_work[0] = (message_work.op_work[1] = (message_work.op_work[2] = (message_work.op_work[3] = (message_work.op_work[4] = (message_work.op_work[5] = (message_work.op_work[6] = 0))))));
			message_work.op_work[7] = 65533;
			message_work.op_workno = 99;
			break;
		case 86:
			Exit(message_work);
			message_work.op_work[7] = 65532;
			break;
		case 88:
			Exit(message_work);
			break;
		case 89:
			soundCtrl.instance.FadeOutSE(164, 60);
			Exit(message_work);
			break;
		case 100:
			global_work_.r.Set(2, 0, 0, 0);
			Exit(message_work);
			break;
		case 101:
		{
			MessageWork activeMessageWork = MessageSystem.GetActiveMessageWork();
			switch (message_work.all_work[0])
			{
			case 0:
				switch (message_work.all_work[2])
				{
				case 0:
					if ((global_work_.status_flag & (true ? 1u : 0u)) != 0)
					{
						global_work_.status_flag &= 4294967294u;
						if (activeMessageWork.message_type != 0)
						{
						}
					}
					AnimationSystem.Instance.GoIdle();
					message_work.op_work[0] = 0;
					if (message_work.all_work[1] == 0 || message_work.all_work[1] == 1 || GSStatic.global_work_.title != 0 || message_work.all_work[1] == 2 || message_work.all_work[1] == 3 || message_work.all_work[1] == 4 || message_work.all_work[1] == 5)
					{
					}
					message_work.all_work[2]++;
					break;
				case 1:
					message_work.op_work[0]++;
					if (message_work.op_work[0] >= 24)
					{
						message_work.op_work[0] = 24;
						message_work.all_work[2]++;
					}
					break;
				case 2:
					MoveNextScript(message_work);
					message_work.op_work[7] = 65532;
					return;
				}
				if (message_work.all_work[1] == 0 || message_work.all_work[1] == 1 || GSStatic.global_work_.title != 0 || message_work.all_work[1] == 2 || message_work.all_work[1] == 3 || message_work.all_work[1] == 4 || message_work.all_work[1] != 5 || global_work_.language == Language.JAPAN || global_work_.language == Language.USA)
				{
				}
				if (objWork != null)
				{
				}
				message_work.op_work[1] = message_work.op_work[0];
				if (message_work.op_work[1] > 18)
				{
					message_work.op_work[1] = 18;
				}
				if (GSStatic.global_work_.title != TitleId.GS2)
				{
				}
				break;
			case 1:
				switch (message_work.all_work[2])
				{
				case 0:
					if ((global_work_.status_flag & (true ? 1u : 0u)) != 0)
					{
						global_work_.status_flag &= 4294967294u;
						if (activeMessageWork.message_type != 0)
						{
						}
					}
					message_work.op_work[0] = 0;
					if (message_work.all_work[1] == 0 || message_work.all_work[1] == 1)
					{
					}
					message_work.all_work[2]++;
					break;
				case 1:
					message_work.op_work[0]++;
					if (message_work.op_work[0] >= 24)
					{
						message_work.op_work[0] = 24;
						message_work.all_work[2]++;
					}
					break;
				case 2:
					MoveNextScript(message_work);
					message_work.op_work[7] = 65532;
					return;
				}
				if (message_work.all_work[1] == 0 || message_work.all_work[1] == 1)
				{
				}
				if (objWork != null)
				{
				}
				message_work.op_work[1] = message_work.op_work[0];
				if (message_work.op_work[1] > 18)
				{
					message_work.op_work[1] = 18;
				}
				break;
			case 255:
				if (bgCtrl.instance.bg_no != 4095 && message_work.all_work[1] != 4)
				{
					spefCtrl.instance.Monochrome_set(4, 1, 1);
				}
				if (GSStatic.global_work_.title == TitleId.GS2)
				{
				}
				Exit(message_work);
				break;
			}
			break;
		}
		case 102:
			if (GSStatic.global_work_.title == TitleId.GS1)
			{
				switch (message_work.all_work[0])
				{
				case 1:
					bgCtrl.instance.SetSeal(1);
					break;
				case 3:
					bgCtrl.instance.SetSeal(0, false);
					bgCtrl.instance.SetSeal(2, false);
					break;
				case 5:
					bgCtrl.instance.SetSeal(5, false);
					break;
				}
			}
			Exit(message_work);
			break;
		case 103:
			Exit(message_work);
			break;
		case 104:
			break;
		case 105:
			advCtrl.instance.message_system_.SetMessage2(message_work.all_work[0], message_work.all_work[1]);
			Exit(message_work);
			break;
		case 106:
			Exit(message_work);
			break;
		case 107:
			if (GSStatic.global_work_.title == TitleId.GS1)
			{
				byte b = global_work_.scenario;
				if (b == 21 && message_work.all_work[0] == 0)
				{
					global_work_.bk_start_mess = 142;
				}
			}
			Exit(message_work);
			break;
		case 108:
			switch (message_work.all_work[0])
			{
			case 0:
				rotBG.instance.Rot_bg_init();
				break;
			case 1:
				rotBG.instance.Rot_bg_end();
				break;
			case 2:
			{
				float num10 = (float)(int)message_work.all_work[2] / 10f;
				rotBG.instance.Rot_bg_rotate_set((int)num10);
				break;
			}
			case 3:
				rotBG.instance.Rot_bg_rotate_add_set((0f - (float)(int)message_work.all_work[2]) / 10f);
				break;
			case 4:
				rotBG.instance.Rot_bg_rotate_add_set((float)(int)message_work.all_work[2] / 10f);
				break;
			}
			Exit(message_work);
			break;
		case 109:
		{
			bgCtrl instance = bgCtrl.instance;
			uint bg256_dir = instance.Bg256_dir;
			if (message_work.all_work[0] == 0)
			{
				int num7 = (int)(0f - instance.body.transform.localPosition.x);
				switch (message_work.op_work[0])
				{
				case 0:
					if ((bg256_dir & 0x20u) != 0 && num7 >= 960)
					{
						Vector3 localPosition = instance.body.transform.localPosition;
						localPosition.x = -960f;
						instance.SetBodyPosition(localPosition);
						instance.stopScroll();
						message_work.op_work[0]++;
					}
					else if ((bg256_dir & 0x10u) != 0 && num7 <= 960)
					{
						Vector3 localPosition2 = instance.body.transform.localPosition;
						localPosition2.x = -960f;
						instance.SetBodyPosition(localPosition2);
						instance.stopScroll();
						message_work.op_work[0]++;
					}
					break;
				case 1:
					message_work.op_flg &= 240;
					message_work.mdt_index += 3u;
					Exit(message_work);
					break;
				}
			}
			else if (message_work.all_work[0] == 1)
			{
				Exit(message_work);
			}
			else if (message_work.all_work[0] == 2)
			{
				if (GSStatic.global_work_.title == TitleId.GS1)
				{
					ushort num8 = 116;
					instance.SetSprite(num8);
					CheckBGChange(num8, 0u);
					instance.Bg256_set_center(num8);
					Exit(message_work);
				}
			}
			else if (GSStatic.global_work_.title == TitleId.GS1)
			{
				ushort num9 = 32884;
				instance.SetSprite(num9);
				CheckBGChange(num9, 0u);
				instance.Bg256_set_center(num9);
				Exit(message_work);
			}
			break;
		}
		case 110:
			switch (message_work.op_work[0])
			{
			case 0:
				message_work.op_work[0]++;
				break;
			case 1:
				message_work.op_work[0]++;
				break;
			case 2:
				message_work.op_flg &= 240;
				message_work.mdt_index += 3u;
				Exit(message_work);
				break;
			}
			break;
		case 111:
			Exit(message_work);
			break;
		case 112:
			Exit(message_work);
			break;
		case 113:
			if (message_work.all_work[0] != 0)
			{
				advCtrl.instance.sub_window_.sprite_routine_[5].r.no_0 = 0;
			}
			Exit(message_work);
			break;
		case 114:
			Exit(message_work);
			break;
		case 115:
			Exit(message_work);
			break;
		case 116:
			Exit(message_work);
			break;
		case 117:
			if (message_work.all_work[0] == 0)
			{
				switch (message_work.op_work[0])
				{
				case 0:
					if (GSStatic.global_work_.language == Language.JAPAN)
					{
						bgCtrl.instance.SetSprite("bg0fa");
					}
					else
					{
						bgCtrl.instance.SetSprite("bg0fau");
					}
					bgCtrl.instance.SetSubSprite("bg133");
					bgCtrl.instance.sub_bg_pos_x = -384f;
					message_work.op_work[0]++;
					break;
				case 1:
				{
					float bg_pos_x = bgCtrl.instance.bg_pos_x;
					if (bg_pos_x >= 1920f)
					{
						message_work.op_work[0] = 10;
						break;
					}
					bg_pos_x += 1.872f;
					if (bg_pos_x >= 1920f)
					{
						bg_pos_x = 1920f;
					}
					bgCtrl.instance.bg_pos_x = bg_pos_x;
					float sub_bg_pos_x = bgCtrl.instance.sub_bg_pos_x;
					sub_bg_pos_x += 0.3744f;
					if (sub_bg_pos_x > 0f)
					{
						sub_bg_pos_x = 0f;
					}
					bgCtrl.instance.shadow_bg_pos_x = sub_bg_pos_x;
					break;
				}
				case 10:
					Exit(message_work);
					break;
				}
			}
			else if (fadeCtrl.instance.status == fadeCtrl.Status.NO_FADE)
			{
				Exit(message_work);
			}
			break;
		case 118:
			switch (message_work.op_work[0])
			{
			case 0:
				if (GSStatic.global_work_.title == TitleId.GS2)
				{
					GSStatic.save_sys_.Scenario_enable = 79;
				}
				else
				{
					GSStatic.save_sys_.Scenario_enable = 95;
				}
				global_work_.Scenario_enable = GSStatic.save_sys_.Scenario_enable;
				message_work.op_work[0]++;
				GSStatic.open_sce_.CurrentSave(GSStatic.save_sys_, GSStatic.global_work_.title);
				break;
			case 1:
				TrophyCtrl.check_trophy_scenario_clear(true);
				message_work.op_work[0]++;
				break;
			case 2:
				message_work.op_work[0]++;
				break;
			case 3:
				message_work.op_flg &= 240;
				message_work.mdt_index += 3u;
				Exit(message_work);
				break;
			}
			break;
		case 119:
			vibrationCtrl.instance.stop();
			Exit(message_work);
			break;
		case 127:
			switch (message_work.op_work[0])
			{
			case 0:
				message_work.op_work[0]++;
				message_work.op_work[1] = 120;
				break;
			case 1:
				if (message_work.op_work[1]-- == 0)
				{
					message_work.op_work[0]++;
				}
				break;
			case 2:
				message_work.op_work[0]--;
				message_work.op_work[1] = 60;
				break;
			}
			break;
		case 200:
			MoveNextScript(message_work);
			break;
		default:
			Exit(message_work);
			break;
		}
	}

	public static void CheckBGChange(uint no, uint flag)
	{
		switch (GSStatic.global_work_.title)
		{
		case TitleId.GS1:
			GS1_BGChange.GS1_CheckBGChange(no, flag);
			break;
		case TitleId.GS2:
			GS2_BGChange.GS2_CheckBGChange(no, flag);
			break;
		case TitleId.GS3:
			GS3_BGChange.GS3_CheckBGChange(no, flag);
			break;
		}
	}

	private static void demo_shut_door()
	{
		MessageWork activeMessageWork = MessageSystem.GetActiveMessageWork();
		int num = 0;
		switch (activeMessageWork.all_work[0])
		{
		case 0:
			gameoverCtrl.instance.init();
			activeMessageWork.all_work[0] = 1;
			activeMessageWork.all_work[1] = 0;
			activeMessageWork.all_work[2] = 0;
			break;
		case 1:
			num = 20 * activeMessageWork.all_work[1];
			if (num > 200)
			{
				num = 200;
			}
			if (activeMessageWork.all_work[1] >= 16)
			{
				activeMessageWork.all_work[0]++;
				activeMessageWork.all_work[1] = 0;
			}
			else
			{
				activeMessageWork.all_work[1]++;
			}
			if (gameoverCtrl.instance.work_state <= 1)
			{
				gameoverCtrl.instance.Play();
			}
			break;
		case 2:
			if (activeMessageWork.all_work[1] >= 120)
			{
				fadeCtrl.instance.play(fadeCtrl.Status.FADE_OUT, 3u, 1u);
				activeMessageWork.all_work[0]++;
			}
			else
			{
				activeMessageWork.all_work[1]++;
			}
			break;
		case 3:
			if (fadeCtrl.instance.status == fadeCtrl.Status.NO_FADE)
			{
				gameoverCtrl.instance.Finalize_State();
				activeMessageWork.all_work[0] = 99;
				activeMessageWork.all_work[1] = 0;
				activeMessageWork.all_work[2] = 0;
			}
			break;
		}
	}

	private static void op4_105_flame_end(int[] index_list)
	{
		foreach (int num in index_list)
		{
			AnimationObject animationObject = AnimationSystem.Instance.FindObject(2, 0, 125 + num);
			Vector3 localPosition = animationObject.transform.localPosition;
			animationObject.Stop(true);
			animationObject = AnimationSystem.Instance.PlayObject(2, 0, 136 + num);
			animationObject.transform.localPosition = localPosition;
		}
	}
}
