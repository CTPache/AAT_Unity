using System.Runtime.InteropServices;
using SaveStruct;
//using Steamworks;
using System.IO;
using UnityEngine;

public static class SaveDataAccessor
{
	private static byte[] load_bytes_;

	public static int data_size_;

	private static bool initialized_;

	public static bool is_load;

	public static bool is_save;

	public static bool is_load_error;

	public static bool is_save_error;

	public static bool is_load_
	{
		get
		{
			return is_load;
		}
		set
		{
			is_load = value;
		}
	}

	public static bool is_save_
	{
		get
		{
			return is_save;
		}
		set
		{
			is_save = value;
		}
	}

	public static void Initialize()
	{
		if (!initialized_)
		{
			initialized_ = true;
		}
	}

	public static void SaveRequest(string file_name, byte[] bytes, bool is_new = false)
	{
		Initialize();
		is_save_ = false;
		is_save_error = false;
		SteamStorageScript.Instance.SaveRequest(file_name, bytes, OnSaveSteamData);
	}

	public static void Save(string file_name, byte[] bytes)
	{
		is_save_ = false;
	}

	public static void LoadRequest(string file_name)
	{
		Initialize();
		is_load_error = false;
		is_load_ = false;
		SteamStorageScript.Instance.LoadRequest(file_name, OnLoadSteamData);
	}

	public static byte[] Load(string file_name)
	{
		is_load_ = false;
		//byte[] array = null;
		return load_bytes_;
	}

	public static void Delete(string file_name)
	{
		//SteamRemoteStorage.FileDelete(file_name);
		File.Delete(file_name);
		load_bytes_ = null;
	}

	public static bool IsExistSaveDataFile(string file_name)
	{
		bool flag = false;
		//return SteamRemoteStorage.FileExists(file_name);
		return File.Exists(file_name);
	}

	private static void OnSaveSteamData(bool success)
	{
		if (success)
		{
			Debug.Log("OnSaveSteamData Save Success");
		}
		else
		{
			is_save_error = true;
			Debug.Log("OnSaveSteamData Save Failure");
		}
		is_save_ = true;
	}

	private static void OnLoadSteamData(byte[] data)
	{
		load_bytes_ = data;
		is_load_ = true;
		int num = Marshal.SizeOf(default(PresideData));
		if (load_bytes_ == null)
		{
			is_load_error = true;
			Debug.Log("OnLoadSteamData Failure");
		}
		else if (num != load_bytes_.Length)
		{
			Debug.Log("OnLoadSteamData Failure, Different load data size\nsave_data_size=" + num + ", Load size=" + load_bytes_.Length);
			is_load_error = true;
			load_bytes_ = null;
		}
		else
		{
			Debug.Log("OnLoadSteamData Success");
		}
	}
}
