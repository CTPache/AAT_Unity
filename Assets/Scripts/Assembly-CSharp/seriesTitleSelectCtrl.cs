using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class seriesTitleSelectCtrl : sceneCtrl
{
	[Serializable]
	public class Title
	{
		public AssetBundleSprite title_;

		public AssetBundleSprite copyright_;

		public bool active
		{
			get
			{
				return title_.transform.parent.gameObject.activeSelf;
			}
			set
			{
				title_.transform.parent.gameObject.SetActive(value);
			}
		}

		public float pos_x
		{
			get
			{
				return title_.transform.parent.localPosition.x;
			}
			set
			{
				title_.transform.parent.localPosition = new Vector3(value, 0f, 0f);
			}
		}
	}

	[SerializeField]
	private titleSelectPlate select_plate_;

	[SerializeField]
	private AssetBundleSprite title_back_;

	[SerializeField]
	private GameObject title_list_body_;

	[SerializeField]
	private List<Title> title_list_;

	[SerializeField]
	private arrowCtrl arrow_ctrl_;

	[SerializeField]
	private Text select_plate_text_;

	private IEnumerator coroutine_copyrightfade_;

	private TitleId title_no_;

	private float title_space_ = 1920f;

	private bool is_decide_;

	public static seriesTitleSelectCtrl instance { get; private set; }

	public bool is_decide
	{
		get
		{
			return is_decide_;
		}
	}

	private void Awake()
	{
		instance = this;
	}

	public override void Play()
	{
		base.End();
		enumerator_state_ = stateCoroutine();
		coroutineCtrl.instance.Play(enumerator_state_);
	}

	private void Init()
	{
		base.body.SetActive(true);
		title_list_body_.SetActive(true);
		title_back_.active = true;
		int num = 0;
		Vector3 zero = Vector3.zero;
		switch (GSStatic.global_work_.language)
		{
		case Language.JAPAN:
		case Language.CHINA_S:
			num = 46;
			zero = new Vector3(0f, 0f, -30f);
			break;
		default:
			num = 37;
			zero = new Vector3(0f, 4f, -30f);
			break;
		case Language.KOREA:
		case Language.CHINA_T:
			num = 40;
			zero = new Vector3(0f, 0f, -30f);
			break;
		}
		for (int i = 0; i < title_list_.Count; i++)
		{
			string text = (i + 1).ToString("D1");
			title_list_[i].title_.load("/GS" + text + "/BG/", "titlegs" + text + GSUtility.GetResourceNameLanguage(GSStatic.global_work_.language), true);
			title_list_[i].copyright_.load("/GS" + text + "/BG/", "copygs" + text, true);
		}
		title_back_.load("/menu/title/", "title_back");
		changeTitle(title_no_);
		select_plate_.body_active = true;
		select_plate_.mainTitleInit(new string[1] { TextDataCtrl.GetText(TextDataCtrl.TitleTextID.PLAY_TITLE) }, "select_window", 0);
		select_plate_.playCursor(0);
		select_plate_.igunore_input = true;
		select_plate_.SetTextFontsize(num);
		select_plate_.SetTextPosition(zero);
		arrow_ctrl_.load();
		arrow_ctrl_.arrowAll(true);
		arrow_ctrl_.SetTouchKeyType(KeyType.Right, 0);
		arrow_ctrl_.SetTouchKeyType(KeyType.Left, 1);
		arrow_ctrl_.ActiveArrow();
		base.is_end = false;
		mainCtrl.instance.addText(select_plate_text_);
	}

	public void changeTitle(TitleId title_id)
	{
		for (int i = 0; i < title_list_.Count; i++)
		{
			int num = (int)(i - title_id);
			num = ((num >= -1) ? num : (num + title_list_.Count));
			num = ((num <= 1) ? num : (num - title_list_.Count));
			float pos_x = title_space_ * (float)num;
			title_list_[i].pos_x = pos_x;
		}
		title_list_body_.transform.localPosition = Vector3.zero;
	}

	public override void End()
	{
		select_plate_.End();
		arrow_ctrl_.arrowAll(false);
		base.End();
		mainCtrl.instance.removeText(select_plate_text_);
	}

	private IEnumerator CoroutineSlideMove(int in_dir)
	{
		float time = 0f;
		float speed = title_space_ * (float)in_dir / 0.5f;
		while (true)
		{
			time += 0.03f;
			if (time > 0.5f)
			{
				break;
			}
			title_list_body_.transform.localPosition = new Vector3(speed * time, 0f, 0f);
			yield return null;
		}
		title_list_body_.transform.localPosition = new Vector3(speed, 0f, 0f);
		changeTitle(title_no_);
	}

	private void PlayCoroutineCopyrightFade(bool _playmode)
	{
		if (coroutine_copyrightfade_ != null)
		{
			coroutineCtrl.instance.Stop(coroutine_copyrightfade_);
		}
		if (_playmode)
		{
			coroutine_copyrightfade_ = CoroutineCopyrightFade(_playmode, 15);
		}
		else
		{
			coroutine_copyrightfade_ = CoroutineCopyrightFade(_playmode, 1);
		}
		coroutineCtrl.instance.Play(coroutine_copyrightfade_);
	}

	private IEnumerator CoroutineCopyrightFade(bool in_fade_type, int in_fade_time)
	{
		int timer = 0;
		float alpha_speed = 1f / (float)in_fade_time;
		Color copyright_color = Color.white;
		if (in_fade_type)
		{
			alpha_speed *= -1f;
			copyright_color.a = 0f;
		}
		while (timer < in_fade_time)
		{
			foreach (Title item in title_list_)
			{
				copyright_color.a -= alpha_speed;
				item.copyright_.sprite_renderer_.color = copyright_color;
			}
			timer++;
			yield return null;
		}
	}

	private IEnumerator CoroutineSlideState(int in_dir)
	{
		PlayCoroutineCopyrightFade(false);
		arrow_ctrl_.arrowAll(false);
		select_plate_.body_active = false;
		do
		{
			if (in_dir > 0)
			{
				title_no_--;
				title_no_ = ((title_no_ >= TitleId.GS1) ? title_no_ : TitleId.GS3);
			}
			else
			{
				title_no_++;
				title_no_ = ((title_no_ <= TitleId.GS3) ? title_no_ : TitleId.GS1);
			}
			soundCtrl.instance.PlaySE(42);
			yield return coroutineCtrl.instance.Play(CoroutineSlideMove(in_dir));
		}
		while ((in_dir > 0 && (padCtrl.instance.GetKey(KeyType.Left) || padCtrl.instance.GetKey(KeyType.StickL_Left))) || (in_dir < 0 && (padCtrl.instance.GetKey(KeyType.Right) || padCtrl.instance.GetKey(KeyType.StickL_Right))));
		arrow_ctrl_.arrowAll(true);
		select_plate_.body_active = true;
		select_plate_.playCursor(0);
		PlayCoroutineCopyrightFade(true);
	}

	private IEnumerator stateCoroutine()
	{
		Init();
		fadeCtrl.instance.play(fadeCtrl.Status.FADE_IN, 30u, 16u);
		while (!fadeCtrl.instance.is_end)
		{
			yield return null;
		}
		coroutineCtrl.instance.Play(titleGuideCtrl.instance.open(keyGuideBase.Type.GS_SELECT));
		bool is_push = false;
		is_decide_ = false;
		select_plate_.igunore_input = false;
		while (true)
		{
			if (!select_plate_.is_play_animation)
			{
				if (select_plate_.is_end)
				{
					if (select_plate_.cursor_no == 0)
					{
						is_push = true;
						base.is_end = true;
						break;
					}
				}
				else if (padCtrl.instance.GetKeyDown(KeyType.B))
				{
					is_push = false;
					base.is_end = true;
					soundCtrl.instance.PlaySE(44);
				}
				else if (padCtrl.instance.IsNextMove())
				{
					if (padCtrl.instance.GetKeyDown(KeyType.Left) || padCtrl.instance.GetKeyDown(KeyType.StickL_Left) || padCtrl.instance.GetWheelMoveUp())
					{
						yield return coroutineCtrl.instance.Play(CoroutineSlideState(1));
					}
					else if (padCtrl.instance.GetKeyDown(KeyType.Right) || padCtrl.instance.GetKeyDown(KeyType.StickL_Right) || padCtrl.instance.GetWheelMoveDown())
					{
						yield return coroutineCtrl.instance.Play(CoroutineSlideState(-1));
					}
				}
			}
			if (base.is_end)
			{
				base.is_end = false;
				break;
			}
			yield return null;
		}
		is_decide_ = true;
		coroutineCtrl.instance.Play(titleGuideCtrl.instance.close());
		fadeCtrl.instance.play(fadeCtrl.Status.FADE_OUT, 30u, 16u);
		while (!fadeCtrl.instance.is_end)
		{
			yield return null;
		}
		while (titleGuideCtrl.instance.CheckClose())
		{
			yield return null;
		}
		titleCtrlRoot.instance.data_system.title_no = (int)title_no_;
		if (is_push)
		{
			GSStatic.global_work_.title = title_no_;
			GSStatic.open_sce_.OpenScenarioSet(GSStatic.save_sys_, GSStatic.global_work_);
			titleCtrlRoot.instance.Scene(titleCtrlRoot.SceneType.Story);
		}
		else
		{
			titleCtrlRoot.instance.Scene(titleCtrlRoot.SceneType.Top);
		}
		is_decide_ = false;
		enumerator_state_ = null;
		End();
	}
}
