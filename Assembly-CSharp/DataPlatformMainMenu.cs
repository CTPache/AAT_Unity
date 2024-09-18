using DataPlatform;
using UnityEngine;

public class DataPlatformMainMenu : MonoBehaviour, IMenu
{
	private ResourceRequest eventManifest;

	private bool firstTime = true;

	public void DisplayText(string text)
	{
		base.gameObject.GetComponent<DisplayWindow>().SetFromText(text);
	}

	private void Start()
	{
		DataPlatformPlugin.InitializePlugin(0);
		eventManifest = Resources.LoadAsync("DataPlatformExample");
		OnScreenLog.Add("Data Platform Example Starting Up\n");
	}

	private void Update()
	{
		if (firstTime && eventManifest.isDone)
		{
			string text = ((TextAsset)eventManifest.asset).text;
			EventManager.CreateFromText(text);
			firstTime = false;
			DisplayText("Data Platform Example\r\n--------------------------------\r\n\r\nThis demo attempts to demonstrate some of the functionality of the Native Data Platform plugin.\r\n* This example only works with the example manifest.\r\n* This example only works in the XDKS.1 sandbox\r\n* DataPlatform is a harder library to provide an example for\r\n  as you must customize it to your game using the XDK_Source folder\r\n  and your own games Manifest.\r\n");
		}
	}

	private void OnGUI()
	{
	}

	public void HandleMenu(MenuLayout layout, Menu self)
	{
		layout.Update(1);
		if (layout.AddButtonWithIndex("Send Button Event"))
		{
			OnScreenLog.Add("> Sending Button Event\n");
		}
	}
}
