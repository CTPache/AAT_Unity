using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tanteiMenu : MonoBehaviour
{
	[Serializable]
	public class SelectPlate
	{
		public AssetBundleSprite sprite_;

		public SpriteRenderer enable_;

		public Text text_;

		public GameObject body_;

		public bool active
		{
			get
			{
				return body_.activeSelf;
			}
			set
			{
				body_.SetActive(value);
			}
		}

		public Transform transform
		{
			get
			{
				return body_.gameObject.transform;
			}
		}

		public int spriteNo
		{
			set
			{
				sprite_.spriteNo(value);
			}
		}

		public string text
		{
			set
			{
				text_.text = value;
			}
		}

		public void InitEnableSprite()
		{
			if (sprite_ != null)
			{
				enable_.sprite = sprite_.sprite_data_[3];
				EnableAlpha(0f);
			}
		}

		public void scale(float in_scale)
		{
			body_.transform.localScale = new Vector3(in_scale, in_scale, 1f);
		}

		public void alpha(float in_alpha)
		{
			Color color = sprite_.sprite_renderer_.color;
			sprite_.sprite_renderer_.color = new Color(color.r, color.g, color.g, in_alpha);
			color = text_.color;
			text_.color = new Color(color.r, color.g, color.g, in_alpha);
		}

		public void EnableAlpha(float in_alpha)
		{
			Color color = enable_.color;
			enable_.color = new Color(color.r, color.g, color.g, in_alpha);
		}
	}

	[Serializable]
	public class SelectCurve
	{
		public AnimationCurve scale_in_ = new AnimationCurve();

		public AnimationCurve alpha_in_ = new AnimationCurve();

		public AnimationCurve scale_enter_ = new AnimationCurve();

		public AnimationCurve alpha_enter_ = new AnimationCurve();

		public AnimationCurve scale_out_ = new AnimationCurve();

		public AnimationCurve alpha_out_ = new AnimationCurve();
	}

	[Serializable]
	public class GuideSymbol
	{
		public AssetBundleSprite sprite_;

		public Text text_;

		public GameObject body_;

		public bool active
		{
			get
			{
				return body_.activeSelf;
			}
			set
			{
				body_.SetActive(value);
			}
		}

		public Transform transform
		{
			get
			{
				return body_.gameObject.transform;
			}
		}

		public int spriteNo
		{
			set
			{
				sprite_.spriteNo(value);
			}
		}

		public string text
		{
			set
			{
				text_.text = value;
			}
		}

		public void scale(float in_scale)
		{
			body_.transform.localScale = new Vector3(1f, in_scale, 1f);
		}

		public void alpha(float in_alpha)
		{
			Color color = sprite_.sprite_renderer_.color;
			sprite_.sprite_renderer_.color = new Color(color.r, color.g, color.g, in_alpha);
		}
	}

	[Serializable]
	public class GuideCurve
	{
		public AnimationCurve scale_in_ = new AnimationCurve();

		public AnimationCurve alpha_in_ = new AnimationCurve();

		public AnimationCurve scale_out_ = new AnimationCurve();

		public AnimationCurve alpha_out_ = new AnimationCurve();
	}

	private static tanteiMenu instance_;

	[SerializeField]
	private GameObject body_;

	[SerializeField]
	private GameObject menu_;

	[SerializeField]
	private AssetBundleSprite menu_bg_;

	[SerializeField]
	private AssetBundleSprite cursor_;

	[SerializeField]
	private List<SelectPlate> select_list_ = new List<SelectPlate>();

	[SerializeField]
	private SelectCurve select_curve_;

	[SerializeField]
	private InputTouch check_touch_;

	[SerializeField]
	private InputTouch moving_touch_;

	[SerializeField]
	private InputTouch speak_touch_;

	[SerializeField]
	private InputTouch point_out_touch_;

	[SerializeField]
	private selectPlateCtrl.EnterCurve enter_curve_;

	[SerializeField]
	private selectPlateCtrl.EnableCurve enable_curve_;

	private IEnumerator enumerator_play_;

	private int setting_;

	private int cursor_num_;

	private int cursor_no_;

	private float pos_x_ = -525f;

	private float space_ = 350f;

	private bool is_play_;

	private bool select_animation_playing_;

	private bool is_loop_;

	public static tanteiMenu instance
	{
		get
		{
			return instance_;
		}
	}

	public bool active
	{
		get
		{
			return body_.activeSelf;
		}
		set
		{
			body_.SetActive(value);
		}
	}

	public bool is_play
	{
		get
		{
			return is_play_;
		}
	}

	public bool select_animation_playing
	{
		get
		{
			return select_animation_playing_;
		}
	}

	public int setting
	{
		get
		{
			return setting_;
		}
		set
		{
			setting_ = value;
		}
	}

	public int cursor_no
	{
		get
		{
			return cursor_no_;
		}
		set
		{
			cursor_no_ = value;
		}
	}

	private SelectPlate select_plate
	{
		get
		{
			return select_list_[cursor_no_];
		}
	}

	private void Awake()
	{
		if (instance_ == null)
		{
			instance_ = this;
		}
	}

	public void load()
	{
		active = false;
		cursor_.load("/menu/common/", "select_button");
		menu_bg_.load("/menu/common/", "button_bg");
		foreach (SelectPlate item in select_list_)
		{
			item.sprite_.load("/menu/common/", "select_button");
			item.spriteNo = 2;
			item.InitEnableSprite();
		}
	}

	public void init()
	{
		load();
		check_touch_.touch_event = CheckTouch;
		check_touch_.touch_key_type = KeyType.A;
		moving_touch_.touch_event = MovingTouch;
		moving_touch_.touch_key_type = KeyType.A;
		speak_touch_.touch_event = SpeakTouch;
		speak_touch_.touch_key_type = KeyType.A;
		point_out_touch_.touch_event = PointTouch;
		point_out_touch_.touch_key_type = KeyType.A;
		int fontSize;
		switch (GSStatic.global_work_.language)
		{
		default:
			fontSize = 46;
			break;
		case Language.FRANCE:
		case Language.GERMAN:
			fontSize = 36;
			break;
		case Language.KOREA:
			fontSize = 38;
			break;
		case Language.CHINA_T:
			fontSize = 40;
			break;
		}
		for (int i = 0; i < select_list_.Count; i++)
		{
			select_list_[i].text_.fontSize = fontSize;
		}
	}

	public void setMenu(int in_type)
	{
		selectPlateCtrl.instance.cursor_no = 0;
		selectPlateCtrl.instance.old_cursor_no = 0;
		selectPlateCtrl.instance.old_cursor_num = 0;
		string[] array = new string[4]
		{
			TextDataCtrl.GetText(TextDataCtrl.CommonTextID.INSPECT),
			TextDataCtrl.GetText(TextDataCtrl.CommonTextID.ROOM_MOVE),
			TextDataCtrl.GetText(TextDataCtrl.CommonTextID.TALK),
			TextDataCtrl.GetText(TextDataCtrl.CommonTextID.TUKITUKE)
		};
		foreach (SelectPlate item in select_list_)
		{
			item.active = false;
		}
		if (setting_ != in_type)
		{
			cursor_no_ = 0;
		}
		setting_ = in_type;
		switch (in_type)
		{
		case 0:
			cursor_num_ = 2;
			menu_.transform.localPosition = new Vector3(0f, -350f, 0f);
			UpdateCursorPos();
			select_list_[0].active = true;
			select_list_[1].active = true;
			select_list_[0].transform.localPosition = new Vector3(pos_x_ + space_ * 1f, 0f, 0f);
			select_list_[1].transform.localPosition = new Vector3(pos_x_ + space_ * 2f, 0f, 0f);
			select_list_[0].text_.text = array[0];
			select_list_[1].text_.text = array[1];
			break;
		case 1:
			cursor_num_ = 4;
			menu_.transform.localPosition = new Vector3(0f, -350f, 0f);
			cursor_.transform.localPosition = new Vector3(pos_x_ + space_ * (float)cursor_no_, 0f, cursor_.transform.localPosition.z);
			select_list_[0].active = true;
			select_list_[1].active = true;
			select_list_[2].active = true;
			select_list_[3].active = true;
			select_list_[0].transform.localPosition = new Vector3(pos_x_ + space_ * 0f, 0f, 0f);
			select_list_[1].transform.localPosition = new Vector3(pos_x_ + space_ * 1f, 0f, 0f);
			select_list_[2].transform.localPosition = new Vector3(pos_x_ + space_ * 2f, 0f, 0f);
			select_list_[3].transform.localPosition = new Vector3(pos_x_ + space_ * 3f, 0f, 0f);
			select_list_[0].text_.text = array[0];
			select_list_[1].text_.text = array[1];
			select_list_[2].text_.text = array[2];
			select_list_[3].text_.text = array[3];
			break;
		}
	}

	public void end()
	{
		cursor_.end();
		select_list_[0].sprite_.end();
		select_list_[1].sprite_.end();
		select_list_[2].sprite_.end();
		select_list_[3].sprite_.end();
		is_play_ = false;
		select_animation_playing_ = false;
		stop();
	}

	public void close()
	{
		stop();
		active = false;
	}

	public IEnumerator CoroutinePlay(int in_type)
	{
		setMenu(in_type);
		active = true;
		is_play_ = true;
		select_animation_playing_ = false;
		if (GSMain_TanteiPart.IsBGSlide(bgCtrl.instance.bg_no))
		{
			coroutineCtrl.instance.Play(keyGuideCtrl.instance.open(keyGuideBase.Type.TANTEI_SLIDE));
		}
		else
		{
			coroutineCtrl.instance.Play(keyGuideCtrl.instance.open(keyGuideBase.Type.TANTEI));
		}
		float time2 = 0f;
		is_loop_ = true;
		while (is_loop_)
		{
			time2 += 0.1f;
			if (time2 > 1f)
			{
				time2 = 1f;
				is_loop_ = false;
			}
			float num = enter_curve_.scale_.Evaluate(time2);
			float num2 = enter_curve_.alpha_.Evaluate(time2);
			Color color = cursor_.sprite_renderer_.color;
			cursor_.sprite_renderer_.color = new Color(color.r, color.g, color.g, num2);
			cursor_.transform.localScale = new Vector3(num, num, 1f);
			foreach (SelectPlate item in select_list_)
			{
				item.scale(num);
				item.alpha(num2);
			}
			float y = select_curve_.scale_in_.Evaluate(time2);
			menu_bg_.transform.localScale = new Vector3(1f, y, 1f);
			UpdateTouchCollider();
			yield return null;
		}
		float key_wait = systemCtrl.instance.key_wait;
		bool is_tantei_mode = true;
		while (true)
		{
			if (GSStatic.global_work_.r.no_0 != 17)
			{
				if (!is_tantei_mode)
				{
					UpdateTouchCollider();
					keyGuideCtrl.instance.ActiveKeyTouch();
					is_tantei_mode = true;
				}
				if (key_wait > 0f)
				{
					key_wait -= 1f;
				}
				else if (padCtrl.instance.GetKeyDown(KeyType.A))
				{
					break;
				}
				if (padCtrl.instance.IsNextMove())
				{
					if (padCtrl.instance.GetKeyDown(KeyType.Right) || padCtrl.instance.GetKeyDown(KeyType.StickL_Right) || padCtrl.instance.GetWheelMoveDown())
					{
						cursor(true);
					}
					else if (padCtrl.instance.GetKeyDown(KeyType.Left) || padCtrl.instance.GetKeyDown(KeyType.StickL_Left) || padCtrl.instance.GetWheelMoveUp())
					{
						cursor(false);
					}
				}
				padCtrl.instance.WheelMoveValUpdate();
			}
			else
			{
				is_tantei_mode = false;
			}
			yield return null;
		}
		TouchSystem.TouchInActive();
		soundCtrl.instance.PlaySE(43);
		select_animation_playing_ = true;
		coroutineCtrl.instance.Play(keyGuideCtrl.instance.close());
		float time = 0f;
		bool is_loop = true;
		while (is_loop)
		{
			time += 0.1f;
			if (time > 1f)
			{
				time = 1f;
				is_loop = false;
			}
			float num3 = enable_curve_.alpha_.Evaluate(time);
			foreach (SelectPlate item2 in select_list_)
			{
				item2.alpha(num3);
			}
			Color color2 = cursor_.color;
			cursor_.color = new Color(color2.r, color2.g, color2.g, num3);
			float num4 = enable_curve_.cursor_.Evaluate(time);
			cursor_.transform.localScale = new Vector3(num4, num4, 1f);
			float num5 = enable_curve_.select_.Evaluate(time);
			select_plate.transform.localScale = new Vector3(num5, num5, 1f);
			float in_alpha = enable_curve_.enable_.Evaluate(time);
			select_plate.EnableAlpha(in_alpha);
			float y2 = select_curve_.scale_out_.Evaluate(time);
			menu_bg_.transform.localScale = new Vector3(1f, y2, 1f);
			yield return null;
		}
		is_play_ = false;
		select_animation_playing_ = false;
	}

	public void cursor(bool is_right)
	{
		cursor_no_ = ((!is_right) ? (cursor_no_ - 1) : (cursor_no_ + 1));
		cursor_no_ = ((cursor_no_ >= 0) ? cursor_no_ : (cursor_num_ - 1));
		cursor_no_ = ((cursor_no_ < cursor_num_) ? cursor_no_ : 0);
		UpdateCursorPos();
		soundCtrl.instance.PlaySE(42);
	}

	public void play(int in_type)
	{
		active = true;
		stop();
		TouchSystem.TouchInActive();
		enumerator_play_ = CoroutinePlay(in_type);
		coroutineCtrl.instance.Play(enumerator_play_);
	}

	private void stop()
	{
		if (enumerator_play_ != null)
		{
			coroutineCtrl.instance.Stop(enumerator_play_);
			enumerator_play_ = null;
		}
		active = false;
	}

	private void CheckTouch(TouchParameter touch)
	{
		if (!is_loop_)
		{
			cursor_no_ = 0;
			UpdateCursorPos();
		}
	}

	private void MovingTouch(TouchParameter touch)
	{
		if (!is_loop_)
		{
			cursor_no_ = 1;
			UpdateCursorPos();
		}
	}

	private void SpeakTouch(TouchParameter touch)
	{
		if (!is_loop_)
		{
			cursor_no_ = 2;
			UpdateCursorPos();
		}
	}

	private void PointTouch(TouchParameter touch)
	{
		if (!is_loop_)
		{
			cursor_no_ = 3;
			UpdateCursorPos();
		}
	}

	private void UpdateCursorPos()
	{
		float x = pos_x_ + space_ * (float)((setting_ != 0) ? cursor_no_ : (cursor_no_ + 1));
		cursor_.transform.localPosition = new Vector3(x, 0f, cursor_.transform.localPosition.z);
	}

	private void UpdateTouchCollider()
	{
		check_touch_.ActiveCollider();
		moving_touch_.ActiveCollider();
		speak_touch_.ActiveCollider();
		point_out_touch_.ActiveCollider();
	}
}
