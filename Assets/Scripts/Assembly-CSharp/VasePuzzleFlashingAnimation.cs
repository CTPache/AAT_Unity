using UnityEngine;

public class VasePuzzleFlashingAnimation : MonoBehaviour
{
	public MeshRenderer renderer_;

	public float base_color_red = 0.8f;

	public float sin_color_red = 0.2f;

	public float sin_animation_speed = 5f;

	private float anm_timer_;

	private void FixedUpdate()
	{
		Process();
	}

	private void Process()
	{
		if (renderer_ != null)
		{
			anm_timer_ += Time.deltaTime;
			renderer_.material.color = new Color(base_color_red + Mathf.Sin(anm_timer_ * sin_animation_speed) * sin_color_red, 0f, 0f);
		}
	}
}
