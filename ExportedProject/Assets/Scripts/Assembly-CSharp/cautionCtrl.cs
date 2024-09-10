using System.Collections;
using UnityEngine;

public class cautionCtrl : sceneCtrl
{
	[SerializeField]
	private AssetBundleSprite caution_;

	private void Init()
	{
		caution_.load("/menu/title/", "caution");
		caution_.spriteNo(0);
	}

	public override void Play()
	{
		End();
		enumerator_state_ = stateCoroutine();
		StartCoroutine(enumerator_state_);
	}

	private IEnumerator stateCoroutine()
	{
		base.body.SetActive(true);
		Init();
		yield return fadeCtrl.instance.play(0.5f, true);
		yield return titleCtrlRoot.instance.keyWait(2.5f);
		yield return fadeCtrl.instance.play(0.5f, false);
		base.body.SetActive(false);
		titleCtrlRoot.instance.Scene(titleCtrlRoot.SceneType.Logo);
	}
}
