public class optionShake : optionToggleItem
{
	public override void Init()
	{
		SetText(TextDataCtrl.GetText(TextDataCtrl.OptionTextID.ITEM_SHAKE));
		select_text_ = new string[2]
		{
			TextDataCtrl.GetText(TextDataCtrl.OptionTextID.SELECT_OFF),
			TextDataCtrl.GetText(TextDataCtrl.OptionTextID.SELECT_ON)
		};
		base.Init();
		setting_value_ = GSStatic.option_work.shake_type;
		old_value_ = setting_value_;
	}

	public override void ChangeValue(int val)
	{
		base.ChangeValue(val);
		if (setting_value_ == 0)
		{
			optionCtrl.instance.is_shake = false;
		}
		else
		{
			optionCtrl.instance.is_shake = true;
		}
		GSStatic.option_work.shake_type = (ushort)setting_value_;
	}

	public override void OnTouch(TouchParameter touch_param)
	{
		base.OnTouch(touch_param);
		if (setting_value_ == 0)
		{
			optionCtrl.instance.is_shake = false;
		}
		else
		{
			optionCtrl.instance.is_shake = true;
		}
		GSStatic.option_work.shake_type = (ushort)setting_value_;
	}

	public override void InitValueSet()
	{
		if (GSStatic.option_work.shake_type == 0)
		{
			optionCtrl.instance.is_shake = false;
		}
		else
		{
			optionCtrl.instance.is_shake = true;
		}
	}

	public override void DefaultValueSet()
	{
		setting_value_ = GSStatic.option_work.shake_type;
		CheckValue();
		base.DefaultValueSet();
	}

	private void CheckValue()
	{
		if (GSStatic.option_work.shake_type == 0)
		{
			optionCtrl.instance.is_shake = false;
		}
		else
		{
			optionCtrl.instance.is_shake = true;
		}
	}

	public override bool ConfirmChange()
	{
		if (old_value_ != setting_value_)
		{
			return true;
		}
		return false;
	}
}
