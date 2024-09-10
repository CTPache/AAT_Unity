using System;

namespace DebugMenu
{
	public class Bool : Item
	{
		private BoolAccessor accessor_;

		private bool last_;

		public bool flag
		{
			get
			{
				return accessor_.value;
			}
			set
			{
				SetFlag(value);
			}
		}

		public event Action update_flag
		{
			add
			{
				update_flag_ += value;
			}
			remove
			{
				update_flag_ -= value;
			}
		}

		private event Action update_flag_;

		public Bool(string name, BoolAccessor accessor)
			: base(name)
		{
			accessor_ = accessor;
			last_ = accessor_.value;
		}

		public Bool AddUpdateFlagEventListener(Action func)
		{
			update_flag_ += func;
			return this;
		}

		public void OnFlip()
		{
			SetFlag(!accessor_.value);
		}

		public override void Update(Menu menu)
		{
			if (((menu.input_down | menu.input_repeat) & Input.Decide) != 0)
			{
				OnFlip();
			}
			else if (last_.CompareTo(accessor_.value) != 0)
			{
				UpdateFlag();
			}
		}

		private void SetFlag(bool flag)
		{
			accessor_.value = flag;
			UpdateFlag();
		}

		private void UpdateFlag()
		{
			last_ = accessor_.value;
			if (this.update_flag_ != null)
			{
				this.update_flag_();
			}
		}
	}
}
