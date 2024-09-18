using UnityEngine;

public class storySelectCtrl : MonoBehaviour
{
	private static storySelectCtrl instance_;

	[SerializeField]
	private GameObject body_;

	public static storySelectCtrl instance
	{
		get
		{
			return instance_;
		}
	}

	public GameObject body
	{
		get
		{
			return body_;
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
