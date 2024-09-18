using System;
using System.Collections.Generic;
using UnityEngine;

public class OnScreenLog : MonoBehaviour
{
	private static int msgCount = 0;

	private static List<string> log = new List<string>();

	public static int maxLines = 16;

	public int fontSize = 24;

	public int startx = 70;

	public int starty = 70;

	public float safeZonePercent;

	private int width;

	public int startAtPercentOfScreenHeight;

	public int startAtPercentOfScreenWidth;

	public int percentOfScreenWidth;

	private void Start()
	{
		int num = 0;
		int num2 = 0;
		if (safeZonePercent != 0f)
		{
			num = (int)((float)Screen.width * (safeZonePercent / 100f));
			num2 = (int)((float)Screen.height * (safeZonePercent / 100f));
		}
		if (startAtPercentOfScreenHeight != 0)
		{
			starty = (int)((float)(Screen.height * startAtPercentOfScreenHeight) / 100f) + num2;
		}
		if (startAtPercentOfScreenWidth != 0)
		{
			startx = (int)((float)(Screen.width * startAtPercentOfScreenWidth) / 100f) + num;
		}
		if (percentOfScreenWidth != 0)
		{
			width = (int)((float)((Screen.width - num * 2) * percentOfScreenWidth) / 100f);
		}
		else
		{
			width = Screen.width - startx * 2;
		}
	}

	private void Update()
	{
	}

	private void OnGUI()
	{
		GUIStyle style = GUI.skin.GetStyle("Label");
		style.fontSize = fontSize;
		style.alignment = TextAnchor.UpperLeft;
		style.wordWrap = false;
		float num = 0f;
		string text = string.Empty;
		for (int i = 0; i < log.Count; i++)
		{
			text = text + " " + log[i];
			text += "\n";
			num += style.lineHeight;
		}
		num += 6f;
		Rect position = new Rect(startx, starty, width, num);
		GUI.Box(position, "Log");
		GUI.Label(position, text, style);
	}

	public static void Add(string msg)
	{
		string text = msg.Replace("\r", " ");
		text = text.Replace("\n", " ");
		Console.WriteLine("[APP] " + text);
		log.Add(text);
		msgCount++;
		if (msgCount > maxLines)
		{
			log.RemoveAt(0);
		}
	}

	public static void Clear()
	{
		log.Clear();
	}
}
