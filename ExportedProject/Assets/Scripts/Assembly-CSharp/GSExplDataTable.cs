using UnityEngine;

public static class GSExplDataTable
{
	public enum GS1_EXPL
	{
		GS1_EXPL_HANNIN = 0,
		GS1_EXPL_HIGAISHA = 1,
		GS1_EXPL_SCE1_SITAI = 2,
		GS1_EXPL_SCE1_OKIMONO = 3,
		GS1_EXPL_SCE2_1_STA = 4,
		GS1_EXPL_SCE2_2_STA = 5,
		GS1_EXPL_SCE2_STAFF_ROOM = 6,
		GS1_EXPL_GATE = 7,
		GS1_EXPL_FRONT_GATE = 8,
		GS1_EXPL_OBACHAN = 9,
		GS1_EXPL_BOAT = 10,
		GS1_EXPL_COTTAGE = 11,
		GS1_EXPL_CAR = 12,
		GS1_EXPL_BOAT2 = 13,
		GS1_EXPL_SCE4_1_WITNESS = 14,
		GS1_EXPL_WALL00 = 15,
		GS1_EXPL_WALL01 = 16,
		GS1_EXPL_MITSURUGI_CAR00 = 17,
		GS1_EXPL_MITSURUGI_CAR01 = 18,
		GS1_EXPL_KYOKA_CAR00 = 19,
		GS1_EXPL_KYOKA_CAR01 = 20,
		GS1_EXPL_FENCE_CMN00 = 21,
		GS1_EXPL_FENCE_CMN01 = 22,
		GS1_EXPL_FENCE_CMN02 = 23,
		GS1_EXPL_FENCE = 24,
		GS1_EXPL_R2_KEKKON = 25,
		GS1_EXPL_R7_TAIHO = 26,
		GS1_EXPL_R7_KEKKON = 27,
		GS1_EXPL_A_PANEL = 28,
		GS1_EXPL_B_PANEL = 29,
		GS1_EXPL_GUARD_ROOM = 30,
		GS1_EXPL_POINT = 31,
		GS1_EXPL_COUNT = 32
	}

	private enum GS1_EXPL_U
	{
		GS1_EXPL_HANNIN_USA = 0,
		GS1_EXPL_HIGAISHA_USA = 1,
		GS1_EXPL_SCE2_1_STA_USA = 2,
		GS1_EXPL_SCE2_2_STA_USA = 3,
		GS1_EXPL_SCE2_STAFF_ROOM_USA = 4,
		GS1_EXPL_FRONT_GATE_USA = 5,
		GS1_EXPL_SCE4_1_WITNESS_USA = 6,
		GS1_EXPL_R7_TAIHO_USA = 7,
		GS1_EXPL_GUARD_ROOM_USA = 8
	}

	public enum GS2_EXPL
	{
		GS2_EXPL_HANNIN = 0,
		GS2_EXPL_HIGAISHA = 1,
		GS2_EXPL_SHOUNIN = 2,
		GS2_EXPL_SCE1_1_DOOR = 3,
		GS2_EXPL_SCE1_1_BYOUBU1 = 4,
		GS2_EXPL_SCE1_1_BYOUBU2 = 5,
		GS2_EXPL_SCE1_1_BYOUBU3 = 6,
		GS2_EXPL_SHOUNIN2 = 7,
		GS2_EXPL_DANKON = 8,
		GS2_EXPL_POINT = 9,
		GS2_EXPL_COUNT = 10
	}

	private enum GS2_EXPL_U
	{
		GS2_EXPL_HANNIN_USA = 0,
		GS2_EXPL_HIGAISHA_USA = 1
	}

	public enum GS3_EXPL
	{
		GS3_EXPL_MOKUGEKI = 0,
		GS3_EXPL_HIGAISHA = 1,
		GS3_EXPL_MIRROR_L = 2,
		GS3_EXPL_MIRROR_R = 3,
		GS3_EXPL_HANNIN = 4,
		GS3_EXPL_DAMMY = 5,
		GS3_EXPL_OTHER = 6,
		GS3_EXPL_CAR_TILE = 7,
		GS3_EXPL_CAR = 8,
		GS3_EXPL_LIFE_DMY = 9,
		GS3_EXPL_RED = 10,
		GS3_EXPL_SNOW = 11,
		GS3_EXPL_SNOW2 = 12,
		GS3_EXPL_BALL = 13,
		GS3_EXPL_WIRE = 14,
		GS3_EXPL_POINT = 15,
		GS3_EXPL_COUNT = 16
	}

	private enum GS3_EXPL_U
	{
		GS3_EXPL_MOKUGEKI_SUB = 0,
		GS3_EXPL_HIGAISHA_SUB = 1,
		GS3_EXPL_HANNIN_SUB = 2
	}

	public struct EXPL_DATA
	{
		public ushort para0;

		public ushort para1;

		public ushort para2;

		public ushort sub_id;

		public EXPL_DATA(ushort para0, ushort para1, ushort para2, ushort sub_id)
		{
			this.para0 = para0;
			this.para1 = para1;
			this.para2 = para2;
			this.sub_id = sub_id;
		}

		public EXPL_DATA(int para0, int para1, ushort para2, ushort sub_id)
		{
			this.para0 = (ushort)para0;
			this.para1 = (ushort)para1;
			this.para2 = para2;
			this.sub_id = sub_id;
		}
	}

	public const int EXPL_NONE_SUB = 65535;

	private static int PARKING_HOSEI_LX = 0;

	private static int PARKING_HOSEI_RX = -14;

	private static int PARKING_HOSEI_Y = 0;

	private static int BENTOU_HOSEI_X = PARKING_HOSEI_RX;

	private static int BENTOU_HOSEI_Y = 0;

	private static int BATD_X = (int)scenario.BATD_X;

	private static int BATD_Y = (int)scenario.BATD_Y;

	private static EXPL_DATA[] GS1_expl_char_data_tbl = new EXPL_DATA[32]
	{
		new EXPL_DATA(BATD_Y, 16384 + BATD_X, 0, 0),
		new EXPL_DATA(BATD_Y, 16384 + BATD_X, 0, 1),
		new EXPL_DATA(32872, 49280, 0, ushort.MaxValue),
		new EXPL_DATA(32880 + BATD_Y, 160, 0, ushort.MaxValue),
		new EXPL_DATA(8 + BATD_Y, 49152 + BATD_X, 0, 2),
		new EXPL_DATA(80 + BATD_Y, 49224 + BATD_X, 0, 3),
		new EXPL_DATA(40 + BATD_Y, 49328 + BATD_X, 0, 4),
		new EXPL_DATA(32784 + BATD_Y, 16488 + BATD_X, 0, ushort.MaxValue),
		new EXPL_DATA(16384 + BATD_Y, 32912 + BATD_X, 0, 5),
		new EXPL_DATA(16 + BATD_Y, 16536 + BATD_X, 0, ushort.MaxValue),
		new EXPL_DATA(16432 + BATD_Y, 80 + BATD_X, 0, ushort.MaxValue),
		new EXPL_DATA(24 + BATD_Y, 49224 + BATD_X, 0, ushort.MaxValue),
		new EXPL_DATA(16392 + BATD_Y, 32872 + BATD_X, 0, ushort.MaxValue),
		new EXPL_DATA(32816 + BATD_Y, 80 + BATD_X, 0, ushort.MaxValue),
		new EXPL_DATA(48, 16488 + PARKING_HOSEI_RX, 0, 6),
		new EXPL_DATA(32807, 32875 + PARKING_HOSEI_LX, 0, ushort.MaxValue),
		new EXPL_DATA(71, 16491 + PARKING_HOSEI_LX, 0, ushort.MaxValue),
		new EXPL_DATA(51 + PARKING_HOSEI_Y, 32916 + PARKING_HOSEI_LX, 0, ushort.MaxValue),
		new EXPL_DATA(32819 + PARKING_HOSEI_Y, 16564 + PARKING_HOSEI_LX, 0, ushort.MaxValue),
		new EXPL_DATA(47 + BENTOU_HOSEI_Y, 32920 + BENTOU_HOSEI_X, 0, ushort.MaxValue),
		new EXPL_DATA(32815 + BENTOU_HOSEI_Y, 16568 + BENTOU_HOSEI_X, 0, ushort.MaxValue),
		new EXPL_DATA(32807, 16573 + PARKING_HOSEI_LX, 0, ushort.MaxValue),
		new EXPL_DATA(32839, 16573 + PARKING_HOSEI_LX, 0, ushort.MaxValue),
		new EXPL_DATA(32871, 16573 + PARKING_HOSEI_LX, 0, ushort.MaxValue),
		new EXPL_DATA(32903, 16573 + PARKING_HOSEI_LX, 0, ushort.MaxValue),
		new EXPL_DATA(55, 16520 + PARKING_HOSEI_LX, 0, ushort.MaxValue),
		new EXPL_DATA(16496, 49280, 0, 7),
		new EXPL_DATA(104, 16528, 0, ushort.MaxValue),
		new EXPL_DATA(80, 32843 + PARKING_HOSEI_LX, 0, ushort.MaxValue),
		new EXPL_DATA(80, 32896 + PARKING_HOSEI_RX, 0, ushort.MaxValue),
		new EXPL_DATA(32800, 49176 + PARKING_HOSEI_LX, 0, 8),
		new EXPL_DATA(0, 32768, 0, ushort.MaxValue)
	};

	private static EXPL_DATA[] GS2_expl_char_data_tbl = new EXPL_DATA[10]
	{
		new EXPL_DATA(BATD_Y, 16384 + BATD_X, 0, 0),
		new EXPL_DATA(BATD_Y, 16384 + BATD_X, 0, 1),
		new EXPL_DATA(BATD_Y, 16384 + BATD_X, 0, ushort.MaxValue),
		new EXPL_DATA(32768 + BATD_Y, 49152 + BATD_X, 0, ushort.MaxValue),
		new EXPL_DATA(32768 + BATD_Y, 32768 + BATD_X, 0, ushort.MaxValue),
		new EXPL_DATA(32768 + BATD_Y, 32768 + BATD_X, 0, ushort.MaxValue),
		new EXPL_DATA(32768 + BATD_Y, 32768 + BATD_X, 0, ushort.MaxValue),
		new EXPL_DATA(BATD_Y, 16384 + BATD_X, 0, ushort.MaxValue),
		new EXPL_DATA(BATD_Y, BATD_X, 0, ushort.MaxValue),
		new EXPL_DATA(0, 32768, 0, ushort.MaxValue)
	};

	private static EXPL_DATA[] GS3_expl_char_data_tbl = new EXPL_DATA[16]
	{
		new EXPL_DATA(BATD_Y, 16384 + BATD_X, 0, 0),
		new EXPL_DATA(BATD_Y, 16384 + BATD_X, 0, 1),
		new EXPL_DATA(16384 + BATD_Y, 16384 + BATD_X, 0, ushort.MaxValue),
		new EXPL_DATA(16384 + BATD_Y, 16384 + BATD_X, 0, ushort.MaxValue),
		new EXPL_DATA(BATD_Y, 16384 + BATD_X, 0, 2),
		new EXPL_DATA(512 + BATD_Y, 16384 + BATD_X, 0, ushort.MaxValue),
		new EXPL_DATA(BATD_Y, 16384 + BATD_X, 0, ushort.MaxValue),
		new EXPL_DATA(16384 + BATD_Y, 32768 + BATD_X, 0, ushort.MaxValue),
		new EXPL_DATA(16384 + BATD_Y, 32768 + BATD_X, 0, ushort.MaxValue),
		new EXPL_DATA(512 + BATD_Y, 16384 + BATD_X, 0, ushort.MaxValue),
		new EXPL_DATA(BATD_Y, 16384 + BATD_X, 0, ushort.MaxValue),
		new EXPL_DATA(BATD_Y, 32768 + BATD_X, 0, ushort.MaxValue),
		new EXPL_DATA(BATD_Y, 36864 + BATD_X, 0, ushort.MaxValue),
		new EXPL_DATA(BATD_Y, BATD_X, 0, ushort.MaxValue),
		new EXPL_DATA(BATD_Y, 32768 + BATD_X, 0, ushort.MaxValue),
		new EXPL_DATA(0, 32768, 0, ushort.MaxValue)
	};

	private static EXPL_DATA[][] expl_tbl = new EXPL_DATA[3][] { GS1_expl_char_data_tbl, GS2_expl_char_data_tbl, GS3_expl_char_data_tbl };

	private static string[] GS1_expl_char_data_path_tbl = new string[32]
	{
		"itb000", "itb001", "itb002", "itb003", "itb004", "itb005", "itb006", "itb007", "itb008", "itb009",
		"itb00a", "itb00b", "itb00c", "itb00d", "itb00e", "itb00f", "itb010", "itb011", "itb012", "itb013",
		"itb014", "itb015", "itb015", "itb015", "itb016", "itb017", "itb018", "itb019", "itb01a", "itb01b",
		"itb01c", "itb01d"
	};

	private static string[] GS1_expl_char_data_path_tbl_sub = new string[9] { "itb000u", "itb001u", "itb004u", "itb005u", "itb006u", "itb008u", "itb00eu", "itb018u", "itb01cu" };

	private static string[] GS2_expl_char_data_path_tbl = new string[10] { "itb000", "itb001", "itb002", "itb003", "itb004", "itb005", "itb006", "itb002", "itb007", "itb01d" };

	private static string[] GS2_expl_char_data_path_tbl_sub = new string[2] { "itb000u", "itb001u" };

	private static string[] GS3_expl_char_data_path_tbl = new string[16]
	{
		"itb000", "itb001", "itb002", "itb003", "itb005", "itb004", "itb007", "itb008", "itb009", "itb004",
		"itb00c", "itb00f", "itb00f", "itb010", "itb011", "itb01d"
	};

	private static string[] GS3_expl_char_data_path_tbl_sub = new string[3] { "itb000u", "itb001u", "itb005u" };

	private static string[][] expl_path_tbl = new string[3][] { GS1_expl_char_data_path_tbl, GS2_expl_char_data_path_tbl, GS3_expl_char_data_path_tbl };

	private static string[][] expl_sub_path_tbl = new string[3][] { GS1_expl_char_data_path_tbl_sub, GS2_expl_char_data_path_tbl_sub, GS3_expl_char_data_path_tbl_sub };

	public static int MAX_EXPL_COUNT
	{
		get
		{
			return Mathf.Max(32, 10, 16);
		}
	}

	public static EXPL_DATA GetExplCharData(uint id)
	{
		return expl_tbl[(int)GSStatic.global_work_.title][id];
	}

	public static string GetExplCharFilename(uint id)
	{
		if (GSStatic.global_work_.language != 0)
		{
			uint sub_id = GetExplCharData(id).sub_id;
			if (sub_id != 65535)
			{
				return expl_sub_path_tbl[(int)GSStatic.global_work_.title][sub_id];
			}
		}
		return expl_path_tbl[(int)GSStatic.global_work_.title][id];
	}
}
