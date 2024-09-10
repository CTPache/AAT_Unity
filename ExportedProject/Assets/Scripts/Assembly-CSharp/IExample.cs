using System.Collections;

public interface IExample
{
	bool AmDone { get; }

	void Start();

	void Cleanup();

	IEnumerator OnSuspend(StorageMainMenu.AmReadyToSuspend callback);

	void OnResume(int userId);
}
