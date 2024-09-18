using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class optionSelectItem : optionItem
{
	[Serializable]
	public class OptionSelectLanguage
	{
		public AssetBundleSprite select_window_;

		public AssetBundleSprite arrowL_;

		public AssetBundleSprite arrowR_;

		public List<AssetBundleSprite> select_guide_;

		public Text text_;
	}

	protected int setting_value_;

	protected string[] select_text_;

	protected int max_value_size_;

	[SerializeField]
	protected OptionSelectLanguage select_;

	[SerializeField]
	private float guide_space_;

	public override void Init()
	{
		base.Init();
		if (max_value_size_ <= 0 && select_.select_guide_.Count > 0)
		{
			max_value_size_ = select_.select_guide_.Count;
		}
		mainCtrl.instance.addText(select_.text_);
		arrowActiveSet(true);
		select_.arrowL_.load("/menu/common/", "select_arrow");
		select_.arrowL_.spriteNo(1);
		select_.arrowR_.load("/menu/common/", "select_arrow");
		select_.arrowR_.spriteNo(1);
		arrowActiveSet(false);
		touch_list_[2].touch_key_type = KeyType.A;
		if (select_.select_guide_.Count > 0)
		{
			AssetBundle assetBundle = AssetBundleCtrl.instance.load("/menu/option/", "option_button");
			foreach (AssetBundleSprite item in select_.select_guide_)
			{
				item.sprite_data_.Clear();
				item.sprite_data_.AddRange(assetBundle.LoadAllAssets<Sprite>());
				item.spriteNo(0);
			}
		}
		SelectTextSet(Color.white);
		guideSet();
	}

	public override void SelectEntry()
	{
		base.SelectEntry();
		arrowActiveSet(true);
		guideSpriteSet(2);
		SelectTextSet(Color.black);
	}

	public override void SelectExit()
	{
		base.SelectExit();
		arrowActiveSet(false);
		guideSpriteSet(1);
		SelectTextSet(Color.white);
	}

	public override void ChangeValue(int val)
	{
		soundCtrl.instance.PlaySE(42);
		setting_value_ += val;
		setting_value_ = ((setting_value_ < max_value_size_) ? setting_value_ : 0);
		setting_value_ = ((setting_value_ >= 0) ? setting_value_ : (max_value_size_ - 1));
		SelectTextSet(Color.black);
		guideSpriteSet(2);
	}

	public override void OnTouch(TouchParameter touch_param)
	{
		SelectEntry();
		optionInfo optionInfo2 = touch_param.argument_parameter as optionInfo;
		switch (optionInfo2.index_)
		{
		case 0:
			EnterValue();
			return;
		case 1:
			setting_value_++;
			break;
		case 2:
			setting_value_--;
			break;
		}
		setting_value_ = ((setting_value_ >= 0) ? setting_value_ : (max_value_size_ - 1));
		setting_value_ = ((setting_value_ < max_value_size_) ? setting_value_ : 0);
		SelectTextSet(Color.black);
		guideSpriteSet(2);
	}

	public virtual void EnterValue()
	{
	}

	public override void Close()
	{
		base.Close();
		mainCtrl.instance.removeText(select_.text_);
		foreach (AssetBundleSprite item in select_.select_guide_)
		{
			item.sprite_data_.Clear();
		}
	}

	private void arrowSpriteSet(int sprite_no)
	{
		select_.arrowL_.spriteNo(sprite_no);
		select_.arrowR_.spriteNo(sprite_no);
	}

	private void arrowActiveSet(bool in_active)
	{
		select_.arrowL_.active = in_active;
		select_.arrowR_.active = in_active;
	}

	private void guideSet()
	{
		float num = (0f - guide_space_) * (float)(select_.select_guide_.Count / 2);
		if (select_.select_guide_.Count % 2 == 0)
		{
			num += guide_space_ / 2f;
		}
		for (int i = 0; i < select_.select_guide_.Count; i++)
		{
			Vector3 localPosition = select_.select_guide_[i].transform.localPosition;
			select_.select_guide_[i].transform.localPosition = new Vector3(num, localPosition.y, localPosition.z);
			num += guide_space_;
		}
	}

	private void guideSpriteSet(int sprite_no)
	{
		for (int i = 0; i < select_.select_guide_.Count; i++)
		{
			if (i == setting_value_)
			{
				select_.select_guide_[i].spriteNo(sprite_no);
			}
			else
			{
				select_.select_guide_[i].spriteNo(0);
			}
		}
	}

	protected void SelectTextSet(Color text_color)
	{
		select_.text_.color = text_color;
		if (select_text_.Length > setting_value_)
		{
			select_.text_.text = select_text_[setting_value_];
		}
	}
}
