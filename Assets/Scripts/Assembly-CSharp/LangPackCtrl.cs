using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;
public static class LangPackCtrl
{
    struct PcView
    {
        public ushort[] fontCountArray;
        public float[][] fontFillAmount;
        public float[] OBJ_OP4_008_DiffPosition;
        public float[] facePhotoPosition;
        public float fontSpritePositionX;
        public float[][] fontSpritePositionY;
        public float cursorPosition;
    }
    struct LangPack
    {
        public string lang;
        public string fallback;
        public string sufix;
        public Dictionary<string, string> menuStrings;
        public Dictionary<string, string> international_files_common;
        public Dictionary<string, string> international_files_gs1;
        public Dictionary<string, string> international_files_gs2;
        public Dictionary<string, string> international_files_gs3;
        public int fontSize;
        public int fontTanteiSize;
        public string font;
        public PcView pcview;
    }

    private readonly static string LangPacksDirectory = Application.dataPath + "/LangPacks";
    private static int defaultFontSize = 52;

    public static Dictionary<string, Dictionary<string, Dictionary<string, string>>> langs =
        new Dictionary<string, Dictionary<string, Dictionary<string, string>>>();

    public static Dictionary<string, Dictionary<string, string>> menuStrings =
        new Dictionary<string, Dictionary<string, string>>();
    public static Dictionary<string, int> fontSizes = new Dictionary<string, int>();
    public static Dictionary<string, int> fontTanteiSizes = new Dictionary<string, int>();

    private static Dictionary<string, Font> customFonts = new Dictionary<string, Font>();
    private static Dictionary<string, PcView> pcviews = new Dictionary<string, PcView>();

    public static void Init()
    {
       if (Directory.Exists(LangPacksDirectory))
        {
            string[] LangPackDirs = Directory.GetDirectories(LangPacksDirectory);
            foreach (string langPackDir in LangPackDirs)
            {
                if (langPackDir.StartsWith("."))
                { continue; }
                LangPack l = Newtonsoft.Json.JsonConvert.DeserializeObject<LangPack>(File.ReadAllText(langPackDir + "\\manifest.json"));
                Language.languages.Add(l.lang);
                Language.langFallback.Add(l.lang, l.fallback);
                Language.sufixes.Add(l.lang, l.sufix);
                Dictionary<string, Dictionary<string, string>> resources = new Dictionary<string, Dictionary<string, string>>();
                resources.Add("international_files_common", l.international_files_common);
                resources.Add("international_files_gs1", l.international_files_gs1);
                resources.Add("international_files_gs2", l.international_files_gs2);
                resources.Add("international_files_gs3", l.international_files_gs3);
                langs.Add(l.lang, resources);
                menuStrings.Add(l.lang, l.menuStrings);
                if (l.fontSize != 0)
                {
                    fontSizes.Add(l.lang, l.fontSize);
                }
                if (l.fontTanteiSize != 0)
                {
                    fontTanteiSizes.Add(l.lang, l.fontTanteiSize);
                }
                if (l.font != null)
                {
                    try
                    {
                        Font font = AssetBundle.LoadFromFile(langPackDir + "/" + l.font + ".unity3d").LoadAllAssets<Font>().FirstOrDefault<Font>();
                        customFonts.Add(l.lang, font);
                    }
                    catch (Exception e) { Debug.Log(e); }
                }
                pcviews.Add(l.lang, l.pcview);
            }
        }
    }

    internal static void AddInternationalFiles(string key, LanguageFileName value, string asset_name)
    {
        foreach (string lang in langs.Keys)
        {
            value.filenames_extra = new Dictionary<string, string>();
            if (langs[lang][asset_name].ContainsKey(key))
            {
                string intFile = langs[lang][asset_name][key];
                value.filenames_extra.Add(lang, intFile);
            }
            else
            {
                string fallback = value.getFallback(Language.langFallback[lang]);
                value.filenames_extra.Add(lang, fallback);
            }
        }
    }

    internal static IEnumerable<string> getMenuStrings(string c_lang)
    {
        List<string> ret = new List<string>();
        foreach (string lang in menuStrings.Keys)
        {
            if (menuStrings[lang].ContainsKey(c_lang))
            {
                ret.Add(menuStrings[lang][c_lang]);
            }
            else
            {
                ret.Add(lang);
            }
        }
        return ret;
    }

    //Font logic
    internal static int GetFontSize(string language)
    {
        if (fontSizes.ContainsKey(language))
        {
            return fontSizes[language];
        }
        return defaultFontSize;
    }


    internal static int GetTanteiFontSize(string language)
    {
        switch (language)
        {
            case "USA":
            case "JAPAN":
                return 46;
            case "FRANCE":
            case "GERMAN":
                return 36;
            case "KOREA":
                return 38;
            case "CHINA_T":
                return 40;
        }
        if (fontTanteiSizes.ContainsKey(language))
        {
            return fontTanteiSizes[language];
        }
        return GetTanteiFontSize(Language.langFallback[language]);
    }

    internal static Font GetFontAsset(string lang)
    {
        if (customFonts.ContainsKey(lang))
        {
            return customFonts[lang];
        }
        return null;
    }


    //PcView Logic
    internal static ushort[] GetFontCountArray(string lang)
    {
        return pcviews[lang].fontCountArray;
    }

    internal static float GetFontFillAmount(string lang, int font_type, int disp_count)
    {
        if (pcviews[lang].fontFillAmount != null)
        {
            var a = pcviews[lang].fontFillAmount[font_type];
            return a[disp_count];
        }
        return 0f;
    }
    internal static float GetFontSpritePosition_X(string lang)
    {
        return pcviews[lang].fontSpritePositionX;
    }

    internal static float[] GetFontSpritePosition_Y(string lang, int in_type)
    {
        if (pcviews[lang].fontSpritePositionY != null)
            if (pcviews[lang].fontSpritePositionY[in_type] != null)
                return pcviews[lang].fontSpritePositionY[in_type];
        return null;
    }

    internal static Vector3 Get_OBJ_OP4_008_DiffPosition(string lang)
    {
        if (pcviews[lang].OBJ_OP4_008_DiffPosition != null)
        {
            float[] array = pcviews[lang].OBJ_OP4_008_DiffPosition;
            return new Vector3(array[0], array[1], array[2]);
        }
        return Vector3.zero;
    }

    internal static Vector2 GetFacePhotoPosition(string lang)
    {
        if (pcviews[lang].facePhotoPosition != null)
        {
            float[] array = pcviews[lang].facePhotoPosition;
            return new Vector2(array[0], array[1]);
        }
        return new Vector2(0, 0);
    }

    internal static float GetCursorPosition(string lang)
    {
        return pcviews[lang].cursorPosition;
    }

}

