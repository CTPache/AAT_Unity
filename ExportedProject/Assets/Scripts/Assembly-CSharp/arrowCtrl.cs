using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class arrowCtrl : MonoBehaviour
{
	[Serializable]
	public class ArrowIcon
	{
		public GameObject obj_;

		public AssetBundleSprite arrow_;

		public AssetBundleSprite icon_;

		public InputTouch touch_;

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

	[SerializeField]
	private List<ArrowIcon> arrow_list_ = new List<ArrowIcon>();

	private bool is_arrow_;

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

	public void load()
	{
		arrow_list_[0].arrow_.load("/menu/common/", "select_arrow", true);
		arrow_list_[1].arrow_.load("/menu/common/", "select_arrow", true);
		arrow_list_[0].arrow_.spriteNo(1);
		arrow_list_[0].text_.text = "進む";
		arrow_list_[1].arrow_.spriteNo(1);
		arrow_list_[1].text_.text = "戻る";
		arrow_list_[0].touch_.argument_parameter = -1;
		arrow_list_[1].touch_.argument_parameter = 1;
	}

	public void SetTouchEventArrow(Action<TouchParameter> touch)
	{
		foreach (ArrowIcon item in arrow_list_)
		{
			item.touch_.touch_event = touch;
		}
	}

	public void SetTouchKeyType(KeyType key_type, int index)
	{
		if (arrow_list_.Count > index && arrow_list_.Count > 0)
		{
			arrow_list_[index].touch_.touch_key_type = key_type;
		}
	}

	public void SetTouchedKeyType(KeyType key_type, int index)
	{
		if (arrow_list_.Count > index && arrow_list_.Count > 0)
		{
			arrow_list_[index].touch_.touched_key_type = key_type;
		}
	}

	public void ActiveArrow()
	{
		foreach (ArrowIcon item in arrow_list_)
		{
			item.touch_.ActiveCollider();
		}
	}

	public void init()
	{
	}

	public void arrow(bool in_arrow, int in_type)
	{
		if (in_type < arrow_list_.Count && arrow_list_[in_type].active != in_arrow)
		{
			if (in_arrow)
			{
				arrow_list_[in_type].active = in_arrow;
				playArrow(in_type);
			}
			else
			{
				stopArrow(in_type);
				is_arrow_ = true;
				arrow_list_[in_type].active = in_arrow;
			}
		}
	}

	public void arrowAll(bool in_arrow)
	{
		foreach (var item in arrow_list_.Select((ArrowIcon body, int idx) => new { body, idx }))
		{
			if (item.body.active != in_arrow)
			{
				if (in_arrow)
				{
					item.body.active = in_arrow;
					playArrow(item.idx);
				}
				else
				{
					stopArrow(item.idx);
					is_arrow_ = true;
					item.body.active = in_arrow;
				}
			}
		}
	}

	public void playArrow(int in_type)
	{
		ArrowIcon arrowIcon = arrow_list_[in_type];
		stopArrow(in_type);
		arrowIcon.enumerator_arrow_ = CoroutineArrow(in_type);
		StartCoroutine(arrowIcon.enumerator_arrow_);
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
}
