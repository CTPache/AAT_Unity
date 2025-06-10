using System.Collections;
using System.Linq;
using UnityEngine;

public class PointMiniGame : MonoBehaviour
{
	private enum touch_state
	{
		TOUCH_NONE = 0,
		TOUCH_DOWN = 1,
		TOUCH_DRAG = 2,
		TOUCH_LOCK = 3
	}

	private bool is_drag_;

	private const float DRAG_NECESSARY_POWER = 40f;

	[SerializeField]
public float appear_time_;

	[SerializeField]
public InputTouch touch_;

	[SerializeField]
public InputTouch cursor_touch_;

	[SerializeField]
public GameObject touch_area_;

	[Header("Debug")]
	[SerializeField]
public bool debug_show_area_;

	private EXPL_CK_DATA expl_ck_data_;

	private GSPoint4[] converted_point_;

	private bool is_running_;

	private bool is_canceled_;

	private bool is_cursor_lock_;

	private int touch_state_;

	private Vector3 base_finger_position_;

	private Vector3 old_finger_position_;

	public static PointMiniGame instance { get; private set; }

	public bool is_running
	{
		get
		{
			return is_running_;
		}
	}

	public bool is_canceled
	{
		get
		{
			return is_canceled_;
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

	public InputTouch cursor_touch
	{
		get
		{
			return cursor_touch_;
		}
		set
		{
			cursor_touch_ = value;
		}
	}

	private void Awake()
	{
		instance = this;
	}

	public void Init(int siteki_no)
	{
		expl_ck_data_ = GSScenario.GetExplChData(siteki_no);
		if (GSStatic.global_work_.title == TitleId.GS1)
		{
			converted_point_ = MiniGameGSPoint4Hit.ConvertPoint(expl_ck_data_.point, new Vector2(290f, -100f), new Vector2(5f, 5f)).ToArray();
		}
		else if (GSStatic.global_work_.title == TitleId.GS3 && GSStatic.global_work_.scenario == 10)
		{
			converted_point_ = MiniGameGSPoint4Hit.ConvertPoint(expl_ck_data_.point, new Vector2(200f, 0f), new Vector2(5.625f, 5.625f)).ToArray();
		}
		else if (GSStatic.global_work_.title == TitleId.GS3 && siteki_no == 10)
		{
			converted_point_ = MiniGameGSPoint4Hit.ConvertPoint(expl_ck_data_.point, new Vector2(0f, 0f), new Vector2(5.625f, 5.625f)).ToArray();
		}
		else
		{
			converted_point_ = MiniGameGSPoint4Hit.ConvertPoint(expl_ck_data_.point, new Vector2(240f, 0f), new Vector2(5.625f, 5.625f)).ToArray();
		}
		InitTouch();
		coroutineCtrl.instance.Play(MainCoroutine());
	}

	private void InitTouch()
	{
		touch_.SetColliderSize(new Vector2(1920f, 1080f));
		cursor_touch_.SetColliderSize(new Vector2(35f, 35f));
		BoxCollider2D component = cursor_touch_.GetComponent<BoxCollider2D>();
		component.enabled = base.enabled;
		component.offset = new Vector2(10f, -10f);
		is_cursor_lock_ = false;
		touch_state_ = 0;
		touch_area_.SetActive(true);
		cursor_touch_.touch_key_type = KeyType.None;
		cursor_touch_.touch_event = delegate
		{
			cursor_touch_.touch_key_type = ((!is_drag_) ? KeyType.X : KeyType.None);
		};
		cursor_touch_.down_event = delegate
		{
			is_cursor_lock_ = true;
			is_drag_ = false;
			old_finger_position_ = TouchUtility.GetTouchPosition();
			touch_state_ = 3;
		};
		cursor_touch_.up_event = delegate
		{
			is_cursor_lock_ = false;
			touch_state_ = 0;
		};
		cursor_touch_.drag_event = delegate(Vector3 p)
		{
			is_cursor_lock_ = false;
			touch_state_ = 2;
			DragCursor(p);
		};
		touch_.down_event = delegate
		{
			is_drag_ = false;
			old_finger_position_ = TouchUtility.GetTouchPosition();
			if (!is_cursor_lock_)
			{
				touch_state_ = 1;
			}
		};
		touch_.drag_event = delegate(Vector3 p)
		{
			if (!is_cursor_lock_)
			{
				touch_state_ = 2;
			}
			DragCursor(p);
		};
		touch_.up_event = delegate
		{
			if (!is_cursor_lock_)
			{
				touch_state_ = 0;
			}
		};
	}

	public uint GetResultMessage()
	{
		bool flag = false;
		int num = 0;
		uint result;
		switch (MiniGameGSPoint4Hit.CheckHit(GetCursorRect(), converted_point_))
		{
		case 0:
			result = expl_ck_data_.trueMes;
			break;
		case 1:
			result = expl_ck_data_.falseMes1;
			break;
		default:
			result = expl_ck_data_.falseMes2;
			flag = true;
			break;
		}
		if (GSStatic.message_work_.op_para == 78 && GSStatic.message_work_.op_no == 0 && flag)
		{
			expl_ck_data_ = GSScenario.GetExplChData(GSStatic.message_work_.all_work[0]);
			converted_point_ = MiniGameGSPoint4Hit.ConvertPoint(expl_ck_data_.point, new Vector2(0f, 0f), new Vector2(5.625f, 5.625f)).ToArray();
			switch (MiniGameGSPoint4Hit.CheckHit(GetCursorRect(), converted_point_))
			{
			case 0:
				result = expl_ck_data_.trueMes;
				break;
			case 1:
				result = expl_ck_data_.falseMes1;
				break;
			default:
				result = expl_ck_data_.falseMes2;
				break;
			}
		}
		return result;
	}

	private IEnumerator MainCoroutine()
	{
		is_running_ = true;
		is_canceled_ = false;
		advCtrl.instance.sub_window_.busy_ = 3u;
		messageBoardCtrl.instance.board(false, false);
		yield return null;
		AssetBundleCtrl asset_bundle_ctrl = AssetBundleCtrl.instance;
		AssetBundle asset_bundle = asset_bundle_ctrl.load("/menu/common/", "s2d051");
		Sprite[] icon_sprites2 = asset_bundle.LoadAssetWithSubAssets<Sprite>("s2d051");
		MiniGameCursor cursor = MiniGameCursor.instance;
		cursor.icon_offset = new Vector3(48f, -48f, 0f);
		cursor.icon_sprite = icon_sprites2[0];
		cursor.icon_visible = true;
		Vector2 cursor_area_size = cursor.cursor_area_size;
		cursor.cursor_position = new Vector3(0f, cursor_area_size.y * 0.5f, 0f);
		yield return null;
		Vector3 src_position = cursor.cursor_position;
		Vector3 dest_position = new Vector3(cursor_area_size.x * 0.5f, cursor_area_size.y * 0.5f, 0f);
		for (float time2 = 0f; time2 < appear_time_; time2 += Time.deltaTime)
		{
			cursor.cursor_position = Vector3.Lerp(src_position, dest_position, time2 / appear_time_);
			yield return null;
		}
		cursor.cursor_position = dest_position;
		advCtrl.instance.sub_window_.busy_ = 0u;
		GameObject debug_root_offset2 = new GameObject("DebugRootOffset");
		debug_root_offset2.layer = base.gameObject.layer;
		debug_root_offset2.transform.SetParent(base.transform, false);
		debug_root_offset2.transform.localPosition = new Vector3(0f - bgCtrl.instance.bg_pos_x, 0f, 0f);
		DebugMiniGameGSPoint4Hit debug_hit2 = new DebugMiniGameGSPoint4Hit();
		debug_hit2.SetParent(debug_root_offset2.transform);
		if (GSStatic.global_work_.r.no_0 == 5)
		{
			coroutineCtrl.instance.Play(keyGuideCtrl.instance.open(keyGuideBase.Type.POINT_TANTEI));
		}
		else
		{
			coroutineCtrl.instance.Play(keyGuideCtrl.instance.open((!GSMain_TanteiPart.IsBGSlide(bgCtrl.instance.bg_no)) ? keyGuideBase.Type.POINT : keyGuideBase.Type.POINT_SLIDE));
		}
		padCtrl pad = padCtrl.instance;
		bool decide2 = false;
		Vector3 prev_position2 = cursor.cursor_position;
		while (true)
		{
			debug_hit2.DebugShowArea(debug_show_area_, GetCursorRect(), converted_point_);
			if (padCtrl.instance.GetKeyDown(KeyType.X))
			{
				if (!padCtrl.instance.GetKeyDown(KeyType.X) && touch_state_ != 2 && !is_drag_)
				{
					touch_state_ = 0;
					decide2 = true;
				}
				break;
			}
			if (GSStatic.global_work_.r.no_0 == 5 && padCtrl.instance.GetKeyDown(KeyType.B))
			{
				soundCtrl.instance.PlaySE(44);
				is_canceled_ = true;
				break;
			}
			if (padCtrl.instance.GetKeyDown(KeyType.L) && GSMain_TanteiPart.IsBGSlide(bgCtrl.instance.bg_no))
			{
				coroutineCtrl.instance.Play(bgCtrl.instance.Slider());
				while (bgCtrl.instance.is_slider)
				{
					yield return null;
					debug_root_offset2.transform.localPosition = new Vector3(0f - bgCtrl.instance.bg_pos_x, 0f, 0f);
				}
			}
			if (!decide2 && !is_cursor_lock_)
			{
				switch (touch_state_)
				{
				case 0:
					base_finger_position_ = Vector3.zero;
					break;
				case 1:
					cursor.cursor_position = base_finger_position_;
					break;
				}
				prev_position2 = cursor.cursor_position;
				cursor.Process();
			}
			yield return null;
		}
		coroutineCtrl.instance.Play(keyGuideCtrl.instance.open(keyGuideBase.Type.NO_GUIDE));
		debug_hit2.DebugShowArea(false, GetCursorRect(), converted_point_);
		debug_hit2.SetParent(null);
		debug_hit2 = null;
		Object.Destroy(debug_root_offset2);
		debug_root_offset2 = null;
		if (!is_canceled_)
		{
			cursor.icon_sprite = icon_sprites2[1];
			Balloon.PlayTakeThat();
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
		}
		cursor.icon_visible = false;
		cursor.icon_sprite = null;
		icon_sprites2 = null;
		if (is_canceled_)
		{
			fadeCtrl.instance.play(2u, 1u, 1u, 31u);
			while (fadeCtrl.instance.status != 0)
			{
				yield return null;
			}
			GSPsylock.PsylockDisp_redisp();
			GSPsylock.PsylockDisp_move();
			if (GSStatic.global_work_.title == TitleId.GS2)
			{
				AnimationSystem.Instance.PlayCharacter((int)GSStatic.global_work_.title, 20, 238, 238);
			}
			else if (GSStatic.global_work_.title == TitleId.GS3)
			{
				if (GSStatic.global_work_.scenario == 15)
				{
					AnimationSystem.Instance.PlayCharacter((int)GSStatic.global_work_.title, 29, 414, 414);
				}
				else
				{
					AnimationSystem.Instance.PlayCharacter((int)GSStatic.global_work_.title, 30, 431, 431);
				}
			}
			GSStatic.tantei_work_.person_flag = 1;
			AnimationSystem.Instance.CharacterAnimationObject.BeFlag &= -134217729;
			AnimationSystem.Instance.CharacterAnimationObject.BeFlag |= 536870912;
			AnimationSystem.Instance.CharacterAnimationObject.gameObject.SetActive(true);
			fadeCtrl.instance.play(1u, 1u, 1u, 31u);
			while (fadeCtrl.instance.status != 0)
			{
				yield return null;
			}
			GSStatic.global_work_.r.no_2 = 8;
			GSStatic.global_work_.r.no_3 = 0;
			advCtrl.instance.sub_window_.tantei_tukituke_ = 0;
			GSStatic.global_work_.Mess_move_flag = 1;
			GSStatic.message_work_.mess_win_rno = 1;
		}
		MessageSystem.GetActiveMessageWork().status &= ~MessageSystem.Status.POINT_TO;
		ResetCursorKeyType();
		is_running_ = false;
		touch_area_.SetActive(false);
		yield return null;
	}

	private GSRect GetCursorRect()
	{
		Vector3 cursor_position = MiniGameCursor.instance.cursor_position;
		return new GSRect((short)(cursor_position.x - 8f + bgCtrl.instance.bg_pos_x), (short)(cursor_position.y - 8f), 16, 16);
	}

	private void ResetCursorKeyType()
	{
		cursor_touch_.touch_event = delegate
		{
			cursor_touch_.touch_key_type = KeyType.None;
		};
	}

	private void DragCursor(Vector3 pos)
	{
		Vector3 touchPosition = TouchUtility.GetTouchPosition();
		float num = Vector3.Distance(old_finger_position_, touchPosition);
		if (num > 40f && !is_drag_)
		{
			is_drag_ = true;
		}
	}
}
