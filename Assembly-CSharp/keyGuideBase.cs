using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class keyGuideBase : MonoBehaviour
{
	public enum Type
	{
		NO_GUIDE = 0,
		HOUTEI = 1,
		QUESTIONING = 2,
		RECORD = 3,
		PROFILE = 4,
		QUESTIONING_RECORD = 5,
		QUESTIONING_PROFILE = 6,
		FORCE_RECORD = 7,
		FORCE_PROFILE = 8,
		INSPECT = 9,
		INSPECT_SLIDE = 10,
		MOVE = 11,
		TANTEI = 12,
		TANTEI_SLIDE = 13,
		TANTEI_TALK = 14,
		KAGAKU_SYOUSAI = 15,
		FORCE_KAGAKU_SYOUSAI = 16,
		PRESENT_KAGAKU_SYOUSAI = 17,
		ENDING_KAGAKU_SYOUSAI = 18,
		POT_PAZZLE = 19,
		VASE_SHOW = 20,
		CONFRONT_WITH_MOVIE = 21,
		LUMINOL = 22,
		LUMINOL_SLIDE = 23,
		LUMINOL_TUTORIAL = 24,
		LUMINOL_INSPECT = 25,
		FINGER_SELECT = 26,
		FINGER_MAIN = 27,
		FINGER_COMP = 28,
		FINGER_COMP_TUTORIAL = 29,
		VIDEO_DETAIL = 30,
		POINT = 31,
		POINT_SLIDE = 32,
		TANCHIKI = 33,
		TANCHIKI_SLIDE = 34,
		CONFRONT_WITH_MOVIE_PAUSE = 35,
		TOP_VIEW = 36,
		GS_MAIN = 37,
		GS_SELECT = 38,
		SCENARIO_SELECT = 39,
		DETAIL_VIEW = 40,
		POINT_TANTEI = 41,
		DYING_MESSAGE = 42
	}

	[Serializable]
	public class GuideIcon
	{
		public AssetBundleSprite sprite_;

		public Text text_;

		public InputTouch touch_;

		public GameObject symbol_;

		public Vector3 text_position_
		{
			get
			{
				return text_.transform.localPosition;
			}
			set
			{
				text_.transform.localPosition = value;
			}
		}

		public bool active
		{
			get
			{
				if (symbol_ != null)
				{
					return symbol_.activeSelf;
				}
				return true;
			}
			set
			{
				if (symbol_ != null)
				{
					symbol_.SetActive(value);
				}
			}
		}

		public void Set(string text, int sprite_no)
		{
			active = true;
			sprite_.spriteNo(sprite_no);
			text_.text = text;
		}

		public void ActiveTouch()
		{
			touch_.ActiveCollider();
		}

		public void InActiveTouch()
		{
			touch_.SetEnableCollider(false);
		}

		public static int GetKeyCodeType(KeyType key_type)
		{
			KeyCode keyCode = padCtrl.instance.GetKeyCode(key_type);
			return GetKeyCodeSpriteNum(keyCode);
		}

		public static int GetKeyCodeSpriteNum(KeyCode key_code)
		{
			int num = 0;
			switch (key_code)
			{
			case KeyCode.Escape:
				return 0;
			case KeyCode.F1:
			case KeyCode.F2:
			case KeyCode.F3:
			case KeyCode.F4:
			case KeyCode.F5:
			case KeyCode.F6:
			case KeyCode.F7:
			case KeyCode.F8:
			case KeyCode.F9:
			case KeyCode.F10:
			case KeyCode.F11:
			case KeyCode.F12:
				return (int)(1 + key_code - 282);
			case KeyCode.Tab:
				return 13;
			case KeyCode.LeftShift:
				return 14;
			case KeyCode.RightShift:
				return 15;
			case KeyCode.LeftControl:
				return 16;
			case KeyCode.RightControl:
				return 17;
			case KeyCode.LeftAlt:
				return 18;
			case KeyCode.RightAlt:
			case KeyCode.AltGr:
				return 19;
			case KeyCode.Space:
				return 20;
			case KeyCode.Backspace:
				return 21;
			case KeyCode.Return:
				return 22;
			case KeyCode.Alpha1:
			case KeyCode.Alpha2:
			case KeyCode.Alpha3:
			case KeyCode.Alpha4:
			case KeyCode.Alpha5:
			case KeyCode.Alpha6:
			case KeyCode.Alpha7:
			case KeyCode.Alpha8:
			case KeyCode.Alpha9:
				return (int)(23 + key_code - 49);
			case KeyCode.Alpha0:
				return 32;
			case KeyCode.A:
			case KeyCode.B:
			case KeyCode.C:
			case KeyCode.D:
			case KeyCode.E:
			case KeyCode.F:
			case KeyCode.G:
			case KeyCode.H:
			case KeyCode.I:
			case KeyCode.J:
			case KeyCode.K:
			case KeyCode.L:
			case KeyCode.M:
			case KeyCode.N:
			case KeyCode.O:
			case KeyCode.P:
			case KeyCode.Q:
			case KeyCode.R:
			case KeyCode.S:
			case KeyCode.T:
			case KeyCode.U:
			case KeyCode.V:
			case KeyCode.W:
			case KeyCode.X:
			case KeyCode.Y:
			case KeyCode.Z:
				return (int)(33 + (key_code - 97));
			case KeyCode.Insert:
				return 59;
			case KeyCode.Home:
				return 60;
			case KeyCode.PageUp:
				return 61;
			case KeyCode.Delete:
				return 62;
			case KeyCode.End:
				return 63;
			case KeyCode.PageDown:
				return 64;
			case KeyCode.UpArrow:
				return 65;
			case KeyCode.DownArrow:
				return 66;
			case KeyCode.LeftArrow:
				return 67;
			case KeyCode.RightArrow:
				return 68;
			case KeyCode.Keypad1:
			case KeyCode.Keypad2:
			case KeyCode.Keypad3:
			case KeyCode.Keypad4:
			case KeyCode.Keypad5:
			case KeyCode.Keypad6:
			case KeyCode.Keypad7:
			case KeyCode.Keypad8:
			case KeyCode.Keypad9:
				return (int)(69 + key_code - 257);
			case KeyCode.Keypad0:
				return 78;
			default:
				return 79;
			}
		}

		public int GetKeyPadSpriteNum(KeyType key_type)
		{
			int result = 0;
			switch (key_type)
			{
			case KeyType.A:
				result = 0;
				break;
			case KeyType.B:
				result = 1;
				break;
			case KeyType.X:
				result = 2;
				break;
			case KeyType.Y:
				result = 3;
				break;
			case KeyType.L:
				result = 4;
				break;
			case KeyType.R:
				result = 5;
				break;
			case KeyType.Start:
				result = 11;
				break;
			case KeyType.StickR_Right:
				result = 10;
				break;
			default:
				Debug.LogError("XOne Not KeyType : " + key_type);
				break;
			}
			return result;
		}

		public virtual void SetKeyTypeSprite(KeyType key_type, bool touched = false)
		{
			active = true;
			if (!touched)
			{
				touch_.touch_key_type = key_type;
				touch_.touched_key_type = KeyType.None;
			}
			else
			{
				touch_.touched_key_type = key_type;
				touch_.touch_key_type = KeyType.None;
			}
			sprite_.active = true;
			if (!keyguid_pad_)
			{
				if (key_type == KeyType.DEFAULT_RETURN_KEY)
				{
					sprite_.spriteNo(GetKeyCodeSpriteNum(KeyCode.Delete));
				}
				else
				{
					sprite_.spriteNo(GetKeyCodeType(key_type));
				}
			}
			else
			{
				switch (key_type)
				{
				case KeyType.DEFAULT_RETURN_KEY:
					sprite_.spriteNo(GetKeyPadSpriteNum(KeyType.X));
					break;
				case KeyType.Record:
					sprite_.spriteNo(GetKeyPadSpriteNum(KeyType.R));
					break;
				default:
					sprite_.spriteNo(GetKeyPadSpriteNum(key_type));
					break;
				}
			}
			text_.rectTransform.sizeDelta = new Vector2(200f, 30f);
			Vector3 localPosition = text_.transform.localPosition;
			localPosition.x = 130f;
			text_.transform.localPosition = localPosition;
		}
	}

	[Serializable]
	public class Curve
	{
		public AnimationCurve open_ = new AnimationCurve();

		public AnimationCurve close_ = new AnimationCurve();

		public float speed_open_ = 0.05f;

		public float speed_close_ = 0.05f;

		public void init()
		{
			open_ = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
			close_ = AnimationCurve.EaseInOut(0f, 1f, 1f, 0f);
		}
	}

	public static bool keyguid_pad_;

	protected TextAnchor guide_text_alignment_;

	protected float guide_text_position_y_;

	protected float guide_space_ = 100f;

	[SerializeField]
	protected List<GuideIcon> guide_list_ = new List<GuideIcon>();

	private Vector2 NORMAL_GUDE_TOUCH_OFFSET = new Vector2(85f, 0f);

	private Vector2 NORMAL_GUDE_TOUCH_SIZE = new Vector2(220f, 40f);

	private Vector2 LAST_GUIDE_TOUCH_OFFSET = new Vector2(100f, 0f);

	private Vector2 LAST_GUIDE_TOUCH_SIZE = new Vector2(250f, 40f);

	public static void UpdateKeyPadGuid(bool key_pad)
	{
		if (keyguid_pad_ != key_pad)
		{
			keyguid_pad_ = key_pad;
			AllGuidReload();
		}
	}

	public static void AllGuidReload()
	{
		if (messageBoardCtrl.instance != null)
		{
			messageBoardCtrl.instance.guide_ctrl.ReLoadGuid();
		}
		if (keyGuideCtrl.instance != null)
		{
			keyGuideCtrl.instance.ReLoadGuid();
		}
		if (ConfrontWithMovie.instance != null && ConfrontWithMovie.instance.movie_guide != null)
		{
			ConfrontWithMovie.instance.movie_guide.ReLoadGuid();
		}
		if (recordListCtrl.instance != null)
		{
			recordListCtrl.instance.infoLoad();
			recordListCtrl.instance.record_guide.ReLoadGuid();
		}
		if (titleGuideCtrl.instance != null)
		{
			titleGuideCtrl.instance.ReLoadGuid();
		}
		if (optionCtrl.instance != null)
		{
			optionCtrl.instance.key_guide.ReLoadGuid();
		}
		if (SaveLoadUICtrl.instance != null)
		{
			SaveLoadUICtrl.instance.key_guide.ReLoadGuid();
		}
		if (creditCtrl.instance != null)
		{
			creditCtrl.instance.guide_ctrl.ReLoadGuid();
		}
	}

	protected virtual void Awake()
	{
		guide_text_alignment_ = guide_list_[0].text_.alignment;
		guide_text_position_y_ = guide_list_[0].text_.transform.localPosition.y;
	}

	public void SymbolLoad()
	{
		string empty = string.Empty;
		empty = (keyguid_pad_ ? "symbol_xbox" : ("symbol" + GSUtility.GetPlatformResourceName()));
		for (int i = 0; i < guide_list_.Count; i++)
		{
			guide_list_[i].sprite_.sprite_data_.Clear();
			guide_list_[i].sprite_.load("/menu/common/", empty);
			SetGuideIconPosition(guide_list_[i], i);
		}
	}

	public virtual void SetGuideIconPosition(GuideIcon target, int index_num)
	{
	}

	protected void UpdateTouchArea()
	{
		for (int i = 0; i < guide_list_.Count; i++)
		{
			if (guide_list_[i].sprite_.active)
			{
				if (i < guide_list_.Count - 1)
				{
					NormalGuideTouchAreaUpdate(i);
				}
				else if (i >= guide_list_.Count - 1)
				{
					LastGuideTouchAreaUpdate(i);
				}
				continue;
			}
			LastGuideTouchAreaUpdate((i <= 0) ? i : (i - 1));
			break;
		}
	}

	public void ActiveTouch()
	{
		foreach (GuideIcon item in guide_list_)
		{
			item.ActiveTouch();
		}
	}

	public void InActiveTouch()
	{
		foreach (GuideIcon item in guide_list_)
		{
			item.InActiveTouch();
		}
	}

	protected virtual void NormalGuideTouchAreaUpdate(int index)
	{
		if (guide_list_.Count > index)
		{
			guide_list_[index].touch_.SetColliderOffset(NORMAL_GUDE_TOUCH_OFFSET);
			guide_list_[index].touch_.SetColliderSize(NORMAL_GUDE_TOUCH_SIZE);
		}
	}

	protected virtual void LastGuideTouchAreaUpdate(int index)
	{
		if (guide_list_.Count > index)
		{
			guide_list_[index].touch_.SetColliderOffset(LAST_GUIDE_TOUCH_OFFSET);
			guide_list_[index].touch_.SetColliderSize(LAST_GUIDE_TOUCH_SIZE);
		}
	}

	protected void SetLanguageLayout()
	{
		int languageTextFontSize = GetLanguageTextFontSize(GSStatic.global_work_.language);
		TextAnchor languageTextAnchor = GetLanguageTextAnchor(GSStatic.global_work_.language);
		float languageTextPositionY = GetLanguageTextPositionY(GSStatic.global_work_.language);
		for (int i = 0; i < guide_list_.Count; i++)
		{
			guide_list_[i].text_.fontSize = languageTextFontSize;
			guide_list_[i].text_.alignment = languageTextAnchor;
			Vector3 text_position_ = guide_list_[i].text_position_;
			text_position_.y = languageTextPositionY;
			guide_list_[i].text_position_ = text_position_;
		}
	}

	protected virtual int GetLanguageTextFontSize(Language language)
	{
		switch (language)
		{
		case Language.JAPAN:
			return 26;
		case Language.USA:
			return 26;
		case Language.FRANCE:
			return 20;
		case Language.GERMAN:
			return 20;
		case Language.KOREA:
			return 22;
		case Language.CHINA_S:
			return 24;
		case Language.CHINA_T:
			return 22;
		default:
			return 26;
		}
	}

	private TextAnchor GetLanguageTextAnchor(Language language)
	{
		switch (language)
		{
		default:
			return guide_text_alignment_;
		case Language.FRANCE:
		case Language.GERMAN:
		case Language.KOREA:
		case Language.CHINA_S:
		case Language.CHINA_T:
			return TextAnchor.MiddleLeft;
		}
	}

	private float GetLanguageTextPositionY(Language language)
	{
		switch (language)
		{
		default:
			return guide_text_position_y_;
		case Language.FRANCE:
		case Language.GERMAN:
		case Language.KOREA:
		case Language.CHINA_S:
		case Language.CHINA_T:
			return 0f;
		}
	}
}
