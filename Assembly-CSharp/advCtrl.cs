using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class advCtrl : MonoBehaviour
{
	private static advCtrl instance_;

	[SerializeField]
	private GameObject body_;

	[SerializeField]
	private Camera adv_camera_;

	[SerializeField]
	private int DebugOverrideScenario = -1;

	private IEnumerator scenario_enumerator_;

	private List<List<int>> scenario_end_ = new List<List<int>>
	{
		new List<int> { 1, 5, 11, 17, 34 },
		new List<int> { 2, 8, 14, 22 },
		new List<int> { 2, 7, 12, 14, 23 }
	};

	public MessageHeader message_header_;

	public ConvertTextData cho_data_;

	public ConvertTextData sel_data_;

	public ConvertTextData ba_data_;

	public ConvertTextData nolb_data_;

	public ConvertLineData note_data_;

	public bool is_stop_;

	public MessageSystem message_system_ = new MessageSystem();

	public SubWindow sub_window_ = new SubWindow();

	public static advCtrl instance
	{
		get
		{
			return instance_;
		}
	}

	public GameObject body
	{
		get
		{
			return body_;
		}
	}

	public Camera adv_camera
	{
		get
		{
			return adv_camera_;
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

	private void Awake()
	{
		if (instance_ == null)
		{
			instance_ = this;
		}
	}

	public void load()
	{
	}

	public void init()
	{
		message_system_.Initialize();
	}

	public void play(int in_title_id, int in_story_id, int in_scenario_id)
	{
		if (0 <= DebugOverrideScenario)
		{
			in_scenario_id = DebugOverrideScenario;
		}
		TrophyCtrl.set_tropthy(26);
		debugLogger.instance.Log("adv", "advCtrl.Play > title[" + in_title_id + "] story[" + in_story_id + "] scenario[" + in_scenario_id + "]");
		coroutineCtrl.instance.Play(stateCoroutine(in_title_id, in_story_id, in_scenario_id, false));
	}

	public void play(int in_title_id, int in_story_id, int in_scenario_id, bool continue_data)
	{
		coroutineCtrl.instance.Play(stateCoroutine(in_title_id, in_story_id, in_scenario_id, continue_data));
	}

	private IEnumerator stateCoroutine(int in_title_id, int in_story_id, int in_scenario_id, bool continue_data)
	{
		active = true;
		fadeCtrl.instance.play(fadeCtrl.Status.FADE_IN, 30u, 16u);
		while (!fadeCtrl.instance.is_end)
		{
			yield return null;
		}
		string syste_mes_mdt = string.Empty;
		string message_header = string.Empty;
		string cho_data = string.Empty;
		string sel_data = string.Empty;
		string ba_data = string.Empty;
		string nolb_data2 = string.Empty;
		string note_data2 = string.Empty;
		string language_text = GSUtility.GetScenarioLanguage(GSStatic.global_work_.language);
		syste_mes_mdt = "GS" + (in_title_id + 1).ToString("D1") + "/scenario/" + GSScenario.GetSystemScenarioMdtPath();
		message_header = "GS" + (in_title_id + 1).ToString("D1") + "/scenario/gs" + (in_title_id + 1).ToString("D1") + ".mh";
		cho_data = "GS" + (in_title_id + 1).ToString("D1") + "/scenario/gs" + (in_title_id + 1).ToString("D1") + language_text + ".cho";
		sel_data = "GS" + (in_title_id + 1).ToString("D1") + "/scenario/gs" + (in_title_id + 1).ToString("D1") + "_sel" + language_text + ".bin";
		ba_data = "GS" + (in_title_id + 1).ToString("D1") + "/scenario/gs" + (in_title_id + 1).ToString("D1") + "_ba" + language_text + ".bin";
		nolb_data2 = "GS" + (in_title_id + 1).ToString("D1") + "/scenario/gs" + (in_title_id + 1).ToString("D1") + "_nolb" + language_text + ".bin";
		note_data2 = "GS" + (in_title_id + 1).ToString("D1") + "/scenario/gs" + (in_title_id + 1).ToString("D1") + "_note" + language_text + ".bin";
		GSStatic.global_work_.title = (TitleId)in_title_id;
		GSStatic.global_work_.story = (byte)in_story_id;
		GSStatic.global_work_.scenario = (byte)in_scenario_id;
		message_system_.LoadSystemMdtFromStreamingAssets(syste_mes_mdt);
		LoadMessageHeader(message_header);
		LoadTextData(ref cho_data_, cho_data, GSStatic.global_work_.language);
		LoadTextData(ref sel_data_, sel_data, GSStatic.global_work_.language);
		LoadTextData(ref ba_data_, ba_data, GSStatic.global_work_.language);
		LoadTextData(ref nolb_data_, nolb_data2, GSStatic.global_work_.language);
		LoadTextData(ref note_data_, note_data2, GSStatic.global_work_.language);
		switch (GSStatic.global_work_.title)
		{
		case TitleId.GS1:
			SelectTable.InitializeInfotable_GS1(message_header_);
			break;
		case TitleId.GS2:
			SelectTable.InitializeInfotable_GS2(message_header_);
			break;
		case TitleId.GS3:
			SelectTable.InitializeInfotable_GS3(message_header_);
			break;
		}
		if (scenario_enumerator_ != null)
		{
			coroutineCtrl.instance.Stop(scenario_enumerator_);
			scenario_enumerator_ = null;
		}
		scenario_enumerator_ = scenarioCoroutine(continue_data, false);
		coroutineCtrl.instance.Play(scenario_enumerator_);
		while (scenario_enumerator_ != null)
		{
			yield return null;
		}
		active = false;
		fadeCtrl.instance.play(fadeCtrl.Status.FADE_IN, 30u, 16u);
		while (!fadeCtrl.instance.is_end)
		{
			yield return null;
		}
		titleCtrlRoot.instance.active = true;
		titleCtrlRoot.instance.Scene(titleCtrlRoot.SceneType.Top);
	}

	private IEnumerator scenarioCoroutine(bool continue_data, bool is_init)
	{
		string scenario_mdt = string.Empty;
		soundCtrl.instance.AllStop();
		GSStatic.open_sce_.OpenScenarioSet(GSStatic.save_sys_, GSStatic.global_work_);
		scenario_mdt = GSScenario.GetScenarioMdtPath(GSStatic.global_work_.scenario);
		GSMain.is_active_ = true;
		if (continue_data)
		{
			scenario_mdt = GSStatic.message_work_.mdt_path;
		}
		message_system_.LoadScenarioMdtFromStreamingAssets(scenario_mdt);
		message_system_.Reset(!continue_data);
		componentCtrl.instance.init();
		componentCtrl.instance.load();
		bgCtrl.instance.init();
		GSMapIcon.instance.Initialize();
		messageBoardCtrl.instance.namePlateLoad();
		messageBoardCtrl.instance.sprite_board.sprite_renderer_.color = new Color(messageBoardCtrl.instance.sprite_board.sprite_renderer_.color.r, messageBoardCtrl.instance.sprite_board.sprite_renderer_.color.g, messageBoardCtrl.instance.sprite_board.sprite_renderer_.color.b, optionWindow.instance.alpha_rate);
		if (is_init && GSStatic.global_work_.story > 0 && GSStatic.global_work_.scenario == scenario_end_[(int)GSStatic.global_work_.title][GSStatic.global_work_.story - 1])
		{
			recordListCtrl.instance.setPartRecord(GSStatic.global_work_.scenario);
		}
		GS2_OpObjCtrl.instance.ClearList();
		if (continue_data)
		{
			for (int i = 0; i < GSStatic.record_list.record.Length && GSStatic.record_list.record[i] >= 0; i++)
			{
				recordListCtrl.instance.addRecord(GSStatic.record_list.record[i]);
			}
			messageBoardCtrl.instance.LoadMsgSet();
			if (GSStatic.sound_save_data.playBgmNo != 254)
			{
				soundCtrl.instance.PlayBGM(GSStatic.sound_save_data.playBgmNo);
			}
			else if (GSStatic.sound_save_data.stopBgmNo != 254)
			{
				soundCtrl.instance.PlayBGM(GSStatic.sound_save_data.stopBgmNo);
				soundCtrl.instance.AllStopBGM();
			}
			int[] se_no = GSStatic.sound_save_data.se_no;
			foreach (int num in se_no)
			{
				if (num != 268435455)
				{
					soundCtrl.instance.PlaySE(num);
				}
			}
			for (int k = 0; k < 3; k++)
			{
				if (GSStatic.sound_save_data.pause_bgm[k])
				{
					soundCtrl.instance.PauseBGM(k);
				}
			}
			soundCtrl.instance.VolumeChangeBGM(GSStatic.global_work_.bgm_vol, 0);
			AnimationSystem.Instance.SystemDataToLoad = GSStatic.obj_work_[1].system_data;
			AnimationSystem.Instance.ObjectStatusToLoad = GSStatic.obj_work_[1].objects_data;
			AnimationSystem.Instance.PlaySavedAnimation();
			bgCtrl.instance.Bg256_SP_Flag = GSStatic.bg_save_data.bg_flag;
			if (GSStatic.bg_save_data.bg_black)
			{
				bgCtrl.instance.SetSprite(4095);
			}
			else
			{
				bgCtrl.instance.SetSprite(GSStatic.bg_save_data.bg_no);
			}
			GSDemo.CheckBGChange(GSStatic.bg_save_data.bg_no, 0u);
			bgCtrl.instance.bg_pos_x = GSStatic.bg_save_data.bg_pos_x;
			bgCtrl.instance.bg_pos_y = GSStatic.bg_save_data.bg_pos_y;
			bgCtrl.instance.bg_no_old = GSStatic.bg_save_data.bg_no_old;
			bgCtrl.instance.is_reverse = GSStatic.bg_save_data.reverse;
			bgCtrl.instance.setNegaPosi(GSStatic.bg_save_data.negaposi, GSStatic.bg_save_data.negaposi_sub);
			bgCtrl.instance.setColor(GSStatic.bg_save_data.color);
			if (GSStatic.bg_save_data.bg_parts_enabled)
			{
				bgCtrl.instance.SetParts(GSStatic.bg_save_data.bg_parts, GSStatic.bg_save_data.bg_parts_enabled);
			}
			for (int l = 0; l < GSStatic.expl_char_work_.expl_char_data_.Length; l++)
			{
				ExplCharData explCharData = GSStatic.expl_char_work_.expl_char_data_[l];
				if (explCharData.id != byte.MaxValue)
				{
					int para = explCharData.para1;
					int para2 = explCharData.para0;
					GSMapIcon.instance.LoadSpriteByIndex(l, explCharData.id);
					if (GSStatic.global_work_.title == TitleId.GS1 && GSStatic.global_work_.scenario == 6 && explCharData.id == 1 && para == 258 && para2 == 256)
					{
						GSMapIcon.instance.is_expl_higaisha_move = true;
					}
					GSMapIcon.instance.SetPositionByIndex(explCharData.id, l, para & 0x1FF, para2 & 0xFF);
					GSMapIcon.instance.SetVisibleByIndex(l, true);
				}
			}
			lifeGaugeCtrl.instance.gauge_mode = GSStatic.menu_work.life_gauge;
			lifeGaugeCtrl.instance.ActionLifeGauge(lifeGaugeCtrl.Gauge_State.UPDATE_REST);
			switch ((lifeGaugeCtrl.Gauge_State)lifeGaugeCtrl.instance.gauge_mode)
			{
			case lifeGaugeCtrl.Gauge_State.LIFE_ON:
				lifeGaugeCtrl.instance.ActionLifeGauge(lifeGaugeCtrl.Gauge_State.LIFE_ON_MOMENT);
				break;
			case lifeGaugeCtrl.Gauge_State.NOTICE_DAMAGE:
				lifeGaugeCtrl.instance.ActionLifeGauge(lifeGaugeCtrl.Gauge_State.LIFE_ON_MOMENT);
				lifeGaugeCtrl.instance.ActionLifeGauge(lifeGaugeCtrl.Gauge_State.NOTICE_DAMAGE);
				break;
			}
			tanteiMenu.instance.cursor_no = GSStatic.tantei_work_.tantei_cursor;
			tanteiMenu.instance.setting = GSStatic.menu_work.tantei_menu_setting;
			if (GSStatic.menu_work.tantei_menu_is_play)
			{
				tanteiMenu.instance.play(GSStatic.menu_work.tantei_menu_setting);
			}
			selectPlateCtrl.instance.cursor_no = GSStatic.tantei_work_.select_cursor;
			selectPlateCtrl.instance.old_cursor_no = GSStatic.tantei_work_.talk_cursor;
			selectPlateCtrl.instance.old_cursor_num = GSStatic.tantei_work_.talk_cursor_num;
			if (GSStatic.menu_work.select_plate_is_select)
			{
				ushort[] selectTextIds = SelectTable.GetSelectTextIds(GSStatic.global_work_.scenario, GSStatic.message_work_.now_no);
				selectPlateCtrl selectPlateCtrl2 = selectPlateCtrl.instance;
				if (selectTextIds != null)
				{
					selectPlateCtrl2.entryCursor(selectTextIds.Length, selectPlateCtrl.FromEntryRequest.SELECT_LOAD);
					for (int m = 0; m < selectTextIds.Length; m++)
					{
						selectPlateCtrl2.setText(m, cho_data_.GetText(selectTextIds[m]));
					}
				}
				selectPlateCtrl2.playCursor(0);
			}
			if (GSStatic.menu_work.select_plate_is_talk)
			{
				TALK_DATA tALK_DATA = GSStatic.talk_work_.talk_data_[0];
				for (int n = 0; n < GSStatic.talk_work_.talk_data_.Length && GSStatic.talk_work_.talk_data_[n].room != 255; n++)
				{
					if (GSStatic.talk_work_.talk_data_[n].room == GSStatic.global_work_.Room && GSStatic.talk_work_.talk_data_[n].pl_id == GSStatic.obj_work_[1].h_num && GSStatic.talk_work_.talk_data_[n].sw == 1)
					{
						tALK_DATA = GSStatic.talk_work_.talk_data_[n];
						break;
					}
				}
				int num2 = 0;
				for (int num3 = 0; num3 < 4; num3++)
				{
					if (tALK_DATA.tag[num3] != 255)
					{
						if (num2 < 4)
						{
							selectPlateCtrl.instance.setText(num3, sel_data_.GetText((ushort)tALK_DATA.tag[num3]));
							selectPlateCtrl.instance.setRead(num3, GSFlag.Check(2u, tALK_DATA.flag[num3]), SubWindow_Talk.msg_lock_dsp((ushort)tALK_DATA.mess[num3]));
							num2++;
						}
						else
						{
							Debug.Log("Talk Num Error!! " + num2);
						}
					}
				}
				if (num2 > 0)
				{
					selectPlateCtrl.instance.entryCursor(num2, selectPlateCtrl.FromEntryRequest.TALK);
				}
				selectPlateCtrl.instance.playCursor(1);
				coroutineCtrl.instance.Play(keyGuideCtrl.instance.open(keyGuideBase.Type.TANTEI_TALK));
			}
			inspectCtrl.instance.SetCursorPos(GSStatic.tantei_work_.inspect_cursor_x, GSStatic.tantei_work_.inspect_cursor_y);
			if (GSStatic.menu_work.inspect_is_play)
			{
				inspectCtrl.instance.play();
			}
			if (GSStatic.menu_work.move_is_play)
			{
				int num4 = 0;
				GlobalWork global_work_ = GSStatic.global_work_;
				for (int num5 = 0; num5 < 4; num5++)
				{
					uint num6 = global_work_.Map_data[global_work_.Room][4 + num5];
					if (num6 != 255)
					{
						num4++;
					}
				}
				moveCtrl.instance.play(num4, GSStatic.tantei_work_.move_cursor);
			}
			if ((GSStatic.cinema_work_.status & 0xFu) != 0)
			{
				if (GSStatic.cinema_work_.film_no == 2)
				{
					ConfrontWithMovie.instance.auto_play = true;
					ConfrontWithMovie.instance.controller.InitData(true);
					ConfrontWithMovie.instance.controller.SetAutoPlayStatus((int)GSStatic.cinema_work_.status);
					ConfrontWithMovie.instance.controller.SetAutoPlayFrame(GSStatic.cinema_work_.frame_set);
					ConfrontWithMovie.instance.controller.SetAutoPlayStartFrame(GSStatic.cinema_work_.frame_top);
					ConfrontWithMovie.instance.controller.SetAutoPlayEndFrame(GSStatic.cinema_work_.frame_end);
					ConfrontWithMovie.instance.StartConfront();
					ConfrontWithMovie.instance.controller.SetAutoPlayStatus((int)GSStatic.cinema_work_.status);
				}
				else if (GSStatic.cinema_work_.film_no == 0)
				{
					ConfrontWithMovie.instance.controller.SetVideo();
					if (GSStatic.cinema_work_.movie_type != 0)
					{
						ConfrontWithMovie.instance.controller.SetAutoPlayStatus((int)GSStatic.cinema_work_.status);
						ConfrontWithMovie.instance.controller.SetAutoPlayEndFrame(GSStatic.cinema_work_.frame_end);
					}
				}
				else
				{
					MovieAccessor.Instance.Play(string.Format("film0{0}", GSStatic.cinema_work_.film_no), false);
				}
			}
			bgCtrl.instance.ResetSpef();
			switch (GSStatic.global_work_.SpEf_status)
			{
			case 4:
			case 5:
			case 8:
			case 9:
			case 10:
			case 14:
			case 16:
			case 18:
			case 22:
				spefCtrl.instance.Monochrome_set(GSStatic.global_work_.SpEf_status, 1, 1);
				break;
			default:
				spefCtrl.instance.Monochrome_set(GSStatic.global_work_.SpEf_status, 1, 31);
				break;
			}
			itemPlateCtrl.instance.LoadItem();
			facePlateCtrl.instance.LoadItem();
			AnimationSystem.Instance.PlaySavedStatus();
		}
		if (!continue_data)
		{
			MessageSystem.SetActiveMessageWindow(WindowType.MAIN);
			GSStatic.global_work_.r.Set(GSScenario.GetScenarioPartData(), 0, 0, 0);
			sub_window_.SetReq(SubWindow.Req.NONE);
			sub_window_.routine_[0].r.Set(0, 0, 0, 0);
			sub_window_.routine_[1].r.Set(0, 0, 0, 0);
			sub_window_.routine_[2].r.Set(0, 0, 0, 0);
			sub_window_.routine_[3].r.Set(0, 0, 0, 0);
			sub_window_.routine_[4].r.Set(0, 0, 0, 0);
			sub_window_.routine_[5].r.Set(0, 0, 0, 0);
			sub_window_.routine_[6].r.Set(0, 0, 0, 0);
			sub_window_.routine_[7].r.Set(0, 0, 0, 0);
		}
		while (!is_stop_ && !message_system_.is_end)
		{
			yield return null;
		}
		Debug.Log("----ChildCoroutine Time Start:" + Time.time);
		float time = 0f;
		float wait = 1f;
		while (true)
		{
			time += Time.deltaTime;
			if (time > wait)
			{
				break;
			}
			yield return null;
		}
		Debug.Log("----ChildCoroutine Time End:" + Time.time);
		if (GSStatic.global_work_.r.no_0 == 1)
		{
			GSMain.End();
			scenario_enumerator_ = null;
		}
		else if (GSStatic.global_work_.scenario < scenario_end_[(int)GSStatic.global_work_.title][GSStatic.global_work_.story])
		{
			GSMain.End();
			yield return null;
			if (scenario_enumerator_ != null)
			{
				coroutineCtrl.instance.Stop(scenario_enumerator_);
				scenario_enumerator_ = null;
			}
			scenario_enumerator_ = scenarioCoroutine(false, true);
			coroutineCtrl.instance.Play(scenario_enumerator_);
		}
	}

	private void LoadMessageHeader(string path)
	{
		string in_path = Application.streamingAssetsPath + "/" + path;
		byte[] bytes = decryptionCtrl.instance.load(in_path);
		message_header_ = new MessageHeader(bytes);
	}

	private void LoadTextData(ref ConvertTextData data, string path, Language language)
	{
		string in_path = Application.streamingAssetsPath + "/" + path;
		byte[] bytes = decryptionCtrl.instance.load(in_path);
		data = new ConvertTextData(bytes, language);
	}

	private void LoadTextData(ref ConvertLineData data, string path, Language language)
	{
		string in_path = Application.streamingAssetsPath + "/" + path;
		byte[] bytes = decryptionCtrl.instance.load(in_path);
		data = new ConvertLineData(bytes, language);
	}

	public void end()
	{
		GSMain.is_active_ = false;
		message_system_.end();
		if (AssetBundleCtrl.instance != null)
		{
			AssetBundleCtrl.instance.end();
		}
		if (soundCtrl.instance != null)
		{
			soundCtrl.instance.end();
		}
		if (bgCtrl.instance != null)
		{
			bgCtrl.instance.end();
		}
		if (selectPlateCtrl.instance != null)
		{
			selectPlateCtrl.instance.end();
		}
		if (tanteiMenu.instance != null)
		{
			tanteiMenu.instance.end();
		}
		if (moveCtrl.instance != null)
		{
			moveCtrl.instance.end();
		}
		if (inspectCtrl.instance != null)
		{
			inspectCtrl.instance.end();
		}
		if (ConfrontWithMovie.instance != null)
		{
			ConfrontWithMovie.instance.EndAutoPlay(true);
		}
		if (MovieAccessor.Instance != null)
		{
			MovieAccessor.Instance.end();
		}
		if (itemPlateCtrl.instance != null)
		{
			itemPlateCtrl.instance.closeItem(true);
		}
		if (TestimonyRoot.instance != null)
		{
			TestimonyRoot.instance.TestimonyIconEnabled = false;
		}
		if (VaseShowMiniGame.instance != null)
		{
			VaseShowMiniGame.instance.end();
		}
		componentCtrl.instance.end();
	}
}
