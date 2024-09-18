using UnityEngine;

public class spefCtrl : MonoBehaviour
{
	private static spefCtrl instance_;

	public static spefCtrl instance
	{
		get
		{
			return instance_;
		}
	}

	public int volumePropetyId { get; private set; }

	private TitleId CurrentTitle
	{
		get
		{
			return GSStatic.global_work_.title;
		}
	}

	private void Awake()
	{
		instance_ = this;
		volumePropetyId = Shader.PropertyToID("_Volume");
	}

	public void Monochrome_set(ushort status, ushort time, ushort speed)
	{
		Play(status, time, speed);
	}

	public void ResetBG()
	{
		bgCtrl.instance.ResetSpef();
		switch (GSStatic.global_work_.SpEf_status)
		{
		case 3:
		case 6:
		case 7:
		case 13:
		case 15:
			GSStatic.global_work_.SpEf_status = 0;
			break;
		}
	}

	private void Play(ushort status, ushort time, ushort speed)
	{
		GSStatic.global_work_.SpEf_status = status;
		switch (status)
		{
		case 3:
		case 7:
			PlayMono(status, time, speed);
			break;
		case 4:
		case 8:
			PlayMonoRet(status, time, speed);
			break;
		case 5:
			PlayRedFadeIn(status, time, speed);
			break;
		case 6:
			PlayRedFadeOut(status, time, speed);
			break;
		case 9:
		case 11:
			PlayMonoObj(status, time, speed);
			break;
		case 10:
		case 12:
			PlayMonoObjRet(status, time, speed);
			break;
		case 13:
		case 15:
		case 23:
			PlaySepia(status, time, speed);
			break;
		case 14:
		case 16:
			PlaySepiaRet(status, time, speed);
			break;
		case 17:
		case 18:
			break;
		case 21:
			PlayMonoChar(status, time, speed);
			break;
		case 22:
			PlayMonoCharRet(status, time, speed);
			break;
		case 19:
		case 20:
			break;
		}
	}

	private void PlayMono(ushort status, ushort time, ushort speed)
	{
		bgCtrl.instance.Bg256_monochrome(time, speed, 0, true);
		if (status == 3)
		{
			AnimationSystem.Instance.Char_monochrome(time, speed, 0, true);
		}
	}

	private void PlayMonoRet(ushort status, ushort time, ushort speed)
	{
		bgCtrl.instance.Bg256_monochrome(time, speed, 0, false);
		if (status == 4)
		{
			AnimationSystem.Instance.Char_monochrome(time, speed, 0, false);
		}
		if (CurrentTitle == TitleId.GS2 && GSStatic.global_work_.scenario == 12)
		{
			AnimationSystem.Instance.OBJ_monochrome2(62u, time, speed, 0, false);
		}
	}

	private void PlayMonoChar(ushort status, ushort time, ushort speed)
	{
		AnimationSystem.Instance.Char_monochrome(time, speed, 0, true);
	}

	private void PlayMonoCharRet(ushort status, ushort time, ushort speed)
	{
		AnimationSystem.Instance.Char_monochrome(time, speed, 0, false);
	}

	private void PlayMonoObj(ushort status, ushort time, ushort speed)
	{
		ushort sw = (ushort)((status == 11) ? 2u : 0u);
		AnimationSystem.Instance.OBJ_monochrome(time, speed, sw, true);
	}

	private void PlayMonoObjRet(ushort status, ushort time, ushort speed)
	{
		ushort sw = (ushort)((status == 12) ? 2u : 0u);
		AnimationSystem.Instance.OBJ_monochrome(time, speed, sw, false);
	}

	private void PlaySepia(ushort status, ushort time, ushort speed)
	{
		bgCtrl.instance.Bg256_monochrome(time, speed, 6, true);
		AnimationSystem.Instance.Char_monochrome(time, speed, 6, true);
	}

	private void PlaySepiaRet(ushort status, ushort time, ushort speed)
	{
		bgCtrl.instance.Bg256_monochrome(time, speed, 6, false);
		AnimationSystem.Instance.Char_monochrome(time, speed, 6, false);
	}

	private void PlayRedFadeIn(ushort status, ushort time, ushort speed)
	{
		bgCtrl.instance.Bg256_monochrome(time, speed, 1, false);
		AnimationSystem.Instance.Char_monochrome(time, speed, 1, false);
	}

	private void PlayRedFadeOut(ushort status, ushort time, ushort speed)
	{
		bgCtrl.instance.Bg256_monochrome(time, speed, 1, true);
		AnimationSystem.Instance.Char_monochrome(time, speed, 1, true);
	}
}
