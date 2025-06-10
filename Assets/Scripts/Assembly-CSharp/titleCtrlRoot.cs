using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class titleCtrlRoot : MonoBehaviour
{
	public enum SceneType
	{
		Caution = 0,
		Logo = 1,
		Top = 2,
		Title = 3,
		Story = 4,
		Start = 5,
		Adv = 6,
		End = 7,
		Max = 7
	}

	[Serializable]
	public class dataSystem
	{
		public int story_no;

		public int title_no;
	}

	[SerializeField]
public List<sceneCtrl> scene_list_ = new List<sceneCtrl>();

	[SerializeField]
public dataSystem data_system_;

	[SerializeField]
public GameObject root_;

	[SerializeField]
public Camera title_camera_;

	private fadeCtrl fade_ctrl_;

	public static titleCtrlRoot instance { get; private set; }

	public bool is_wait_skip { get; private set; }

	public fadeCtrl fade_ctrl
	{
		get
		{
			return fade_ctrl_;
		}
	}

	public dataSystem data_system
	{
		get
		{
			return data_system_;
		}
	}

	public Camera title_camera
	{
		get
		{
			return title_camera_;
		}
	}

	public bool active
	{
		get
		{
			return root_.activeSelf;
		}
		set
		{
			root_.SetActive(value);
		}
	}

	private void Awake()
	{
		instance = this;
		fade_ctrl_ = fadeCtrl.instance;
	}

	public void Scene(SceneType in_scene_type)
	{
		if ((int)in_scene_type < scene_list_.Count)
		{
			scene_list_[(int)in_scene_type].Play();
		}
	}

	public void End()
	{
		foreach (sceneCtrl item in scene_list_)
		{
			item.End();
		}
		if (soundCtrl.instance != null)
		{
			soundCtrl.instance.end();
		}
	}

	public void End(SceneType in_scene_type)
	{
		if ((int)in_scene_type < scene_list_.Count)
		{
			scene_list_[(int)in_scene_type].End();
		}
	}

	public IEnumerator keyWait(float in_time)
	{
		float time = in_time;
		is_wait_skip = false;
		while (true)
		{
			if (padCtrl.instance.InputGetKeyDown(KeyCode.Return) || padCtrl.instance.InControlInputWasPressedAction1() || IsTouchDown())
			{
				is_wait_skip = true;
				break;
			}
			time -= Time.deltaTime;
			if (time <= 0f)
			{
				break;
			}
			yield return null;
		}
	}

	private bool IsTouchDown()
	{
		return TouchUtility.GetTouch() == TouchInfo.Begin;
	}
}
