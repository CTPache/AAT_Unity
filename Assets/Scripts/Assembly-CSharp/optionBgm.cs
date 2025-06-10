using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class optionBgm : optionItem
{
	[Serializable]
	public class OptionGauge
	{
		public List<AssetBundleSprite> gauge_btn_;

		public List<AssetBundleSprite> gauge_bar_;

		public AssetBundleSprite count_window_;

		public Text count_text_;
	}

	private int setting_value_;

	[SerializeField]
public OptionGauge gauge_;

	public override void Init()
	{
		base.Init();
		mainCtrl.instance.addText(gauge_.count_text_);
		SetText(TextDataCtrl.GetText(TextDataCtrl.OptionTextID.ITEM_BGM));
		gauge_.count_window_.load("/menu/option/", "option_count_bg01");
		gauge_.count_window_.spriteNo(0);
		AssetBundle assetBundle = AssetBundleCtrl.instance.load("/menu/option/", "option_button");
		foreach (AssetBundleSprite item in gauge_.gauge_btn_)
		{
			item.sprite_data_.Clear();
			item.sprite_data_.AddRange(assetBundle.LoadAllAssets<Sprite>());
			item.spriteNo(0);
		}
		assetBundle = AssetBundleCtrl.instance.load("/menu/option/", "option_gauge_bar");
		foreach (AssetBundleSprite item2 in gauge_.gauge_bar_)
		{
			item2.sprite_data_.Clear();
			item2.sprite_data_.AddRange(assetBundle.LoadAllAssets<Sprite>());
			item2.spriteNo(0);
		}
		setting_value_ = GSStatic.option_work.bgm_value;
		old_value_ = setting_value_;
		gaugePosSet();
		ValueSet();
		gaugeSpriteSet(1);
	}

	public override void SelectEntry()
	{
		base.SelectEntry();
		gauge_.count_window_.spriteNo(1);
		gaugeSpriteSet(2);
	}

	public override void SelectExit()
	{
		base.SelectExit();
		gauge_.count_window_.spriteNo(0);
		gaugeSpriteSet(1);
	}

	public override void ChangeValue(int val)
	{
		soundCtrl instance = soundCtrl.instance;
		if (setting_value_ + val > 0 && setting_value_ + val <= gauge_.gauge_btn_.Count)
		{
			soundCtrl.instance.PlaySE(42);
			setting_value_ += val;
		}
		ValueSet();
		gaugeSpriteSet(2);
		instance.option_set_bgm_rate = 0.25f * (float)(setting_value_ - 1);
		instance.VolumeChangeBGM(GSStatic.global_work_.bgm_vol, 0);
	}

	public override void Close()
	{
		base.Close();
		mainCtrl.instance.removeText(gauge_.count_text_);
	}

	public override void OnTouch(TouchParameter touch_param)
	{
		SelectEntry();
		optionInfo optionInfo2 = touch_param.argument_parameter as optionInfo;
		int index_ = optionInfo2.index_;
		setting_value_ = index_;
		ValueSet();
		gaugeSpriteSet(2);
		soundCtrl.instance.option_set_bgm_rate = 0.25f * (float)(setting_value_ - 1);
		soundCtrl.instance.VolumeChangeBGM(GSStatic.global_work_.bgm_vol, 0);
	}

	private void ValueSet()
	{
		GSStatic.option_work.bgm_value = (ushort)setting_value_;
		gauge_.count_text_.text = (setting_value_ - 1).ToString();
		gauge_.count_window_.transform.localPosition = new Vector3(gauge_.gauge_btn_[setting_value_ - 1].transform.localPosition.x, gauge_.count_window_.transform.localPosition.y, gauge_.count_window_.transform.localPosition.z);
	}

	private void gaugeSpriteSet(int sprite_no)
	{
		for (int i = 0; i < gauge_.gauge_btn_.Count; i++)
		{
			if (i < setting_value_)
			{
				gauge_.gauge_btn_[i].spriteNo(sprite_no);
			}
			else
			{
				gauge_.gauge_btn_[i].spriteNo(0);
			}
		}
	}

	private void gaugePosSet()
	{
		float x = gauge_.gauge_bar_[0].sprite_renderer_.size.x;
		float x2 = gauge_.gauge_btn_[0].sprite_renderer_.size.x;
		Vector3 localPosition = gauge_.gauge_bar_[0].transform.localPosition;
		Vector3 localPosition2 = gauge_.gauge_btn_[0].transform.localPosition;
		float num = x * (float)(gauge_.gauge_bar_.Count / 2) + x2 * (float)(gauge_.gauge_btn_.Count / 2);
		num *= -1f;
		for (int i = 0; i < gauge_.gauge_btn_.Count; i++)
		{
			gauge_.gauge_btn_[i].transform.localPosition = new Vector3(num, localPosition2.y, localPosition2.z);
			if (i < gauge_.gauge_bar_.Count)
			{
				gauge_.gauge_bar_[i].transform.localPosition = new Vector3(num + x2 / 2f + x / 2f, localPosition.y, localPosition.z);
			}
			num += x + x2;
		}
	}

	public override void InitValueSet()
	{
		soundCtrl.instance.option_set_bgm_rate = 0.25f * (float)(GSStatic.option_work.bgm_value - 1);
		soundCtrl.instance.VolumeChangeBGM(GSStatic.global_work_.bgm_vol, 0);
	}

	public override void DefaultValueSet()
	{
		setting_value_ = 3;
		gaugePosSet();
		ValueSet();
		gaugeSpriteSet(1);
		InitValueSet();
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
