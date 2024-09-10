using UnityEngine;
using UnityEngine.UI;

public class BackAnimation : MonoBehaviour
{
	private static BackAnimation instance_;

	[SerializeField]
	private RawImage irregular_image_;

	[SerializeField]
	private Camera canvas_camera_;

	public static BackAnimation instance
	{
		get
		{
			return instance_;
		}
	}

	public RawImage irregular_image
	{
		get
		{
			return irregular_image_;
		}
	}

	public Camera canvas_camera
	{
		get
		{
			return canvas_camera_;
		}
	}

	private void Awake()
	{
		if (instance_ == null)
		{
			instance_ = this;
		}
	}
}
