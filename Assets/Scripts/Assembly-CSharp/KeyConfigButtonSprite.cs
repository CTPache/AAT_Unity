using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyConfigButtonSprite : MonoBehaviour
{
	public AssetBundleSprite item_bg_;

	public Text option_title_;

	public List<InputTouch> touch_list_ = new List<InputTouch>();

	public AssetBundleSprite input_config_sprite_;

	public AssetBundleSprite key_sprite_enable;

	public AssetBundleSprite key_sprite_diseble;

	[SerializeField]
	protected AssetBundleSprite ander_line_;

	public void Init(bool quarter)
	{
		if (quarter)
		{
			input_config_sprite_.load("/menu/option/", "option_list_bg_6");
			item_bg_.load("/menu/option/", "option_list_bg_4");
		}
		else
		{
			input_config_sprite_.load("/menu/option/", "option_list_bg_5");
			item_bg_.load("/menu/option/", "option_list_bg_2");
		}
		input_config_sprite_.sprite_renderer_.gameObject.SetActive(false);
		key_sprite_enable.load("/menu/option/", "option_list_bg_8");
		key_sprite_enable.sprite_renderer_.gameObject.SetActive(false);
		key_sprite_diseble.load("/menu/option/", "option_list_bg_7");
		key_sprite_diseble.sprite_renderer_.gameObject.SetActive(true);
		if (ander_line_.sprite_renderer_ != null)
		{
			ander_line_.load("/menu/option/", "option_hr");
			ander_line_.sprite_renderer_.enabled = true;
			option_title_.color = Color.white;
			ander_line_.sprite_renderer_.size = new Vector2(490f, 2f);
		}
	}

	public void SetInputFase(bool input)
	{
		if (input)
		{
			input_config_sprite_.sprite_renderer_.gameObject.SetActive(true);
			key_sprite_enable.sprite_renderer_.gameObject.SetActive(false);
			key_sprite_diseble.sprite_renderer_.gameObject.SetActive(false);
		}
		else
		{
			input_config_sprite_.sprite_renderer_.gameObject.SetActive(false);
			SelectEntry();
		}
	}

	public void SetSprite(KeyCode current_key_code)
	{
		if (key_sprite_diseble.sprite_renderer_ != null)
		{
			int keyCodeSpriteNum = keyGuideBase.GuideIcon.GetKeyCodeSpriteNum(current_key_code);
			key_sprite_diseble.spriteNo(keyCodeSpriteNum);
			key_sprite_enable.spriteNo(keyCodeSpriteNum);
		}
	}

	public void SelectEntry()
	{
		key_sprite_enable.sprite_renderer_.gameObject.SetActive(true);
		key_sprite_diseble.sprite_renderer_.gameObject.SetActive(false);
	}

	public void SelectExit()
	{
		key_sprite_enable.sprite_renderer_.gameObject.SetActive(false);
		key_sprite_diseble.sprite_renderer_.gameObject.SetActive(true);
	}
}
