using System.Collections;
using UnityEngine;

public class vibrationCtrl : MonoBehaviour
{
	private IEnumerator enumerator_play_;

	private bool is_enable_ = true;

	private bool is_play_;

	public static vibrationCtrl instance { get; private set; }

	public bool is_enable
	{
		get
		{
			return is_enable_;
		}
		set
		{
			is_enable_ = value;
		}
	}

	public bool is_play
	{
		get
		{
			return is_play_;
		}
	}

	private void Awake()
	{
		instance = this;
	}

	public void init()
	{
	}

	public void play(int in_time)
	{
		if (is_enable_)
		{
			stop();
			int num = in_time / 2;
			if (num > 0)
			{
				enumerator_play_ = coroutine_play(num);
				StartCoroutine(enumerator_play_);
			}
		}
	}

	public void stop()
	{
		if (enumerator_play_ != null)
		{
			StopCoroutine(enumerator_play_);
		}
		padCtrl.instance.vibration_stop();
		is_play_ = false;
	}

	private IEnumerator coroutine_play(int in_time)
	{
		is_play_ = true;
		padCtrl.instance.vibration_play();
		while (in_time > 0)
		{
			in_time--;
			yield return null;
		}
		padCtrl.instance.vibration_stop();
		is_play_ = false;
	}
}
