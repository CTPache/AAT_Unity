using System;

namespace DebugMenu
{
	public abstract class NumberAccessor<T> : VariableAccessor<T>
	{
		public NumberAccessor(Func<T> get, Action<T> set)
			: base(get, set)
		{
		}

		public abstract T Add(T a, T b);

		public abstract T Sub(T a, T b);

		public abstract T Clamp(T value, T min, T max);
	}
}
