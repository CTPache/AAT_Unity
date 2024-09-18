using System;
using System.Collections.Generic;
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

	public AssetBundle load(string in_path, string in_name, bool is_common = false, bool is_decryption = true, int in_language = -1)
	{
		in_name = ReplaceLanguage.GetFileName(in_path, in_name, in_language);
		foreach (AssetBundleData item in common_list_)
		{
			if (item.path_ == in_path && item.name_ == in_name)
			{
				return item.bundle_;
			}
		}
		foreach (AssetBundleData item2 in asset_list_)
		{
			if (item2.path_ == in_path && item2.name_ == in_name)
			{
				return item2.bundle_;
			}
		}
		AssetBundleData assetBundleData = new AssetBundleData();
		assetBundleData.path_ = in_path;
		assetBundleData.name_ = in_name;
		string text = Application.streamingAssetsPath + in_path + in_name + ".unity3d";
		if (is_decryption)
		{
			byte[] binary = decryptionCtrl.instance.load(text);
			assetBundleData.bundle_ = AssetBundle.LoadFromMemory(binary);
		}
		else
		{
			assetBundleData.bundle_ = AssetBundle.LoadFromFile(text);
		}
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
