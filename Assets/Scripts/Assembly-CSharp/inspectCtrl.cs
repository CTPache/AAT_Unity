using System.Collections;
using UnityEngine;

public class inspectCtrl : MonoBehaviour
{
	private static inspectCtrl instance_;

	private GameObject debug_area_root_;

	private LineRenderer[] debug_area_;

	[SerializeField]
	private GameObject body_;

	[SerializeField]
	private AssetBundleSprite cursor_;

	[SerializeField]
	private InputTouch touch_;

	[SerializeField]
	private InputTouch cursor_touch_;

	private IEnumerator enumerator_play_;

	private ushort cash_no_;

	private ushort cash_retun_no_;

	public float speed_x_;

	public float speed_rate_x_ = 0.1f;

	public float speed_y_;

	public float speed_rate_y_ = 0.1f;

	public float speed_max_ = 10f;

	public float pos_x_;

	public float pos_y_;

	public int disp_x_;

	public int disp_y_;

	public float offset_x_ = 300f;

	public float offset_y_ = 180f;

	private float move_speed_ = 10f;

	private Vector2 cursor_offset_ = Vector2.zero;

	private bool is_play_;

	private bool is_cancel_;

	private bool select_animation_playing_;

	private bool is_drag_;

	private Vector3 old_list_drag_position_ = default(Vector3);

	private const float CURSOL_POS_Z = -1f;

	private float screen_widht_half_;

	private float screen_height_half_;

	private Vector2 cursor_half_size_ = Vector2.zero;

	private const float KEY_GUIDE_HEIGHT = -400f;

	private const float DRAG_NECESSARY_POWER = 30f;

	[Header("Debug")]
	[SerializeField]
	private bool debug_show_area_;

	public static inspectCtrl instance
	{
		get
		{
			return instance_;
		}
	}

	private bool line_active
	{
		get
		{
			return debug_area_root_.activeSelf;
		}
		set
		{
			debug_area_root_.SetActive(value);
		}
	}

	public bool debug_show_area
	{
		get
		{
			return debug_show_area_;
		}
		set
		{
			debug_show_area_ = value;
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

	public bool is_play
	{
		get
		{
			return is_play_;
		}
	}

	public bool is_cancel
	{
		get
		{
			return is_cancel_;
		}
	}

	public bool select_animation_playing
	{
		get
		{
			return select_animation_playing_;
		}
	}

	public float pos_x
	{
		get
		{
			return pos_x_;
		}
	}

	public float pos_y
	{
		get
		{
			return pos_y_;
		}
	}

	private void Awake()
	{
		instance_ = this;
	}

	public void load()
	{
		active = false;
		cursor_.load("/menu/common/", "inspect");
	}

	public void init()
	{
		load();
		touch_.touch_event = TouchScreen;
		touch_.down_event = DownCursorOrScreen;
		touch_.drag_event = DragCursor;
		touch_.SetColliderSize(new Vector2(systemCtrl.instance.ScreenWidth, systemCtrl.instance.ScreenHeight));
		cursor_touch_.touch_key_type = KeyType.None;
		cursor_touch_.touch_event = TouchCursor;
		cursor_touch_.down_event = DownCursorOrScreen;
		cursor_touch_.drag_event = DragCursor;
		screen_widht_half_ = (float)systemCtrl.instance.ScreenWidth * 0.5f;
		screen_height_half_ = (float)systemCtrl.instance.ScreenHeight * 0.5f;
		cursor_offset_ = new Vector2(0f - screen_widht_half_, 0f - screen_height_half_);
		cursor_half_size_ = cursor_.sprite_renderer_.size / 2f;
	}

	public void end()
	{
		stop();
		is_play_ = false;
		is_cancel_ = false;
		select_animation_playing_ = false;
	}

	public IEnumerator CoroutinePlay()
	{
		active = true;
		is_play_ = true;
		is_cancel_ = false;
		select_animation_playing_ = false;
		cursor_.sprite_renderer_.enabled = false;
		while (bgCtrl.instance.is_slider)
		{
			yield return null;
		}
		cursor_.sprite_renderer_.enabled = true;
		touch_.ActiveCollider();
		cursor_touch_.ActiveCollider();
		speed_x_ = 0f;
		speed_y_ = 0f;
		cursor_.transform.localPosition = new Vector3(pos_x_, pos_y_, -1f);
		Set_InspectCursor();
		if (GSMain_TanteiPart.IsBGSlide(bgCtrl.instance.bg_no))
		{
			coroutineCtrl.instance.Play(keyGuideCtrl.instance.open(keyGuideBase.Type.INSPECT_SLIDE));
		}
		else
		{
			coroutineCtrl.instance.Play(keyGuideCtrl.instance.open(keyGuideBase.Type.INSPECT));
		}
		debug_line_start();
		float key_wait = systemCtrl.instance.key_wait;
		float limit_x = cursor_.sprite_renderer_.size.x / 2f;
		float limit_y = cursor_.sprite_renderer_.size.y / 2f;
		float guide_limit = (float)systemCtrl.instance.ScreenWidth - keyGuideCtrl.instance.GetGuideWidth() - keyGuideCtrl.instance.guide_space - 960f;
		while (true)
		{
			if (GSStatic.global_work_.r.no_0 != 17)
			{
				keyGuideCtrl.instance.UpdateActiveIconTouch();
				touch_.ActiveCollider();
				cursor_touch_.ActiveCollider();
				if (key_wait > 0f)
				{
					key_wait -= 1f;
				}
				else
				{
					if (padCtrl.instance.GetKeyDown(KeyType.A))
					{
						soundCtrl.instance.PlaySE(43);
						touch_.SetEnableCollider(false);
						cursor_touch_.SetEnableCollider(false);
						break;
					}
					if (padCtrl.instance.GetKeyDown(KeyType.B))
					{
						soundCtrl.instance.PlaySE(44);
						advCtrl.instance.sub_window_.busy_ = 3u;
						is_cancel_ = true;
						break;
					}
				}
				bool is_cursor = false;
				if (padCtrl.instance.axis_pos_L.x > 0f || padCtrl.instance.axis_pos_L.x < 0f)
				{
					is_cursor = true;
					speed_x_ = padCtrl.instance.axis_pos_L.x * move_speed_;
				}
				if (padCtrl.instance.axis_pos_L.y > 0f || padCtrl.instance.axis_pos_L.y < 0f)
				{
					is_cursor = true;
					speed_y_ = padCtrl.instance.axis_pos_L.y * move_speed_;
				}
				Vector2 direction = Vector2.zero;
				if (padCtrl.instance.GetKey(KeyType.Up))
				{
					direction.y = 1f;
				}
				if (padCtrl.instance.GetKey(KeyType.Down))
				{
					direction.y = -1f;
				}
				if (padCtrl.instance.GetKey(KeyType.Right))
				{
					direction.x = 1f;
				}
				if (padCtrl.instance.GetKey(KeyType.Left))
				{
					direction.x = -1f;
				}
				if (direction != Vector2.zero)
				{
					is_cursor = true;
					direction.Normalize();
					direction *= move_speed_;
					speed_x_ = direction.x;
					speed_y_ = direction.y;
				}
				if (padCtrl.instance.GetKeyDown(KeyType.L) && GSMain_TanteiPart.IsBGSlide(bgCtrl.instance.bg_no))
				{
					soundCtrl.instance.PlaySE(43);
					cursor_.spriteNo(0);
					yield return coroutineCtrl.instance.Play(bgCtrl.instance.Slider());
					is_cursor = true;
					if (debug_area_root_ != null)
					{
						Transform transform = debug_area_root_.transform;
						transform.SetParent(body_.transform, false);
						transform.localPosition = new Vector3(-960f - bgCtrl.instance.bg_pos_x, 540f, -10f);
					}
				}
				if (is_cursor)
				{
					pos_x_ += speed_x_;
					pos_y_ += speed_y_;
					pos_x_ = ((!(pos_x_ > screen_widht_half_ - limit_x)) ? pos_x_ : (screen_widht_half_ - limit_x));
					pos_x_ = ((!(pos_x_ < 0f - (screen_widht_half_ - limit_x))) ? pos_x_ : (0f - (screen_widht_half_ - limit_x)));
					pos_y_ = ((!(pos_y_ > screen_height_half_ - limit_y)) ? pos_y_ : (screen_height_half_ - limit_y));
					pos_y_ = ((!(pos_y_ < 0f - (screen_height_half_ - limit_y))) ? pos_y_ : (0f - (screen_height_half_ - limit_y)));
					if (pos_y_ < -400f && pos_x_ > guide_limit)
					{
						pos_y_ = cursor_.transform.localPosition.y;
					}
					if (pos_x_ > guide_limit && pos_y_ < -400f)
					{
						pos_x_ = guide_limit;
					}
					cursor_.transform.localPosition = new Vector3(pos_x_, pos_y_, -1f);
					Set_InspectCursor();
				}
				else
				{
					speed_x_ = 0f;
					speed_y_ = 0f;
				}
			}
			yield return null;
		}
		select_animation_playing_ = true;
		debug_line_end();
		yield return coroutineCtrl.instance.Play(keyGuideCtrl.instance.close());
		InspectCashReset();
		active = false;
		is_play_ = false;
		select_animation_playing_ = false;
	}

	public void play()
	{
		stop();
		enumerator_play_ = CoroutinePlay();
		coroutineCtrl.instance.Play(enumerator_play_);
	}

	public void stop()
	{
		if (enumerator_play_ != null)
		{
			coroutineCtrl.instance.Stop(enumerator_play_);
			enumerator_play_ = null;
		}
		active = false;
	}

	public void reset()
	{
		pos_x_ = 0f;
		pos_y_ = 0f;
	}

	public void debug_line_end()
	{
		if (debug_area_root_ != null)
		{
			debug_area_ = null;
			Object.Destroy(debug_area_root_);
			debug_area_root_ = null;
		}
	}

	public void debug_line_start()
	{
		if (!(debug_area_root_ == null))
		{
			return;
		}
		debug_area_root_ = new GameObject("DebugAreaRoot");
		debug_area_root_.layer = body_.layer;
		Transform transform = debug_area_root_.transform;
		transform.SetParent(body_.transform, false);
		transform.localPosition = new Vector3(-960f - bgCtrl.instance.bg_pos_x, 540f, -10f);
		debug_area_ = new LineRenderer[GSStatic.inspect_data_.Length];
		int num = 0;
		for (int i = 0; i < GSStatic.inspect_data_.Length; i++)
		{
			INSPECT_DATA iNSPECT_DATA = GSStatic.inspect_data_[i];
			if (iNSPECT_DATA.place == uint.MaxValue)
			{
				break;
			}
			Color green = Color.green;
			GameObject gameObject = new GameObject("Area" + num);
			gameObject.layer = debug_area_root_.layer;
			Transform transform2 = gameObject.transform;
			transform2.SetParent(transform, false);
			LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
			lineRenderer.useWorldSpace = false;
			lineRenderer.positionCount = 4;
			lineRenderer.SetPositions(new Vector3[4]
			{
				new Vector3(iNSPECT_DATA.x0, 0L - (long)iNSPECT_DATA.y0, 0f),
				new Vector3(iNSPECT_DATA.x1, 0L - (long)iNSPECT_DATA.y1, 0f),
				new Vector3(iNSPECT_DATA.x2, 0L - (long)iNSPECT_DATA.y2, 0f),
				new Vector3(iNSPECT_DATA.x3, 0L - (long)iNSPECT_DATA.y3, 0f)
			});
			lineRenderer.loop = true;
			lineRenderer.startColor = green;
			lineRenderer.endColor = green;
			lineRenderer.widthMultiplier = 4f;
			lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
			debug_area_[num] = lineRenderer;
			num++;
		}
		if (debug_show_area_)
		{
			line_active = true;
		}
		else
		{
			line_active = false;
		}
	}

	public void SetLineActive()
	{
		if (debug_area_root_ != null)
		{
			if (debug_show_area)
			{
				line_active = true;
			}
			else
			{
				line_active = false;
			}
		}
	}

	private void Set_InspectCursor()
	{
		int num = sw_inspect_main_CheckHitMessage();
		if (num == 1)
		{
			ushort nextInspectNumber = GetNextInspectNumber(SubWindow_Inspect.finger_pos_check(false));
			if (GSStatic.global_work_.inspect_readed_[0, nextInspectNumber] == 1)
			{
				num = 3;
			}
		}
		cursor_.spriteNo(num);
	}

	public static void InspectCashReset()
	{
		instance.cash_retun_no_ = (instance.cash_no_ = 0);
	}

	public static ushort GetNextInspectNumber(uint target_num, uint add_sc = 0u)
	{
		if (instance.cash_no_ != (ushort)target_num)
		{
			TrophyCtrl.disable_check_trophy_by_mes_no = true;
			MessageWork activeMessageWork = MessageSystem.GetActiveMessageWork();
			ushort now_no = activeMessageWork.now_no;
			uint mdt_index = activeMessageWork.mdt_index;
			uint mdt_index_top = activeMessageWork.mdt_index_top;
			instance.cash_no_ = (ushort)target_num;
			if (add_sc != 0)
			{
				advCtrl.instance.message_system_.AddScript2(add_sc, 0u);
			}
			advCtrl.instance.message_system_.SetMessage(target_num);
			instance.cash_retun_no_ = GetNextCode(activeMessageWork, activeMessageWork.now_no);
			advCtrl.instance.message_system_.ScienceRecoverySetMessage(now_no);
			activeMessageWork.mdt_index = mdt_index;
			activeMessageWork.mdt_index_top = mdt_index_top;
			TrophyCtrl.disable_check_trophy_by_mes_no = false;
		}
		return instance.cash_retun_no_;
	}

	private static ushort GetNextCode(MessageWork message_work, ushort set_no)
	{
		ushort result = set_no;
		while (true)
		{
			MessageSystem.ControlCode message = (MessageSystem.ControlCode)message_work.mdt_data.GetMessage(message_work.mdt_index);
			if ((int)message < 128)
			{
				switch (message)
				{
				case MessageSystem.ControlCode.CODE_35:
					MessageSystem.CodeProc_35(message_work);
					continue;
				case MessageSystem.ControlCode.CODE_36:
				case MessageSystem.ControlCode.CODE_78:
					MessageSystem.CodeProc_36(message_work);
					continue;
				case MessageSystem.ControlCode.CODE_2C:
					MessageSystem.CodeProc_2c(message_work);
					continue;
				case MessageSystem.ControlCode.CODE_08:
				case MessageSystem.ControlCode.CODE_09:
				case MessageSystem.ControlCode.CODE_0D:
					return result;
				}
				ushort num = MessageSystem.code_proc_arg_count_table[(uint)message];
				if (num > 0)
				{
					message_work.mdt_index += num;
				}
				message_work.mdt_index++;
			}
			else
			{
				result = message_work.now_no;
				message_work.mdt_index++;
			}
		}
	}

	private static byte sw_inspect_main_CheckHitMessage()
	{
		byte result = 0;
		short mes_id = (short)SubWindow_Inspect.finger_pos_check(true);
		switch (GSStatic.global_work_.title)
		{
		case TitleId.GS1:
			result = GS1_sw_inspect_main_CheckHitMessage(mes_id);
			break;
		case TitleId.GS2:
			result = GS2_sw_inspect_main_CheckHitMessage(mes_id);
			break;
		case TitleId.GS3:
			result = GS3_sw_inspect_main_CheckHitMessage(mes_id);
			break;
		}
		return result;
	}

	private static byte GS1_sw_inspect_main_CheckHitMessage(short mes_id)
	{
		byte b = GSStatic.global_work_.scenario;
		if (mes_id != scenario.SYS_M0320 && (b != 1 || mes_id != scenario.SC1_00285) && (b != 18 || mes_id != scenario.SC4_63600))
		{
			return 1;
		}
		return 0;
	}

	private static byte GS2_sw_inspect_main_CheckHitMessage(short mes_id)
	{
		if (mes_id != SubWindow_Inspect.GS2_finger_pos_check_02())
		{
			return 1;
		}
		return 0;
	}

	private static byte GS3_sw_inspect_main_CheckHitMessage(short mes_id)
	{
		if (mes_id != scenario_GS3.SYS_M0320 && mes_id != scenario_GS3.SYS_M0330)
		{
			return 1;
		}
		return 0;
	}

	public void SetCursorPos(float x, float y)
	{
		cursor_.transform.position = new Vector3(x, y, -1f);
		pos_x_ = x;
		pos_y_ = y;
	}

	private void TouchScreen(TouchParameter touch)
	{
		if (!IsTouchBan())
		{
			UpdateTouchCursorPosition(touch.pos);
			Set_InspectCursor();
		}
	}

	private void TouchCursor(TouchParameter touch)
	{
		if (!IsTouchBan())
		{
			UpdateTouchCursorPosition(touch.pos);
			Set_InspectCursor();
			cursor_touch_.touch_key_type = ((!is_drag_) ? KeyType.A : KeyType.None);
		}
	}

	private void DownCursorOrScreen(Vector3 touch_pos)
	{
		is_drag_ = false;
		old_list_drag_position_ = TouchUtility.GetTouchPosition();
		Vector3 vector = systemCtrl.instance.ScreenToViewportPoint(TouchUtility.GetTouchPosition());
		Vector2 vector2 = new Vector2((float)systemCtrl.instance.ScreenWidth * vector.x, (float)systemCtrl.instance.ScreenHeight * vector.y) + cursor_offset_;
		vector2.x = Mathf.Clamp(vector2.x, 0f - screen_widht_half_ + cursor_half_size_.x, screen_widht_half_ - cursor_half_size_.x);
		vector2.y = Mathf.Clamp(vector2.y, 0f - screen_height_half_ + cursor_half_size_.y, screen_height_half_ - cursor_half_size_.y);
		pos_x_ = vector2.x;
		pos_y_ = vector2.y;
		float num = (float)systemCtrl.instance.ScreenWidth - keyGuideCtrl.instance.GetGuideWidth() - keyGuideCtrl.instance.guide_space - screen_widht_half_;
		if (pos_y_ < -400f && pos_x_ > num)
		{
			pos_y_ = -400f;
		}
		if (pos_x_ > num && pos_y_ < -400f)
		{
			pos_x_ = num;
		}
		cursor_.transform.localPosition = new Vector3(pos_x_, pos_y_, -1f);
		Set_InspectCursor();
	}

	private void DragCursor(Vector3 pos)
	{
		if (!IsTouchBan())
		{
			UpdateTouchCursorPosition(pos);
			Set_InspectCursor();
			Vector3 touchPosition = TouchUtility.GetTouchPosition();
			float num = Vector3.Distance(old_list_drag_position_, touchPosition);
			if (num > 30f && !is_drag_)
			{
				is_drag_ = true;
			}
		}
	}

	private void UpdateTouchCursorPosition(Vector3 wold_pos)
	{
		float num = (float)systemCtrl.instance.ScreenWidth - keyGuideCtrl.instance.GetGuideWidth() - keyGuideCtrl.instance.guide_space - screen_widht_half_;
		Vector3 vector = systemCtrl.instance.ScreenToViewportPoint(TouchUtility.GetTouchPosition());
		Vector2 vector2 = new Vector2((float)systemCtrl.instance.ScreenWidth * vector.x, (float)systemCtrl.instance.ScreenHeight * vector.y) + cursor_offset_;
		vector2.x = Mathf.Clamp(vector2.x, 0f - screen_widht_half_ + cursor_half_size_.x, screen_widht_half_ - cursor_half_size_.x);
		vector2.y = Mathf.Clamp(vector2.y, 0f - screen_height_half_ + cursor_half_size_.y, screen_height_half_ - cursor_half_size_.y);
		pos_x_ = vector2.x;
		pos_y_ = vector2.y;
		if (pos_y_ < -400f && pos_x_ > num)
		{
			pos_y_ = cursor_.transform.localPosition.y;
		}
		if (pos_x_ > num && pos_y_ < -400f)
		{
			pos_x_ = num;
		}
		cursor_.transform.localPosition = new Vector3(pos_x_, pos_y_, -1f);
	}

	private bool IsTouchBan()
	{
		return bgCtrl.instance.is_slider || select_animation_playing_ || !is_play_;
	}
}
