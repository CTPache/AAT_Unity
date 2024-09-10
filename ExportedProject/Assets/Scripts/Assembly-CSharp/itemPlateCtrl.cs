using System.Collections.Generic;
using UnityEngine;

public class itemPlateCtrl : plateCtrlBase
{
	private static itemPlateCtrl instance_;

	public AssetBundleSprite sStatusEffectSprite;

	[SerializeField]
	private AssetBundleSprite sStatusEffectSprite_bg_;

	public static itemPlateCtrl instance
	{
		get
		{
			return instance_;
		}
	}

	private void Awake()
	{
		if (instance_ == null)
		{
			instance_ = this;
		}
	}

	public override void init()
	{
		base.init();
	}

	public override void entryItem(int in_id)
	{
		base.entryItem(in_id);
		GSStatic.message_work_.Item_id = (byte)base.id;
		List<piceData> note_data = piceDataCtrl.instance.note_data;
		if (in_id < note_data.Count)
		{
			string iconItFile = piceDataCtrl.instance.GetIconItFile(note_data[in_id].no);
			if (iconItFile == string.Empty)
			{
				Debug.LogWarning(string.Empty);
			}
			else
			{
				loadItem(note_data[in_id].path, iconItFile);
			}
		}
	}

	public override void openItem(int in_type, float in_wait, bool immediate = false)
	{
		base.openItem(in_type, in_wait, immediate);
		GSStatic.message_work_.Item_open_type = (short)base.open_type;
	}

	public override void closeItem(bool immediate = false)
	{
		base.closeItem(immediate);
		GSStatic.message_work_.Item_open_type = -1;
	}

	public override void LoadItem()
	{
		if (GSStatic.message_work_.Item_open_type >= 0)
		{
			entryItem(GSStatic.message_work_.Item_id);
			openItem(GSStatic.message_work_.Item_open_type, 0f, true);
		}
	}

	public void Load_sStatusEffectSprite()
	{
		string iconItFile = piceDataCtrl.instance.GetIconItFile(6);
		sStatusEffectSprite.load(piceDataCtrl.instance.note_data[6].path, iconItFile);
		sStatusEffectSprite.active = true;
		sStatusEffectSprite.transform.localRotation = Quaternion.identity;
		sStatusEffectSprite.transform.localScale = Vector3.one;
		sStatusEffectSprite.transform.localPosition = new Vector3(0f, 210f, -5f);
		sStatusEffectSprite_bg_.load("/menu/common/", "evidence_base");
	}

	public void Terminate_sStatusEffectSprite()
	{
		sStatusEffectSprite_bg_.sprite_renderer_.sprite = null;
		sStatusEffectSprite_bg_.sprite_data_.Clear();
		sStatusEffectSprite_bg_.asset_bundle_ = null;
		sStatusEffectSprite.sprite_renderer_.sprite = null;
		sStatusEffectSprite.active = false;
		sStatusEffectSprite.remove();
		sStatusEffectSprite.end();
	}
}
