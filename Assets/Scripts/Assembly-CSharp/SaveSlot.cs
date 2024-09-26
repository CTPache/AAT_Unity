using System;
using UnityEngine;
using UnityEngine.UI;

public class SaveSlot : MonoBehaviour
{
	[Serializable]
	public class Slot
	{
		public AssetBundleSprite board_;

		public Text number;

		public AssetBundleSprite num_bg_;

		public Text time_;

		public Text title_;

		public Text scenario_;

		public Text progress;

		public Text no_data_;

		public bool save_active
		{
			get
			{
				return time_.gameObject.activeSelf & title_.gameObject.activeSelf & scenario_.gameObject.activeSelf & progress.gameObject.activeSelf;
			}
			set
			{
				time_.gameObject.SetActive(value);
				title_.gameObject.SetActive(value);
				scenario_.gameObject.SetActive(value);
				progress.gameObject.SetActive(value);
			}
		}
	}

	private Color num_color = Color.white;

	private const int DEFAULT_FONT_SIZE = 44;

	private const int FRANCE_FONT_SIZE = 40;

	private Vector3 slot_num_def_pos_ = default(Vector3);

	[SerializeField]
	private Slot slot_;

	[SerializeField]
	[Tooltip("日本語版\nタイトル名のフォントサイズ")]
	private int title_text_fontsize_jpn_ = 40;

	[SerializeField]
	[Tooltip("英語版\nタイトル名のフォントサイズ")]
	private int title_text_fontsize_usa_ = 38;

	public Slot slot
	{
		get
		{
			return slot_;
		}
	}

	private int title_text_fontsize_
	{
		get
		{
			switch (GSUtility.GetLanguageLayoutType(GSStatic.global_work_.language))
			{
			case "JAPAN":
				return title_text_fontsize_jpn_;
			case "USA":
				return title_text_fontsize_usa_;
			default:
				return title_text_fontsize_usa_;
			}
		}
	}

	private float time_text_lineSpacing_
	{
		get
		{
			string lang = Language.langFallback[GSStatic.global_work_.language].ToUpper();
			switch (lang)
			{
			case "KOREA":
				return 0.8f;
			case "CHINA_S":
				return 0.7f;
			default:
				return 0.6f;
			}
		}
	}

	public void Awake()
	{
		slot_num_def_pos_ = slot.number.gameObject.transform.localPosition;
	}

	public void Load()
	{
		slot_.board_.load("/menu/common/", "save_data_window");
		slot_.num_bg_.load("/menu/common/", "save_data_slot");
		slot.title_.fontSize = title_text_fontsize_;
	}

	public void SlotDataSet(int num, SaveLoadUICtrl.SlotType type)
	{
		slot.board_.color = Color.white;
		slot.number.color = num_color;
		mainCtrl.instance.addText(slot.no_data_);
		mainCtrl.instance.addText(slot.number);
		mainCtrl.instance.addText(slot.progress);
		mainCtrl.instance.addText(slot.scenario_);
		mainCtrl.instance.addText(slot.time_);
		mainCtrl.instance.addText(slot.title_);
        //Debug.Log("SlotDataSet num: " + num);
        if (GSStatic.save_data[num].in_data > 0)
		{
			slot.board_.spriteNo(3);
			slot.save_active = true;
			slot.no_data_.gameObject.SetActive(false);
			SlotTextSet(num);
			TitleSpriteSet(num);
		}
		else
		{
			slot.board_.spriteNo(2);
			slot.save_active = false;
			slot.no_data_.gameObject.SetActive(true);
			slot_.num_bg_.active = false;
			slot.no_data_.text = TextDataCtrl.GetText(TextDataCtrl.SaveTextID.NO_DATA);
			if (type == SaveLoadUICtrl.SlotType.LOAD)
			{
				Color color = new Color(0.5f, 0.5f, 0.5f, 1f);
				slot.board_.color *= color;
				slot.number.color *= color;
			}
		}
		num++;
		slot.number.text = num.ToString();
		Vector3 localPosition = slot.number.gameObject.transform.localPosition;
		if (num >= 10)
		{
			localPosition.x = slot_num_def_pos_.x + -3.5f;
		}
		else
		{
			localPosition.x = slot_num_def_pos_.x;
		}
		slot.number.gameObject.transform.localPosition = localPosition;
	}

	private void TitleSpriteSet(int slot_num)
	{
		slot_.num_bg_.active = true;
		slot_.number.color = Color.white;
		switch ((TitleId)GSStatic.save_data[slot_num].title)
		{
		case TitleId.GS1:
			slot_.num_bg_.spriteNo(0);
			break;
		case TitleId.GS2:
			slot_.num_bg_.spriteNo(1);
			break;
		case TitleId.GS3:
			slot_.num_bg_.spriteNo(2);
			break;
		}
	}

	private void SlotTextSet(int slot_num)
	{
		if (GSStatic.global_work_.language == "FRANCE")
		{
			slot.scenario_.fontSize = 40;
		}
		else
		{
			slot.scenario_.fontSize = 44;
		}
		slot.time_.lineSpacing = time_text_lineSpacing_;
		slot.time_.text = GSStatic.save_data[slot_num].time;
		slot.title_.text = GetTitleText((TitleId)GSStatic.save_data[slot_num].title);
		slot.scenario_.text = GetScenarioText((TitleId)GSStatic.save_data[slot_num].title, GSStatic.save_data[slot_num].scenario);
		slot.progress.text = GetProgressText((TitleId)GSStatic.save_data[slot_num].title, GSStatic.save_data[slot_num].progress);
	}

	private string GetTitleText(TitleId in_title)
	{
		return TextDataCtrl.GetText(TextDataCtrl.TitleTextID.TITLE_NAME, (int)in_title);
	}

	private string GetScenarioText(TitleId in_title, int in_scenario)
	{
		if (in_title == TitleId.GS2 && in_scenario >= 4)
		{
			return string.Empty;
		}
		TextDataCtrl.TitleTextID in_text_id;
		switch (in_title)
		{
		case TitleId.GS1:
			in_text_id = TextDataCtrl.TitleTextID.GS1_SCENARIO_NAME;
			break;
		case TitleId.GS2:
			in_text_id = TextDataCtrl.TitleTextID.GS2_SCENARIO_NAME;
			break;
		case TitleId.GS3:
			in_text_id = TextDataCtrl.TitleTextID.GS3_SCENARIO_NAME;
			break;
		default:
			return string.Empty;
		}
		string text = TextDataCtrl.GetText(TextDataCtrl.TitleTextID.EPISODE_NUMBER, in_scenario);
		string text2 = TextDataCtrl.GetText(in_text_id, in_scenario);
		return text + "\u3000" + text2;
	}

	private string GetProgressText(TitleId in_title, int in_progress)
	{
		int in_text_id = 0;
		switch (in_title)
		{
		case TitleId.GS1:
			switch (in_progress)
			{
			case 17:
				in_text_id = 34;
				break;
			case 18:
				in_text_id = 34;
				break;
			case 19:
				in_text_id = 35;
				break;
			case 20:
				in_text_id = 35;
				break;
			case 21:
				in_text_id = 36;
				break;
			case 22:
				in_text_id = 37;
				break;
			case 23:
				in_text_id = 37;
				break;
			case 24:
				in_text_id = 37;
				break;
			case 25:
				in_text_id = 38;
				break;
			case 26:
				in_text_id = 39;
				break;
			case 27:
				in_text_id = 39;
				break;
			case 28:
				in_text_id = 40;
				break;
			case 29:
				in_text_id = 40;
				break;
			case 30:
				in_text_id = 40;
				break;
			case 31:
				in_text_id = 41;
				break;
			case 32:
				in_text_id = 42;
				break;
			case 33:
				in_text_id = 43;
				break;
			case 34:
				in_text_id = 43;
				break;
			default:
				in_text_id = 17 + in_progress;
				break;
			}
			break;
		case TitleId.GS2:
			in_text_id = 44 + in_progress;
			break;
		case TitleId.GS3:
			in_text_id = 66 + in_progress;
			break;
		}
		return TextDataCtrl.GetText((TextDataCtrl.SaveTextID)in_text_id);
	}

	public void End()
	{
		mainCtrl.instance.removeText(slot.no_data_);
		mainCtrl.instance.removeText(slot.number);
		mainCtrl.instance.removeText(slot.progress);
		mainCtrl.instance.removeText(slot.scenario_);
		mainCtrl.instance.removeText(slot.time_);
		mainCtrl.instance.removeText(slot.title_);
	}
}
