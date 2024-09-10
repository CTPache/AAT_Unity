using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class optionKeyConfig : optionItem
{
	public List<optionKeyConfigButton> button_list_ = new List<optionKeyConfigButton>();

	public int list_count_;

	[SerializeField]
	private KeyConfigButtonSprite sprite_prefab_;

	[SerializeField]
	protected AssetBundleSprite ander_line_;

	public override optionCtrl.CurrentPoint GetCurrentPoint()
	{
		if (list_count_ == 3)
		{
			return optionCtrl.CurrentPoint.FULL;
		}
		if (list_count_ >= button_list_.Count / 2)
		{
			return optionCtrl.CurrentPoint.HALF;
		}
		if (list_count_ == 1)
		{
			return optionCtrl.CurrentPoint.QUARTER;
		}
		return optionCtrl.CurrentPoint.NONE;
	}

	public override void SetCurrentPoint(optionCtrl.CurrentPoint point)
	{
		switch (point)
		{
		case optionCtrl.CurrentPoint.FULL:
			list_count_ = button_list_.Count - 1;
			break;
		case optionCtrl.CurrentPoint.HALF:
			list_count_ = Mathf.FloorToInt((button_list_.Count + 1) / 2);
			break;
		case optionCtrl.CurrentPoint.QUARTER:
			list_count_ = Mathf.FloorToInt((button_list_.Count + 1) / 4);
			break;
		default:
			list_count_ = 0;
			break;
		}
	}

	public override void Init()
	{
		float num = 0f;
		float num2 = ((button_list_.Count != 2) ? 172f : 516f);
		foreach (optionKeyConfigButton item in button_list_)
		{
			item.InitButton(sprite_prefab_, button_list_.Count > 3);
			item.transform.localPosition = new Vector3(num, 0f, 0f);
			num += num2;
		}
		if (item_bg_.sprite_renderer_ != null)
		{
			item_bg_.load("/menu/option/", "option_list_bg_3");
			item_bg_.sprite_renderer_.enabled = false;
			option_title_.color = Color.white;
		}
		if (ander_line_.sprite_renderer_ != null)
		{
			ander_line_.load("/menu/option/", "option_hr");
			ander_line_.sprite_renderer_.enabled = true;
			option_title_.color = Color.white;
			ander_line_.sprite_renderer_.size = new Vector2(1000f, 2f);
		}
	}

	public override void SelectEntry()
	{
		button_list_[list_count_].SelectEntry();
		for (int i = 0; i < button_list_.Count; i++)
		{
			if (i != list_count_)
			{
				button_list_[i].SelectExit();
			}
		}
	}

	public override void SelectExit()
	{
		foreach (optionKeyConfigButton item in button_list_)
		{
			item.SelectExit();
		}
		list_count_ = 0;
	}

	public override void ChangeItemBg(int sprite_no)
	{
		button_list_[list_count_].ChangeItemBg(sprite_no);
		for (int i = 0; i < button_list_.Count; i++)
		{
			if (i != list_count_)
			{
				button_list_[i].ChangeItemBg(0);
			}
		}
		if (item_bg_.sprite_renderer_ != null)
		{
			if (sprite_no != 0)
			{
				item_bg_.sprite_renderer_.enabled = true;
				option_title_.color = Color.black;
			}
			else
			{
				item_bg_.sprite_renderer_.enabled = false;
				option_title_.color = Color.white;
			}
		}
	}

	public override void ChangeValue(int val)
	{
		list_count_ += val;
		soundCtrl.instance.PlaySE(42);
		if (button_list_.Count <= list_count_)
		{
			list_count_ = 0;
		}
		else if (list_count_ < 0)
		{
			list_count_ = button_list_.Count - 1;
		}
		SelectEntry();
		ChangeItemBg(1);
	}

	public override void Close()
	{
		list_count_ = 0;
		foreach (optionKeyConfigButton item in button_list_)
		{
			item.Close();
		}
	}

	public override void DefaultValueSet()
	{
		foreach (optionKeyConfigButton item in button_list_)
		{
			item.DefaultValueSet();
		}
	}

	public override bool ConfirmChange()
	{
		foreach (optionKeyConfigButton item in button_list_)
		{
			if (item.ConfirmChange())
			{
				return true;
			}
		}
		return false;
	}

	public override void InitValueSet()
	{
		list_count_ = 0;
		foreach (optionKeyConfigButton item in button_list_)
		{
			if (item.ConfirmChange())
			{
				item.InitValueSet();
			}
		}
	}

	public override void PlayDecide()
	{
		base.play_decide = true;
		button_list_[list_count_].PlayDecide();
		StartCoroutine(play_config_wait());
	}

	private IEnumerator play_config_wait()
	{
		while (button_list_[list_count_].play_decide)
		{
			yield return null;
		}
		base.play_decide = false;
	}

	public override bool SelectDecision()
	{
		return true;
	}
}
