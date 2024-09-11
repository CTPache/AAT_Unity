using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SaveConfirmationWindow : MonoBehaviour
{
	[Serializable]
	public class ConfirmationWindow
	{
		public AssetBundleSprite window_;

		public AssetBundleSprite anim_;

		public SaveSlot slot_;

		public titleSelectPlate select_plate_;

		public Text confirmation_text_;

		public Text save_text_;
	}

	private float start_time_;

	private SaveLoadUICtrl.SlotType type_;

	private int slot_num_;

	private string[] confirmation_text_;

	private string[] io_text_;

	private bool is_save;

	[SerializeField]
	private GameObject body_;

	[SerializeField]
	private ConfirmationWindow window_;

	public bool is_open { get; private set; }

	public bool is_loaded { get; private set; }

	private float save_text_lineSpacing_
	{
		get
		{
			switch (GSStatic.global_work_.language)
			{
			case Language.FRANCE:
				return 0.65f;
			case Language.GERMAN:
				return 0.65f;
			case Language.KOREA:
				return 1f;
			case Language.CHINA_S:
				return 1f;
			case Language.CHINA_T:
				return 0.8f;
			default:
				return 0.5f;
			}
		}
	}

	public void Load()
	{
		if (type_ == SaveLoadUICtrl.SlotType.SAVE)
		{
			window_.anim_.load("/menu/common/", "writing");
		}
		else
		{
			window_.anim_.load("/menu/common/", "loading");
		}
		window_.window_.load("/menu/common/", "save_window");
		window_.slot_.Load();
	}

	public void Init(SaveLoadUICtrl.SlotType in_type)
	{
		type_ = in_type;
		Load();
		string empty = string.Empty;
		string empty2 = string.Empty;
		empty = TextDataCtrl.GetText(TextDataCtrl.SaveTextID.OVERWRITE);
		empty2 = TextDataCtrl.GetText(TextDataCtrl.SaveTextID.SELECT_CONFIRM);
		confirmation_text_ = new string[2] { empty, empty2 };
		string empty3 = string.Empty;
		string empty4 = string.Empty;
		empty3 = TextDataCtrl.GetText(TextDataCtrl.SaveTextID.SAVING_STEAM) + "\n" + TextDataCtrl.GetText(TextDataCtrl.SaveTextID.SAVING_STEAM, 1);
		empty4 = TextDataCtrl.GetText(TextDataCtrl.SaveTextID.LOADDING);
		io_text_ = new string[2] { empty3, empty4 };
		window_.window_.sprite_renderer_.size = new Vector2(1114f, 584f);
		window_.confirmation_text_.text = confirmation_text_[(int)type_];
		window_.save_text_.text = io_text_[(int)type_];
		window_.save_text_.lineSpacing = save_text_lineSpacing_;
		window_.select_plate_.mainTitleInit(new string[2]
		{
			TextDataCtrl.GetText(TextDataCtrl.TitleTextID.YES),
			TextDataCtrl.GetText(TextDataCtrl.TitleTextID.NO)
		}, "select_button", 1);
		window_.anim_.active = false;
		is_open = false;
		is_loaded = false;
		body_.SetActive(false);
	}

	public void OpenWindow(int slot_num, bool is_confirmation)
	{
		mainCtrl.instance.addText(window_.confirmation_text_);
		mainCtrl.instance.addText(window_.save_text_);
		body_.SetActive(true);
		is_open = true;
		slot_num_ = slot_num;
		window_.slot_.SlotDataSet(slot_num, type_);
		mainCtrl.instance.addText(window_.slot_.slot.no_data_);
		mainCtrl.instance.addText(window_.slot_.slot.number);
		mainCtrl.instance.addText(window_.slot_.slot.progress);
		mainCtrl.instance.addText(window_.slot_.slot.scenario_);
		mainCtrl.instance.addText(window_.slot_.slot.time_);
		mainCtrl.instance.addText(window_.slot_.slot.title_);
		window_.confirmation_text_.gameObject.SetActive(true);
		window_.save_text_.gameObject.SetActive(false);
		window_.anim_.active = false;
		if (is_confirmation)
		{
			coroutineCtrl.instance.Play(CoroutineConfirmation());
		}
		else
		{
			coroutineCtrl.instance.Play(CoroutineNotConfirmation());
		}
	}

	private IEnumerator CoroutineConfirmation()
	{
		window_.select_plate_.body_active = true;
		window_.select_plate_.playCursor();
		while (true)
		{
			if (window_.select_plate_.is_end)
			{
				if (window_.select_plate_.cursor_no == 0)
				{
					IEnumerator coroutine = ((type_ != 0) ? CoroutineLoad() : CoroutineSave());
					yield return coroutineCtrl.instance.Play(coroutine);
				}
				else
				{
					body_.SetActive(false);
				}
				break;
			}
			if (!window_.select_plate_.is_play_animation && padCtrl.instance.GetKeyDown(KeyType.B))
			{
				soundCtrl.instance.PlaySE(44);
				window_.select_plate_.StopAllCoroutines();
				window_.select_plate_.End();
				body_.SetActive(false);
				break;
			}
			yield return null;
		}
		CloseWindow();
	}

	private IEnumerator CoroutineNotConfirmation()
	{
		window_.select_plate_.body_active = false;
		IEnumerator coroutine = ((type_ != 0) ? CoroutineLoad() : CoroutineSave());
		yield return coroutineCtrl.instance.Play(coroutine);
		CloseWindow();
	}

	private void CloseWindow()
	{
		mainCtrl.instance.removeText(window_.confirmation_text_);
		mainCtrl.instance.removeText(window_.save_text_);
		mainCtrl.instance.removeText(window_.slot_.slot.no_data_);
		mainCtrl.instance.removeText(window_.slot_.slot.number);
		mainCtrl.instance.removeText(window_.slot_.slot.progress);
		mainCtrl.instance.removeText(window_.slot_.slot.scenario_);
		mainCtrl.instance.removeText(window_.slot_.slot.time_);
		mainCtrl.instance.removeText(window_.slot_.slot.title_);
		is_open = false;
	}

	public void End()
	{
		body_.SetActive(false);
	}

	public void LoadingPlay()
	{
		is_save = true;
		coroutineCtrl.instance.Play(CoroutineSaveAnim());
		start_time_ = Time.time;
	}

	public IEnumerator LoadingWait()
	{
		while (true)
		{
			float time = Time.time - start_time_;
			if (time > 1f)
			{
				break;
			}
			yield return null;
		}
		start_time_ = 0f;
	}

	public void LoadingStop()
	{
		is_save = false;
	}

	private IEnumerator CoroutineSaveAnim()
	{
		int anim_max = window_.anim_.sprite_data_.Count - 1;
		int anim_no2 = 0;
		int cnt = 0;
		window_.confirmation_text_.gameObject.SetActive(false);
		window_.save_text_.gameObject.SetActive(true);
		window_.anim_.active = true;
		while (is_save)
		{
			cnt++;
			if (cnt > 7)
			{
				anim_no2++;
				anim_no2 = ((anim_no2 <= anim_max) ? anim_no2 : 0);
				window_.anim_.spriteNo(anim_no2);
				cnt = 0;
			}
			yield return null;
		}
	}

	private IEnumerator CoroutineSave()
	{
		LoadingPlay();
		SaveData save_slot_temp = new SaveData
		{
			time = GSStatic.save_data[slot_num_].time,
			title = GSStatic.save_data[slot_num_].title,
			scenario = GSStatic.save_data[slot_num_].scenario,
			progress = GSStatic.save_data[slot_num_].progress,
			in_data = GSStatic.save_data[slot_num_].in_data
		};
		string time_text;
		switch (GSStatic.global_work_.language)
		{
		case Language.FRANCE:
			time_text = DateTime.Now.ToString("dd/MM/yyyy\nHH:mm:ss");
			break;
		case Language.GERMAN:
			time_text = DateTime.Now.ToString("dd.MM.yyyy\nHH:mm:ss");
			break;
		default:
			time_text = DateTime.Now.ToString("yyyy/MM/dd\nHH:mm:ss");
			break;
		}
		GSStatic.save_data[slot_num_].time = time_text;
		GSStatic.save_data[slot_num_].title = (ushort)GSStatic.global_work_.title;
		GSStatic.save_data[slot_num_].scenario = GSStatic.global_work_.story;
		GSStatic.save_data[slot_num_].progress = GSStatic.global_work_.scenario;
		GSStatic.save_data[slot_num_].in_data = 1;
		SaveControl.SaveGameDataRequest(GSUtility.GetLanguageSlotNum(slot_num_, GSStatic.global_work_.language));
		while (!SaveControl.is_save_)
		{
			yield return null;
		}
		SaveControl.SaveGameData();
		if (SaveControl.is_save_error)
		{
			GSStatic.save_data[slot_num_].time = save_slot_temp.time;
			GSStatic.save_data[slot_num_].title = save_slot_temp.title;
			GSStatic.save_data[slot_num_].scenario = save_slot_temp.scenario;
			GSStatic.save_data[slot_num_].progress = save_slot_temp.progress;
			GSStatic.save_data[slot_num_].in_data = save_slot_temp.in_data;
		}
		yield return coroutineCtrl.instance.Play(LoadingWait());
		LoadingStop();
		if (SaveControl.is_save_error)
		{
			messageBoxCtrl.instance.init();
			messageBoxCtrl.instance.SetWindowSize(new Vector2(1200f, 360f));
			messageBoxCtrl.instance.SetText(TextDataCtrl.GetTexts(TextDataCtrl.SaveTextID.SAVE_ERROR));
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
		}
		End();
	}

	private IEnumerator CoroutineLoad()
	{
		is_loaded = true;
		yield return null;
	}
}
