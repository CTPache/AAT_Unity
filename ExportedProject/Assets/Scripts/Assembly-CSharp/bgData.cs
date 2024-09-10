using System;
using System.Collections.Generic;
using UnityEngine;

public class bgData : MonoBehaviour
{
	[Serializable]
	public class DataBG
	{
		public string name_ = string.Empty;

		public string sub_ = string.Empty;

		public string icon_ = string.Empty;

		public uint type_;

		public uint language_ = 32768u;

		public DataBG(string in_name, string in_sub, string in_icon, uint in_type, uint in_language)
		{
			name_ = in_name;
			sub_ = in_sub;
			icon_ = in_icon;
			type_ = in_type;
			language_ = in_language;
		}
	}

	[Serializable]
	public class DataSeal
	{
		public string name_ = string.Empty;

		public string name_u_ = string.Empty;

		public float x_;

		public float y_;

		public float w_;

		public float h_;

		public DataSeal(string in_name, string in_name_u, float in_x, float in_y, float in_w, float in_h)
		{
			name_ = in_name;
			name_u_ = in_name_u;
			x_ = in_x;
			y_ = in_y;
			w_ = in_w;
			h_ = in_h;
		}
	}

	public enum BG_SEAL
	{
		BG09E_SEAL = 44
	}

	public enum GS3_BG_SEAL
	{
		SEAL_BG564 = 37,
		SEAL_BG565 = 38,
		SEAL_BG566 = 39,
		SEAL_BG567 = 40,
		SEAL_BG568 = 41,
		SEAL_BG569 = 42,
		SEAL_BG56A = 43,
		SEAL_BG56B = 44,
		SEAL_BG56C = 45,
		SEAL_BG56D = 46,
		SEAL_BG56E = 47,
		SEAL_BG56F = 48,
		SEAL_BG570 = 49,
		SEAL_BG571 = 50,
		SEAL_BG572 = 51,
		SEAL_BG573 = 52,
		SEAL_BG574 = 53,
		SEAL_BG57A = 54
	}

	private static bgData instance_;

	public const float DISP_RATE = 5.625f;

	private List<DataSeal> gs1_seal_data_ = new List<DataSeal>
	{
		new DataSeal("bg078", "bg078", 417.5f, -253f, 22.5f, 39.375f),
		new DataSeal("bg079", "bg079u", 512f, -375f, 11.25f, 11.25f),
		new DataSeal("bg07a", "bg07a", 599.5f, -312f, 16.875f, 28.125f),
		new DataSeal("bg07b", "bg07b", 525f, -189f, 22.5f, 11.25f),
		new DataSeal("bg07d", "bg07d", 229.5f, -97f, 22.5f, 33.75f),
		new DataSeal("bg07c", "bg07c", 247f, -411f, 16.875f, 16.875f),
		new DataSeal("bg118", "bg118u", 17.5f, 177.5f, 16.875f, 16.875f),
		new DataSeal("bg119", "bg119u", 27f, 57f, 16.875f, 16.875f),
		new DataSeal("bg0b0", "bg0b0", 773f, -266f, 16.875f, 16.875f),
		new DataSeal("bg0af", "bg0af", 2036f, -401f, 16.875f, 16.875f),
		new DataSeal("bg0a8", "bg0a8", 2499f, 107f, 16.875f, 16.875f),
		new DataSeal("bg0b1", "bg0b1", 205f, -285f, 16.875f, 16.875f),
		new DataSeal("bg0c4", "bg0c4", 2963f, 60.5f, 16.875f, 16.875f),
		new DataSeal("ita00a", "ita00a", 1056.5f, -83f, 16.875f, 16.875f),
		new DataSeal("bg11a", "bg11a", 486f, 189f, 16.875f, 16.875f),
		new DataSeal("bg11b", "bg11b", 486f, 378f, 16.875f, 16.875f),
		new DataSeal("bg11au", "bg11au", 486f, 189f, 16.875f, 16.875f),
		new DataSeal("bg11bu", "bg11bu", 486f, 378f, 16.875f, 16.875f),
		new DataSeal("bg118u", "bg118u", 17.5f, 177.5f, 16.875f, 16.875f),
		new DataSeal("bg119u", "bg119u", 27f, 57f, 16.875f, 16.875f),
		new DataSeal("bg08e", "bg08e", 618f, 486.5f, 16.875f, 16.875f),
		new DataSeal("bg08eu", "bg08eu", 618f, 486.5f, 16.875f, 16.875f),
		new DataSeal("bg08f", "bg08f", 321.5f, 486.5f, 16.875f, 16.875f),
		new DataSeal("bg090", "bg090", 268.5f, 242.5f, 16.875f, 16.875f),
		new DataSeal("bg090u", "bg090u", 268.5f, 242.5f, 16.875f, 16.875f),
		new DataSeal("bg091", "bg091", 268.5f, -27.5f, 16.875f, 16.875f),
		new DataSeal("bg092", "bg092", 268.5f, -272f, 16.875f, 16.875f),
		new DataSeal("bg093", "bg093", 321.5f, -488.5f, 16.875f, 16.875f),
		new DataSeal("bg094", "bg094", 699.5f, -488.5f, 16.875f, 16.875f),
		new DataSeal("bg095", "bg095", 1130.5f, -488.5f, 16.875f, 16.875f),
		new DataSeal("bg096", "bg096", 1561.5f, -488.5f, 16.875f, 16.875f),
		new DataSeal("bg097", "bg097", 1830f, -488.5f, 16.875f, 16.875f),
		new DataSeal("bg098", "bg098", 2098.5f, -488.5f, 16.875f, 16.875f),
		new DataSeal("bg099", "bg099", 2448f, -488.5f, 16.875f, 16.875f),
		new DataSeal("bg09a", "bg09a", 2500.5f, -301.5f, 16.875f, 16.875f),
		new DataSeal("bg0a0", "bg0a0", 800.5f, 347f, 16.875f, 16.875f),
		new DataSeal("bg08d", "bg08d", 1125.5f, 240f, 16.875f, 16.875f),
		new DataSeal("bg0df", "bg0df", -380f, -105.5f, 16.875f, 16.875f),
		new DataSeal("bg0dd", "bg0dd", 410f, 350f, 16.875f, 16.875f),
		new DataSeal("bg0a0", "bg0a0", 190f, -115f, 16.875f, 16.875f),
		new DataSeal("bg09f", "bg09f", 166.5f, -105.5f, 16.875f, 16.875f),
		new DataSeal("bg0de", "bg0de", 216f, -162f, 16.875f, 16.875f),
		new DataSeal("bg0deu", "bg0deu", 216f, -162f, 16.875f, 16.875f),
		new DataSeal("bg09b", "bg09b", 2176.5f, 223f, 16.875f, 16.875f),
		new DataSeal("bg09e", "bg09e", 467f, -86f, 16.875f, 16.875f)
	};

	private List<DataSeal> gs2_seal_data_ = new List<DataSeal>
	{
		new DataSeal("ita002", "ita002", 1668f, -304f, 16.875f, 16.875f),
		new DataSeal("ita001", "ita001", -507f, -75f, 16.875f, 16.875f),
		new DataSeal("bg502", "bg502u", -270f, -401.5f, 16.875f, 16.875f),
		new DataSeal("bg502u", "bg502u", -270f, -401.5f, 16.875f, 16.875f),
		new DataSeal("bg50c", "bg50c", 486f, -42f, 16.875f, 16.875f),
		new DataSeal("bg50d", "bg50du", -189f, 174f, 16.875f, 16.875f),
		new DataSeal("bg50du", "bg50du", -189f, 174f, 16.875f, 16.875f),
		new DataSeal("bg503", "bg503", 405f, 3f, 16.875f, 16.875f),
		new DataSeal("bg505", "bg505u", 1157.5f, -357.5f, 16.875f, 16.875f),
		new DataSeal("bg505u", "bg505u", 1157.5f, -357.5f, 16.875f, 16.875f),
		new DataSeal("bg509", "bg509", 718f, -138f, 16.875f, 16.875f),
		new DataSeal("ita02a", "ita02a", 433f, -60f, 16.875f, 16.875f),
		new DataSeal("ita028", "ita028", 7f, -129f, 16.875f, 16.875f),
		new DataSeal("ita023", "ita023", -28f, -460f, 16.875f, 16.875f),
		new DataSeal("ita029", "ita029", 91f, -393f, 16.875f, 16.875f)
	};

	private List<DataSeal> gs3_seal_data_ = new List<DataSeal>
	{
		new DataSeal("bg208", "bg208u", 2605f, 165f, 16.875f, 16.875f),
		new DataSeal("bg208u", "bg208u", 2605f, 165f, 16.875f, 16.875f),
		new DataSeal("bg209", "bg209", 2724f, -14.5f, 16.875f, 16.875f),
		new DataSeal("bg209", "bg209", 3190f, -14.5f, 16.875f, 16.875f),
		new DataSeal("bg20ed", "bg20edu", 1360f, -370f, 16.875f, 16.875f),
		new DataSeal("bg20edu", "bg20edu", 1360f, -370f, 16.875f, 16.875f),
		new DataSeal("bg20f", "bg20f", 5f, -320f, 16.875f, 16.875f),
		new DataSeal("bg22b", "bg22b", -594f, -270f, 16.875f, 16.875f),
		new DataSeal("bg307", "bg307", -108f, -108f, 16.875f, 16.875f),
		new DataSeal("bg30d", "bg30d", 220f, -144f, 16.875f, 16.875f),
		new DataSeal("bg30e", "bg30e", 142f, 155.5f, 16.875f, 16.875f),
		new DataSeal("ita001", "ita001u", 541f, -287f, 16.875f, 16.875f),
		new DataSeal("ita001u", "ita001u", 541f, -287f, 16.875f, 16.875f),
		new DataSeal("ita002", "ita002", 507.5f, 286.5f, 16.875f, 16.875f),
		new DataSeal("ita003", "ita003u", 3351f, -376f, 16.875f, 16.875f),
		new DataSeal("ita003u", "ita003u", 3351f, -376f, 16.875f, 16.875f),
		new DataSeal("ita004", "ita004", 636f, -398f, 16.875f, 16.875f),
		new DataSeal("bg606", "bg606u", 82f, 280f, 16.875f, 16.875f),
		new DataSeal("bg606u", "bg606u", 82f, 280f, 16.875f, 16.875f),
		new DataSeal("bg607", "bg607", -354f, 360f, 16.875f, 16.875f),
		new DataSeal("bg558-", "bg558-", 108f, 269f, 16.875f, 16.875f),
		new DataSeal("bg55a", "bg55a", 265.5f, 121f, 16.875f, 16.875f),
		new DataSeal("bg511", "bg511", -70f, -50f, 16.875f, 16.875f),
		new DataSeal("bg518", "bg518u", 297f, -108f, 16.875f, 16.875f),
		new DataSeal("bg518u", "bg518u", 297f, -108f, 16.875f, 16.875f),
		new DataSeal("bg51b", "bg51b", -370f, -220f, 16.875f, 16.875f),
		new DataSeal("bg51c", "bg51c", 459f, -297f, 16.875f, 16.875f),
		new DataSeal("bg522", "bg522", 729f, -54f, 16.875f, 16.875f),
		new DataSeal("bg524", "bg524", -54f, 54f, 16.875f, 16.875f),
		new DataSeal("bg52d", "bg52d", -54f, 54f, 16.875f, 16.875f),
		new DataSeal("bg54d", "bg54d", 1149f, -38f, 16.875f, 16.875f),
		new DataSeal("bg510", "bg510", 128.5f, 216f, 16.875f, 16.875f),
		new DataSeal("bg585", "bg585", 143f, 30f, 16.875f, 16.875f),
		new DataSeal("bg554", "bg554", 290f, -283f, 16.875f, 16.875f),
		new DataSeal("bg55d", "bg55d", 231f, -150f, 16.875f, 16.875f),
		new DataSeal("bg57b", "bg57b", -490f, -210f, 16.875f, 16.875f),
		new DataSeal("bg57c", "bg57c", -486f, -216f, 16.875f, 16.875f),
		new DataSeal("bg564", "bg564", 351f, 26f, 16.875f, 16.875f),
		new DataSeal("bg565", "bg565", 324f, 80f, 16.875f, 16.875f),
		new DataSeal("bg566", "bg566", 351f, 26f, 16.875f, 16.875f),
		new DataSeal("bg567", "bg567", 270f, 134f, 16.875f, 16.875f),
		new DataSeal("bg568", "bg568", 297f, 80f, 16.875f, 16.875f),
		new DataSeal("bg569", "bg569", 216f, 188f, 16.875f, 16.875f),
		new DataSeal("bg56a", "bg56a", 243f, 134f, 16.875f, 16.875f),
		new DataSeal("bg56b", "bg56b", 162f, 242f, 16.875f, 16.875f),
		new DataSeal("bg56c", "bg56c", 189f, 188f, 16.875f, 16.875f),
		new DataSeal("bg56d", "bg56d", 108f, 296f, 16.875f, 16.875f),
		new DataSeal("bg56e", "bg56e", 135f, 242f, 16.875f, 16.875f),
		new DataSeal("bg56f", "bg56f", 54f, 350f, 16.875f, 16.875f),
		new DataSeal("bg570", "bg570", 81f, 296f, 16.875f, 16.875f),
		new DataSeal("bg571", "bg571", 0f, 404f, 16.875f, 16.875f),
		new DataSeal("bg572", "bg572", 27f, 350f, 16.875f, 16.875f),
		new DataSeal("bg573", "bg573", -54f, 458f, 16.875f, 16.875f),
		new DataSeal("bg574", "bg574", -27f, 404f, 16.875f, 16.875f),
		new DataSeal("bg57a", "bg57a", 243f, -108f, 16.875f, 16.875f)
	};

	private List<DataBG> gs1_bg_data_ = new List<DataBG>
	{
		new DataBG("bg000", string.Empty, "bg000t", 1u, 32768u),
		new DataBG("bg001", string.Empty, "bg001", 0u, 32768u),
		new DataBG("bg002", string.Empty, "bg002", 0u, 32768u),
		new DataBG("bg003", string.Empty, "bg003", 0u, 32768u),
		new DataBG("bg004", string.Empty, "bg004", 0u, 32768u),
		new DataBG("bg005", string.Empty, "bg005", 0u, 32768u),
		new DataBG("bg006", string.Empty, "bg006", 0u, 32768u),
		new DataBG("bg007", string.Empty, "bg007", 0u, 32768u),
		new DataBG("bg008", string.Empty, "bg008", 0u, 32768u),
		new DataBG("bg009", string.Empty, "bg009", 0u, 32768u),
		new DataBG("bg00a", string.Empty, "bg00a", 0u, 32768u),
		new DataBG("bg00b", string.Empty, "bg00b", 0u, 0u),
		new DataBG("bg00c", string.Empty, "bg00cb", 1u, 1u),
		new DataBG("bg00d", string.Empty, "bg00d", 0u, 32768u),
		new DataBG("bg00e", string.Empty, "bg00e", 0u, 32768u),
		new DataBG("bg00f", string.Empty, "bg00f", 0u, 32768u),
		new DataBG("bg010", string.Empty, "bg010", 0u, 32768u),
		new DataBG("bg011", string.Empty, "bg011", 0u, 32768u),
		new DataBG("bg012", string.Empty, "bg012", 0u, 32768u),
		new DataBG("bg013", string.Empty, "bg013", 0u, 63u),
		new DataBG("bg014", string.Empty, "bg014", 0u, 32768u),
		new DataBG("bg015", string.Empty, "bg015", 0u, 32768u),
		new DataBG("bg016", string.Empty, "bg016", 0u, 32768u),
		new DataBG("bg017", string.Empty, "bg017", 0u, 32768u),
		new DataBG("bg018", string.Empty, "bg018", 0u, 67u),
		new DataBG("bg019", string.Empty, "bg019", 0u, 32768u),
		new DataBG("bg01a", string.Empty, "bg01a", 0u, 32768u),
		new DataBG("bg01b", string.Empty, "bg01b", 0u, 32768u),
		new DataBG("bg01c", string.Empty, "bg01c", 0u, 32768u),
		new DataBG("bg01d", string.Empty, "bg01d", 0u, 32768u),
		new DataBG("bg01e", "bg01ew", "bg01e", 0u, 32768u),
		new DataBG("bg01f", string.Empty, "bg01f", 0u, 2u),
		new DataBG("bg020", string.Empty, "bg020", 0u, 32768u),
		new DataBG("bg021", string.Empty, "bg021", 0u, 32768u),
		new DataBG("bg022", string.Empty, "bg022", 0u, 32768u),
		new DataBG("bg023", string.Empty, "bg023", 0u, 32768u),
		new DataBG("bg024", string.Empty, "bg024", 0u, 32768u),
		new DataBG("bg025", string.Empty, "bg025", 0u, 32768u),
		new DataBG("bg026", string.Empty, "bg026", 0u, 32768u),
		new DataBG("bg027", string.Empty, "bg027", 0u, 32768u),
		new DataBG("bg028", string.Empty, "bg028", 0u, 32768u),
		new DataBG("bg029", string.Empty, "bg029", 0u, 32768u),
		new DataBG("bg02a", string.Empty, "bg02a", 0u, 32768u),
		new DataBG("bg02b", string.Empty, "bg02b", 0u, 32768u),
		new DataBG("bg02c", string.Empty, "bg02cb", 2u, 32768u),
		new DataBG("bg02d", string.Empty, "bg02d", 0u, 3u),
		new DataBG("bg02e", string.Empty, "bg02e", 0u, 4u),
		new DataBG("bg02f", string.Empty, "bg02f", 0u, 32768u),
		new DataBG("bg030", string.Empty, "bg030", 0u, 5u),
		new DataBG("bg031", string.Empty, "bg031", 0u, 32768u),
		new DataBG("bg032", string.Empty, "bg032", 0u, 32768u),
		new DataBG("bg033", string.Empty, "bg033", 0u, 32768u),
		new DataBG("bg034", string.Empty, "bg034", 0u, 32768u),
		new DataBG("bg035", string.Empty, "bg035", 0u, 32768u),
		new DataBG("bg036", string.Empty, "bg036", 0u, 32768u),
		new DataBG("bg037", string.Empty, "bg037", 0u, 32768u),
		new DataBG("bg038", string.Empty, "bg038", 0u, 32768u),
		new DataBG("bg039", string.Empty, "bg039", 0u, 32768u),
		new DataBG("bg03a", string.Empty, "bg03a", 0u, 64u),
		new DataBG("bg03b", string.Empty, "bg03b", 0u, 6u),
		new DataBG("bg03c", string.Empty, "bg03c", 0u, 32768u),
		new DataBG("bg03d", string.Empty, "bg03d", 0u, 7u),
		new DataBG("bg03e", string.Empty, "bg03e", 0u, 32768u),
		new DataBG("bg03f", string.Empty, "bg03f", 0u, 8u),
		new DataBG("bg040", string.Empty, "bg040", 0u, 32768u),
		new DataBG("bg041", string.Empty, "bg041", 0u, 32768u),
		new DataBG("bg042", string.Empty, "bg042", 0u, 32768u),
		new DataBG("bg043", string.Empty, "bg043", 0u, 32768u),
		new DataBG("bg044", string.Empty, "bg044", 0u, 32768u),
		new DataBG("bg045", string.Empty, "bg045", 3u, 32768u),
		new DataBG("bg046", string.Empty, "bg046", 0u, 9u),
		new DataBG("bg047", string.Empty, "bg048", 0u, 10u),
		new DataBG("bg048", string.Empty, "bg048", 0u, 11u),
		new DataBG("bg049", string.Empty, "bg049b", 2u, 61u),
		new DataBG("bg045", string.Empty, "bg045", 0u, 32768u),
		new DataBG("bg045", string.Empty, "bg045", 0u, 32768u),
		new DataBG("bg04c", string.Empty, "bg04c", 0u, 32768u),
		new DataBG("bg04d", string.Empty, "bg04d", 0u, 12u),
		new DataBG("bg04e", string.Empty, "bg04e", 0u, 62u),
		new DataBG("bg04f", string.Empty, "bg04f", 0u, 60u),
		new DataBG("bg050", string.Empty, "bg050", 0u, 32768u),
		new DataBG("bg051", string.Empty, "bg051", 0u, 32768u),
		new DataBG("bg052", string.Empty, "bg052", 0u, 13u),
		new DataBG("bg053", string.Empty, "bg053", 0u, 14u),
		new DataBG("bg054", string.Empty, "bg054", 0u, 15u),
		new DataBG("bg055", string.Empty, "bg055", 0u, 32768u),
		new DataBG("bg056", string.Empty, "bg056", 0u, 32768u),
		new DataBG("bg057", string.Empty, "bg057", 0u, 32768u),
		new DataBG("bg058", string.Empty, "bg058", 0u, 32768u),
		new DataBG("bg059", string.Empty, "bg059", 0u, 16u),
		new DataBG("bg05a", string.Empty, "bg05a", 0u, 32768u),
		new DataBG("bg05b", string.Empty, "bg05b", 0u, 32768u),
		new DataBG("bg05c", string.Empty, "bg05c", 0u, 32768u),
		new DataBG("bg05d", string.Empty, "bg05d", 0u, 32768u),
		new DataBG("bg05e", string.Empty, "bg05e", 0u, 32768u),
		new DataBG("bg05f", string.Empty, "bg05f", 0u, 69u),
		new DataBG("bg060", string.Empty, "bg060", 0u, 68u),
		new DataBG("bg061", string.Empty, "bg061", 0u, 32768u),
		new DataBG("bg062", string.Empty, "bg062", 0u, 32768u),
		new DataBG("bg063", string.Empty, "bg063", 0u, 32768u),
		new DataBG("bg064", string.Empty, "bg064", 0u, 32768u),
		new DataBG("bg065", string.Empty, "bg065", 0u, 32768u),
		new DataBG("bg066", string.Empty, "bg066", 0u, 32768u),
		new DataBG("bg067", string.Empty, "bg067", 0u, 32768u),
		new DataBG("bg068", string.Empty, "bg068", 0u, 32768u),
		new DataBG("bg069", string.Empty, "bg069", 0u, 17u),
		new DataBG("bg06a", string.Empty, "bg06a", 0u, 32768u),
		new DataBG("bg06b", string.Empty, "bg06b", 0u, 32768u),
		new DataBG("bg06c", string.Empty, "bg06c", 0u, 32768u),
		new DataBG("bg06d", string.Empty, "bg06d", 0u, 32768u),
		new DataBG(string.Empty, string.Empty, string.Empty, 0u, 32768u),
		new DataBG(string.Empty, string.Empty, string.Empty, 0u, 32768u),
		new DataBG("bg070", string.Empty, "bg070", 0u, 32768u),
		new DataBG("bg072", string.Empty, "bg072", 0u, 18u),
		new DataBG("bg073", string.Empty, "bg073", 0u, 32768u),
		new DataBG("bg074", string.Empty, "bg074a", 2u, 32768u),
		new DataBG("bg075", string.Empty, "bg075c", 2u, 32768u),
		new DataBG("bg076", string.Empty, "bg076a", 2u, 19u),
		new DataBG("bg077", string.Empty, "bg077", 0u, 32768u),
		new DataBG("bg084", string.Empty, "bg084", 2u, 20u),
		new DataBG("bg085", string.Empty, "bg085", 0u, 32768u),
		new DataBG("bg086", string.Empty, "bg086", 0u, 32768u),
		new DataBG("bg087", string.Empty, "bg087", 0u, 32768u),
		new DataBG("bg088", string.Empty, "bg088", 0u, 32768u),
		new DataBG("bg089", string.Empty, "bg089", 0u, 21u),
		new DataBG("bg08a", string.Empty, "bg08a", 0u, 22u),
		new DataBG("bg08b", string.Empty, "bg08b", 0u, 32768u),
		new DataBG("bg08b", string.Empty, "bg08b", 0u, 32768u),
		new DataBG("bg0a1", string.Empty, "bg0a1", 0u, 32768u),
		new DataBG("bg0a2", string.Empty, "bg0a2", 0u, 32768u),
		new DataBG("bg0a3", string.Empty, "bg0a3", 0u, 32768u),
		new DataBG("bg0a4", string.Empty, "bg0a4", 0u, 32768u),
		new DataBG("bg0a9", string.Empty, "bg0a9", 0u, 32768u),
		new DataBG("bg0aa", string.Empty, "bg0aa", 0u, 32768u),
		new DataBG("bg0ab", string.Empty, "bg0ab", 0u, 32768u),
		new DataBG("bg0ac", string.Empty, "bg0ac", 0u, 32768u),
		new DataBG("bg0ad", string.Empty, "bg0ad", 0u, 32768u),
		new DataBG("bg0ae", string.Empty, "bg0ae", 0u, 32768u),
		new DataBG("bg015", string.Empty, "bg015", 0u, 32768u),
		new DataBG("bg0b2", string.Empty, "bg0b2", 0u, 32768u),
		new DataBG("bg0be", string.Empty, "bg0be", 0u, 32768u),
		new DataBG("bg0bf", string.Empty, "bg0bf", 0u, 24u),
		new DataBG("bg0c5", string.Empty, "bg0c5", 0u, 27u),
		new DataBG("bg0c6", string.Empty, "bg0c6", 0u, 32768u),
		new DataBG("bg0c7", string.Empty, "bg0c7", 0u, 28u),
		new DataBG("bg0c8", string.Empty, "bg0c8", 0u, 32768u),
		new DataBG("bg0c9", string.Empty, "bg0c9", 0u, 32768u),
		new DataBG("bg0ca", string.Empty, "bg0ca", 0u, 32768u),
		new DataBG("bg0cb", string.Empty, "bg0cb", 0u, 29u),
		new DataBG("bg0cc", string.Empty, "bg0cc", 0u, 30u),
		new DataBG("bg0cd", string.Empty, "bg0cd", 0u, 31u),
		new DataBG("bg0ce", string.Empty, "bg0ce", 0u, 32u),
		new DataBG("bg0cf", string.Empty, "bg0cf", 0u, 33u),
		new DataBG("bg0d4", string.Empty, "bg0d4", 0u, 32768u),
		new DataBG("bg0d5", string.Empty, "bg0d5", 0u, 32768u),
		new DataBG("bg0d6", string.Empty, "bg0d6", 0u, 32768u),
		new DataBG("bg0d7", string.Empty, "bg0d7", 0u, 32768u),
		new DataBG("bg0d8", string.Empty, "bg0d8", 3u, 32768u),
		new DataBG("bg0da", string.Empty, "bg0da", 0u, 32768u),
		new DataBG("bg0db", string.Empty, "bg0db", 0u, 32768u),
		new DataBG("bg0e0", string.Empty, "bg0e0", 0u, 34u),
		new DataBG("bg0e3", string.Empty, "bg0e3", 0u, 36u),
		new DataBG("bg0e4", string.Empty, "bg0e4", 0u, 37u),
		new DataBG("bg0e5", string.Empty, "bg0e5", 0u, 38u),
		new DataBG("bg0e6", string.Empty, "bg0e6", 0u, 39u),
		new DataBG("bg0e7", string.Empty, "bg0e7", 0u, 40u),
		new DataBG("bg0e8", string.Empty, "bg0e8", 0u, 41u),
		new DataBG("bg0e9", string.Empty, "bg0e9", 0u, 65u),
		new DataBG("bg0ea", string.Empty, "bg0ea", 0u, 32768u),
		new DataBG("bg0eb", string.Empty, "bg0eb", 0u, 32768u),
		new DataBG("bg0ec", string.Empty, "bg0ec", 0u, 42u),
		new DataBG("bg0ed", string.Empty, "bg0ed", 0u, 43u),
		new DataBG("bg0ee", string.Empty, "bg0ee", 0u, 32768u),
		new DataBG("bg0ef", string.Empty, "bg0ef", 0u, 32768u),
		new DataBG("bg0f0", string.Empty, "bg0f0", 0u, 32768u),
		new DataBG("bg0f1", string.Empty, "bg0f1", 0u, 32768u),
		new DataBG("bg0f2", string.Empty, "bg0f2", 0u, 32768u),
		new DataBG("bg0f3", string.Empty, "bg0f3", 0u, 32768u),
		new DataBG("bg0f4", string.Empty, "bg0f4", 0u, 32768u),
		new DataBG("bg09c", string.Empty, "bg09c", 0u, 32768u),
		new DataBG("bg09d", string.Empty, "bg09d", 0u, 23u),
		new DataBG("bg0b3", string.Empty, "bg0b3", 0u, 32768u),
		new DataBG("bg0b4", string.Empty, "bg0b4", 0u, 32768u),
		new DataBG("bg0b5", string.Empty, "bg0b5", 0u, 32768u),
		new DataBG("bg0b6", string.Empty, "bg0b6", 0u, 32768u),
		new DataBG("bg0b7", string.Empty, "bg0b7", 0u, 32768u),
		new DataBG("bg0b8", string.Empty, "bg0b8", 0u, 32768u),
		new DataBG("bg0b9", string.Empty, "bg0b9", 0u, 32768u),
		new DataBG("bg0ba", string.Empty, "bg0ba", 0u, 32768u),
		new DataBG("bg0bb", string.Empty, "bg0bb", 0u, 32768u),
		new DataBG("bg0bc", string.Empty, "bg0bc", 0u, 32768u),
		new DataBG("bg0bd", string.Empty, "bg0bd", 0u, 32768u),
		new DataBG("bg100", string.Empty, "bg100", 0u, 44u),
		new DataBG("bg101", string.Empty, "bg101", 0u, 45u),
		new DataBG("bg103", string.Empty, "bg103", 0u, 46u),
		new DataBG("bg104", string.Empty, "bg104", 0u, 32768u),
		new DataBG("bg105", string.Empty, "bg105", 0u, 32768u),
		new DataBG("bg0c0", string.Empty, "bg0c0", 0u, 25u),
		new DataBG("bg0c1", string.Empty, "bg0c1", 0u, 26u),
		new DataBG("bg0c3", string.Empty, "bg0c3", 0u, 32768u),
		new DataBG("bg0dc", string.Empty, "bg0dc", 0u, 59u),
		new DataBG("bg0fa", string.Empty, "bg0fa", 2u, 32768u),
		new DataBG("bg102", string.Empty, "bg102", 0u, 32768u),
		new DataBG("bg106", string.Empty, "bg106", 0u, 32768u),
		new DataBG("bg107", string.Empty, "bg107", 0u, 47u),
		new DataBG("bg109", string.Empty, "bg109", 0u, 32768u),
		new DataBG("bg10e", string.Empty, "bg10e", 0u, 32768u),
		new DataBG("bg10f", string.Empty, "bg10f", 0u, 32768u),
		new DataBG("bg110", string.Empty, "bg110", 0u, 32768u),
		new DataBG("bg111", string.Empty, "bg111", 0u, 32768u),
		new DataBG("bg112", string.Empty, "bg112", 0u, 32768u),
		new DataBG("bg0c2", string.Empty, "bg0c2", 0u, 32768u),
		new DataBG("bg0e1", string.Empty, "bg0e1", 0u, 35u),
		new DataBG("bg114", string.Empty, "bg114", 0u, 32768u),
		new DataBG("bg115", string.Empty, "bg115", 0u, 49u),
		new DataBG("bg116", string.Empty, "bg116", 0u, 50u),
		new DataBG("bg117", string.Empty, "bg117", 0u, 51u),
		new DataBG("bg123", string.Empty, "bg123", 0u, 52u),
		new DataBG("bg124", string.Empty, "bg124", 0u, 32768u),
		new DataBG("bg125", string.Empty, "bg125", 0u, 32768u),
		new DataBG("bgfff", string.Empty, "bgfff", 0u, 32768u),
		new DataBG("bg10d", string.Empty, "bg10d", 0u, 48u),
		new DataBG("bg122", string.Empty, "bg122", 0u, 59u),
		new DataBG("bg10a", string.Empty, "bg10a", 0u, 32768u),
		new DataBG("bg10b", string.Empty, "bg10b", 0u, 32768u),
		new DataBG("bg10c", string.Empty, "bg10c", 0u, 66u),
		new DataBG("bg126", string.Empty, "bg126", 0u, 53u),
		new DataBG("bg127", string.Empty, "bg127", 0u, 32768u),
		new DataBG("bg128", string.Empty, "bg128", 0u, 32768u),
		new DataBG("bg128f", string.Empty, "bg128f", 0u, 32768u),
		new DataBG("bg108", string.Empty, "bg108", 0u, 32768u),
		new DataBG("bg135", string.Empty, "bg135", 0u, 32768u),
		new DataBG("bg139", string.Empty, "bg139", 0u, 32768u),
		new DataBG("bg13a", string.Empty, "bg13a", 0u, 55u),
		new DataBG("bg13b", string.Empty, "bg13b", 0u, 32768u),
		new DataBG("bg134", string.Empty, "bg134", 0u, 54u),
		new DataBG("bg13c", string.Empty, "bg13c", 0u, 32768u),
		new DataBG("bg13d", string.Empty, "bg13d", 0u, 32768u),
		new DataBG("bg13e", string.Empty, "bg13e", 0u, 32768u),
		new DataBG("bg13f", string.Empty, "bg13f", 0u, 56u),
		new DataBG("bg121", string.Empty, "bg121", 0u, 32768u),
		new DataBG("bgffe", string.Empty, "bgffe", 0u, 32768u),
		new DataBG("bg140", string.Empty, "bg140", 0u, 57u),
		new DataBG("title", string.Empty, "title", 0u, 58u),
		new DataBG("bg080", string.Empty, "bg080", 0u, 32768u),
		new DataBG("frame03", string.Empty, "frame03", 0u, 32768u),
		new DataBG("frame04", string.Empty, "frame04", 0u, 32768u),
		new DataBG("bg08c", string.Empty, "bg08c", 0u, 32768u),
		new DataBG("title_back", string.Empty, "title_back", 0u, 32768u)
	};

	private List<DataBG> gs2_bg_data_ = new List<DataBG>
	{
		new DataBG("bg044", string.Empty, "bg044", 0u, 32768u),
		new DataBG("bg02f", string.Empty, "bg02f", 0u, 32768u),
		new DataBG("bg06d", string.Empty, "bg06d", 0u, 32768u),
		new DataBG("bg000", string.Empty, "bg000", 0u, 32768u),
		new DataBG("bg001", string.Empty, "bg001", 0u, 32768u),
		new DataBG("bg002", string.Empty, "bg002", 0u, 32768u),
		new DataBG("bg003", string.Empty, "bg003", 0u, 32768u),
		new DataBG("bg004", string.Empty, "bg004", 0u, 32768u),
		new DataBG("bg005", string.Empty, "bg005", 0u, 32768u),
		new DataBG("bg006", string.Empty, "bg006", 0u, 32768u),
		new DataBG("bg007", string.Empty, "bg007", 0u, 32768u),
		new DataBG("bg008", string.Empty, "bg008", 0u, 32768u),
		new DataBG("bg009", string.Empty, "bg009", 0u, 32768u),
		new DataBG("bg00a", string.Empty, "bg00a", 0u, 32768u),
		new DataBG("bg040", string.Empty, "bg040", 0u, 32768u),
		new DataBG("bg041", string.Empty, "bg041", 0u, 32768u),
		new DataBG("bg00d", string.Empty, "bg00d", 0u, 32768u),
		new DataBG("bg010", "bg010w", "bg010", 0u, 32768u),
		new DataBG("bg011", string.Empty, "bg011", 0u, 32768u),
		new DataBG("bg012", string.Empty, "bg012", 0u, 39u),
		new DataBG("bg013", string.Empty, "bg013", 0u, 32768u),
		new DataBG("bg100", string.Empty, "bg100", 0u, 32768u),
		new DataBG("bg101", string.Empty, "bg101", 0u, 0u),
		new DataBG("bg102", string.Empty, "bg102", 0u, 1u),
		new DataBG("bg103", string.Empty, "bg103", 0u, 32768u),
		new DataBG("bg104", string.Empty, "bg104", 2u, 46u),
		new DataBG("bg105", string.Empty, "bg105", 0u, 32768u),
		new DataBG("bg106", string.Empty, "bg106", 0u, 32768u),
		new DataBG("bg200", string.Empty, "bg200", 0u, 43u),
		new DataBG("bg201", string.Empty, "bg201", 0u, 44u),
		new DataBG("bg202", string.Empty, "bg202a", 2u, 32768u),
		new DataBG("bg204", string.Empty, "bg204", 0u, 32768u),
		new DataBG("bg205", string.Empty, "bg206", 0u, 32768u),
		new DataBG("bg206", string.Empty, "bg206", 0u, 32768u),
		new DataBG("bg207", string.Empty, "bg207", 0u, 2u),
		new DataBG("bg208", string.Empty, "bg208", 0u, 3u),
		new DataBG("bg209", string.Empty, "bg209", 0u, 32768u),
		new DataBG("bg20a", string.Empty, "bg20a", 0u, 32768u),
		new DataBG("bg20b", string.Empty, "bg20b", 0u, 32768u),
		new DataBG("bg20c", string.Empty, "bg20c", 0u, 4u),
		new DataBG("bg20d", string.Empty, "bg20d", 0u, 5u),
		new DataBG("bg20e", string.Empty, "bg20e", 0u, 6u),
		new DataBG("bg20f", string.Empty, "bg20f", 0u, 32768u),
		new DataBG("bg210", string.Empty, "bg210", 0u, 7u),
		new DataBG("bg211", string.Empty, "bg211", 0u, 32768u),
		new DataBG("bg212", string.Empty, "bg212", 0u, 32768u),
		new DataBG("bg213", string.Empty, "bg213", 0u, 32768u),
		new DataBG("bg214", string.Empty, "bg214", 0u, 32768u),
		new DataBG("bg215", string.Empty, "bg215", 0u, 32768u),
		new DataBG("bg216", string.Empty, "bg216", 0u, 32768u),
		new DataBG("bg217", string.Empty, "bg217", 0u, 32768u),
		new DataBG("bg218", string.Empty, "bg218", 0u, 32768u),
		new DataBG("bg219", string.Empty, "bg219", 0u, 8u),
		new DataBG("bg21a", string.Empty, "bg21a", 0u, 32768u),
		new DataBG("bg21b", string.Empty, "bg21b", 0u, 32768u),
		new DataBG("bg21c", string.Empty, "bg21c", 0u, 32768u),
		new DataBG("bg21d", string.Empty, "bg21d", 0u, 32768u),
		new DataBG("bg21e", string.Empty, "bg21e", 0u, 32768u),
		new DataBG("bg21f", string.Empty, "bg21f", 0u, 32768u),
		new DataBG("bg300", string.Empty, "bg300", 0u, 45u),
		new DataBG("bg301", string.Empty, "bg301", 0u, 9u),
		new DataBG("bg302", string.Empty, "bg302", 0u, 40u),
		new DataBG("bg303", string.Empty, "bg303", 0u, 10u),
		new DataBG("bg304", string.Empty, "bg304", 0u, 11u),
		new DataBG("bg305", string.Empty, "bg305", 0u, 32768u),
		new DataBG("bg306", string.Empty, "bg306", 0u, 41u),
		new DataBG("bg307", string.Empty, "bg307", 0u, 32768u),
		new DataBG("bg308", string.Empty, "bg308", 0u, 32768u),
		new DataBG("bg309", string.Empty, "bg309", 0u, 32768u),
		new DataBG("bg30a", string.Empty, "bg30a", 1u, 12u),
		new DataBG("bg30b", string.Empty, "bg30b", 0u, 32768u),
		new DataBG("bg30c", string.Empty, "bg30c", 0u, 13u),
		new DataBG("bg30d", string.Empty, "bg30d", 0u, 14u),
		new DataBG("bg30e", string.Empty, "bg30e", 0u, 32768u),
		new DataBG("bg30f", string.Empty, "bg30f", 0u, 15u),
		new DataBG("bg310", string.Empty, "bg310", 0u, 16u),
		new DataBG("bg311", string.Empty, "bg311", 0u, 32768u),
		new DataBG("bg312", string.Empty, "bg312", 0u, 32768u),
		new DataBG("bg313", string.Empty, "bg313", 0u, 32768u),
		new DataBG("bg314", string.Empty, "bg314", 0u, 32768u),
		new DataBG("bg315", string.Empty, "bg315", 0u, 32768u),
		new DataBG("bg316", string.Empty, "bg316", 0u, 17u),
		new DataBG("bg317", string.Empty, "bg317", 0u, 32768u),
		new DataBG("bg318", string.Empty, "bg318", 0u, 32768u),
		new DataBG("bg319", string.Empty, "bg319", 0u, 32768u),
		new DataBG("bg31a", string.Empty, "bg31a", 0u, 18u),
		new DataBG("bg31b", string.Empty, "bg31b", 0u, 32768u),
		new DataBG("bg31c", string.Empty, "bg31c", 1u, 32768u),
		new DataBG("bg400", string.Empty, "bg400", 0u, 32768u),
		new DataBG("bg401", string.Empty, "bg401", 0u, 19u),
		new DataBG("bg402", string.Empty, "bg402", 0u, 32768u),
		new DataBG("bg403", string.Empty, "bg403a", 2u, 32768u),
		new DataBG("bg404", string.Empty, "bg404", 0u, 32768u),
		new DataBG("bg405", string.Empty, "bg405", 0u, 20u),
		new DataBG("bg406", string.Empty, "bg406", 0u, 32768u),
		new DataBG("bg407", string.Empty, "bg407", 0u, 32768u),
		new DataBG("bg408", string.Empty, "bg408", 0u, 47u),
		new DataBG("bg410", string.Empty, "bg410", 0u, 32768u),
		new DataBG("bg411", string.Empty, "bg411", 0u, 22u),
		new DataBG("bg412", string.Empty, "bg412", 0u, 32768u),
		new DataBG("bg413", string.Empty, "bg413", 0u, 32768u),
		new DataBG("bg414", string.Empty, "bg414", 0u, 23u),
		new DataBG("bg415", string.Empty, "bg415", 0u, 32768u),
		new DataBG("bg416", string.Empty, "bg416", 0u, 24u),
		new DataBG("bg417", string.Empty, "bg417", 0u, 42u),
		new DataBG("bg418", string.Empty, "bg418", 0u, 32768u),
		new DataBG("bg419", string.Empty, "bg419", 0u, 32768u),
		new DataBG("bg41a", string.Empty, "bg41a", 0u, 32768u),
		new DataBG("bg41b", string.Empty, "bg41b", 0u, 32768u),
		new DataBG("bg41c", string.Empty, "bg41c", 0u, 25u),
		new DataBG("bg41d", string.Empty, "bg41d", 0u, 32768u),
		new DataBG("bg41e", string.Empty, "bg41e", 0u, 26u),
		new DataBG("bg41f", string.Empty, "bg41f", 0u, 32768u),
		new DataBG("bg420", string.Empty, "bg420", 0u, 27u),
		new DataBG("bg421", string.Empty, "bg421", 0u, 32768u),
		new DataBG("bg422", string.Empty, "bg422", 0u, 32768u),
		new DataBG("bg423", string.Empty, "bg423", 0u, 28u),
		new DataBG("bg424", string.Empty, "bg424", 0u, 29u),
		new DataBG("bg425", string.Empty, "bg425", 0u, 30u),
		new DataBG("bg426", string.Empty, "bg426", 0u, 48u),
		new DataBG("bg40a", string.Empty, "bg40a", 3u, 32768u),
		new DataBG("bg40a", string.Empty, "bg40a", 1u, 32768u),
		new DataBG("bg40a", string.Empty, "bg40a", 0u, 32768u),
		new DataBG("bg40c", string.Empty, "bg40c", 0u, 32768u),
		new DataBG("bg40d", string.Empty, "bg40d", 0u, 21u),
		new DataBG("bg40e", string.Empty, "bg40e", 0u, 21u),
		new DataBG("bg211", string.Empty, "bg211", 0u, 32768u),
		new DataBG("bg700", string.Empty, "bg700", 0u, 31u),
		new DataBG("bg701", string.Empty, "bg701", 0u, 32u),
		new DataBG("bg702", string.Empty, "bg702", 0u, 33u),
		new DataBG("bg703", string.Empty, "bg703", 0u, 34u),
		new DataBG("bg13f", string.Empty, "bg13f", 0u, 36u),
		new DataBG("bg043", string.Empty, "bg043", 0u, 32768u),
		new DataBG("bg10e", string.Empty, "bg10e", 0u, 32768u),
		new DataBG("bg10f", string.Empty, "bg10f", 0u, 32768u),
		new DataBG("bg110", string.Empty, "bg110", 0u, 32768u),
		new DataBG("bg111", string.Empty, "bg111", 0u, 32768u),
		new DataBG("bg127", string.Empty, "bg127", 0u, 32768u),
		new DataBG("bg070", string.Empty, "bg070", 0u, 32768u),
		new DataBG("bg705u", string.Empty, "bg705u", 0u, 35u),
		new DataBG("bg124", string.Empty, "bg124", 0u, 32768u),
		new DataBG("bg125", string.Empty, "bg125", 0u, 32768u),
		new DataBG("bg141", string.Empty, "bg141", 0u, 37u),
		new DataBG("bg090", string.Empty, "bg090", 0u, 32768u),
		new DataBG("title", string.Empty, "title", 0u, 38u),
		new DataBG("bg080", string.Empty, "bg080", 0u, 32768u),
		new DataBG("frame03", string.Empty, "frame03", 0u, 32768u),
		new DataBG("title_back", string.Empty, "title_back", 0u, 32768u),
		new DataBG("bg10e", string.Empty, "bg10e", 0u, 32768u),
		new DataBG("bg10f", string.Empty, "bg10f", 0u, 32768u),
		new DataBG("bg110", string.Empty, "bg110", 0u, 32768u),
		new DataBG("bg111", string.Empty, "bg111", 0u, 32768u),
		new DataBG("bg10e", string.Empty, "bg10e", 0u, 32768u),
		new DataBG("bg10f", string.Empty, "bg10f", 0u, 32768u),
		new DataBG("bg110", string.Empty, "bg110", 0u, 32768u),
		new DataBG("bg111", string.Empty, "bg111", 0u, 32768u),
		new DataBG("bg10e", string.Empty, "bg10e", 0u, 32768u),
		new DataBG("bg10f", string.Empty, "bg10f", 0u, 32768u),
		new DataBG("bg110", string.Empty, "bg110", 0u, 32768u),
		new DataBG("bg111", string.Empty, "bg111", 0u, 32768u),
		new DataBG("bg10e", string.Empty, "bg10e", 0u, 32768u),
		new DataBG("bg10f", string.Empty, "bg10f", 0u, 32768u),
		new DataBG("bg110", string.Empty, "bg110", 0u, 32768u),
		new DataBG("bg111", string.Empty, "bg111", 0u, 32768u)
	};

	private List<DataBG> gs3_bg_data_ = new List<DataBG>
	{
		new DataBG("bg044", string.Empty, "bg044", 0u, 32768u),
		new DataBG("bg02f", string.Empty, "bg02f", 0u, 32768u),
		new DataBG("bg06d", string.Empty, "bg06d", 0u, 32768u),
		new DataBG("bg000", string.Empty, "bg000", 0u, 32768u),
		new DataBG("bg001", string.Empty, "bg001", 0u, 32768u),
		new DataBG("bg002", string.Empty, "bg002", 0u, 32768u),
		new DataBG("bg003", string.Empty, "bg003", 0u, 32768u),
		new DataBG("bg004", string.Empty, "bg004", 0u, 32768u),
		new DataBG("bg005", string.Empty, "bg005", 0u, 32768u),
		new DataBG("bg006", string.Empty, "bg006", 0u, 32768u),
		new DataBG("bg008", string.Empty, "bg008", 0u, 32768u),
		new DataBG("bg009", string.Empty, "bg009", 0u, 32768u),
		new DataBG("bg00b", string.Empty, "bg00b", 0u, 32768u),
		new DataBG("bg00c", string.Empty, "bg00c", 0u, 32768u),
		new DataBG("bg00d", string.Empty, "bg00d", 0u, 32768u),
		new DataBG("bg00e", string.Empty, "bg00e", 0u, 32768u),
		new DataBG("bg00f", string.Empty, "bg00f", 0u, 32768u),
		new DataBG("bg010", string.Empty, "bg010", 0u, 32768u),
		new DataBG("bg013", "bg013w", "bg013", 0u, 32768u),
		new DataBG("bg014", string.Empty, "bg014", 0u, 32768u),
		new DataBG("bg015", string.Empty, "bg015", 0u, 0u),
		new DataBG("bg200", string.Empty, "bg200", 0u, 1u),
		new DataBG("bg201", string.Empty, "bg201", 2u, 2u),
		new DataBG("bg201", string.Empty, "bg201", 2u, 2u),
		new DataBG("bg202", string.Empty, "bg202", 0u, 32768u),
		new DataBG("bg203", string.Empty, "bg203", 0u, 32768u),
		new DataBG("bg204", string.Empty, "bg204", 0u, 60u),
		new DataBG("bg204", string.Empty, "bg204", 0u, 60u),
		new DataBG("bg205", string.Empty, "bg205", 0u, 61u),
		new DataBG("bg300", string.Empty, "bg300", 1u, 3u),
		new DataBG("bg300", string.Empty, "bg300", 2u, 3u),
		new DataBG("bg301", string.Empty, "bg301", 0u, 32768u),
		new DataBG("bg302", string.Empty, "bg302", 0u, 4u),
		new DataBG("bg303", string.Empty, "bg303", 0u, 32768u),
		new DataBG("bg304", string.Empty, "bg304", 0u, 5u),
		new DataBG("bg500", string.Empty, "bg500", 0u, 6u),
		new DataBG("bg500", string.Empty, "bg500", 0u, 6u),
		new DataBG("bg501", string.Empty, "bg501", 0u, 32768u),
		new DataBG("bg501", string.Empty, "bg501", 0u, 32768u),
		new DataBG("bg502", string.Empty, "bg502", 0u, 32768u),
		new DataBG("bg503", string.Empty, "bg503", 0u, 7u),
		new DataBG("bg504", string.Empty, "bg504", 0u, 8u),
		new DataBG("bg504", string.Empty, "bg504", 0u, 8u),
		new DataBG("bg505", string.Empty, "bg505", 0u, 9u),
		new DataBG("bg506", string.Empty, "bg506", 0u, 10u),
		new DataBG("bg507", string.Empty, "bg507", 1u, 11u),
		new DataBG("bg507", string.Empty, "bg507", 2u, 11u),
		new DataBG("bg507", string.Empty, "bg507", 1u, 11u),
		new DataBG("bg507", string.Empty, "bg507", 2u, 11u),
		new DataBG("bg509", string.Empty, "bg509", 0u, 32768u),
		new DataBG("bg509", string.Empty, "bg509", 0u, 32768u),
		new DataBG("bg509", string.Empty, "bg509", 0u, 32768u),
		new DataBG("bg509", string.Empty, "bg509", 0u, 32768u),
		new DataBG("bg50a", string.Empty, "bg50a", 0u, 12u),
		new DataBG("bg50a", string.Empty, "bg50a", 0u, 12u),
		new DataBG("bg100", string.Empty, "bg100", 0u, 32768u),
		new DataBG("bg101", string.Empty, "bg101", 0u, 32768u),
		new DataBG("bg102", string.Empty, "bg102", 0u, 32768u),
		new DataBG("bg105", string.Empty, "bg105", 0u, 32768u),
		new DataBG("bg106", string.Empty, "bg106", 0u, 13u),
		new DataBG("bg107", string.Empty, "bg107", 0u, 32768u),
		new DataBG("bg108", string.Empty, "bg108", 0u, 32768u),
		new DataBG("bg109", string.Empty, "bg109", 0u, 14u),
		new DataBG("bg10a", string.Empty, "bg10a", 0u, 15u),
		new DataBG("bg10b", string.Empty, "bg10b", 0u, 16u),
		new DataBG("bg21b", string.Empty, "bg21b", 0u, 17u),
		new DataBG("bg21c", string.Empty, "bg21c", 0u, 32768u),
		new DataBG("bg21d", string.Empty, "bg21d", 0u, 58u),
		new DataBG("bg21e", string.Empty, "bg21e", 0u, 18u),
		new DataBG("bg21f", string.Empty, "bg21f", 0u, 32768u),
		new DataBG("bg220", string.Empty, "bg220", 0u, 19u),
		new DataBG("bg221", string.Empty, "bg221", 0u, 32768u),
		new DataBG("bg211", "bg216", "bg211", 0u, 32768u),
		new DataBG("bg212", "bg211", "bg212", 0u, 53u),
		new DataBG("bg213", string.Empty, "bg213", 0u, 20u),
		new DataBG("bg214", string.Empty, "bg214", 0u, 32768u),
		new DataBG("bg215", string.Empty, "bg215", 0u, 32768u),
		new DataBG("bg216", string.Empty, "bg216", 0u, 32768u),
		new DataBG("bg222", string.Empty, "bg222", 0u, 32768u),
		new DataBG("bg223", string.Empty, "bg223", 0u, 32768u),
		new DataBG("bg224", string.Empty, "bg224", 0u, 21u),
		new DataBG("bg225", string.Empty, "bg225", 0u, 22u),
		new DataBG("bg226", string.Empty, "bg226", 0u, 32768u),
		new DataBG("bg227", string.Empty, "bg227", 0u, 23u),
		new DataBG("bg228", string.Empty, "bg228", 0u, 54u),
		new DataBG("bg229", string.Empty, "bg229", 0u, 32768u),
		new DataBG("bg22a", string.Empty, "bg22a", 0u, 24u),
		new DataBG("bg206", string.Empty, "bg206", 0u, 25u),
		new DataBG("bg207", string.Empty, "bg207", 0u, 26u),
		new DataBG("bg21a", string.Empty, "bg21a", 0u, 27u),
		new DataBG("bg308", "bg308", "bg308", 1u, 32768u),
		new DataBG("bg30b", string.Empty, "bg30b", 0u, 32768u),
		new DataBG("bg30c", "bg30b", "bg30c", 0u, 32768u),
		new DataBG("bg30f", string.Empty, "bg30f", 0u, 28u),
		new DataBG("bg310", string.Empty, "bg310", 0u, 32768u),
		new DataBG("bg310", string.Empty, "bg310", 0u, 32768u),
		new DataBG("bg311", string.Empty, "bg311", 0u, 32768u),
		new DataBG("bg312", string.Empty, "bg312", 0u, 29u),
		new DataBG("bg313", string.Empty, "bg313", 0u, 30u),
		new DataBG("bg314", string.Empty, "bg314", 0u, 32768u),
		new DataBG("bg315", string.Empty, "bg315", 0u, 32768u),
		new DataBG("bg316", string.Empty, "bg316", 0u, 32768u),
		new DataBG("bg317", string.Empty, "bg317", 0u, 31u),
		new DataBG("bg318", string.Empty, "bg318", 0u, 32u),
		new DataBG("bg319", string.Empty, "bg319", 0u, 32768u),
		new DataBG("bg31a", string.Empty, "bg31a", 0u, 55u),
		new DataBG("bg31b", string.Empty, "bg31b", 0u, 56u),
		new DataBG("bg400", string.Empty, "bg400", 0u, 32768u),
		new DataBG("bg401", string.Empty, "bg401", 0u, 32768u),
		new DataBG("bg402", string.Empty, "bg402", 0u, 32768u),
		new DataBG("bg403", string.Empty, "bg403", 0u, 32768u),
		new DataBG("bg404", string.Empty, "bg404", 0u, 33u),
		new DataBG("bg405", string.Empty, "bg405", 0u, 34u),
		new DataBG("bg405", string.Empty, "bg405", 0u, 34u),
		new DataBG("bg405", string.Empty, "bg405", 0u, 34u),
		new DataBG("bg405", string.Empty, "bg405", 0u, 34u),
		new DataBG("bg405", string.Empty, "bg405", 0u, 34u),
		new DataBG("bg406", string.Empty, "bg406", 0u, 32768u),
		new DataBG("bg407", string.Empty, "bg407", 0u, 32768u),
		new DataBG("bg408", string.Empty, "bg408", 0u, 32768u),
		new DataBG("bg50b", string.Empty, "bg50b", 0u, 35u),
		new DataBG("bg50c", string.Empty, "bg50c", 4u, 32768u),
		new DataBG("bg50d", string.Empty, "bg50d", 0u, 36u),
		new DataBG("bg50e", string.Empty, "bg50e", 0u, 37u),
		new DataBG("bg50f", string.Empty, "bg50f", 0u, 38u),
		new DataBG("bg50f", string.Empty, "bg50f", 0u, 38u),
		new DataBG("bg50f", string.Empty, "bg50f", 0u, 38u),
		new DataBG("bg50f", string.Empty, "bg50f", 0u, 38u),
		new DataBG("bg50f", string.Empty, "bg50f", 0u, 38u),
		new DataBG("bg50f", string.Empty, "bg50f", 0u, 38u),
		new DataBG("bg53a", string.Empty, "bg53a", 0u, 39u),
		new DataBG("bg53b", string.Empty, "bg53b", 0u, 40u),
		new DataBG("bg53c", string.Empty, "bg53c", 0u, 41u),
		new DataBG("bg53d", string.Empty, "bg53d", 0u, 42u),
		new DataBG("bg53e", string.Empty, "bg53e", 0u, 32768u),
		new DataBG("bg53f", string.Empty, "bg53f", 0u, 43u),
		new DataBG("bg540", string.Empty, "bg540", 0u, 32768u),
		new DataBG("bg541", string.Empty, "bg541", 0u, 32768u),
		new DataBG("bg542", string.Empty, "bg542", 0u, 32768u),
		new DataBG("bg543", string.Empty, "bg543", 0u, 32768u),
		new DataBG("bg544", string.Empty, "bg544", 0u, 32768u),
		new DataBG("bg545", string.Empty, "bg545", 0u, 32768u),
		new DataBG("bg546", string.Empty, "bg546", 0u, 32768u),
		new DataBG("bg547", string.Empty, "bg547", 0u, 32768u),
		new DataBG("bg548", string.Empty, "bg548", 0u, 32768u),
		new DataBG("bg549", string.Empty, "bg549", 0u, 32768u),
		new DataBG("bg54a", string.Empty, "bg54a", 0u, 32768u),
		new DataBG("bg552", string.Empty, "bg552", 0u, 32768u),
		new DataBG("bg545", string.Empty, "bg545", 0u, 32768u),
		new DataBG("bg563", string.Empty, "bg563", 0u, 32768u),
		new DataBG("bg575", string.Empty, "bg575", 0u, 32768u),
		new DataBG("bg576", string.Empty, "bg576", 0u, 32768u),
		new DataBG("bg577", string.Empty, "bg577", 0u, 32768u),
		new DataBG("bg578", string.Empty, "bg578", 0u, 32768u),
		new DataBG("bg579", string.Empty, "bg579", 0u, 32768u),
		new DataBG("bg57e", string.Empty, "bg57e", 0u, 32768u),
		new DataBG("bg584", string.Empty, "bg584", 0u, 45u),
		new DataBG("bg700", string.Empty, "bg700", 0u, 46u),
		new DataBG("bg701", string.Empty, "bg701", 0u, 47u),
		new DataBG("bg702", string.Empty, "bg702", 0u, 48u),
		new DataBG("bg703", string.Empty, "bg703", 0u, 49u),
		new DataBG("bg704", string.Empty, "bg704", 0u, 50u),
		new DataBG("bg13f", string.Empty, "bg13f", 0u, 51u),
		new DataBG("bg043", string.Empty, "bg043", 0u, 32768u),
		new DataBG("bg10e", string.Empty, "bg10e", 0u, 32768u),
		new DataBG("bg10f", string.Empty, "bg10f", 0u, 32768u),
		new DataBG("bg110", string.Empty, "bg110", 0u, 32768u),
		new DataBG("bg111", string.Empty, "bg111", 0u, 32768u),
		new DataBG("bg112", string.Empty, "bg112", 0u, 32768u),
		new DataBG("bg127", string.Empty, "bg127", 0u, 32768u),
		new DataBG("bg070", string.Empty, "bg070", 0u, 32768u),
		new DataBG("bg071", string.Empty, "bg071", 0u, 32768u),
		new DataBG("bg072", string.Empty, "bg072", 0u, 32768u),
		new DataBG("bg124", string.Empty, "bg124", 0u, 32768u),
		new DataBG("bg125", string.Empty, "bg125", 0u, 32768u),
		new DataBG("bg141", string.Empty, "bg141", 0u, 52u),
		new DataBG("bg090", string.Empty, "bg090", 0u, 32768u),
		new DataBG("bg073", string.Empty, "bg073", 0u, 32768u),
		new DataBG("bg103", string.Empty, "bg103", 0u, 32768u),
		new DataBG("bg219", string.Empty, "bg219", 0u, 32768u),
		new DataBG("title", string.Empty, "title", 0u, 57u),
		new DataBG("bg080", string.Empty, "bg080", 0u, 32768u),
		new DataBG("frame03", string.Empty, "frame03", 0u, 32768u),
		new DataBG("bg320", string.Empty, "bg320", 2u, 32768u),
		new DataBG("title_back", string.Empty, "title_back", 0u, 32768u),
		new DataBG("bg405_2", string.Empty, "bg405_2", 0u, 59u),
		new DataBG("bg315", string.Empty, "bg315", 0u, 32768u),
		new DataBG("bg315", string.Empty, "bg315", 0u, 32768u)
	};

	private List<string> gs1_bg_data_u_ = new List<string>
	{
		"bg00bu", "bg00cu", "bg01fu", "bg02du", "bg02eu", "bg030u", "bg03bu", "bg03du", "bg03fu", "bg046u",
		"bg047u", "bg048u", "bg04du", "bg052u", "bg053u", "bg054u", "bg059u", "bg069u", "bg072u", "bg076u",
		"bg084u", "bg089u", "bg08au", "bg09du", "bg0bfu", "bg0c0u", "bg0c1u", "bg0c5u", "bg0c7u", "bg0cbu",
		"bg0ccu", "bg0cdu", "bg0ceu", "bg0cfu", "bg0e0u", "bg0e1u", "bg0e3u", "bg0e4u", "bg0e5u", "bg0e6u",
		"bg0e7u", "bg0e8u", "bg0ecu", "bg0edu", "bg100u", "bg101u", "bg103u", "bg107u", "bg10du", "bg115u",
		"bg116u", "bg117u", "bg123u", "bg126u", "bg134u", "bg13au", "bg13fu", "bg140u", "titleu", "bg122u",
		"bg04fu", "bg049u", "bg04eu", "bg013u", "bg03au", "bg0e9u", "bg10cu", "bg018u", "bg060u", "bg05fu"
	};

	private List<string> gs2_bg_data_u_ = new List<string>
	{
		"bg101u", "bg102u", "bg207u", "bg208u", "bg20cu", "bg20du", "bg20eu", "bg210u", "bg219u", "bg301u",
		"bg303u", "bg304u", "bg30au", "bg30cu", "bg30du", "bg30fu", "bg310u", "bg316u", "bg31au", "bg401u",
		"bg405u", "bg40du", "bg411u", "bg414u", "bg416u", "bg41cu", "bg41eu", "bg420u", "bg423u", "bg424u",
		"bg425u", "bg700u", "bg701u", "bg702u", "bg703u", "bg705u", "bg13fu", "bg141u", "titleu", "bg012u",
		"bg302u", "bg306u", "bg417u", "bg200u", "bg201u", "bg300u", "bg104u", "bg408u", "bg426u"
	};

	private List<string> gs3_bg_data_u_ = new List<string>
	{
		"bg015u", "bg200u", "bg201u", "bg300u", "bg302u", "bg304u", "bg500", "bg503u", "bg504u", "bg505u",
		"bg506u", "bg507u", "bg50au", "bg106u", "bg109u", "bg10au", "bg10bu", "bg21bu", "bg21eu", "bg220u",
		"bg213u", "bg224u", "bg225u", "bg227u", "bg22au", "bg206u", "bg207u", "bg21au", "bg30fu", "bg312u",
		"bg313u", "bg317u", "bg318u", "bg404u", "bg405u", "bg50bu", "bg50du", "bg50eu", "bg50fu", "bg53au",
		"bg53bu", "bg53cu", "bg53du", "bg53fu", "bg543", "bg584u", "bg700u", "bg701u", "bg702u", "bg703u",
		"bg704u", "bg13fu", "bg141u", "bg212u", "bg228u", "bg31au", "bg31bu", "titleu", "bg21du", "bg405_2u",
		"bg204u", "bg205u"
	};

	public static bgData instance
	{
		get
		{
			return instance_;
		}
	}

	public List<DataBG> data
	{
		get
		{
			switch (GSStatic.global_work_.title)
			{
			case TitleId.GS1:
				return gs1_bg_data_;
			case TitleId.GS2:
				return gs2_bg_data_;
			case TitleId.GS3:
				return gs3_bg_data_;
			default:
				return null;
			}
		}
	}

	public List<DataSeal> seal
	{
		get
		{
			switch (GSStatic.global_work_.title)
			{
			case TitleId.GS1:
				return gs1_seal_data_;
			case TitleId.GS2:
				return gs2_seal_data_;
			case TitleId.GS3:
				return gs3_seal_data_;
			default:
				return null;
			}
		}
	}

	public List<string> data_language
	{
		get
		{
			Language language = GSStatic.global_work_.language;
			if (language == Language.USA)
			{
				return bg_data_u_;
			}
			return null;
		}
	}

	private List<string> bg_data_u_
	{
		get
		{
			switch (GSStatic.global_work_.title)
			{
			case TitleId.GS1:
				return gs1_bg_data_u_;
			case TitleId.GS2:
				return gs2_bg_data_u_;
			case TitleId.GS3:
				return gs3_bg_data_u_;
			default:
				return null;
			}
		}
	}

	private void Awake()
	{
		if (instance_ == null)
		{
			instance_ = this;
		}
	}

	public string GetBGName(int in_bg_no)
	{
		if (in_bg_no >= data.Count)
		{
			return string.Empty;
		}
		return data[in_bg_no].name_;
	}

	public string GetBGIcon(int in_bg_no)
	{
		if (in_bg_no >= data.Count)
		{
			return string.Empty;
		}
		bool flag = (long)in_bg_no != 117;
		if (data[in_bg_no].language_ != 32768 && GSStatic.global_work_.language != 0 && flag)
		{
			Language language = GSStatic.global_work_.language;
			if (language == Language.USA)
			{
				return data[in_bg_no].icon_ + "u";
			}
			return string.Empty;
		}
		return data[in_bg_no].icon_;
	}

	public uint GetBGType(int in_bg_no)
	{
		if (in_bg_no >= data.Count)
		{
			return 0u;
		}
		return data[in_bg_no].type_;
	}
}
