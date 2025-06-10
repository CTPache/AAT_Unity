using System;
using UnityEngine;

public class ScrollBarCtrl : MonoBehaviour
{
	[Serializable]
	public class ScrollBar
	{
		public AssetBundleSprite bar_;

		public AssetBundleSprite bar_frame_;

		public InputTouch bar_touch_;
	}

	private float move_width_;

	private float move_val_;

	private float flame_size_y;

	private Vector2 bar_def_size = default(Vector2);

	[SerializeField]
public ScrollBar scrl_bar_;

	public float scroll_bar_normalize
	{
		get
		{
			return scrl_bar_.bar_.transform.localPosition.y / (0f - move_width_);
		}
		set
		{
			Vector3 localPosition = scrl_bar_.bar_.transform.localPosition;
			float num = value;
			if (num > 1f)
			{
				num = 1f;
			}
			else if (num < 0f)
			{
				num = 0f;
			}
			scrl_bar_.bar_.transform.localPosition = new Vector3(localPosition.x, (0f - move_width_) * value, localPosition.z);
		}
	}

	public void Load()
	{
		scrl_bar_.bar_.load("/menu/common/", "scrollbar");
		scrl_bar_.bar_frame_.load("/menu/common/", "scrollbar_frame");
		bar_def_size = scrl_bar_.bar_.sprite_renderer_.size;
		scrl_bar_.bar_touch_.down_event = delegate
		{
			SaveLoadUICtrl.instance.is_touch_scroll_bar = true;
			SaveLoadUICtrl.instance.is_touch_move_list = true;
		};
		scrl_bar_.bar_touch_.up_event = delegate
		{
			SaveLoadUICtrl.instance.is_touch_scroll_bar = false;
		};
		scrl_bar_.bar_touch_.drag_event = delegate(Vector3 p)
		{
			SaveLoadUICtrl.instance.is_keyboard = false;
			Vector3 position = scrl_bar_.bar_.transform.position;
			scrl_bar_.bar_.transform.position = new Vector3(position.x, p.y, position.z);
			Vector3 barPos = scrl_bar_.bar_.transform.localPosition;
			if (barPos.y > 0f)
			{
				barPos = new Vector3(barPos.x, 0f, barPos.z);
			}
			else if (barPos.y < 0f - move_width_)
			{
				barPos = new Vector3(barPos.x, 0f - move_width_, barPos.z);
			}
			SetBarPos(barPos);
			SaveLoadUICtrl.instance.UpdateSlotList();
			SaveLoadUICtrl.instance.UpdateCursorwithScroll();
		};
		scrl_bar_.bar_touch_.ActiveCollider();
	}

	public void BarLengthReset()
	{
		scrl_bar_.bar_.sprite_renderer_.size = bar_def_size;
	}

	public void BarLengthSet(int item_cnt)
	{
		flame_size_y = scrl_bar_.bar_frame_.sprite_data_[0].bounds.size.y;
		move_val_ = flame_size_y / (float)(item_cnt - 1);
		float num = move_val_ / bar_def_size.y;
		move_width_ = flame_size_y;
		scrl_bar_.bar_.sprite_renderer_.size = new Vector2(bar_def_size.x, bar_def_size.y * num);
		BarMoveTop();
	}

	public void BarLengthSet(int item_cnt, int disp_cnt)
	{
		flame_size_y = scrl_bar_.bar_frame_.sprite_data_[0].bounds.size.y;
		float num = bar_def_size.y * (float)(item_cnt - disp_cnt);
		move_width_ = flame_size_y - num;
		move_val_ = move_width_ / (float)(item_cnt - disp_cnt);
		scrl_bar_.bar_.sprite_renderer_.size = new Vector2(bar_def_size.x, num);
		BarMoveTop();
	}

	public void BarMove(int dir)
	{
		Vector3 localPosition = scrl_bar_.bar_.transform.localPosition;
		localPosition += new Vector3(0f, move_val_ * (float)dir, 0f);
		SetBarPos(localPosition);
	}

	public void BarMoveTop()
	{
		Vector3 localPosition = scrl_bar_.bar_.transform.localPosition;
		SetBarPos(new Vector3(localPosition.x, 0f, localPosition.z));
	}

	public void BarMoveBottom()
	{
		Vector3 localPosition = scrl_bar_.bar_.transform.localPosition;
		SetBarPos(new Vector3(localPosition.x, 0f - move_width_ / 2f, localPosition.z));
	}

	public void SetBarPos(Vector3 pos)
	{
		scrl_bar_.bar_.transform.localPosition = pos;
	}

	public void ResetBarCollider()
	{
		scrl_bar_.bar_touch_.ActiveCollider();
	}
}
