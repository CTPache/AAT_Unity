using System;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
	private List<IMenu> menuStack = new List<IMenu>();

	private MenuLayout layout;

	public int width;

	public int fontSize = 16;

	public Rect menuRect = new Rect(0f, 0f, 0f, 0f);

	public float safeZonePercent;

	public int startAtPercentOfScreenHeight;

	public int startAtPercentOfScreenWidth;

	public int percentOfScreenWidth;

	public MonoBehaviour menuScript;

	public bool useDPad = true;

	public void Start()
	{
		if (fontSize <= 0)
		{
			fontSize = 16;
		}
		if (safeZonePercent != 0f)
		{
			menuRect.x = (int)((float)Screen.width * (safeZonePercent / 100f));
			menuRect.y = (int)((float)Screen.height * (safeZonePercent / 100f));
		}
		if (startAtPercentOfScreenHeight != 0)
		{
			menuRect.y = (int)((float)(Screen.height * startAtPercentOfScreenHeight) / 100f);
		}
		if (startAtPercentOfScreenWidth != 0)
		{
			menuRect.x = (int)((float)(Screen.width * startAtPercentOfScreenWidth) / 100f);
		}
		if (percentOfScreenWidth != 0)
		{
			menuRect.width = (float)(int)((float)(Screen.width * percentOfScreenWidth) / 100f) - menuRect.x * 2f;
		}
		else if (menuRect.width == 0f)
		{
			menuRect.width = (float)Screen.width - menuRect.x * 2f;
		}
		layout = new MenuLayout(menuRect, fontSize, this);
		IMenu menu = menuScript as IMenu;
		if (menu == null)
		{
			throw new Exception("DemoUtils.Menu requires a script that implements IMenu.  Remember to set the \"Menu Script\" property.");
		}
		ClearMenu();
		PushMenu(menu);
	}

	public void ClearMenu()
	{
		if (menuStack.Count > 0)
		{
			menuStack.Clear();
			menuStack = new List<IMenu>();
		}
	}

	public void OnGUI()
	{
		if (menuStack.Count > 0)
		{
			menuStack[menuStack.Count - 1].HandleMenu(layout, this);
		}
	}

	public void PushMenu(IMenu menu)
	{
		menuStack.Add(menu);
		layout.SetSelectedItem(0);
	}

	public void PopMenu()
	{
		if (menuStack.Count > 1)
		{
			menuStack.RemoveAt(menuStack.Count - 1);
			layout.SetSelectedItem(0);
		}
	}
}
