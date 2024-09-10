using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class signedInCtrl : MonoBehaviour
{
	private static signedInCtrl instance_;

	[SerializeField]
	private GameObject body_;

	[SerializeField]
	private AssetBundleSprite window_;

	[SerializeField]
	private AssetBundleSprite mask_;

	[SerializeField]
	private Text text_;

	private IEnumerator play_enumerator_;

	private bool is_close_;

	private bool is_play_change_;

	public static signedInCtrl instance
	{
		get
		{
			return instance_;
		}
	}

	public bool is_play_change
	{
		get
		{
			return is_play_change_;
		}
	}

	private bool active
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

	private void Awake()
	{
		if (instance_ == null)
		{
			instance_ = this;
		}
	}

	private IEnumerator OpenCoroutine()
	{
		text_.text = TextDataCtrl.StringArrayToString(TextDataCtrl.GetTexts(TextDataCtrl.SystemTextID.DISCONNECT_GAME_PAD));
		active = true;
		is_close_ = false;
		while (!is_close_)
		{
			yield return null;
		}
		active = false;
		play_enumerator_ = null;
	}

	public void init()
	{
		window_.load("/menu/common/", "save_window");
		mask_.load("/menu/common/", "mask");
		window_.sprite_renderer_.size = new Vector2(1200f, 360f);
	}

	public void open()
	{
		if (play_enumerator_ != null)
		{
			StopCoroutine(play_enumerator_);
			play_enumerator_ = null;
		}
		play_enumerator_ = OpenCoroutine();
		StartCoroutine(play_enumerator_);
	}

	public void close()
	{
		is_close_ = true;
	}
}
