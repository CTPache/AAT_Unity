using System;

namespace DebugMenu
{
	public class BoolAccessor : VariableAccessor<bool>
	{
		public BoolAccessor(Func<bool> get, Action<bool> set)
			: base(get, set)
		{
		}

		public bool Flip(bool flag)
		{
			return !flag;
		}
	}
}
