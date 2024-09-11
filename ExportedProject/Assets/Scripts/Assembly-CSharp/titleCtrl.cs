using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class titleCtrl : MonoBehaviour
{
	private static titleCtrl instance_;

	[SerializeField]
	private GameObject body_;

	[SerializeField]
	private Text text_;

	public static titleCtrl instance
	{
		get
		{
			return instance_;
		}
	}

	public Text text
	{
		get
		{
			return text_;
		}
	}

	public bool active
	{
		get
		{
			return body_.activeSelf;
		}
		set
		{
			body_.SetActive(value);
		}
	}

	private void Awake()
	{
		if (instance_ == null)
		{
			instance_ = this;
		}
	}

	public void play()
	{
		coroutineCtrl.instance.Play(stateCoroutine());
	}

	private IEnumerator stateCoroutine()
	{
		active = true;
		mainCtrl.instance.addText(text_);
		yield return coroutineCtrl.instance.Play(fadeCtrl.instance.play_coroutine(30, true, Color.black));
		yield return coroutineCtrl.instance.Play(key_wait());
		yield return coroutineCtrl.instance.Play(doorCtrl.instance.CoroutineClose());
		yield return coroutineCtrl.instance.Play(fadeCtrl.instance.play_coroutine(30, false, Color.black));
		doorCtrl.instance.stop();
		mainCtrl.instance.removeText(text_);
		active = false;
		advCtrl.instance.play(0, 0, 0);
	}

	private IEnumerator key_wait()
	{
		while (!padCtrl.instance.GetKeyDown(KeyType.A))
		{
			if (padCtrl.instance.GetKeyDown(KeyType.R))
			{
				doorCtrl.instance.close();
			}
			if (padCtrl.instance.GetKeyDown(KeyType.B))
			{
				soundCtrl.instance.PlaySE(14);
			}
			if (padCtrl.instance.GetKeyDown(KeyType.X))
			{
				soundCtrl.instance.StopSE(14);
			}
			yield return null;
		}
	}
}
