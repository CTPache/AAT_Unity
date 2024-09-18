using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class selectPlateCtrl : MonoBehaviour
{
	public enum FromEntryRequest
	{
		NONE = 0,
		TALK = 1,
		SELECT = 2,
		SELECT_LOAD = 3
	}

	[Serializable]
	public class SelectPlate
	{
		public AssetBundleSprite select_;

		public AssetBundleSprite enable_;

		public AssetBundleSprite psylook_;

		public Text text_;

		public InputTouch touch_;

		public bool active
		{
			get
			{
				return select_.active;
			}
			set
			{
				select_.active = value;
			}
		}
	}

	[Serializable]
	public class EnterCurve
	{
		public AnimationCurve mask_ = new AnimationCurve();

		public AnimationCurve scale_ = new AnimationCurve();

		public AnimationCurve alpha_ = new AnimationCurve();
	}

	[Serializable]
	public class EnableCurve
	{
		public AnimationCurve select_ = new AnimationCurve();

		public AnimationCurve cursor_ = new AnimationCurve();

		public AnimationCurve alpha_ = new AnimationCurve();

		public AnimationCurve enable_ = new AnimationCurve();
	}

	private static selectPlateCtrl instance_;

	[SerializeField]
	private List<SelectPlate> select_list_ = new List<SelectPlate>();

	[SerializeField]
	private AssetBundleSprite cursor_;

	[SerializeField]
	private GameObject body_;

	[SerializeField]
	private EnterCurve enter_curve_ = new EnterCurve();

	[SerializeField]
	private EnableCurve enable_curve_ = new EnableCurve();

	[SerializeField]
	private SpriteRenderer mask_;

	public float alpha_;

	public float mask_alpha_ = 0.5f;

	private int cursor_num_;

	private int cursor_no_;

	private int old_cursor_no_;

	private float pos_y_;

	private float space_ = 120f;

	private IEnumerator enumerator_cursor_;

	private IEnumerator enumerator_enable_;

	private bool is_select_;

	private bool is_talk_;

	private bool select_animation_playing_;

	private bool is_end_;

	private bool is_cancel_;

	private int type_;

	private FromEntryRequest from_request_;

	public static selectPlateCtrl instance
	{
		get
		{
			return instance_;
		}
	}

	public bool body_active
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

	public AssetBundleSprite select_sprite
	{
		get
		{
			return select_list_[cursor_no_].select_;
		}
	}

	public AssetBundleSprite enable_sprite
	{
		get
		{
			return select_list_[cursor_no_].enable_;
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

	public int old_cursor_no
	{
		get
		{
			return old_cursor_no_;
		}
		set
		{
			old_cursor_no_ = value;
		}
	}

	public bool is_select
	{
		get
		{
			return is_select_;
		}
		set
		{
			is_select_ = value;
		}
	}

	public bool is_talk
	{
		get
		{
			return is_talk_;
		}
		set
		{
			is_talk_ = value;
		}
	}

	public bool select_animation_playing
	{
		get
		{
			return select_animation_playing_;
		}
	}

	public bool is_end
	{
		get
		{
			return is_end_;
		}
	}

	public bool is_cancel
	{
		get
		{
			return is_cancel_;
		}
	}

	public int type
	{
		get
		{
			return type_;
		}
		set
		{
			type_ = value;
		}
	}

	public int old_cursor_num { get; set; }

	private void Awake()
	{
		if (instance_ == null)
		{
			instance_ = this;
		}
	}

	public void load()
	{
		string text;
		switch (GSUtility.GetLanguageLayoutType(GSStatic.global_work_.language))
		{
		case Language.JAPAN:
			text = string.Empty;
			break;
		case Language.USA:
			text = "u";
			break;
		default:
			text = "u";
			break;
		}
		cursor_.load("/menu/common/", "select_window" + text);
		cursor_.spriteNo(0);
		foreach (var item in select_list_.Select((SelectPlate item, int index) => new { item, index }))
		{
			item.item.select_.load("/menu/common/", "select_window" + text);
			item.item.enable_.load("/menu/common/", "select_window" + text);
			item.item.psylook_.load("/menu/common/", "plicn");
			item.item.select_.spriteNo(2);
			item.item.enable_.spriteNo(5);
			item.item.psylook_.spriteNo(0);
			item.item.touch_.touch_key_type = KeyType.A;
			item.item.touch_.touch_event = TouchItem;
			item.item.touch_.argument_parameter = item.index;
			Vector3 localPosition = item.item.text_.transform.localPosition;
			Vector2 sizeDelta = item.item.text_.rectTransform.sizeDelta;
			Vector3 localPosition2 = item.item.psylook_.transform.localPosition;
			int fontSize;
			switch (GSStatic.global_work_.language)
			{
			case Language.JAPAN:
				fontSize = 46;
				localPosition.y = -5f;
				sizeDelta.x = 640f;
				localPosition2.x = -315f;
				item.item.touch_.SetColliderSize(new Vector2(720f, 60f));
				break;
			default:
				fontSize = 37;
				localPosition.y = -8f;
				sizeDelta.x = 800f;
				localPosition2.x = -375f;
				item.item.touch_.SetColliderSize(new Vector2(item.item.select_.sprite_renderer_.sprite.rect.size.x, 60f));
				break;
			case Language.KOREA:
				fontSize = 38;
				localPosition.y = -7f;
				sizeDelta.x = 640f;
				localPosition2.x = -315f;
				item.item.touch_.SetColliderSize(new Vector2(720f, 60f));
				break;
			case Language.CHINA_S:
				fontSize = 46;
				localPosition.y = 0f;
				sizeDelta.x = 640f;
				localPosition2.x = -315f;
				item.item.touch_.SetColliderSize(new Vector2(720f, 60f));
				break;
			case Language.CHINA_T:
				fontSize = 40;
				localPosition.y = -2f;
				sizeDelta.x = 640f;
				localPosition2.x = -315f;
				item.item.touch_.SetColliderSize(new Vector2(720f, 60f));
				break;
			}
			item.item.text_.fontSize = fontSize;
			item.item.text_.transform.localPosition = localPosition;
			item.item.text_.rectTransform.sizeDelta = sizeDelta;
			item.item.psylook_.transform.localPosition = localPosition2;
			item.item.psylook_.active = false;
		}
		foreach (SelectPlate item2 in select_list_)
		{
			mainCtrl.instance.addText(item2.text_);
		}
		entryCursor(2, FromEntryRequest.NONE);
	}

	public void init()
	{
		load();
	}

	public void end()
	{
		stopCursor();
		stopEnable();
		is_select_ = false;
		is_talk_ = false;
		select_animation_playing_ = false;
		is_end_ = false;
		is_cancel_ = false;
		cursor_no_ = 0;
		body_active = false;
	}

	public void Terminate()
	{
		foreach (SelectPlate item in select_list_)
		{
			mainCtrl.instance.removeText(item.text_);
		}
	}

	public void entryCursor(int in_num, FromEntryRequest request)
	{
		from_request_ = request;
		cursor_num_ = ((in_num >= select_list_.Count) ? select_list_.Count : in_num);
		float num = (pos_y_ = (float)(cursor_num_ - 1) * 60f);
		switch (from_request_)
		{
		case FromEntryRequest.TALK:
			if (old_cursor_num > cursor_num_)
			{
				old_cursor_no_ = 0;
			}
			cursor_no_ = old_cursor_no_;
			old_cursor_num = cursor_num_;
			break;
		default:
			cursor_no_ = 0;
			break;
		case FromEntryRequest.SELECT_LOAD:
			break;
		}
		cursor_.transform.localPosition = new Vector3(0f, num - (float)cursor_no_ * 120f, 0f);
		for (int i = 0; i < select_list_.Count; i++)
		{
			if (i < cursor_num_)
			{
				select_list_[i].active = true;
				select_list_[i].select_.transform.localPosition = new Vector3(0f, num, 0f);
				num -= space_;
			}
			else
			{
				select_list_[i].active = false;
			}
		}
	}

	public void setText(int index, string text)
	{
		select_list_[index].text_.text = text;
		select_list_[index].select_.spriteNo(2);
		select_list_[index].psylook_.active = false;
	}

	public void setRead(int index, bool is_read, bool is_psylooc = false)
	{
		TALK_DATA tALK_DATA = null;
		for (int i = 0; i < GSStatic.talk_work_.talk_data_.Length && GSStatic.talk_work_.talk_data_[i].room != 255; i++)
		{
			tALK_DATA = GSStatic.talk_work_.talk_data_[i];
			if (tALK_DATA.room == GSStatic.global_work_.Room && tALK_DATA.pl_id == AnimationSystem.Instance.IdlingCharacterMasked && tALK_DATA.sw == 1)
			{
				break;
			}
		}
		switch (GSStatic.global_work_.title)
		{
		case TitleId.GS1:
			if (GSStatic.global_work_.scenario == 3)
			{
				if (tALK_DATA.mess[index] == scenario.SC1_03240 && !GSFlag.Check(0u, scenario.SCE2_KONAKA_ANGRY))
				{
					is_read = false;
				}
				else if (tALK_DATA.mess[index] == scenario.SC1_02700)
				{
					is_read = false;
				}
			}
			else if (GSStatic.global_work_.scenario == 9)
			{
				if (tALK_DATA.mess[index] == scenario.SC2_17020 && !GSFlag.Check(0u, scenario.SCE24_GET_PHOTO))
				{
					is_read = false;
				}
			}
			else if (GSStatic.global_work_.scenario == 13)
			{
				if (tALK_DATA.mess[index] == scenario.SC3_03240)
				{
					is_read = false;
				}
			}
			else if (GSStatic.global_work_.scenario == 28)
			{
				if (tALK_DATA.mess[index] == 168)
				{
					is_read = false;
				}
				else if (tALK_DATA.mess[index] == 172)
				{
					is_read = false;
				}
			}
			break;
		case TitleId.GS2:
			if (GSStatic.global_work_.scenario == 8 && tALK_DATA.mess[index] == 276)
			{
				is_read = false;
			}
			break;
		case TitleId.GS3:
			if (GSStatic.global_work_.scenario == 9)
			{
				if (tALK_DATA.mess[index] == scenario_GS3.SC2_2_42990 && GSStatic.talk_work_.talk_data_[scenario_GS3.SC2_2_TALK_IGARASHI00].sw == 1)
				{
					is_read = false;
				}
			}
			else if (GSStatic.global_work_.scenario == 19 && tALK_DATA.mess[index] == 314)
			{
				is_read = false;
			}
			break;
		}
		if (is_read && is_psylooc)
		{
			select_list_[index].select_.spriteNo(4);
			select_list_[index].psylook_.active = true;
		}
		else
		{
			select_list_[index].select_.spriteNo((!is_read) ? 4 : 3);
			select_list_[index].psylook_.active = false;
		}
	}

	public void playCursor(int in_type = 0)
	{
		messageBoardCtrl.instance.InActiveNormalMessageNextTouch();
		is_select_ = false;
		is_talk_ = false;
		if (in_type == 0)
		{
			is_select_ = true;
		}
		else
		{
			is_talk_ = true;
		}
		type_ = in_type;
		stopCursor();
		foreach (SelectPlate item in select_list_)
		{
			mainCtrl.instance.addText(item.text_);
		}
		enumerator_cursor_ = CoroutineCursor();
		coroutineCtrl.instance.Play(enumerator_cursor_);
	}

	public void stopCursor()
	{
		if (enumerator_cursor_ != null)
		{
			coroutineCtrl.instance.Stop(enumerator_cursor_);
			enumerator_cursor_ = null;
		}
	}

	private void TouchItem(TouchParameter touch)
	{
		cursor_no_ = (int)touch.argument_parameter;
		cursor_.transform.localPosition = new Vector3(0f, pos_y_ - space_ * (float)cursor_no_, 0f);
		if (type_ != 0)
		{
			old_cursor_no_ = cursor_no_;
		}
	}

	private IEnumerator CoroutineCursor()
	{
		is_end_ = false;
		is_cancel_ = false;
		body_active = true;
		float time = 0f;
		while (true)
		{
			time += 0.1f;
			if (time > 1f)
			{
				break;
			}
			float num = enter_curve_.mask_.Evaluate(time);
			Color color = mask_.color;
			mask_.color = new Color(color.r, color.g, color.g, mask_alpha_ * num);
			float num2 = enter_curve_.scale_.Evaluate(time);
			float a = enter_curve_.alpha_.Evaluate(time);
			Color white = Color.white;
			Color color2 = cursor_.sprite_renderer_.color;
			cursor_.sprite_renderer_.color = new Color(color2.r, color2.g, color2.g, a);
			cursor_.transform.localScale = new Vector3(num2, num2, 1f);
			foreach (SelectPlate item in select_list_)
			{
				color2 = item.select_.sprite_renderer_.color;
				item.select_.sprite_renderer_.color = new Color(color2.r, color2.g, color2.g, a);
				item.select_.transform.localScale = new Vector3(num2, num2, 1f);
				color2 = item.select_.sprite_renderer_.color;
				item.text_.color = new Color(color2.r, color2.g, color2.g, a);
				color2 = item.psylook_.sprite_renderer_.color;
				item.psylook_.color = new Color(color2.r, color2.g, color2.g, a);
			}
			yield return null;
		}
		Color color3 = mask_.color;
		mask_.color = new Color(color3.r, color3.g, color3.g, mask_alpha_);
		Color white2 = Color.white;
		Color color4 = cursor_.sprite_renderer_.color;
		cursor_.sprite_renderer_.color = new Color(color4.r, color4.g, color4.g, 1f);
		cursor_.transform.localScale = new Vector3(1f, 1f, 1f);
		foreach (SelectPlate item2 in select_list_)
		{
			color4 = item2.select_.sprite_renderer_.color;
			item2.select_.sprite_renderer_.color = new Color(color4.r, color4.g, color4.g, 1f);
			item2.select_.transform.localScale = new Vector3(1f, 1f, 1f);
			color4 = item2.select_.sprite_renderer_.color;
			item2.text_.color = new Color(color4.r, color4.g, color4.g, 1f);
			color4 = item2.psylook_.sprite_renderer_.color;
			item2.psylook_.color = new Color(color4.r, color4.g, color4.g, 1f);
		}
		setAlpha(1f);
		float key_wait = systemCtrl.instance.key_wait;
		bool is_plate_mode = false;
		while (true)
		{
			if (!advCtrl.instance.sub_window_.IsBusy() && GSStatic.global_work_.r.no_0 != 8 && GSStatic.global_work_.r.no_0 != 17)
			{
				if (!is_plate_mode)
				{
					ActivePlateTouch();
					keyGuideCtrl.instance.ActiveKeyTouch();
					is_plate_mode = true;
				}
				if (key_wait > 0f)
				{
					key_wait -= 1f;
				}
				else
				{
					if (padCtrl.instance.GetKeyDown(KeyType.A))
					{
						soundCtrl.instance.PlaySE(43);
						playEnable();
						break;
					}
					if (type_ == 1 && padCtrl.instance.GetKeyDown(KeyType.B))
					{
						soundCtrl.instance.PlaySE(44);
						is_cancel_ = true;
						playEnable();
						break;
					}
				}
				if (padCtrl.instance.IsNextMove())
				{
					if (cursor_num_ > 1 && (padCtrl.instance.GetKeyDown(KeyType.Up) || padCtrl.instance.GetKeyDown(KeyType.StickL_Up) || padCtrl.instance.GetWheelMoveUp()))
					{
						cursor_no_--;
						soundCtrl.instance.PlaySE(42);
					}
					if (cursor_num_ > 1 && (padCtrl.instance.GetKeyDown(KeyType.Down) || padCtrl.instance.GetKeyDown(KeyType.StickL_Down) || padCtrl.instance.GetWheelMoveDown()))
					{
						cursor_no_++;
						soundCtrl.instance.PlaySE(42);
					}
				}
				padCtrl.instance.WheelMoveValUpdate();
				cursor_no_ = ((cursor_no_ >= 0) ? cursor_no_ : (cursor_num_ - 1));
				cursor_no_ = ((cursor_no_ < cursor_num_) ? cursor_no_ : 0);
				cursor_.transform.localPosition = new Vector3(0f, pos_y_ - space_ * (float)cursor_no_, 0f);
				if (type_ != 0)
				{
					old_cursor_no_ = cursor_no_;
				}
			}
			else
			{
				is_plate_mode = false;
			}
			yield return null;
		}
		foreach (SelectPlate item3 in select_list_)
		{
			mainCtrl.instance.removeText(item3.text_);
		}
	}

	public void playEnable()
	{
		stopEnable();
		if (messageBoardCtrl.instance.body_active)
		{
			messageBoardCtrl.instance.ActiveNormalMessageNextTouch();
		}
		enumerator_enable_ = CoroutineEnable();
		coroutineCtrl.instance.Play(enumerator_enable_);
	}

	private void stopEnable()
	{
		if (enumerator_enable_ != null)
		{
			coroutineCtrl.instance.Stop(enumerator_enable_);
			enumerator_enable_ = null;
		}
	}

	private IEnumerator CoroutineEnable()
	{
		select_animation_playing_ = true;
		is_end_ = false;
		float time = 0f;
		bool is_end = false;
		while (true)
		{
			time += 0.1f;
			if (time > 1f)
			{
				time = 1f;
				is_end = true;
			}
			setAlpha(enable_curve_.alpha_.Evaluate(time));
			if (!is_cancel_)
			{
				float num = enable_curve_.cursor_.Evaluate(time);
				cursor_.transform.localScale = new Vector3(num, num, 1f);
				float num2 = enable_curve_.select_.Evaluate(time);
				select_sprite.transform.localScale = new Vector3(num2, num2, 1f);
				float a = enable_curve_.enable_.Evaluate(time);
				Color color = enable_sprite.sprite_renderer_.color;
				enable_sprite.sprite_renderer_.color = new Color(color.r, color.g, color.g, a);
			}
			if (is_end)
			{
				break;
			}
			yield return null;
		}
		cursor_.transform.localScale = Vector3.one;
		select_sprite.transform.localScale = Vector3.one;
		body_active = false;
		select_animation_playing_ = false;
		is_end_ = true;
		is_select_ = false;
		is_talk_ = false;
	}

	private void setAlpha(float in_alpha)
	{
		Color white = Color.white;
		foreach (SelectPlate item in select_list_)
		{
			white = item.select_.sprite_renderer_.color;
			item.select_.sprite_renderer_.color = new Color(white.r, white.g, white.g, in_alpha);
			white = item.text_.color;
			item.text_.color = new Color(white.r, white.g, white.g, in_alpha);
			white = item.psylook_.color;
			item.psylook_.color = new Color(white.r, white.g, white.g, in_alpha);
		}
		white = cursor_.sprite_renderer_.color;
		cursor_.sprite_renderer_.color = new Color(white.r, white.g, white.g, in_alpha);
		white = mask_.color;
		mask_.color = new Color(white.r, white.g, white.g, mask_alpha_ * in_alpha);
	}

	private void ActivePlateTouch()
	{
		foreach (SelectPlate item in select_list_)
		{
			item.touch_.ActiveCollider();
		}
	}

	public void SetCursorNo(int in_cursor_no)
	{
		cursor_no_ = in_cursor_no;
		cursor_.transform.localPosition = new Vector3(0f, pos_y_ - space_ * (float)cursor_no_, 0f);
	}
}
