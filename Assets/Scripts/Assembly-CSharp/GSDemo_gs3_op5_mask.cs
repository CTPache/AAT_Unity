using UnityEngine;

public class GSDemo_gs3_op5_mask : MonoBehaviour
{
	[SerializeField]
public AssetBundleSprite[] sprites_;

	public static GSDemo_gs3_op5_mask instance { get; private set; }

	private void Awake()
	{
		instance = this;
	}

	public void Init()
	{
		Vector3 zero = Vector3.zero;
		for (int i = 0; i < 4; i++)
		{
			sprites_[i].load("/GS3/BG/", "spr501_01");
			sprites_[i].spriteNo(i);
			zero.y = 540 - 566 * i;
			sprites_[i].transform.localPosition = zero;
			sprites_[i].obj.SetActive(true);
		}
	}

	public void Exit()
	{
		AssetBundleSprite[] array = sprites_;
		foreach (AssetBundleSprite assetBundleSprite in array)
		{
			assetBundleSprite.end();
			assetBundleSprite.remove();
			assetBundleSprite.sprite_renderer_.sprite = null;
			assetBundleSprite.obj.SetActive(false);
		}
	}

	public void op05_scroll_down(OP05_WK op)
	{
		if (++op.wait < op.speed)
		{
			return;
		}
		op.wait = 0;
		for (ushort num = 0; num < 4; num++)
		{
			Vector3 localPosition = sprites_[num].transform.localPosition;
			localPosition.y += 5.625f;
			if (localPosition.y >= 1106f)
			{
				localPosition.y -= 2264f;
				if (sprites_[num].sprite_no_ == 0 || sprites_[num].sprite_no_ == 1)
				{
					sprites_[num].spriteNo(sprites_[num].sprite_no_ + 4);
				}
			}
			sprites_[num].transform.localPosition = localPosition;
		}
	}
}
