using System;
using UnityEngine;

namespace DebugMenu
{
	public class IntAccessor : NumberAccessor<int>
	{
		public IntAccessor(Func<int> get, Action<int> set)
			: base(get, set)
		{
		}

		public override int Add(int a, int b)
		{
			return a + b;
		}

		public override int Sub(int a, int b)
		{
			return a - b;
		}

		public override int Clamp(int value, int min, int max)
		{
			return Mathf.Clamp(value, min, max);
		}
	}
}
