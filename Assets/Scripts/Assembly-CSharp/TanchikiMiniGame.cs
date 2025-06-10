using UnityEngine;

public class TanchikiMiniGame : MonoBehaviour
{
	[SerializeField]
public TanchikiRadioWave radio_wave_;

	private MiniGameCursor cursor_;

	private Sprite[] cursor_sprites_;

	private static readonly GSPoint4[] GS2_find_target = new GSPoint4[22]
	{
		new GSPoint4(835, 90, 875, 90, 875, 125, 835, 125),
		new GSPoint4(480, 130, 590, 130, 600, 340, 490, 340),
		new GSPoint4(395, 320, 485, 320, 485, 455, 395, 455),
		new GSPoint4(490, 345, 655, 345, 655, 635, 490, 535),
		new GSPoint4(900, 815, 1145, 680, 1110, 810, 1015, 850),
		new GSPoint4(1420, 710, 1485, 710, 1485, 790, 1420, 790),
		new GSPoint4(1445, 810, 1475, 835, 1375, 900, 1350, 880),
		new GSPoint4(1635, 340, 1775, 340, 1770, 590, 1650, 595),
		new GSPoint4(1785, 585, 1900, 585, 1900, 700, 1785, 700),
		new GSPoint4(1760, 850, 1780, 855, 1820, 910, 1740, 925),
		new GSPoint4(1850, 815, 1905, 830, 1860, 895, 1810, 880),
		new GSPoint4(1955, 850, 2120, 930, 2090, 1020, 1925, 945),
		new GSPoint4(2020, 150, 2205, 150, 2205, 225, 2020, 225),
		new GSPoint4(2020, 360, 2130, 360, 2130, 440, 2020, 440),
		new GSPoint4(2525, 355, 2575, 355, 2575, 435, 2525, 435),
		new GSPoint4(2440, 620, 2520, 600, 2585, 770, 2440, 800),
		new GSPoint4(2600, 610, 2655, 590, 2655, 625, 2605, 650),
		new GSPoint4(2645, 630, 2725, 630, 2725, 680, 2625, 680),
		new GSPoint4(2910, 130, 2970, 130, 2970, 240, 2910, 240),
		new GSPoint4(2780, 785, 2840, 760, 2845, 800, 2785, 825),
		new GSPoint4(3225, 240, 3290, 240, 3270, 545, 3240, 545),
		new GSPoint4(0, 0, 512, 0, 512, 256, 0, 256)
	};

	private static readonly GSPoint4[] GS3_find_target = new GSPoint4[20]
	{
		new GSPoint4(325, 740, 415, 760, 365, 790, 325, 770),
		new GSPoint4(420, 955, 510, 955, 510, 1005, 420, 1005),
		new GSPoint4(630, 870, 665, 845, 735, 895, 710, 920),
		new GSPoint4(890, 850, 945, 850, 945, 880, 890, 880),
		new GSPoint4(1190, 750, 1260, 750, 1275, 790, 1205, 790),
		new GSPoint4(1365, 780, 1420, 790, 1375, 905, 1310, 900),
		new GSPoint4(1595, 770, 1695, 810, 1580, 960, 1420, 895),
		new GSPoint4(1465, 515, 1520, 520, 1490, 795, 1440, 785),
		new GSPoint4(1465, 515, 1520, 520, 1490, 795, 1440, 785),
		new GSPoint4(1790, 550, 1820, 550, 1790, 905, 1750, 870),
		new GSPoint4(1305, 395, 1360, 375, 1340, 680, 1285, 650),
		new GSPoint4(900, 305, 935, 285, 980, 295, 1005, 340),
		new GSPoint4(900, 305, 1005, 340, 965, 420, 895, 330),
		new GSPoint4(895, 330, 965, 420, 925, 410, 895, 365),
		new GSPoint4(905, 415, 990, 415, 1010, 530, 855, 565),
		new GSPoint4(810, 455, 890, 445, 845, 605, 810, 605),
		new GSPoint4(795, 535, 855, 535, 845, 730, 795, 730),
		new GSPoint4(880, 565, 1010, 530, 1000, 720, 845, 730),
		new GSPoint4(1515, 155, 1550, 155, 1550, 190, 1515, 190),
		new GSPoint4(1065, 165, 1095, 165, 1095, 200, 1065, 200)
	};

	private static readonly uint[] GS2_find_message = new uint[22]
	{
		234u, 228u, 229u, 230u, 220u, 222u, 232u, 225u, 217u, 221u,
		219u, 218u, 227u, 226u, 223u, 233u, 224u, 224u, 223u, 231u,
		223u, 216u
	};

	private static readonly uint[] GS3_find_message = new uint[20]
	{
		328u, 318u, 319u, 320u, 321u, 322u, 323u, 324u, 324u, 324u,
		325u, 326u, 326u, 326u, 326u, 326u, 326u, 326u, 327u, 317u
	};

	private const float correct_x = 6.3f;

	private const float correct_y = 5.7f;

	private const float offset_x = 300f;

	private const float offset_y = 0f;

	private const int cursor_icon_default_ = 0;

	private const int cursor_icon_hit_ = 1;

	public const int TANCHIKI_CRS_HOSEI_SX = 0;

	public const int TANCHIKI_CRS_HOSEI_SY = 0;

	public const int TANCHIKI_CRS_HOSEI_SY2 = 0;

	public const int TANCHIKI_RECT_X = 0;

	public const int TANCHIKI_RECT_Y = 0;

	public const int TANCHIKI_RECT_W = 2;

	public const int TANCHIKI_RECT_H = 2;

	public const int DENPA_SCALE_WAIT = 9;

	public const int DENPA_SCALEMAX = 6;

	public const int ANTENA_FLASH_WAIT = 8;

	public static TanchikiMiniGame instance { get; private set; }

	public static GSPoint4[] find_target
	{
		get
		{
			switch (GSStatic.global_work_.title)
			{
			case TitleId.GS2:
				return GS2_find_target;
			case TitleId.GS3:
				return GS3_find_target;
			default:
				return null;
			}
		}
	}

	public static uint[] find_message
	{
		get
		{
			switch (GSStatic.global_work_.title)
			{
			case TitleId.GS2:
				return GS2_find_message;
			case TitleId.GS3:
				return GS3_find_message;
			default:
				return null;
			}
		}
	}

	private void Awake()
	{
		instance = this;
	}

	private void load()
	{
		string in_path = "/menu/common/";
		string in_name = ((GSStatic.global_work_.title != TitleId.GS2) ? "base1" : "base");
		AssetBundle assetBundle = AssetBundleCtrl.instance.load(in_path, in_name);
		cursor_sprites_ = assetBundle.LoadAllAssets<Sprite>();
	}

	public void init()
	{
		radio_wave_.init();
		load();
		cursor_ = MiniGameCursor.instance;
		cursor_.icon_visible = false;
		cursor_.icon_sprite = cursor_sprites_[0];
		cursor_position_reset();
	}

	public void end()
	{
		cursor_.icon_visible = false;
		cursor_.icon_sprite = null;
		cursor_position_reset();
		radio_wave_.end();
	}

	public int hit_check()
	{
		GSRect rect = default(GSRect);
		rect.x = (short)(cursor_.cursor_position.x + bgCtrl.instance.bg_pos_x);
		rect.y = (short)cursor_.cursor_position.y;
		rect.w = 2;
		rect.h = 2;
		int num = MiniGameGSPoint4Hit.CheckHit(rect, find_target);
		if (num == -1 || num >= find_target.Length - 1)
		{
			num = find_target.Length - 1;
		}
		return num;
	}

	public void UpdateCursor()
	{
		cursor_.Process();
		set_level();
		radio_wave_.update();
		radio_wave_.position = new Vector2(cursor_.cursor_position.x, 0f - cursor_.cursor_position.y);
	}

	public void EnabledCursor(bool i_enabled)
	{
		cursor_.icon_visible = i_enabled;
		radio_wave_.active = i_enabled;
	}

	public void EnabledWave(bool i_enabled)
	{
		radio_wave_.active = i_enabled;
	}

	public void UpdateWavePos()
	{
		if (cursor_ != null)
		{
			radio_wave_.position = new Vector2(cursor_.cursor_position.x, 0f - cursor_.cursor_position.y);
		}
	}

	public void UpdateWave()
	{
		set_level();
		radio_wave_.update();
		radio_wave_.position = new Vector2(cursor_.cursor_position.x, 0f - cursor_.cursor_position.y);
	}

	private int Min_distance()
	{
		uint num = uint.MaxValue;
		Vector2 vector = default(Vector2);
		for (int i = 0; i < find_target.Length - 1; i++)
		{
			GSPoint4 gSPoint = find_target[i];
			vector.x = (gSPoint.x0 + gSPoint.x1 + gSPoint.x2 + gSPoint.x3) / 4;
			vector.y = (gSPoint.y0 + gSPoint.y1 + gSPoint.y2 + gSPoint.y3) / 4;
			vector.x += 0f - bgCtrl.instance.bg_pos_x;
			vector.y = vector.y;
			Vector2 vector2 = vector - (Vector2)cursor_.cursor_position;
			uint num2 = (uint)(Mathf.Abs(vector2.x) + Mathf.Abs(vector2.y));
			if (num2 < num)
			{
				num = num2;
			}
		}
		return (int)num;
	}

	private void set_level()
	{
		if (hit_check() != find_message.Length - 1)
		{
			cursor_.icon_sprite = cursor_sprites_[1];
			radio_wave_.level = 5;
			return;
		}
		cursor_.icon_sprite = cursor_sprites_[0];
		int num = Min_distance();
		int num2 = 157;
		int num3 = num / num2 + 1;
		num3 = 5 - num3;
		num3 = Mathf.Clamp(num3, 1, 4);
		radio_wave_.level = num3;
	}

	private void cursor_position_reset()
	{
		cursor_.cursor_position = new Vector3(systemCtrl.instance.ScreenWidth / 2, systemCtrl.instance.ScreenHeight / 2, 0f);
		radio_wave_.position = new Vector2(cursor_.cursor_position.x, 0f - cursor_.cursor_position.y);
		cursor_.icon_offset = Vector3.zero;
	}
}
