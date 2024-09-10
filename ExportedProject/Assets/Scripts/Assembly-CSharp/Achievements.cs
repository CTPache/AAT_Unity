using System;
using System.Text;
using ConsoleUtils;
using DataPlatform;
using UnityAOT;
using UnityEngine;
using UnityPlugin;
using Users;

public class Achievements : MonoBehaviour, IMenu
{
	private ResourceRequest eventManifest;

	private bool firstTime = true;

	private bool haveRequestedSignIn;

	private string welcomeText = "\r\nAchievements Example\r\n----------------------\r\nThis example attempts to show some of the basics of the Achievements system.\r\nNOTE that this example unlike the basic DataPlatform example requires you be\r\n     running on the UTDK.1 sandbox.\r\n";

	private static Guid GameSessionId = Guid.NewGuid();

	private User CurrentUser;

	public void DisplayText(string text)
	{
		base.gameObject.GetComponent<DisplayWindow>().SetFromText(text);
	}

	private void Start()
	{
		DataPlatformPlugin.InitializePlugin(0);
		UsersManager.Create();
		AchievementsManager.Create();
		eventManifest = Resources.LoadAsync("AngryBots");
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
			AchievementsManager.OnUnlockNotifications += AchievementsManager_OnUnlockNotifications;
			NotificationManager.SetNotificationPositionHint(NotificationPositionHint.BottomRight);
			Events.SendDoorOpened(CurrentUser.UID, ref GameSessionId, 100f, 0f, 0f, 0f);
			OnScreenLog.Add("Send DoorOpened event to XDP");
			firstTime = false;
		}
	}

	private void AchievementsManager_OnUnlockNotifications(AchievementUnlockedEventArgs payload)
	{
		OnScreenLog.Add("Achievement notice: " + payload.AchievementId + " by user: " + payload.XboxUserId);
	}

	private void OnAchievementSnapshotReady(AchievementsResult achievements, GetObjectAsyncOp<AchievementsResult> op)
	{
		OnScreenLog.Add("Achievement snapshot has arrived");
		if (!op.Success)
		{
			OnScreenLog.Add("Achievement snapshot failed HRESULT: 0x" + op.Result.ToString("X8"));
			return;
		}
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append("\nAchievements\n---------------------------\n\n");
		for (int i = 0; i < achievements.Items.Length; i++)
		{
			Achievement achievement = achievements.Items[i];
			bool flag = achievement.ProgressState == AchievementProgressState.Achieved;
			stringBuilder.Append(string.Format("[{0}] {1} - {2}\n", (!flag) ? "  " : "x", achievement.Name.PadRight(20), (!flag) ? achievement.LockedDescription : achievement.UnlockedDescription));
		}
		DisplayText(stringBuilder.ToString());
	}

	public void HandleMenu(MenuLayout layout, Menu self)
	{
		if (!firstTime)
		{
			layout.Update(2);
			if (layout.AddButtonWithIndex("Show Achievements UI"))
			{
				AchievementsManager.LaunchAchievementsUIAsync((uint)CurrentUser.Id, ConsoleUtilsManager.TitleIdInt(), null);
				OnScreenLog.Add("Bringing up achievements UI");
			}
			if (layout.AddButtonWithIndex("Grab Achievement Snapshot"))
			{
				AchievementsManager.GetAchievementsForTitleIdAsync(CurrentUser.Id, CurrentUser.UID, ConsoleUtilsManager.TitleIdInt(), AchievementType.All, false, AchievementOrderBy.TitleId, 0u, 0u, OnAchievementSnapshotReady);
				OnScreenLog.Add("Achievement snapshot request has been sent");
			}
		}
	}
}
