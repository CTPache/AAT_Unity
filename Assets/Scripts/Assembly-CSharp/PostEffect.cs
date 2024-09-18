using System.Collections;
using UnityEngine;

public class PostEffect : MonoBehaviour
{
	public Material mosaic_;

	private float mosaic_end_;

	private float mosaic_num_;

	private float mosaic_speed_ = 10f;

	public bool is_mosaic_run_;

	public static PostEffect instance { get; private set; }

	private void Awake()
	{
		instance = this;
	}

	private void OnRenderImage(RenderTexture src, RenderTexture dest)
	{
		if (mosaic_ != null)
		{
			Graphics.Blit(src, dest, mosaic_);
		}
	}

	public void SetPostEffect(PostEffectRec rec, int speed)
	{
		mosaic_speed_ = speed;
		if (rec == PostEffectRec.None)
		{
			base.enabled = false;
		}
		if (rec == PostEffectRec.MosaicIN)
		{
			coroutineCtrl.instance.Play(MosaicIn());
			base.enabled = true;
		}
		if (rec == PostEffectRec.MosaicOut)
		{
			coroutineCtrl.instance.Play(MosaicOut());
			base.enabled = true;
		}
	}

	public IEnumerator MosaicOut()
	{
		is_mosaic_run_ = true;
		mosaic_num_ = 20f;
		mosaic_end_ = 350f;
		while (mosaic_num_ < mosaic_end_)
		{
			mosaic_num_ += mosaic_speed_;
			mosaic_.SetFloat("_BlockNum", mosaic_num_);
			yield return null;
		}
		yield return null;
		base.enabled = false;
		is_mosaic_run_ = false;
	}

	public IEnumerator MosaicIn()
	{
		is_mosaic_run_ = true;
		mosaic_num_ = 350f;
		mosaic_end_ = 20f;
		while (mosaic_num_ > mosaic_end_)
		{
			mosaic_num_ -= mosaic_speed_;
			mosaic_.SetFloat("_BlockNum", mosaic_num_);
			yield return null;
		}
		yield return null;
		is_mosaic_run_ = false;
	}
}
