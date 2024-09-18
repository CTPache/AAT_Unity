using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class optionToggleItem : optionItem
{
	[Serializable]
	public class OptionSelectButton
	{
		public List<AssetBundleSprite> select_btn_;

		public AssetBundleSprite count_window_;

		public List<Text> count_text_;
	}

	protected int setting_value_;

	protected int max_value_size_;

	protected string[] select_text_;

	[SerializeField]
	protected OptionSelectButton select_;

	[SerializeField]
	private float select_space_;

	public override void Init()
	{
		base.Init();
		if (max_value_size_ <= 0 && select_.select_btn_.Count > 0)
		{
			max_value_size_ = select_.select_btn_.Count;
		}
		foreach (Text item in select_.count_text_)
		{
			mainCtrl.instance.addText(item);
		}
		select_.count_window_.load("/menu/option/", "option_count_bg01");
		select_.count_window_.spriteNo(0);
		if (select_.select_btn_.Count > 0)
		{
			AssetBundle assetBundle = AssetBundleCtrl.instance.load("/menu/option/", "option_button");
			foreach (AssetBundleSprite item2 in select_.select_btn_)
			{
				item2.sprite_data_.Clear();
				item2.sprite_data_.AddRange(assetBundle.LoadAllAssets<Sprite>());
				item2.spriteNo(0);
			}
		}
		btnSet();
		btnSpriteSet(1);
		countWindowSet(0);
	}

	public override void SelectEntry()
	{
		base.SelectEntry();
		countWindowSet(1);
		btnSpriteSet(2);
	}

	public override void SelectExit()
	{
		base.SelectExit();
		countWindowSet(0);
		btnSpriteSet(1);
	}

	public override void ChangeValue(int val)
	{
		soundCtrl.instance.PlaySE(42);
		setting_value_ += val;
		setting_value_ = ((setting_value_ < max_value_size_) ? setting_value_ : 0);
		setting_value_ = ((setting_value_ >= 0) ? setting_value_ : (max_value_size_ - 1));
		btnSpriteSet(2);
		countWindowSet(1);
	}

	public override void Close()
	{
		base.Close();
		foreach (Text item in select_.count_text_)
		{
			mainCtrl.instance.removeText(item);
		}
	}

	public override void OnTouch(TouchParameter touch_param)
	{
		SelectEntry();
		optionInfo optionInfo2 = touch_param.argument_parameter as optionInfo;
		int num = optionInfo2.index_ - 1;
		setting_value_ = num;
		btnSpriteSet(2);
		countWindowSet(1);
	}

	public override void DefaultValueSet()
	{
		btnSet();
		btnSpriteSet(1);
		countWindowSet(0);
	}

	private void countWindowSet(int sprite_no)
	{
		select_.count_window_.spriteNo(sprite_no);
		Vector3 localPosition = select_.count_window_.transform.localPosition;
		for (int i = 0; i < select_.count_text_.Count; i++)
		{
			if (i == setting_value_)
			{
				select_.count_window_.transform.localPosition = new Vector3(select_.count_text_[setting_value_].transform.localPosition.x, localPosition.y, localPosition.z);
			}
		}
	}

	private void btnSpriteSet(int sprite_no)
	{
		for (int i = 0; i < select_.select_btn_.Count; i++)
		{
			if (i == setting_value_)
			{
				select_.select_btn_[i].spriteNo(sprite_no);
			}
			else
			{
				select_.select_btn_[i].spriteNo(0);
			}
		}
	}

	private void btnSet()
	{
		int num = -(select_.select_btn_.Count / 2);
		float num2 = select_space_ * (float)num;
		if (select_.select_btn_.Count % 2 == 0)
		{
			num2 += select_space_ * 0.5f;
		}
		foreach (AssetBundleSprite item in select_.select_btn_)
		{
			item.transform.localPosition = new Vector3(num2, item.transform.localPosition.y, item.transform.localPosition.z);
			num2 += select_space_;
		}
		num2 = select_space_ * (float)(-(select_.count_text_.Count / 2));
		if (select_.count_text_.Count % 2 == 0)
		{
			num2 += select_space_ * 0.5f;
		}
		for (int i = 0; i < select_.count_text_.Count; i++)
		{
			select_.count_text_[i].text = select_text_[i];
			Vector3 localPosition = select_.count_text_[i].transform.localPosition;
			select_.count_text_[i].transform.localPosition = new Vector3(num2, localPosition.y, localPosition.z);
			num2 += select_space_;
		}
	}
}
