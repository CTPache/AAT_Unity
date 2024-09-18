using System;
using UnityEngine;

namespace DebugMenu
{
	public class FloatAccessor : NumberAccessor<float>
	{
		public FloatAccessor(Func<float> get, Action<float> set)
			: base(get, set)
		{
		}

		public override float Add(float a, float b)
		{
			return a + b;
		}

		public override float Sub(float a, float b)
		{
			return a - b;
		}

		public override float Clamp(float value, float min, float max)
		{
			return Mathf.Clamp(value, min, max);
		}
	}
}
