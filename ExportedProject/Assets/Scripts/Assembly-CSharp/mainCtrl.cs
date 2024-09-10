using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class mainCtrl : MonoBehaviour
{
	private static mainCtrl instance_;

	[SerializeField]
	private DebugMemory debug_memory_;

	[SerializeField]
	private Text text_;

	public static mainCtrl instance
	{
		get
		{
			return instance_;
		}
	}

	public DebugMemory debug_memory
	{
		get
		{
			return debug_memory_;
		}
	}

	public Text text
	{
		get
		{
			return text_;
		}
	}

	private void Awake()
	{
		if (instance_ == null)
		{
			instance_ = this;
		}
	}

	private void Start()
	{
		StartCoroutine(SystemInitialization());
		MouseCursorMonitoringCtrl mouseCursorMonitoringCtrl = base.gameObject.AddComponent<MouseCursorMonitoringCtrl>();
		mouseCursorMonitoringCtrl.RegisterHideCursorTimeOverCallBack(delegate(bool p)
		{
			Cursor.visible = p;
		});
		mouseCursorMonitoringCtrl.hide_cursor_time = 2f;
	}

	private void Update()
	{
		while (true)
		{
			GSMain.MainLoop();
			if (MessageSystem.GetActiveMessageWork().code == 27 && !bgCtrl.instance.is_scrolling_court)
			{
				MessageSystem.GetActiveMessageWork().code = 0;
				continue;
			}
			break;
		}
	}

	private void reset()
	{
		GlobalWork global_work_ = GSStatic.global_work_;
		if (global_work_.language == Language.JAPAN)
		{
			global_work_.language = Language.USA;
		}
		else if (global_work_.language == Language.USA)
		{
			global_work_.language = Language.JAPAN;
		}
		else
		{
			global_work_.language = Language.JAPAN;
		}
		messageBoardCtrl.instance.init();
		titleCtrlRoot.instance.active = true;
		titleCtrlRoot.instance.Scene(titleCtrlRoot.SceneType.Logo);
	}

	private IEnumerator SystemInitialization()
	{
		ScreenUtility.Init();
		GSStatic.init();
		GSStatic.global_work_.Random_seed = 3383;
		GSStatic.global_work_.system_language = SteamCtrl.ConvertLanguage();
		AnimationIdentifier.instance.LoadDictionary();
		yield return new WaitWhile(() => !AnimationIdentifier.instance.isInitialized);
		AssetBundleCtrl.instance.load("/menu/common/", "select_button", true);
		AssetBundleCtrl.instance.load("/menu/common/", "select_window", true);
		AssetBundleCtrl.instance.load("/menu/option/", "option_count_bg01", true);
		AssetBundleCtrl.instance.load("/menu/option/", "option_button", true);
		AssetBundleCtrl.instance.load("/menu/option/", "option_gauge_bar", true);
		AssetBundleCtrl.instance.load("/menu/common/", "mask", true);
		AssetBundleCtrl.instance.load("/menu/title/", "title_text_back", true);
		AssetBundleCtrl.instance.load("/menu/title/", "def_title", true);
		AssetBundleCtrl.instance.load("/menu/common/", "writing", true);
		AssetBundleCtrl.instance.load("/menu/common/", "loading", true);
		AssetBundleCtrl.instance.load("/menu/common/", "save_window", true);
		AssetBundleCtrl.instance.load("/menu/common/", "save_data_slot", true);
		AssetBundleCtrl.instance.load("/menu/common/", "save_data_window", true);
		AssetBundleCtrl.instance.load("/menu/common/", "scrollbar", true);
		AssetBundleCtrl.instance.load("/menu/common/", "scrollbar_frame", true);
		AssetBundleCtrl.instance.load("/GS1/BG/", "bg043", true);
		AssetBundleCtrl.instance.load("/menu/common/", "menu_bg", true);
		string in_name = "symbol" + GSUtility.GetPlatformResourceName();
		AssetBundleCtrl.instance.load("/menu/common/", in_name, true);
		AssetBundleCtrl.instance.load("/menu/common/", "symbol_xbox", true);
		advCtrl.instance.init();
		vibrationCtrl.instance.init();
		signedInCtrl.instance.init();
		titleGuideCtrl.instance.init();
		fadeCtrl.instance.fade_target.Clear();
		fadeCtrl.instance.fade_target.Add(fadeCtrl.instance.target_list[0]);
		fadeCtrl.instance.fade_target.Add(fadeCtrl.instance.target_list[1]);
		initTexture();
		fadeCtrl.instance.play(30, true);
		while (!fadeCtrl.instance.is_end)
		{
			yield return null;
		}
		titleCtrlRoot.instance.active = true;
		bool flag = false;
		if (SteamCtrl.IsJapanIP() && GSStatic.global_work_.system_language == SystemLanguage.Japanese)
		{
			flag = true;
		}
		if (flag)
		{
			titleCtrlRoot.instance.Scene(titleCtrlRoot.SceneType.Caution);
		}
		else
		{
			titleCtrlRoot.instance.Scene(titleCtrlRoot.SceneType.Logo);
		}
	}

	private IEnumerator stateCoroutine()
	{
		fadeCtrl.instance.play(30, true);
		while (!fadeCtrl.instance.is_end)
		{
			yield return null;
		}
		yield return null;
	}

	private void DebugKeyDisp()
	{
		string empty = string.Empty;
		empty += "Key InPut\n";
		empty += "GetKey\n";
		foreach (KeyType value in Enum.GetValues(typeof(KeyType)))
		{
			if (value != 0 && padCtrl.instance.GetKey(value))
			{
				empty = empty + " " + value;
			}
		}
		empty += "\n";
		empty += "GetKeyDown\n";
		foreach (KeyType value2 in Enum.GetValues(typeof(KeyType)))
		{
			if (value2 != 0 && padCtrl.instance.GetKeyDown(value2))
			{
				empty = empty + " " + value2;
			}
		}
		empty += "\n";
		empty += "GetKeyUp\n";
		foreach (KeyType value3 in Enum.GetValues(typeof(KeyType)))
		{
			if (value3 != 0 && padCtrl.instance.GetKeyUp(value3))
			{
				empty = empty + " " + value3;
			}
		}
		empty += "\n";
		empty += "\n";
		string text = empty;
		empty = text + "L2:" + padCtrl.instance.InputGetAxisRaw("L2") + "\n";
		text = empty;
		empty = text + "R2:" + padCtrl.instance.InputGetAxisRaw("R2") + "\n";
		if (padCtrl.instance.InputAnyKey())
		{
			foreach (KeyCode value4 in Enum.GetValues(typeof(KeyCode)))
			{
				if (padCtrl.instance.InputGetKey(value4))
				{
					empty = string.Concat(empty, value4, "\n");
				}
			}
		}
		text_.text = empty;
	}

	public void addText(Text text)
	{
	}

	public void removeText(Text text)
	{
	}

	private void initTexture()
	{
	}

	private void rebuild(Font font)
	{
	}
}
