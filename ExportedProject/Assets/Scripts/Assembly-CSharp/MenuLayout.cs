using System;
using UnityEngine;

public class MenuLayout
{
	private Rect bounds;

	private int ySpace;

	private int y;

	private GUIStyle style;

	private GUIStyle styleSelected;

	private GUIStyle toggleStyle;

	private GUIStyle toggleStyleSelected;

	private GUIStyle activeToggleStyle;

	private GUIStyle activeToggleStyleSelected;

	public int selectedItemIndex;

	private bool buttonPressed;

	private bool backButtonPressed;

	private int numItems;

	private int fontSize = 16;

	private static DateTime lastOperationTime = DateTime.Now;

	private int currCount;

	private Menu mSelf;

	public int Selected
	{
		get
		{
			return selectedItemIndex;
		}
		set
		{
			SetSelectedItem(value);
		}
	}

	public MenuLayout(Rect menuBounds, int itemFontSize, Menu self)
	{
		mSelf = self;
		bounds = menuBounds;
		fontSize = itemFontSize;
	}

	private void DoLayout(int itemCount)
	{
		numItems = itemCount;
		style = new GUIStyle(GUI.skin.GetStyle("Button"));
		style.fontSize = fontSize;
		style.alignment = TextAnchor.MiddleCenter;
		styleSelected = new GUIStyle(GUI.skin.GetStyle("Button"));
		styleSelected.fontSize = fontSize + 8;
		styleSelected.alignment = TextAnchor.MiddleCenter;
		styleSelected.fontStyle = FontStyle.BoldAndItalic;
		toggleStyle = new GUIStyle(GUI.skin.GetStyle("Label"));
		toggleStyle.normal.textColor = Color.black;
		toggleStyle.fontSize = fontSize;
		toggleStyle.alignment = TextAnchor.MiddleLeft;
		toggleStyleSelected = new GUIStyle(GUI.skin.GetStyle("Label"));
		toggleStyleSelected.normal.textColor = Color.black;
		toggleStyleSelected.fontSize = fontSize + 8;
		toggleStyleSelected.alignment = TextAnchor.MiddleLeft;
		toggleStyleSelected.fontStyle = FontStyle.BoldAndItalic;
		activeToggleStyle = new GUIStyle(GUI.skin.GetStyle("Label"));
		activeToggleStyle.fontSize = fontSize;
		activeToggleStyle.alignment = TextAnchor.MiddleLeft;
		activeToggleStyleSelected = new GUIStyle(GUI.skin.GetStyle("Label"));
		activeToggleStyleSelected.fontSize = fontSize + 8;
		activeToggleStyleSelected.alignment = TextAnchor.MiddleLeft;
		activeToggleStyleSelected.fontStyle = FontStyle.BoldAndItalic;
		bounds.height = style.fontSize + 16;
		ySpace = 8;
		y = (int)bounds.y;
		currCount = 0;
	}

	public void SetSelectedItem(int index)
	{
		if (index < 0)
		{
			selectedItemIndex = 0;
		}
		else if (index > numItems - 1)
		{
			selectedItemIndex = index;
		}
	}

	public void ItemNext()
	{
		if (numItems > 0)
		{
			selectedItemIndex++;
			if (selectedItemIndex >= numItems)
			{
				selectedItemIndex = 0;
			}
		}
	}

	public void ItemPrev()
	{
		if (numItems > 0)
		{
			selectedItemIndex--;
			if (selectedItemIndex < 0)
			{
				selectedItemIndex = numItems - 1;
			}
		}
	}

	public void Update(int itemCount)
	{
		DoLayout(itemCount);
		HandleInput(mSelf.useDPad);
	}

	private void HandleInput(bool useDPad)
	{
		bool flag = (DateTime.Now - lastOperationTime).TotalMilliseconds > 150.0;
		buttonPressed = false;
		backButtonPressed = false;
		if (padCtrl.instance.InputGetButtonDown("Fire1") && flag)
		{
			lastOperationTime = DateTime.Now;
			buttonPressed = true;
			return;
		}
		if (padCtrl.instance.InputGetButtonDown("Fire2") && flag)
		{
			lastOperationTime = DateTime.Now;
			backButtonPressed = true;
			return;
		}
		float axis = Input.GetAxis("Vertical");
		bool flag2 = (double)axis <= -0.99;
		bool flag3 = (double)axis >= 0.99;
		if (useDPad && !flag3 && padCtrl.instance.InputGetKeyDown(KeyCode.JoystickButton12))
		{
			flag3 = true;
			axis = 1f;
		}
		if (useDPad && !flag2 && padCtrl.instance.InputGetKeyDown(KeyCode.JoystickButton13))
		{
			flag2 = true;
			axis = -1f;
		}
		if (flag2 && flag)
		{
			ItemNext();
			lastOperationTime = DateTime.Now;
		}
		if (flag3 && flag)
		{
			ItemPrev();
			lastOperationTime = DateTime.Now;
		}
	}

	private static bool ClickOncePerFrame()
	{
		return Event.current.type == EventType.Layout;
	}

	private bool AddButton(string text, bool enabled = true, bool selected = false)
	{
		GUI.enabled = enabled;
		if (selected)
		{
			Rect rect = GetRect();
			rect.x -= 1f;
			rect.y -= 1f;
			rect.width += 2f;
			rect.height += 2f;
			GUI.Box(rect, string.Empty);
		}
		bool result = GUI.Button(GetRect(), text, (!selected) ? style : styleSelected);
		y += (int)(bounds.height + (float)ySpace);
		return result;
	}

	public bool AddButtonWithIndex(string name, bool enabled = true)
	{
		bool result = false;
		if (AddButton(name, enabled, selectedItemIndex == currCount))
		{
			selectedItemIndex = currCount;
			result = true;
		}
		else if (buttonPressed && enabled && selectedItemIndex == currCount)
		{
			result = ClickOncePerFrame();
			buttonPressed = false;
		}
		currCount++;
		return result;
	}

	private bool AddToggleAt(string text, bool enabled, ref bool state, bool selected)
	{
		GUI.enabled = enabled;
		if (selected)
		{
			Rect rect = GetRect();
			rect.x -= 1f;
			rect.y -= 1f;
			rect.width += 2f;
			rect.height += 2f;
			GUI.Box(rect, string.Empty);
		}
		bool result = GUI.Button(GetRect(), ((!state) ? "[  ] " : "[x] ") + text, state ? ((!selected) ? activeToggleStyle : activeToggleStyleSelected) : ((!selected) ? toggleStyle : toggleStyleSelected));
		y += (int)(bounds.height + (float)ySpace);
		return result;
	}

	public bool AddToggle(string name, ref bool state, bool enabled = true)
	{
		bool flag = false;
		if (AddToggleAt(name, enabled, ref state, selectedItemIndex == currCount))
		{
			selectedItemIndex = currCount;
			flag = true;
		}
		else if (buttonPressed && enabled && selectedItemIndex == currCount)
		{
			flag = ClickOncePerFrame();
			buttonPressed = false;
		}
		currCount++;
		if (flag)
		{
			state = !state;
		}
		return flag;
	}

	public bool AddBackButtonWithIndex(string name, bool enabled = true)
	{
		bool result = false;
		if (AddButton(name, enabled, selectedItemIndex == currCount))
		{
			selectedItemIndex = currCount;
			result = true;
		}
		else if (buttonPressed && enabled && selectedItemIndex == currCount)
		{
			result = ClickOncePerFrame();
			buttonPressed = false;
		}
		else if (backButtonPressed && enabled)
		{
			result = ClickOncePerFrame();
			backButtonPressed = false;
		}
		currCount++;
		return result;
	}

	public void AddLabel(string text, bool enabled = true)
	{
		GUI.enabled = enabled;
		GUI.Label(GetRect(), text);
		y += style.fontSize + ySpace;
	}

	public Rect GetRect()
	{
		return new Rect(bounds.x, y, bounds.width, bounds.height);
	}
}
