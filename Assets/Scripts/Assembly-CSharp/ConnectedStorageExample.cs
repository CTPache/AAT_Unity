using System.Collections;
using Storage;

public class ConnectedStorageExample : IExample
{
	private ConnectedStorage mConnectedStorage;

	private ContainerContext mContainer;

	private DataMap mDataToSave;

	private int mUserId;

	private DataMap mReadAsyncMap;

	private bool amDone;

	private bool didPass = true;

	private bool shouldSuspend;

	private bool readyToSuspend;

	private ConnectedStorageCreationFlags mFlags;

	private const int kBufferSize = 1048576;

	public bool AmDone
	{
		get
		{
			return amDone;
		}
	}

	public ConnectedStorageExample(int userId = -1, ConnectedStorageCreationFlags flags = (ConnectedStorageCreationFlags)0)
	{
		mFlags = flags;
		mUserId = userId;
	}

	public IEnumerator OnSuspend(StorageMainMenu.AmReadyToSuspend callback)
	{
		shouldSuspend = true;
		while (!readyToSuspend)
		{
			yield return null;
		}
		mConnectedStorage.Dispose();
		mConnectedStorage = null;
		if (callback != null)
		{
			callback();
		}
	}

	private bool SuspendCheck()
	{
		if (shouldSuspend)
		{
			readyToSuspend = true;
			return true;
		}
		return false;
	}

	public void OnResume(int userId)
	{
		amDone = true;
		shouldSuspend = false;
		readyToSuspend = false;
		didPass = true;
		OnScreenLog.Add("[Connected Storage] resume\n");
	}

	public void Start()
	{
		didPass = true;
		ConnectedStorage.CreateAsync(mUserId, "MyContainer", OnConnectedStorageCreated, mFlags);
	}

	public void Cleanup()
	{
		if (mConnectedStorage != null)
		{
			mConnectedStorage.Dispose();
			mConnectedStorage = null;
		}
	}

	private void OnConnectedStorageCreated(ConnectedStorage storage, CreateConnectedStorageOp op)
	{
		if (SuspendCheck())
		{
			return;
		}
		if (op.Success)
		{
			mConnectedStorage = storage;
			if (mUserId == -1)
			{
				OnScreenLog.Add("[Machine storage created]\n");
			}
			else if (mFlags == (ConnectedStorageCreationFlags)0)
			{
				OnScreenLog.Add("[User ConnectedStorage created]\n");
			}
			else
			{
				OnScreenLog.Add("[On Demand User ConnectedStorage created]\n");
			}
			mConnectedStorage.GetAsync(new string[1] { "DoesNotExist" }, OnDoesNotExistReturns);
			OnScreenLog.Add("      . Creating new buffer\n");
			mDataToSave = DataMap.Create();
			byte[] array = new byte[1048576];
			for (int i = 0; i < 1048576; i++)
			{
				array[i] = (byte)i;
			}
			mDataToSave.AddOrReplaceBuffer("NewBuffer", array);
			mConnectedStorage.SubmitUpdatesAsync(mDataToSave, null, OnSubmitDone);
		}
		else
		{
			didPass = false;
			OnScreenLog.Add("Machine storage failed initialization RESULT: [0x" + op.Result.ToString("X") + "]\n");
		}
	}

	private void OnSubmitDone(ContainerContext storage, SubmitDataMapUpdatesAsyncOp op)
	{
		if (!SuspendCheck())
		{
			bool flag = op.Success && op.Status == ConnectedStorageStatus.SUCCESS;
			didPass = didPass && flag;
			OnScreenLog.Add("  [" + ((!flag) ? "FAIL" : "PASS") + "] SAVE: Completed RESULT: [" + op.Result + "] STATUS: [" + op.Status.ToString() + "]\n");
			if (op.Success)
			{
				storage.QueryBlobInfoAsync(string.Empty, QueryBlobInfoReturns);
				mReadAsyncMap = DataMap.Create();
				mReadAsyncMap.AddNewBuffer("NewBuffer", 1048576);
				mConnectedStorage.ReadAsync(mReadAsyncMap, ReadAsyncReturns);
			}
		}
	}

	private void OnDoesNotExistReturns(ContainerContext storage, GetDataMapViewAsyncOp op, DataMapView view)
	{
		if (!SuspendCheck())
		{
			bool flag = !op.Success;
			didPass = didPass && flag;
			OnScreenLog.Add("  [" + ((!flag) ? "FAIL" : "PASS") + "] BADREAD: [" + ((!op.Success) ? "NonExistent" : "Exists") + "] [0x" + op.Result.ToString("X") + "]\n");
		}
	}

	private void ReadAsyncReturns(ContainerContext storage, ReadDataMapAsyncOp op)
	{
		if (!SuspendCheck())
		{
			bool success = op.Success;
			didPass = didPass && success;
			OnScreenLog.Add("  [" + ((!success) ? "FAIL" : "PASS") + "] READASYNC: \"NewBuffer\": [" + ((!op.Success) ? "NonExistent" : "Exists") + "] [0x" + op.Result.ToString("X") + "]\n");
			if (op.Success)
			{
				byte[] buffer = op.Map.GetBuffer("NewBuffer");
				OnScreenLog.Add("        . BUFFERINFO: LENGTH: [" + buffer.Length + "] 0=" + buffer[0] + " 99=" + buffer[99] + "\n");
			}
			mConnectedStorage.GetAsync(new string[1] { "NewBuffer" }, GetAsyncReturns);
		}
	}

	private void GetAsyncReturns(ContainerContext storage, GetDataMapViewAsyncOp op, DataMapView view)
	{
		if (!SuspendCheck())
		{
			bool success = op.Success;
			didPass = didPass && success;
			OnScreenLog.Add("  [" + ((!success) ? "FAIL" : "PASS") + "] GETASYNC: \"NewBuffer\": [" + ((!op.Success) ? "NonExistent" : "Exists") + "] [0x" + op.Result.ToString("X") + "]\n");
			if (op.Success)
			{
				byte[] buffer = view.GetBuffer("NewBuffer");
				OnScreenLog.Add("        . BUFFERINFO: LENGTH: [" + buffer.Length + "] 0=" + buffer[0] + " 99=" + buffer[99] + "\n");
			}
			mConnectedStorage.QueryRemainingQuotaAsync(QueryRemainingQuotaExistsReturns);
		}
	}

	private void QueryRemainingQuotaExistsReturns(ConnectedStorage storage, QueryRemainingQuotaAsyncOp op, StorageQuota quota)
	{
		if (!SuspendCheck())
		{
			bool flag = op.Success && (double)quota.SizeLeft <= (double)quota.TotalSize - 1.0 && (double)quota.SizeConsumed >= 1.0;
			didPass = didPass && flag;
			OnScreenLog.Add("  [" + ((!flag) ? "FAIL" : "PASS") + "] QUOTA EXISTS: [" + (quota.SizeLeft / 1048576).ToString("F2") + "] of [" + (quota.TotalSize / 1048576).ToString("F2") + "] \n");
			mConnectedStorage.DeleteAsync(new string[1] { "NewBuffer" }, DeleteAsyncReturns);
		}
	}

	private void DeleteAsyncReturns(ContainerContext storage, SubmitDataMapUpdatesAsyncOp op)
	{
		if (!SuspendCheck())
		{
			bool success = op.Success;
			didPass = didPass && success;
			OnScreenLog.Add("  [" + ((!success) ? "FAIL" : "PASS") + "] DELETE: \"NewBuffer\"\n");
			mConnectedStorage.GetAsync(new string[1] { "NewBuffer" }, GetAsyncFailedReturns);
		}
	}

	private void GetAsyncFailedReturns(ContainerContext storage, GetDataMapViewAsyncOp op, DataMapView view)
	{
		if (!SuspendCheck())
		{
			bool flag = !op.Success;
			didPass = didPass && flag;
			OnScreenLog.Add("  [" + ((!flag) ? "FAIL" : "PASS") + "] GETASYNC: \"NewBuffer\": [" + ((!op.Success) ? "NonExistent" : "Exists") + "] [0x" + op.Result.ToString("X") + "]\n");
			mConnectedStorage.QueryRemainingQuotaAsync(QueryRemainingQuotaReturns);
		}
	}

	private void QueryRemainingQuotaReturns(ConnectedStorage storage, QueryRemainingQuotaAsyncOp op, StorageQuota quota)
	{
		if (!SuspendCheck())
		{
			bool flag = op.Success && quota.SizeLeft == quota.TotalSize;
			didPass = didPass && flag;
			OnScreenLog.Add("  [" + ((!flag) ? "FAIL" : "PASS") + "] QUOTA: [" + (quota.SizeLeft / 1048576).ToString("F2") + "] of [" + (quota.TotalSize / 1048576).ToString("F2") + "] \n");
			mConnectedStorage.QueryContainerInfoAsync(string.Empty, QueryContainerInfoAsyncReturns);
		}
	}

	private void QueryContainerInfoAsyncReturns(ConnectedStorage storage, ContainerInfoQueryAsyncOp op)
	{
		if (SuspendCheck())
		{
			return;
		}
		bool success = op.Success;
		didPass = didPass && success;
		OnScreenLog.Add("  [" + ((!success) ? "FAIL" : "PASS") + "] CONTAINER INFO\n");
		if (op.Success)
		{
			for (uint num = 0u; num < op.Query.Length; num++)
			{
				ContainerInfo containerInfo = op.Query[num];
				OnScreenLog.Add("        . " + num.ToString() + ". " + containerInfo.Name + " [" + containerInfo.TotalSize + " @" + containerInfo.LastModifiedTime.ToString() + "] NeedSync: " + ((!containerInfo.NeedsSync) ? "No" : "Yes") + "\n");
			}
			op.Query.Dispose();
		}
		ContainerContext containerContext = mConnectedStorage.OpenOrCreateContainer("SecondaryContainer");
		OnScreenLog.Add("        . Creating new buffer\n");
		mDataToSave = DataMap.Create();
		byte[] array = new byte[1048576];
		for (int i = 0; i < 1048576; i++)
		{
			array[i] = (byte)i;
		}
		mDataToSave.AddOrReplaceBuffer("NewBuffer2", array);
		containerContext.SubmitUpdatesAsync(mDataToSave, null, OnContainerSubmitDone);
	}

	private void QueryBlobInfoReturns(ContainerContext storage, BlobInfoQueryAsyncOp op)
	{
		if (SuspendCheck())
		{
			return;
		}
		bool success = op.Success;
		didPass = didPass && success;
		OnScreenLog.Add("  [" + ((!success) ? "FAIL" : "PASS") + "] BLOB INFO\n");
		if (op.Success)
		{
			for (uint num = 0u; num < op.Query.Length; num++)
			{
				BlobInfo blobInfo = op.Query[num];
				OnScreenLog.Add("Found Blob " + blobInfo.Name + " [" + blobInfo.TotalSize + "]\n");
			}
			op.Query.Dispose();
		}
	}

	private void OnContainerSubmitDone(ContainerContext storage, SubmitDataMapUpdatesAsyncOp op)
	{
		if (!SuspendCheck())
		{
			bool flag = op.Success && op.Status == ConnectedStorageStatus.SUCCESS;
			didPass = didPass && flag;
			OnScreenLog.Add("  [" + ((!flag) ? "FAIL" : "PASS") + "] SAVE: 2nd Container Completed RESULT: [" + op.Result + "] STATUS: [" + op.Status.ToString() + "]\n");
			if (op.Success)
			{
				mConnectedStorage.DeleteThisContainer(DeleteFirstContainerReturns);
			}
		}
	}

	private void DeleteFirstContainerReturns(ConnectedStorage storage, DeleteContainerAsyncOp op)
	{
		if (!SuspendCheck())
		{
			bool success = op.Success;
			didPass = didPass && success;
			OnScreenLog.Add("  [" + ((!success) ? "FAIL" : "PASS") + "] CONTAINER: [" + ((!op.Success) ? "DeleteFailed" : "Deleted") + "] Primary\n");
			mConnectedStorage.DeleteContainer("SecondaryContainer", DeleteSecondaryContainerReturns);
		}
	}

	private void DeleteSecondaryContainerReturns(ConnectedStorage storage, DeleteContainerAsyncOp op)
	{
		if (!SuspendCheck())
		{
			bool success = op.Success;
			didPass = didPass && success;
			OnScreenLog.Add("  [" + ((!success) ? "FAIL" : "PASS") + "] CONTAINER: [" + ((!op.Success) ? "DeleteFailed" : "Deleted") + "] Secondary\n");
			if (didPass)
			{
				OnScreenLog.Add("ALL OPERATIONS OK");
			}
			else
			{
				OnScreenLog.Add(">>> SOME OPERATIONS FAILED <<<");
			}
			OnScreenLog.Add(string.Empty);
			amDone = true;
		}
	}
}
