using UnityEngine;

public class DisplayWindow : MonoBehaviour
{
	public Texture2D backgroundImage;

	public int width;

	public int fontSize = 16;

	public Rect windowRect = new Rect(0f, 0f, 0f, 0f);

	public float safeZonePercent;

	public int startAtPercentOfScreenWidth;

	public int startAtPercentOfScreenHeight;

	public int percentOfScreenWidth;

	public int percentOfScreenHeight;

	public string labelText = "Info";

	private IDrawMe currentDisplay;

	private TextWindow textWindow = new TextWindow(string.Empty);

	public void SetCurrentDisplay(IDrawMe display)
	{
		currentDisplay = display;
	}

	public void SetFromText(string text)
	{
		textWindow.text = text;
		SetCurrentDisplay(textWindow);
	}

	public string GetCurrentText()
	{
		return textWindow.text;
	}

	public void Start()
	{
		if (fontSize <= 0)
		{
			fontSize = 16;
		}
		int num = 0;
		int num2 = 0;
		if (safeZonePercent != 0f)
		{
			num = (int)((float)Screen.width * (safeZonePercent / 100f));
			num2 = (int)((float)Screen.height * (safeZonePercent / 100f));
			windowRect.x = num;
			windowRect.y = num2;
		}
		if (startAtPercentOfScreenHeight != 0)
		{
			windowRect.y = (int)((float)(Screen.height * startAtPercentOfScreenHeight) / 100f) + num2;
		}
		if (startAtPercentOfScreenWidth != 0)
		{
			windowRect.x = (int)((float)(Screen.width * startAtPercentOfScreenWidth) / 100f) + num;
		}
		if (percentOfScreenWidth != 0)
		{
			windowRect.width = (int)((float)(Screen.width * percentOfScreenWidth) / 100f) - num * 2;
		}
		else if (windowRect.width == 0f)
		{
			windowRect.width = (float)Screen.width - windowRect.x * 2f;
		}
		if (percentOfScreenHeight != 0)
		{
			windowRect.height = (int)((float)(Screen.height * percentOfScreenHeight) / 100f) - num2 * 2;
		}
		else if (windowRect.height == 0f)
		{
			windowRect.height = (float)Screen.height - windowRect.y * 2f;
		}
	}

	public void OnGUI()
	{
		if (backgroundImage != null)
		{
			GUI.Box(windowRect, backgroundImage);
		}
		GUI.Box(windowRect, labelText);
		if (currentDisplay != null)
		{
			currentDisplay.Display(windowRect, fontSize);
		}
	}
}
