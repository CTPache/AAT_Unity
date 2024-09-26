using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class AssetBundleCtrl : MonoBehaviour
{
    [Serializable]
    public class AssetBundleData
    {
        public string path_ = string.Empty;

        public string name_ = string.Empty;

        public AssetBundle bundle_;
    }

    private static AssetBundleCtrl instance_;

    public List<AssetBundleData> asset_list_ = new List<AssetBundleData>();

    public List<AssetBundleData> common_list_ = new List<AssetBundleData>();

    public static AssetBundleCtrl instance
    {
        get
        {
            return instance_;
        }
    }

    private void Awake()
    {
        if (instance_ == null)
        {
            instance_ = this;
        }
    }

    public AssetBundle load(string in_path, string in_name, bool is_common = false, int in_language = -1, bool force = false)
    {

        //Debug.Log("Loading AssetBunde: " + in_path + in_name);
        in_name = ReplaceLanguage.GetFileName(in_path, in_name, in_language);
        foreach (AssetBundleData item in common_list_)
        {
            if (item.path_ == in_path && item.name_ == in_name)
            {
                return item.bundle_;
            }
        }
        AssetBundleData toRemove = null;
        foreach (AssetBundleData item in asset_list_)
        {
            if (item.path_ == in_path && item.name_ == in_name)
            {
                if (!force)
                    return item.bundle_;
                toRemove = item;
            }
        }
        if (toRemove != null)
        {
            toRemove.bundle_.Unload(false);
            asset_list_.Remove(toRemove);
        }
        AssetBundleData assetBundleData = new AssetBundleData();

        assetBundleData.name_ = in_name;
        assetBundleData.path_ = in_path;

        string lang = in_language > 0 ? Language.languages[in_language] : GSStatic.global_work_.language;
        string path = Application.streamingAssetsPath + "/../LangPacks/" + lang + in_path;
        if (!File.Exists(path + in_name + ".unity3d"))
        {
            //Debug.Log("Failed to load " + path + in_name + ", falling back");
            path = Application.streamingAssetsPath + in_path;
        }
        string text = path + in_name + ".unity3d";
        assetBundleData.bundle_ = AssetBundle.LoadFromFile(text);
        if (is_common)
        {
            common_list_.Add(assetBundleData);
        }
        else
        {
            asset_list_.Add(assetBundleData);
        }
        return assetBundleData.bundle_;

    }

    public bool reloadAll(bool common = false)
    {
        List<string[]> assets = new List<string[]>();
        if (common)
        {
            foreach (var asset in common_list_)
            {
                assets.Add(new string[] { asset.path_, asset.name_ });
                if (asset.bundle_ != null)
                {
                    asset.bundle_.Unload(true);
                }
            }
            common_list_.Clear();
            Resources.UnloadUnusedAssets();
        }
        else
        {
            foreach (var asset in asset_list_)
            {
                assets.Add(new string[] { asset.path_, asset.name_ });
            }
            end();
        }
        foreach (var asset in assets)
        {
            load(asset[0], asset[1], common);
        }

        return true;
    }


    public void remove(string in_path, string in_name, int in_language = -1)
    {
        if (!(in_path != string.Empty) || !(in_name != string.Empty))
        {
            return;
        }
        in_name = ReplaceLanguage.GetFileName(in_path, in_name, in_language);
        foreach (AssetBundleData item in asset_list_)
        {
            if (item.path_ == in_path && item.name_ == in_name)
            {
                if (item.bundle_ != null)
                {
                    item.bundle_.Unload(true);
                }
                asset_list_.Remove(item);
                break;
            }
        }
    }

    public void end()
    {
        foreach (AssetBundleData item in asset_list_)
        {
            if (item.bundle_ != null)
            {
                item.bundle_.Unload(true);
            }
        }
        asset_list_.Clear();
        Resources.UnloadUnusedAssets();
    }
}
