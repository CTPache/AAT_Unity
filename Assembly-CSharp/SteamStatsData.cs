using System;
using Steamworks;

[Serializable]
public class SteamStatsData
{
	private static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, 0);

	public string api { get; private set; }

	public CSteamID steam_id { get; private set; }

	public string name { get; private set; }

	public string description { get; private set; }

	public bool secret { get; private set; }

	public bool achieved { get; private set; }

	public DateTime unlock_time { get; private set; }

	public byte[] icon_raw { get; private set; }

	public SteamStatsData(CSteamID steam_id, string api)
	{
		this.steam_id = steam_id;
		this.api = api;
		name = string.Empty;
		description = string.Empty;
		secret = false;
		achieved = false;
		unlock_time = UnixEpoch;
		icon_raw = null;
		Setup();
	}

	public bool IsMyself()
	{
		return steam_id == SteamUser.GetSteamID();
	}

	public void SetAchievement(bool notification = true)
	{
		if (IsMyself())
		{
			SteamUserStats.SetAchievement(api);
			achieved = true;
			if (notification)
			{
				Notification();
			}
		}
	}

	public void SetAchievementProgress(uint current, uint max, bool notification = true)
	{
		if (IsMyself() && max != 0)
		{
			if (current >= max)
			{
				SetAchievement(notification);
			}
			else if (notification)
			{
				SteamUserStats.IndicateAchievementProgress(api, current, max);
			}
		}
	}

	public void ClearAchievement()
	{
	}

	private void Setup()
	{
		name = SteamUserStats.GetAchievementDisplayAttribute(api, "name");
		description = SteamUserStats.GetAchievementDisplayAttribute(api, "desc");
		secret = SteamUserStats.GetAchievementDisplayAttribute(api, "hidden") == "1";
		bool pbAchieved;
		uint punUnlockTime;
		if (IsMyself())
		{
			SteamUserStats.GetAchievementAndUnlockTime(api, out pbAchieved, out punUnlockTime);
		}
		else
		{
			SteamUserStats.GetUserAchievementAndUnlockTime(steam_id, api, out pbAchieved, out punUnlockTime);
		}
		achieved = pbAchieved;
		unlock_time = UnixEpoch.AddSeconds(punUnlockTime);
		byte[] array = null;
		int achievementIcon = SteamUserStats.GetAchievementIcon(api);
		uint pnWidth;
		uint pnHeight;
		if (SteamUtils.GetImageSize(achievementIcon, out pnWidth, out pnHeight))
		{
			array = new byte[pnWidth * pnHeight * 4];
			if (SteamUtils.GetImageRGBA(achievementIcon, array, array.Length))
			{
				icon_raw = array;
			}
			else
			{
				array = null;
			}
		}
		icon_raw = array;
	}

	public static void Notification()
	{
		SteamUserStats.StoreStats();
	}
}
