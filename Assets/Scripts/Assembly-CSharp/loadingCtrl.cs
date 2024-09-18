using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class loadingCtrl : MonoBehaviour
{
	public enum Type
	{
		LOADING = 0,
		SAVEING = 1,
		DIFF = 2,
		DELETING = 3
	}

	private static loadingCtrl instance_;

	private float start_time_;

	[SerializeField]
	private GameObject body_;

	[SerializeField]
	private AssetBundleSprite window_;

	[SerializeField]
	private List<AssetBundleSprite> icon_ = new List<AssetBundleSprite>();

	[SerializeField]
	private AssetBundleSprite mask_;

	[SerializeField]
	private Text text_;

	private IEnumerator play_enumerator_;

	private bool is_wait_;

	public static loadingCtrl instance
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

	public bool is_wait
	{
		get
		{
			return is_wait_;
		}
	}

	private void Awake()
	{
		if (instance_ == null)
		{
			instance_ = this;
		}
	}

	private IEnumerator PlayCoroutine(int in_type, int in_time)
	{
		switch (in_type)
		{
		case 0:
			text_.text = TextDataCtrl.GetText(TextDataCtrl.SaveTextID.LOADDING);
			break;
		case 1:
			text_.text = TextDataCtrl.GetText(TextDataCtrl.SaveTextID.SAVING_STEAM) + "\n";
			text_.text += TextDataCtrl.GetText(TextDataCtrl.SaveTextID.SAVING_STEAM, 1);
			break;
		case 2:
			text_.text = TextDataCtrl.GetText(TextDataCtrl.SystemTextID.SAVE_DATA_DIFF, 0) + "\n";
			text_.text += TextDataCtrl.GetText(TextDataCtrl.SystemTextID.SAVE_DATA_DIFF, 1);
			break;
		case 3:
			text_.text = TextDataCtrl.StringArrayToString(TextDataCtrl.GetTexts(TextDataCtrl.SaveTextID.DELETING));
			break;
		}
		active = true;
		if (in_type != 0 && in_type != 1 && in_type != 3)
		{
			yield break;
		}
		int icon_index = in_type;
		switch (in_type)
		{
		case 0:
		case 3:
			icon_index = 0;
			break;
		case 1:
			icon_index = 1;
			break;
		}
		int sprite_num = icon_[icon_index].sprite_data_.Count - 1;
		int sprite_no2 = 0;
		int time = 0;
		icon_[icon_index].active = true;
		while (true)
		{
			time++;
			if (time > in_time)
			{
				sprite_no2++;
				sprite_no2 = ((sprite_no2 <= sprite_num) ? sprite_no2 : 0);
				icon_[icon_index].spriteNo(sprite_no2);
				time = 0;
			}
			yield return null;
		}
	}

	public void init()
	{
		icon_[0].load("/menu/common/", "loading");
		icon_[1].load("/menu/common/", "writing");
		window_.load("/menu/common/", "save_window");
		mask_.load("/menu/common/", "mask");
		icon_[0].active = false;
		icon_[1].active = false;
		window_.sprite_renderer_.size = new Vector2(1200f, 360f);
		mainCtrl.instance.addText(text_);
	}

	public void play(Type in_type)
	{
		play_enumerator_ = PlayCoroutine((int)in_type, 10);
		coroutineCtrl.instance.Play(play_enumerator_);
		start_time_ = Time.time;
	}

	public IEnumerator wait(float wait_time = 1f)
	{
		is_wait_ = true;
		while (true)
		{
			float time = Time.time - start_time_;
			if (time > wait_time)
			{
				break;
			}
			yield return null;
		}
		is_wait_ = false;
		start_time_ = 0f;
	}

	public void wait_start()
	{
		coroutineCtrl.instance.Play(wait(1f));
	}

	public void stop()
	{
		if (play_enumerator_ != null)
		{
			coroutineCtrl.instance.Stop(play_enumerator_);
			play_enumerator_ = null;
		}
		icon_[0].active = false;
		icon_[1].active = false;
		mainCtrl.instance.removeText(text_);
		active = false;
	}
}
