public static class GS2_mess
{
	public static void GS2_CheckScenario(byte no)
	{
		GlobalWork global_work_ = GSStatic.global_work_;
		MessageWork activeMessageWork = MessageSystem.GetActiveMessageWork();
		if (global_work_.scenario >= 2)
		{
			global_work_.sw_move_flag[0] = 1;
			global_work_.sw_move_flag[1] = 1;
		}
		if (global_work_.scenario >= 3 && global_work_.scenario <= 7)
		{
			global_work_.sw_move_flag[3] = 1;
			global_work_.sw_move_flag[4] = 1;
			global_work_.sw_move_flag[5] = 1;
			global_work_.sw_move_flag[6] = 1;
			global_work_.sw_move_flag[7] = 1;
		}
		if (global_work_.scenario >= 8 && global_work_.scenario <= 13)
		{
			global_work_.sw_move_flag[8] = 1;
		}
		if (global_work_.scenario >= 9 && global_work_.scenario <= 13)
		{
			global_work_.sw_move_flag[9] = 1;
			global_work_.sw_move_flag[10] = 1;
			global_work_.sw_move_flag[11] = 1;
			global_work_.sw_move_flag[12] = 1;
			global_work_.sw_move_flag[13] = 1;
			global_work_.sw_move_flag[14] = 1;
		}
		if (global_work_.scenario >= 14)
		{
			global_work_.sw_move_flag[2] = 1;
			global_work_.sw_move_flag[19] = 1;
			global_work_.sw_move_flag[25] = 1;
		}
		if (global_work_.scenario == 15)
		{
			global_work_.sw_move_flag[18] = 1;
			global_work_.sw_move_flag[20] = 1;
		}
		if (global_work_.scenario >= 16 && global_work_.scenario <= 18)
		{
			global_work_.sw_move_flag[15] = 1;
			global_work_.sw_move_flag[16] = 1;
			global_work_.sw_move_flag[17] = 1;
			global_work_.sw_move_flag[18] = 1;
			global_work_.sw_move_flag[20] = 1;
		}
		if (global_work_.scenario == 19)
		{
			global_work_.sw_move_flag[15] = 1;
			global_work_.sw_move_flag[16] = 1;
			global_work_.sw_move_flag[17] = 1;
			global_work_.sw_move_flag[18] = 1;
			global_work_.sw_move_flag[20] = 1;
			global_work_.sw_move_flag[22] = 1;
			global_work_.sw_move_flag[21] = 1;
		}
		if (global_work_.Scenario_enable >= 79)
		{
			for (int i = 0; i < global_work_.sw_move_flag.Length; i++)
			{
				global_work_.sw_move_flag[i] = 1;
			}
		}
		else if (global_work_.Scenario_enable >> 4 == 3)
		{
			global_work_.sw_move_flag[2] = 1;
			global_work_.sw_move_flag[3] = 1;
			global_work_.sw_move_flag[4] = 1;
			global_work_.sw_move_flag[5] = 1;
			global_work_.sw_move_flag[6] = 1;
			global_work_.sw_move_flag[7] = 1;
		}
		else if (global_work_.Scenario_enable >> 4 == 4)
		{
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
			global_work_.sw_move_flag[14] = 1;
		}
		byte b = 1;
		byte b2 = 1;
		switch (global_work_.scenario)
		{
		case 0:
		case 1:
			b = 1;
			b2 += global_work_.scenario;
			break;
		case 2:
		case 3:
		case 4:
		case 5:
		case 6:
		case 7:
			b = 2;
			b2 += (byte)(global_work_.scenario - 2);
			break;
		case 8:
		case 9:
		case 10:
		case 11:
		case 12:
		case 13:
			b = 3;
			b2 += (byte)(global_work_.scenario - 8);
			break;
		case 14:
		case 15:
		case 16:
		case 17:
		case 18:
		case 19:
		case 20:
		case 21:
			b = 4;
			b2 += (byte)(global_work_.scenario - 14);
			break;
		}
		if (global_work_.Scenario_enable < b << 4)
		{
			global_work_.Scenario_enable = (byte)(b << 4);
		}
		if ((global_work_.Scenario_enable >> 4 > b || (global_work_.Scenario_enable & 0xF) > b2) && activeMessageWork.now_no > 127)
		{
			activeMessageWork.status |= MessageSystem.Status.READ_MESSAGE;
		}
	}
}
