using System;
using System.Collections;
using Storage;

public class TitleStorageExample : IExample
{
	private TitleStorage mStorage;

	private bool amDone;

	private bool shouldSuspend;

	private bool readyToSuspend;

	private bool didPass = true;

	private const int kBufferSize = 100;

	public bool AmDone
	{
		get
		{
			return amDone;
		}
	}

	public TitleStorageExample(int userId)
	{
		if (userId < 0)
		{
			throw new Exception("You MUST have a valid UserId to use title storage at the moment. Please note this API is in flux as we work out the best way to support the full feature set in this library");
		}
		OnScreenLog.Add("[TitleStorage]\n");
		mStorage = TitleStorage.Create(userId);
	}

	public IEnumerator OnSuspend(StorageMainMenu.AmReadyToSuspend callback)
	{
		shouldSuspend = true;
		while (!readyToSuspend)
		{
			yield return null;
		}
		mStorage.Dispose();
		mStorage = null;
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
		OnScreenLog.Add("[TitleStorage] reinit after resume\n");
		mStorage = TitleStorage.Create(userId);
	}

	public void Start()
	{
		didPass = true;
		mStorage.RequestQuotaAsync(QuotaReturns);
	}

	private void QuotaReturns(TitleStorage storage, AsyncQuotaOp op, StorageQuota quota)
	{
		if (!SuspendCheck())
		{
			bool flag = op.Success && quota.SizeConsumed == 0 && quota.TotalSize / 1048576 == 256;
			didPass = didPass && flag;
			OnScreenLog.Add("  [" + ((!flag) ? "FAIL" : "PASS") + "] QUOTA: Total [" + quota.TotalSize / 1048576 + " MB ] Used [" + quota.SizeConsumed + "] Left [" + ((double)quota.SizeLeft / 1048576.0).ToString("N2") + " MB]\n");
			byte[] buffer = new byte[2];
			mStorage.DownloadFileAsync("DoesNotExist", buffer, NotExistsReturns);
		}
	}

	private void NotExistsReturns(TitleStorage storage, SaveLoadOp op, uint size)
	{
		if (!SuspendCheck())
		{
			bool flag = !op.Success;
			didPass = didPass && flag;
			OnScreenLog.Add("  [" + ((!flag) ? "FAIL" : "PASS") + "] NOTEXIST: [" + ((!op.Success) ? "Ok:FileDidNotExist" : "BAD:Exists?") + "]\n");
			byte[] array = new byte[100];
			for (int i = 0; i < 100; i++)
			{
				array[i] = (byte)i;
			}
			mStorage.UploadFileAsync("NewBuffer", array, UploadFileReturns);
		}
	}

	private void UploadFileReturns(TitleStorage storage, SaveLoadOp op, uint size)
	{
		if (!SuspendCheck())
		{
			bool success = op.Success;
			didPass = didPass && success;
			OnScreenLog.Add("  [" + ((!success) ? "FAIL" : "PASS") + "] UPLOAD");
			byte[] buffer = new byte[100];
			mStorage.DownloadFileAsync("NewBuffer", buffer, DownloadFileReturns);
		}
	}

	private void DownloadFileReturns(TitleStorage storage, SaveLoadOp op, uint size)
	{
		if (!SuspendCheck())
		{
			bool success = op.Success;
			didPass = didPass && success;
			OnScreenLog.Add("  [" + ((!success) ? "FAIL" : "PASS") + "] DOWNLOAD\n");
			mStorage.RequestQuotaAsync(QuotaReturns2);
		}
	}

	private void QuotaReturns2(TitleStorage storage, AsyncQuotaOp op, StorageQuota quota)
	{
		if (!SuspendCheck())
		{
			bool flag = op.Success && quota.SizeConsumed == 100;
			didPass = didPass && flag;
			OnScreenLog.Add("  [" + ((!flag) ? "FAIL" : "PASS") + "] NEWQUOTA: Total [" + quota.TotalSize / 1048576 + " MB ] Used [" + quota.SizeConsumed + "] Left [" + ((double)quota.SizeLeft / 1048576.0).ToString("N2") + " MB]\n");
			mStorage.DeleteFileAsync("NewBuffer", DeleteFileReturns);
		}
	}

	private void DeleteFileReturns(TitleStorage storage, DeleteFileOp op)
	{
		if (!SuspendCheck())
		{
			bool success = op.Success;
			didPass = didPass && success;
			OnScreenLog.Add("  [" + ((!success) ? "FAIL" : "PASS") + "] DELETE\n");
			mStorage.RequestQuotaAsync(QuotaReturns3);
		}
	}

	private void QuotaReturns3(TitleStorage storage, AsyncQuotaOp op, StorageQuota quota)
	{
		if (!SuspendCheck())
		{
			bool flag = op.Success && quota.SizeConsumed == 0;
			didPass = didPass && flag;
			OnScreenLog.Add("  [" + ((!flag) ? "FAIL" : "PASS") + "] QUOTA: Total [" + quota.TotalSize / 1048576 + " MB ] Used [" + quota.SizeConsumed + "] Left [" + ((double)quota.SizeLeft / 1048576.0).ToString("N2") + " MB]\n");
			amDone = true;
		}
	}

	public void Cleanup()
	{
		if (mStorage != null)
		{
			if (didPass)
			{
				OnScreenLog.Add("ALL OPERATIONS OK");
			}
			else
			{
				OnScreenLog.Add(">>> SOME OPERATIONS FAILED <<<");
			}
			OnScreenLog.Add(string.Empty);
			mStorage.Dispose();
			mStorage = null;
		}
	}
}
