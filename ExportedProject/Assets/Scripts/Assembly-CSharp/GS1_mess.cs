public static class GS1_mess
{
	public static void GS1_CheckScenario(byte no)
	{
		GlobalWork global_work_ = GSStatic.global_work_;
		MessageWork activeMessageWork = MessageSystem.GetActiveMessageWork();
		if (global_work_.scenario >= 3)
		{
			global_work_.sw_move_flag[2] = 1;
		}
		if (global_work_.Scenario_enable >= 95)
		{
			for (int i = 0; i < global_work_.sw_move_flag.Length; i++)
			{
				global_work_.sw_move_flag[i] = 1;
			}
		}
		else if (global_work_.Scenario_enable >= 80)
		{
			for (int j = 0; j < 21; j++)
			{
				global_work_.sw_move_flag[j] = 1;
			}
		}
		byte b = 1;
		byte b2 = 1;
		switch (no)
		{
		case 0:
			b = 1;
			b2 += global_work_.scenario;
			break;
		case 1:
		case 2:
		case 3:
		case 4:
			b = 2;
			b2 += (byte)(global_work_.scenario - 1);
			break;
		case 5:
		case 6:
		case 7:
		case 8:
		case 9:
		case 10:
			b = 3;
			b2 += (byte)(global_work_.scenario - 5);
			break;
		case 11:
		case 12:
		case 13:
		case 14:
		case 15:
		case 16:
			global_work_.sw_move_flag[3] = 1;
			b = 4;
			b2 += (byte)(global_work_.scenario - 11);
			break;
		case 17:
		case 18:
			b = 5;
			b2 = 0;
			break;
		case 19:
		case 20:
			b = 5;
			b2 = 1;
			goto case 21;
		case 21:
			b = 5;
			b2 = 2;
			break;
		case 22:
		case 23:
		case 24:
			b = 5;
			b2 = 3;
			break;
		case 25:
			b = 5;
			b2 = 4;
			break;
		case 26:
		case 27:
			b = 5;
			b2 = 5;
			break;
		case 28:
		case 29:
		case 30:
			b = 5;
			b2 = 6;
			break;
		case 31:
			b = 5;
			b2 = 7;
			break;
		case 32:
			b = 5;
			b2 = 8;
			break;
		case 33:
		case 34:
			b = 5;
			b2 = 9;
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
