using UnityEngine;

public class TextWindow : IDrawMe
{
	public string text;

	public TextWindow(string output)
	{
		text = output;
	}

	public void Display(Rect boundingBox, int fontSize)
	{
		GUIStyle style = GUI.skin.GetStyle("Label");
		style.fontSize = fontSize;
		style.alignment = TextAnchor.UpperLeft;
		style.wordWrap = false;
		GUI.Label(boundingBox, text, style);
	}
}
