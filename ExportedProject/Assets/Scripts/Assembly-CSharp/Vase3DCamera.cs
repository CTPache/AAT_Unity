using UnityEngine;

public class Vase3DCamera : MonoBehaviour
{
	[SerializeField]
	private Camera target_camera_;

	[SerializeField]
	private float adjust_ratio_;

	private int last_screen_width_;

	private int last_screen_height_;

	private void Update()
	{
		if (last_screen_width_ != Screen.width || last_screen_height_ != Screen.height)
		{
			last_screen_width_ = Screen.width;
			last_screen_height_ = Screen.height;
			float num = (float)last_screen_width_ / (float)last_screen_height_ / adjust_ratio_;
			if (num < 1f)
			{
				target_camera_.rect = new Rect(0f, (1f - num) * 0.5f, 1f, num);
				return;
			}
			num = 1f / num;
			target_camera_.rect = new Rect((1f - num) * 0.5f, 0f, num, 1f);
		}
	}
}
