using UnityEngine;
using System.Collections.Generic;
using System.IO;

public static class TextDataCtrl
{
    public enum CommonTextID
    {
        INSPECT = 0,
        ROOM_MOVE = 1,
        TALK = 2,
        TUKITUKE = 3,
        OPTION = 4,
        RECORD = 5,
        YUSABURU = 6,
        BACK = 7,
        SLIDE = 8,
        PROFILES = 9,
        EVIDENCE = 10,
        ROTATE = 11,
        ROTATE_LEFT = 12,
        ROTATE_RIGHT = 13,
        ZOOM_IN = 14,
        ZOOM_OUT = 15,
        FAST_FORWARD = 16,
        PAUSE = 17,
        REWIND = 18,
        HUKITUKR = 19,
        DETECTION = 20,
        DUST = 21,
        BLOW = 22,
        COMPARE = 23,
        COMBINE = 24,
        MOVIE_PLAY = 25,
        ROT_UP = 26,
        ROT_DOWN = 27,
        ROT_LEFT = 28,
        ROT_RIGHT = 29,
        SWITCH_PROFILE = 30,
        DYING_BACK = 31
    }

    public enum TitleTextID
    {
        NEW_GAME = 0,
        CONTINUE = 1,
        OPTION = 2,
        PLAY_TITLE = 3,
        PLAY_EPISODE = 4,
        PLAY_CONFIRM = 5,
        YES = 6,
        NO = 7,
        TITLE_NAME = 8,
        EPISODE_NUMBER = 9,
        GS1_SCENARIO_NAME = 10,
        GS2_SCENARIO_NAME = 11,
        GS3_SCENARIO_NAME = 12,
        EXIT = 13,
        EXIT_MESSAGE = 14,
        START_INPUT = 19
    }

    public enum OptionTextID
    {
        ITEM_SAVE = 0,
        ITEM_BGM = 1,
        ITEM_SE = 2,
        ITEM_SKIP = 3,
        ITEM_SHAKE = 4,
        ITEM_VIBRATION = 5,
        ITEM_TRANSPARENCY = 6,
        ITEM_LANGUAGE = 7,
        ITEM_CREDITS = 8,
        SELECT_SAVE = 9,
        SELECT_LOAD = 10,
        SELECT_ON = 11,
        SELECT_OFF = 12,
        SELECT_STOP = 13,
        SELECT_SKIP = 14,
        SELECT_LOW = 15,
        SELECT_HIGH = 16,
        JAPANESE = 17,
        ENGLISH = 18,
        FRENCH = 19,
        GERMAN = 20,
        KOREAN = 21,
        CHINESE_SIMPLIFIED = 22,
        CHINESE_TRADITIONAL = 23,
        COMMENT_SAVE = 24,
        COMMENT_BGM = 25,
        COMMENT_SE = 26,
        COMMENT_SKIP = 31,
        COMMENT_SHAKE = 32,
        COMMENT_VIBRATION = 33,
        COMMENT_TRANSPARENCY = 34,
        COMMENT_LANGUAGE = 39,
        COMMENT_CREDITS = 40,
        LANGUAGE_CHANGE = 41,
        GO_TITLE = 42,
        BACK = 43,
        TITLE = 44,
        DEFAULT = 45,
        LANGUAGE_SAVE = 50,
        SET_DEFAULT = 51,
        ITEM_SCREEN = 52,
        ITEM_RESOLUTION = 53,
        ITEM_VSYNC = 54,
        SELECT_WINDOW = 55,
        SELECT_FULLSCREEN = 56,
        SELECT_VFULLSCREEN = 57,
        COMMENT_SCREEN = 58,
        COMMENT_RESOLUTION = 59,
        COMMENT_VSYNC = 60,
        COMMENT_KEY_CONFIG_DECIDE = 61,
        COMMENT_KEY_CONFIG = 62,
        COMMENT_KEY_CONFIG_INPUT = 63,
        COMMENT_KEY_CONFIG_CHECK = 64,
        COMMENT_KEY_CONFIG_CHECK2 = 65,
        ITEM_KEYCONFIG = 66,
        ITEM_CONFIG_DECIDE = 67,
        ITEM_DECIDE = 68,
        ITEM_CANCEL = 69,
        ITEM_TUKITUKE = 70,
        ITEM_YUSABURI = 71,
        ITEM_RECORD = 72,
        ITEM_OPTION = 73,
        ITEM_PERSON = 74,
        ITEM_TITLE_RETURN = 75,
        ITEM_KEY_MOVE = 76,
        ITEM_KEY_ROT = 77,
        ITEM_UP_ARROW = 78,
        ITEM_DOWM_ARROW = 79,
        ITEM_RIGHT_ARROW = 80,
        ITEM_LEFT_ARROW = 81,
        CHANGE = 82
    }

    public enum SaveTextID
    {
        SELECT_SLOT = 0,
        SELECT_DATA_SW = 1,
        SELECT_DATA = 2,
        NO_DATA_SW = 3,
        NO_DATA = 4,
        SELECT_CONFIRM_SW = 5,
        SELECT_CONFIRM = 6,
        LOADDING_SW = 7,
        LOADDING = 8,
        OVERWRITE_SW = 9,
        OVERWRITE = 10,
        SAVING_SW = 11,
        SAVING_XO = 12,
        SAVING_PS4 = 13,
        SAVING_STEAM = 14,
        CLEAR_SAVE = 15,
        ADD_NEW_EPISODE = 16,
        GS1_SC0 = 17,
        GS1_SC1_0 = 18,
        GS1_SC1_1 = 19,
        GS1_SC1_2 = 20,
        GS1_SC1_3 = 21,
        GS1_SC2_0 = 22,
        GS1_SC2_1 = 23,
        GS1_SC2_2 = 24,
        GS1_SC2_3 = 25,
        GS1_SC2_4 = 26,
        GS1_SC2_5 = 27,
        GS1_SC3_0 = 28,
        GS1_SC3_1 = 29,
        GS1_SC3_2 = 30,
        GS1_SC3_3 = 31,
        GS1_SC3_4 = 32,
        GS1_SC3_5 = 33,
        GS1_SC4_0 = 34,
        GS1_SC4_1_0 = 35,
        GS1_SC4_1_1 = 36,
        GS1_SC4_2 = 37,
        GS1_SC4_3_0 = 38,
        GS1_SC4_3_1 = 39,
        GS1_SC4_4 = 40,
        GS1_SC4_5_0 = 41,
        GS1_SC4_5_1 = 42,
        GS1_SC4_5_2 = 43,
        GS2_SC0_0 = 44,
        GS2_SC0_1 = 45,
        GS2_SC1_0 = 46,
        GS2_SC1_1_0 = 47,
        GS2_SC1_1_1 = 48,
        GS2_SC1_2 = 49,
        GS2_SC1_3_0 = 50,
        GS2_SC1_3_1 = 51,
        GS2_SC2_0 = 52,
        GS2_SC2_1_0 = 53,
        GS2_SC2_1_1 = 54,
        GS2_SC2_2 = 55,
        GS2_SC2_3_0 = 56,
        GS2_SC2_3_1 = 57,
        GS2_SC3_0_0 = 58,
        GS2_SC3_0_1 = 59,
        GS2_SC3_1_0 = 60,
        GS2_SC3_1_1 = 61,
        GS2_SC3_2_0 = 62,
        GS2_SC3_2_1 = 63,
        GS2_SC3_3_0 = 64,
        GS2_SC3_3_1 = 65,
        GS3_SC0_0 = 66,
        GS3_SC0_1 = 67,
        GS3_SC1_0 = 68,
        GS3_SC1_1 = 69,
        GS3_SC1_2 = 70,
        GS3_SC1_3_0 = 71,
        GS3_SC1_3_1 = 72,
        GS3_SC2_0 = 73,
        GS3_SC2_1_0 = 74,
        GS3_SC2_2 = 75,
        GS3_SC2_3_0 = 76,
        GS3_SC2_3_1 = 77,
        GS3_SC3_0_0 = 78,
        GS3_SC3_0_1 = 79,
        GS3_SC4_0_0 = 80,
        GS3_SC4_0_1 = 81,
        GS3_SC4_1_0 = 82,
        GS3_SC4_1_1 = 83,
        GS3_SC4_2_0 = 84,
        GS3_SC4_2_1 = 85,
        GS3_SC4_3_0 = 86,
        GS3_SC4_3_1 = 87,
        GS3_SC4_3_2 = 88,
        LOAD_ERROR = 90,
        DELETING = 91,
        SAVE_ERROR = 92,
        CREATE_ERROR = 93
    }

    public enum PlatformTextID
    {
        SWITCH_A = 0,
        SWITCH_B = 1,
        SWITCH_X = 2,
        SWITCH_R = 3,
        SIE_JA_CIRCLE = 4,
        SIE_JA_CROSS = 5,
        SIE_JA_TRIANGLE = 6,
        SIE_JA_R1 = 7,
        SIE_A_CROSS = 8,
        SIE_A_CIRCLE = 9,
        SIE_A_TRIANGLE = 10,
        SIE_A_R1 = 11,
        XBOX_A = 12,
        XBOX_B = 13,
        XBOX_Y = 14,
        XBOX_RB = 15,
        STEAM_L = 16,
        STEAM_K = 17,
        STEAM_R = 18,
        STEAM_E = 19
    }

    public enum SystemTextID
    {
        DISCONNECT_GAME_PAD = 0,
        CHANGE_USER = 1,
        FAILED_SIGN_IN = 2,
        SIGN_OUT = 3,
        SAVE_DATA_DIFF_SW = 4,
        SAVE_DATA_DIFF = 5
    }

    private static int current_language_ = -1;

    private static ConvertTextData common_text_data_ = null;

    private static ConvertLineData title_text_data_ = null;

    private static ConvertLineData option_text_data_ = null;

    private static ConvertLineData save_text_data_ = null;

    private static ConvertTextData platform_text_data_ = null;

    private static ConvertLineData system_text_data_ = null;

    private static string common_text_path_ = "/menu/text/common_text{0}.bin";
    private static string title_text_path_ = "/menu/text/title_text{0}.bin";
    private static string option_text_path_ = "/menu/text/option_text{0}.bin";
    private static string save_text_path_ = "/menu/text/save_text{0}.bin";
    private static string platform_text_path_ = "/menu/text/platform_text{0}.bin";
    private static string system_text_path_ = "/menu/text/system_text{0}.bin";
    public const int PlatformTextCount = 4;

    public static void SetLanguage(string in_language)
    {
//        Debug.Log(string.Concat("SetLanguage in_language:", in_language, " current_language_ ", current_language_));
        if (current_language_ < Language.languages.Count && current_language_ >= 0)
        {
            if (in_language == Language.languages[current_language_])
            {
                return;
            }
        }
        int indexOfLang = Language.languages.IndexOf(in_language);
        LoadTextData(ref common_text_data_, common_text_path_, in_language);
        LoadTextData(ref title_text_data_, title_text_path_, in_language);
        LoadTextData(ref option_text_data_, option_text_path_, in_language);
        LoadTextData(ref save_text_data_, save_text_path_, in_language);
        LoadTextData(ref platform_text_data_, platform_text_path_, in_language);
        LoadTextData(ref system_text_data_, system_text_path_, in_language);
        current_language_ = indexOfLang;

    }

    public static string GetText(CommonTextID in_text_id)
    {
        return common_text_data_.GetText((ushort)in_text_id);
    }

    public static string GetText(TitleTextID in_text_id, int in_text_line = 0)
    {
        return title_text_data_.GetText((ushort)in_text_id, in_text_line);
    }

    public static string GetText(OptionTextID in_text_id, int in_text_line = 0)
    {
        return option_text_data_.GetText((ushort)in_text_id, in_text_line);
    }

    public static string GetText(SaveTextID in_text_id, int in_text_line = 0)
    {
        return save_text_data_.GetText((ushort)in_text_id, in_text_line);
    }

    public static string GetText(PlatformTextID in_text_id)
    {
        return platform_text_data_.GetText((ushort)in_text_id);
    }

    public static string GetText(SystemTextID in_text_id, int in_text_line)
    {
        return system_text_data_.GetText((ushort)in_text_id, in_text_line);
    }

    public static string[] GetTexts(OptionTextID in_text_id)
    {
        return option_text_data_.GetTexts((ushort)in_text_id);
    }

    public static string[] GetTexts(SaveTextID in_text_id)
    {
        return save_text_data_.GetTexts((ushort)in_text_id);
    }

    public static string[] GetTexts(SystemTextID in_text_id)
    {
        return system_text_data_.GetTexts((ushort)in_text_id);
    }

    public static string StringArrayToString(string[] in_texts)
    {
        string text = in_texts[0];
        for (int i = 1; i < in_texts.Length; i++)
        {
            text = text + '\n' + in_texts[i];
        }
        return text;
    }

    private static void LoadTextData(ref ConvertTextData data, string path, string language)
    {
        string in_path;
        if (Language.languages.IndexOf(language) > 6)
        {
            in_path = Application.streamingAssetsPath + "/../LangPacks/" + language + path.Replace("{0}", Language.getSufix(language));
            if (!File.Exists(in_path))
                in_path = Application.streamingAssetsPath + "/" + path.Replace("{0}", Language.getSufix(language));
        }
        else
            in_path = Application.streamingAssetsPath + "/" + path.Replace("{0}", Language.getSufix(language));
        byte[] bytes = decryptionCtrl.instance.load(in_path);
        data = new ConvertTextData(bytes, language);
    }

    private static void LoadTextData(ref ConvertLineData data, string path, string language)
    {
        string in_path;
        if (Language.languages.IndexOf(language) > 6)
        {
            in_path = Application.streamingAssetsPath + "/../LangPacks/" + language + path.Replace("{0}", Language.getSufix(language));
            if (!File.Exists(in_path))
                in_path = Application.streamingAssetsPath + "/" + path.Replace("{0}", Language.getSufix(language));
        }
        else
            in_path = Application.streamingAssetsPath + "/" + path.Replace("{0}", Language.getSufix(language));
        byte[] bytes = decryptionCtrl.instance.load(in_path);
        data = new ConvertLineData(bytes, language);
    }
}
