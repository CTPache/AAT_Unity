using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


public class mainTitleCtrl : sceneCtrl
{
	[Serializable]
	public class MainTitle
	{
		public AssetBundleSprite title_;

		public AssetBundleSprite copyright_;

		public AssetBundleSprite title_back_;

		public bool active
		{
			get
			{
				return title_.active & copyright_.active & title_back_.active;
			}
			set
			{
				title_.active = value;
				copyright_.active = value;
				title_back_.active = value;
			}
		}
	}

	private string[][] select_text_;

	private int select_type_;

	[SerializeField]
	private MainTitle main_title_;

	[SerializeField]
	private titleSelectPlate select_plate_;

	[SerializeField]
	private Text push_text_;

	[SerializeField]
	private AnimationCurve alpha_curve_ = new AnimationCurve();

	[SerializeField]
	private titleSelectPlate game_end_select_;

	[SerializeField]
	private AssetBundleSprite mask_;

	[SerializeField]
	private AssetBundleSprite message_window_;

	[SerializeField]
	private List<Text> message_text_list_ = new List<Text>();

	[SerializeField]
	private Text select_plate_text_00_;

	[SerializeField]
	private Text select_plate_text_01_;

	[SerializeField]
	private Text select_plate_text_02_;

	public static mainTitleCtrl instance { get; private set; }

	public bool body_active
	{
		get
		{
			return base.body.gameObject.activeSelf;
		}
		set
		{
			base.body.gameObject.SetActive(value);
		}
	}

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

	public bool load { get; set; }

	private void Awake()
	{
		instance = this;
	}

	public override void Play()
	{
		End();
		enumerator_state_ = stateCoroutine();
		coroutineCtrl.instance.Play(enumerator_state_);
	}

	public void Init()
	{
		select_type_ = 0;
		GSStatic.trophy_manager.Init();
		GSStatic.trophy_manager.GetTrophyData();
		optionCtrl.instance.OptionSet();
		ReplaceFont.instance.ChangeFont(GSStatic.global_work_.language);
		TextDataCtrl.SetLanguage(GSStatic.global_work_.language);
		GSStatic.save_slot_language_ = GSStatic.global_work_.language;
		if (GSStatic.global_work_.language != "JAPAN" && GSStatic.global_work_.language != "USA")
		{
			if (GSStatic.global_work_.language == "FRANCE" || GSStatic.global_work_.language == "CHINA_S")
			{
				systemCtrl.instance.EnableDoubleQuotationAdjustoment(true);
			}
			else
			{
				systemCtrl.instance.EnableDoubleQuotationAdjustoment(false);
			}
		}
		changeText();
		mainCtrl.instance.addText(select_plate_text_00_);
		mainCtrl.instance.addText(select_plate_text_01_);
		mainCtrl.instance.addText(select_plate_text_02_);
		message_window_.active = true;
		message_window_.load("/menu/common/", "talk_bg");
		message_window_.active = false;
		game_end_select_.body_active = true;
		game_end_select_.mainTitleInit(new string[2]
		{
			TextDataCtrl.GetText(TextDataCtrl.TitleTextID.YES),
			TextDataCtrl.GetText(TextDataCtrl.TitleTextID.NO)
		}, "select_button", 1);
		game_end_select_.body_active = false;
		mask_.active = true;
		mask_.load("/menu/common/", "mask");
		mask_.active = false;
		base.body.SetActive(true);
		main_title_.active = true;
		load = false;
		GSStatic.global_work_.lifegauge_init_hp();
        main_title_.copyright_.load("/menu/title/", "copy", false);
        main_title_.copyright_.spriteNo(0);
        main_title_.copyright_.sprite_data_.Clear();
        main_title_.title_back_.load("/menu/title/", "title_back");
        select_plate_.body_active = false;
		soundCtrl.instance.PlayBGM(400);
		advCtrl.instance.sub_window_.req_ = SubWindow.Req.NONE;
		advCtrl.instance.sub_window_.busy_ = 0u;
	}

    public void loadLang()
    {
        //AssetBundleCtrl.instance.reloadAll(true);

        main_title_.title_.load("/menu/title/", "title" + GSUtility.GetResourceNameLanguage(GSStatic.global_work_.language), false, true);
        main_title_.title_.spriteNo(0);
        main_title_.title_.sprite_data_.Clear();

        main_title_.copyright_.load("/menu/title/", "copy", false, true);
        main_title_.copyright_.spriteNo(0);
        main_title_.copyright_.sprite_data_.Clear();
    }

    public override void End()
	{
		select_plate_.End();
		base.End();
		mainCtrl.instance.removeText(select_plate_text_00_);
		mainCtrl.instance.removeText(select_plate_text_01_);
		mainCtrl.instance.removeText(select_plate_text_02_);
	}

	private IEnumerator stateCoroutine()
	{
		Init();
		fadeCtrl.instance.play(fadeCtrl.Status.FADE_IN, 30u, 16u);
		while (!fadeCtrl.instance.is_end)
		{
			yield return null;
		}
		if (GSStatic.save_data.Where((SaveData data) => data.in_data > 0).Count() > 0)
		{
			select_type_ = 1;
		}
		select_plate_.mainTitleInit(select_text_[select_type_], "select_button", select_type_);
		select_plate_.body_active = true;
		select_plate_.playCursor(0);
		while (true)
		{
			if (select_plate_.is_end)
			{
				if (select_plate_.cursor_no == 0)
				{
					break;
				}
				if (select_text_[select_type_].Length - 1 == select_plate_.cursor_no)
				{
					game_end_select_.body_active = true;
					game_end_select_.playCursor(0);
					mask_.active = true;
					message_window_.active = true;
					message_text_list_[0].text = TextDataCtrl.GetText(TextDataCtrl.TitleTextID.EXIT_MESSAGE);
					message_text_list_[1].text = TextDataCtrl.GetText(TextDataCtrl.TitleTextID.EXIT_MESSAGE, 1);
					while (true)
					{
						if (game_end_select_.is_end)
						{
							if (game_end_select_.cursor_no == 0)
							{
								float delta_time = 0f;
								float se_time2 = 0f;
								se_time2 = soundCtrl.instance.GetAudioClipLength(43);
								while (true)
								{
									delta_time += Time.deltaTime;
									if (delta_time > se_time2 && !soundCtrl.instance.IsPlayingSE(43))
									{
										break;
									}
									yield return null;
								}
								Window.Exit();
								yield break;
							}
							game_end_select_.stopCursor();
							game_end_select_.body_active = false;
							message_window_.active = false;
							mask_.active = false;
							break;
						}
						if (!game_end_select_.is_play_animation && padCtrl.instance.GetKeyDown(KeyType.B))
						{
							soundCtrl.instance.PlaySE(44);
							game_end_select_.stopCursor();
							game_end_select_.body_active = false;
							message_window_.active = false;
							mask_.active = false;
							break;
						}
						yield return null;
					}
					select_plate_.body_active = true;
					select_plate_.playCursor(0);
				}
				else if (select_text_[select_type_].Length - 2 == select_plate_.cursor_no)
				{
					optionCtrl.instance.change_language = false;
					optionCtrl.instance.Open(optionCtrl.OptionType.TITLE);
					while (optionCtrl.instance.is_open)
					{
						yield return null;
					}
					if (optionCtrl.instance.change_language)
					{
						changeText();
						optionCtrl.instance.change_language = false;
						if (GSStatic.save_data.Where((SaveData data) => data.in_data > 0).Count() > 0)
						{
							select_type_ = 1;
						}
						else
						{
							select_type_ = 0;
						}
						game_end_select_.mainTitleInit(new string[2]
						{
							TextDataCtrl.GetText(TextDataCtrl.TitleTextID.YES),
							TextDataCtrl.GetText(TextDataCtrl.TitleTextID.NO)
						}, "select_button", 1);
						select_plate_.mainTitleInit(select_text_[select_type_], "select_button", select_type_);
						select_plate_.body_active = true;
						select_plate_.playCursor(0);
					}
				}
				else
				{
					SaveLoadUICtrl.instance.Open(SaveLoadUICtrl.SlotType.LOAD);
					while (SaveLoadUICtrl.instance.is_open)
					{
						yield return null;
					}
					if (SaveLoadUICtrl.instance.is_loaded)
					{
						titleCtrlRoot.instance.active = false;
						End();
						yield break;
					}
				}
				select_plate_.body_active = true;
				select_plate_.playCursor(0);
			}
			else if (MenuTest.DebugAdvStart())
			{
				load = true;
				break;
			}
			yield return null;
		}
		fadeCtrl.instance.play(fadeCtrl.Status.FADE_OUT, 30u, 16u);
		while (!fadeCtrl.instance.is_end)
		{
			yield return null;
		}
		if (load)
		{
			titleCtrlRoot.instance.active = false;
			advCtrl.instance.play((int)GSStatic.global_work_.title, GSStatic.global_work_.story, GSStatic.global_work_.scenario, false);
			soundCtrl.instance.FadeOutBGM(30);
		}
		else
		{
			titleCtrlRoot.instance.Scene(titleCtrlRoot.SceneType.Title);
		}
		yield return null;
		enumerator_state_ = null;
		End();
	}

	private void changeText()
	{
		select_text_ = new string[2][]
		{
			new string[3]
			{
				TextDataCtrl.GetText(TextDataCtrl.TitleTextID.NEW_GAME),
				TextDataCtrl.GetText(TextDataCtrl.TitleTextID.OPTION),
				TextDataCtrl.GetText(TextDataCtrl.TitleTextID.EXIT)
			},
			new string[4]
			{
				TextDataCtrl.GetText(TextDataCtrl.TitleTextID.NEW_GAME),
				TextDataCtrl.GetText(TextDataCtrl.TitleTextID.CONTINUE),
				TextDataCtrl.GetText(TextDataCtrl.TitleTextID.OPTION),
				TextDataCtrl.GetText(TextDataCtrl.TitleTextID.EXIT)
			}
		};
		select_plate_.mainTitleInit(select_text_[select_type_], "select_button", select_type_);
		int num = 0;
		Vector3 zero = Vector3.zero;
		switch (GSUtility.GetLanguageLayoutType(GSStatic.global_work_.language))
		{
		case "JAPAN":
			num = 38;
			zero = new Vector3(0f, 0f, -30f);
			break;
		default:
			num = 37;
			zero = new Vector3(0f, 4f, -30f);
			break;
		}
		select_plate_.SetTextFontsize(num);
		select_plate_.SetTextPosition(zero);
		main_title_.title_.load("/menu/title/", "title" + GSUtility.GetResourceNameLanguage(GSStatic.global_work_.language), false);
		main_title_.title_.spriteNo(0);
		main_title_.title_.sprite_data_.Clear();
	}
}
