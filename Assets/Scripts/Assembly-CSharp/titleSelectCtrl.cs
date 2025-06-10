using UnityEngine;

public class titleSelectCtrl : MonoBehaviour
{
	private static titleSelectCtrl instance_;

	[SerializeField]
public GameObject body_;

	public static titleSelectCtrl instance
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
