using System;
using UnityEngine;

[Serializable]
public struct GSColor
{
	public byte r;

	public byte g;

	public byte b;

	public byte a;

	public GSColor(byte r, byte g, byte b, byte a)
	{
		this.r = r;
		this.g = g;
		this.b = b;
		this.a = a;
	}

	public static implicit operator GSColor(Color color)
	{
		Color32 color2 = color;
		return new GSColor(color2.r, color2.b, color2.g, color2.a);
	}

	public static implicit operator Color(GSColor c)
	{
		return new Color32(c.r, c.b, c.g, c.a);
	}

	public void init()
	{
		r = 0;
		g = 0;
		b = 0;
		a = 0;
	}
}
