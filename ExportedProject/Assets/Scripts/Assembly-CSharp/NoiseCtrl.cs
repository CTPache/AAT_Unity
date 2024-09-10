using System.Collections.Generic;
using UnityEngine;

public class NoiseCtrl : MonoBehaviour
{
	private struct LINE01
	{
		public int pos_x;

		public int time;

		public int wait;
	}

	private struct QuakeWork
	{
		public ushort time;

		public short Qx;

		public short Qy;
	}

	public class LINE_WORK
	{
		public int pos_x;

		public int type;

		public int state;

		public int time;

		public int mNLinPosX;

		public int[] work = new int[8];
	}

	private class LW
	{
		public int Bright;

		public int Btime;

		public int time;

		public int Qtime;

		public int Qcount;

		public int Noise;

		public int NoiseSW;

		public int NoisePara;

		public int LineFlag;

		public short BG3HOFS;

		public short BG3VOFS;

		public LINE_WORK[] lew_ = new LINE_WORK[4];
	}

	private ushort[] Bdata = new ushort[16]
	{
		12, 20, 32, 40, 44, 50, 60, 68, 77, 88,
		96, 108, 128, 134, 144, 160
	};

	private static NoiseCtrl instance_;

	[SerializeField]
	public List<SpriteRenderer> line_list_ = new List<SpriteRenderer>();

	[SerializeField]
	public List<SpriteRenderer> noise_list_ = new List<SpriteRenderer>();

	[SerializeField]
	private GameObject body_;

	[SerializeField]
	private GameObject noise_body_;

	[SerializeField]
	private Transform bg_body_;

	[SerializeField]
	private SpriteRenderer bright_sprite_;

	private const int E_OP4_DEMO_LINE = 3;

	private const int E_OP4_DEMO_NOISE = 64;

	private LW lw_ = new LW();

	private QuakeWork[] Qdata = new QuakeWork[12];

	private LINE01[] Line01Data = new LINE01[8];

	private LINE01[] Line02Data = new LINE01[8];

	public static NoiseCtrl instance
	{
		get
		{
			return instance_;
		}
	}

	public Transform bg_body
	{
		get
		{
			return bg_body_;
		}
		set
		{
			bg_body_ = value;
		}
	}

	public int NoiseSW
	{
		get
		{
			return lw_.NoiseSW;
		}
	}

	private void Awake()
	{
		instance_ = this;
		int num = 0;
		Qdata[num].time = 12;
		Qdata[num].Qx = 0;
		Qdata[num].Qy = -4;
		Qdata[++num].time = 78;
		Qdata[num].Qx = 0;
		Qdata[num].Qy = 2;
		Qdata[++num].time = 150;
		Qdata[num].Qx = 0;
		Qdata[num].Qy = -4;
		Qdata[++num].time = 214;
		Qdata[num].Qx = 0;
		Qdata[num].Qy = 2;
		Qdata[++num].time = 288;
		Qdata[num].Qx = 0;
		Qdata[num].Qy = 4;
		Qdata[++num].time = 345;
		Qdata[num].Qx = 0;
		Qdata[num].Qy = -4;
		Qdata[++num].time = 420;
		Qdata[num].Qx = 0;
		Qdata[num].Qy = 2;
		Qdata[++num].time = 488;
		Qdata[num].Qx = 0;
		Qdata[num].Qy = -3;
		Qdata[++num].time = 512;
		Qdata[num].Qx = 0;
		Qdata[num].Qy = 2;
		Qdata[++num].time = 588;
		Qdata[num].Qx = 0;
		Qdata[num].Qy = -2;
		Qdata[++num].time = 640;
		Qdata[num].Qx = 0;
		Qdata[num].Qy = 3;
		Qdata[++num].time = 670;
		Qdata[num].Qx = 0;
		Qdata[num].Qy = 0;
		num = 0;
		Line01Data[num].pos_x = 32;
		Line01Data[num].time = 8;
		Line01Data[num].wait = 3;
		Line01Data[++num].pos_x = 24;
		Line01Data[num].time = 16;
		Line01Data[num].wait = 2;
		Line01Data[++num].pos_x = 12;
		Line01Data[num].time = 24;
		Line01Data[num].wait = 3;
		Line01Data[++num].pos_x = 8;
		Line01Data[num].time = 32;
		Line01Data[num].wait = 2;
		Line01Data[++num].pos_x = 64;
		Line01Data[num].time = 36;
		Line01Data[num].wait = 4;
		Line01Data[++num].pos_x = 28;
		Line01Data[num].time = 42;
		Line01Data[num].wait = 2;
		Line01Data[++num].pos_x = 10;
		Line01Data[num].time = 50;
		Line01Data[num].wait = 3;
		Line01Data[++num].pos_x = 48;
		Line01Data[num].time = 60;
		Line01Data[num].wait = 2;
		num = 0;
		Line02Data[num].pos_x = 200;
		Line02Data[num].time = 20;
		Line02Data[num].wait = 3;
		Line02Data[++num].pos_x = 230;
		Line02Data[num].time = 32;
		Line02Data[num].wait = 2;
		Line02Data[++num].pos_x = 180;
		Line02Data[num].time = 48;
		Line02Data[num].wait = 3;
		Line02Data[++num].pos_x = 130;
		Line02Data[num].time = 60;
		Line02Data[num].wait = 2;
		Line02Data[++num].pos_x = 192;
		Line02Data[num].time = 72;
		Line02Data[num].wait = 4;
		Line02Data[++num].pos_x = 160;
		Line02Data[num].time = 88;
		Line02Data[num].wait = 2;
		Line02Data[++num].pos_x = 200;
		Line02Data[num].time = 96;
		Line02Data[num].wait = 3;
		Line02Data[++num].pos_x = 220;
		Line02Data[num].time = 100;
		Line02Data[num].wait = 2;
		SpriteRenderer original = line_list_[0];
		for (int i = 0; i < 2; i++)
		{
			line_list_.Add(Object.Instantiate(original));
			line_list_[i + 1].transform.SetParent(body_.transform);
			line_list_[i + 1].transform.localScale = new Vector3(1f, 1100f, 1f);
			line_list_[i + 1].transform.localPosition = Vector3.zero;
		}
		original = noise_list_[0];
		for (int j = 0; j < 63; j++)
		{
			noise_list_.Add(Object.Instantiate(original));
			noise_list_[j + 1].transform.SetParent(noise_body_.transform);
			noise_list_[j + 1].transform.localScale = Vector3.one * 0.5f;
		}
		lw_.lew_[0] = new LINE_WORK();
		lw_.lew_[1] = new LINE_WORK();
		lw_.lew_[2] = new LINE_WORK();
		body_.SetActive(false);
	}

	public void FILM_Effect(int type)
	{
		if (lw_.NoiseSW != 2)
		{
			if (++lw_.Noise >= 2)
			{
				lw_.Noise = 0;
				NoiseView();
			}
			else
			{
				noise_body_.SetActive(false);
			}
		}
		else
		{
			noise_body_.SetActive(false);
		}
		if (type != 1)
		{
			if (++lw_.time >= Qdata[lw_.Qtime].time)
			{
				lw_.BG3HOFS = Qdata[lw_.Qtime].Qx;
				if (lw_.BG3VOFS < -128)
				{
					lw_.BG3VOFS = -272;
				}
				else
				{
					lw_.BG3VOFS = 0;
				}
				lw_.BG3VOFS += Qdata[lw_.Qtime].Qy;
				bg_body_.localPosition = Vector3.up * lw_.BG3VOFS * 4.5f;
				if (++lw_.Qtime > 11)
				{
					if (lw_.BG3VOFS < -128)
					{
						lw_.BG3VOFS = -272;
					}
					else
					{
						lw_.BG3VOFS = 0;
					}
					lw_.BG3HOFS = 0;
					lw_.time = 0;
					lw_.Qtime = 0;
					lw_.Qcount = 0;
					bg_body_.localPosition = Vector3.zero;
				}
			}
			else
			{
				if (lw_.BG3VOFS < -128)
				{
					lw_.BG3VOFS = -272;
				}
				else
				{
					lw_.BG3VOFS = 0;
				}
				lw_.BG3HOFS = 0;
				bg_body_.localPosition = Vector3.zero;
			}
			if (lw_.NoiseSW != 1)
			{
			}
		}
		if (++lw_.Btime >= Bdata[lw_.Bright])
		{
			if (lw_.NoiseSW != 0)
			{
				if ((lw_.Bright & 1) <= 0)
				{
				}
			}
			else if ((lw_.Bright & 1) > 0)
			{
				bright_sprite_.color = new Color(0f, 0f, 0f, 0.1f);
			}
			else
			{
				bright_sprite_.color = new Color(0f, 0f, 0f, 0f);
			}
			if (++lw_.Bright > 15)
			{
				lw_.Bright = 0;
				lw_.Btime = 0;
			}
		}
		LINE_WORK lINE_WORK = lw_.lew_[0];
		line_list_[0].gameObject.SetActive(false);
		lINE_WORK = lw_.lew_[1];
		switch (lINE_WORK.state)
		{
		case 0:
			lINE_WORK.mNLinPosX = 32;
			lINE_WORK.time = 0;
			lINE_WORK.work[0] = 0;
			lINE_WORK.work[1] = 0;
			lINE_WORK.work[2] = 0;
			lINE_WORK.state = 1;
			break;
		case 1:
		case 2:
		case 3:
		case 4:
		case 5:
		case 6:
		case 7:
		case 8:
			lINE_WORK.time++;
			if (lINE_WORK.work[2] == 1)
			{
				if (lINE_WORK.time >= lINE_WORK.work[1])
				{
					line_list_[1].transform.localPosition = Vector3.right * -32f * 4.5f;
					lINE_WORK.work[2] = 0;
					lINE_WORK.state++;
				}
			}
			else if (lINE_WORK.time >= Line01Data[lINE_WORK.state - 1].time)
			{
				line_list_[1].transform.localPosition = Vector3.right * Line01Data[lINE_WORK.state - 1].pos_x * 4.5f * 1.5f;
				lINE_WORK.work[1] = lINE_WORK.time + Line01Data[lINE_WORK.state - 1].wait;
				lINE_WORK.work[2] = 1;
			}
			break;
		case 9:
			if (++lINE_WORK.work[2] > 60)
			{
				lINE_WORK.state = 0;
			}
			break;
		}
		lINE_WORK = lw_.lew_[2];
		switch (lINE_WORK.state)
		{
		case 0:
			lINE_WORK.mNLinPosX = 208;
			lINE_WORK.time = 0;
			lINE_WORK.work[0] = 0;
			lINE_WORK.work[1] = 0;
			lINE_WORK.work[2] = 0;
			lINE_WORK.state = 1;
			break;
		case 1:
		case 2:
		case 3:
		case 4:
		case 5:
		case 6:
		case 7:
		case 8:
			lINE_WORK.time++;
			if (lINE_WORK.work[2] == 1)
			{
				if (lINE_WORK.time >= lINE_WORK.work[1])
				{
					line_list_[2].transform.localPosition = Vector3.right * -32f * 4.5f;
					lINE_WORK.work[2] = 0;
					lINE_WORK.state++;
				}
			}
			else if (lINE_WORK.time >= Line02Data[lINE_WORK.state - 1].time)
			{
				line_list_[2].transform.localPosition = Vector3.right * Line02Data[lINE_WORK.state - 1].pos_x * 1.5f * 4.5f;
				lINE_WORK.work[1] = lINE_WORK.time + Line02Data[lINE_WORK.state - 1].wait;
				lINE_WORK.work[2] = 1;
			}
			break;
		case 9:
			if (++lINE_WORK.work[2] > 40)
			{
				lINE_WORK.state = 0;
			}
			break;
		}
	}

	public void FILM_EffectInit()
	{
		MessageWork activeMessageWork = MessageSystem.GetActiveMessageWork();
		NoiseInit();
		lw_.lew_ = new LINE_WORK[4]
		{
			new LINE_WORK(),
			new LINE_WORK(),
			new LINE_WORK(),
			new LINE_WORK()
		};
		int num = 0;
		LINE_WORK[] lew_ = lw_.lew_;
		foreach (LINE_WORK lINE_WORK in lew_)
		{
			lINE_WORK.pos_x = 0;
			lINE_WORK.type = num;
			lINE_WORK.state = 0;
			lINE_WORK.time = 0;
			for (int j = 0; j < lINE_WORK.work.Length; j++)
			{
				lINE_WORK.work[j] = 0;
			}
			num++;
		}
		lw_.NoiseSW = activeMessageWork.all_work[0];
		switch (lw_.NoiseSW)
		{
		case 0:
			lw_.NoisePara = 64;
			break;
		case 1:
			lw_.NoisePara = 24;
			break;
		case 2:
			lw_.NoisePara = 0;
			break;
		case 3:
			lw_.NoisePara = 24;
			break;
		case 4:
			lw_.NoisePara = 24;
			break;
		}
		foreach (SpriteRenderer item in noise_list_)
		{
			item.gameObject.SetActive(true);
			item.transform.localPosition = new Vector3(-1000f, 0f, 0f);
		}
		foreach (SpriteRenderer item2 in line_list_)
		{
			item2.gameObject.SetActive(true);
			item2.transform.localPosition = new Vector3(-1000f, 0f, 0f);
		}
		lw_.time = 0;
		lw_.Bright = 0;
		lw_.Btime = 0;
		lw_.Qtime = 0;
		lw_.Qcount = 0;
		lw_.Noise = 0;
		lw_.LineFlag = 0;
		SetLineColor(Color.black);
		bright_sprite_.color = Color.clear;
	}

	public void NoiseInit()
	{
		body_.SetActive(true);
	}

	public void NoiseView()
	{
		noise_body_.SetActive(true);
		foreach (SpriteRenderer item in noise_list_)
		{
			float num = Random.Range(0f, 1920f);
			float num2 = Random.Range(-540f, 540f);
			item.gameObject.SetActive(true);
			item.transform.localPosition = Vector3.right * num;
			item.transform.localPosition += Vector3.up * num2;
		}
	}

	public void NoiseDelete()
	{
		bg_body_.localPosition = Vector3.zero;
		body_.SetActive(false);
	}

	public void SetLineColor(Color color)
	{
		foreach (SpriteRenderer item in line_list_)
		{
			item.color = color;
		}
	}
}
