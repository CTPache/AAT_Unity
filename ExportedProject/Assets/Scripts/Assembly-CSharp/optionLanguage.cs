public class optionLanguage : optionSelectItem
{
	public override void Init()
	{
		SetText(TextDataCtrl.GetText(TextDataCtrl.OptionTextID.ITEM_LANGUAGE));
		select_text_ = new string[7]
		{
			TextDataCtrl.GetText(TextDataCtrl.OptionTextID.JAPANESE),
			TextDataCtrl.GetText(TextDataCtrl.OptionTextID.ENGLISH),
			TextDataCtrl.GetText(TextDataCtrl.OptionTextID.FRENCH),
			TextDataCtrl.GetText(TextDataCtrl.OptionTextID.GERMAN),
			TextDataCtrl.GetText(TextDataCtrl.OptionTextID.KOREAN),
			TextDataCtrl.GetText(TextDataCtrl.OptionTextID.CHINESE_SIMPLIFIED),
			TextDataCtrl.GetText(TextDataCtrl.OptionTextID.CHINESE_TRADITIONAL)
		};
		base.Init();
		setting_value_ = GSStatic.option_work.language_type;
	}

	public override bool SelectDecision()
	{
		GSStatic.option_work.language_type = (ushort)setting_value_;
		return GSStatic.global_work_.language != (Language)setting_value_;
	}

	public override void EnterValue()
	{
		ChangeLanguage();
	}

	private bool ChangeLanguage()
	{
		GlobalWork global_work_ = GSStatic.global_work_;
		Language language;
		switch (setting_value_)
		{
		case 0:
			language = Language.JAPAN;
			break;
		case 1:
			language = Language.USA;
			break;
		case 2:
			language = Language.FRANCE;
			break;
		case 3:
			language = Language.GERMAN;
			break;
		case 4:
			language = Language.KOREA;
			break;
		case 5:
			language = Language.CHINA_S;
			break;
		case 6:
			language = Language.CHINA_T;
			break;
		default:
			language = Language.USA;
			break;
		}
		if (global_work_.language != language)
		{
			global_work_.language = language;
			ReplaceFont.instance.ChangeFont(global_work_.language);
			TextDataCtrl.SetLanguage(global_work_.language);
			return true;
		}
		return false;
	}

	public override void InitValueSet()
	{
		setting_value_ = GSStatic.option_work.language_type;
		ChangeLanguage();
	}

	public override bool ConfirmChange()
	{
		if (GSStatic.save_slot_language_ != GSStatic.global_work_.language)
		{
			return true;
		}
		return false;
	}
}
