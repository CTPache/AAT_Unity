public class optionKeyConfigDecide : optionCredit
{
	public override void Init()
	{
		base.Init();
		SetText(TextDataCtrl.GetText(TextDataCtrl.OptionTextID.ITEM_KEYCONFIG));
		select_.text_.text = TextDataCtrl.GetText(TextDataCtrl.OptionTextID.ITEM_CONFIG_DECIDE);
	}
}
