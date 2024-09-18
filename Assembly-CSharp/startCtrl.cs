using System.Collections;
using System.Collections.Generic;
using Steamworks;
using UnityEngine;
using UnityEngine.UI;

public class startCtrl : sceneCtrl
{
	[SerializeField]
	private Text push_text_;

	[SerializeField]
	private AnimationCurve alpha_curve_ = new AnimationCurve();

	[SerializeField]
	private textKeyIconCtrl key_icon_;

	[SerializeField]
	private Text icon_text_;

	public static startCtrl instance { get; private set; }

	public bool push_active
	{
		get
		{
			return push_text_.gameObject.activeSelf;
		}
		set
		{
			push_text_.gameObject.SetActive(value);
		}
	}

	public AnimationCurve alpha_curve
	{
		get
		{
			return alpha_curve_;
		}
	}

	private void Awake()
	{
		instance = this;
	}

	private void Init()
	{
		switch (GSStatic.global_work_.system_language)
		{
		case SystemLanguage.Japanese:
			GSStatic.global_work_.language = Language.JAPAN;
			GSStatic.option_work.language_type = 0;
			break;
		case SystemLanguage.English:
			GSStatic.global_work_.language = Language.USA;
			GSStatic.option_work.language_type = 1;
			break;
		case SystemLanguage.French:
			GSStatic.global_work_.language = Language.FRANCE;
			GSStatic.option_work.language_type = 2;
			break;
		case SystemLanguage.German:
			GSStatic.global_work_.language = Language.GERMAN;
			GSStatic.option_work.language_type = 3;
			break;
		case SystemLanguage.Korean:
			GSStatic.global_work_.language = Language.KOREA;
			GSStatic.option_work.language_type = 4;
			break;
		case SystemLanguage.ChineseSimplified:
			GSStatic.global_work_.language = Language.CHINA_S;
			GSStatic.option_work.language_type = 5;
			break;
		case SystemLanguage.ChineseTraditional:
			GSStatic.global_work_.language = Language.CHINA_T;
			GSStatic.option_work.language_type = 6;
			break;
		default:
			GSStatic.global_work_.language = Language.USA;
			GSStatic.option_work.language_type = 1;
			break;
		}
		ReplaceFont.instance.ChangeFont(GSStatic.global_work_.language);
		TextDataCtrl.SetLanguage(GSStatic.global_work_.language);
		GSStatic.save_slot_language_ = GSStatic.global_work_.language;
	}

	public override void Play()
	{
		base.End();
		enumerator_state_ = stateCoroutine();
		coroutineCtrl.instance.Play(enumerator_state_);
	}

	private IEnumerator stateCoroutine()
	{
		base.body.SetActive(true);
		Init();
		DeviceCtrlXO.instance.gamer_tag.Clear();
		padCtrl.instance.off_ = false;
		mainCtrl.instance.addText(push_text_);
		push_active = true;
		push_text_.color = new Color(1f, 1f, 1f, 0f);
		yield return coroutineCtrl.instance.Play(fadeCtrl.instance.play(0.5f, true));
		inputTextSet();
		float timer = 0f;
		while (true)
		{
			timer += 0.01f;
			timer = ((!(timer > 1f)) ? timer : 0f);
			float alpha = alpha_curve_.Evaluate(timer);
			push_text_.color = new Color(1f, 1f, 1f, alpha);
			key_icon_.key_icon[0].icon_color_ = push_text_.color;
			if (padCtrl.instance.InputGetKeyDown(KeyCode.Return) || padCtrl.instance.InControlInputWasPressedAction1() || IsTouchDown())
			{
				break;
			}
			yield return null;
		}
		push_text_.color = new Color(1f, 1f, 1f, 0f);
		key_icon_.key_icon[0].icon_color_ = push_text_.color;
		mainCtrl.instance.removeText(push_text_);
		push_active = false;
		yield return null;
		loadingCtrl.instance.init();
		loadingCtrl.instance.play(loadingCtrl.Type.LOADING);
		yield return coroutineCtrl.instance.Play(SteamCtrl.ShareFiles());
		if (SteamCtrl.IsShareFilesError)
		{
			yield return coroutineCtrl.instance.Play(loadingCtrl.instance.wait(1f));
			loadingCtrl.instance.stop();
		}
		else
		{
			SaveControl.LoadSystemDataRequest();
			while (!SaveControl.is_load_)
			{
				yield return null;
			}
			yield return coroutineCtrl.instance.Play(loadingCtrl.instance.wait(1f));
			loadingCtrl.instance.stop();
		}
		if (!SaveControl.IsExistSaveDataFile())
		{
			Debug.Log("startCtrl stateCoroutine() IsExistSaveDataFile() = False");
		}
		else if (SaveControl.is_load_error || SteamCtrl.IsShareFilesError)
		{
			fadeCtrl.instance.play(fadeCtrl.Status.FADE_OUT, 30u, 16u);
			while (!fadeCtrl.instance.is_end)
			{
				yield return null;
			}
			messageBoxCtrl.instance.init();
			messageBoxCtrl.instance.SetWindowSize(new Vector2(1200f, 360f));
			messageBoxCtrl.instance.SetText(TextDataCtrl.GetTexts(TextDataCtrl.SaveTextID.LOAD_ERROR));
			messageBoxCtrl.instance.SetTextPosCenter();
			messageBoxCtrl.instance.OpenWindowSelect();
			while (!messageBoxCtrl.instance.select_end)
			{
				yield return null;
			}
			messageBoxCtrl.instance.CloseWindowSelect();
			if (messageBoxCtrl.instance.select_cursor_no != 0)
			{
				GSStatic.init();
				TrophyCtrl.reset();
				GSStatic.global_work_.system_language = SteamCtrl.ConvertLanguage();
				Init();
				titleCtrlRoot.instance.End();
				titleCtrlRoot.instance.active = true;
				titleCtrlRoot.instance.Scene(titleCtrlRoot.SceneType.Start);
				yield break;
			}
			loadingCtrl.instance.init();
			loadingCtrl.instance.play(loadingCtrl.Type.DELETING);
			SaveControl.DeleteSaveData();
			GSStatic.init();
			TrophyCtrl.reset();
			yield return coroutineCtrl.instance.Play(loadingCtrl.instance.wait(1f));
			loadingCtrl.instance.stop();
			GSStatic.global_work_.system_language = SteamCtrl.ConvertLanguage();
			Init();
			loadingCtrl.instance.play(loadingCtrl.Type.SAVEING);
			SaveControl.SaveCreateSystemDataRequest();
			while (!SaveControl.is_save_)
			{
				yield return null;
			}
			yield return coroutineCtrl.instance.Play(loadingCtrl.instance.wait(1f));
			loadingCtrl.instance.stop();
			if (SaveControl.is_save_error)
			{
				messageBoxCtrl.instance.init();
				messageBoxCtrl.instance.SetWindowSize(new Vector2(1200f, 360f));
				messageBoxCtrl.instance.SetText(TextDataCtrl.GetTexts(TextDataCtrl.SaveTextID.CREATE_ERROR));
				messageBoxCtrl.instance.SetTextPosCenter();
				messageBoxCtrl.instance.OpenWindow();
				while (messageBoxCtrl.instance.active)
				{
					yield return null;
					if (padCtrl.instance.GetKeyDown(KeyType.A))
					{
						messageBoxCtrl.instance.CloseWindow();
					}
				}
				titleCtrlRoot.instance.End();
				titleCtrlRoot.instance.active = true;
				titleCtrlRoot.instance.Scene(titleCtrlRoot.SceneType.Start);
				yield break;
			}
		}
		if (!SaveControl.IsExistSaveDataFile() || !SaveControl.LoadSystemData())
		{
			loadingCtrl.instance.play(loadingCtrl.Type.SAVEING);
			SaveControl.SaveCreateSystemDataRequest();
			while (!SaveControl.is_save_)
			{
				yield return null;
			}
			SaveControl.SaveSystemData();
			yield return coroutineCtrl.instance.Play(loadingCtrl.instance.wait(1f));
			loadingCtrl.instance.stop();
			if (SaveControl.is_save_error)
			{
				messageBoxCtrl.instance.init();
				messageBoxCtrl.instance.SetWindowSize(new Vector2(1200f, 360f));
				messageBoxCtrl.instance.SetText(TextDataCtrl.GetTexts(TextDataCtrl.SaveTextID.CREATE_ERROR));
				messageBoxCtrl.instance.SetTextPosCenter();
				messageBoxCtrl.instance.OpenWindow();
				while (messageBoxCtrl.instance.active)
				{
					yield return null;
					if (padCtrl.instance.GetKeyDown(KeyType.A))
					{
						messageBoxCtrl.instance.CloseWindow();
					}
				}
				titleCtrlRoot.instance.End();
				titleCtrlRoot.instance.active = true;
				titleCtrlRoot.instance.Scene(titleCtrlRoot.SceneType.Start);
				yield break;
			}
		}
		uint save_data_account_id = SteamCtrl.GetSaveDataAccountID();
		uint current_account_id = SteamUser.GetSteamID().GetAccountID().m_AccountID;
		if (save_data_account_id != current_account_id)
		{
			fadeCtrl.instance.play(fadeCtrl.Status.FADE_OUT, 30u, 16u);
			while (!fadeCtrl.instance.is_end)
			{
				yield return null;
			}
			messageBoxCtrl.instance.init();
			messageBoxCtrl.instance.SetWindowSize(new Vector2(1200f, 360f));
			messageBoxCtrl.instance.SetText(TextDataCtrl.GetTexts(TextDataCtrl.SaveTextID.LOAD_ERROR));
			messageBoxCtrl.instance.SetTextPosCenter();
			messageBoxCtrl.instance.OpenWindowSelect();
			while (!messageBoxCtrl.instance.select_end)
			{
				yield return null;
			}
			messageBoxCtrl.instance.CloseWindowSelect();
			if (messageBoxCtrl.instance.select_cursor_no != 0)
			{
				GSStatic.init();
				TrophyCtrl.reset();
				GSStatic.global_work_.system_language = SteamCtrl.ConvertLanguage();
				Init();
				titleCtrlRoot.instance.End();
				titleCtrlRoot.instance.active = true;
				titleCtrlRoot.instance.Scene(titleCtrlRoot.SceneType.Start);
				yield break;
			}
			loadingCtrl.instance.init();
			loadingCtrl.instance.play(loadingCtrl.Type.DELETING);
			SaveControl.DeleteSaveData();
			GSStatic.init();
			TrophyCtrl.reset();
			yield return coroutineCtrl.instance.Play(loadingCtrl.instance.wait(1f));
			loadingCtrl.instance.stop();
			GSStatic.global_work_.system_language = SteamCtrl.ConvertLanguage();
			Init();
			loadingCtrl.instance.play(loadingCtrl.Type.SAVEING);
			SaveControl.SaveCreateSystemDataRequest();
			while (!SaveControl.is_save_)
			{
				yield return null;
			}
			yield return coroutineCtrl.instance.Play(loadingCtrl.instance.wait(1f));
			loadingCtrl.instance.stop();
			if (SaveControl.is_save_error)
			{
				messageBoxCtrl.instance.init();
				messageBoxCtrl.instance.SetWindowSize(new Vector2(1200f, 360f));
				messageBoxCtrl.instance.SetText(TextDataCtrl.GetTexts(TextDataCtrl.SaveTextID.CREATE_ERROR));
				messageBoxCtrl.instance.SetTextPosCenter();
				messageBoxCtrl.instance.OpenWindow();
				while (messageBoxCtrl.instance.active)
				{
					yield return null;
					if (padCtrl.instance.GetKeyDown(KeyType.A))
					{
						messageBoxCtrl.instance.CloseWindow();
					}
				}
				titleCtrlRoot.instance.End();
				titleCtrlRoot.instance.active = true;
				titleCtrlRoot.instance.Scene(titleCtrlRoot.SceneType.Start);
				yield break;
			}
		}
		if (GSStatic.save_ver > systemCtrl.instance.save_ver)
		{
			Debug.Log("Up Ver Seve Data!!");
			loadingCtrl.instance.play(loadingCtrl.Type.DIFF);
			yield return coroutineCtrl.instance.Play(loadingCtrl.instance.wait(3f));
			loadingCtrl.instance.stop();
			yield return coroutineCtrl.instance.Play(fadeCtrl.instance.play(0.5f, false));
			base.body.SetActive(false);
			titleCtrlRoot.instance.Scene(titleCtrlRoot.SceneType.Start);
			yield break;
		}
		ScreenUtility.SetVsync(GSStatic.option_work.vsync);
		if (GSStatic.option_work.resolution_w != 0 && GSStatic.option_work.resolution_h != 0)
		{
			Resolution[] resolutions = Screen.resolutions;
			List<Vector2Int> list = new List<Vector2Int>();
			Resolution[] array = resolutions;
			for (int i = 0; i < array.Length; i++)
			{
				Resolution resolution = array[i];
				if (resolution.height % 9 == 0 && resolution.width % 16 == 0)
				{
					list.Add(new Vector2Int(resolution.width, resolution.height));
				}
			}
			Vector2Int vector2Int = ((list.Count <= 0) ? new Vector2Int(0, 0) : list[list.Count - 1]);
			if (vector2Int.x < (int)GSStatic.option_work.resolution_w || vector2Int.y < (int)GSStatic.option_work.resolution_h)
			{
				GSStatic.option_work.resolution_w = 1280u;
				GSStatic.option_work.resolution_h = 720u;
			}
			ScreenUtility.SetResolution((int)GSStatic.option_work.resolution_w, (int)GSStatic.option_work.resolution_h, (GSStatic.option_work.window_mode != 0) ? true : false);
			list.Clear();
		}
		else
		{
			GSStatic.option_work.resolution_w = 1280u;
			GSStatic.option_work.resolution_h = 720u;
			Resolution[] resolutions2 = Screen.resolutions;
			for (int j = 0; j < resolutions2.Length; j++)
			{
				Resolution resolution2 = resolutions2[j];
				float num = (float)resolution2.height / ((float)resolution2.width / 16f);
				int num2 = resolution2.width / 16 * 9;
				Debug.Log("width:" + resolution2.width + " height:" + resolution2.height + "(" + num2 + ") refreshRate:" + resolution2.refreshRate + "ratio 16:" + num);
				if (resolution2.height == num2)
				{
					GSStatic.option_work.resolution_w = (uint)resolution2.width;
					GSStatic.option_work.resolution_h = (uint)resolution2.height;
					break;
				}
			}
			ScreenUtility.SetResolution((int)GSStatic.option_work.resolution_w, (int)GSStatic.option_work.resolution_h, (GSStatic.option_work.window_mode != 0) ? true : false);
		}
		yield return coroutineCtrl.instance.Play(fadeCtrl.instance.play(0.5f, false));
		base.body.SetActive(false);
		titleCtrlRoot.instance.Scene(titleCtrlRoot.SceneType.Top);
	}

	public void keyIconSet(string original_text)
	{
		float preferredWidth = push_text_.preferredWidth;
		int num = original_text.IndexOf("【");
		if (num < 0)
		{
			icon_text_.text = original_text;
		}
		else
		{
			icon_text_.text = original_text.Remove(original_text.IndexOf("【"));
		}
		float preferredWidth2 = icon_text_.preferredWidth;
		icon_text_.text += key_icon_.changeTextToIconSpase(string.Empty);
		float preferredWidth3 = icon_text_.preferredWidth;
		float x = (preferredWidth2 - preferredWidth / 2f + (preferredWidth3 - preferredWidth / 2f)) / 2f;
		key_icon_.load(0);
		key_icon_.iconSet(KeyCode.Return);
		key_icon_.iconPosSet(push_text_.transform, new Vector3(x, 0f, 0f));
	}

	private void inputTextSet()
	{
		TextDataCtrl.TitleTextID in_text_id = TextDataCtrl.TitleTextID.START_INPUT;
		string text = MessageSystem.EnToHalf(TextDataCtrl.GetText(in_text_id), Language.USA);
		push_text_.text = text.Replace("【】", key_icon_.changeTextToIconSpase(string.Empty));
		string text2 = push_text_.text;
		keyIconSet(text);
	}
}
