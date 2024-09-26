using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class signedInCtrl : MonoBehaviour
{
	private static signedInCtrl instance_;

	private const int DEFAULT_FONT_SIZE = 40;

	private const int FRANCE_FONT_SIZE = 38;

	[SerializeField]
	private GameObject body_;

	[SerializeField]
	private AssetBundleSprite window_;

	[SerializeField]
	private AssetBundleSprite mask_;

	[SerializeField]
	private Text text_;

	private IEnumerator play_enumerator_;

	private bool is_close_ = true;

	private bool is_play_change_;

	public static signedInCtrl instance
	{
		get
		{
			return instance_;
		}
	}

	public bool is_close
	{
		get
		{
			return is_close_;
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

	private float text_lineSpacing_
	{
		get
		{
			string lang = Language.langFallback[GSStatic.global_work_.language].ToUpper();
			switch (lang)
			{
			case "KOREA":
				return 1f;
			case "CHINA_S":
				return 1f;
			case "CHINA_T":
				return 1f;
			default:
				return 0.8f;
			}
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
		FontSizeSet(GSStatic.global_work_.language);
		text_.text = TextDataCtrl.StringArrayToString(TextDataCtrl.GetTexts(TextDataCtrl.SystemTextID.DISCONNECT_GAME_PAD));
		text_.lineSpacing = text_lineSpacing_;
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
			coroutineCtrl.instance.Stop(play_enumerator_);
			play_enumerator_ = null;
		}
		play_enumerator_ = OpenCoroutine();
		coroutineCtrl.instance.Play(play_enumerator_);
	}

	private void FontSizeSet(string language)
	{
		int fontSize = ((language != "FRANCE") ? 40 : 38);
		text_.fontSize = fontSize;
	}

	public void close()
	{
		is_close_ = true;
	}
}
