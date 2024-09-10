using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class moveCtrl : MonoBehaviour
{
	[Serializable]
	public class SelectPlate
	{
		public AssetBundleSprite sprite_;

		public Text text_;

		public SpriteRenderer enable_;

		public GameObject select_;

		public uint bg_no_;

		public uint thumbnail_;

		public InputTouch touch_;

		public Transform transform
		{
			get
			{
				return select_.transform;
			}
		}

		public bool active
		{
			get
			{
				return select_.activeSelf;
			}
			set
			{
				select_.SetActive(value);
			}
		}

		public string text
		{
			get
			{
				return text_.text;
			}
			set
			{
				text_.text = value;
			}
		}
	}

	[Serializable]
	public class Place
	{
		public AssetBundleSprite sprite_;

		public GameObject place_;

		public Transform transform
		{
			get
			{
				return place_.gameObject.transform;
			}
		}

		public void load(string in_path, string in_name)
		{
			sprite_.load(in_path, in_name);
		}

		public void spriteNo(int in_sprite_no)
		{
			sprite_.spriteNo(in_sprite_no);
		}

		public void end()
		{
			sprite_.end();
		}
	}

	[Serializable]
	public class Curve
	{
		public AnimationCurve open_ = new AnimationCurve();

		public AnimationCurve close_ = new AnimationCurve();

		public float speed_open_ = 0.05f;

		public float speed_close_ = 0.05f;
	}

	private static moveCtrl instance_ = null;

	public static readonly string[] GS1_JP_ThumbFileNames = new string[29]
	{
		"thumb02", "thumb00", "thumb09", "thumb0c", "thumb0a", "thumb0b", "thumb01", "thumb07", "thumb04", "thumb06",
		"thumb03", "thumb08", "thumb0e", "thumb05", "thumb15", "thumb16", "thumb0f", "thumb10", "thumb12", "thumb13",
		"thumb14", "thumb19", "thumb18", "thumb17", "thumb1c", "thumb1a", "thumb1b", "thumb0d", "thumb11"
	};

	public static readonly string[] GS2_JP_ThumbFileNames = new string[27]
	{
		"thumb00", "thumb01", "thumb02", "thumb03", "thumb04", "thumb05", "thumb06", "thumb07", "thumb08", "thumb09",
		"thumb0a", "thumb0b", "thumb0c", "thumb0d", "thumb0e", "thumb0f", "thumb10", "thumb11", "thumb12", "thumb13",
		"thumb14", "thumb15", "thumb16", "thumb17", "thumb18", "thumb19", "thumb1a"
	};

	public static readonly string[] GS3_JP_ThumbFileNames = new string[36]
	{
		"thumb00", "thumb01", "thumb02", "thumb03", "thumb04", "thumb05", "thumb06", "thumb07", "thumb08", "thumb09",
		"thumb10", "thumb11", "thumb12", "thumb13", "thumb14", "thumb15", "thumb16", "thumb17", "thumb18", "thumb19",
		"thumb20", "thumb21", "thumb22", "thumb23", "thumb24", "thumb25", "thumb26", "thumb27", "thumb28", "thumb29",
		"thumb30", "thumb31", "thumb32", "thumb33", "thumb34", "thumb35"
	};

	public static readonly string[] GS1_US_ThumbFileNames = new string[29]
	{
		"thumb02", "thumb00", "thumb09", "thumb0c", "thumb0au", "thumb0b", "thumb01", "thumb07", "thumb04u", "thumb06u",
		"thumb03u", "thumb08", "thumb0e", "thumb05u", "thumb15u", "thumb16", "thumb0fu", "thumb10u", "thumb12u", "thumb13u",
		"thumb14u", "thumb19", "thumb18", "thumb17u", "thumb1c", "thumb1a", "thumb1bu", "thumb0d", "thumb11u"
	};

	public static readonly string[] GS2_US_ThumbFileNames = new string[27]
	{
		"thumb00", "thumb01", "thumb02u", "thumb03u", "thumb04u", "thumb05", "thumb06", "thumb07", "thumb08u", "thumb09u",
		"thumb0a", "thumb0bu", "thumb0cu", "thumb0du", "thumb0e", "thumb0fu", "thumb10", "thumb11", "thumb12u", "thumb13",
		"thumb14u", "thumb15", "thumb16u", "thumb17u", "thumb18", "thumb19", "thumb1au"
	};

	public static readonly string[] GS3_US_ThumbFileNames = new string[36]
	{
		"thumb00", "thumb01", "thumb02", "thumb03u", "thumb04u", "thumb05u", "thumb06u", "thumb07", "thumb08u", "thumb09",
		"thumb10", "thumb11u", "thumb12u", "thumb13u", "thumb14u", "thumb15", "thumb16", "thumb17u", "thumb18", "thumb19",
		"thumb20", "thumb21u", "thumb22u", "thumb23", "thumb24u", "thumb25", "thumb26u", "thumb27u", "thumb28u", "thumb29",
		"thumb30", "thumb31", "thumb32", "thumb33u", "thumb34u", "thumb35"
	};

	public string[,][] ThumbFileNames = new string[3, 2][]
	{
		{ GS1_JP_ThumbFileNames, GS1_US_ThumbFileNames },
		{ GS2_JP_ThumbFileNames, GS2_US_ThumbFileNames },
		{ GS3_JP_ThumbFileNames, GS3_US_ThumbFileNames }
	};

	[SerializeField]
	private GameObject body_;

	[SerializeField]
	private AssetBundleSprite cursor_;

	[SerializeField]
	private Place place_;

	[SerializeField]
	private List<SelectPlate> select_list_ = new List<SelectPlate>();

	[SerializeField]
	private GameObject select_;

	[SerializeField]
	private Curve curve_ = new Curve();

	[SerializeField]
	private AssetBundleSprite place_window_;

	[SerializeField]
	private AnimationSprite place_noise_;

	[SerializeField]
	private AssetBundleSprite mask_;

	[SerializeField]
	private selectPlateCtrl.EnterCurve enter_curve_;

	[SerializeField]
	private selectPlateCtrl.EnableCurve enable_curve_;

	private IEnumerator enumerator_play_;

	private IEnumerator enumerator_open_;

	private IEnumerator enumerator_close_;

	private int cursor_num_;

	private int cursor_no_;

	private int bk_cursor_no_;

	private bool is_play_;

	private bool is_cancel_;

	private bool select_animation_playing_;

	private float mask_alpha_ = 0.5f;

	private int firest_touch_item_index_;

	public static moveCtrl instance
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

	public bool is_cancel
	{
		get
		{
			return is_cancel_;
		}
	}

	public bool select_animation_playing
	{
		get
		{
			return select_animation_playing_;
		}
	}

	public List<SelectPlate> select_list
	{
		get
		{
			return select_list_;
		}
	}

	public int cursor_no
	{
		get
		{
			return cursor_no_;
		}
	}

	public int bk_cursor_no
	{
		get
		{
			return bk_cursor_no_;
		}
	}

	private SelectPlate select_plate
	{
		get
		{
			return select_list[cursor_no_];
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
		string text = ((GSStatic.global_work_.language != 0) ? "u" : string.Empty);
		cursor_.load("/menu/common/", "select_window" + text);
		cursor_.spriteNo(0);
		place_window_.load("/menu/common/", "noise_window");
		AssetBundle assetBundle = AssetBundleCtrl.instance.load("/menu/common/", "snow_noise");
		place_noise_.sprite_data_.Clear();
		place_noise_.sprite_data_.AddRange(assetBundle.LoadAllAssets<Sprite>());
		place_noise_.active = false;
		firest_touch_item_index_ = 0;
		foreach (var select in select_list_.Select((SelectPlate item, int index) => new { item, index }))
		{
			select.item.sprite_.load("/menu/common/", "select_window" + text);
			select.item.sprite_.spriteNo(2);
			select.item.enable_.sprite = select.item.sprite_.sprite_data_[5];
			Color color = select.item.enable_.color;
			select.item.enable_.color = new Color(color.r, color.g, color.b, 0f);
			select.item.touch_.argument_parameter = select.index;
			select.item.touch_.touch_event = delegate(TouchParameter p)
			{
				cursor_no_ = (int)p.argument_parameter;
				cursor_.transform.localPosition = new Vector3(0f, 0f - (float)cursor_no_ * 90f, 0f);
				select.item.touch_.touch_key_type = ((cursor_no_ == firest_touch_item_index_) ? KeyType.A : KeyType.None);
				if (select.item.touch_.touch_key_type == KeyType.None)
				{
					soundCtrl.instance.PlaySE(42);
				}
				firest_touch_item_index_ = cursor_no_;
				move_set_thumbnail_image();
			};
			Vector2 sizeDelta = select.item.text_.rectTransform.sizeDelta;
			Vector3 localPosition = select.item.text_.transform.localPosition;
			int fontSize;
			if (GSStatic.global_work_.language == Language.JAPAN)
			{
				fontSize = 46;
				sizeDelta.x = 640f;
				localPosition.y = 0f;
				select.item.touch_.SetColliderSize(new Vector2(720f, 60f));
			}
			else
			{
				fontSize = 37;
				sizeDelta.x = 800f;
				localPosition.y = 3f;
				select.item.touch_.SetColliderSize(new Vector2(select.item.sprite_.sprite_renderer_.sprite.rect.size.x, 60f));
			}
			select.item.text_.fontSize = fontSize;
			select.item.text_.rectTransform.sizeDelta = sizeDelta;
			select.item.text_.transform.localPosition = localPosition;
			select.item.active = false;
		}
		mask_.load("/menu/common/", "mask");
		mask_.sprite_renderer_.color = Color.clear;
	}

	public void init()
	{
		load();
		firest_touch_item_index_ = 0;
	}

	public void end()
	{
		stop();
		open_stop();
		close_stop();
		active = false;
		is_play_ = false;
		select_animation_playing_ = false;
	}

	public void setting(int in_num, int in_cursor_no)
	{
		cursor_num_ = in_num;
		cursor_no_ = in_cursor_no;
		if (cursor_no_ >= cursor_num_)
		{
			cursor_no_ = 0;
		}
		firest_touch_item_index_ = 0;
		if (cursor_no_ != 0)
		{
			firest_touch_item_index_ = cursor_no_;
		}
		ActiveMoveTouch();
		keyGuideCtrl.instance.ActiveKeyTouch();
		place_.place_.transform.localScale = new Vector3(1f, 1f, 1f);
		move_set_thumbnail_image();
		foreach (SelectPlate item in select_list_)
		{
			item.active = false;
		}
		uint[] array = new uint[4];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = GSStatic.global_work_.Map_data[GSStatic.global_work_.Room][i + 4];
		}
		for (int j = 0; j < cursor_num_; j++)
		{
			if (array[j] == 255)
			{
				select_list_[j].active = false;
				select_list_[j].text = string.Empty;
			}
			else
			{
				select_list_[j].active = true;
				select_list_[j].text = advCtrl.instance.ba_data_.GetText((ushort)array[j]);
			}
		}
		select_.transform.localPosition = new Vector3(select_.transform.localPosition.x, (float)(cursor_num_ - 1) * 90f * 0.5f, 0f);
		cursor_.transform.localPosition = new Vector3(0f, 0f - (float)cursor_no_ * 90f, 0f);
	}

	private void ActiveMoveTouch()
	{
		foreach (SelectPlate item in select_list_)
		{
			item.touch_.ActiveCollider();
		}
	}

	private void InActiveMoveTouch()
	{
		foreach (SelectPlate item in select_list_)
		{
			item.touch_.SetEnableCollider(false);
		}
	}

	private IEnumerator CoroutineOpen()
	{
		float time = 0f;
		float select_plate_time = 0f;
		while (time < 1f || select_plate_time < 1f)
		{
			time += curve_.speed_open_;
			select_plate_time += 0.1f;
			if (time > 1f)
			{
				time = 1f;
			}
			if (select_plate_time > 1f)
			{
				select_plate_time = 1f;
			}
			float num = enter_curve_.scale_.Evaluate(select_plate_time);
			float a = enter_curve_.alpha_.Evaluate(select_plate_time);
			Color white = Color.white;
			Color color = cursor_.sprite_renderer_.color;
			cursor_.sprite_renderer_.color = new Color(color.r, color.g, color.g, a);
			cursor_.transform.localScale = new Vector3(num, num, 1f);
			foreach (SelectPlate item in select_list_)
			{
				color = item.sprite_.color;
				item.sprite_.color = new Color(color.r, color.g, color.g, a);
				item.select_.transform.localScale = new Vector3(num, num, 1f);
				color = item.text_.color;
				item.text_.color = new Color(color.r, color.g, color.g, a);
			}
			float num2 = curve_.open_.Evaluate(time);
			place_.transform.localScale = new Vector3(1f, num2, 1f);
			mask_.sprite_renderer_.color = new Color(0f, 0f, 0f, (!(num2 < mask_alpha_)) ? mask_alpha_ : num2);
			yield return null;
		}
		enumerator_open_ = null;
	}

	private IEnumerator CoroutineClose()
	{
		float time = 0f;
		float select_plate_time = 0f;
		while (time < 1f || select_plate_time < 1f)
		{
			time += curve_.speed_close_;
			select_plate_time += 0.1f;
			if (time > 1f)
			{
				time = 1f;
			}
			if (select_plate_time > 1f)
			{
				select_plate_time = 1f;
			}
			float a = enable_curve_.alpha_.Evaluate(select_plate_time);
			Color color = cursor_.sprite_renderer_.color;
			cursor_.sprite_renderer_.color = new Color(color.r, color.g, color.g, a);
			foreach (SelectPlate item in select_list_)
			{
				color = item.sprite_.color;
				item.sprite_.color = new Color(color.r, color.g, color.g, a);
				color = item.text_.color;
				item.text_.color = new Color(color.r, color.g, color.g, a);
			}
			if (!is_cancel_)
			{
				float num = enable_curve_.cursor_.Evaluate(select_plate_time);
				cursor_.transform.localScale = new Vector3(num, num, 1f);
				float num2 = enable_curve_.select_.Evaluate(select_plate_time);
				select_plate.transform.localScale = new Vector3(num2, num2, 1f);
				float a2 = enable_curve_.enable_.Evaluate(select_plate_time);
				Color color2 = select_plate.enable_.color;
				select_plate.enable_.color = new Color(color2.r, color2.g, color2.g, a2);
			}
			float num3 = curve_.close_.Evaluate(time);
			place_.transform.localScale = new Vector3(1f, num3, 1f);
			mask_.sprite_renderer_.color = new Color(0f, 0f, 0f, (!(num3 < mask_alpha_)) ? mask_alpha_ : num3);
			yield return null;
		}
		enumerator_close_ = null;
	}

	private IEnumerator CoroutinePlay(int in_num, int in_cursor_no)
	{
		setting(in_num, in_cursor_no);
		active = true;
		is_play_ = true;
		is_cancel_ = false;
		select_animation_playing_ = false;
		keyGuideCtrl.instance.open(keyGuideBase.Type.MOVE);
		yield return open();
		float key_wait = systemCtrl.instance.key_wait;
		bool is_move_mode = false;
		while (true)
		{
			if (GSStatic.global_work_.r.no_0 != 8 && GSStatic.global_work_.r.no_0 != 17)
			{
				if (!is_move_mode)
				{
					ActiveMoveTouch();
					keyGuideCtrl.instance.ActiveKeyTouch();
					is_move_mode = true;
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
						break;
					}
					if (padCtrl.instance.GetKeyDown(KeyType.B))
					{
						soundCtrl.instance.PlaySE(44);
						is_cancel_ = true;
						break;
					}
				}
				bool flag = false;
				if (padCtrl.instance.IsNextMove())
				{
					if ((padCtrl.instance.GetKeyDown(KeyType.Up) || padCtrl.instance.GetKeyDown(KeyType.StickL_Up) || padCtrl.instance.GetWheelMoveUp()) && cursor_num_ != 1)
					{
						soundCtrl.instance.PlaySE(42);
						flag = true;
						cursor_no_--;
					}
					if ((padCtrl.instance.GetKeyDown(KeyType.Down) || padCtrl.instance.GetKeyDown(KeyType.StickL_Down) || padCtrl.instance.GetWheelMoveDown()) && cursor_num_ != 1)
					{
						soundCtrl.instance.PlaySE(42);
						flag = true;
						cursor_no_++;
					}
				}
				padCtrl.instance.WheelMoveValUpdate();
				if (flag)
				{
					cursor_no_ = ((cursor_no_ < cursor_num_) ? cursor_no_ : 0);
					cursor_no_ = ((cursor_no_ < 0) ? (cursor_num_ - 1) : cursor_no_);
					firest_touch_item_index_ = cursor_no_;
					cursor_.transform.localPosition = new Vector3(0f, 0f - (float)cursor_no_ * 90f, 0f);
					move_set_thumbnail_image();
				}
			}
			else
			{
				is_move_mode = false;
			}
			yield return null;
		}
		InActiveMoveTouch();
		select_animation_playing_ = true;
		keyGuideCtrl.instance.close();
		yield return close();
		place_.end();
		active = false;
		is_play_ = false;
		select_animation_playing_ = false;
	}

	public void play(int in_num, int in_cursor_no)
	{
		stop();
		enumerator_play_ = CoroutinePlay(in_num, in_cursor_no);
		StartCoroutine(enumerator_play_);
	}

	public void stop()
	{
		if (enumerator_play_ != null)
		{
			StopCoroutine(enumerator_play_);
			enumerator_play_ = null;
		}
		active = false;
	}

	public Coroutine open()
	{
		if (enumerator_open_ != null)
		{
			StopCoroutine(enumerator_open_);
			enumerator_open_ = null;
		}
		enumerator_open_ = CoroutineOpen();
		return StartCoroutine(enumerator_open_);
	}

	public void open_stop()
	{
		if (enumerator_open_ != null)
		{
			StopCoroutine(enumerator_open_);
			enumerator_open_ = null;
		}
	}

	public Coroutine close()
	{
		if (enumerator_close_ != null)
		{
			StopCoroutine(enumerator_close_);
			enumerator_close_ = null;
		}
		enumerator_close_ = CoroutineClose();
		return StartCoroutine(enumerator_close_);
	}

	public void close_stop()
	{
		if (enumerator_close_ != null)
		{
			StopCoroutine(enumerator_close_);
			enumerator_close_ = null;
		}
	}

	public static uint get_area_image(uint area_id)
	{
		switch (GSStatic.global_work_.title)
		{
		case TitleId.GS1:
			return GS1_get_area_image(area_id);
		case TitleId.GS2:
			return GS2_get_area_image(area_id);
		case TitleId.GS3:
			return GS3_get_area_image(area_id);
		default:
			return 255u;
		}
	}

	private static uint GS1_get_area_image(uint area_id)
	{
		uint num = 0u;
		switch (area_id)
		{
		case 0u:
			GSStatic.global_work_.sw_move_flag[area_id] = 1;
			if (GSFlag.Check(0u, scenario.SCE0123_FLAG_SCE1_OP_END) || (uint)GSStatic.global_work_.scenario > 1u)
			{
				return 6u;
			}
			return area_id;
		case 1u:
		case 6u:
			GSStatic.global_work_.sw_move_flag[area_id] = 1;
			return area_id;
		case 3u:
			if ((uint)GSStatic.global_work_.scenario < 3u)
			{
				return area_id;
			}
			return 27u;
		case 17u:
			if ((uint)GSStatic.global_work_.scenario < 11u)
			{
				return area_id;
			}
			return 28u;
		case 22u:
		case 23u:
		case 24u:
		case 25u:
		case 26u:
		case 27u:
			return area_id - 1;
		default:
			return area_id;
		}
	}

	private static uint GS2_get_area_image(uint area_id)
	{
		uint num = 0u;
		switch (area_id)
		{
		case 3u:
			if (GSFlag.Check(0u, scenario_GS2.SCE1_0_KURAIN_SAIKAI))
			{
				GSStatic.global_work_.sw_move_flag[area_id] = 1;
				return 3u;
			}
			return area_id;
		case 5u:
			if (GSFlag.Check(0u, scenario_GS2.SCE1_KEISATSUTOUCHAKU))
			{
				return 24u;
			}
			if (GSStatic.global_work_.scenario == 5)
			{
				return 24u;
			}
			return area_id;
		case 7u:
			if (GSFlag.Check(0u, scenario_GS2.SCE1_0_RYUUCHIJO_MAYOI))
			{
				return 25u;
			}
			if (GSStatic.global_work_.scenario == 5)
			{
				return 25u;
			}
			return area_id;
		case 19u:
			GSStatic.global_work_.sw_move_flag[area_id] = 1;
			return 19u;
		case 21u:
			return 23u;
		case 22u:
			return 21u;
		case 23u:
			return 22u;
		case 24u:
			return 23u;
		case 25u:
			GSStatic.global_work_.sw_move_flag[area_id] = 1;
			return 22u;
		default:
			return area_id;
		}
	}

	private static uint GS3_get_area_image(uint area_id)
	{
		uint result = 0u;
		switch (area_id)
		{
		case 3u:
			result = ((GSStatic.global_work_.scenario != 2) ? 22u : ((!GSFlag.Check(0u, scenario_GS3.SCE1_0_KIRIO_DINNER)) ? area_id : ((!GSFlag.Check(0u, scenario_GS3.SCE1_0_TANTEI_TOUJOU)) ? area_id : 22u)));
			break;
		case 14u:
			if (GSStatic.global_work_.scenario == 14)
			{
				result = 24u;
				GSStatic.global_work_.sw_move_flag[area_id] = 1;
				break;
			}
			result = area_id;
			if (GSStatic.global_work_.scenario == 15 && !GSFlag.Check(0u, scenario_GS3.SCE4_0_1_MASISU_GEKIDO))
			{
				result = 24u;
			}
			break;
		case 15u:
			result = ((GSStatic.global_work_.scenario != 14) ? ((GSStatic.global_work_.scenario != 19) ? 25u : ((!GSFlag.Check(0u, scenario_GS3.SCE4_2_1_GET_CANE)) ? 25u : 15u)) : area_id);
			break;
		case 17u:
			if (GSStatic.global_work_.scenario == 14)
			{
				result = area_id;
			}
			else if (GSStatic.global_work_.scenario == 15)
			{
				result = ((!GSFlag.Check(0u, scenario_GS3.SCE4_0_1_ITONOKO_KANRUI)) ? 34u : 27u);
			}
			else if (GSStatic.global_work_.scenario == 18)
			{
				result = ((!GSFlag.Check(0u, scenario_GS3.SCE4_2_0_OBORO_FUKKATU)) ? 27u : 28u);
			}
			else if (GSStatic.global_work_.scenario == 19)
			{
				result = 28u;
			}
			break;
		case 18u:
			if (GSStatic.global_work_.Room == 19 || GSStatic.global_work_.Room == 20)
			{
				result = 35u;
				if (GSStatic.global_work_.scenario == 14 && GSFlag.Check(0u, scenario_GS3.SCE4_0_SYUUKEN_INAI))
				{
					GSStatic.global_work_.sw_move_flag[area_id] = 1;
				}
				break;
			}
			result = 29u;
			if (GSStatic.global_work_.scenario == 14)
			{
				if (GSFlag.Check(0u, scenario_GS3.SCE4_0_SYUUKEN_INAI))
				{
					GSStatic.global_work_.sw_move_flag[area_id] = 1;
					GSStatic.global_work_.sw_move_flag[18] = 1;
				}
			}
			else if (GSStatic.global_work_.scenario == 18)
			{
				if (GSFlag.Check(0u, scenario_GS3.SCE4_2_0_HARUMI_ZIBOUZIKI))
				{
					result = 18u;
				}
			}
			else if (GSStatic.global_work_.scenario == 19)
			{
				result = 18u;
			}
			break;
		case 19u:
			result = ((GSStatic.global_work_.scenario != 14) ? ((GSStatic.global_work_.scenario != 18) ? 32u : (GSFlag.Check(0u, scenario_GS3.SCE4_2_0_DOUJNAI_INAI) ? 31u : area_id)) : area_id);
			break;
		case 20u:
			if (GSStatic.global_work_.scenario == 14)
			{
				result = area_id;
				break;
			}
			result = 33u;
			if (GSStatic.global_work_.scenario == 18 && GSFlag.Check(0u, scenario_GS3.SCE4_2_0_ITONOKO_ROUBAI))
			{
				GSStatic.global_work_.sw_move_flag[20] = 1;
			}
			break;
		default:
			result = area_id;
			break;
		}
		return result;
	}

	public void move_set_thumbnail_image()
	{
		place_.end();
		uint num = GSStatic.global_work_.Map_data[GSStatic.global_work_.Room][cursor_no_ + 4];
		uint num2 = get_area_image(GSStatic.global_work_.Map_data[GSStatic.global_work_.Room][cursor_no_ + 4]);
		string in_name = ThumbFileNames[(int)GSStatic.global_work_.title, (int)GSStatic.global_work_.language][num2];
		place_.load("/GS" + (int)(GSStatic.global_work_.title + 1) + "/thumb/", in_name);
		place_.spriteNo(0);
		if (GSStatic.global_work_.title == TitleId.GS1 && num >= 21)
		{
			num--;
		}
		bool flag = GSStatic.global_work_.sw_move_flag[num] != 1;
		place_noise_.active = flag;
	}

	public void BackupCursorNo()
	{
		bk_cursor_no_ = cursor_no_;
	}
}
