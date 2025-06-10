using System;
using System.Collections.Generic;
using System.Linq;
using TextEffect;
using UnityEngine;

public class systemCtrl : MonoBehaviour
{
	[Serializable]
	public class ScreenData
	{
		public int width;

		public int height;

		public ScreenData(int in_width, int in_height)
		{
			width = in_width;
			height = in_height;
		}
	}

	private static systemCtrl instance_;

	[SerializeField]
public Camera system_camera_;

	[SerializeField]
public Camera front_camera_;

	[SerializeField]
public SpriteRenderer front_image_;

	private List<DoubleQuotationAdjustment> doublequotation_adjustment_list_ = new List<DoubleQuotationAdjustment>();

	private int ScreenWidth_ = 1920;

	private int ScreenHeight_ = 1080;

	private float key_wait_ = 10f;

	private int save_ver_ = 4097;

	private List<ScreenData> screen_list_ = new List<ScreenData>();

	private List<ScreenData> old_screen_list_ = new List<ScreenData>();

	public static systemCtrl instance
	{
		get
		{
			return instance_;
		}
	}

	public int ScreenWidth
	{
		get
		{
			return ScreenWidth_;
		}
	}

	public int ScreenHeight
	{
		get
		{
			return ScreenHeight_;
		}
	}

	public float key_wait
	{
		get
		{
			return key_wait_;
		}
	}

	public int save_ver
	{
		get
		{
			return save_ver_;
		}
	}

	public Camera system_camera
	{
		get
		{
			return system_camera_;
		}
	}

	public Camera front_camera
	{
		get
		{
			return front_camera_;
		}
	}

	public SpriteRenderer front_image
	{
		get
		{
			return front_image_;
		}
	}

	public List<ScreenData> screen_list
	{
		get
		{
			return screen_list_;
		}
	}

	public List<ScreenData> old_screen_list
	{
		get
		{
			return old_screen_list_;
		}
	}

	public WaitForEndOfFrame wait_time
	{
		get
		{
			return null;
		}
	}

	public void EnableDoubleQuotationAdjustoment(bool enable)
	{
		foreach (DoubleQuotationAdjustment item in doublequotation_adjustment_list_)
		{
			item.enabled = enable;
		}
	}

	public Vector3 ScreenToViewportPoint(Vector3 touch_pos)
	{
		return system_camera_.ScreenToViewportPoint(touch_pos);
	}

	private void Awake()
	{
		if (instance_ == null)
		{
			instance_ = this;
		}
		Time.fixedDeltaTime = 1f / 60f;
		Application.targetFrameRate = 60;
		InitScreen();
		GSStatic.option_work.resolution_w = (uint)screen_list_[0].width;
		GSStatic.option_work.resolution_h = (uint)screen_list_[0].height;
		ScreenUtility.SetVsync(GSStatic.option_work.vsync);
		ScreenUtility.SetResolution((int)GSStatic.option_work.resolution_w, (int)GSStatic.option_work.resolution_h, (GSStatic.option_work.window_mode != 0) ? true : false);
		SteamCtrl.Init();
	}

	private void Start()
	{
		doublequotation_adjustment_list_ = (Resources.FindObjectsOfTypeAll(typeof(DoubleQuotationAdjustment)) as DoubleQuotationAdjustment[]).Where((DoubleQuotationAdjustment c) => c.hideFlags == HideFlags.None).ToList();
		EnableDoubleQuotationAdjustoment(GSStatic.save_slot_language_ == "JAPAN");
	}

	private void Update()
	{
	}

	private void FixedUpdate()
	{
		Process();
	}

	private void Process()
	{
	}

	public void InitScreen()
	{
		screen_list_.Clear();
		Resolution[] resolutions = Screen.resolutions;
		for (int i = 0; i < resolutions.Length; i++)
		{
			Resolution resolution = resolutions[i];
			int num = resolution.width / 16 * 9;
			if (resolution.height != num)
			{
				continue;
			}
			bool flag = true;
			foreach (ScreenData item in screen_list_)
			{
				if (item.width == resolution.width && item.height == resolution.height)
				{
					flag = false;
				}
			}
			if (flag)
			{
				screen_list_.Add(new ScreenData(resolution.width, resolution.height));
			}
		}
		if (screen_list_.Count == 0)
		{
			screen_list_.Add(new ScreenData(1280, 720));
		}
	}

	public void UpdateOldScreenList()
	{
		old_screen_list_.Clear();
		for (int i = 0; i < screen_list_.Count; i++)
		{
			old_screen_list_.Add(new ScreenData(screen_list_[i].width, screen_list_[i].height));
		}
	}

	public void RevertScreenList()
	{
		screen_list_.Clear();
		for (int i = 0; i < old_screen_list_.Count; i++)
		{
			screen_list_.Add(new ScreenData(old_screen_list_[i].width, old_screen_list_[i].height));
		}
	}
}
