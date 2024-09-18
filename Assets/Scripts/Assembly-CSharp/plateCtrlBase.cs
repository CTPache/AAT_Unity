using System;
using System.Collections;
using UnityEngine;

public class plateCtrlBase : MonoBehaviour
{
	[Serializable]
	public class ItemCurve
	{
		public AnimationCurve in_ = new AnimationCurve();

		public AnimationCurve out_x_ = new AnimationCurve();

		public AnimationCurve out_y_ = new AnimationCurve();
	}

	public const ushort WIN_ITEM_COLOR_MASK = 2;

	[SerializeField]
	private AssetBundleSprite icon_;

	[SerializeField]
	private AssetBundleSprite icon_base_;

	[SerializeField]
	private ItemCurve item_curve_ = new ItemCurve();

	[SerializeField]
	private GameObject body_;

	[SerializeField]
	private Material default_material_;

	[SerializeField]
	private Material grayscale_material_;

	private bool is_play_;

	private IEnumerator enumerator_open_;

	private IEnumerator enumerator_close_;

	public bool body_active
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

	public int open_type { get; private set; }

	public int id { get; private set; }

	public bool is_play
	{
		get
		{
			return is_play_;
		}
	}

	public virtual void load()
	{
		icon_base_.load("/menu/common/", "evidence_00");
		icon_base_.spriteNo(0);
	}

	public virtual void init()
	{
		id = -1;
		load();
	}

	public virtual void entryItem(int in_id)
	{
		id = in_id;
	}

	protected void loadItem(string in_path, string in_name)
	{
		icon_.load(in_path, in_name);
		icon_.spriteNo(1);
	}

	public virtual void openItem(int in_type, float in_wait, bool immediate = false)
	{
		open_type = in_type;
		stopItem();
		enumerator_open_ = CoroutineOpen(in_type, in_wait, immediate);
		coroutineCtrl.instance.Play(enumerator_open_);
	}

	public virtual void closeItem(bool immediate = false)
	{
		id = -1;
		stopItem();
		enumerator_close_ = CoroutineClose(immediate);
		coroutineCtrl.instance.Play(enumerator_close_);
	}

	private IEnumerator CoroutineClose(bool immediate)
	{
		if (immediate)
		{
			float x = item_curve_.out_x_.Evaluate(1f);
			float y = item_curve_.out_y_.Evaluate(1f);
			body_.transform.localScale = new Vector3(x, y, 1f);
		}
		else
		{
			float time = 0f;
			while (time < 1f)
			{
				time += 0.2f;
				if (time > 1f)
				{
					time = 1f;
				}
				float x2 = item_curve_.out_x_.Evaluate(time);
				float y2 = item_curve_.out_y_.Evaluate(time);
				body_.transform.localScale = new Vector3(x2, y2, 1f);
				yield return null;
			}
		}
		body_active = false;
		is_play_ = false;
		enumerator_close_ = null;
	}

	private void stopItem()
	{
		if (enumerator_open_ != null)
		{
			coroutineCtrl.instance.Stop(enumerator_open_);
			enumerator_open_ = null;
		}
		if (enumerator_close_ != null)
		{
			coroutineCtrl.instance.Stop(enumerator_close_);
			enumerator_close_ = null;
		}
	}

	private IEnumerator CoroutineOpen(int in_type, float in_wait, bool immediate)
	{
		body_active = true;
		is_play_ = true;
		if ((in_type & 1) == 0)
		{
			body_.transform.localPosition = new Vector3(-660f, 210f, 0f);
		}
		else
		{
			body_.transform.localPosition = new Vector3(660f, 210f, 0f);
		}
		if (GSStatic.global_work_.message_active_window == 0 && IsItemMonochrom())
		{
			SetMaterial(grayscale_material_);
			SetSpefVolume();
		}
		else
		{
			SetMaterial(default_material_);
		}
		if (immediate)
		{
			float num = item_curve_.in_.Evaluate(1f);
			body_.transform.localScale = new Vector3(num, num, 1f);
		}
		else
		{
			float time = 0f;
			while (time < 1f)
			{
				time += 0.2f;
				if (time > 1f)
				{
					time = 1f;
				}
				float num2 = item_curve_.in_.Evaluate(time);
				body_.transform.localScale = new Vector3(num2, num2, 1f);
				yield return null;
			}
		}
		enumerator_open_ = null;
	}

	public virtual void LoadItem()
	{
	}

	public bool IsItemMonochrom()
	{
		switch (GSStatic.global_work_.title)
		{
		case TitleId.GS1:
			return GS1_IsItemMonochrome();
		case TitleId.GS2:
			return GS2_IsItemMonochrome();
		case TitleId.GS3:
			return GS3_IsItemMonochrome();
		default:
			return false;
		}
	}

	private bool GS1_IsItemMonochrome()
	{
		if (GSStatic.global_work_.SpEf_status == 7 || GSStatic.global_work_.SpEf_status == 3)
		{
			return true;
		}
		if (AnimationSystem.Instance.IsCharaMonochrom())
		{
			return true;
		}
		return false;
	}

	private bool GS2_IsItemMonochrome()
	{
		bool result = false;
		if (GSStatic.global_work_.SpEf_status == 7 || GSStatic.global_work_.SpEf_status == 3)
		{
			result = true;
		}
		if (AnimationSystem.Instance.IsCharaMonochrom())
		{
			result = true;
		}
		if (GSStatic.global_work_.scenario == 14 && GSStatic.message_work_.now_no == 184 && id == scenario_GS2.NOTE_RECEIVER1)
		{
			result = false;
		}
		return result;
	}

	private bool GS3_IsItemMonochrome()
	{
		return (open_type & 2) != 0;
	}

	public void SetMaterial(Material mat)
	{
		icon_.sprite_renderer_.material = mat;
		icon_base_.sprite_renderer_.material = mat;
	}

	public void SetSpefVolume()
	{
		MaterialPropertyBlock materialPropertyBlock = new MaterialPropertyBlock();
		icon_.sprite_renderer_.GetPropertyBlock(materialPropertyBlock);
		materialPropertyBlock.SetFloat(spefCtrl.instance.volumePropetyId, 1f);
		icon_.sprite_renderer_.SetPropertyBlock(materialPropertyBlock);
		icon_base_.sprite_renderer_.GetPropertyBlock(materialPropertyBlock);
		materialPropertyBlock.SetFloat(spefCtrl.instance.volumePropetyId, 1f);
		icon_base_.sprite_renderer_.SetPropertyBlock(materialPropertyBlock);
	}
}
