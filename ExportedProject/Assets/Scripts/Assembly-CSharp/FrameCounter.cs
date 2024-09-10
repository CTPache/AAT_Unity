using UnityEngine;

public class FrameCounter : MonoBehaviour
{
	public static int fontSize = 24;

	public int frameCount;

	private int safeZone;

	private void Start()
	{
		safeZone = (int)((float)Screen.width * 0.05f);
	}

	private void Update()
	{
		frameCount++;
	}

	private void OnGUI()
	{
		GUIStyle style = GUI.skin.GetStyle("Label");
		style.fontSize = fontSize;
		style.alignment = TextAnchor.LowerLeft;
		style.wordWrap = false;
		GUIStyle style2 = GUI.skin.GetStyle("Box");
		style2.alignment = TextAnchor.UpperRight;
		float height = style.lineHeight + 16f;
		Rect position = new Rect(Screen.width - 200, Screen.height - 100, 200 - safeZone, height);
		GUI.Box(position, "frame", style2);
		GUI.Label(position, frameCount.ToString());
	}
}
