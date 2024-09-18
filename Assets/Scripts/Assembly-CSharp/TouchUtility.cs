using UnityEngine;

public static class TouchUtility
{
	private static readonly Vector3 ZERO_POSITION = Vector3.zero;

	public static TouchInfo GetTouch()
	{
		if (padCtrl.instance.InputGetMouseButtonDown(0))
		{
			return TouchInfo.Begin;
		}
		if (padCtrl.instance.InputGetMouseButton(0))
		{
			return TouchInfo.Move;
		}
		if (padCtrl.instance.InputGetMouseButtonUp(0))
		{
			return TouchInfo.End;
		}
		return TouchInfo.None;
	}

	public static TouchInfo GetTouchR()
	{
		if (padCtrl.instance.InputGetMouseButtonDown(1))
		{
			return TouchInfo.Begin;
		}
		if (padCtrl.instance.InputGetMouseButton(1))
		{
			return TouchInfo.Move;
		}
		if (padCtrl.instance.InputGetMouseButtonUp(1))
		{
			return TouchInfo.End;
		}
		return TouchInfo.None;
	}

	public static Vector3 GetTouchPosition()
	{
		TouchInfo touch = GetTouch();
		if (touch != TouchInfo.None)
		{
			return padCtrl.instance.InputMousePosition();
		}
		return ZERO_POSITION;
	}

	public static Vector3 GetTouchWorldPosition(Camera camera)
	{
		return camera.ScreenToWorldPoint(GetTouchPosition());
	}
}
