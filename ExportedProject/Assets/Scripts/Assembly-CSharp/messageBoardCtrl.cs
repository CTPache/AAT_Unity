using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class messageBoardCtrl : MonoBehaviour
{
	[Serializable]
	public class GuideIcon
	{
		public AssetBundleSprite sprite_;

		public Text text_;
	}

	[Serializable]
	public class ArrowIcon
	{
		public GameObject obj_;

		public AssetBundleSprite arrow_;

		public AssetBundleSprite icon_;

		public Text text_;

		public AnimationCurve in_ = new AnimationCurve();

		public AnimationCurve loop_ = new AnimationCurve();

		public AnimationCurve out_ = new AnimationCurve();

		public AnimationCurve enter_scale_ = new AnimationCurve();

		public AnimationCurve enter_alpha_ = new AnimationCurve();

		public AnimationCurve enter_pos_ = new AnimationCurve();

		public IEnumerator enumerator_arrow_;

		public IEnumerator enumerator_next_;

		public bool active
		{
			get
			{
				return obj_.activeSelf;
			}
			set
			{
				obj_.SetActive(value);
			}
		}
	}

	private static messageBoardCtrl instance_;

	private int[] name_id_tbl = new int[69]
	{
		49, 6, 6, 0, 20, 4, 18, 1, 39, 3,
		0, 5, 9, 14, 10, 19, 12, 13, 28, 24,
		27, 26, 1, 29, 25, 47, 37, 36, 43, 19,
		15, 49, 40, 41, 44, 45, 0, 1, 49, 49,
		49, 1, 2, 1, 20, 49, 1, 17, 21, 8,
		7, 16, 11, 22, 23, 49, 49, 13, 31, 30,
		32, 33, 34, 38, 35, 42, 49, 46, 48
	};

	[SerializeField]
	private textKeyIconCtrl msg_key_icon_;

	private ushort touch_skip_frame;

	[SerializeField]
	private guideCtrl guide_ctrl_;

	[SerializeField]
	private List<AssetBundleSprite> sprite_list_ = new List<AssetBundleSprite>();

	[SerializeField]
	private List<ArrowIcon> arrow_list_ = new List<ArrowIcon>();

	[SerializeField]
	private List<Text> line_list_ = new List<Text>();

	[SerializeField]
	private GameObject text_;

	[SerializeField]
	private GameObject body_;

	[SerializeField]
	private InputTouch left_arrow_touch_;

	[SerializeField]
	private InputTouch message_board_touch_;

	[SerializeField]
	private Transform quake_targets_;

	private bool is_arrow_;

	private string name_plate_path_ = string.Empty;

	private Language name_plate_language_;

	public const ushort MES_WIN_DISP_ON = 0;

	public const ushort MES_WIN_DISP_OFF = 1;

	public const ushort MES_WIN_SCROLL_IN = 2;

	public const ushort MES_WIN_SCROLL_OUT = 3;

	public const ushort MES_WIN_DISP_OFF2 = 4;

	public const ushort MES_WIN_NO_SCROLL_OUT = 6;

	public const ushort MES_WIN_SCROLL_UP = 8;

	public const ushort MES_WIN_SCROLL_DOWN = 16;

	public static messageBoardCtrl instance
	{
		get
		{
			return instance_;
		}
	}

	public textKeyIconCtrl msg_key_icon
	{
		get
		{
			return msg_key_icon_;
		}
	}

	public bool touch_skip_flag { get; private set; }

	public guideCtrl guide_ctrl
	{
		get
		{
			return guide_ctrl_;
		}
	}

	public List<Text> line_list
	{
		get
		{
			return line_list_;
		}
	}

	public AssetBundleSprite sprite_board
	{
		get
		{
			return sprite_list_[0];
		}
	}

	public AssetBundleSprite sprite_name
	{
		get
		{
			return sprite_list_[1];
		}
	}

	public ArrowIcon arrowR
	{
		get
		{
			return arrow_list_[0];
		}
	}

	public ArrowIcon arrowL
	{
		get
		{
			return arrow_list_[1];
		}
	}

	public GameObject text
	{
		get
		{
			return text_;
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

	public bool is_arrow
	{
		get
		{
			return is_arrow_;
		}
		set
		{
			is_arrow_ = value;
		}
	}

	public Transform quake_targets
	{
		get
		{
			return quake_targets_;
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
		int fontSize = 64;
		string in_name = "talk_bg";
		if (GSStatic.global_work_.language == Language.JAPAN)
		{
			fontSize = 64;
			in_name = "talk_bg";
			sprite_name.transform.localPosition = new Vector3(-640f, 135f, 0f);
			text_.transform.localPosition = new Vector3(0f, 42f, 0f);
			line_list[0].rectTransform.localPosition = new Vector3(0f, 0f, 0f);
			line_list[1].rectTransform.localPosition = new Vector3(0f, -84f, 0f);
			line_list[2].rectTransform.localPosition = new Vector3(0f, -168f, 0f);
		}
		else if (GSStatic.global_work_.language == Language.USA)
		{
			fontSize = 52;
			in_name = "talk_bgu";
			sprite_name.transform.localPosition = new Vector3(-640f, 177f, 0f);
			text_.transform.localPosition = new Vector3(0f, 80f, 0f);
			line_list[0].rectTransform.localPosition = new Vector3(0f, 0f, 0f);
			line_list[1].rectTransform.localPosition = new Vector3(0f, -74f, 0f);
			line_list[2].rectTransform.localPosition = new Vector3(0f, -148f, 0f);
		}
		SetArrowPosition(false, 0);
		SetArrowPosition(false, 1);
		namePlateLoad();
		sprite_board.load("/menu/common/", in_name);
		sprite_name.spriteNo(0);
		arrow_list_[0].arrow_.load("/menu/common/", "select_arrow");
		arrow_list_[0].icon_.load("/menu/common/", "symbol");
		arrow_list_[0].arrow_.spriteNo(1);
		arrow_list_[0].icon_.spriteNo(0);
		arrow_list_[0].text_.text = "進む";
		arrow_list_[1].arrow_.load("/menu/common/", "select_arrow");
		arrow_list_[1].icon_.load("/menu/common/", "symbol");
		arrow_list_[1].arrow_.spriteNo(1);
		arrow_list_[1].icon_.spriteNo(1);
		arrow_list_[1].text_.text = "戻る";
		line_list_[0].text = string.Empty;
		line_list_[1].text = string.Empty;
		line_list_[2].text = string.Empty;
		line_list_[0].fontSize = fontSize;
		line_list_[1].fontSize = fontSize;
		line_list_[2].fontSize = fontSize;
		foreach (Text item in line_list_)
		{
			mainCtrl.instance.addText(item);
		}
		guide_ctrl_.load();
	}

	public void SetArrowPosition(bool in_type, int in_direction)
	{
		Vector3 localPosition = arrow_list_[in_direction].obj_.transform.localPosition;
		if (GSStatic.global_work_.language == Language.JAPAN)
		{
			localPosition.y = 0f;
		}
		else
		{
			localPosition.y = ((!in_type) ? (-50f) : 25f);
		}
		arrow_list_[in_direction].obj_.transform.localPosition = localPosition;
	}

	public void namePlateLoad()
	{
		if (name_plate_path_ == string.Empty)
		{
			name_plate_path_ = "/GS1/etc/";
		}
		string remove_name;
		string in_name = (remove_name = "frame02" + GSUtility.GetResourceNameLanguage(GSStatic.global_work_.language));
		if (GSStatic.global_work_.language != name_plate_language_)
		{
			remove_name = "frame02" + GSUtility.GetResourceNameLanguage(name_plate_language_);
			name_plate_language_ = GSStatic.global_work_.language;
		}
		AssetBundleCtrl.AssetBundleData assetBundleData = AssetBundleCtrl.instance.asset_list_.Find((AssetBundleCtrl.AssetBundleData data) => data.name_ == remove_name);
		if (assetBundleData != null)
		{
			AssetBundleCtrl.instance.remove(name_plate_path_, remove_name);
		}
		switch (GSStatic.global_work_.title)
		{
		case TitleId.GS1:
			name_plate_path_ = "/GS1/etc/";
			break;
		case TitleId.GS2:
			name_plate_path_ = "/GS2/etc/";
			break;
		case TitleId.GS3:
			name_plate_path_ = "/GS3/etc/";
			break;
		}
		sprite_name.load(name_plate_path_, in_name);
	}

	public void init()
	{
		load();
		guide_ctrl_.init();
		left_arrow_touch_.touch_key_type = KeyType.Left;
		left_arrow_touch_.touch_event = delegate
		{
			message_board_touch_.SetEnableCollider(false);
		};
		message_board_touch_.touch_key_type = KeyType.None;
		message_board_touch_.down_event = delegate
		{
			MessageWork activeMessageWork = MessageSystem.GetActiveMessageWork();
			if (activeMessageWork.code == 21)
			{
				message_board_touch_.dow_key_type = KeyType.A;
				message_board_touch_.questioning_touch_type = KeyType.Right;
			}
			else if (episodeReleaseCtrl.instance != null && episodeReleaseCtrl.instance.body_active_)
			{
				message_board_touch_.touch_key_type = KeyType.A;
			}
			else
			{
				message_board_touch_.dow_key_type = KeyType.A;
			}
			touch_skip_frame = 0;
			touch_skip_flag = false;
		};
		message_board_touch_.drag_event = DragEvent;
		body_active = false;
	}

	private void DragEvent(Vector3 p)
	{
		if (!touch_skip_flag)
		{
			touch_skip_frame++;
			if (touch_skip_frame >= 6)
			{
				touch_skip_flag = true;
				touch_skip_frame = 0;
			}
		}
	}

	public void Close()
	{
		sprite_board.active = false;
		sprite_name.active = false;
		guide_ctrl_.guideIconSet(true, guideCtrl.GuideType.NO_GUIDE);
	}

	public void End()
	{
		line_list_[0].text = string.Empty;
		line_list_[1].text = string.Empty;
		line_list_[2].text = string.Empty;
		body_active = false;
	}

	public void Terminate()
	{
		guide_ctrl_.guideIconSet(true, guideCtrl.GuideType.NO_GUIDE);
		foreach (Text item in line_list_)
		{
			mainCtrl.instance.removeText(item);
		}
	}

	public void guide_set(bool is_move, guideCtrl.GuideType in_type)
	{
		if (is_move)
		{
			guide_ctrl_.changeGuide(in_type);
		}
		else
		{
			guide_ctrl_.guideIconSet(true, in_type);
		}
	}

	public bool is_guide_set()
	{
		if ((GSStatic.global_work_.status_flag & 0x10) == 0 && GSStatic.global_work_.r.no_0 != 12 && (GSStatic.global_work_.r.no_0 != 5 || GSStatic.global_work_.r.no_1 != 10) && GSStatic.global_work_.r.no_0 != 8 && !scienceInvestigationCtrl.instance.active && body_active && sprite_board.active)
		{
			return true;
		}
		return false;
	}

	public void name_plate(bool in_name, int in_name_no, int in_pos)
	{
		GSStatic.msg_save_data.name_no = (ushort)in_name_no;
		if (in_name_no == 0)
		{
			in_name = false;
		}
		if (GSStatic.global_work_.title == TitleId.GS3)
		{
			in_name_no = name_id_tbl[in_name_no];
			if (in_name_no == 49)
			{
				in_name = false;
			}
		}
		if (in_name != sprite_name.active || in_name_no != sprite_name.sprite_no_)
		{
			if (in_name)
			{
				sprite_name.active = true;
				sprite_name.spriteNo(in_name_no);
			}
			else
			{
				sprite_name.active = false;
			}
		}
	}

	public void board(bool in_board, bool in_mes)
	{
		if (in_board != body_active || in_board != sprite_board.active)
		{
			if (in_board)
			{
				ActiveNormalMessageNextTouch();
				body_active = true;
				sprite_board.active = true;
				arrow(false, 0);
				arrow(false, 1);
				Canvas.ForceUpdateCanvases();
			}
			else
			{
				InActiveNormalMessageNextTouch();
				body_active = false;
				sprite_board.active = false;
				guide_ctrl_.guideIconSet(true, guideCtrl.GuideType.NO_GUIDE);
			}
		}
	}

	public void arrow(bool in_arrow, int in_type)
	{
		if ((in_arrow && in_type == 1) || in_arrow || in_type == 0)
		{
		}
		if (in_type < arrow_list_.Count && arrow_list_[in_type].active != in_arrow)
		{
			if (in_arrow)
			{
				arrow_list_[in_type].active = in_arrow;
				playArrow(in_type);
			}
			else
			{
				is_arrow_ = true;
				arrow_list_[in_type].active = in_arrow;
			}
		}
	}

	public void playArrow(int in_type)
	{
		ArrowIcon arrowIcon = arrow_list_[in_type];
		stopArrow(in_type);
		arrowIcon.enumerator_arrow_ = CoroutineArrow(in_type);
		StartCoroutine(arrowIcon.enumerator_arrow_);
		if (in_type != 1)
		{
		}
	}

	private void stopArrow(int in_type)
	{
		ArrowIcon arrowIcon = arrow_list_[in_type];
		if (arrowIcon.enumerator_arrow_ != null)
		{
			StopCoroutine(arrowIcon.enumerator_arrow_);
			arrowIcon.enumerator_arrow_ = null;
		}
	}

	private IEnumerator CoroutineArrow(int in_type)
	{
		ArrowIcon arrow = arrow_list_[in_type];
		float time2 = 0f;
		while (true)
		{
			time2 += 0.0625f;
			if (time2 > 1f)
			{
				break;
			}
			float alpha = arrow.in_.Evaluate(time2);
			Color color = arrow.arrow_.sprite_renderer_.color;
			arrow.arrow_.sprite_renderer_.color = new Color(color.r, color.g, color.g, alpha);
			yield return null;
		}
		Color color2 = arrow.arrow_.sprite_renderer_.color;
		arrow.arrow_.sprite_renderer_.color = new Color(color2.r, color2.g, color2.g, 1f);
		float time = 0f;
		float speed = ((in_type != 0) ? (-1f) : 1f);
		while (true)
		{
			time += 0.025f;
			if (time > 1f)
			{
				time = 0f;
			}
			float pos_x = arrow.loop_.Evaluate(time) * 5f * speed;
			arrow.arrow_.transform.localPosition = new Vector3(pos_x, 0f, 0f);
			yield return null;
		}
	}

	public void playNext(int in_type)
	{
		ArrowIcon arrowIcon = arrow_list_[in_type];
		stopArrow(in_type);
		stopNext(in_type);
		arrowIcon.enumerator_next_ = CoroutineNext(in_type);
		StartCoroutine(arrowIcon.enumerator_next_);
	}

	private void stopNext(int in_type)
	{
		ArrowIcon arrowIcon = arrow_list_[in_type];
		if (arrowIcon.enumerator_next_ != null)
		{
			StopCoroutine(arrowIcon.enumerator_next_);
			arrowIcon.enumerator_next_ = null;
		}
	}

	private IEnumerator CoroutineNext(int in_type)
	{
		is_arrow_ = false;
		ArrowIcon arrow = arrow_list_[in_type];
		Color color2 = arrow.arrow_.sprite_renderer_.color;
		arrow.arrow_.sprite_renderer_.color = new Color(color2.r, color2.g, color2.g, 1f);
		arrow.arrow_.transform.localPosition = new Vector3(0f, 0f, 0f);
		arrow.arrow_.transform.localScale = new Vector3(1f, 1f, 1f);
		float time = 0f;
		while (true)
		{
			time += 0.1f;
			if (time > 1f)
			{
				break;
			}
			float scale = arrow.enter_scale_.Evaluate(time);
			float alpha = arrow.enter_alpha_.Evaluate(time);
			float pos = arrow.enter_pos_.Evaluate(time) * 60f;
			Color color = arrow.arrow_.sprite_renderer_.color;
			arrow.arrow_.sprite_renderer_.color = new Color(color.r, color.g, color.g, alpha);
			arrow.arrow_.transform.localPosition = new Vector3(pos, 0f, 0f);
			arrow.arrow_.transform.localScale = new Vector3(1f, scale, 1f);
			yield return null;
		}
		Color color3 = arrow.arrow_.sprite_renderer_.color;
		arrow.arrow_.sprite_renderer_.color = new Color(color3.r, color3.g, color3.g, 1f);
		arrow.arrow_.transform.localPosition = new Vector3(0f, 0f, 0f);
		arrow.arrow_.transform.localScale = new Vector3(1f, 1f, 1f);
		arrow.active = false;
		is_arrow_ = true;
	}

	public void SaveMsgSet()
	{
		GSStatic.msg_save_data.window_visible = body_active;
		GSStatic.msg_save_data.name_visible = sprite_name.active;
		GSStatic.msg_save_data.msg_line1 = line_list_[0].text;
		GSStatic.msg_save_data.msg_line2 = line_list_[1].text;
		GSStatic.msg_save_data.msg_line3 = line_list_[2].text;
		for (int i = 0; i < GSStatic.msg_save_data.line_x.Length; i++)
		{
			GSStatic.msg_save_data.line_x[i] = (int)line_list_[i].transform.localPosition.x;
		}
		SaveMsgKeyIconSet();
	}

	public void SaveMsgKeyIconSet()
	{
		for (int i = 0; i < msg_key_icon_.key_icon.Length; i++)
		{
			GSStatic.msg_save_data.key_icon[i].key_icon_visible = msg_key_icon_.key_icon[i].icon_active_;
			GSStatic.msg_save_data.key_icon[i].key_icon_pos_x = msg_key_icon_.key_icon[i].icon_pos_.x;
			GSStatic.msg_save_data.key_icon[i].key_icon_pos_y = msg_key_icon_.key_icon[i].icon_pos_.y;
			GSStatic.msg_save_data.key_icon[i].key_icon_type = (ushort)msg_key_icon_.key_icon[i].icon_key_type_;
		}
	}

	public void LoadMsgSet()
	{
		if (GSStatic.msg_save_data.window_visible)
		{
			board(true, true);
			name_plate(GSStatic.msg_save_data.name_visible, GSStatic.msg_save_data.name_no, GSStatic.global_work_.win_name_set);
			line_list_[0].text = GSStatic.msg_save_data.msg_line1;
			line_list_[1].text = GSStatic.msg_save_data.msg_line2;
			line_list_[2].text = GSStatic.msg_save_data.msg_line3;
			for (int i = 0; i < GSStatic.msg_save_data.line_x.Length; i++)
			{
				Vector3 localPosition = line_list_[i].transform.localPosition;
				localPosition.x = GSStatic.msg_save_data.line_x[i];
				line_list_[i].transform.localPosition = localPosition;
			}
			for (int j = 0; j <= GSStatic.message_work_.message_line; j++)
			{
				if (GSStatic.message_work_.message_line >= line_list.Count)
				{
					break;
				}
				line_list[j].gameObject.SetActive(true);
			}
		}
		LoadMsgKeyIconSet();
	}

	public void LoadMsgKeyIconSet()
	{
		msg_key_icon_.keyIconActiveSet(false);
		for (int i = 0; i < msg_key_icon_.key_icon.Length; i++)
		{
			if (GSStatic.msg_save_data.key_icon[i].key_icon_visible)
			{
				msg_key_icon_.load(i);
				msg_key_icon_.key_icon[i].icon_active_ = GSStatic.msg_save_data.key_icon[i].key_icon_visible;
				msg_key_icon_.iconPosSet(line_list_[i].transform, new Vector3(GSStatic.msg_save_data.key_icon[i].key_icon_pos_x, GSStatic.msg_save_data.key_icon[i].key_icon_pos_y, 0f), i);
				msg_key_icon_.iconSet((KeyType)GSStatic.msg_save_data.key_icon[i].key_icon_type, i);
			}
		}
	}

	public void SetPos(float x, float y)
	{
		Vector3 localPosition = text_.transform.localPosition;
		localPosition.x = x;
		localPosition.y = y;
		text_.transform.localPosition = localPosition;
	}

	public void SetAlphaText(float a)
	{
		foreach (Text item in line_list_)
		{
			Color color = item.color;
			color.a = a;
			item.color = color;
		}
	}

	public void ActiveMessageBoardTouch()
	{
		left_arrow_touch_.ActiveCollider();
		guide_ctrl_.ActiveTouch();
		message_board_touch_.ActiveCollider();
	}

	public void InActiveNormalMessageNextTouch()
	{
		message_board_touch_.SetEnableCollider(false);
	}

	public void ActiveNormalMessageNextTouch()
	{
		message_board_touch_.SetEnableCollider(true);
	}
}
