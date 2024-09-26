using UnityEngine;

public static class GSScenario
{
    public delegate void sce_proc(GlobalWork global_work);

    public delegate void sce_init(GlobalWork global_work, bool is_reset);

    public static EXPL_CK_DATA[] GS1_expl_ck_data_tbl;

    private const uint SHOUZOKU_HALL_HOSEI_X = 10u;

    private const uint SHOUZOKU_HALL_HOSEI_Y = 0u;

    private const uint SHOUZOKU_TAMOTO_HOSEI_W = 14u;

    private const uint SHOUZOKU_TAMOTO_HOSEI_H = 8u;

    public static EXPL_CK_DATA[] GS2_expl_ck_data_tbl;

    private const int DATE_OFF_X = -15;

    private const int DATE_OFF_Y = -10;

    public static EXPL_CK_DATA[] GS3_expl_ck_data_tbl;

    private static readonly string[] GS1_gameover_message_data_table;

    private static readonly string[] GS2_gameover_message_data_table;

    private static readonly string[] GS3_gameover_message_data_table;

    private static readonly string[] GS1_scenario_mdt_path_table;

    private static readonly string[] GS2_scenario_mdt_path_table;

    private static readonly string[] GS3_scenario_mdt_path_table;

    private static readonly byte[] GS1_scenario_part_data;

    private static readonly byte[] GS2_scenario_part_data;

    private static readonly byte[] GS3_scenario_part_data;

    private static readonly uint[][] GS1_Note_init_data;

    private static readonly uint[][] GS2_Note_init_data;

    private static readonly uint[][] GS3_Note_init_data;

    private static readonly MUJYUN_CK_DATA[][] GS1_Mujyun_ck_data_tbl;

    private static readonly MUJYUN_CK_DATA[][] GS2_Mujyun_ck_data_tbl;

    private static readonly MUJYUN_CK_DATA[][] GS3_Mujyun_ck_data_tbl;

    private static readonly SHOW_DATA[][] GS1_Tantei_show_data_tbl;

    private static readonly SHOW_DATA[][] GS2_Tantei_show_data_tbl;

    private static readonly SHOW_DATA[][] GS3_Tantei_show_data_tbl;

    private static readonly sce_init[] GS1_Sce_init_proc_tbl;

    private static readonly sce_init[] GS2_Sce_init_proc_tbl;

    private static readonly sce_init[] GS3_Sce_init_proc_tbl;

    private static readonly sce_init[] GS1_Sce_room_init_tbl;

    private static readonly sce_init[] GS2_Sce_room_init_tbl;

    private static readonly sce_init[] GS3_Sce_room_init_tbl;

    private static readonly sce_proc[] GS1_Sce_loop_proc_tbl;

    private static readonly sce_proc[] GS2_Sce_loop_proc_tbl;

    private static readonly sce_proc[] GS3_Sce_loop_proc_tbl;

    static GSScenario()
    {
        GS1_expl_ck_data_tbl = new EXPL_CK_DATA[13]
        {
            new EXPL_CK_DATA(new GSPoint4(scenario.BATD_X + 119, scenario.BATD_Y + 44 + 16, scenario.BATD_X + 119 + 23, scenario.BATD_Y + 44 + 16, scenario.BATD_X + 119 + 23, scenario.BATD_Y + 44 + 16 + 23, scenario.BATD_X + 119, scenario.BATD_Y + 44 + 16 + 23), new GSPoint4(scenario.BATD_X + 120, scenario.BATD_Y + 73 + 16, scenario.BATD_X + 120 + 23, scenario.BATD_Y + 73 + 16, scenario.BATD_X + 120 + 23, scenario.BATD_Y + 73 + 16 + 23, scenario.BATD_X + 120, scenario.BATD_Y + 73 + 16 + 23), scenario.SC1_06370, scenario.SC1_07230, scenario.SC1_06360, 0u, new ushort[4] { 8, 16, 255, 192 }),
            new EXPL_CK_DATA(new GSPoint4(185, 20, 242, 20, 242, 42, 185, 42), new GSPoint4(158, 58, 192, 58, 212, 188, 136, 196), scenario.SC2_15600, scenario.SC2_15590, scenario.SC2_15580, 0u, new ushort[4] { 8, 16, 255, 192 }),
            new EXPL_CK_DATA(new GSPoint4(scenario.BATD_X + 65, scenario.BATD_Y + 116 + 5, scenario.BATD_X + 144, scenario.BATD_Y + 116 + 5, scenario.BATD_X + 144, scenario.BATD_Y + 195 + 5, scenario.BATD_X + 65, scenario.BATD_Y + 195 + 5), new GSPoint4(0, 0, 1, 0, 1, 1, 0, 1), scenario.SC2_15640, scenario.SC2_15620, scenario.SC2_15620, 0u, new ushort[4] { 8, 16, 255, 192 }),
            new EXPL_CK_DATA(new GSPoint4(150, 104, 179, 106, 178, 127, 160, 125), new GSPoint4(scenario.BATD_X + 175, scenario.BATD_Y + 20, scenario.BATD_X + 210, scenario.BATD_Y + 20, scenario.BATD_X + 240, scenario.BATD_Y + 200, scenario.BATD_X + 165, scenario.BATD_Y + 200), scenario.SC3_02410, scenario.SC3_02390, scenario.SC3_02400, 0u, new ushort[4] { 8, 16, 255, 192 }),
            new EXPL_CK_DATA(new GSPoint4(scenario.BATD_X + 67, scenario.BATD_Y + 43, scenario.BATD_X + 95, scenario.BATD_Y + 43, scenario.BATD_X + 95, scenario.BATD_Y + 70, scenario.BATD_X + 67, scenario.BATD_Y + 70), new GSPoint4(scenario.BATD_X + 112, scenario.BATD_Y + 14, scenario.BATD_X + 145, scenario.BATD_Y + 14, scenario.BATD_X + 145, scenario.BATD_Y + 35, scenario.BATD_X + 112, scenario.BATD_Y + 35), scenario.SC3_04900, scenario.SC3_04870, scenario.SC3_04880, 0u, new ushort[4] { 8, 16, 255, 192 }),
            new EXPL_CK_DATA(new GSPoint4(scenario.BATD_X + 135, scenario.BATD_Y + 6, scenario.BATD_X + 168, scenario.BATD_Y + 6, scenario.BATD_X + 168, scenario.BATD_Y + 42, scenario.BATD_X + 135, scenario.BATD_Y + 32), new GSPoint4(scenario.BATD_X, scenario.BATD_Y + 70, scenario.BATD_X + 22, scenario.BATD_Y + 62, scenario.BATD_X + 160, scenario.BATD_Y + 210, scenario.BATD_X, scenario.BATD_Y + 210), scenario.SC3_06720, scenario.SC3_06700, scenario.SC3_06710, 0u, new ushort[4] { 8, 16, 255, 192 }),
            new EXPL_CK_DATA(new GSPoint4(scenario.BATD_X + 135, scenario.BATD_Y + 6, scenario.BATD_X + 168, scenario.BATD_Y + 6, scenario.BATD_X + 168, scenario.BATD_Y + 42, scenario.BATD_X + 135, scenario.BATD_Y + 32), new GSPoint4(scenario.BATD_X, scenario.BATD_Y + 70, scenario.BATD_X + 22, scenario.BATD_Y + 62, scenario.BATD_X + 160, scenario.BATD_Y + 210, scenario.BATD_X, scenario.BATD_Y + 210), scenario.SC3_06520, scenario.SC3_06500, scenario.SC3_06510, 0u, new ushort[4] { 8, 16, 255, 192 }),
            new EXPL_CK_DATA(new GSPoint4(7, 50, 48, 50, 48, 102, 7, 102), new GSPoint4(7, 50, 117, 50, 117, 205, 7, 205), 163u, 162u, 160u, 0u, new ushort[4] { 8, 16, 255, 192 }),
            new EXPL_CK_DATA(new GSPoint4(scenario.BATD_X, scenario.BATD_Y, scenario.BATD_X + 240, scenario.BATD_Y, scenario.BATD_X + 240, scenario.BATD_Y + 47, scenario.BATD_X, scenario.BATD_Y + 47), new GSPoint4(scenario.BATD_X, scenario.BATD_Y + 48, scenario.BATD_X + 240, scenario.BATD_Y + 48, scenario.BATD_X + 240, scenario.BATD_Y + 95, scenario.BATD_X, scenario.BATD_Y + 95), 189u, 184u, 184u, 0u, new ushort[4] { 8, 16, 255, 192 }),
            new EXPL_CK_DATA(new GSPoint4(scenario.BATD_X, scenario.BATD_Y, scenario.BATD_X + 240, scenario.BATD_Y, scenario.BATD_X + 240, scenario.BATD_Y + 47, scenario.BATD_X, scenario.BATD_Y + 47), new GSPoint4(scenario.BATD_X, scenario.BATD_Y + 48, scenario.BATD_X + 240, scenario.BATD_Y + 48, scenario.BATD_X + 240, scenario.BATD_Y + 95, scenario.BATD_X, scenario.BATD_Y + 95), 186u, 184u, 184u, 0u, new ushort[4] { 8, 16, 255, 192 }),
            new EXPL_CK_DATA(new GSPoint4(175, 180, 190, 192, 185, 212, 165, 200), new GSPoint4(0, 20, 255, 20, 255, 230, 0, 230), 192u, 191u, 191u, 0u, new ushort[4] { 8, 16, 255, 192 }),
            new EXPL_CK_DATA(new GSPoint4(220, 41, 242, 41, 242, 64, 220, 64), new GSPoint4(8, 20, 260, 20, 260, 220, 8, 220), 166u, 165u, 165u, 0u, new ushort[4] { 8, 16, 255, 192 }),
            new EXPL_CK_DATA(new GSPoint4(93, 25, 135, 48, 127, 56, 93, 46), new GSPoint4(0, 20, 255, 20, 255, 230, 0, 230), scenario.SC4_68320, scenario.SC4_68310, scenario.SC4_68310, 0u, new ushort[4] { 8, 16, 255, 192 })
        };
        GS2_expl_ck_data_tbl = new EXPL_CK_DATA[8]
        {
            new EXPL_CK_DATA(new GSPoint4(192u, 82u, 199u, 94u, 194u, 97u, 182u, 89u), new GSPoint4(70, 57, 103, 78, 133, 107, 82, 96), scenario_GS2.SC1_04170, scenario_GS2.SC1_04150, scenario_GS2.SC1_04160, 0u, new ushort[4] { 8, 16, 255, 192 }),
            new EXPL_CK_DATA(new GSPoint4(192, 90, 217, 98, 212, 107, 187, 99), new GSPoint4(224, 20, 243, 20, 243, 112, 224, 112), scenario_GS2.SC1_04370, scenario_GS2.SC1_04365, scenario_GS2.SC1_04360, 0u, new ushort[4] { 8, 16, 255, 192 }),
            new EXPL_CK_DATA(new GSPoint4(211, 74, 219, 96, 184, 107, 161, 88), new GSPoint4(53, 10, 104, 10, 93, 42, 66, 36), scenario_GS2.SC1_04840, scenario_GS2.SC1_04830, scenario_GS2.SC1_04850, 0u, new ushort[4] { 8, 16, 255, 192 }),
            new EXPL_CK_DATA(new GSPoint4(36, 17, 76, 17, 76, 86, 36, 86), new GSPoint4(0, 0, 0, 0, 0, 0, 0, 0), scenario_GS2.SC1_08190, scenario_GS2.SC1_08180, scenario_GS2.SC1_08180, 0u, new ushort[4] { 8, 16, 255, 192 }),
            new EXPL_CK_DATA(new GSPoint4(224, 20, 243, 20, 243, 112, 224, 112), new GSPoint4(0, 0, 0, 0, 0, 0, 0, 0), scenario_GS2.SC1_08225, scenario_GS2.SC1_08210, scenario_GS2.SC1_08210, 0u, new ushort[4] { 8, 16, 255, 192 }),
            new EXPL_CK_DATA(new GSPoint4(180, 26, 201, 26, 201, 93, 180, 93), new GSPoint4(119, 93, 201, 93, 201, 115, 119, 115), scenario_GS2.SC1_05190, scenario_GS2.SC1_05190, scenario_GS2.SC1_05180, 0u, new ushort[4] { 8, 16, 255, 192 }),
            new EXPL_CK_DATA(new GSPoint4(218, 53, 265, 53, 265, 90, 218, 90), new GSPoint4(166, 54, 214, 54, 214, 93, 166, 93), scenario_GS2.SC2_13920, scenario_GS2.SC2_13900, scenario_GS2.SC2_13910, 0u, new ushort[4] { 8, 16, 255, 192 }),
            new EXPL_CK_DATA(new GSPoint4(69, 100, 119, 120, 129, 146, 56, 118), new GSPoint4(130, 10, 153, 10, 119, 120, 69, 100), scenario_GS2.SC3_20340, scenario_GS2.SC3_20310, scenario_GS2.SC3_20320, 0u, new ushort[4] { 8, 16, 255, 192 })
        };
        GS3_expl_ck_data_tbl = new EXPL_CK_DATA[13]
        {
            new EXPL_CK_DATA(new GSPoint4(100, 10, 110, 10, 105, 86, 95, 86), new GSPoint4(0, 0, 1, 0, 1, 1, 0, 1), scenario_GS3.SC0_0_30510, scenario_GS3.SC0_0_30500, scenario_GS3.SC0_0_30500, 0u, new ushort[4] { 8, 16, 255, 192 }),
            new EXPL_CK_DATA(new GSPoint4(80, 65, 95, 65, 100, 84, 79, 88), new GSPoint4(0, 0, 1, 0, 1, 1, 0, 1), scenario_GS3.SC1_1_34120, scenario_GS3.SC1_1_34110, scenario_GS3.SC1_1_34110, 0u, new ushort[4] { 8, 16, 255, 192 }),
            new EXPL_CK_DATA(new GSPoint4(155, 60, 262, 45, 262, 120, 155, 130), new GSPoint4(7, 23, 66, 23, 66, 42, 7, 42), scenario_GS3.SC1_3_38040, scenario_GS3.SC1_3_38015, scenario_GS3.SC1_3_38030, 0u, new ushort[4] { 8, 16, 255, 192 }),
            new EXPL_CK_DATA(new GSPoint4(7, 23, 66, 23, 66, 42, 7, 42), new GSPoint4(59, 24, 120, 42, 119, 182, 40, 182), scenario_GS3.SC1_3_38120, scenario_GS3.SC1_3_38110, scenario_GS3.SC1_3_38130, 0u, new ushort[4] { 8, 16, 255, 192 }),
            new EXPL_CK_DATA(new GSPoint4(1, 31, 18, 31, 11, 102, 1, 102), new GSPoint4(0, 0, 1, 0, 1, 1, 0, 1), scenario_GS3.SC2_3_44480, scenario_GS3.SC2_3_44470, scenario_GS3.SC2_3_44470, 0u, new ushort[4] { 8, 16, 255, 192 }),
            new EXPL_CK_DATA(new GSPoint4(2, 6, 70, 6, 70, 124, 2, 124), new GSPoint4(0, 0, 1, 0, 1, 1, 0, 1), scenario_GS3.SC2_3_44825, scenario_GS3.SC2_3_44900, scenario_GS3.SC2_3_44900, 0u, new ushort[4] { 8, 16, 255, 192 }),
            new EXPL_CK_DATA(new GSPoint4(142, 100, 210, 83, 205, 128, 131, 131), new GSPoint4(0, 0, 1, 0, 1, 1, 0, 1), scenario_GS3.SC3_0_46490, scenario_GS3.SC3_0_46480, scenario_GS3.SC3_0_46480, 0u, new ushort[4] { 8, 16, 255, 192 }),
            new EXPL_CK_DATA(new GSPoint4(142, 37, 164, 37, 164, 57, 142, 57), new GSPoint4(51, 25, 80, 25, 80, 39, 51, 39), scenario_GS3.SC3_0_47060, scenario_GS3.SC3_0_47040, scenario_GS3.SC3_0_47050, 0u, new ushort[4] { 8, 16, 255, 192 }),
            new EXPL_CK_DATA(new GSPoint4(132, 32, 183, 32, 167, 68, 149, 68), new GSPoint4(0, 0, 1, 0, 1, 1, 0, 1), scenario_GS3.SC4_0_49660, scenario_GS3.SC4_0_49650, scenario_GS3.SC4_0_49650, 0u, new ushort[4] { 8, 16, 255, 192 }),
            new EXPL_CK_DATA(new GSPoint4(220, 104, 273, 104, 258, 141, 234, 141), new GSPoint4(0, 0, 1, 0, 1, 1, 0, 1), 356u, 250u, 250u, 0u, new ushort[4] { 8, 16, 255, 192 }),
            new EXPL_CK_DATA(new GSPoint4(5, 0, 28, 0, 157, 96, 148, 96), new GSPoint4(154, 18, 175, 12, 206, 42, 186, 59), scenario_GS3.SC4_3_56070, scenario_GS3.SC4_3_56010, scenario_GS3.SC4_3_56020, 0u, new ushort[4] { 8, 16, 255, 192 }),
            new EXPL_CK_DATA(new GSPoint4(0, 0, 1, 0, 1, 1, 0, 1), new GSPoint4(0, 0, 1, 0, 1, 1, 0, 1), scenario_GS3.SC4_0_49650, scenario_GS3.SC4_0_49650, scenario_GS3.SC4_0_49650, 0u, new ushort[4] { 8, 16, 255, 192 }),
            new EXPL_CK_DATA(new GSPoint4(194, 96, 315, 0, 335, 0, 206, 96), new GSPoint4(0, 0, 1, 0, 1, 1, 0, 1), scenario_GS3.SC4_3_56070, scenario_GS3.SC4_3_56020, scenario_GS3.SC4_3_56020, 0u, new ushort[4] { 8, 16, 255, 192 })
        };
        GS1_scenario_part_data = new byte[35]
        {
            4, 5, 4, 5, 4, 5, 4, 5, 4, 5,
            4, 5, 4, 5, 4, 5, 4, 5, 5, 4,
            4, 4, 5, 5, 5, 4, 4, 4, 5, 5,
            5, 4, 4, 4, 4
        };
        GS2_scenario_part_data = new byte[22]
        {
            4, 4, 5, 4, 4, 5, 4, 4, 5, 4,
            4, 5, 4, 4, 5, 5, 4, 4, 5, 5,
            4, 4
        };
        GS3_scenario_part_data = new byte[23]
        {
            4, 4, 5, 4, 5, 4, 4, 5, 4, 5,
            4, 4, 4, 4, 5, 5, 4, 4, 5, 5,
            4, 4, 4
        };
        GS1_gameover_message_data_table = new string[35]
        {
            "SC0_GAMEOVER", null, "SC1_1_GAMEOVER", null, "SC1_3_GAMEOVER", null, "SC2_1_GAMEOVER", null, "SC2_3_GAMEOVER", null,
            "SC2_5_GAMEOVER", null, "SC3_1_GAMEOVER", null, "SC3_3_GAMEOVER", null, "SC3_5_GAMEOVER", null, null, "SC4_1A_GAMEOVER",
            "SC4_1B_GAMEOVER", "SC4_1C_GAMEOVER", null, null, null, "SC4_3A_GAMEOVER", "SC4_3B_GAMEOVER", "SC4_3C_GAMEOVER", null, null,
            null, "SC4_5A_GAMEOVER", "SC4_5B_GAMEOVER", "SC4_5C_GAMEOVER", null
        };
        GS2_gameover_message_data_table = new string[22]
        {
            "SC0_0_GAMEOVER", "SC0_1_GAMEOVER", null, "SC1_1_0_GAMEOVER", "SC1_1_1_GAMEOVER", null, "SC1_3_0_GAMEOVER", "SC1_3_1_GAMEOVER", null, "SC2_1_0_GAMEOVER",
            "SC2_1_1_GAMEOVER", null, "SC2_3_0_GAMEOVER", "SC2_3_1_GAMEOVER", null, null, "SC3_1_0_GAMEOVER", "SC3_1_1_GAMEOVER", null, null,
            "SC3_3_0_GAMEOVER", "SC3_3_1_GAMEOVER"
        };
        GS3_gameover_message_data_table = new string[23]
        {
            "SC0_0_GAMEOVER", "SC0_1_GAMEOVER", null, "SC1_0_GAMEOVER", null, "SC1_3_0_GAMEOVER", "SC1_3_1_GAMEOVER", null, "SC2_1_GAMEOVER", null,
            "SC2_3_0_GAMEOVER", "SC2_3_1_GAMEOVER", "SC3_0_0_GAMEOVER", "SC3_0_1_GAMEOVER", null, null, "SC4_1_0_GAMEOVER", "SC4_1_1_GAMEOVER", null, null,
            "SC4_3_0_GAMEOVER", "SC4_3_1_GAMEOVER", "SC4_3_2_GAMEOVER"
        };
        GS1_scenario_mdt_path_table = new string[36]
        { "GS1/scenario/sc0_text", "GS1/scenario/sc1_0_text", "GS1/scenario/sc1_1_text", "GS1/scenario/sc1_2_text", "GS1/scenario/sc1_3_text", "GS1/scenario/sc2_0_text", "GS1/scenario/sc2_1_text", "GS1/scenario/sc2_2_text", "GS1/scenario/sc2_3_text", "GS1/scenario/sc2_4_text", "GS1/scenario/sc2_5_text", "GS1/scenario/sc3_0_text", "GS1/scenario/sc3_1_text", "GS1/scenario/sc3_2_text", "GS1/scenario/sc3_3_text", "GS1/scenario/sc3_4_text", "GS1/scenario/sc3_5_text", "GS1/scenario/sc4_0a_text", "GS1/scenario/sc4_0b_text", "GS1/scenario/sc4_1a_text", "GS1/scenario/sc4_1b_text", "GS1/scenario/sc4_1c_text", "GS1/scenario/sc4_2a_text", "GS1/scenario/sc4_2b_text", "GS1/scenario/sc4_2c_text", "GS1/scenario/sc4_3a_text", "GS1/scenario/sc4_3b_text", "GS1/scenario/sc4_3c_text", "GS1/scenario/sc4_4a_text", "GS1/scenario/sc4_4b_text", "GS1/scenario/sc4_4c_text", "GS1/scenario/sc4_5a_text", "GS1/scenario/sc4_5b_text", "GS1/scenario/sc4_5c_text", "GS1/scenario/sc4_5d_text", "GS1/scenario/ev0_mes"

       };
        GS2_scenario_mdt_path_table = new string[22]
        {
            "GS2/scenario/sc0_0_text", "GS2/scenario/sc0_1_text", "GS2/scenario/sc1_0_text", "GS2/scenario/sc1_1_0_text", "GS2/scenario/sc1_1_1_text", "GS2/scenario/sc1_2_text", "GS2/scenario/sc1_3_0_text", "GS2/scenario/sc1_3_1_text", "GS2/scenario/sc2_0_text", "GS2/scenario/sc2_1_0_text",            "GS2/scenario/sc2_1_1_text", "GS2/scenario/sc2_2_text", "GS2/scenario/sc2_3_0_text", "GS2/scenario/sc2_3_1_text", "GS2/scenario/sc3_0_0_text", "GS2/scenario/sc3_0_1_text", "GS2/scenario/sc3_1_0_text", "GS2/scenario/sc3_1_1_text", "GS2/scenario/sc3_2_0_text", "GS2/scenario/sc3_2_1_text",            "GS2/scenario/sc3_3_0_text", "GS2/scenario/sc3_3_1_text"
        };
        GS3_scenario_mdt_path_table = new string[23]
        {
            "GS3/scenario/sc0_0_text", "GS3/scenario/sc0_1_text", "GS3/scenario/sc1_0_text", "GS3/scenario/sc1_1_text", "GS3/scenario/sc1_2_text", "GS3/scenario/sc1_3_0_text", "GS3/scenario/sc1_3_1_text", "GS3/scenario/sc2_0_text", "GS3/scenario/sc2_1_text", "GS3/scenario/sc2_2_text","GS3/scenario/sc2_3_0_text", "GS3/scenario/sc2_3_1_text", "GS3/scenario/sc3_0_0_text", "GS3/scenario/sc3_0_1_text", "GS3/scenario/sc4_0_0_text", "GS3/scenario/sc4_0_1_text", "GS3/scenario/sc4_1_0_text", "GS3/scenario/sc4_1_1_text", "GS3/scenario/sc4_2_0_text", "GS3/scenario/sc4_2_1_text","GS3/scenario/sc4_3_0_text", "GS3/scenario/sc4_3_1_text", "GS3/scenario/sc4_3_2_text"
        };
        GS1_Note_init_data = new uint[35][]
        {
            scenario.Sce0_note_init_data,
            scenario.Sce1_0_note_init_data,
            scenario.Sce1_1_note_init_data,
            scenario.Sce1_2_note_init_data,
            scenario.Sce1_3_note_init_data,
            scenario.Sce2_0_note_init_data,
            scenario.Sce2_1_note_init_data,
            scenario.Sce2_2_note_init_data,
            scenario.Sce2_3_note_init_data,
            scenario.Sce2_4_note_init_data,
            scenario.Sce2_5_note_init_data,
            scenario.Sce3_0_note_init_data,
            scenario.Sce3_1_note_init_data,
            scenario.Sce3_2_note_init_data,
            scenario.Sce3_3_note_init_data,
            scenario.Sce3_4_note_init_data,
            scenario.Sce3_5_note_init_data,
            scenario.Sce4_0_note_init_data,
            scenario.Sce4_0_note_init_data,
            scenario.Sce4_1a_note_init_data,
            scenario.Sce4_1b_note_init_data,
            scenario.Sce4_1c_note_init_data,
            scenario.Sce4_2a_note_init_data,
            scenario.Sce4_2b_note_init_data,
            scenario.Sce4_2c_note_init_data,
            scenario.Sce4_3a_note_init_data,
            scenario.Sce4_3b_note_init_data,
            scenario.Sce4_3c_note_init_data,
            scenario.Sce4_4a_note_init_data,
            scenario.Sce4_4b_note_init_data,
            scenario.Sce4_4c_note_init_data,
            scenario.Sce4_5a_note_init_data,
            scenario.Sce4_5b_note_init_data,
            scenario.Sce4_5c_note_init_data,
            scenario.Sce4_5d_note_init_data
        };
        GS2_Note_init_data = new uint[22][]
        {
            scenario_GS2.Sce0_0_note_init_data,
            scenario_GS2.Sce0_1_note_init_data,
            scenario_GS2.Sce1_0_note_init_data,
            scenario_GS2.Sce1_1_0_note_init_data,
            scenario_GS2.Sce1_1_1_note_init_data,
            scenario_GS2.Sce1_2_note_init_data,
            scenario_GS2.Sce1_3_note_init_data,
            scenario_GS2.Sce1_3_note_init_data,
            scenario_GS2.Sce2_0_note_init_data,
            scenario_GS2.Sce2_1_0_note_init_data,
            scenario_GS2.Sce2_1_1_note_init_data,
            scenario_GS2.Sce2_2_note_init_data,
            scenario_GS2.Sce2_3_0_note_init_data,
            scenario_GS2.Sce2_3_1_note_init_data,
            scenario_GS2.Sce3_0_0_note_init_data,
            scenario_GS2.Sce3_0_1_note_init_data,
            scenario_GS2.Sce3_1_0_note_init_data,
            scenario_GS2.Sce3_1_1_note_init_data,
            scenario_GS2.Sce3_2_0_note_init_data,
            scenario_GS2.Sce3_2_1_note_init_data,
            scenario_GS2.Sce3_3_0_note_init_data,
            scenario_GS2.Sce3_3_1_note_init_data
        };
        GS3_Note_init_data = new uint[23][]
        {
            scenario_GS3.Sce0_0_note_init_data,
            scenario_GS3.Sce0_1_note_init_data,
            scenario_GS3.Sce1_0_note_init_data,
            scenario_GS3.Sce1_1_0_note_init_data,
            scenario_GS3.Sce1_2_note_init_data,
            scenario_GS3.Sce1_3_0_note_init_data,
            scenario_GS3.Sce1_3_1_note_init_data,
            scenario_GS3.Sce2_0_note_init_data,
            scenario_GS3.Sce2_1_note_init_data,
            scenario_GS3.Sce2_2_note_init_data,
            scenario_GS3.Sce2_3_0_note_init_data,
            scenario_GS3.Sce2_3_1_note_init_data,
            scenario_GS3.Sce3_0_0_note_init_data,
            scenario_GS3.Sce3_0_1_note_init_data,
            scenario_GS3.Sce4_0_0_note_init_data,
            scenario_GS3.Sce4_0_1_note_init_data,
            scenario_GS3.Sce4_1_0_note_init_data,
            scenario_GS3.Sce4_1_1_note_init_data,
            scenario_GS3.Sce4_2_0_note_init_data,
            scenario_GS3.Sce4_2_1_note_init_data,
            scenario_GS3.Sce4_3_0_note_init_data,
            scenario_GS3.Sce4_3_1_note_init_data,
            scenario_GS3.Sce4_3_2_note_init_data
        };
        GS1_Mujyun_ck_data_tbl = new MUJYUN_CK_DATA[35][]
        {
            scenario.Sce0_mujyun_data,
            scenario.Sce1_1_mujyun_data,
            scenario.Sce1_1_mujyun_data,
            scenario.Sce1_3_mujyun_data,
            scenario.Sce1_3_mujyun_data,
            scenario.Sce2_1_mujyun_data,
            scenario.Sce2_1_mujyun_data,
            scenario.Sce2_3_mujyun_data,
            scenario.Sce2_3_mujyun_data,
            scenario.Sce2_4_mujyun_data,
            scenario.Sce2_5_mujyun_data,
            scenario.Sce3_1_mujyun_data,
            scenario.Sce3_1_mujyun_data,
            scenario.Sce3_2_mujyun_data,
            scenario.Sce3_3_mujyun_data,
            scenario.Sce3_5_mujyun_data,
            scenario.Sce3_5_mujyun_data,
            scenario.Sce4_1a_mujyun_data,
            scenario.Sce4_1a_mujyun_data,
            scenario.Sce4_1a_mujyun_data,
            scenario.Sce4_1b_mujyun_data,
            scenario.Sce4_1c_mujyun_data,
            scenario.Sce4_3a_mujyun_data,
            scenario.Sce4_3a_mujyun_data,
            scenario.Sce4_3a_mujyun_data,
            scenario.Sce4_3a_mujyun_data,
            scenario.Sce4_3b_mujyun_data,
            scenario.Sce4_3c_mujyun_data,
            scenario.Sce4_4a_mujyun_data,
            scenario.Sce4_5a_mujyun_data,
            scenario.Sce4_5a_mujyun_data,
            scenario.Sce4_5a_mujyun_data,
            scenario.Sce4_5b_mujyun_data,
            scenario.Sce4_5c_mujyun_data,
            scenario.Sce4_5d_mujyun_data
        };
        GS2_Mujyun_ck_data_tbl = new MUJYUN_CK_DATA[22][]
        {
            scenario_GS2.Sce0_0_mujyun_data,
            scenario_GS2.Sce0_1_mujyun_data,
            scenario_GS2.Sce1_1_0_mujyun_data,
            scenario_GS2.Sce1_1_0_mujyun_data,
            scenario_GS2.Sce1_1_1_mujyun_data,
            scenario_GS2.Sce1_3_0_mujyun_data,
            scenario_GS2.Sce1_3_0_mujyun_data,
            scenario_GS2.Sce1_3_1_mujyun_data,
            scenario_GS2.Sce2_1_0_mujyun_data,
            scenario_GS2.Sce2_1_0_mujyun_data,
            scenario_GS2.Sce2_1_1_mujyun_data,
            scenario_GS2.Sce2_3_0_mujyun_data,
            scenario_GS2.Sce2_3_0_mujyun_data,
            scenario_GS2.Sce2_3_1_mujyun_data,
            scenario_GS2.Sce3_1_0_mujyun_data,
            scenario_GS2.Sce3_1_1_mujyun_data,
            scenario_GS2.Sce3_1_0_mujyun_data,
            scenario_GS2.Sce3_1_1_mujyun_data,
            scenario_GS2.Sce3_3_0_mujyun_data,
            scenario_GS2.Sce3_3_1_mujyun_data,
            scenario_GS2.Sce3_3_0_mujyun_data,
            scenario_GS2.Sce3_3_1_mujyun_data
        };
        GS3_Mujyun_ck_data_tbl = new MUJYUN_CK_DATA[23][]
        {
            scenario_GS3.Sce0_0_mujyun_data,
            scenario_GS3.Sce0_1_mujyun_data,
            scenario_GS3.Sce1_1_0_mujyun_data,
            scenario_GS3.Sce1_1_0_mujyun_data,
            scenario_GS3.Sce1_3_0_mujyun_data,
            scenario_GS3.Sce1_3_0_mujyun_data,
            scenario_GS3.Sce1_3_1_mujyun_data,
            scenario_GS3.Sce2_1_mujyun_data,
            scenario_GS3.Sce2_1_mujyun_data,
            scenario_GS3.Sce2_3_0_mujyun_data,
            scenario_GS3.Sce2_3_0_mujyun_data,
            scenario_GS3.Sce2_3_1_mujyun_data,
            scenario_GS3.Sce3_0_0_mujyun_data,
            scenario_GS3.Sce3_0_1_mujyun_data,
            scenario_GS3.Sce4_1_0_mujyun_data,
            scenario_GS3.Sce4_1_0_mujyun_data,
            scenario_GS3.Sce4_1_0_mujyun_data,
            scenario_GS3.Sce4_1_1_mujyun_data,
            scenario_GS3.Sce4_3_0_mujyun_data,
            scenario_GS3.Sce4_3_0_mujyun_data,
            scenario_GS3.Sce4_3_0_mujyun_data,
            scenario_GS3.Sce4_3_1_mujyun_data,
            scenario_GS3.Sce4_3_2_mujyun_data
        };
        GS1_Tantei_show_data_tbl = new SHOW_DATA[35][]
        {
            scenario.Sce1_0_show_data,
            scenario.Sce1_0_show_data,
            scenario.Sce1_0_show_data,
            scenario.Sce1_2_show_data,
            scenario.Sce1_0_show_data,
            scenario.Sce2_0_show_data,
            scenario.Sce2_0_show_data,
            scenario.Sce2_2_show_data,
            scenario.Sce2_0_show_data,
            scenario.Sce2_4_show_data,
            scenario.Sce2_0_show_data,
            scenario.Sce3_0_show_data,
            scenario.Sce3_0_show_data,
            scenario.Sce3_2_show_data,
            scenario.Sce3_0_show_data,
            scenario.Sce3_4_show_data,
            scenario.Sce3_0_show_data,
            scenario.Sce4_0_show_data,
            scenario.Sce4_0_show_data,
            scenario.Sce4_0_show_data,
            scenario.Sce4_0_show_data,
            scenario.Sce4_0_show_data,
            scenario.Sce4_2_show_data,
            scenario.Sce4_2_show_data,
            scenario.Sce4_2_show_data,
            scenario.Sce4_2_show_data,
            scenario.Sce4_2_show_data,
            scenario.Sce4_2_show_data,
            scenario.Sce4_4_show_data,
            scenario.Sce4_4_show_data,
            scenario.Sce4_4_show_data,
            scenario.Sce4_4_show_data,
            scenario.Sce4_4_show_data,
            scenario.Sce4_4_show_data,
            scenario.Sce4_4_show_data
        };
        GS2_Tantei_show_data_tbl = new SHOW_DATA[22][]
        {
            scenario_GS2.Sce1_0_show_data,
            scenario_GS2.Sce1_0_show_data,
            scenario_GS2.Sce1_0_show_data,
            scenario_GS2.Sce1_0_show_data,
            scenario_GS2.Sce1_0_show_data,
            scenario_GS2.Sce1_2_show_data,
            scenario_GS2.Sce1_2_show_data,
            scenario_GS2.Sce1_2_show_data,
            scenario_GS2.Sce2_0_show_data,
            scenario_GS2.Sce2_0_show_data,
            scenario_GS2.Sce2_0_show_data,
            scenario_GS2.Sce2_2_show_data,
            scenario_GS2.Sce2_2_show_data,
            scenario_GS2.Sce2_2_show_data,
            scenario_GS2.Sce3_0_0_show_data,
            scenario_GS2.Sce3_0_1_show_data,
            scenario_GS2.Sce3_0_1_show_data,
            scenario_GS2.Sce3_0_1_show_data,
            scenario_GS2.Sce3_2_0_show_data,
            scenario_GS2.Sce3_2_1_show_data,
            scenario_GS2.Sce3_2_1_show_data,
            scenario_GS2.Sce3_2_1_show_data
        };
        GS3_Tantei_show_data_tbl = new SHOW_DATA[23][]
        {
            scenario_GS3.Sce1_0_show_data,
            scenario_GS3.Sce1_0_show_data,
            scenario_GS3.Sce1_0_show_data,
            scenario_GS3.Sce1_2_show_data,
            scenario_GS3.Sce1_2_show_data,
            scenario_GS3.Sce1_2_show_data,
            scenario_GS3.Sce1_2_show_data,
            scenario_GS3.Sce2_0_show_data,
            scenario_GS3.Sce2_0_show_data,
            scenario_GS3.Sce2_2_show_data,
            scenario_GS3.Sce2_2_show_data,
            scenario_GS3.Sce2_2_show_data,
            scenario_GS3.Sce2_2_show_data,
            scenario_GS3.Sce2_2_show_data,
            scenario_GS3.Sce4_0_0_show_data,
            scenario_GS3.Sce4_0_1_show_data,
            scenario_GS3.Sce4_0_1_show_data,
            scenario_GS3.Sce4_0_1_show_data,
            scenario_GS3.Sce4_2_0_show_data,
            scenario_GS3.Sce4_2_1_show_data,
            scenario_GS3.Sce4_2_1_show_data,
            scenario_GS3.Sce4_2_1_show_data,
            scenario_GS3.Sce4_2_1_show_data
        };
        GS1_Sce_init_proc_tbl = new sce_init[35]
        {
            dm_init,
            scenario.Sce1_0_tantei_init,
            dm_init,
            scenario.Sce1_2_tantei_init,
            dm_init,
            scenario.Sce2_0_tantei_init,
            dm_init,
            scenario.Sce2_2_tantei_init,
            dm_init,
            scenario.Sce2_4_tantei_init,
            dm_init,
            scenario.Sce3_0_tantei_init,
            dm_init,
            scenario.Sce3_2_tantei_init,
            dm_init,
            scenario.Sce3_4_tantei_init,
            dm_init,
            scenario.Sce4_0_tantei_init,
            scenario.Sce4_0_tantei_init,
            dm_init,
            dm_init,
            dm_init,
            scenario.Sce4_2_tantei_init,
            scenario.Sce4_2_tantei_init,
            scenario.Sce4_2_tantei_init,
            dm_init,
            dm_init,
            dm_init,
            scenario.Sce4_4_tantei_init,
            scenario.Sce4_4_tantei_init,
            scenario.Sce4_4_tantei_init,
            dm_init,
            dm_init,
            dm_init,
            dm_init
        };
        GS2_Sce_init_proc_tbl = new sce_init[22]
        {
            dm_init,
            dm_init,
            scenario_GS2.Sce1_0_tantei_init,
            dm_init,
            dm_init,
            scenario_GS2.Sce1_2_tantei_init,
            dm_init,
            dm_init,
            scenario_GS2.Sce2_0_tantei_init,
            dm_init,
            dm_init,
            scenario_GS2.Sce2_2_tantei_init,
            dm_init,
            dm_init,
            scenario_GS2.Sce3_0_0_tantei_init,
            scenario_GS2.Sce3_0_1_tantei_init,
            dm_init,
            dm_init,
            scenario_GS2.Sce3_2_0_tantei_init,
            scenario_GS2.Sce3_2_1_tantei_init,
            dm_init,
            dm_init
        };
        GS3_Sce_init_proc_tbl = new sce_init[23]
        {
            dm_init,
            dm_init,
            scenario_GS3.Sce1_0_tantei_init,
            dm_init,
            scenario_GS3.Sce1_2_tantei_init,
            dm_init,
            dm_init,
            scenario_GS3.Sce2_0_tantei_init,
            dm_init,
            scenario_GS3.Sce2_2_tantei_init,
            dm_init,
            dm_init,
            dm_init,
            dm_init,
            scenario_GS3.Sce4_0_0_tantei_init,
            scenario_GS3.Sce4_0_1_tantei_init,
            dm_init,
            dm_init,
            scenario_GS3.Sce4_2_0_tantei_init,
            scenario_GS3.Sce4_2_1_tantei_init,
            dm_init,
            dm_init,
            dm_init
        };
        GS1_Sce_room_init_tbl = new sce_init[35]
        {
            dm_init,
            scenario.Sce1_0_tantei_room_init,
            dm_init,
            scenario.Sce1_2_tantei_room_init,
            dm_init,
            scenario.Sce2_0_tantei_room_init,
            dm_init,
            scenario.Sce2_2_tantei_room_init,
            dm_init,
            scenario.Sce2_4_tantei_room_init,
            dm_init,
            scenario.Sce3_0_tantei_room_init,
            dm_init,
            scenario.Sce3_2_tantei_room_init,
            dm_init,
            scenario.Sce3_4_tantei_room_init,
            dm_init,
            scenario.Sce4_0_tantei_room_init,
            scenario.Sce4_0_tantei_room_init,
            dm_init,
            dm_init,
            dm_init,
            scenario.Sce4_2_tantei_room_init,
            scenario.Sce4_2_tantei_room_init,
            scenario.Sce4_2_tantei_room_init,
            dm_init,
            dm_init,
            dm_init,
            scenario.Sce4_4_tantei_room_init,
            scenario.Sce4_4_tantei_room_init,
            scenario.Sce4_4_tantei_room_init,
            dm_init,
            dm_init,
            dm_init,
            dm_init
        };
        GS2_Sce_room_init_tbl = new sce_init[22]
        {
            dm_init,
            dm_init,
            scenario_GS2.Sce1_0_tantei_room_init,
            dm_init,
            dm_init,
            scenario_GS2.Sce1_2_tantei_room_init,
            dm_init,
            dm_init,
            scenario_GS2.Sce2_0_tantei_room_init,
            dm_init,
            dm_init,
            scenario_GS2.Sce2_2_tantei_room_init,
            dm_init,
            dm_init,
            scenario_GS2.Sce3_0_0_tantei_room_init,
            scenario_GS2.Sce3_0_1_tantei_room_init,
            dm_init,
            dm_init,
            scenario_GS2.Sce3_2_0_tantei_room_init,
            scenario_GS2.Sce3_2_1_tantei_room_init,
            dm_init,
            dm_init
        };
        GS3_Sce_room_init_tbl = new sce_init[23]
        {
            dm_init,
            dm_init,
            scenario_GS3.Sce1_0_tantei_room_init,
            dm_init,
            scenario_GS3.Sce1_2_tantei_room_init,
            dm_init,
            dm_init,
            scenario_GS3.Sce2_0_tantei_room_init,
            dm_init,
            scenario_GS3.Sce2_2_tantei_room_init,
            dm_init,
            dm_init,
            dm_init,
            dm_init,
            scenario_GS3.Sce4_0_0_tantei_room_init,
            scenario_GS3.Sce4_0_1_tantei_room_init,
            dm_init,
            dm_init,
            scenario_GS3.Sce4_2_0_tantei_room_init,
            scenario_GS3.Sce4_2_1_tantei_room_init,
            dm_init,
            dm_init,
            dm_init
        };
        GS1_Sce_loop_proc_tbl = new sce_proc[35]
        {
            dm_proc,
            scenario.Sce1_0_tantei_main,
            dm_proc,
            scenario.Sce1_2_tantei_main,
            dm_proc,
            scenario.Sce2_0_tantei_main,
            dm_proc,
            scenario.Sce2_2_tantei_main,
            dm_proc,
            scenario.Sce2_4_tantei_main,
            dm_proc,
            scenario.Sce3_0_tantei_main,
            dm_proc,
            scenario.Sce3_2_tantei_main,
            dm_proc,
            scenario.Sce3_4_tantei_main,
            dm_proc,
            scenario.Sce4_0_tantei_main,
            scenario.Sce4_0_tantei_main,
            dm_proc,
            dm_proc,
            dm_proc,
            scenario.Sce4_2_tantei_main,
            scenario.Sce4_2_tantei_main,
            scenario.Sce4_2_tantei_main,
            dm_proc,
            dm_proc,
            dm_proc,
            scenario.Sce4_4_tantei_main,
            scenario.Sce4_4_tantei_main,
            scenario.Sce4_4_tantei_main,
            dm_proc,
            dm_proc,
            dm_proc,
            dm_proc
        };
        GS2_Sce_loop_proc_tbl = new sce_proc[22]
        {
            dm_proc,
            dm_proc,
            scenario_GS2.Sce1_0_tantei_main,
            dm_proc,
            dm_proc,
            scenario_GS2.Sce1_2_tantei_main,
            dm_proc,
            dm_proc,
            scenario_GS2.Sce2_0_tantei_main,
            dm_proc,
            dm_proc,
            scenario_GS2.Sce2_2_tantei_main,
            dm_proc,
            dm_proc,
            scenario_GS2.Sce3_0_0_tantei_main,
            scenario_GS2.Sce3_0_1_tantei_main,
            dm_proc,
            dm_proc,
            scenario_GS2.Sce3_2_0_tantei_main,
            scenario_GS2.Sce3_2_1_tantei_main,
            dm_proc,
            dm_proc
        };
        GS3_Sce_loop_proc_tbl = new sce_proc[23]
        {
            dm_proc,
            dm_proc,
            scenario_GS3.Sce1_0_tantei_main,
            dm_proc,
            scenario_GS3.Sce1_2_tantei_main,
            dm_proc,
            dm_proc,
            scenario_GS3.Sce2_0_tantei_main,
            dm_proc,
            scenario_GS3.Sce2_2_tantei_main,
            dm_proc,
            dm_proc,
            dm_proc,
            dm_proc,
            scenario_GS3.Sce4_0_0_tantei_main,
            scenario_GS3.Sce4_0_1_tantei_main,
            dm_proc,
            dm_proc,
            scenario_GS3.Sce4_2_0_tantei_main,
            scenario_GS3.Sce4_2_1_tantei_main,
            dm_proc,
            dm_proc,
            dm_proc
        };
    }

    public static void Set_char(uint id, uint foa0, uint foa1, bool is_reset)
    {
        if (is_reset)
        {
            AnimationSystem.Instance.StopCharacters();
            AnimationSystem.Instance.PlayCharacter((int)GSStatic.global_work_.title, (int)id, (int)foa0, (int)foa1);
            GSStatic.tantei_work_.person_flag = 1;
            GSStatic.obj_work_[1].h_num = (byte)(id & 0x7Fu);
            GSStatic.obj_work_[1].foa = (ushort)foa0;
            GSStatic.obj_work_[1].idlingFOA = (ushort)foa1;
        }
    }

    public static void Set_event(uint mess, uint flag, bool is_reset)
    {
        if (is_reset)
        {
            switch (GSStatic.global_work_.title)
            {
                case TitleId.GS1:
                    GS1_Set_event(mess, flag);
                    break;
                case TitleId.GS2:
                    GS2_Set_event(mess, flag);
                    break;
                case TitleId.GS3:
                    GS3_Set_event(mess, flag);
                    break;
            }
        }
    }

    private static void GS1_Set_event(uint mess, uint flag)
    {
        GSFlag.Set(0u, flag, 1u);
        advCtrl.instance.message_system_.SetMessage(mess);
        MessageSystem.Mess_window_set(3u);
        soundCtrl.instance.AllStopBGM();
    }

    private static void GS2_Set_event(uint mess, uint flag)
    {
        GSFlag.Set(0u, flag, 1u);
        advCtrl.instance.message_system_.SetMessage(mess);
        MessageSystem.Mess_window_set(3u);
        soundCtrl.instance.AllStopBGM();
    }

    private static void GS3_Set_event(uint mess, uint flag)
    {
        GSFlag.Set(0u, flag, 1u);
        advCtrl.instance.message_system_.SetMessage(mess);
        if ((GSStatic.global_work_.scenario == 7 && mess == 334) || (GSStatic.global_work_.scenario == 14 && mess == 149))
        {
            MessageSystem.Mess_window_set(7u);
        }
        else
        {
            MessageSystem.Mess_window_set(3u);
        }
        soundCtrl.instance.AllStopBGM();
    }

    public static void Set_event2(uint mess, uint flag)
    {
        GSFlag.Set(0u, flag, 1u);
        advCtrl.instance.message_system_.SetMessage(mess);
        MessageSystem.Mess_window_set(3u);
    }

    public static void Set_event_loop(uint mess, uint flag)
    {
        GSFlag.Set(0u, flag, 1u);
    }

    public static void Set_event_no_window(uint mess, uint flag)
    {
        GSFlag.Set(0u, flag, 1u);
        advCtrl.instance.message_system_.SetMessage(mess);
        MessageSystem.Mess_window_set(7u);
    }

    public static ushort GetGameOverMesData()
    {
        string text = null;
        switch (GSStatic.global_work_.title)
        {
            case TitleId.GS1:
                text = GS1_gameover_message_data_table[GSStatic.global_work_.scenario];
                break;
            case TitleId.GS2:
                text = GS2_gameover_message_data_table[GSStatic.global_work_.scenario];
                break;
            case TitleId.GS3:
                text = GS3_gameover_message_data_table[GSStatic.global_work_.scenario];
                break;
        }
        ushort result = 0;
        if (text != null)
        {
            result = advCtrl.instance.message_header_.GetMessageNo(text);
        }
        return result;
    }

    public static byte GetScenarioPartData()
    {
        return GetScenarioPartData(GSStatic.global_work_.scenario);
    }

    public static byte GetScenarioPartData(byte scenario)
    {
        switch (GSStatic.global_work_.title)
        {
            case TitleId.GS1:
                return GS1_scenario_part_data[scenario];
            case TitleId.GS2:
                return GS2_scenario_part_data[scenario];
            case TitleId.GS3:
                return GS3_scenario_part_data[scenario];
            default:
                return 0;
        }
    }

    public static uint[] GetNoteInitData()
    {
        switch (GSStatic.global_work_.title)
        {
            case TitleId.GS1:
                return GS1_Note_init_data[GSStatic.global_work_.scenario];
            case TitleId.GS2:
                return GS2_Note_init_data[GSStatic.global_work_.scenario];
            case TitleId.GS3:
                return GS3_Note_init_data[GSStatic.global_work_.scenario];
            default:
                return null;
        }
    }

    public static MUJYUN_CK_DATA[] GetMujyunCkData()
    {
        switch (GSStatic.global_work_.title)
        {
            case TitleId.GS1:
                return GS1_Mujyun_ck_data_tbl[GSStatic.global_work_.scenario];
            case TitleId.GS2:
                return GS2_Mujyun_ck_data_tbl[GSStatic.global_work_.scenario];
            case TitleId.GS3:
                return GS3_Mujyun_ck_data_tbl[GSStatic.global_work_.scenario];
            default:
                return null;
        }
    }

    public static SHOW_DATA[] GetTanteiShowData()
    {
        switch (GSStatic.global_work_.title)
        {
            case TitleId.GS1:
                return GS1_Tantei_show_data_tbl[GSStatic.global_work_.scenario];
            case TitleId.GS2:
                return GS2_Tantei_show_data_tbl[GSStatic.global_work_.scenario];
            case TitleId.GS3:
                return GS3_Tantei_show_data_tbl[GSStatic.global_work_.scenario];
            default:
                return null;
        }
    }

    public static sce_init GetSceInitProc()
    {
        switch (GSStatic.global_work_.title)
        {
            case TitleId.GS1:
                return GS1_Sce_init_proc_tbl[GSStatic.global_work_.scenario];
            case TitleId.GS2:
                return GS2_Sce_init_proc_tbl[GSStatic.global_work_.scenario];
            case TitleId.GS3:
                return GS3_Sce_init_proc_tbl[GSStatic.global_work_.scenario];
            default:
                return null;
        }
    }

    public static sce_init GetSceRoomInitData()
    {
        switch (GSStatic.global_work_.title)
        {
            case TitleId.GS1:
                return GS1_Sce_room_init_tbl[GSStatic.global_work_.scenario];
            case TitleId.GS2:
                return GS2_Sce_room_init_tbl[GSStatic.global_work_.scenario];
            case TitleId.GS3:
                return GS3_Sce_room_init_tbl[GSStatic.global_work_.scenario];
            default:
                return null;
        }
    }

    public static sce_proc GetSceLoopProc()
    {
        switch (GSStatic.global_work_.title)
        {
            case TitleId.GS1:
                return GS1_Sce_loop_proc_tbl[GSStatic.global_work_.scenario];
            case TitleId.GS2:
                return GS2_Sce_loop_proc_tbl[GSStatic.global_work_.scenario];
            case TitleId.GS3:
                return GS3_Sce_loop_proc_tbl[GSStatic.global_work_.scenario];
            default:
                return null;
        }
    }

    public static string GetScenarioMdtPath(int scenario_no)
    {
        string[] scenarioMdtPathTable = GetScenarioMdtPathTable(GSStatic.global_work_.title);
        if (scenarioMdtPathTable != null && scenario_no < scenarioMdtPathTable.Length)
        {
            return scenarioMdtPathTable[scenario_no];
        }
        if ((long)scenario_no == 65535)
        {
            return scenarioMdtPathTable[35];
        }
        return string.Empty;
    }

    public static string GetSystemScenarioMdtPath()
    {
        return "sys_mes" + GSUtility.GetScenarioLanguage(GSStatic.global_work_.language) + ".mdt";
    }

    public static string[] GetScenarioMdtPathTable(TitleId title)
    {
        string[] res;
        switch (title)
        {
            case TitleId.GS1:
                res = new string[GS1_scenario_mdt_path_table.Length];
                GS1_scenario_mdt_path_table.CopyTo(res, 0);
                break;
            case TitleId.GS2:
                res = new string[GS2_scenario_mdt_path_table.Length];
                GS2_scenario_mdt_path_table.CopyTo(res, 0);
                break;
            case TitleId.GS3:
                res = new string[GS3_scenario_mdt_path_table.Length];
                GS3_scenario_mdt_path_table.CopyTo(res, 0);
                break;
            default:
                Debug.LogWarning("Not TitleId");
                return null;
        }
        for (int i = 0; i < res.Length; i++)
        {
            res[i] += GSUtility.GetScenarioLanguage(GSStatic.global_work_.language) + ".mdt";
        }
        return res;
    }

    public static EXPL_CK_DATA GetExplChData(int arg)
    {
        switch (GSStatic.global_work_.title)
        {
            case TitleId.GS1:
                return GS1_expl_ck_data_tbl[arg];
            case TitleId.GS2:
                return GS2_expl_ck_data_tbl[arg];
            case TitleId.GS3:
                return GS3_expl_ck_data_tbl[arg];
            default:
                return null;
        }
    }

    private static void dm_proc(GlobalWork global_work)
    {
    }

    private static void dm_init(GlobalWork global_work, bool is_reset)
    {
    }
}

