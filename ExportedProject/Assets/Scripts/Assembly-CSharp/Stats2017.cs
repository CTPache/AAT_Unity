using ConsoleUtils;
using DataPlatform;
using UnityEngine;
using UnityPluginLog;
using Users;

public class Stats2017 : MonoBehaviour, IMenu
{
	public const string HighScoreStat = "highScore";

	public const string GamesCompletedStat = "gamesCompleted";

	private bool firstTime = true;

	private bool currentUserAdded;

	private bool haveRequestedSignIn;

	private LeaderboardResults leaderboardResults;

	private LeaderboardResults skipToLeaderboardResults;

	private LeaderboardResults topPlayersleaderboardResults;

	private string welcomeText = "\r\n Stats 2017 Example\r\n ----------------------\r\n This example attempts to show some of the basics of using Stats 2017 in your game.\r\n NOTE that this example unlike the basic DataPlatform example requires you be\r\n      running on the UTDK.1 sandbox.\r\n\r\n * gamesCompleted - tracks how many matches you have completed.\r\n * highscore - left pane tracks a basic high score window\r\n               right pane uses a skip the the current user's rank.\r\n\r\n * Increasing your score or finishing the current match do not update in the\r\n   leaderboards until you refresh from the Live Services. Events take\r\n   a moment to propagate through the system so a refresh may not\r\n   appear in your leaderboard immediately.\r\n * You can attempt to manually request a flush of the stats to the back-end\r\n   but keep in mind this is rate limited to 30 seconds.\r\n";

	private User CurrentUser;

	private long score = 1L;

	private GUIStyle style = new GUIStyle();

	private GUIStyle leaderboardHeaderStyle = new GUIStyle();

	private GUIStyle leaderboardStyle = new GUIStyle();

	public void DisplayText(string text)
	{
		base.gameObject.GetComponent<DisplayWindow>().SetFromText(text);
	}

	private void Start()
	{
		PluginLogManager.Create(string.Empty);
		PluginLogManager.OnLog += PluginLogManager_OnLog;
		DataPlatformPlugin.InitializePlugin(0);
		UsersManager.Create();
	}

	private void PluginLogManager_OnLog(LogChannels channel, string message)
	{
		if (channel == LogChannels.kLogExceptions || channel == LogChannels.kLogErrors)
		{
			Debug.LogError(message);
		}
	}

	private bool SanityCheckApplicationSetup()
	{
		string text = string.Empty;
		bool flag = false;
		if (!UsersManager.IsSomeoneSignedIn)
		{
			text += "\n\nERROR: You MUST have someone signed in to use this demo.";
			if (!haveRequestedSignIn)
			{
				UsersManager.RequestSignIn(AccountPickerOptions.AllowGuests, ulong.MaxValue);
				haveRequestedSignIn = true;
			}
		}
		if (ConsoleUtilsManager.SandboxId() != "UTDK.1")
		{
			text += "\n\nERROR: SandboxId not set to UTDK.1 sample will not run";
			text = text + "\n       >> Current Id: " + ConsoleUtilsManager.SandboxId();
		}
		if (ConsoleUtilsManager.PrimaryServiceConfigId() != "dd5d0100-6626-4bb3-a6a9-81991a81fb73")
		{
			text += "\n\nERROR: SCID not set to dd5d0100-6626-4bb3-a6a9-81991a81fb73 sample will not run";
			text = text + "\n       >> Current Id: " + ConsoleUtilsManager.PrimaryServiceConfigId();
			flag = true;
		}
		if (ConsoleUtilsManager.TitleIdHex() != "1A81FB73")
		{
			text += "\n\nERROR: TID not set to 1A81FB73 sample will not run";
			text = text + "\n       >> Current Id: " + ConsoleUtilsManager.TitleIdHex();
			flag = true;
		}
		if (flag)
		{
			text += "\n\n       >> Use the XboxOne -> Set Title Settings For Achievements 2017 menu option in the Editor to set the correct TID & SCID";
		}
		DisplayText(welcomeText + text);
		bool flag2 = text == string.Empty && UsersManager.IsSomeoneSignedIn;
		if (!flag2)
		{
			return false;
		}
		return flag2;
	}

	private void Update()
	{
		if (firstTime && SanityCheckApplicationSetup())
		{
			CurrentUser = UsersManager.Users[0];
			StatsManager.AddLocalUser(CurrentUser.Id);
			OnScreenLog.Add("Adding current user to Stats Manager");
			firstTime = false;
		}
		if (currentUserAdded)
		{
			RefreshLeaderboardData();
			currentUserAdded = false;
		}
		StatEventList eventList = StatsManager.DoWork();
		ProcessStatEvents(eventList);
	}

	private void ProcessStatEvents(StatEventList eventList)
	{
		for (int i = 0; i < eventList.Length; i++)
		{
			StatEvent statEvent = eventList[i];
			if (statEvent.ErrorCode == 0)
			{
				if (statEvent.UserId != CurrentUser.Id)
				{
					continue;
				}
				switch (statEvent.EventType)
				{
				case StatEventType.LocalUserAdded:
					currentUserAdded = true;
					OnScreenLog.Add("Finished adding current user to Stats Manager.");
					break;
				case StatEventType.StatUpdateComplete:
					OnScreenLog.Add("Finished updating stats for current user.");
					break;
				case StatEventType.LocalUserRemoved:
					OnScreenLog.Add("Removed current user from Stats Manager.");
					break;
				case StatEventType.GetLeaderboardComplete:
				{
					LeaderboardResultEventArgs leaderboardResultArgs = statEvent.LeaderboardResultArgs;
					LeaderboardQuery nextQuery = leaderboardResultArgs.Result.GetNextQuery();
					if (nextQuery == null)
					{
						leaderboardResults = leaderboardResultArgs.Result;
					}
					else if (nextQuery.StatName == "highScore")
					{
						if (nextQuery.SkipResultToMe)
						{
							OnScreenLog.Add("Finished getting leaderboard data for highScore with skip to me");
							skipToLeaderboardResults = leaderboardResultArgs.Result;
							if (skipToLeaderboardResults != null)
							{
								OnScreenLog.Add("No of rows: " + skipToLeaderboardResults.Rows.Length);
							}
						}
						else
						{
							OnScreenLog.Add("Finished getting leaderboard data for highScore");
							leaderboardResults = leaderboardResultArgs.Result;
							if (leaderboardResults != null)
							{
								OnScreenLog.Add("No of rows: " + leaderboardResults.Rows.Length);
							}
						}
					}
					else
					{
						OnScreenLog.Add("Finished getting leaderboard data for gamesCompleted");
						topPlayersleaderboardResults = leaderboardResultArgs.Result;
						if (topPlayersleaderboardResults != null)
						{
							OnScreenLog.Add("No of rows: " + topPlayersleaderboardResults.Rows.Length);
						}
					}
					break;
				}
				default:
					OnScreenLog.Add("Unexpected stat event type received: " + (int)statEvent.EventType);
					break;
				}
			}
			else
			{
				OnScreenLog.Add(string.Concat("Stats Manager ERROR: eventType ", statEvent.EventType, " error code ", statEvent.ErrorCode));
				OnScreenLog.Add("Error message: " + statEvent.ErrorMessage);
			}
		}
	}

	private void OnGUI()
	{
		DrawLeaderboards();
		DrawCurrentScore();
	}

	public void DrawQueryingMessage(Rect menuRect)
	{
		style.fontSize = 22;
		style.normal.textColor = Color.red;
		GUILayout.BeginArea(menuRect);
		GUILayout.Label("Querying Leaderboard Data...", style);
		GUILayout.EndArea();
	}

	public void DrawLeaderboardData(Rect menuRect, LeaderboardResults resultObject)
	{
		leaderboardHeaderStyle.fontSize = 17;
		leaderboardHeaderStyle.normal.textColor = Color.cyan;
		leaderboardStyle.fontSize = 17;
		leaderboardStyle.normal.textColor = Color.white;
		GUI.Box(menuRect, string.Empty);
		GUILayout.BeginArea(menuRect);
		GUILayout.Label("  " + resultObject.DisplayName, leaderboardHeaderStyle);
		GUILayout.Label(string.Empty, leaderboardStyle);
		for (int i = 0; i < resultObject.Rows.Length; i++)
		{
			LeaderboardResultRow leaderboardResultRow = resultObject.Rows[i];
			string text = string.Empty;
			for (int j = 0; j < leaderboardResultRow.Length; j++)
			{
				text = text + leaderboardResultRow[j] + " ";
			}
			GUILayout.Label("  " + leaderboardResultRow.Gamertag.PadRight(30), leaderboardStyle);
		}
		GUILayout.EndArea();
		menuRect = new Rect(menuRect.x + menuRect.width / 1.5f, menuRect.y, menuRect.width, menuRect.height);
		GUILayout.BeginArea(menuRect);
		GUILayout.Label(string.Empty, leaderboardHeaderStyle);
		GUILayout.Label(string.Empty, leaderboardStyle);
		for (int k = 0; k < resultObject.Rows.Length; k++)
		{
			LeaderboardResultRow leaderboardResultRow2 = resultObject.Rows[k];
			string text2 = string.Empty;
			for (int l = 0; l < leaderboardResultRow2.Length; l++)
			{
				text2 = text2 + leaderboardResultRow2[l] + " ";
			}
			GUILayout.Label(text2.PadRight(5), leaderboardStyle);
		}
		GUILayout.EndArea();
	}

	private void DrawLeaderboard(Rect menuRect, LeaderboardResults lr)
	{
		if (lr != null)
		{
			DrawLeaderboardData(menuRect, lr);
		}
		else
		{
			DrawQueryingMessage(menuRect);
		}
	}

	public void DrawLeaderboards()
	{
		float num = (float)Screen.width * 0.03f;
		float num2 = (float)Screen.width * 0.03f;
		float num3 = (int)((double)Screen.width * 0.25 - (double)num);
		float num4 = 340f;
		Rect menuRect = new Rect(num, (float)Screen.height * 0.5f + num2, num3, num4);
		DrawLeaderboard(menuRect, leaderboardResults);
		menuRect.x = menuRect.x + num3 + num;
		DrawLeaderboard(menuRect, skipToLeaderboardResults);
		menuRect.y = menuRect.y - num4 - num2 / 2f;
		menuRect.x = menuRect.x - num3 - num;
		DrawLeaderboard(menuRect, topPlayersleaderboardResults);
	}

	public void DrawCurrentScore()
	{
		Rect position = new Rect((int)((double)Screen.width * 0.5), (int)((double)Screen.height * 0.05), 90f, 52f);
		GUI.Box(position, string.Empty);
		GUI.Label(position, "\n  Score: " + score, leaderboardHeaderStyle);
	}

	public void RefreshLeaderboardData()
	{
		LeaderboardQuery leaderboardQuery = new LeaderboardQuery();
		leaderboardQuery.MaxItems = 10u;
		leaderboardQuery.Order = SortOrder.Descending;
		StatsManager.GetLeaderboard(CurrentUser.Id, "highScore", leaderboardQuery);
		LeaderboardQuery leaderboardQuery2 = new LeaderboardQuery();
		leaderboardQuery2.MaxItems = 10u;
		leaderboardQuery2.Order = SortOrder.Descending;
		leaderboardQuery2.SkipResultToMe = true;
		StatsManager.GetLeaderboard(CurrentUser.Id, "highScore", leaderboardQuery2);
		LeaderboardQuery leaderboardQuery3 = new LeaderboardQuery();
		leaderboardQuery3.MaxItems = 10u;
		leaderboardQuery3.Order = SortOrder.Descending;
		StatsManager.GetLeaderboard(CurrentUser.Id, "gamesCompleted", leaderboardQuery3);
		OnScreenLog.Add("Refreshing leaderboard data");
	}

	public void HandleMenu(MenuLayout layout, Menu self)
	{
		if (firstTime)
		{
			return;
		}
		layout.Update(4);
		if (layout.AddButtonWithIndex("Refresh Leaderboard"))
		{
			RefreshLeaderboardData();
		}
		if (layout.AddButtonWithIndex("Increase My Score"))
		{
			score++;
			StatsManager.SetStatAsInteger(CurrentUser.Id, "highScore", score);
			OnScreenLog.Add("Sending updated score: " + score);
		}
		if (layout.AddButtonWithIndex("Complete Match"))
		{
			StatValue stat = StatsManager.GetStat(CurrentUser.Id, "gamesCompleted");
			long num = 0L;
			if (stat != null)
			{
				num = stat.AsInteger;
			}
			num++;
			StatsManager.SetStatAsInteger(CurrentUser.Id, "gamesCompleted", num);
			OnScreenLog.Add("Sending Match Complete: " + num);
			StatValue stat2 = StatsManager.GetStat(CurrentUser.Id, "highScore");
			long num2 = 0L;
			if (stat2 != null)
			{
				num2 = stat2.AsInteger;
			}
			if (score > num2)
			{
				StatsManager.SetStatAsInteger(CurrentUser.Id, "highScore", score);
			}
			score = 0L;
		}
		if (layout.AddButtonWithIndex("Request Flush to Service"))
		{
			StatsManager.RequestFlushToService(CurrentUser.Id, false);
		}
	}
}
