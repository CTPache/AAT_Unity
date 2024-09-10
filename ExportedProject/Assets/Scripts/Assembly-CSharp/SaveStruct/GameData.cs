using System;

namespace SaveStruct
{
	[Serializable]
	public struct GameData
	{
		public int title_id;

		public GlobalWork global_work_;

		public MessageWork message_work_;

		public SubWindow sub_window_;

		public BgWork bg_work_;

		public RecordList record_list_;

		public MenuWork menu_work_;

		public ObjWork obj_work_;

		public MessageData msg_data_;

		public TanteiWork tantei_work_;

		public InspectWork inspect_work_;

		public TalkWork talk_work_;

		public MovieWork movie_work_;

		public CinemaWork cinema_work_;

		public ExplCharWork expl_char_work_;

		public SoundWork sound_work_;

		public GameReserveWork game_reserve_work_;

		public static void New(out GameData game_data)
		{
			game_data = default(GameData);
			GlobalWork.New(out game_data.global_work_);
			MessageWork.New(out game_data.message_work_);
			SubWindow.New(out game_data.sub_window_);
			BgWork.New(out game_data.bg_work_);
			RecordList.New(out game_data.record_list_);
			MenuWork.New(out game_data.menu_work_);
			ObjWork.New(out game_data.obj_work_);
			MessageData.New(out game_data.msg_data_);
			TanteiWork.New(out game_data.tantei_work_);
			InspectWork.New(out game_data.inspect_work_);
			TalkWork.New(out game_data.talk_work_);
			MovieWork.New(out game_data.movie_work_);
			CinemaWork.New(out game_data.cinema_work_);
			ExplCharWork.New(out game_data.expl_char_work_);
			SoundWork.New(out game_data.sound_work_);
			GameReserveWork.New(out game_data.game_reserve_work_);
		}

		public void CopyFromStatic()
		{
			title_id = (int)GSStatic.global_work_.title;
			global_work_.CopyFrom(GSStatic.global_work_);
			message_work_.CopyFrom(GSStatic.message_work_);
			sub_window_.CopyFrom(advCtrl.instance.sub_window_);
			bg_work_.CopyFrom(GSStatic.bg_save_data);
			record_list_.CopyFrom(recordListCtrl.instance);
			menu_work_.CopyFrom();
			obj_work_.CopyFrom();
			msg_data_.CopyFrom(GSStatic.msg_save_data);
			tantei_work_.CopyFrom(GSStatic.tantei_work_);
			inspect_work_.CopyFrom(GSStatic.inspect_work_);
			talk_work_.CopyFrom(GSStatic.talk_work_);
			movie_work_.CopyFrom(GSStatic.movie_work_);
			cinema_work_.CopyFrom(GSStatic.cinema_work_);
			expl_char_work_.CopyFrom(GSStatic.expl_char_work_);
			sound_work_.CopyFrom(GSStatic.sound_save_data);
			game_reserve_work_.CopyFrom(GSStatic.game_reserve_data);
		}

		public void CopyToStatic()
		{
			GSStatic.global_work_.title = (TitleId)title_id;
			global_work_.CopyTo(GSStatic.global_work_);
			message_work_.CopyTo(GSStatic.message_work_);
			sub_window_.CopyTo(advCtrl.instance.sub_window_);
			bg_work_.CopyTo(GSStatic.bg_save_data);
			record_list_.CopyTo(GSStatic.record_list);
			menu_work_.CopyTo(GSStatic.menu_work);
			obj_work_.CopyTo(GSStatic.obj_work_[1]);
			msg_data_.CopyTo(GSStatic.msg_save_data);
			tantei_work_.CopyTo(GSStatic.tantei_work_);
			inspect_work_.CopyTo(GSStatic.inspect_work_);
			talk_work_.CopyTo(GSStatic.talk_work_);
			movie_work_.CopyTo(GSStatic.movie_work_);
			cinema_work_.CopyTo(GSStatic.cinema_work_);
			expl_char_work_.CopyTo(GSStatic.expl_char_work_);
			sound_work_.CopyTo(GSStatic.sound_save_data);
		}
	}
}
