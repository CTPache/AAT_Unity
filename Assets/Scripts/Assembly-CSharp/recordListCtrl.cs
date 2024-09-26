using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class recordListCtrl : MonoBehaviour
{
	[Serializable]
	public class RecordIcon
	{
		public GameObject body_;

		public InputTouch touch_;

		public AssetBundleSprite icon_;

		public AssetBundleSprite coursor_;

		public AssetBundleSprite board_;
	}

	[Serializable]
	public class Comment
	{
		public List<Text> line_ = new List<Text>();

		public GameObject body_;
	}

	[Serializable]
	public class RecordCurve
	{
		public AnimationCurve in_ = new AnimationCurve();

		public AnimationCurve in_alpha_ = new AnimationCurve();

		public AnimationCurve out_00 = new AnimationCurve();

		public AnimationCurve out_01 = new AnimationCurve();

		public AnimationCurve out_alpha_ = new AnimationCurve();

		public AnimationCurve change_in_ = new AnimationCurve();

		public AnimationCurve change_out_ = new AnimationCurve();
	}

	[Serializable]
	public class RecordSlideCurve
	{
		public AnimationCurve in_ = new AnimationCurve();

		public AnimationCurve out_ = new AnimationCurve();
	}

	[Serializable]
	public class RecodeData
	{
		public int cursor_num_;

		public List<int> note_list_ = new List<int>();

		public int cursor_no_
		{
			get
			{
				if (instance.record_type_ == 0)
				{
					return advCtrl.instance.sub_window_.note_.item_cursor;
				}
				return advCtrl.instance.sub_window_.note_.man_cursor;
			}
			set
			{
				if (instance.record_type_ == 0)
				{
					advCtrl.instance.sub_window_.note_.item_cursor = (byte)value;
				}
				else
				{
					advCtrl.instance.sub_window_.note_.man_cursor = (byte)value;
				}
			}
		}
	}

	private static recordListCtrl instance_;

	private const int KEY_DOWN_COUNT = 8;

	private const int KEY_DOWN_COUNT_UD = 8;

	private const int KEY_DOWN_COUNT_FIRST = 16;

	[SerializeField]
	private AssetBundleSprite board_;

	[SerializeField]
	private AssetBundleSprite info_;

	[SerializeField]
	private InputTouch info_touch_;

	[SerializeField]
	private AssetBundleSprite icon_;

	[SerializeField]
	private AssetBundleSprite icon_base_;

	[SerializeField]
	private AssetBundleSprite title_;

	[SerializeField]
	private SpriteRenderer mask_;

	[SerializeField]
	private SpriteMask sprite_mask_;

	[SerializeField]
	private Text icon_name_;

	[SerializeField]
	private Comment comment_;

	[SerializeField]
	private List<RecordIcon> record_list_ = new List<RecordIcon>();

	[SerializeField]
	private GameObject record_list_obj_;

	[SerializeField]
	private RecordCurve record_curve_ = new RecordCurve();

	[SerializeField]
	private RecordSlideCurve record_slide_curve_ = new RecordSlideCurve();

	[SerializeField]
	private GameObject body_;

	[SerializeField]
	private arrowCtrl arrow_ctrl_;

	[SerializeField]
	private recordGuideCtrl record_guide_;

	[SerializeField]
	private float slide_dist_x_;

	[SerializeField]
	private recordDetailCtrl detail_ctrl_;

	[SerializeField]
	private Camera board_camera_;

	private int page_cnt_;

	private int record_open_type = -1;

	private int record_cursor_no = -1;

	private int page_slide_dir_ = -1;

	private int one_page_max_ = 10;

	private bool page_changing;

	private int detail_data_id_;

	private int detail_obj_id_;

	private List<RecordIcon> record_disp_list_ = new List<RecordIcon>();

	private List<RecordIcon> record_sub_list_ = new List<RecordIcon>();

	private Vector3 list_init_pos_ = default(Vector3);

	private float mask_alpha_ = 0.5f;

	private bool is_open_;

	private bool is_note_on_;

	private bool is_info_;

	private bool select_type_;

	private bool is_select_;

	private bool is_back_;

	private bool is_change_;

	private bool is_disp_detail_;

	private int record_type_;

	public List<RecodeData> record_data_ = new List<RecodeData>();

	private IEnumerator enumerator_record_;

	private IEnumerator enumerator_change_;

	public int note_id;

	private bool close_;

	private bool force_decide_;

	private bool isFingerPrint_;

	private readonly byte[] sProfiles_ = new byte[8] { 123, 192, 120, 122, 15, 118, 207, 119 };

	private RecodeData record_data_profiles_;

	public static recordListCtrl instance
	{
		get
		{
			return instance_;
		}
	}

	private int page_num_
	{
		get
		{
			if (record_type_ == 0)
			{
				return advCtrl.instance.sub_window_.note_.item_page;
			}
			return advCtrl.instance.sub_window_.note_.man_page;
		}
		set
		{
			if (record_type_ == 0)
			{
				advCtrl.instance.sub_window_.note_.item_page = (byte)value;
			}
			else
			{
				advCtrl.instance.sub_window_.note_.man_page = (byte)value;
			}
		}
	}

	public piceData current_pice_
	{
		get
		{
			return piceDataCtrl.instance.note_data[note_id];
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

	public GameObject body
	{
		get
		{
			return body_;
		}
	}

	public bool is_open
	{
		get
		{
			return is_open_;
		}
	}

	public bool is_info
	{
		get
		{
			return is_info_;
		}
	}

	public Comment comment
	{
		get
		{
			return comment_;
		}
	}

	public Text icon_name
	{
		get
		{
			return icon_name_;
		}
	}

	public bool is_note_on
	{
		get
		{
			return is_note_on_;
		}
		set
		{
			is_note_on_ = value;
		}
	}

	public bool select_type
	{
		get
		{
			return select_type_;
		}
		set
		{
			select_type_ = value;
		}
	}

	public bool is_back
	{
		get
		{
			return is_back_;
		}
		set
		{
			is_back_ = value;
		}
	}

	public bool is_change
	{
		get
		{
			return is_change_;
		}
		set
		{
			is_change_ = value;
		}
	}

	public int record_type
	{
		get
		{
			return record_type_;
		}
		set
		{
			record_type_ = value;
		}
	}

	public bool is_select
	{
		get
		{
			return is_select_;
		}
	}

	public int detail_data_id
	{
		get
		{
			return detail_data_id_;
		}
	}

	public int detail_obj_id
	{
		get
		{
			return detail_obj_id_;
		}
	}

	public bool detail_open
	{
		get
		{
			return detail_ctrl_.is_open;
		}
	}

	public int one_page_max
	{
		get
		{
			return one_page_max_;
		}
	}

	public bool is_page_changing
	{
		get
		{
			return page_changing;
		}
	}

	public Camera board_camera
	{
		get
		{
			return board_camera_;
		}
	}

	public recordGuideCtrl record_guide
	{
		get
		{
			return record_guide_;
		}
	}

	public recordDetailCtrl detail_ctrl
	{
		get
		{
			return detail_ctrl_;
		}
	}

	public bool is_cursor_update { get; set; }

	private void Awake()
	{
		if (instance_ == null)
		{
			instance_ = this;
		}
	}

	public void noteOpen(bool seal_key = false, int mode = 0, bool isFingerPrint = false)
	{
		isFingerPrint_ = isFingerPrint;
		if (!is_open_)
		{
			inspectCtrl.instance.end();
			tanteiMenu.instance.end();
			moveCtrl.instance.end();
			if (selectPlateCtrl.instance.body_active)
			{
				record_cursor_no = selectPlateCtrl.instance.cursor_no;
				record_open_type = selectPlateCtrl.instance.type;
				selectPlateCtrl.instance.end();
			}
			record_type_ = mode;
			playRecord(seal_key);
		}
	}

	public void infoLoad()
	{
		string resourceNameLanguage = GSUtility.GetResourceNameLanguage(GSStatic.global_work_.language);
		string in_name = "court_record_03" + GSUtility.GetPlatformResourceName() + resourceNameLanguage;
		if (keyGuideBase.keyguid_pad_)
		{
			in_name = "court_record_02_xbox" + resourceNameLanguage;
		}
		info_.load("/menu/common/", in_name);
		if (!keyGuideBase.keyguid_pad_)
		{
			info_.spriteNo(keyGuideBase.GuideIcon.GetKeyCodeType(KeyType.A));
		}
		else
		{
			info_.spriteNo(3);
		}
	}

	public void load()
	{
		string resourceNameLanguage = GSUtility.GetResourceNameLanguage(GSStatic.global_work_.language);
		board_.load("/menu/common/", "record" + resourceNameLanguage);
		title_.load("/menu/common/", "record" + resourceNameLanguage);
		icon_base_.load("/menu/common/", "evidence_base");
		board_.spriteNo(2);
		title_.spriteNo(0);
		infoLoad();
		info_touch_.touch_key_type = KeyType.A;
		info_touch_.touch_event = delegate
		{
			TouchSystem.TouchInActive();
			detail_ctrl_.ActiveDetailTouch();
		};
		int num = 0;
		foreach (var item in record_list_.Select((RecordIcon item, int index) => new { item, index }))
		{
			item.item.coursor_.load("/menu/common/", "record_icon");
			item.item.board_.load("/menu/common/", "record_icon");
			item.item.touch_.touch_event = TouchIcon;
			item.item.touch_.argument_parameter = num;
			num++;
			if (num >= 10)
			{
				num = 0;
			}
			item.item.icon_.active = false;
			item.item.coursor_.active = false;
			item.item.board_.spriteNo(2);
		}
		arrow_ctrl_.SetTouchEventArrow(delegate(TouchParameter p)
		{
			soundCtrl.instance.PlaySE(42);
			int num3 = (int)p.argument_parameter;
			if (num3 == -1)
			{
				page_num_++;
			}
			else
			{
				page_num_--;
			}
			page_slide_dir_ = num3;
			page_changing = true;
		});
		Vector3 localPosition = title_.transform.localPosition;
		Vector3 localPosition2 = icon_.transform.parent.localPosition;
		Vector3 localPosition3 = icon_name_.transform.localPosition;
		Vector3 localPosition4 = comment_.body_.transform.localPosition;
		Vector3 localPosition5 = info_.transform.localPosition;
		Vector2 sizeDelta = icon_name_.rectTransform.sizeDelta;
		Vector2 sizeDelta2 = comment_.line_[0].rectTransform.sizeDelta;
		int fontSize;
		int fontSize2;
		string lang = Language.langFallback[GSStatic.global_work_.language].ToUpper();
		switch (lang)
		{
		case "JAPAN":
			localPosition.x = -530f;
			localPosition2.x = -490f;
			localPosition3.y = 180f;
			localPosition4.x = 130f;
			localPosition4.y = -35f;
			localPosition5.x = 557f;
			sizeDelta.x = 720f;
			sizeDelta2.x = 700f;
			fontSize = 64;
			fontSize2 = 56;
			break;
		default:
			localPosition.x = -554f;
			localPosition2.x = -530f;
			localPosition3.y = 167f;
			localPosition4.x = 100f;
			localPosition4.y = -45f;
			localPosition5.x = 610f;
			sizeDelta.x = 950f;
			sizeDelta2.x = 850f;
			fontSize = 50;
			fontSize2 = 42;
			break;
		case "KOREA":
			localPosition.x = -554f;
			localPosition2.x = -530f;
			localPosition3.y = 167f;
			localPosition4.x = 100f;
			localPosition4.y = -45f;
			localPosition5.x = 618f;
			sizeDelta.x = 950f;
			sizeDelta2.x = 850f;
			fontSize = 50;
			fontSize2 = 42;
			break;
		case "CHINA_S":
			localPosition.x = -530f;
			localPosition2.x = -490f;
			localPosition3.y = 190f;
			localPosition4.x = 130f;
			localPosition4.y = -35f;
			localPosition5.x = 557f;
			sizeDelta.x = 720f;
			sizeDelta2.x = 700f;
			fontSize = 60;
			fontSize2 = 46;
			break;
		case "CHINA_T":
			localPosition.x = -530f;
			localPosition2.x = -490f;
			localPosition3.y = 195f;
			localPosition4.x = 130f;
			localPosition4.y = -35f;
			localPosition5.x = 557f;
			sizeDelta.x = 720f;
			sizeDelta2.x = 700f;
			fontSize = 60;
			fontSize2 = 44;
			break;
		}
		title_.transform.localPosition = localPosition;
		icon_.transform.parent.localPosition = localPosition2;
		icon_name_.transform.localPosition = localPosition3;
		comment_.body_.transform.localPosition = localPosition4;
		info_.transform.localPosition = localPosition5;
		icon_name_.rectTransform.sizeDelta = sizeDelta;
		icon_name.fontSize = fontSize;
		foreach (Text item2 in comment_.line_)
		{
			item2.fontSize = fontSize2;
			item2.rectTransform.sizeDelta = sizeDelta2;
		}
		sprite_mask_.sprite = board_.sprite_renderer_.sprite;
		int num2 = 0;
		foreach (RecordIcon item3 in record_list_)
		{
			item3.body_.transform.localPosition = new Vector3(-607.5f + 135f * (float)num2, -170f, -15f);
			item3.icon_.transform.localPosition = new Vector3(0f, -10f, -5f);
			item3.icon_.sprite_renderer_.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
			item3.board_.sprite_renderer_.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
			num2 = ((num2 != 9) ? (num2 + 1) : 0);
		}
		record_guide_.load();
		arrow_ctrl_.load();
	}

	public void init()
	{
		load();
		is_disp_detail_ = false;
		record_data_.Clear();
		record_data_.Add(new RecodeData());
		record_data_.Add(new RecodeData());
		record_data_[0].cursor_no_ = 0;
		record_data_[0].cursor_num_ = 0;
		record_data_[1].cursor_no_ = 0;
		record_data_[1].cursor_num_ = 0;
		record_data_profiles_ = new RecodeData();
		record_data_profiles_.cursor_no_ = 0;
		record_data_profiles_.cursor_num_ = 0;
		byte[] array = sProfiles_;
		foreach (byte item in array)
		{
			record_data_profiles_.note_list_.Add(item);
		}
		for (int j = 0; j < one_page_max_; j++)
		{
			record_disp_list_.Add(record_list_[j]);
		}
		for (int k = one_page_max_; k < record_list_.Count; k++)
		{
			record_sub_list_.Add(record_list_[k]);
		}
		RegistrationRecordItem();
		list_init_pos_ = record_list_obj_.transform.localPosition;
		record_guide_.init();
		arrow_ctrl_.init();
	}

	private void RegistrationRecordItem()
	{
		body_active = true;
		foreach (RecordIcon item in record_list_)
		{
			if (!item.body_.activeSelf)
			{
				item.touch_.gameObject.SetActive(true);
				item.touch_.gameObject.SetActive(false);
			}
		}
		body_active = false;
	}

	public void addRecord(int in_id)
	{
		List<piceData> note_data = piceDataCtrl.instance.note_data;
		if (in_id >= note_data.Count)
		{
			Debug.LogWarning("Not Note Data!! in_id:" + in_id);
			return;
		}
		if (piceDataCtrl.instance.GetIconItFile(note_data[in_id].no) == string.Empty)
		{
			Debug.LogWarning("Not Note Data!! in_id:" + in_id);
			return;
		}
		if (note_data[in_id].type >= record_data_.Count)
		{
			Debug.LogWarning("Not Note Type!! in_id:" + in_id + " type:" + note_data[in_id].type);
			return;
		}
		RecodeData recodeData = record_data_[note_data[in_id].type];
		int num = recodeData.note_list_.IndexOf(in_id);
		if (0 <= num)
		{
			Debug.LogWarning("already waiting item!!in_id:" + in_id);
			return;
		}
		recodeData.note_list_.Add(in_id);
		recodeData.cursor_num_++;
	}

	public void deleteRecord(int in_id)
	{
		List<piceData> note_data = piceDataCtrl.instance.note_data;
		if (in_id >= note_data.Count)
		{
			Debug.LogWarning("Not Note Data!! in_id:" + in_id);
			return;
		}
		if (piceDataCtrl.instance.GetIconItFile(note_data[in_id].no) == string.Empty)
		{
			Debug.LogWarning("Not Note Data!! in_id:" + in_id);
			return;
		}
		if (note_data[in_id].type >= record_data_.Count)
		{
			Debug.LogWarning("Not Note Type!! in_id:" + in_id + " type:" + note_data[in_id].type);
			return;
		}
		RecodeData recodeData = record_data_[note_data[in_id].type];
		int num = recodeData.note_list_.IndexOf(in_id);
		if (num < 0)
		{
			Debug.LogWarning("Not Record Data!!in_id:" + in_id);
			return;
		}
		recodeData.note_list_.RemoveAt(num);
		recodeData.cursor_num_--;
		if (note_data[in_id].type == 0)
		{
			advCtrl.instance.sub_window_.note_.item_cursor = 0;
			advCtrl.instance.sub_window_.note_.item_cursor_old = 0;
			advCtrl.instance.sub_window_.note_.item_page = 0;
			advCtrl.instance.sub_window_.note_.item_page_old = 0;
		}
		else
		{
			advCtrl.instance.sub_window_.note_.man_cursor = 0;
			advCtrl.instance.sub_window_.note_.man_cursor_old = 0;
			advCtrl.instance.sub_window_.note_.man_page = 0;
			advCtrl.instance.sub_window_.note_.man_page_old = 0;
		}
	}

	public void updateRecord(int in_id, int in_id2)
	{
		List<piceData> note_data = piceDataCtrl.instance.note_data;
		if (in_id >= note_data.Count)
		{
			Debug.LogWarning("Not Note Data!! in_id:" + in_id);
		}
		else if (in_id2 >= note_data.Count)
		{
			Debug.LogWarning("Not Note Data!! in_id2:" + in_id2);
		}
		else if (piceDataCtrl.instance.GetIconItFile(note_data[in_id].no) == string.Empty)
		{
			Debug.LogWarning("Not Note Data!! in_id:" + in_id);
		}
		else if (piceDataCtrl.instance.GetIconItFile(note_data[in_id2].no) == string.Empty)
		{
			Debug.LogWarning("Not Note Data!! in_id2:" + in_id2);
		}
		else if (note_data[in_id].type >= record_data_.Count)
		{
			Debug.LogWarning("Not Note Type!! in_id:" + in_id + " type:" + note_data[in_id].type);
		}
		else if (note_data[in_id2].type >= record_data_.Count)
		{
			Debug.LogWarning("Not Note Type!! in_id2:" + in_id2 + " type:" + note_data[in_id].type);
		}
		else
		{
			RecodeData recodeData = record_data_[note_data[in_id].type];
			int num = recodeData.note_list_.IndexOf(in_id);
			if (num < 0)
			{
				Debug.LogWarning("Not Record Data!!in_id:" + in_id);
			}
			else
			{
				recodeData.note_list_[num] = in_id2;
			}
		}
	}

	private void TouchIcon(TouchParameter param)
	{
		int num = (int)param.argument_parameter;
		int num2 = one_page_max_ * page_num_ + num;
		if (num2 < record_data_[record_type_].cursor_num_)
		{
			if (CurrentRecodeData().cursor_no_ != num)
			{
				CurrentRecodeData().cursor_no_ = num;
				is_cursor_update = true;
				record_disp_list_[num].touch_.touch_key_type = KeyType.None;
				soundCtrl.instance.PlaySE(42);
			}
			else if (info_.active)
			{
				record_disp_list_[num].touch_.touch_key_type = KeyType.A;
			}
		}
	}

	private void cursorRecord(int in_type, int in_no)
	{
		note_id = 0;
		int num = in_no + one_page_max_ * page_num_;
		if (in_type >= record_data_.Count)
		{
			Debug.LogWarning("Not Note Type!! int_type:" + in_type + " in_no:" + in_no);
			return;
		}
		RecodeData recodeData = CurrentRecodeData();
		if (num >= recodeData.note_list_.Count)
		{
			Debug.LogWarning("Not piece_ Data!! in_no:" + in_no + " record_data.note_list_.Count:" + recodeData.note_list_.Count);
			return;
		}
		note_id = recodeData.note_list_[num];
		List<piceData> note_data = piceDataCtrl.instance.note_data;
		if (note_id >= note_data.Count)
		{
			Debug.LogWarning("Not Note Data!! note_id:" + note_id);
			return;
		}
		piceData piceData2 = note_data[note_id];
		string iconItFile = piceDataCtrl.instance.GetIconItFile(piceData2.no);
		if (iconItFile == string.Empty)
		{
			Debug.LogWarning("Not Note Data!! note_id:" + note_id);
			return;
		}
		icon_.load(piceData2.path, iconItFile);
		icon_name_.text = piceData2.name;
		comment_.line_[0].text = piceData2.comment00;
		comment_.line_[1].text = piceData2.comment01;
		comment_.line_[2].text = piceData2.comment02;
		info_.active = false;
		if (CheckSetumei(note_id) != 0)
		{
			detail_data_id_ = piceData2.detail_id;
			detail_obj_id_ = 0;
			info_.active = true;
		}
		else if (scienceInvestigationCtrl.instance.IsUsable() && piceData2.obj_id != 0)
		{
			detail_data_id_ = 0;
			detail_obj_id_ = piceData2.obj_id;
			if (advCtrl.instance.sub_window_.tutorial_ == 0)
			{
				info_.active = true;
			}
		}
		if (!record_guide_.uniqueGuideSet(piceData2.no))
		{
			record_guide_.recordGuideSet(record_type_);
		}
		foreach (RecordIcon item in record_disp_list_)
		{
			item.board_.spriteNo(2);
			if (item.icon_.active)
			{
				item.board_.spriteNo(1);
			}
		}
		record_disp_list_[in_no].board_.spriteNo(0);
	}

	private void entryRecord(int in_id, int in_no, bool is_sub = false)
	{
		List<piceData> note_data = piceDataCtrl.instance.note_data;
		if (in_id >= note_data.Count)
		{
			Debug.LogWarning("Not Note Data!! in_id:" + in_id);
			return;
		}
		string iconItcFile = piceDataCtrl.instance.GetIconItcFile(note_data[in_id].no);
		if (iconItcFile == string.Empty)
		{
			Debug.LogWarning("Not Note Data!! in_id:" + in_id);
			return;
		}
		List<RecordIcon> list = record_disp_list_;
		if (is_sub)
		{
			list = record_sub_list_;
		}
		list[in_no].icon_.load(note_data[in_id].path, iconItcFile);
		list[in_no].icon_.active = true;
		list[in_no].board_.spriteNo(1);
	}

	private void playRecord(bool seal_key)
	{
		stopRecord();
		if (!keyGuideBase.keyguid_pad_)
		{
			info_.spriteNo(keyGuideBase.GuideIcon.GetKeyCodeType(KeyType.A));
		}
		TouchSystem.TouchInActive();
		enumerator_record_ = CoroutineRecord(seal_key);
		if (seal_key)
		{
			record_data_[0].cursor_no_ = 0;
		}
		mainCtrl.instance.addText(icon_name_);
		foreach (Text item in comment_.line_)
		{
			mainCtrl.instance.addText(item);
		}
		coroutineCtrl.instance.Play(enumerator_record_);
	}

	private void stopRecord()
	{
		if (enumerator_record_ != null)
		{
			coroutineCtrl.instance.Stop(enumerator_record_);
			enumerator_record_ = null;
		}
	}

	public bool KeyDownClose()
	{
		bool flag = false;
		if (GSStatic.global_work_.r.no_3 != 2)
		{
			flag = padCtrl.instance.GetKeyDown(KeyType.R, 2, true, KeyType.B);
		}
		if (padCtrl.instance.GetKeyDown(KeyType.B) || flag)
		{
			return true;
		}
		return false;
	}

	private IEnumerator CoroutineChange()
	{
		arrow_ctrl_.arrowAll(false);
		float time2 = 0f;
		while (time2 < 1f)
		{
			time2 += 0.2f;
			if (time2 > 1f)
			{
				time2 = 1f;
			}
			float y = record_curve_.change_out_.Evaluate(time2);
			board_.transform.localScale = new Vector3(1f, y, 1f);
			yield return null;
		}
		advCtrl.instance.sub_window_.note_.current_mode_old = (byte)record_type_;
		record_type_ = ((record_type_ == 0) ? 1 : 0);
		advCtrl.instance.sub_window_.note_.current_mode = (byte)record_type_;
		record_guide_.recordGuideSet(record_type_);
		ChangeRecord();
		float time = 0f;
		while (time < 1f)
		{
			time += 0.2f;
			if (time > 1f)
			{
				time = 1f;
			}
			float y2 = record_curve_.change_in_.Evaluate(time);
			board_.transform.localScale = new Vector3(1f, y2, 1f);
			yield return null;
		}
		if (page_cnt_ > 1)
		{
			arrow_ctrl_.arrowAll(true);
		}
		ActiveRecodeListTouch();
	}

	private IEnumerator CoroutineRecord(bool seal_key)
	{
		body_active = true;
		is_open_ = true;
		is_select_ = false;
		is_info_ = false;
		close_ = false;
		record_guide_.recordGuideSet(record_type_);
		ChangeRecord();
		if (page_cnt_ > 1)
		{
			arrow_ctrl_.arrowAll(true);
		}
		soundCtrl.instance.PlaySE(49);
		float time = 0f;
		while (time < 1f)
		{
			time += 0.2f;
			if (time > 1f)
			{
				time = 1f;
			}
			float num = record_curve_.in_.Evaluate(time);
			board_.transform.localScale = new Vector3(num, num, 1f);
			float num2 = record_curve_.in_alpha_.Evaluate(time);
			Color color = mask_.color;
			mask_.color = new Color(color.r, color.g, color.g, mask_alpha_ * num2);
			yield return null;
		}
		ActiveRecodeListTouch();
		float key_wait = systemCtrl.instance.key_wait;
		int lr_key_duwn_counter = 0;
		int ud_key_duwn_counter = 0;
		bool is_first_key_down = true;
		while (true)
		{
			if (select_type_)
			{
				if (key_wait > 0f)
				{
					key_wait -= 1f;
				}
				else if (!seal_key && padCtrl.instance.GetKeyDown(KeyType.X))
				{
					soundCtrl.instance.PlaySE(47);
					while (bgCtrl.instance.is_slider)
					{
						yield return null;
					}
					is_select_ = true;
					break;
				}
			}
			if (close_)
			{
				close_ = false;
				if (is_back_)
				{
					break;
				}
			}
			if ((info_.active && !seal_key && padCtrl.instance.GetKeyDown(KeyType.A)) || force_decide_)
			{
				force_decide_ = false;
				is_info_ = true;
				is_disp_detail_ = true;
				while (bgCtrl.instance.is_slider)
				{
					yield return null;
				}
				break;
			}
			if (is_back_ && !seal_key && KeyDownClose())
			{
				yield return null;
				break;
			}
			if (is_change_ && !seal_key && padCtrl.instance.GetKeyDown(KeyType.Record, 2, true, KeyType.R))
			{
				soundCtrl.instance.PlaySE(49);
				enumerator_change_ = CoroutineChange();
				yield return coroutineCtrl.instance.Play(enumerator_change_);
			}
			if (page_changing)
			{
				recordPageChange();
				yield return coroutineCtrl.instance.Play(CoroutinePageChabge());
			}
			RecodeData recodeData = CurrentRecodeData();
			if (!seal_key && recodeData.note_list_.Count > 1 && (padCtrl.instance.GetKey(KeyType.Left) || padCtrl.instance.GetKey(KeyType.StickL_Left) || (padCtrl.instance.GetWheelMoveUp() && padCtrl.instance.IsNextMove())))
			{
				ud_key_duwn_counter = 0;
				if (lr_key_duwn_counter <= 0)
				{
					recodeData.cursor_no_--;
					soundCtrl.instance.PlaySE(42);
					if (is_first_key_down)
					{
						lr_key_duwn_counter = 16;
						is_first_key_down = false;
					}
					else
					{
						lr_key_duwn_counter = 8;
					}
				}
				else
				{
					lr_key_duwn_counter--;
				}
				is_cursor_update = true;
			}
			else if (!seal_key && recodeData.note_list_.Count > 1 && (padCtrl.instance.GetKey(KeyType.Right) || padCtrl.instance.GetKey(KeyType.StickL_Right) || (padCtrl.instance.GetWheelMoveDown() && padCtrl.instance.IsNextMove())))
			{
				ud_key_duwn_counter = 0;
				if (lr_key_duwn_counter <= 0)
				{
					recodeData.cursor_no_++;
					soundCtrl.instance.PlaySE(42);
					if (is_first_key_down)
					{
						lr_key_duwn_counter = 16;
						is_first_key_down = false;
					}
					else
					{
						lr_key_duwn_counter = 8;
					}
				}
				else
				{
					lr_key_duwn_counter--;
				}
				is_cursor_update = true;
			}
			else if (!seal_key && (padCtrl.instance.GetKey(KeyType.Up) || padCtrl.instance.GetKey(KeyType.StickL_Up)))
			{
				lr_key_duwn_counter = 0;
				if (page_cnt_ > 1)
				{
					if (ud_key_duwn_counter <= 0)
					{
						soundCtrl.instance.PlaySE(42);
						page_slide_dir_ = 1;
						page_changing = true;
						page_num_--;
						ud_key_duwn_counter = 8;
						continue;
					}
					ud_key_duwn_counter--;
				}
				is_cursor_update = true;
			}
			else if (!seal_key && (padCtrl.instance.GetKey(KeyType.Down) || padCtrl.instance.GetKey(KeyType.StickL_Down)))
			{
				lr_key_duwn_counter = 0;
				if (page_cnt_ > 1)
				{
					if (ud_key_duwn_counter <= 0)
					{
						soundCtrl.instance.PlaySE(42);
						page_slide_dir_ = -1;
						page_changing = true;
						page_num_++;
						ud_key_duwn_counter = 8;
						continue;
					}
					ud_key_duwn_counter--;
				}
				is_cursor_update = true;
			}
			else
			{
				lr_key_duwn_counter = 0;
				ud_key_duwn_counter = 0;
			}
			if (!is_first_key_down && !padCtrl.instance.GetKey(KeyType.Left) && !padCtrl.instance.GetKey(KeyType.StickL_Left) && !padCtrl.instance.GetKey(KeyType.Right) && !padCtrl.instance.GetKey(KeyType.StickL_Right))
			{
				is_first_key_down = true;
			}
			int num3 = recodeData.note_list_.Count - page_num_ * one_page_max_;
			if (num3 > one_page_max_)
			{
				num3 = one_page_max_;
			}
			if (recodeData.cursor_no_ < 0 || recodeData.cursor_no_ == 255)
			{
				if (page_cnt_ > 1)
				{
					page_slide_dir_ = 1;
					page_changing = true;
					page_num_--;
					continue;
				}
				recodeData.cursor_no_ = num3 - 1;
			}
			if (recodeData.cursor_no_ >= num3)
			{
				if (page_cnt_ > 1)
				{
					page_slide_dir_ = -1;
					page_changing = true;
					page_num_++;
					continue;
				}
				recodeData.cursor_no_ = 0;
			}
			if (is_cursor_update)
			{
				for (int i = 0; i < recodeData.cursor_num_ % one_page_max_; i++)
				{
					record_disp_list_[i].board_.spriteNo(1);
				}
				record_disp_list_[recodeData.cursor_no_].board_.spriteNo(0);
				cursorRecord(record_type_, recodeData.cursor_no_);
				is_cursor_update = false;
			}
			padCtrl.instance.WheelMoveValUpdate();
			yield return null;
		}
		arrow_ctrl_.arrowAll(false);
		float time2 = 0f;
		while (time2 < 1f)
		{
			time2 += 0.2f;
			if (time2 > 1f)
			{
				time2 = 1f;
			}
			float y = record_curve_.out_00.Evaluate(time2);
			float x = record_curve_.out_00.Evaluate(time2);
			board_.transform.localScale = new Vector3(x, y, 1f);
			float num4 = record_curve_.out_alpha_.Evaluate(time2);
			Color color2 = mask_.color;
			mask_.color = new Color(color2.r, color2.g, color2.g, mask_alpha_ * num4);
			yield return null;
		}
		if (record_open_type != -1 && !is_info_)
		{
			selectPlateCtrl.instance.playCursor(record_open_type);
			selectPlateCtrl.instance.cursor_no = record_cursor_no;
			record_open_type = -1;
			messageBoardCtrl.instance.InActiveNormalMessageNextTouch();
		}
		mainCtrl.instance.removeText(icon_name_);
		foreach (Text item in comment_.line_)
		{
			mainCtrl.instance.removeText(item);
		}
		record_guide_.guideIconSet(false, guideCtrl.GuideType.NO_GUIDE);
		record_guide_.Close();
		body_active = false;
		is_open_ = false;
	}

	private void ChangeRecord()
	{
		foreach (RecordIcon item in record_disp_list_)
		{
			item.icon_.active = false;
		}
		if (is_disp_detail_)
		{
			is_disp_detail_ = false;
		}
		RecodeData recodeData = CurrentRecodeData();
		page_cnt_ = (recodeData.note_list_.Count - 1) / one_page_max_ + 1;
		record_guide_.pageGuideReset();
		if (page_cnt_ > 1)
		{
			record_guide_.pageGuideSet(page_cnt_);
			record_guide_.pageChange(page_num_);
		}
		int num = one_page_max_ * page_num_;
		int num2 = recodeData.note_list_.Count - num;
		if (num2 > one_page_max_)
		{
			num2 = one_page_max_;
		}
		for (int i = 0; i < num2; i++)
		{
			entryRecord(recodeData.note_list_[num + i], i);
		}
		cursorRecord(record_type_, recodeData.cursor_no_);
		title_.spriteNo(record_type_);
	}

	private void recordPageChange()
	{
		page_num_ = (((sbyte)page_num_ >= 0) ? page_num_ : (page_cnt_ - 1));
		page_num_ = (((sbyte)page_num_ < page_cnt_) ? page_num_ : 0);
		RecodeData recodeData = CurrentRecodeData();
		int num = one_page_max_ * page_num_;
		int num2 = recodeData.note_list_.Count - num;
		if (num2 > one_page_max_)
		{
			num2 = one_page_max_;
		}
		recodeData.cursor_no_ = ((page_slide_dir_ == 1) ? (num2 - 1) : 0);
		for (int i = 0; i < num2; i++)
		{
			entryRecord(recodeData.note_list_[num + i], i, true);
		}
		foreach (RecordIcon item in record_disp_list_)
		{
			if (item.board_.sprite_no_ == 0)
			{
				item.board_.spriteNo(1);
			}
		}
	}

	private IEnumerator CoroutinePageChabge()
	{
		arrow_ctrl_.arrowAll(false);
		for (int i = 0; i < one_page_max_; i++)
		{
			record_sub_list_[i].body_.SetActive(true);
			record_sub_list_[i].body_.transform.localPosition -= new Vector3(slide_dist_x_ * (float)page_slide_dir_, 0f, 0f);
		}
		float time = 0f;
		while (time < 1f)
		{
			time += 0.05f;
			if (time > 1f)
			{
				time = 1f;
			}
			float num = record_slide_curve_.in_.Evaluate(time);
			num = list_init_pos_.x + slide_dist_x_ * (float)page_slide_dir_ * num;
			record_list_obj_.transform.localPosition = new Vector3(num, list_init_pos_.y, list_init_pos_.z);
			yield return null;
		}
		for (int j = 0; j < one_page_max_; j++)
		{
			record_sub_list_[j].body_.transform.localPosition += new Vector3(slide_dist_x_ * (float)page_slide_dir_, 0f, 0f);
		}
		record_list_obj_.transform.localPosition = list_init_pos_;
		for (int k = 0; k < one_page_max_; k++)
		{
			record_disp_list_[k].icon_.active = false;
			record_disp_list_[k].body_.SetActive(false);
		}
		List<RecordIcon> work2 = new List<RecordIcon>();
		work2 = record_sub_list_;
		record_sub_list_ = record_disp_list_;
		record_disp_list_ = work2;
		arrow_ctrl_.arrowAll(true);
		record_guide_.pageChange(page_num_);
		cursorRecord(record_type_, record_data_[record_type_].cursor_no_);
		page_changing = false;
	}

	public void Decide()
	{
		force_decide_ = true;
	}

	public void Close()
	{
		close_ = true;
	}

	public int selectNoteID()
	{
		RecodeData recodeData = CurrentRecodeData();
		return recodeData.note_list_[recodeData.cursor_no_ + page_num_ * one_page_max_];
	}

	public int selectNoteIDToMujyunID(uint in_message_id)
	{
		int result = -1;
		int num = selectNoteID();
		MUJYUN_CK_DATA[] mujyunCkData = GSScenario.GetMujyunCkData();
		for (int i = 0; i < mujyunCkData.Length; i++)
		{
			if (mujyunCkData[i].jump == in_message_id && mujyunCkData[i].key == num)
			{
				result = i;
				break;
			}
		}
		return result;
	}

	public void setPartRecord(byte scenario_no)
	{
		record_data_[0].note_list_.Clear();
		record_data_[0].cursor_num_ = 0;
		record_data_[0].cursor_no_ = 0;
		record_data_[1].note_list_.Clear();
		record_data_[1].cursor_num_ = 0;
		record_data_[1].cursor_no_ = 0;
		uint[] noteInitData = GSScenario.GetNoteInitData();
		uint[] array = noteInitData;
		foreach (uint num in array)
		{
			if (num != 254 && num != 255)
			{
				addRecord((int)num);
			}
		}
		int num2 = ((record_data_[record_type_].note_list_.Count >= one_page_max_) ? one_page_max_ : record_data_[record_type_].note_list_.Count);
		for (int j = 0; j < num2; j++)
		{
			entryRecord(record_data_[record_type_].note_list_[j], j);
		}
		cursorRecord(record_type_, record_data_[record_type_].cursor_no_);
	}

	public void detailPlay()
	{
		coroutineCtrl.instance.Play(detail_ctrl_.CoroutineViewDetail(detail_data_id_));
	}

	public bool scenarioChange()
	{
		bool result = true;
		if (GSStatic.global_work_.title == TitleId.GS1 && (GSStatic.global_work_.r.no_3 == 1 || GSStatic.global_work_.r.no_3 == 2))
		{
			result = false;
		}
		return result;
	}

	public void recordListSet(int[] list)
	{
		record_data_.Clear();
		record_data_.Add(new RecodeData());
		record_data_.Add(new RecodeData());
		for (int i = 0; i < list.Length && list[i] >= 0; i++)
		{
			addRecord(list[i]);
		}
	}

	private void ActiveRecodeListTouch()
	{
		foreach (RecordIcon item in record_list_)
		{
			item.touch_.ActiveCollider();
		}
		record_guide_.ActiveTouch();
		arrow_ctrl_.ActiveArrow();
		info_touch_.ActiveCollider();
	}

	private sbyte CheckSetumei(int item_num)
	{
		if (GSStatic.global_work_.title == TitleId.GS1)
		{
			sbyte b = GS1_sw_status_CheckSetumei(GSStatic.global_work_.scenario, item_num);
			if (b != -1)
			{
				return b;
			}
		}
		if (piceDataCtrl.instance.note_data[item_num].detail_id != 0)
		{
			return 1;
		}
		return 0;
	}

	private sbyte GS1_sw_status_CheckSetumei(byte scenarioID, int item_num)
	{
		if (advCtrl.instance.sub_window_.tutorial_ != 0)
		{
			return 0;
		}
		if (scenarioID >= 17 && ((scenarioID < 19 && GSFlag.Check(0u, scenario.SCE4_FLAG_STATUS_3D_ENABLE)) || scenarioID >= 19))
		{
			piceData piceData2 = piceDataCtrl.instance.note_data[item_num];
			if (piceData2.detail_id != 0)
			{
				if (piceData2.detail_id == 31 && GSStatic.cinema_work_.status != 0)
				{
					return 0;
				}
				return 1;
			}
			return 0;
		}
		return -1;
	}

	private RecodeData CurrentRecodeData()
	{
		return (!isFingerPrint_) ? record_data_[record_type_] : record_data_profiles_;
	}
}
