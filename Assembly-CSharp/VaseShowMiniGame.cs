using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VaseShowMiniGame : MonoBehaviour
{
	private static VaseShowMiniGame instance_;

	[SerializeField]
	private GameObject body_;

	[SerializeField]
	private GameObject vase_camera_;

	[SerializeField]
	private GameObject vase_;

	[SerializeField]
	private evidenceObjectManager evidence_manager_;

	[SerializeField]
	private AssetBundleSprite taiho_kun_;

	[SerializeField]
	private AssetBundleSprite bg_sprite_;

	[SerializeField]
	private InputTouch touch_;

	[SerializeField]
	private GameObject debug_;

	[SerializeField]
	private List<Text> debug_text_;

	private Vector3 vase_rotate_ = new Vector3(0f, 0f, 0f);

	private Vector3 answer_rotate_1_ = new Vector3(60f, 0f, 180f);

	private Vector3 answer_rotate_2_ = new Vector3(120f, 180f, 0f);

	private Vector3 safe_range_ = new Vector3(5f, 3f, 3f);

	private bool is_Correct_;

	private float rotate_speed_ = 1f;

	private polyData poly_data_;

	private Vector3 preve_pos_ = Vector3.zero;

	private Vector2 delta_ = Vector2.zero;

	private Vector2 preve_ = Vector2.zero;

	private float drag_offset_ = 30f;

	private float touch_rotate_speed_ = 5f;

	private bool is_drag_;

	private bool is_touch_;

	public static VaseShowMiniGame instance
	{
		get
		{
			return instance_;
		}
	}

	public GameObject debug
	{
		get
		{
			return debug_;
		}
	}

	private void Awake()
	{
		if (instance == null)
		{
			instance_ = this;
		}
	}

	public void Init()
	{
		bg_sprite_.load("/GS1/minigame/", "frame05");
		taiho_kun_.load("/GS1/minigame/", "taiho");
		bg_sprite_.sprite_renderer_.color = new Color(0.2f, 0.9f, 0.2f);
		poly_data_ = polyDataCtrl.instance.GetPolyData(17);
		evidence_manager_.LoadEvidenceModel(poly_data_);
		touch_.SetColliderSize(new Vector2(1920f, 1080f));
		touch_.touch_key_type = KeyType.None;
		InitTouch();
		touch_.ActiveCollider();
		vase_rotate_ = Vector3.zero;
		vase_.transform.localRotation = Quaternion.Euler(vase_rotate_);
		coroutineCtrl.instance.Play(VaseShowMain());
	}

	public void end()
	{
		evidence_manager_.Release();
	}

	private void InitTouch()
	{
		touch_.down_event = delegate
		{
			delta_ = Vector2.zero;
			preve_ = TouchUtility.GetTouchPosition();
			preve_pos_ = preve_;
			is_drag_ = false;
			is_touch_ = true;
		};
		touch_.drag_event = delegate
		{
			if (touch_.box_collider_2d_enable)
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
		touch_.up_event = delegate
		{
			is_touch_ = false;
		};
	}

	private IEnumerator VaseShowMain()
	{
		float time2 = 0f;
		float wait2 = 1.2f;
		while (true)
		{
			time2 += Time.deltaTime;
			if (time2 > wait2)
			{
				break;
			}
			yield return null;
		}
		messageBoardCtrl.instance.board(false, false);
		fadeCtrl.instance.play(1u, 1u, 1u);
		body_.SetActive(true);
		ConfrontWithMovie.instance.controller.SetAutoPlayStatus(2);
		ConfrontWithMovie.instance.controller.ResetScreenTexture();
		coroutineCtrl.instance.Play(keyGuideCtrl.instance.open(keyGuideBase.Type.VASE_SHOW));
		float time3 = 0f;
		float wait3 = 0.4f;
		while (true)
		{
			time3 += Time.deltaTime;
			if (time3 > wait3)
			{
				break;
			}
			yield return null;
		}
		vase_camera_.SetActive(true);
		while (true)
		{
			if (!is_touch_)
			{
				if (padCtrl.instance.GetKeyDown(KeyType.X))
				{
					break;
				}
				InputRotateKey();
			}
			vase_rotate_ = vase_.transform.localEulerAngles;
			if (vase_rotate_.x < -179.9f)
			{
				vase_rotate_.x += 360f;
			}
			if (vase_rotate_.y < -179.9f)
			{
				vase_rotate_.y += 360f;
			}
			if (vase_rotate_.z < -179.9f)
			{
				vase_rotate_.z += 360f;
			}
			if (vase_rotate_.x > 180f)
			{
				vase_rotate_.x -= 360f;
			}
			if (vase_rotate_.y > 180f)
			{
				vase_rotate_.y -= 360f;
			}
			if (vase_rotate_.z > 180f)
			{
				vase_rotate_.z -= 360f;
			}
			yield return false;
		}
		fadeCtrl.instance.play(3u, 1u, 4u);
		vase_camera_.SetActive(false);
		Balloon.PlayTakeThat();
		float time = 0f;
		float wait = 1.1f;
		while (true)
		{
			time += Time.deltaTime;
			if (time > wait)
			{
				break;
			}
			yield return null;
		}
		objMoveCtrl.instance.stop(2);
		AnswerView();
	}

	private void InputRotateKey()
	{
		if (padCtrl.instance.axis_pos_R.x > 0f || padCtrl.instance.axis_pos_R.x < 0f || padCtrl.instance.axis_pos_R.y > 0f || padCtrl.instance.axis_pos_R.y < 0f)
		{
			if (padCtrl.instance.axis_pos_R.x > 0f || padCtrl.instance.axis_pos_R.x < 0f)
			{
				evidence_manager_.AddHorizontalRotate((0f - padCtrl.instance.axis_pos_R.x) * rotate_speed_);
			}
			if (padCtrl.instance.axis_pos_R.y > 0f || padCtrl.instance.axis_pos_R.y < 0f)
			{
				evidence_manager_.AddVerticalRotate(padCtrl.instance.axis_pos_R.y * rotate_speed_);
			}
		}
		else
		{
			if (padCtrl.instance.GetKey(KeyType.StickR_Up))
			{
				evidence_manager_.AddVerticalRotate(rotate_speed_);
			}
			if (padCtrl.instance.GetKey(KeyType.StickR_Down))
			{
				evidence_manager_.AddVerticalRotate(0f - rotate_speed_);
			}
			if (padCtrl.instance.GetKey(KeyType.StickR_Left))
			{
				evidence_manager_.AddHorizontalRotate(rotate_speed_);
			}
			if (padCtrl.instance.GetKey(KeyType.StickR_Right))
			{
				evidence_manager_.AddHorizontalRotate(0f - rotate_speed_);
			}
		}
		if (padCtrl.instance.GetKey(KeyType.L) || padCtrl.instance.GetKeyDown(KeyType.L))
		{
			evidence_manager_.AddRoll_Z_Rotate(rotate_speed_);
		}
		if (padCtrl.instance.GetKey(KeyType.Record, 2, true, KeyType.R) || padCtrl.instance.GetKeyDown(KeyType.Record, 2, true, KeyType.R))
		{
			evidence_manager_.AddRoll_Z_Rotate(0f - rotate_speed_);
		}
	}

	private void AnswerView()
	{
		if (answer_rotate_1_.x - safe_range_.x <= vase_rotate_.x && vase_rotate_.x <= answer_rotate_1_.x + safe_range_.x && answer_rotate_1_.y - safe_range_.y <= vase_rotate_.y && vase_rotate_.y <= answer_rotate_1_.y + safe_range_.y && (answer_rotate_1_.z - safe_range_.z <= vase_rotate_.z || vase_rotate_.z <= 0f - answer_rotate_1_.z + safe_range_.z))
		{
			is_Correct_ = true;
		}
		if (answer_rotate_2_.x - safe_range_.x <= vase_rotate_.x && vase_rotate_.x <= answer_rotate_2_.x + safe_range_.x && (answer_rotate_2_.y - safe_range_.y <= vase_rotate_.y || vase_rotate_.y <= 0f - answer_rotate_2_.y + safe_range_.y) && answer_rotate_2_.z - safe_range_.z <= vase_rotate_.z && vase_rotate_.z <= answer_rotate_2_.z + safe_range_.z)
		{
			is_Correct_ = true;
		}
		coroutineCtrl.instance.Play(keyGuideCtrl.instance.open(keyGuideBase.Type.NO_GUIDE));
		if (is_Correct_)
		{
			advCtrl.instance.message_system_.SetMessage(scenario.SC4_68560);
		}
		else
		{
			advCtrl.instance.message_system_.SetMessage(scenario.SC4_68550);
		}
		bg_sprite_.end();
		bg_sprite_.remove();
		taiho_kun_.end();
		taiho_kun_.remove();
		evidence_manager_.Release();
		body_.SetActive(false);
	}

	private void SetLayerChildrens(GameObject _obj, string _layer)
	{
		Transform[] componentsInChildren = _obj.GetComponentsInChildren<Transform>();
		Transform[] array = componentsInChildren;
		foreach (Transform transform in array)
		{
			transform.gameObject.layer = LayerMask.NameToLayer(_layer);
		}
	}

	private void DebugShow()
	{
		debug_text_[0].text = "Rot_X : " + (int)vase_rotate_.x;
		debug_text_[1].text = "Rot_Y : " + (int)vase_rotate_.y;
		debug_text_[2].text = "Rot_Z : " + (int)vase_rotate_.z;
		if (answer_rotate_1_.x - safe_range_.x <= vase_rotate_.x && vase_rotate_.x <= answer_rotate_1_.x + safe_range_.x)
		{
			debug_text_[0].color = new Color(0.1f, 0.8f, 0.1f);
		}
		else if (answer_rotate_2_.x - safe_range_.x <= vase_rotate_.x && vase_rotate_.x <= answer_rotate_2_.x + safe_range_.x)
		{
			debug_text_[0].color = new Color(0.4f, 0.8f, 1f);
		}
		else
		{
			debug_text_[0].color = new Color(0.9f, 0.9f, 0.9f);
		}
		if (answer_rotate_1_.y - safe_range_.y <= vase_rotate_.y && vase_rotate_.y <= answer_rotate_1_.y + safe_range_.y)
		{
			debug_text_[1].color = new Color(0.1f, 0.8f, 0.1f);
		}
		else if (answer_rotate_2_.y - safe_range_.y <= vase_rotate_.y || vase_rotate_.y <= 0f - answer_rotate_2_.y + safe_range_.y)
		{
			debug_text_[1].color = new Color(0.4f, 0.8f, 1f);
		}
		else
		{
			debug_text_[1].color = new Color(0.9f, 0.9f, 0.9f);
		}
		if (answer_rotate_1_.z - safe_range_.z <= vase_rotate_.z || vase_rotate_.z <= 0f - answer_rotate_1_.z + safe_range_.z)
		{
			debug_text_[2].color = new Color(0.1f, 0.8f, 0.1f);
		}
		else if (answer_rotate_2_.z - safe_range_.z <= vase_rotate_.z && vase_rotate_.z <= answer_rotate_2_.z + safe_range_.z)
		{
			debug_text_[2].color = new Color(0.4f, 0.8f, 1f);
		}
		else
		{
			debug_text_[2].color = new Color(0.9f, 0.9f, 0.9f);
		}
	}
}
