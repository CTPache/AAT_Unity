using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class FingerCursor : MonoBehaviour
{
	[SerializeField]
	private Image finger_image;

	public bool ChangeSpriteIfHovering;

	[SerializeField]
	public MonoBehaviour RectHolder;

	[SerializeField]
	public Camera WorldStandard;

	public Rect MovableRange;

	public float Speed;

	public bool is_answer = true;

	private Sprite default_cursor_image;

	private Sprite hit_cursor_image;

	[SerializeField]
	private RectTransform touch_rect;

	[SerializeField]
	private InputTouch touch_;

	[SerializeField]
	private InputTouch cursor_touch_;

	private Vector3 old_finger_position_ = default(Vector3);

	private bool is_drag_;

	private Vector2 cursor_pos_offset_ = new Vector2(36f, -36f);

	private const float KEY_GUIDE_HEIGHT = -400f;

	private const float DRAG_NECESSARY_POWER = 30f;

	private AssetBundleCtrl bundle_controll
	{
		get
		{
			return AssetBundleCtrl.instance;
		}
	}

	public float movie_keyguide_scroll_limit { get; set; }

	public void Initialize()
	{
		if (default_cursor_image == null)
		{
			AssetBundle assetBundle = bundle_controll.load("/menu/common/", "s2d051");
			Sprite[] array = assetBundle.LoadAssetWithSubAssets<Sprite>("s2d051");
			default_cursor_image = array[0];
			hit_cursor_image = array[1];
		}
		finger_image.sprite = default_cursor_image;
		is_answer = false;
		cursor_touch_ = GetComponent<InputTouch>();
		cursor_touch_.SetColliderSize(new Vector2(28f, 28f));
		cursor_touch_.GetComponent<BoxCollider2D>().offset = new Vector2(-30f, 30f);
		touch_.SetColliderSize(new Vector2(systemCtrl.instance.ScreenWidth, systemCtrl.instance.ScreenHeight));
		InitTouch();
	}

	public void ActiveTouch()
	{
		if (cursor_touch_ != null)
		{
			cursor_touch_.ActiveCollider();
		}
		touch_.ActiveCollider();
	}

	private void InitTouch()
	{
		cursor_touch_.touch_event = delegate
		{
			if (!is_answer)
			{
				UpdateTouchPosition(false);
			}
			cursor_touch_.touch_key_type = ((!is_drag_) ? KeyType.X : KeyType.None);
		};
		cursor_touch_.down_event = DownCursorOrScreen;
		cursor_touch_.drag_event = DragCursor;
		touch_.down_event = DownCursorOrScreen;
		touch_.drag_event = DragCursor;
	}

	private void UpdateTouchPosition(bool _is_touch_down = false)
	{
		UpdateCursor();
		Vector2 vector = TouchUtility.GetTouchPosition();
		Vector2 vector2 = WorldStandard.ScreenToViewportPoint(vector);
		vector = new Vector2((float)systemCtrl.instance.ScreenWidth * vector2.x, (float)systemCtrl.instance.ScreenHeight * vector2.y) + new Vector2(-960f, -540f);
		vector += cursor_pos_offset_;
		vector.x = Mathf.Max(vector.x, MovableRange.x);
		vector.x = Mathf.Min(vector.x, MovableRange.x + MovableRange.width);
		vector.y = Mathf.Max(vector.y, MovableRange.y);
		vector.y = Mathf.Min(vector.y, MovableRange.y + MovableRange.height);
		if (vector.x > movie_keyguide_scroll_limit && vector.y < -400f)
		{
			vector.y = ((!_is_touch_down) ? base.transform.localPosition.y : (-400f));
		}
		if (vector.x > movie_keyguide_scroll_limit && vector.y < -400f)
		{
			vector.x = movie_keyguide_scroll_limit;
		}
		base.transform.localPosition = vector;
	}

	public void UpdateCursor()
	{
		Vector3 localPosition = base.transform.localPosition;
		if (TouchUtility.GetTouch() == TouchInfo.None)
		{
			padCtrl instance = padCtrl.instance;
			Vector3 vector = Vector2.zero;
			vector.x = (instance.GetKey(KeyType.Right) ? 1f : ((!instance.GetKey(KeyType.Left)) ? 0f : (-1f)));
			vector.y = (instance.GetKey(KeyType.Up) ? 1f : ((!instance.GetKey(KeyType.Down)) ? 0f : (-1f)));
			if (padCtrl.instance.axis_pos_L.x > 0f || padCtrl.instance.axis_pos_L.x < 0f)
			{
				localPosition.x += padCtrl.instance.axis_pos_L.x * Speed;
			}
			if (padCtrl.instance.axis_pos_L.y > 0f || padCtrl.instance.axis_pos_L.y < 0f)
			{
				localPosition.y += padCtrl.instance.axis_pos_L.y * Speed;
			}
			if (vector != Vector3.zero)
			{
				vector.Normalize();
				localPosition += vector * Speed;
				localPosition += vector;
			}
			if (MovableRange.size != Vector2.zero)
			{
				localPosition.x = Mathf.Max(localPosition.x, MovableRange.x);
				localPosition.x = Mathf.Min(localPosition.x, MovableRange.x + MovableRange.width);
				localPosition.y = Mathf.Max(localPosition.y, MovableRange.y);
				localPosition.y = Mathf.Min(localPosition.y, MovableRange.y + MovableRange.height);
			}
			if (localPosition.x > movie_keyguide_scroll_limit && localPosition.y < -400f)
			{
				localPosition.y = base.transform.localPosition.y;
			}
			if (localPosition.x > movie_keyguide_scroll_limit && localPosition.y < -400f)
			{
				localPosition.x = movie_keyguide_scroll_limit;
			}
			base.transform.localPosition = localPosition;
		}
		if (!(RectHolder == null) && ChangeSpriteIfHovering)
		{
		}
	}

	private void DownCursorOrScreen(Vector3 touch_pos)
	{
		if (!is_answer)
		{
			is_drag_ = false;
			old_finger_position_ = TouchUtility.GetTouchPosition();
			UpdateTouchPosition(true);
		}
	}

	private void DragCursor(Vector3 pos)
	{
		if (!is_answer)
		{
			UpdateTouchPosition(false);
			Vector3 touchPosition = TouchUtility.GetTouchPosition();
			float num = Vector3.Distance(old_finger_position_, touchPosition);
			if (num > 30f && !is_drag_)
			{
				is_drag_ = true;
			}
		}
		else
		{
			is_drag_ = false;
		}
	}

	public void ChangeCursor()
	{
		finger_image.sprite = hit_cursor_image;
	}

	private bool IsCollided(Rect a, IEnumerable<Rect> bs)
	{
		return bs.Any((Rect r) => IsCollided(a, r));
	}

	private bool IsCollided(Rect a, Rect b)
	{
		return a.Overlaps(b);
	}

	public int GetCollidedNo()
	{
		IEnumerable<RectTransform> rects = (RectHolder as IRectHolder).Rects;
		return GetCollidedNo(touch_rect, rects);
	}

	private int GetCollidedNo(RectTransform a, IEnumerable<RectTransform> b)
	{
		Vector3[] array = new Vector3[4];
		a.GetWorldCorners(array);
		int num = 0;
		foreach (RectTransform item in b)
		{
			if (item != null)
			{
				Vector3[] array2 = new Vector3[4];
				item.GetWorldCorners(array2);
				if (IsCollided(array2, array))
				{
					return num;
				}
			}
			num++;
		}
		return 4;
	}

	private bool IsCollided(Vector3[] a, Vector3[] b)
	{
		Vector3[] array = new Vector3[4]
		{
			a[1] - a[0],
			a[2] - a[1],
			a[3] - a[2],
			a[0] - a[3]
		};
		for (int i = 0; i < 4; i++)
		{
			bool flag = true;
			Vector3 vector = b[i];
			for (int j = 0; j < 4; j++)
			{
				if (Vector3.Cross(array[j], vector - a[j]).z > 0f)
				{
					flag = false;
					break;
				}
			}
			if (flag)
			{
				return true;
			}
		}
		return false;
	}
}
