using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PcViewCtrl : MonoBehaviour
{
	public enum gs3_op4_sprite
	{
		E_OP4_DEMO_SPR_7 = 0,
		E_OP4_DEMO_SPR_8 = 1,
		E_OP4_DEMO_SPR_9 = 2,
		E_OP4_DEMO_SPR_A = 3,
		E_OP4_DEMO_SPR_B = 4,
		E_OP4_DEMO_SPR_C = 5,
		E_OP4_DEMO_SPR_D = 6,
		E_OP4_DEMO_SPR_E = 7,
		E_OP4_DEMO_SPR_F = 8,
		E_OP4_DEMO_SPR_G = 9
	}

	[Serializable]
	public class FontImageData
	{
		private ushort[] font_count_j_ = new ushort[8] { 9, 8, 6, 9, 11, 11, 11, 17 };

		private ushort[] font_count_u_ = new ushort[10] { 18, 26, 24, 22, 18, 18, 18, 28, 19, 12 };

		private ushort[] font_count_f_ = new ushort[10] { 20, 34, 25, 18, 23, 20, 17, 20, 30, 13 };

		private ushort[] font_count_g_ = new ushort[10] { 18, 25, 20, 20, 23, 21, 14, 26, 28, 14 };

		private ushort[] font_count_k_ = new ushort[8] { 11, 8, 6, 12, 15, 12, 14, 19 };

		private ushort[] font_count_s_ = new ushort[8] { 9, 8, 6, 9, 11, 13, 12, 11 };

		private ushort[] font_count_t_ = new ushort[8] { 9, 8, 6, 9, 13, 13, 12, 12 };

		private float[][] fillAmount_rate_u = new float[10][]
		{
			new float[19]
			{
				0.035f, 0.06f, 0.09f, 0.12f, 0.14f, 0.14f, 0.17f, 0.195f, 0.216f, 0.234f,
				0.256f, 0.256f, 0.29f, 0.315f, 0.345f, 0.355f, 0.382f, 0.402f, 0.402f
			},
			new float[27]
			{
				0.035f, 0.06f, 0.085f, 0.103f, 0.128f, 0.153f, 0.178f, 0.178f, 0.197f, 0.21f,
				0.235f, 0.256f, 0.281f, 0.306f, 0.331f, 0.339f, 0.362f, 0.387f, 0.412f, 0.412f,
				0.442f, 0.467f, 0.489f, 0.514f, 0.539f, 0.564f, 0.564f
			},
			new float[24]
			{
				0.035f, 0.055f, 0.08f, 0.098f, 0.123f, 0.143f, 0.168f, 0.193f, 0.218f, 0.218f,
				0.246f, 0.271f, 0.294f, 0.31f, 0.335f, 0.335f, 0.376f, 0.401f, 0.426f, 0.451f,
				0.458f, 0.471f, 0.496f, 0.496f
			},
			new float[23]
			{
				0.035f, 0.055f, 0.065f, 0.092f, 0.117f, 0.117f, 0.152f, 0.172f, 0.197f, 0.222f,
				0.246f, 0.256f, 0.278f, 0.303f, 0.328f, 0.328f, 0.353f, 0.3766f, 0.4026f, 0.4126f,
				0.4376f, 0.4626f, 0.4626f
			},
			new float[19]
			{
				0.04f, 0.065f, 0.09f, 0.09f, 0.13f, 0.142f, 0.162f, 0.187f, 0.212f, 0.212f,
				0.235f, 0.259f, 0.285f, 0.285f, 0.31f, 0.335f, 0.36f, 0.385f, 0.385f
			},
			new float[19]
			{
				0.04f, 0.065f, 0.08f, 0.105f, 0.13f, 0.149f, 0.172f, 0.197f, 0.21f, 0.21f,
				0.246f, 0.271f, 0.29f, 0.315f, 0.337f, 0.36f, 0.382f, 0.398f, 0.398f
			},
			new float[19]
			{
				0.035f, 0.06f, 0.07f, 0.095f, 0.11f, 0.12f, 0.145f, 0.145f, 0.185f, 0.21f,
				0.24f, 0.254f, 0.279f, 0.304f, 0.329f, 0.352f, 0.377f, 0.577f, 0.577f
			},
			new float[29]
			{
				0.035f, 0.06f, 0.082f, 0.105f, 0.128f, 0.143f, 0.165f, 0.181f, 0.206f, 0.231f,
				0.231f, 0.271f, 0.296f, 0.296f, 0.336f, 0.358f, 0.383f, 0.393f, 0.418f, 0.418f,
				0.463f, 0.488f, 0.513f, 0.535f, 0.548f, 0.573f, 0.585f, 0.61f, 0.61f
			},
			new float[20]
			{
				0.03f, 0.055f, 0.08f, 0.1f, 0.115f, 0.115f, 0.165f, 0.165f, 0.195f, 0.22f,
				0.245f, 0.26f, 0.285f, 0.285f, 0.325f, 0.34f, 0.355f, 0.38f, 0.43f, 0.43f
			},
			new float[12]
			{
				0.03f, 0.04f, 0.065f, 0.065f, 0.1f, 0.123f, 0.145f, 0.168f, 0.191f, 0.214f,
				0.234f, 0.234f
			}
		};

		private float[][] fillAmount_rate_f = new float[10][]
		{
			new float[21]
			{
				0.034f, 0.059f, 0.092f, 0.105f, 0.118f, 0.131f, 0.158f, 0.183f, 0.207f, 0.218f,
				0.24f, 0.253f, 0.277f, 0.287f, 0.313f, 0.333f, 0.343f, 0.376f, 0.401f, 0.418f,
				0.418f
			},
			new float[35]
			{
				0.034f, 0.057f, 0.081f, 0.099f, 0.119f, 0.132f, 0.157f, 0.168f, 0.191f, 0.214f,
				0.237f, 0.26f, 0.281f, 0.303f, 0.32f, 0.33f, 0.354f, 0.378f, 0.39f, 0.403f,
				0.415f, 0.434f, 0.456f, 0.481f, 0.498f, 0.511f, 0.523f, 0.556f, 0.579f, 0.603f,
				0.621f, 0.638f, 0.658f, 0.684f, 0.684f
			},
			new float[25]
			{
				0.03f, 0.055f, 0.078f, 0.095f, 0.119f, 0.142f, 0.165f, 0.19f, 0.202f, 0.214f,
				0.227f, 0.251f, 0.275f, 0.285f, 0.308f, 0.332f, 0.345f, 0.369f, 0.394f, 0.406f,
				0.437f, 0.462f, 0.482f, 0.498f, 0.498f
			},
			new float[19]
			{
				0.033f, 0.058f, 0.078f, 0.103f, 0.125f, 0.138f, 0.159f, 0.184f, 0.209f, 0.222f,
				0.246f, 0.269f, 0.293f, 0.314f, 0.324f, 0.349f, 0.373f, 0.387f, 0.387f
			},
			new float[24]
			{
				0.031f, 0.042f, 0.069f, 0.09f, 0.101f, 0.135f, 0.161f, 0.179f, 0.194f, 0.218f,
				0.233f, 0.253f, 0.28f, 0.305f, 0.33f, 0.356f, 0.381f, 0.4f, 0.421f, 0.447f,
				0.461f, 0.487f, 0.506f, 0.506f
			},
			new float[21]
			{
				0.029f, 0.049f, 0.069f, 0.092f, 0.112f, 0.133f, 0.141f, 0.164f, 0.188f, 0.199f,
				0.208f, 0.232f, 0.243f, 0.264f, 0.288f, 0.307f, 0.329f, 0.353f, 0.376f, 0.392f,
				0.392f
			},
			new float[18]
			{
				0.032f, 0.055f, 0.065f, 0.089f, 0.108f, 0.118f, 0.142f, 0.155f, 0.18f, 0.19f,
				0.213f, 0.236f, 0.253f, 0.276f, 0.286f, 0.31f, 0.321f, 0.321f
			},
			new float[21]
			{
				0.032f, 0.056f, 0.078f, 0.1f, 0.123f, 0.14f, 0.162f, 0.181f, 0.204f, 0.216f,
				0.24f, 0.262f, 0.276f, 0.306f, 0.33f, 0.352f, 0.37f, 0.38f, 0.404f, 0.43f,
				0.43f
			},
			new float[31]
			{
				0.017f, 0.025f, 0.052f, 0.061f, 0.085f, 0.093f, 0.117f, 0.128f, 0.152f, 0.174f,
				0.196f, 0.206f, 0.224f, 0.247f, 0.269f, 0.28f, 0.306f, 0.317f, 0.341f, 0.364f,
				0.386f, 0.406f, 0.429f, 0.449f, 0.46f, 0.484f, 0.507f, 0.526f, 0.549f, 0.57f,
				0.57f
			},
			new float[13]
			{
				0.027f, 0.051f, 0.073f, 0.085f, 0.109f, 0.13f, 0.152f, 0.173f, 0.181f, 0.206f,
				0.228f, 0.24f, 0.24f
			}
		};

		private float[][] fillAmount_rate_g = new float[10][]
		{
			new float[19]
			{
				0.033f, 0.057f, 0.089f, 0.114f, 0.127f, 0.14f, 0.166f, 0.191f, 0.211f, 0.23f,
				0.253f, 0.266f, 0.29f, 0.314f, 0.344f, 0.353f, 0.378f, 0.4f, 0.4f
			},
			new float[26]
			{
				0.033f, 0.056f, 0.077f, 0.087f, 0.109f, 0.134f, 0.158f, 0.17f, 0.183f, 0.207f,
				0.229f, 0.247f, 0.264f, 0.288f, 0.311f, 0.331f, 0.354f, 0.377f, 0.401f, 0.414f,
				0.426f, 0.458f, 0.483f, 0.502f, 0.526f, 0.526f
			},
			new float[20]
			{
				0.031f, 0.05f, 0.067f, 0.092f, 0.101f, 0.111f, 0.123f, 0.135f, 0.161f, 0.186f,
				0.21f, 0.234f, 0.255f, 0.276f, 0.293f, 0.312f, 0.335f, 0.353f, 0.377f, 0.377f
			},
			new float[21]
			{
				0.033f, 0.056f, 0.079f, 0.103f, 0.116f, 0.141f, 0.166f, 0.186f, 0.199f, 0.223f,
				0.234f, 0.257f, 0.281f, 0.305f, 0.322f, 0.334f, 0.352f, 0.372f, 0.395f, 0.414f,
				0.414f
			},
			new float[24]
			{
				0.03f, 0.042f, 0.067f, 0.092f, 0.106f, 0.132f, 0.156f, 0.188f, 0.199f, 0.225f,
				0.248f, 0.263f, 0.296f, 0.307f, 0.326f, 0.34f, 0.368f, 0.395f, 0.419f, 0.452f,
				0.472f, 0.483f, 0.508f, 0.508f
			},
			new float[22]
			{
				0.03f, 0.053f, 0.061f, 0.085f, 0.104f, 0.112f, 0.136f, 0.148f, 0.175f, 0.197f,
				0.226f, 0.242f, 0.265f, 0.289f, 0.308f, 0.33f, 0.354f, 0.365f, 0.388f, 0.411f,
				0.435f, 0.435f
			},
			new float[15]
			{
				0.031f, 0.051f, 0.082f, 0.107f, 0.127f, 0.151f, 0.174f, 0.192f, 0.216f, 0.228f,
				0.25f, 0.259f, 0.283f, 0.295f, 0.295f
			},
			new float[27]
			{
				0.028f, 0.046f, 0.069f, 0.093f, 0.115f, 0.132f, 0.155f, 0.167f, 0.195f, 0.219f,
				0.236f, 0.258f, 0.281f, 0.304f, 0.328f, 0.351f, 0.374f, 0.396f, 0.419f, 0.45f,
				0.474f, 0.485f, 0.509f, 0.525f, 0.553f, 0.576f, 0.576f
			},
			new float[29]
			{
				0.029f, 0.041f, 0.065f, 0.081f, 0.104f, 0.127f, 0.151f, 0.174f, 0.197f, 0.209f,
				0.231f, 0.254f, 0.276f, 0.299f, 0.311f, 0.331f, 0.355f, 0.364f, 0.386f, 0.41f,
				0.429f, 0.441f, 0.464f, 0.473f, 0.496f, 0.518f, 0.54f, 0.557f, 0.557f
			},
			new float[14]
			{
				0.03f, 0.061f, 0.072f, 0.099f, 0.123f, 0.132f, 0.155f, 0.174f, 0.198f, 0.221f,
				0.24f, 0.263f, 0.274f, 0.274f
			}
		};

		private int font_type;

		public Image font_;

		private Transform cached_transform_;

		public int disp_count;

		public Transform cached_transform
		{
			get
			{
				if (cached_transform_ == null)
				{
					cached_transform_ = font_.transform;
					return cached_transform_;
				}
				return cached_transform_;
			}
			private set
			{
				cached_transform_ = value;
			}
		}

		private ushort[] GetFontCountArray()
		{
			switch (GSStatic.global_work_.language)
			{
			case Language.JAPAN:
				return font_count_j_;
			case Language.USA:
				return font_count_u_;
			case Language.FRANCE:
				return font_count_f_;
			case Language.GERMAN:
				return font_count_g_;
			case Language.KOREA:
				return font_count_k_;
			case Language.CHINA_S:
				return font_count_s_;
			case Language.CHINA_T:
				return font_count_t_;
			default:
				return font_count_j_;
			}
		}

		private float GetFontFillAmount()
		{
			switch (GSStatic.global_work_.language)
			{
			case Language.JAPAN:
				return 0.039f * (float)disp_count;
			case Language.USA:
				return fillAmount_rate_u[font_type][disp_count - 1];
			case Language.FRANCE:
				return fillAmount_rate_f[font_type][disp_count - 1];
			case Language.GERMAN:
				return fillAmount_rate_g[font_type][disp_count - 1];
			case Language.KOREA:
				return 0.0325f * (float)disp_count + 0.005f;
			case Language.CHINA_S:
				return 0.0355f * (float)disp_count + 0.005f;
			case Language.CHINA_T:
				return 0.0355f * (float)disp_count + 0.005f;
			default:
				return 0.039f * (float)disp_count;
			}
		}

		public void Init(Sprite set_source, gs3_op4_sprite font)
		{
			font_.gameObject.SetActive(true);
			if (cached_transform == null)
			{
				cached_transform = font_.transform;
			}
			font_type = (int)font;
			if (set_source != null)
			{
				font_.sprite = set_source;
			}
			disp_count = 0;
			font_.type = Image.Type.Filled;
			font_.fillAmount = 0f;
		}

		public bool EndLine()
		{
			ushort num = GetFontCountArray()[font_type];
			return disp_count >= num;
		}

		public float ViewFont(int set_disp_count)
		{
			ushort num = GetFontCountArray()[font_type];
			if (num < set_disp_count)
			{
				set_disp_count = num;
			}
			disp_count = set_disp_count;
			font_.fillAmount = GetFontFillAmount();
			return font_.fillAmount;
		}
	}

	private static PcViewCtrl instance_;

	public int deb_count;

	[SerializeField]
	private List<FontImageData> font_sprite_ = new List<FontImageData>();

	[SerializeField]
	private List<Image> icon_sprite_list_ = new List<Image>();

	[SerializeField]
	private SpriteRenderer line_sprite_;

	[SerializeField]
	public GameObject body_;

	[SerializeField]
	private AssetBundleSprite[] font_load_sprites_;

	[SerializeField]
	private AssetBundleSprite[] face_sprites_;

	[SerializeField]
	private AssetBundleSprite cursor_sprites_;

	[SerializeField]
	private Transform cursor_root_;

	[SerializeField]
	private AssetBundleSprite bg_;

	public RectTransform rect_transform_;

	private float cursor_position_offset_ = 5f;

	private int type_;

	private int line;

	public static PcViewCtrl instance
	{
		get
		{
			return instance_;
		}
	}

	public Vector3 Get_OBJ_OP4_008_DiffPosition()
	{
		switch (GSStatic.global_work_.language)
		{
		case Language.JAPAN:
			return Vector3.zero;
		case Language.USA:
			return Vector3.right * 48f;
		case Language.FRANCE:
			return Vector3.right * 93f;
		case Language.GERMAN:
			return Vector3.right * 93f;
		case Language.KOREA:
			return Vector3.right * 83f;
		case Language.CHINA_S:
			return Vector3.right * 49f;
		case Language.CHINA_T:
			return Vector3.right * 49f;
		default:
			return Vector3.zero;
		}
	}

	private Vector2 GetFacePhotoPosition()
	{
		switch (GSStatic.global_work_.language)
		{
		case Language.JAPAN:
			return new Vector2(57f, -56f);
		case Language.USA:
			return new Vector2(106f, -56f);
		case Language.FRANCE:
			return new Vector2(150f, -56f);
		case Language.GERMAN:
			return new Vector2(150f, -56f);
		case Language.KOREA:
			return new Vector2(140f, -56f);
		case Language.CHINA_S:
			return new Vector2(106f, -56f);
		case Language.CHINA_T:
			return new Vector2(106f, -56f);
		default:
			return new Vector2(57f, -56f);
		}
	}

	private float GetFontSpritePosition_X()
	{
		switch (GSStatic.global_work_.language)
		{
		case Language.JAPAN:
			return 163f;
		case Language.USA:
			return 133f;
		case Language.FRANCE:
			return 120f;
		case Language.GERMAN:
			return 120f;
		case Language.KOREA:
			return 130f;
		case Language.CHINA_S:
			return 133f;
		case Language.CHINA_T:
			return 133f;
		default:
			return 163f;
		}
	}

	private float[] GetFontSpritePosition_Y(int in_type)
	{
		if (in_type == 0)
		{
			switch (GSStatic.global_work_.language)
			{
			case Language.JAPAN:
				return new float[3] { -248f, -477f, -709f };
			case Language.USA:
				return new float[3] { -436f, -736f, -837f };
			case Language.FRANCE:
				return new float[3] { -442f, -742f, -842f };
			case Language.GERMAN:
				return new float[3] { -442f, -742f, -842f };
			case Language.KOREA:
				return new float[3] { -248f, -477f, -709f };
			case Language.CHINA_S:
				return new float[3] { -248f, -477f, -709f };
			case Language.CHINA_T:
				return new float[3] { -248f, -477f, -709f };
			default:
				return new float[3] { -248f, -477f, -709f };
			}
		}
		switch (GSStatic.global_work_.language)
		{
		case Language.JAPAN:
			return new float[5] { -248f, -364f, -478f, -708f, -823f };
		case Language.USA:
			return new float[7] { -337f, -435f, -535f, -636f, -737f, -837f, -937f };
		case Language.FRANCE:
			return new float[7] { -343f, -442f, -542f, -642f, -743f, -843f, -943f };
		case Language.GERMAN:
			return new float[7] { -343f, -442f, -542f, -642f, -743f, -843f, -943f };
		case Language.KOREA:
			return new float[5] { -248f, -364f, -478f, -708f, -823f };
		case Language.CHINA_S:
			return new float[5] { -248f, -364f, -478f, -708f, -823f };
		case Language.CHINA_T:
			return new float[5] { -248f, -364f, -478f, -708f, -823f };
		default:
			return new float[5] { -248f, -364f, -478f, -708f, -823f };
		}
	}

	private Vector3 GetCursorPosition(int in_type, int in_line)
	{
		switch (GSStatic.global_work_.language)
		{
		case Language.JAPAN:
			return new Vector3(163f, GetFontSpritePosition_Y(in_type)[in_line]);
		case Language.USA:
			return new Vector3(133f, GetFontSpritePosition_Y(in_type)[in_line]);
		case Language.FRANCE:
			return new Vector3(120f, GetFontSpritePosition_Y(in_type)[in_line]);
		case Language.GERMAN:
			return new Vector3(120f, GetFontSpritePosition_Y(in_type)[in_line]);
		case Language.KOREA:
			return new Vector3(130f, GetFontSpritePosition_Y(in_type)[in_line]);
		case Language.CHINA_S:
			return new Vector3(133f, GetFontSpritePosition_Y(in_type)[in_line]);
		case Language.CHINA_T:
			return new Vector3(133f, GetFontSpritePosition_Y(in_type)[in_line]);
		default:
			return new Vector3(163f, GetFontSpritePosition_Y(in_type)[in_line]);
		}
	}

	private void Awake()
	{
		instance_ = this;
		rect_transform_ = body_.GetComponent<RectTransform>();
	}

	public void PcViewInitialize(int type)
	{
		body_.SetActive(true);
		rect_transform_.localPosition = Vector3.up * 590f;
		type_ = type;
		line = 0;
		deb_count = 0;
		cursor_sprites_.sprite_renderer_.transform.localPosition = Vector3.zero;
		float fontSpritePosition_X = GetFontSpritePosition_X();
		float[] fontSpritePosition_Y = GetFontSpritePosition_Y(type);
		int num;
		int num2;
		if (type == 0)
		{
			num = 0;
			num2 = 2;
		}
		else
		{
			num = 3;
			num2 = ((GSUtility.GetLanguageLayoutType(GSStatic.global_work_.language) != 0) ? 9 : 7);
		}
		for (int i = 0; i < fontSpritePosition_Y.Length; i++)
		{
			if (i + num <= num2)
			{
				font_sprite_[i].Init(LoadFont((gs3_op4_sprite)(i + num)), (gs3_op4_sprite)(i + num));
				font_sprite_[i].cached_transform.localPosition = new Vector3(fontSpritePosition_X, fontSpritePosition_Y[i]);
			}
		}
		switch (GSStatic.global_work_.language)
		{
		default:
			cursor_root_.localPosition = font_sprite_[0].cached_transform.localPosition;
			cursor_position_offset_ = 5f;
			break;
		case Language.FRANCE:
		case Language.GERMAN:
		case Language.CHINA_S:
		case Language.CHINA_T:
			cursor_root_.localPosition = GetCursorPosition(type_, line);
			cursor_position_offset_ = 5f;
			break;
		case Language.KOREA:
			cursor_root_.localPosition = GetCursorPosition(type_, line);
			cursor_position_offset_ = -5f;
			break;
		}
	}

	public void LoadBackGround()
	{
		bg_.end();
		bg_.remove();
		bg_.sprite_renderer_.sprite = null;
		bg_.load("/GS3/BG/", "pcback");
		bg_.sprite_renderer_.enabled = true;
		cursor_sprites_.end();
		cursor_sprites_.remove();
		cursor_sprites_.sprite_renderer_.sprite = null;
		cursor_sprites_.load("/GS3/BG/", "cursor");
		cursor_sprites_.sprite_renderer_.enabled = false;
		cursor_sprites_.sprite_renderer_.transform.localPosition = Vector3.zero;
		switch (GSStatic.global_work_.language)
		{
		default:
			cursor_root_.localPosition = new Vector3(163f, -248f);
			break;
		case Language.USA:
			cursor_root_.localPosition = new Vector3(133f, -436f);
			break;
		case Language.FRANCE:
		case Language.GERMAN:
		case Language.KOREA:
		case Language.CHINA_S:
		case Language.CHINA_T:
			cursor_root_.localPosition = GetCursorPosition(type_, line);
			break;
		}
		line_sprite_.transform.localPosition = Vector3.down * 1380f + Vector3.forward * -20f;
		line_sprite_.enabled = true;
		body_.SetActive(true);
	}

	public Sprite LoadFont(gs3_op4_sprite font)
	{
		font_load_sprites_[(int)font].end();
		font_load_sprites_[(int)font].remove();
		font_load_sprites_[(int)font].sprite_renderer_.sprite = null;
		font_load_sprites_[(int)font].load("/GS3/BG/", "bg4_font" + (int)(font + 1) + GSUtility.GetResourceNameLanguage(GSStatic.global_work_.language));
		return font_load_sprites_[(int)font].sprite_renderer_.sprite;
	}

	public void CursorEnable(bool active)
	{
		cursor_sprites_.sprite_renderer_.enabled = active;
	}

	public void EndView()
	{
		foreach (FontImageData item in font_sprite_)
		{
			item.cached_transform.gameObject.SetActive(false);
			item.font_.sprite = null;
		}
		foreach (Image item2 in icon_sprite_list_)
		{
			item2.gameObject.SetActive(false);
			item2.sprite = null;
		}
		AssetBundleSprite[] array = face_sprites_;
		foreach (AssetBundleSprite assetBundleSprite in array)
		{
			assetBundleSprite.end();
			assetBundleSprite.remove();
			assetBundleSprite.sprite_renderer_.sprite = null;
		}
		AssetBundleSprite[] array2 = font_load_sprites_;
		foreach (AssetBundleSprite assetBundleSprite2 in array2)
		{
			assetBundleSprite2.end();
			assetBundleSprite2.remove();
			assetBundleSprite2.sprite_renderer_.sprite = null;
		}
		bg_.end();
		bg_.remove();
		bg_.sprite_renderer_.sprite = null;
		bg_.sprite_renderer_.enabled = false;
		cursor_sprites_.end();
		cursor_sprites_.remove();
		cursor_sprites_.sprite_renderer_.sprite = null;
		bg_.sprite_renderer_.enabled = false;
		line_sprite_.enabled = false;
		body_.SetActive(false);
	}

	public void PcViewNext()
	{
		deb_count++;
		float num = font_sprite_[line].ViewFont(font_sprite_[line].disp_count + 1);
		if (font_sprite_[line].EndLine())
		{
			line++;
			if (type_ == 0)
			{
				if (line < 3)
				{
					switch (GSStatic.global_work_.language)
					{
					default:
						cursor_root_.localPosition = font_sprite_[line].cached_transform.localPosition;
						break;
					case Language.FRANCE:
					case Language.GERMAN:
					case Language.KOREA:
					case Language.CHINA_S:
					case Language.CHINA_T:
						cursor_root_.localPosition = GetCursorPosition(type_, line);
						break;
					}
					cursor_sprites_.sprite_renderer_.transform.localPosition = Vector3.zero;
				}
				return;
			}
			int num2 = ((GSUtility.GetLanguageLayoutType(GSStatic.global_work_.language) != 0) ? 7 : 5);
			if (line < num2)
			{
				switch (GSStatic.global_work_.language)
				{
				default:
					cursor_root_.localPosition = font_sprite_[line].cached_transform.localPosition;
					break;
				case Language.FRANCE:
				case Language.GERMAN:
				case Language.KOREA:
				case Language.CHINA_S:
				case Language.CHINA_T:
					cursor_root_.localPosition = GetCursorPosition(type_, line);
					break;
				}
				cursor_sprites_.sprite_renderer_.transform.localPosition = Vector3.zero;
			}
		}
		else
		{
			cursor_sprites_.sprite_renderer_.transform.localPosition = Vector3.right * (2304f * num + cursor_position_offset_);
		}
	}

	public void icon_view(int icon_type, float fill)
	{
		switch (icon_type)
		{
		case 1:
			icon_sprite_list_[0].fillAmount = fill;
			break;
		case 2:
			icon_sprite_list_[1].fillAmount = fill;
			break;
		case 3:
			icon_sprite_list_[0].fillAmount = fill;
			break;
		}
	}

	public void PcLineUpdate(ushort enable_line)
	{
		if (enable_line != 0)
		{
		}
		line_sprite_.transform.localPosition += Vector3.up * 4.5f * 2f;
		if (line_sprite_.transform.localPosition.y > 684f)
		{
			line_sprite_.transform.localPosition = Vector3.down * 660f + Vector3.forward * -20f;
		}
	}

	public void DellIcon()
	{
		icon_sprite_list_[0].fillAmount = 0f;
		icon_sprite_list_[0].gameObject.SetActive(false);
		icon_sprite_list_[1].fillAmount = 0f;
		icon_sprite_list_[1].gameObject.SetActive(false);
	}

	public void init_icon(int icon_type)
	{
		string in_name = string.Empty;
		int num = 0;
		switch (icon_type)
		{
		case 1:
			num = 0;
			in_name = "it058_";
			break;
		case 2:
			num = 1;
			in_name = "it05c_";
			break;
		case 3:
			num = 0;
			in_name = "it059_";
			break;
		}
		face_sprites_[num].end();
		face_sprites_[num].remove();
		face_sprites_[num].sprite_renderer_.sprite = null;
		face_sprites_[num].load("/GS3/icon/", in_name);
		icon_sprite_list_[num].sprite = face_sprites_[num].sprite_renderer_.sprite;
		icon_sprite_list_[num].type = Image.Type.Filled;
		icon_sprite_list_[num].fillAmount = 0f;
		icon_sprite_list_[num].transform.localPosition = GetFacePhotoPosition();
		icon_sprite_list_[num].rectTransform.sizeDelta = new Vector2(406f, 406f);
		icon_sprite_list_[num].gameObject.SetActive(true);
	}
}
