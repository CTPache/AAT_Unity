using SaveStruct;

public static class GSStatic
{
    public static int save_ver = 0;

    public static GlobalWork global_work_ = new GlobalWork();

    public static BgWork bg_work_ = new BgWork();

    public static MessageWork message_work_ = new MessageWork();

    public static MessageWork message_work_2_ = new MessageWork();

    public static MdtData[] mdt_datas_ = new MdtData[2];

    public static StatusWork status_work_ = new StatusWork();

    public static MovieWork movie_work_ = new MovieWork();

    public static CinemaWork cinema_work_ = new CinemaWork();

    public static RotBGWork rot_bg_work_ = new RotBGWork();

    public static SaibanWork saiban_work_ = new SaibanWork();

    public static TanteiWork tantei_work_ = new TanteiWork();

    public static InspectWork inspect_work_ = new InspectWork();

    public static ExplCharWork expl_char_work_ = new ExplCharWork();

    public static TalkWork talk_work_ = new TalkWork();

    public static ushort[] talk_psy_data_ = new ushort[20];

    private const int MAX_OBJ_NUM = 32;

    public static ObjWork[] obj_work_ = new ObjWork[32];

    public static FURIKO_WK furiko_wk_ = new FURIKO_WK();

    public static OP05_WK op5_wk_ = new OP05_WK();

    public static EffectWork[] effect_work_ = new EffectWork[16];

    public static SaveSys save_sys_ = new SaveSys();

    public static OpenScenarioData open_sce_ = new OpenScenarioData();

    public static BgSaveData bg_save_data = new BgSaveData();

    public static MenuWork menu_work = new MenuWork();

    public static MessageSaveData msg_save_data = new MessageSaveData();

    public static OptionWork option_work = new OptionWork();

    public static TrophyData trophy_data = new TrophyData();

    public static ReserveData reserve_data = new ReserveData();

    public static RecordList record_list = new RecordList();

    public static TrophyManager trophy_manager = new TrophyManager();

    public static SoundSaveData sound_save_data = new SoundSaveData();

    public static GameReserveData game_reserve_data = new GameReserveData();

    public static string save_slot_language_ = "JAPAN";

    public static SaveData[] save_data;

    public static SaveData[] save_data_temp;

    public static GameData[] game_data_temp_;

    public static INSPECT_DATA[] inspect_data_
    {
        get
        {
            return inspect_work_.inspect_data_;
        }
    }

    public static void ClearDemo_buff()
    {
        furiko_wk_.Clear();
        op5_wk_.Clear();
    }

    public static void init()
    {
        Language.Init();
        save_data = new SaveData[Language.languages.Count];
        save_data_temp = new SaveData[Language.languages.Count * 10];
        game_data_temp_ = new GameData[1000];
        global_work_.init();
        bg_work_.init();
        message_work_.init();
        message_work_2_.init();
        status_work_.init();
        movie_work_.init();
        cinema_work_.init();
        rot_bg_work_.init();
        saiban_work_.init();
        tantei_work_.init();
        inspect_work_.init();
        expl_char_work_.init();
        talk_work_.init();
        furiko_wk_.init();
        op5_wk_.init();
        save_sys_.init();
        open_sce_.init();
        bg_save_data.init();
        menu_work.init();
        msg_save_data.init();
        option_work.init();
        trophy_data.init();
        record_list.init();
        sound_save_data.init();
        for (int i = 0; i < talk_psy_data_.Length; i++)
        {
            talk_psy_data_[i] = 0;
        }
        for (int j = 0; j < save_data.Length; j++)
        {
            save_data[j].init();
        }
        for (int k = 0; k < save_data_temp.Length; k++)
        {
            save_data_temp[k].init();
        }
    }
}
