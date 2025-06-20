

using System;
using System.Collections.Generic;

public static class Language
{
    public static List<string> languages = new List<string>(new string[]{
    "JAPAN",
    "USA",
    "FRANCE",
    "GERMAN",
    "KOREA",
    "CHINA_S",
    "CHINA_T" });

    public static Dictionary<string, string> langFallback = new Dictionary<string, string>();
    public static Dictionary<string, string> suffixes = new Dictionary<string, string>();

    public static void Init()
    {
        langFallback.Add("JAPAN", "JAPAN");
        langFallback.Add("USA", "USA");
        langFallback.Add("FRANCE", "FRANCE");
        langFallback.Add("GERMAN", "GERMAN");
        langFallback.Add("KOREA", "KOREA");
        langFallback.Add("CHINA_S", "CHINA_S");
        langFallback.Add("CHINA_T", "CHINA_T");

        sufixes.Add("JAPAN", "");
        sufixes.Add("USA", "_u");
        sufixes.Add("FRANCE", "_f");
        sufixes.Add("GERMAN", "_g");
        sufixes.Add("KOREA", "_k");
        sufixes.Add("CHINA_S", "_s");
        sufixes.Add("CHINA_T", "_t");

        LangPackCtrl.Init();
    }
    public static string toString()
    {
        string ret = string.Empty;
        foreach (string s in languages)
        {
            ret += s;
        }
        return ret;
    }

    internal static string getSufix(string language)
    {
        return suffixes[language];
    }
}
