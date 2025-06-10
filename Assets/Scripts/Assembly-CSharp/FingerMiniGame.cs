using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class FingerMiniGame : MonoBehaviour
{
	private class FINGER_TBL
	{
		public GSRect rect;

		public GSRect discover_rect;

		public INSPECT_DATA finger_inspect;

		public byte add_script;

		public ushort script_no;

		public ushort mes_no;

		public byte hit;

		public byte init_finger;

		public FINGER_TBL(GSRect rect, GSRect discover_rect, INSPECT_DATA finger_inspect, byte add_script, ushort script_no, ushort mes_no, byte hit, byte init_finger)
		{
			this.rect = rect;
			this.discover_rect = discover_rect;
			this.finger_inspect = finger_inspect;
			this.add_script = add_script;
			this.script_no = script_no;
			this.mes_no = mes_no;
			this.hit = hit;
			this.init_finger = init_finger;
		}

		public FINGER_TBL(GSRect rect, GSRect discover_rect, INSPECT_DATA finger_inspect, byte add_script, uint script_no, uint mes_no, uint hit, uint init_finger)
		{
			this.rect = rect;
			this.discover_rect = discover_rect;
			this.finger_inspect = finger_inspect;
			this.add_script = add_script;
			this.script_no = (ushort)script_no;
			this.mes_no = (ushort)mes_no;
			this.hit = (byte)hit;
			this.init_finger = (byte)init_finger;
		}
	}

	private class FINGER_INFO
	{
		public uint bg_no;

		public byte tbl_num;

		public byte correct;

		public FINGER_TBL[] ptbl;

		public FINGER_INFO(uint bg_no, byte tbl_num, byte correct, FINGER_TBL[] ptbl)
		{
			this.bg_no = bg_no;
			this.tbl_num = tbl_num;
			this.correct = correct;
			this.ptbl = ptbl;
		}
	}

	private const int SW_HUMAN_AKANE = 0;

	private const int SW_HUMAN_TOMOE = 1;

	private const int SW_HUMAN_ZAIMON = 2;

	private const int SW_HUMAN_KYOUKA = 3;

	private const int SW_HUMAN_TADASIKI = 4;

	private const int SW_HUMAN_HARABAI = 5;

	private const int SW_HUMAN_GANTO = 6;

	private const int SW_HUMAN_ITONOKO = 7;

	private static byte[] human_idx_tbl = new byte[8] { 6, 5, 2, 4, 7, 1, 0, 3 };

	private static FINGER_TBL[] finger_bg0a1_tbl = new FINGER_TBL[6]
	{
		new FINGER_TBL(new GSRect(72, 98, 16, 16), new GSRect(40, 16, 160, 136), new INSPECT_DATA(0u, 0u, scenario.SCE_DUMMY_FLAG, 70u, 98u, 84u, 95u, 97u, 111u, 80u, 121u), 1, 65535u, scenario.EV0_70000, 0u, 0u),
		new FINGER_TBL(new GSRect(92, 44, 16, 16), new GSRect(64, 8, 96, 128), new INSPECT_DATA(0u, 0u, scenario.SCE_DUMMY_FLAG, 91u, 42u, 104u, 42u, 108u, 64u, 91u, 63u), 1, 65535u, scenario.EV0_70000, 0u, 1u),
		new FINGER_TBL(new GSRect(117, 32, 16, 16), new GSRect(72, 16, 96, 128), new INSPECT_DATA(0u, 0u, scenario.SCE_DUMMY_FLAG, 116u, 30u, 132u, 32u, 133u, 47u, 116u, 71u), 1, 65535u, scenario.EV0_70000, 0u, 2u),
		new FINGER_TBL(new GSRect(140, 45, 16, 16), new GSRect(72, 8, 96, 128), new INSPECT_DATA(0u, 0u, scenario.SCE_DUMMY_FLAG, 143u, 42u, 155u, 45u, 154u, 57u, 135u, 66u), 1, 65535u, scenario.EV0_70000, 0u, 3u),
		new FINGER_TBL(new GSRect(158, 71, 16, 16), new GSRect(72, 8, 96, 128), new INSPECT_DATA(0u, 0u, scenario.SCE_DUMMY_FLAG, 156u, 73u, 167u, 68u, 172u, 80u, 156u, 85u), 1, 65535u, scenario.EV0_70000, 0u, 4u),
		new FINGER_TBL(new GSRect(48, 125, 16, 16), new GSRect(80, 40, 96, 128), new INSPECT_DATA(0u, 0u, scenario.SCE_DUMMY_FLAG, 44u, 128u, 61u, 121u, 72u, 142u, 55u, 143u), 1, 65535u, scenario.EV0_70000, 1u, 5u)
	};

	private static FINGER_TBL[] finger_bg0a2_tbl = new FINGER_TBL[2]
	{
		new FINGER_TBL(new GSRect(85, 46, 16, 16), new GSRect(72, 32, 96, 96), new INSPECT_DATA(0u, 0u, scenario.SCE_DUMMY_FLAG, 89u, 49u, 105u, 49u, 105u, 65u, 89u, 65u), 1, 65535u, scenario.EV0_70000, 0u, 6u),
		new FINGER_TBL(new GSRect(111, 32, 16, 16), new GSRect(80, 48, 96, 128), new INSPECT_DATA(0u, 0u, scenario.SCE_DUMMY_FLAG, 115u, 35u, 130u, 35u, 130u, 51u, 115u, 51u), 1, 65535u, scenario.EV0_70000, 1u, 7u)
	};

	private static FINGER_TBL[] finger_bg0a3_tbl = new FINGER_TBL[5]
	{
		new FINGER_TBL(new GSRect(58, 111, 16, 16), new GSRect(32, 24, 160, 96), new INSPECT_DATA(0u, 0u, scenario.SCE_DUMMY_FLAG, 64u, 111u, 77u, 118u, 76u, 131u, 58u, 117u), 1, 65535u, scenario.EV0_70000, 0u, 8u),
		new FINGER_TBL(new GSRect(81, 41, 16, 16), new GSRect(72, 8, 136, 152), new INSPECT_DATA(0u, 0u, scenario.SCE_DUMMY_FLAG, 81u, 40u, 94u, 43u, 92u, 61u, 81u, 52u), 1, 65535u, scenario.EV0_70000, 0u, 9u),
		new FINGER_TBL(new GSRect(159, 34, 16, 16), new GSRect(88, 16, 96, 144), new INSPECT_DATA(0u, 0u, scenario.SCE_DUMMY_FLAG, 160u, 34u, 173u, 35u, 170u, 51u, 159u, 48u), 1, 65535u, scenario.EV0_70000, 0u, 10u),
		new FINGER_TBL(new GSRect(187, 65, 16, 16), new GSRect(88, 24, 96, 136), new INSPECT_DATA(0u, 0u, scenario.SCE_DUMMY_FLAG, 187u, 68u, 198u, 64u, 199u, 75u, 187u, 79u), 1, 65535u, scenario.EV0_70000, 0u, 11u),
		new FINGER_TBL(new GSRect(120, 21, 16, 16), new GSRect(88, 48, 96, 128), new INSPECT_DATA(0u, 0u, scenario.SCE_DUMMY_FLAG, 123u, 21u, 134u, 21u, 133u, 41u, 121u, 39u), 1, 65535u, scenario.EV0_70000, 1u, 12u)
	};

	private static FINGER_INFO[] finger_info = new FINGER_INFO[3]
	{
		new FINGER_INFO(128u, 6, 7, finger_bg0a1_tbl),
		new FINGER_INFO(129u, 2, 2, finger_bg0a2_tbl),
		new FINGER_INFO(130u, 5, 0, finger_bg0a3_tbl)
	};

	private static MG_CHECK_TBL[] mg_chk_finger00 = new MG_CHECK_TBL[1]
	{
		new MG_CHECK_TBL(0, 45, 10, 234, 220, 234, 220, 23636.8f, scenario.SCE42_FLAG_BLOODSTAIN00, scenario.SC4_62520, "si2")
	};

	private static MG_CHECK_TBL[] mg_chk_finger01 = new MG_CHECK_TBL[1]
	{
		new MG_CHECK_TBL(0, 71, 0, 150, 240, 150, 240, 21471.201f, scenario.SCE42_FLAG_BLOODSTAIN00, scenario.SC4_62520, "si3")
	};

	private static MG_CHECK_TBL[] mg_chk_finger02 = new MG_CHECK_TBL[1]
	{
		new MG_CHECK_TBL(0, 77, 0, 152, 240, 152, 240, 20281.6f, scenario.SCE42_FLAG_BLOODSTAIN00, scenario.SC4_62520, "si4")
	};

	private static MG_CHECK_TBL[] mg_chk_finger03 = new MG_CHECK_TBL[1]
	{
		new MG_CHECK_TBL(0, 75, 0, 148, 234, 148, 234, 19503.201f, scenario.SCE42_FLAG_BLOODSTAIN00, scenario.SC4_62520, "si5")
	};

	private static MG_CHECK_TBL[] mg_chk_finger04 = new MG_CHECK_TBL[1]
	{
		new MG_CHECK_TBL(0, 85, 2, 132, 217, 132, 217, 14212.8f, scenario.SCE42_FLAG_BLOODSTAIN00, scenario.SC4_62520, "si6")
	};

	private static MG_CHECK_TBL[] mg_chk_finger05 = new MG_CHECK_TBL[1]
	{
		new MG_CHECK_TBL(0, 86, 0, 171, 232, 171, 232, 35200f, scenario.SCE42_FLAG_BLOODSTAIN00, scenario.SC4_62520, "si1")
	};

	private static MG_CHECK_TBL[] mg_chk_finger06 = new MG_CHECK_TBL[1]
	{
		new MG_CHECK_TBL(0, 98, 50, 101, 99, 101, 99, 4284f, scenario.SCE42_FLAG_BLOODSTAIN00, scenario.SC4_62520, "zaimon2")
	};

	private static MG_CHECK_TBL[] mg_chk_finger07 = new MG_CHECK_TBL[1]
	{
		new MG_CHECK_TBL(0, 73, 0, 175, 240, 175, 240, 36800f, scenario.SCE42_FLAG_BLOODSTAIN00, scenario.SC4_62520, "zaimon1")
	};

	private static MG_CHECK_TBL[] mg_chk_finger08 = new MG_CHECK_TBL[1]
	{
		new MG_CHECK_TBL(0, 41, 31, 207, 164, 207, 164, 5870.4f, scenario.SCE42_FLAG_BLOODSTAIN00, scenario.SC4_62520, "finger2_4")
	};

	private static MG_CHECK_TBL[] mg_chk_finger09 = new MG_CHECK_TBL[1]
	{
		new MG_CHECK_TBL(0, 48, 8, 212, 196, 212, 196, 17292.8f, scenario.SCE42_FLAG_BLOODSTAIN00, scenario.SC4_62520, "finger2_3")
	};

	private static MG_CHECK_TBL[] mg_chk_finger0A = new MG_CHECK_TBL[1]
	{
		new MG_CHECK_TBL(0, 113, 21, 117, 188, 117, 188, 9322.4f, scenario.SCE42_FLAG_BLOODSTAIN00, scenario.SC4_62520, "finger2_1")
	};

	private static MG_CHECK_TBL[] mg_chk_finger0B = new MG_CHECK_TBL[1]
	{
		new MG_CHECK_TBL(0, 103, 22, 127, 191, 127, 191, 7253.6f, scenario.SCE42_FLAG_BLOODSTAIN00, scenario.SC4_62520, "finger2_0")
	};

	private static MG_CHECK_TBL[] mg_chk_finger0C = new MG_CHECK_TBL[1]
	{
		new MG_CHECK_TBL(0, 72, 0, 173, 240, 173, 240, 37600f, scenario.SCE42_FLAG_BLOODSTAIN00, scenario.SC4_62520, "finger2_2")
	};

	private static MG_CHECK_INFO[] chk_inf_finger = new MG_CHECK_INFO[13]
	{
		new MG_CHECK_INFO(1, 1828, 128, mg_chk_finger00),
		new MG_CHECK_INFO(1, 991, 128, mg_chk_finger01),
		new MG_CHECK_INFO(1, 1320, 128, mg_chk_finger02),
		new MG_CHECK_INFO(1, 1667, 128, mg_chk_finger03),
		new MG_CHECK_INFO(1, 1117, 128, mg_chk_finger04),
		new MG_CHECK_INFO(2, 1956, 128, mg_chk_finger05),
		new MG_CHECK_INFO(1, 1694, 128, mg_chk_finger06),
		new MG_CHECK_INFO(2, 2504, 128, mg_chk_finger07),
		new MG_CHECK_INFO(1, 9969, 128, mg_chk_finger08),
		new MG_CHECK_INFO(1, 671, 128, mg_chk_finger09),
		new MG_CHECK_INFO(1, 1176, 128, mg_chk_finger0A),
		new MG_CHECK_INFO(1, 6360, 128, mg_chk_finger0B),
		new MG_CHECK_INFO(2, 2191, 128, mg_chk_finger0C)
	};

	private readonly string[] finger_bg_name_tbl = new string[8] { "etc03f", "etc040", "etc041", "etc042", "etc043", "etc044", "etc045", "etc046" };

	private readonly string[] finger_sprite_word_name = new string[10] { "etc022", "etc023", "etc024", "etc025", "etc026", "etc027", "etc028", "etc029", "etc02a", "etc02b" };

	private static readonly GSPoint[] finger_check_point0 = new GSPoint[16]
	{
		new GSPoint(53, 7),
		new GSPoint(88, 10),
		new GSPoint(59, 69),
		new GSPoint(7, 92),
		new GSPoint(26, 112),
		new GSPoint(21, 10),
		new GSPoint(22, 16),
		new GSPoint(42, 26),
		new GSPoint(21, 56),
		new GSPoint(49, 66),
		new GSPoint(80, 94),
		new GSPoint(85, 30),
		new GSPoint(89, 50),
		new GSPoint(80, 56),
		new GSPoint(61, 110),
		new GSPoint(39, 118)
	};

	private static readonly GSPoint[] finger_check_point1 = new GSPoint[16]
	{
		new GSPoint(10, 16),
		new GSPoint(64, 30),
		new GSPoint(42, 70),
		new GSPoint(72, 100),
		new GSPoint(12, 110),
		new GSPoint(70, 17),
		new GSPoint(55, 21),
		new GSPoint(9, 35),
		new GSPoint(36, 56),
		new GSPoint(89, 107),
		new GSPoint(61, 124),
		new GSPoint(81, 25),
		new GSPoint(14, 38),
		new GSPoint(5, 61),
		new GSPoint(13, 89),
		new GSPoint(40, 118)
	};

	private static readonly GSPoint[] finger_check_point2 = new GSPoint[16]
	{
		new GSPoint(81, 6),
		new GSPoint(84, 22),
		new GSPoint(14, 38),
		new GSPoint(41, 43),
		new GSPoint(76, 105),
		new GSPoint(49, 11),
		new GSPoint(55, 28),
		new GSPoint(59, 48),
		new GSPoint(46, 57),
		new GSPoint(55, 85),
		new GSPoint(30, 95),
		new GSPoint(7, 82),
		new GSPoint(10, 92),
		new GSPoint(89, 110),
		new GSPoint(73, 112),
		new GSPoint(28, 120)
	};

	private static readonly GSPoint[][] finger_check_point = new GSPoint[3][] { finger_check_point0, finger_check_point1, finger_check_point2 };

	private static readonly int[][] finger_scan_order = new int[18][]
	{
		new int[16]
		{
			0, 1, 2, 3, 4, 5, 6, 7, 8, 9,
			10, 11, 12, 13, 14, 15
		},
		new int[16],
		new int[16]
		{
			0, 0, 1, 0, 0, 0, 0, 0, 1, 0,
			1, 0, 0, 1, 0, 0
		},
		new int[16]
		{
			5, 6, 7, 8, 9, 10, 0, 1, 2, 3,
			4, 11, 12, 13, 14, 15
		},
		new int[16],
		new int[16]
		{
			0, 1, 0, 0, 0, 1, 0, 0, 0, 0,
			0, 0, 0, 1, 0, 0
		},
		new int[16]
		{
			11, 12, 13, 14, 15, 0, 1, 2, 3, 4,
			5, 6, 7, 8, 9, 10
		},
		new int[16],
		new int[16]
		{
			0, 1, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 1, 0, 0
		},
		new int[16]
		{
			0, 1, 2, 3, 4, 5, 6, 7, 8, 9,
			10, 11, 12, 13, 14, 15
		},
		new int[16],
		new int[16]
		{
			1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
			1, 1, 1, 1, 1, 1
		},
		new int[16]
		{
			5, 6, 7, 8, 9, 10, 0, 1, 2, 3,
			4, 11, 12, 13, 14, 15
		},
		new int[16],
		new int[16]
		{
			1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
			1, 1, 1, 1, 1, 1
		},
		new int[16]
		{
			11, 12, 13, 14, 15, 0, 1, 2, 3, 4,
			5, 6, 7, 8, 9, 10
		},
		new int[16],
		new int[16]
		{
			1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
			1, 1, 1, 1, 1, 1
		}
	};

	private const int FINGER_MES_FRAME = 4;

	private static readonly int[] finger_mes_pos0 = new int[6] { 7, 69, 132, 195, 258, 321 };

	private static readonly int[] finger_mes_pos1 = new int[4] { 20, 116, 212, 308 };

	[SerializeField]
public float cursor_appear_time_;

	[Header("Select")]
	[SerializeField]
public GameObject select_root_;

	private Vector2[] select_finger_offset_ = new Vector2[3]
	{
		new Vector2(270f, -30f),
		new Vector2(270f, -30f),
		new Vector2(100f, -100f)
	};

	private Vector2[] select_finger_scale_ = new Vector2[3]
	{
		new Vector2(5.75f, 5.75f),
		new Vector2(5.75f, 5.75f),
		new Vector2(6.75f, 6.75f)
	};

	[Header("Main")]
	[SerializeField]
public GameObject main_root_;

	[SerializeField]
public SpriteRenderer background_;

	[SerializeField]
public MeshRenderer render_image_;

	[SerializeField]
public MeshRenderer temporary_image_;

	[SerializeField]
public GameObject dicovery_cursor_root_;

	[SerializeField]
public SpriteRenderer[] discovery_cursor_ = new SpriteRenderer[4];

	[Header("CompMain")]
	[SerializeField]
public GameObject comp_main_root_;

	[SerializeField]
public MeshRenderer comp_left_image_;

	[SerializeField]
public MeshRenderer comp_right_image_;

	[SerializeField]
public Vector2 comp_mesh_size_;

	[SerializeField]
public Vector2 discover_offset_;

	[SerializeField]
public Vector2 discover_scale_;

	[SerializeField]
public SpriteRenderer bar_image_;

	[SerializeField]
public SpriteRenderer info_frame_image_;

	[SerializeField]
public Text[] info_texts_;

	[SerializeField]
public SpriteRenderer left_arrow_image_;

	[SerializeField]
public SpriteRenderer right_arrow_image_;

	[SerializeField]
public SpriteRenderer[] comp_select_frame_images_;

	[SerializeField]
public SpriteRenderer[] comp_select_icon_images_;

	[SerializeField]
public Transform comp_sprite_root_;

	[SerializeField]
public InputTouch touch_right_arrow_;

	[SerializeField]
public InputTouch touch_left_arrow_;

	[SerializeField]
public InputTouch touch_infoframe_;

	[SerializeField]
public List<InputTouch> touch_icon_list_ = new List<InputTouch>();

	[Header("Debug")]
	[SerializeField]
public bool debug_show_area_;

	private readonly Vector2 SCREEN_SIZE = new Vector2(1920f, 1080f);

	private int game_id_;

	private byte req_;

	private bool is_running_;

	private int finger_index_;

	private int score_;

	private int comp_cursor_;

	private bool comp_hit_;

	private Sprite[] powder_sprites_;

	private int mask_width_;

	private int mask_height_;

	private byte[] mask_data_;

	private RenderTexture render_texture_;

	private Material render_material_;

	private Mesh render_image_mesh_;

	private Texture2D temporary_texture_;

	private Mesh temporary_image_mesh_;

	private Mesh comp_left_image_mesh_;

	private Mesh comp_right_image_mesh_;

	private Sprite[] comp_select_icon_sprites_;

	private Sprite marker_sprite_;

	private Sprite scan_line_sprite_;

	private SpriteRenderer[][] marker_images_;

	private SpriteRenderer[] scan_line_images_;

	private Sprite[] comp_result_message_sprites_;

	private SpriteRenderer[] comp_result_message_images_;

	private Sprite comp_match_background_sprite_;

	private Sprite comp_no_match_background_sprite_;

	private SpriteRenderer comp_result_background_;

	private const int profile_name_fontsize_jpn_ = 36;

	private const int profile_name_fontsize_usa_ = 32;

	private const int profile_name_fontsize_korea_ = 24;

	private const float profile_name_position_y_jpn_ = 40f;

	private const float profile_name_position_y_usa_ = 35f;

	private const float comp_result_background_position_y_jpn_ = -290f;

	private const float comp_result_background_position_y_usa_ = -350f;

	public static FingerMiniGame instance { get; private set; }

	public bool is_running
	{
		get
		{
			return is_running_;
		}
	}

	public int game_id
	{
		get
		{
			return game_id_;
		}
	}

	public bool debug_show_area
	{
		get
		{
			return debug_show_area_;
		}
		set
		{
			debug_show_area_ = value;
		}
	}

	private int profile_name_fontsize_
	{
		get
        {
            string lang = Language.langFallback[GSStatic.global_work_.language].ToUpper();
            switch (lang)
            {
			case "JAPAN":
				return 36;
			case "USA":
				return 32;
			case "KOREA":
				return 24;
			default:
				return 32;
			}
		}
	}

	private float profile_name_position_y_
	{
		get
        {
            string lang = Language.langFallback[GSStatic.global_work_.language].ToUpper();
            switch (lang)
            {
			case "JAPAN":
				return 40f;
			case "USA":
				return 35f;
			default:
				return 35f;
			}
		}
	}

	private float comp_result_background_position_y_
	{
		get
        {
            string lang = Language.langFallback[GSStatic.global_work_.language].ToUpper();
            switch (lang)
            {
			case "JAPAN":
				return -290f;
			case "USA":
				return -350f;
			default:
				return -350f;
			}
		}
	}

	private void Awake()
	{
		instance = this;
	}

	private void TouchActive()
	{
		touch_right_arrow_.ActiveCollider();
		touch_left_arrow_.ActiveCollider();
		touch_infoframe_.ActiveCollider();
		foreach (InputTouch item in touch_icon_list_)
		{
			item.ActiveCollider();
		}
	}

	public void Init(ushort id)
	{
		game_id_ = id;
		if (game_id_ == 2)
		{
			advCtrl.instance.sub_window_.tantei_tukituke_ = 0;
		}
		touch_right_arrow_.ActiveCollider();
		touch_right_arrow_.touch_key_type = KeyType.Right;
		touch_left_arrow_.ActiveCollider();
		touch_left_arrow_.touch_key_type = KeyType.Left;
		touch_infoframe_.ActiveCollider();
		touch_infoframe_.touch_key_type = KeyType.X;
		MiniGameCursor.instance.ActiveCursorTouch();
		MiniGameCursor.instance.SetCursorTouchAction(delegate
		{
			MiniGameCursor.instance.cursor_key_type = KeyType.A;
		});
		int num = 3;
		for (int i = -num; i < touch_icon_list_.Count - 2; i++)
		{
			if (i == 0)
			{
				num = 2;
				continue;
			}
			InputTouch inputTouch = touch_icon_list_[i + num];
			inputTouch.argument_parameter = i;
			inputTouch.touch_event = delegate(TouchParameter p)
			{
				comp_cursor_ += (int)p.argument_parameter;
				if (comp_cursor_ < 0)
				{
					comp_cursor_ = human_idx_tbl.Length + comp_cursor_;
				}
				if (comp_cursor_ >= human_idx_tbl.Length)
				{
					comp_cursor_ -= human_idx_tbl.Length;
				}
				soundCtrl.instance.PlaySE(42);
				UpdateCompCursor();
			};
		}
		coroutineCtrl.instance.Play(ProcCoroutine());
	}

	public void SetReq(byte req)
	{
		req_ = req;
	}

	private IEnumerator ProcCoroutine()
	{
		SubWindow sub_window = advCtrl.instance.sub_window_;
		sub_window.GetCurrentRoutine().r.Set(0, 0, 0, 0);
		sub_window.req_ = SubWindow.Req.NONE;
		sub_window.busy_ = 0u;
		is_running_ = true;
		yield return null;
		if (game_id_ == 0)
		{
			yield return coroutineCtrl.instance.Play(TutorialCoroutine(0));
		}
		while (true)
		{
			yield return coroutineCtrl.instance.Play(SelectCoroutine());
			if (finger_index_ < 0)
			{
				break;
			}
			yield return coroutineCtrl.instance.Play(MainCoroutine());
			if (finger_index_ >= 0)
			{
				yield return coroutineCtrl.instance.Play(CompMainCoroutine());
				if (finger_index_ >= 0)
				{
					break;
				}
			}
		}
		advCtrl.instance.message_system_.AddScript(GSStatic.global_work_.scenario);
		if (game_id_ == 0)
		{
			bgCtrl.instance.SetSprite(117);
			GSDemo.CheckBGChange(117u, 0u);
			bgCtrl.instance.bg_pos_x = 1920f;
		}
		else if (game_id_ == 1)
		{
			bgCtrl.instance.SetSprite(117);
			GSDemo.CheckBGChange(117u, 0u);
		}
		else if (game_id_ == 2)
		{
			bgCtrl.instance.SetSprite(116);
			GSDemo.CheckBGChange(116u, 0u);
			bgCtrl.instance.bg_pos_x = 1920f;
		}
		if (game_id_ == 2)
		{
			MiniGameCursor.instance.cursor_exception_limit = new Vector2(1500f, 900f);
			if (finger_index_ < 0)
			{
				soundCtrl.instance.PlayBGM(383, 60);
				AnimationSystem.Instance.PlayCharacter((int)GSStatic.global_work_.title, 16404, 140, 140);
				GSStatic.tantei_work_.person_flag = 1;
			}
		}
		GSStatic.global_work_.Mess_move_flag = 0;
		if (finger_index_ >= 0)
		{
			uint num = uint.MaxValue;
			switch (game_id_)
			{
			case 0:
				num = scenario.SC4_64710;
				break;
			case 1:
				num = scenario.SC4_64760;
				break;
			case 2:
				num = scenario.SC4_67790;
				break;
			}
			if (num != uint.MaxValue)
			{
				MessageSystem.Mess_window_set(5u);
				GSStatic.global_work_.Mess_move_flag = 1;
				advCtrl.instance.message_system_.SetMessage(num);
			}
		}
		if (game_id_ == 1)
		{
			GSStatic.global_work_.status_flag &= 4294967279u;
		}
		advCtrl.instance.sub_window_.stack_ = 0;
		advCtrl.instance.sub_window_.GetCurrentRoutine().r.Set(5, 0, 0, 0);
		GSStatic.global_work_.r.Set(5, 1, 0, 0);
		is_running_ = false;
		float guide_limit_widht = keyGuideCtrl.instance.GetGuideWidth();
		MiniGameCursor.instance.cursor_exception_limit = new Vector2((float)systemCtrl.instance.ScreenWidth - guide_limit_widht, 900f);
	}

	private IEnumerator SelectCoroutine()
	{
		bgCtrl.instance.SetSprite((int)finger_info[game_id_].bg_no);
		AssetBundleCtrl asset_bundle_ctrl = AssetBundleCtrl.instance;
		AssetBundle asset_bundle = asset_bundle_ctrl.load("/GS1/minigame/", "s2d008");
		Sprite[] icon_sprites2 = asset_bundle.LoadAssetWithSubAssets<Sprite>("s2d008");
		MiniGameCursor cursor = MiniGameCursor.instance;
		cursor.icon_offset = Vector3.zero;
		cursor.icon_sprite = icon_sprites2[0];
		cursor.icon_visible = true;
		Vector2 cursor_area_size = cursor.cursor_area_size;
		cursor.cursor_position = new Vector3(0f, 0f, 0f);
		float guide_limit_widht = keyGuideCtrl.instance.GetGuideWidth();
		MiniGameCursor.instance.cursor_exception_limit = new Vector2((float)systemCtrl.instance.ScreenWidth - guide_limit_widht, 900f);
		yield return null;
		Vector3 src_position = cursor.cursor_position;
		Vector3 dest_position = new Vector3(cursor_area_size.x * 0.5f, cursor_area_size.y * 0.5f, 0f);
		for (float time = 0f; time < cursor_appear_time_; time += Time.deltaTime)
		{
			cursor.cursor_position = Vector3.Lerp(src_position, dest_position, time / cursor_appear_time_);
			yield return null;
		}
		cursor.cursor_position = dest_position;
		advCtrl.instance.sub_window_.busy_ = 0u;
		advCtrl.instance.sub_window_.req_ = SubWindow.Req.NONE;
		FINGER_TBL[] finger_tbl = finger_info[game_id_].ptbl;
		GSPoint4[] points2 = finger_tbl.Select(delegate(FINGER_TBL x)
		{
			INSPECT_DATA finger_inspect = x.finger_inspect;
			return new GSPoint4(finger_inspect.x0, finger_inspect.y0, finger_inspect.x1, finger_inspect.y1, finger_inspect.x2, finger_inspect.y2, finger_inspect.x3, finger_inspect.y3);
		}).ToArray();
		points2 = MiniGameGSPoint4Hit.ConvertPoint(points2, select_finger_offset_[game_id_], select_finger_scale_[game_id_]).ToArray();
		DebugMiniGameGSPoint4Hit debug_hit = new DebugMiniGameGSPoint4Hit();
		debug_hit.SetParent(base.transform);
		coroutineCtrl.instance.Play(keyGuideCtrl.instance.open(keyGuideBase.Type.FINGER_SELECT));
		messageBoardCtrl.instance.InActiveNormalMessageNextTouch();
		Vector3 last_position = Vector3.zero;
		int select = -1;
		padCtrl pad = padCtrl.instance;
		bool loop = true;
		while (loop)
		{
			cursor.Process();
			debug_hit.DebugShowArea(debug_show_area_, GetCursorRect(), points2);
			if (last_position != cursor.cursor_position)
			{
				last_position = cursor.cursor_position;
				select = -1;
				int num = ((game_id_ != 0) ? (-1) : (GSFlag.Check(0u, scenario.SCE4_FLAG_ST_G_FINGER_MES2) ? 1 : 0));
				for (int i = 0; i < finger_tbl.Length; i++)
				{
					if ((num < 0 || num == finger_tbl[i].hit) && GSUtility.ObjHitCheck2(GetCursorRect(), points2[i]))
					{
						select = i;
						break;
					}
				}
			}
			cursor.icon_sprite = icon_sprites2[(select >= 0) ? 1 : 0];
			if (padCtrl.instance.GetKeyDown(KeyType.B) && game_id_ != 0)
			{
				loop = false;
				finger_index_ = -1;
			}
			if (select >= 0 && padCtrl.instance.GetKeyDown(KeyType.A))
			{
				loop = false;
				finger_index_ = select;
			}
			yield return null;
		}
		debug_hit.DebugShowArea(false, GetCursorRect(), points2);
		coroutineCtrl.instance.Play(keyGuideCtrl.instance.open(keyGuideBase.Type.NO_GUIDE));
		cursor.icon_visible = false;
		cursor.icon_sprite = null;
		icon_sprites2 = null;
	}

	private IEnumerator MainCoroutine()
	{
		AssetBundleCtrl asset_bundle_ctrl = AssetBundleCtrl.instance;
		AssetBundle assetBundle = asset_bundle_ctrl.load("/GS1/minigame/", "frame05");
		Sprite sprite = null;
		if (assetBundle != null)
		{
			sprite = assetBundle.LoadAsset<Sprite>("frame05");
		}
		background_.sprite = sprite;
		AssetBundle assetBundle2 = asset_bundle_ctrl.load("/GS1/minigame/", "s2d052");
		Sprite[] icon_sprites2 = assetBundle2.LoadAssetWithSubAssets<Sprite>("s2d052");
		LoadMainCoroutineData();
		main_root_.SetActive(true);
		if (game_id_ == 0 && !GSFlag.Check(0u, scenario.SCE4_FLAG_ST_G_FINGER_MES1))
		{
			GSFlag.Set(0u, scenario.SCE4_FLAG_ST_G_FINGER_MES1, 1u);
			yield return coroutineCtrl.instance.Play(TutorialCoroutine(1));
		}
		padCtrl pad = padCtrl.instance;
		MiniGameCursor cursor = MiniGameCursor.instance;
		cursor.cursor_position = cursor.cursor_area_size * 0.5f;
		cursor.icon_offset = new Vector3(48f, -48f, 0f);
		cursor.icon_sprite = icon_sprites2[0];
		cursor.icon_visible = true;
		coroutineCtrl.instance.Play(keyGuideCtrl.instance.open(keyGuideBase.Type.FINGER_MAIN));
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
		messageBoardCtrl.instance.InActiveNormalMessageNextTouch();
		bool loop = true;
		while (loop)
		{
			cursor.Process();
			bool mouse_down = padCtrl.instance.InputGetMouseButtonDown(0);
			if (pad.GetKeyDown(KeyType.A) || mouse_down)
			{
				if (mouse_down)
				{
					if (MiniGameCursor.instance.IsTouchSafeArea())
					{
						Vector3 cursor_position = cursor.cursor_position;
						DropPowder((int)cursor_position.x, (int)cursor_position.y);
					}
				}
				else
				{
					Vector3 cursor_position2 = cursor.cursor_position;
					DropPowder((int)cursor_position2.x, (int)cursor_position2.y);
				}
			}
			else if (pad.GetKeyDown(KeyType.B))
			{
				loop = false;
				finger_index_ = -1;
			}
			else if (pad.GetKeyDown(KeyType.X))
			{
				yield return coroutineCtrl.instance.Play(Blow(false));
				if (CheckClear())
				{
					loop = false;
				}
			}
			yield return null;
		}
		coroutineCtrl.instance.Play(keyGuideCtrl.instance.open(keyGuideBase.Type.NO_GUIDE));
		cursor.icon_visible = false;
		cursor.icon_sprite = null;
		if (finger_index_ >= 0)
		{
			FINGER_TBL[] finger_tbl = finger_info[game_id_].ptbl;
			if (game_id_ == 0)
			{
				if (finger_tbl[finger_index_].hit == 1)
				{
					yield return coroutineCtrl.instance.Play(TutorialCoroutine(6));
				}
				else
				{
					yield return coroutineCtrl.instance.Play(TutorialCoroutine(2));
					finger_index_ = -1;
					GSFlag.Set(0u, scenario.SCE4_FLAG_ST_G_FINGER_MES2, 1u);
				}
			}
			else
			{
				soundCtrl.instance.PlaySE(17);
				yield return coroutineCtrl.instance.Play(DiscoveryCursorCoroutine());
				if (finger_tbl[finger_index_].hit != 1)
				{
					yield return coroutineCtrl.instance.Play(TutorialCoroutine(9));
					finger_index_ = -1;
				}
			}
			if (finger_index_ >= 0)
			{
				yield return coroutineCtrl.instance.Play(MoveFingerPrintCoroutine());
			}
		}
		else if (game_id_ == 0)
		{
			yield return coroutineCtrl.instance.Play(TutorialCoroutine(3));
		}
		else
		{
			yield return coroutineCtrl.instance.Play(TutorialCoroutine(8));
		}
		yield return null;
		main_root_.SetActive(false);
		UnloadMainCoroutineData();
		icon_sprites2 = null;
	}

	private IEnumerator DiscoveryCursorCoroutine()
	{
		FINGER_TBL[] finger_tbl = finger_info[game_id_].ptbl;
		FINGER_TBL finger = finger_tbl[finger_index_];
		int[][] cursor_position = new int[2][]
		{
			new int[4],
			null
		};
		cursor_position[0][0] = 0;
		cursor_position[0][1] = 16;
		cursor_position[0][2] = 256;
		cursor_position[0][3] = 176;
		cursor_position[1] = new int[4];
		cursor_position[1][0] = finger.discover_rect.x;
		cursor_position[1][1] = finger.discover_rect.y;
		cursor_position[1][2] = finger.discover_rect.x + finger.discover_rect.w;
		cursor_position[1][3] = finger.discover_rect.y + finger.discover_rect.h;
		SetDiscoveryCursorPosition(cursor_position[0]);
		dicovery_cursor_root_.SetActive(true);
		yield return null;
		while (cursor_position[0][0] != cursor_position[1][0] || cursor_position[0][1] != cursor_position[1][1] || cursor_position[0][2] != cursor_position[1][2] || cursor_position[0][3] != cursor_position[1][3])
		{
			for (int j = 0; j < 4; j++)
			{
				int distance = cursor_position[1][j] - cursor_position[0][j];
				int move = distance / 4;
				if (move != 0)
				{
					cursor_position[0][j] += move;
				}
				else
				{
					cursor_position[0][j] = cursor_position[1][j];
				}
			}
			SetDiscoveryCursorPosition(cursor_position[0]);
			yield return null;
		}
		for (int i = 80; i >= 0; i--)
		{
			dicovery_cursor_root_.SetActive(i % 16 <= 3);
			yield return null;
		}
		dicovery_cursor_root_.SetActive(false);
	}

	private void SetDiscoveryCursorPosition(int[] position)
	{
		float x = discover_offset_.x + (float)position[0] * discover_scale_.x;
		float y = 0f - (discover_offset_.y + (float)position[1] * discover_scale_.y);
		float x2 = discover_offset_.x + (float)position[2] * discover_scale_.x;
		float y2 = 0f - (discover_offset_.y + (float)position[3] * discover_scale_.y);
		discovery_cursor_[0].transform.localPosition = new Vector3(x, y);
		discovery_cursor_[1].transform.localPosition = new Vector3(x2, y);
		discovery_cursor_[2].transform.localPosition = new Vector3(x, y2);
		discovery_cursor_[3].transform.localPosition = new Vector3(x2, y2);
	}

	private IEnumerator MoveFingerPrintCoroutine()
	{
		temporary_image_.material.mainTexture = render_texture_;
		temporary_image_.material.color = Color.white;
		temporary_image_.enabled = true;
		render_image_.material.color = Color.gray;
		FINGER_TBL[] finger_tbl = finger_info[game_id_].ptbl;
		FINGER_TBL finger = finger_tbl[finger_index_];
		GSRect discover_rect = finger.discover_rect;
		Rect clip_rect = new Rect(discover_offset_.x + (float)discover_rect.x * discover_scale_.x, discover_offset_.y + (float)discover_rect.y * discover_scale_.y, (float)discover_rect.w * discover_scale_.x, (float)discover_rect.h * discover_scale_.y);
		Rect uv_rect = new Rect(clip_rect.x / (float)render_texture_.width, 1f - (clip_rect.y + clip_rect.height) / (float)render_texture_.height, clip_rect.width / (float)render_texture_.width, clip_rect.height / (float)render_texture_.height);
		temporary_image_mesh_.uv = new Vector2[4]
		{
			new Vector2(uv_rect.x, uv_rect.y),
			new Vector2(uv_rect.x + uv_rect.width, uv_rect.y),
			new Vector2(uv_rect.x, uv_rect.y + uv_rect.height),
			new Vector2(uv_rect.x + uv_rect.width, uv_rect.y + uv_rect.height)
		};
		Vector2 sCREEN_SIZE = SCREEN_SIZE;
		float x = (0f - sCREEN_SIZE.x) * 0.5f + clip_rect.x;
		Vector2 sCREEN_SIZE2 = SCREEN_SIZE;
		float num = (0f - sCREEN_SIZE2.y) * 0.5f;
		Vector2 sCREEN_SIZE3 = SCREEN_SIZE;
		float y = num + (sCREEN_SIZE3.y - (clip_rect.y + clip_rect.height));
		Vector2 sCREEN_SIZE4 = SCREEN_SIZE;
		float z = (0f - sCREEN_SIZE4.x) * 0.5f + clip_rect.x + clip_rect.width;
		Vector2 sCREEN_SIZE5 = SCREEN_SIZE;
		float num2 = (0f - sCREEN_SIZE5.y) * 0.5f;
		Vector2 sCREEN_SIZE6 = SCREEN_SIZE;
		Vector4 src_vert = new Vector4(x, y, z, num2 + (sCREEN_SIZE6.y - clip_rect.y));
		Vector3 dest_position = comp_left_image_.transform.localPosition;
		dest_position.y = 0f - dest_position.y;
		Vector2 dest_half_size = comp_mesh_size_ * 0.5f;
		Vector2 sCREEN_SIZE7 = SCREEN_SIZE;
		float x2 = (0f - sCREEN_SIZE7.x) * 0.5f + dest_position.x - dest_half_size.x;
		Vector2 sCREEN_SIZE8 = SCREEN_SIZE;
		float num3 = (0f - sCREEN_SIZE8.y) * 0.5f;
		Vector2 sCREEN_SIZE9 = SCREEN_SIZE;
		float y2 = num3 + (sCREEN_SIZE9.y - (dest_position.y + dest_half_size.y));
		Vector2 sCREEN_SIZE10 = SCREEN_SIZE;
		float z2 = (0f - sCREEN_SIZE10.x) * 0.5f + dest_position.x + dest_half_size.x;
		Vector2 sCREEN_SIZE11 = SCREEN_SIZE;
		float num4 = (0f - sCREEN_SIZE11.y) * 0.5f;
		Vector2 sCREEN_SIZE12 = SCREEN_SIZE;
		Vector4 dest_vert = new Vector4(x2, y2, z2, num4 + (sCREEN_SIZE12.y - (dest_position.y - dest_half_size.y)));
		for (int i = 0; i < 16; i++)
		{
			float t = (float)i / 16f;
			ModifyQuadMeshPosition(temporary_image_mesh_, Vector4.Lerp(src_vert, dest_vert, t));
			yield return null;
		}
		ModifyQuadMeshPosition(temporary_image_mesh_, dest_vert);
		yield return null;
	}

	private void ModifyQuadMeshPosition(Mesh mesh, Vector4 position)
	{
		mesh.vertices = new Vector3[4]
		{
			new Vector3(position.x, position.y, 0f),
			new Vector3(position.z, position.y, 0f),
			new Vector3(position.x, position.w, 0f),
			new Vector3(position.z, position.w, 0f)
		};
	}

	private IEnumerator CompMainCoroutine()
	{
		LoadCompMainCoroutineData();
		comp_cursor_ = 0;
		UpdateCompCursor();
		comp_main_root_.SetActive(true);
		yield return null;
		coroutineCtrl.instance.Play(keyGuideCtrl.instance.open((game_id_ == 0) ? keyGuideBase.Type.FINGER_COMP_TUTORIAL : keyGuideBase.Type.FINGER_COMP));
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
		padCtrl pad = padCtrl.instance;
		bool loop = true;
		while (loop)
		{
			if (pad.GetKeyDown(KeyType.Left) || pad.GetKeyDown(KeyType.StickL_Left) || (pad.IsNextMove() && pad.GetWheelMoveUp()))
			{
				comp_cursor_--;
				if (comp_cursor_ < 0)
				{
					comp_cursor_ = human_idx_tbl.Length - 1;
				}
				soundCtrl.instance.PlaySE(42);
				UpdateCompCursor();
			}
			else if (pad.GetKeyDown(KeyType.Right) || pad.GetKeyDown(KeyType.StickL_Right) || (pad.IsNextMove() && pad.GetWheelMoveDown()))
			{
				comp_cursor_++;
				if (comp_cursor_ >= human_idx_tbl.Length)
				{
					comp_cursor_ = 0;
				}
				soundCtrl.instance.PlaySE(42);
				UpdateCompCursor();
			}
			else if (pad.GetKeyDown(KeyType.X))
			{
				TouchSystem.TouchInActive();
				yield return coroutineCtrl.instance.Play(ComparisonCoroutine());
				if (comp_hit_)
				{
					loop = false;
				}
				else
				{
					if (game_id_ == 0)
					{
						yield return coroutineCtrl.instance.Play(TutorialCoroutine(4));
					}
					else if (game_id_ == 2)
					{
						yield return coroutineCtrl.instance.Play(TutorialCoroutine(7));
					}
					TouchActive();
				}
			}
			padCtrl.instance.WheelMoveValUpdate();
			yield return null;
		}
		coroutineCtrl.instance.Play(keyGuideCtrl.instance.open(keyGuideBase.Type.NO_GUIDE));
		comp_main_root_.SetActive(false);
		UnloadCompMainCoroutineData();
		if (render_texture_ != null)
		{
			Object.Destroy(render_texture_);
			render_texture_ = null;
		}
	}

	private IEnumerator ComparisonCoroutine()
	{
		comp_hit_ = finger_info[game_id_].correct == human_idx_tbl[comp_cursor_];
		fadeCtrl.instance.play(fadeCtrl.Status.FADE_IN, 1u, 8u);
		AnimationSystem.Instance.PlayObject((int)GSStatic.global_work_.title, 0, 104);
		AnimationSystem.Instance.PlayObject((int)GSStatic.global_work_.title, 0, 87);
		for (int k = 0; k < 60; k++)
		{
			yield return null;
		}
		int order_offset = ((comp_hit_ ? 3 : 0) + Random.Range(0, 3)) * 3;
		GSPoint[] check_point = finger_check_point[game_id_];
		Vector2 marker_scale = new Vector2(comp_mesh_size_.x / 124f, comp_mesh_size_.y / 136f);
		int marker_sprite_width = (int)marker_sprite_.rect.width;
		int marker_sprite_height = (int)marker_sprite_.rect.height;
		check_point = check_point.Select((GSPoint p) => new GSPoint((ushort)((float)(int)p.x * marker_scale.x), (ushort)((float)(int)p.y * marker_scale.y))).ToArray();
		int marker_order_count = 0;
		int marker_hit_count = 0;
		int scan_line_timer = 0;
		int scan_line_y = 0;
		int scan_line_speed = 10;
		int scan_count = 0;
		int[] marker_timer = new int[16];
		for (int l = 0; l < marker_timer.Length; l++)
		{
			marker_timer[l] = -1;
		}
		while (marker_order_count < 16 || marker_timer[15] >= 0 || scan_line_timer == 0)
		{
			int line_sprite_height = (int)scan_line_sprite_.rect.height;
			if (scan_line_timer == 0)
			{
				Vector3 localPosition = comp_left_image_.transform.localPosition;
				scan_line_images_[0].transform.localPosition = new Vector3(localPosition.x, localPosition.y + (comp_mesh_size_.y - (float)line_sprite_height) * 0.5f - (float)scan_line_y, -0.2f);
				Vector3 localPosition2 = comp_right_image_.transform.localPosition;
				scan_line_images_[1].transform.localPosition = new Vector3(localPosition2.x, localPosition2.y + (comp_mesh_size_.y - (float)line_sprite_height) * 0.5f - (float)scan_line_y, -0.2f);
				if ((float)(scan_line_y + scan_line_speed) < comp_mesh_size_.y - (float)line_sprite_height)
				{
					scan_line_y += scan_line_speed;
				}
				else
				{
					scan_line_y = (int)(comp_mesh_size_.y - (float)line_sprite_height);
					scan_line_timer = 30;
				}
				bool flag = scan_line_timer == 0;
				scan_line_images_[0].enabled = flag;
				scan_line_images_[1].enabled = flag;
			}
			else
			{
				scan_line_timer--;
				if (scan_line_timer <= 0)
				{
					scan_count++;
					scan_line_y = 0;
					soundCtrl.instance.PlaySE(422);
				}
			}
			if (marker_order_count < 16)
			{
				int num = finger_scan_order[order_offset + 1][marker_order_count];
				if (num <= scan_count)
				{
					int num2 = finger_scan_order[order_offset][marker_order_count];
					GSPoint gSPoint = check_point[num2];
					if (gSPoint.y >= scan_line_y - scan_line_speed && gSPoint.y <= scan_line_y)
					{
						marker_timer[marker_order_count] = 0;
						Vector2 vector = comp_left_image_.transform.localPosition;
						Vector2 vector2 = comp_right_image_.transform.localPosition;
						marker_images_[0][marker_order_count].transform.localPosition = new Vector3(vector.x - comp_mesh_size_.x * 0.5f + (float)(int)gSPoint.x + (float)marker_sprite_width * 0.5f, vector.y + comp_mesh_size_.y * 0.5f - (float)(int)gSPoint.y - (float)marker_sprite_height * 0.5f, -0.1f);
						marker_images_[1][marker_order_count].transform.localPosition = new Vector3(vector2.x - comp_mesh_size_.x * 0.5f + (float)(int)gSPoint.x + (float)marker_sprite_width * 0.5f, vector2.y + comp_mesh_size_.y * 0.5f - (float)(int)gSPoint.y - (float)marker_sprite_height * 0.5f, -0.1f);
						marker_order_count++;
					}
				}
			}
			for (int m = 0; m < 16; m++)
			{
				if (marker_timer[m] == -1)
				{
					continue;
				}
				marker_timer[m]++;
				if (marker_timer[m] < 40)
				{
					if (marker_timer[m] / 4 % 2 == 0)
					{
						marker_images_[0][m].enabled = false;
						marker_images_[1][m].enabled = false;
					}
					else
					{
						marker_images_[0][m].enabled = true;
						marker_images_[1][m].enabled = true;
					}
					continue;
				}
				if (finger_scan_order[order_offset + 2][m] == 1)
				{
					marker_images_[0][m].enabled = true;
					marker_images_[1][m].enabled = true;
					marker_hit_count++;
					soundCtrl.instance.PlaySE(423);
					AnimationSystem.Instance.StopObject((int)GSStatic.global_work_.title, 0, 87 + (marker_hit_count - 1));
					AnimationSystem.Instance.PlayObject((int)GSStatic.global_work_.title, 0, 87 + marker_hit_count);
				}
				else
				{
					marker_images_[0][m].enabled = false;
					marker_images_[1][m].enabled = false;
				}
				marker_timer[m] = -1;
			}
			yield return null;
		}
		AnimationSystem.Instance.StopObject((int)GSStatic.global_work_.title, 0, 104);
		AnimationSystem.Instance.PlayObject((int)GSStatic.global_work_.title, 0, 105);
		for (int j = 0; j < 120; j++)
		{
			yield return null;
		}
		fadeCtrl.instance.play(fadeCtrl.Status.FADE_IN, 1u, 8u);
		comp_result_background_.sprite = ((!comp_hit_) ? comp_no_match_background_sprite_ : comp_match_background_sprite_);
		comp_result_background_.enabled = true;
		if (GSStatic.global_work_.language != "JAPAN")
		{
			yield return coroutineCtrl.instance.Play(CompResultMessage_U_Coroutine());
		}
		else
		{
			yield return coroutineCtrl.instance.Play(CompResultMessageCoroutine());
		}
		comp_result_background_.sprite = null;
		comp_result_background_.enabled = false;
		for (int i = 0; i < 60; i++)
		{
			yield return null;
		}
		for (int n = 0; n < marker_images_.Length; n++)
		{
			for (int num3 = 0; num3 < marker_images_[n].Length; num3++)
			{
				marker_images_[n][num3].enabled = false;
			}
		}
		AnimationSystem.Instance.StopObject((int)GSStatic.global_work_.title, 0, 87 + marker_hit_count);
		AnimationSystem.Instance.StopObject((int)GSStatic.global_work_.title, 0, 105);
		yield return null;
	}

	private IEnumerator TutorialCoroutine(int tutorial_no)
	{
		keyGuideBase.Type guid_type = keyGuideCtrl.instance.current_guide;
		coroutineCtrl.instance.Play(keyGuideCtrl.instance.open(keyGuideBase.Type.NO_GUIDE));
		uint message;
		switch (tutorial_no)
		{
		case 0:
			message = scenario.SC4_70300;
			guid_type = keyGuideBase.Type.NO_GUIDE;
			break;
		case 1:
			message = scenario.SC4_70310;
			break;
		case 2:
			message = scenario.SC4_70330;
			break;
		case 3:
			message = scenario.SC4_70320;
			break;
		case 4:
			message = scenario.SC4_64770;
			break;
		case 5:
			message = scenario.SC4_64770;
			break;
		case 6:
			message = scenario.SC4_70340;
			break;
		case 7:
			message = scenario.SC4_67780;
			break;
		case 8:
			message = scenario.EV0_70380;
			break;
		case 9:
			message = scenario.EV0_70390;
			break;
		default:
			message = scenario.EV0_70380;
			break;
		}
		if (tutorial_no == 8 || tutorial_no == 9)
		{
			advCtrl.instance.message_system_.AddScript(65535u);
		}
		else
		{
			advCtrl.instance.message_system_.AddScript(GSStatic.global_work_.scenario);
		}
		MessageSystem.GetActiveMessageWork().message_trans_flag = 1;
		GSStatic.global_work_.Mess_move_flag = 1;
		advCtrl.instance.message_system_.SetMessage(message);
		req_ = 0;
		while (req_ != 33)
		{
			yield return null;
			if (req_ == 55)
			{
				req_ = 0;
				yield return coroutineCtrl.instance.Play(FingerEff0Coroutine());
			}
			else if (req_ == 56)
			{
				req_ = 0;
				yield return coroutineCtrl.instance.Play(FingerEff1Coroutine());
			}
			else if (req_ == 57)
			{
				req_ = 0;
				yield return coroutineCtrl.instance.Play(FingerEff2Coroutine());
			}
			else if (req_ == 58)
			{
				req_ = 0;
			}
			else if (req_ == 59)
			{
				req_ = 0;
			}
			else if (req_ == 60)
			{
				req_ = 0;
				bgCtrl.instance.SetSprite(186);
				comp_main_root_.SetActive(false);
			}
			else if (req_ == 61)
			{
				req_ = 0;
				bgCtrl.instance.SetSprite(139);
				comp_main_root_.SetActive(true);
			}
		}
		messageBoardCtrl.instance.board(false, false);
		messageBoardCtrl.instance.InActiveNormalMessageNextTouch();
		MessageSystem.GetActiveMessageWork().message_trans_flag = 0;
		coroutineCtrl.instance.Play(keyGuideCtrl.instance.open(guid_type));
		yield return null;
	}

	private IEnumerator FingerEff0Coroutine()
	{
		DropPowder(960, 480);
		for (int i = 0; i < 30; i++)
		{
			yield return null;
		}
	}

	private IEnumerator FingerEff1Coroutine()
	{
		yield return coroutineCtrl.instance.Play(Blow(true));
	}

	private IEnumerator FingerEff2Coroutine()
	{
		fadeCtrl.instance.play(2u, 1u, 1u, 8u);
		while (!fadeCtrl.instance.is_end)
		{
			yield return null;
		}
		main_root_.SetActive(false);
		fadeCtrl.instance.play(1u, 1u, 1u, 8u);
		while (!fadeCtrl.instance.is_end)
		{
			yield return null;
		}
	}

	private void LoadMainCoroutineData()
	{
		AssetBundleCtrl assetBundleCtrl = AssetBundleCtrl.instance;
		string[] array = new string[4] { "eff021", "eff024", "eff025", "eff026" };
		powder_sprites_ = new Sprite[array.Length];
		for (int i = 0; i < powder_sprites_.Length; i++)
		{
			AssetBundle assetBundle = assetBundleCtrl.load("/GS1/3D/eff/", array[i]);
			if (assetBundle != null)
			{
				powder_sprites_[i] = assetBundle.LoadAsset<Sprite>(array[i]);
			}
		}
		string file_name = chk_inf_finger[finger_info[game_id_].ptbl[finger_index_].init_finger].ptbl[0].file_name;
		AssetBundle assetBundle2 = assetBundleCtrl.load("/GS1/minigame/", file_name);
		if (assetBundle2 != null)
		{
			Sprite sprite = assetBundle2.LoadAsset<Sprite>(file_name);
			if ((bool)sprite)
			{
				mask_width_ = sprite.texture.width;
				mask_height_ = sprite.texture.height;
				mask_data_ = (from x in sprite.texture.GetPixels32()
					select x.a).ToArray();
			}
		}
		render_material_ = new Material(Shader.Find("Sprites/Default"));
		render_texture_ = new RenderTexture(2048, 2048, 0);
		render_image_.material.mainTexture = render_texture_;
		render_image_.material.color = Color.white;
		GL.PushMatrix();
		Graphics.SetRenderTarget(render_texture_);
		GL.Begin(4);
		GL.Clear(false, true, Color.clear);
		GL.End();
		GL.PopMatrix();
		Vector2 sCREEN_SIZE = SCREEN_SIZE;
		float y = 1f - sCREEN_SIZE.y / (float)render_texture_.height;
		Vector2 sCREEN_SIZE2 = SCREEN_SIZE;
		float width = sCREEN_SIZE2.x / (float)render_texture_.width;
		Vector2 sCREEN_SIZE3 = SCREEN_SIZE;
		Rect uv_rect = new Rect(0f, y, width, sCREEN_SIZE3.y / (float)render_texture_.height);
		render_image_mesh_ = CreateQuadMesh(SCREEN_SIZE, uv_rect);
		render_image_.GetComponent<MeshFilter>().mesh = render_image_mesh_;
		temporary_texture_ = new Texture2D(2048, 2048);
		temporary_image_.material.mainTexture = temporary_texture_;
		temporary_image_.material.color = Color.white;
		Vector2 sCREEN_SIZE4 = SCREEN_SIZE;
		float y2 = 1f - sCREEN_SIZE4.y / (float)temporary_texture_.height;
		Vector2 sCREEN_SIZE5 = SCREEN_SIZE;
		float width2 = sCREEN_SIZE5.x / (float)temporary_texture_.width;
		Vector2 sCREEN_SIZE6 = SCREEN_SIZE;
		Rect uv_rect2 = new Rect(0f, y2, width2, sCREEN_SIZE6.y / (float)temporary_texture_.height);
		temporary_image_mesh_ = CreateQuadMesh(SCREEN_SIZE, uv_rect2);
		temporary_image_.GetComponent<MeshFilter>().mesh = temporary_image_mesh_;
		temporary_image_.enabled = false;
		AssetBundle assetBundle3 = assetBundleCtrl.load("/GS1/minigame/", "s2d00e");
		if (assetBundle3 != null)
		{
			Sprite sprite2 = assetBundle3.LoadAsset<Sprite>("s2d00e");
			for (int j = 0; j < discovery_cursor_.Length; j++)
			{
				discovery_cursor_[j].sprite = sprite2;
			}
		}
	}

	private Mesh CreateQuadMesh(Vector2 size, Rect uv_rect)
	{
		Mesh mesh = new Mesh();
		Vector2 vector = size * 0.5f;
		mesh.vertices = new Vector3[4]
		{
			new Vector3(0f - vector.x, 0f - vector.y, 0f),
			new Vector3(vector.x, 0f - vector.y, 0f),
			new Vector3(0f - vector.x, vector.y, 0f),
			new Vector3(vector.x, vector.y, 0f)
		};
		mesh.uv = new Vector2[4]
		{
			new Vector2(uv_rect.x, uv_rect.y),
			new Vector2(uv_rect.x + uv_rect.width, uv_rect.y),
			new Vector2(uv_rect.x, uv_rect.y + uv_rect.height),
			new Vector2(uv_rect.x + uv_rect.width, uv_rect.y + uv_rect.height)
		};
		mesh.triangles = new int[6] { 0, 1, 2, 1, 2, 3 };
		return mesh;
	}

	private void UnloadMainCoroutineData()
	{
		for (int i = 0; i < discovery_cursor_.Length; i++)
		{
			discovery_cursor_[i].sprite = null;
		}
		temporary_image_.GetComponent<MeshFilter>().mesh = null;
		temporary_image_mesh_ = null;
		Object.Destroy(temporary_texture_);
		temporary_texture_ = null;
		render_image_.GetComponent<MeshFilter>().mesh = null;
		render_image_mesh_ = null;
		render_image_.material.mainTexture = null;
		Object.Destroy(render_material_);
		render_material_ = null;
		if (finger_index_ < 0)
		{
			Object.Destroy(render_texture_);
			render_texture_ = null;
		}
		mask_data_ = null;
		powder_sprites_ = null;
	}

	private void DropPowder(int x, int y)
	{
		int num = Random.Range(0, 4);
		Sprite sprite = powder_sprites_[num];
		Rect sourceRect = new Rect(0f, 0f, 1f, 1f);
		Rect screenRect = new Rect((float)x + sprite.rect.width * -0.5f, (float)y + sprite.rect.height * -0.5f, sprite.rect.width, sprite.rect.height);
		GL.PushMatrix();
		GL.LoadPixelMatrix(0f, render_texture_.width, render_texture_.height, 0f);
		Graphics.SetRenderTarget(render_texture_);
		Graphics.DrawTexture(screenRect, sprite.texture, sourceRect, 0, 0, 0, 0, Color.white, render_material_);
		Graphics.SetRenderTarget(null);
		GL.PopMatrix();
	}

	private IEnumerator Blow(bool igunore_mask = false)
	{
		Rect screen_rect = new Rect(0f, 0f, systemCtrl.instance.ScreenWidth, systemCtrl.instance.ScreenHeight);
		if (mask_data_ != null && !igunore_mask)
		{
			Graphics.SetRenderTarget(render_texture_);
			temporary_texture_.ReadPixels(screen_rect, 0, (int)((float)temporary_texture_.height - screen_rect.height));
			Graphics.SetRenderTarget(null);
			int score = 0;
			Color32[] pixels = temporary_texture_.GetPixels32();
			Color32[] pixels2 = new Color32[pixels.Length];
			int end_x = Mathf.Min((int)screen_rect.width, mask_width_);
			int end_y = Mathf.Min((int)screen_rect.height, mask_height_);
			int temporary_y_index = (int)((float)temporary_texture_.height - screen_rect.height) * temporary_texture_.width;
			int mask_y_index = 0;
			Color32 clear_color = new Color32(0, 0, 0, 0);
			for (int k = 0; k < end_y; k++)
			{
				for (int l = 0; l < end_x; l++)
				{
					int num = temporary_y_index + l;
					int num2 = mask_y_index + l;
					if (pixels[num].a <= 0)
					{
						continue;
					}
					if (mask_data_[num2] != 0 && pixels[num].a >= mask_data_[num2])
					{
						score++;
					}
					if (mask_data_[num2] <= 0)
					{
						pixels2[num] = pixels[num];
						pixels[num] = clear_color;
					}
					else if (mask_data_[num2] < byte.MaxValue)
					{
						if (pixels[num].a > mask_data_[num2])
						{
							pixels2[num] = pixels[num];
							pixels2[num].a = (byte)(pixels[num].a - mask_data_[num2]);
							pixels[num].a = mask_data_[num2];
						}
						else
						{
							pixels[num] = new Color32(190, 190, 190, pixels[num].a);
						}
					}
				}
				temporary_y_index += temporary_texture_.width;
				mask_y_index += mask_width_;
			}
			score_ = score;
			temporary_texture_.SetPixels32(pixels);
			temporary_texture_.Apply();
			Rect sprite_rect = new Rect(0f, 1f - screen_rect.height / (float)temporary_texture_.height, screen_rect.width / (float)temporary_texture_.width, screen_rect.height / (float)temporary_texture_.height);
			GL.PushMatrix();
			Graphics.SetRenderTarget(render_texture_);
			GL.Begin(4);
			GL.Clear(false, true, Color.clear);
			GL.End();
			GL.LoadPixelMatrix(0f, render_texture_.width, render_texture_.height, 0f);
			Graphics.DrawTexture(screen_rect, temporary_texture_, sprite_rect, 0, 0, 0, 0, Color.white, render_material_);
			Graphics.SetRenderTarget(null);
			GL.PopMatrix();
			temporary_texture_.SetPixels32(pixels2);
			temporary_texture_.Apply();
			temporary_image_.enabled = true;
			for (int i = 0; i < 30; i++)
			{
				float t = (float)i / 30f;
				temporary_image_.material.color = new Color(1f, 1f, 1f, 1f - t);
				yield return null;
			}
			temporary_image_.enabled = false;
		}
		else
		{
			for (int j = 0; j < 30; j++)
			{
				float t2 = (float)j / 30f;
				render_image_.material.color = new Color(1f, 1f, 1f, 1f - t2);
				yield return null;
			}
			render_image_.enabled = false;
			yield return null;
			Graphics.SetRenderTarget(render_texture_);
			GL.Begin(4);
			GL.Clear(false, true, Color.clear);
			GL.End();
			Graphics.SetRenderTarget(null);
			render_image_.material.color = Color.white;
			render_image_.enabled = true;
		}
		yield return null;
	}

	private bool CheckClear()
	{
		return score_ >= (int)((float)chk_inf_finger[finger_info[game_id_].ptbl[finger_index_].init_finger].ptbl[0].min_cnt * 15f);
	}

	private void LoadCompMainCoroutineData()
	{
		bgCtrl.instance.SetSprite(139);
		AssetBundleCtrl assetBundleCtrl = AssetBundleCtrl.instance;
		comp_left_image_.material.mainTexture = render_texture_;
		GSRect discover_rect = finger_info[game_id_].ptbl[finger_index_].discover_rect;
		Rect rect = new Rect(discover_offset_.x + (float)discover_rect.x * discover_scale_.x, discover_offset_.y + (float)discover_rect.y * discover_scale_.y, (float)discover_rect.w * discover_scale_.x, (float)discover_rect.h * discover_scale_.y);
		Rect uv_rect = new Rect(rect.x / (float)render_texture_.width, 1f - (rect.y + rect.height) / (float)render_texture_.height, rect.width / (float)render_texture_.width, rect.height / (float)render_texture_.height);
		comp_left_image_mesh_ = CreateQuadMesh(comp_mesh_size_, uv_rect);
		comp_left_image_.GetComponent<MeshFilter>().mesh = comp_left_image_mesh_;
		Rect uv_rect2 = new Rect(0f, 0f, 1f, 1f);
		comp_right_image_mesh_ = CreateQuadMesh(comp_mesh_size_, uv_rect2);
		comp_right_image_.GetComponent<MeshFilter>().mesh = comp_right_image_mesh_;
		comp_right_image_.enabled = false;
		bar_image_.sprite = LoadAsset<Sprite>("/GS1/minigame/", "record02");
		info_frame_image_.sprite = LoadAsset<Sprite>("/GS1/minigame/", "record_icon02");
		AssetBundle assetBundle = assetBundleCtrl.load("/menu/common/", "select_arrow");
		Sprite[] array = assetBundle.LoadAssetWithSubAssets<Sprite>("select_arrow");
		left_arrow_image_.sprite = array[1];
		right_arrow_image_.sprite = array[1];
		AssetBundle assetBundle2 = assetBundleCtrl.load("/menu/common/", "record_icon");
		Sprite[] array2 = assetBundle2.LoadAssetWithSubAssets<Sprite>("record_icon");
		for (int i = 0; i < comp_select_frame_images_.Length; i++)
		{
			comp_select_frame_images_[i].sprite = array2[1];
		}
		piceDataCtrl piceDataCtrl2 = piceDataCtrl.instance;
		comp_select_icon_sprites_ = new Sprite[8];
		for (int j = 0; j < comp_select_icon_sprites_.Length; j++)
		{
			piceData piceData2 = piceDataCtrl2.note_data[(int)scenario.NOTE_FINGER_DMY0 + j];
			piceData piceData3 = piceDataCtrl2.note_data[piceData2.obj_id];
			AssetBundle assetBundle3 = assetBundleCtrl.load(piceData3.path, piceDataCtrl2.GetIconItcFile(piceData3.no));
			if (assetBundle3 != null)
			{
				comp_select_icon_sprites_[j] = assetBundle3.LoadAsset<Sprite>(piceDataCtrl2.GetIconItcFile(piceData3.no));
			}
		}
		marker_sprite_ = LoadAsset<Sprite>("/GS1/minigame/", "s2d012");
		scan_line_sprite_ = LoadAsset<Sprite>("/GS1/minigame/", "etc404");
		if (GSStatic.global_work_.language == "JAPAN")
		{
			comp_result_message_sprites_ = new Sprite[finger_sprite_word_name.Length];
			for (int k = 0; k < comp_result_message_sprites_.Length; k++)
			{
				comp_result_message_sprites_[k] = LoadAsset<Sprite>("/GS1/minigame/", finger_sprite_word_name[k]);
			}
		}
		comp_match_background_sprite_ = LoadAsset<Sprite>("/GS1/minigame/", "same_bg");
		comp_no_match_background_sprite_ = LoadAsset<Sprite>("/GS1/minigame/", "miss_bg");
		marker_images_ = new SpriteRenderer[2][];
		for (int l = 0; l < marker_images_.Length; l++)
		{
			marker_images_[l] = new SpriteRenderer[16];
			for (int m = 0; m < marker_images_[l].Length; m++)
			{
				GameObject gameObject = new GameObject("Mark_" + l + "_" + m);
				gameObject.layer = comp_sprite_root_.gameObject.layer;
				gameObject.transform.SetParent(comp_sprite_root_, false);
				SpriteRenderer spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
				spriteRenderer.enabled = false;
				spriteRenderer.sprite = marker_sprite_;
				marker_images_[l][m] = spriteRenderer;
			}
		}
		scan_line_images_ = new SpriteRenderer[2];
		for (int n = 0; n < scan_line_images_.Length; n++)
		{
			GameObject gameObject2 = new GameObject("ScanLine_" + n);
			gameObject2.layer = comp_sprite_root_.gameObject.layer;
			gameObject2.transform.SetParent(comp_sprite_root_, false);
			SpriteRenderer spriteRenderer2 = gameObject2.AddComponent<SpriteRenderer>();
			spriteRenderer2.enabled = false;
			spriteRenderer2.sprite = scan_line_sprite_;
			gameObject2.transform.localScale = new Vector3(216f, 1f, 1f);
			scan_line_images_[n] = spriteRenderer2;
		}
		GameObject gameObject3 = new GameObject("ResultBackgrond");
		gameObject3.layer = comp_sprite_root_.gameObject.layer;
		gameObject3.transform.SetParent(comp_sprite_root_, false);
		SpriteRenderer spriteRenderer3 = gameObject3.AddComponent<SpriteRenderer>();
		spriteRenderer3.enabled = false;
		gameObject3.transform.localPosition = new Vector3(960f, comp_result_background_position_y_, -5f);
		comp_result_background_ = spriteRenderer3;
		if (GSStatic.global_work_.language == "JAPAN")
		{
			comp_result_message_images_ = new SpriteRenderer[6];
			for (int num = 0; num < comp_result_message_images_.Length; num++)
			{
				GameObject gameObject4 = new GameObject("ResultMessage_" + num);
				gameObject4.layer = comp_sprite_root_.gameObject.layer;
				gameObject4.transform.SetParent(comp_sprite_root_, false);
				SpriteRenderer spriteRenderer4 = gameObject4.AddComponent<SpriteRenderer>();
				spriteRenderer4.enabled = false;
				comp_result_message_images_[num] = spriteRenderer4;
			}
		}
		Vector3 localPosition = info_texts_[0].rectTransform.localPosition;
		localPosition.y = profile_name_position_y_;
		info_texts_[0].rectTransform.localPosition = localPosition;
		info_texts_[0].fontSize = profile_name_fontsize_;
		for (int num2 = 0; num2 < info_texts_.Length; num2++)
		{
			info_texts_[num2].verticalOverflow = VerticalWrapMode.Overflow;
		}
	}

	private void UnloadCompMainCoroutineData()
	{
		foreach (Transform item in comp_sprite_root_)
		{
			Object.Destroy(item.gameObject);
		}
		comp_result_message_sprites_ = null;
		scan_line_sprite_ = null;
		marker_sprite_ = null;
		for (int i = 0; i < comp_select_icon_images_.Length; i++)
		{
			comp_select_icon_images_[i].sprite = null;
		}
		comp_select_icon_sprites_ = null;
		comp_right_image_.GetComponent<MeshFilter>().mesh = null;
		comp_right_image_mesh_ = null;
		comp_right_image_.material.mainTexture = null;
		comp_left_image_.GetComponent<MeshFilter>().mesh = null;
		comp_left_image_mesh_ = null;
		comp_left_image_.material.mainTexture = null;
	}

	private T LoadAsset<T>(string path, string name) where T : Object
	{
		AssetBundle assetBundle = AssetBundleCtrl.instance.load(path, name);
		if (assetBundle != null)
		{
			return assetBundle.LoadAsset<T>(name);
		}
		return (T)null;
	}

	private void UpdateCompCursor()
	{
		AssetBundleCtrl assetBundleCtrl = AssetBundleCtrl.instance;
		int num = (comp_cursor_ - 3 + human_idx_tbl.Length) % human_idx_tbl.Length;
		for (int i = 0; i < comp_select_icon_images_.Length; i++)
		{
			comp_select_icon_images_[i].sprite = comp_select_icon_sprites_[human_idx_tbl[num]];
			num = (num + 1) % human_idx_tbl.Length;
		}
		int num2 = human_idx_tbl[comp_cursor_];
		piceData noteData = GetNoteData(num2);
		info_texts_[0].text = noteData.name;
		info_texts_[1].text = noteData.comment00;
		info_texts_[2].text = noteData.comment01;
		string in_name = finger_bg_name_tbl[num2];
		AssetBundle assetBundle = assetBundleCtrl.load("/GS1/etc/", in_name);
		Sprite sprite = null;
		if (assetBundle != null)
		{
			sprite = assetBundle.LoadAsset<Sprite>(in_name);
		}
		comp_right_image_.material.mainTexture = ((!(sprite != null)) ? null : sprite.texture);
		comp_right_image_.enabled = sprite != null;
	}

	private IEnumerator CompResultMessageCoroutine()
	{
		Vector2 offset = new Vector2(150f, -30f);
		Vector2 scale = new Vector2(5f, 5f);
		int startL = (int)(offset.x + -128f * scale.x);
		int startR = (int)(offset.x + 400f * scale.x);
		int finger_mes_speed = (int)(offset.x + 2f * scale.x);
		int[] finger_mes_pos = new int[6];
		int[] finger_mes_timer = new int[6];
		int finger_mes_pos_y = (int)(offset.y + 64f * scale.y);
		int num;
		int[] dest_finger_mes_pos2;
		if (comp_hit_)
		{
			num = 6;
			comp_result_message_images_[0].sprite = comp_result_message_sprites_[0];
			finger_mes_timer[0] = 12;
			finger_mes_pos[0] = startL;
			comp_result_message_images_[1].sprite = comp_result_message_sprites_[1];
			finger_mes_timer[1] = 8;
			finger_mes_pos[1] = startL;
			comp_result_message_images_[2].sprite = comp_result_message_sprites_[2];
			finger_mes_timer[2] = 4;
			finger_mes_pos[2] = startL;
			comp_result_message_images_[3].sprite = comp_result_message_sprites_[3];
			finger_mes_timer[3] = 4;
			finger_mes_pos[3] = startR;
			comp_result_message_images_[4].sprite = comp_result_message_sprites_[4];
			finger_mes_timer[4] = 8;
			finger_mes_pos[4] = startR;
			comp_result_message_images_[5].sprite = comp_result_message_sprites_[5];
			finger_mes_timer[5] = 12;
			finger_mes_pos[5] = startR;
			dest_finger_mes_pos2 = finger_mes_pos0;
			soundCtrl.instance.PlaySE(416);
		}
		else
		{
			num = 4;
			comp_result_message_images_[0].sprite = comp_result_message_sprites_[6];
			finger_mes_timer[0] = 8;
			finger_mes_pos[0] = startL;
			comp_result_message_images_[1].sprite = comp_result_message_sprites_[7];
			finger_mes_timer[1] = 4;
			finger_mes_pos[1] = startL;
			comp_result_message_images_[2].sprite = comp_result_message_sprites_[8];
			finger_mes_timer[2] = 4;
			finger_mes_pos[2] = startR;
			comp_result_message_images_[3].sprite = comp_result_message_sprites_[9];
			finger_mes_timer[3] = 8;
			finger_mes_pos[3] = startR;
			dest_finger_mes_pos2 = finger_mes_pos1;
			soundCtrl.instance.PlaySE(32);
		}
		dest_finger_mes_pos2 = dest_finger_mes_pos2.Select((int x) => (int)(offset.x + (float)x * scale.x)).ToArray();
		for (int i = 0; i < comp_result_message_images_.Length; i++)
		{
			comp_result_message_images_[i].enabled = comp_result_message_images_[i].sprite != null;
		}
		while (true)
		{
			bool result = true;
			for (int j = 0; j < num; j++)
			{
				if (finger_mes_timer[j] == 0)
				{
					if (Mathf.Abs(finger_mes_pos[j] - dest_finger_mes_pos2[j]) < 16)
					{
						if (finger_mes_pos[j] < dest_finger_mes_pos2[j])
						{
							if (finger_mes_pos[j] + finger_mes_speed < dest_finger_mes_pos2[j])
							{
								finger_mes_pos[j] += finger_mes_speed;
								result = false;
							}
							else
							{
								finger_mes_pos[j] = dest_finger_mes_pos2[j];
								finger_mes_timer[j] = -1;
							}
						}
						else if (finger_mes_pos[j] - finger_mes_speed > dest_finger_mes_pos2[j])
						{
							finger_mes_pos[j] -= finger_mes_speed;
							result = false;
						}
						else
						{
							finger_mes_pos[j] = dest_finger_mes_pos2[j];
							finger_mes_timer[j] = -1;
						}
					}
					else
					{
						finger_mes_pos[j] += (dest_finger_mes_pos2[j] - finger_mes_pos[j]) / 2;
						result = false;
					}
				}
				else if (finger_mes_timer[j] < 0)
				{
					finger_mes_timer[j]--;
				}
				else
				{
					finger_mes_timer[j]--;
					result = false;
				}
				comp_result_message_images_[j].transform.localPosition = new Vector3(finger_mes_pos[j], -finger_mes_pos_y, -5.1f);
			}
			if (result)
			{
				if (finger_mes_timer[0] < -180)
				{
					break;
				}
				finger_mes_timer[0]--;
			}
			yield return null;
		}
		for (int k = 0; k < comp_result_message_images_.Length; k++)
		{
			comp_result_message_images_[k].enabled = false;
			comp_result_message_images_[k].sprite = null;
		}
		yield return null;
	}

	private IEnumerator CompResultMessage_U_Coroutine()
	{
		if (comp_hit_)
		{
			AnimationSystem.Instance.PlayObject((int)GSStatic.global_work_.title, 0, 81);
			AnimationSystem.Instance.PlayObject((int)GSStatic.global_work_.title, 0, 82);
			soundCtrl.instance.PlaySE(416);
			for (int i = 0; i < 120; i++)
			{
				yield return null;
			}
			AnimationSystem.Instance.StopObject((int)GSStatic.global_work_.title, 0, 81);
			AnimationSystem.Instance.StopObject((int)GSStatic.global_work_.title, 0, 82);
		}
		else
		{
			AnimationSystem.Instance.PlayObject((int)GSStatic.global_work_.title, 0, 83);
			AnimationSystem.Instance.PlayObject((int)GSStatic.global_work_.title, 0, 84);
			AnimationSystem.Instance.PlayObject((int)GSStatic.global_work_.title, 0, 85);
			soundCtrl.instance.PlaySE(32);
			for (int j = 0; j < 120; j++)
			{
				yield return null;
			}
			AnimationSystem.Instance.StopObject((int)GSStatic.global_work_.title, 0, 83);
			AnimationSystem.Instance.StopObject((int)GSStatic.global_work_.title, 0, 84);
			AnimationSystem.Instance.StopObject((int)GSStatic.global_work_.title, 0, 85);
		}
		yield return null;
	}

	private piceData GetNoteData(int id)
	{
		return piceDataCtrl.instance.note_data[(int)scenario.NOTE_FINGER_DMY0 + id];
	}

	private GSRect GetCursorRect()
	{
		Vector3 cursor_position = MiniGameCursor.instance.cursor_position;
		return new GSRect((short)cursor_position.x, (short)cursor_position.y, 4, 4);
	}
}
