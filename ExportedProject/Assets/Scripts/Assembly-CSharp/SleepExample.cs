using System;
using System.Collections;

public class SleepExample : IExample
{
	private DateTime mStart;

	public bool AmDone
	{
		get
		{
			return (DateTime.Now - mStart).Seconds > 2;
		}
	}

	public void Start()
	{
		mStart = DateTime.Now;
	}

	public IEnumerator OnSuspend(StorageMainMenu.AmReadyToSuspend callback)
	{
		if (callback != null)
		{
			callback();
		}
		yield break;
	}

	public void OnResume(int userId)
	{
	}

	public void Cleanup()
	{
	}
}
