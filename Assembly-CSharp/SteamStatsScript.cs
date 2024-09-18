using System.Collections.Generic;
using System.Collections.ObjectModel;
//using Steamworks;
using UnityEngine;

[DisallowMultipleComponent]
public class SteamStatsScript : SteamScriptBase
{
	/*
	public delegate void OnRequestRecived(ReadOnlyCollection<SteamStatsData> stats_list);

	private static SteamStatsScript s_instance_;

	private Dictionary<CSteamID, List<SteamStatsData>> cache_user_stats_list_ = new Dictionary<CSteamID, List<SteamStatsData>>();

	private Dictionary<CSteamID, OnRequestRecived> cache_on_completed_ = new Dictionary<CSteamID, OnRequestRecived>();

	private Callback<UserStatsReceived_t> on_user_stats_received_;

	private Callback<UserAchievementStored_t> on_user_achievement_stored_;

	private CallResult<UserStatsReceived_t> on_user_stats_received_callresult_;

	private const string AchievementAPINameFormat = "SUMMARY_ACHIEVEMENTS_{0:00}";

	public static SteamStatsScript Instance
	{
		get
		{
			if (s_instance_ == null)
			{
				s_instance_ = SteamScriptBase.AddSteamScriptComponent<SteamStatsScript>();
			}
			return s_instance_;
		}
	}

	protected override Rect gui_layout_area
	{
		get
		{
			return new Rect(Screen.width - 200, 0f, 200f, Screen.height);
		}
	}

	protected override void AwakeSteam()
	{
		if (s_instance_ == null)
		{
			s_instance_ = this;
		}
		on_user_stats_received_ = Callback<UserStatsReceived_t>.Create(OnUserStatsReceived);
		on_user_achievement_stored_ = Callback<UserAchievementStored_t>.Create(OnUserAchievementStored);
		on_user_stats_received_callresult_ = CallResult<UserStatsReceived_t>.Create(OnUserStatsReceived);
	}

	protected override void StartSteam()
	{
	}

	protected override void OnActive()
	{
		base.OnActive();
		if (s_instance_ == null)
		{
			s_instance_ = this;
		}
	}

	protected override void OnDeactive()
	{
		base.OnDeactive();
		if (!(s_instance_ != this))
		{
			s_instance_ = null;
		}
	}

	public override void Init()
	{
	}

	public void RequestCurrentStats(OnRequestRecived on_complete)
	{
		CSteamID steamID = SteamUser.GetSteamID();
		if (!cache_on_completed_.ContainsKey(steamID))
		{
			cache_on_completed_.Add(steamID, null);
		}
		cache_on_completed_[steamID] = on_complete;
		SteamUserStats.RequestCurrentStats();
	}

	public void RequestUserStats(CSteamID steam_id, OnRequestRecived on_complete)
	{
		if (!cache_on_completed_.ContainsKey(steam_id))
		{
			cache_on_completed_.Add(steam_id, null);
		}
		cache_on_completed_[steam_id] = on_complete;
		on_user_stats_received_callresult_.Set(SteamUserStats.RequestUserStats(steam_id));
	}

	public ReadOnlyCollection<SteamStatsData> GetStatsData()
	{
		return GetUserStatsData(SteamUser.GetSteamID());
	}

	public ReadOnlyCollection<SteamStatsData> GetUserStatsData(CSteamID steam_id)
	{
		if (cache_user_stats_list_.ContainsKey(steam_id))
		{
			return cache_user_stats_list_[steam_id].AsReadOnly();
		}
		return null;
	}

	public void ClearCache()
	{
		cache_user_stats_list_.Clear();
	}

	private void CreateStatsList(CSteamID steam_id)
	{
		List<SteamStatsData> list = new List<SteamStatsData>();
		for (uint num = 0u; num < SteamUserStats.GetNumAchievements(); num++)
		{
			string achievementName = SteamUserStats.GetAchievementName(num);
			list.Add(new SteamStatsData(steam_id, achievementName));
		}
		if (!cache_user_stats_list_.ContainsKey(steam_id))
		{
			cache_user_stats_list_.Add(steam_id, list);
		}
		else
		{
			cache_user_stats_list_[steam_id] = list;
		}
	}

	private void OnUserStatsReceived(UserStatsReceived_t pCallback)
	{
		if ((ulong)base.game_id == pCallback.m_nGameID && SteamUser.GetSteamID() == pCallback.m_steamIDUser)
		{
			Debug.Log(string.Concat("[", 1101, " - UserStatsReceived] - ", pCallback.m_nGameID, " -- ", pCallback.m_steamIDUser, " -- ", pCallback.m_steamIDUser.GetAccountID(), " -- ", pCallback.m_eResult));
			if (pCallback.m_eResult == EResult.k_EResultOK)
			{
				CreateStatsList(pCallback.m_steamIDUser);
				CallOnComplete(pCallback.m_steamIDUser);
			}
			else
			{
				Debug.LogError("RequestStats - failed, " + pCallback.m_eResult);
				CallOnComplete(pCallback.m_steamIDUser);
			}
		}
	}

	private void OnUserStatsReceived(UserStatsReceived_t pCallback, bool bIOFailure)
	{
		if ((ulong)base.game_id == pCallback.m_nGameID && SteamUser.GetSteamID() != pCallback.m_steamIDUser)
		{
			Debug.Log(string.Concat("[", 1101, " - UserStatsReceived] - ", pCallback.m_nGameID, " -- ", pCallback.m_steamIDUser, " -- ", pCallback.m_steamIDUser.GetAccountID(), " -- ", pCallback.m_eResult));
			if (pCallback.m_eResult == EResult.k_EResultOK)
			{
				CreateStatsList(pCallback.m_steamIDUser);
				CallOnComplete(pCallback.m_steamIDUser);
			}
			else
			{
				Debug.LogError("RequestStats - failed, " + pCallback.m_eResult);
				CallOnComplete(pCallback.m_steamIDUser);
			}
		}
	}

	private void OnUserAchievementStored(UserAchievementStored_t pCallback)
	{
		Debug.Log("[" + 1103 + " - UserAchievementStored] - " + pCallback.m_nGameID + " -- " + pCallback.m_rgchAchievementName + " -- " + pCallback.m_nCurProgress + "/" + pCallback.m_nMaxProgress);
		if ((ulong)base.game_id == pCallback.m_nGameID && pCallback.m_nCurProgress != pCallback.m_nMaxProgress)
		{
		}
	}

	private void CallOnComplete(CSteamID steam_id)
	{
		if (cache_on_completed_.ContainsKey(steam_id))
		{
			if (cache_on_completed_[steam_id] != null)
			{
				cache_on_completed_[steam_id](GetUserStatsData(steam_id));
			}
			cache_on_completed_.Remove(steam_id);
		}
	}

	public void SetAchievement(int trophy_id)
	{
		ReadOnlyCollection<SteamStatsData> statsData = GetStatsData();
		foreach (SteamStatsData item in statsData)
		{
			if (item.api.Contains(string.Format("SUMMARY_ACHIEVEMENTS_{0:00}", trophy_id)))
			{
				item.SetAchievement(true);
				break;
			}
		}
	}
	*/
}
