using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class optionCredit : optionItem
{
	[Serializable]
	public class OptionSelectCredit
	{
		public AssetBundleSprite select_window_;

		public AssetBundleSprite cursor_;

		public AssetBundleSprite enable_;

		public Text text_;
	}

	[SerializeField]
	protected OptionSelectCredit select_;

	[SerializeField]
	private selectPlateCtrl.EnableCurve enable_curve_;

	public override void Init()
	{
		base.Init();
		SetText(TextDataCtrl.GetText(TextDataCtrl.OptionTextID.ITEM_CREDITS));
		select_.select_window_.load("/menu/common/", "select_window");
		select_.select_window_.spriteNo(2);
		select_.cursor_.active = true;
		select_.cursor_.load("/menu/common/", "select_window");
		select_.cursor_.spriteNo(0);
		select_.cursor_.active = false;
		select_.enable_.load("/menu/common/", "select_window");
		select_.enable_.spriteNo(5);
		Color color = select_.enable_.color;
		select_.enable_.color = new Color(color.r, color.g, color.b, 0f);
		foreach (InputTouch item in touch_list_)
		{
			item.touch_key_type = KeyType.A;
		}
		mainCtrl.instance.addText(select_.text_);
		select_.text_.text = TextDataCtrl.GetText(TextDataCtrl.OptionTextID.ITEM_CREDITS);
	}

	public override void SelectEntry()
	{
		base.SelectEntry();
		select_.cursor_.active = true;
	}

	public override void SelectExit()
	{
		base.SelectExit();
		select_.cursor_.active = false;
	}

	public override bool SelectDecision()
	{
		return true;
	}

	public override void Close()
	{
		base.Close();
		mainCtrl.instance.removeText(select_.text_);
	}

	public override void OnTouch(TouchParameter touch_param)
	{
		SelectEntry();
	}

	public override void InitValueSet()
	{
		base.InitValueSet();
		Color color = select_.enable_.color;
		select_.enable_.color = new Color(color.r, color.g, color.b, 0f);
	}

	public override void PlayDecide()
	{
		base.PlayDecide();
		base.play_decide = true;
		coroutineCtrl.instance.Play(PlayDecideInner());
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
			float num = enable_curve_.cursor_.Evaluate(timer);
			select_.cursor_.transform.localScale = new Vector3(num, num, 1f);
			float num2 = enable_curve_.select_.Evaluate(timer);
			select_.select_window_.transform.localScale = new Vector3(num2, num2, 1f);
			float a = enable_curve_.enable_.Evaluate(timer);
			Color color = select_.enable_.color;
			select_.enable_.color = new Color(color.r, color.g, color.b, a);
			yield return null;
		}
		base.play_decide = false;
	}
}
