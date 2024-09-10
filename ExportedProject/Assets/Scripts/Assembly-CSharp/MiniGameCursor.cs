using System;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameCursor : MonoBehaviour
{
	[SerializeField]
	private Canvas canvas_;

	[SerializeField]
	private RectTransform cursor_parent_;

	[SerializeField]
	private RectTransform cursor_;

	[SerializeField]
	private Image icon_;

	[SerializeField]
	private float cursor_speed_;

	[SerializeField]
	private InputTouch touch_cursor_;

	[SerializeField]
	private Canvas cursor_canvas_;

	private Vector3 cursor_position_;

	private Rect cursor_limit_;

	private Vector2 cursor_exception_limit_ = Vector2.zero;

	private bool possible_update_ = true;

	public static MiniGameCursor instance { get; private set; }

	public InputTouch touch_cursor
	{
		get
		{
			return touch_cursor_;
		}
	}

	public Canvas canvas
	{
		get
		{
			return canvas_;
		}
		set
		{
			canvas_ = value;
		}
	}

	public Vector2 cursor_area_size
	{
		get
		{
			return cursor_parent_.sizeDelta;
		}
	}

	public Vector3 icon_offset
	{
		set
		{
			icon_.rectTransform.localPosition = value;
		}
	}

	public KeyType cursor_key_type
	{
		set
		{
			touch_cursor_.touch_key_type = value;
		}
	}

	public Rect cursor_limit
	{
		set
		{
			cursor_limit_ = value;
		}
	}

	public Vector2 cursor_exception_limit
	{
		get
		{
			return cursor_exception_limit_;
		}
		set
		{
			cursor_exception_limit_ = value;
		}
	}

	public bool icon_visible
	{
		set
		{
			icon_.enabled = value;
			touch_cursor_.SetEnableCollider(value);
		}
	}

	public Vector3 cursor_position
	{
		get
		{
			return cursor_position_;
		}
		set
		{
			cursor_position_ = value;
			UpdateCursorPosition();
		}
	}

	public Sprite icon_sprite
	{
		set
		{
			icon_.sprite = value;
			icon_.SetNativeSize();
		}
	}

	public void ResetCursorLimit()
	{
		Vector2 vector = cursor_area_size;
		cursor_limit_ = new Rect(0f, 0f, vector.x, vector.y);
	}

	public void ActiveCursorTouch()
	{
		touch_cursor_.ActiveCollider();
	}

	public void ActiveCursorCanvas(bool active)
	{
		cursor_canvas_.overrideSorting = active;
	}

	public bool IsTouchSafeArea()
	{
		Vector2 localPoint;
		RectTransformUtility.ScreenPointToLocalPointInRectangle(cursor_parent_, padCtrl.instance.InputMousePosition(), canvas_.worldCamera, out localPoint);
		localPoint.y = 0f - localPoint.y;
		if (cursor_exception_limit_ != Vector2.zero && localPoint.x > cursor_exception_limit_.x && localPoint.y > cursor_exception_limit_.y)
		{
			return false;
		}
		return true;
	}

	public void SetCursorTouchAction(Action<TouchParameter> action)
	{
		touch_cursor_.touch_event = action;
	}

	public void Update()
	{
		if (padCtrl.instance.InputGetMouseButtonDown(0))
		{
			if (TouchSystem.instance.HitSomeCollider())
			{
				possible_update_ = false;
			}
			else
			{
				UpdateExceptionLimitHitCursorPosition();
				UpdateCursorPosition();
			}
		}
		else if (!padCtrl.instance.InputGetMouseButton(0))
		{
			possible_update_ = true;
		}
		if (padCtrl.instance.InputGetMouseButton(0))
		{
			if (possible_update_)
			{
				UpdateTouchCursorPosition();
			}
		}
		else
		{
			cursor_position_.x = cursor_.localPosition.x;
			cursor_position_.y = 0f - cursor_.localPosition.y;
			Vector3 zero = Vector3.zero;
			Vector3 zero2 = Vector3.zero;
			if (padCtrl.instance.axis_pos_L.x > 0f || padCtrl.instance.axis_pos_L.x < 0f)
			{
				zero2.x = padCtrl.instance.axis_pos_L.x * cursor_speed_;
			}
			if (padCtrl.instance.axis_pos_L.y > 0f || padCtrl.instance.axis_pos_L.y < 0f)
			{
				zero2.y = 0f - padCtrl.instance.axis_pos_L.y * cursor_speed_;
			}
			padCtrl padCtrl2 = padCtrl.instance;
			if (padCtrl2.GetKey(KeyType.Up))
			{
				zero.y = -1f;
			}
			if (padCtrl2.GetKey(KeyType.Down))
			{
				zero.y = 1f;
			}
			if (zero != Vector3.zero)
			{
				zero.Normalize();
				zero2.y = zero.y * cursor_speed_;
			}
			if (zero2.y != 0f)
			{
				cursor_position_.y += zero2.y;
			}
			cursor_position_.y = Mathf.Clamp(cursor_position_.y, cursor_limit_.y, cursor_limit_.height);
			if (cursor_exception_limit_ != Vector2.zero && cursor_position_.x > cursor_exception_limit_.x && cursor_position_.y > cursor_exception_limit_.y)
			{
				cursor_position_.y = Mathf.Abs(cursor_.localPosition.y);
			}
			UpdateCursorPosition();
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
				zero.Normalize();
				zero2.x = zero.x * cursor_speed_;
			}
			if (zero2.x != 0f)
			{
				cursor_position_.x += zero2.x;
			}
			cursor_position_.x = Mathf.Clamp(cursor_position_.x, cursor_limit_.x, cursor_limit_.width);
			if (cursor_exception_limit_ != Vector2.zero && cursor_position_.x > cursor_exception_limit_.x && cursor_position_.y > cursor_exception_limit_.y)
			{
				cursor_position_.x = cursor_exception_limit_.x;
				if (zero != Vector3.zero)
				{
					zero.Normalize();
					cursor_position_.y += zero.y * cursor_speed_;
				}
				cursor_position_.y = Mathf.Clamp(cursor_position_.y, cursor_limit_.y, cursor_limit_.height);
				if (cursor_position_.x > cursor_exception_limit_.x && cursor_position_.y > cursor_exception_limit_.y)
				{
					cursor_position_.y = Mathf.Abs(cursor_.localPosition.y);
				}
			}
		}
		UpdateCursorPosition();
	}

	private void UpdateCursorPosition()
	{
		Vector2 vector = cursor_position_;
		vector.y = 0f - vector.y;
		cursor_.localPosition = vector;
	}

	private void UpdateTouchCursorPosition()
	{
		Vector2 localPoint;
		RectTransformUtility.ScreenPointToLocalPointInRectangle(cursor_parent_, padCtrl.instance.InputMousePosition(), canvas_.worldCamera, out localPoint);
		localPoint.y = 0f - localPoint.y;
		localPoint.y = Mathf.Clamp(localPoint.y, cursor_limit_.y, cursor_limit_.height);
		localPoint.x = Mathf.Clamp(localPoint.x, cursor_limit_.x, cursor_limit_.width);
		cursor_position_ = localPoint;
		if (cursor_exception_limit_ != Vector2.zero && localPoint.x > cursor_exception_limit_.x && localPoint.y > cursor_exception_limit_.y)
		{
			cursor_position_.y = Mathf.Abs(cursor_.localPosition.y);
			if (cursor_position_.x > cursor_exception_limit_.x && cursor_position_.y > cursor_exception_limit_.y)
			{
				cursor_position_.x = cursor_exception_limit_.x;
			}
		}
	}

	private void UpdateExceptionLimitHitCursorPosition()
	{
		Vector2 localPoint;
		RectTransformUtility.ScreenPointToLocalPointInRectangle(cursor_parent_, padCtrl.instance.InputMousePosition(), canvas_.worldCamera, out localPoint);
		localPoint.y = 0f - localPoint.y;
		localPoint.y = Mathf.Clamp(localPoint.y, cursor_limit_.y, cursor_limit_.height);
		localPoint.x = Mathf.Clamp(localPoint.x, cursor_limit_.x, cursor_limit_.width);
		cursor_position_ = localPoint;
		if (cursor_exception_limit_ != Vector2.zero && localPoint.x > cursor_exception_limit_.x && localPoint.y > cursor_exception_limit_.y)
		{
			cursor_position_.y = 0f - cursor_exception_limit_.y;
			if (cursor_position_.x > cursor_exception_limit_.x && cursor_position_.y > cursor_exception_limit_.y)
			{
				cursor_position_.x = cursor_exception_limit_.x;
			}
		}
	}

	private void Awake()
	{
		instance = this;
		ResetCursorLimit();
		base.enabled = false;
		touch_cursor_.SetEnableCollider(false);
		ActiveCursorCanvas(false);
	}
}
