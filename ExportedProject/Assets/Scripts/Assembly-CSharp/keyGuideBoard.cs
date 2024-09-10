using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class keyGuideBoard : keyGuideBase
{
	private Vector2 ROT_GUDE_TOUCH_OFFSET = new Vector2(-10f, 0f);

	private Vector2 ROT_GUDE_TOUCH_SIZE = new Vector2(410f, 40f);

	private Vector2 ROT_LONG_GUDE_TOUCH_OFFSET = new Vector2(5f, 0f);

	private Vector2 ROT_LONG_GUDE_TOUCH_SIZE = new Vector2(440f, 40f);

	private Vector2 NORMAL_LONG_GUDE_TOUCH_OFFSET = new Vector2(100f, 0f);

	private Vector2 NORMAL_LONG_GUDE_TOUCH_SIZE = new Vector2(250f, 40f);

	private Vector2 LAST_LONG_GUIDE_TOUCH_OFFSET = new Vector2(115f, 0f);

	private Vector2 LAST_LONG_GUIDE_TOUCH_SIZE = new Vector2(280f, 40f);

	private bool debug_is_disp_ = true;

	[SerializeField]
	private GameObject body_;

	[SerializeField]
	private Transform symbol_body_;

	[SerializeField]
	private AssetBundleSprite board_;

	[SerializeField]
	private List<AssetBundleSprite> rot_symbol_list_;

	[SerializeField]
	private Curve curve_ = new Curve();

	private Type current_guide_;

	private bool rot_union;

	private float board_width_default_ = 200f;

	private float board_pos_x_ = 860f;

	private float board_pos_y_;

	private int guide_cnt_;

	private IEnumerator enumerator_open_;

	private IEnumerator enumerator_close_;

	public Type current_guide
	{
		get
		{
			return current_guide_;
		}
	}

	protected virtual bool is_long_guide
	{
		get
		{
			return board_width_ > board_width_default_;
		}
	}

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

	public float guide_space
	{
		get
		{
			return guide_space_;
		}
	}

	protected virtual float board_width_
	{
		get
		{
			return board_width_default_;
		}
	}

	public void load()
	{
		board_.load("/menu/common/", "menu_bg");
		board_.spriteNo(0);
		board_.active = false;
		SymbolLoad();
		string in_name = "symbol" + GSUtility.GetPlatformResourceName();
		foreach (AssetBundleSprite item in rot_symbol_list_)
		{
			item.load("/menu/common/", in_name);
			item.sprite_renderer_.enabled = false;
		}
		active = false;
	}

	public override void SetGuideIconPosition(GuideIcon target, int index_num)
	{
		Vector3 localPosition = target.symbol_.transform.localPosition;
		localPosition.x = board_width_ * (float)index_num + guide_space_;
		target.symbol_.transform.localPosition = localPosition;
	}

	public void ReLoadGuid()
	{
		SymbolLoad();
		setting(current_guide_, false);
	}

	public void init()
	{
		load();
	}

	public void ActiveKeyTouch()
	{
		foreach (GuideIcon item in guide_list_)
		{
			item.touch_.ActiveCollider();
		}
	}

	public float GetGuideWidth()
	{
		int num = 0;
		foreach (GuideIcon item in guide_list_)
		{
			if (item.active)
			{
				num++;
			}
		}
		return (float)num * board_width_ + ((!rot_union) ? 0f : 138f);
	}

	protected override void NormalGuideTouchAreaUpdate(int index)
	{
		if (is_long_guide)
		{
			if (guide_list_.Count > index)
			{
				guide_list_[index].touch_.SetColliderOffset(NORMAL_LONG_GUDE_TOUCH_OFFSET);
				guide_list_[index].touch_.SetColliderSize(NORMAL_LONG_GUDE_TOUCH_SIZE);
			}
		}
		else
		{
			base.NormalGuideTouchAreaUpdate(index);
		}
	}

	protected override void LastGuideTouchAreaUpdate(int index)
	{
		if (is_long_guide)
		{
			if (guide_list_.Count > index)
			{
				guide_list_[index].touch_.SetColliderOffset(LAST_LONG_GUIDE_TOUCH_OFFSET);
				guide_list_[index].touch_.SetColliderSize(LAST_LONG_GUIDE_TOUCH_SIZE);
			}
		}
		else
		{
			base.LastGuideTouchAreaUpdate(index);
		}
	}

	private void SetRotGuideArea()
	{
		if (rot_union && !keyGuideBase.keyguid_pad_)
		{
			if (is_long_guide)
			{
				guide_list_[0].touch_.SetColliderOffset(ROT_LONG_GUDE_TOUCH_OFFSET);
				guide_list_[0].touch_.SetColliderSize(ROT_LONG_GUDE_TOUCH_SIZE);
			}
			else
			{
				guide_list_[0].touch_.SetColliderOffset(ROT_GUDE_TOUCH_OFFSET);
				guide_list_[0].touch_.SetColliderSize(ROT_GUDE_TOUCH_SIZE);
			}
		}
	}

	private void SetRotUnionGuidIcon()
	{
		if (keyGuideBase.keyguid_pad_)
		{
			guide_list_[0].SetKeyTypeSprite(KeyType.StickR_Right, true);
			guide_list_[0].touch_.touched_key_type = KeyType.None;
			guide_list_[0].touch_.touch_key_type = KeyType.None;
			guide_list_[0].text_.text = TextDataCtrl.GetText(TextDataCtrl.CommonTextID.ROTATE);
			return;
		}
		rot_union = true;
		int index = 0;
		rot_symbol_list_[index].sprite_renderer_.enabled = true;
		rot_symbol_list_[index++].spriteNo(GuideIcon.GetKeyCodeType(KeyType.StickR_Up));
		rot_symbol_list_[index].sprite_renderer_.enabled = true;
		rot_symbol_list_[index++].spriteNo(GuideIcon.GetKeyCodeType(KeyType.StickR_Down));
		rot_symbol_list_[index].sprite_renderer_.enabled = true;
		rot_symbol_list_[index++].spriteNo(GuideIcon.GetKeyCodeType(KeyType.StickR_Left));
		guide_list_[0].SetKeyTypeSprite(KeyType.StickR_Right, true);
		guide_list_[0].touch_.touched_key_type = KeyType.None;
		guide_list_[0].touch_.touch_key_type = KeyType.None;
		guide_list_[0].text_.text = TextDataCtrl.GetText(TextDataCtrl.CommonTextID.ROTATE);
	}

	public void setting(Type in_type, bool active_touch = true)
	{
		current_guide_ = in_type;
		foreach (GuideIcon item in guide_list_)
		{
			item.active = false;
			item.sprite_.active = false;
			mainCtrl.instance.addText(item.text_);
		}
		if (active_touch)
		{
			ActiveTouch();
		}
		if (!DebugDispCheck())
		{
			return;
		}
		float num = 0f;
		rot_union = false;
		foreach (AssetBundleSprite item2 in rot_symbol_list_)
		{
			item2.sprite_renderer_.enabled = false;
		}
		switch (in_type)
		{
		case Type.NO_GUIDE:
			foreach (GuideIcon item3 in guide_list_)
			{
				mainCtrl.instance.removeText(item3.text_);
			}
			board_.active = false;
			break;
		case Type.INSPECT:
			board_.active = true;
			num = -20f;
			guide_list_[0].SetKeyTypeSprite(KeyType.B);
			guide_list_[1].SetKeyTypeSprite(KeyType.Start);
			guide_list_[2].SetKeyTypeSprite(KeyType.R);
			guide_list_[0].text_.text = TextDataCtrl.GetText(TextDataCtrl.CommonTextID.BACK);
			guide_list_[1].text_.text = TextDataCtrl.GetText(TextDataCtrl.CommonTextID.OPTION);
			guide_list_[2].text_.text = TextDataCtrl.GetText(TextDataCtrl.CommonTextID.RECORD);
			break;
		case Type.MOVE:
		case Type.TANTEI_TALK:
			board_.active = true;
			guide_list_[0].SetKeyTypeSprite(KeyType.B);
			guide_list_[1].SetKeyTypeSprite(KeyType.Start);
			guide_list_[2].SetKeyTypeSprite(KeyType.R);
			guide_list_[0].text_.text = TextDataCtrl.GetText(TextDataCtrl.CommonTextID.BACK);
			guide_list_[1].text_.text = TextDataCtrl.GetText(TextDataCtrl.CommonTextID.OPTION);
			guide_list_[2].text_.text = TextDataCtrl.GetText(TextDataCtrl.CommonTextID.RECORD);
			break;
		case Type.INSPECT_SLIDE:
			num = -20f;
			board_.active = true;
			guide_list_[0].SetKeyTypeSprite(KeyType.B);
			guide_list_[1].SetKeyTypeSprite(KeyType.Start);
			guide_list_[2].SetKeyTypeSprite(KeyType.L);
			guide_list_[3].SetKeyTypeSprite(KeyType.R);
			guide_list_[0].text_.text = TextDataCtrl.GetText(TextDataCtrl.CommonTextID.BACK);
			guide_list_[1].text_.text = TextDataCtrl.GetText(TextDataCtrl.CommonTextID.OPTION);
			guide_list_[2].text_.text = TextDataCtrl.GetText(TextDataCtrl.CommonTextID.SLIDE);
			guide_list_[3].text_.text = TextDataCtrl.GetText(TextDataCtrl.CommonTextID.RECORD);
			break;
		case Type.HOUTEI:
		case Type.TANTEI:
			board_.active = true;
			guide_list_[0].SetKeyTypeSprite(KeyType.Start);
			guide_list_[1].SetKeyTypeSprite(KeyType.R);
			guide_list_[0].text_.text = TextDataCtrl.GetText(TextDataCtrl.CommonTextID.OPTION);
			guide_list_[1].text_.text = TextDataCtrl.GetText(TextDataCtrl.CommonTextID.RECORD);
			break;
		case Type.TANTEI_SLIDE:
			board_.active = true;
			guide_list_[0].SetKeyTypeSprite(KeyType.Start);
			guide_list_[1].SetKeyTypeSprite(KeyType.L);
			guide_list_[2].SetKeyTypeSprite(KeyType.R);
			guide_list_[0].text_.text = TextDataCtrl.GetText(TextDataCtrl.CommonTextID.OPTION);
			guide_list_[1].text_.text = TextDataCtrl.GetText(TextDataCtrl.CommonTextID.SLIDE);
			guide_list_[2].text_.text = TextDataCtrl.GetText(TextDataCtrl.CommonTextID.RECORD);
			break;
		case Type.QUESTIONING:
			board_.active = true;
			guide_list_[0].SetKeyTypeSprite(KeyType.Start);
			guide_list_[1].SetKeyTypeSprite(KeyType.L);
			guide_list_[2].SetKeyTypeSprite(KeyType.R);
			guide_list_[0].text_.text = TextDataCtrl.GetText(TextDataCtrl.CommonTextID.OPTION);
			guide_list_[1].text_.text = TextDataCtrl.GetText(TextDataCtrl.CommonTextID.YUSABURU);
			guide_list_[2].text_.text = TextDataCtrl.GetText(TextDataCtrl.CommonTextID.TUKITUKE);
			messageBoardCtrl.instance.InActiveNormalMessageNextTouch();
			break;
		case Type.RECORD:
			board_.active = true;
			guide_list_[0].SetKeyTypeSprite(KeyType.B);
			guide_list_[1].SetKeyTypeSprite(KeyType.Record);
			guide_list_[0].text_.text = TextDataCtrl.GetText(TextDataCtrl.CommonTextID.BACK);
			guide_list_[1].text_.text = TextDataCtrl.GetText(TextDataCtrl.CommonTextID.PROFILES);
			break;
		case Type.PROFILE:
			board_.active = true;
			guide_list_[0].SetKeyTypeSprite(KeyType.B);
			guide_list_[1].SetKeyTypeSprite(KeyType.Record);
			guide_list_[0].text_.text = TextDataCtrl.GetText(TextDataCtrl.CommonTextID.BACK);
			guide_list_[1].text_.text = TextDataCtrl.GetText(TextDataCtrl.CommonTextID.EVIDENCE);
			break;
		case Type.QUESTIONING_RECORD:
			board_.active = true;
			guide_list_[0].SetKeyTypeSprite(KeyType.B);
			guide_list_[1].SetKeyTypeSprite(KeyType.X);
			guide_list_[0].text_.text = TextDataCtrl.GetText(TextDataCtrl.CommonTextID.BACK);
			guide_list_[1].text_.text = TextDataCtrl.GetText(TextDataCtrl.CommonTextID.TUKITUKE);
			if (GSStatic.global_work_.title != 0)
			{
				guide_list_[2].SetKeyTypeSprite(KeyType.Record);
				guide_list_[2].text_.text = TextDataCtrl.GetText(TextDataCtrl.CommonTextID.EVIDENCE);
			}
			break;
		case Type.QUESTIONING_PROFILE:
			board_.active = true;
			guide_list_[0].SetKeyTypeSprite(KeyType.B);
			guide_list_[1].SetKeyTypeSprite(KeyType.X);
			guide_list_[2].SetKeyTypeSprite(KeyType.Record);
			guide_list_[0].text_.text = TextDataCtrl.GetText(TextDataCtrl.CommonTextID.BACK);
			guide_list_[1].text_.text = TextDataCtrl.GetText(TextDataCtrl.CommonTextID.TUKITUKE);
			guide_list_[2].text_.text = TextDataCtrl.GetText(TextDataCtrl.CommonTextID.EVIDENCE);
			break;
		case Type.FORCE_RECORD:
			board_.active = true;
			guide_list_[0].SetKeyTypeSprite(KeyType.X);
			guide_list_[0].text_.text = TextDataCtrl.GetText(TextDataCtrl.CommonTextID.TUKITUKE);
			if (GSStatic.global_work_.title != 0)
			{
				guide_list_[1].SetKeyTypeSprite(KeyType.Record);
				guide_list_[1].text_.text = TextDataCtrl.GetText(TextDataCtrl.CommonTextID.PROFILES);
			}
			break;
		case Type.FORCE_PROFILE:
			board_.active = true;
			guide_list_[0].SetKeyTypeSprite(KeyType.X);
			guide_list_[1].SetKeyTypeSprite(KeyType.Record);
			guide_list_[0].text_.text = TextDataCtrl.GetText(TextDataCtrl.CommonTextID.TUKITUKE);
			guide_list_[1].text_.text = TextDataCtrl.GetText(TextDataCtrl.CommonTextID.EVIDENCE);
			break;
		case Type.KAGAKU_SYOUSAI:
		{
			board_.active = true;
			SetRotUnionGuidIcon();
			int num7 = 1;
			guide_list_[num7++].SetKeyTypeSprite(KeyType.B);
			guide_list_[num7++].SetKeyTypeSprite(KeyType.X, true);
			guide_list_[num7++].SetKeyTypeSprite(KeyType.Y, true);
			num7 = 1;
			guide_list_[num7++].text_.text = TextDataCtrl.GetText(TextDataCtrl.CommonTextID.BACK);
			guide_list_[num7++].text_.text = TextDataCtrl.GetText(TextDataCtrl.CommonTextID.ZOOM_IN);
			guide_list_[num7++].text_.text = TextDataCtrl.GetText(TextDataCtrl.CommonTextID.ZOOM_OUT);
			break;
		}
		case Type.FORCE_KAGAKU_SYOUSAI:
		{
			board_.active = true;
			SetRotUnionGuidIcon();
			int num6 = 1;
			guide_list_[num6++].SetKeyTypeSprite(KeyType.X, true);
			guide_list_[num6++].SetKeyTypeSprite(KeyType.Y, true);
			num6 = 1;
			guide_list_[num6++].text_.text = TextDataCtrl.GetText(TextDataCtrl.CommonTextID.ZOOM_IN);
			guide_list_[num6++].text_.text = TextDataCtrl.GetText(TextDataCtrl.CommonTextID.ZOOM_OUT);
			break;
		}
		case Type.PRESENT_KAGAKU_SYOUSAI:
		{
			board_.active = true;
			SetRotUnionGuidIcon();
			int num5 = 1;
			guide_list_[num5++].SetKeyTypeSprite(KeyType.X);
			num5 = 1;
			guide_list_[num5++].text_.text = TextDataCtrl.GetText(TextDataCtrl.CommonTextID.TUKITUKE);
			break;
		}
		case Type.ENDING_KAGAKU_SYOUSAI:
		{
			board_.active = true;
			SetRotUnionGuidIcon();
			int num4 = 1;
			guide_list_[num4++].SetKeyTypeSprite(KeyType.X, true);
			guide_list_[num4++].SetKeyTypeSprite(KeyType.Y, true);
			num4 = 1;
			guide_list_[num4++].text_.text = TextDataCtrl.GetText(TextDataCtrl.CommonTextID.ZOOM_IN);
			guide_list_[num4++].text_.text = TextDataCtrl.GetText(TextDataCtrl.CommonTextID.ZOOM_OUT);
			break;
		}
		case Type.POT_PAZZLE:
			board_.active = true;
			guide_list_[0].SetKeyTypeSprite(KeyType.B);
			guide_list_[1].SetKeyTypeSprite(KeyType.X);
			guide_list_[2].SetKeyTypeSprite(KeyType.L);
			guide_list_[3].SetKeyTypeSprite(KeyType.Record);
			guide_list_[0].text_.text = TextDataCtrl.GetText(TextDataCtrl.CommonTextID.BACK);
			guide_list_[1].text_.text = TextDataCtrl.GetText(TextDataCtrl.CommonTextID.COMBINE);
			guide_list_[2].text_.text = TextDataCtrl.GetText(TextDataCtrl.CommonTextID.ROTATE_LEFT);
			guide_list_[3].text_.text = TextDataCtrl.GetText(TextDataCtrl.CommonTextID.ROTATE_RIGHT);
			break;
		case Type.VASE_SHOW:
		{
			board_.active = true;
			guide_list_[0].active = true;
			num = -25f;
			SetRotUnionGuidIcon();
			int num3 = 1;
			guide_list_[num3++].SetKeyTypeSprite(KeyType.X);
			guide_list_[num3++].SetKeyTypeSprite(KeyType.L, true);
			guide_list_[num3++].SetKeyTypeSprite(KeyType.Record, true);
			num3 = 1;
			guide_list_[num3++].text_.text = TextDataCtrl.GetText(TextDataCtrl.CommonTextID.TUKITUKE);
			guide_list_[num3++].text_.text = TextDataCtrl.GetText(TextDataCtrl.CommonTextID.ROTATE_LEFT);
			guide_list_[num3++].text_.text = TextDataCtrl.GetText(TextDataCtrl.CommonTextID.ROTATE_RIGHT);
			break;
		}
		case Type.CONFRONT_WITH_MOVIE:
			board_.active = true;
			guide_list_[2].SetKeyTypeSprite(KeyType.A, true);
			guide_list_[1].SetKeyTypeSprite(KeyType.B);
			guide_list_[3].SetKeyTypeSprite(KeyType.X);
			guide_list_[0].SetKeyTypeSprite(KeyType.Y, true);
			guide_list_[4].SetKeyTypeSprite(KeyType.R);
			guide_list_[2].text_.text = TextDataCtrl.GetText(TextDataCtrl.CommonTextID.FAST_FORWARD);
			guide_list_[1].text_.text = TextDataCtrl.GetText(TextDataCtrl.CommonTextID.PAUSE);
			guide_list_[3].text_.text = TextDataCtrl.GetText(TextDataCtrl.CommonTextID.TUKITUKE);
			guide_list_[0].text_.text = TextDataCtrl.GetText(TextDataCtrl.CommonTextID.REWIND);
			guide_list_[4].text_.text = TextDataCtrl.GetText(TextDataCtrl.CommonTextID.RECORD);
			break;
		case Type.LUMINOL:
			num = -25f;
			board_.active = true;
			guide_list_[0].SetKeyTypeSprite(KeyType.B);
			guide_list_[1].SetKeyTypeSprite(KeyType.A);
			guide_list_[0].text_.text = TextDataCtrl.GetText(TextDataCtrl.CommonTextID.BACK);
			guide_list_[1].text_.text = TextDataCtrl.GetText(TextDataCtrl.CommonTextID.HUKITUKR);
			break;
		case Type.LUMINOL_SLIDE:
			num = -25f;
			board_.active = true;
			guide_list_[0].SetKeyTypeSprite(KeyType.B);
			guide_list_[1].SetKeyTypeSprite(KeyType.A);
			guide_list_[2].SetKeyTypeSprite(KeyType.L);
			guide_list_[0].text_.text = TextDataCtrl.GetText(TextDataCtrl.CommonTextID.BACK);
			guide_list_[1].text_.text = TextDataCtrl.GetText(TextDataCtrl.CommonTextID.HUKITUKR);
			guide_list_[2].text_.text = TextDataCtrl.GetText(TextDataCtrl.CommonTextID.SLIDE);
			break;
		case Type.LUMINOL_TUTORIAL:
			num = -25f;
			board_.active = true;
			guide_list_[0].SetKeyTypeSprite(KeyType.A);
			guide_list_[0].text_.text = TextDataCtrl.GetText(TextDataCtrl.CommonTextID.HUKITUKR);
			break;
		case Type.LUMINOL_INSPECT:
			num = -25f;
			guide_list_[0].SetKeyTypeSprite(KeyType.A);
			guide_list_[0].text_.text = TextDataCtrl.GetText(TextDataCtrl.CommonTextID.INSPECT);
			break;
		case Type.FINGER_SELECT:
			board_.active = true;
			guide_list_[0].SetKeyTypeSprite(KeyType.A);
			guide_list_[0].text_.text = TextDataCtrl.GetText(TextDataCtrl.CommonTextID.DETECTION);
			if (FingerMiniGame.instance.game_id != 0)
			{
				guide_list_[1].SetKeyTypeSprite(KeyType.B);
				guide_list_[1].text_.text = TextDataCtrl.GetText(TextDataCtrl.CommonTextID.BACK);
			}
			break;
		case Type.FINGER_MAIN:
			board_.active = true;
			guide_list_[0].SetKeyTypeSprite(KeyType.A);
			guide_list_[1].SetKeyTypeSprite(KeyType.B);
			guide_list_[2].SetKeyTypeSprite(KeyType.X);
			guide_list_[0].text_.text = TextDataCtrl.GetText(TextDataCtrl.CommonTextID.DUST);
			guide_list_[1].text_.text = TextDataCtrl.GetText(TextDataCtrl.CommonTextID.BACK);
			guide_list_[2].text_.text = TextDataCtrl.GetText(TextDataCtrl.CommonTextID.BLOW);
			break;
		case Type.FINGER_COMP:
			board_.active = true;
			guide_list_[0].SetKeyTypeSprite(KeyType.X);
			guide_list_[0].text_.text = TextDataCtrl.GetText(TextDataCtrl.CommonTextID.COMPARE);
			break;
		case Type.FINGER_COMP_TUTORIAL:
			board_.active = true;
			guide_list_[0].SetKeyTypeSprite(KeyType.X);
			guide_list_[0].text_.text = TextDataCtrl.GetText(TextDataCtrl.CommonTextID.COMPARE);
			break;
		case Type.VIDEO_DETAIL:
			board_.active = true;
			guide_list_[0].SetKeyTypeSprite(KeyType.B);
			guide_list_[0].text_.text = TextDataCtrl.GetText(TextDataCtrl.CommonTextID.BACK);
			break;
		case Type.POINT:
			board_.active = true;
			num = -25f;
			guide_list_[0].SetKeyTypeSprite(KeyType.X);
			guide_list_[0].text_.text = TextDataCtrl.GetText(TextDataCtrl.CommonTextID.TUKITUKE);
			break;
		case Type.POINT_SLIDE:
			board_.active = true;
			num = -25f;
			guide_list_[0].SetKeyTypeSprite(KeyType.X);
			guide_list_[1].SetKeyTypeSprite(KeyType.L);
			guide_list_[0].text_.text = TextDataCtrl.GetText(TextDataCtrl.CommonTextID.TUKITUKE);
			guide_list_[1].text_.text = TextDataCtrl.GetText(TextDataCtrl.CommonTextID.SLIDE);
			break;
		case Type.POINT_TANTEI:
			board_.active = true;
			num = -25f;
			guide_list_[0].SetKeyTypeSprite(KeyType.X);
			guide_list_[1].SetKeyTypeSprite(KeyType.B);
			guide_list_[0].text_.text = TextDataCtrl.GetText(TextDataCtrl.CommonTextID.TUKITUKE);
			guide_list_[1].text_.text = TextDataCtrl.GetText(TextDataCtrl.CommonTextID.BACK);
			break;
		case Type.TANCHIKI:
			board_.active = false;
			break;
		case Type.TANCHIKI_SLIDE:
			board_.active = true;
			num = -20f;
			guide_list_[0].SetKeyTypeSprite(KeyType.L);
			guide_list_[0].text_.text = TextDataCtrl.GetText(TextDataCtrl.CommonTextID.SLIDE);
			break;
		case Type.CONFRONT_WITH_MOVIE_PAUSE:
			board_.active = true;
			guide_list_[2].SetKeyTypeSprite(KeyType.A, true);
			guide_list_[1].SetKeyTypeSprite(KeyType.B);
			guide_list_[3].SetKeyTypeSprite(KeyType.X);
			guide_list_[0].SetKeyTypeSprite(KeyType.Y, true);
			guide_list_[4].SetKeyTypeSprite(KeyType.R);
			guide_list_[2].text_.text = TextDataCtrl.GetText(TextDataCtrl.CommonTextID.FAST_FORWARD);
			guide_list_[1].text_.text = TextDataCtrl.GetText(TextDataCtrl.CommonTextID.MOVIE_PLAY);
			guide_list_[3].text_.text = TextDataCtrl.GetText(TextDataCtrl.CommonTextID.TUKITUKE);
			guide_list_[0].text_.text = TextDataCtrl.GetText(TextDataCtrl.CommonTextID.REWIND);
			guide_list_[4].text_.text = TextDataCtrl.GetText(TextDataCtrl.CommonTextID.RECORD);
			break;
		case Type.TOP_VIEW:
			board_.active = true;
			guide_list_[0].SetKeyTypeSprite(KeyType.B);
			guide_list_[1].SetKeyTypeSprite(KeyType.L);
			guide_list_[0].text_.text = TextDataCtrl.GetText(TextDataCtrl.CommonTextID.BACK);
			guide_list_[1].text_.text = TextDataCtrl.GetText(TextDataCtrl.CommonTextID.SLIDE);
			break;
		case Type.DYING_MESSAGE:
			board_.active = true;
			num = -34f;
			guide_list_[0].SetKeyTypeSprite(KeyType.B);
			guide_list_[1].SetKeyTypeSprite(KeyType.X);
			guide_list_[2].SetKeyTypeSprite(KeyType.R);
			guide_list_[0].text_.text = TextDataCtrl.GetText(TextDataCtrl.CommonTextID.DYING_BACK);
			guide_list_[1].text_.text = TextDataCtrl.GetText(TextDataCtrl.CommonTextID.TUKITUKE);
			guide_list_[2].text_.text = TextDataCtrl.GetText(TextDataCtrl.CommonTextID.RECORD);
			break;
		case Type.GS_MAIN:
			board_.active = false;
			break;
		case Type.GS_SELECT:
			board_.active = true;
			guide_list_[0].active = true;
			guide_list_[0].SetKeyTypeSprite(KeyType.B);
			guide_list_[0].text_.text = TextDataCtrl.GetText(TextDataCtrl.CommonTextID.BACK);
			break;
		case Type.SCENARIO_SELECT:
			board_.active = true;
			guide_list_[0].SetKeyTypeSprite(KeyType.B);
			guide_list_[0].text_.text = TextDataCtrl.GetText(TextDataCtrl.CommonTextID.BACK);
			break;
		case Type.DETAIL_VIEW:
		{
			board_.active = true;
			float num2 = recordListCtrl.instance.transform.localPosition.z + recordListCtrl.instance.detail_ctrl.transform.localPosition.z + recordListCtrl.instance.detail_ctrl.detail_sprite_pos.z;
			num = num2 - base.transform.localPosition.z - 1f;
			guide_list_[0].SetKeyTypeSprite(KeyType.B);
			guide_list_[0].text_.text = TextDataCtrl.GetText(TextDataCtrl.CommonTextID.BACK);
			break;
		}
		default:
			board_.active = false;
			break;
		}
		UpdateTouchArea();
		SetRotGuideArea();
		guide_cnt_ = guide_list_.Count((GuideIcon guide) => guide.active);
		Vector3 localPosition = board_.transform.localPosition;
		localPosition.x = board_pos_x_ - board_width_ * (float)guide_cnt_;
		if (symbol_body_ != null)
		{
			if (rot_union)
			{
				localPosition.x -= 138f;
				symbol_body_.localPosition = Vector3.right * 46f * 3f;
			}
			else
			{
				symbol_body_.localPosition = Vector3.zero;
			}
		}
		localPosition.y = board_pos_y_;
		localPosition.z = 0f;
		board_.transform.localPosition = localPosition;
		board_.transform.localPosition += Vector3.forward * num;
	}

	private IEnumerator CoroutineOpen(Type in_type)
	{
		active = true;
		setting(in_type);
		float time = 0f;
		while (time < 1f)
		{
			time += curve_.speed_open_;
			if (time > 1f)
			{
				time = 1f;
			}
			float scale = curve_.open_.Evaluate(time);
			body_.transform.localScale = new Vector3(1f, scale, 1f);
			yield return null;
		}
		ActiveTouch();
		float guide_widht = GetGuideWidth() + guide_space_;
		if (MiniGameCursor.instance != null && guide_widht > guide_space_)
		{
			MiniGameCursor.instance.cursor_exception_limit = new Vector2((float)systemCtrl.instance.ScreenWidth - guide_widht - 60f, 880f);
		}
		enumerator_open_ = null;
	}

	private IEnumerator CoroutineClose()
	{
		float time = 0f;
		while (time < 1f)
		{
			time += curve_.speed_close_;
			if (time > 1f)
			{
				time = 1f;
			}
			float scale = curve_.close_.Evaluate(time);
			body_.transform.localScale = new Vector3(1f, scale, 1f);
			yield return null;
		}
		foreach (GuideIcon item in guide_list_)
		{
			mainCtrl.instance.removeText(item.text_);
		}
		enumerator_close_ = null;
		active = false;
	}

	public Coroutine open(Type in_type)
	{
		if (enumerator_open_ != null)
		{
			StopCoroutine(enumerator_open_);
			enumerator_open_ = null;
		}
		if (enumerator_close_ != null)
		{
			StopCoroutine(enumerator_close_);
			enumerator_open_ = null;
		}
		enumerator_open_ = CoroutineOpen(in_type);
		return StartCoroutine(enumerator_open_);
	}

	public Coroutine close()
	{
		if (enumerator_close_ != null)
		{
			StopCoroutine(enumerator_close_);
			enumerator_close_ = null;
		}
		if (enumerator_open_ != null)
		{
			StopCoroutine(enumerator_open_);
			enumerator_close_ = null;
		}
		enumerator_close_ = CoroutineClose();
		return StartCoroutine(enumerator_close_);
	}

	public bool CheckOpen()
	{
		return enumerator_open_ != null;
	}

	public bool CheckClose()
	{
		return enumerator_close_ != null;
	}

	public void UpdateActiveIconTouch()
	{
		foreach (GuideIcon item in guide_list_)
		{
			if (item.active && !item.touch_.box_collider_2d_enable)
			{
				item.touch_.ActiveCollider();
			}
		}
	}

	public virtual void DebugGuideDisp()
	{
		debug_is_disp_ = !debug_is_disp_;
		setting(current_guide_);
	}

	protected virtual bool DebugDispCheck()
	{
		if (!debug_is_disp_)
		{
			board_.active = false;
			return false;
		}
		return true;
	}
}
