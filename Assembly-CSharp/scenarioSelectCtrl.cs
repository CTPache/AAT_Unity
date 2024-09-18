using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scenarioSelectCtrl : sceneCtrl
{
	[Serializable]
	public class storyTitle
	{
		public AssetBundleSprite title_;

		public int scenario_no_;

		public AssetBundleSprite title_text_;

		public InputTouch touch_;

		public bool active
		{
			get
			{
				return title_.active;
			}
			set
			{
				title_.active = value;
			}
		}
	}

	[SerializeField]
	private bool debug_;

	private bool is_back_;

	private int clear_num_;

	private int open_num_;

	private int current_num_;

	private int move_stack_;

	private bool is_scenario_move_;

	private int story_cnt_;

	private int[] story_max = new int[3] { 5, 4, 5 };

	private string path_;

	private string[] start_text_base;

	[SerializeField]
	private AssetBundleSprite select_cursor;

	[SerializeField]
	private titleSelectPlate confirmation_select_;

	[SerializeField]
	private arrowCtrl arrow_ctrl_;

	[SerializeField]
	private float title_space_ = 730f;

	[SerializeField]
	private GameObject story_list_body_;

	[SerializeField]
	private List<storyTitle> story_title_ = new List<storyTitle>();

	[SerializeField]
	private AssetBundleSprite story_mask_;

	[SerializeField]
	private AssetBundleSprite story_focus_R_;

	[SerializeField]
	private AssetBundleSprite story_focus_L_;

	[SerializeField]
	private AssetBundleSprite text_window_;

	[SerializeField]
	private List<Text> start_text_ = new List<Text>();

	[SerializeField]
	private AssetBundleSprite mask_;

	[SerializeField]
	private AssetBundleSprite focus_R_;

	[SerializeField]
	private AssetBundleSprite focus_L_;

	[SerializeField]
	private AssetBundleSprite back_ground_;

	[SerializeField]
	private SpriteMask sprite_mask_;

	[SerializeField]
	private AnimationCurve focus_scale_curve_ = new AnimationCurve();

	[SerializeField]
	[Tooltip("１つ隣の画像へスライドするまでの時間(フレーム数：60 = 1秒)")]
	private int slide_time_ = 24;

	[SerializeField]
	private AnimationCurve curve_ = new AnimationCurve();

	private IEnumerator enumerator_confirmation_;

	private IEnumerator enumerator_key_wait_;

	public static scenarioSelectCtrl instance { get; private set; }

	public bool is_back
	{
		get
		{
			return is_back_;
		}
		set
		{
			is_back_ = value;
		}
	}

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

	private void Init()
	{
		base.is_end = false;
		is_back_ = false;
		base.body.SetActive(true);
		storyTitleSet((TitleId)titleCtrlRoot.instance.data_system.title_no);
		confirmation_select_.mainTitleInit(new string[2]
		{
			TextDataCtrl.GetText(TextDataCtrl.TitleTextID.YES),
			TextDataCtrl.GetText(TextDataCtrl.TitleTextID.NO)
		}, "select_button", 0);
		confirmation_select_.body_active = false;
		select_cursor.load("/menu/common/", "title_select");
		select_cursor.sprite_renderer_.size = new Vector2(700f, 1060f);
		select_cursor.active = true;
		mask_.active = true;
		mask_.load("/menu/common/", "mask");
		mask_.active = false;
		focus_R_.load("/menu/title/", "title_g02");
		focus_L_.load("/menu/title/", "title_g02");
		SetFocusActive(true);
		text_window_.load("/menu/common/", "talk_bg");
		text_window_.active = false;
		arrow_ctrl_.load();
		back_ground_.load("/menu/title/", "title_select_bg");
		back_ground_.active = true;
		story_mask_.load("/menu/common/", "mask");
		story_mask_.active = false;
		story_focus_R_.load("/menu/title/", "title_g02");
		story_focus_L_.load("/menu/title/", "title_g02");
		story_focus_R_.active = true;
		story_focus_L_.active = true;
		start_text_base = new string[2]
		{
			TextDataCtrl.GetText(TextDataCtrl.TitleTextID.PLAY_CONFIRM),
			TextDataCtrl.GetText(TextDataCtrl.TitleTextID.PLAY_CONFIRM, 1)
		};
		arrow_ctrl_.SetTouchedKeyType(KeyType.Right, 0);
		arrow_ctrl_.SetTouchedKeyType(KeyType.Left, 1);
		arrow_ctrl_.ActiveArrow();
		for (int i = 0; i < story_title_.Count; i++)
		{
			story_title_[i].touch_.ActiveCollider();
		}
		float x = (float)story_cnt_ * title_space_ / story_mask_.sprite_renderer_.size.x;
		float y = (float)systemCtrl.instance.ScreenHeight / story_mask_.sprite_renderer_.size.y;
		story_mask_.sprite_renderer_.transform.localScale = new Vector3(x, y, 1f);
		Vector3 localPosition = story_mask_.transform.localPosition;
		localPosition.x = (float)story_cnt_ * title_space_ / 2f - story_title_[0].title_.sprite_renderer_.size.x / 2f;
		story_mask_.transform.localPosition = localPosition;
		Vector3 localPosition2 = story_focus_R_.transform.localPosition;
		Vector3 localPosition3 = story_focus_L_.transform.localPosition;
		float num = 70f;
		localPosition2.x = (float)story_cnt_ * title_space_ - num;
		localPosition3.x = 0f - title_space_ + num;
		story_focus_R_.transform.localPosition = localPosition2;
		story_focus_L_.transform.localPosition = localPosition3;
		sprite_mask_.sprite = select_cursor.sprite_renderer_.sprite;
		sprite_mask_.transform.localScale = new Vector3(13.846154f, 20.76923f, 1f);
		UpdateStoryIconFade(story_list_body_.transform.localPosition.x);
		foreach (Text item in start_text_)
		{
			mainCtrl.instance.addText(item);
		}
	}

	private void storyTitleSet(TitleId title_id)
	{
		string resourceNameLanguage = GSUtility.GetResourceNameLanguage(GSStatic.global_work_.language);
		string in_name = string.Empty;
		string in_name2 = string.Empty;
		byte b = 0;
		path_ = string.Empty;
		switch (title_id)
		{
		case TitleId.GS1:
			path_ = "/GS1/BG/";
			in_name = "title_textgs1" + resourceNameLanguage;
			in_name2 = "storygs1";
			b = GSStatic.open_sce_.GS1_Scenario_enable;
			break;
		case TitleId.GS2:
			path_ = "/GS2/BG/";
			in_name = "title_textgs2" + resourceNameLanguage;
			in_name2 = "storygs2";
			b = GSStatic.open_sce_.GS2_Scenario_enable;
			break;
		case TitleId.GS3:
			path_ = "/GS3/BG/";
			in_name = "title_textgs3" + resourceNameLanguage;
			in_name2 = "storygs3";
			b = GSStatic.open_sce_.GS3_Scenario_enable;
			break;
		}
		story_cnt_ = story_max[(int)title_id];
		clear_num_ = b >> 4;
		if (story_cnt_ >= clear_num_ && (b & 0xF) != 15)
		{
			clear_num_--;
		}
		float num = focus_scale_curve_.Evaluate(0f);
		Vector3 localScale = new Vector3(num, num, 1f);
		AssetBundle assetBundle = AssetBundleCtrl.instance.load(path_, in_name2, true);
		for (int i = 0; i < story_title_.Count; i++)
		{
			story_title_[i].title_.load("/menu/title/", "def_title", true);
			story_title_[i].title_.sprite_data_.AddRange(assetBundle.LoadAllAssets<Sprite>());
			story_title_[i].title_.spriteNo(i);
			story_title_[i].scenario_no_ = i + 1;
			story_title_[i].title_text_.load(path_, in_name, true);
			story_title_[i].title_.transform.localScale = localScale;
			story_title_[i].touch_.touch_key_type = KeyType.None;
			story_title_[i].touch_.argument_parameter = i;
			story_title_[i].touch_.touch_event = delegate(TouchParameter p)
			{
				int index = (int)p.argument_parameter;
				if (is_scenario_move_ || !fadeCtrl.instance.is_end || confirmation_select_.body_active)
				{
					story_title_[index].touch_.touch_key_type = KeyType.None;
				}
				else
				{
					story_title_[index].touch_.touch_key_type = KeyType.A;
				}
			};
			story_title_[i].active = ((i < story_cnt_) ? true : false);
		}
		open_num_ = ((clear_num_ >= story_cnt_) ? (clear_num_ - 1) : clear_num_);
		current_num_ = 0;
		if (clear_num_ > 0)
		{
			ArrowOn();
		}
		titlePosSet(current_num_);
		num = focus_scale_curve_.Evaluate(1f);
		localScale = new Vector3(num, num, 1f);
		story_title_[current_num_].title_.transform.localScale = localScale;
	}

	private void titlePosSet(int story_no)
	{
		story_list_body_.transform.localPosition = new Vector3(0f, 0f, story_list_body_.transform.localPosition.z);
		float num = 0f;
		for (int i = 0; i < story_title_.Count; i++)
		{
			if (open_num_ >= i)
			{
				story_title_[i].title_.spriteNo(i + 1);
				story_title_[i].scenario_no_ = i;
				story_title_[i].title_text_.active = true;
				story_title_[i].title_text_.spriteNo(i);
			}
			else
			{
				story_title_[i].title_.spriteNo(0);
				story_title_[i].title_text_.active = false;
			}
			num = (float)i * title_space_;
			story_title_[i].title_.transform.localPosition = new Vector3(num, 0f, 0f);
		}
	}

	private IEnumerator CoroutineFocus(AssetBundleSprite in_target, bool in_type)
	{
		Vector3 temp_scale = in_target.sprite_renderer_.transform.localScale;
		int end_time = 2;
		int timer = ((!in_type) ? end_time : 0);
		while (timer <= end_time && timer >= 0)
		{
			temp_scale.y = (temp_scale.x = focus_scale_curve_.Evaluate((float)timer / (float)end_time));
			in_target.sprite_renderer_.transform.localScale = temp_scale;
			timer += (in_type ? 1 : (-1));
			yield return null;
		}
	}

	private IEnumerator CoroutineSlideMove()
	{
		int timer = 0;
		int end_time = slide_time_;
		Vector3 target_position = story_list_body_.transform.localPosition;
		float end_pos_x = (float)current_num_ * (0f - title_space_);
		float speed_x = (end_pos_x - target_position.x) / (float)end_time;
		focus_L_.transform.gameObject.SetActive(false);
		focus_R_.transform.gameObject.SetActive(false);
		while (timer < end_time)
		{
			timer++;
			yield return null;
			target_position.x += speed_x;
			story_list_body_.transform.localPosition = target_position;
			UpdateStoryIconFade(target_position.x);
		}
		focus_L_.transform.gameObject.SetActive(true);
		focus_R_.transform.gameObject.SetActive(true);
	}

	private IEnumerator CoroutineSlideState(int in_dir)
	{
		arrow_ctrl_.arrowAll(false);
		SetFocusActive(false);
		IEnumerator count_coroutine = MoveStackCounter();
		coroutineCtrl.instance.Play(count_coroutine);
		int slide_dir = in_dir;
		while ((slide_dir == -1 && current_num_ > 0) || (slide_dir == 1 && current_num_ < open_num_))
		{
			is_scenario_move_ = true;
			current_num_ += slide_dir;
			soundCtrl.instance.PlaySE(42);
			yield return coroutineCtrl.instance.Play(CoroutineSlideMove());
			slide_dir = 0;
			if (move_stack_ != 0)
			{
				slide_dir += ((move_stack_ >= 1) ? 1 : (-1));
				move_stack_ += ((slide_dir != 1) ? 1 : (-1));
				continue;
			}
			if (padCtrl.instance.GetKey(KeyType.Left) || padCtrl.instance.GetKey(KeyType.StickL_Left) || padCtrl.instance.GetWheelMoveUp())
			{
				slide_dir--;
			}
			if (padCtrl.instance.GetKey(KeyType.Right) || padCtrl.instance.GetKey(KeyType.StickL_Right) || padCtrl.instance.GetWheelMoveDown())
			{
				slide_dir++;
			}
		}
		float num = (float)current_num_ * (0f - title_space_);
		story_list_body_.transform.localPosition = Vector3.right * num;
		move_stack_ = 0;
		coroutineCtrl.instance.Stop(count_coroutine);
		is_scenario_move_ = false;
		ArrowOn();
		SetFocusActive(true);
	}

	private IEnumerator MoveStackCounter()
	{
		bool is_first_input = true;
		while (true)
		{
			if (is_first_input)
			{
				is_first_input = false;
			}
			else if (padCtrl.instance.GetKeyDown(KeyType.Left) || padCtrl.instance.GetKeyDown(KeyType.StickL_Left))
			{
				move_stack_--;
			}
			else if (padCtrl.instance.GetKeyDown(KeyType.Right) || padCtrl.instance.GetKeyDown(KeyType.StickL_Right))
			{
				move_stack_++;
			}
			yield return null;
		}
	}

	public override void End()
	{
		coroutineCtrl.instance.Play(titleGuideCtrl.instance.close());
		foreach (storyTitle item in story_title_)
		{
			item.title_.sprite_data_.Clear();
			item.title_.sprite_renderer_.sprite = null;
			item.title_text_.sprite_data_.Clear();
			item.title_text_.sprite_renderer_.sprite = null;
		}
		if (enumerator_key_wait_ != null)
		{
			coroutineCtrl.instance.Stop(enumerator_key_wait_);
			enumerator_key_wait_ = null;
		}
		if (enumerator_confirmation_ != null)
		{
			coroutineCtrl.instance.Stop(enumerator_confirmation_);
			enumerator_confirmation_ = null;
		}
		confirmation_select_.End();
		arrow_ctrl_.arrowAll(false);
		foreach (Text item2 in start_text_)
		{
			mainCtrl.instance.removeText(item2);
		}
		base.End();
	}

	private IEnumerator CoroutineConfirmation()
	{
		arrow_ctrl_.arrowAll(false);
		select_cursor.active = false;
		confirmation_select_.body_active = true;
		confirmation_select_.playCursor(0);
		mask_.active = true;
		SetMessage((int)GSStatic.global_work_.title, current_num_);
		while (true)
		{
			if (confirmation_select_.is_end)
			{
				if (confirmation_select_.cursor_no == 0)
				{
					base.is_end = true;
					soundCtrl.instance.FadeOutBGM(30);
				}
				else
				{
					BackScenarioSelect();
				}
				break;
			}
			if (!confirmation_select_.is_play_animation && padCtrl.instance.GetKeyDown(KeyType.B))
			{
				confirmation_select_.stopCursor();
				confirmation_select_.body_active = false;
				BackScenarioSelect();
				soundCtrl.instance.PlaySE(44);
				break;
			}
			yield return null;
		}
		enumerator_confirmation_ = null;
	}

	private void BackScenarioSelect()
	{
		mask_.active = false;
		text_window_.active = false;
		if (clear_num_ > 0)
		{
			ArrowOn();
		}
		foreach (storyTitle item in story_title_)
		{
			item.touch_.ActiveCollider();
		}
		arrow_ctrl_.ActiveArrow();
		select_cursor.active = true;
	}

	private IEnumerator CoroutineKeyWait()
	{
		while (true)
		{
			int slide_dir = 0;
			if (padCtrl.instance.IsNextMove())
			{
				if (padCtrl.instance.GetKey(KeyType.Left) || padCtrl.instance.GetKey(KeyType.StickL_Left) || padCtrl.instance.GetWheelMoveUp())
				{
					slide_dir = -1;
				}
				else if (padCtrl.instance.GetKey(KeyType.Right) || padCtrl.instance.GetKey(KeyType.StickL_Right) || padCtrl.instance.GetWheelMoveDown())
				{
					slide_dir = 1;
				}
			}
			if (slide_dir != 0 && ((slide_dir == -1 && current_num_ > 0) || (slide_dir == 1 && current_num_ < open_num_)))
			{
				coroutineCtrl.instance.Play(CoroutineFocus(story_title_[current_num_].title_, false));
				coroutineCtrl.instance.Play(CoroutineFocus(story_title_[current_num_].title_text_, false));
				yield return coroutineCtrl.instance.Play(CoroutineSlideState(slide_dir));
				coroutineCtrl.instance.Play(CoroutineFocus(story_title_[current_num_].title_, true));
				coroutineCtrl.instance.Play(CoroutineFocus(story_title_[current_num_].title_text_, true));
			}
			if (padCtrl.instance.GetKeyDown(KeyType.B))
			{
				is_back_ = true;
				base.is_end = true;
				soundCtrl.instance.PlaySE(44);
				coroutineCtrl.instance.Play(titleGuideCtrl.instance.close());
			}
			else if (padCtrl.instance.GetKeyDown(KeyType.A) && fadeCtrl.instance.is_end)
			{
				soundCtrl.instance.PlaySE(43);
				coroutineCtrl.instance.Play(titleGuideCtrl.instance.open(keyGuideBase.Type.NO_GUIDE));
				TouchSystem.TouchInActive();
				enumerator_confirmation_ = CoroutineConfirmation();
				yield return coroutineCtrl.instance.Play(enumerator_confirmation_);
				if (!base.is_end)
				{
					coroutineCtrl.instance.Play(titleGuideCtrl.instance.open(keyGuideBase.Type.SCENARIO_SELECT));
				}
			}
			if (base.is_end)
			{
				break;
			}
			yield return null;
		}
		enumerator_key_wait_ = null;
	}

	private IEnumerator stateCoroutine()
	{
		Init();
		fadeCtrl.instance.play(fadeCtrl.Status.FADE_IN, 30u, 16u);
		while (!fadeCtrl.instance.is_end)
		{
			yield return null;
		}
		coroutineCtrl.instance.Play(titleGuideCtrl.instance.open(keyGuideBase.Type.SCENARIO_SELECT));
		enumerator_key_wait_ = CoroutineKeyWait();
		yield return coroutineCtrl.instance.Play(enumerator_key_wait_);
		fadeCtrl.instance.play(fadeCtrl.Status.FADE_OUT, 30u, 16u);
		while (!fadeCtrl.instance.is_end)
		{
			yield return null;
		}
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
		if (is_back_)
		{
			coroutineCtrl.instance.Play(titleGuideCtrl.instance.close());
			while (titleGuideCtrl.instance.CheckClose())
			{
				yield return null;
			}
			titleCtrlRoot.instance.Scene(titleCtrlRoot.SceneType.Title);
		}
		else
		{
			titleCtrlRoot.instance.data_system.story_no = current_num_;
			titleCtrlRoot.instance.active = false;
			List<List<uint>> list = new List<List<uint>>();
			list.Add(new List<uint> { 0u, 1u, 5u, 11u, 17u });
			list.Add(new List<uint> { 0u, 2u, 8u, 14u });
			list.Add(new List<uint> { 0u, 2u, 7u, 12u, 14u });
			List<List<uint>> list2 = list;
			int in_scenario_id = (int)list2[titleCtrlRoot.instance.data_system.title_no][titleCtrlRoot.instance.data_system.story_no];
			advCtrl.instance.play(titleCtrlRoot.instance.data_system.title_no, titleCtrlRoot.instance.data_system.story_no, in_scenario_id);
		}
		enumerator_state_ = null;
		End();
	}

	private void ArrowOn()
	{
		if (current_num_ >= open_num_)
		{
			arrow_ctrl_.arrow(true, 1);
		}
		else if (current_num_ == 0)
		{
			arrow_ctrl_.arrow(true, 0);
		}
		else
		{
			arrow_ctrl_.arrowAll(true);
		}
	}

	private void SetFocusActive(bool in_enabled)
	{
	}

	private void UpdateStoryIconFade(float target_pos_x)
	{
		if (!debug_)
		{
			for (int i = 0; i < story_title_.Count; i++)
			{
				float f = story_title_[i].title_.sprite_renderer_.transform.localPosition.x + target_pos_x;
				float num = curve_.Evaluate(Mathf.Abs(Mathf.Round(f)) / title_space_);
				Color color = new Color(num, num, num);
				story_title_[i].title_.color = color;
				story_title_[i].title_text_.color = color;
			}
		}
	}

	public void SetMessage(int in_title, int in_story)
	{
		text_window_.active = true;
		string text = string.Empty;
		switch ((TitleId)in_title)
		{
		case TitleId.GS1:
			text = TextDataCtrl.GetText(TextDataCtrl.TitleTextID.GS1_SCENARIO_NAME, in_story);
			break;
		case TitleId.GS2:
			text = TextDataCtrl.GetText(TextDataCtrl.TitleTextID.GS2_SCENARIO_NAME, in_story);
			break;
		case TitleId.GS3:
			text = TextDataCtrl.GetText(TextDataCtrl.TitleTextID.GS3_SCENARIO_NAME, in_story);
			break;
		}
		switch (GSStatic.global_work_.language)
		{
		case Language.JAPAN:
			start_text_[0].text = text + "\u3000" + start_text_base[0];
			start_text_[1].text = start_text_base[1];
			break;
		default:
			start_text_[0].text = start_text_base[0] + text + start_text_base[1];
			start_text_[1].text = string.Empty;
			break;
		case Language.GERMAN:
			start_text_[0].text = start_text_base[0] + "\"" + text + "\"";
			start_text_[1].text = start_text_base[1];
			break;
		case Language.KOREA:
			start_text_[0].text = text + start_text_base[0];
			start_text_[1].text = start_text_base[1];
			break;
		case Language.CHINA_S:
		case Language.CHINA_T:
			start_text_[0].text = start_text_base[0];
			start_text_[1].text = text + start_text_base[1];
			break;
		}
	}
}
