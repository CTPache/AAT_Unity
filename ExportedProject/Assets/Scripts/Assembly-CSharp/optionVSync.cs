public class optionVSync : optionToggleItem
{
	public override void Init()
	{
		max_value_size_ = 2;
		SetText(TextDataCtrl.GetText(TextDataCtrl.OptionTextID.ITEM_VSYNC));
		select_text_ = new string[2]
		{
			TextDataCtrl.GetText(TextDataCtrl.OptionTextID.SELECT_OFF),
			TextDataCtrl.GetText(TextDataCtrl.OptionTextID.SELECT_ON)
		};
		base.Init();
		setting_value_ = GSStatic.option_work.vsync;
		old_value_ = setting_value_;
	}

	public override void ChangeValue(int val)
	{
		base.ChangeValue(val);
		SetVsync();
	}

	public override void OnTouch(TouchParameter touch_param)
	{
		base.OnTouch(touch_param);
		SetVsync();
	}

	public override void InitValueSet()
	{
		ScreenUtility.SetVsync(GSStatic.option_work.vsync);
	}

	public override void DefaultValueSet()
	{
		setting_value_ = 1;
		SetVsync();
		base.DefaultValueSet();
	}

	public override bool ConfirmChange()
	{
		if (old_value_ != setting_value_)
		{
			return true;
		}
		return false;
	}

	private void SetVsync()
	{
		GSStatic.option_work.vsync = (byte)setting_value_;
		ScreenUtility.SetVsync(GSStatic.option_work.vsync);
	}
}
