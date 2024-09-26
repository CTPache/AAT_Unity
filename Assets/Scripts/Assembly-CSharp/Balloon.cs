using UnityEngine;

public class Balloon
{
	private static TitleId CurrentTitle
	{
		get
		{
			return GSStatic.global_work_.title;
		}
	}

	private static GlobalWork GlobalWork
	{
		get
		{
			return GSStatic.global_work_;
		}
	}

	private static ushort TakeThatSE
	{
		get
		{
			if (CurrentTitle != TitleId.GS3)
			{
				return 55;
			}
			switch (GlobalWork.scenario)
			{
			case 0:
			case 1:
			case 12:
			case 13:
				return 371;
			case 15:
			case 16:
			case 17:
				return 368;
			default:
				return 55;
			}
		}
	}

	private static ushort HoldItSE
	{
		get
		{
			if (CurrentTitle != TitleId.GS3)
			{
				return 71;
			}
			switch (GlobalWork.scenario)
			{
			case 0:
			case 1:
			case 12:
			case 13:
				return 370;
			case 14:
			case 15:
			case 16:
			case 17:
				return 367;
			default:
				return 71;
			}
		}
	}

	private static ushort ObjectionSE
	{
		get
		{
			if (CurrentTitle != TitleId.GS3)
			{
				return 81;
			}
			switch (GlobalWork.scenario)
			{
			case 0:
			case 1:
			case 12:
			case 13:
				return 369;
			case 15:
			case 16:
			case 17:
				return 56;
			default:
				return 81;
			}
		}
	}

	public static void PlayTakeThat()
	{
		switch (CurrentTitle)
		{
		case TitleId.GS1:
			objMoveCtrl.instance.play(4);
			break;
		case TitleId.GS2:
			objMoveCtrl.instance.play(4);
			break;
		case TitleId.GS3:
			objMoveCtrl.instance.play(4);
			break;
		}
		soundCtrl.instance.PlaySE(TakeThatSE);
	}

	public static void PlayHoldIt()
	{
		switch (CurrentTitle)
		{
		case TitleId.GS1:
			objMoveCtrl.instance.play(12);
			break;
		case TitleId.GS2:
			objMoveCtrl.instance.play(9);
			break;
		case TitleId.GS3:
			objMoveCtrl.instance.play(9);
			break;
		}
		soundCtrl.instance.PlaySE(HoldItSE);
	}

	public static void PlayObjection()
	{
		switch (CurrentTitle)
		{
		case TitleId.GS1:
			objMoveCtrl.instance.play(2);
			break;
		case TitleId.GS2:
			objMoveCtrl.instance.play(2);
			break;
		case TitleId.GS3:
			objMoveCtrl.instance.play(2);
			break;
		}
		soundCtrl.instance.PlaySE(ObjectionSE);
	}

	public static void ChangeLanguageObjection()
	{
        //ushort[] array = new ushort[7] { 81, 56, 57, 369, 150, 372, 65 };
        //ushort in_se_no = array[Random.Range(0, array.Length)];

        ushort in_se_no = 81;

        objMoveCtrl.instance.play(2);
		soundCtrl.instance.PlaySE(in_se_no, true, true);
	}
}
