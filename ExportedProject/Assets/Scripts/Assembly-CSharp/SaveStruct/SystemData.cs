using System;

namespace SaveStruct
{
	[Serializable]
	public struct SystemData
	{
		public int save_ver;

		public SaveSlotData slot_data_;

		public OpenScenarioData sce_data_;

		public OptionWork option_work_;

		public TrophyWork trophy_work_;

		public ReserveWork reserve_work_;

		public static void New(out SystemData system_data)
		{
			system_data = default(SystemData);
			system_data.save_ver = 4096;
			OpenScenarioData.New(out system_data.sce_data_);
			OptionWork.New(out system_data.option_work_);
			TrophyWork.New(out system_data.trophy_work_);
			SaveSlotData.New(out system_data.slot_data_);
			ReserveWork.New(out system_data.reserve_work_);
		}

		public void CopyFromStatic()
		{
			save_ver = GSStatic.save_ver;
			sce_data_.CopyFrom(GSStatic.open_sce_);
			option_work_.CopyFrom(GSStatic.option_work);
			trophy_work_.CopyFrom(GSStatic.trophy_data);
			slot_data_.CopyFrom(GSStatic.save_data_temp);
			reserve_work_.CopyFrom(GSStatic.reserve_data);
		}

		public void CopyToStatic()
		{
			GSStatic.save_ver = save_ver;
			sce_data_.CopyTo(GSStatic.open_sce_);
			option_work_.CopyTo(GSStatic.option_work);
			trophy_work_.CopyTo(GSStatic.trophy_data);
			slot_data_.CopyTo(GSStatic.save_data_temp);
			reserve_work_.CopyTo(GSStatic.reserve_data);
		}
	}
}
