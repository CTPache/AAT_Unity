using UnityEngine;

public class GS3_BGChange : MonoBehaviour
{
	public class GS3_MapData
	{
		public int st;

		public int num;

		public GS3_MapData(int _in_st, int _in_num)
		{
			st = _in_st;
			num = _in_num;
		}
	}

	public static GS3_MapData[] MapDemo = new GS3_MapData[10]
	{
		new GS3_MapData(37, 1),
		new GS3_MapData(38, 2),
		new GS3_MapData(40, 2),
		new GS3_MapData(42, 2),
		new GS3_MapData(44, 2),
		new GS3_MapData(46, 2),
		new GS3_MapData(48, 2),
		new GS3_MapData(50, 2),
		new GS3_MapData(52, 2),
		new GS3_MapData(-1, -1)
	};

	public static void GS3_CheckBGChange(uint no, uint flag)
	{
		no &= 0x7FFFu;
		switch (no)
		{
		case 22u:
			Sce02_SoukoBG();
			break;
		case 24u:
			Sce02_AigaBG();
			break;
		case 27u:
			Sce02_SyachoBG();
			break;
		case 32u:
			if (GSStatic.global_work_.Room == 12 && GSStatic.global_work_.scenario == 7)
			{
				Sce03_ZasshiBG();
			}
			Sce03_ScooterBG();
			break;
		case 34u:
			if (GSStatic.global_work_.Room == 11 && GSStatic.global_work_.scenario == 9)
			{
				Sce03_NisebengoBG();
				Sce03_MacchiBG();
			}
			Sce03_RyoshuBG();
			break;
		case 31u:
			Sce03_MagatamaBG();
			break;
		case 29u:
		case 30u:
			if (GSStatic.global_work_.Room == 8 && GSStatic.global_work_.scenario == 7 && !GSFlag.Check(0u, scenario_GS3.SCE2_0_PASS_SNEWS_MAKO) && !GSFlag.Check(0u, scenario_GS3.SCE2_0_GET_SNEWS))
			{
				Sce03_NewsBG();
			}
			break;
		case 112u:
			if (GSStatic.global_work_.scenario == 12)
			{
				Sce04_MapShiteki();
			}
			if ((GSStatic.global_work_.scenario == 12 || GSStatic.global_work_.scenario == 13) && GSFlag.Check(0u, scenario_GS3.SCF3_0_CARMASK) && flag != 2)
			{
				Sce04_MapCarMask();
			}
			break;
		case 113u:
			Sce04_Map00();
			break;
		case 114u:
			Sce04_Map01();
			break;
		case 41u:
			Sce05_Oboro01BG();
			break;
		case 42u:
			Sce05_Oboro02BG();
			break;
		case 39u:
			Sce05_YahariBG();
			break;
		case 36u:
			Sce05_SnowBG();
			break;
		case 49u:
			Sce05_CurryBG();
			break;
		case 50u:
			Sce05_Key01BG();
			break;
		case 51u:
			Sce05_Key02BG();
			break;
		case 47u:
			Sce05_WireBG();
			break;
		case 48u:
			Sce05_WireBG();
			break;
		case 124u:
			Sce05_Map00();
			break;
		case 125u:
			Sce05_Map01();
			break;
		case 127u:
			Sce05_Map02();
			break;
		case 128u:
			Sce05_Map03();
			break;
		case 129u:
			Sce05_Map05();
			break;
		case 115u:
			Sce04_Map00();
			Sce05_Map04();
			break;
		case 116u:
			Sce04_Map00();
			Sce05_Map06();
			break;
		case 53u:
			Sce05_Nakaniwa2();
			break;
		case 54u:
			Sce05_Nakaniwa();
			break;
		case 38u:
			Sce05_Keidai();
			break;
		}
	}

	private static void Sce02_SoukoBG()
	{
		if (!GSFlag.Check(0u, scenario_GS3.SCE1_0_KIRIO_DINNER))
		{
			bgCtrl.instance.SetSeal(3, false);
			bgCtrl.instance.SetSeal(2);
			if (GSStatic.global_work_.language == "USA")
			{
				bgCtrl.instance.SetSeal(5, false);
				bgCtrl.instance.SetSeal(1);
			}
			else
			{
				bgCtrl.instance.SetSeal(4, false);
				bgCtrl.instance.SetSeal(0);
			}
		}
		else
		{
			bgCtrl.instance.SetSeal(2, false);
			bgCtrl.instance.SetSeal(3);
			if (GSStatic.global_work_.language == "USA")
			{
				bgCtrl.instance.SetSeal(1, false);
				bgCtrl.instance.SetSeal(5);
			}
			else
			{
				bgCtrl.instance.SetSeal(0, false);
				bgCtrl.instance.SetSeal(4);
			}
		}
	}

	private static void Sce02_AigaBG()
	{
		if (GSFlag.Check(0u, scenario_GS3.SCE1_0_HARUMI_DENWA))
		{
			bgCtrl.instance.SetSeal(6);
		}
	}

	private static void Sce02_SyachoBG()
	{
		bgCtrl.instance.SetSeal(7);
	}

	private static void Sce03_ScooterBG()
	{
		if (!GSFlag.Check(0u, scenario_GS3.SCE2_0_CHK_SCOOTER))
		{
			bgCtrl.instance.SetSeal(8);
		}
		else
		{
			bgCtrl.instance.SetSeal(8, false);
		}
	}

	private static void Sce03_MagatamaBG()
	{
		if (!GSFlag.Check(0u, scenario_GS3.SCE2_0_GET_MAGATAMA))
		{
			bgCtrl.instance.SetSeal(9);
		}
		else
		{
			bgCtrl.instance.SetSeal(9, false);
		}
	}

	private static void Sce03_RyoshuBG()
	{
		if (!GSFlag.Check(0u, scenario_GS3.SCE2_2_GET_BILL))
		{
			bgCtrl.instance.SetSeal(10);
		}
		else
		{
			bgCtrl.instance.SetSeal(10, false);
		}
	}

	private static void Sce03_ZasshiBG()
	{
		if (!GSFlag.Check(0u, scenario_GS3.SCE2_0_GET_SHIGOTO))
		{
			if (GSStatic.global_work_.language == "USA")
			{
				bgCtrl.instance.SetSeal(12);
			}
			else
			{
				bgCtrl.instance.SetSeal(11);
			}
		}
		else if (GSStatic.global_work_.language == "USA")
		{
			bgCtrl.instance.SetSeal(12, false);
		}
		else
		{
			bgCtrl.instance.SetSeal(11, false);
		}
	}

	private static void Sce03_NisebengoBG()
	{
		if (!GSFlag.Check(0u, scenario_GS3.SCE2_2_GET_KAMIBADGE))
		{
			bgCtrl.instance.SetSeal(13);
		}
		else
		{
			bgCtrl.instance.SetSeal(13, false);
		}
	}

	private static void Sce03_NewsBG()
	{
		if (GSStatic.global_work_.language == "USA")
		{
			bgCtrl.instance.SetSeal(15);
		}
		else
		{
			bgCtrl.instance.SetSeal(14);
		}
	}

	private static void Sce03_MacchiBG()
	{
		if (!GSFlag.Check(0u, scenario_GS3.SCE2_2_GET_MATCH))
		{
			bgCtrl.instance.SetSeal(16);
		}
		else
		{
			bgCtrl.instance.SetSeal(16, false);
		}
	}

	private static void Sce04_Map00()
	{
		bgCtrl.instance.SetSeal(19);
		bgCtrl.instance.SetSeal(20);
	}

	private static void Sce04_Map01()
	{
		bgCtrl.instance.SetSeal(19);
		bgCtrl.instance.SetSeal(20);
		bgCtrl.instance.SetSeal(21);
	}

	private static void Sce05_Oboro01BG()
	{
		bgCtrl.instance.SetSeal(22);
	}

	private static void Sce05_Oboro02BG()
	{
		if (GSStatic.global_work_.language == "USA")
		{
			bgCtrl.instance.SetSeal(24);
		}
		else
		{
			bgCtrl.instance.SetSeal(23);
		}
	}

	private static void Sce05_YahariBG()
	{
		if (!GSFlag.Check(0u, scenario_GS3.SCE4_0_1_GET_SCRROL))
		{
			bgCtrl.instance.SetSeal(25);
		}
		else
		{
			bgCtrl.instance.SetSeal(25, false);
		}
	}

	private static void Sce05_SnowBG()
	{
		bgCtrl.instance.SetSeal(26);
	}

	private static void Sce05_CurryBG()
	{
		bgCtrl.instance.SetSeal(27);
	}

	private static void Sce05_Key01BG()
	{
		bgCtrl.instance.SetSeal(28);
	}

	private static void Sce05_Key02BG()
	{
		bgCtrl.instance.SetSeal(29);
	}

	private static void Sce05_WireBG()
	{
		bgCtrl.instance.SetSeal(30);
	}

	private static void Sce05_Map00()
	{
		bgCtrl.instance.SetSeal(31);
		bgCtrl.instance.SetSeal(33);
	}

	private static void Sce05_Map01()
	{
		bgCtrl.instance.SetSeal(31);
	}

	private static void Sce05_Map02()
	{
		bgCtrl.instance.SetSeal(32);
	}

	private static void Sce05_Map03()
	{
		bgCtrl.instance.SetSeal(32);
		bgCtrl.instance.SetSeal(54);
	}

	private static void Sce05_Nakaniwa()
	{
		bgCtrl.instance.SetSeal(34);
	}

	private static void Sce05_Map04()
	{
		int num = 0;
		for (num = 0; num < 15; num++)
		{
			bgCtrl.instance.SetSeal(37 + num);
		}
		bgCtrl.instance.SetSeal(53);
	}

	private static void Sce05_Map06()
	{
	}

	private static void Sce05_Nakaniwa2()
	{
		bgCtrl.instance.SetSeal(35);
	}

	private static void Sce05_Keidai()
	{
		bgCtrl.instance.SetSeal(36);
	}

	private static void Sce05_Map05()
	{
		bgCtrl.instance.SetSeal(33);
	}

	private static void Sce04_MapShiteki()
	{
		if (GSFlag.Check(0u, scenario_GS3.SCE3_0_MAPSHITEKI))
		{
			if (GSStatic.global_work_.language == "USA")
			{
				bgCtrl.instance.SetSeal(18);
			}
			else
			{
				bgCtrl.instance.SetSeal(17);
			}
		}
	}

	private static void Sce04_MapCarMask()
	{
		bgCtrl.instance.SetSeal(19);
	}
}
