using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bgCtrl : MonoBehaviour
{
	[Serializable]
	public class SealData
	{
		public string path_ = string.Empty;

		public string name_ = string.Empty;

		public int id_ = 255;

		public SpriteRenderer sprite_;

		public bool active
		{
			get
			{
				return sprite_.gameObject.activeSelf;
			}
			set
			{
				sprite_.gameObject.SetActive(value);
			}
		}

		public Transform transform
		{
			get
			{
				return sprite_.gameObject.transform;
			}
		}
	}

	private static bgCtrl instance_;

	[SerializeField]
	private bgData bg_data_;

	[SerializeField]
	private SpriteRenderer sprite_renderer_;

	[SerializeField]
	private SpriteRenderer sub_sprite_;

	[SerializeField]
	private SpriteRenderer fore_renderer_;

	[SerializeField]
	private SpriteRenderer seal_renderer_;

	[SerializeField]
	private List<SealData> seal_list_ = new List<SealData>();

	[SerializeField]
	private List<SpriteRenderer> scrool_sprite_ = new List<SpriteRenderer>();

	[SerializeField]
	private List<SpriteRenderer> parts_sprite_;

	[SerializeField]
	private RawImage cutup_sprite_;

	[SerializeField]
	private SpriteRenderer image_;

	[SerializeField]
	private GameObject body_;

	[SerializeField]
	private GameObject cutup_scroll_;

	[SerializeField]
	private AnimationCurve scroll_rate = new AnimationCurve();

	[SerializeField]
	private Material default_material_;

	[SerializeField]
	private Material invert_color_material_;

	[SerializeField]
	private Material grayscale_material_;

	[SerializeField]
	private Material sepia_material_;

	[SerializeField]
	private Material black_material_;

	[SerializeField]
	private Material white_material_;

	[SerializeField]
	private SpriteRenderer ending_table_;

	[SerializeField]
	private SpriteRenderer red_bg_;

	private Sprite sprite_data_;

	private IEnumerator enumerator_scroll_;

	private IEnumerator enumerator_court_;

	private IEnumerator enumerator_slider_;

	private IEnumerator spef_enumerator_court_;

	private bool is_scrolling_court_;

	private float bg_pos_x_;

	private float bg_pos_y_;

	private float bg_pos_x_old_;

	private float bg_pos_y_old_;

	private Vector3 bg_origin_pos = Vector3.zero;

	private float sub_bg_pos_x_;

	private bool is_scroll_;

	private bool is_slider_;

	private bool is_reverse_;

	private bool is_cutup_scroll_;

	private float cutup_speed_x_ = 0.03f;

	private int bg_no_ = 65535;

	private int bg_no_now_ = 65535;

	private int bg_no_old_ = 65535;

	private int bg_no_reserve_ = 65535;

	private string path_ = string.Empty;

	private string name_ = string.Empty;

	private string bg_path_ = string.Empty;

	private int op_state_;

	private int op_count_;

	private int etc20_ = 5;

	private int etc21_ = 4;

	private int etc22_ = 3;

	private GameObject parts_parent_;

	public const int BGD_PSYLOCK = 254;

	public const int BGD_BLACK = 4095;

	public const int BGCFL_NONE = 32768;

	public const ushort BG256_SCROLL_L = 0;

	public const ushort BG256_SCROLL_R = 1;

	public const ushort BG256_SCROLL_U = 2;

	public const ushort BG256_SCROLL_D = 3;

	public const ushort SCROLL_SPEED_FAST = 8;

	public const ushort SCROLL_SPEED_NORMAL = 4;

	public const ushort SCROLL_SPEED_SLOW = 1;

	public const int MOV_PLAY_REVERSE = 1;

	public const int BG40B_SPEED_Y = 16;

	public int bg_moasic_no_ = 65535;

	public ushort Bg256_dir;

	public byte Bg256_SP_Flag;

	private ushort current_sw_ = 32;

	private const int GS2_OP_ANIMATION_MAX = 16;

	private int gs2_op_animation_timer_;

	private int gs2_op_animation_no_;

	private Sprite[] gs2_op_animation_sprites_;

	private bool white_bg_;

	public static bgCtrl instance
	{
		get
		{
			return instance_;
		}
	}

	public GameObject body
	{
		get
		{
			return body_;
		}
	}

	public Sprite sprite_data
	{
		get
		{
			return sprite_data_;
		}
	}

	public bool is_scrolling_court
	{
		get
		{
			return is_scrolling_court_;
		}
	}

	public int next_characterID_in_court_scroll { get; private set; }

	public float next_posX_in_court_scroll { get; private set; }

	public bool is_scroll
	{
		get
		{
			return is_scroll_;
		}
	}

	public bool is_slider
	{
		get
		{
			return is_slider_;
		}
	}

	public int bg_no
	{
		get
		{
			return bg_no_;
		}
		set
		{
			bg_no_ = value;
		}
	}

	public int bg_no_now
	{
		get
		{
			return bg_no_now_;
		}
		set
		{
			bg_no_now_ = value;
		}
	}

	public int bg_no_old
	{
		get
		{
			return bg_no_old_;
		}
		set
		{
			bg_no_old_ = value;
		}
	}

	public int bg_no_reserve
	{
		get
		{
			return bg_no_reserve_;
		}
		set
		{
			bg_no_reserve_ = value;
		}
	}

	public bool is_reverse
	{
		get
		{
			return is_reverse_;
		}
		set
		{
			is_reverse_ = value;
		}
	}

	public float bg_pos_x
	{
		get
		{
			return bg_pos_x_;
		}
		set
		{
			bg_pos_x_ = value;
			SetBodyPosition(new Vector3(0f - bg_pos_x_, bg_pos_y_, 0f));
		}
	}

	public float bg_pos_y
	{
		get
		{
			return bg_pos_y_;
		}
		set
		{
			bg_pos_y_ = value;
			SetBodyPosition(new Vector3(0f - bg_pos_x_, bg_pos_y_, 0f));
		}
	}

	public float sub_bg_pos_x
	{
		get
		{
			return sub_bg_pos_x_;
		}
		set
		{
			sub_bg_pos_x_ = value;
			Vector3 localPosition = sub_sprite_.transform.localPosition;
			localPosition.x = sub_bg_pos_x_;
			sub_sprite_.transform.localPosition = localPosition;
		}
	}

	public float shadow_bg_pos_x
	{
		get
		{
			return sub_bg_pos_x_;
		}
		set
		{
			sub_bg_pos_x_ = value;
			Vector3 localPosition = sub_sprite_.transform.localPosition;
			localPosition.x = sub_bg_pos_x_ + bg_pos_x_;
			sub_sprite_.transform.localPosition = localPosition;
		}
	}

	public ushort current_sw
	{
		get
		{
			return current_sw_;
		}
	}

	public Transform ending_table_trans
	{
		get
		{
			return ending_table_.transform;
		}
	}

	public List<SpriteRenderer> parts_sprite
	{
		get
		{
			return parts_sprite_;
		}
	}

	public GameObject parts_parent
	{
		get
		{
			return parts_parent_;
		}
		set
		{
			parts_parent_ = value;
		}
	}

	public bgData bg_data
	{
		get
		{
			return bg_data_;
		}
	}

	public SpriteRenderer ending_table
	{
		get
		{
			return ending_table_;
		}
		set
		{
			ending_table_ = value;
		}
	}

	private void Awake()
	{
		if (instance_ == null)
		{
			instance_ = this;
		}
	}

	public void load()
	{
		scrool_sprite_[1].sprite = SetCourtSprite("/GS1/BG/", "houscr");
		scrool_sprite_[2].sprite = SetCourtSprite("/GS1/BG/", "houscr");
		parts_sprite_[0].sprite = SetCourtSprite("/GS1/etc/", "etc20");
		parts_sprite_[1].sprite = SetCourtSprite("/GS1/etc/", "etc21");
		parts_sprite_[2].sprite = SetCourtSprite("/GS1/etc/", "etc22");
	}

	public void init()
	{
		load();
		bg_origin_pos = sprite_renderer_.transform.localPosition;
		bg_path_ = "/GS" + (int)(GSStatic.global_work_.title + 1) + "/BG/";
		switch (GSStatic.global_work_.title)
		{
		case TitleId.GS1:
			etc20_ = 5;
			etc21_ = 4;
			etc22_ = 3;
			break;
		case TitleId.GS2:
			etc20_ = 6;
			etc21_ = 5;
			etc22_ = 4;
			break;
		case TitleId.GS3:
			etc20_ = 6;
			etc21_ = 5;
			etc22_ = 4;
			break;
		}
		parts_sprite_[0].gameObject.SetActive(false);
		parts_sprite_[1].gameObject.SetActive(false);
		parts_sprite_[2].gameObject.SetActive(false);
	}

	public void end()
	{
		bg_no_ = 65535;
		bg_no_now_ = 65535;
		bg_no_old_ = 65535;
		parts_sprite_[0].gameObject.SetActive(false);
		parts_sprite_[1].gameObject.SetActive(false);
		parts_sprite_[2].gameObject.SetActive(false);
		spefCtrl.instance.ResetBG();
	}

	public string GetBGName(int in_bg_no)
	{
		if (bg_data_.data[in_bg_no].language_ != 32768 && GSStatic.global_work_.language == "USA")
		{
			return bg_data_.data_language[(int)bg_data_.data[in_bg_no].language_];
		}
		return bg_data_.GetBGName(in_bg_no);
	}

	public void SetForeSprite(int fore_no)
	{
		SetSprite(fore_no, fore_renderer_, false);
	}

	public void DeleteForeSprite()
	{
		judgmentCtrl.instance.eff_active = false;
		fore_renderer_.gameObject.SetActive(false);
	}

	public void GameOverInactiveAnmChild()
	{
		AnimationSystem.Instance.StopAll();
	}

	public void SetSprite(int bg_no, bool set_parts = true)
	{
		if (set_parts)
		{
			SetParts(bg_no);
		}
		DeleteForeSprite();
		SetSprite(bg_no, sprite_renderer_, true);
	}

	public void SetReserveSprite(bool set_parts = true)
	{
		if (bg_no_reserve == 65535)
		{
			return;
		}
		if (!set_parts && bg_no_reserve == 4095)
		{
			foreach (SpriteRenderer item in parts_sprite_)
			{
				item.gameObject.SetActive(false);
			}
		}
		else
		{
			set_parts = true;
		}
		SetSprite(bg_no_reserve, set_parts);
		GSDemo.CheckBGChange((uint)bg_no_, 0u);
		bg_no_reserve = 65535;
	}

	public void SetSeal(int seal_no, bool in_active = true)
	{
		if (in_active)
		{
			int num = 0;
			{
				foreach (SealData item in seal_list_)
				{
					if (item.active && item.id_ == seal_no)
					{
						break;
					}
					bgData.DataSeal dataSeal = bg_data_.seal[seal_no];
					if (!seal_list_[num].active)
					{
						item.id_ = seal_no;
						if (GSStatic.global_work_.language == "USA")
						{
							item.name_ = dataSeal.name_u_;
							item.sprite_.sprite = SetCourtSprite(bg_path_, dataSeal.name_u_);
						}
						else
						{
							item.name_ = dataSeal.name_;
							item.sprite_.sprite = SetCourtSprite(bg_path_, dataSeal.name_);
						}
						item.active = true;
						item.transform.localPosition = new Vector3(dataSeal.x_, dataSeal.y_, -3f + -0.1f * (float)num);
						break;
					}
					num++;
				}
				return;
			}
		}
		foreach (SealData item2 in seal_list_)
		{
			if (item2.id_ == seal_no)
			{
				item2.sprite_.gameObject.SetActive(false);
			}
		}
	}

	public void SetParts(int parts_no, bool enabled)
	{
		if (enabled)
		{
			foreach (SpriteRenderer item in parts_sprite_)
			{
				item.gameObject.SetActive(false);
			}
		}
		GSStatic.bg_save_data.bg_parts = (ushort)parts_no;
		GSStatic.bg_save_data.bg_parts_enabled = enabled;
		switch (parts_no)
		{
		case 0:
			parts_sprite_[0].gameObject.transform.localPosition = new Vector3(0f, -540f, -20f);
			parts_sprite_[0].gameObject.SetActive(enabled);
			break;
		case 1:
			parts_sprite_[2].gameObject.transform.localPosition = new Vector3(0f, -540f, -20f);
			parts_sprite_[2].gameObject.SetActive(enabled);
			break;
		case 2:
			parts_sprite_[1].gameObject.transform.localPosition = new Vector3(0f, -540f, -20f);
			parts_sprite_[1].gameObject.SetActive(enabled);
			break;
		}
		MessageWork activeMessageWork = MessageSystem.GetActiveMessageWork();
		if (activeMessageWork.op_work[7] == ushort.MaxValue)
		{
			activeMessageWork.op_work[6] = 0;
			activeMessageWork.op_work[7] = 0;
		}
	}

	private void SetParts(int in_bg_no)
	{
		GlobalWork global_work_ = GSStatic.global_work_;
		parts_sprite_[0].gameObject.SetActive(false);
		parts_sprite_[1].gameObject.SetActive(false);
		parts_sprite_[2].gameObject.SetActive(false);
		int num = -1;
		if (in_bg_no == 4095 && AnimationSystem.Instance.IsCharacterPlaying && AnimationSystem.Instance.CharacterAnimationObject.gameObject.activeSelf)
		{
			switch (global_work_.title)
			{
			case TitleId.GS1:
				switch (GSStatic.obj_work_[1].h_num)
				{
				case 2:
					num = 2;
					break;
				default:
				{
					MessageWork activeMessageWork3 = MessageSystem.GetActiveMessageWork();
					if (activeMessageWork3.op_work[7] == ushort.MaxValue)
					{
						switch ((ushort)(activeMessageWork3.op_work[6] & 0xF))
						{
						case 0:
							num = 0;
							break;
						case 1:
							num = 2;
							break;
						case 2:
							num = 1;
							break;
						}
					}
					break;
				}
				case 33:
					break;
				}
				break;
			case TitleId.GS2:
				switch (GSStatic.obj_work_[1].h_num)
				{
				case 3:
					num = 2;
					break;
				case 24:
					num = 0;
					break;
				default:
				{
					MessageWork activeMessageWork2 = MessageSystem.GetActiveMessageWork();
					if (activeMessageWork2.op_work[7] == ushort.MaxValue)
					{
						switch ((ushort)(activeMessageWork2.op_work[6] & 0xF))
						{
						case 0:
							num = 0;
							break;
						case 1:
							num = 2;
							break;
						case 2:
							num = 1;
							break;
						}
					}
					break;
				}
				case 8:
					break;
				}
				break;
			case TitleId.GS3:
				switch (GSStatic.obj_work_[1].h_num)
				{
				case 3:
				case 7:
					num = 2;
					break;
				case 17:
					num = 1;
					break;
				default:
				{
					MessageWork activeMessageWork = MessageSystem.GetActiveMessageWork();
					if (activeMessageWork.op_work[7] == ushort.MaxValue)
					{
						switch ((ushort)(activeMessageWork.op_work[6] & 0xF))
						{
						case 0:
							num = 0;
							break;
						case 1:
							num = 2;
							break;
						case 2:
							num = 1;
							break;
						}
					}
					break;
				}
				}
				break;
			}
		}
		else if (global_work_.title == TitleId.GS2 && ((long)in_bg_no == 22 || (long)in_bg_no == 24) && GSStatic.obj_work_[1].h_num == 16 && (global_work_.r.no_0 != 8 || global_work_.r.no_1 != 5 || global_work_.r.no_2 == 4))
		{
			num = 0;
		}
		else if (global_work_.title == TitleId.GS2 && (long)in_bg_no == 83 && GSStatic.obj_work_[1].h_num == 3 && (global_work_.r.no_0 != 8 || global_work_.r.no_1 != 5 || global_work_.r.no_2 == 4))
		{
			num = 2;
		}
		else
		{
			if (in_bg_no == etc22_)
			{
				num = 2;
			}
			if (in_bg_no == etc21_)
			{
				num = 1;
			}
			if (in_bg_no == etc20_)
			{
				num = 0;
			}
		}
		if (num != -1)
		{
			parts_sprite_[num].gameObject.transform.localPosition = new Vector3(0f, -540f, -20f);
			parts_sprite_[num].gameObject.SetActive(true);
		}
		MessageWork activeMessageWork4 = MessageSystem.GetActiveMessageWork();
		if (activeMessageWork4.op_work[7] == ushort.MaxValue)
		{
			activeMessageWork4.op_work[6] = 0;
			activeMessageWork4.op_work[7] = 0;
		}
	}

	private void SetSprite(int in_bg_no, SpriteRenderer targetRenderer, bool is_offset)
	{
		if (GSStatic.global_work_.SpEf_status == 3 || GSStatic.global_work_.SpEf_status == 7)
		{
			if ((long)in_bg_no == 66)
			{
				spefCtrl.instance.ResetBG();
			}
		}
		else if (white_bg_)
		{
			spefCtrl.instance.ResetBG();
		}
		setColor(Color.white);
		sprite_renderer_.enabled = true;
		if (GSStatic.global_work_.title == TitleId.GS1 && GSStatic.global_work_.scenario == 21 && MessageSystem.GetMessageWork(WindowType.MAIN).mdt_index == 17515 && (long)in_bg_no == 124)
		{
			sprite_renderer_.transform.localEulerAngles = new Vector3(0f, 0f, 180f);
		}
		else
		{
			sprite_renderer_.transform.localEulerAngles = Vector3.zero;
		}
		bool flag = false;
		if (in_bg_no >= 32768)
		{
			in_bg_no &= -32769;
			flag = true;
		}
		if (bg_no_ == in_bg_no && is_reverse_ == flag && targetRenderer.gameObject.activeSelf)
		{
			MessageWork activeMessageWork = MessageSystem.GetActiveMessageWork();
			if ((activeMessageWork.status2 & MessageSystem.Status2.MOSAIC_MONO) != 0)
			{
				activeMessageWork.status2 &= ~MessageSystem.Status2.MOSAIC_MONO;
				spefCtrl.instance.Monochrome_set(7, 1, 31);
			}
			return;
		}
		foreach (SealData item in seal_list_)
		{
			item.sprite_.gameObject.SetActive(false);
		}
		seal_renderer_.gameObject.SetActive(false);
		targetRenderer.gameObject.SetActive(false);
		image_.gameObject.SetActive(false);
		sub_sprite_.gameObject.SetActive(false);
		stopScroll();
		SetBodyPosition(Vector3.zero);
		if (in_bg_no == 4095)
		{
			StopCutUpScroll();
			MessageWork activeMessageWork2 = MessageSystem.GetActiveMessageWork();
			activeMessageWork2.status2 &= ~MessageSystem.Status2.MOSAIC_MONO;
			GSStatic.bg_save_data.bg_black = true;
			image_.gameObject.SetActive(true);
			image_.color = new Color(0f, 0f, 0f, 1f);
			return;
		}
		GSStatic.bg_save_data.bg_black = false;
		if (in_bg_no >= 32768)
		{
			bg_no_ = bg_no_old_;
			in_bg_no = bg_no_old_;
			bg_pos_x_ = bg_pos_x_old_;
			bg_pos_y_ = bg_pos_y_old_;
			is_offset = false;
			if (bg_data_.data.Count <= in_bg_no)
			{
				Debug.LogError("bg_data_.data.Count(" + bg_data_.data.Count + ") <= in_bg_no( " + in_bg_no + " )");
				bg_no_ = 0;
				in_bg_no = 0;
				bg_pos_x_ = 0f;
				bg_pos_y_ = 0f;
			}
			if (bg_data_.data[in_bg_no].type_ == 1)
			{
				targetRenderer.gameObject.transform.localPosition = new Vector3(-960f, 0f, 0f);
				sub_sprite_.transform.localPosition = new Vector3(-960f, 0f, -20f);
			}
			else if (bg_data_.data[in_bg_no].type_ == 2)
			{
				targetRenderer.gameObject.transform.localPosition = new Vector3(-960f, 0f, 0f);
				sub_sprite_.transform.localPosition = new Vector3(-960f, 0f, -20f);
			}
			else
			{
				targetRenderer.gameObject.transform.localPosition = new Vector3(0f, 0f, 0f);
				sub_sprite_.transform.localPosition = new Vector3(0f, 0f, -20f);
			}
			SetBodyPosition(new Vector3(0f - bg_pos_x_, bg_pos_y_, 0f));
		}
		if (in_bg_no >= bg_data_.data.Count || bg_data_.data[in_bg_no].name_ == string.Empty)
		{
			return;
		}
		bg_no_old_ = bg_no_;
		bg_no_ = in_bg_no;
		is_reverse_ = flag;
		op_state_ = 0;
		op_count_ = 0;
		if (is_offset)
		{
			AssetBundleCtrl.instance.remove(path_, name_);
			path_ = bg_path_;
			name_ = bg_data_.data[in_bg_no].name_;
		}
		if (bg_data_.data[in_bg_no].language_ != 32768 && GSStatic.global_work_.language == "USA")
		{
			targetRenderer.sprite = SetCourtSprite(bg_path_, bg_data_.data_language[(int)bg_data_.data[in_bg_no].language_]);
		}
		else
		{
			targetRenderer.sprite = SetCourtSprite(bg_path_, bg_data_.data[in_bg_no].name_);
		}
		targetRenderer.gameObject.SetActive(true);
		float num = 0f;
		num = ((!(sprite_renderer_.sprite.pivot.y > 0f)) ? (-540f) : ((!(sprite_renderer_.sprite.pivot.y > sprite_renderer_.sprite.rect.height * 0.5f)) ? 0f : 540f));
		Bg256_dir = 0;
		if (is_offset)
		{
			bg_pos_x_old_ = bg_pos_x_;
			bg_pos_x_ = 0f;
			bg_pos_y_old_ = bg_pos_y_;
			bg_pos_y_ = 0f;
			if ((bg_data_.data[in_bg_no].type_ == 1 && !flag) || (bg_data_.data[in_bg_no].type_ == 2 && flag))
			{
				targetRenderer.gameObject.transform.localPosition = new Vector3(-960f, num, 0f);
				sub_sprite_.transform.localPosition = new Vector3(-960f, num, -20f);
				bg_pos_x_ = targetRenderer.sprite.rect.width - 1920f;
				bg_pos_y_ = targetRenderer.sprite.rect.height - 1080f;
				SetBodyPosition(new Vector3(0f - bg_pos_x_, bg_pos_y_, 0f));
				Bg256_dir &= 65503;
				Bg256_dir |= 16;
			}
			else if ((bg_data_.data[in_bg_no].type_ == 1 && flag) || (bg_data_.data[in_bg_no].type_ == 2 && !flag))
			{
				targetRenderer.gameObject.transform.localPosition = new Vector3(-960f, num, 0f);
				sub_sprite_.transform.localPosition = new Vector3(-960f, num, -20f);
				bg_pos_x_ = 0f;
				bg_pos_y_ = 0f;
				SetBodyPosition(new Vector3(0f - bg_pos_x_, bg_pos_y_, 0f));
				Bg256_dir &= 65519;
				Bg256_dir |= 32;
			}
			else if ((bg_data_.data[in_bg_no].type_ == 3 && flag) || (bg_data_.data[in_bg_no].type_ == 4 && !flag))
			{
				targetRenderer.gameObject.transform.localPosition = new Vector3(0f, num, 0f);
				sub_sprite_.transform.localPosition = new Vector3(0f, num, -20f);
				bg_pos_x_ = 0f;
				bg_pos_y_ = 0f;
				SetBodyPosition(new Vector3(0f - bg_pos_x_, bg_pos_y_, 0f));
				Bg256_dir &= 65471;
				Bg256_dir |= 128;
			}
			else if ((bg_data_.data[in_bg_no].type_ == 4 && flag) || (bg_data_.data[in_bg_no].type_ == 3 && !flag))
			{
				targetRenderer.gameObject.transform.localPosition = new Vector3(0f, num, 0f);
				sub_sprite_.transform.localPosition = new Vector3(0f, num, -20f);
				bg_pos_x_ = 0f;
				bg_pos_y_ = sprite_renderer_.sprite.pivot.y - (sprite_renderer_.transform.localPosition.y + 540f);
				SetBodyPosition(new Vector3(0f - bg_pos_x_, bg_pos_y_, 0f));
				Bg256_dir &= 65407;
				Bg256_dir |= 64;
			}
			else
			{
				targetRenderer.gameObject.transform.localPosition = new Vector3(0f, num, 0f);
				sub_sprite_.transform.localPosition = new Vector3(0f, num, -20f);
			}
		}
		if (bg_data_.data[in_bg_no].sub_ != string.Empty)
		{
			sub_sprite_.sprite = SetCourtSprite(bg_path_, bg_data_.data[in_bg_no].sub_);
			sub_sprite_.gameObject.SetActive(true);
			sub_bg_pos_x = 0f;
		}
		if (in_bg_no == Bg_GetRyuchijyoBgNo())
		{
			if (((uint)Bg256_SP_Flag & (true ? 1u : 0u)) != 0)
			{
				ChangeSubSpriteEnable(false);
			}
			else
			{
				ChangeSubSpriteEnable(true);
			}
		}
		if (GSStatic.global_work_.title == TitleId.GS2 && (long)bg_no_ == 126)
		{
			targetRenderer.flipX = true;
		}
		else if (GSStatic.global_work_.title == TitleId.GS3 && (long)bg_no_ == 148)
		{
			targetRenderer.flipX = true;
			targetRenderer.flipY = true;
		}
		else
		{
			targetRenderer.flipX = false;
			targetRenderer.flipY = false;
		}
		MessageWork activeMessageWork3 = MessageSystem.GetActiveMessageWork();
		if ((activeMessageWork3.status2 & MessageSystem.Status2.MOSAIC_MONO) != 0)
		{
			activeMessageWork3.status2 &= ~MessageSystem.Status2.MOSAIC_MONO;
			spefCtrl.instance.Monochrome_set(7, 1, 31);
		}
	}

	public void SetSprite(string no)
	{
		sprite_renderer_.sprite = SetCourtSprite(bg_path_, no);
	}

	public void SetSubSprite(string no)
	{
		sub_sprite_.sprite = SetCourtSprite(bg_path_, no);
		sub_sprite_.gameObject.SetActive(true);
		sub_bg_pos_x = 0f;
	}

	public void SetSubSprite(int in_bg_no)
	{
		sub_sprite_.sprite = SetCourtSprite(bg_path_, GetBGName(in_bg_no));
		sub_sprite_.gameObject.SetActive(true);
		sub_bg_pos_x = 0f;
	}

	public void SetSpriteDelay(int in_delay_time)
	{
		coroutineCtrl.instance.Play(CroutineSetSpriteDelay(in_delay_time));
	}

	private IEnumerator CroutineSetSpriteDelay(int in_delay_time)
	{
		int current_bg_no = bg_no_;
		int timer = 0;
		sprite_renderer_.enabled = false;
		while (timer < in_delay_time)
		{
			timer++;
			yield return null;
		}
		if (current_bg_no == bg_no_)
		{
			sprite_renderer_.enabled = true;
		}
	}

	public void Scroll(float speed_x, float speed_y, bool center_stop = false, Transform target = null)
	{
		stopScroll();
		if (target == null)
		{
			target = body_.transform;
		}
		enumerator_scroll_ = CoroutineScroll(speed_x, speed_y, center_stop, target);
		coroutineCtrl.instance.Play(enumerator_scroll_);
	}

	public void stopScroll()
	{
		if (enumerator_scroll_ != null)
		{
			coroutineCtrl.instance.Stop(enumerator_scroll_);
			enumerator_scroll_ = null;
		}
		is_scroll_ = false;
	}

	private IEnumerator CoroutineScroll(float speed_x, float speed_y, bool center_stop, Transform target)
	{
		is_scroll_ = true;
		Vector3 speed = new Vector3(speed_x * 1920f / 256f, speed_y * 1080f / 192f, 0f);
		float start_pos_y = 0f;
		float start_pos_x = 0f;
		float end_pos_y = 0f;
		float end_pos_x = 0f;
		Bg256_dir = 0;
		if (speed_y < 0f)
		{
			start_pos_y = sprite_renderer_.sprite.pivot.y - (sprite_renderer_.transform.localPosition.y + 540f);
			end_pos_y = start_pos_y - sprite_renderer_.sprite.rect.height + 1080f;
			Bg256_dir |= 64;
			if (GSStatic.global_work_.title == TitleId.GS2 && (long)bg_no_ == 80)
			{
				end_pos_y += 270f;
			}
		}
		else if (speed_y > 0f)
		{
			start_pos_y = sprite_renderer_.sprite.pivot.y - sprite_renderer_.sprite.rect.height;
			end_pos_y = sprite_renderer_.sprite.rect.height - sprite_renderer_.sprite.pivot.y + 1080f;
			Bg256_dir |= 128;
		}
		else
		{
			start_pos_y = target.localPosition.y;
			end_pos_y = target.localPosition.y;
		}
		if (speed_x < 0f)
		{
			start_pos_x = target.localPosition.x;
			end_pos_x = 0f - (sprite_renderer_.sprite.rect.width - 1920f);
			Bg256_dir |= 32;
		}
		else if (speed_x > 0f)
		{
			start_pos_x = target.localPosition.x;
			end_pos_x = 0f;
			Bg256_dir |= 16;
		}
		else
		{
			start_pos_x = target.localPosition.x;
			end_pos_x = target.localPosition.x;
			Bg256_dir |= 32;
		}
		if (GSStatic.global_work_.title == TitleId.GS2)
		{
			if (GSStatic.global_work_.scenario == 14 && MessageSystem.GetActiveMessageWork().now_no == 128 && ((Bg256_dir & 0x20u) != 0 || (Bg256_dir & 0x10u) != 0))
			{
				speed *= 0.5f;
			}
			if ((long)bg_no_ == 87)
			{
				end_pos_x = -470f;
				speed *= 0.19800001f;
			}
		}
		target.transform.localPosition = new Vector3(start_pos_x, start_pos_y, 0f);
		bg_pos_x_ = 0f - end_pos_x;
		bg_pos_y_ = end_pos_y;
		while (true)
		{
			target.localPosition += speed;
			if (bg_data_.data[bg_no_].type_ != 3 && !recordListCtrl.instance.detail_ctrl.is_open)
			{
				AnimationSystem.Instance.Scroll(speed);
				GSMapIcon.instance.ExplScroll(new Vector3(speed.x * 284.44446f / 1920f, speed.y * 192f / 1080f, 0f));
			}
			if (speed_y < 0f)
			{
				if (target.localPosition.y < end_pos_y)
				{
					break;
				}
			}
			else if (speed_y > 0f && target.localPosition.y > end_pos_y)
			{
				break;
			}
			if (speed_x < 0f)
			{
				if (target.localPosition.x < end_pos_x)
				{
					break;
				}
			}
			else if (speed_x > 0f && target.localPosition.x > end_pos_x)
			{
				break;
			}
			yield return null;
		}
		if (bg_data_.data[bg_no_].type_ != 3 && !recordListCtrl.instance.detail_ctrl.is_open)
		{
			AnimationSystem.Instance.Scroll(new Vector3(end_pos_x - target.localPosition.x, end_pos_y - target.localPosition.y, 0f));
			GSMapIcon.instance.ExplScroll(new Vector3((end_pos_x - target.localPosition.x) * 284.44446f / 1920f, (end_pos_y - target.localPosition.y) * 192f / 1080f, 0f));
		}
		target.localPosition = new Vector3(end_pos_x, end_pos_y, 0f);
		if (bg_data_.data[bg_no_].type_ == 1 && speed_x < 0f)
		{
			is_reverse_ = false;
		}
		else if (bg_data_.data[bg_no_].type_ == 1 && speed_x > 0f)
		{
			is_reverse_ = true;
		}
		else if (bg_data_.data[bg_no_].type_ == 2 && speed_x < 0f)
		{
			is_reverse_ = true;
		}
		else if (bg_data_.data[bg_no_].type_ == 2 && speed_x > 0f)
		{
			is_reverse_ = false;
		}
		else
		{
			is_reverse_ = !is_reverse_;
		}
		enumerator_scroll_ = null;
		is_scroll_ = false;
		uint num = Bg256_dir;
		if ((num & 0x40u) != 0)
		{
			num = 0u;
			num |= 0x80u;
		}
		else if ((num & 0x80u) != 0)
		{
			num = 0u;
			num |= 0x40u;
		}
		else if ((num & 0x10u) != 0)
		{
			num = 0u;
			num |= 0x20u;
		}
		else if ((num & 0x20u) != 0)
		{
			num = 0u;
			num |= 0x10u;
		}
		Bg256_dir = (ushort)num;
	}

	public void CourtScrol(uint in_mov_no, uint in_play, uint next_char, uint in_start, uint in_time, uint in_adr)
	{
		stopCourtScrol();
		enumerator_court_ = CoroutineCourt(in_mov_no, in_play, next_char, in_start, in_time, in_adr);
		coroutineCtrl.instance.Play(enumerator_court_);
	}

	public void darkness_scroll()
	{
		enumerator_court_ = Coroutine_darkness_scroll();
		coroutineCtrl.instance.Play(enumerator_court_);
	}

	private void stopCourtScrol()
	{
		SetBodyPosition(Vector3.zero);
		if (enumerator_court_ != null)
		{
			coroutineCtrl.instance.Stop(enumerator_court_);
			enumerator_court_ = null;
		}
	}

	private Sprite SetCourtSprite(string in_path, string in_name)
	{
		AssetBundle assetBundle = AssetBundleCtrl.instance.load(in_path, in_name);
		return assetBundle.LoadAllAssets<Sprite>()[0];
	}

	private IEnumerator CoroutineCourt(uint in_mov_no, uint in_play, uint next_char, uint in_start, uint in_time, uint in_adr)
	{
		if ((MessageSystem.GetActiveMessageWork().status2 & MessageSystem.Status2.MV_BLACK) != 0)
		{
			foreach (SpriteRenderer item in scrool_sprite_)
			{
				item.color = Color.black;
			}
		}
		else if ((MessageSystem.GetActiveMessageWork().status2 & MessageSystem.Status2.MV_WHITE) != 0)
		{
			foreach (SpriteRenderer item2 in scrool_sprite_)
			{
				item2.material = white_material_;
			}
		}
		is_scrolling_court_ = true;
		int timer = 0;
		float speed = 5070f / (float)in_time;
		float pos_x = 0f;
		float pos_x2 = 0f;
		float pos_x3 = 0f;
		switch (in_mov_no)
		{
		case 0u:
		{
			if ((in_play & 1) == 0)
			{
				scrool_sprite_[3].sprite = SetCourtSprite(bg_path_, bg_data_.data[etc20_].name_);
				pos_x = 2213f;
				pos_x3 = 3466f;
				speed = 0f - pos_x3 / (float)in_time;
				parts_sprite_[2].gameObject.transform.localPosition = new Vector3(0f, -540f, -20f);
				parts_sprite_[0].gameObject.transform.localPosition = new Vector3(pos_x3, -540f, -20f);
			}
			else
			{
				scrool_sprite_[3].sprite = SetCourtSprite(bg_path_, bg_data_.data[etc22_].name_);
				pos_x = -1280f;
				pos_x3 = -3493f;
				speed = 0f - pos_x3 / (float)in_time;
				parts_sprite_[0].gameObject.transform.localPosition = new Vector3(0f, -540f, -20f);
				parts_sprite_[2].gameObject.transform.localPosition = new Vector3(pos_x3, -540f, -20f);
			}
			scrool_sprite_[1].flipX = false;
			scrool_sprite_[1].gameObject.SetActive(true);
			scrool_sprite_[3].gameObject.SetActive(true);
			parts_sprite_[0].gameObject.SetActive(true);
			parts_sprite_[2].gameObject.SetActive(true);
			MessageWork activeMessageWork2 = MessageSystem.GetActiveMessageWork();
			if (activeMessageWork2.op_work[7] == ushort.MaxValue)
			{
				activeMessageWork2.op_work[6] = 0;
				activeMessageWork2.op_work[7] = 0;
			}
			break;
		}
		case 1u:
		{
			if ((in_play & 1) == 0)
			{
				scrool_sprite_[3].sprite = SetCourtSprite(bg_path_, bg_data_.data[etc22_].name_);
				pos_x = -2213f;
				pos_x2 = -4773f;
				pos_x3 = -6986f;
				speed = 0f - pos_x3 / (float)in_time;
				scrool_sprite_[1].flipX = true;
				scrool_sprite_[2].flipX = false;
				parts_sprite_[1].gameObject.transform.localPosition = new Vector3(0f, -540f, -20f);
				parts_sprite_[2].gameObject.transform.localPosition = new Vector3(pos_x3, -540f, -20f);
			}
			else
			{
				scrool_sprite_[3].sprite = SetCourtSprite(bg_path_, bg_data_.data[etc21_].name_);
				pos_x = 2213f;
				pos_x2 = 4773f;
				pos_x3 = 6986f;
				speed = 0f - pos_x3 / (float)in_time;
				scrool_sprite_[1].flipX = false;
				scrool_sprite_[2].flipX = true;
				parts_sprite_[2].gameObject.transform.localPosition = new Vector3(0f, -540f, -20f);
				parts_sprite_[1].gameObject.transform.localPosition = new Vector3(pos_x3, -540f, -20f);
			}
			scrool_sprite_[1].gameObject.SetActive(true);
			scrool_sprite_[2].gameObject.SetActive(true);
			scrool_sprite_[3].gameObject.SetActive(true);
			parts_sprite_[0].gameObject.SetActive(false);
			parts_sprite_[1].gameObject.SetActive(true);
			parts_sprite_[2].gameObject.SetActive(true);
			MessageWork activeMessageWork3 = MessageSystem.GetActiveMessageWork();
			if (activeMessageWork3.op_work[7] == ushort.MaxValue)
			{
				activeMessageWork3.op_work[6] = 0;
				activeMessageWork3.op_work[7] = 0;
			}
			break;
		}
		case 2u:
		{
			if ((in_play & 1) == 0)
			{
				scrool_sprite_[3].sprite = SetCourtSprite(bg_path_, bg_data_.data[etc20_].name_);
				pos_x = -2213f;
				pos_x3 = -3493f;
				speed = 0f - pos_x3 / (float)in_time;
				parts_sprite_[1].gameObject.transform.localPosition = new Vector3(0f, -540f, -20f);
				parts_sprite_[0].gameObject.transform.localPosition = new Vector3(pos_x3, -540f, -20f);
			}
			else
			{
				scrool_sprite_[3].sprite = SetCourtSprite(bg_path_, bg_data_.data[etc21_].name_);
				pos_x = 1280f;
				pos_x3 = 3493f;
				speed = 0f - pos_x3 / (float)in_time;
				parts_sprite_[0].gameObject.transform.localPosition = new Vector3(0f, -540f, -20f);
				parts_sprite_[1].gameObject.transform.localPosition = new Vector3(pos_x3, -540f, -20f);
			}
			scrool_sprite_[1].flipX = true;
			scrool_sprite_[1].gameObject.SetActive(true);
			scrool_sprite_[3].gameObject.SetActive(true);
			parts_sprite_[0].gameObject.SetActive(true);
			parts_sprite_[1].gameObject.SetActive(true);
			MessageWork activeMessageWork = MessageSystem.GetActiveMessageWork();
			if (activeMessageWork.op_work[7] == ushort.MaxValue)
			{
				activeMessageWork.op_work[6] = 0;
				activeMessageWork.op_work[7] = 0;
			}
			break;
		}
		default:
			Debug.LogWarning("----bgCtrl.CourtScrol Not Move No!! in_mov_no:" + in_mov_no);
			break;
		}
		scrool_sprite_[1].gameObject.transform.localPosition = new Vector3(pos_x, 0f, 1f);
		scrool_sprite_[2].gameObject.transform.localPosition = new Vector3(pos_x2, 0f, 1f);
		scrool_sprite_[3].gameObject.transform.localPosition = new Vector3(pos_x3, 0f, 0f);
		next_characterID_in_court_scroll = (int)next_char;
		next_posX_in_court_scroll = pos_x3;
		float pos = 0f;
		uint half_time = in_time / 2;
		bool is_anime = false;
		AnimationObject character_object = null;
		while (true)
		{
			float move = speed * scroll_rate.Evaluate((float)timer / (float)in_time);
			pos += move;
			SetBodyPosition(new Vector3(pos, 0f, 0f));
			AnimationSystem.Instance.Scroll(new Vector3(move, 0f, 0f));
			for (int i = 0; i < parts_sprite_.Count; i++)
			{
				parts_sprite_[i].transform.localPosition += new Vector3(move, 0f, 0f);
			}
			timer++;
			if (timer > half_time)
			{
				if (!is_anime)
				{
					is_anime = true;
					AnimationSystem animationSystem = AnimationSystem.Instance;
					if (GSStatic.global_work_.title == TitleId.GS3 && (long)animationSystem.IdlingCharacterMasked == 33)
					{
						AnimationObject animationObject = animationSystem.FindObject(0, 0, 220);
						if (animationObject != null)
						{
							animationObject.Stop(true);
						}
					}
					animationSystem.StopCharacters();
					character_object = animationSystem.PlayCharacter((int)GSStatic.global_work_.title, (int)next_char, (int)in_adr, (int)in_adr);
					if (character_object != null)
					{
						character_object.transform.localPosition = new Vector3(pos + pos_x3, 0f, character_object.transform.localPosition.z);
						if ((MessageSystem.GetActiveMessageWork().status2 & MessageSystem.Status2.MV_MONO) != 0)
						{
							animationSystem.Char_monochrome(1, 31, 0, true);
						}
						GSStatic.tantei_work_.person_flag = 1;
						GSStatic.obj_work_[1].h_num = (byte)next_char;
						GSStatic.obj_work_[1].foa = (ushort)in_adr;
						GSStatic.obj_work_[1].idlingFOA = (ushort)next_char;
						if (GSStatic.global_work_.title == TitleId.GS3)
						{
							if (AnimationSystem.Instance.IdlingCharacterMasked == 13 && AnimationSystem.Instance.CharacterAnimationObject.ObjectFOA == 213)
							{
								Vector3 localPosition = character_object.transform.localPosition;
								localPosition.y = -324f;
								character_object.transform.localPosition = localPosition;
							}
							if ((long)AnimationSystem.Instance.IdlingCharacterMasked == 11 && (in_play & 1) == 0 && (in_mov_no == 0 || in_mov_no == 2) && GSStatic.message_work_.choustate == 6)
							{
								GSStatic.message_work_.choustate = 9;
							}
						}
						if (GSStatic.global_work_.title == TitleId.GS2 && (long)AnimationSystem.Instance.IdlingCharacterMasked == 32)
						{
							for (int j = 62; (long)j <= 68L; j++)
							{
								AnimationObject animationObject2 = AnimationSystem.Instance.FindObject((int)GSStatic.global_work_.title, 0, j);
								if (animationObject2 != null)
								{
									animationObject2.transform.localPosition = new Vector3(character_object.transform.localPosition.x, character_object.transform.localPosition.y, animationObject2.transform.localPosition.z);
								}
							}
						}
					}
				}
			}
			else if (in_mov_no == 1 && (in_play & 1) == 0)
			{
				AnimationObject animationObject3 = AnimationSystem.Instance.FindObject(2, 0, 221);
				if (animationObject3 != null)
				{
					animationObject3.Stop(true);
				}
			}
			if (timer >= in_time)
			{
				break;
			}
			yield return null;
		}
		if ((MessageSystem.GetActiveMessageWork().status2 & MessageSystem.Status2.MV_WHITE) != 0)
		{
			foreach (SpriteRenderer item3 in scrool_sprite_)
			{
				item3.material = default_material_;
			}
		}
		MessageSystem.GetActiveMessageWork().status2 &= ~MessageSystem.Status2.MV_BLACK;
		MessageSystem.GetActiveMessageWork().status2 &= ~MessageSystem.Status2.MV_WHITE;
		MessageSystem.GetActiveMessageWork().status2 &= ~MessageSystem.Status2.MV_MONO;
		foreach (SpriteRenderer item4 in scrool_sprite_)
		{
			item4.color = Color.white;
		}
		SetBodyPosition(Vector3.zero);
		if (character_object != null)
		{
			character_object.transform.localPosition = new Vector3(0f, 0f, character_object.transform.localPosition.z);
			if (GSStatic.global_work_.title == TitleId.GS2)
			{
				if ((long)AnimationSystem.Instance.IdlingCharacterMasked == 32)
				{
					for (int k = 62; (long)k <= 68L; k++)
					{
						AnimationObject animationObject4 = AnimationSystem.Instance.FindObject((int)GSStatic.global_work_.title, 0, k);
						if (animationObject4 != null)
						{
							animationObject4.transform.localPosition = new Vector3(character_object.transform.localPosition.x, character_object.transform.localPosition.y, animationObject4.transform.localPosition.z);
						}
					}
				}
			}
			else if (GSStatic.global_work_.title == TitleId.GS3 && AnimationSystem.Instance.IdlingCharacterMasked == 13 && AnimationSystem.Instance.CharacterAnimationObject.ObjectFOA == 213)
			{
				Vector3 localPosition2 = character_object.transform.localPosition;
				localPosition2.y = -324f;
				character_object.transform.localPosition = localPosition2;
			}
		}
		scrool_sprite_[0].gameObject.SetActive(false);
		scrool_sprite_[1].gameObject.SetActive(false);
		scrool_sprite_[2].gameObject.SetActive(false);
		scrool_sprite_[3].gameObject.SetActive(false);
		parts_sprite_[0].gameObject.transform.localPosition = new Vector3(0f, -540f, -20f);
		parts_sprite_[1].gameObject.transform.localPosition = new Vector3(0f, -540f, -20f);
		parts_sprite_[2].gameObject.transform.localPosition = new Vector3(0f, -540f, -20f);
		parts_sprite_[0].gameObject.SetActive(false);
		parts_sprite_[1].gameObject.SetActive(false);
		parts_sprite_[2].gameObject.SetActive(false);
		switch (in_mov_no)
		{
		case 0u:
			if ((in_play & 1) == 0)
			{
				sprite_renderer_.sprite = SetCourtSprite(bg_path_, bg_data_.data[etc20_].name_);
				parts_sprite_[0].gameObject.SetActive(true);
				SetBgNoEndScroll(0);
			}
			else
			{
				sprite_renderer_.sprite = SetCourtSprite(bg_path_, bg_data_.data[etc22_].name_);
				parts_sprite_[2].gameObject.SetActive(true);
				SetBgNoEndScroll(2);
			}
			break;
		case 1u:
			if ((in_play & 1) == 0)
			{
				sprite_renderer_.sprite = SetCourtSprite(bg_path_, bg_data_.data[etc22_].name_);
				parts_sprite_[2].gameObject.SetActive(true);
				SetBgNoEndScroll(2);
			}
			else
			{
				sprite_renderer_.sprite = SetCourtSprite(bg_path_, bg_data_.data[etc21_].name_);
				parts_sprite_[1].gameObject.SetActive(true);
				SetBgNoEndScroll(1);
			}
			break;
		case 2u:
			if ((in_play & 1) == 0)
			{
				sprite_renderer_.sprite = SetCourtSprite(bg_path_, bg_data_.data[etc20_].name_);
				parts_sprite_[0].gameObject.SetActive(true);
				SetBgNoEndScroll(0);
			}
			else
			{
				sprite_renderer_.sprite = SetCourtSprite(bg_path_, bg_data_.data[etc21_].name_);
				parts_sprite_[1].gameObject.SetActive(true);
				SetBgNoEndScroll(1);
			}
			break;
		default:
			Debug.LogWarning("----bgCtrl.CourtScrol Not Move No!! in_mov_no:" + in_mov_no);
			break;
		}
		MessageWork activeMessageWork4 = MessageSystem.GetActiveMessageWork();
		if (activeMessageWork4.op_work[7] == ushort.MaxValue)
		{
			activeMessageWork4.op_work[6] = 0;
			activeMessageWork4.op_work[7] = 0;
		}
		enumerator_court_ = null;
		is_scrolling_court_ = false;
		next_characterID_in_court_scroll = 0;
		next_posX_in_court_scroll = 0f;
	}

	private IEnumerator Coroutine_darkness_scroll()
	{
		yield return null;
		uint in_time = 31u;
		is_scrolling_court_ = true;
		int timer = 0;
		float speed = 5070f / (float)in_time;
		float pos_x3 = 0f;
		pos_x3 = 6986f;
		speed = 0f - pos_x3 / (float)in_time;
		float pos = 0f;
		uint half_time = in_time / 2;
		bool is_anime = false;
		AnimationSystem animationSystem = AnimationSystem.Instance;
		AnimationObject anim_object = null;
		while (true)
		{
			float move = speed * scroll_rate.Evaluate((float)timer / (float)in_time);
			pos += move;
			AnimationSystem.Instance.Scroll(new Vector3(move, 0f, 0f));
			timer++;
			if (timer > half_time && !is_anime)
			{
				is_anime = true;
				animationSystem.StopObject((int)GSStatic.global_work_.title, 0, 208);
				anim_object = animationSystem.PlayObject((int)GSStatic.global_work_.title, 0, 209);
				anim_object.transform.localPosition = new Vector3(pos + pos_x3, 0f, anim_object.transform.localPosition.z);
			}
			if (timer >= in_time)
			{
				break;
			}
			yield return null;
		}
		if (anim_object != null)
		{
			anim_object.transform.localPosition = new Vector3(0f, 0f, anim_object.transform.localPosition.z);
		}
		enumerator_court_ = null;
		is_scrolling_court_ = false;
	}

	public IEnumerator Slider()
	{
		if (enumerator_slider_ == null)
		{
			enumerator_slider_ = CoroutineSlider();
		}
		return enumerator_slider_;
	}

	private IEnumerator CoroutineSlider()
	{
		is_slider_ = true;
		Bg256_dir = 0;
		if (sprite_renderer_.sprite.rect.width > 1920f)
		{
			float speed = 30f;
			if (bg_pos_x_ > 0f)
			{
				Bg256_dir |= 16;
				while (true)
				{
					AnimationSystem.Instance.Scroll(new Vector3(Mathf.Min(speed, bg_pos_x_), bg_pos_y_, 0f));
					bg_pos_x_ -= speed;
					if (bg_pos_x_ < 0f)
					{
						break;
					}
					SetBodyPosition(new Vector3(0f - bg_pos_x_, bg_pos_y_, 0f));
					yield return null;
				}
				bg_pos_x_ = 0f;
				Bg256_dir = 0;
				Bg256_dir |= 32;
				SetBodyPosition(new Vector3(0f, 0f, 0f));
			}
			else
			{
				float end_pos = sprite_renderer_.sprite.rect.width - 1920f;
				Bg256_dir |= 32;
				while (true)
				{
					AnimationSystem.Instance.Scroll(new Vector3(0f - Mathf.Min(speed, end_pos - bg_pos_x_), bg_pos_y_, 0f));
					bg_pos_x_ += speed;
					if (bg_pos_x_ > end_pos)
					{
						break;
					}
					SetBodyPosition(new Vector3(0f - bg_pos_x_, bg_pos_y_, 0f));
					yield return null;
				}
				bg_pos_x_ = end_pos;
				Bg256_dir = 0;
				Bg256_dir |= 16;
				SetBodyPosition(new Vector3(0f - end_pos, 0f, 0f));
			}
		}
		is_reverse_ = !is_reverse_;
		is_slider_ = false;
		enumerator_slider_ = null;
	}

	public void StartCutUpScroll(int _face_no)
	{
		is_cutup_scroll_ = true;
		coroutineCtrl.instance.Play(CoroutineCutUpScroll(_face_no));
	}

	public void StopCutUpScroll()
	{
		is_cutup_scroll_ = false;
	}

	private IEnumerator CoroutineCutUpScroll(int _face_no)
	{
		int direction = 1;
		if (0 <= _face_no)
		{
			if (cutup_sprite_.texture == null)
			{
				AssetBundle assetBundle = AssetBundleCtrl.instance.load("/GS1/BG/", "bg042");
				if (assetBundle != null)
				{
					Sprite sprite = assetBundle.LoadAsset<Sprite>("bg042");
					cutup_sprite_.texture = sprite.texture;
				}
			}
			cutup_scroll_.SetActive(true);
			if (_face_no != 0)
			{
				if (GSStatic.global_work_.title == TitleId.GS3)
				{
					if (_face_no != 1 && _face_no != 3)
					{
						direction = -1;
					}
				}
				else
				{
					direction = -1;
				}
			}
			float pos_x = 0f;
			while (is_cutup_scroll_)
			{
				if (pos_x < -5f || 5f < pos_x)
				{
					pos_x = 0f;
				}
				cutup_sprite_.uvRect = new Rect(pos_x, 0f, 1f, 1f);
				pos_x += cutup_speed_x_ * (float)direction;
				yield return null;
			}
			cutup_scroll_.SetActive(false);
			cutup_sprite_.uvRect = new Rect(0f, 0f, 1f, 1f);
			cutup_sprite_.texture = null;
		}
		yield return null;
	}

	public void ChangeSubSpriteAlpha(float alpha)
	{
		sub_sprite_.color = new Color(sub_sprite_.color.r, sub_sprite_.color.b, sub_sprite_.color.g, alpha);
	}

	public void ChangeSubSpritePosition(Vector3 pos)
	{
		sub_sprite_.transform.localPosition = pos;
	}

	public Vector3 SubSpritePosition()
	{
		return sub_sprite_.transform.localPosition;
	}

	public void ChangeSubSpriteEnable(bool enable)
	{
		sub_sprite_.gameObject.SetActive(enable);
	}

	public bool GetChangeSubSpriteEnable()
	{
		return sub_sprite_.gameObject.activeSelf;
	}

	public void DetentionBgFadeInit(bool _fade_in)
	{
		float a = ((!_fade_in) ? 1f : 0f);
		sprite_renderer_.color = new Color(sprite_renderer_.color.r, sprite_renderer_.color.b, sprite_renderer_.color.g, a);
		sub_sprite_.color = new Color(sub_sprite_.color.r, sub_sprite_.color.b, sub_sprite_.color.g, a);
	}

	public void DetentionBgFade(float _rate)
	{
		sprite_renderer_.color = new Color(sprite_renderer_.color.r, sprite_renderer_.color.b, sprite_renderer_.color.g, _rate);
		sub_sprite_.color = new Color(sub_sprite_.color.r, sub_sprite_.color.b, sub_sprite_.color.g, _rate);
	}

	public void DetentionBgFadeInit(uint in_status, uint in_time, uint in_speed)
	{
		int num;
		switch (in_status)
		{
		case 0u:
			return;
		case 1u:
			num = 0;
			break;
		case 2u:
			num = 1;
			break;
		case 3u:
			num = 0;
			break;
		case 4u:
			num = 1;
			break;
		case 5u:
			num = 1;
			break;
		case 6u:
			num = 0;
			break;
		default:
			num = 0;
			break;
		}
		bool in_type = (byte)num != 0;
		Color color = sprite_renderer_.color;
		Color color2 = sub_sprite_.color;
		fadeCtrl.instance.status = (fadeCtrl.Status)in_status;
		coroutineCtrl.instance.Play(play_speed_coroutine(in_time, in_speed, in_type, color, color2));
	}

	public IEnumerator play_speed_coroutine(uint in_time, uint in_speed, bool in_type, Color in_color_main, Color in_color_sub)
	{
		float alpha2 = ((!in_type) ? 0f : 1f);
		sprite_renderer_.color = new Color(in_color_main.r, in_color_main.g, in_color_main.b, alpha2);
		sub_sprite_.color = new Color(in_color_sub.r, in_color_sub.g, in_color_sub.b, alpha2);
		if (in_speed == 0)
		{
			alpha2 = ((!in_type) ? 1f : 0f);
			sprite_renderer_.color = new Color(in_color_main.r, in_color_main.g, in_color_main.b, alpha2);
			sub_sprite_.color = new Color(in_color_sub.r, in_color_sub.g, in_color_sub.b, alpha2);
		}
		else
		{
			uint timer = 0u;
			uint rate = 0u;
			while (true)
			{
				timer++;
				if (timer >= in_time)
				{
					timer = 0u;
					rate += in_speed;
					if (rate >= 16)
					{
						break;
					}
					float num = (float)rate / 16f;
					alpha2 = ((!in_type) ? (alpha2 + num) : (alpha2 - num));
					sprite_renderer_.color = new Color(in_color_main.r, in_color_main.g, in_color_main.b, alpha2);
					sub_sprite_.color = new Color(in_color_sub.r, in_color_sub.g, in_color_sub.b, alpha2);
				}
				yield return null;
			}
			alpha2 = ((!in_type) ? 1f : 0f);
			sprite_renderer_.color = new Color(in_color_main.r, in_color_main.g, in_color_main.b, alpha2);
			sub_sprite_.color = new Color(in_color_sub.r, in_color_sub.g, in_color_sub.b, alpha2);
		}
		fadeCtrl.instance.status = fadeCtrl.Status.NO_FADE;
	}

	public void setSpritePosZ(float in_pos_z)
	{
		Vector3 vector = (bg_origin_pos = sprite_renderer_.transform.localPosition);
		sprite_renderer_.transform.localPosition = new Vector3(vector.x, vector.y, in_pos_z);
	}

	public void resetSpritePosZ()
	{
		Vector3 localPosition = sprite_renderer_.transform.localPosition;
		sprite_renderer_.transform.localPosition = new Vector3(localPosition.x, localPosition.y, bg_origin_pos.z);
	}

	public void Bg256_set_center(uint no)
	{
		bg_pos_x_ = 960f;
		bg_pos_y_ = 0f;
		SetBodyPosition(new Vector3(0f - bg_pos_x_, bg_pos_y_, 0f));
	}

	public void setNegaPosi(bool flg, bool sub = true)
	{
		Material material = ((!flg) ? default_material_ : invert_color_material_);
		sprite_renderer_.material = material;
		if (sub)
		{
			sub_sprite_.material = material;
		}
		foreach (SealData item in seal_list_)
		{
			item.sprite_.material = material;
		}
		GSStatic.bg_save_data.negaposi = flg;
		GSStatic.bg_save_data.negaposi_sub = sub;
	}

	public void setAlpha(float alpha)
	{
		sprite_renderer_.color = new Color(sprite_renderer_.color.r, sprite_renderer_.color.b, sprite_renderer_.color.g, alpha);
		foreach (SealData item in seal_list_)
		{
			item.sprite_.color = new Color(item.sprite_.color.r, item.sprite_.color.b, item.sprite_.color.g, alpha);
		}
	}

	public void setColor(Color color)
	{
		sprite_renderer_.color = color;
		sub_sprite_.color = color;
		foreach (SealData item in seal_list_)
		{
			item.sprite_.color = color;
		}
	}

	public Color getColor()
	{
		return sprite_renderer_.color;
	}

	public void setVisible(bool on_off)
	{
		sprite_renderer_.enabled = on_off;
		cutup_scroll_.SetActive(on_off);
	}

	public void BG256_main()
	{
		if (GSStatic.global_work_.title == TitleId.GS2)
		{
			GS2_BG256_main();
		}
	}

	private void GS2_BG256_main()
	{
		if ((long)bg_no_ == 26)
		{
			if (gs2_op_animation_sprites_ == null)
			{
				AssetBundleCtrl assetBundleCtrl = AssetBundleCtrl.instance;
				gs2_op_animation_sprites_ = new Sprite[16];
				for (int i = 0; i < gs2_op_animation_sprites_.Length; i++)
				{
					string in_name = "bgg" + i.ToString("000");
					AssetBundle assetBundle = assetBundleCtrl.load("/GS2/BG/", in_name);
					if (assetBundle != null)
					{
						gs2_op_animation_sprites_[i] = assetBundle.LoadAsset<Sprite>(in_name);
					}
				}
				gs2_op_animation_timer_ = 0;
				gs2_op_animation_no_ = 0;
				sprite_renderer_.sprite = gs2_op_animation_sprites_[gs2_op_animation_no_];
			}
			gs2_op_animation_timer_++;
			if (gs2_op_animation_timer_ >= 10)
			{
				gs2_op_animation_timer_ = 0;
				gs2_op_animation_no_++;
				gs2_op_animation_no_ %= gs2_op_animation_sprites_.Length;
				sprite_renderer_.sprite = gs2_op_animation_sprites_[gs2_op_animation_no_];
			}
		}
		else if (gs2_op_animation_sprites_ != null)
		{
			gs2_op_animation_sprites_ = null;
		}
		if ((long)bg_no_ == 120)
		{
			switch (op_state_)
			{
			case 0:
				SetBodyPosition(new Vector3(-2880f, -1080f, body_.transform.localPosition.z));
				op_state_++;
				op_count_ = 0;
				break;
			case 1:
				op_count_++;
				if (op_count_ > 192)
				{
					op_state_++;
				}
				break;
			case 2:
			{
				float num = 1f;
				Vector3 vector = new Vector3(0f, num * 1080f / 192f, 0f);
				SetBodyPosition(body_.transform.localPosition + vector);
				if (body_.transform.localPosition.y >= 0f)
				{
					SetBodyPosition(new Vector3(body_.transform.localPosition.x, 0f, body_.transform.localPosition.z));
					op_state_++;
				}
				break;
			}
			}
		}
		if ((long)bg_no_ != 122)
		{
			return;
		}
		switch (op_state_)
		{
		case 0:
			SetBodyPosition(new Vector3(-2880f, 0f, body_.transform.localPosition.z));
			op_state_++;
			op_count_ = 0;
			break;
		case 1:
		{
			float num2 = -16f;
			Vector3 vector2 = new Vector3(0f, num2 * 1080f / 192f, 0f);
			SetBodyPosition(body_.transform.localPosition + vector2);
			op_count_++;
			if (op_count_ > 14)
			{
				op_state_++;
			}
			break;
		}
		case 2:
			op_count_++;
			if (op_count_ >= 10)
			{
				op_state_++;
			}
			break;
		case 3:
			break;
		}
	}

	public void SetBodyPosition(Vector3 pos)
	{
		body_.transform.localPosition = pos;
	}

	public void Bg256_monochrome(ushort time, ushort speed, ushort sw, bool fadeIn)
	{
		if (spef_enumerator_court_ != null)
		{
			coroutineCtrl.instance.Stop(spef_enumerator_court_);
			spef_enumerator_court_ = null;
		}
		current_sw_ = sw;
		switch (current_sw_)
		{
		case 0:
			SetSpefMaterial(grayscale_material_);
			break;
		case 1:
			SetSpefMaterial(default_material_);
			break;
		case 3:
			SetSpefMaterial(black_material_);
			break;
		case 6:
			SetSpefMaterial(sepia_material_);
			break;
		default:
			SetSpefMaterial(default_material_);
			return;
		}
		if (fadeIn)
		{
			if (current_sw_ == 1)
			{
				spef_enumerator_court_ = SetSpefRed(time, speed);
			}
			else
			{
				spef_enumerator_court_ = SetSpef(time, speed);
			}
		}
		else if (current_sw_ == 1)
		{
			spef_enumerator_court_ = SetSpefRedRet(time, speed);
		}
		else
		{
			spef_enumerator_court_ = SetSpefRet(time, speed);
		}
		coroutineCtrl.instance.Play(spef_enumerator_court_);
	}

	private IEnumerator SetSpef(ushort time, ushort speed)
	{
		ushort timer = 0;
		float volume2 = ((speed < 32) ? ((float)(int)speed / 32f) : 1f);
		SetSpefVolume(volume2);
		do
		{
			ushort num;
			timer = (num = (ushort)(timer + 1));
			if (num >= time)
			{
				timer = 0;
				speed = (num = (ushort)(speed + 1));
				volume2 = (float)(int)num / 32f;
				if (speed >= 32)
				{
					volume2 = 1f;
				}
				SetSpefVolume(volume2);
			}
			yield return null;
		}
		while (speed < 32);
		spef_enumerator_court_ = null;
	}

	private IEnumerator SetSpefRed(ushort time, ushort speed)
	{
		red_bg_.enabled = true;
		ushort timer = 0;
		float volume2 = ((speed < 32) ? ((float)(int)speed / 32f) : 1f);
		red_bg_.color = new Color(1f, 0f, 0f, volume2);
		do
		{
			ushort num;
			timer = (num = (ushort)(timer + 1));
			if (num >= time)
			{
				timer = 0;
				speed = (num = (ushort)(speed + 1));
				volume2 = (float)(int)num / 32f;
				if (speed >= 32)
				{
					volume2 = 1f;
				}
				red_bg_.color = new Color(1f, 0f, 0f, volume2);
			}
			yield return null;
		}
		while (speed < 32);
		spef_enumerator_court_ = null;
	}

	private IEnumerator SetSpefRet(ushort time, ushort speed)
	{
		ushort timer = 0;
		float volume2 = ((speed < 32) ? ((float)(int)speed / 32f) : 1f);
		SetSpefVolume(volume2);
		do
		{
			ushort num;
			timer = (num = (ushort)(timer + 1));
			if (num >= time)
			{
				timer = 0;
				speed = (num = (ushort)(speed - 1));
				volume2 = (float)(int)num / 32f;
				if ((short)speed <= 0)
				{
					volume2 = 0f;
				}
				SetSpefVolume(volume2);
			}
			yield return null;
		}
		while ((short)speed > 0);
		current_sw_ = 32;
		SetSpefMaterial(default_material_);
		spef_enumerator_court_ = null;
	}

	private IEnumerator SetSpefRedRet(ushort time, ushort speed)
	{
		ushort timer = 0;
		float volume2 = ((speed < 32) ? ((float)(int)speed / 32f) : 1f);
		red_bg_.color = new Color(1f, 0f, 0f, volume2);
		do
		{
			ushort num;
			timer = (num = (ushort)(timer + 1));
			if (num >= time)
			{
				timer = 0;
				speed = (num = (ushort)(speed - 1));
				volume2 = (float)(int)num / 32f;
				if ((short)speed <= 0)
				{
					volume2 = 0f;
				}
				red_bg_.color = new Color(1f, 0f, 0f, volume2);
			}
			yield return null;
		}
		while ((short)speed > 0);
		red_bg_.enabled = false;
		current_sw_ = 32;
		spef_enumerator_court_ = null;
	}

	private void SetSpefMaterial(Material material)
	{
		sprite_renderer_.material = material;
		sub_sprite_.material = material;
		fore_renderer_.material = material;
		foreach (SealData item in seal_list_)
		{
			item.sprite_.material = material;
		}
		foreach (SpriteRenderer item2 in scrool_sprite_)
		{
			item2.material = material;
		}
		foreach (SpriteRenderer item3 in parts_sprite_)
		{
			item3.material = material;
		}
		image_.material = material;
	}

	private void SetSpefVolume(float volume)
	{
		SetSpefVolume(volume, sprite_renderer_);
		SetSpefVolume(volume, sub_sprite_);
		SetSpefVolume(volume, fore_renderer_);
		foreach (SealData item in seal_list_)
		{
			SetSpefVolume(volume, item.sprite_);
		}
		foreach (SpriteRenderer item2 in scrool_sprite_)
		{
			SetSpefVolume(volume, item2);
		}
		foreach (SpriteRenderer item3 in parts_sprite_)
		{
			SetSpefVolume(volume, item3);
		}
		SetSpefVolume(volume, image_);
	}

	private void SetSpefVolume(float volume, SpriteRenderer renderer)
	{
		MaterialPropertyBlock materialPropertyBlock = new MaterialPropertyBlock();
		renderer.GetPropertyBlock(materialPropertyBlock);
		materialPropertyBlock.SetFloat(spefCtrl.instance.volumePropetyId, volume);
		renderer.SetPropertyBlock(materialPropertyBlock);
	}

	public void ResetSpef()
	{
		current_sw_ = 32;
		SetSpefMaterial(default_material_);
	}

	public Transform GetPartsTransform(int index)
	{
		return parts_sprite_[index].transform;
	}

	public static int Bg_GetRyuchijyoBgNo()
	{
		uint[] array = new uint[3] { 30u, 17u, 18u };
		return (int)array[(int)GSStatic.global_work_.title];
	}

	public void SetEndingParts(int parts_id)
	{
		ending_table_.gameObject.SetActive(true);
		Vector3 localPosition = ending_table_.transform.localPosition;
		switch (parts_id)
		{
		case 1:
			ending_table_.sprite = SetCourtSprite("/GS1/etc/", "etc22");
			localPosition.x = -1980f;
			break;
		case 2:
			ending_table_.sprite = SetCourtSprite("/GS1/etc/", "etc21");
			localPosition.x = 1980f;
			break;
		default:
			ending_table_.sprite = null;
			break;
		}
		ending_table_.transform.localPosition = localPosition;
	}

	public void ResetEndingParts()
	{
		ending_table_.gameObject.SetActive(false);
	}

	private void SetBgNoEndScroll(int type)
	{
		bg_no_old_ = bg_no_;
		switch (type)
		{
		case 0:
			bg_no_ = etc20_;
			break;
		case 1:
			bg_no_ = etc21_;
			break;
		case 2:
			bg_no_ = etc22_;
			break;
		}
	}

	public void SetColorImage(Color color)
	{
		image_.color = color;
	}

	public void SetAlphaImage(float a)
	{
		image_.color = new Color(image_.color.r, image_.color.g, image_.color.b, a);
	}

	public void Bg256_set_ex2(uint num, uint flag)
	{
		uint num2 = num;
		uint type_ = bg_data_.data[(int)num2].type_;
		if (type_ == 2 && (flag & 0x10u) != 0)
		{
			num2 |= 0x8000u;
		}
		instance.SetSprite((int)num2);
	}

	public List<int> GetSealIDList()
	{
		List<int> list = new List<int>();
		foreach (SealData item in seal_list_)
		{
			if (item.active)
			{
				list.Add(item.id_);
			}
		}
		return list;
	}

	public void SetWhiteBG()
	{
		white_bg_ = true;
		sprite_renderer_.material = white_material_;
	}
}
