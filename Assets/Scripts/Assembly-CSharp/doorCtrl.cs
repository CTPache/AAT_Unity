using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorCtrl : MonoBehaviour
{
	private static doorCtrl instance_;

	[SerializeField]
	private GameObject body_;

	[SerializeField]
	private Camera camera_;

	[SerializeField]
	private List<SpriteRenderer> sprite_list_ = new List<SpriteRenderer>();

	[SerializeField]
	private AnimationCurve camera_y_ = new AnimationCurve();

	[SerializeField]
	private AnimationCurve camera_z_ = new AnimationCurve();

	[SerializeField]
	private AnimationCurve door_y_ = new AnimationCurve();

	private IEnumerator enumerator_close_;

	private Sprite sprite_data_;

	private bool is_play_;

	public static doorCtrl instance
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

	private void Awake()
	{
		if (instance_ == null)
		{
			instance_ = this;
		}
	}

	public IEnumerator CoroutineClose()
	{
		is_play_ = true;
		active = true;
		sprite_list_[0].gameObject.SetActive(true);
		sprite_list_[1].gameObject.SetActive(true);
		float time = 0f;
		bool is_loop = true;
		while (is_loop)
		{
			if (time > 1f)
			{
				time = 1f;
				is_loop = false;
			}
			float pos_y = camera_y_.Evaluate(time) * 100f;
			float pos_z = camera_z_.Evaluate(time) * 500f;
			float rot_y = door_y_.Evaluate(time) * 50f;
			sprite_list_[0].gameObject.transform.localEulerAngles = new Vector3(0f, 50f - rot_y, 0f);
			sprite_list_[1].gameObject.transform.localEulerAngles = new Vector3(0f, 310f + rot_y, 0f);
			camera_.gameObject.transform.localPosition = new Vector3(0f, pos_y, 0f - (400f + pos_z));
			time += 0.05f;
			yield return null;
		}
		is_play_ = false;
	}

	public void close()
	{
		stop();
		enumerator_close_ = CoroutineClose();
		coroutineCtrl.instance.Play(enumerator_close_);
	}

	public void stop()
	{
		if (enumerator_close_ != null)
		{
			coroutineCtrl.instance.Stop(enumerator_close_);
			enumerator_close_ = null;
		}
		sprite_list_[0].gameObject.SetActive(false);
		sprite_list_[1].gameObject.SetActive(false);
		active = false;
	}

	public void load()
	{
		AssetBundle assetBundle = AssetBundleCtrl.instance.load("/GS1/etc/", "etc013");
		sprite_data_ = assetBundle.LoadAsset<Sprite>("etc013");
		sprite_list_[0].sprite = sprite_data_;
		sprite_list_[1].sprite = sprite_data_;
		sprite_list_[0].gameObject.SetActive(false);
		sprite_list_[1].gameObject.SetActive(false);
		active = false;
	}

	public void init()
	{
		load();
	}
}
