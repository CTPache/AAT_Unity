public static class GS3_mess
{
	public static void GS3_AddScript2(ushort no, ushort flag)
	{
		MessageWork activeMessageWork = MessageSystem.GetActiveMessageWork();
		if (no != ushort.MaxValue)
		{
			activeMessageWork.sc_no = no;
		}
		string text = ((GSStatic.global_work_.language != 0) ? "_u" : string.Empty);
		switch (no)
		{
		default:
			return;
		case 2:
			return;
		case 1:
			advCtrl.instance.message_system_.LoadScenarioMdtFromStreamingAssets("GS3/scenario/sc0_1b_text" + text + ".mdt");
			break;
		case 3:
			advCtrl.instance.message_system_.LoadScenarioMdtFromStreamingAssets("GS3/scenario/sc1_1b_text" + text + ".mdt");
			break;
		case 4:
			advCtrl.instance.message_system_.LoadScenarioMdtFromStreamingAssets("GS3/scenario/sc1_2_text" + text + ".mdt");
			return;
		case 5:
			advCtrl.instance.message_system_.LoadScenarioMdtFromStreamingAssets("GS3/scenario/sc1_2b_text" + text + ".mdt");
			return;
		case 6:
			advCtrl.instance.message_system_.LoadScenarioMdtFromStreamingAssets("GS3/scenario/sc1_2c_text" + text + ".mdt");
			return;
		case 7:
			advCtrl.instance.message_system_.LoadScenarioMdtFromStreamingAssets("GS3/scenario/sc1_0_text" + text + ".mdt");
			return;
		case 8:
			advCtrl.instance.message_system_.LoadScenarioMdtFromStreamingAssets("GS3/scenario/sc1_0b_text" + text + ".mdt");
			return;
		case 9:
			advCtrl.instance.message_system_.LoadScenarioMdtFromStreamingAssets("GS3/scenario/sc1_3_1b_text" + text + ".mdt");
			break;
		case 10:
			advCtrl.instance.message_system_.LoadScenarioMdtFromStreamingAssets("GS3/scenario/sc2_0_text" + text + ".mdt");
			return;
		case 11:
			advCtrl.instance.message_system_.LoadScenarioMdtFromStreamingAssets("GS3/scenario/sc2_0b_text" + text + ".mdt");
			return;
		case 12:
			advCtrl.instance.message_system_.LoadScenarioMdtFromStreamingAssets("GS3/scenario/sc2_2_text" + text + ".mdt");
			return;
		case 13:
			advCtrl.instance.message_system_.LoadScenarioMdtFromStreamingAssets("GS3/scenario/sc2_2b_text" + text + ".mdt");
			return;
		case 14:
			advCtrl.instance.message_system_.LoadScenarioMdtFromStreamingAssets("GS3/scenario/sc2_2c_text" + text + ".mdt");
			return;
		case 15:
			advCtrl.instance.message_system_.LoadScenarioMdtFromStreamingAssets("GS3/scenario/sc2_3_1b_text" + text + ".mdt");
			break;
		case 16:
			advCtrl.instance.message_system_.LoadScenarioMdtFromStreamingAssets("GS3/scenario/sc3_0_0b_text" + text + ".mdt");
			break;
		case 17:
			advCtrl.instance.message_system_.LoadScenarioMdtFromStreamingAssets("GS3/scenario/sc2_1b_text" + text + ".mdt");
			break;
		case 18:
			advCtrl.instance.message_system_.LoadScenarioMdtFromStreamingAssets("GS3/scenario/sc4_0_1_text" + text + ".mdt");
			return;
		case 19:
			advCtrl.instance.message_system_.LoadScenarioMdtFromStreamingAssets("GS3/scenario/sc4_0_1b_text" + text + ".mdt");
			return;
		case 20:
			advCtrl.instance.message_system_.LoadScenarioMdtFromStreamingAssets("GS3/scenario/sc3_0_1b_text" + text + ".mdt");
			break;
		case 21:
			advCtrl.instance.message_system_.LoadScenarioMdtFromStreamingAssets("GS3/scenario/sc4_2_0_text" + text + ".mdt");
			return;
		case 22:
			advCtrl.instance.message_system_.LoadScenarioMdtFromStreamingAssets("GS3/scenario/sc4_2_0b_text" + text + ".mdt");
			return;
		case 23:
			advCtrl.instance.message_system_.LoadScenarioMdtFromStreamingAssets("GS3/scenario/sc4_2_1_text" + text + ".mdt");
			return;
		case 24:
			advCtrl.instance.message_system_.LoadScenarioMdtFromStreamingAssets("GS3/scenario/sc4_2_1b_text" + text + ".mdt");
			return;
		case 25:
			advCtrl.instance.message_system_.LoadScenarioMdtFromStreamingAssets("GS3/scenario/sc4_3_0b_text" + text + ".mdt");
			break;
		case 26:
			advCtrl.instance.message_system_.LoadScenarioMdtFromStreamingAssets("GS3/scenario/sc4_3_2b_text" + text + ".mdt");
			break;
		case 27:
			advCtrl.instance.message_system_.LoadScenarioMdtFromStreamingAssets("GS3/scenario/sc4_3_2c_text" + text + ".mdt");
			break;
		}
		if (flag != 0)
		{
			GS3_CheckScenario(GSStatic.global_work_.scenario);
			advCtrl.instance.message_system_.SetMessage(128u);
		}
	}

	public static void GS3_CheckScenario(byte no)
	{
		GlobalWork global_work_ = GSStatic.global_work_;
		MessageWork activeMessageWork = MessageSystem.GetActiveMessageWork();
		if (global_work_.scenario >= 2)
		{
			global_work_.sw_move_flag[0] = 1;
			global_work_.sw_move_flag[4] = 1;
		}
		if (global_work_.scenario >= 3)
		{
			global_work_.sw_move_flag[1] = 1;
			global_work_.sw_move_flag[2] = 1;
			global_work_.sw_move_flag[3] = 1;
			global_work_.sw_move_flag[7] = 1;
		}
		if (global_work_.scenario >= 8)
		{
			global_work_.sw_move_flag[8] = 1;
			global_work_.sw_move_flag[9] = 1;
			global_work_.sw_move_flag[12] = 1;
			global_work_.sw_move_flag[13] = 1;
		}
		if (global_work_.scenario >= 14)
		{
			global_work_.sw_move_flag[14] = 1;
			global_work_.sw_move_flag[24] = 1;
		}
		if (global_work_.scenario >= 15)
		{
			global_work_.sw_move_flag[15] = 1;
			global_work_.sw_move_flag[16] = 1;
			global_work_.sw_move_flag[17] = 1;
			global_work_.sw_move_flag[26] = 1;
			global_work_.sw_move_flag[27] = 1;
			global_work_.sw_move_flag[34] = 1;
		}
		if (global_work_.scenario >= 16)
		{
			global_work_.sw_move_flag[18] = 1;
			global_work_.sw_move_flag[19] = 1;
			global_work_.sw_move_flag[21] = 1;
		}
		if (global_work_.scenario >= 19)
		{
			global_work_.sw_move_flag[20] = 1;
			global_work_.sw_move_flag[28] = 1;
			global_work_.sw_move_flag[29] = 1;
			global_work_.sw_move_flag[30] = 1;
			global_work_.sw_move_flag[31] = 1;
		}
		if (global_work_.Scenario_enable >= 90)
		{
			for (byte b = 0; b < global_work_.sw_move_flag.Length; b++)
			{
				global_work_.sw_move_flag[b] = 1;
			}
		}
		else if (global_work_.Scenario_enable >> 4 == 3)
		{
			global_work_.sw_move_flag[0] = 1;
			global_work_.sw_move_flag[1] = 1;
			global_work_.sw_move_flag[2] = 1;
			global_work_.sw_move_flag[3] = 1;
			global_work_.sw_move_flag[4] = 1;
			global_work_.sw_move_flag[5] = 1;
			global_work_.sw_move_flag[6] = 1;
			global_work_.sw_move_flag[7] = 1;
		}
		else if (global_work_.Scenario_enable >> 4 >= 4)
		{
			global_work_.sw_move_flag[0] = 1;
			global_work_.sw_move_flag[1] = 1;
			global_work_.sw_move_flag[2] = 1;
			global_work_.sw_move_flag[3] = 1;
			global_work_.sw_move_flag[4] = 1;
			global_work_.sw_move_flag[5] = 1;
			global_work_.sw_move_flag[6] = 1;
			global_work_.sw_move_flag[7] = 1;
			global_work_.sw_move_flag[8] = 1;
			global_work_.sw_move_flag[9] = 1;
			global_work_.sw_move_flag[10] = 1;
			global_work_.sw_move_flag[11] = 1;
			global_work_.sw_move_flag[12] = 1;
			global_work_.sw_move_flag[13] = 1;
		}
		byte b2 = 1;
		byte b3 = 1;
		switch (no)
		{
		case 0:
		case 1:
			b2 = 1;
			b3 += global_work_.scenario;
			break;
		case 2:
		case 3:
		case 4:
		case 5:
		case 6:
			b2 = 2;
			b3 += (byte)(global_work_.scenario - 2);
			break;
		case 7:
		case 8:
		case 9:
		case 10:
		case 11:
			b2 = 3;
			b3 += (byte)(global_work_.scenario - 7);
			break;
		case 12:
		case 13:
			b2 = 4;
			b3 += (byte)(global_work_.scenario - 12);
			break;
		case 14:
		case 15:
		case 16:
		case 17:
		case 18:
		case 19:
		case 20:
		case 21:
		case 22:
			b2 = 5;
			b3 += (byte)(global_work_.scenario - 14);
			break;
		}
		if (global_work_.Scenario_enable < b2 << 4)
		{
			global_work_.Scenario_enable = (byte)(b2 << 4);
		}
		if (global_work_.Scenario_enable >> 4 > b2 || (global_work_.Scenario_enable & 0xF) > b3)
		{
			for (byte b = 0; b < 8; b++)
			{
			}
			if (activeMessageWork.now_no > 127)
			{
				activeMessageWork.status |= MessageSystem.Status.READ_MESSAGE;
			}
		}
	}
}
