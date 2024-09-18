public class optionWindowMode : optionSelectItem
{
	public override void Init()
	{
		max_value_size_ = 2;
		select_text_ = new string[2]
		{
			TextDataCtrl.GetText(TextDataCtrl.OptionTextID.SELECT_WINDOW),
			TextDataCtrl.GetText(TextDataCtrl.OptionTextID.SELECT_FULLSCREEN)
		};
		SetText(TextDataCtrl.GetText(TextDataCtrl.OptionTextID.ITEM_SCREEN));
		base.Init();
		setting_value_ = GSStatic.option_work.window_mode;
		old_value_ = setting_value_;
	}

	public override void InitValueSet()
	{
		ScreenUtility.SetResolution((int)GSStatic.option_work.resolution_w, (int)GSStatic.option_work.resolution_h, (GSStatic.option_work.window_mode != 0) ? true : false);
	}

	public override void EnterValue()
	{
	}

	public override void DefaultValueSet()
	{
		setting_value_ = 0;
		GSStatic.option_work.window_mode = (byte)setting_value_;
		ScreenUtility.SetResolution((int)GSStatic.option_work.resolution_w, (int)GSStatic.option_work.resolution_h, (GSStatic.option_work.window_mode != 0) ? true : false);
		base.DefaultValueSet();
	}

	public override bool ConfirmChange()
	{
		if (old_value_ != GSStatic.option_work.window_mode)
		{
			return true;
		}
		return false;
	}

	public override bool SelectDecision()
	{
		GSStatic.option_work.window_mode = (byte)setting_value_;
		ScreenUtility.SetResolution((int)GSStatic.option_work.resolution_w, (int)GSStatic.option_work.resolution_h, (GSStatic.option_work.window_mode != 0) ? true : false);
		return false;
	}

	public override void ChangeValue(int val)
	{
		base.ChangeValue(val);
		SelectDecision();
	}

	public override void OnTouch(TouchParameter touch_param)
	{
		base.OnTouch(touch_param);
		SelectDecision();
	}
}
