using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fadeCtrl : MonoBehaviour
{
	public enum Status
	{
		NO_FADE = 0,
		FADE_IN = 1,
		FADE_OUT = 2,
		WFADE_IN = 3,
		WFADE_OUT = 4,
		WFADE_OUT_ONE = 5,
		WFADE_IN2 = 6,
		WFADE_DUMMY = 7
	}

	public enum State
	{
		None = 0,
		ExecOut = 1,
		Out = 2,
		ExecIn = 3
	}

	private static fadeCtrl instance_;

	[SerializeField]
	private List<SpriteRenderer> target_list_ = new List<SpriteRenderer>();

	private State[] states = new State[3];

	private List<SpriteRenderer> fade_target_ = new List<SpriteRenderer>();

	private IEnumerator[] enumerator_ = new IEnumerator[3];

	private IEnumerator bldy_enumerator_;

	private IEnumerator flash_timer_enumerator_;

	private IEnumerator dummy_flash_enumerator_;

	private int pReg_BLDY = 16;

	private bool is_end_ = true;

	public static fadeCtrl instance
	{
		get
		{
			return instance_;
		}
	}

	public List<SpriteRenderer> target_list
	{
		get
		{
			return target_list_;
		}
	}

	public List<SpriteRenderer> fade_target
	{
		get
		{
			return fade_target_;
		}
	}

	public bool is_end
	{
		get
		{
			return is_end_;
		}
	}

	public Status status { get; set; }

	private void Awake()
	{
		if (instance_ == null)
		{
			instance_ = this;
		}
	}

	public bool IsOut(int target)
	{
		return states[target] == State.Out;
	}

	public bool IsIn(int target)
	{
		return states[target] == State.None;
	}

	public bool IsExecOut(int target)
	{
		return states[target] == State.ExecOut;
	}

	public bool IsExecIn(int target)
	{
		return states[target] == State.ExecIn;
	}

	public IEnumerator play_coroutine(int in_time, bool in_type, Color in_color)
	{
		is_end_ = false;
		SpriteRenderer sprite = fade_target_[0];
		states[0] = ((!in_type) ? State.ExecOut : State.ExecIn);
		float alpha = ((!in_type) ? 0f : 1f);
		float rate = 1f / (float)in_time * ((!in_type) ? 1f : (-1f));
		sprite.color = new Color(in_color.r, in_color.g, in_color.b, alpha);
		yield return null;
		while (true)
		{
			alpha += rate;
			sprite.color = new Color(in_color.r, in_color.g, in_color.b, alpha);
			in_time--;
			if (in_time < 0)
			{
				break;
			}
			yield return null;
		}
		states[0] = ((!in_type) ? State.Out : State.None);
		is_end_ = true;
		status = Status.NO_FADE;
	}

	public void play(int in_time, bool in_type)
	{
		coroutineCtrl.instance.Play(play_coroutine(in_time, in_type, Color.black));
	}

	public IEnumerator play(float in_time, bool in_type)
	{
		float init_alpha = ((!in_type) ? 0f : 1f);
		float rate = ((!in_type) ? 1f : (-1f));
		float time = 0f;
		float alpha2 = init_alpha;
		SpriteRenderer sprite = fade_target_[0];
		states[0] = ((!in_type) ? State.ExecOut : State.ExecIn);
		while (true)
		{
			alpha2 = init_alpha + time / in_time * rate;
			sprite.color = new Color(0f, 0f, 0f, alpha2);
			time += Time.deltaTime;
			if (in_time <= time)
			{
				break;
			}
			yield return null;
		}
		sprite.color = new Color(0f, 0f, 0f, (!in_type) ? 1f : 0f);
		states[0] = ((!in_type) ? State.Out : State.None);
	}

	public void play(Status in_status, uint in_time, uint in_speed)
	{
		play((uint)in_status, in_time, in_speed);
	}

	public void play(uint in_status, uint in_time, uint in_speed)
	{
		play(in_status, in_time, in_speed, 31u);
	}

	public void play(uint in_status, uint in_time, uint in_speed, uint target)
	{
		if (in_status == 0)
		{
			return;
		}
		bool flag = target == 31 && in_status == 3 && in_speed == 8 && in_time == 1;
		if (flag)
		{
			if (flash_timer_enumerator_ != null)
			{
				if (dummy_flash_enumerator_ == null)
				{
					status = Status.WFADE_DUMMY;
					dummy_flash_enumerator_ = DummyFlashCoroutine();
					coroutineCtrl.instance.Play(dummy_flash_enumerator_);
				}
				return;
			}
			flash_timer_enumerator_ = FlashTimerCoroutine();
			coroutineCtrl.instance.Play(flash_timer_enumerator_);
		}
		int num;
		switch (in_status)
		{
		case 1u:
			num = 1;
			break;
		case 2u:
			num = 0;
			break;
		case 3u:
			num = 1;
			break;
		case 4u:
			num = 0;
			break;
		case 5u:
			num = 0;
			break;
		case 6u:
			num = 1;
			break;
		default:
			num = 0;
			break;
		}
		bool flag2 = (byte)num != 0;
		Color color;
		switch (in_status)
		{
		case 1u:
			color = Color.black;
			break;
		case 2u:
			color = Color.black;
			break;
		case 3u:
			color = Color.white;
			break;
		case 4u:
			color = Color.white;
			break;
		case 5u:
			color = Color.white;
			break;
		case 6u:
			color = Color.white;
			break;
		default:
			color = Color.white;
			break;
		}
		Color in_color = color;
		if (flag)
		{
			in_color = new Color(0.6f, 0.6f, 0.6f);
		}
		if (in_status != 1 || pReg_BLDY > 0 || in_time < 2)
		{
			status = (Status)in_status;
			if ((target & 2u) != 0)
			{
				SetFade(0, in_time, in_speed, flag2, in_color, false);
			}
			if ((target & 0x10u) != 0)
			{
				SetFade(1, in_time, in_speed, flag2, in_color, (target & 2) != 0);
			}
			if ((target & 8u) != 0)
			{
				SetFade(2, in_time, in_speed, flag2, in_color, (target & 0x12) != 0);
			}
			if (bldy_enumerator_ != null)
			{
				coroutineCtrl.instance.Stop(bldy_enumerator_);
				bldy_enumerator_ = null;
			}
			bldy_enumerator_ = ((!flag2) ? BLDYFadeoutCoroutine(in_time, (int)in_speed) : BLDYFadeinCoroutine(in_time, (int)in_speed));
			coroutineCtrl.instance.Play(bldy_enumerator_);
		}
	}

	public void SetFade(int in_index, uint in_time, uint in_speed, bool in_type, Color in_color, bool under_other_fade)
	{
		if (in_index < fade_target_.Count)
		{
			if (enumerator_[in_index] != null)
			{
				coroutineCtrl.instance.Stop(enumerator_[in_index]);
				enumerator_[in_index] = null;
			}
			enumerator_[in_index] = FadeCoroutine(in_index, in_time, in_speed, in_type, in_color, under_other_fade);
			coroutineCtrl.instance.Play(enumerator_[in_index]);
		}
	}

	private IEnumerator FadeCoroutine(int in_index, uint in_time, uint in_speed, bool in_type, Color in_color, bool under_other_fade)
	{
		is_end_ = false;
		SpriteRenderer target = fade_target_[in_index];
		states[in_index] = ((!in_type) ? State.ExecOut : State.ExecIn);
		float start_alpha = ((!in_type) ? 0f : 1f);
		float end_alpha = 1f - start_alpha;
		if (in_speed != 0)
		{
			if (under_other_fade)
			{
				in_color.a = 0f;
				target.color = in_color;
			}
			int frame = (int)(in_time * 16) / (int)in_speed;
			for (int timer = 0; timer < frame; timer++)
			{
				if (!under_other_fade)
				{
					in_color.a = start_alpha + (end_alpha - start_alpha) * ((float)timer / (float)frame);
					target.color = in_color;
				}
				yield return null;
			}
		}
		in_color.a = end_alpha;
		target.color = in_color;
		yield return null;
		states[in_index] = ((!in_type) ? State.Out : State.None);
		is_end_ = true;
		status = Status.NO_FADE;
		enumerator_[in_index] = null;
	}

	private IEnumerator BLDYFadeinCoroutine(uint in_time, int in_speed)
	{
		int timer = 0;
		while (pReg_BLDY > 0 && in_speed > 0)
		{
			timer++;
			if (timer >= in_time)
			{
				timer = 0;
				pReg_BLDY -= in_speed;
			}
			yield return null;
		}
		pReg_BLDY = 0;
		bldy_enumerator_ = null;
	}

	private IEnumerator BLDYFadeoutCoroutine(uint in_time, int in_speed)
	{
		int timer = 0;
		while (pReg_BLDY < 16 && in_speed > 0)
		{
			timer++;
			if (timer >= in_time)
			{
				timer = 0;
				pReg_BLDY += in_speed;
			}
			yield return null;
		}
		pReg_BLDY = 16;
		bldy_enumerator_ = null;
	}

	private IEnumerator FlashTimerCoroutine()
	{
		for (int i = 0; i < 60; i++)
		{
			yield return null;
		}
		flash_timer_enumerator_ = null;
	}

	private IEnumerator DummyFlashCoroutine()
	{
		is_end_ = false;
		for (int timer = 0; timer <= 1; timer++)
		{
			yield return null;
		}
		is_end_ = true;
		status = Status.NO_FADE;
		dummy_flash_enumerator_ = null;
	}
}
