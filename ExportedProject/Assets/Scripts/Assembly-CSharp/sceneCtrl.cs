using System.Collections;
using UnityEngine;

public class sceneCtrl : MonoBehaviour
{
	[SerializeField]
	private GameObject body_;

	private bool is_end_;

	public IEnumerator enumerator_state_;

	public GameObject body
	{
		get
		{
			return body_;
		}
	}

	public bool is_end
	{
		get
		{
			return is_end_;
		}
		set
		{
			is_end_ = value;
		}
	}

	public virtual void Play()
	{
	}

	public virtual void End()
	{
		body_.SetActive(false);
		if (enumerator_state_ != null)
		{
			StopCoroutine(enumerator_state_);
			enumerator_state_ = null;
		}
	}

	protected bool IsTouchDown()
	{
		return TouchUtility.GetTouch() == TouchInfo.Begin;
	}
}
