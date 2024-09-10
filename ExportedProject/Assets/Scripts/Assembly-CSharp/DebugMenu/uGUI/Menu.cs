using System;
using UnityEngine;

namespace DebugMenu.uGUI
{
	public class Menu : DebugMenu.Menu
	{
		[SerializeField]
		private int item_width_;

		[SerializeField]
		private int item_height_;

		[SerializeField]
		private Color frame_color_ = Color.white;

		[SerializeField]
		private Color selected_frame_color_ = Color.yellow;

		[SerializeField]
		private Font text_font_;

		[SerializeField]
		private int text_font_size_;

		[SerializeField]
		private Color text_color_ = Color.black;

		[SerializeField]
		private Color selected_text_color_ = Color.black;

		private Group current_ugui_;

		private GameObject current_obj_;

		public int item_width
		{
			get
			{
				return item_width_;
			}
		}

		public int item_height
		{
			get
			{
				return item_height_;
			}
		}

		public Color frame_color
		{
			get
			{
				return frame_color_;
			}
		}

		public Color selected_frame_color
		{
			get
			{
				return selected_frame_color_;
			}
		}

		public Font text_font
		{
			get
			{
				return text_font_;
			}
		}

		public int text_font_size
		{
			get
			{
				return text_font_size_;
			}
		}

		public Color text_color
		{
			get
			{
				return text_color_;
			}
		}

		public Color selected_text_color
		{
			get
			{
				return selected_text_color_;
			}
		}

		public Item NewItem(DebugMenu.Item item)
		{
			Type type = item.GetType();
			if (type == typeof(DebugMenu.Group))
			{
				return new Group((DebugMenu.Group)item);
			}
			if (type == typeof(DebugMenu.Func))
			{
				return new Func((DebugMenu.Func)item);
			}
			if (type == typeof(DebugMenu.Number<int>))
			{
				return new Number<int>((DebugMenu.Number<int>)item);
			}
			if (type == typeof(DebugMenu.Number<float>))
			{
				return new Number<float>((DebugMenu.Number<float>)item);
			}
			if (type == typeof(DebugMenu.Bool))
			{
				return new Bool((DebugMenu.Bool)item);
			}
			return null;
		}

		protected override void OnChangeCurrent(DebugMenu.Group last)
		{
			if (current_obj_ != null)
			{
				UnityEngine.Object.Destroy(current_obj_);
				current_obj_ = null;
			}
			current_ugui_ = null;
			if (base.current != null)
			{
				current_ugui_ = new Group(base.current);
				current_obj_ = current_ugui_.CreateObject(this, base.transform, true);
			}
		}

		protected override void OnChangeCurrentGroupSelected(DebugMenu.Item last)
		{
			if (current_ugui_ != null)
			{
				Item item = current_ugui_.FindItem(last);
				if (item != null)
				{
					item.SetSelected(this, false);
				}
				item = current_ugui_.FindItem(base.current.selected);
				if (item != null)
				{
					item.SetSelected(this, true);
				}
			}
		}
	}
}
