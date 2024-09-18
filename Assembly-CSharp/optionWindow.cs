using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class optionWindow : optionItem
{
	[Serializable]
	public class OptionSelectButton
	{
		public List<AssetBundleSprite> select_btn_;

		public AssetBundleSprite count_window_;

		public List<Text> count_text_;
	}

	private static optionWindow instance_;

	private int setting_value_;

	private const int DEFAULT_FONT_SIZE = 26;

	private const int GERMAN_FONT_SIZE = 21;

	private string[] select_text_;

	[SerializeField]
	private OptionSelectButton select_;

	[SerializeField]
	private float select_space_;

	private List<float> change_alpha_rate_ = new List<float> { 1f, 0.88f, 0.76f };

	private const float default_alpha_ = 1f;

	public static optionWindow instance
	{
		get
		{
			return instance_;
		}
	}

	public float alpha_rate
	{
		get
		{
			return 1f * change_alpha_rate_[GSStatic.option_work.window_type];
		}
	}

	private void Awake()
	{
		if (instance_ == null)
		{
			instance_ = this;
		}
	}

	public override void Init()
	{
		base.Init();
		foreach (Text item in select_.count_text_)
		{
			mainCtrl.instance.addText(item);
		}
		SetText(TextDataCtrl.GetText(TextDataCtrl.OptionTextID.ITEM_TRANSPARENCY));
		select_text_ = new string[3]
		{
			TextDataCtrl.GetText(TextDataCtrl.OptionTextID.SELECT_OFF),
			TextDataCtrl.GetText(TextDataCtrl.OptionTextID.SELECT_LOW),
			TextDataCtrl.GetText(TextDataCtrl.OptionTextID.SELECT_HIGH)
		};
		Language language = GSStatic.global_work_.language;
		if (language == Language.FRANCE || language == Language.GERMAN)
		{
			select_.count_window_.load("/menu/option/", "option_count_bg03");
		}
		else
		{
			select_.count_window_.load("/menu/option/", "option_count_bg01");
		}
		select_.count_window_.spriteNo(0);
		AssetBundle assetBundle = AssetBundleCtrl.instance.load("/menu/option/", "option_button");
		foreach (AssetBundleSprite item2 in select_.select_btn_)
		{
			item2.sprite_data_.Clear();
			item2.sprite_data_.AddRange(assetBundle.LoadAllAssets<Sprite>());
			item2.spriteNo(0);
		}
		fontSizeSet();
		setting_value_ = GSStatic.option_work.window_type;
		old_value_ = setting_value_;
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
		messageBoardCtrl messageBoardCtrl2 = messageBoardCtrl.instance;
		optionCtrl optionCtrl2 = optionCtrl.instance;
		setting_value_ += val;
		setting_value_ = ((setting_value_ < select_.select_btn_.Count) ? setting_value_ : 0);
		setting_value_ = ((setting_value_ >= 0) ? setting_value_ : (select_.select_btn_.Count - 1));
		btnSpriteSet(2);
		countWindowSet(1);
		GSStatic.option_work.window_type = (ushort)setting_value_;
		UpdateMessageWindowRendererColor();
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
		GSStatic.option_work.window_type = (ushort)setting_value_;
		UpdateMessageWindowRendererColor();
	}

	private void UpdateMessageWindowRendererColor()
	{
		optionCtrl optionCtrl2 = optionCtrl.instance;
		optionCtrl2.message_window.sprite_renderer_.color = new Color(optionCtrl2.message_window.sprite_renderer_.color.r, optionCtrl2.message_window.sprite_renderer_.color.g, optionCtrl2.message_window.sprite_renderer_.color.b, 1f * change_alpha_rate_[setting_value_]);
		if (messageBoardCtrl.instance != null)
		{
			messageBoardCtrl.instance.sprite_board.sprite_renderer_.color = new Color(messageBoardCtrl.instance.sprite_board.sprite_renderer_.color.r, messageBoardCtrl.instance.sprite_board.sprite_renderer_.color.g, messageBoardCtrl.instance.sprite_board.sprite_renderer_.color.b, 1f * change_alpha_rate_[setting_value_]);
		}
	}

	private void countWindowSet(int sprite_no)
	{
		select_.count_window_.spriteNo(sprite_no);
		Vector3 localPosition = select_.count_window_.transform.localPosition;
		select_.count_window_.transform.localPosition = new Vector3(select_.select_btn_[setting_value_].transform.localPosition.x, localPosition.y, localPosition.z);
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
		for (int i = 0; i < select_.count_text_.Count; i++)
		{
			select_.count_text_[i].text = select_text_[i];
			Vector3 localPosition = select_.count_text_[i].transform.localPosition;
			select_.count_text_[i].transform.localPosition = new Vector3(select_.select_btn_[i].transform.transform.localPosition.x, localPosition.y, localPosition.z);
		}
	}

	public override void InitValueSet()
	{
		optionCtrl optionCtrl2 = optionCtrl.instance;
		optionCtrl2.message_window.sprite_renderer_.color = new Color(optionCtrl2.message_window.sprite_renderer_.color.r, optionCtrl2.message_window.sprite_renderer_.color.g, optionCtrl2.message_window.sprite_renderer_.color.b, alpha_rate);
	}

	public override void DefaultValueSet()
	{
		setting_value_ = GSStatic.option_work.window_type;
		GSStatic.option_work.window_type = (ushort)setting_value_;
		UpdateMessageWindowRendererColor();
		btnSet();
		btnSpriteSet(1);
		countWindowSet(0);
	}

	public override bool ConfirmChange()
	{
		if (old_value_ != setting_value_)
		{
			return true;
		}
		return false;
	}

	private void fontSizeSet()
	{
		Language language = GSStatic.global_work_.language;
		int fontSize = ((language != Language.GERMAN) ? 26 : 21);
		foreach (Text item in select_.count_text_)
		{
			item.fontSize = fontSize;
		}
	}
}
