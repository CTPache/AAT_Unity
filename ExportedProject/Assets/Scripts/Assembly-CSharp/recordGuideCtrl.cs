using System.Collections.Generic;
using UnityEngine;

public class recordGuideCtrl : guideCtrl
{
	private int cnt;

	[SerializeField]
	private GameObject left_end_guide_;

	[SerializeField]
	private float page_guide_space_;

	[SerializeField]
	private List<AssetBundleSprite> page_guide_list_ = new List<AssetBundleSprite>();

	[SerializeField]
	private SpriteRenderer guide_bg_var_;

	private float guide_offset_x = 26f;

	public override void load()
	{
		base.load();
		guide_offset_x = ((GSStatic.global_work_.language != 0) ? 26f : 0f);
		string in_name = "court_record_02" + GSUtility.GetPlatformResourceName() + ((GSStatic.global_work_.language != 0) ? "u" : string.Empty);
		foreach (AssetBundleSprite item in page_guide_list_)
		{
			item.load("/menu/common/", in_name);
			item.spriteNo(1);
			item.active = false;
		}
		guide_bg_var_.sprite = Sprite.Create(guide_bg_var_.sprite.texture, guide_bg_var_.sprite.rect, Vector2.up * 0.5f, guide_bg_var_.sprite.pixelsPerUnit);
	}

	public override void init()
	{
		base.init();
	}

	public override void Close()
	{
		base.Close();
		foreach (AssetBundleSprite item in page_guide_list_)
		{
			item.active = false;
		}
	}

	public void pageGuideSet(int page_cnt)
	{
		float num = (0f - page_guide_space_) * (float)(page_cnt / 2);
		if (page_cnt % 2 == 0)
		{
			num += page_guide_space_ / 2f;
		}
		for (int i = 0; i < page_cnt; i++)
		{
			page_guide_list_[i].active = true;
			page_guide_list_[i].spriteNo(1);
			page_guide_list_[i].transform.localPosition = new Vector3(num + (float)i * page_guide_space_, 0f, 0f);
		}
		pageChange(0);
	}

	public void pageGuideReset()
	{
		foreach (AssetBundleSprite item in page_guide_list_)
		{
			item.active = false;
		}
	}

	public void pageChange(int page_num)
	{
		for (int i = 0; i < page_guide_list_.Count; i++)
		{
			if (i == page_num)
			{
				page_guide_list_[i].spriteNo(1);
			}
			else
			{
				page_guide_list_[i].spriteNo(0);
			}
		}
	}

	public bool uniqueGuideSet(int no)
	{
		GlobalWork global_work_ = GSStatic.global_work_;
		SubWindow sub_window_ = advCtrl.instance.sub_window_;
		if (GSStatic.global_work_.title == TitleId.GS1 && no == 144 && SubWindow_Status.CheckFukitukeru(sub_window_) == 1 && global_work_.r.no_3 != 1 && global_work_.r.no_3 != 2)
		{
			base.guideIconSet(false, GuideType.Luminol);
			bgWidthSet(3);
			return true;
		}
		return false;
	}

	public override void ReLoadGuid()
	{
		SymbolLoad();
		base.guideIconSet(false, base.current_guide);
		bgWidthSet(cnt);
	}

	public void recordGuideSet(int record_type)
	{
		if (GSStatic.global_work_.title == TitleId.GS1 && advCtrl.instance.sub_window_.tutorial_ != 0)
		{
			Close();
			return;
		}
		int num = 0;
		if ((GSStatic.global_work_.status_flag & 0x300u) != 0 || advCtrl.instance.sub_window_.GetCurrentRoutine().r.no_0 == 16)
		{
			if (record_type == 0)
			{
				base.guideIconSet(false, GuideType.FORCE_RECORD);
			}
			else
			{
				base.guideIconSet(false, GuideType.FORCE_PROFILE);
			}
			num = 2;
			if (GSStatic.global_work_.title == TitleId.GS1)
			{
				num--;
			}
		}
		else if (GSStatic.global_work_.r_bk.no_0 == 7 && GSStatic.global_work_.r.no_3 == 1)
		{
			if (record_type == 0)
			{
				base.guideIconSet(false, GuideType.QUESTIONING_RECORD);
			}
			else
			{
				base.guideIconSet(false, GuideType.QUESTIONING_PROFILE);
			}
			num = 3;
			if (GSStatic.global_work_.title == TitleId.GS1)
			{
				num--;
			}
		}
		else if (GSStatic.global_work_.r_bk.no_0 == 5 && GSStatic.global_work_.r.no_3 == 2)
		{
			if (record_type == 0)
			{
				base.guideIconSet(false, GuideType.SHOW_RECORD);
			}
			else
			{
				base.guideIconSet(false, GuideType.SHOW_PROFILE);
			}
			num = 3;
			if (GSStatic.global_work_.title == TitleId.GS1)
			{
				num--;
			}
		}
		else
		{
			if (record_type == 0)
			{
				base.guideIconSet(false, GuideType.RECORD);
			}
			else
			{
				base.guideIconSet(false, GuideType.PROFILE);
			}
			num = ((advCtrl.instance.sub_window_.status_force_ == 1) ? 1 : 2);
		}
		bgWidthSet(num);
	}

	private void bgWidthSet(int guide_cnt)
	{
		cnt = guide_cnt;
		Vector2 in_size = new Vector2((float)guide_cnt * base.guide_width_ + left_end_guide_.transform.localPosition.x, guide_bg_var_.size.y);
		bgWidthSet(in_size);
		guide_bg_var_.transform.localPosition = new Vector3(slide_out_pos_x_ - in_size.x + guide_offset_x, guide_pos_y_, -10f);
	}

	private void bgWidthSet(Vector2 in_size)
	{
		Vector2[] array = new Vector2[4];
		ushort[] array2 = new ushort[6];
		array[0] = new Vector2(0f, 0f);
		array[1] = new Vector2(0f, in_size.y);
		array[2] = new Vector2(in_size.x, 0f);
		array[3] = new Vector2(in_size.x, in_size.y);
		array2[0] = 0;
		array2[1] = 1;
		array2[2] = 2;
		array2[3] = 1;
		array2[4] = 2;
		array2[5] = 3;
		guide_bg_var_.sprite.OverrideGeometry(array, array2);
	}
}
