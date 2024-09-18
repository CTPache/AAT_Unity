using System;

[Serializable]
public class OpenScenarioData
{
	public byte GS1_Scenario_enable;

	public byte GS2_Scenario_enable;

	public byte GS3_Scenario_enable;

	public OpenScenarioData()
	{
		init();
	}

	public void CurrentSave(SaveSys save_sys, TitleId title)
	{
		if (save_sys.Scenario_enable == 0)
		{
			save_sys.Scenario_enable = 16;
		}
		switch (title)
		{
		case TitleId.GS1:
			GS1_Scenario_enable = save_sys.Scenario_enable;
			break;
		case TitleId.GS2:
			GS2_Scenario_enable = save_sys.Scenario_enable;
			break;
		case TitleId.GS3:
			GS3_Scenario_enable = save_sys.Scenario_enable;
			break;
		}
	}

	public void OpenScenarioSet(SaveSys save_sys, GlobalWork global_work)
	{
		switch (global_work.title)
		{
		case TitleId.GS1:
			save_sys.Scenario_enable = GS1_Scenario_enable;
			break;
		case TitleId.GS2:
			save_sys.Scenario_enable = GS2_Scenario_enable;
			break;
		case TitleId.GS3:
			save_sys.Scenario_enable = GS3_Scenario_enable;
			break;
		}
		global_work.Scenario_enable = save_sys.Scenario_enable;
	}

	public void init()
	{
		GS1_Scenario_enable = 16;
		GS2_Scenario_enable = 16;
		GS3_Scenario_enable = 16;
	}
}
