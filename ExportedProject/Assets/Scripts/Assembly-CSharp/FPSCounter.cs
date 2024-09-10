using UnityEngine;

internal class FPSCounter : MonoBehaviour
{
	public float updateInterval = 0.5f;

	public int fontSize = 24;

	private int safeZone;

	private float accum;

	private float timeleft;

	private float fps;

	private int frames;

	private void Start()
	{
		safeZone = (int)((float)Screen.width * 0.05f);
	}

	private void Update()
	{
		timeleft -= Time.deltaTime;
		accum += Time.timeScale / Time.deltaTime;
		frames++;
		if ((double)timeleft <= 0.0)
		{
			fps = accum / (float)frames;
			timeleft = updateInterval;
			accum = 0f;
			frames = 0;
		}
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
		Rect position = new Rect(Screen.width - 200, Screen.height - 150, 200 - safeZone, height);
		GUI.Box(position, "fps", style2);
		GUI.Label(position, string.Format("{0:F2}", fps));
	}
}
