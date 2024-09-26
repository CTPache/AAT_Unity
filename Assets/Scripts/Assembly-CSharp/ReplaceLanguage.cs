using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class ReplaceLanguage
{
    private static readonly Dictionary<string, LanguageFileName> file_names_gs1_ = new Dictionary<string, LanguageFileName>();

    private static readonly Dictionary<string, LanguageFileName> file_names_gs2_ = new Dictionary<string, LanguageFileName>();

    private static readonly Dictionary<string, LanguageFileName> file_names_gs3_ = new Dictionary<string, LanguageFileName>();

    private static readonly Dictionary<string, LanguageFileName> file_names_common_ = new Dictionary<string, LanguageFileName>();

    public static void Init()
    {
        LoadFileNames();
    }

    public static string GetFileName(string folder_path, string file_name, int language = -1)
    {
        if (language == -1)
        {
            language = Language.languages.IndexOf(GSStatic.global_work_.language);
        }
        if (language == 0 || language == 1)
        {
            return file_name;
        }
        switch (GetTitleId(folder_path))
        {
            case 0:
                return GetFileName(file_names_gs1_, file_name, language);
            case 1:
                return GetFileName(file_names_gs2_, file_name, language);
            case 2:
                return GetFileName(file_names_gs3_, file_name, language);
            default:
                return GetFileName(file_names_common_, file_name, language);
        }
    }

    private static string GetFileName(IDictionary<string, LanguageFileName> file_names, string file_name, int language)
    {
        if (!file_names.ContainsKey(file_name) || language <= 1)
        {
            return file_name;
        }
        string lan = Language.languages[language];
        string ret = string.Empty;
        //Debug.Log("Looking for " + file_name + " in " + lan);
        switch (lan)
        {
            case "FRANCE":
                ret = file_names[file_name].flance;
                break;
            case "GERMAN":
                ret = file_names[file_name].german;
                break;
            case "KOREA":
                ret = file_names[file_name].korea;
                break;
            case "CHINA_S":
                ret = file_names[file_name].china_s;
                break;
            case "CHINA_T":
                ret = file_names[file_name].china_t;
                break;
            default:
                //Si encuentra el nombre en el diccionario de extras, devuelve el valor
                if (file_names[file_name].filenames_extra.ContainsKey(Language.languages[language]))
                {
                    if (file_names[file_name].filenames_extra[Language.languages[language]] != null)
                    {
                        return file_names[file_name].filenames_extra[Language.languages[language]];
                    }
                }
                //Si no devuelve el fallback
                ret = GetFileName(file_names, file_name, Language.languages.IndexOf(Language.langFallback[lan]));
                break;
        }
        return ret;
    }

    private static int GetTitleId(string folder_path)
    {
        if (folder_path.Length < 3)
        {
            return -1;
        }
        char[] array = folder_path.Substring(1, 3).ToCharArray();
        if (array[0] == 'G' && array[1] == 'S')
        {
            return array[2] - 48 - 1;
        }
        return -1;
    }

    private static void LoadFileNames()
    {
        LoadFileNames(file_names_common_, "international_files_common");
        LoadFileNames(file_names_gs1_, "international_files_gs1");
        LoadFileNames(file_names_gs2_, "international_files_gs2");
        LoadFileNames(file_names_gs3_, "international_files_gs3");
        Debug.Log("ReplaceLanguage LoadFileNames\nfile_names_common_.Count = " + file_names_common_.Count + "\nfile_names_gs1_.Count = " + file_names_gs1_.Count + "\nfile_names_gs2_.Count = " + file_names_gs2_.Count + "\nfile_names_gs3_.Count = " + file_names_gs3_.Count);
    }

    private static void LoadFileNames(Dictionary<string, LanguageFileName> file_names_dictionary, string asset_name)
    {
        AssetBundle assetBundle = AssetBundleCtrl.instance.load("/InternationalFiles/", asset_name, true, 0);
        InternationalFilesObject internationalFilesObject = assetBundle.LoadAsset<InternationalFilesObject>(asset_name);
        for (int i = 0; i < internationalFilesObject.file_names.Count; i++)
        {
            LanguageFileName value = internationalFilesObject.file_names[i].value;
            string key = internationalFilesObject.file_names[i].key;
            LangPackCtrl.AddInternationalFiles(key, value, asset_name);
            file_names_dictionary.Add(internationalFilesObject.file_names[i].key, internationalFilesObject.file_names[i].value);
        }
    }

}
