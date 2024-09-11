using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class scienceInvestigationCtrl : MonoBehaviour
{
	private class StInvestigateType
	{
		public readonly InvestigateType type_;

		public readonly keyGuideBase.Type guide_ = keyGuideBase.Type.KAGAKU_SYOUSAI;

		public StInvestigateType(InvestigateType in_type, keyGuideBase.Type in_guide)
		{
			type_ = in_type;
			guide_ = in_guide;
		}
	}

	[Space(20f)]
	public InvestigateType play_test_type;

	[Range(0f, 64f)]
	public int play_test_obj_id;

	[Space(20f)]
	public Vector3 request_test_pos = new Vector3(0f, 0f, 0f);

	public Vector3 request_test_ang = new Vector3(0f, 0f, 0f);

	public float request_test_scale = 1f;

	public float request_test_seconds = 1f;

	[Space(20f)]
	[Range(-1f, 16f)]
	public int check_test_index = -1;

	[Space(20f)]
	[SerializeField]
	private Camera camera_;

	[SerializeField]
	private GameObject body_;

	[SerializeField]
	private SpriteRenderer background_renderer_;

	[SerializeField]
	private evidenceObjectManager evidence_manager_;

	[SerializeField]
	private evidenceCallbackManager callback_manager_;

	[SerializeField]
	private AssetBundleSprite cursor_;

	[SerializeField]
	private RectTransform movable_area_;

	[SerializeField]
	private Rect taboo_area_ = default(Rect);

	[SerializeField]
	private InputTouch rotate_touch_area_;

	[SerializeField]
	private InputTouch cursor_touch_area_;

	[SerializeField]
	[Range(0f, 100f)]
	private float zoom_speed_ = 0.04f;

	[SerializeField]
	[Range(0f, 100f)]
	private float rotate_speed_ = 5f;

	[SerializeField]
	[Range(0f, 100f)]
	private float touch_rotate_speed_ = 5f;

	[SerializeField]
	[Range(0f, 100f)]
	private float move_speed_ = 10f;

	[SerializeField]
	[Range(0f, 2f)]
	private float ray_radius_;

	[SerializeField]
	[Range(-5f, 5f)]
	private float ray_adjust_x_;

	[SerializeField]
	private float drag_offset_ = 30f;

	[SerializeField]
	private Camera science_camera_;

	private GameObject back_to_body_;

	private MessageSystem.Status last_status_;

	private bool last_message_active_;

	private bool last_selection_active_;

	private ushort last_now_no_;

	private ushort last_next_no_;

	private uint last_mdt_index_;

	private uint last_mdt_index_top_;

	private List<string> last_message_line_ = new List<string>();

	private List<float> last_message_line_x_ = new List<float>();

	private int last_message_color_;

	private int last_item_plate_id_ = -1;

	private int last_face_plate_id_ = -1;

	private int last_item_plate_type_ = -1;

	private int last_face_plate_type_ = -1;

	private byte last_Mess_move_flag_;

	private byte last_op_para;

	private ushort last_opwork_7;

	private ushort last_all_work;

	private bool is_play_;

	private polyData poly_data_;

	private StInvestigateType investigate_type_;

	private int check_point_index_ = -1;

	private bool called_back_;

	private bool called_check_;

	private bool called_present_;

	private bool is_operate_key_;

	private bool is_ending_failed_;

	private Vector2 delta_ = Vector2.zero;

	private Vector2 preve_ = Vector2.zero;

	private bool is_drag_;

	private Vector3 preve_pos_ = Vector3.zero;

	private bool standby_gauge_;

	private keyGuideBase.Type standby_keyguid_type_;

	private float guide_limit_;

	private float cursor_init_pos_z_ = -2700f;

	private bool is_cursor_drag_;

	private bool is_cursor_down_;

	public bool debug_show_area;

	private float ScaleMin = 1f;

	private float ScaleMax = 2f;

	private const int InvalidPointIndex = -1;

	private readonly List<StInvestigateType> ui_setting_data_ = new List<StInvestigateType>
	{
		new StInvestigateType(InvestigateType.NORMAL, keyGuideBase.Type.KAGAKU_SYOUSAI),
		new StInvestigateType(InvestigateType.FORCE, keyGuideBase.Type.FORCE_KAGAKU_SYOUSAI),
		new StInvestigateType(InvestigateType.PRESENT, keyGuideBase.Type.PRESENT_KAGAKU_SYOUSAI),
		new StInvestigateType(InvestigateType.ENDING, keyGuideBase.Type.ENDING_KAGAKU_SYOUSAI)
	};

	public static scienceInvestigationCtrl instance { get; private set; }

	private bool IsBackable
	{
		get
		{
			return investigate_type_.type_ == InvestigateType.NORMAL;
		}
	}

	private bool IsZoomable
	{
		get
		{
			return investigate_type_.type_ != InvestigateType.PRESENT;
		}
	}

	private bool IsPresentable
	{
		get
		{
			return investigate_type_.type_ == InvestigateType.PRESENT;
		}
	}

	public GameObject body
	{
		get
		{
			return body_;
		}
	}

	public evidenceObjectManager evidence_manager
	{
		get
		{
			return evidence_manager_;
		}
	}

	public int check_point_index
	{
		get
		{
			return check_point_index_;
		}
	}

	public int check_state
	{
		get
		{
			return callback_manager_.state;
		}
	}

	public int hit_point_index { get; private set; }

	public bool is_play_ready { get; private set; }

	public bool is_play
	{
		get
		{
			return is_play_;
		}
	}

	public bool called_back
	{
		get
		{
			return called_back_;
		}
	}

	public bool called_check
	{
		get
		{
			return called_check_;
		}
	}

	public bool called_present
	{
		get
		{
			return called_present_;
		}
	}

	public bool is_ending_failed
	{
		get
		{
			return is_ending_failed_;
		}
	}

	public bool is_exist_mesasge
	{
		get
		{
			return callback_manager_.is_exist_mesasge;
		}
	}

	public bool active_message
	{
		get
		{
			return callback_manager_.runnning_mesasge;
		}
	}

	public bool active_cursor
	{
		get
		{
			return cursor_.sprite_renderer_.enabled;
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

	public Camera science_camera
	{
		get
		{
			return science_camera_;
		}
	}

	public int poly_obj_id { get; set; }

	public InvestigateType mode_type { get; set; }

	public bool mess_win_out { get; set; }

	public byte speaker_id { get; set; }

	public byte win_name_set { get; set; }

	private void Awake()
	{
		instance = this;
	}

	public bool IsUsable()
	{
		if (play_test_obj_id > 0)
		{
			return true;
		}
		if ((uint)GSStatic.global_work_.scenario >= 17u && (((uint)GSStatic.global_work_.scenario < 19u && GSFlag.Check(0u, scenario.SCE4_FLAG_STATUS_3D_ENABLE)) || (uint)GSStatic.global_work_.scenario >= 19u))
		{
			return true;
		}
		return false;
	}

	public bool IsEnding()
	{
		bool flag = true;
		flag &= GSStatic.global_work_.title == TitleId.GS1 && (uint)GSStatic.global_work_.scenario >= 34u;
		return flag & (advCtrl.instance.sub_window_.tutorial_ == 30);
	}

	public bool IsAnyKey()
	{
		if (is_operate_key_)
		{
			return true;
		}
		if (called_back_)
		{
			return true;
		}
		if (called_check_)
		{
			return true;
		}
		if (called_present_)
		{
			return true;
		}
		return false;
	}

	public IEnumerator Play(InvestigateType in_type, int in_obj_id, GameObject in_body)
	{
		back_to_body_ = in_body;
		return Play(in_type, in_obj_id);
	}

	public IEnumerator Play(InvestigateType in_type, int in_obj_id)
	{
		active = true;
		is_play_ = true;
		is_play_ready = false;
		if (play_test_obj_id > 0)
		{
			in_type = play_test_type;
			in_obj_id = play_test_obj_id;
		}
		InitMembers(in_type);
		poly_data_ = polyDataCtrl.instance.GetPolyData(in_obj_id - 1);
		TouchInit();
		return StatePlayCoroutine();
	}

	private void TouchInit()
	{
		rotate_touch_area_.touch_event = delegate(TouchParameter p)
		{
			if (!is_drag_)
			{
				cursor_.transform.position = p.pos;
				Vector3 localPosition = cursor_.transform.localPosition;
				localPosition.z = cursor_init_pos_z_;
				localPosition.y = Mathf.Clamp(localPosition.y, movable_area_.rect.y, movable_area_.rect.height / 2f);
				if (localPosition.y < taboo_area_.y && localPosition.y > taboo_area_.y - taboo_area_.height && localPosition.x > guide_limit_ && localPosition.x < taboo_area_.x + taboo_area_.width)
				{
					localPosition.y = taboo_area_.y;
					cursor_.transform.localPosition = localPosition;
				}
			}
		};
		rotate_touch_area_.down_event = delegate
		{
			delta_ = Vector2.zero;
			preve_ = TouchUtility.GetTouchPosition();
			preve_pos_ = preve_;
			is_drag_ = false;
		};
		rotate_touch_area_.drag_event = delegate
		{
			if (rotate_touch_area_.box_collider_2d_enable || called_check_ || called_back_ || called_present_)
			{
				Vector3 touchPosition = TouchUtility.GetTouchPosition();
				delta_ = preve_ - (Vector2)touchPosition;
				if (is_drag_)
				{
					evidence_manager_.AddVerticalRotate(Mathf.Clamp(0f - delta_.y, 0f - touch_rotate_speed_, touch_rotate_speed_));
					evidence_manager_.AddHorizontalRotate(Mathf.Clamp(delta_.x, 0f - touch_rotate_speed_, touch_rotate_speed_));
				}
				preve_ = TouchUtility.GetTouchPosition();
				float f = Vector2.Distance(preve_pos_, touchPosition);
				if (Mathf.Abs(f) > drag_offset_ && !is_drag_)
				{
					is_drag_ = true;
				}
			}
		};
		cursor_touch_area_.touch_key_type = KeyType.None;
		cursor_touch_area_.touch_event = delegate
		{
			if (is_cursor_drag_)
			{
				cursor_touch_area_.touch_key_type = KeyType.None;
			}
			else
			{
				cursor_touch_area_.touch_key_type = ((!IsPresentable) ? KeyType.A : KeyType.X);
			}
		};
		cursor_touch_area_.down_event = delegate
		{
			delta_ = Vector2.zero;
			preve_ = TouchUtility.GetTouchPosition();
			preve_pos_ = preve_;
			is_cursor_drag_ = false;
			is_cursor_down_ = true;
		};
		rotate_touch_area_.ActiveCollider();
		cursor_touch_area_.ActiveCollider();
	}

	private void FixedUpdate()
	{
		Process();
	}

	private void Process()
	{
		UpdateCursorTouchObjectRotate();
	}

	private void UpdateCursorTouchObjectRotate()
	{
		TouchInfo touch = TouchUtility.GetTouch();
		if (touch == TouchInfo.Move && !called_check_ && !called_back_ && !called_present_ && active && is_cursor_down_)
		{
			if (!rotate_touch_area_.box_collider_2d_enable)
			{
				return;
			}
			Vector3 touchPosition = TouchUtility.GetTouchPosition();
			delta_ = preve_ - (Vector2)touchPosition;
			if (is_cursor_drag_)
			{
				evidence_manager_.AddVerticalRotate(Mathf.Clamp(0f - delta_.y, 0f - touch_rotate_speed_, touch_rotate_speed_));
				evidence_manager_.AddHorizontalRotate(Mathf.Clamp(delta_.x, 0f - touch_rotate_speed_, touch_rotate_speed_));
			}
			preve_ = TouchUtility.GetTouchPosition();
			float f = Vector2.Distance(preve_pos_, touchPosition);
			if (Mathf.Abs(f) > drag_offset_ && !is_cursor_drag_)
			{
				is_cursor_drag_ = true;
			}
		}
		switch (touch)
		{
		case TouchInfo.Cancel:
			if (touch != TouchInfo.None)
			{
				break;
			}
			goto case TouchInfo.End;
		case TouchInfo.End:
			is_cursor_down_ = false;
			break;
		}
	}

	public IEnumerator Play()
	{
		return Play(mode_type, poly_obj_id);
	}

	public IEnumerator Resume(bool re_init = false)
	{
		InitMembers(investigate_type_.type_);
		return StateResumeCoroutine(re_init);
	}

	public IEnumerator Close(bool in_fast = false)
	{
		return StateCloseCoroutine(in_fast);
	}

	private void InitMembers(InvestigateType in_type)
	{
		investigate_type_ = ui_setting_data_.Find((StInvestigateType p) => p.type_ == in_type);
		check_point_index_ = -1;
		hit_point_index = -1;
		called_back_ = false;
		called_check_ = false;
		called_present_ = false;
		is_operate_key_ = false;
		is_ending_failed_ = false;
	}

	private IEnumerator StatePlayCoroutine()
	{
		yield return coroutineCtrl.instance.Play(StateInitCoroutine());
		if (advCtrl.instance.sub_window_.tutorial_ == 1)
		{
			Routine routine = advCtrl.instance.sub_window_.GetCurrentRoutine();
			while (true)
			{
				if (routine.r.no_1 == 0 && routine.r.no_2 == 3)
				{
					yield return null;
					continue;
				}
				if (routine.r.no_1 == 5 && (routine.r.no_2 == 0 || routine.r.no_2 == 1))
				{
					yield return null;
					continue;
				}
				break;
			}
			float time = 0f;
			float wait = 0.5f;
			while (true)
			{
				time += Time.deltaTime;
				if (time > wait)
				{
					break;
				}
				yield return null;
			}
		}
		messageBoardCtrl.instance.InActiveNormalMessageNextTouch();
		yield return coroutineCtrl.instance.Play(StateMainCoroutine());
	}

	private IEnumerator StateResumeCoroutine(bool re_init)
	{
		if (re_init)
		{
			yield return coroutineCtrl.instance.Play(fadeCtrl.instance.play_coroutine(20, false, Color.black));
			callback_manager_.OnInit();
			yield return coroutineCtrl.instance.Play(fadeCtrl.instance.play_coroutine(20, true, Color.black));
		}
		yield return coroutineCtrl.instance.Play(StateMainCoroutine());
	}

	private IEnumerator StateInitCoroutine()
	{
		rotate_touch_area_.SetEnableCollider(false);
		cursor_touch_area_.SetEnableCollider(false);
		yield return coroutineCtrl.instance.Play(fadeCtrl.instance.play_coroutine(30, false, Color.black));
		if (back_to_body_ != null)
		{
			back_to_body_.SetActive(false);
		}
		DyingMessageMiniGame.instance.body_active = false;
		if (lifeGaugeCtrl.instance.body_active)
		{
			standby_gauge_ = true;
			lifeGaugeCtrl.instance.ActionLifeGauge(lifeGaugeCtrl.Gauge_State.LIFE_OFF_MOMENT);
		}
		standby_keyguid_type_ = keyGuideBase.Type.NO_GUIDE;
		if (keyGuideCtrl.instance.active)
		{
			standby_keyguid_type_ = keyGuideCtrl.instance.current_guide;
			coroutineCtrl.instance.Play(keyGuideCtrl.instance.close());
		}
		BackupScenarioMessage();
		LoadBackground();
		background_renderer_.enabled = true;
		cursor_.transform.localPosition = new Vector3(0f, 0f, cursor_init_pos_z_);
		cursor_.load("/menu/common/", "inspect");
		cursor_.sprite_renderer_.enabled = false;
		try
		{
			evidence_manager_.LoadEvidenceModel(poly_data_);
			callback_manager_.OnInit();
		}
		catch (Exception message)
		{
			Debug.LogError(message);
			called_back_ = true;
		}
		yield return coroutineCtrl.instance.Play(fadeCtrl.instance.play_coroutine(30, true, Color.black));
		is_play_ready = true;
	}

	private void LoadBackground()
	{
		if (!(background_renderer_.sprite != null))
		{
			AssetBundle assetBundle = AssetBundleCtrl.instance.load("/GS1/minigame/", "frame05");
			background_renderer_.sprite = assetBundle.LoadAsset<Sprite>("frame05");
		}
	}

	private IEnumerator StateMainCoroutine()
	{
		yield return coroutineCtrl.instance.Play(keyGuideCtrl.instance.open(investigate_type_.guide_));
		cursor_.sprite_renderer_.enabled = true;
		rotate_touch_area_.SetEnableCollider(true);
		cursor_touch_area_.SetEnableCollider(true);
		guide_limit_ = 860f - keyGuideCtrl.instance.GetGuideWidth();
		messageBoardCtrl.instance.InActiveNormalMessageNextTouch();
		yield return coroutineCtrl.instance.Play(StateMainInputCoroutine());
		cursor_.sprite_renderer_.enabled = false;
		TouchSystem.TouchInActive();
		yield return coroutineCtrl.instance.Play(keyGuideCtrl.instance.close());
		yield return coroutineCtrl.instance.Play(StateMainCallbackCoroutine());
	}

	private IEnumerator StateMainInputCoroutine()
	{
		float wait_time = 20f;
		while (!UpdateMainInput())
		{
			UpdateSystemInput();
			ChangeGuideIconSprite();
			if (IsEnding())
			{
				wait_time = ((!IsAnyKey() && !padCtrl.instance.InputGetMouseButton(0)) ? (wait_time - Time.deltaTime) : 20f);
				if (wait_time <= 0f)
				{
					is_ending_failed_ = true;
					last_message_active_ = false;
					break;
				}
			}
			if (advCtrl.instance.sub_window_.tutorial_ == 1 && hit_point_index >= 0)
			{
				cursor_.sprite_renderer_.enabled = false;
				rotate_touch_area_.SetEnableCollider(false);
				yield return coroutineCtrl.instance.Play(keyGuideCtrl.instance.close());
				Routine routine = advCtrl.instance.sub_window_.GetCurrentRoutine();
				while (routine.r.no_1 == 5 && (routine.r.no_2 == 2 || routine.r.no_2 == 3 || routine.r.no_2 == 4))
				{
					yield return null;
				}
				float time = 0f;
				float wait = 0.5f;
				while (true)
				{
					time += Time.deltaTime;
					if (time > wait)
					{
						break;
					}
					yield return null;
				}
				yield return coroutineCtrl.instance.Play(keyGuideCtrl.instance.open(investigate_type_.guide_));
				cursor_.sprite_renderer_.enabled = true;
				rotate_touch_area_.SetEnableCollider(true);
				messageBoardCtrl.instance.InActiveNormalMessageNextTouch();
			}
			yield return 0;
		}
	}

	private IEnumerator StateMainCallbackCoroutine()
	{
		if (is_ending_failed_)
		{
			coroutineCtrl.instance.Play(Close());
		}
		else if (called_check_)
		{
			callback_manager_.OnCheck();
			if (callback_manager_.on_main != null)
			{
				yield return coroutineCtrl.instance.Play(callback_manager_.on_main);
			}
		}
		else if (called_back_)
		{
			if (advCtrl.instance.sub_window_.tutorial_ == 10)
			{
				advCtrl.instance.message_system_.AddScript2(GSStatic.global_work_.scenario, 0u);
				MessageSystem.GetActiveMessageWork().message_trans_flag = 1;
				GSStatic.global_work_.Mess_move_flag = 1;
				advCtrl.instance.message_system_.SetMessage(scenario.SC4_60850);
				messageBoardCtrl.instance.board(true, true);
				callback_manager_.runnning_mesasge = true;
				advCtrl.instance.sub_window_.req_ = SubWindow.Req.NONE;
				while (advCtrl.instance.sub_window_.req_ != SubWindow.Req.MESS_EXIT)
				{
					yield return null;
				}
				GSStatic.global_work_.Mess_move_flag = 0;
				advCtrl.instance.sub_window_.req_ = SubWindow.Req.NONE;
				callback_manager_.runnning_mesasge = false;
				messageBoardCtrl.instance.board(false, false);
			}
			coroutineCtrl.instance.Play(Close());
		}
		else if (called_present_)
		{
			last_message_active_ = false;
			fadeCtrl.instance.play(3u, 1u, 4u);
			Balloon.PlayTakeThat();
			while (objMoveCtrl.instance.is_play)
			{
				yield return null;
			}
			objMoveCtrl.instance.stop(2);
			if (GSStatic.global_work_.title == TitleId.GS1 && MessageSystem.GetActiveMessageWork().now_no == 183)
			{
				bgCtrl.instance.SetSprite(4095);
				AnimationSystem.Instance.StopCharacters();
			}
			coroutineCtrl.instance.Play(Close(called_present));
		}
	}

	private IEnumerator StateCloseCoroutine(bool in_fast)
	{
		int fade_time2 = ((!in_fast) ? 30 : 10);
		fade_time2 = ((!IsEnding()) ? fade_time2 : 90);
		if (advCtrl.instance.sub_window_.tutorial_ == 30)
		{
			fadeCtrl.instance.play(2u, (uint)fade_time2, 16u, 31u);
			while (!fadeCtrl.instance.is_end)
			{
				yield return null;
			}
		}
		else
		{
			yield return coroutineCtrl.instance.Play(fadeCtrl.instance.play_coroutine(fade_time2, false, Color.black));
		}
		Release();
		background_renderer_.enabled = false;
		DyingMessageMiniGame.instance.body_active = true;
		if (back_to_body_ != null)
		{
			back_to_body_.SetActive(true);
			back_to_body_ = null;
		}
		if (standby_gauge_)
		{
			standby_gauge_ = false;
			lifeGaugeCtrl.instance.ActionLifeGauge(lifeGaugeCtrl.Gauge_State.LIFE_ON_MOMENT);
			int gauge_mode = lifeGaugeCtrl.instance.gauge_mode;
			if (gauge_mode == 5)
			{
				lifeGaugeCtrl.instance.ActionLifeGauge(lifeGaugeCtrl.Gauge_State.NOTICE_DAMAGE);
			}
		}
		if (standby_keyguid_type_ != 0)
		{
			coroutineCtrl.instance.Play(keyGuideCtrl.instance.open(standby_keyguid_type_));
		}
		RecoveryScenarioMessage();
		if (!IsEnding())
		{
			yield return coroutineCtrl.instance.Play(fadeCtrl.instance.play_coroutine(fade_time2, true, Color.black));
		}
		active = false;
		is_play_ = false;
	}

	private void Release()
	{
		evidence_manager_.Release();
		callback_manager_.Release();
		cursor_.end();
	}

	public void SetNextScenario()
	{
		if (advCtrl.instance.sub_window_.tutorial_ == 2)
		{
			advCtrl.instance.message_system_.SetMessage(scenario.SC4_60264);
			MessageSystem.Mess_window_set(3u);
			advCtrl.instance.sub_window_.tutorial_ = 0;
		}
		else if (advCtrl.instance.sub_window_.tutorial_ == 10)
		{
			if (GSFlag.Check(0u, scenario.SCE40_FLAG_ST_G_KEITAI_MES))
			{
				advCtrl.instance.message_system_.SetMessage(scenario.SC4_60871);
				MessageSystem.Mess_window_set(3u);
				advCtrl.instance.sub_window_.tutorial_ = 0;
			}
			else
			{
				advCtrl.instance.sub_window_.tutorial_ = 0;
			}
		}
		else if (advCtrl.instance.sub_window_.tutorial_ == 20)
		{
			advCtrl.instance.message_system_.SetMessage(130u);
			MessageSystem.Mess_window_set(3u);
			advCtrl.instance.sub_window_.tutorial_ = 0;
		}
		else if (advCtrl.instance.sub_window_.status_force_ == 1 && poly_obj_id == 28 && advCtrl.instance.sub_window_.stack_ == 0)
		{
			advCtrl.instance.message_system_.SetMessage(scenario.SC4_69630);
			MessageSystem.Mess_window_set(3u);
			advCtrl.instance.sub_window_.status_force_ = 0;
		}
		else if (advCtrl.instance.sub_window_.tutorial_ == 30)
		{
			advCtrl.instance.sub_window_.tutorial_ = 0;
			if (is_ending_failed_)
			{
				advCtrl.instance.message_system_.SetMessage(scenario.SC4_70275);
			}
			else
			{
				advCtrl.instance.message_system_.SetMessage(scenario.SC4_70270);
			}
		}
	}

	private void BackupScenarioMessage()
	{
		MessageWork activeMessageWork = MessageSystem.GetActiveMessageWork();
		last_Mess_move_flag_ = GSStatic.global_work_.Mess_move_flag;
		if (!IsPresentable)
		{
			GSStatic.global_work_.Mess_move_flag = 0;
		}
		last_status_ = activeMessageWork.status;
		last_now_no_ = activeMessageWork.now_no;
		last_next_no_ = activeMessageWork.next_no;
		last_mdt_index_ = activeMessageWork.mdt_index;
		last_mdt_index_top_ = activeMessageWork.mdt_index_top;
		speaker_id = activeMessageWork.speaker_id;
		win_name_set = GSStatic.global_work_.win_name_set;
		if (activeMessageWork.op_work[7] == 65532)
		{
			last_op_para = activeMessageWork.op_para;
			last_opwork_7 = activeMessageWork.op_work[7];
			last_all_work = activeMessageWork.all_work[0];
		}
		last_message_line_ = new List<string>();
		last_message_line_x_ = new List<float>();
		for (int i = 0; i < messageBoardCtrl.instance.line_list.Count; i++)
		{
			last_message_line_.Add(messageBoardCtrl.instance.line_list[i].text);
			last_message_line_x_.Add(messageBoardCtrl.instance.line_list[i].transform.localPosition.x);
		}
		last_message_active_ = messageBoardCtrl.instance.body_active;
		last_message_color_ = activeMessageWork.message_text.color;
		last_selection_active_ = selectPlateCtrl.instance.body_active;
		last_item_plate_id_ = itemPlateCtrl.instance.id;
		last_item_plate_type_ = itemPlateCtrl.instance.open_type;
		last_face_plate_id_ = facePlateCtrl.instance.id;
		last_face_plate_type_ = facePlateCtrl.instance.open_type;
		messageBoardCtrl.instance.board(false, false);
		itemPlateCtrl.instance.closeItem(true);
		facePlateCtrl.instance.closeItem(true);
		selectPlateCtrl.instance.body_active = false;
		if (investigate_type_.type_ == InvestigateType.NORMAL || investigate_type_.type_ == InvestigateType.FORCE)
		{
			MessageSystem.SetActiveMessageWindow(WindowType.SUB);
		}
	}

	private void RecoveryScenarioMessage()
	{
		MessageSystem.SetActiveMessageWindow(WindowType.MAIN);
		advCtrl.instance.message_system_.AddScript2(GSStatic.global_work_.scenario, 0u);
		advCtrl.instance.message_system_.ScienceRecoverySetMessage(last_now_no_);
		MessageWork activeMessageWork = MessageSystem.GetActiveMessageWork();
		activeMessageWork.status = last_status_;
		activeMessageWork.mdt_index_top = last_mdt_index_top_;
		activeMessageWork.mdt_index = last_mdt_index_;
		activeMessageWork.now_no = last_now_no_;
		activeMessageWork.next_no = last_next_no_;
		activeMessageWork.speaker_id = speaker_id;
		GSStatic.global_work_.Mess_move_flag = last_Mess_move_flag_;
		GSStatic.global_work_.win_name_set = win_name_set;
		if (last_opwork_7 == 65532)
		{
			activeMessageWork.op_para = last_op_para;
			activeMessageWork.op_work[7] = last_opwork_7;
			activeMessageWork.all_work[0] = last_all_work;
		}
		for (int i = 0; i < messageBoardCtrl.instance.line_list.Count; i++)
		{
			messageBoardCtrl.instance.line_list[i].text = last_message_line_[i];
			messageBoardCtrl.instance.line_list[i].gameObject.SetActive(!string.IsNullOrEmpty(last_message_line_[i]));
			Vector3 localPosition = messageBoardCtrl.instance.line_list[i].transform.localPosition;
			localPosition.x = last_message_line_x_[i];
			messageBoardCtrl.instance.line_list[i].transform.localPosition = localPosition;
		}
		messageBoardCtrl.instance.name_plate(true, speaker_id, win_name_set);
		if (advCtrl.instance.sub_window_.status_force_ == 1 && poly_obj_id == 28 && advCtrl.instance.sub_window_.routine_[advCtrl.instance.sub_window_.stack_].r.no_3 == 0)
		{
			messageBoardCtrl.instance.board(false, false);
		}
		else
		{
			messageBoardCtrl.instance.board(last_message_active_, true);
		}
		activeMessageWork.message_text.SetColor(last_message_color_);
		selectPlateCtrl.instance.body_active = last_selection_active_;
		if (last_selection_active_)
		{
			activeMessageWork.status |= MessageSystem.Status.SELECT;
		}
		if (last_item_plate_id_ >= 0)
		{
			itemPlateCtrl.instance.entryItem(last_item_plate_id_);
			itemPlateCtrl.instance.openItem(last_item_plate_type_, 0f, true);
		}
		if (last_face_plate_id_ >= 0)
		{
			facePlateCtrl.instance.entryItem(last_face_plate_id_);
			facePlateCtrl.instance.openItem(last_face_plate_type_, 0f, true);
		}
	}

	private void ChangeGuideIconSprite()
	{
		hit_point_index = GetSelectingCheckIndex();
		if (hit_point_index != -1 && !IsPresentable)
		{
			uint[] array = callback_manager_.OnCheckCursor();
			uint add_sc = ((array[1] != 0) ? 65535u : GSStatic.global_work_.scenario);
			ushort nextInspectNumber = inspectCtrl.GetNextInspectNumber(array[0], add_sc);
			if (GSStatic.global_work_.inspect_readed_[(array[1] != 0) ? 1 : 0, nextInspectNumber] == 1)
			{
				cursor_.spriteNo(3);
			}
			else
			{
				cursor_.spriteNo(1);
			}
		}
		else
		{
			cursor_.spriteNo(0);
		}
	}

	private int GetSelectingCheckIndex()
	{
		if (check_test_index != -1)
		{
			return check_test_index;
		}
		Vector3 position = camera_.WorldToScreenPoint(cursor_.transform.position);
		position.x += ray_adjust_x_;
		Ray ray = camera_.ScreenPointToRay(position);
		int result = -1;
		RaycastHit hitInfo;
		if (Physics.SphereCast(ray, ray_radius_, out hitInfo, camera_.farClipPlane, 1 << evidence_manager_.gameObject.layer))
		{
			Transform transform = hitInfo.transform;
			Regex regex = new Regex("(?<nuki>nuki|nuke)", RegexOptions.IgnoreCase | RegexOptions.Singleline);
			Match match = regex.Match(transform.name);
			if (match.Success)
			{
				return -1;
			}
			Regex regex2 = new Regex("(?<number>\\d+?)", RegexOptions.IgnoreCase | RegexOptions.Singleline);
			Match match2 = regex2.Match(transform.name);
			result = (match2.Success ? (Utility.intParse(match2.Groups["number"].Value) - 1) : 0);
		}
		return result;
	}

	private bool UpdateMainInput()
	{
		padCtrl padCtrl2 = padCtrl.instance;
		hit_point_index = GetSelectingCheckIndex();
		if (!IsPresentable)
		{
			if (hit_point_index != -1 && padCtrl2.GetKeyDown(KeyType.A))
			{
				soundCtrl.instance.PlaySE(43);
				check_point_index_ = hit_point_index;
				called_check_ = true;
				is_cursor_drag_ = false;
				TouchSystem.instance.TouchReset();
				return true;
			}
		}
		else if (padCtrl2.GetKeyDown(KeyType.X))
		{
			GSStatic.message_work_.status &= ~MessageSystem.Status.LOOP;
			check_point_index_ = hit_point_index;
			called_present_ = true;
			TouchSystem.instance.TouchReset();
			return true;
		}
		if (IsBackable && padCtrl2.GetKeyDown(KeyType.B))
		{
			soundCtrl.instance.PlaySE(44);
			called_back_ = true;
			TouchSystem.instance.TouchReset();
			return true;
		}
		return called_check_ || called_back_ || called_present_;
	}

	private void UpdateSystemInput()
	{
		padCtrl padCtrl2 = padCtrl.instance;
		is_operate_key_ = false;
		if (IsZoomable)
		{
			float scale_ratio = evidence_manager_.scale_ratio;
			if (padCtrl2.GetKey(KeyType.X) || padCtrl.instance.GetWheelMoveUp())
			{
				is_operate_key_ = true;
				evidence_manager_.SetScaleRatio(Mathf.Min(scale_ratio + zoom_speed_, ScaleMax));
			}
			if (padCtrl2.GetKey(KeyType.Y) || padCtrl.instance.GetWheelMoveDown())
			{
				is_operate_key_ = true;
				evidence_manager_.SetScaleRatio(Mathf.Max(scale_ratio - zoom_speed_, ScaleMin));
			}
		}
		if (padCtrl.instance.axis_pos_R.x > 0f || padCtrl.instance.axis_pos_R.x < 0f || padCtrl.instance.axis_pos_R.y > 0f || padCtrl.instance.axis_pos_R.y < 0f)
		{
			if (padCtrl.instance.axis_pos_R.x > 0f || padCtrl.instance.axis_pos_R.x < 0f)
			{
				is_operate_key_ = true;
				evidence_manager_.AddHorizontalRotate((0f - padCtrl.instance.axis_pos_R.x) * rotate_speed_);
			}
			if (padCtrl.instance.axis_pos_R.y > 0f || padCtrl.instance.axis_pos_R.y < 0f)
			{
				is_operate_key_ = true;
				evidence_manager_.AddVerticalRotate(padCtrl.instance.axis_pos_R.y * rotate_speed_);
			}
		}
		else
		{
			if (padCtrl2.GetKey(KeyType.StickR_Up))
			{
				is_operate_key_ = true;
				evidence_manager_.AddVerticalRotate(rotate_speed_);
			}
			if (padCtrl2.GetKey(KeyType.StickR_Down))
			{
				is_operate_key_ = true;
				evidence_manager_.AddVerticalRotate(0f - rotate_speed_);
			}
			if (padCtrl2.GetKey(KeyType.StickR_Left))
			{
				is_operate_key_ = true;
				evidence_manager_.AddHorizontalRotate(rotate_speed_);
			}
			if (padCtrl2.GetKey(KeyType.StickR_Right))
			{
				is_operate_key_ = true;
				evidence_manager_.AddHorizontalRotate(0f - rotate_speed_);
			}
		}
		if (cursor_.sprite_renderer_.enabled)
		{
			Vector3 localPosition = cursor_.transform.localPosition;
			Vector3 vector = Vector3.zero;
			if (padCtrl.instance.axis_pos_L.x > 0f || padCtrl.instance.axis_pos_L.x < 0f)
			{
				is_operate_key_ = true;
				vector.x = padCtrl.instance.axis_pos_L.x * move_speed_;
			}
			if (padCtrl.instance.axis_pos_L.y > 0f || padCtrl.instance.axis_pos_L.y < 0f)
			{
				is_operate_key_ = true;
				vector.y = padCtrl.instance.axis_pos_L.y * move_speed_;
			}
			Vector3 zero = Vector3.zero;
			if (padCtrl2.GetKey(KeyType.Up))
			{
				zero.y = 1f;
			}
			if (padCtrl2.GetKey(KeyType.Down))
			{
				zero.y = -1f;
			}
			if (padCtrl2.GetKey(KeyType.Left))
			{
				zero.x = -1f;
			}
			if (padCtrl2.GetKey(KeyType.Right))
			{
				zero.x = 1f;
			}
			if (zero != Vector3.zero)
			{
				is_operate_key_ = true;
				zero.Normalize();
				vector = zero * move_speed_;
			}
			if (vector != Vector3.zero)
			{
				localPosition += vector;
			}
			localPosition.y = Mathf.Clamp(localPosition.y, movable_area_.rect.y, movable_area_.rect.height / 2f);
			if (localPosition.y < taboo_area_.y && localPosition.y > taboo_area_.y - taboo_area_.height && localPosition.x > guide_limit_ && localPosition.x < taboo_area_.x + taboo_area_.width)
			{
				localPosition.y = cursor_.transform.localPosition.y;
			}
			localPosition.x = Mathf.Clamp(localPosition.x, movable_area_.rect.x, movable_area_.rect.width / 2f);
			if (localPosition.y < taboo_area_.y && localPosition.y > taboo_area_.y - taboo_area_.height && localPosition.x > guide_limit_)
			{
				localPosition.x = guide_limit_;
			}
			localPosition.z = cursor_init_pos_z_;
			cursor_.transform.localPosition = localPosition;
		}
	}

	public void end()
	{
		Release();
	}

	[ContextMenu("Play Test")]
	private void PlayTest()
	{
		poly_obj_id = play_test_obj_id;
		mode_type = play_test_type;
		Play(play_test_type, play_test_obj_id);
	}

	[ContextMenu("Request Test")]
	private void RequestTest()
	{
		evidence_manager_.RequestTo(request_test_pos, request_test_ang, request_test_scale, request_test_seconds);
	}

	public void SetCollColorActive()
	{
		evidence_manager_.SetDebugColliderCollor();
	}
}
