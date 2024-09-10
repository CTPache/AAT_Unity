using System.Collections.Generic;
using UnityEngine;

public class AnimationSprite : MonoBehaviour
{
	public SpriteRenderer sprite_renderer_;

	public float changeFrameSecond;

	public List<Sprite> sprite_data_ = new List<Sprite>();

	private int firstFrameNum;

	private float dTime;

	public bool active
	{
		get
		{
			return sprite_renderer_.enabled;
		}
		set
		{
			sprite_renderer_.enabled = value;
		}
	}

	private void Start()
	{
		firstFrameNum = 1;
		dTime = 0f;
		if (sprite_data_ != null && firstFrameNum < sprite_data_.Count)
		{
			sprite_renderer_.sprite = sprite_data_[firstFrameNum];
		}
	}

	private void Update()
	{
		dTime += Time.deltaTime;
		if (changeFrameSecond < dTime)
		{
			dTime = 0f;
			firstFrameNum++;
			if (firstFrameNum >= sprite_data_.Count)
			{
				firstFrameNum = 0;
			}
			sprite_renderer_.sprite = sprite_data_[firstFrameNum];
		}
	}
}
