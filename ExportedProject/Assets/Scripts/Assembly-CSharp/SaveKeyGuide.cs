using System.Linq;
using UnityEngine;

public class SaveKeyGuide : guideCtrl
{
	public override void load()
	{
		base.load();
	}

	public override void init()
	{
		base.init();
		load();
		ActiveTouch();
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
		guideIconSet(false, base.current_guide);
	}

	public override void guideIconSet(bool in_guide, GuideType in_type)
	{
		sprite_guide_.active = true;
		sprite_line_.active = false;
		foreach (GuideIcon item in guide_list_)
		{
			mainCtrl.instance.addText(item.text_);
			item.sprite_.active = false;
		}
		if (DebugDispCheck())
		{
			current_guide_ = in_type;
			if (in_type == GuideType.SAVE)
			{
				guide_list_[0].SetKeyTypeSprite(KeyType.B);
				guide_list_[0].text_.text = TextDataCtrl.GetText(TextDataCtrl.CommonTextID.BACK);
			}
			UpdateTouchArea();
			int num = guide_list_.Count((GuideIcon guide) => guide.sprite_.active);
			sprite_guide_.transform.localPosition = new Vector3(slide_out_pos_x_ - base.guide_width_ * (float)num, guide_pos_y_, 0f);
		}
	}
}
