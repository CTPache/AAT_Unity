using System.Collections.Generic;
using UnityEngine;

public interface IRectHolder
{
	IEnumerable<RectTransform> Rects { get; }
}
