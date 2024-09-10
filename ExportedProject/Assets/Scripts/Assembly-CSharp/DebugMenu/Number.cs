using System;

namespace DebugMenu
{
	public class Number<T> : Item where T : IComparable<T>
	{
		private NumberAccessor<T> accessor_;

		private T min_;

		private T max_;

		private T step_;

		private T last_;

		public T number
		{
			get
			{
				return accessor_.value;
			}
			set
			{
				SetNumber(value);
			}
		}

		public T min
		{
			get
			{
				return min_;
			}
		}

		public T max
		{
			get
			{
				return max_;
			}
		}

		public T step
		{
			get
			{
				return step_;
			}
		}

		public event Action update_number
		{
			add
			{
				update_number_ += value;
			}
			remove
			{
				update_number_ -= value;
			}
		}

		private event Action update_number_;

		public Number(string name, NumberAccessor<T> accessor, T min, T max, T step)
			: base(name)
		{
			accessor_ = accessor;
			min_ = min;
			max_ = max;
			step_ = step;
			last_ = accessor_.value;
		}

		public Number<T> AddUpdateNumberEventListener(Action func)
		{
			update_number_ += func;
			return this;
		}

		public void OnAdd()
		{
			SetNumber(accessor_.Add(accessor_.value, step_));
		}

		public void OnSub()
		{
			SetNumber(accessor_.Sub(accessor_.value, step_));
		}

		public override void Update(Menu menu)
		{
			if (((menu.input_down | menu.input_repeat) & Input.Left) != 0)
			{
				OnSub();
			}
			else if (((menu.input_down | menu.input_repeat) & Input.Right) != 0)
			{
				OnAdd();
			}
			else if (last_.CompareTo(accessor_.value) != 0)
			{
				UpdateNumber();
			}
		}

		private void SetNumber(T number)
		{
			number = accessor_.Clamp(number, min_, max_);
			if (accessor_.value.CompareTo(number) != 0)
			{
				accessor_.value = number;
				UpdateNumber();
			}
		}

		private void UpdateNumber()
		{
			last_ = accessor_.value;
			if (this.update_number_ != null)
			{
				this.update_number_();
			}
		}
	}
}
