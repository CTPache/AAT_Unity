using System;
using System.Collections.Generic;
using ConsoleUtils;
using DataPlatform;
using UnityAOT;
using UnityEngine;
using UnityPlugin;
using UnityPluginLog;
using Users;

public class RTAandStatistics : MonoBehaviour, IMenu
{
	private ResourceRequest eventManifest;

	private bool firstTime = true;

	private bool haveRequestedSignIn;

	private DateTime lastFailedSubscribeTime = DateTime.Now;

	private List<string> resubscribeRequests = new List<string>();

	private GetObjectAsyncOp<UserStatisticsResult> statisticsData;

	private string kDoorsOpened = "DoorsOpened";

	private string kEnemyDefeats = "EnemyDefeats";

	private const int kTotalSubscriptions = 2;

	private int DoorsOpened;

	private int EnemysDefeated;

	private int SubscribeCount;

	private RTA m_RTA;

	private GUIStyle headerStyle = new GUIStyle();

	private GUIStyle valueStyle = new GUIStyle();

	private GUIStyle queryDataStyle = new GUIStyle();

	private string welcomeText = "\r\n RTA and Statistics Example\r\n ----------------------\r\n This example attempts to show some of the basics of using the RTA and Statistics systems in your game.\r\n NOTE that this example unlike the basic DataPlatform example requires you be\r\n      running on the UTDK.1 sandbox.\r\n\r\n * The statistics functionality allows you to pull / query the Microsoft servers for statistical data.\r\n * RTA is the complement to statistics, RTA is a push notification, or a statistics subscription\r\n   that you manage for a particular statistic you have defined.\r\n\r\n ** For example notice that when you kill an enemy or open a door the RTA table will update\r\n    but the statistics table will not until you manually refresh.\r\n\r\n ** Subscription failure will wait 30 seconds then attempt to recreate the RTA object which\r\n    seems to be the best way of handling subscription failures.\r\n";

	private static Guid GameSessionId = Guid.NewGuid();

	private User CurrentUser;

	private GUIStyle statHeaderStyle = new GUIStyle();

	private GUIStyle statStyle = new GUIStyle();

	private int unsubscribeCount;

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
		StatisticsManager.Create();
		statHeaderStyle.fontSize = 22;
		statHeaderStyle.normal.textColor = Color.cyan;
		statStyle.fontSize = 22;
		statStyle.normal.textColor = Color.white;
		headerStyle.fontSize = 22;
		headerStyle.normal.textColor = Color.cyan;
		valueStyle.fontSize = 22;
		valueStyle.normal.textColor = Color.white;
		queryDataStyle.fontSize = 25;
		queryDataStyle.normal.textColor = Color.red;
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
		if (firstTime && SanityCheckApplicationSetup() && eventManifest.isDone)
		{
			CurrentUser = UsersManager.Users[0];
			string text = ((TextAsset)eventManifest.asset).text;
			EventManager.CreateFromText(text);
			RTAManager.CreateAsync(CurrentUser.Id, OnRTACreated);
			RefreshStatisticsData();
			firstTime = false;
		}
		if (resubscribeRequests.Count > 0 && (DateTime.Now - lastFailedSubscribeTime).TotalMilliseconds > 30000.0)
		{
			OnScreenLog.Add("Attempting resubscribe...");
			m_RTA.OnStatisticChanged -= OnStatisticChanged;
			m_RTA.OnSubscribed -= OnSubscribed;
			m_RTA.Dispose();
			m_RTA = null;
			RTAManager.CreateAsync(CurrentUser.Id, OnRTACreated);
			resubscribeRequests.Clear();
			RefreshStatisticsData();
		}
	}

	private void OnRTACreated(RTA rta)
	{
		if (rta == null)
		{
			OnScreenLog.Add("ERROR: rta controller for my user could not be created!");
			return;
		}
		rta.OnStatisticChanged += OnStatisticChanged;
		rta.OnSubscribed += OnSubscribed;
		rta.SubscribeToStatistic(kDoorsOpened);
		rta.SubscribeToStatistic(kEnemyDefeats);
		m_RTA = rta;
	}

	private void OnStatisticChanged(uint hresult, RealTimeActivityStatisticChangeEventArgs args)
	{
		if (args == null)
		{
			return;
		}
		if (args.LatestStatistic.Name == kDoorsOpened)
		{
			try
			{
				DoorsOpened = Convert.ToInt32(args.LatestStatistic.Value);
			}
			catch
			{
			}
		}
		if (args.LatestStatistic.Name == kEnemyDefeats)
		{
			try
			{
				EnemysDefeated = Convert.ToInt32(args.LatestStatistic.Value);
			}
			catch
			{
			}
		}
		OnScreenLog.Add("StatChange: " + args.LatestStatistic.Name + " = " + args.LatestStatistic.Value + "\n");
	}

	private void OnSubscribed(uint hresult, RealTimeActivityStatisticChangeSubscription sub, string statName)
	{
		if (sub != null)
		{
			SubscribeCount++;
			OnScreenLog.Add("StatSubscribe: " + sub.StatisticName + " CURRENT STATE = " + sub.State.ToString() + "\n");
			return;
		}
		OnScreenLog.Add("SUBSCRIBE FAIL: [" + statName + "] [0x" + hresult.ToString("X8") + "]");
		lastFailedSubscribeTime = DateTime.Now;
		if (statName == kDoorsOpened)
		{
			OpenADoor();
			resubscribeRequests.Add(kDoorsOpened);
		}
		if (statName == kEnemyDefeats)
		{
			KillAnEnemy();
			resubscribeRequests.Add(kEnemyDefeats);
		}
	}

	private void OpenADoor()
	{
		Events.SendDoorOpened(CurrentUser.UID, ref GameSessionId, 100f, 0f, 0f, 0f);
		OnScreenLog.Add("Send DoorOpened event to XDP");
	}

	private void KillAnEnemy()
	{
		int enemyRoleId = 3;
		string empty = string.Empty;
		Events.SendEnemyDefeated(CurrentUser.UID, 0, ref GameSessionId, empty, 0, 0, ref GameSessionId, 0, 0, enemyRoleId, 0, 0f, 0f, 0f, 0);
		OnScreenLog.Add("Send EnemyDefeated event to XDP");
	}

	private void OnGUI()
	{
		DrawRTA();
		DrawStatistics();
	}

	private void DrawRTA()
	{
		if (m_RTA != null)
		{
			float num = (float)Screen.width * 0.03f;
			float num2 = (float)Screen.width * 0.03f;
			float width = (int)((double)Screen.width * 0.25 - (double)num);
			float height = 340f;
			Rect rect = new Rect(num, (float)Screen.height * 0.5f + num2, width, height);
			GUI.Box(rect, string.Empty);
			GUILayout.BeginArea(rect);
			GUILayout.Label("  RTA  :  Pushed Updates", headerStyle);
			GUILayout.Label(string.Empty, headerStyle);
			if (SubscribeCount >= 2)
			{
				GUILayout.Label("  " + kDoorsOpened, valueStyle);
				GUILayout.Label("  " + kEnemyDefeats, valueStyle);
			}
			else
			{
				GUILayout.Label("  Waiting for data...", queryDataStyle);
			}
			GUILayout.EndArea();
			rect = new Rect(rect.x + rect.width / 1.5f, rect.y, rect.width, rect.height);
			GUILayout.BeginArea(rect);
			GUILayout.Label(string.Empty, headerStyle);
			GUILayout.Label(string.Empty, headerStyle);
			if (SubscribeCount >= 2)
			{
				GUILayout.Label(DoorsOpened.ToString(), valueStyle);
				GUILayout.Label(EnemysDefeated.ToString(), valueStyle);
			}
			GUILayout.EndArea();
		}
	}

	public void DrawQueryingMessage(Rect menuRect)
	{
		GUILayout.BeginArea(menuRect);
		GUILayout.Label("Querying Statistics Data...", queryDataStyle);
		GUILayout.EndArea();
	}

	public void DrawStatisticData(Rect menuRect, GetObjectAsyncOp<UserStatisticsResult> sd)
	{
		GUI.Box(menuRect, string.Empty);
		GUILayout.BeginArea(menuRect);
		GUILayout.Label("  Statistics", statHeaderStyle);
		GUILayout.Label(string.Empty, statHeaderStyle);
		foreach (ServiceConfigurationStatistic item in sd.ResultObject)
		{
			foreach (Statistic item2 in item)
			{
				GUILayout.Label("  " + item2.Name, statStyle);
			}
		}
		GUILayout.EndArea();
		float width = menuRect.width;
		float x = menuRect.x;
		menuRect = new Rect(x + width / 1.7f, menuRect.y, menuRect.width, menuRect.height);
		GUILayout.BeginArea(menuRect);
		GUILayout.Label(string.Empty, statHeaderStyle);
		GUILayout.Label(string.Empty, statHeaderStyle);
		foreach (ServiceConfigurationStatistic item3 in sd.ResultObject)
		{
			foreach (Statistic item4 in item3)
			{
				GUILayout.Label(item4.Value, statStyle);
			}
		}
		GUILayout.EndArea();
		menuRect = new Rect(x + width / 1.4f, menuRect.y, menuRect.width, menuRect.height);
		GUILayout.BeginArea(menuRect);
		GUILayout.Label(string.Empty, statHeaderStyle);
		GUILayout.Label(string.Empty, statHeaderStyle);
		foreach (ServiceConfigurationStatistic item5 in sd.ResultObject)
		{
			foreach (Statistic item6 in item5)
			{
				GUILayout.Label(string.Concat("[", item6.Type, "]"), statStyle);
			}
		}
		GUILayout.EndArea();
	}

	private void DrawStatistic(Rect menuRect, GetObjectAsyncOp<UserStatisticsResult> sd)
	{
		if (sd != null && sd.IsComplete && sd.Success)
		{
			DrawStatisticData(menuRect, sd);
		}
		else
		{
			DrawQueryingMessage(menuRect);
		}
	}

	public void DrawStatistics()
	{
		float num = (float)Screen.width * 0.03f;
		float num2 = (float)Screen.width * 0.03f;
		float num3 = (int)((double)Screen.width * 0.25 - (double)num);
		float height = 340f;
		Rect menuRect = new Rect(num3 + num * 2f, (float)Screen.height * 0.5f + num2, num3, height);
		DrawStatistic(menuRect, statisticsData);
	}

	public void RefreshStatisticsData()
	{
		string[] statisticNames = new string[2] { kDoorsOpened, kEnemyDefeats };
		statisticsData = StatisticsManager.GetSingleUserStatisticsAsyncMultipleStats(CurrentUser.Id, CurrentUser.UID, ConsoleUtilsManager.PrimaryServiceConfigId(), statisticNames, null);
		OnScreenLog.Add("Refreshing statistics data");
	}

	private void OnUnsubscribed(AsyncStatus status, UnsubscribeActionOp op)
	{
		OnScreenLog.Add("Subscription to [" + op.StatisticName + "] terminated!");
		unsubscribeCount--;
		if (unsubscribeCount <= 0)
		{
			m_RTA.SubscribeToStatistic(kDoorsOpened);
			m_RTA.SubscribeToStatistic(kEnemyDefeats);
		}
	}

	public void HandleMenu(MenuLayout layout, Menu self)
	{
		if (!firstTime)
		{
			layout.Update(4);
			if (layout.AddButtonWithIndex("Refresh Statistics"))
			{
				RefreshStatisticsData();
			}
			if (layout.AddButtonWithIndex("Open a Door"))
			{
				OpenADoor();
			}
			if (layout.AddButtonWithIndex("Kill an Enemy"))
			{
				KillAnEnemy();
			}
			if (layout.AddButtonWithIndex("Refresh Subscription"))
			{
				unsubscribeCount = 2;
				m_RTA.UnsubscribeFromStatistic(kDoorsOpened, OnUnsubscribed);
				m_RTA.UnsubscribeFromStatistic(kEnemyDefeats, OnUnsubscribed);
			}
		}
	}
}
