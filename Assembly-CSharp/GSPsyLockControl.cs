using UnityEngine;

public class GSPsyLockControl
{
	public enum SET_TYPE
	{
		E_TYPE_FRONT = 0,
		E_TYPE_BACK = 1
	}

	private static GSPsyLockControl instance_;

	private AnimationObject mPsyLockBackPlayer;

	private AnimationObject mPsyLockFrontPlayer;

	public static GSPsyLockControl instance
	{
		get
		{
			if (instance_ == null)
			{
				instance_ = new GSPsyLockControl();
			}
			return instance_;
		}
	}

	private TitleId CurrentTitle
	{
		get
		{
			return GSStatic.global_work_.title;
		}
	}

	public void Load(int _SetType, int _SetNo)
	{
		switch (_SetType)
		{
		case 0:
		{
			int num2 = ((CurrentTitle != TitleId.GS2) ? 235 : 161);
			if (mPsyLockFrontPlayer == null)
			{
				mPsyLockFrontPlayer = AnimationSystem.Instance.PlayObject(0, 0, num2 + _SetNo);
				Vector3 localPosition2 = mPsyLockFrontPlayer.transform.localPosition;
				localPosition2.x = 0f;
				localPosition2.y = 0f;
				mPsyLockFrontPlayer.transform.localPosition = localPosition2;
			}
			break;
		}
		case 1:
		{
			int num = ((CurrentTitle != TitleId.GS2) ? 235 : 161);
			if (mPsyLockBackPlayer == null)
			{
				mPsyLockBackPlayer = AnimationSystem.Instance.PlayObject(0, 0, num + _SetNo);
				mPsyLockBackPlayer.Z = -4f;
				Vector3 localPosition = mPsyLockBackPlayer.transform.localPosition;
				localPosition.x = 0f;
				localPosition.y = 0f;
				mPsyLockBackPlayer.transform.localPosition = localPosition;
			}
			break;
		}
		}
	}

	public void SetChainAnimation(int _SetFronNo, int _SetBackNo)
	{
		Load(0, _SetFronNo);
		Load(1, _SetBackNo);
	}

	public bool IsChainAnimation()
	{
		if (mPsyLockFrontPlayer.IsPlaying && mPsyLockBackPlayer.IsPlaying)
		{
			return true;
		}
		return false;
	}

	public void ChainAnimationDelete()
	{
		if (mPsyLockFrontPlayer == null)
		{
			int num = ((CurrentTitle != TitleId.GS2) ? 235 : 161);
			for (int i = 0; i < 6; i++)
			{
				mPsyLockFrontPlayer = AnimationSystem.Instance.FindObject(0, 0, num + i);
				if (mPsyLockFrontPlayer != null)
				{
					break;
				}
			}
		}
		if (mPsyLockBackPlayer == null)
		{
			int num2 = ((CurrentTitle != TitleId.GS2) ? 235 : 161);
			for (int j = 0; j < 6; j++)
			{
				mPsyLockBackPlayer = AnimationSystem.Instance.FindObject(0, 0, num2 + 6 + j);
				if (mPsyLockBackPlayer != null)
				{
					break;
				}
			}
		}
		if (mPsyLockFrontPlayer != null)
		{
			mPsyLockFrontPlayer.Stop();
			mPsyLockFrontPlayer = null;
		}
		if (mPsyLockBackPlayer != null)
		{
			mPsyLockBackPlayer.Stop();
			mPsyLockBackPlayer = null;
		}
	}

	public void ChainAnimationSetEndFlame()
	{
		mPsyLockFrontPlayer.HoldAndSetAsLastSequence();
		mPsyLockBackPlayer.HoldAndSetAsLastSequence();
	}

	public void SetChainOffsetPos(Vector2 offset)
	{
		Vector3 localPosition = default(Vector3);
		localPosition.x = offset.x;
		localPosition.y = offset.y;
		localPosition.z = 0f;
		mPsyLockFrontPlayer.transform.localPosition = localPosition;
		mPsyLockBackPlayer.transform.localPosition = localPosition;
	}

	public void SetChainMonochrome(bool _b)
	{
	}
}
