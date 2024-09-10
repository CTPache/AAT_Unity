using System;
using UnityEngine;

namespace SaveStruct
{
	[Serializable]
	public struct GSColor
	{
		public byte r;

		public byte g;

		public byte b;

		public byte a;

		public void CopyFrom(Color src)
		{
			Color32 color = src;
			r = color.r;
			g = color.g;
			b = color.b;
			a = color.a;
		}

		public void CopyTo(ref Color dest)
		{
			Color color = new Color32(r, g, b, a);
			dest.r = color.r;
			dest.g = color.g;
			dest.b = color.b;
			dest.a = color.a;
		}
	}
}
