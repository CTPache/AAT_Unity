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
		private ushort[] font_count = new ushort[8] { 9, 8, 6, 9, 11, 11, 11, 17 };

		private ushort[] font_count_u = new ushort[10] { 18, 26, 24, 22, 18, 18, 18, 28, 19, 12 };

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
			if (GSStatic.global_work_.language == Language.JAPAN)
			{
				return disp_count >= font_count[font_type];
			}
			return disp_count >= font_count_u[font_type];
		}

		public float ViewFont(int set_disp_count)
		{
			if (GSStatic.global_work_.language == Language.JAPAN)
			{
				if (font_count[font_type] < set_disp_count)
				{
					set_disp_count = font_count[font_type];
				}
			}
			else if (font_count_u[font_type] < set_disp_count)
			{
				set_disp_count = font_count_u[font_type];
			}
			disp_count = set_disp_count;
			if (GSStatic.global_work_.language == Language.JAPAN)
			{
				font_.fillAmount = 0.039f * (float)disp_count;
			}
			else
			{
				font_.fillAmount = fillAmount_rate_u[font_type][disp_count - 1];
			}
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

	private int type_;

	private int line;

	public static PcViewCtrl instance
	{
		get
		{
			return instance_;
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
		if (type == 0)
		{
			if (GSStatic.global_work_.language == Language.JAPAN)
			{
				font_sprite_[0].Init(LoadFont(gs3_op4_sprite.E_OP4_DEMO_SPR_7), gs3_op4_sprite.E_OP4_DEMO_SPR_7);
				font_sprite_[0].cached_transform.localPosition = new Vector3(163f, -248f);
				font_sprite_[1].Init(LoadFont(gs3_op4_sprite.E_OP4_DEMO_SPR_8), gs3_op4_sprite.E_OP4_DEMO_SPR_8);
				font_sprite_[1].cached_transform.localPosition = new Vector3(163f, -477f);
				font_sprite_[2].Init(LoadFont(gs3_op4_sprite.E_OP4_DEMO_SPR_9), gs3_op4_sprite.E_OP4_DEMO_SPR_9);
				font_sprite_[2].cached_transform.localPosition = new Vector3(163f, -709f);
			}
			else
			{
				font_sprite_[0].Init(LoadFont(gs3_op4_sprite.E_OP4_DEMO_SPR_7), gs3_op4_sprite.E_OP4_DEMO_SPR_7);
				font_sprite_[0].cached_transform.localPosition = new Vector3(133f, -436f);
				font_sprite_[1].Init(LoadFont(gs3_op4_sprite.E_OP4_DEMO_SPR_8), gs3_op4_sprite.E_OP4_DEMO_SPR_8);
				font_sprite_[1].cached_transform.localPosition = new Vector3(133f, -736f);
				font_sprite_[2].Init(LoadFont(gs3_op4_sprite.E_OP4_DEMO_SPR_9), gs3_op4_sprite.E_OP4_DEMO_SPR_9);
				font_sprite_[2].cached_transform.localPosition = new Vector3(133f, -837f);
			}
		}
		else if (GSStatic.global_work_.language == Language.JAPAN)
		{
			font_sprite_[0].Init(LoadFont(gs3_op4_sprite.E_OP4_DEMO_SPR_A), gs3_op4_sprite.E_OP4_DEMO_SPR_A);
			font_sprite_[0].cached_transform.localPosition = new Vector3(163f, -248f);
			font_sprite_[1].Init(LoadFont(gs3_op4_sprite.E_OP4_DEMO_SPR_B), gs3_op4_sprite.E_OP4_DEMO_SPR_B);
			font_sprite_[1].cached_transform.localPosition = new Vector3(163f, -364f);
			font_sprite_[2].Init(LoadFont(gs3_op4_sprite.E_OP4_DEMO_SPR_C), gs3_op4_sprite.E_OP4_DEMO_SPR_C);
			font_sprite_[2].cached_transform.localPosition = new Vector3(163f, -478f);
			font_sprite_[3].Init(LoadFont(gs3_op4_sprite.E_OP4_DEMO_SPR_D), gs3_op4_sprite.E_OP4_DEMO_SPR_D);
			font_sprite_[3].cached_transform.localPosition = new Vector3(163f, -708f);
			font_sprite_[4].Init(LoadFont(gs3_op4_sprite.E_OP4_DEMO_SPR_E), gs3_op4_sprite.E_OP4_DEMO_SPR_E);
			font_sprite_[4].cached_transform.localPosition = new Vector3(163f, -823f);
		}
		else
		{
			font_sprite_[0].Init(LoadFont(gs3_op4_sprite.E_OP4_DEMO_SPR_A), gs3_op4_sprite.E_OP4_DEMO_SPR_A);
			font_sprite_[0].cached_transform.localPosition = new Vector3(133f, -337f);
			font_sprite_[1].Init(LoadFont(gs3_op4_sprite.E_OP4_DEMO_SPR_B), gs3_op4_sprite.E_OP4_DEMO_SPR_B);
			font_sprite_[1].cached_transform.localPosition = new Vector3(133f, -435f);
			font_sprite_[2].Init(LoadFont(gs3_op4_sprite.E_OP4_DEMO_SPR_C), gs3_op4_sprite.E_OP4_DEMO_SPR_C);
			font_sprite_[2].cached_transform.localPosition = new Vector3(133f, -535f);
			font_sprite_[3].Init(LoadFont(gs3_op4_sprite.E_OP4_DEMO_SPR_D), gs3_op4_sprite.E_OP4_DEMO_SPR_D);
			font_sprite_[3].cached_transform.localPosition = new Vector3(133f, -636f);
			font_sprite_[4].Init(LoadFont(gs3_op4_sprite.E_OP4_DEMO_SPR_E), gs3_op4_sprite.E_OP4_DEMO_SPR_E);
			font_sprite_[4].cached_transform.localPosition = new Vector3(133f, -737f);
			font_sprite_[5].Init(LoadFont(gs3_op4_sprite.E_OP4_DEMO_SPR_F), gs3_op4_sprite.E_OP4_DEMO_SPR_F);
			font_sprite_[5].cached_transform.localPosition = new Vector3(133f, -837f);
			font_sprite_[6].Init(LoadFont(gs3_op4_sprite.E_OP4_DEMO_SPR_G), gs3_op4_sprite.E_OP4_DEMO_SPR_G);
			font_sprite_[6].cached_transform.localPosition = new Vector3(133f, -937f);
		}
		cursor_root_.localPosition = font_sprite_[0].cached_transform.localPosition;
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
		if (GSStatic.global_work_.language == Language.JAPAN)
		{
			cursor_root_.localPosition = new Vector3(163f, -248f);
		}
		else
		{
			cursor_root_.localPosition = new Vector3(133f, -436f);
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
		if (GSStatic.global_work_.language == Language.JAPAN)
		{
			font_load_sprites_[(int)font].load("/GS3/BG/", "bg4_font" + (int)(font + 1));
		}
		else
		{
			font_load_sprites_[(int)font].load("/GS3/BG/", "bg4_font" + (int)(font + 1) + "u");
		}
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
					cursor_root_.localPosition = font_sprite_[line].cached_transform.localPosition;
					cursor_sprites_.sprite_renderer_.transform.localPosition = Vector3.zero;
				}
			}
			else if (GSStatic.global_work_.language == Language.JAPAN)
			{
				if (line < 5)
				{
					cursor_root_.localPosition = font_sprite_[line].cached_transform.localPosition;
					cursor_sprites_.sprite_renderer_.transform.localPosition = Vector3.zero;
				}
			}
			else if (line < 7)
			{
				cursor_root_.localPosition = font_sprite_[line].cached_transform.localPosition;
				cursor_sprites_.sprite_renderer_.transform.localPosition = Vector3.zero;
			}
		}
		else
		{
			cursor_sprites_.sprite_renderer_.transform.localPosition = Vector3.right * (2304f * num + 5f);
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
		if (GSStatic.global_work_.language == Language.JAPAN)
		{
			icon_sprite_list_[num].transform.localPosition = new Vector2(57f, -56f);
		}
		else
		{
			icon_sprite_list_[num].transform.localPosition = new Vector2(106f, -56f);
		}
		icon_sprite_list_[num].rectTransform.sizeDelta = new Vector2(406f, 406f);
		icon_sprite_list_[num].gameObject.SetActive(true);
	}
}
