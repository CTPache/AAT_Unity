using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class optionItem : MonoBehaviour
{
	public int old_value_;

	[SerializeField]
	private GameObject body_;

	[SerializeField]
	protected AssetBundleSprite item_bg_;

	[SerializeField]
	protected Text option_title_;

	[SerializeField]
	protected List<InputTouch> touch_list_ = new List<InputTouch>();

	public List<InputTouch> touch_list
	{
		get
		{
			return touch_list_;
		}
	}

	public int option_no_ { get; private set; }

	public bool active
	{
		get
		{
			return body_.activeSelf;
		}
		set
		{
			body_.SetActive(value);
		}
	}

	public bool play_decide { get; protected set; }

	public virtual void Init()
	{
		item_bg_.load("/menu/option/", "option_list_bg");
		item_bg_.spriteNo(0);
		option_title_.color = Color.white;
		mainCtrl.instance.addText(option_title_);
	}

	public virtual optionCtrl.CurrentPoint GetCurrentPoint()
	{
		return optionCtrl.CurrentPoint.NONE;
	}

	public virtual void SetCurrentPoint(optionCtrl.CurrentPoint point)
	{
	}

	public virtual void ChangeItemBg(int sprite_no)
	{
		item_bg_.spriteNo(sprite_no);
	}

	public virtual void ChangeValue(int val)
	{
	}

	public virtual void SelectEntry()
	{
		option_title_.color = Color.black;
	}

	public virtual void SelectExit()
	{
		option_title_.color = Color.white;
	}

	public virtual bool SelectDecision()
	{
		return false;
	}

	public virtual void SetText(string text)
	{
		option_title_.text = text;
	}

	public virtual void Close()
	{
		mainCtrl.instance.removeText(option_title_);
	}

	public virtual void InitValueSet()
	{
	}

	public virtual void DefaultValueSet()
	{
	}

	public virtual void OnTouch(TouchParameter touch_param)
	{
	}

	public virtual bool ConfirmChange()
	{
		return false;
	}

	public virtual void PlayDecide()
	{
	}
}
