using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class MiniGameGSPoint4Hit
{
	public static IEnumerable<GSPoint4> ConvertPoint(IEnumerable<GSPoint4> points, Vector2 offset, Vector2 scale)
	{
		return points.Select((GSPoint4 _) => new GSPoint4((ushort)Mathf.FloorToInt(offset.x + (float)(int)_.x0 * scale.x), (ushort)Mathf.FloorToInt(offset.y + (float)(int)_.y0 * scale.y), (ushort)Mathf.FloorToInt(offset.x + (float)(int)_.x1 * scale.x), (ushort)Mathf.FloorToInt(offset.y + (float)(int)_.y1 * scale.y), (ushort)Mathf.FloorToInt(offset.x + (float)(int)_.x2 * scale.x), (ushort)Mathf.FloorToInt(offset.y + (float)(int)_.y2 * scale.y), (ushort)Mathf.FloorToInt(offset.x + (float)(int)_.x3 * scale.x), (ushort)Mathf.FloorToInt(offset.y + (float)(int)_.y3 * scale.y)));
	}

	public static int CheckHit(GSRect rect, IEnumerable<GSPoint4> points)
	{
		if (rect.x < 0)
		{
			rect.w += rect.x;
			rect.x = 0;
		}
		if (rect.y < 0)
		{
			rect.h += rect.y;
			rect.y = 0;
		}
		int num = 0;
		foreach (GSPoint4 point in points)
		{
			if (GSUtility.ObjHitCheck2(rect, point))
			{
				return num;
			}
			num++;
		}
		return -1;
	}
}
