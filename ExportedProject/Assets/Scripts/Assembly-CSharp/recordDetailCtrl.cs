using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class recordDetailCtrl : MonoBehaviour
{
	private bool arrow_touch;

	private bool is_reverse_;

	private int bg_no;

	private bool item_active;

	private bool message_active;

	private keyGuideBase.Type standby_keyguid_type_;

	[SerializeField]
	private AnimationCurve in_ = new AnimationCurve();

	[SerializeField]
	private AnimationCurve out_ = new AnimationCurve();

	[SerializeField]
	private AssetBundleSprite detail_sprite_;

	[SerializeField]
	private AssetBundleSprite fade_mask_;

	[SerializeField]
	private arrowCtrl arrow_ctrl_;

	[SerializeField]
	private GameObject movie_player_;

	[SerializeField]
	private InputTouch touch_;

	private piceDetailData detailData_;

	private string dataPath_ = string.Empty;

	private int data_id_;

	private List<piceDetailData> data_list_ = new List<piceDetailData>();

	private bool is_open_;

	public Vector3 detail_sprite_pos
	{
		get
		{
			return detail_sprite_.transform.localPosition;
		}
	}

	public bool is_open
	{
		get
		{
			return is_open_;
		}
	}

	public GameObject movie_player
	{
		get
		{
			return movie_player_;
		}
		set
		{
			movie_player_ = value;
		}
	}

	public IEnumerator CoroutineViewDetail(int in_id)
	{
		is_open_ = true;
		is_reverse_ = false;
		data_id_ = in_id;
		data_list_ = piceDataCtrl.instance.status_ext_bg_tbl;
		detailData_ = data_list_[data_id_];
		if (detailData_.page_num != 0)
		{
			for (int i = data_id_ + 1; i < data_list_.Count; i++)
			{
				if (detailData_.bg_id == data_list_[i].bg_id)
				{
					data_id_ = i;
					break;
				}
			}
		}
		standby_keyguid_type_ = keyGuideBase.Type.NO_GUIDE;
		if (keyGuideCtrl.instance.active)
		{
			standby_keyguid_type_ = keyGuideCtrl.instance.current_guide;
			keyGuideCtrl.instance.close();
		}
		detail_sprite_.active = true;
		detail_sprite_.sprite_renderer_.color = Color.white;
		fade_mask_.active = true;
		fade_mask_.sprite_renderer_.color = Color.black;
		switch (GSStatic.global_work_.title)
		{
		case TitleId.GS1:
			dataPath_ = "/GS1/BG/";
			break;
		case TitleId.GS2:
			dataPath_ = "/GS2/BG/";
			break;
		case TitleId.GS3:
			dataPath_ = "/GS3/BG/";
			break;
		default:
			dataPath_ = string.Empty;
			break;
		}
		fade_mask_.load("/menu/common/", "mask");
		ushort tmp_sw = 32;
		DyingMessageMiniGame.instance.body_active = false;
		bool select_plate_active = selectPlateCtrl.instance.body_active;
		selectPlateCtrl.instance.body_active = false;
		bool life_gauge_active = lifeGaugeCtrl.instance.body_active;
		lifeGaugeCtrl.instance.body_active = false;
		bool tantei_menu_active = tanteiMenu.instance.active;
		tanteiMenu.instance.active = false;
		message_active = messageBoardCtrl.instance.body_active;
		messageBoardCtrl.instance.board(false, false);
		bool testimony = false;
		bool top_view = CheckTopView(detailData_.bg_id);
		Color bg_color = Color.white;
		bool subsprite_enable = false;
		bool negaposi = false;
		bool negaposi_sub = false;
		Vector2 bgctrl_pos = Vector2.zero;
		List<int> seal_list = new List<int>();
		if (top_view)
		{
			detail_sprite_.active = false;
			if (GSStatic.bg_save_data.bg_black)
			{
				bg_no = 4095;
			}
			else
			{
				bg_no = bgCtrl.instance.bg_no;
			}
			subsprite_enable = bgCtrl.instance.GetChangeSubSpriteEnable();
			bg_color = bgCtrl.instance.getColor();
			tmp_sw = bgCtrl.instance.current_sw;
			bgctrl_pos.x = bgCtrl.instance.bg_pos_x;
			bgctrl_pos.y = bgCtrl.instance.bg_pos_y;
			testimony = TestimonyRoot.instance.TestimonyIconEnabled;
			negaposi = GSStatic.bg_save_data.negaposi;
			negaposi_sub = GSStatic.bg_save_data.negaposi_sub;
			if (GSStatic.global_work_.title == TitleId.GS1 && (long)bg_no == 159)
			{
				seal_list = bgCtrl.instance.GetSealIDList();
			}
			TestimonyRoot.instance.TestimonyIconEnabled = false;
			bgCtrl.instance.bg_no = 4095;
			bgCtrl.instance.SetSprite((int)detailData_.bg_id);
			bgCtrl.instance.Bg256_monochrome(0, 32, 32, true);
			bgCtrl.instance.setSpritePosZ(-50f);
			bgCtrl.instance.bg_pos_x = 0f;
			recordSealSet(1u);
			item_active = false;
			if (itemPlateCtrl.instance.body_active)
			{
				item_active = itemPlateCtrl.instance.body_active;
				itemPlateCtrl.instance.body_active = false;
			}
			if (movie_player_.activeSelf)
			{
				Camera component = movie_player_.GetComponent<Camera>();
				component.enabled = false;
			}
		}
		else
		{
			string bGName = bgCtrl.instance.GetBGName((int)detailData_.bg_id);
			detail_sprite_.load(dataPath_, bGName);
		}
		if (bgData.instance.data[(int)detailData_.bg_id].type_ == 1)
		{
			detail_sprite_.transform.localPosition = new Vector3(-960f, 0f, 0f);
		}
		else if (bgData.instance.data[(int)detailData_.bg_id].type_ == 2)
		{
			detail_sprite_.transform.localPosition = new Vector3(-960f, 0f, 0f);
		}
		else
		{
			detail_sprite_.transform.localPosition = new Vector3(0f, 0f, 0f);
		}
		arrow_ctrl_.load();
		arrow_ctrl_.SetTouchEventArrow(delegate
		{
			arrow_touch = true;
		});
		if (detailData_.page_num != 0)
		{
			arrow_ctrl_.arrow(true, 0);
			arrow_ctrl_.ActiveArrow();
		}
		float time2 = 0f;
		int page_num = 0;
		while (true)
		{
			time2 += 0.03f;
			if (time2 > 1f)
			{
				break;
			}
			float color4 = in_.Evaluate(time2);
			color4 = out_.Evaluate(time2);
			fade_mask_.sprite_renderer_.color = new Color(0f, 0f, 0f, color4);
			yield return null;
		}
		if (GSStatic.global_work_.title == TitleId.GS1 && detailData_.bg_id == 119)
		{
			yield return keyGuideCtrl.instance.open(keyGuideBase.Type.TOP_VIEW);
		}
		else
		{
			yield return keyGuideCtrl.instance.open(keyGuideBase.Type.DETAIL_VIEW);
		}
		while (true)
		{
			if (detailData_.page_num != 0)
			{
				if (padCtrl.instance.IsNextMove())
				{
					if (padCtrl.instance.GetKeyDown(KeyType.A) || padCtrl.instance.GetKeyDown(KeyType.StickL_Right) || padCtrl.instance.GetKeyDown(KeyType.Right) || arrow_touch || padCtrl.instance.GetWheelMoveDown())
					{
						soundCtrl.instance.PlaySE(43);
						arrow_touch = false;
						page_num++;
						if (page_num >= detailData_.page_num)
						{
							page_num = 0;
						}
						yield return StartCoroutine(ChengPage(page_num));
					}
					if (padCtrl.instance.GetKeyDown(KeyType.StickL_Left) || padCtrl.instance.GetKeyDown(KeyType.Left) || padCtrl.instance.GetWheelMoveUp())
					{
						soundCtrl.instance.PlaySE(43);
						page_num--;
						if (page_num < 0)
						{
							page_num = (int)(detailData_.page_num - 1);
						}
						yield return StartCoroutine(ChengPage(page_num));
					}
				}
				padCtrl.instance.WheelMoveValUpdate();
			}
			else if (detailData_.scroll != 0 && padCtrl.instance.GetKeyDown(KeyType.L))
			{
				soundCtrl.instance.PlaySE(43);
				yield return StartCoroutine(CoroutineSlider());
			}
			if (padCtrl.instance.GetKeyDown(KeyType.B))
			{
				break;
			}
			yield return null;
		}
		soundCtrl.instance.PlaySE(44);
		arrow_ctrl_.arrow(false, 0);
		yield return keyGuideCtrl.instance.close();
		time2 = 0f;
		while (true)
		{
			time2 += 0.03f;
			if (time2 > 1f)
			{
				break;
			}
			float color2 = out_.Evaluate(time2);
			color2 = in_.Evaluate(time2);
			fade_mask_.sprite_renderer_.color = new Color(0f, 0f, 0f, color2);
			yield return null;
		}
		if (standby_keyguid_type_ != 0)
		{
			yield return keyGuideCtrl.instance.open(standby_keyguid_type_);
		}
		detail_sprite_.sprite_renderer_.color = Color.black;
		detail_sprite_.active = false;
		fade_mask_.active = false;
		if (top_view)
		{
			recordSealSet(0u);
			TestimonyRoot.instance.TestimonyIconEnabled = testimony;
			if (bg_no != 4095)
			{
				bgCtrl.instance.bg_no = 4095;
			}
			bgCtrl.instance.SetSprite(bg_no);
			GSDemo.CheckBGChange((uint)bg_no, 1u);
			if (GSStatic.global_work_.title == TitleId.GS1 && (long)bg_no == 159)
			{
				foreach (int item in seal_list)
				{
					bgCtrl.instance.SetSeal(item);
				}
			}
			bgCtrl.instance.resetSpritePosZ();
			bgCtrl.instance.Bg256_monochrome(0, 32, tmp_sw, true);
			switch (tmp_sw)
			{
			default:
				bgCtrl.instance.setNegaPosi(negaposi, negaposi_sub);
				break;
			case 0:
			case 1:
			case 3:
			case 6:
				break;
			}
			bgCtrl.instance.setColor(bg_color);
			bgCtrl.instance.bg_pos_x = bgctrl_pos.x;
			bgCtrl.instance.bg_pos_y = bgctrl_pos.y;
			if (bgCtrl.instance.GetChangeSubSpriteEnable() != subsprite_enable)
			{
				bgCtrl.instance.ChangeSubSpriteEnable(subsprite_enable);
			}
			if (GSStatic.bg_save_data.bg_parts_enabled)
			{
				bgCtrl.instance.SetParts(GSStatic.bg_save_data.bg_parts, GSStatic.bg_save_data.bg_parts_enabled);
			}
			if (movie_player_.activeSelf)
			{
				Camera component2 = movie_player_.GetComponent<Camera>();
				component2.enabled = true;
			}
			itemPlateCtrl.instance.body_active = item_active;
		}
		if (select_plate_active)
		{
			selectPlateCtrl.instance.body_active = select_plate_active;
		}
		if (life_gauge_active)
		{
			lifeGaugeCtrl.instance.body_active = life_gauge_active;
		}
		if (tantei_menu_active)
		{
			tanteiMenu.instance.active = tantei_menu_active;
		}
		if (message_active)
		{
			messageBoardCtrl.instance.board(true, true);
		}
		DyingMessageMiniGame.instance.body_active = true;
		is_open_ = false;
	}

	private IEnumerator ChengPage(int page_num)
	{
		if (GSStatic.global_work_.title == TitleId.GS3 && detailData_.bg_id == 142)
		{
			arrow_ctrl_.arrow(false, 0);
			yield return StartCoroutine(fadeCtrl.instance.play_coroutine(30, false, Color.black));
			base.name = bgCtrl.instance.GetBGName((int)data_list_[data_id_ + page_num].bg_id);
			detail_sprite_.load(dataPath_, base.name);
			yield return StartCoroutine(fadeCtrl.instance.play_coroutine(30, true, Color.black));
			arrow_ctrl_.arrow(true, 0);
		}
		else
		{
			base.name = bgCtrl.instance.GetBGName((int)data_list_[data_id_ + page_num].bg_id);
			detail_sprite_.load(dataPath_, base.name);
		}
	}

	public void ActiveDetailTouch()
	{
		touch_.ActiveCollider();
	}

	private IEnumerator CoroutineSlider()
	{
		float bg_pos = detail_sprite_.transform.localPosition.x;
		float speed = 30f;
		float move_pos = 0f;
		if (detail_sprite_.active)
		{
			move_pos = detail_sprite_.sprite_renderer_.sprite.rect.width - 1920f;
		}
		if (!is_reverse_)
		{
			speed *= -1f;
			move_pos *= -1f;
		}
		if (detailData_.bg_id == 119 || detailData_.bg_id == 159)
		{
			bgCtrl.instance.Scroll(speed, 0f);
			while (bgCtrl.instance.is_scroll)
			{
				yield return null;
			}
			is_reverse_ = !is_reverse_;
			yield break;
		}
		float end_pos = bg_pos + move_pos;
		while (true)
		{
			bg_pos += speed;
			if ((speed > 0f && bg_pos >= end_pos) || (speed < 0f && bg_pos <= end_pos))
			{
				break;
			}
			detail_sprite_.transform.localPosition = new Vector3(bg_pos, 0f, 0f);
			yield return null;
		}
		detail_sprite_.transform.localPosition = new Vector3(end_pos, 0f, 0f);
		is_reverse_ = !is_reverse_;
	}

	private void recordSealSet(uint disp_type)
	{
		switch (GSStatic.global_work_.title)
		{
		case TitleId.GS1:
			if (data_list_[data_id_].bg_id == 119)
			{
				if ((GSStatic.global_work_.scenario == 20 && GSFlag.Check(0u, scenario.SCE41_FLAG_MES61280)) || (uint)GSStatic.global_work_.scenario >= 21u)
				{
				}
				if (GSFlag.Check(0u, scenario.SCE42_FLAG_BLOODSTAIN01_SET) || (uint)GSStatic.global_work_.scenario >= 25u)
				{
					GSFlag.Set(0u, scenario.SCE42_FLAG_BLOODSTAIN01_DISP, disp_type);
				}
				if (GSFlag.Check(0u, scenario.SCE42_FLAG_BLOODSTAIN02_SET))
				{
					GSFlag.Set(0u, scenario.SCE42_FLAG_BLOODSTAIN02_DISP, disp_type);
				}
				if (disp_type == 1)
				{
					GS1_BGChange.Sce04_bg003change();
					GS1_BGChange.Sce04_bg002change();
				}
			}
			else if (detailData_.bg_id == 159)
			{
				if (GSFlag.Check(0u, scenario.SCE43_FLAG_BG_TAIHOKUN_SET) || (uint)GSStatic.global_work_.scenario >= 28u)
				{
					GSFlag.Set(0u, scenario.SCE43_FLAG_BG_TAIHOKUN_DISP, disp_type);
				}
				if (GSFlag.Check(0u, scenario.SCE42_FLAG_BLOODSTAIN03_SET) || (uint)GSStatic.global_work_.scenario >= 25u)
				{
					GSFlag.Set(0u, scenario.SCE42_FLAG_BLOODSTAIN03_DISP, disp_type);
				}
				if (GSFlag.Check(0u, scenario.SCE42_FLAG_BLOODSTAIN05_SET) || (uint)GSStatic.global_work_.scenario >= 25u)
				{
					GSFlag.Set(0u, scenario.SCE42_FLAG_BLOODSTAIN05_DISP, disp_type);
				}
				if (GSFlag.Check(0u, scenario.SCE42_FLAG_BLOODSTAIN08_SET) || (uint)GSStatic.global_work_.scenario >= 25u)
				{
					GSFlag.Set(0u, scenario.SCE42_FLAG_BLOODSTAIN08_DISP, disp_type);
				}
				if (disp_type == 1)
				{
					GSDemo.CheckBGChange(detailData_.bg_id, 2u);
				}
			}
			break;
		case TitleId.GS2:
			if (disp_type == 1)
			{
				GS2_BGChange.GS2_CheckBGChange(detailData_.bg_id, 2u);
			}
			break;
		case TitleId.GS3:
			if (disp_type == 1)
			{
				GS3_BGChange.GS3_CheckBGChange(detailData_.bg_id, 2u);
			}
			break;
		}
	}

	private bool CheckTopView(uint bg_no)
	{
		switch (GSStatic.global_work_.title)
		{
		case TitleId.GS1:
			return CheckTopViewGs1(bg_no);
		case TitleId.GS2:
			return CheckTopViewGs2(bg_no);
		case TitleId.GS3:
			return CheckTopViewGs3(bg_no);
		default:
			return false;
		}
	}

	private bool CheckTopViewGs1(uint bg_no)
	{
		bool result = false;
		if (bg_no == 15 || bg_no == 119 || bg_no == 159)
		{
			result = true;
		}
		return result;
	}

	private bool CheckTopViewGs2(uint bg_no)
	{
		bool result = false;
		if (bg_no == 42)
		{
			result = true;
		}
		return result;
	}

	private bool CheckTopViewGs3(uint bg_no)
	{
		bool result = false;
		switch (bg_no)
		{
		case 36u:
		case 39u:
		case 41u:
		case 42u:
		case 47u:
		case 48u:
		case 49u:
		case 50u:
		case 51u:
		case 112u:
		case 113u:
		case 114u:
		case 115u:
		case 116u:
		case 124u:
		case 125u:
		case 127u:
		case 128u:
		case 129u:
			result = true;
			break;
		}
		return result;
	}
}
