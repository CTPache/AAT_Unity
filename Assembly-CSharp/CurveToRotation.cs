using System.Collections.Generic;
using UnityEngine;

public class CurveToRotation : MonoBehaviour, IEvaluateReceiver
{
	public float min;

	public float max;

	public Axis axis;

	public List<Transform> targetTransform;

	public float Current { get; private set; }

	private void Awake()
	{
		Current = min;
	}

	public void SetEvaluatedValue(float value)
	{
		Current = min + (max - min) * value;
		foreach (Transform item in targetTransform)
		{
			if (axis == Axis.X)
			{
				item.localRotation = Quaternion.Euler(Current, item.localRotation.eulerAngles.y, item.localRotation.eulerAngles.z);
			}
			if (axis == Axis.Y)
			{
				item.localRotation = Quaternion.Euler(item.localRotation.eulerAngles.x, Current, item.localRotation.eulerAngles.z);
			}
			if (axis == Axis.Z)
			{
				item.localRotation = Quaternion.Euler(item.localRotation.eulerAngles.x, item.localRotation.eulerAngles.y, Current);
			}
		}
	}
}
