using System;
using UnityEngine;

namespace DebugMenu
{
	public class Menu : MonoBehaviour
	{
		[SerializeField]
		private int input_repeat_delay_frame_ = 15;

		[SerializeField]
		private int input_repeat_frame_ = 5;

		private Group root_ = new Group("Root");

		private Group current_;

		private Func<Input> input_func_ = DefaultInputFunc;

		private Input input_down_;

		private Input input_press_;

		private Input input_repeat_;

		private Input input_repeat_delay_;

		private int[] input_repeat_frame_counter_ = new int[6];

		public Group root
		{
			get
			{
				return root_;
			}
		}

		public Group current
		{
			get
			{
				return current_;
			}
			set
			{
				if (current_ != value && (value == null || HasItem(value)))
				{
					Group group = current_;
					current_ = value;
					if (group != null)
					{
						group.OnSetCurrent(false);
					}
					if (current_ != null)
					{
						current_.OnSetCurrent(true);
					}
					OnChangeCurrent(group);
				}
			}
		}

		public Func<Input> input_func
		{
			set
			{
				input_func_ = value;
			}
		}

		public int input_repeat_delay_frame
		{
			get
			{
				return input_repeat_delay_frame_;
			}
			set
			{
				input_repeat_delay_frame_ = value;
			}
		}

		public int input_repeat_frame
		{
			get
			{
				return input_repeat_frame_;
			}
			set
			{
				input_repeat_frame_ = value;
			}
		}

		public Input input_down
		{
			get
			{
				return input_down_;
			}
			protected set
			{
				input_down_ = value;
			}
		}

		public Input input_press
		{
			get
			{
				return input_press_;
			}
			protected set
			{
				input_press_ = value;
			}
		}

		public Input input_repeat
		{
			get
			{
				return input_repeat_;
			}
			protected set
			{
				input_repeat_ = value;
			}
		}

		public Input input_repeat_delay
		{
			get
			{
				return input_repeat_delay_;
			}
			protected set
			{
				input_repeat_delay_ = value;
			}
		}

		public int[] input_repeat_frame_counter
		{
			get
			{
				return input_repeat_frame_counter_;
			}
		}

		public static Input DefaultInputFunc()
		{
			Input input = Input.None;
			if (padCtrl.instance.InputGetKeyDown(KeyCode.LeftArrow) || padCtrl.instance.InputGetKey(KeyCode.LeftArrow))
			{
				input |= Input.Left;
			}
			if (padCtrl.instance.InputGetKeyDown(KeyCode.RightArrow) || padCtrl.instance.InputGetKey(KeyCode.RightArrow))
			{
				input |= Input.Right;
			}
			if (padCtrl.instance.InputGetKeyDown(KeyCode.UpArrow) || padCtrl.instance.InputGetKey(KeyCode.UpArrow))
			{
				input |= Input.Up;
			}
			if (padCtrl.instance.InputGetKeyDown(KeyCode.DownArrow) || padCtrl.instance.InputGetKey(KeyCode.DownArrow))
			{
				input |= Input.Down;
			}
			if (padCtrl.instance.InputGetKeyDown(KeyCode.Return) || padCtrl.instance.InputGetKey(KeyCode.Return))
			{
				input |= Input.Decide;
			}
			if (padCtrl.instance.InputGetKeyDown(KeyCode.Escape) || padCtrl.instance.InputGetKey(KeyCode.Escape))
			{
				input |= Input.Cancel;
			}
			return input;
		}

		public void Open()
		{
			if (!IsOpen())
			{
				current = root_;
			}
		}

		public void Close()
		{
			current = null;
		}

		public bool IsOpen()
		{
			return current_ != null;
		}

		private void FixedUpdate()
		{
			Process();
		}

		protected virtual void Process()
		{
			UpdateInput();
			UpdateCurrent();
		}

		protected void UpdateCurrent()
		{
			if (current_ != null)
			{
				current_.Process(this);
			}
		}

		protected void UpdateInput()
		{
			Input input = Input.None;
			if (input_func_ != null)
			{
				input = input_func_();
			}
			input_down_ = input & (input ^ input_press_);
			input_press_ = input;
			input_repeat_ = Input.None;
			for (int i = 0; i < input_repeat_frame_counter_.Length; i++)
			{
				if (((uint)((int)input >> i) & (true ? 1u : 0u)) != 0)
				{
					input_repeat_frame_counter_[i]++;
					if (((uint)((int)input_repeat_delay_ >> i) & (true ? 1u : 0u)) != 0)
					{
						if (input_repeat_frame_counter_[i] >= input_repeat_frame_)
						{
							input_repeat_frame_counter_[i] = 0;
							input_repeat_ |= (Input)(1 << i);
						}
					}
					else if (input_repeat_frame_counter_[i] >= input_repeat_delay_frame_)
					{
						input_repeat_frame_counter_[i] = 0;
						input_repeat_ |= (Input)(1 << i);
						input_repeat_delay_ |= (Input)(1 << i);
					}
				}
				else
				{
					input_repeat_frame_counter_[i] = 0;
					input_repeat_delay_ &= (Input)(~(1 << i));
				}
			}
		}

		private bool HasItem(Item item)
		{
			while (item != null && item != root_)
			{
				item = item.parent;
			}
			return item == root_;
		}

		protected virtual void OnChangeCurrent(Group last)
		{
		}

		protected virtual void OnChangeCurrentGroupItem()
		{
		}

		protected virtual void OnChangeCurrentGroupSelected(Item last)
		{
		}
	}
}
