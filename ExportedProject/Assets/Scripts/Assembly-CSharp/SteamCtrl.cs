using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Steamworks;
using UnityEngine;

public static class SteamCtrl
{
	public static bool IsShareFilesError;

	public static bool IsReady { get; private set; }

	public static bool IsFailed { get; private set; }

	public static void Init()
	{
		IsReady = false;
		IsFailed = false;
		if (!SteamManager.Initialized)
		{
			Application.Quit();
			return;
		}
		if (SteamAPI.RestartAppIfNecessary(AppId_t.Invalid) || !SteamApps.BIsSubscribed())
		{
			Application.Quit();
			return;
		}
		Debug.Log("[Steamworks.NET] API Init Success!");
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendLine("Steam Info Data");
		stringBuilder.AppendLine("AppID : " + SteamUtils.GetAppID());
		stringBuilder.AppendLine("User Name : " + SteamFriends.GetPersonaName());
		stringBuilder.AppendLine("User SteamID : " + SteamUser.GetSteamID());
		stringBuilder.AppendLine("User AccountID : " + SteamUser.GetSteamID().GetAccountID());
		stringBuilder.AppendLine("IP Country : " + SteamUtils.GetIPCountry());
		stringBuilder.AppendLine("Language : " + SteamApps.GetCurrentGameLanguage());
		stringBuilder.AppendLine("Support Langs : " + SteamApps.GetAvailableGameLanguages());
		stringBuilder.AppendLine("Is Owner : " + (SteamApps.GetAppOwner() == SteamUser.GetSteamID()));
		stringBuilder.AppendLine("Is Subscribed : " + SteamApps.BIsSubscribed());
		stringBuilder.AppendLine("Is VAC Banned : " + SteamApps.BIsVACBanned());
		Debug.Log(stringBuilder);
		SteamAuthSessionScript.Instance.Init();
		if (IsOnline())
		{
			SteamAuthSessionScript.Instance.StartAuthSession(delegate(bool ok)
			{
				if (ok)
				{
					SteamStatsScript.Instance.Init();
					SteamStorageScript.Instance.Init();
					LoadAchievement();
				}
				else
				{
					IsReady = true;
					IsFailed = true;
				}
			});
		}
		else
		{
			SteamStatsScript.Instance.Init();
			SteamStorageScript.Instance.Init();
			LoadAchievement();
		}
	}

	public static SystemLanguage ConvertLanguage()
	{
		if (!SteamManager.Initialized)
		{
			return SystemLanguage.English;
		}
		switch (SteamApps.GetCurrentGameLanguage())
		{
		case "japanese":
			return SystemLanguage.Japanese;
		case "english":
			return SystemLanguage.English;
		case "schinese":
			return SystemLanguage.ChineseSimplified;
		case "tchinese":
			return SystemLanguage.ChineseTraditional;
		case "french":
			return SystemLanguage.French;
		case "german":
			return SystemLanguage.German;
		case "koreana":
			return SystemLanguage.Korean;
		default:
			return SystemLanguage.English;
		}
	}

	public static bool IsJapanIP()
	{
		return SteamManager.Initialized && SteamUtils.GetIPCountry().ToLower().Contains("jp");
	}

	public static IEnumerator ShareFiles()
	{
		if (!IsOnline() || !IsCloudEnabled())
		{
			yield break;
		}
		IsShareFilesError = false;
		int filecount = SteamRemoteStorage.GetFileCount();
		for (int i = 0; i < filecount; i++)
		{
			int bytesize;
			string filename = SteamRemoteStorage.GetFileNameAndSize(i, out bytesize);
			bool next_file = false;
			if (SteamStorageScript.Instance.RequestShareFile(filename, delegate(byte[] data)
			{
				next_file = data != null;
			}))
			{
				while (!next_file && !IsShareFilesError)
				{
					yield return null;
				}
				if (IsShareFilesError)
				{
					yield break;
				}
			}
		}
		yield return null;
	}

	public static void LoadAchievement()
	{
		SteamStatsScript.Instance.RequestCurrentStats(delegate(ReadOnlyCollection<SteamStatsData> stats_list)
		{
			IsReady = true;
			IsFailed = stats_list == null;
			Debug.Log("SteamCtrl LoadAchievement : " + (IsFailed ? "Failure" : "Success"));
			if (!IsFailed)
			{
				List<bool> list = new List<bool> { false };
				foreach (SteamStatsData item in stats_list)
				{
					list.Add(item.achieved);
				}
				TrophyCtrl.init(list.ToArray());
			}
		});
	}

	public static void SetAchievement(int trophy_id)
	{
		Debug.Log("SteamCtrl SetAchievement : trophy_id = " + trophy_id + " : IsReady = " + IsReady);
		if (IsReady)
		{
			SteamStatsScript.Instance.SetAchievement(trophy_id);
		}
		else
		{
			TrophyCtrl.set_trophy(trophy_id, false);
		}
	}

	public static uint GetSaveDataAccountID()
	{
		return (uint)GSStatic.reserve_data.reserve[1];
	}

	public static int GetAccountID()
	{
		return (int)SteamUser.GetSteamID().GetAccountID().m_AccountID;
	}

	public static bool IsOnline()
	{
		bool flag = SteamUser.BLoggedOn();
		Debug.Log("SteamCtrl IsOnline = " + flag);
		return flag;
	}

	public static bool IsCloudEnabled()
	{
		bool flag = SteamRemoteStorage.IsCloudEnabledForAccount() && SteamRemoteStorage.IsCloudEnabledForApp();
		Debug.Log("SteamCtrl IsCloudEnabled = " + flag);
		return flag;
	}
}
