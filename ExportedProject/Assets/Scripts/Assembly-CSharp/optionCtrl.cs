using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class optionCtrl : MonoBehaviour
{
	public enum OptionType
	{
		TITLE = 0,
		IN_GAME = 1
	}

	public enum CurrentPoint
	{
		NONE = 0,
		QUARTER = 1,
		HALF = 2,
		FULL = 3
	}

	public enum OptionState
	{
		Main = 0,
		SUB = 1
	}

	public enum SkipType
	{
		NO_SKIP = 0,
		WAIT_CURSOR = 1,
		ALL_SKIP = 2
	}

	public enum OptionItem
	{
		SAVE_LOAD = 0,
		BGM = 1,
		SE = 2,
		SKIP = 3,
		SHAKE = 4,
		VIBRATION = 5,
		WINDOW = 6,
		LANGUAGE = 7,
		CREDIT = 8
	}

	public enum OptionItemSub
	{
		WINDOW_SIZE = 0,
		WINDOW_MODE = 1,
		V_SYNC = 2,
		KEY_CONFIG_DECIDE = 3,
		KEY_DECISION_CANCEL = 4,
		KEY_TUKITUKE_YUSABURI = 5,
		KEY_RECORD_OPTION = 6,
		KEY_PERSON_TITLE_RETURN = 7,
		KEY_MOVE = 8,
		KEY_ROT = 9
	}

	private int begin_num_;

	private int end_num_;

	private int current_num_;

	private bool go_title_;

	private bool is_end_;

	private OptionType type;

	private List<optionItem> available_option_ = new List<optionItem>();

	private List<optionItem> available_option_all_ = new List<optionItem>();

	[SerializeField]
	private List<optionKeyConfigButton> key_config_button_ = new List<optionKeyConfigButton>();

	private bool is_default_;

	private IEnumerator coroutine_;

	private TextDataCtrl.OptionTextID[] option_description_ = new TextDataCtrl.OptionTextID[9]
	{
		TextDataCtrl.OptionTextID.COMMENT_SAVE,
		TextDataCtrl.OptionTextID.COMMENT_BGM,
		TextDataCtrl.OptionTextID.COMMENT_SE,
		TextDataCtrl.OptionTextID.COMMENT_SKIP,
		TextDataCtrl.OptionTextID.COMMENT_SHAKE,
		TextDataCtrl.OptionTextID.COMMENT_VIBRATION,
		TextDataCtrl.OptionTextID.COMMENT_TRANSPARENCY,
		TextDataCtrl.OptionTextID.COMMENT_LANGUAGE,
		TextDataCtrl.OptionTextID.COMMENT_CREDITS
	};

	private TextDataCtrl.OptionTextID[] option_sub_description_ = new TextDataCtrl.OptionTextID[8]
	{
		TextDataCtrl.OptionTextID.COMMENT_RESOLUTION,
		TextDataCtrl.OptionTextID.COMMENT_SCREEN,
		TextDataCtrl.OptionTextID.COMMENT_VSYNC,
		TextDataCtrl.OptionTextID.COMMENT_KEY_CONFIG_DECIDE,
		TextDataCtrl.OptionTextID.COMMENT_KEY_CONFIG,
		TextDataCtrl.OptionTextID.COMMENT_KEY_CONFIG_INPUT,
		TextDataCtrl.OptionTextID.COMMENT_KEY_CONFIG_CHECK,
		TextDataCtrl.OptionTextID.COMMENT_KEY_CONFIG_CHECK2
	};

	[SerializeField]
	private GameObject body_;

	[SerializeField]
	private List<optionItem> option_item_ = new List<optionItem>();

	[SerializeField]
	private List<optionItem> option_item_sub_ = new List<optionItem>();

	[SerializeField]
	private List<AssetBundleSprite> line_list_ = new List<AssetBundleSprite>();

	[SerializeField]
	private List<AssetBundleSprite> sub_menu_line_list_ = new List<AssetBundleSprite>();

	[SerializeField]
	private AssetBundleSprite bg_;

	[SerializeField]
	private AssetBundleSprite menu_window_;

	[SerializeField]
	private AssetBundleSprite sub_menu_window_;

	[SerializeField]
	private AssetBundleSprite message_window_;

	[SerializeField]
	private List<Text> title_back_text_ = new List<Text>();

	[SerializeField]
	private List<AssetBundleSprite> page_guid_;

	[SerializeField]
	private float top_pos_;

	[SerializeField]
	private float top_space_;

	[SerializeField]
	private float item_space_;

	private float item_space_sub_ = 54f;

	[SerializeField]
	private optionKeyGuide key_guide_;

	[SerializeField]
	private titleSelectPlate confirmation_select_;

	[SerializeField]
	private AssetBundleSprite mask_;

	[SerializeField]
	private GameObject obj_move_prefab_;

	[SerializeField]
	private Canvas canvas_;

	[SerializeField]
	private arrowCtrl arrow_ctrl_;

	[SerializeField]
	private Transform menu_root_;

	private OptionState state_;

	[SerializeField]
	private textKeyIconCtrl key_icon_;

	[SerializeField]
	private Text icon_text_;

	private bool is_shake_ = true;

	private SkipType skip_type_ = SkipType.ALL_SKIP;

	private GameObject obj_move_;

	private IEnumerator enumerator_credit_;

	private int loop_se_ = 268435455;

	public static optionCtrl instance { get; private set; }

	public bool change_language { get; set; }

	public bool is_open { get; private set; }

	public optionKeyGuide key_guide
	{
		get
		{
			return key_guide_;
		}
	}

	public OptionState state
	{
		get
		{
			return state_;
		}
	}

	public AssetBundleSprite message_window
	{
		get
		{
			return message_window_;
		}
	}

	public bool is_shake
	{
		get
		{
			return is_shake_;
		}
		set
		{
			is_shake_ = value;
		}
	}

	public SkipType skip_type
	{
		get
		{
			return skip_type_;
		}
		set
		{
			skip_type_ = value;
		}
	}

	private void Awake()
	{
		instance = this;
		is_shake_ = true;
	}

	private void Init()
	{
		is_open = true;
		body_.SetActive(true);
		state_ = OptionState.Main;
		menu_root_.localPosition = new Vector3(0f, 0f, 0f);
		bg_.load("/GS1/BG/", "bg043");
		menu_window_.load("/menu/option/", "option_window");
		sub_menu_window_.load("/menu/option/", "option_window");
		message_window_.active = true;
		message_window_.load("/menu/common/", "talk_bg");
		confirmation_select_.body_active = true;
		confirmation_select_.mainTitleInit(new string[2]
		{
			TextDataCtrl.GetText(TextDataCtrl.TitleTextID.YES),
			TextDataCtrl.GetText(TextDataCtrl.TitleTextID.NO)
		}, "select_button", 1);
		confirmation_select_.body_active = false;
		mask_.active = true;
		mask_.load("/menu/common/", "mask");
		mask_.active = false;
		foreach (optionItem item in option_item_)
		{
			item.active = false;
		}
		foreach (AssetBundleSprite item2 in line_list_)
		{
			item2.active = true;
			item2.load("/menu/option/", "option_hr");
			item2.active = false;
		}
		foreach (Text item3 in title_back_text_)
		{
			mainCtrl.instance.addText(item3);
		}
		if (type == OptionType.TITLE)
		{
			arrow_ctrl_.init();
			arrow_ctrl_.load();
			arrow_ctrl_.SetTouchKeyType(KeyType.L, 0);
			arrow_ctrl_.SetTouchKeyType(KeyType.L, 1);
		}
		foreach (AssetBundleSprite item4 in sub_menu_line_list_)
		{
			item4.active = true;
			item4.load("/menu/option/", "option_hr");
			item4.active = false;
		}
		key_guide_.init();
	}

	public void Open(OptionType in_type)
	{
		TouchSystem.TouchInActive();
		type = in_type;
		Init();
		loop_se_ = soundCtrl.instance.se_no[0];
		if (loop_se_ != 268435455)
		{
			soundCtrl.instance.StopSE(loop_se_);
			soundCtrl.instance.SetLoopSEID(loop_se_);
		}
		if (AnimationSystem.Instance != null)
		{
			AnimationSystem.Instance.pause = true;
		}
		key_guide_.ActiveTouch();
		if (type == OptionType.TITLE)
		{
			begin_num_ = 1;
			end_num_ = 8;
			key_guide_.guideIconSet(false, guideCtrl.GuideType.OPTION_TITLE);
			arrow_ctrl_.arrow(true, 0);
			arrow_ctrl_.arrow(false, 1);
			AssetBundle assetBundle = AssetBundleCtrl.instance.load("/menu/common/", "court_record_02");
			foreach (AssetBundleSprite item3 in page_guid_)
			{
				item3.sprite_renderer_.enabled = true;
				item3.sprite_data_.Clear();
				item3.sprite_data_.AddRange(assetBundle.LoadAllAssets<Sprite>());
				item3.spriteNo(0);
			}
			page_guid_[1].spriteNo(1);
		}
		else
		{
			begin_num_ = 0;
			end_num_ = 6;
			key_guide_.guideIconSet(false, guideCtrl.GuideType.OPTION_INGAME);
			arrow_ctrl_.arrowAll(false);
			foreach (AssetBundleSprite item4 in page_guid_)
			{
				item4.sprite_renderer_.enabled = false;
			}
		}
		available_option_all_.Clear();
		available_option_.Clear();
		for (int j = begin_num_; j <= end_num_; j++)
		{
			option_item_[j].active = true;
			option_item_[j].Init();
			SetText((OptionItem)j);
			available_option_.Add(option_item_[j]);
			available_option_all_.Add(option_item_[j]);
		}
		if (type == OptionType.TITLE)
		{
			for (int k = 0; k < option_item_sub_.Count; k++)
			{
				option_item_sub_[k].active = true;
				option_item_sub_[k].Init();
				available_option_all_.Add(option_item_sub_[k]);
			}
		}
		int num = begin_num_ - 1;
		num = ((num >= 0) ? num : 0);
		foreach (optionItem item2 in available_option_)
		{
			foreach (var item5 in item2.touch_list.Select((InputTouch v, int i) => new { v, i }))
			{
				item5.v.argument_parameter = new optionInfo
				{
					type_ = num,
					index_ = item5.i + 1
				};
				item5.v.touch_event = delegate(TouchParameter p)
				{
					optionInfo optionInfo4 = p.argument_parameter as optionInfo;
					current_num_ = optionInfo4.type_;
					SelectItemSet();
					item2.OnTouch(p);
					if ((type != OptionType.IN_GAME || optionInfo4.type_ != 0) && (type != 0 || optionInfo4.type_ != 7))
					{
						soundCtrl.instance.PlaySE(42);
					}
				};
			}
			num++;
		}
		num = 0;
		if (type == OptionType.TITLE)
		{
			foreach (optionItem item in option_item_sub_)
			{
				foreach (var item6 in item.touch_list.Select((InputTouch v, int i) => new { v, i }))
				{
					item6.v.argument_parameter = new optionInfo
					{
						type_ = num,
						index_ = item6.i + 1
					};
					item6.v.touch_event = delegate(TouchParameter p)
					{
						optionInfo optionInfo3 = p.argument_parameter as optionInfo;
						current_num_ = optionInfo3.type_;
						SelectItemSet();
						item.OnTouch(p);
						if (optionInfo3.type_ != 3)
						{
							soundCtrl.instance.PlaySE(42);
						}
					};
				}
				if (item is optionKeyConfig)
				{
					optionKeyConfig config_item = item as optionKeyConfig;
					int num2 = 0;
					foreach (optionKeyConfigButton item7 in config_item.button_list_)
					{
						int index = num2;
						foreach (var item8 in item7.touch_list.Select((InputTouch v, int i) => new { v, i }))
						{
							item8.v.argument_parameter = new optionInfo
							{
								type_ = num,
								index_ = item8.i + 1
							};
							item8.v.touch_event = delegate(TouchParameter p)
							{
								optionInfo optionInfo2 = p.argument_parameter as optionInfo;
								config_item.list_count_ = index;
								current_num_ = optionInfo2.type_;
								SelectItemSet(config_item.GetCurrentPoint());
								item.OnTouch(p);
							};
						}
						num2++;
					}
				}
				switch (num)
				{
				case 8:
					item.SetText(TextDataCtrl.GetText(TextDataCtrl.OptionTextID.ITEM_KEY_MOVE));
					break;
				case 9:
					item.SetText(TextDataCtrl.GetText(TextDataCtrl.OptionTextID.ITEM_KEY_ROT));
					break;
				}
				num++;
			}
		}
		ActiveOptionTouch();
		current_num_ = 0;
		SelectItemSet();
		ItemPosSet(type);
		if (coroutine_ != null)
		{
			StopCoroutine(coroutine_);
		}
		coroutine_ = CoroutineOption();
		StartCoroutine(coroutine_);
	}

	private IEnumerator CoroutineSlider()
	{
		arrow_ctrl_.arrowAll(false);
		float speed = 90f;
		float bg_pos_x_3 = menu_root_.localPosition.x;
		current_num_ = 0;
		message_window_.active = false;
		if (state_ == OptionState.Main)
		{
			state_ = OptionState.SUB;
			available_option_.Clear();
			for (int i = 0; i < option_item_sub_.Count; i++)
			{
				available_option_.Add(option_item_sub_[i]);
			}
			page_guid_[0].spriteNo(1);
			page_guid_[1].spriteNo(0);
		}
		else
		{
			state_ = OptionState.Main;
			available_option_.Clear();
			for (int j = begin_num_; j <= end_num_; j++)
			{
				available_option_.Add(option_item_[j]);
			}
			page_guid_[0].spriteNo(0);
			page_guid_[1].spriteNo(1);
		}
		SelectItemSet();
		if (state_ == OptionState.Main)
		{
			while (true)
			{
				bg_pos_x_3 += speed;
				if (bg_pos_x_3 > 0f)
				{
					break;
				}
				menu_root_.localPosition = new Vector3(bg_pos_x_3, 0f, 0f);
				yield return null;
			}
			bg_pos_x_3 = 0f;
			menu_root_.localPosition = new Vector3(0f, 0f, 0f);
			arrow_ctrl_.arrow(true, 0);
		}
		else
		{
			float end_pos = -1920f;
			while (true)
			{
				bg_pos_x_3 -= speed;
				if (bg_pos_x_3 < end_pos)
				{
					break;
				}
				menu_root_.localPosition = new Vector3(bg_pos_x_3, 0f, 0f);
				yield return null;
			}
			bg_pos_x_3 = end_pos;
			menu_root_.localPosition = new Vector3(end_pos, 0f, 0f);
			arrow_ctrl_.arrow(true, 1);
		}
		key_guide_.guideIconSet(false, guideCtrl.GuideType.OPTION_TITLE);
		message_window_.active = true;
	}

	private IEnumerator CoroutineOption()
	{
		go_title_ = false;
		is_end_ = false;
		if (change_language)
		{
			messageBoxCtrl.instance.init();
			messageBoxCtrl.instance.SetWindowSize(new Vector2(1200f, 360f));
			TextDataCtrl.OptionTextID id = TextDataCtrl.OptionTextID.LANGUAGE_SAVE;
			messageBoxCtrl.instance.SetText(TextDataCtrl.GetTexts(id));
			messageBoxCtrl.instance.SetTextPosCenter();
			messageBoxCtrl.instance.OpenWindow();
			InActiveOptionTouch();
			key_guide_.InActiveTouch();
			while (true)
			{
				if (padCtrl.instance.GetKeyDown(KeyType.A))
				{
					messageBoxCtrl.instance.CloseWindow();
				}
				if (!messageBoxCtrl.instance.active)
				{
					break;
				}
				yield return null;
			}
			ActiveOptionTouch();
			key_guide_.ActiveTouch();
		}
		while (true)
		{
			if ((padCtrl.instance.GetKeyDown(KeyType.Up) || padCtrl.instance.GetKeyDown(KeyType.StickL_Up) || padCtrl.instance.GetWheelMoveUp()) && padCtrl.instance.IsNextMove())
			{
				CurrentPoint currentPoint = available_option_[current_num_].GetCurrentPoint();
				if (state_ == OptionState.SUB)
				{
					if (padCtrl.instance.GetWheelMoveUp())
					{
						if (currentPoint == CurrentPoint.HALF || currentPoint == CurrentPoint.QUARTER || currentPoint == CurrentPoint.FULL)
						{
							soundCtrl.instance.PlaySE(42);
							available_option_[current_num_].ChangeValue(-1);
						}
						else if (current_num_ == 0 || current_num_ == 9)
						{
							currentPoint = CurrentPoint.FULL;
							current_num_--;
							soundCtrl.instance.PlaySE(42);
							current_num_ = ((current_num_ >= 0) ? current_num_ : (available_option_.Count - 1));
							SelectItemSet(currentPoint);
						}
						else if (current_num_ >= 4 && current_num_ <= 8)
						{
							current_num_--;
							currentPoint = CurrentPoint.HALF;
							soundCtrl.instance.PlaySE(42);
							current_num_ = ((current_num_ >= 0) ? current_num_ : (available_option_.Count - 1));
							available_option_[current_num_].ChangeValue(-1);
							SelectItemSet(currentPoint);
						}
						else
						{
							current_num_--;
							soundCtrl.instance.PlaySE(42);
							current_num_ = ((current_num_ >= 0) ? current_num_ : (available_option_.Count - 1));
							SelectItemSet(currentPoint);
						}
					}
					else
					{
						current_num_--;
						soundCtrl.instance.PlaySE(42);
						current_num_ = ((current_num_ >= 0) ? current_num_ : (available_option_.Count - 1));
						SelectItemSet(currentPoint);
					}
				}
				else
				{
					current_num_--;
					soundCtrl.instance.PlaySE(42);
					current_num_ = ((current_num_ >= 0) ? current_num_ : (available_option_.Count - 1));
					SelectItemSet(currentPoint);
				}
			}
			else if (padCtrl.instance.GetKeyDown(KeyType.Down) || padCtrl.instance.GetKeyDown(KeyType.StickL_Down) || (padCtrl.instance.GetWheelMoveDown() && padCtrl.instance.IsNextMove()))
			{
				CurrentPoint currentPoint2 = available_option_[current_num_].GetCurrentPoint();
				if (state_ == OptionState.SUB)
				{
					if (padCtrl.instance.GetWheelMoveDown())
					{
						switch (currentPoint2)
						{
						case CurrentPoint.HALF:
							if (current_num_ < 8)
							{
								available_option_[current_num_].ChangeValue(-1);
								currentPoint2 = CurrentPoint.NONE;
								current_num_++;
								soundCtrl.instance.PlaySE(42);
								current_num_ = ((current_num_ < available_option_.Count) ? current_num_ : 0);
								SelectItemSet(currentPoint2);
							}
							else
							{
								soundCtrl.instance.PlaySE(42);
								available_option_[current_num_].ChangeValue(1);
							}
							break;
						case CurrentPoint.QUARTER:
							soundCtrl.instance.PlaySE(42);
							available_option_[current_num_].ChangeValue(1);
							break;
						case CurrentPoint.FULL:
							currentPoint2 = CurrentPoint.NONE;
							available_option_[current_num_].ChangeValue(1);
							current_num_++;
							soundCtrl.instance.PlaySE(42);
							current_num_ = ((current_num_ < available_option_.Count) ? current_num_ : 0);
							SelectItemSet(currentPoint2);
							break;
						default:
							if (current_num_ < 4)
							{
								current_num_++;
								soundCtrl.instance.PlaySE(42);
								current_num_ = ((current_num_ < available_option_.Count) ? current_num_ : 0);
								SelectItemSet(currentPoint2);
							}
							else
							{
								soundCtrl.instance.PlaySE(42);
								available_option_[current_num_].ChangeValue(1);
							}
							break;
						}
					}
					else
					{
						current_num_++;
						soundCtrl.instance.PlaySE(42);
						current_num_ = ((current_num_ < available_option_.Count) ? current_num_ : 0);
						SelectItemSet(currentPoint2);
					}
				}
				else
				{
					current_num_++;
					soundCtrl.instance.PlaySE(42);
					current_num_ = ((current_num_ < available_option_.Count) ? current_num_ : 0);
					SelectItemSet(currentPoint2);
				}
			}
			else if (padCtrl.instance.GetKeyDown(KeyType.Right) || padCtrl.instance.GetKeyDown(KeyType.StickL_Right))
			{
				available_option_[current_num_].ChangeValue(1);
			}
			else if (padCtrl.instance.GetKeyDown(KeyType.Left) || padCtrl.instance.GetKeyDown(KeyType.StickL_Left))
			{
				available_option_[current_num_].ChangeValue(-1);
			}
			else if (padCtrl.instance.GetKeyDown(KeyType.A))
			{
				if (available_option_[current_num_].SelectDecision())
				{
					if (state_ == OptionState.Main)
					{
						if (current_num_ + begin_num_ == 7)
						{
							InActiveOptionTouch();
							change_language = true;
							yield return StartCoroutine(CoroutineChengeLanguage());
							yield break;
						}
						if (current_num_ + begin_num_ == 0)
						{
							TouchSystem.TouchInActive();
							soundCtrl.instance.PlaySE(43);
							option_item_[0].PlayDecide();
							yield return new WaitWhile(() => option_item_[0].play_decide);
							body_.SetActive(false);
							while (SaveLoadUICtrl.instance.is_open)
							{
								yield return null;
							}
							ActiveOptionTouch();
							key_guide_.ActiveTouch();
							option_item_[0].InitValueSet();
							body_.SetActive(true);
						}
						else if (current_num_ + begin_num_ == 8)
						{
							soundCtrl.instance.PlaySE(43);
							if (enumerator_credit_ != null)
							{
								StopCoroutine(enumerator_credit_);
								enumerator_credit_ = null;
							}
							enumerator_credit_ = CoroutineCredit();
							yield return StartCoroutine(enumerator_credit_);
						}
					}
					else if (current_num_ != 0 && current_num_ != 1)
					{
						if (current_num_ == 3)
						{
							soundCtrl.instance.PlaySE(43);
							option_item_sub_[3].PlayDecide();
							yield return new WaitWhile(() => option_item_sub_[3].play_decide);
							key_icon_.keyIconActiveSet(false);
							title_back_text_[0].text = TextDataCtrl.GetText(option_sub_description_[6]);
							title_back_text_[1].text = TextDataCtrl.GetText(option_sub_description_[6], 1);
							yield return StartCoroutine(KeyConfigWait());
							option_item_sub_[3].InitValueSet();
							key_guide_.guideIconSet(false, guideCtrl.GuideType.OPTION_TITLE);
						}
						else if (current_num_ <= 9 || 4 <= current_num_)
						{
							if (padCtrl.instance.InputAnyKeyPad())
							{
								soundCtrl.instance.PlaySE(53);
								yield return null;
								continue;
							}
							InActiveOptionTouch();
							soundCtrl.instance.PlaySE(43);
							option_item_sub_[current_num_].PlayDecide();
							key_icon_.keyIconActiveSet(false);
							title_back_text_[0].text = TextDataCtrl.GetText(TextDataCtrl.OptionTextID.COMMENT_KEY_CONFIG_INPUT);
							title_back_text_[1].text = TextDataCtrl.GetText(TextDataCtrl.OptionTextID.COMMENT_KEY_CONFIG_INPUT, 1);
							key_guide_.setEnables(false);
							yield return new WaitWhile(() => option_item_sub_[current_num_].play_decide);
							key_guide_.setEnables(true);
							key_guide_.ReLoadGuid();
							SetTextSub((OptionItemSub)current_num_);
							ActiveOptionTouch();
						}
					}
				}
			}
			else
			{
				if (padCtrl.instance.GetKeyDown(KeyType.B))
				{
					soundCtrl.instance.PlaySE(44);
					InActiveOptionTouch();
					break;
				}
				if (type == OptionType.IN_GAME && padCtrl.instance.GetKeyDown(KeyType.Start, 2, true, KeyType.B))
				{
					soundCtrl.instance.PlaySE(44);
					InActiveOptionTouch();
					break;
				}
				if (padCtrl.instance.GetKeyDown(KeyType.Y))
				{
					if (type == OptionType.IN_GAME)
					{
						soundCtrl.instance.PlaySE(43);
						TouchSystem.TouchInActive();
						yield return StartCoroutine(CoroutineConfirmation());
						if (is_end_)
						{
							go_title_ = true;
							break;
						}
						ActiveOptionTouch();
						key_guide_.ActiveTouch();
					}
				}
				else if (padCtrl.instance.GetKeyDown(KeyType.DEFAULT_RETURN_KEY, 2, true, KeyType.X))
				{
					TouchSystem.TouchInActive();
					soundCtrl.instance.PlaySE(43);
					yield return StartCoroutine(CoroutineDefaultConfirmation());
					if (is_default_)
					{
						GSStatic.option_work.bgm_value = 3;
						GSStatic.option_work.se_value = 3;
						GSStatic.option_work.skip_type = 2;
						GSStatic.option_work.shake_type = 1;
						GSStatic.option_work.vibe_type = 1;
						GSStatic.option_work.window_type = 0;
						foreach (optionItem item in available_option_all_)
						{
							item.DefaultValueSet();
						}
						SelectItemSet();
					}
					if (type == OptionType.TITLE)
					{
						key_guide_.guideIconSet(false, guideCtrl.GuideType.OPTION_TITLE);
					}
					ActiveOptionTouch();
					key_guide_.ActiveTouch();
				}
				else if (padCtrl.instance.GetKeyDown(KeyType.L) && state_ == OptionState.Main)
				{
					if (type == OptionType.TITLE)
					{
						soundCtrl.instance.PlaySE(42);
						yield return StartCoroutine(CoroutineSlider());
					}
				}
				else if (padCtrl.instance.GetKeyDown(KeyType.L) && state_ == OptionState.SUB && type == OptionType.TITLE)
				{
					soundCtrl.instance.PlaySE(42);
					yield return StartCoroutine(CoroutineSlider());
				}
			}
			padCtrl.instance.WheelMoveValUpdate();
			yield return null;
		}
		if (type == OptionType.TITLE)
		{
			yield return StartCoroutine(CoroutineCheckKeyConfig());
		}
		yield return StartCoroutine(CoroutineCheckSave());
		if (go_title_)
		{
			fadeCtrl.instance.play(fadeCtrl.Status.FADE_OUT, 30u, 16u);
			yield return new WaitWhile(() => !fadeCtrl.instance.is_end);
			Close();
			while (is_open)
			{
				yield return null;
			}
			selectPlateCtrl.instance.stopCursor();
			selectPlateCtrl.instance.body_active = false;
			messageBoardCtrl.instance.init();
			GSMain.End();
			for (int i = 0; i < GSStatic.global_work_.sw_move_flag.Length; i++)
			{
				GSStatic.global_work_.sw_move_flag[i] = 0;
			}
			titleCtrlRoot.instance.active = true;
			titleCtrlRoot.instance.Scene(titleCtrlRoot.SceneType.Top);
		}
		else
		{
			if (loop_se_ != 268435455)
			{
				soundCtrl.instance.PlaySE(loop_se_);
			}
			if (AnimationSystem.Instance != null)
			{
				AnimationSystem.Instance.pause = false;
			}
			Close();
		}
	}

	public void ReleaseOverLapKeyCode(KeyCode target, KeyCode change)
	{
		foreach (optionKeyConfigButton item in key_config_button_)
		{
			if (target == item.current_key_code)
			{
				item.ChangeValue((int)change);
			}
		}
	}

	private IEnumerator KeyConfigWait()
	{
		confirmation_select_.body_active = true;
		confirmation_select_.playCursor();
		key_guide_.setEnables(false);
		mask_.active = true;
		while (true)
		{
			if (confirmation_select_.is_end)
			{
				if (confirmation_select_.cursor_no == 0)
				{
					foreach (optionKeyConfigButton item in key_config_button_)
					{
						padCtrl.instance.SetKeyType(item.set_type_, item.current_key_code);
					}
				}
				SetTextSub((OptionItemSub)current_num_);
				break;
			}
			if (!confirmation_select_.is_play_animation && padCtrl.instance.GetKeyDown(KeyType.B))
			{
				soundCtrl.instance.PlaySE(44);
				confirmation_select_.stopCursor();
				confirmation_select_.body_active = false;
				SetTextSub((OptionItemSub)current_num_);
				break;
			}
			yield return null;
		}
		key_guide_.setEnables(true);
		mask_.active = false;
	}

	private IEnumerator CoroutineCheckKeyConfig()
	{
		bool configChange = false;
		foreach (optionKeyConfigButton item in key_config_button_)
		{
			if (!item.configChanged())
			{
				configChange = true;
				break;
			}
		}
		if (configChange)
		{
			yield return null;
			key_icon_.keyIconActiveSet(false);
			title_back_text_[0].text = TextDataCtrl.GetText(option_sub_description_[7]);
			title_back_text_[1].text = TextDataCtrl.GetText(option_sub_description_[7], 1);
			yield return StartCoroutine(KeyConfigWait());
		}
	}

	private IEnumerator CoroutineCheckSave()
	{
		if (ChangeValueCheck())
		{
			GSStatic.option_work.language_type = (ushort)GSStatic.global_work_.language;
			loadingCtrl.instance.play(loadingCtrl.Type.SAVEING);
			SaveControl.SaveSystemDataRequest();
			while (!SaveControl.is_save_)
			{
				yield return null;
			}
			SaveControl.SaveSystemData();
			yield return loadingCtrl.instance.wait();
			loadingCtrl.instance.stop();
			GSStatic.save_slot_language_ = GSStatic.global_work_.language;
			loadingCtrl.instance.init();
			loadingCtrl.instance.play(loadingCtrl.Type.LOADING);
			SaveControl.LoadSystemDataRequest();
			while (!SaveControl.is_load_)
			{
				yield return null;
			}
			SaveControl.LoadSystemData();
			yield return loadingCtrl.instance.wait();
			loadingCtrl.instance.stop();
		}
	}

	private IEnumerator CoroutineChengeLanguage()
	{
		message_window_.active = false;
		GSStatic.global_work_.language = (Language)GSStatic.option_work.language_type;
		obj_move_ = Object.Instantiate(obj_move_prefab_);
		obj_move_.transform.parent = canvas_.transform;
		obj_move_.transform.localPosition = Vector3.zero;
		obj_move_.transform.localScale = Vector3.one;
		Balloon.ChangeLanguageObjection();
		while (objMoveCtrl.instance.is_play)
		{
			yield return null;
		}
		Object.Destroy(obj_move_);
		obj_move_ = null;
		fadeCtrl.instance.play(30, false);
		while (!fadeCtrl.instance.is_end)
		{
			yield return null;
		}
		TextDataCtrl.SetLanguage(GSStatic.global_work_.language);
		List<int> tmp_value = new List<int>();
		List<int> tmp_key = new List<int>();
		foreach (optionItem item in available_option_all_)
		{
			tmp_value.Add(item.old_value_);
		}
		foreach (optionKeyConfigButton item2 in key_config_button_)
		{
			tmp_key.Add(item2.old_value_);
		}
		Open(OptionType.TITLE);
		int value_count2 = 0;
		foreach (optionItem item3 in available_option_all_)
		{
			item3.old_value_ = tmp_value[value_count2++];
		}
		value_count2 = 0;
		foreach (optionKeyConfigButton item4 in key_config_button_)
		{
			item4.old_value_ = tmp_key[value_count2];
		}
		fadeCtrl.instance.play(30, true);
		while (!fadeCtrl.instance.is_end)
		{
			yield return null;
		}
	}

	private IEnumerator CoroutineCredit()
	{
		TouchSystem.TouchInActive();
		option_item_[8].PlayDecide();
		soundCtrl.instance.FadeOutBGM(30);
		fadeCtrl.instance.play(30, false);
		while (!fadeCtrl.instance.is_end)
		{
			yield return null;
		}
		yield return new WaitWhile(() => option_item_[8].play_decide);
		option_item_[8].InitValueSet();
		creditCtrl.instance.Play();
		while (creditCtrl.instance.is_play)
		{
			yield return null;
		}
		soundCtrl.instance.PlayBGM(400, 30);
		fadeCtrl.instance.play(30, true);
		while (!fadeCtrl.instance.is_end)
		{
			yield return null;
		}
		ActiveOptionTouch();
		key_guide_.ActiveTouch();
		enumerator_credit_ = null;
	}

	private void ActiveOptionTouch()
	{
		foreach (optionItem item in available_option_all_)
		{
			foreach (InputTouch item2 in item.touch_list)
			{
				item2.ActiveCollider();
			}
			if (!(item is optionKeyConfig))
			{
				continue;
			}
			foreach (optionKeyConfigButton item3 in (item as optionKeyConfig).button_list_)
			{
				foreach (InputTouch item4 in item3.touch_list)
				{
					item4.ActiveCollider();
				}
			}
		}
		if (type == OptionType.TITLE)
		{
			arrow_ctrl_.ActiveArrow();
		}
	}

	private void InActiveOptionTouch()
	{
		foreach (optionItem item in available_option_all_)
		{
			foreach (InputTouch item2 in item.touch_list)
			{
				item2.SetEnableCollider(false);
			}
			if (!(item is optionKeyConfig))
			{
				continue;
			}
			foreach (optionKeyConfigButton item3 in (item as optionKeyConfig).button_list_)
			{
				foreach (InputTouch item4 in item3.touch_list)
				{
					item4.SetEnableCollider(false);
				}
			}
		}
	}

	private IEnumerator CoroutineDefaultConfirmation()
	{
		confirmation_select_.body_active = true;
		confirmation_select_.playCursor();
		key_guide_.setEnables(false);
		mask_.active = true;
		key_icon_.keyIconActiveSet(false);
		title_back_text_[0].text = TextDataCtrl.GetText(TextDataCtrl.OptionTextID.SET_DEFAULT);
		title_back_text_[1].text = string.Empty;
		is_default_ = false;
		while (true)
		{
			if (confirmation_select_.is_end)
			{
				if (confirmation_select_.cursor_no == 0)
				{
					is_default_ = true;
				}
				else if (state_ == OptionState.Main)
				{
					SetText((OptionItem)(current_num_ + begin_num_));
				}
				else
				{
					SetTextSub((OptionItemSub)current_num_);
				}
				break;
			}
			if (!confirmation_select_.is_play_animation && padCtrl.instance.GetKeyDown(KeyType.B))
			{
				soundCtrl.instance.PlaySE(44);
				confirmation_select_.stopCursor();
				confirmation_select_.body_active = false;
				if (state_ == OptionState.Main)
				{
					SetText((OptionItem)(current_num_ + begin_num_));
				}
				else
				{
					SetTextSub((OptionItemSub)current_num_);
				}
				break;
			}
			yield return null;
		}
		key_guide_.setEnables(true);
		key_guide_.ReLoadGuid();
		mask_.active = false;
	}

	private IEnumerator CoroutineConfirmation()
	{
		confirmation_select_.body_active = true;
		confirmation_select_.playCursor();
		key_icon_.keyIconActiveSet(false);
		key_guide_.setEnables(false);
		mask_.active = true;
		if (current_num_ + begin_num_ == 7)
		{
			title_back_text_[0].text = TextDataCtrl.GetText(TextDataCtrl.OptionTextID.LANGUAGE_CHANGE);
			title_back_text_[1].text = TextDataCtrl.GetText(TextDataCtrl.OptionTextID.LANGUAGE_CHANGE, 1);
		}
		else
		{
			title_back_text_[0].text = TextDataCtrl.GetText(TextDataCtrl.OptionTextID.GO_TITLE);
			title_back_text_[1].text = TextDataCtrl.GetText(TextDataCtrl.OptionTextID.GO_TITLE, 1);
		}
		while (true)
		{
			if (confirmation_select_.is_end)
			{
				if (confirmation_select_.cursor_no == 0)
				{
					is_end_ = true;
					yield break;
				}
				is_end_ = false;
				mask_.active = false;
				key_guide_.setEnables(true);
				key_guide_.ReLoadGuid();
				SetText((OptionItem)(current_num_ + begin_num_));
				yield break;
			}
			if (!confirmation_select_.is_play_animation && padCtrl.instance.GetKeyDown(KeyType.B))
			{
				break;
			}
			yield return null;
		}
		soundCtrl.instance.PlaySE(44);
		confirmation_select_.stopCursor();
		confirmation_select_.body_active = false;
		is_end_ = false;
		mask_.active = false;
		key_guide_.setEnables(true);
		key_guide_.ReLoadGuid();
		SetText((OptionItem)(current_num_ + begin_num_));
	}

	private void SelectItemSet(CurrentPoint point = CurrentPoint.NONE)
	{
		for (int i = 0; i < available_option_.Count; i++)
		{
			if (current_num_ == i)
			{
				if (point != 0)
				{
					available_option_[i].SetCurrentPoint(point);
				}
				available_option_[i].ChangeItemBg(1);
				available_option_[i].SelectEntry();
			}
			else
			{
				available_option_[i].ChangeItemBg(0);
				available_option_[i].SelectExit();
			}
		}
		if (state_ == OptionState.Main)
		{
			SetText((OptionItem)(current_num_ + begin_num_));
		}
		else
		{
			SetTextSub((OptionItemSub)current_num_);
		}
	}

	private void SetText(OptionItem item_type)
	{
		key_icon_.keyIconActiveSet(false);
		title_back_text_[0].text = TextDataCtrl.GetText(option_description_[(int)item_type]);
		title_back_text_[1].text = TextDataCtrl.GetText(option_description_[(int)item_type], 1);
		keyIconSet(item_type);
	}

	private void SetTextSub(OptionItemSub item_type)
	{
		key_icon_.keyIconActiveSet(false);
		if (item_type <= OptionItemSub.KEY_CONFIG_DECIDE)
		{
			title_back_text_[0].text = TextDataCtrl.GetText(option_sub_description_[(int)item_type]);
			title_back_text_[1].text = TextDataCtrl.GetText(option_sub_description_[(int)item_type], 1);
		}
		else if (item_type >= OptionItemSub.KEY_DECISION_CANCEL && item_type <= OptionItemSub.KEY_ROT)
		{
			title_back_text_[0].text = TextDataCtrl.GetText(option_sub_description_[4]);
			title_back_text_[1].text = TextDataCtrl.GetText(option_sub_description_[4], 1);
		}
		keyIconSet(OptionItem.SAVE_LOAD, true);
	}

	private void ItemPosSet(OptionType type)
	{
		float num = top_pos_;
		if (type == OptionType.IN_GAME)
		{
			num -= top_space_;
		}
		int num2 = 0;
		foreach (optionItem item in available_option_)
		{
			if (num2 < available_option_.Count - 1)
			{
				line_list_[num2].active = true;
				line_list_[num2].transform.localPosition = new Vector3(0f, num - item_space_ / 2f, 0f);
				num2++;
			}
			item.transform.localPosition = new Vector3(0f, num, item.transform.localPosition.z);
			num -= item_space_;
		}
		num = top_pos_;
		if (type != 0)
		{
			return;
		}
		for (int i = 0; i < 4; i++)
		{
			option_item_sub_[i].transform.localPosition = new Vector3(0f, num, option_item_sub_[i].transform.localPosition.z);
			if (i < 3)
			{
				sub_menu_line_list_[i].active = true;
				sub_menu_line_list_[i].transform.localPosition = new Vector3(0f, num - item_space_ / 2f, 0f);
			}
			num -= item_space_;
		}
		num -= 5f;
		for (int j = 4; j < option_item_sub_.Count; j++)
		{
			option_item_sub_[j].transform.localPosition = new Vector3(0f, num, option_item_sub_[j].transform.localPosition.z);
			num -= item_space_sub_;
		}
	}

	public void OptionCoroutineStop()
	{
		if (coroutine_ != null)
		{
			StopCoroutine(coroutine_);
			coroutine_ = null;
		}
		Close();
	}

	private void Close()
	{
		loop_se_ = 268435455;
		foreach (optionItem item in option_item_)
		{
			item.Close();
		}
		foreach (optionItem item2 in option_item_sub_)
		{
			item2.Close();
		}
		if (enumerator_credit_ != null)
		{
			StopCoroutine(enumerator_credit_);
			enumerator_credit_ = null;
			if (creditCtrl.instance.is_play)
			{
				creditCtrl.instance.Stop();
			}
		}
		message_window_.active = false;
		body_.SetActive(false);
		key_guide_.Close();
		if (type == OptionType.TITLE)
		{
			is_open = false;
		}
		else
		{
			StartCoroutine(CloseWait());
		}
		if (selectPlateCtrl.instance != null && selectPlateCtrl.instance.body_active && messageBoardCtrl.instance != null)
		{
			messageBoardCtrl.instance.InActiveNormalMessageNextTouch();
		}
		systemCtrl.instance.EnableDoubleQuotationAdjustoment(GSStatic.save_slot_language_ == Language.JAPAN);
		foreach (Text item3 in title_back_text_)
		{
			mainCtrl.instance.removeText(item3);
		}
		key_icon_.keyIconActiveSet(false);
	}

	private IEnumerator CloseWait()
	{
		int timer = 0;
		while (timer < 10 && !padCtrl.instance.GetKeyUp(KeyType.B))
		{
			timer++;
			yield return null;
		}
		is_open = false;
	}

	public void OptionSet()
	{
		change_language = false;
		foreach (optionItem item in option_item_)
		{
			item.InitValueSet();
		}
		foreach (optionItem item2 in option_item_sub_)
		{
			item2.InitValueSet();
		}
	}

	private bool ChangeValueCheck()
	{
		foreach (optionItem item in available_option_all_)
		{
			if (item.ConfirmChange())
			{
				return true;
			}
		}
		return false;
	}

	public void keyIconSet(OptionItem optoin_type, bool sub_option = false)
	{
		for (int i = 0; i < title_back_text_.Count; i++)
		{
			if (title_back_text_[i].text.IndexOf("【】") < 0)
			{
				continue;
			}
			string text = title_back_text_[i].text;
			title_back_text_[i].text = text.Replace("【】", "\u3000\u3000\u3000");
			string text2 = title_back_text_[i].text;
			float preferredWidth = title_back_text_[i].preferredWidth;
			icon_text_.text = text.Remove(text.IndexOf("【"));
			float preferredWidth2 = icon_text_.preferredWidth;
			icon_text_.text += "\u3000\u3000\u3000";
			float preferredWidth3 = icon_text_.preferredWidth;
			float x = (preferredWidth2 - preferredWidth / 2f + (preferredWidth3 - preferredWidth / 2f)) / 2f;
			float y = (title_back_text_[i].rectTransform.sizeDelta.y - title_back_text_[i].preferredHeight) / 2f;
			KeyType key;
			if (!sub_option)
			{
				switch (optoin_type)
				{
				case OptionItem.SKIP:
					key = KeyType.B;
					break;
				case OptionItem.LANGUAGE:
					key = KeyType.A;
					break;
				default:
					key = KeyType.None;
					break;
				}
			}
			else
			{
				key = KeyType.A;
			}
			key_icon_.load(i);
			key_icon_.iconSet(key, i);
			key_icon_.iconPosSet(title_back_text_[i].transform, new Vector3(x, y, 0f), i);
		}
	}
}
