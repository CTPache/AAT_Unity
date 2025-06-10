using System.Collections;
using System.Linq;
using UnityEngine;

public class guideCtrl : keyGuideBase
{
	public enum GuideType
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
		Luminol = 9,
		SHOW_RECORD = 10,
		SHOW_PROFILE = 11,
		OPTION_TITLE = 12,
		OPTION_INGAME = 13,
		SAVE = 14,
		CREDIT = 15,
		PSYLOCK = 16
	}

	private bool debug_is_disp_ = true;

	[SerializeField]
public GameObject body_;

	private int guide_cnt;

	protected GuideType current_guide_;

	private GuideType old_guide_;

	private GuideType next_guide_;

	[SerializeField]
public Curve curve_ = new Curve();

	private IEnumerator enumerator_open_close_;

	private IEnumerator enumerator_open_;

	private IEnumerator enumerator_close_;

	[SerializeField]
public float guide_width_j_ = 200f;

	[SerializeField]
public float guide_width_u_ = 250f;

	[SerializeField]
public float guide_pos_y_ = -120f;

	[SerializeField]
public float slide_out_pos_x_ = 860f;

	[SerializeField]
public AssetBundleSprite sprite_guide_;

	[SerializeField]
public AssetBundleSprite sprite_line_;

	public GuideType current_guide
	{
		get
		{
			return current_guide_;
		}
	}

	public GuideType old_guid
	{
		get
		{
			return old_guide_;
		}
	}

	public GuideType next_guid
	{
		get
		{
			return next_guide_;
		}
		set
		{
			next_guide_ = value;
		}
	}

	protected float guide_width_
	{
		get
        {
            string lang = Language.langFallback[GSStatic.global_work_.language].ToUpper();
			switch (lang)
            {
			case "JAPAN":
			case "CHINA_S":
			case "CHINA_T":
				return guide_width_j_;
			default:
				return guide_width_u_;
			}
		}
	}

	protected override void Awake()
	{
		curve_.init();
		if (body_ == null)
		{
			foreach (Transform item in base.transform)
			{
				if (item.name.Contains("body"))
				{
					body_ = item.gameObject;
				}
			}
		}
		guide_text_alignment_ = guide_list_[0].text_.alignment;
		guide_text_position_y_ = guide_list_[0].text_.transform.localPosition.y;
	}

	private void OnDisable()
	{
		enumerator_open_close_ = null;
		enumerator_open_ = null;
		enumerator_close_ = null;
	}

	public virtual void load()
	{
		sprite_guide_.load("/menu/common/", "menu_bg");
		sprite_line_.load("/menu/common/", "menu_bg");
		sprite_guide_.spriteNo(0);
		sprite_line_.spriteNo(1);
		sprite_guide_.active = false;
		sprite_line_.active = false;
		SymbolLoad();
	}

	public virtual void ReLoadGuid()
	{
		SymbolLoad();
		if (!sprite_guide_.active)
		{
			settingSprite(current_guide_);
			setEnables(false);
		}
		else
		{
			settingSprite(current_guide_);
		}
	}

	public virtual void init()
	{
		current_guide_ = GuideType.NO_GUIDE;
		old_guide_ = GuideType.NO_GUIDE;
		load();
	}

	public override void SetGuideIconPosition(GuideIcon target, int index_num)
	{
		Vector3 localPosition = target.sprite_.transform.localPosition;
		localPosition.x = guide_width_ * (float)index_num + guide_space_;
		target.sprite_.transform.localPosition = localPosition;
		target.text_.horizontalOverflow = HorizontalWrapMode.Overflow;
	}

	public void setEnables(bool in_enables)
	{
		sprite_guide_.active = in_enables;
	}

	public bool getEnables()
	{
		return sprite_guide_.active || sprite_line_.active;
	}

	public virtual void Close()
	{
		sprite_guide_.active = false;
		sprite_line_.active = false;
	}

	public void settingSprite(GuideType in_type)
	{
		current_guide_ = in_type;
		if (in_type != 0)
		{
			old_guide_ = in_type;
		}
		if (!DebugDispCheck())
		{
			return;
		}
		sprite_guide_.active = true;
		switch (in_type)
		{
		case GuideType.NO_GUIDE:
			foreach (GuideIcon item in guide_list_)
			{
				mainCtrl.instance.removeText(item.text_);
			}
			sprite_guide_.active = false;
			sprite_line_.active = false;
			break;
		case GuideType.HOUTEI:
			if (!GSMain_SaibanPart.CheckSaveKey() && (GSStatic.global_work_.r.no_0 == 4 || GSStatic.global_work_.r.no_0 == 7 || GSStatic.global_work_.r.no_0 == 6))
			{
				guide_list_[0].SetKeyTypeSprite(KeyType.R);
				guide_list_[0].text_.text = TextDataCtrl.GetText(TextDataCtrl.CommonTextID.RECORD);
				break;
			}
			guide_list_[0].SetKeyTypeSprite(KeyType.Start);
			guide_list_[0].text_.text = TextDataCtrl.GetText(TextDataCtrl.CommonTextID.OPTION);
			guide_list_[1].SetKeyTypeSprite(KeyType.R);
			guide_list_[1].text_.text = TextDataCtrl.GetText(TextDataCtrl.CommonTextID.RECORD);
			break;
		case GuideType.QUESTIONING:
			guide_list_[0].SetKeyTypeSprite(KeyType.Start);
			guide_list_[1].SetKeyTypeSprite(KeyType.L);
			guide_list_[2].SetKeyTypeSprite(KeyType.R);
			guide_list_[0].text_.text = TextDataCtrl.GetText(TextDataCtrl.CommonTextID.OPTION);
			guide_list_[1].text_.text = TextDataCtrl.GetText(TextDataCtrl.CommonTextID.YUSABURU);
			guide_list_[2].text_.text = TextDataCtrl.GetText(TextDataCtrl.CommonTextID.TUKITUKE);
			break;
		case GuideType.RECORD:
			if (advCtrl.instance.sub_window_.status_force_ == 1)
			{
				guide_list_[0].SetKeyTypeSprite(KeyType.Record);
				guide_list_[0].text_.text = TextDataCtrl.GetText(TextDataCtrl.CommonTextID.PROFILES);
				break;
			}
			guide_list_[0].SetKeyTypeSprite(KeyType.B);
			guide_list_[1].SetKeyTypeSprite(KeyType.Record);
			guide_list_[0].text_.text = TextDataCtrl.GetText(TextDataCtrl.CommonTextID.BACK);
			guide_list_[1].text_.text = TextDataCtrl.GetText(TextDataCtrl.CommonTextID.PROFILES);
			break;
		case GuideType.PROFILE:
			if (advCtrl.instance.sub_window_.status_force_ == 1)
			{
				guide_list_[0].SetKeyTypeSprite(KeyType.Record);
				guide_list_[0].text_.text = TextDataCtrl.GetText(TextDataCtrl.CommonTextID.EVIDENCE);
				break;
			}
			guide_list_[0].SetKeyTypeSprite(KeyType.B);
			guide_list_[1].SetKeyTypeSprite(KeyType.Record);
			guide_list_[0].text_.text = TextDataCtrl.GetText(TextDataCtrl.CommonTextID.BACK);
			guide_list_[1].text_.text = TextDataCtrl.GetText(TextDataCtrl.CommonTextID.EVIDENCE);
			break;
		case GuideType.QUESTIONING_RECORD:
			guide_list_[0].SetKeyTypeSprite(KeyType.B);
			guide_list_[1].SetKeyTypeSprite(KeyType.X);
			guide_list_[0].text_.text = TextDataCtrl.GetText(TextDataCtrl.CommonTextID.BACK);
			guide_list_[1].text_.text = TextDataCtrl.GetText(TextDataCtrl.CommonTextID.TUKITUKE);
			if (GSStatic.global_work_.title != 0)
			{
				guide_list_[2].SetKeyTypeSprite(KeyType.Record);
				guide_list_[2].text_.text = TextDataCtrl.GetText(TextDataCtrl.CommonTextID.PROFILES);
			}
			break;
		case GuideType.SHOW_RECORD:
			guide_list_[0].SetKeyTypeSprite(KeyType.B);
			guide_list_[1].SetKeyTypeSprite(KeyType.X);
			guide_list_[0].text_.text = TextDataCtrl.GetText(TextDataCtrl.CommonTextID.BACK);
			guide_list_[1].text_.text = TextDataCtrl.GetText(TextDataCtrl.CommonTextID.TUKITUKE);
			if (GSStatic.global_work_.title != 0)
			{
				guide_list_[2].SetKeyTypeSprite(KeyType.Record);
				guide_list_[2].text_.text = TextDataCtrl.GetText(TextDataCtrl.CommonTextID.PROFILES);
			}
			break;
		case GuideType.QUESTIONING_PROFILE:
		case GuideType.SHOW_PROFILE:
			guide_list_[0].SetKeyTypeSprite(KeyType.B);
			guide_list_[1].SetKeyTypeSprite(KeyType.X);
			guide_list_[2].SetKeyTypeSprite(KeyType.Record);
			guide_list_[0].text_.text = TextDataCtrl.GetText(TextDataCtrl.CommonTextID.BACK);
			guide_list_[1].text_.text = TextDataCtrl.GetText(TextDataCtrl.CommonTextID.TUKITUKE);
			guide_list_[2].text_.text = TextDataCtrl.GetText(TextDataCtrl.CommonTextID.EVIDENCE);
			break;
		case GuideType.FORCE_RECORD:
			guide_list_[0].SetKeyTypeSprite(KeyType.X);
			guide_list_[0].text_.text = TextDataCtrl.GetText(TextDataCtrl.CommonTextID.TUKITUKE);
			if (GSStatic.global_work_.title != 0 || advCtrl.instance.sub_window_.status_force_ == 1)
			{
				guide_list_[1].SetKeyTypeSprite(KeyType.Record);
				guide_list_[1].text_.text = TextDataCtrl.GetText(TextDataCtrl.CommonTextID.PROFILES);
			}
			break;
		case GuideType.FORCE_PROFILE:
			guide_list_[0].SetKeyTypeSprite(KeyType.X);
			guide_list_[0].text_.text = TextDataCtrl.GetText(TextDataCtrl.CommonTextID.TUKITUKE);
			if (GSStatic.global_work_.title != 0 || advCtrl.instance.sub_window_.status_force_ == 1)
			{
				guide_list_[1].SetKeyTypeSprite(KeyType.Record);
				guide_list_[1].text_.text = TextDataCtrl.GetText(TextDataCtrl.CommonTextID.EVIDENCE);
			}
			break;
		case GuideType.Luminol:
			guide_list_[0].SetKeyTypeSprite(KeyType.B);
			guide_list_[1].SetKeyTypeSprite(KeyType.X);
			guide_list_[2].SetKeyTypeSprite(KeyType.Record);
			guide_list_[0].text_.text = TextDataCtrl.GetText(TextDataCtrl.CommonTextID.BACK);
			guide_list_[1].text_.text = TextDataCtrl.GetText(TextDataCtrl.CommonTextID.HUKITUKR);
			guide_list_[2].text_.text = TextDataCtrl.GetText(TextDataCtrl.CommonTextID.PROFILES);
			break;
		case GuideType.CREDIT:
			guide_list_[0].SetKeyTypeSprite(KeyType.B);
			guide_list_[0].text_.text = TextDataCtrl.GetText(TextDataCtrl.CommonTextID.BACK);
			break;
		case GuideType.PSYLOCK:
			guide_list_[0].SetKeyTypeSprite(KeyType.R);
			guide_list_[0].text_.text = TextDataCtrl.GetText(TextDataCtrl.CommonTextID.RECORD);
			break;
		default:
			sprite_guide_.active = false;
			sprite_line_.active = false;
			break;
		}
		UpdateTouchArea();
		SetLanguageLayout();
		guide_cnt = guide_list_.Count((GuideIcon guide) => guide.sprite_.active);
		Vector3 localPosition = sprite_guide_.transform.localPosition;
		localPosition.x = slide_out_pos_x_ - guide_width_ * (float)guide_cnt;
		localPosition.y = guide_pos_y_;
		sprite_guide_.transform.localPosition = localPosition;
	}

	public virtual void guideIconSet(bool in_guide, GuideType in_type)
	{
		foreach (GuideIcon item in guide_list_)
		{
			item.sprite_.active = false;
			mainCtrl.instance.addText(item.text_);
		}
		ActiveTouch();
		settingSprite(in_type);
	}

	public void changeGuide(GuideType in_type)
	{
		if (keyGuideCtrl.instance.CheckClose())
		{
			RotationChange(in_type);
		}
		else if (next_guide_ != 0)
		{
			RotationChange(next_guide_);
		}
		else if (sprite_guide_.active)
		{
			if (current_guide_ != in_type)
			{
				RotationChange(in_type);
			}
		}
		else if (old_guide_ != 0 && in_type != 0 && old_guide_ != in_type)
		{
			RotationChange(in_type);
		}
		else if (enumerator_open_close_ == null)
		{
			body_.transform.localScale = new Vector3(1f, 1f, 1f);
			guideIconSet(true, in_type);
		}
	}

	public void changeSaveOffGuide(GuideType in_type)
	{
		GuideType in_type2 = in_type;
		switch (GSStatic.global_work_.r.no_0)
		{
		case 4:
		case 6:
			in_type2 = GuideType.HOUTEI;
			break;
		case 7:
			if ((GSStatic.message_work_.status & MessageSystem.Status.LOOP) == 0)
			{
				in_type2 = GuideType.HOUTEI;
			}
			break;
		}
		if (sprite_guide_.active)
		{
			RotationChange(in_type2);
		}
		else
		{
			guideIconSet(true, in_type2);
		}
	}

	public void changeGuide()
	{
		changeGuide(GetChangeGuideType());
	}

	public GuideType GetChangeGuideType()
	{
		switch (GSStatic.global_work_.r.no_0)
		{
		case 4:
		case 6:
			return GuideType.HOUTEI;
		case 5:
			if (GSStatic.global_work_.r.no_1 == 11 || bgCtrl.instance.bg_no_now == 254)
			{
				return GuideType.PSYLOCK;
			}
			return GuideType.HOUTEI;
		case 7:
			if ((GSStatic.message_work_.status & MessageSystem.Status.LOOP) != 0)
			{
				return GuideType.QUESTIONING;
			}
			return GuideType.HOUTEI;
		default:
			return GuideType.HOUTEI;
		}
	}

	public void RotationChange(GuideType in_type)
	{
		if (enumerator_open_close_ == null)
		{
			enumerator_open_close_ = CoroutineRotationChange(in_type);
			coroutineCtrl.instance.Play(enumerator_open_close_);
		}
	}

	private IEnumerator CoroutineRotationChange(GuideType in_type)
	{
		yield return coroutineCtrl.instance.Play(close());
		while (keyGuideCtrl.instance.CheckClose())
		{
			yield return null;
		}
		next_guide_ = GuideType.NO_GUIDE;
		yield return coroutineCtrl.instance.Play(open(in_type));
		enumerator_open_close_ = null;
	}

	public IEnumerator open(GuideType in_type)
	{
		if (enumerator_open_ != null)
		{
			coroutineCtrl.instance.Stop(enumerator_open_);
			enumerator_open_ = null;
		}
		if (enumerator_close_ != null)
		{
			coroutineCtrl.instance.Stop(enumerator_close_);
			enumerator_open_ = null;
		}
		enumerator_open_ = CoroutineOpen(in_type);
		return enumerator_open_;
	}

	public IEnumerator close()
	{
		if (enumerator_close_ != null)
		{
			coroutineCtrl.instance.Stop(enumerator_close_);
			enumerator_close_ = null;
		}
		if (enumerator_open_ != null)
		{
			coroutineCtrl.instance.Stop(enumerator_open_);
			enumerator_close_ = null;
		}
		enumerator_close_ = CoroutineClose();
		return enumerator_close_;
	}

	private IEnumerator CoroutineOpen(GuideType in_type)
	{
		guideIconSet(false, in_type);
		float time = 0f;
		while (time < 1f)
		{
			time += curve_.speed_open_;
			if (time > 1f)
			{
				time = 1f;
			}
			float scale = curve_.open_.Evaluate(time);
			body_.transform.localScale = new Vector3(1f, scale, 1f);
			yield return null;
		}
		body_.transform.localScale = new Vector3(1f, 1f, 1f);
		foreach (GuideIcon item in guide_list_)
		{
			item.touch_.ActiveCollider();
		}
	}

	private IEnumerator CoroutineClose()
	{
		float time = 0f;
		if (current_guide_ == GuideType.NO_GUIDE && getEnables())
		{
			guideIconSet(false, old_guide_);
		}
		if (current_guide_ != 0)
		{
			while (time < 1f)
			{
				time += curve_.speed_close_;
				if (time > 1f)
				{
					time = 1f;
				}
				float scale = curve_.close_.Evaluate(time);
				body_.transform.localScale = new Vector3(1f, scale, 1f);
				yield return null;
			}
		}
		body_.transform.localScale = new Vector3(1f, 0f, 1f);
		enumerator_close_ = null;
	}

	public bool is_open_close_guid()
	{
		return enumerator_open_close_ != null;
	}

	public virtual void DebugGuideDisp()
	{
		debug_is_disp_ = !debug_is_disp_;
		guideIconSet(false, current_guide_);
	}

	protected virtual bool DebugDispCheck()
	{
		if (!debug_is_disp_)
		{
			sprite_guide_.active = false;
			return false;
		}
		return true;
	}

	public bool getGuideActive()
	{
		if (body_ != null)
		{
			return body_.activeSelf;
		}
		return false;
	}
}
