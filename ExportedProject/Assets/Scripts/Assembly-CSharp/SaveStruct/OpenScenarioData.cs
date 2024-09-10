using System;

namespace SaveStruct
{
	[Serializable]
	public struct OpenScenarioData
	{
		public byte GS1_Scenario_enable;

		public byte GS2_Scenario_enable;

		public byte GS3_Scenario_enable;

		public static void New(out OpenScenarioData sce_data)
		{
			sce_data = default(OpenScenarioData);
		}

		public void CopyFrom(global::OpenScenarioData src)
		{
			GS1_Scenario_enable = src.GS1_Scenario_enable;
			GS2_Scenario_enable = src.GS2_Scenario_enable;
			GS3_Scenario_enable = src.GS3_Scenario_enable;
		}

		public void CopyTo(global::OpenScenarioData dest)
		{
			dest.GS1_Scenario_enable = GS1_Scenario_enable;
			dest.GS2_Scenario_enable = GS2_Scenario_enable;
			dest.GS3_Scenario_enable = GS3_Scenario_enable;
		}
	}
}
