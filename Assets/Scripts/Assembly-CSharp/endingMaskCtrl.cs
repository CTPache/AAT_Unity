using UnityEngine;
using UnityEngine.UI;

public class endingMaskCtrl : MonoBehaviour
{
	private static endingMaskCtrl instance_;

	[SerializeField]
public RawImage image_;

	[SerializeField]
public Material default_;

	[SerializeField]
public Material mask_;

	public static endingMaskCtrl instance
	{
		get
		{
			return instance_;
		}
	}

	private void Awake()
	{
		instance_ = this;
	}

	public void SetAlpha(float alpha)
	{
		Color color = image_.color;
		color.a = alpha;
		image_.color = color;
	}

	public void SetMask()
	{
		image_.material = mask_;
		SetAlpha(0f);
	}

	public void ResetMask()
	{
		image_.material = default_;
		SetAlpha(1f);
	}
}
