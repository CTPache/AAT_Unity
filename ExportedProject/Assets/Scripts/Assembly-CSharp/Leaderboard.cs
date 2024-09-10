using System;
using ConsoleUtils;
using DataPlatform;
using UnityAOT;
using UnityEngine;
using UnityPluginLog;
using Users;

public class Leaderboard : MonoBehaviour, IMenu
{
	private ResourceRequest eventManifest;

	private bool firstTime = true;

	private bool haveRequestedSignIn;

	private GetObjectAsyncOp<LeaderboardResults> leaderboardData;

	private GetObjectAsyncOp<LeaderboardResults> skipToLeaderboardData;

	private GetObjectAsyncOp<LeaderboardResults> topPlayersLeaderboardData;

	private string welcomeText = "\r\n Leaderboard Example\r\n ----------------------\r\n This example attempts to show some of the basics of using Leaderboards in your game.\r\n NOTE that this example unlike the basic DataPlatform example requires you be\r\n      running on the UTDK.1 sandbox.\r\n\r\n * TopPlayers - tracks how many matches you have completed.\r\n * HighScores - left pane tracks a basic high score window\r\n                right pane uses a skip list in a confusing fashion.\r\n                It asks for 1/2 of current score, however HighScores\r\n                is sorted in descending order so SkipTo shows everything\r\n                at that value and below not higher. This is a good illustration\r\n                of how this system works.\r\n\r\n * Increasing your score or finishing the current match do not update in the\r\n   leaderboards until you refresh from the Live Services. Events take\r\n   a moment to propagate through the system so an immediate refresh may not\r\n   appear in your leaderboard immediately. \r\n";

	private static Guid GameSessionId = Guid.NewGuid();

	private User CurrentUser;

	private int score = 1;

	private GUIStyle style = new GUIStyle();

	private GUIStyle leaderboardHeaderStyle = new GUIStyle();

	private GUIStyle leaderboardStyle = new GUIStyle();

	public void DisplayText(string text)
	{
		base.gameObject.GetComponent<DisplayWindow>().SetFromText(text);
	}

	private void Start()
	{
		eventManifest = Resources.LoadAsync("AngryBots");
		PluginLogManager.Create(string.Empty);
		PluginLogManager.OnLog += PluginLogManager_OnLog;
		DataPlatformPlugin.InitializePlugin(0);
		UsersManager.Create();
		LeaderboardManager.Create();
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
		if (ConsoleUtilsManager.PrimaryServiceConfigId() != "1cd30100-93a1-4c62-a8a8-1e294c51822b")
		{
			text += "\n\nERROR: SCID not set to 1cd30100-93a1-4c62-a8a8-1e294c51822b sample will not run";
			text = text + "\n       >> Current Id: " + ConsoleUtilsManager.PrimaryServiceConfigId();
			flag = true;
		}
		if (ConsoleUtilsManager.TitleIdHex() != "4C51822B")
		{
			text += "\n\nERROR: TID not set to 4C51822B sample will not run";
			text = text + "\n       >> Current Id: " + ConsoleUtilsManager.TitleIdHex();
			flag = true;
		}
		if (flag)
		{
			text += "\n\n       >> Use the XboxOne -> Reset Title Settings menu option in the Editor to set the correct TID & SCID";
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
		if (firstTime && eventManifest.isDone && SanityCheckApplicationSetup())
		{
			CurrentUser = UsersManager.Users[0];
			string text = ((TextAsset)eventManifest.asset).text;
			EventManager.CreateFromText(text);
			Events.SendScoreChanged(CurrentUser.UID, ref GameSessionId, score);
			OnScreenLog.Add("Sending current score of 1");
			RefreshLeaderboardData();
			firstTime = false;
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

	public void DrawLeaderboardData(Rect menuRect, GetObjectAsyncOp<LeaderboardResults> ld)
	{
		leaderboardHeaderStyle.fontSize = 17;
		leaderboardHeaderStyle.normal.textColor = Color.cyan;
		leaderboardStyle.fontSize = 17;
		leaderboardStyle.normal.textColor = Color.white;
		GUI.Box(menuRect, string.Empty);
		GUILayout.BeginArea(menuRect);
		GUILayout.Label("  " + ld.ResultObject.DisplayName, leaderboardHeaderStyle);
		GUILayout.Label(string.Empty, leaderboardStyle);
		for (int i = 0; i < ld.ResultObject.Rows.Length; i++)
		{
			LeaderboardResultRow leaderboardResultRow = ld.ResultObject.Rows[i];
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
		for (int k = 0; k < ld.ResultObject.Rows.Length; k++)
		{
			LeaderboardResultRow leaderboardResultRow2 = ld.ResultObject.Rows[k];
			string text2 = string.Empty;
			for (int l = 0; l < leaderboardResultRow2.Length; l++)
			{
				text2 = text2 + leaderboardResultRow2[l] + " ";
			}
			GUILayout.Label(text2.PadRight(5), leaderboardStyle);
		}
		GUILayout.EndArea();
	}

	private void DrawLeaderboard(Rect menuRect, GetObjectAsyncOp<LeaderboardResults> ld)
	{
		if (ld != null && ld.IsComplete && ld.Success)
		{
			DrawLeaderboardData(menuRect, ld);
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
		DrawLeaderboard(menuRect, leaderboardData);
		menuRect.x = menuRect.x + num3 + num;
		DrawLeaderboard(menuRect, skipToLeaderboardData);
		menuRect.y = menuRect.y - num4 - num2 / 2f;
		menuRect.x = menuRect.x - num3 - num;
		DrawLeaderboard(menuRect, topPlayersLeaderboardData);
	}

	public void DrawCurrentScore()
	{
		Rect position = new Rect((int)((double)Screen.width * 0.5), (int)((double)Screen.height * 0.05), 90f, 52f);
		GUI.Box(position, string.Empty);
		GUI.Label(position, "\n  Score: " + score, leaderboardHeaderStyle);
	}

	public void RefreshLeaderboardData()
	{
		leaderboardData = LeaderboardManager.GetLeaderboardAsync(CurrentUser.Id, ConsoleUtilsManager.PrimaryServiceConfigId(), "HighScores", null);
		skipToLeaderboardData = LeaderboardManager.GetLeaderboardAsyncWithSkipToRank(CurrentUser.Id, ConsoleUtilsManager.PrimaryServiceConfigId(), "HighScores", (uint)((double)score / 2.0), 15u, null);
		topPlayersLeaderboardData = LeaderboardManager.GetLeaderboardAsync(CurrentUser.Id, ConsoleUtilsManager.PrimaryServiceConfigId(), "TopPlayers", null);
		OnScreenLog.Add("Refreshing leaderboard data");
	}

	public void HandleMenu(MenuLayout layout, Menu self)
	{
		if (!firstTime)
		{
			layout.Update(3);
			if (layout.AddButtonWithIndex("Refresh Leaderboard"))
			{
				RefreshLeaderboardData();
			}
			if (layout.AddButtonWithIndex("Increase My Score"))
			{
				score++;
				Events.SendScoreChanged(CurrentUser.UID, ref GameSessionId, score);
				OnScreenLog.Add("Sending updated score: " + score);
			}
			if (layout.AddButtonWithIndex("Complete Match"))
			{
				string empty = string.Empty;
				Events.SendGameCompleted(CurrentUser.UID, 0, ref GameSessionId, empty, 0, 0, 1, score);
				OnScreenLog.Add("Sending Match Complete: " + score);
				score = 0;
				GameSessionId = Guid.NewGuid();
			}
		}
	}
}
