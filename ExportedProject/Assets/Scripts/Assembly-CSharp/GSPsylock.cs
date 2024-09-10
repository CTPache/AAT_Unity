using UnityEngine;

public static class GSPsylock
{
	public enum Rno0
	{
		PSYLOCK_WAIT = 0,
		PSYLOCK_WHITESHOCK = 1,
		PSYLOCK_CHAIN_APPEAR = 2,
		PSYLOCK_LOCK_APPEAR = 3,
		PSYLOCK_LOCK_UNLOCK = 4,
		PSYLOCK_CHAIN_DISAPPEAR = 5,
		PSYLOCK_UNLOCK_MESSAGE = 6,
		PSYLOCK_CLEAR_ALL = 7,
		PSYLOCK_TO_NORMAL_BG = 8,
		PSYLOCK_REDISP = 9,
		PSYLOCK_NULL = 10,
		PSY_MODE_INIT = 11,
		PSY_MODE_START = 12,
		PSY_MODE_CHAIN_ON = 13
	}

	private class PsylockDispController
	{
		public short level;

		public short rest;

		public short r_no_0;

		public short r_no_1;

		public short cnt0;

		public short cnt1;

		public AnimationObject[] lock_obj = new AnimationObject[5];

		public GameObject[] lock_destroy_obj = new GameObject[5];

		public void Clear()
		{
			level = 0;
			rest = 0;
			r_no_0 = 0;
			r_no_1 = 0;
			cnt0 = 0;
			cnt1 = 0;
		}
	}

	private static PsylockDispController psy = new PsylockDispController();

	private static readonly Vector3[][] lock_pos = new Vector3[6][]
	{
		new Vector3[1]
		{
			new Vector3(0f, -115f)
		},
		new Vector3[2]
		{
			new Vector3(-644f, 12f),
			new Vector3(644f, 12f)
		},
		new Vector3[3]
		{
			new Vector3(0f, -115f),
			new Vector3(-644f, 12f),
			new Vector3(644f, 12f)
		},
		new Vector3[4]
		{
			new Vector3(-460f, 330f),
			new Vector3(460f, 330f),
			new Vector3(-644f, -12f),
			new Vector3(644f, -12f)
		},
		new Vector3[5]
		{
			new Vector3(0f, -115f),
			new Vector3(-460f, 330f),
			new Vector3(460f, 330f),
			new Vector3(-644f, -12f),
			new Vector3(644f, -12f)
		},
		new Vector3[5]
		{
			new Vector3(0f, 425f),
			new Vector3(-460f, 330f),
			new Vector3(460f, 330f),
			new Vector3(-644f, -12f),
			new Vector3(644f, -12f)
		}
	};

	private const float ANIMATION_SCALE = 4.5f;

	private static readonly Vector3 EFFECT_SCREEN_HALF_SIZE = new Vector3(1700f, 960f, 0f);

	private static readonly Vector3 DESTROY_EFFECT_OFFSET = new Vector3(0f, -34f, 0f);

	private const float DESTROY_EFFECT_SCALE = 32.5f;

	private static TitleId CurrentTitle
	{
		get
		{
			return GSStatic.global_work_.title;
		}
	}

	private static AnimationSystem animation_system
	{
		get
		{
			return AnimationSystem.Instance;
		}
	}

	public static void PsylockDisp_init(int _level)
	{
		psy.Clear();
		GSStatic.global_work_.psy_unlock_success = 0;
		psy.level = (short)_level;
		if (psy.level < 6)
		{
			psy.rest = (short)_level;
		}
		else
		{
			psy.rest = 5;
		}
	}

	public static void PsylockDisp_appear()
	{
		if (GSStatic.global_work_.title == TitleId.GS2)
		{
			bool flag = false;
			for (int i = 62; i <= 64; i++)
			{
				if (animation_system.IsPlayingObject(1, 0, i))
				{
					animation_system.StopObject(1, 0, i);
					flag = true;
				}
			}
			if (flag)
			{
				animation_system.PlayObject(1, 0, 65);
			}
		}
		psy.r_no_0 = 1;
		psy.r_no_1 = 0;
	}

	public static void PsylockDisp_unlock()
	{
		psy.r_no_0 = 4;
		psy.r_no_1 = 0;
	}

	public static void PsylockDisp_disappear()
	{
		psy.r_no_0 = 5;
		psy.r_no_1 = 0;
	}

	public static void PsylockDisp_unlock_message()
	{
		psy.r_no_0 = 6;
		psy.r_no_1 = 0;
	}

	public static void PsylockDisp_clear_all()
	{
		psy.r_no_0 = 7;
		psy.r_no_1 = 0;
	}

	public static void PsylockDisp_to_normal_bg()
	{
		psy.r_no_0 = 8;
		psy.r_no_1 = 0;
	}

	public static void PsylockDisp_redisp()
	{
		psy.r_no_0 = 9;
		psy.r_no_1 = 0;
	}

	public static bool PsylockDisp_is_wait()
	{
		if (psy.r_no_0 == 0)
		{
			return true;
		}
		return false;
	}

	public static void PsylockDisp_move()
	{
		switch ((Rno0)psy.r_no_0)
		{
		case Rno0.PSYLOCK_WAIT:
			psylock_move_wait();
			break;
		case Rno0.PSYLOCK_WHITESHOCK:
			psylock_move_whiteshock();
			break;
		case Rno0.PSYLOCK_CHAIN_APPEAR:
			psylock_move_chain_appear();
			break;
		case Rno0.PSYLOCK_LOCK_APPEAR:
			psylock_move_lock_appear();
			break;
		case Rno0.PSYLOCK_LOCK_UNLOCK:
			psylock_move_lock_unlock();
			break;
		case Rno0.PSYLOCK_CHAIN_DISAPPEAR:
			psylock_move_chain_disappear();
			break;
		case Rno0.PSYLOCK_UNLOCK_MESSAGE:
			psylock_move_unlock_message();
			break;
		case Rno0.PSYLOCK_CLEAR_ALL:
			psylock_move_clear_all();
			break;
		case Rno0.PSYLOCK_TO_NORMAL_BG:
			psylock_move_to_normal_bg();
			break;
		case Rno0.PSYLOCK_REDISP:
			psylock_move_redisp();
			break;
		case Rno0.PSYLOCK_NULL:
			psylock_move_null();
			break;
		}
	}

	private static void psylock_move_wait()
	{
	}

	private static void psylock_move_whiteshock()
	{
		if (GSStatic.global_work_.title == TitleId.GS2)
		{
			AnimationObject animationObject = AnimationSystem.Instance.FindObject(1, 0, 65);
			if (animationObject != null)
			{
				if (animation_system.IsPlayingObject(0, 0, 65))
				{
					return;
				}
				animation_system.StopObject(1, 0, 65);
			}
		}
		switch (psy.r_no_1)
		{
		case 0:
			psy.r_no_1++;
			break;
		case 1:
			AnimationSystem.Instance.CharacterAnimationObject.Alpha = 1f;
			soundCtrl.instance.PlaySE(122);
			bgCtrl.instance.bg_no_now = 254;
			messageBoardCtrl.instance.guide_ctrl.next_guid = guideCtrl.GuideType.PSYLOCK;
			psy.cnt0 = 255;
			psy.cnt1 = 0;
			psy.r_no_1++;
			goto case 2;
		case 2:
			if (psy.cnt0++ > 70)
			{
				psy.cnt0 = 0;
				psy.cnt1++;
				if (psy.cnt1 >= 2)
				{
					psy.r_no_1++;
					break;
				}
				fadeCtrl.instance.play(fadeCtrl.Status.WFADE_IN, 1u, 4u);
				bgCtrl.instance.setNegaPosi(true);
			}
			break;
		case 3:
		{
			int num = 256 - 8 * psy.cnt0;
			bgCtrl.instance.setColor(new Color((float)num / 256f, (float)num / 256f, (float)num / 256f));
			if (psy.cnt0++ > 32)
			{
				psy.r_no_1++;
				bgCtrl.instance.setColor(Color.black);
				if (bgCtrl.instance.bg_no == bgCtrl.Bg_GetRyuchijyoBgNo())
				{
					bgCtrl.instance.ChangeSubSpriteEnable(false);
				}
			}
			break;
		}
		default:
			psy.r_no_0++;
			psy.r_no_1 = 0;
			break;
		}
	}

	private static void psylock_move_chain_appear()
	{
		switch (psy.r_no_1)
		{
		default:
			return;
		case 0:
			GSPsyLockControl.instance.SetChainAnimation(psy.level - 1, psy.level + 6 - 1);
			if (psy.level < 6)
			{
				soundCtrl.instance.PlaySE(128 + (psy.level - 1));
			}
			else
			{
				soundCtrl.instance.PlaySE(132);
			}
			psy.r_no_1++;
			return;
		case 1:
			if (!GSPsyLockControl.instance.IsChainAnimation())
			{
				psy.r_no_1++;
			}
			ScreenCtrl.instance.Quake(2, 1u);
			return;
		case 2:
			if (psy.level < 6)
			{
				soundCtrl.instance.FadeOutSE(128 + (psy.level - 1), 60);
			}
			else
			{
				soundCtrl.instance.FadeOutSE(132, 60);
			}
			psy.cnt0 = 0;
			psy.r_no_1++;
			break;
		case 3:
			break;
		}
		if (psy.cnt0++ >= 30)
		{
			psy.r_no_0++;
			psy.r_no_1 = 0;
		}
	}

	private static void psylock_move_lock_appear()
	{
		int num = ((psy.level >= 6) ? 5 : psy.level);
		switch (psy.r_no_1)
		{
		case 0:
		{
			for (int k = 0; k < num; k++)
			{
				int num8 = 0;
				num8 = ((CurrentTitle != TitleId.GS2) ? (71 + k) : (41 + k));
				psy.lock_obj[k] = AnimationSystem.Instance.PlayObject(0, 0, num8);
				if (psy.lock_obj[k] != null)
				{
					Vector3 localPosition = lock_pos[psy.level - 1][k];
					localPosition.z = -31f;
					psy.lock_obj[k].transform.localPosition = localPosition;
				}
				psy.lock_obj[k].BeFlag |= 256;
				psy.lock_obj[k].BeFlag &= -536870913;
				psy.lock_obj[k].Alpha = 0f;
			}
			psy.cnt0 = 0;
			psy.r_no_1++;
			goto case 1;
		}
		case 1:
		{
			int num2 = 8;
			int num3 = 256;
			int num4 = 8;
			psy.cnt0++;
			for (int i = 0; i < num; i++)
			{
				int num5 = i * num2;
				int num6 = psy.cnt0 - num5;
				if (num6 > 0)
				{
					psy.lock_obj[i].BeFlag |= 536870912;
					if (num6 == num4)
					{
						soundCtrl.instance.PlaySE(115);
					}
					if (num6 < num4)
					{
						psy.lock_obj[i].Alpha = 1f;
						int num7 = 256 + num3 / num4 * (num4 - num6);
						psy.lock_obj[i].transform.localScale = new Vector3((float)num7 / 256f, (float)num7 / 256f, 1f);
					}
					else
					{
						psy.lock_obj[i].transform.localScale = Vector3.one;
						psy.lock_obj[i].BeFlag &= -257;
					}
				}
			}
			if (psy.cnt0 > num2 * num)
			{
				for (int j = 0; j < num; j++)
				{
				}
				psy.cnt0 = 0;
				psy.r_no_1++;
			}
			break;
		}
		case 2:
			ScreenCtrl.instance.Quake(2, 2u);
			if (psy.cnt0++ > 20)
			{
				fadeCtrl.instance.play(fadeCtrl.Status.WFADE_OUT, 1u, 8u);
				psy.cnt0 = 0;
				psy.r_no_1++;
			}
			break;
		case 3:
			if (psy.cnt0++ > 10)
			{
				fadeCtrl.instance.play(fadeCtrl.Status.WFADE_IN, 0u, 0u);
				psy.cnt0 = 0;
				psy.r_no_1++;
			}
			break;
		case 4:
			if (psy.cnt0++ > 40)
			{
				psy.cnt0 = 0;
				psy.r_no_1++;
			}
			break;
		case 5:
			psy.r_no_0 = 0;
			psy.r_no_1 = 0;
			break;
		}
	}

	private static void psylock_move_lock_unlock()
	{
		int[] array = new int[4] { 80, 125, 150, 165 };
		switch (psy.r_no_1)
		{
		case 0:
		{
			psy.rest--;
			soundCtrl.instance.PlaySE(116);
			Vector3 localPosition2 = psy.lock_obj[psy.rest].transform.localPosition;
			if (psy.lock_obj[psy.rest] != null)
			{
				psy.lock_obj[psy.rest].Stop();
				psy.lock_obj[psy.rest] = null;
			}
			psy.lock_destroy_obj[psy.rest] = Object.Instantiate(advCtrl.instance.message_system_.psylock_destroy_effect_prefab_);
			if (psy.lock_destroy_obj[psy.rest] != null)
			{
				Utility.SetLayerAll(psy.lock_destroy_obj[psy.rest].transform, LayerMask.NameToLayer("adv"));
				localPosition2 = DESTROY_EFFECT_OFFSET + localPosition2;
				localPosition2.z = 0f;
				float x2 = localPosition2.x;
				Vector3 eFFECT_SCREEN_HALF_SIZE3 = EFFECT_SCREEN_HALF_SIZE;
				localPosition2.x = x2 * (eFFECT_SCREEN_HALF_SIZE3.x / 960f);
				float y2 = localPosition2.y;
				Vector3 eFFECT_SCREEN_HALF_SIZE4 = EFFECT_SCREEN_HALF_SIZE;
				localPosition2.y = y2 * (eFFECT_SCREEN_HALF_SIZE4.y / 540f);
				psy.lock_destroy_obj[psy.rest].transform.localPosition = localPosition2;
				psy.lock_destroy_obj[psy.rest].transform.localScale = new Vector3(32.5f, 32.5f, -1f);
			}
			ScreenCtrl.instance.Quake(2, 2u);
			fadeCtrl.instance.play(fadeCtrl.Status.WFADE_OUT, 0u, 0u);
			psy.cnt0 = 0;
			psy.r_no_1++;
			break;
		}
		case 1:
			if (psy.cnt0++ > 2)
			{
				fadeCtrl.instance.play(fadeCtrl.Status.WFADE_IN, 0u, 0u);
				psy.cnt0 = 0;
				psy.r_no_1++;
			}
			break;
		case 2:
		{
			if (GSStatic.message_work_.op_para != 69)
			{
				if (psy.cnt0++ > 60)
				{
					if (psy.lock_destroy_obj[psy.rest] != null)
					{
						Object.Destroy(psy.lock_destroy_obj[psy.rest]);
						psy.lock_destroy_obj[psy.rest] = null;
					}
					psy.r_no_0 = 0;
					psy.r_no_1 = 0;
				}
				break;
			}
			for (int i = 0; i < 4; i++)
			{
				if (psy.cnt0 == array[i])
				{
					psy.rest--;
					soundCtrl.instance.PlaySE(116);
					Vector3 localPosition = psy.lock_obj[psy.rest].transform.localPosition;
					if (psy.lock_obj[psy.rest] != null)
					{
						psy.lock_obj[psy.rest].Stop();
						psy.lock_obj[psy.rest] = null;
					}
					psy.lock_destroy_obj[psy.rest] = Object.Instantiate(advCtrl.instance.message_system_.psylock_destroy_effect_prefab_);
					if (psy.lock_destroy_obj[psy.rest] != null)
					{
						Utility.SetLayerAll(psy.lock_destroy_obj[psy.rest].transform, LayerMask.NameToLayer("adv"));
						localPosition = DESTROY_EFFECT_OFFSET + localPosition;
						localPosition.z = 0f;
						float x = localPosition.x;
						Vector3 eFFECT_SCREEN_HALF_SIZE = EFFECT_SCREEN_HALF_SIZE;
						localPosition.x = x * (eFFECT_SCREEN_HALF_SIZE.x / 960f);
						float y = localPosition.y;
						Vector3 eFFECT_SCREEN_HALF_SIZE2 = EFFECT_SCREEN_HALF_SIZE;
						localPosition.y = y * (eFFECT_SCREEN_HALF_SIZE2.y / 540f);
						psy.lock_destroy_obj[psy.rest].transform.localPosition = localPosition;
						psy.lock_destroy_obj[psy.rest].transform.localScale = new Vector3(32.5f, 32.5f, -1f);
					}
				}
			}
			if (psy.cnt0++ > 300)
			{
				if (psy.lock_obj[psy.rest] != null)
				{
					Object.Destroy(psy.lock_obj[psy.rest]);
					psy.lock_obj[psy.rest] = null;
				}
				psy.r_no_0 = 0;
				psy.r_no_1 = 0;
			}
			break;
		}
		}
	}

	private static void psylock_move_chain_disappear()
	{
		int num = 0;
		num = ((GSStatic.global_work_.title != TitleId.GS3) ? psy.level : ((psy.level >= 6) ? 5 : psy.level));
		switch (psy.r_no_1)
		{
		case 0:
			psy.cnt0 = 0;
			psy.r_no_1++;
			goto case 1;
		case 1:
			if (psy.cnt0++ > 20)
			{
				psy.cnt0 = 0;
				psy.r_no_1++;
			}
			break;
		case 2:
			psy.cnt0 = 0;
			psy.r_no_1++;
			goto case 3;
		case 3:
			if (psy.cnt0++ > 4)
			{
				psy.r_no_1++;
			}
			break;
		case 4:
			if (psy.cnt0-- <= 0)
			{
				psy.cnt0 = 0;
				psy.r_no_1++;
			}
			break;
		case 5:
			if (psy.cnt0++ > 40)
			{
				GSPsyLockControl.instance.ChainAnimationDelete();
				GSPsyLockControl.instance.SetChainAnimation(psy.level + 12 - 1, psy.level + 12 + 6 - 1);
				soundCtrl.instance.PlaySE(128 + (num - 1));
				psy.r_no_1++;
			}
			break;
		case 6:
			if (!GSPsyLockControl.instance.IsChainAnimation())
			{
				soundCtrl.instance.FadeOutSE(128 + (num - 1), 60);
				psy.r_no_1++;
				psy.cnt0 = 0;
			}
			ScreenCtrl.instance.Quake(2, 1u);
			break;
		case 7:
			if (psy.cnt0++ > 80)
			{
				fadeCtrl.instance.play(fadeCtrl.Status.WFADE_OUT, 1u, 8u);
				psy.cnt0 = 0;
				psy.r_no_1++;
			}
			break;
		case 8:
			if (fadeCtrl.instance.status == fadeCtrl.Status.NO_FADE)
			{
				psy.cnt0 = 0;
				psy.r_no_1++;
			}
			break;
		case 9:
			if (psy.cnt0++ > 20)
			{
				psy.r_no_1++;
			}
			break;
		default:
			psy.r_no_0 = 0;
			psy.r_no_1 = 0;
			break;
		}
	}

	private static void psylock_move_unlock_message()
	{
		switch (psy.r_no_1)
		{
		case 0:
		{
			fadeCtrl.instance.play(fadeCtrl.Status.FADE_IN, 0u, 0u);
			uint objectFOA5 = 0u;
			uint objectFOA6 = 0u;
			switch (GSStatic.global_work_.title)
			{
			case TitleId.GS2:
				objectFOA5 = 57u;
				objectFOA6 = 58u;
				break;
			case TitleId.GS3:
				objectFOA5 = 87u;
				objectFOA6 = 88u;
				break;
			}
			AnimationObject animationObject = AnimationSystem.Instance.PlayObject((int)GSStatic.global_work_.title, 0, (int)objectFOA5);
			AnimationObject animationObject2 = AnimationSystem.Instance.PlayObject((int)GSStatic.global_work_.title, 0, (int)objectFOA6);
			animationObject.transform.localPosition += new Vector3(-960f, 0f);
			animationObject2.transform.localPosition += new Vector3(960f, 0f);
			soundCtrl.instance.PlaySE(166);
			psy.r_no_1++;
			break;
		}
		case 1:
		{
			uint objectFOA3 = 0u;
			uint objectFOA4 = 0u;
			switch (GSStatic.global_work_.title)
			{
			case TitleId.GS2:
				objectFOA3 = 57u;
				objectFOA4 = 58u;
				break;
			case TitleId.GS3:
				objectFOA3 = 87u;
				objectFOA4 = 88u;
				break;
			}
			AnimationObject animationObject = AnimationSystem.Instance.FindObject((int)GSStatic.global_work_.title, 0, (int)objectFOA3);
			AnimationObject animationObject2 = AnimationSystem.Instance.FindObject((int)GSStatic.global_work_.title, 0, (int)objectFOA4);
			if (animationObject != null)
			{
				Vector3 vector2 = new Vector3(45f, 0f);
				animationObject.transform.localPosition += vector2;
				animationObject2.transform.localPosition -= vector2;
				animationObject.BeFlag |= 536870912;
			}
			if (animationObject.transform.localPosition.x > 0f)
			{
				Vector3 localPosition = animationObject.transform.localPosition;
				localPosition.x = 0f;
				animationObject.transform.localPosition = localPosition;
				Vector3 localPosition2 = animationObject2.transform.localPosition;
				localPosition2.x = 0f;
				animationObject2.transform.localPosition = localPosition2;
				psy.r_no_1++;
			}
			break;
		}
		case 2:
		{
			fadeCtrl.instance.play(fadeCtrl.Status.WFADE_IN, 1u, 8u);
			uint stopFOA = 0u;
			uint stopFOA2 = 0u;
			uint objectFOA7 = 0u;
			switch (GSStatic.global_work_.title)
			{
			case TitleId.GS2:
				stopFOA = 57u;
				objectFOA7 = 56u;
				stopFOA2 = 58u;
				break;
			case TitleId.GS3:
				stopFOA = 87u;
				objectFOA7 = 86u;
				stopFOA2 = 88u;
				break;
			}
			AnimationSystem.Instance.StopObject((int)GSStatic.global_work_.title, 0, (int)stopFOA);
			AnimationSystem.Instance.StopObject((int)GSStatic.global_work_.title, 0, (int)stopFOA2);
			AnimationSystem.Instance.PlayObject((int)GSStatic.global_work_.title, 0, (int)objectFOA7);
			psy.r_no_1++;
			break;
		}
		case 3:
			if (fadeCtrl.instance.status == fadeCtrl.Status.NO_FADE)
			{
				psy.r_no_1++;
			}
			break;
		case 4:
		{
			uint objectFOA8 = 0u;
			switch (GSStatic.global_work_.title)
			{
			case TitleId.GS2:
				objectFOA8 = 56u;
				break;
			case TitleId.GS3:
				objectFOA8 = 86u;
				break;
			}
			AnimationObject animationObject = AnimationSystem.Instance.FindObject((int)GSStatic.global_work_.title, 0, (int)objectFOA8);
			if (animationObject == null || !animationObject.IsPlaying)
			{
				if (animationObject != null)
				{
					animationObject.Stop();
				}
				uint objectFOA9 = 0u;
				uint objectFOA10 = 0u;
				switch (GSStatic.global_work_.title)
				{
				case TitleId.GS2:
					objectFOA10 = 57u;
					objectFOA9 = 58u;
					break;
				case TitleId.GS3:
					objectFOA10 = 87u;
					objectFOA9 = 88u;
					break;
				}
				AnimationSystem.Instance.PlayObject((int)GSStatic.global_work_.title, 0, (int)objectFOA10);
				AnimationSystem.Instance.PlayObject((int)GSStatic.global_work_.title, 0, (int)objectFOA9);
				soundCtrl.instance.PlaySE(167);
				psy.r_no_1++;
			}
			break;
		}
		case 5:
		{
			uint objectFOA = 0u;
			uint objectFOA2 = 0u;
			switch (GSStatic.global_work_.title)
			{
			case TitleId.GS2:
				objectFOA = 57u;
				objectFOA2 = 58u;
				break;
			case TitleId.GS3:
				objectFOA = 87u;
				objectFOA2 = 88u;
				break;
			}
			AnimationObject animationObject = AnimationSystem.Instance.FindObject((int)GSStatic.global_work_.title, 0, (int)objectFOA);
			AnimationObject animationObject2 = AnimationSystem.Instance.FindObject((int)GSStatic.global_work_.title, 0, (int)objectFOA2);
			if (animationObject2 != null)
			{
				Vector3 vector = new Vector3(0f, 31.5f);
				animationObject.transform.localPosition += vector;
				animationObject2.transform.localPosition -= vector;
				animationObject2.BeFlag |= 536870912;
			}
			if (animationObject.transform.localPosition.y > 540f)
			{
				if (animationObject != null)
				{
					animationObject.Stop();
				}
				if (animationObject2 != null)
				{
					animationObject2.Stop();
				}
				psy.r_no_1++;
			}
			break;
		}
		case 6:
			if (!lifeGaugeCtrl.instance.is_lifegauge_moving())
			{
				psy.r_no_0 = 0;
				psy.r_no_1 = 0;
			}
			break;
		}
	}

	private static void psylock_move_clear_all()
	{
		int num = ((psy.level >= 6) ? 5 : psy.level);
		switch (psy.r_no_1)
		{
		case 0:
		{
			for (uint num2 = 0u; num2 < num; num2++)
			{
				if (psy.lock_obj[num2] != null)
				{
					psy.lock_obj[num2].Stop();
					psy.lock_obj[num2] = null;
				}
			}
			GSPsyLockControl.instance.ChainAnimationDelete();
			bgCtrl.instance.setNegaPosi(false);
			bgCtrl.instance.setColor(Color.white);
			if (bgCtrl.instance.bg_no == bgCtrl.Bg_GetRyuchijyoBgNo())
			{
				bgCtrl.instance.ChangeSubSpriteEnable(true);
			}
			uint num3 = GSStatic.global_work_.Map_data[GSStatic.global_work_.Room][0];
			bgCtrl.instance.SetSprite((int)num3);
			GSDemo.CheckBGChange(num3, 0u);
			bgCtrl.instance.bg_no_now = (int)num3;
			messageBoardCtrl.instance.guide_ctrl.changeGuide();
			if (GSStatic.global_work_.title == TitleId.GS2)
			{
				if (GSStatic.global_work_.scenario == 8 && bgCtrl.instance.bg_no == bgCtrl.Bg_GetRyuchijyoBgNo())
				{
					bgCtrl.instance.Bg256_SP_Flag &= 254;
					bgCtrl.instance.ChangeSubSpriteEnable(true);
				}
				if (GSStatic.global_work_.scenario == 11 && (long)bgCtrl.instance.bg_no == 66 && 0 < psy.rest)
				{
					animation_system.RevertPsyAcroBird();
				}
			}
			if ((bgCtrl.instance.Bg256_SP_Flag & 1) == 0)
			{
			}
			psy.r_no_0 = 0;
			psy.r_no_1 = 0;
			break;
		}
		}
	}

	private static void psylock_move_to_normal_bg()
	{
		int num = ((psy.level >= 6) ? 5 : psy.level);
		for (uint num2 = 0u; num2 < num; num2++)
		{
			if (psy.lock_obj[num2] != null)
			{
				psy.lock_obj[num2].Stop();
				psy.lock_obj[num2] = null;
			}
		}
		GSPsyLockControl.instance.ChainAnimationDelete();
		bgCtrl.instance.setColor(Color.clear);
		bgCtrl.instance.setNegaPosi(false);
		if (bgCtrl.instance.bg_no == bgCtrl.Bg_GetRyuchijyoBgNo())
		{
			bgCtrl.instance.ChangeSubSpriteEnable(false);
		}
		psy.r_no_0 = 0;
		psy.r_no_1 = 0;
	}

	private static void psylock_move_redisp()
	{
		int num = ((psy.level >= 6) ? 5 : psy.level);
		int rest = psy.rest;
		PsylockDisp_show_static(psy.level);
		psy.rest = (short)rest;
		bgCtrl.instance.setNegaPosi(true);
		bgCtrl.instance.setColor(Color.black);
		for (uint num2 = 0u; num2 < num - psy.rest; num2++)
		{
			if (psy.lock_obj[num - 1 - num2] != null)
			{
				psy.lock_obj[num - 1 - num2].Stop();
				psy.lock_obj[num - 1 - num2] = null;
			}
		}
		psy.r_no_0 = 0;
		psy.r_no_1 = 0;
	}

	private static void psylock_move_null()
	{
	}

	private static void PsylockDisp_show_static(int _level)
	{
		int num = ((psy.level >= 6) ? 5 : psy.level);
		PsylockDisp_init(_level);
		GSPsyLockControl.instance.SetChainAnimation(psy.level - 1, psy.level + 6 - 1);
		GSPsyLockControl.instance.ChainAnimationSetEndFlame();
		int num2 = 0;
		num2 = ((CurrentTitle != TitleId.GS2) ? 76 : 46);
		for (int i = 0; i < num; i++)
		{
			if (psy.lock_obj[i] == null)
			{
				psy.lock_obj[i] = AnimationSystem.Instance.PlayObject(0, 0, num2 + i);
			}
			if (psy.lock_obj[i] != null)
			{
				Vector3 vector = lock_pos[psy.level - 1][i];
				psy.lock_obj[i].transform.localPosition = new Vector3(vector.x, vector.y, -31f);
			}
		}
	}

	public static void PsylockDisp_reset_static()
	{
		PsylockDisp_clear_all();
		PsylockDisp_move();
		for (int i = 0; i < 5; i++)
		{
			if (psy.lock_obj[i] == null)
			{
				int num = 0;
				num = ((CurrentTitle != TitleId.GS2) ? (71 + i) : (41 + i));
				psy.lock_obj[i] = AnimationSystem.Instance.FindObject(0, 0, num);
			}
			if (psy.lock_obj[i] != null)
			{
				psy.lock_obj[i].Stop();
				psy.lock_obj[i] = null;
			}
		}
	}

	public static int is_on_psylock_flag_in_room(ushort room, ushort pl_id)
	{
		for (int i = 0; i < 4; i++)
		{
			PsylockData psylockData = GSStatic.global_work_.psylock[i];
			if ((psylockData.status & (true ? 1u : 0u)) != 0 && psylockData.room == room && psylockData.pl_id == pl_id)
			{
				return i;
			}
		}
		return -1;
	}

	public static int is_psylock_correct_item(PsylockData psy, ushort item_no)
	{
		int result = -1;
		for (int i = 0; i < psy.item_size; i++)
		{
			if (psy.item_no[i] == item_no)
			{
				result = i;
			}
		}
		return result;
	}

	public static void KeyObjDelete()
	{
		int num = ((psy.level >= 6) ? 5 : psy.level);
		for (uint num2 = 0u; num2 < num; num2++)
		{
			if (psy.lock_obj[num2] != null)
			{
				psy.lock_obj[num2].Stop();
				psy.lock_obj[num2] = null;
			}
		}
		GSPsyLockControl.instance.ChainAnimationDelete();
		bgCtrl.instance.setColor(Color.white);
		bgCtrl.instance.setNegaPosi(false);
	}

	public static void Set_Psylock_Mode(int mode, int flag)
	{
		switch (mode)
		{
		case 11:
			psy.Clear();
			psy.level = (short)flag;
			if (psy.level < 6)
			{
				psy.rest = (short)flag;
			}
			else
			{
				psy.rest = 5;
			}
			break;
		case 12:
			psy.r_no_0 = 12;
			psy.r_no_1 = 0;
			break;
		case 13:
			psy.r_no_0 = 2;
			psy.r_no_1 = 0;
			break;
		case 9:
			psy.r_no_0 = (short)mode;
			psy.r_no_1 = 0;
			break;
		case 10:
			break;
		}
	}
}
