using UnityEngine;

public class GS1_BGChange : MonoBehaviour
{
	public static void GS1_CheckBGChange(uint no, uint flag)
	{
		no &= 0x7FFFu;
		switch (no)
		{
		case 0u:
			Sce00_bg000change();
			break;
		case 12u:
			Sce02_bg00Cchange();
			break;
		case 40u:
			Sce01_bg028change();
			break;
		case 15u:
			Sce01_bg00Fchange();
			break;
		case 72u:
			Sce03_bg048change();
			break;
		case 80u:
			Sce03_bg050change();
			break;
		case 115u:
			Sce04_bg000change();
			Sce04_bg001change();
			break;
		case 119u:
			Sce04_bg002change();
			Sce04_bg003change();
			Sce04_bg007change();
			Sce04_bg008change();
			break;
		case 116u:
			Sce04_bg004change();
			break;
		case 114u:
			Sce04_bg005change();
			break;
		case 117u:
			Sce04_bg006change();
			break;
		case 159u:
			Sce04_bg009change();
			break;
		case 225u:
			Sce04_bg00achange();
			break;
		case 200u:
			Sce04_bg00cchange();
			break;
		case 220u:
			Sce04_bg00bchange();
			break;
		}
	}

	public static void Sce00_bg000change()
	{
		if (!GSFlag.Check(0u, scenario.SCE1_CHIHIRO_DISCOVER))
		{
			bgCtrl.instance.SetSeal(13);
		}
		else
		{
			bgCtrl.instance.SetSeal(13, false);
		}
		if (GSFlag.Check(0u, scenario.SCE1_SITAI))
		{
			bgCtrl.instance.SetSeal(1);
		}
	}

	public static void Sce01_bg028change()
	{
		if (GSStatic.global_work_.scenario == 3 && !GSFlag.Check(0u, scenario.SCE2_BOY_DEPOSITION_1))
		{
			bgCtrl.instance.SetSeal(3);
		}
	}

	public static void Sce01_bg00Fchange()
	{
		if (GSFlag.Check(0u, scenario.SCE0123_FLAG_SCE1_3BG_SET))
		{
			if (GSStatic.global_work_.language == "USA")
			{
				bgCtrl.instance.SetSeal(18);
				bgCtrl.instance.SetSeal(19);
			}
			else
			{
				bgCtrl.instance.SetSeal(6);
				bgCtrl.instance.SetSeal(7);
			}
		}
		else if (GSStatic.global_work_.language == "USA")
		{
			bgCtrl.instance.SetSeal(18, false);
			bgCtrl.instance.SetSeal(19, false);
		}
		else
		{
			bgCtrl.instance.SetSeal(6, false);
			bgCtrl.instance.SetSeal(7, false);
		}
	}

	public static void Sce02_bg00Cchange()
	{
		if (GSStatic.global_work_.scenario == 7 && !GSFlag.Check(0u, scenario.SCE22_DRAIN_DESTROY))
		{
			bgCtrl.instance.SetSeal(0);
			bgCtrl.instance.SetSeal(2);
		}
	}

	public static void Sce03_bg048change()
	{
		if (GSStatic.global_work_.scenario == 11 && !GSFlag.Check(0u, scenario.SCE30_GET_CRACKER))
		{
			bgCtrl.instance.SetSeal(5);
		}
	}

	public static void Sce03_bg050change()
	{
		if (GSStatic.global_work_.scenario == 15)
		{
			bgCtrl.instance.SetSeal(4);
		}
	}

	public static void Sce04_bg000change()
	{
		if (GSStatic.global_work_.scenario == 17 || GSStatic.global_work_.scenario == 18)
		{
			if (!GSFlag.Check(0u, scenario.SCE4_GET_TADASIKI_ID))
			{
				bgCtrl.instance.SetSeal(8);
			}
			else
			{
				bgCtrl.instance.SetSeal(8, false);
			}
		}
	}

	public static void Sce04_bg001change()
	{
		if (GSStatic.global_work_.scenario == 17 || GSStatic.global_work_.scenario == 18)
		{
			if (!GSFlag.Check(0u, scenario.SCE4_GET_TOMOE_PHONE))
			{
				bgCtrl.instance.SetSeal(9);
			}
			else
			{
				bgCtrl.instance.SetSeal(9, false);
			}
		}
	}

	public static void Sce04_bg002change()
	{
		if (GSFlag.Check(0u, scenario.SCE42_FLAG_BLOODSTAIN01_DISP))
		{
			bgCtrl.instance.SetSeal(36);
		}
		if (GSFlag.Check(0u, scenario.SCE42_FLAG_BLOODSTAIN02_DISP))
		{
			bgCtrl.instance.SetSeal(35);
		}
		if ((uint)GSStatic.global_work_.scenario >= 22u || GSFlag.Check(0u, scenario.SCE41_FLAG_DISP_JYOMEN_PASSAGE))
		{
			if (GSStatic.global_work_.language == "USA")
			{
				bgCtrl.instance.SetSeal(21);
				bgCtrl.instance.SetSeal(24);
			}
			else
			{
				bgCtrl.instance.SetSeal(20);
				bgCtrl.instance.SetSeal(23);
			}
			bgCtrl.instance.SetSeal(22);
			bgCtrl.instance.SetSeal(25);
			bgCtrl.instance.SetSeal(26);
			bgCtrl.instance.SetSeal(27);
			bgCtrl.instance.SetSeal(28);
			bgCtrl.instance.SetSeal(29);
			bgCtrl.instance.SetSeal(30);
			bgCtrl.instance.SetSeal(31);
			bgCtrl.instance.SetSeal(32);
			bgCtrl.instance.SetSeal(33);
			bgCtrl.instance.SetSeal(34);
		}
	}

	public static void Sce04_bg003change()
	{
	}

	public static void Sce04_bg004change()
	{
		if (GSFlag.Check(0u, scenario.SCE44_FLAG_DISP_SAFE))
		{
			bgCtrl.instance.SetSeal(12);
		}
	}

	public static void Sce04_bg005change()
	{
		if (GSStatic.global_work_.scenario == 29 || GSStatic.global_work_.scenario == 30)
		{
			if (GSFlag.Check(0u, scenario.SCE44_FLAG_DISP_KENJI_JIHYO))
			{
				bgCtrl.instance.SetSeal(11);
			}
			else
			{
				bgCtrl.instance.SetSeal(11, false);
			}
		}
	}

	public static void Sce04_bg006change()
	{
		if (GSStatic.global_work_.scenario == 25)
		{
			if (GSFlag.Check(0u, scenario.SCE43_FLAG_SET_DOOR))
			{
				bgCtrl.instance.SetSeal(10);
			}
			else
			{
				bgCtrl.instance.SetSeal(10, false);
			}
		}
		else if (GSStatic.global_work_.scenario == 24)
		{
			if (GSFlag.Check(0u, scenario.SCE42_FLAG_SET_DOOR))
			{
				bgCtrl.instance.SetSeal(10);
			}
			else
			{
				bgCtrl.instance.SetSeal(10, false);
			}
		}
	}

	public static void Sce04_bg007change()
	{
	}

	public static void Sce04_bg008change()
	{
		if (GSStatic.global_work_.scenario == 20 && GSFlag.Check(0u, scenario.SCE41_FLAG_DISP_CAR))
		{
			bgCtrl.instance.SetSeal(43);
		}
	}

	public static void Sce04_bg009change()
	{
		if (GSFlag.Check(0u, scenario.SCE42_FLAG_BLOODSTAIN03_DISP))
		{
			bgCtrl.instance.SetSeal(37);
		}
		if (GSFlag.Check(0u, scenario.SCE42_FLAG_BLOODSTAIN05_DISP))
		{
			bgCtrl.instance.SetSeal(38);
		}
		if (GSFlag.Check(0u, scenario.SCE42_FLAG_BLOODSTAIN08_DISP))
		{
			bgCtrl.instance.SetSeal(40);
		}
		if (GSFlag.Check(0u, scenario.SCE43_FLAG_BG_TAIHOKUN_DISP))
		{
			if (GSStatic.global_work_.language == "USA")
			{
				bgCtrl.instance.SetSeal(42);
			}
			else
			{
				bgCtrl.instance.SetSeal(41);
			}
		}
		if (GSStatic.global_work_.scenario == 26 && GSFlag.Check(0u, scenario.SCE43_FLAG_BG_SET))
		{
			if (GSStatic.global_work_.language == "USA")
			{
				bgCtrl.instance.SetSeal(16);
				bgCtrl.instance.SetSeal(17);
			}
			else
			{
				bgCtrl.instance.SetSeal(14);
				bgCtrl.instance.SetSeal(15);
			}
		}
	}

	public static void Sce04_bg00achange()
	{
		bgCtrl.instance.SetSeal(44);
	}

	public static void Sce04_bg00bchange()
	{
	}

	public static void Sce04_bg00cchange()
	{
	}
}
