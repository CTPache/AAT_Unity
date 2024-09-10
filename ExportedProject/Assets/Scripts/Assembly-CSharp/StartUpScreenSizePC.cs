using System.Collections.Generic;
using UnityEngine;

public class StartUpScreenSizePC : MonoBehaviour
{
	private static readonly string ST_PRERSF_SCREEN_WIDTH = "Screenmanager Resolution Width";

	private static readonly string ST_PRERSF_SCREEN_HEIGHT = "Screenmanager Resolution Height";

	private static readonly string ST_PRERSF_IS_FULLSCREEN = "Screenmanager Is Fullscreen mode";

	private static readonly Vector2Int ST_DEFAULT_SCREEN_SIZE = new Vector2Int(1280, 720);

	private List<Vector2Int> list_window_size = new List<Vector2Int>();

	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
	private void PCWindowStartUp()
	{
		Vector2Int screenList = GetScreenList();
		Screen.SetResolution(screenList.x, screenList.y, false);
	}

	private void OnApplicationQuit()
	{
		Vector2Int screenList = GetScreenList();
		PlayerPrefs.SetInt(ST_PRERSF_IS_FULLSCREEN, 0);
		PlayerPrefs.SetInt(ST_PRERSF_SCREEN_HEIGHT, screenList.y);
		PlayerPrefs.SetInt(ST_PRERSF_SCREEN_WIDTH, screenList.x);
	}

	private Vector2Int GetScreenList()
	{
		Vector2Int vector2Int = new Vector2Int(0, 0);
		Resolution[] resolutions = Screen.resolutions;
		Resolution[] array = resolutions;
		for (int i = 0; i < array.Length; i++)
		{
			Resolution resolution = array[i];
			if (resolution.height % 9 == 0 && resolution.width % 16 == 0)
			{
				list_window_size.Add(new Vector2Int(resolution.width, resolution.height));
			}
		}
		vector2Int = ((list_window_size.Count <= 0) ? vector2Int : list_window_size[list_window_size.Count - 1]);
		vector2Int = ((list_window_size.Count != 0 && (ST_DEFAULT_SCREEN_SIZE.x > vector2Int.x || ST_DEFAULT_SCREEN_SIZE.y > vector2Int.y)) ? new Vector2Int(list_window_size[list_window_size.Count - 1].x, list_window_size[list_window_size.Count - 1].y) : new Vector2Int(ST_DEFAULT_SCREEN_SIZE.x, ST_DEFAULT_SCREEN_SIZE.y));
		return vector2Int;
	}
}
