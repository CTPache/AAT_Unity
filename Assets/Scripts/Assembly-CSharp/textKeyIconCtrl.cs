using System;
using UnityEngine;
using UnityEngine.UI;

public class textKeyIconCtrl : MonoBehaviour
{
	[Serializable]
	public class KeyIcon
	{
		public AssetBundleSprite icon;

		public KeyType icon_key_type_;

		public bool icon_active_
		{
			get
			{
				return icon.active;
			}
			set
			{
				icon.active = value;
			}
		}

		public Vector3 icon_pos_
		{
			get
			{
				return icon.transform.localPosition;
			}
		}

		public Color icon_color_
		{
			get
			{
				return icon.sprite_renderer_.color;
			}
			set
			{
				icon.sprite_renderer_.color = value;
			}
		}
	}

	private const int TEST_SPACE_JAPAN = 3;

	private const int TEST_SPACE_USA = 9;

	[SerializeField]
	private KeyIcon[] key_icon_;

	private float old_text_width_;

	public KeyIcon[] key_icon
	{
		get
		{
			return key_icon_;
		}
	}

	public void load(int icon_idx = 0)
	{
		key_icon_[icon_idx].icon.load("/menu/option/", "option_list_bg_9");
		key_icon_[icon_idx].icon.active = false;
	}

	public void load(string resource_name, int icon_idx = 0)
	{
		key_icon_[icon_idx].icon.load("/menu/option/", resource_name);
		key_icon_[icon_idx].icon.active = false;
	}

	public void oldTextWidthSet(Text text)
	{
		old_text_width_ = text.preferredWidth;
	}

	public void textIconSet(Text disp_message, KeyType key, int icon_idx = 0)
	{
		key_icon_[icon_idx].icon_key_type_ = key;
		textIconSet(disp_message, padCtrl.instance.GetKeyCode(key), icon_idx);
	}

	public void textIconSet(Text disp_message, KeyCode key, int icon_idx = 0)
	{
		key_icon_[icon_idx].icon.active = false;
		Vector3 zero = Vector3.zero;
		key_icon_[icon_idx].icon.transform.SetParent(disp_message.gameObject.transform);
		key_icon_[icon_idx].icon.transform.localPosition = Vector3.zero;
		zero.x = 0f - disp_message.rectTransform.sizeDelta.x / 2f + (disp_message.preferredWidth - (disp_message.preferredWidth - old_text_width_) / 2f);
		zero.y = disp_message.rectTransform.sizeDelta.y / 2f - disp_message.preferredHeight / 2f;
		key_icon_[icon_idx].icon.transform.localPosition = zero;
		iconSet(key, icon_idx);
	}

	public void iconPosSet(Transform parent_trans, Vector3 pos, int icon_idx = 0)
	{
		key_icon_[icon_idx].icon.transform.SetParent(parent_trans);
		key_icon_[icon_idx].icon.transform.localPosition = pos;
	}

	public string changeTextToIconSpase(string text)
	{
		string text2 = "\u3000";
		string language = GSStatic.global_work_.language;
		int num;
		if (language == "CHINA_S" || language == "CHINA_T" || language == "JAPAN")
		{
			num = 3;
		}
		else
		{
			text2 = MessageSystem.EnToHalf(text2, GSStatic.global_work_.language);
			num = 9;
		}
		string text3 = string.Empty;
		for (int i = 0; i < num; i++)
		{
			text3 += text2;
		}
		return text3;
	}

	public void iconSet(KeyCode key, int icon_idx = 0)
	{
		key_icon_[icon_idx].icon.spriteNo(keyGuideBase.GuideIcon.GetKeyCodeSpriteNum(key));
		key_icon_[icon_idx].icon.active = true;
	}

	public void iconSet(KeyType key, int icon_idx = 0)
	{
		key_icon_[icon_idx].icon_key_type_ = key;
		iconSet(padCtrl.instance.GetKeyCode(key), icon_idx);
	}

	private string textTagDelete(string text)
	{
		while (true)
		{
			int num = text.IndexOf("<");
			int num2 = text.IndexOf(">");
			if (num < 0 || num2 < 0)
			{
				break;
			}
			text = text.Remove(num, num2 - num + 1);
		}
		return text;
	}

	public void keyIconActiveSet(bool active)
	{
		KeyIcon[] array = key_icon_;
		foreach (KeyIcon keyIcon in array)
		{
			keyIcon.icon_active_ = active;
		}
	}
}
