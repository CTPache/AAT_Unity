using System;

namespace DebugMenu
{
	public class VariableAccessor<T>
	{
		private Func<T> get_;

		private Action<T> set_;

		public T value
		{
			get
			{
				return get_();
			}
			set
			{
				set_(value);
			}
		}

		public VariableAccessor(Func<T> get, Action<T> set)
		{
			get_ = get;
			set_ = set;
		}
	}
}
