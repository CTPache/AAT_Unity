using UnityEngine;

public class GS2_BGChange : MonoBehaviour
{
	public static void GS2_CheckBGChange(uint no, uint flag)
	{
		no &= 0x7FFFu;
		switch (no)
		{
		case 30u:
			Sce02_bg202change();
			break;
		case 31u:
			Sce02_bg204change();
			break;
		case 32u:
			Sce02_bg205change();
			break;
		case 42u:
			Sce02_bg20Fchange();
			break;
		case 61u:
			Sce03_bg302change();
			break;
		case 91u:
			Sce04_bg403change();
			break;
		case 94u:
			Sce04_bg406change();
			break;
		case 95u:
			Sce04_bg407change();
			break;
		case 96u:
			Sce04_bg408change();
			break;
		}
	}

	private static void Sce02_bg202change()
	{
		if (GSFlag.Check(0u, scenario_GS2.SCE1_KURAIN1) || GSStatic.global_work_.scenario >= 5)
		{
			AnimationObject animationObject = AnimationSystem.Instance.FindObject((int)GSStatic.global_work_.title, 0, 12);
			if (animationObject == null)
			{
				bgCtrl.instance.SetSeal(0);
			}
		}
	}

	private static void Sce02_bg204change()
	{
		if (!GSFlag.Check(0u, scenario_GS2.SCE2_NEWS2_2) || !GSFlag.Check(0u, scenario_GS2.SCE2_LICE))
		{
			if (GSFlag.Check(0u, scenario_GS2.SCE2_BLKEY) && !GSFlag.Check(0u, scenario_GS2.SCE2_SYOUZOKU))
			{
				bgCtrl.instance.SetSeal(1);
			}
			else
			{
				bgCtrl.instance.SetSeal(1, false);
			}
		}
	}

	private static void Sce02_bg205change()
	{
		if (!GSFlag.Check(0u, scenario_GS2.SCE2_MARI) || !GSFlag.Check(0u, scenario_GS2.SCE2_BLKEY) || !GSFlag.Check(0u, scenario_GS2.SCE2_SYOUZOKU) || !GSFlag.Check(0u, scenario_GS2.SCE2_KURAIN_2ND))
		{
			return;
		}
		if (!GSFlag.Check(0u, scenario_GS2.SCE2_MARI2))
		{
			if (GSStatic.global_work_.language == "USA")
			{
				bgCtrl.instance.SetSeal(3);
			}
			else
			{
				bgCtrl.instance.SetSeal(2);
			}
		}
		else
		{
			bgCtrl.instance.SetSeal(3, false);
			bgCtrl.instance.SetSeal(2, false);
		}
	}

	private static void Sce02_bg20Fchange()
	{
		if (GSFlag.Check(0u, scenario_GS2.SCE2_POINT_SEAL_FLAG) || GSStatic.message_work_.now_no == scenario_GS2.SC1_04350)
		{
			if (GSStatic.global_work_.language == "USA")
			{
				bgCtrl.instance.SetSeal(6);
			}
			else
			{
				bgCtrl.instance.SetSeal(5);
			}
			bgCtrl.instance.SetSeal(4);
		}
		else
		{
			bgCtrl.instance.SetSeal(6, false);
			bgCtrl.instance.SetSeal(5, false);
			bgCtrl.instance.SetSeal(4, false);
		}
	}

	private static void Sce03_bg302change()
	{
		if ((GSStatic.global_work_.scenario == 8 || GSStatic.global_work_.scenario == 11) && !GSFlag.Check(0u, scenario_GS2.SCE32_HARIGAMI2))
		{
			bgCtrl.instance.SetSeal(7);
		}
	}

	private static void Sce04_bg403change()
	{
		if (GSFlag.Check(0u, scenario_GS2.SCE401_SHIKISHIOBJ_FLG))
		{
			if (GSStatic.global_work_.language == "USA")
			{
				bgCtrl.instance.SetSeal(9);
			}
			else
			{
				bgCtrl.instance.SetSeal(8);
			}
		}
		else
		{
			bgCtrl.instance.SetSeal(9, false);
			bgCtrl.instance.SetSeal(8, false);
		}
	}

	private static void Sce04_bg406change()
	{
		if (GSFlag.Check(0u, scenario_GS2.SCE421_TANAKA))
		{
			if (!GSFlag.Check(0u, scenario_GS2.SCE421_ACCE))
			{
				bgCtrl.instance.SetSeal(10);
			}
			else
			{
				bgCtrl.instance.SetSeal(10, false);
			}
		}
	}

	private static void Sce04_bg407change()
	{
		if (GSStatic.global_work_.scenario == 18 && GSFlag.Check(0u, scenario_GS2.SCE420_AVROOM_1ST))
		{
			bgCtrl.instance.SetSeal(12);
			bgCtrl.instance.SetSeal(11);
		}
	}

	private static void Sce04_bg408change()
	{
		if (GSFlag.Check(0u, scenario_GS2.SCE401_SAZAE))
		{
			bgCtrl.instance.SetSeal(13);
		}
		else
		{
			bgCtrl.instance.SetSeal(13, false);
		}
		if (GSStatic.global_work_.scenario == 19)
		{
			if (!GSFlag.Check(0u, scenario_GS2.SCE421_PHOTO))
			{
				bgCtrl.instance.SetSeal(14);
			}
			else
			{
				bgCtrl.instance.SetSeal(14, false);
			}
		}
	}
}
