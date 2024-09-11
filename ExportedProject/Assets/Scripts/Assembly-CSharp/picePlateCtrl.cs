using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class picePlateCtrl : MonoBehaviour
{
	[Serializable]
	public class Comment
	{
		public List<Text> line_ = new List<Text>();

		public RectTransform rect_transform;
	}

	[Serializable]
	public class PiceCurve
	{
		public AnimationCurve in_ = new AnimationCurve();

		public AnimationCurve out_ = new AnimationCurve();

		public AnimationCurve alpha_in_ = new AnimationCurve();

		public AnimationCurve alpha_out_ = new AnimationCurve();
	}

	private static picePlateCtrl instance_;

	[SerializeField]
	private AssetBundleSprite board_;

	[SerializeField]
	private AssetBundleSprite icon_;

	[SerializeField]
	private AssetBundleSprite icon_base_;

	[SerializeField]
	private SpriteRenderer mask_;

	[SerializeField]
	private Text icon_name_;

	[SerializeField]
	private Comment comment_;

	[SerializeField]
	private PiceCurve pice_curve_ = new PiceCurve();

	[SerializeField]
	private GameObject body_;

	[SerializeField]
	private GameObject icon_obj_;

	[SerializeField]
	private InputTouch touch_;

	private bool is_play_;

	private IEnumerator enumerator_pice_;

	private float mask_alpha_ = 0.5f;

	private Vector3 icon_pos_jpn_ = new Vector3(-340f, 0f, -10f);

	private Vector3 icon_pos_usa_ = new Vector3(-520f, 0f, -10f);

	private Vector3 name_position_jpn_ = new Vector3(180f, 70f, -50f);

	private Vector3 name_position_usa_ = new Vector3(150f, 67f, -50f);

	private Vector3 name_position_chi_s_ = new Vector3(180f, 80f, -50f);

	private Vector3 name_position_chi_t_ = new Vector3(180f, 85f, -50f);

	private Vector2 name_size_jpn_ = new Vector2(640f, 120f);

	private Vector2 name_size_usa_ = new Vector2(950f, 120f);

	private int name_fontsize_jpn_ = 54;

	private int name_fontsize_usa_ = 50;

	private Vector3 comment_position_jpn_ = new Vector3(210f, -120f, -50f);

	private Vector3 comment_position_usa_ = new Vector3(130f, -122f, -50f);

	private Vector3 comment_position_chi_t_ = new Vector3(210f, -115f, -50f);

	private Vector2 comment_size_jpn_ = new Vector2(700f, 60f);

	private Vector2 comment_size_usa_ = new Vector2(900f, 60f);

	private int comment_fontsize_jpn_ = 50;

	private int comment_fontsize_usa_ = 42;

	private int comment_fontsize_chi_s_ = 46;

	private int comment_fontsize_chi_t_ = 44;

	private bool keep_note_off_flag_;

	private bool keep_keyguide_enables_;

	public static picePlateCtrl instance
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

	public Text icon_name
	{
		get
		{
			return icon_name_;
		}
	}

	public Comment comment
	{
		get
		{
			return comment_;
		}
	}

	public bool is_play
	{
		get
		{
			return is_play_;
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
		switch (GSStatic.global_work_.language)
		{
		case Language.JAPAN:
			board_.load("/menu/common/", "evidence_01");
			icon_obj_.transform.localPosition = icon_pos_jpn_;
			icon_name_.rectTransform.localPosition = name_position_jpn_;
			icon_name_.rectTransform.sizeDelta = name_size_jpn_;
			icon_name_.fontSize = name_fontsize_jpn_;
			comment_.rect_transform.localPosition = comment_position_jpn_;
			foreach (Text item in comment_.line_)
			{
				item.rectTransform.sizeDelta = comment_size_jpn_;
				item.fontSize = comment_fontsize_jpn_;
			}
			break;
		default:
			board_.load("/menu/common/", "evidence_01u");
			icon_obj_.transform.localPosition = icon_pos_usa_;
			icon_name_.rectTransform.localPosition = name_position_usa_;
			icon_name_.rectTransform.sizeDelta = name_size_usa_;
			icon_name_.fontSize = name_fontsize_usa_;
			comment_.rect_transform.localPosition = comment_position_usa_;
			foreach (Text item2 in comment_.line_)
			{
				item2.rectTransform.sizeDelta = comment_size_usa_;
				item2.fontSize = comment_fontsize_usa_;
			}
			break;
		case Language.CHINA_S:
			board_.load("/menu/common/", "evidence_01");
			icon_obj_.transform.localPosition = icon_pos_jpn_;
			icon_name_.rectTransform.localPosition = name_position_chi_s_;
			icon_name_.rectTransform.sizeDelta = name_size_jpn_;
			icon_name_.fontSize = name_fontsize_jpn_;
			comment_.rect_transform.localPosition = comment_position_jpn_;
			foreach (Text item3 in comment_.line_)
			{
				item3.rectTransform.sizeDelta = comment_size_jpn_;
				item3.fontSize = comment_fontsize_chi_s_;
			}
			break;
		case Language.CHINA_T:
			board_.load("/menu/common/", "evidence_01");
			icon_obj_.transform.localPosition = icon_pos_jpn_;
			icon_name_.rectTransform.localPosition = name_position_chi_t_;
			icon_name_.rectTransform.sizeDelta = name_size_jpn_;
			icon_name_.fontSize = name_fontsize_jpn_;
			comment_.rect_transform.localPosition = comment_position_chi_t_;
			foreach (Text item4 in comment_.line_)
			{
				item4.rectTransform.sizeDelta = comment_size_jpn_;
				item4.fontSize = comment_fontsize_chi_t_;
			}
			break;
		}
		icon_base_.load("/menu/common/", "evidence_base");
		board_.spriteNo(2);
		icon_base_.spriteNo(0);
	}

	public void init()
	{
		load();
	}

	public void entryPice(int in_type, int in_id)
	{
		keep_note_off_flag_ = (GSStatic.global_work_.status_flag & 0x10) != 0;
		GSStatic.global_work_.status_flag |= 16u;
		keep_keyguide_enables_ = messageBoardCtrl.instance.guide_ctrl.getEnables();
		messageBoardCtrl.instance.guide_ctrl.setEnables(false);
		List<piceData> note_data = piceDataCtrl.instance.note_data;
		if (in_id < note_data.Count)
		{
			string iconItFile = piceDataCtrl.instance.GetIconItFile(note_data[in_id].no);
			if (iconItFile == string.Empty)
			{
				Debug.LogWarning("Not Note Data!! in_id:" + in_id);
				return;
			}
			icon_.load(note_data[in_id].path, iconItFile);
			icon_.spriteNo(0);
			icon_name_.text = note_data[in_id].name;
			comment_.line_[0].text = note_data[in_id].comment00;
			comment_.line_[1].text = note_data[in_id].comment01;
			comment_.line_[2].text = note_data[in_id].comment02;
		}
	}

	public void playPice(float in_wait)
	{
		stopPice();
		enumerator_pice_ = CoroutinePice(in_wait);
		touch_.SetColliderSize(new Vector2(1980f, 1080f));
		touch_.touch_key_type = KeyType.A;
		coroutineCtrl.instance.Play(enumerator_pice_);
	}

	public bool closePice()
	{
		if (enumerator_pice_ == null)
		{
			enumerator_pice_ = CoroutineClosePice();
			coroutineCtrl.instance.Play(enumerator_pice_);
			return true;
		}
		return false;
	}

	private IEnumerator CoroutineClosePice()
	{
		soundCtrl.instance.PlaySE(43);
		float end_time = 2f;
		float time = 0f;
		while (time < end_time)
		{
			time += 0.1f;
			if (time > end_time)
			{
				time = end_time;
			}
			float num = pice_curve_.out_.Evaluate(time / end_time);
			board_.transform.localPosition = new Vector3((0f - num) * 1920f, 0f, 0f);
			float num2 = pice_curve_.alpha_out_.Evaluate(time);
			Color color = mask_.color;
			mask_.color = new Color(color.r, color.g, color.g, mask_alpha_ * num2);
			yield return null;
		}
		body_active = false;
		is_play_ = false;
		if (!keep_note_off_flag_)
		{
			GSStatic.global_work_.status_flag &= 4294967279u;
		}
		messageBoardCtrl.instance.guide_ctrl.setEnables(keep_keyguide_enables_);
		MessageSystem.GetActiveMessageWork().status |= MessageSystem.Status.RT_GO;
	}

	private void stopPice()
	{
		if (enumerator_pice_ != null)
		{
			coroutineCtrl.instance.Stop(enumerator_pice_);
			enumerator_pice_ = null;
		}
	}

	private IEnumerator CoroutinePice(float in_wait)
	{
		body_active = true;
		is_play_ = true;
		soundCtrl.instance.PlaySE(15);
		float end_time = 2f;
		float time = 0f;
		while (time < end_time)
		{
			time += 0.1f;
			if (time > end_time)
			{
				time = end_time;
			}
			float num = pice_curve_.in_.Evaluate(time / end_time);
			board_.transform.localPosition = new Vector3(num * 1920f, 0f, 0f);
			float num2 = pice_curve_.alpha_in_.Evaluate(time);
			Color color = mask_.color;
			mask_.color = new Color(color.r, color.g, color.g, mask_alpha_ * num2);
			yield return null;
		}
		enumerator_pice_ = null;
	}
}
