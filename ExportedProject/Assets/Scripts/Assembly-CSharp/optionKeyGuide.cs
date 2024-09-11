using System.Linq;
using UnityEngine;

public class optionKeyGuide : guideCtrl
{
	public override void load()
	{
		base.load();
	}

	public override void init()
	{
		base.init();
		load();
	}

	public override void Close()
	{
		base.Close();
		foreach (GuideIcon item in guide_list_)
		{
			mainCtrl.instance.removeText(item.text_);
		}
	}

	public override void ReLoadGuid()
	{
		SymbolLoad();
		if (getGuideActive())
		{
			guideIconSet(false, base.current_guide);
		}
	}

	public override void guideIconSet(bool in_guide, GuideType in_type)
	{
		sprite_guide_.active = true;
		sprite_line_.active = true;
		current_guide_ = in_type;
		foreach (GuideIcon item in guide_list_)
		{
			item.sprite_.active = false;
			mainCtrl.instance.addText(item.text_);
		}
		if (DebugDispCheck())
		{
			switch (in_type)
			{
			case GuideType.OPTION_TITLE:
				guide_list_[0].SetKeyTypeSprite(KeyType.B);
				guide_list_[1].SetKeyTypeSprite(KeyType.DEFAULT_RETURN_KEY);
				guide_list_[2].SetKeyTypeSprite(KeyType.L);
				guide_list_[0].text_.text = TextDataCtrl.GetText(TextDataCtrl.OptionTextID.BACK);
				guide_list_[1].text_.text = TextDataCtrl.GetText(TextDataCtrl.OptionTextID.DEFAULT);
				guide_list_[2].text_.text = TextDataCtrl.GetText(TextDataCtrl.OptionTextID.CHANGE);
				break;
			case GuideType.OPTION_INGAME:
				guide_list_[0].SetKeyTypeSprite(KeyType.B);
				guide_list_[1].SetKeyTypeSprite(KeyType.DEFAULT_RETURN_KEY);
				guide_list_[2].SetKeyTypeSprite(KeyType.Y);
				guide_list_[0].text_.text = TextDataCtrl.GetText(TextDataCtrl.OptionTextID.BACK);
				guide_list_[1].text_.text = TextDataCtrl.GetText(TextDataCtrl.OptionTextID.DEFAULT);
				guide_list_[2].text_.text = TextDataCtrl.GetText(TextDataCtrl.OptionTextID.TITLE);
				break;
			}
			UpdateTouchArea();
			SetLanguageLayout();
			int num = guide_list_.Count((GuideIcon guide) => guide.sprite_.active);
			sprite_guide_.transform.localPosition = new Vector3(slide_out_pos_x_ - base.guide_width_ * (float)num, guide_pos_y_, 0f);
		}
	}
}
