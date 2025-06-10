using System.Collections.Generic;
using UnityEngine;

public class gameoverCtrl : MonoBehaviour
{
	private static gameoverCtrl instance_;

	[SerializeField]
public List<AssetBundleSprite> sprite_list_;

	[SerializeField]
public GameObject body_;

	[SerializeField]
public GameObject door_;

	private bool is_play_;

	private float move_distance_X = 960f;

	private float move_speed = 80f;

	private float move_count;

	private float time;

	private float move_dis;

	private float spd;

	private int work_state_;

	private int se_count;

	public static gameoverCtrl instance
	{
		get
		{
			return instance_;
		}
	}

	public int work_state
	{
		get
		{
			return work_state_;
		}
	}

	public bool is_play
	{
		get
		{
			return is_play_;
		}
	}

	public AssetBundleSprite sprite_door_L
	{
		get
		{
			return sprite_list_[0];
		}
	}

	public AssetBundleSprite sprite_door_R
	{
		get
		{
			return sprite_list_[1];
		}
	}

	public bool body_active
	{
		get
		{
			return body_.activeSelf;
		}
		set
		{
			body_.SetActive(value);
		}
	}

	public bool door_active
	{
		get
		{
			return door_.activeSelf;
		}
		set
		{
			door_.SetActive(value);
		}
	}

	private void Awake()
	{
		if (instance == null)
		{
			instance_ = this;
		}
	}

	public void load()
	{
		sprite_door_L.load("/GS1/etc/", "etc013");
		sprite_door_R.load("/GS1/etc/", "etc013");
	}

	public void init()
	{
		load();
	}

	public void Play()
	{
		switch (work_state)
		{
		case 0:
			Init_State();
			break;
		case 1:
			Closing_State();
			break;
		case 2:
			ClosedWait_State();
			break;
		case 3:
			ActFade_State();
			break;
		case 4:
			WaitFade_State();
			break;
		case 5:
			Finalize_State();
			break;
		}
	}

	private void Init_State()
	{
		is_play_ = true;
		body_active = true;
		door_active = true;
		move_dis = move_distance_X;
		spd = move_speed;
		time = 0f;
		move_count = 0f;
		se_count = 0;
		work_state_ = 0;
		work_state_++;
	}

	private void Closing_State()
	{
		if (move_count < move_dis)
		{
			sprite_door_L.transform.localPosition += new Vector3(spd, 0f, 0f);
			sprite_door_R.transform.localPosition -= new Vector3(spd, 0f, 0f);
			move_count += spd;
		}
		if (move_count >= move_dis && se_count == 0)
		{
			soundCtrl.instance.PlaySE(86);
			se_count++;
		}
		if (se_count == 1)
		{
			work_state_++;
		}
	}

	private void ClosedWait_State()
	{
		if (time < 120f)
		{
			time += 1f;
			return;
		}
		time = 0f;
		work_state_++;
	}

	private void ActFade_State()
	{
		fadeCtrl.instance.play(30, false);
		work_state_++;
	}

	private void WaitFade_State()
	{
		if (time < 60f)
		{
			time += 1f;
			return;
		}
		time = 0f;
		work_state_++;
	}

	public void Finalize_State()
	{
		sprite_door_L.transform.localPosition = new Vector3(0f - move_dis, 0f, 0f);
		sprite_door_R.transform.localPosition = new Vector3(move_dis, 0f, 0f);
		body_active = false;
		door_active = false;
		is_play_ = false;
		work_state_ = 0;
	}
}
