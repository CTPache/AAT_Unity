using UnityEngine;

public class butterflyCtrl : MonoBehaviour
{
	public const ushort BUTTERFLY_TYPE_MASK = 255;

	public const ushort BUTTERFLY_NO_MASK = 65280;

	public const ushort BUTTERFLY_NONE = 0;

	public const ushort BUTTERFLY_ENTER = 1;

	public const ushort BUTTERFLY_LOOP = 2;

	public const ushort BUTTERFLY_KILL = 3;

	public const ushort BUTTERFLY_ESCAPE = 4;

	public const ushort BUTTERFLY_PAUSE = 5;

	public const ushort BUTTERFLY_PAUSE2 = 6;

	public const ushort BUTTERFLY_DELETE = 7;

	public const ushort BUTTERFLY_APPEAR = 8;

	public const ushort BUTTERFLY_RESTORE = 9;

	public const ushort BUTTERFLY_ESCAPE2 = 10;

	public const ushort BUTTERFLY_DAMAGE = 11;

	public const ushort BUTTERFLY_DAMAGE2 = 12;

	public const ushort CHOU_ERASE = 0;

	public const ushort CHOU_ENTER = 1;

	public const ushort CHOU_NONE = 2;

	public const ushort CHOU_RIGHT = 3;

	public const ushort CHOU_ESCAPE = 4;

	public const ushort CHOU_KILL = 5;

	public const ushort CHOU_MOVE = 6;

	public const ushort CHOU_STAY = 7;

	public const ushort CHOU_SET = 8;

	public const ushort CHOU_DAMAGE = 9;

	private static readonly sbyte[] Butterfly_Y = new sbyte[16]
	{
		0, 1, 3, 5, 8, 12, 16, 14, 13, 11,
		7, 4, 2, 0, 6, 4
	};

	private static readonly sbyte[] Butterfly_X = new sbyte[16]
	{
		0, -1, -3, -4, -2, 0, 3, 5, 0, 2,
		4, 2, -2, -4, -2, -1
	};

	private static readonly Vector3[] Butterfly_Offset = new Vector3[12]
	{
		new Vector3(-80f, -40f, 0f),
		new Vector3(-96f, -8f, 0f),
		new Vector3(-64f, 6f, 0f),
		new Vector3(64f, -16f, 0f),
		new Vector3(80f, -32f, 0f),
		new Vector3(90f, 8f, 0f),
		new Vector3(-180f, -80f, 0f),
		new Vector3(-192f, -48f, 0f),
		new Vector3(-160f, -32f, 0f),
		new Vector3(204f, -48f, 0f),
		new Vector3(200f, 0f, 0f),
		new Vector3(204f, 16f, 0f)
	};

	private static readonly byte[] Butterfly_pos = new byte[3] { 0, 2, 3 };

	private const ushort BUTTERFLY_RANDOM = 20;

	private static butterflyCtrl instance_ = null;

	public static butterflyCtrl instance
	{
		get
		{
			return instance_;
		}
	}

	private void Awake()
	{
		if (instance_ == null)
		{
			instance_ = this;
		}
	}

	public void Butterfly()
	{
		MessageWork message_work_ = GSStatic.message_work_;
		if (message_work_.choustate != 0)
		{
			AnimationObject characterAnimationObject = AnimationSystem.Instance.CharacterAnimationObject;
			if (((long)AnimationSystem.Instance.IdlingCharacterMasked != 11 || !characterAnimationObject.Exists) && message_work_.choustate != 5 && message_work_.choustate != 6)
			{
				message_work_.choustateBK = message_work_.choustate;
				message_work_.choustate = 5;
			}
		}
		switch ((ushort)(message_work_.choustate & 0xFF))
		{
		default:
			return;
		case 7:
		{
			for (uint num4 = 0u; num4 < 6; num4++)
			{
				AnimationObject animationObject3 = AnimationSystem.Instance.FindObject(2, 0, (int)(114 + num4));
				if (animationObject3 != null)
				{
					animationObject3.Z = animationObject3.DefaultZ;
					animationObject3.Stop(true);
					animationObject3.gameObject.SetActive(true);
				}
			}
			message_work_.choustate = 0;
			break;
		}
		case 5:
		{
			for (uint num5 = 0u; num5 < 6; num5++)
			{
				AnimationObject animationObject4 = AnimationSystem.Instance.FindObject(2, 0, (int)(114 + num5));
				if (animationObject4 != null)
				{
					animationObject4.BeFlag |= 134217728;
					animationObject4.gameObject.SetActive(false);
				}
			}
			if (message_work_.choustate == 5)
			{
				message_work_.choustate = 6;
			}
			else
			{
				message_work_.choustate = 0;
			}
			return;
		}
		case 0:
		case 6:
			return;
		case 9:
		{
			for (uint num8 = 0u; num8 < 6; num8++)
			{
				AnimationObject animationObject7 = AnimationSystem.Instance.FindObject(2, 0, (int)(114 + num8));
				if (animationObject7 != null)
				{
					animationObject7.BeFlag &= -134217729;
					animationObject7.gameObject.SetActive(true);
				}
			}
			if (message_work_.choustate != 0)
			{
				message_work_.choustate = message_work_.choustateBK;
			}
			break;
		}
		case 1:
		{
			for (int i = 0; i < 3; i++)
			{
				ChouWork chouWork4 = message_work_.chou[i];
				chouWork4.flg = 1;
				chouWork4.work[0] = (byte)(Butterfly_pos[i] + 6);
				chouWork4.work[1] = (byte)(i * 2);
				chouWork4.work[2] = (byte)(i * 3);
				chouWork4.work[3] = Butterfly_pos[i];
				chouWork4.work16 = 0;
				AnimationObject characterAnimationObject4 = AnimationSystem.Instance.CharacterAnimationObject;
				AnimationObject animationObject8 = AnimationSystem.Instance.PlayObject(2, 0, (int)(114L + (long)i));
				Vector3 localPosition5 = animationObject8.transform.localPosition;
				chouWork4.x = (short)Butterfly_Offset[chouWork4.work[0]].x;
				chouWork4.y = (short)Butterfly_Offset[chouWork4.work[0]].y;
				characterAnimationObject4.BeFlag &= -134217729;
				animationObject8.gameObject.SetActive(true);
				localPosition5.x = characterAnimationObject4.transform.localPosition.x + Butterfly_Offset[chouWork4.work[0]].x * 6.75f;
				localPosition5.y = characterAnimationObject4.transform.localPosition.y + (Butterfly_Offset[chouWork4.work[0]].y + 32f) * 6.75f;
				localPosition5.z = characterAnimationObject4.transform.localPosition.z - 1f;
				animationObject8.transform.localPosition = localPosition5;
			}
			message_work_.chou_no = 0;
			message_work_.chou_st = 0;
			message_work_.chou_cnt = 0;
			message_work_.choustate = 2;
			break;
		}
		case 3:
		{
			for (uint num6 = 0u; num6 < 3; num6++)
			{
				AnimationObject animationObject5 = AnimationSystem.Instance.FindObject(2, 0, (int)(114 + num6));
				if (animationObject5 != null)
				{
					AnimationObject animationObject6 = AnimationSystem.Instance.PlayObject(2, 0, (int)(117 + num6));
					Vector3 localPosition4 = animationObject6.transform.localPosition;
					localPosition4.x = animationObject5.transform.localPosition.x;
					localPosition4.y = animationObject5.transform.localPosition.y;
					animationObject6.transform.localPosition = localPosition4;
					animationObject5.Z = animationObject5.DefaultZ;
					animationObject5.Stop(true);
					message_work_.chou[num6].flg = 5;
					if (AnimationSystem.Instance.IsCharaMonochrom())
					{
						AnimationSystem.Instance.OBJ_monochrome2(117 + num6, 1, 31, 0, true);
					}
					if (num6 == 2)
					{
						message_work_.choustate = 2;
					}
				}
			}
			return;
		}
		case 8:
		{
			for (uint num3 = 0u; num3 < 3; num3++)
			{
				ChouWork chouWork2 = message_work_.chou[num3];
				chouWork2.flg = 7;
				chouWork2.work[0] = Butterfly_pos[num3];
				chouWork2.work[1] = (byte)(num3 * 2);
				chouWork2.work[2] = (byte)(num3 * 3);
				chouWork2.work[3] = 0;
				chouWork2.work16 = 0;
				AnimationObject characterAnimationObject3 = AnimationSystem.Instance.CharacterAnimationObject;
				AnimationObject animationObject2 = AnimationSystem.Instance.PlayObject(2, 0, (int)(114 + num3));
				Vector3 localPosition3 = animationObject2.transform.localPosition;
				localPosition3.x = characterAnimationObject3.transform.localPosition.x + Butterfly_Offset[chouWork2.work[0]].x * 6.75f;
				localPosition3.y = characterAnimationObject3.transform.localPosition.y + (Butterfly_Offset[chouWork2.work[0]].y + 32f) * 6.75f;
				localPosition3.z = characterAnimationObject3.transform.localPosition.z - 1f;
				animationObject2.transform.localPosition = localPosition3;
				if (characterAnimationObject3.IsMonochrom())
				{
					AnimationSystem.Instance.OBJ_monochrome2(114 + num3, 1, 31, 0, true);
				}
			}
			message_work_.chou_no = 0;
			message_work_.chou_st = 0;
			message_work_.chou_cnt = (ushort)((GSUtility.Rnd() & 0xF) * 20 * 2);
			message_work_.choustate = 2;
			break;
		}
		case 4:
		{
			for (uint num7 = 0u; num7 < 3; num7++)
			{
				ChouWork chouWork3 = message_work_.chou[num7];
				if ((chouWork3.flg & 0xF) == 1)
				{
					chouWork3.work[0] = chouWork3.work[3];
				}
				chouWork3.flg = 4;
			}
			message_work_.chou_no = 0;
			message_work_.chou_st = 0;
			message_work_.chou_cnt = 0;
			message_work_.choustate = 10;
			break;
		}
		case 11:
		{
			for (uint num = 0u; num < 3; num++)
			{
				AnimationObject characterAnimationObject2 = AnimationSystem.Instance.CharacterAnimationObject;
				AnimationObject animationObject = AnimationSystem.Instance.FindObject(2, 0, (int)(114 + num));
				if (animationObject != null && !animationObject.gameObject.activeSelf)
				{
					animationObject.BeFlag &= -134217729;
					animationObject.gameObject.SetActive(true);
					Vector3 localPosition = AnimationSystem.Instance.CharacterAnimationObject.transform.localPosition;
					Vector3 localPosition2 = animationObject.transform.localPosition;
					if (localPosition2.x > 2533f)
					{
						localPosition2.x -= 1573f;
					}
					else if (localPosition2.x < -2533f)
					{
						localPosition2.x += 3493f;
					}
					animationObject.transform.localPosition = localPosition2;
				}
			}
			for (uint num2 = 0u; num2 < 3; num2++)
			{
				ChouWork chouWork = message_work_.chou[num2];
				chouWork.flg = 9;
			}
			message_work_.chou_no = 0;
			message_work_.chou_st = 0;
			message_work_.chou_cnt = 0;
			message_work_.choustate = 12;
			break;
		}
		case 2:
		case 10:
		case 12:
			break;
		}
		uint num9 = 0u;
		for (uint num10 = 0u; num10 < 3; num10++)
		{
			ChouWork chouWork5 = message_work_.chou[num10];
			switch (chouWork5.flg & 0xF)
			{
			case 0:
			{
				AnimationObject animationObject9 = AnimationSystem.Instance.FindObject(2, 0, (int)(114 + num10));
				if (animationObject9 != null)
				{
					animationObject9.Z = animationObject9.DefaultZ;
					animationObject9.Stop(true);
					animationObject9.gameObject.SetActive(true);
				}
				uint num13 = 0u;
				num9 = 0u;
				for (; num13 < 3; num13++)
				{
					animationObject9 = AnimationSystem.Instance.FindObject(2, 0, (int)(114 + num13));
					if (animationObject9 == null)
					{
						num9++;
					}
				}
				if (num9 == 3)
				{
					message_work_.choustate = 0;
				}
				break;
			}
			case 1:
			{
				AnimationObject animationObject9 = AnimationSystem.Instance.FindObject(2, 0, (int)(114 + num10));
				if (animationObject9 != null)
				{
					AnimationObject characterAnimationObject5 = AnimationSystem.Instance.CharacterAnimationObject;
					ushort num11 = (ushort)((uint)(chouWork5.work[1] / 2) & 0xFu);
					ushort num12 = (ushort)((uint)(chouWork5.work[2] / 3) & 0xFu);
					chouWork5.x = (short)Butterfly_Offset[chouWork5.work[0]].x;
					chouWork5.y = (short)Butterfly_Offset[chouWork5.work[0]].y;
					Vector3 localPosition7 = animationObject9.transform.localPosition;
					localPosition7.x = characterAnimationObject5.transform.localPosition.x + (float)(chouWork5.x + Butterfly_X[num11]) * 6.75f;
					localPosition7.y = characterAnimationObject5.transform.localPosition.y + (float)(chouWork5.y + Butterfly_Y[num12] + 32) * 6.75f;
					localPosition7.z = characterAnimationObject5.transform.localPosition.z - 1f;
					animationObject9.transform.localPosition = localPosition7;
					if (Butterfly_Offset[chouWork5.work[0]].x < 0f)
					{
						animationObject9.BeFlag &= -2;
					}
					else
					{
						animationObject9.BeFlag |= 1;
					}
					chouWork5.work[1]++;
					chouWork5.work[2]++;
					init_move_butterfly(num10);
					chouWork5.flg = 6;
				}
				break;
			}
			case 5:
			{
				chouWork5.flg = 2;
				if (num10 != 2)
				{
					break;
				}
				uint num13 = 0u;
				num9 = 0u;
				for (; num13 < 3; num13++)
				{
					AnimationObject animationObject9 = AnimationSystem.Instance.FindObject(2, 0, (int)(117 + num10));
					if (animationObject9 == null)
					{
						num9++;
					}
				}
				if (num9 == 3)
				{
					message_work_.choustate = 0;
				}
				break;
			}
			case 7:
			{
				AnimationObject animationObject9 = AnimationSystem.Instance.FindObject(2, 0, (int)(114 + num10));
				if (!(animationObject9 != null))
				{
					break;
				}
				AnimationObject characterAnimationObject6 = AnimationSystem.Instance.CharacterAnimationObject;
				ushort num11 = (ushort)((uint)(chouWork5.work[1] / 2) & 0xFu);
				ushort num12 = (ushort)((uint)(chouWork5.work[2] / 3) & 0xFu);
				chouWork5.x = (short)Butterfly_Offset[chouWork5.work[0]].x;
				chouWork5.y = (short)Butterfly_Offset[chouWork5.work[0]].y;
				Vector3 localPosition8 = animationObject9.transform.localPosition;
				localPosition8.x = characterAnimationObject6.transform.localPosition.x + (float)(chouWork5.x + Butterfly_X[num11]) * 6.75f;
				localPosition8.y = characterAnimationObject6.transform.localPosition.y + (float)(chouWork5.y + Butterfly_Y[num12] + 32) * 6.75f;
				localPosition8.z = characterAnimationObject6.transform.localPosition.z - 1f;
				animationObject9.transform.localPosition = localPosition8;
				if (Butterfly_Offset[chouWork5.work[0]].x < 0f)
				{
					animationObject9.BeFlag &= -2;
				}
				else
				{
					animationObject9.BeFlag |= 1;
				}
				chouWork5.work[1]++;
				chouWork5.work[2]++;
				if (num10 != message_work_.chou_no || ++chouWork5.work16 <= message_work_.chou_cnt)
				{
					break;
				}
				short num14 = CheckMoveButterfly();
				if (num14 != -1)
				{
					message_work_.chou_cnt = (ushort)((GSUtility.Rnd() & 0xF) * 20);
					if (++message_work_.chou_no > 2)
					{
						message_work_.chou_no = 0;
					}
					chouWork5.work[3] = 0;
					chouWork5.work16 = 0;
				}
				else
				{
					InstructionButterfly(num10);
				}
				break;
			}
			case 4:
			{
				AnimationObject animationObject9 = AnimationSystem.Instance.FindObject(2, 0, (int)(114 + num10));
				if (animationObject9 != null)
				{
					chouWork5.work[3] = (byte)(Butterfly_pos[num10] + 6);
					init_move_butterfly(num10);
					chouWork5.flg = 6;
				}
				break;
			}
			case 6:
			{
				AnimationObject animationObject9 = AnimationSystem.Instance.FindObject(2, 0, (int)(114 + num10));
				if (animationObject9 != null)
				{
					move_butterfly(num10);
				}
				break;
			}
			case 9:
			{
				if (message_work_.chou_cnt < 15)
				{
					message_work_.chou_cnt++;
					break;
				}
				AnimationObject animationObject9 = AnimationSystem.Instance.FindObject(2, 0, (int)(114 + num10));
				if (animationObject9 != null)
				{
					Vector3 localPosition6 = animationObject9.transform.localPosition;
					localPosition6.x += 40.5f;
					localPosition6.y += 13.5f;
					if (localPosition6.y > 594f)
					{
						chouWork5.flg = 0;
					}
					else if (localPosition6.x > 1728f)
					{
						chouWork5.flg = 0;
					}
					localPosition6.y += 216f;
					animationObject9.transform.localPosition = localPosition6;
				}
				break;
			}
			}
		}
	}

	private void init_move_butterfly(uint no)
	{
		MessageWork message_work_ = GSStatic.message_work_;
		ChouWork chouWork = message_work_.chou[no];
		short x = chouWork.x;
		short y = chouWork.y;
		short num = (short)Butterfly_Offset[chouWork.work[3]].x;
		short num2 = (short)Butterfly_Offset[chouWork.work[3]].y;
		short ax = (short)((num > x) ? 1 : (-1));
		short dx = (short)((num <= x) ? (x - num) : (num - x));
		short ay = (short)((num2 > y) ? 1 : (-1));
		short dy = (short)((num2 <= y) ? (y - num2) : (num2 - y));
		chouWork.dx = dx;
		chouWork.dy = dy;
		chouWork.ax = ax;
		chouWork.ay = ay;
		chouWork.num = 0;
	}

	private void move_butterfly(uint no)
	{
		MessageWork message_work_ = GSStatic.message_work_;
		ChouWork chouWork = message_work_.chou[no];
		AnimationObject animationObject = AnimationSystem.Instance.FindObject(2, 0, (int)(114 + no));
		if (animationObject == null)
		{
			return;
		}
		AnimationObject characterAnimationObject = AnimationSystem.Instance.CharacterAnimationObject;
		short num = (short)Butterfly_Offset[chouWork.work[3]].x;
		short num2 = (short)Butterfly_Offset[chouWork.work[3]].y;
		if (chouWork.ax > 0)
		{
			animationObject.BeFlag &= -2;
		}
		else
		{
			animationObject.BeFlag |= 1;
		}
		if (chouWork.dx >= chouWork.dy)
		{
			chouWork.x += chouWork.ax;
			chouWork.E += (short)(chouWork.dy * 2);
			if (chouWork.E >= 0)
			{
				chouWork.y += chouWork.ay;
				chouWork.E -= (short)(chouWork.dx * 2);
			}
			chouWork.num++;
			if (chouWork.num >= chouWork.dx)
			{
				chouWork.x = num;
				chouWork.y = num2;
			}
		}
		else
		{
			chouWork.y += chouWork.ay;
			chouWork.E += (short)(chouWork.dx * 2);
			if (chouWork.E >= 0)
			{
				chouWork.x += chouWork.ax;
				chouWork.E -= (short)(chouWork.dy * 2);
			}
			chouWork.num++;
			if (chouWork.num >= chouWork.dy)
			{
				chouWork.x = num;
				chouWork.y = num2;
			}
		}
		if (chouWork.x == num && chouWork.y == num2)
		{
			chouWork.work[0] = chouWork.work[3];
			short num3 = (short)((chouWork.work[1] / 2) & 0xF);
			short num4 = (short)((chouWork.work[2] / 3) & 0xF);
			chouWork.x = (short)Butterfly_Offset[chouWork.work[0]].x;
			chouWork.y = (short)Butterfly_Offset[chouWork.work[0]].y;
			Vector3 localPosition = animationObject.transform.localPosition;
			Vector3 localPosition2 = characterAnimationObject.transform.localPosition;
			if (Butterfly_Offset[chouWork.work[0]].x < 0f)
			{
				animationObject.BeFlag &= -2;
			}
			else
			{
				animationObject.BeFlag |= 1;
			}
			if (message_work_.choustate == 10)
			{
				chouWork.flg = 0;
			}
			else
			{
				chouWork.flg = 7;
				message_work_.chou_cnt = (ushort)((GSUtility.Rnd() & 0xF) * 20 * 2);
				if (++message_work_.chou_no > 2)
				{
					message_work_.chou_no = 0;
				}
			}
			localPosition.x = localPosition2.x + (float)(chouWork.x + Butterfly_X[num3]) * 6.75f;
			localPosition.y = localPosition2.y + (float)(chouWork.y + Butterfly_Y[num4] + 32) * 6.75f;
			localPosition.z = localPosition2.z - 1f;
			animationObject.transform.localPosition = localPosition;
			chouWork.work[3] = 0;
			chouWork.work16 = 0;
		}
		else
		{
			short num3 = (short)((chouWork.work[1] / 2) & 0xF);
			short num4 = (short)((chouWork.work[2] / 3) & 0xF);
			Vector3 localPosition3 = animationObject.transform.localPosition;
			Vector3 localPosition4 = characterAnimationObject.transform.localPosition;
			localPosition3.x = localPosition4.x + (float)(chouWork.x + Butterfly_X[num3]) * 6.75f;
			localPosition3.y = localPosition4.y + (float)(chouWork.y + Butterfly_Y[num4] + 32) * 6.75f;
			animationObject.transform.localPosition = localPosition3;
		}
		chouWork.work[1]++;
		chouWork.work[2]++;
	}

	private short CheckButterfly(uint no)
	{
		MessageWork message_work_ = GSStatic.message_work_;
		for (uint num = 0u; num < 3; num++)
		{
			ChouWork chouWork = message_work_.chou[num];
			if (chouWork.work[0] == no)
			{
				return (short)num;
			}
		}
		return -1;
	}

	private short CheckMoveButterfly()
	{
		MessageWork message_work_ = GSStatic.message_work_;
		for (uint num = 0u; num < 3; num++)
		{
			ChouWork chouWork = message_work_.chou[num];
			if (chouWork.flg == 6)
			{
				return (short)num;
			}
		}
		return -1;
	}

	private short CheckLRButterfly(ushort pos)
	{
		MessageWork message_work_ = GSStatic.message_work_;
		short num = 0;
		for (uint num2 = 0u; num2 < 3; num2++)
		{
			ChouWork chouWork = message_work_.chou[num2];
			if (pos == 0)
			{
				if (chouWork.work[0] < 3)
				{
					num++;
				}
			}
			else if (chouWork.work[0] > 2)
			{
				num++;
			}
		}
		return num;
	}

	private void InstructionButterfly(uint no)
	{
		MessageWork message_work_ = GSStatic.message_work_;
		ChouWork chouWork = message_work_.chou[no];
		ushort num = 0;
		switch (chouWork.work[0])
		{
		case 0:
		{
			short num2 = CheckButterfly(1u);
			if (num2 != -1)
			{
				num2 = CheckButterfly(2u);
				if (num2 != -1)
				{
					message_work_.chou_cnt = (ushort)((GSUtility.Rnd() & 0xF) * 20);
					if (++message_work_.chou_no > 2)
					{
						message_work_.chou_no = 0;
					}
					chouWork.work[3] = 0;
					chouWork.work16 = 0;
					break;
				}
				chouWork.work[3] = 2;
			}
			else
			{
				chouWork.work[3] = 1;
			}
			init_move_butterfly(no);
			chouWork.flg = 6;
			break;
		}
		case 1:
		{
			short num2 = (short)(((GSUtility.Rnd() & 0xF) * (GSUtility.Rnd() & 0xF)) & 3);
			switch (num2)
			{
			case 0:
				num2 = CheckButterfly(0u);
				num = 0;
				break;
			case 1:
				num2 = CheckButterfly(2u);
				num = 2;
				break;
			case 2:
			case 3:
				num2 = CheckLRButterfly(1);
				if (num2 < 2)
				{
					num2 = CheckButterfly(5u);
					num = 5;
				}
				break;
			}
			if (num2 != -1)
			{
				message_work_.chou_cnt = (ushort)((GSUtility.Rnd() & 0xF) * 20);
				if (++message_work_.chou_no > 2)
				{
					message_work_.chou_no = 0;
				}
				chouWork.work[3] = 0;
				chouWork.work16 = 0;
			}
			else
			{
				chouWork.work[3] = (byte)num;
				init_move_butterfly(no);
				chouWork.flg = 6;
			}
			break;
		}
		case 2:
		{
			short num2 = (short)(((GSUtility.Rnd() & 0xF) * (GSUtility.Rnd() & 0xF)) & 3);
			switch (num2)
			{
			case 0:
				num2 = CheckButterfly(0u);
				num = 0;
				break;
			case 1:
				num2 = CheckButterfly(1u);
				num = 1;
				break;
			case 2:
				num2 = CheckButterfly(0u);
				num = 0;
				break;
			case 3:
				num2 = CheckButterfly(1u);
				num = 1;
				break;
			}
			if (num2 != -1)
			{
				message_work_.chou_cnt = (ushort)((GSUtility.Rnd() & 0xF) * 20);
				if (++message_work_.chou_no > 2)
				{
					message_work_.chou_no = 0;
				}
				chouWork.work[3] = 0;
				chouWork.work16 = 0;
			}
			else
			{
				chouWork.work[3] = (byte)num;
				init_move_butterfly(no);
				chouWork.flg = 6;
			}
			break;
		}
		case 3:
		{
			short num2 = (short)(((GSUtility.Rnd() & 0xF) * (GSUtility.Rnd() & 0xF)) & 3);
			switch (num2)
			{
			case 0:
				num2 = CheckButterfly(5u);
				num = 5;
				break;
			case 1:
				num2 = CheckButterfly(4u);
				num = 4;
				break;
			case 2:
			case 3:
				num2 = CheckLRButterfly(0);
				if (num2 < 2)
				{
					num2 = CheckButterfly(2u);
					num = 2;
				}
				break;
			}
			if (num2 != -1)
			{
				message_work_.chou_cnt = (ushort)((GSUtility.Rnd() & 0xF) * 20);
				if (++message_work_.chou_no > 2)
				{
					message_work_.chou_no = 0;
				}
				chouWork.work16 = 0;
				chouWork.work[3] = 0;
			}
			else
			{
				chouWork.work[3] = (byte)num;
				init_move_butterfly(no);
				chouWork.flg = 6;
			}
			break;
		}
		case 4:
		{
			short num2 = (short)(((GSUtility.Rnd() & 0xF) * (GSUtility.Rnd() & 0xF)) & 3);
			switch (num2)
			{
			case 0:
				num2 = CheckButterfly(3u);
				num = 3;
				break;
			case 1:
				num2 = CheckButterfly(5u);
				num = 5;
				break;
			case 2:
				num2 = CheckButterfly(5u);
				num = 5;
				break;
			case 3:
				num2 = 1;
				break;
			}
			if (num2 != -1)
			{
				message_work_.chou_cnt = (ushort)((GSUtility.Rnd() & 0xF) * 20);
				if (++message_work_.chou_no > 2)
				{
					message_work_.chou_no = 0;
				}
				chouWork.work[3] = 0;
				chouWork.work16 = 0;
			}
			else
			{
				chouWork.work[3] = (byte)num;
				init_move_butterfly(no);
				chouWork.flg = 6;
			}
			break;
		}
		case 5:
		{
			short num2 = (short)(((GSUtility.Rnd() & 0xF) * (GSUtility.Rnd() & 0xF)) & 3);
			switch (num2)
			{
			case 0:
				num2 = CheckButterfly(3u);
				num = 3;
				break;
			case 1:
				num2 = CheckButterfly(4u);
				num = 4;
				break;
			case 2:
				num2 = CheckButterfly(3u);
				num = 3;
				break;
			case 4:
				num2 = CheckButterfly(4u);
				num = 4;
				break;
			}
			if (num2 != -1)
			{
				message_work_.chou_cnt = (ushort)((GSUtility.Rnd() & 0xF) * 20);
				if (++message_work_.chou_no > 2)
				{
					message_work_.chou_no = 0;
				}
				chouWork.work[3] = 0;
				chouWork.work16 = 0;
			}
			else
			{
				chouWork.work[3] = (byte)num;
				init_move_butterfly(no);
				chouWork.flg = 6;
			}
			break;
		}
		}
	}
}
