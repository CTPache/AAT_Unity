using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ConfrontWithMovie : MonoBehaviour
{
	public class SMT_WK
	{
		public byte atari_no;

		public byte change_flag;

		public short btimer;

		public short script_no;

		public short bgm_flag;

		public short frame_no;

		public void Clear()
		{
			atari_no = 0;
			change_flag = 0;
			btimer = 0;
			script_no = 0;
			bgm_flag = 0;
			frame_no = 0;
		}
	}

	private enum SMT_R1
	{
		SMT_R1_INIT = 0,
		SMT_R1_MAIN = 1,
		SMT_R1_B_BUTTON = 2,
		SMT_R1_Y_BUTTON = 3,
		SMT_R1_A_BUTTON = 4,
		SMT_R1_X_BUTTON = 5,
		SMT_R1_R_BUTTON = 6,
		SMT_R1_RESTART = 7
	}

	[SerializeField]
	private MovieController movie_controller_;

	[SerializeField]
	private MovieCollisionPlayer collision_player_;

	[SerializeField]
	private FingerCursor cursor_;

	[SerializeField]
	private Camera system_camera_;

	[SerializeField]
	private keyGuideCtrl key_guide_;

	private keyGuideCtrl movie_key_guide;

	[SerializeField]
	private Canvas parent_canvas;

	[SerializeField]
	private Camera movie_camera_;

	private List<KeyValuePair<GameObject, RawImage>> sprite_list_ = new List<KeyValuePair<GameObject, RawImage>>();

	private List<KeyValuePair<GameObject, RawImage>> noise_list_ = new List<KeyValuePair<GameObject, RawImage>>();

	private AssetBundle bundle;

	private List<MovieCollision> collisions;

	public bool auto;

	public bool first = true;

	private static readonly uint[,] atari_tbl = new uint[5, 4]
	{
		{ 162u, 165u, 158u, 168u },
		{ 157u, 166u, 158u, 168u },
		{ 157u, 164u, 160u, 168u },
		{ 161u, 165u, 158u, 169u },
		{ 161u, 165u, 158u, 168u }
	};

	public SMT_WK pSmt = new SMT_WK();

	public int target;

	private IEnumerator noise_coroutine;

	private bool bk_message_window_active;

	public static ConfrontWithMovie instance { get; private set; }

	public MovieController movie_controller
	{
		get
		{
			return movie_controller_;
		}
	}

	public MovieCollisionPlayer collision_player
	{
		get
		{
			return collision_player_;
		}
	}

	public FingerCursor cursor
	{
		get
		{
			return cursor_;
		}
		set
		{
			cursor_ = value;
		}
	}

	public keyGuideCtrl key_guide
	{
		get
		{
			return key_guide_;
		}
		set
		{
			key_guide_ = value;
		}
	}

	public keyGuideCtrl movie_guide
	{
		get
		{
			return movie_key_guide;
		}
	}

	public List<KeyValuePair<GameObject, RawImage>> sprite_list
	{
		get
		{
			return sprite_list_;
		}
	}

	public List<KeyValuePair<GameObject, RawImage>> noise_list
	{
		get
		{
			return noise_list_;
		}
	}

	public MovieController controller
	{
		get
		{
			return movie_controller;
		}
	}

	public Camera movie_camera
	{
		get
		{
			return movie_camera_;
		}
	}

	public Camera system_camera
	{
		get
		{
			return system_camera_;
		}
		set
		{
			system_camera_ = value;
		}
	}

	public bool auto_play
	{
		get
		{
			return auto;
		}
		set
		{
			auto = value;
			if (movie_controller != null)
			{
				movie_controller.auto_play = value;
			}
		}
	}

	public bool IsDetailing { get; private set; }

	private void Awake()
	{
		instance = this;
	}

	public static void Proc(SubWindow pwork)
	{
		Routine routine = pwork.routine_[pwork.stack_];
		Routine3d[] routine_3d = routine.routine_3d;
		switch (routine.r.no_1)
		{
		case 0:
			instance.sw_movie_thrust_init(pwork);
			return;
		case 1:
			instance.sw_movie_thrust_main(pwork);
			break;
		case 5:
			instance.sw_movie_thrust_x_button(pwork);
			return;
		case 2:
			instance.sw_movie_thrust_b_button(pwork);
			break;
		case 3:
			instance.sw_movie_thrust_y_button(pwork);
			break;
		case 4:
			instance.sw_movie_thrust_a_button(pwork);
			break;
		case 6:
			instance.sw_movie_thrust_r_button(pwork);
			break;
		case 7:
			instance.sw_movie_thrust_restart(pwork);
			break;
		}
		instance.sprite_list_[0].Value.enabled = false;
		instance.sprite_list_[1].Value.enabled = false;
		if (routine_3d[1].disp_off != 0)
		{
			routine_3d[1].disp_off++;
			if (routine_3d[1].disp_off > 100)
			{
				routine_3d[1].disp_off = 1;
			}
			instance.sprite_list_[0].Value.enabled = routine_3d[1].disp_off <= 50;
		}
		else if (routine_3d[2].disp_off != 0)
		{
			if (instance.movie_controller.Cinema_get_frame() >= 1390)
			{
				instance.sprite_list_[1].Value.enabled = true;
			}
			else
			{
				instance.sprite_list_[1].Value.enabled = false;
			}
		}
	}

	private void sw_movie_thrust_init(SubWindow pwork)
	{
		Routine routine = pwork.routine_[pwork.stack_];
		Routine3d[] routine_3d = routine.routine_3d;
		switch (routine.r.no_2)
		{
		case 0:
			messageBoardCtrl.instance.body_active = false;
			messageBoardCtrl.instance.InActiveNormalMessageNextTouch();
			movie_controller.StopAllObject();
			movie_controller.SetSandStorm();
			if (movie_key_guide != null && movie_key_guide.gameObject.activeSelf)
			{
				coroutineCtrl.instance.Play(movie_key_guide.close());
			}
			pwork.scan_.flag = 0;
			routine_3d[1].disp_off = 0;
			routine_3d[2].disp_off = 0;
			foreach (KeyValuePair<GameObject, RawImage> item in sprite_list_)
			{
				item.Value.enabled = false;
			}
			pSmt.btimer = 30;
			routine.r.no_2++;
			break;
		case 1:
			if (pSmt.btimer-- == 0)
			{
				pwork.bar_req_ = SubWindow.BarReq.MOVIE_THRUST;
				pSmt.change_flag = 1;
				pwork.SetObjDispFlag(30);
				routine.r.no_2++;
			}
			break;
		case 2:
			if (pwork.CheckObjOut())
			{
				CraeteKeyGuide();
				movie_key_guide.gameObject.SetActive(true);
				movie_key_guide.active = true;
				coroutineCtrl.instance.Play(movie_key_guide.open(keyGuideBase.Type.CONFRONT_WITH_MOVIE));
				routine.r.no_2++;
			}
			break;
		case 3:
			if (!movie_key_guide.CheckOpen())
			{
				cursor_.movie_keyguide_scroll_limit = (float)(systemCtrl.instance.ScreenWidth / 2) - movie_key_guide.GetGuideWidth() - keyGuideCtrl.instance.guide_space;
				movie_controller.SetAutoPlayStatus(2);
				movie_controller.Clear();
				movie_controller.gameObject.SetActive(false);
				auto_play = false;
				controller.InitData(false);
				controller.auto_unload = false;
				StartConfront();
				controller.SetAutoPlayStatus(1);
				noise_coroutine = movie_controller.NoiseAnimation();
				routine.r.no_2++;
			}
			break;
		case 4:
			pwork.busy_ = 0u;
			pwork.req_ = SubWindow.Req.NONE;
			routine_3d[2].disp_off = 1;
			if (pSmt.bgm_flag == 0)
			{
				soundCtrl.instance.PlayBGM(386);
			}
			cursor.enabled = true;
			pSmt.bgm_flag = 1;
			routine.r.no_1 = 1;
			routine.r.no_2 = 0;
			break;
		}
		cursor.ActiveTouch();
	}

	private void sw_movie_thrust_main(SubWindow pwork)
	{
		Routine routine = pwork.routine_[pwork.stack_];
		Routine3d[] routine_3d = routine.routine_3d;
		padCtrl padCtrl2 = padCtrl.instance;
		if (padCtrl2.GetKeyDown(KeyType.R))
		{
			routine_3d[2].disp_off = 0;
			routine.r.no_1 = 6;
			routine.r.no_2 = 0;
			return;
		}
		if (padCtrl2.GetKeyDown(KeyType.X))
		{
			routine_3d[2].disp_off = 0;
			routine.r.no_1 = 5;
			routine.r.no_2 = 0;
			return;
		}
		if (padCtrl2.GetKeyDown(KeyType.B))
		{
			soundCtrl.instance.PlaySE(424);
			routine.r.no_1 = 2;
			routine.r.no_2 = 0;
			return;
		}
		if (padCtrl2.GetKey(KeyType.Y))
		{
			soundCtrl.instance.PlaySE(425);
			routine.r.no_1 = 3;
			routine.r.no_2 = 0;
			return;
		}
		if (padCtrl2.GetKey(KeyType.A))
		{
			soundCtrl.instance.PlaySE(425);
			routine.r.no_1 = 4;
			routine.r.no_2 = 0;
			return;
		}
		if (!instance.movie_controller.Cinema_check_end())
		{
			if (pSmt.change_flag == 1)
			{
				routine_3d[2].disp_off = 0;
				routine.r.no_1 = 7;
				routine.r.no_2 = 0;
				return;
			}
			if ((controller.State & MovieController.PlayState.FastForwarding) == 0)
			{
				routine_3d[2].disp_off = 0;
				routine.r.no_1 = 7;
				routine.r.no_2 = 0;
				return;
			}
		}
		cursor_.UpdateCursor();
	}

	private void sw_movie_thrust_x_button(SubWindow pwork)
	{
		Routine routine = pwork.routine_[pwork.stack_];
		Routine3d[] routine_3d = routine.routine_3d;
		switch (routine.r.no_2)
		{
		case 0:
			routine_3d[1].disp_off = 0;
			messageBoardCtrl.instance.Close();
			MessageSystem.GetActiveMessageWork().message_trans_flag = 0;
			target = (byte)cursor.GetCollidedNo();
			noise_coroutine = null;
			movie_controller.StopAllObject();
			FreeAssetBundle();
			system_camera.enabled = true;
			collision_player.enabled = false;
			cursor.enabled = false;
			movie_controller.Stop();
			controller.auto_unload = true;
			routine.r.no_2++;
			break;
		case 1:
			cursor_.is_answer = true;
			soundCtrl.instance.FadeOutBGM(30);
			Balloon.PlayTakeThat();
			routine.r.no_2++;
			break;
		case 2:
			if (!objMoveCtrl.instance.is_play)
			{
				routine_3d[0].timer = 41;
				routine.r.no_2++;
				cursor.ChangeCursor();
			}
			break;
		case 3:
			if (routine_3d[0].timer == 0)
			{
				routine.r.no_2++;
			}
			else
			{
				routine_3d[0].timer--;
			}
			break;
		case 4:
			bgCtrl.instance.SetSprite(4095);
			routine.r.no_2++;
			break;
		case 5:
			routine.r.no_2++;
			break;
		case 6:
			GSStatic.message_work_.message_trans_flag = 0;
			movie_controller.Clear();
			movie_controller.gameObject.SetActive(false);
			AnimationSystem.Instance.StopCharacters();
			GSStatic.message_work_.status &= ~(MessageSystem.Status.LOOP | MessageSystem.Status.POINT_TO);
			advCtrl.instance.message_system_.SetMessage(atari_tbl[target, pSmt.atari_no]);
			messageBoardCtrl.instance.body_active = true;
			messageBoardCtrl.instance.ActiveNormalMessageNextTouch();
			if (bundle != null)
			{
				bundle.Unload(true);
			}
			MessageSystem.GetActiveMessageWork().message_trans_flag = 1;
			pwork.scan_.flag = 1;
			pwork.stack_--;
			break;
		}
	}

	private void sw_movie_thrust_b_button(SubWindow pwork)
	{
		Routine routine = pwork.routine_[pwork.stack_];
		Routine3d[] routine_3d = routine.routine_3d;
		switch (routine.r.no_2)
		{
		case 0:
			if (pSmt.change_flag == 0)
			{
				movie_controller.SetAutoPlayStatus(1);
				pSmt.change_flag = 1;
				advCtrl.instance.sub_window_.SetObjDispFlag(30);
				coroutineCtrl.instance.Play(movie_key_guide.close());
				routine_3d[1].disp_off = 0;
				controller.video_pause = false;
			}
			else
			{
				movie_controller.SetAutoPlayStatus(3);
				pSmt.change_flag = 0;
				advCtrl.instance.sub_window_.SetObjDispFlag(31);
				coroutineCtrl.instance.Play(movie_key_guide.close());
				if (routine_3d[1].disp_off == 0)
				{
					routine_3d[1].disp_off = 1;
				}
				controller.video_pause = true;
			}
			routine.r.no_2++;
			break;
		case 1:
			if (advCtrl.instance.sub_window_.CheckObjOut() && !movie_key_guide.CheckClose())
			{
				coroutineCtrl.instance.Play(movie_key_guide.open((pSmt.change_flag != 0) ? keyGuideBase.Type.CONFRONT_WITH_MOVIE : keyGuideBase.Type.CONFRONT_WITH_MOVIE_PAUSE));
				routine.r.no_2++;
			}
			break;
		case 2:
			if (advCtrl.instance.sub_window_.CheckObjIn() && pwork.bar_req_ == SubWindow.BarReq.NONE && !movie_key_guide.CheckOpen())
			{
				pwork.busy_ = 0u;
				pwork.req_ = SubWindow.Req.NONE;
				if (pSmt.change_flag == 1 && !movie_controller.Cinema_check_end())
				{
					routine.r.no_1 = 7;
					routine.r.no_2 = 0;
					return;
				}
				routine.r.no_1 = 1;
				routine.r.no_2 = 0;
			}
			break;
		}
		cursor_.UpdateCursor();
	}

	private void sw_movie_thrust_y_button(SubWindow pwork)
	{
		Routine routine = pwork.routine_[pwork.stack_];
		Routine3d[] routine_3d = routine.routine_3d;
		padCtrl padCtrl2 = padCtrl.instance;
		if (pSmt.change_flag == 0)
		{
			if (!padCtrl2.GetKey(KeyType.Y))
			{
				soundCtrl.instance.PlaySE(426);
				controller.SetAutoPlaySpeed(100);
				controller.SetAutoPlayStatus(3);
				if (routine_3d[1].disp_off == 0)
				{
					routine_3d[1].disp_off = 1;
				}
				routine.r.no_1 = 1;
				routine.r.no_2 = 0;
				return;
			}
			switch (routine.r.no_2)
			{
			case 0:
				controller.SetAutoPlaySpeed(100);
				controller.SetAutoPlayStatus(4);
				controller.SetAutoPlayStatus(8192);
				if (routine_3d[1].disp_off == 0)
				{
					routine_3d[1].disp_off = 1;
				}
				pSmt.btimer = 30;
				routine.r.no_2++;
				break;
			case 1:
				pSmt.btimer--;
				if (pSmt.btimer == 0)
				{
					controller.SetAutoPlaySpeed(100);
					controller.SetAutoPlayStatus(4);
					routine_3d[1].disp_off = 0;
					routine.r.no_2++;
				}
				break;
			}
		}
		else
		{
			if (!padCtrl2.GetKey(KeyType.Y))
			{
				soundCtrl.instance.PlaySE(426);
				controller.SetAutoPlaySpeed(100);
				controller.SetAutoPlayStatus(1);
				routine.r.no_1 = 1;
				routine.r.no_2 = 0;
				noise_coroutine = movie_controller.NoiseAnimation();
				movie_controller.StopNoise();
				return;
			}
			if (routine.r.no_2 == 0)
			{
				controller.SetAutoPlaySpeed(400);
				controller.SetAutoPlayStatus(4);
				routine_3d[1].disp_off = 0;
				routine.r.no_2++;
			}
			noise_coroutine.MoveNext();
		}
		cursor_.UpdateCursor();
	}

	private void sw_movie_thrust_a_button(SubWindow pwork)
	{
		Routine routine = pwork.routine_[pwork.stack_];
		Routine3d[] routine_3d = routine.routine_3d;
		padCtrl padCtrl2 = padCtrl.instance;
		if (pSmt.change_flag == 0)
		{
			if (!padCtrl2.GetKey(KeyType.A))
			{
				soundCtrl.instance.PlaySE(426);
				controller.SetAutoPlaySpeed(100);
				controller.SetAutoPlayStatus(3);
				if (routine_3d[1].disp_off == 0)
				{
					routine_3d[1].disp_off = 1;
				}
				routine.r.no_1 = 1;
				routine.r.no_2 = 0;
				return;
			}
			switch (routine.r.no_2)
			{
			case 0:
				controller.SetAutoPlaySpeed(100);
				controller.SetAutoPlayStatus(1);
				controller.SetAutoPlayStatus(8192);
				if (routine_3d[1].disp_off == 0)
				{
					routine_3d[1].disp_off = 1;
				}
				pSmt.btimer = 30;
				routine.r.no_2++;
				break;
			case 1:
				if (routine_3d[1].disp_off == 0)
				{
					routine_3d[1].disp_off = 1;
				}
				pSmt.btimer--;
				if (pSmt.btimer == 0)
				{
					controller.SetAutoPlaySpeed(100);
					controller.SetAutoPlayStatus(1);
					routine_3d[1].disp_off = 0;
					routine.r.no_2++;
				}
				break;
			}
		}
		else
		{
			if (!instance.movie_controller.Cinema_check_end())
			{
				routine.r.no_1 = 7;
				routine.r.no_2 = 0;
				return;
			}
			if (!padCtrl2.GetKey(KeyType.A))
			{
				soundCtrl.instance.PlaySE(426);
				controller.SetAutoPlaySpeed(100);
				controller.SetAutoPlayStatus(1);
				routine.r.no_1 = 1;
				routine.r.no_2 = 0;
				noise_coroutine = movie_controller.NoiseAnimation();
				movie_controller.StopNoise();
				return;
			}
			if (routine.r.no_2 == 0)
			{
				controller.SetAutoPlaySpeed(400);
				controller.SetAutoPlayStatus(1);
				routine.r.no_2++;
			}
			noise_coroutine.MoveNext();
		}
		cursor_.UpdateCursor();
	}

	private void sw_movie_thrust_r_button(SubWindow pwork)
	{
		Routine routine = pwork.routine_[pwork.stack_];
		Routine3d[] routine_3d = routine.routine_3d;
		GlobalWork global_work_ = GSStatic.global_work_;
		switch (routine.r.no_2)
		{
		case 0:
			global_work_.r_bk.Set(global_work_.r.no_0, global_work_.r.no_1, global_work_.r.no_2, global_work_.r.no_3);
			global_work_.r.Set(8, 0, 0, 0);
			if (movie_key_guide.current_guide == keyGuideBase.Type.CONFRONT_WITH_MOVIE)
			{
				coroutineCtrl.instance.Play(movie_key_guide.close());
			}
			Cinema.Cinema_set_status(3u);
			movie_controller.SetAutoPlayStatus(3);
			pwork.busy_ = 3u;
			routine.r.no_2++;
			break;
		case 1:
			if (!movie_key_guide.CheckClose())
			{
				routine_3d[1].disp_off = 0;
				routine_3d[2].disp_off = 0;
				pwork.scan_.flag = 1;
				pwork.SetObjDispFlag(37);
				pwork.bar_req_ = SubWindow.BarReq.IDLE;
				if (movie_key_guide.current_guide == keyGuideBase.Type.CONFRONT_WITH_MOVIE)
				{
					coroutineCtrl.instance.Play(movie_key_guide.open(keyGuideBase.Type.CONFRONT_WITH_MOVIE_PAUSE));
				}
				routine.r.no_2++;
			}
			break;
		case 2:
			if (!movie_key_guide.CheckOpen())
			{
				pwork.req_ = SubWindow.Req.NONE;
				pSmt.frame_no = (short)instance.controller.Frame;
				routine.r.Set(29, 6, 3, 0);
				pwork.stack_++;
				pwork.routine_[pwork.stack_].r.Set(11, 0, 0, 0);
				pwork.routine_[pwork.stack_].tex_no = routine.tex_no;
			}
			break;
		case 3:
			pwork.scan_.flag = 0;
			movie_controller.gameObject.SetActive(true);
			pwork.bar_req_ = SubWindow.BarReq.MOVIE_THRUST;
			pSmt.change_flag = 0;
			routine_3d[1].disp_off = 1;
			routine_3d[2].disp_off = 1;
			pwork.SetObjDispFlag(31);
			routine.r.no_2++;
			break;
		case 4:
			routine.r.no_2++;
			break;
		case 5:
			pwork.busy_ = 0u;
			pwork.req_ = SubWindow.Req.NONE;
			routine.r.no_1 = 1;
			routine.r.no_2 = 0;
			cursor.ActiveTouch();
			movie_key_guide.ActiveKeyTouch();
			break;
		}
	}

	private void sw_movie_thrust_restart(SubWindow pwork)
	{
		Routine routine = pwork.routine_[pwork.stack_];
		Routine3d[] routine_3d = routine.routine_3d;
		switch (routine.r.no_2)
		{
		case 0:
			routine_3d[1].disp_off = 0;
			pwork.SetObjDispFlag(37);
			routine.r.no_2++;
			break;
		case 1:
			if (pwork.CheckObjOut())
			{
				routine.r.no_1 = 0;
				routine.r.no_2 = 0;
			}
			break;
		}
	}

	public void StartConfront()
	{
		if (first)
		{
			lifeGaugeCtrl lifeGaugeCtrl2 = lifeGaugeCtrl.instance;
			if (lifeGaugeCtrl2.gauge_mode != 0 && lifeGaugeCtrl2.gauge_mode != 8)
			{
				lifeGaugeCtrl2.ActionLifeGauge(lifeGaugeCtrl.Gauge_State.LIFE_OFF_MOMENT);
			}
		}
		movie_controller.gameObject.SetActive(true);
		collision_player.enabled = true;
		cursor.enabled = true;
		coroutineCtrl.instance.Play(PlayAfterLoaded(0));
	}

	public void StartDetail()
	{
		IsDetailing = true;
		movie_controller.gameObject.SetActive(true);
		movie_controller.InitData(true);
		coroutineCtrl.instance.Play(PlayAfterLoaded(0));
		movie_controller.SetAutoPlayStatus(1);
		movie_controller.SetAutoPlayStatus(32768);
	}

	public void SetScreenActivate()
	{
		foreach (Transform item in base.transform)
		{
			item.gameObject.SetActive(true);
		}
		cursor.gameObject.SetActive(false);
	}

	private void ChangeTextPosZ(Transform trans)
	{
		Vector3 zero = Vector3.zero;
		foreach (Transform tran in trans)
		{
			if (tran.name == "text")
			{
				zero = tran.localPosition;
				tran.localPosition = new Vector3(zero.x, zero.y, -6f);
			}
			ChangeTextPosZ(tran);
		}
	}

	private void CraeteKeyGuide()
	{
		if (movie_key_guide == null)
		{
			movie_key_guide = Object.Instantiate(key_guide.gameObject).GetComponent<keyGuideCtrl>();
			movie_key_guide.transform.SetParent(parent_canvas.transform);
			movie_key_guide.gameObject.layer = parent_canvas.gameObject.layer;
			Utility.SetLayerAllChildren(movie_key_guide.transform, LayerMask.NameToLayer("movie"));
			movie_key_guide.transform.localPosition = new Vector3(0f, -470f, -20f);
			ChangeTextPosZ(movie_key_guide.transform);
		}
	}

	private IEnumerator PlayAfterLoaded(int playTarget, int start_idx = 0, int end_idx = 0)
	{
		CraeteKeyGuide();
		if (IsDetailing)
		{
			system_camera.enabled = false;
			bk_message_window_active = messageBoardCtrl.instance.body_active;
			messageBoardCtrl.instance.body_active = false;
		}
		if (bundle == null)
		{
			string path = Application.streamingAssetsPath + string.Format("/GS1/moviescriptable/film02_confront.unity3d");
			bundle = AssetBundle.LoadFromFile(path);
		}
		if (sprite_list_.Count == 0)
		{
			SetMovieObject();
		}
		movie_controller.Play(bundle);
		if (!IsDetailing)
		{
			if (collisions == null)
			{
				AssetBundle assetBundle = AssetBundleCtrl.instance.load("/GS1/OtherInclusions/", "moviecollisions");
				Object[] source = assetBundle.LoadAllAssets();
				collisions = (from col in source
					orderby col.name
					select col as MovieCollision).ToList();
			}
			yield return null;
		}
		SetScreenActivate();
		movie_key_guide.gameObject.SetActive(true);
		movie_key_guide.active = true;
		if (IsDetailing)
		{
			coroutineCtrl.instance.Play(movie_key_guide.open(keyGuideBase.Type.VIDEO_DETAIL));
		}
		else if (auto_play)
		{
			movie_key_guide.gameObject.SetActive(false);
		}
		if (!IsDetailing)
		{
			if (!auto)
			{
				cursor.gameObject.SetActive(true);
			}
			collision_player.Play(collisions);
			cursor.Initialize();
		}
	}

	private void SetMovieObject()
	{
		if (auto_play && !IsDetailing)
		{
			return;
		}
		string text = "eff004";
		switch (GSStatic.global_work_.language)
		{
		case Language.USA:
			text += GSUtility.GetResourceNameLanguage(Language.USA);
			break;
		default:
			text = ReplaceLanguage.GetFileName("/GS1/minigame/", text, (int)GSStatic.global_work_.language);
			break;
		case Language.JAPAN:
			break;
		}
		List<string> list = new List<string>();
		list.Add(text);
		list.Add("eff00c");
		List<string> list2 = list;
		List<Vector3> list3 = new List<Vector3>();
		list3.Add(new Vector3(-510f, 450f, -20f));
		list3.Add(new Vector3(600f, 450f, -20f));
		List<Vector3> list4 = list3;
		list3 = new List<Vector3>();
		list3.Add(Vector3.one);
		list3.Add(Vector3.one);
		List<Vector3> list5 = list3;
		list = new List<string>();
		list.Add("etc04b");
		list.Add("etc04b");
		list.Add("etc04b");
		list.Add("etc04b");
		list.Add("etc04b");
		List<string> list6 = list;
		list3 = new List<Vector3>();
		list3.Add(new Vector3(0f, 510f, -20f));
		list3.Add(new Vector3(0f, 68f, -20f));
		list3.Add(new Vector3(0f, 34f, -20f));
		list3.Add(new Vector3(0f, 0f, -20f));
		list3.Add(new Vector3(0f, -510f, -20f));
		List<Vector3> list7 = list3;
		list3 = new List<Vector3>();
		list3.Add(Vector3.zero);
		list3.Add(Vector3.zero);
		list3.Add(new Vector3(180f, 0f, 180f));
		list3.Add(new Vector3(180f, 0f, 0f));
		list3.Add(Vector3.zero);
		List<Vector3> list8 = list3;
		Transform parent = base.transform.Find("canvas").transform;
		int num = 0;
		foreach (string item in list2)
		{
			GameObject gameObject = new GameObject();
			gameObject.transform.parent = parent;
			gameObject.name = item;
			gameObject.layer = base.gameObject.layer;
			gameObject.transform.localPosition = list4[num];
			gameObject.transform.localScale = list5[num];
			RawImage rawImage = gameObject.AddComponent<RawImage>();
			AssetBundle assetBundle = AssetBundleCtrl.instance.load("/GS1/minigame/", item);
			Sprite sprite = null;
			if (assetBundle != null)
			{
				sprite = assetBundle.LoadAsset<Sprite>(item);
			}
			rawImage.texture = sprite.texture;
			rawImage.rectTransform.sizeDelta = new Vector2(sprite.texture.width, sprite.texture.height);
			sprite_list_.Add(new KeyValuePair<GameObject, RawImage>(gameObject, rawImage));
			rawImage.enabled = false;
			num++;
		}
		num = 0;
		foreach (string item2 in list6)
		{
			GameObject gameObject2 = new GameObject();
			gameObject2.transform.parent = parent;
			gameObject2.name = item2;
			gameObject2.layer = base.gameObject.layer;
			gameObject2.transform.localPosition = list7[num];
			gameObject2.transform.Rotate(list8[num]);
			gameObject2.transform.localScale = new Vector3(0.75f, 1f, 1f);
			RawImage rawImage2 = gameObject2.AddComponent<RawImage>();
			AssetBundle assetBundle2 = AssetBundleCtrl.instance.load("/GS1/minigame/", item2);
			Sprite sprite2 = null;
			if (assetBundle2 != null)
			{
				sprite2 = assetBundle2.LoadAsset<Sprite>(item2);
			}
			rawImage2.texture = sprite2.texture;
			rawImage2.rectTransform.sizeDelta = new Vector2(sprite2.texture.width, sprite2.texture.height);
			noise_list_.Add(new KeyValuePair<GameObject, RawImage>(gameObject2, rawImage2));
			gameObject2.SetActive(false);
			num++;
		}
	}

	public void StopDetail()
	{
		movie_key_guide.gameObject.SetActive(false);
		foreach (Transform item in base.transform)
		{
			item.gameObject.SetActive(false);
		}
		movie_controller.StopAllObject();
		FreeAssetBundle();
		movie_controller.SetAutoPlayStatus(2);
		movie_controller.Clear();
		movie_controller.gameObject.SetActive(false);
		if (bk_message_window_active)
		{
			messageBoardCtrl.instance.body_active = true;
		}
		if (bundle != null)
		{
			bundle.Unload(true);
		}
		IsDetailing = false;
		system_camera.enabled = true;
	}

	private void FreeAssetBundle()
	{
		foreach (KeyValuePair<GameObject, RawImage> item in sprite_list_)
		{
			GameObject key = item.Key;
			Object.Destroy(key);
		}
		sprite_list_.Clear();
		foreach (KeyValuePair<GameObject, RawImage> item2 in noise_list_)
		{
			GameObject key2 = item2.Key;
			Object.Destroy(key2);
		}
		noise_list_.Clear();
	}

	public void EndAutoPlay(bool kill = false)
	{
		if (controller.auto_state == 2 || first || kill)
		{
			foreach (Transform item in base.transform)
			{
				item.gameObject.SetActive(false);
			}
			movie_controller.Stop();
			movie_controller.Clear();
			movie_controller.gameObject.SetActive(false);
			collision_player.enabled = false;
			cursor.enabled = false;
			if (first)
			{
				lifeGaugeCtrl lifeGaugeCtrl2 = lifeGaugeCtrl.instance;
				if (lifeGaugeCtrl2.gauge_mode != 0 && lifeGaugeCtrl2.gauge_mode != 8)
				{
					lifeGaugeCtrl2.ActionLifeGauge(lifeGaugeCtrl.Gauge_State.LIFE_ON);
				}
				first = false;
			}
		}
		else
		{
			movie_controller.Stop();
			collision_player.enabled = false;
			cursor.enabled = false;
		}
		if (bundle != null)
		{
			bundle.Unload(true);
		}
		system_camera.enabled = true;
	}

	public void CheckEndPlay()
	{
		coroutineCtrl.instance.Play(CheckEndPlayCoroutine());
	}

	private IEnumerator CheckEndPlayCoroutine()
	{
		fadeCtrl.instance.status = fadeCtrl.Status.FADE_IN;
		while (controller.is_play)
		{
			yield return null;
		}
		EndAutoPlay(false);
		fadeCtrl.instance.status = fadeCtrl.Status.NO_FADE;
		if (bundle != null)
		{
			bundle.Unload(true);
			bundle = null;
		}
	}
}
