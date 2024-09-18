using System.Runtime.InteropServices;

public static class PS4DetectEnterButton
{
	public enum ParamID
	{
		SCE_SYSTEM_SERVICE_PARAM_ID_LANG = 1,
		SCE_SYSTEM_SERVICE_PARAM_ID_DATE_FORMAT = 2,
		SCE_SYSTEM_SERVICE_PARAM_ID_TIME_FORMAT = 3,
		SCE_SYSTEM_SERVICE_PARAM_ID_TIME_ZONE = 4,
		SCE_SYSTEM_SERVICE_PARAM_ID_SUMMERTIME = 5,
		SCE_SYSTEM_SERVICE_PARAM_ID_GAME_PARENTAL_LEVEL = 7,
		SCE_SYSTEM_SERVICE_PARAM_ID_ENTER_BUTTON_ASSIGN = 1000
	}

	private enum RESULT_ENTER_BUTTON_ASSIGN
	{
		SCE_SYSTEM_PARAM_ENTER_BUTTON_ASSIGN_CIRCLE = 0,
		SCE_SYSTEM_PARAM_ENTER_BUTTON_ASSIGN_CROSS = 1
	}

	public static bool IsEnterButtonAssignCircle()
	{
		return GetSceSystemServiceParam(1000) == 0;
	}

	public static int GetSceSystemServiceParam(int param_id)
	{
		return PRXsceSystemServiceParamGetInt(param_id);
	}

	[DllImport("PS4sceSystemServiceParamGetInt_plugin")]
	private static extern int PRXsceSystemServiceParamGetInt(int paramId);
}
