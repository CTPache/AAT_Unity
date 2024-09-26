using System;
using System.Collections.Generic;

[Serializable]
public class LanguageFileName
{
    public string flance;

    public string german;

    public string korea;

    public string china_s;

    public string china_t;

    public Dictionary<string, string> filenames_extra = new Dictionary<string, string>();

    public LanguageFileName(string in_flance, string in_german, string in_korea, string in_china_s, string in_china_t)
    {
        flance = in_flance;
        german = in_german;
        korea = in_korea;
        china_s = in_china_s;
        china_t = in_china_t;
        filenames_extra = new Dictionary<string, string>();
    }

    internal string getFallback(string fallback)
    {

        switch (fallback)
        {
            case "GERMAN":
                return german;
            case "FRANCE":
                return flance;
            case "KOREA":
                return korea;
            case "CHINA_S":
                return china_s;
            case "CHINA_T":
                return china_t;
        }
        return null;
    }
}
