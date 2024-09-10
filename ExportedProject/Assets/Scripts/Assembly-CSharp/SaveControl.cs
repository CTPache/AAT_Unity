using System;
using System.Linq;
using SaveStruct;
using UnityEngine;

public static class SaveControl
{
	public const int MAX_SLOT = 10;

	public const int MAX_REGION_NUM = 10;

	public const int system_data_size_ = 884;

	public const int game_data_size_ = 3252;

	private static byte[] temp_data = (byte[])Enumerable.Empty<byte>();

	public static bool is_load_
	{
		get
		{
			return SaveDataAccessor.is_load_;
		}
	}

	public static bool is_save_
	{
		get
		{
			return SaveDataAccessor.is_save_;
		}
	}

	public static bool is_load_error
	{
		get
		{
			return SaveDataAccessor.is_load_error;
		}
	}

	public static bool is_save_error
	{
		get
		{
			return SaveDataAccessor.is_save_error;
		}
	}

	public static void SaveCreateSystemDataRequest()
	{
		Debug.Log("SaveCreateSystemDataRequest");
		PresideData preside_data = default(PresideData);
		PresideData.New(out preside_data);
		Array.Clear(GSStatic.game_data_temp_, 0, GSStatic.game_data_temp_.Length);
		preside_data.system_data_.CopyFromStatic();
		preside_data.slot_list_.CopyTo(GSStatic.game_data_temp_, 0);
		byte[] array = StructSerializer.Serialize(ref preside_data);
		tempDataSet(array);
		SaveDataAccessor.SaveRequest(GetSystemDataFileName(), array);
	}

	public static void SaveSystemDataRequest()
	{
		Debug.Log("SaveSystemDataRequest");
		PresideData preside_data = default(PresideData);
		PresideData.New(out preside_data);
		preside_data.system_data_.CopyFromStatic();
		tempDataGet();
		GSStatic.game_data_temp_.CopyTo(preside_data.slot_list_, 0);
		byte[] array = StructSerializer.Serialize(ref preside_data);
		tempDataSet(array);
		SaveDataAccessor.SaveRequest(GetSystemDataFileName(), array);
	}

	public static void SaveSystemData()
	{
		Debug.Log("SaveSystemData");
	}

	public static void LoadSystemDataRequest()
	{
		Debug.Log("LoadSystemDataRequest");
		SaveDataAccessor.data_size_ = 884;
		SaveDataAccessor.LoadRequest(GetSystemDataFileName());
	}

	public static bool LoadSystemData()
	{
		Debug.Log("LoadSystemData");
		byte[] array = SaveDataAccessor.Load(GetSystemDataFileName());
		if (array != null)
		{
			PresideData preside_data = default(PresideData);
			PresideData.New(out preside_data);
			StructSerializer.Deserialize(ref preside_data, array);
			preside_data.system_data_.CopyToStatic();
			tempDataSet(array);
			return true;
		}
		return false;
	}

	public static void SaveGameDataRequest(int slot)
	{
		Debug.Log("SaveGameDataRequest:" + slot);
		PresideData preside_data = default(PresideData);
		PresideData.New(out preside_data);
		preside_data.system_data_.CopyFromStatic();
		tempDataGet();
		GameData.New(out GSStatic.game_data_temp_[slot]);
		GSStatic.game_data_temp_[slot].CopyFromStatic();
		GSStatic.game_data_temp_.CopyTo(preside_data.slot_list_, 0);
		byte[] array = StructSerializer.Serialize(ref preside_data);
		tempDataSet(array);
		SaveDataAccessor.SaveRequest(GetSystemDataFileName(), array);
	}

	public static void SaveGameData()
	{
		Debug.Log("SaveGameData");
	}

	public static void LoadGameDataRequest(int slot)
	{
		Debug.Log("LoadGameDataRequest");
		SaveDataAccessor.data_size_ = 3252;
		SaveDataAccessor.LoadRequest(GetSystemDataFileName());
	}

	public static bool LoadGameData(int slot)
	{
		Debug.Log("LoadGameData:" + slot);
		byte[] array = SaveDataAccessor.Load(GetSystemDataFileName());
		if (array != null)
		{
			PresideData preside_data = default(PresideData);
			PresideData.New(out preside_data);
			StructSerializer.Deserialize(ref preside_data, array);
			preside_data.system_data_.CopyToStatic();
			preside_data.slot_list_[slot].CopyToStatic();
			tempDataSet(array);
			return true;
		}
		return false;
	}

	public static void DeleteSaveData()
	{
		Debug.Log("DeleteSaveData");
		SaveDataAccessor.Delete(GetSystemDataFileName());
	}

	public static bool IsExistSaveDataFile()
	{
		return SaveDataAccessor.IsExistSaveDataFile(GetSystemDataFileName());
	}

	private static string GetSystemDataFileName()
	{
		return "systemdata";
	}

	private static string GetGameDataFileName(int slot)
	{
		slot += (int)GSStatic.global_work_.language * 10;
		slot = Mathf.Clamp(slot, 0, 99);
		return "gamedata_" + slot;
	}

	private static void tempDataGet()
	{
		PresideData preside_data = default(PresideData);
		PresideData.New(out preside_data);
		if (temp_data.Length <= 0)
		{
			temp_data = SaveDataAccessor.Load(GetSystemDataFileName());
		}
		if (temp_data != null)
		{
			StructSerializer.Deserialize(ref preside_data, temp_data);
			preside_data.slot_list_.CopyTo(GSStatic.game_data_temp_, 0);
		}
	}

	private static void tempDataSet(byte[] data)
	{
		temp_data = new byte[data.Length];
		data.CopyTo(temp_data, 0);
	}
}
