using System;
using System.Collections;
using UnityEngine;

public class luminolBloodstain : MonoBehaviour
{
	private enum CursolPos
	{
		LeftTop = 0,
		RightTop = 1,
		LeftBottom = 2,
		RightBottom = 3,
		Center = 4,
		SpriteNum = 5
	}

	public int discovery_count_;

	public SpriteRenderer sprite_;

	public SpriteRenderer sprite_pink_;

	public BloodstainState state_;

	public GameObject body_;

	public GameObject blood_obj_;

	public GameObject cursor_obj_;

	[SerializeField]
	private AssetBundleSprite[] cursor_;

	[SerializeField]
	private Transform cursor_root_;

	public Evaluator evaluator_;

	public Action anim_end_call_back_;

	[SerializeField]
	private InputTouch cursor_touch_;

	private IEnumerator enumerator_;

	public void Init(BloodstainState state, Action anim_end_call_back, string name)
	{
		AssetBundleCtrl instance = AssetBundleCtrl.instance;
		AssetBundle assetBundle = instance.load("/GS1/minigame/", name);
		cursor_[0].load("/GS1/minigame/", "s2d00e");
		cursor_[1].load("/GS1/minigame/", "s2d00e");
		cursor_[2].load("/GS1/minigame/", "s2d00e");
		cursor_[3].load("/GS1/minigame/", "s2d00e");
		cursor_[4].load("/GS1/minigame/", "s2d00f");
		sprite_pink_.sprite = assetBundle.LoadAllAssets<Sprite>()[1];
		sprite_.sprite = assetBundle.LoadAllAssets<Sprite>()[0];
		state_ = state;
		anim_end_call_back_ = anim_end_call_back;
		blood_obj_.SetActive(false);
		cursor_obj_.SetActive(false);
		cursor_touch_.gameObject.SetActive(false);
		cursor_touch_.ActiveCollider();
		cursor_root_.localPosition = -base.transform.localPosition;
		AssetBundleSprite[] array = cursor_;
		foreach (AssetBundleSprite assetBundleSprite in array)
		{
			assetBundleSprite.obj.SetActive(false);
		}
		discovery_count_ = 3;
		if (state_ == BloodstainState.Acquired)
		{
			body_.SetActive(true);
		}
		cursor_touch_.touch_key_type = KeyType.A;
	}

	public void AppearBlood()
	{
		if (state_ == BloodstainState.Undiscovered)
		{
			body_.SetActive(true);
			blood_obj_.SetActive(true);
			discovery_count_--;
			if (discovery_count_ <= 0)
			{
				TouchSystem.TouchInActive();
				state_ = BloodstainState.Discovery;
				coroutineCtrl.instance.Play(WaitAnum());
			}
		}
	}

	private IEnumerator WaitAnum()
	{
		coroutineCtrl.instance.Play(keyGuideCtrl.instance.close());
		evaluator_.Initialize();
		evaluator_.enabled = true;
		int timer3 = 20;
		while (timer3 > 0)
		{
			timer3--;
			yield return null;
		}
		AssetBundleSprite[] array = cursor_;
		foreach (AssetBundleSprite assetBundleSprite in array)
		{
			assetBundleSprite.obj.SetActive(true);
		}
		timer3 = 50;
		Vector3 target = base.transform.localPosition;
		Vector3 distance = target - new Vector3(960f, -540f, 0f);
		while (timer3 > 0)
		{
			timer3--;
			cursor_[4].transform.localPosition = target - distance * timer3 / 60f;
			LuminolCursorSetRect((float)timer3 / 60f);
			yield return null;
		}
		cursor_touch_.gameObject.SetActive(true);
		enumerator_ = FlashCursor();
		coroutineCtrl.instance.Play(enumerator_);
		timer3 = 40;
		while (timer3 > 0)
		{
			timer3--;
			yield return null;
		}
		state_ = BloodstainState.Discovered;
		cursor_obj_.SetActive(true);
		cursor_touch_.ActiveCollider();
		if (anim_end_call_back_ != null)
		{
			anim_end_call_back_();
		}
	}

	private void LuminolCursorSetRect(float rate)
	{
		Vector3 localPosition = cursor_[4].transform.localPosition;
		float num = 896f * rate;
		float num2 = 476f * rate;
		Vector3 localPosition2 = cursor_[0].transform.localPosition;
		localPosition2.x = localPosition.x - num - 64f;
		localPosition2.y = localPosition.y + num2 + 64f;
		cursor_[0].transform.localPosition = localPosition2;
		localPosition2 = cursor_[1].transform.localPosition;
		localPosition2.x = localPosition.x + num + 64f;
		localPosition2.y = localPosition.y + num2 + 64f;
		cursor_[1].transform.localPosition = localPosition2;
		localPosition2 = cursor_[2].transform.localPosition;
		localPosition2.x = localPosition.x - num - 64f;
		localPosition2.y = localPosition.y - num2 - 64f;
		cursor_[2].transform.localPosition = localPosition2;
		localPosition2 = cursor_[3].transform.localPosition;
		localPosition2.x = localPosition.x + num + 64f;
		localPosition2.y = localPosition.y - num2 - 64f;
		cursor_[3].transform.localPosition = localPosition2;
	}

	public void StopFlashCursor()
	{
		if (enumerator_ != null)
		{
			coroutineCtrl.instance.Stop(enumerator_);
		}
		cursor_root_.gameObject.SetActive(true);
	}

	private IEnumerator FlashCursor()
	{
		int timer = 0;
		while (true)
		{
			timer = (timer + 1) % 16;
			if (timer > 3)
			{
				cursor_root_.gameObject.SetActive(true);
			}
			else
			{
				cursor_root_.gameObject.SetActive(false);
			}
			yield return null;
		}
	}
}
