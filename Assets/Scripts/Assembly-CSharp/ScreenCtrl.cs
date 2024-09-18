using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenCtrl : MonoBehaviour
{
	private static ScreenCtrl instance_;

	[SerializeField]
	private List<Transform> quake_targets_ = new List<Transform>();

	[SerializeField]
	private RawImage movie_rendere_;

	private List<Transform> quake_list_ = new List<Transform>();

	private int quake_timer_;

	private uint quake_power_;

	private int quake_x_;

	private int quake_y_;

	private IEnumerator quake_enumerator_;

	public static ScreenCtrl instance
	{
		get
		{
			return instance_;
		}
	}

	public int quake_timer
	{
		get
		{
			return quake_timer_;
		}
		set
		{
			quake_timer_ = value;
		}
	}

	public uint quake_power
	{
		get
		{
			return quake_power_;
		}
		set
		{
			quake_power_ = value;
		}
	}

	public RawImage movie_rendere
	{
		get
		{
			return movie_rendere_;
		}
	}

	private void Awake()
	{
		if (instance_ == null)
		{
			instance_ = this;
		}
	}

	public void init()
	{
		quake_list_.Clear();
		quake_list_.Add(messageBoardCtrl.instance.quake_targets);
		quake_list_.Add(AnimationCameraManager.Instance.quake_targets);
		quake_list_.Add(AnimationCameraManager.Instance.quake_targets_mapicon);
		for (int i = 0; i < quake_targets_.Count; i++)
		{
			quake_list_.Add(quake_targets_[i]);
		}
	}

	public void Quake(int timer, uint power)
	{
		quake_timer_ = timer;
		quake_power_ = power;
		Quake();
	}

	public void Quake()
	{
		stopQuake();
		quake_enumerator_ = quakeCoroutine();
		coroutineCtrl.instance.Play(quake_enumerator_);
	}

	private IEnumerator quakeCoroutine()
	{
		SetQuakePosition(0f, 0f);
		while (true)
		{
			int pow;
			switch (quake_power_)
			{
			case 0u:
				pow = 1;
				break;
			case 1u:
				pow = 3;
				break;
			case 2u:
				pow = 7;
				break;
			default:
				pow = 3;
				break;
			}
			int tmp2 = Random.Range(0, 17);
			if (tmp2 >= 8)
			{
				quake_x_ = tmp2 & pow;
				quake_x_ *= -1;
			}
			else
			{
				quake_x_ = tmp2 & pow;
			}
			tmp2 = Random.Range(0, 17);
			if (tmp2 >= 8)
			{
				quake_y_ = tmp2 & pow;
				quake_y_ *= -1;
			}
			else
			{
				quake_y_ = tmp2 & pow;
			}
			SetQuakePosition(quake_x_ * 6, quake_y_ * 6);
			if (quake_timer != 0)
			{
				quake_timer--;
				if (quake_timer == 0)
				{
					break;
				}
			}
			yield return null;
		}
		SetQuakePosition(0f, 0f);
		quake_enumerator_ = null;
	}

	private void stopQuake()
	{
		if (quake_enumerator_ != null)
		{
			coroutineCtrl.instance.Stop(quake_enumerator_);
			quake_enumerator_ = null;
		}
	}

	private void SetQuakePosition(float x, float y)
	{
		for (int i = 0; i < quake_list_.Count; i++)
		{
			if (quake_list_[i] == AnimationCameraManager.Instance.quake_targets_mapicon)
			{
				if (GSStatic.global_work_.r.no_0 != 4 && GSStatic.global_work_.r.no_0 != 5 && GSStatic.global_work_.r.no_0 != 6 && (GSStatic.global_work_.r.no_0 != 8 || GSStatic.global_work_.r.no_1 == 5 || GSStatic.global_work_.r.no_1 == 8) && GSStatic.global_work_.r.no_0 != 7)
				{
					continue;
				}
				bool flag = false;
				switch (GSStatic.global_work_.title)
				{
				case TitleId.GS1:
					if (GSStatic.global_work_.scenario == 20 && (GSStatic.message_work_.now_no == 153 || GSStatic.message_work_.now_no == 163))
					{
						flag = true;
					}
					break;
				case TitleId.GS2:
					if (GSStatic.global_work_.scenario == 3 && GSStatic.message_work_.now_no == scenario_GS2.SC1_03840)
					{
						flag = true;
					}
					break;
				case TitleId.GS3:
					if ((GSStatic.global_work_.scenario == 10 && GSStatic.message_work_.now_no == scenario_GS3.SC2_3_44430) || (GSStatic.global_work_.scenario == 11 && GSStatic.message_work_.now_no == scenario_GS3.SC2_3_45550) || (GSStatic.global_work_.scenario == 13 && (GSStatic.message_work_.now_no == scenario_GS3.SC3_0_47730B || GSStatic.message_work_.now_no == scenario_GS3.SC3_0_47731)))
					{
						flag = true;
					}
					break;
				}
				if (flag)
				{
					quake_list_[i].localPosition = new Vector3(x - 960f, y + 540f, quake_list_[i].localPosition.z);
				}
				else
				{
					quake_list_[i].localPosition = new Vector3(-960f, 540f, quake_list_[i].localPosition.z);
				}
			}
			else
			{
				quake_list_[i].localPosition = new Vector3(x, y, quake_list_[i].localPosition.z);
			}
		}
	}
}
