using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public abstract class BaseTouch : MonoBehaviour
{
	[SerializeField]
	private BoxCollider2D box_collider_2d_;

	public Action<TouchParameter> touch_event { protected get; set; }

	public Action<Vector3> drag_event { protected get; set; }

	public Action<Vector3> wait_event { protected get; set; }

	public Action<Vector3> down_event { protected get; set; }

	public Action<Vector3> up_event { protected get; set; }

	public object argument_parameter { private get; set; }

	public KeyType touch_key_type { get; set; }

	public KeyType touched_key_type { get; set; }

	public KeyType dow_key_type { get; set; }

	public KeyType questioning_touch_type { get; set; }

	public bool box_collider_2d_enable
	{
		get
		{
			return box_collider_2d_.enabled;
		}
	}

	public void ActiveCollider()
	{
		box_collider_2d_.enabled = false;
		box_collider_2d_.enabled = true;
	}

	public void SetColliderSize(Vector2 size)
	{
		box_collider_2d_.size = size;
	}

	public void SetColliderOffset(Vector2 offset)
	{
		box_collider_2d_.offset = offset;
	}

	public virtual void OnDown(Vector3 pos)
	{
		if (down_event != null)
		{
			down_event(pos);
		}
	}

	public virtual void OnTouch(Vector3 touch_pos)
	{
		if (touch_event != null)
		{
			TouchParameter touchParameter = new TouchParameter();
			touchParameter.argument_parameter = argument_parameter;
			touchParameter.pos = touch_pos;
			TouchParameter obj = touchParameter;
			touch_event(obj);
		}
	}

	public virtual void OnUp(Vector3 up_pos)
	{
		if (up_event != null)
		{
			up_event(up_pos);
		}
	}

	public virtual void OnDrag(Vector3 drag_pos)
	{
		if (drag_event != null)
		{
			drag_event(drag_pos);
		}
	}

	public virtual void OnWait(Vector3 pos)
	{
		if (wait_event != null)
		{
			wait_event(pos);
		}
	}

	public void SetEnableCollider(bool enable)
	{
		if (box_collider_2d_.enabled != enable)
		{
			box_collider_2d_.enabled = enable;
		}
	}

	private void Awake()
	{
		TouchSystem.AddTouch(this);
		if (box_collider_2d_ == null)
		{
			box_collider_2d_ = GetComponent<BoxCollider2D>();
		}
	}

	private void Reset()
	{
		box_collider_2d_ = GetComponent<BoxCollider2D>();
	}
}
