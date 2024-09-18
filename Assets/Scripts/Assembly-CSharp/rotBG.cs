using UnityEngine;

public class rotBG : MonoBehaviour
{
	private static rotBG instance_;

	[SerializeField]
	private RectTransform bg_sprite_transform_;

	private bool rotFlg;

	public static rotBG instance
	{
		get
		{
			return instance_;
		}
	}

	private void Awake()
	{
		if (instance_ == null)
		{
			instance_ = this;
		}
	}

	public void Rot_bg_init()
	{
		GSStatic.rot_bg_work_.rot_move_flag = 1;
	}

	public void Rot_bg_end()
	{
		instance.Rot_bg_rotate_set(0);
		GSStatic.rot_bg_work_.rot_move_flag = 0;
	}

	public void Rot_bg_rotate_set(int rotate_now)
	{
		bg_sprite_transform_.eulerAngles = new Vector3(0f, 0f, rotate_now);
	}

	public void Rot_bg_rotate_add_set(float rotate_add)
	{
		GSStatic.rot_bg_work_.rotate_add = rotate_add;
		if (Mathf.FloorToInt(rotate_add) != 0)
		{
			rotFlg = true;
		}
		else
		{
			rotFlg = false;
		}
	}

	public void Rot_bg_main()
	{
		if (GSStatic.rot_bg_work_.rot_move_flag != 0 && GSStatic.global_work_.r.no_0 != 15)
		{
			procRotation();
		}
	}

	private void procRotation()
	{
		if (rotFlg)
		{
			Vector3 localEulerAngles = bg_sprite_transform_.localEulerAngles;
			localEulerAngles.z += GSStatic.rot_bg_work_.rotate_add;
			bg_sprite_transform_.localEulerAngles = localEulerAngles;
		}
	}
}
