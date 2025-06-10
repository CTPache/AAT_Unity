using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class optionKeyConfigButton : optionItem
{
	public KeyType set_type_;

	public Text set_text_;

	public KeyCode current_key_code;

	[SerializeField]
public KeyConfigButtonSprite button_sprite_;

	private bool init_;

	public string RetunText()
	{
		TextDataCtrl.OptionTextID in_text_id = TextDataCtrl.OptionTextID.DEFAULT;
		switch (set_type_)
		{
		case KeyType.A:
			in_text_id = TextDataCtrl.OptionTextID.ITEM_DECIDE;
			break;
		case KeyType.B:
			in_text_id = TextDataCtrl.OptionTextID.ITEM_CANCEL;
			break;
		case KeyType.X:
			in_text_id = TextDataCtrl.OptionTextID.ITEM_TUKITUKE;
			break;
		case KeyType.L:
			in_text_id = TextDataCtrl.OptionTextID.ITEM_YUSABURI;
			break;
		case KeyType.R:
			in_text_id = TextDataCtrl.OptionTextID.ITEM_RECORD;
			break;
		case KeyType.Start:
			in_text_id = TextDataCtrl.OptionTextID.ITEM_OPTION;
			break;
		case KeyType.Record:
			in_text_id = TextDataCtrl.OptionTextID.ITEM_PERSON;
			break;
		case KeyType.Y:
			in_text_id = TextDataCtrl.OptionTextID.ITEM_TITLE_RETURN;
			break;
		case KeyType.Up:
		case KeyType.StickR_Up:
			return "↑";
		case KeyType.Down:
		case KeyType.StickR_Down:
			return "↓";
		case KeyType.Right:
		case KeyType.StickR_Right:
			return "→";
		case KeyType.Left:
		case KeyType.StickR_Left:
			return "←";
		}
		return TextDataCtrl.GetText(in_text_id);
	}

	public void InitButton(bool quarter)
	{
		if (!init_)
		{
			option_title_ = button_sprite_.option_title_;
			item_bg_ = button_sprite_.item_bg_;
			touch_list_ = button_sprite_.touch_list_;
		}
		button_sprite_.Init(quarter);
		foreach (InputTouch item in touch_list_)
		{
			item.touch_key_type = KeyType.A;
		}
		item_bg_.spriteNo(0);
		option_title_.color = Color.white;
		mainCtrl.instance.addText(option_title_);
		SetText(RetunText());
		old_value_ = (int)current_key_code;
		SetSprite();
		init_ = true;
	}

	public void SetSprite()
	{
		if (button_sprite_ != null)
		{
			button_sprite_.SetSprite(current_key_code);
		}
	}

	public override void SetText(string text)
	{
		string text2 = text;
		if (GSStatic.global_work_.language == "KOREA")
		{
			text2 = text.Replace(".", "・");
		}
		base.SetText(text2);
	}

	public override void ChangeValue(int val)
	{
		current_key_code = (KeyCode)val;
		SetSprite();
	}

	public override void InitValueSet()
	{
		current_key_code = padCtrl.instance.GetKeyCode(set_type_);
		SetSprite();
	}

	public override void DefaultValueSet()
	{
		current_key_code = padCtrl.instance.GetDefaultKeyCode(set_type_);
		SetSprite();
		padCtrl.instance.SetKeyType(set_type_, current_key_code);
	}

	public override void Close()
	{
		base.Close();
		current_key_code = padCtrl.instance.GetKeyCode(set_type_);
	}

	public override void SelectEntry()
	{
		base.SelectEntry();
		button_sprite_.SelectEntry();
	}

	public override void SelectExit()
	{
		base.SelectExit();
		button_sprite_.SelectExit();
	}

	public bool configChanged()
	{
		if (current_key_code == padCtrl.instance.GetKeyCode(set_type_))
		{
			return true;
		}
		return false;
	}

	public override bool ConfirmChange()
	{
		if (old_value_ != (int)padCtrl.instance.GetKeyCode(set_type_))
		{
			return true;
		}
		return false;
	}

	public override void OnTouch(TouchParameter touch_param)
	{
	}

	public override void PlayDecide()
	{
		base.play_decide = true;
		coroutineCtrl.instance.Play(play_config());
	}

	private IEnumerator play_config()
	{
		yield return null;
		button_sprite_.SetInputFase(true);
		while (true)
		{
			if (padCtrl.instance.InputAnyKeyPad())
			{
				soundCtrl.instance.PlaySE(53);
				yield return null;
				continue;
			}
			bool enter = false;
			IEnumerator enumerator = Enum.GetValues(typeof(KeyCode)).GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					KeyCode keyCode = (KeyCode)enumerator.Current;
					KeyCode keyCode2 = keyCode;
					if (padCtrl.instance.InputGetKeyDown(keyCode2))
					{
						if (padCtrl.instance.InputGetKeyDown(KeyCode.AltGr))
						{
							keyCode2 = KeyCode.RightAlt;
						}
						if (padCtrl.instance.SetKeyEnable(keyCode2))
						{
							optionCtrl.instance.ReleaseOverLapKeyCode(keyCode2, current_key_code);
							current_key_code = keyCode2;
							SetSprite();
							enter = true;
							break;
						}
					}
				}
			}
			finally
			{
				IDisposable disposable;
				IDisposable disposable2 = (disposable = enumerator as IDisposable);
				if (disposable != null)
				{
					disposable2.Dispose();
				}
			}
			if (enter)
			{
				break;
			}
			yield return null;
		}
		soundCtrl.instance.PlaySE(43);
		button_sprite_.SetInputFase(false);
		base.play_decide = false;
	}
}
