using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DebugMiniGameGSPoint4Hit
{
	private Transform parent_;

	private GameObject debug_area_root_;

	private LineRenderer[] debug_area_;

	public void SetParent(Transform parent)
	{
		parent_ = parent;
	}

	public void DebugShowArea(bool show, GSRect rect, IEnumerable<GSPoint4> points)
	{
		if (show)
		{
			if (debug_area_root_ == null)
			{
				debug_area_root_ = new GameObject("DebugAreaRoot");
				debug_area_root_.layer = parent_.gameObject.layer;
				Transform transform = debug_area_root_.transform;
				transform.SetParent(parent_, false);
				transform.localPosition = new Vector3(0f, 0f, -10f);
				debug_area_ = new LineRenderer[points.Count()];
				int num = 0;
				foreach (GSPoint4 point in points)
				{
					GameObject gameObject = new GameObject("Area" + num);
					gameObject.layer = debug_area_root_.layer;
					Transform transform2 = gameObject.transform;
					transform2.SetParent(transform, false);
					LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
					lineRenderer.useWorldSpace = false;
					lineRenderer.positionCount = 4;
					lineRenderer.SetPositions(new Vector3[4]
					{
						new Vector3((int)point.x0, -point.y0, 0f),
						new Vector3((int)point.x1, -point.y1, 0f),
						new Vector3((int)point.x2, -point.y2, 0f),
						new Vector3((int)point.x3, -point.y3, 0f)
					});
					lineRenderer.loop = true;
					Color endColor = (lineRenderer.startColor = Color.green);
					lineRenderer.endColor = endColor;
					lineRenderer.widthMultiplier = 4f;
					lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
					debug_area_[num] = lineRenderer;
					num++;
				}
			}
			int num2 = MiniGameGSPoint4Hit.CheckHit(rect, points);
			for (int i = 0; i < debug_area_.Length; i++)
			{
				Color color = ((i != num2) ? Color.green : Color.red);
				debug_area_[i].startColor = color;
				debug_area_[i].endColor = color;
			}
		}
		else if (debug_area_root_ != null)
		{
			debug_area_ = null;
			Object.Destroy(debug_area_root_);
			debug_area_root_ = null;
		}
	}
}
