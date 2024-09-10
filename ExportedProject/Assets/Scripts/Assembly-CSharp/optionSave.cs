using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class optionSave : optionItem
{
	[Serializable]
	public class OptionSelectPlate
	{
		public AssetBundleSprite select_;

		public AssetBundleSprite enable_;

		public Text text_;

		public bool active
		{
			get
			{
				return select_.active;
			}
			set
			{
				select_.active = value;
			}
		}
	}

	private int cursor_num_;

	[SerializeField]
	private List<OptionSelectPlate> select_plate_ = new List<OptionSelectPlate>();

	[SerializeField]
	private AssetBundleSprite cursor_;

	[SerializeField]
	private selectPlateCtrl.EnableCurve enable_curve_;

	public override void Init()
	{
		base.Init();
		foreach (OptionSelectPlate item in select_plate_)
		{
			mainCtrl.instance.addText(item.text_);
		}
		SetText(TextDataCtrl.GetText(TextDataCtrl.OptionTextID.ITEM_SAVE));
		select_plate_[0].active = true;
		select_plate_[0].select_.load("/menu/common/", "select_button");
		select_plate_[0].select_.spriteNo(2);
		select_plate_[0].enable_.load("/menu/common/", "select_button");
		select_plate_[0].enable_.spriteNo(3);
		Color color = select_plate_[0].enable_.color;
		select_plate_[0].enable_.color = new Color(color.r, color.g, color.b, 0f);
		select_plate_[0].text_.text = TextDataCtrl.GetText(TextDataCtrl.OptionTextID.SELECT_SAVE);
		select_plate_[1].active = true;
		select_plate_[1].select_.load("/menu/common/", "select_button");
		select_plate_[1].select_.spriteNo(2);
		select_plate_[1].enable_.load("/menu/common/", "select_button");
		select_plate_[1].enable_.spriteNo(3);
		color = select_plate_[0].enable_.color;
		select_plate_[1].enable_.color = new Color(color.r, color.g, color.b, 0f);
		select_plate_[1].text_.text = TextDataCtrl.GetText(TextDataCtrl.OptionTextID.SELECT_LOAD);
		foreach (InputTouch item2 in touch_list_)
		{
			item2.touch_key_type = KeyType.A;
		}
		cursor_num_ = 0;
		cursor_.active = true;
		cursor_.load("/menu/common/", "select_button");
		cursor_.spriteNo(0);
		cursor_.transform.localPosition = select_plate_[cursor_num_].select_.transform.localPosition;
		cursor_.active = false;
	}

	public override void SelectEntry()
	{
		base.SelectEntry();
		cursor_num_ = 0;
		cursor_.transform.localPosition = select_plate_[cursor_num_].select_.transform.localPosition;
		cursor_.active = true;
	}

	public override void SelectExit()
	{
		base.SelectExit();
		cursor_.active = false;
	}

	public override void ChangeValue(int val)
	{
		soundCtrl.instance.PlaySE(42);
		cursor_num_ += val;
		cursor_num_ = ((cursor_num_ >= 0) ? cursor_num_ : (select_plate_.Count - 1));
		cursor_num_ = ((cursor_num_ < select_plate_.Count) ? cursor_num_ : 0);
		cursor_.transform.localPosition = select_plate_[cursor_num_].select_.transform.localPosition;
	}

	public override bool SelectDecision()
	{
		return true;
	}

	public override void Close()
	{
		base.Close();
		foreach (OptionSelectPlate item in select_plate_)
		{
			mainCtrl.instance.removeText(item.text_);
		}
	}

	public override void OnTouch(TouchParameter touch_param)
	{
		SelectEntry();
		optionInfo optionInfo2 = touch_param.argument_parameter as optionInfo;
		int num = optionInfo2.index_ - 1;
		cursor_num_ = num;
		cursor_.transform.localPosition = select_plate_[cursor_num_].select_.transform.localPosition;
	}

	public override void InitValueSet()
	{
		base.InitValueSet();
		foreach (OptionSelectPlate item in select_plate_)
		{
			Color color = item.enable_.color;
			item.enable_.color = new Color(color.r, color.g, color.b, 0f);
		}
	}

	public override void PlayDecide()
	{
		base.PlayDecide();
		base.play_decide = true;
		StartCoroutine(PlayDecideInner());
	}

	private IEnumerator PlayDecideInner()
	{
		float timer = 0f;
		while (timer < 1f)
		{
			timer += 0.1f;
			if (timer > 1f)
			{
				timer = 1f;
			}
			OptionSelectPlate select = select_plate_[cursor_num_];
			float num = enable_curve_.cursor_.Evaluate(timer);
			cursor_.transform.localScale = new Vector3(num, num, 1f);
			float num2 = enable_curve_.select_.Evaluate(timer);
			select.select_.transform.localScale = new Vector3(num2, num2, 1f);
			float a = enable_curve_.enable_.Evaluate(timer);
			Color color = select.enable_.color;
			select.enable_.color = new Color(color.r, color.g, color.b, a);
			yield return null;
		}
		if (cursor_num_ == 0)
		{
			SaveLoadUICtrl.instance.Open(SaveLoadUICtrl.SlotType.SAVE);
		}
		else
		{
			SaveLoadUICtrl.instance.Open(SaveLoadUICtrl.SlotType.LOAD);
		}
		base.play_decide = false;
	}
}
