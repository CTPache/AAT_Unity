using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-99999)]
public class TouchSystem : MonoBehaviour
{
	[SerializeField]
public List<Camera> touch_camera_list_ = new List<Camera>();

	private static KeyType touched_type_ = KeyType.None;

	private static KeyType touch_type_ = KeyType.None;

	public static KeyType down_type_ = KeyType.None;

	public static KeyType questioning_touch_type_ = KeyType.None;

	private Collider2D touch_collider_;

	private static List<BaseTouch> base_touch_list_ = new List<BaseTouch>();

	private BaseTouch begin_touch_;

	private static TouchSystem instance_ = null;

	public static TouchSystem instance
	{
		get
		{
			return instance_;
		}
	}

	public List<Camera> touch_camera_list
	{
		get
		{
			return touch_camera_list_;
		}
	}

	public static void AddTouch(BaseTouch touch)
	{
		base_touch_list_.Add(touch);
	}

	public static bool GetTouch(KeyType key)
	{
		return touch_type_ == key;
	}

	public static bool GetTouched(KeyType key)
	{
		return touched_type_ == key;
	}

	public static bool GetDown(KeyType key)
	{
		bool result = down_type_ == key;
		down_type_ = KeyType.None;
		return result;
	}

	public static bool GetTouchDownQuestioning(KeyType key)
	{
		bool flag = questioning_touch_type_ == key;
		if (flag)
		{
			questioning_touch_type_ = KeyType.None;
		}
		return flag;
	}

	public static void TouchInActive()
	{
		foreach (BaseTouch item in base_touch_list_)
		{
			if (!(item == null))
			{
				item.SetEnableCollider(false);
			}
		}
	}

	public void TouchReset()
	{
		touched_type_ = KeyType.None;
		begin_touch_ = null;
	}

	public bool HitSomeCollider()
	{
		bool result = false;
		foreach (Camera item in touch_camera_list_)
		{
			if (item.gameObject.activeInHierarchy)
			{
				if (fadeCtrl.instance != null && !fadeCtrl.instance.is_end)
				{
					break;
				}
				int cullingMask = item.cullingMask;
				Vector2 point = TouchUtility.GetTouchWorldPosition(item);
				Collider2D collider2D = Physics2D.OverlapPoint(point, cullingMask);
				if (collider2D != null && collider2D.enabled && collider2D.tag == "Untagged")
				{
					result = true;
					break;
				}
			}
		}
		return result;
	}

	public void UpdateDownEvent()
	{
		Vector3 touchWorldPosition = TouchUtility.GetTouchWorldPosition(touch_camera_list_[0]);
		int cullingMask = touch_camera_list_[0].cullingMask;
		if (!(Physics2D.OverlapPoint(touchWorldPosition, cullingMask) == null) && TouchUtility.GetTouch() == TouchInfo.Begin && begin_touch_ != null)
		{
			begin_touch_.OnDown(touchWorldPosition);
			down_type_ = begin_touch_.dow_key_type;
			questioning_touch_type_ = begin_touch_.questioning_touch_type;
		}
	}

	private void UpdateTouch()
	{
		TouchInfo touch = TouchUtility.GetTouch();
		touch_type_ = KeyType.None;
		down_type_ = KeyType.None;
		questioning_touch_type_ = KeyType.None;
		foreach (Camera item in touch_camera_list_)
		{
			if (!item.gameObject.activeInHierarchy)
			{
				continue;
			}
			if (fadeCtrl.instance != null && !fadeCtrl.instance.is_end)
			{
				break;
			}
			bool flag = true;
			int cullingMask = item.cullingMask;
			switch (touch)
			{
			case TouchInfo.Begin:
			{
				Vector3 touchWorldPosition = TouchUtility.GetTouchWorldPosition(item);
				touch_collider_ = Physics2D.OverlapPoint(touchWorldPosition, cullingMask);
				if (touch_collider_ == null)
				{
					flag = false;
					break;
				}
				begin_touch_ = touch_collider_.GetComponent<BaseTouch>();
				if (begin_touch_ != null)
				{
					begin_touch_.OnDown(touchWorldPosition);
					touched_type_ = begin_touch_.touched_key_type;
					down_type_ = begin_touch_.dow_key_type;
					questioning_touch_type_ = begin_touch_.questioning_touch_type;
				}
				break;
			}
			case TouchInfo.Wait:
				down_type_ = KeyType.None;
				questioning_touch_type_ = KeyType.None;
				if (begin_touch_ != null)
				{
					Vector3 touchWorldPosition = TouchUtility.GetTouchWorldPosition(item);
					begin_touch_.OnWait(touchWorldPosition);
					touched_type_ = begin_touch_.touched_key_type;
				}
				break;
			case TouchInfo.Move:
				down_type_ = KeyType.None;
				questioning_touch_type_ = KeyType.None;
				if (begin_touch_ != null)
				{
					Vector3 touchWorldPosition = TouchUtility.GetTouchWorldPosition(item);
					begin_touch_.OnDrag(touchWorldPosition);
					touched_type_ = begin_touch_.touched_key_type;
				}
				break;
			case TouchInfo.None:
			case TouchInfo.End:
			{
				down_type_ = KeyType.None;
				questioning_touch_type_ = KeyType.None;
				touched_type_ = KeyType.None;
				Vector3 touchWorldPosition;
				if (begin_touch_ != null)
				{
					touchWorldPosition = TouchUtility.GetTouchWorldPosition(item);
					begin_touch_.OnUp(touchWorldPosition);
					begin_touch_ = null;
				}
				touchWorldPosition = TouchUtility.GetTouchWorldPosition(item);
				Collider2D collider2D = Physics2D.OverlapPoint(touchWorldPosition, cullingMask);
				if (touch_collider_ == null || collider2D == null)
				{
					flag = false;
					break;
				}
				if (collider2D.GetInstanceID() == touch_collider_.GetInstanceID())
				{
					BaseTouch component = collider2D.GetComponent<BaseTouch>();
					if (component != null)
					{
						component.OnTouch(touchWorldPosition);
						touch_type_ = component.touch_key_type;
					}
				}
				touch_collider_ = null;
				break;
			}
			}
			if (!flag)
			{
				continue;
			}
			break;
		}
	}

	public void SetAllTouchEnabled(bool enable)
	{
		for (int i = 0; i < base_touch_list_.Count; i++)
		{
			if (base_touch_list_[i] != null)
			{
				base_touch_list_[i].SetEnableCollider(enable);
			}
			else
			{
				base_touch_list_.Remove(base_touch_list_[i]);
			}
		}
	}

	private void Awake()
	{
		if (instance_ == null)
		{
			instance_ = this;
		}
	}

	private void FixedUpdate()
	{
		Process();
	}

	private void Process()
	{
		UpdateTouch();
	}
}
