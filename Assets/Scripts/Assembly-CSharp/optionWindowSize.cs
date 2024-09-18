using UnityEngine;

public class optionWindowSize : optionSelectItem
{
	public override void Init()
	{
		SelectTextInit(true);
		SetText(TextDataCtrl.GetText(TextDataCtrl.OptionTextID.ITEM_RESOLUTION));
		base.Init();
		old_value_ = setting_value_;
	}

	private void SelectTextInit(bool _is_init = false)
	{
		systemCtrl.instance.InitScreen();
		select_text_ = new string[systemCtrl.instance.screen_list.Count];
		setting_value_ = 0;
		for (int i = 0; i < systemCtrl.instance.screen_list.Count; i++)
		{
			select_text_[i] = systemCtrl.instance.screen_list[i].width + "☓" + systemCtrl.instance.screen_list[i].height;
			if (GSStatic.option_work.resolution_w == (uint)systemCtrl.instance.screen_list[i].width && GSStatic.option_work.resolution_h == (uint)systemCtrl.instance.screen_list[i].height)
			{
				setting_value_ = i;
			}
		}
		if (_is_init && (GSStatic.option_work.resolution_w != (uint)systemCtrl.instance.screen_list[setting_value_].width || GSStatic.option_work.resolution_h != (uint)systemCtrl.instance.screen_list[setting_value_].height))
		{
			select_text_ = new string[systemCtrl.instance.old_screen_list.Count];
			for (int j = 0; j < systemCtrl.instance.old_screen_list.Count; j++)
			{
				select_text_[j] = systemCtrl.instance.old_screen_list[j].width + "☓" + systemCtrl.instance.old_screen_list[j].height;
				if (GSStatic.option_work.resolution_w == (uint)systemCtrl.instance.old_screen_list[j].width && GSStatic.option_work.resolution_h == (uint)systemCtrl.instance.old_screen_list[j].height)
				{
					setting_value_ = j;
				}
			}
		}
		max_value_size_ = systemCtrl.instance.screen_list.Count;
	}

	public override void InitValueSet()
	{
		if (GSStatic.option_work.resolution_w != 0 && GSStatic.option_work.resolution_h != 0)
		{
			ScreenUtility.SetResolution((int)GSStatic.option_work.resolution_w, (int)GSStatic.option_work.resolution_h, (GSStatic.option_work.window_mode != 0) ? true : false);
		}
		else
		{
			if (setting_value_ < systemCtrl.instance.screen_list.Count)
			{
				GSStatic.option_work.resolution_w = (uint)systemCtrl.instance.screen_list[setting_value_].width;
				GSStatic.option_work.resolution_h = (uint)systemCtrl.instance.screen_list[setting_value_].height;
			}
			ScreenUtility.SetResolution((int)GSStatic.option_work.resolution_w, (int)GSStatic.option_work.resolution_h, (GSStatic.option_work.window_mode != 0) ? true : false);
		}
		systemCtrl.instance.UpdateOldScreenList();
	}

	public override void EnterValue()
	{
	}

	public override void DefaultValueSet()
	{
		setting_value_ = 0;
		systemCtrl.instance.InitScreen();
		if (setting_value_ < systemCtrl.instance.screen_list.Count)
		{
			GSStatic.option_work.resolution_w = (uint)systemCtrl.instance.screen_list[setting_value_].width;
			GSStatic.option_work.resolution_h = (uint)systemCtrl.instance.screen_list[setting_value_].height;
		}
		ScreenUtility.SetResolution((int)GSStatic.option_work.resolution_w, (int)GSStatic.option_work.resolution_h, (GSStatic.option_work.window_mode != 0) ? true : false);
		base.DefaultValueSet();
	}

	public override bool ConfirmChange()
	{
		for (int i = 0; i < systemCtrl.instance.screen_list.Count; i++)
		{
			if (GSStatic.option_work.resolution_w == (uint)systemCtrl.instance.screen_list[i].width && GSStatic.option_work.resolution_h == (uint)systemCtrl.instance.screen_list[i].height && old_value_ != i)
			{
				return true;
			}
		}
		return false;
	}

	public override bool SelectDecision()
	{
		if (setting_value_ < systemCtrl.instance.screen_list.Count)
		{
			GSStatic.option_work.resolution_w = (uint)systemCtrl.instance.screen_list[setting_value_].width;
			GSStatic.option_work.resolution_h = (uint)systemCtrl.instance.screen_list[setting_value_].height;
		}
		ScreenUtility.SetResolution((int)GSStatic.option_work.resolution_w, (int)GSStatic.option_work.resolution_h, (GSStatic.option_work.window_mode != 0) ? true : false);
		return false;
	}

	public override void ChangeValue(int val)
	{
		int before_value = setting_value_;
		int before_max_value_size = max_value_size_;
		SelectTextInit(false);
		base.ChangeValue(val);
		if (CheckDifferenceScreenList())
		{
			CheckScreenSize(before_value, before_max_value_size, val);
		}
		else
		{
			SelectDecision();
		}
		systemCtrl.instance.UpdateOldScreenList();
	}

	public override void OnTouch(TouchParameter touch_param)
	{
		int before_value = setting_value_;
		int before_max_value_size = max_value_size_;
		SelectTextInit(false);
		base.OnTouch(touch_param);
		optionInfo optionInfo2 = touch_param.argument_parameter as optionInfo;
		int num = optionInfo2.index_;
		if (num == 3)
		{
			RevertSetting();
			return;
		}
		if (CheckDifferenceScreenList())
		{
			switch (num)
			{
			case 1:
				num = 1;
				break;
			case 2:
				num = -1;
				break;
			}
			CheckScreenSize(before_value, before_max_value_size, num);
		}
		else
		{
			SelectDecision();
		}
		systemCtrl.instance.UpdateOldScreenList();
	}

	private void RevertSetting()
	{
		systemCtrl.instance.RevertScreenList();
		select_text_ = new string[systemCtrl.instance.screen_list.Count];
		setting_value_ = 0;
		for (int i = 0; i < systemCtrl.instance.screen_list.Count; i++)
		{
			select_text_[i] = systemCtrl.instance.screen_list[i].width + "☓" + systemCtrl.instance.screen_list[i].height;
			if (GSStatic.option_work.resolution_w == (uint)systemCtrl.instance.screen_list[i].width && GSStatic.option_work.resolution_h == (uint)systemCtrl.instance.screen_list[i].height)
			{
				setting_value_ = i;
			}
		}
		max_value_size_ = systemCtrl.instance.screen_list.Count;
		SelectTextSet(Color.black);
	}

	private bool CheckDifferenceScreenList()
	{
		bool result = false;
		if (systemCtrl.instance.screen_list.Count != systemCtrl.instance.old_screen_list.Count)
		{
			result = true;
		}
		else
		{
			for (int i = 0; i < systemCtrl.instance.screen_list.Count; i++)
			{
				if (systemCtrl.instance.screen_list[i].width != systemCtrl.instance.old_screen_list[i].width || systemCtrl.instance.screen_list[i].height != systemCtrl.instance.old_screen_list[i].height)
				{
					result = true;
					break;
				}
			}
		}
		return result;
	}

	private void CheckScreenSize(int _before_value, int _before_max_value_size, int _in_val)
	{
		bool flag = false;
		int num = 0;
		int num2 = _before_value + _in_val;
		if (_before_max_value_size <= num2)
		{
			num = 1;
		}
		else if (num2 < 0)
		{
			num = -1;
		}
		num2 = ((_before_max_value_size > num2) ? num2 : 0);
		num2 = ((num2 >= 0) ? num2 : (_before_max_value_size - 1));
		if (systemCtrl.instance.old_screen_list.Count <= num2 || systemCtrl.instance.old_screen_list.Count <= _before_max_value_size - 1)
		{
			systemCtrl.instance.UpdateOldScreenList();
		}
		if ((systemCtrl.instance.old_screen_list[num2].width != systemCtrl.instance.screen_list[setting_value_].width || systemCtrl.instance.old_screen_list[num2].height != systemCtrl.instance.screen_list[setting_value_].height) && systemCtrl.instance.screen_list[max_value_size_ - 1].height < systemCtrl.instance.old_screen_list[systemCtrl.instance.old_screen_list.Count - 1].height)
		{
			switch (num)
			{
			case 1:
				setting_value_ = 0;
				flag = true;
				break;
			case -1:
				setting_value_ = max_value_size_ - 1;
				flag = true;
				break;
			default:
				if (systemCtrl.instance.screen_list[max_value_size_ - 1].height < systemCtrl.instance.old_screen_list[num2].height)
				{
					setting_value_ = 0;
					flag = true;
				}
				break;
			}
		}
		if (flag)
		{
			SelectTextSet(Color.black);
		}
		SelectDecision();
	}
}
