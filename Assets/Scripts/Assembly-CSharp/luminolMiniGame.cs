using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class luminolMiniGame : MonoBehaviour
{
	public static Vector2 offset = new Vector2(0f, 0f);

	public static Vector2 scale = new Vector2(5f, 5f);

	private luminolState state;

	private List<luminolTable> mg_chk_lumi_dmy = new List<luminolTable>();

	private List<luminolTable> mg_chk_lumi00 = new List<luminolTable>();

	private List<luminolTable> mg_chk_lumi01 = new List<luminolTable>();

	private List<luminolTable> mg_chk_lumi02 = new List<luminolTable>();

	private List<luminolTable> mg_chk_lumi03 = new List<luminolTable>();

	private List<luminolTable> mg_chk_lumi04 = new List<luminolTable>();

	private List<luminolTable> mg_chk_lumi05 = new List<luminolTable>();

	private List<luminolTable> mg_chk_lumi06 = new List<luminolTable>();

	private List<luminolTable> mg_chk_lumi07 = new List<luminolTable>();

	private List<List<luminolTable>> mg_chk_lumi_list = new List<List<luminolTable>>();

	private List<luminolBGTable> lumi_bg_tbl = new List<luminolBGTable>();

	private List<Vector2> lumi_bg_offset = new List<Vector2>();

	public bool reset_;

	private Vector2 body_size;

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

	[SerializeField]
public bgData bg_data_;

	[SerializeField]
public Transform body_;

	[SerializeField]
public RectTransform sc_body_;

	[SerializeField]
public RectTransform bg_body_;

	[SerializeField]
public RectTransform icon_;

	[SerializeField]
public SpriteRenderer icon_image_;

	[SerializeField]
public float appear_time_;

	[SerializeField]
public float cursor_speed_;

	[SerializeField]
public Transform effect_prefabs_;

	[SerializeField]
public Transform blood_stain_prefabs_;

	[SerializeField]
public List<luminolBloodstain> active_blood_ = new List<luminolBloodstain>();

	[SerializeField]
public List<luminolBloodstain> blood_ = new List<luminolBloodstain>();

	[SerializeField]
public SpriteRenderer bg_sprite_;

	[SerializeField]
public Transform blood_parent_;

	[SerializeField]
public Transform blood_parent_right_;

	[SerializeField]
public Transform blood_parent_left_;

	[SerializeField]
public Material m_default_;

	[SerializeField]
public Material m_red_;

	[SerializeField]
public InputTouch touch_area_;

	[Header("Debug")]
	[SerializeField]
public bool debug_show_area_;

	private int base_bg_id_;

	private int acquired_index_;

	private bool blood_anim_end_;

	private List<luminolTable> expl_ck_data_ = new List<luminolTable>();

	public List<GSPoint4> converted_point_ = new List<GSPoint4>();

	private bool is_end_ = true;

	private Sprite[] icon_sprites_;

	private Vector3 cursor_position_;

	private List<Sprite> bg_anim_sprite_data_ = new List<Sprite>();

	private GameObject debug_area_root_;

	public LineRenderer debug_area_;

	private bool backup_enable_;

	private uint backup_index_;

	private ushort backup_index_no_;

	private bool scenario_exit_;

	private const float SCREEN_MAX_WIDHT = 1920f;

	private const float KEY_GUIDE_HEIGHT = 900f;

	public static luminolMiniGame instance { get; private set; }

	public bool is_end
	{
		get
		{
			return is_end_;
		}
	}

	public bgData bg_data
	{
		get
		{
			return bg_data_;
		}
		set
		{
			bg_data_ = value;
		}
	}

	private void Awake()
	{
		instance = this;
		lumi_bg_tbl.Add(new luminolBGTable(117u, 146u, 147u, 129u));
		lumi_bg_tbl.Add(new luminolBGTable(114u, 172u, 173u, 176u));
		lumi_bg_tbl.Add(new luminolBGTable(115u, 135u, 136u, 137u));
		lumi_bg_tbl.Add(new luminolBGTable(115u, 132u, 133u, 134u));
		lumi_bg_tbl.Add(new luminolBGTable(116u, 174u, 175u, 177u));
		lumi_bg_tbl.Add(new luminolBGTable(0u, 0u, 0u, 0u));
		lumi_bg_tbl.Add(new luminolBGTable(117u, 146u, 147u, 129u));
		lumi_bg_tbl.Add(new luminolBGTable(117u, 170u, 171u, 180u));
		lumi_bg_tbl.Add(new luminolBGTable(117u, 194u, 195u, 196u));
		lumi_bg_tbl.Add(new luminolBGTable(118u, 168u, 169u, 179u));
		mg_chk_lumi_dmy.Add(new luminolTable(0u, 88u, 136u, 16u, 16u, 16u, 16u, 47u, scenario.SCE42_FLAG_BLOODSTAIN00, scenario.SC4_64440, 0f, 0f, lumi_bg_tbl[0], string.Empty));
		mg_chk_lumi00.Add(new luminolTable(0u, 173u, 181u, 32u, 16u, 24u, 16u, 23u, scenario.SCE42_FLAG_BLOODSTAIN06, scenario.EV0_64490, 0f, 0f, lumi_bg_tbl[1], "eff03c"));
		mg_chk_lumi01.Add(new luminolTable(0u, 298u, 96u, 16u, 16u, 16u, 16u, 30u, scenario.SCE42_FLAG_BLOODSTAIN02, scenario.EV0_64440, 0f, 0f, lumi_bg_tbl[2], "eff023"));
		mg_chk_lumi02.Add(new luminolTable(0u, 10u, 154u, 64u, 32u, 64u, 32u, 258u, scenario.SCE42_FLAG_BLOODSTAIN01, scenario.SC4_62520, 200f, 880f, lumi_bg_tbl[3], "eff022"));
		mg_chk_lumi03.Add(new luminolTable(0u, 64u, 176u, 64u, 32u, 64u, 32u, 258u, scenario.SCE42_FLAG_BLOODSTAIN07, scenario.EV0_64500, 10f, 0f, lumi_bg_tbl[4], "eff03d"));
		mg_chk_lumi04.Add(new luminolTable(0u, 10u, 186u, 64u, 32u, 64u, 32u, 258u, scenario.SCE42_FLAG_BLOODSTAIN07, scenario.EV0_64500, 10f, 0f, lumi_bg_tbl[5], string.Empty));
		mg_chk_lumi05.Add(new luminolTable(0u, 157u, 73u, 16u, 32u, 18u, 18u, 68u, scenario.SCE42_FLAG_BLOODSTAIN03, scenario.EV0_64480, 99f, -90f, lumi_bg_tbl[6], "eff039"));
		mg_chk_lumi06.Add(new luminolTable(0u, 115u, 148u, 64u, 16u, 52u, 16u, 163u, scenario.SCE42_FLAG_BLOODSTAIN05, scenario.EV0_64460, 0f, 0f, lumi_bg_tbl[7], "eff03b"));
		mg_chk_lumi06.Add(new luminolTable(0u, 101u, 82u, 16u, 16u, 18u, 18u, 79u, scenario.SCE42_FLAG_BLOODSTAIN08, scenario.EV0_64470, 0f, 0f, lumi_bg_tbl[8], "eff06f"));
		mg_chk_lumi07.Add(new luminolTable(0u, 72u, 62u, 16u, 16u, 16u, 16u, 78u, scenario.SCE42_FLAG_BLOODSTAIN04, scenario.EV0_64510, 0f, 0f, lumi_bg_tbl[9], "eff03a"));
		mg_chk_lumi_list.Add(mg_chk_lumi00);
		mg_chk_lumi_list.Add(mg_chk_lumi01);
		mg_chk_lumi_list.Add(mg_chk_lumi02);
		mg_chk_lumi_list.Add(mg_chk_lumi03);
		mg_chk_lumi_list.Add(mg_chk_lumi05);
		mg_chk_lumi_list.Add(mg_chk_lumi06);
		mg_chk_lumi_list.Add(mg_chk_lumi07);
		lumi_bg_offset.Add(new Vector2(0f, 0f));
		lumi_bg_offset.Add(new Vector2(0f, 0f));
		lumi_bg_offset.Add(new Vector2(-1f, 1f));
		lumi_bg_offset.Add(new Vector2(0f, 0f));
		lumi_bg_offset.Add(new Vector2(0f, 0f));
		lumi_bg_offset.Add(new Vector2(0f, 0f));
		lumi_bg_offset.Add(new Vector2(0f, 0f));
	}

	private void FixedUpdate()
	{
		Process();
	}

	private void Process()
	{
		if (state == luminolState.Search)
		{
			SubWindow sub_window_ = advCtrl.instance.sub_window_;
			Routine currentRoutine = sub_window_.GetCurrentRoutine();
			if (currentRoutine.r.no_3 != 1)
			{
				bg_body_.localPosition = new Vector3(0f - bgCtrl.instance.bg_pos_x, 0f, 0f);
			}
			else
			{
				bg_sprite_.transform.localPosition = new Vector3(-2880f, 0f, 0f);
			}
		}
		if (reset_)
		{
			mg_chk_lumi_list.Clear();
			mg_chk_lumi_list.Add(mg_chk_lumi00);
			mg_chk_lumi_list.Add(mg_chk_lumi01);
			mg_chk_lumi_list.Add(mg_chk_lumi02);
			mg_chk_lumi_list.Add(mg_chk_lumi03);
			mg_chk_lumi_list.Add(mg_chk_lumi05);
			mg_chk_lumi_list.Add(mg_chk_lumi06);
			mg_chk_lumi_list.Add(mg_chk_lumi07);
			Init();
			reset_ = false;
		}
	}

	public void Init()
	{
		BloodClear();
		SubWindow sub_window_ = advCtrl.instance.sub_window_;
		Routine currentRoutine = sub_window_.GetCurrentRoutine();
		if (currentRoutine.r.no_3 != 1)
		{
			uint bGType = bgData.instance.GetBGType(bgCtrl.instance.bg_no);
			if (bGType == 1 || bGType == 2)
			{
				coroutineCtrl.instance.Play(keyGuideCtrl.instance.open(keyGuideBase.Type.LUMINOL_SLIDE));
			}
			else
			{
				coroutineCtrl.instance.Play(keyGuideCtrl.instance.open(keyGuideBase.Type.LUMINOL));
			}
		}
		bg_sprite_.material = m_red_;
		base_bg_id_ = bgCtrl.instance.bg_no;
		SetExplCkData();
		blood_parent_.localPosition = Vector3.left * bgCtrl.instance.bg_pos_x;
		blood_anim_end_ = false;
		backup_enable_ = false;
		backup_index_ = 0u;
		backup_index_no_ = 0;
		scenario_exit_ = false;
		LoadAsset();
		is_end_ = false;
		body_.gameObject.SetActive(true);
		touch_area_.ActiveCollider();
		coroutineCtrl.instance.Play(MainCoroutine());
		DebugShowArea(debug_show_area_);
		touch_area_.touch_key_type = KeyType.A;
		touch_area_.touch_event = delegate
		{
			Vector3 vector = padCtrl.instance.InputMousePosition();
			vector.x *= body_size.x / (float)Screen.width;
			vector.y *= body_size.y / (float)Screen.height;
			vector.y = body_size.y - vector.y;
			cursor_position_ = vector;
			UpdateCursorPosition();
		};
	}

	private void SetExplCkData()
	{
		SubWindow sub_window_ = advCtrl.instance.sub_window_;
		Routine currentRoutine = sub_window_.GetCurrentRoutine();
		List<List<luminolTable>> list = new List<List<luminolTable>>();
		expl_ck_data_.Clear();
		if (currentRoutine.r.no_3 == 1)
		{
			expl_ck_data_.AddRange(mg_chk_lumi02);
			list.Add(mg_chk_lumi02);
			base_bg_id_ = (int)mg_chk_lumi02[0].bg.base_bg;
		}
		else
		{
			foreach (List<luminolTable> item in mg_chk_lumi_list)
			{
				if (item[0].bg.base_bg == base_bg_id_)
				{
					if (bg_data_.data[base_bg_id_].type_ == 0)
					{
						list.Add(item);
						expl_ck_data_.AddRange(item);
						break;
					}
					list.Add(item);
				}
			}
			if (bg_data_.data[base_bg_id_].type_ != 0)
			{
				if (bgCtrl.instance.bg_pos_x < 500f)
				{
					expl_ck_data_.AddRange(list[0]);
				}
				else if (list.Count > 1)
				{
					expl_ck_data_.AddRange(list[1]);
				}
			}
		}
		converted_point_ = ConvertPoint(expl_ck_data_);
		blood_.Clear();
		active_blood_.Clear();
		if (list.Count <= 0)
		{
			return;
		}
		foreach (luminolTable item2 in list[0])
		{
			Transform transform = Object.Instantiate(blood_stain_prefabs_);
			luminolBloodstain component = transform.GetComponent<luminolBloodstain>();
			if (currentRoutine.r.no_3 == 1)
			{
				active_blood_.Add(component);
				transform.parent = blood_parent_left_;
			}
			else
			{
				transform.parent = blood_parent_right_;
			}
			transform.localScale = Vector3.one;
			transform.localPosition = new Vector3(item2.pos_x + lumi_bg_offset[list.Count].x, item2.pos_y + lumi_bg_offset[list.Count].y);
			BloodstainState bloodstainState = BloodstainState.Undiscovered;
			if (GSFlag.Check(0u, item2.sce_flag))
			{
				bloodstainState = BloodstainState.Acquired;
			}
			component.Init(bloodstainState, BloodAnimEnd, item2.sprite_name);
			blood_.Add(component);
			if (bgCtrl.instance.bg_pos_x < 500f)
			{
				active_blood_.Add(component);
			}
		}
		if (list.Count <= 1)
		{
			return;
		}
		foreach (luminolTable item3 in list[1])
		{
			Transform transform2 = Object.Instantiate(blood_stain_prefabs_);
			transform2.parent = blood_parent_left_;
			transform2.localScale = Vector3.one;
			transform2.localPosition = new Vector3(item3.pos_x, item3.pos_y);
			luminolBloodstain component2 = transform2.GetComponent<luminolBloodstain>();
			BloodstainState bloodstainState2 = BloodstainState.Undiscovered;
			if (GSFlag.Check(0u, item3.sce_flag))
			{
				bloodstainState2 = BloodstainState.Acquired;
			}
			component2.Init(bloodstainState2, BloodAnimEnd, item3.sprite_name);
			blood_.Add(component2);
			if (bgCtrl.instance.bg_pos_x >= 500f)
			{
				active_blood_.Add(component2);
			}
		}
	}

	private void BloodClear()
	{
		foreach (luminolBloodstain item in blood_)
		{
			if (item != null)
			{
				Object.Destroy(item.gameObject);
			}
		}
		active_blood_.Clear();
		blood_.Clear();
	}

	private IEnumerator MainCoroutine()
	{
		SubWindow sub_window = advCtrl.instance.sub_window_;
		Routine routine = sub_window.GetCurrentRoutine();
		if (routine.r.no_3 == 1)
		{
			yield return coroutineCtrl.instance.Play(TutorialCoroutine());
		}
		state = luminolState.Search;
		yield return null;
		TouchSystem.TouchInActive();
		touch_area_.ActiveCollider();
		icon_image_.sprite = icon_sprites_[0];
		icon_.gameObject.SetActive(true);
		body_size = sc_body_.sizeDelta;
		cursor_position_ = new Vector3(960f, 540f, 0f);
		UpdateCursorPosition();
		yield return null;
		while (state == luminolState.Search)
		{
			padCtrl pad = padCtrl.instance;
			while (true)
			{
				Vector3 direction = Vector3.zero;
				Vector3 dist = Vector3.zero;
				if (padCtrl.instance.axis_pos_L.x > 0f || padCtrl.instance.axis_pos_L.x < 0f)
				{
					dist.x = padCtrl.instance.axis_pos_L.x * (cursor_speed_ * Time.deltaTime);
				}
				if (padCtrl.instance.axis_pos_L.y > 0f || padCtrl.instance.axis_pos_L.y < 0f)
				{
					dist.y = 0f - padCtrl.instance.axis_pos_L.y * (cursor_speed_ * Time.deltaTime);
				}
				if (pad.GetKey(KeyType.Left))
				{
					direction.x = -1f;
				}
				if (pad.GetKey(KeyType.Right))
				{
					direction.x = 1f;
				}
				if (pad.GetKey(KeyType.Up))
				{
					direction.y = -1f;
				}
				if (pad.GetKey(KeyType.Down))
				{
					direction.y = 1f;
				}
				if (routine.r.no_3 != 1 && pad.GetKeyDown(KeyType.B))
				{
					soundCtrl.instance.PlaySE(44);
					state = luminolState.Wait;
					break;
				}
				if (direction != Vector3.zero)
				{
					direction.Normalize();
					dist = direction * (cursor_speed_ * Time.deltaTime);
					dist += direction;
				}
				if (dist != Vector3.zero)
				{
					cursor_position_ += dist;
				}
				if (cursor_position_.x < 0f)
				{
					cursor_position_.x = 0f;
				}
				else if (cursor_position_.x > body_size.x)
				{
					cursor_position_.x = body_size.x;
				}
				if (cursor_position_.y < 0f)
				{
					cursor_position_.y = 0f;
				}
				else if (cursor_position_.y > body_size.y)
				{
					cursor_position_.y = body_size.y;
				}
				float guide_limit = 1920f - keyGuideCtrl.instance.GetGuideWidth() - keyGuideCtrl.instance.guide_space;
				if (cursor_position_.y > 900f && cursor_position_.x > guide_limit)
				{
					cursor_position_.y = Mathf.Abs(icon_.localPosition.y);
				}
				if (cursor_position_.x > guide_limit && cursor_position_.y > 900f)
				{
					cursor_position_.x = guide_limit;
				}
				UpdateCursorPosition();
				if (padCtrl.instance.GetKeyDown(KeyType.A))
				{
					int hit = CheckHit();
					soundCtrl.instance.PlaySE(420);
					Transform t = Object.Instantiate(effect_prefabs_);
					t.position = icon_.position;
					if (hit != -1 && active_blood_[hit].state_ == BloodstainState.Undiscovered)
					{
						active_blood_[hit].AppearBlood();
						if (active_blood_[hit].discovery_count_ <= 0)
						{
							icon_.gameObject.SetActive(false);
							state = luminolState.Acquired;
							acquired_index_ = hit;
							AssetBundleCtrl asset_bundle_ctrl = AssetBundleCtrl.instance;
							for (int i = 0; i < 3; i++)
							{
								AssetBundle assetBundle = asset_bundle_ctrl.load("/GS1/BG/", bgCtrl.instance.GetBGName((int)expl_ck_data_[hit].bg.bg_anime_[i]));
								bg_anim_sprite_data_.AddRange(assetBundle.LoadAllAssets<Sprite>());
							}
							yield return null;
							break;
						}
					}
				}
				if (pad.GetKeyDown(KeyType.L) && routine.r.no_3 != 1 && GSMain_TanteiPart.IsBGSlide(bgCtrl.instance.bg_no))
				{
					soundCtrl.instance.PlaySE(43);
					coroutineCtrl.instance.Play(bgCtrl.instance.Slider());
					while (bgCtrl.instance.is_slider)
					{
						blood_parent_.localPosition = Vector3.left * bgCtrl.instance.bg_pos_x;
						yield return null;
					}
					blood_parent_.localPosition = Vector3.left * bgCtrl.instance.bg_pos_x;
					BloodClear();
					SetExplCkData();
				}
				yield return null;
			}
			if (state == luminolState.Acquired)
			{
				blood_anim_end_ = false;
				while (!blood_anim_end_)
				{
					yield return null;
				}
				GSFlag.Set(0u, expl_ck_data_[acquired_index_].sce_flag, 1u);
				coroutineCtrl.instance.Play(keyGuideCtrl.instance.open(keyGuideBase.Type.LUMINOL_INSPECT));
				yield return coroutineCtrl.instance.Play(AcquiredCoroutine());
			}
		}
		if (!scenario_exit_)
		{
			if (routine.r.no_3 == 1)
			{
				AnimationSystem.Instance.StopCharacters();
				advCtrl.instance.message_system_.SetMessage(scenario.SC4_62525);
				sub_window.req_ = SubWindow.Req.NONE;
				while (sub_window.req_ != SubWindow.Req.SELECT)
				{
					yield return null;
				}
				sub_window.req_ = SubWindow.Req.NONE;
				sub_window.busy_ = 0u;
				sub_window.stack_++;
				sub_window.GetCurrentRoutine().r.Set(3, 0, 0, 0);
				while (sub_window.GetCurrentRoutine().r.no_0 != 23)
				{
					yield return null;
				}
				sub_window.req_ = SubWindow.Req.NONE;
				sub_window.busy_ = 0u;
				while (sub_window.req_ != SubWindow.Req.MESS_EXIT)
				{
					yield return null;
				}
				sub_window.req_ = SubWindow.Req.NONE;
				sub_window.busy_ = 0u;
				GSStatic.global_work_.Mess_move_flag = 0;
				messageBoardCtrl.instance.board(false, false);
				bgCtrl.instance.bg_pos_x = 0f;
			}
			if (routine.r.no_3 == 0)
			{
				advCtrl.instance.message_system_.AddScript(GSStatic.global_work_.scenario);
				if (backup_enable_)
				{
					backup_enable_ = false;
					GSStatic.message_work_.mdt_index = backup_index_;
					GSStatic.message_work_.now_no = backup_index_no_;
				}
				GSStatic.global_work_.Mess_move_flag = 0;
				messageBoardCtrl.instance.board(false, false);
			}
			else if (routine.r.no_3 == 1)
			{
				GSStatic.global_work_.Mess_move_flag = 1;
			}
			if (AnimationSystem.Instance.CheckCharFade())
			{
				AnimationSystem.Instance.CharFade(3, 1);
			}
			while (AnimationSystem.Instance.isFade(4))
			{
				yield return null;
			}
			sub_window.bg_return_ = 0;
			sub_window.req_ = SubWindow.Req.NONE;
			sub_window.busy_ = 0u;
			sub_window.stack_ = 0;
			sub_window.GetCurrentRoutine().r.Set(5, 0, 0, 0);
			GSStatic.global_work_.r.Set(5, 1, 0, 0);
		}
		EndMiniGame();
		is_end_ = true;
	}

	private void BloodAnimEnd()
	{
		blood_anim_end_ = true;
	}

	private IEnumerator AcquiredCoroutine()
	{
		while (!padCtrl.instance.GetKeyDown(KeyType.A))
		{
			yield return null;
		}
		foreach (luminolBloodstain item in blood_)
		{
			item.StopFlashCursor();
		}
		coroutineCtrl.instance.Play(keyGuideCtrl.instance.open(keyGuideBase.Type.NO_GUIDE));
		soundCtrl.instance.PlaySE(43);
		int timer = 30;
		while (timer > 0)
		{
			timer--;
			yield return null;
		}
		foreach (luminolBloodstain item2 in blood_)
		{
			item2.body_.gameObject.SetActive(false);
		}
		state = luminolState.AcquiredAnim;
		yield return coroutineCtrl.instance.Play(AcquiredAnimCoroutine());
	}

	private IEnumerator AcquiredAnimCoroutine()
	{
		Sprite back_spirte = bg_sprite_.sprite;
		for (int i = 0; i < bg_anim_sprite_data_.Count; i++)
		{
			fadeCtrl.instance.play(3u, 1u, 4u);
			bg_sprite_.material = m_default_;
			bg_sprite_.transform.position = Vector3.zero;
			bg_sprite_.sprite = bg_anim_sprite_data_[i];
			soundCtrl.instance.PlaySE(93);
			float time = 0f;
			float wait = 0.5f;
			while (true)
			{
				time += Time.deltaTime;
				if (time > wait)
				{
					break;
				}
				yield return null;
			}
		}
		bg_anim_sprite_data_.Clear();
		SubWindow sub_window = advCtrl.instance.sub_window_;
		Routine routine = sub_window.GetCurrentRoutine();
		if (routine.r.no_3 != 1)
		{
			if (!backup_enable_)
			{
				backup_enable_ = true;
				backup_index_ = GSStatic.message_work_.mdt_index;
				backup_index_no_ = GSStatic.message_work_.now_no;
			}
			advCtrl.instance.message_system_.AddScript(65535u);
		}
		GSStatic.global_work_.Mess_move_flag = 1;
		advCtrl.instance.message_system_.SetMessage(expl_ck_data_[acquired_index_].message_id);
		sub_window.req_ = SubWindow.Req.NONE;
		while (sub_window.req_ != SubWindow.Req.LUMINOL_SCENARIO && sub_window.req_ != SubWindow.Req.MESS_EXIT)
		{
			yield return null;
		}
		if (sub_window.req_ == SubWindow.Req.MESS_EXIT)
		{
			itemPlateCtrl.instance.closeItem(false);
			if (routine.r.no_3 != 1)
			{
				state = luminolState.Search;
				uint bg_type = bgData.instance.GetBGType(bgCtrl.instance.bg_no);
				if (bg_type == 1 || bg_type == 2)
				{
					yield return coroutineCtrl.instance.Play(keyGuideCtrl.instance.open(keyGuideBase.Type.LUMINOL_SLIDE));
				}
				else
				{
					yield return coroutineCtrl.instance.Play(keyGuideCtrl.instance.open(keyGuideBase.Type.LUMINOL));
				}
				blood_parent_.localPosition = Vector3.left * bgCtrl.instance.bg_pos_x;
				BloodClear();
				SetExplCkData();
				bg_sprite_.sprite = back_spirte;
				bg_sprite_.material = m_red_;
				SetBGPosition(base_bg_id_, bg_sprite_);
				icon_.gameObject.SetActive(true);
				touch_area_.ActiveCollider();
				messageBoardCtrl.instance.InActiveNormalMessageNextTouch();
			}
		}
		if (sub_window.req_ == SubWindow.Req.LUMINOL_SCENARIO)
		{
			advCtrl.instance.message_system_.AddScript(GSStatic.global_work_.scenario);
			backup_enable_ = false;
			scenario_exit_ = true;
			advCtrl.instance.message_system_.SetMessage(scenario.SC4_64490);
			MessageSystem.Mess_window_set(5u);
			sub_window.bg_return_ = 0;
			sub_window.req_ = SubWindow.Req.NONE;
			sub_window.busy_ = 0u;
			sub_window.stack_ = 1;
			sub_window.GetCurrentRoutine().r.Set(10, 0, 0, 0);
			GSStatic.global_work_.r.Set(5, 1, 0, 0);
		}
	}

	private IEnumerator TutorialCoroutine()
	{
		state = luminolState.Tutorial;
		SubWindow sub_window = advCtrl.instance.sub_window_;
		MessageWork message_work = MessageSystem.GetActiveMessageWork();
		message_work.message_trans_flag = 1;
		GSStatic.global_work_.Mess_move_flag = 1;
		advCtrl.instance.message_system_.SetMessage(scenario.SC4_62515);
		while (sub_window.req_ != SubWindow.Req.LUMINOL_ANM)
		{
			yield return null;
		}
		Transform t = Object.Instantiate(effect_prefabs_);
		t.position = Vector3.zero;
		soundCtrl.instance.PlaySE(420);
		sub_window.req_ = SubWindow.Req.NONE;
		while (sub_window.req_ != SubWindow.Req.MESS_EXIT)
		{
			yield return null;
		}
		coroutineCtrl.instance.Play(keyGuideCtrl.instance.open(keyGuideBase.Type.LUMINOL_TUTORIAL));
	}

	public void EndMiniGame()
	{
		BloodClear();
		state = luminolState.Wait;
		icon_.gameObject.SetActive(false);
		body_.gameObject.SetActive(false);
	}

	private void LoadAsset()
	{
		AssetBundleCtrl assetBundleCtrl = AssetBundleCtrl.instance;
		AssetBundle assetBundle = assetBundleCtrl.load("/GS1/minigame/", "s2d050");
		icon_sprites_ = assetBundle.LoadAssetWithSubAssets<Sprite>("s2d050");
		bg_anim_sprite_data_.Clear();
		SetBGSprite(base_bg_id_, bg_sprite_);
	}

	private void UpdateCursorPosition()
	{
		Vector2 vector = cursor_position_;
		vector.y = 0f - vector.y;
		icon_.transform.localPosition = vector;
	}

	private List<GSPoint4> ConvertPoint(List<luminolTable> table_list)
	{
		List<GSPoint4> list = new List<GSPoint4>();
		foreach (luminolTable item2 in table_list)
		{
			GSPoint4 item = default(GSPoint4);
			item.x0 = (ushort)Mathf.FloorToInt(offset.x + (float)item2.x * scale.x);
			item.y0 = (ushort)Mathf.FloorToInt(offset.y + (float)item2.y * scale.y);
			item.x1 = (ushort)Mathf.FloorToInt(offset.x + (float)(item2.x + item2.w) * scale.x);
			item.y1 = (ushort)Mathf.FloorToInt(offset.y + (float)item2.y * scale.y);
			item.x2 = (ushort)Mathf.FloorToInt(offset.x + (float)(item2.x + item2.w) * scale.x);
			item.y2 = (ushort)Mathf.FloorToInt(offset.y + (float)(item2.y + item2.h) * scale.y);
			item.x3 = (ushort)Mathf.FloorToInt(offset.x + (float)item2.x * scale.x);
			item.y3 = (ushort)Mathf.FloorToInt(offset.y + (float)(item2.y + item2.h) * scale.y);
			list.Add(item);
		}
		return list;
	}

	private int CheckHit()
	{
		GSRect rect = new GSRect((short)(cursor_position_.x - 8f), (short)(cursor_position_.y - 8f), 16, 16);
		if (rect.x < 0)
		{
			rect.w += rect.x;
			rect.x = 0;
		}
		if (rect.y < 0)
		{
			rect.h += rect.y;
			rect.y = 0;
		}
		for (int i = 0; i < converted_point_.Count; i++)
		{
			if (GSUtility.ObjHitCheck2(rect, converted_point_[i]))
			{
				return i;
			}
		}
		return -1;
	}

	private void DebugShowArea(bool flag)
	{
		if (flag)
		{
			if (debug_area_root_ != null)
			{
				Object.Destroy(debug_area_root_.gameObject);
				debug_area_root_ = null;
			}
			debug_area_root_ = new GameObject("DebugAreaRoot");
			debug_area_root_.layer = body_.gameObject.layer;
			Transform transform = debug_area_root_.transform;
			transform.SetParent(sc_body_, false);
			transform.localPosition = new Vector3(0f, 0f, -10f);
			debug_area_ = new LineRenderer();
			for (int i = 0; i < converted_point_.Count; i++)
			{
				GSPoint4 gSPoint = converted_point_[i];
				GameObject gameObject = new GameObject("Area" + 0);
				gameObject.layer = debug_area_root_.layer;
				Transform transform2 = gameObject.transform;
				transform2.SetParent(transform, false);
				LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
				lineRenderer.useWorldSpace = false;
				lineRenderer.positionCount = 4;
				lineRenderer.SetPositions(new Vector3[4]
				{
					new Vector3((int)gSPoint.x0, -gSPoint.y0, 0f),
					new Vector3((int)gSPoint.x1, -gSPoint.y1, 0f),
					new Vector3((int)gSPoint.x2, -gSPoint.y2, 0f),
					new Vector3((int)gSPoint.x3, -gSPoint.y3, 0f)
				});
				lineRenderer.loop = true;
				Color endColor = (lineRenderer.startColor = Color.green);
				lineRenderer.endColor = endColor;
				lineRenderer.widthMultiplier = 4f;
				lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
				debug_area_ = lineRenderer;
			}
		}
		else if (debug_area_root_ != null)
		{
			debug_area_ = null;
			Object.Destroy(debug_area_root_);
			debug_area_root_ = null;
		}
	}

	private Sprite SetCourtSprite(string in_path, string in_name)
	{
		AssetBundle assetBundle = AssetBundleCtrl.instance.load(in_path, in_name);
		return assetBundle.LoadAllAssets<Sprite>()[0];
	}

	private void SetBGSprite(int in_bg_no, SpriteRenderer targetRenderer)
	{
		targetRenderer.gameObject.SetActive(false);
		bg_body_.transform.localPosition = Vector3.zero;
		if (in_bg_no < bg_data_.data.Count && !(bg_data_.data[in_bg_no].name_ == string.Empty))
		{
			if (bg_data_.data[in_bg_no].language_ != 32768 && GSStatic.global_work_.language == "USA")
			{
				targetRenderer.sprite = SetCourtSprite("/GS1/BG/", bg_data_.data_language[(int)bg_data_.data[in_bg_no].language_]);
			}
			else
			{
				targetRenderer.sprite = SetCourtSprite("/GS1/BG/", bg_data_.data[in_bg_no].name_);
			}
			targetRenderer.gameObject.SetActive(true);
			SetBGPosition(in_bg_no, targetRenderer);
		}
	}

	private void SetBGPosition(int in_bg_no, SpriteRenderer targetRenderer)
	{
		float num = 0f;
		float num2 = 0f;
		num2 = ((!(bg_sprite_.sprite.pivot.y > 0f)) ? (-540f) : ((!(bg_sprite_.sprite.pivot.y > bg_sprite_.sprite.rect.height * 0.5f)) ? 0f : 540f));
		num = 0f;
		if (bg_data_.data[in_bg_no].type_ == 1)
		{
			targetRenderer.gameObject.transform.localPosition = new Vector3(-960f, num2, 0f);
			num = targetRenderer.sprite.rect.width - 1920f;
			bg_body_.transform.localPosition = new Vector3(0f - num, 0f, 0f);
		}
		else if (bg_data_.data[in_bg_no].type_ == 2)
		{
			targetRenderer.gameObject.transform.localPosition = new Vector3(-960f, num2, 0f);
			num = 0f;
			bg_body_.transform.localPosition = new Vector3(0f - num, 0f, 0f);
		}
		else
		{
			targetRenderer.gameObject.transform.localPosition = new Vector3(0f, num2, 0f);
		}
		SubWindow sub_window_ = advCtrl.instance.sub_window_;
		Routine currentRoutine = sub_window_.GetCurrentRoutine();
		if (currentRoutine.r.no_3 != 1)
		{
			bg_body_.localPosition = new Vector3(0f - bgCtrl.instance.bg_pos_x, 0f, 0f);
		}
		else
		{
			bg_sprite_.transform.localPosition = new Vector3(-2880f, 0f, 0f);
		}
		AnimationSystem.Instance.instance_parent.localPosition = new Vector3(num, 0f, 0f);
	}
}
