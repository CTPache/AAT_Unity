using System;
using System.Diagnostics;

namespace DataPlatform
{
	public class Events
	{
		public static void SendBase(string UserId, ref Guid PlayerSessionId)
		{
			DynamicEvent dynamicEvent = new DynamicEvent("Base");
			dynamicEvent.AddParamUnicodeString(null, UserId);
			dynamicEvent.AddParamGUID(null, PlayerSessionId);
			dynamicEvent.Send();
		}

		public static void SendDoorOpened(string UserId, ref Guid PlayerSessionId, float CompletionPercent, float LocationX, float LocationY, float LocationZ)
		{
			DynamicEvent dynamicEvent = new DynamicEvent("DoorOpened");
			dynamicEvent.AddParamUnicodeString(null, UserId);
			dynamicEvent.AddParamGUID(null, PlayerSessionId);
			dynamicEvent.AddParamFloat(null, CompletionPercent);
			dynamicEvent.AddParamFloat(null, LocationX);
			dynamicEvent.AddParamFloat(null, LocationY);
			dynamicEvent.AddParamFloat(null, LocationZ);
			dynamicEvent.Send();
		}

		public static void SendEnemyDefeated(string UserId, int SectionId, ref Guid PlayerSessionId, string MultiplayerCorrelationId, int GameplayModeId, int DifficultyLevelId, ref Guid RoundId, int PlayerRoleId, int PlayerWeaponId, int EnemyRoleId, int KillTypeId, float LocationX, float LocationY, float LocationZ, int EnemyWeaponId)
		{
			DynamicEvent dynamicEvent = new DynamicEvent("EnemyDefeated");
			dynamicEvent.AddParamUnicodeString(null, UserId);
			dynamicEvent.AddParamInt32(null, SectionId);
			dynamicEvent.AddParamGUID(null, PlayerSessionId);
			dynamicEvent.AddParamUnicodeString(null, MultiplayerCorrelationId);
			dynamicEvent.AddParamInt32(null, GameplayModeId);
			dynamicEvent.AddParamInt32(null, DifficultyLevelId);
			dynamicEvent.AddParamGUID(null, RoundId);
			dynamicEvent.AddParamInt32(null, PlayerRoleId);
			dynamicEvent.AddParamInt32(null, PlayerWeaponId);
			dynamicEvent.AddParamInt32(null, EnemyRoleId);
			dynamicEvent.AddParamInt32(null, KillTypeId);
			dynamicEvent.AddParamFloat(null, LocationX);
			dynamicEvent.AddParamFloat(null, LocationY);
			dynamicEvent.AddParamFloat(null, LocationZ);
			dynamicEvent.AddParamInt32(null, EnemyWeaponId);
			dynamicEvent.Send();
		}

		public static void SendGameCompleted(string UserId, int SectionId, ref Guid PlayerSessionId, string MultiplayerCorrelationId, int GameplayModeId, int DifficultyLevelId, int ExitStatusId, int Score)
		{
			DynamicEvent dynamicEvent = new DynamicEvent("GameCompleted");
			dynamicEvent.AddParamUnicodeString(null, UserId);
			dynamicEvent.AddParamInt32(null, SectionId);
			dynamicEvent.AddParamGUID(null, PlayerSessionId);
			dynamicEvent.AddParamUnicodeString(null, MultiplayerCorrelationId);
			dynamicEvent.AddParamInt32(null, GameplayModeId);
			dynamicEvent.AddParamInt32(null, DifficultyLevelId);
			dynamicEvent.AddParamInt32(null, ExitStatusId);
			dynamicEvent.AddParamInt32(null, Score);
			dynamicEvent.Send();
		}

		public static void SendGameProgress(string UserId, ref Guid PlayerSessionId, float CompletionPercent)
		{
			DynamicEvent dynamicEvent = new DynamicEvent("GameProgress");
			dynamicEvent.AddParamUnicodeString(null, UserId);
			dynamicEvent.AddParamGUID(null, PlayerSessionId);
			dynamicEvent.AddParamFloat(null, CompletionPercent);
			dynamicEvent.Send();
		}

		public static void SendMediaUsage(string AppSessionId, string AppSessionStartDateTime, uint UserIdType, string UserId, string SubscriptionTierType, string SubscriptionTier, string MediaType, string ProviderId, string ProviderMediaId, string ProviderMediaInstanceId, ref Guid BingId, ulong MediaLengthMs, uint MediaControlAction, float PlaybackSpeed, ulong MediaPositionMs, ulong PlaybackDurationMs, string AcquisitionType, string AcquisitionContext, string AcquisitionContextType, string AcquisitionContextId, int PlaybackIsStream, int PlaybackIsTethered, string MarketplaceLocation, string ContentLocale, float TimeZoneOffset, uint ScreenState)
		{
			DynamicEvent dynamicEvent = new DynamicEvent("MediaUsage");
			dynamicEvent.AddParamUnicodeString(null, AppSessionId);
			dynamicEvent.AddParamUnicodeString(null, AppSessionStartDateTime);
			dynamicEvent.AddParamUInt32(null, UserIdType);
			dynamicEvent.AddParamUnicodeString(null, UserId);
			dynamicEvent.AddParamUnicodeString(null, SubscriptionTierType);
			dynamicEvent.AddParamUnicodeString(null, SubscriptionTier);
			dynamicEvent.AddParamUnicodeString(null, MediaType);
			dynamicEvent.AddParamUnicodeString(null, ProviderId);
			dynamicEvent.AddParamUnicodeString(null, ProviderMediaId);
			dynamicEvent.AddParamUnicodeString(null, ProviderMediaInstanceId);
			dynamicEvent.AddParamGUID(null, BingId);
			dynamicEvent.AddParamUInt64(null, MediaLengthMs);
			dynamicEvent.AddParamUInt32(null, MediaControlAction);
			dynamicEvent.AddParamFloat(null, PlaybackSpeed);
			dynamicEvent.AddParamUInt64(null, MediaPositionMs);
			dynamicEvent.AddParamUInt64(null, PlaybackDurationMs);
			dynamicEvent.AddParamUnicodeString(null, AcquisitionType);
			dynamicEvent.AddParamUnicodeString(null, AcquisitionContext);
			dynamicEvent.AddParamUnicodeString(null, AcquisitionContextType);
			dynamicEvent.AddParamUnicodeString(null, AcquisitionContextId);
			dynamicEvent.AddParamInt32(null, PlaybackIsStream);
			dynamicEvent.AddParamInt32(null, PlaybackIsTethered);
			dynamicEvent.AddParamUnicodeString(null, MarketplaceLocation);
			dynamicEvent.AddParamUnicodeString(null, ContentLocale);
			dynamicEvent.AddParamFloat(null, TimeZoneOffset);
			dynamicEvent.AddParamUInt32(null, ScreenState);
			dynamicEvent.Send();
		}

		public static void SendMultiplayerRoundEnd(string UserId, ref Guid RoundId, int SectionId, ref Guid PlayerSessionId, string MultiplayerCorrelationId, int GameplayModeId, int MatchTypeId, int DifficultyLevelId, float TimeInSeconds, int ExitStatusId)
		{
			DynamicEvent dynamicEvent = new DynamicEvent("MultiplayerRoundEnd");
			dynamicEvent.AddParamUnicodeString(null, UserId);
			dynamicEvent.AddParamGUID(null, RoundId);
			dynamicEvent.AddParamInt32(null, SectionId);
			dynamicEvent.AddParamGUID(null, PlayerSessionId);
			dynamicEvent.AddParamUnicodeString(null, MultiplayerCorrelationId);
			dynamicEvent.AddParamInt32(null, GameplayModeId);
			dynamicEvent.AddParamInt32(null, MatchTypeId);
			dynamicEvent.AddParamInt32(null, DifficultyLevelId);
			dynamicEvent.AddParamFloat(null, TimeInSeconds);
			dynamicEvent.AddParamInt32(null, ExitStatusId);
			dynamicEvent.Send();
		}

		public static void SendMultiplayerRoundStart(string UserId, ref Guid RoundId, int SectionId, ref Guid PlayerSessionId, string MultiplayerCorrelationId, int GameplayModeId, int MatchTypeId, int DifficultyLevelId)
		{
			DynamicEvent dynamicEvent = new DynamicEvent("MultiplayerRoundStart");
			dynamicEvent.AddParamUnicodeString(null, UserId);
			dynamicEvent.AddParamGUID(null, RoundId);
			dynamicEvent.AddParamInt32(null, SectionId);
			dynamicEvent.AddParamGUID(null, PlayerSessionId);
			dynamicEvent.AddParamUnicodeString(null, MultiplayerCorrelationId);
			dynamicEvent.AddParamInt32(null, GameplayModeId);
			dynamicEvent.AddParamInt32(null, MatchTypeId);
			dynamicEvent.AddParamInt32(null, DifficultyLevelId);
			dynamicEvent.Send();
		}

		public static void SendObjectiveEnd(string UserId, int SectionId, ref Guid PlayerSessionId, string MultiplayerCorrelationId, int GameplayModeId, int DifficultyLevelId, int ObjectiveId, int ExitStatusId)
		{
			DynamicEvent dynamicEvent = new DynamicEvent("ObjectiveEnd");
			dynamicEvent.AddParamUnicodeString(null, UserId);
			dynamicEvent.AddParamInt32(null, SectionId);
			dynamicEvent.AddParamGUID(null, PlayerSessionId);
			dynamicEvent.AddParamUnicodeString(null, MultiplayerCorrelationId);
			dynamicEvent.AddParamInt32(null, GameplayModeId);
			dynamicEvent.AddParamInt32(null, DifficultyLevelId);
			dynamicEvent.AddParamInt32(null, ObjectiveId);
			dynamicEvent.AddParamInt32(null, ExitStatusId);
			dynamicEvent.Send();
		}

		public static void SendObjectiveStart(string UserId, int SectionId, ref Guid PlayerSessionId, string MultiplayerCorrelationId, int GameplayModeId, int DifficultyLevelId, int ObjectiveId)
		{
			DynamicEvent dynamicEvent = new DynamicEvent("ObjectiveStart");
			dynamicEvent.AddParamUnicodeString(null, UserId);
			dynamicEvent.AddParamInt32(null, SectionId);
			dynamicEvent.AddParamGUID(null, PlayerSessionId);
			dynamicEvent.AddParamUnicodeString(null, MultiplayerCorrelationId);
			dynamicEvent.AddParamInt32(null, GameplayModeId);
			dynamicEvent.AddParamInt32(null, DifficultyLevelId);
			dynamicEvent.AddParamInt32(null, ObjectiveId);
			dynamicEvent.Send();
		}

		public static void SendPageAction(string UserId, ref Guid PlayerSessionId, int ActionTypeId, int ActionInputMethodId, string Page, string TemplateId, string DestinationPage, string Content)
		{
			DynamicEvent dynamicEvent = new DynamicEvent("PageAction");
			dynamicEvent.AddParamUnicodeString(null, UserId);
			dynamicEvent.AddParamGUID(null, PlayerSessionId);
			dynamicEvent.AddParamInt32(null, ActionTypeId);
			dynamicEvent.AddParamInt32(null, ActionInputMethodId);
			dynamicEvent.AddParamUnicodeString(null, Page);
			dynamicEvent.AddParamUnicodeString(null, TemplateId);
			dynamicEvent.AddParamUnicodeString(null, DestinationPage);
			dynamicEvent.AddParamUnicodeString(null, Content);
			dynamicEvent.Send();
		}

		public static void SendPageView(string UserId, ref Guid PlayerSessionId, string Page, string RefererPage, int PageTypeId, string PageTags, string TemplateId, string Content)
		{
			DynamicEvent dynamicEvent = new DynamicEvent("PageView");
			dynamicEvent.AddParamUnicodeString(null, UserId);
			dynamicEvent.AddParamGUID(null, PlayerSessionId);
			dynamicEvent.AddParamUnicodeString(null, Page);
			dynamicEvent.AddParamUnicodeString(null, RefererPage);
			dynamicEvent.AddParamInt32(null, PageTypeId);
			dynamicEvent.AddParamUnicodeString(null, PageTags);
			dynamicEvent.AddParamUnicodeString(null, TemplateId);
			dynamicEvent.AddParamUnicodeString(null, Content);
			dynamicEvent.Send();
		}

		public static void SendPlayerSessionEnd(string UserId, ref Guid PlayerSessionId, string MultiplayerCorrelationId, int GameplayModeId, int DifficultyLevelId, int ExitStatusId)
		{
			DynamicEvent dynamicEvent = new DynamicEvent("PlayerSessionEnd");
			dynamicEvent.AddParamUnicodeString(null, UserId);
			dynamicEvent.AddParamGUID(null, PlayerSessionId);
			dynamicEvent.AddParamUnicodeString(null, MultiplayerCorrelationId);
			dynamicEvent.AddParamInt32(null, GameplayModeId);
			dynamicEvent.AddParamInt32(null, DifficultyLevelId);
			dynamicEvent.AddParamInt32(null, ExitStatusId);
			dynamicEvent.Send();
		}

		public static void SendPlayerSessionPause(string UserId, ref Guid PlayerSessionId, string MultiplayerCorrelationId)
		{
			DynamicEvent dynamicEvent = new DynamicEvent("PlayerSessionPause");
			dynamicEvent.AddParamUnicodeString(null, UserId);
			dynamicEvent.AddParamGUID(null, PlayerSessionId);
			dynamicEvent.AddParamUnicodeString(null, MultiplayerCorrelationId);
			dynamicEvent.Send();
		}

		public static void SendPlayerSessionResume(string UserId, ref Guid PlayerSessionId, string MultiplayerCorrelationId, int GameplayModeId, int DifficultyLevelId)
		{
			DynamicEvent dynamicEvent = new DynamicEvent("PlayerSessionResume");
			dynamicEvent.AddParamUnicodeString(null, UserId);
			dynamicEvent.AddParamGUID(null, PlayerSessionId);
			dynamicEvent.AddParamUnicodeString(null, MultiplayerCorrelationId);
			dynamicEvent.AddParamInt32(null, GameplayModeId);
			dynamicEvent.AddParamInt32(null, DifficultyLevelId);
			dynamicEvent.Send();
		}

		public static void SendPlayerSessionStart(string UserId, ref Guid PlayerSessionId, string MultiplayerCorrelationId, int GameplayModeId, int DifficultyLevelId)
		{
			DynamicEvent dynamicEvent = new DynamicEvent("PlayerSessionStart");
			dynamicEvent.AddParamUnicodeString(null, UserId);
			dynamicEvent.AddParamGUID(null, PlayerSessionId);
			dynamicEvent.AddParamUnicodeString(null, MultiplayerCorrelationId);
			dynamicEvent.AddParamInt32(null, GameplayModeId);
			dynamicEvent.AddParamInt32(null, DifficultyLevelId);
			dynamicEvent.Send();
		}

		public static void SendPlaySessionStart(string UserId, ref Guid PlayerSessionId, string MultiplayerCorrelationId, int GameplayModeId, int DifficultyLevelId, ulong StartTime)
		{
			DynamicEvent dynamicEvent = new DynamicEvent("PlaySessionStart");
			dynamicEvent.AddParamUnicodeString(null, UserId);
			dynamicEvent.AddParamGUID(null, PlayerSessionId);
			dynamicEvent.AddParamUnicodeString(null, MultiplayerCorrelationId);
			dynamicEvent.AddParamInt32(null, GameplayModeId);
			dynamicEvent.AddParamInt32(null, DifficultyLevelId);
			dynamicEvent.AddParamUInt64(null, StartTime);
			dynamicEvent.Send();
		}

		public static void SendSceneChanged(string UserId, ref Guid PlayerSessionId, int SceneId)
		{
			DynamicEvent dynamicEvent = new DynamicEvent("SceneChanged");
			dynamicEvent.AddParamUnicodeString(null, UserId);
			dynamicEvent.AddParamGUID(null, PlayerSessionId);
			dynamicEvent.AddParamInt32(null, SceneId);
			dynamicEvent.Send();
		}

		public static void SendScoreChanged(string UserId, ref Guid PlayerSessionId, int CurrentScore)
		{
			DynamicEvent dynamicEvent = new DynamicEvent("ScoreChanged");
			dynamicEvent.AddParamUnicodeString(null, UserId);
			dynamicEvent.AddParamGUID(null, PlayerSessionId);
			dynamicEvent.AddParamInt32(null, CurrentScore);
			dynamicEvent.Send();
		}

		public static void SendSectionEnd(string UserId, int SectionId, ref Guid PlayerSessionId, string MultiplayerCorrelationId, int GameplayModeId, int DifficultyLevelId, int ExitStatusId)
		{
			DynamicEvent dynamicEvent = new DynamicEvent("SectionEnd");
			dynamicEvent.AddParamUnicodeString(null, UserId);
			dynamicEvent.AddParamInt32(null, SectionId);
			dynamicEvent.AddParamGUID(null, PlayerSessionId);
			dynamicEvent.AddParamUnicodeString(null, MultiplayerCorrelationId);
			dynamicEvent.AddParamInt32(null, GameplayModeId);
			dynamicEvent.AddParamInt32(null, DifficultyLevelId);
			dynamicEvent.AddParamInt32(null, ExitStatusId);
			dynamicEvent.Send();
		}

		public static void SendSectionStart(string UserId, int SectionId, ref Guid PlayerSessionId, string MultiplayerCorrelationId, int GameplayModeId, int DifficultyLevelId)
		{
			DynamicEvent dynamicEvent = new DynamicEvent("SectionStart");
			dynamicEvent.AddParamUnicodeString(null, UserId);
			dynamicEvent.AddParamInt32(null, SectionId);
			dynamicEvent.AddParamGUID(null, PlayerSessionId);
			dynamicEvent.AddParamUnicodeString(null, MultiplayerCorrelationId);
			dynamicEvent.AddParamInt32(null, GameplayModeId);
			dynamicEvent.AddParamInt32(null, DifficultyLevelId);
			dynamicEvent.Send();
		}

		public static void SendSplashScreen(string UserId, ref Guid PlayerSessionId, string Page, string RefererPage, int PageTypeId, string PageTags, string TemplateId, string Content)
		{
			DynamicEvent dynamicEvent = new DynamicEvent("SplashScreen");
			dynamicEvent.AddParamUnicodeString(null, UserId);
			dynamicEvent.AddParamGUID(null, PlayerSessionId);
			dynamicEvent.AddParamUnicodeString(null, Page);
			dynamicEvent.AddParamUnicodeString(null, RefererPage);
			dynamicEvent.AddParamInt32(null, PageTypeId);
			dynamicEvent.AddParamUnicodeString(null, PageTags);
			dynamicEvent.AddParamUnicodeString(null, TemplateId);
			dynamicEvent.AddParamUnicodeString(null, Content);
			dynamicEvent.Send();
		}

		public static void SendViewOffer(string UserId, ref Guid PlayerSessionId, ref Guid OfferGuid, ref Guid ProductGuid)
		{
			DynamicEvent dynamicEvent = new DynamicEvent("ViewOffer");
			dynamicEvent.AddParamUnicodeString(null, UserId);
			dynamicEvent.AddParamGUID(null, PlayerSessionId);
			dynamicEvent.AddParamGUID(null, OfferGuid);
			dynamicEvent.AddParamGUID(null, ProductGuid);
			dynamicEvent.Send();
		}

		[Conditional("UNITY_XBOXONE")]
		public static void SendButtonPress(string Button)
		{
		}
	}
}
