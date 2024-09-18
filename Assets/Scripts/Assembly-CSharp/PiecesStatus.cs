using System;

[Serializable]
public class PiecesStatus
{
	public const int RotateMax = 4;

	public const int RotateAngle = 90;

	public bool used;

	public int angle_id;

	public string icon_name { get; private set; }

	public string model_name { get; private set; }

	public float front_rotate_y { get; private set; }

	public float offset_y { get; private set; }

	public PiecesStatus(string in_icon_name, string in_model_name, float in_front_rotate_y, float in_offset_y)
	{
		icon_name = in_icon_name;
		model_name = in_model_name;
		front_rotate_y = in_front_rotate_y;
		offset_y = in_offset_y;
		reset();
	}

	public void reset()
	{
		used = false;
		angle_id = 0;
	}

	public void rotateLeft()
	{
		angle_id++;
		if (4 <= angle_id)
		{
			angle_id = 0;
		}
	}

	public void rotateRight()
	{
		angle_id--;
		if (angle_id < 0)
		{
			angle_id = 3;
		}
	}
}
