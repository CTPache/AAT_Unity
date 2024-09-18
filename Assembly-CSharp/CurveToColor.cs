using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurveToColor : MonoBehaviour, IEvaluateReceiver
{
	public Color min;

	public Color max;

	public List<Image> targetImages;

	public List<RawImage> targetRawImages;

	public List<SpriteRenderer> targetSpriteRenderers;

	public Color Current { get; private set; }

	private void Awake()
	{
		Current = min;
	}

	public void SetEvaluatedValue(float value)
	{
		Current = min + (max - min) * value;
		foreach (Image targetImage in targetImages)
		{
			targetImage.color = Current;
		}
		foreach (RawImage targetRawImage in targetRawImages)
		{
			targetRawImage.color = Current;
		}
		foreach (SpriteRenderer targetSpriteRenderer in targetSpriteRenderers)
		{
			targetSpriteRenderer.color = Current;
		}
	}
}
