using System.Collections.Generic;
using UnityEngine;

public class polyDataCtrl : MonoBehaviour
{
	private const int ANIM_EV_START = 1;

	private const int NOANIM_EV_START = 8;

	public const int SHOES_SUCSESS = 20;

	private static int[] ev_itm0060 = new int[4] { 9, 8, 8, 8 };

	private static int[] ev_itm0460 = new int[1] { 5 };

	private static int[] ev_itm0465 = new int[1] { 2 };

	private static int[] ev_itm0480 = new int[1] { 10 };

	private static int[] ev_itm0490 = new int[2] { 11, 12 };

	private static int[] ev_itm04a0 = new int[1] { 13 };

	private static int[] ev_itm04b0 = new int[1] { 14 };

	private static int[] ev_itm04c0 = new int[1] { 15 };

	private static int[] ev_itm04e0 = new int[4] { 3, 16, 16, 16 };

	private static int[] ev_itm04e8 = new int[4] { 18, 17, 18, 18 };

	private static int[] ev_itm0500 = new int[3] { 19, 20, 20 };

	private static int[] ev_itm0510 = new int[1] { 21 };

	private static int[] ev_itm0520 = new int[2] { 22, 4 };

	private static int[] ev_itm0525 = new int[2] { 24, 23 };

	private static int[] ev_itm0540 = new int[1] { 25 };

	private static int[] ev_itm0550 = new int[4] { 36, 38, 38, 38 };

	private static int[] ev_itm0560 = new int[1] { 26 };

	private static int[] ev_itm05a0 = new int[2] { 28, 27 };

	private static int[] ev_itm05a2 = new int[2] { 37, 27 };

	private static int[] ev_itm05c0 = new int[1] { 29 };

	private static int[] ev_itm05f0 = new int[1] { 30 };

	private static int[] ev_itm0600 = new int[1] { 31 };

	private static int[] ev_itm0610 = new int[1] { 35 };

	private static int[] ev_itm0620 = new int[1] { 32 };

	private static int[] ev_itm0700 = new int[2] { 33, 34 };

	private static int[] ev_itm0730 = new int[3] { 39, 39, 39 };

	private static int[] ev_itm0820 = new int[1] { 6 };

	private static int[] ev_itm0830 = new int[1] { 7 };

	private List<polyData> obj_table_ = new List<polyData>
	{
		new polyData("itm0060", "itm0060", null, "itm0062", ev_itm0060),
		new polyData("itm0460", "itm0463", null, "itm0462", ev_itm0460),
		new polyData("itm0460", "itm0465", null, "itm0467", ev_itm0465),
		new polyData("itm0480", "itm0480", null, "atari", ev_itm0480),
		new polyData("itm0490", "itm0490", null, "atari", ev_itm0490),
		new polyData("itm04a0", "itm04a0", null, "atari", ev_itm04a0),
		new polyData("itm04b0", "itm04b0", null, "atari", ev_itm04b0),
		new polyData("itm04c0", "itm04c0", null, "atari", ev_itm04c0),
		new polyData("itm04e0", "itm04e02", "itm04e03", "itm04e2", ev_itm04e0, new string[2] { "itm04e02_3", "itm04e02_7" }),
		new polyData("itm04e0", "itm04e03", null, "itm04e3", ev_itm04e8, new string[4] { "itm04e03_1", "itm04e03_2", "itm04e03_3", "itm04e03_4" }),
		new polyData("itm0500", "itm0500", null, "atari", ev_itm0500),
		new polyData("itm0510", "itm0510", null, null, ev_itm0510),
		new polyData("itm0520", "itm0521", null, "itm0523", ev_itm0520),
		new polyData("itm0520", "itm0521", null, "itm0524", ev_itm0525),
		new polyData("itm0540", "itm0540", null, "atari", ev_itm0540),
		new polyData("itm0550", "itm0550", null, "itm0551", ev_itm0550),
		new polyData("itm0560", "itm0560", null, "itm0562", ev_itm0560),
		new polyData("itm05a0", "itm05a0", null, "itm05a1", ev_itm05a0),
		new polyData("itm05a0", "itm05a2", null, "itm05a1", ev_itm05a2),
		new polyData("itm05c0", "itm05c0", null, "atari", ev_itm05c0),
		new polyData("itm05d0", "itm05d0", null, null, null),
		new polyData("itm05f0", "itm05f0", null, "itm05f1", ev_itm05f0),
		new polyData("itm0600", "itm0600", null, "itm0601", ev_itm0600),
		new polyData("itm0610", "itm0610", null, "atari", ev_itm0610),
		new polyData("itm0620", "itm0620", null, "itm0621", ev_itm0620),
		new polyData("itm0700", "itm0700", null, "atari", ev_itm0700),
		new polyData("itm0730", "itm0730", null, "itm0730a", ev_itm0730),
		new polyData("itm0820", "itm0820", null, "itm0820a", ev_itm0820),
		new polyData("itm0820", "itm0820", null, null, null),
		new polyData("itm0830", "itm0830", null, "itm0830a", ev_itm0830),
		new polyData("itm0460", "itm0463", null, null, null),
		new polyData("itm0460", "itm0464", null, null, null),
		new polyData("itm0460", "itm0468", null, null, null),
		new polyData("itm0460", "itm0469", null, null, null),
		new polyData("itm04e0", "itm04e5", null, null, null),
		new polyData("itm04e0", "itm04e6", null, null, null),
		new polyData("itm0520", "itm0523", null, null, null),
		new polyData("itm0520", "itm0524", null, null, null),
		new polyData("itm0820", "itm0821", null, null, null),
		new polyData("itm0820", "itm0822", null, null, null),
		new polyData("itm0820", "itm0823", null, null, null),
		new polyData("itm0830", "itm0831", null, null, null),
		new polyData("itm0830", "itm0832", null, null, null),
		new polyData("itm0830", "itm0833", null, null, null),
		new polyData("itm0830", "itm0834", null, null, null),
		new polyData("itm0830", "itm0835", null, null, null),
		new polyData("itm05a0", "itm05a3", null, null, null),
		new polyData("itm05a0", "itm05a4", null, null, null),
		new polyData("itm05a0", "itm05a5", null, null, null),
		new polyData("itm05a0", "itm05a6", null, null, null),
		new polyData("itm05a0", "itm05a7", null, null, null),
		new polyData("itm05a0", "itm05a8", null, null, null),
		new polyData("itm05a0", "itm05a9", null, null, null),
		new polyData("itm05a0", "itm05aa", null, null, null),
		new polyData("itm05a0", "itm05ab", null, null, null),
		new polyData("itm05a0", "itm05ac", null, null, null),
		new polyData("itm05a0", "itm0590", null, null, null),
		new polyData("itm05a0", "itm0591", null, null, null),
		new polyData("itm05a0", "itm0592", null, null, null),
		new polyData("itm05a0", "itm0593", null, null, null),
		new polyData("itm05a0", "itm0594", null, null, null),
		new polyData("itm05a0", "itm0595", null, null, null),
		new polyData("itm05a0", "itm0596", null, null, null),
		new polyData("itm05a0", "itm05a2", null, null, null),
		new polyData("itm05a0", "itm05a0", null, null, null),
		new polyData("itm0460", "itm0461", null, null, null)
	};

	private List<polyData> obj_sub_table_ = new List<polyData>
	{
		new polyData("itm04e0", "itm04e03", null, "itm04e3", null, new string[4] { "itm04e03_1", "itm04e03_2", "itm04e03_3", "itm04e03_4" }),
		new polyData("itm0460", "itm0461", "itm0463", null, null),
		new polyData("itm0460", "itm0463", "itm0464", null, null),
		new polyData("itm0460", "itm0464", null, null, null),
		new polyData("itm0460", "itm0466", null, null, null)
	};

	private List<usPolyData> replace_table_ = new List<usPolyData>
	{
		new usPolyData("itm0060", "itm0060u"),
		new usPolyData("itm0463", "itm0463u"),
		new usPolyData("itm0466", "itm0466u"),
		new usPolyData("itm0469", "itm0469u"),
		new usPolyData("itm0480", "itm0480u"),
		new usPolyData("itm04a0", "itm04a0u"),
		new usPolyData("itm04c0", "itm04c0u"),
		new usPolyData("itm0540", "itm0540u"),
		new usPolyData("itm0550", "itm0550u"),
		new usPolyData("itm0600", "itm0600u"),
		new usPolyData("itm0610", "itm0610u"),
		new usPolyData("itm0820", "itm0820u"),
		new usPolyData("itm0821", "itm0821u"),
		new usPolyData("itm0822", "itm0822u"),
		new usPolyData("itm0830", "itm0830u"),
		new usPolyData("itm0831", "itm0831u"),
		new usPolyData("itm0832", "itm0832u"),
		new usPolyData("itm0833", "itm0833u"),
		new usPolyData("itm05a0", "itm05a0u"),
		new usPolyData("itm05f0", "itm05f0u"),
		new usPolyData("itm082a", "itm082au"),
		new usPolyData("itm0061", "itm0061u"),
		new usPolyData("itm083c", "itm083cu"),
		new usPolyData("itm05a2", "itm05a2u")
	};

	public static polyDataCtrl instance { get; private set; }

	public List<polyData> obj_table
	{
		get
		{
			return obj_table_;
		}
	}

	private void Awake()
	{
		instance = this;
	}

	public int GetObjId(polyData poly)
	{
		int num = -1;
		string poly_prefab_name = poly.prefab_name;
		if (GSStatic.global_work_.language == Language.USA)
		{
			usPolyData usPolyData2 = replace_table_.Find((usPolyData p) => p.us_name == poly.prefab_name);
			if (usPolyData2 != null)
			{
				poly_prefab_name = usPolyData2.jp_name;
			}
		}
		List<polyData> finds = obj_table_.FindAll((polyData p) => p.common_name == poly.common_name && p.prefab_name == poly_prefab_name);
		if (finds.Count == 1)
		{
			return obj_table_.FindIndex((polyData p) => p == finds[0]);
		}
		return obj_table_.FindIndex((polyData p) => p.common_name == poly.common_name && p.prefab_name == poly_prefab_name && p.sub_prefab_name == poly.sub_prefab_name && p.event_tbl == poly.event_tbl);
	}

	public polyData GetPolyData(int obj_id)
	{
		return GetData(obj_table_, obj_id);
	}

	public polyData GetSubPolyData(polyData poly)
	{
		if (string.IsNullOrEmpty(poly.sub_prefab_name))
		{
			return null;
		}
		int num = obj_sub_table_.FindIndex((polyData p) => p.prefab_name == poly.sub_prefab_name);
		if (num < 0)
		{
			return null;
		}
		return GetData(obj_sub_table_, num);
	}

	private polyData GetData(List<polyData> table, int obj_id)
	{
		polyData polyData2 = table[obj_id];
		polyData polyData3 = new polyData(polyData2.common_name, polyData2.prefab_name, polyData2.sub_prefab_name, polyData2.hit_prefab_name, polyData2.event_tbl, polyData2.col_obj_names);
		if (GSStatic.global_work_.language == Language.USA)
		{
			ReplaceNameToUS(polyData3);
		}
		if (GSStatic.global_work_.language == Language.JAPAN)
		{
			ReplaceNameToJP(polyData3);
		}
		return polyData3;
	}

	private void ReplaceNameToUS(polyData poly)
	{
		if (GSStatic.global_work_.language == Language.USA)
		{
			usPolyData usPolyData2 = replace_table_.Find((usPolyData p) => p.jp_name == poly.prefab_name);
			if (usPolyData2 != null)
			{
				poly.prefab_name = usPolyData2.us_name;
			}
			usPolyData2 = replace_table_.Find((usPolyData p) => p.jp_name == poly.sub_prefab_name);
			if (usPolyData2 != null)
			{
				poly.sub_prefab_name = usPolyData2.us_name;
			}
		}
	}

	private void ReplaceNameToJP(polyData poly)
	{
		if (GSStatic.global_work_.language == Language.JAPAN)
		{
			usPolyData usPolyData2 = replace_table_.Find((usPolyData p) => p.us_name == poly.prefab_name);
			if (usPolyData2 != null)
			{
				poly.prefab_name = usPolyData2.jp_name;
			}
			usPolyData2 = replace_table_.Find((usPolyData p) => p.us_name == poly.sub_prefab_name);
			if (usPolyData2 != null)
			{
				poly.sub_prefab_name = usPolyData2.jp_name;
			}
		}
	}
}
