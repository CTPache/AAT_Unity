using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
//using Steamworks;
using UnityEngine;

[DisallowMultipleComponent]
public class SteamStorageScript : SteamScriptBase
{
    public delegate void OnSaveRequestRecived(bool success);

    public delegate void OnLoadRequestRecived(byte[] data);

    private static SteamStorageScript s_instance_;

    private bool saving_;

    private OnSaveRequestRecived on_complete_save_request_;

    private Dictionary<string, OnLoadRequestRecived> request_callback_ = new Dictionary<string, OnLoadRequestRecived>();

    /*
    private Dictionary<string, SteamAPICall_t> request_load_cache_ = new Dictionary<string, SteamAPICall_t>();

	private CallResult<RemoteStorageFileWriteAsyncComplete_t> OnRemoteStorageFileWriteAsyncCompleteCallResult;

	private CallResult<RemoteStorageFileReadAsyncComplete_t> OnRemoteStorageFileReadAsyncCompleteCallResult;

	private CallResult<RemoteStorageFileShareResult_t> OnRemoteStorageFileShareResultCallResult;

	private CallResult<RemoteStorageDownloadUGCResult_t> OnRemoteStorageDownloadUGCResultCallResult;
	*/
    public static SteamStorageScript Instance
    {
        get
        {
            if (s_instance_ == null)
            {
                s_instance_ = SteamScriptBase.AddSteamScriptComponent<SteamStorageScript>();
            }
            return s_instance_;
        }
    }

    protected override Rect gui_layout_area
    {
        get
        {
            return new Rect(Screen.width - 400, 0f, 200f, Screen.height);
        }
    }

    protected override void AwakeSteam()
    {
        if (s_instance_ == null)
        {
            s_instance_ = this;
        }
        /*
		OnRemoteStorageFileWriteAsyncCompleteCallResult = CallResult<RemoteStorageFileWriteAsyncComplete_t>.Create(OnRemoteStorageFileWriteAsyncComplete);
		OnRemoteStorageFileReadAsyncCompleteCallResult = CallResult<RemoteStorageFileReadAsyncComplete_t>.Create(OnRemoteStorageFileReadAsyncComplete);
		OnRemoteStorageFileShareResultCallResult = CallResult<RemoteStorageFileShareResult_t>.Create(OnFileShareResult);
		OnRemoteStorageDownloadUGCResultCallResult = CallResult<RemoteStorageDownloadUGCResult_t>.Create(OnDownloadUGCResult);
		*/
    }

    protected override void StartSteam()
    {
    }

    protected override void OnActive()
    {
        base.OnActive();
        if (s_instance_ == null)
        {
            s_instance_ = this;
        }
    }

    protected override void OnDeactive()
    {
        base.OnDeactive();
        if (!(s_instance_ != this))
        {
            s_instance_ = null;
        }
    }

    public override void Init()
    {
    }

    public bool RequestShareFile(string filename, OnLoadRequestRecived callback)
    {
        /*
        if (!request_load_cache_.ContainsKey(filename) && SteamRemoteStorage.FileExists(filename))
        {
            request_load_cache_.Add(filename, (SteamAPICall_t)0uL);
            request_callback_.Add(filename, callback);
            ShareFile(filename);
            return true;
        }
        return false;
        */
        if (File.Exists(filename))
        {
            request_callback_.Add(filename, callback);
            return true;
        }
        return false;
    }

    public bool SaveSteamData(string filename, byte[] savedata)
    {
        //bool flag = SteamRemoteStorage.FileWrite(filename, savedata, savedata.Length);
        bool flag = true;
        try
        {
            File.WriteAllBytes(filename, savedata);
        }
        catch
        {
            flag = false;
        }
        Debug.Log("SaveSteam(" + filename + ", Data, " + savedata.Length + ") - " + flag);
        return flag;
    }

    public void SaveRequest(string filename, byte[] savedata, OnSaveRequestRecived callback)
    {

        /*if (!saving_)//&& !request_load_cache_.ContainsKey(filename))
    {
        /*
        SteamAPICall_t steamAPICall_t = SteamRemoteStorage.FileWriteAsync(filename, savedata, (uint)savedata.Length);
        if (steamAPICall_t != (SteamAPICall_t)0uL)
        {
            OnRemoteStorageFileWriteAsyncCompleteCallResult.Set(steamAPICall_t);
            on_complete_save_request_ = callback;
            saving_ = true;
        }
        else
        {
            callback(false);
        }
        */
        try
        {
            File.WriteAllBytes(filename, savedata);
            callback(true);
            saving_ = true;
        }
        catch (Exception e)
        {
            Debug.Log(e);
            callback(false);
        }

    }

    public byte[] LoadSteamData(string filename)
    {
        byte[] array = null;
        /*if (SteamRemoteStorage.FileExists(filename))
            {
            array = new byte[SteamRemoteStorage.GetFileSize(filename)];
            int num = SteamRemoteStorage.FileRead(filename, array, array.Length);
            Debug.Log("FileRead(" + filename + ", Data, " + array.Length + ") - " + num);
            if (num == 0)
            {
                array = null;
            }
        */
        if (File.Exists(filename))
        {
            array = File.ReadAllBytes(filename);
            Debug.Log("FileRead(" + filename + ", Data, " + array.Length + ") - " + array.Length);
            if (array.Length == 0)
            {
                array = null;
            }
        }
        return array;
    }

    public void LoadRequest(string filename, OnLoadRequestRecived callback)
    {
        /*
        if (!request_load_cache_.ContainsKey(filename))
        {
        */
        if (IsExistLocalStorage(filename))
        {

            //SteamAPICall_t steamAPICall_t = SteamRemoteStorage.FileReadAsync(filename, 0u, fileSize);
            //if (steamAPICall_t != (SteamAPICall_t)0uL)

            Debug.Log("Save found.");
            try
            {
                callback(File.ReadAllBytes(filename));
            }
            catch
            {
                callback(null);
            }
        }
        else
        {
            Debug.Log("Save not found.");
            callback(null);
        }
        /*
        }
        else
        {
            callback(null);
        }
        */
    }

    public bool IsExistLocalStorage(string filename)
    {
        /*
        byte[] array = new byte[SteamRemoteStorage.GetFileSize(filename)];
        int num = SteamRemoteStorage.FileRead(filename, array, array.Length);
        return num != 0;
        */

        return File.Exists(filename);
    }

    public DateTime GetSteamDataTimestamp(string filename)
    {
        //DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0);
        //long fileTimestamp = SteamRemoteStorage.GetFileTimestamp(filename);
        //return dateTime.AddSeconds(fileTimestamp).ToLocalTime();
        return File.GetLastAccessTime(filename);
    }

    /*
    private void ShareFile(string filename)
    {
        OnRemoteStorageFileShareResultCallResult.Set(SteamRemoteStorage.FileShare(filename));
    }

    private void OnRemoteStorageFileWriteAsyncComplete(RemoteStorageFileWriteAsyncComplete_t pCallback, bool bIOFailure)
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.AppendLine("[RemoteStorageFileWriteAsyncComplete]");
        stringBuilder.AppendLine("m_eResult : " + pCallback.m_eResult);
        Debug.Log(stringBuilder);
        if (on_complete_save_request_ != null)
        {
            on_complete_save_request_(pCallback.m_eResult == EResult.k_EResultOK);
            on_complete_save_request_ = null;
        }
        saving_ = false;
    }

    private void OnRemoteStorageFileReadAsyncComplete(RemoteStorageFileReadAsyncComplete_t pCallback, bool bIOFailure)
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.AppendLine("[RemoteStorageFileReadAsyncComplete]");
        stringBuilder.AppendLine("m_hFileReadAsync : " + pCallback.m_hFileReadAsync);
        stringBuilder.AppendLine("m_eResult : " + pCallback.m_eResult);
        stringBuilder.AppendLine("m_nOffset : " + pCallback.m_nOffset);
        stringBuilder.AppendLine("m_cubRead : " + pCallback.m_cubRead);
        Debug.Log(stringBuilder);
        string key = string.Empty;
        foreach (KeyValuePair<string, SteamAPICall_t> item in request_load_cache_)
        {
            if (item.Value == pCallback.m_hFileReadAsync)
            {
                key = item.Key;
                break;
            }
        }
        OnLoadRequestRecived onLoadRequestRecived = request_callback_[key];
        request_load_cache_.Remove(key);
        request_callback_.Remove(key);
        byte[] array = null;
        if (pCallback.m_eResult == EResult.k_EResultOK)
        {
            array = new byte[pCallback.m_cubRead];
            bool flag = SteamRemoteStorage.FileReadAsyncComplete(pCallback.m_hFileReadAsync, array, pCallback.m_cubRead);
            Debug.Log("FileReadAsyncComplete(m_FileReadAsyncHandle, data, pCallback.m_cubRead) : " + flag);
        }
        if (onLoadRequestRecived != null)
        {
            onLoadRequestRecived(array);
        }
    }

    private void OnFileShareResult(RemoteStorageFileShareResult_t pCallback, bool bIOFailure)
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.AppendLine("[RemoteStorageFileShareResult]");
        stringBuilder.AppendLine("m_eResult : " + pCallback.m_eResult);
        stringBuilder.AppendLine("m_hFile : " + pCallback.m_hFile);
        stringBuilder.AppendLine("m_rgchFilename : " + pCallback.m_rgchFilename);
        Debug.Log(stringBuilder);
        if (pCallback.m_eResult == EResult.k_EResultOK)
        {
            OnRemoteStorageDownloadUGCResultCallResult.Set(SteamRemoteStorage.UGCDownload(pCallback.m_hFile, 0u));
            return;
        }
        request_callback_[pCallback.m_rgchFilename](null);
        request_callback_.Remove(pCallback.m_rgchFilename);
        request_load_cache_.Remove(pCallback.m_rgchFilename);
        SteamCtrl.IsShareFilesError = true;
    }

    private void OnDownloadUGCResult(RemoteStorageDownloadUGCResult_t pCallback, bool bIOFailure)
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.AppendLine("[RemoteStorageDownloadUGCResult]");
        stringBuilder.AppendLine("m_eResult : " + pCallback.m_eResult);
        stringBuilder.AppendLine("m_nAppID : " + pCallback.m_nAppID);
        stringBuilder.AppendLine("m_hFile : " + pCallback.m_hFile);
        stringBuilder.AppendLine("m_pchFileName : " + pCallback.m_pchFileName);
        stringBuilder.AppendLine("m_nSizeInBytes : " + pCallback.m_nSizeInBytes);
        stringBuilder.AppendLine("m_ulSteamIDOwner : " + pCallback.m_ulSteamIDOwner);
        Debug.Log(stringBuilder);
        request_load_cache_.Remove(pCallback.m_pchFileName);
        byte[] savedata = new byte[pCallback.m_nSizeInBytes];
        int num = SteamRemoteStorage.UGCRead(pCallback.m_hFile, savedata, savedata.Length, 0u, EUGCReadAction.k_EUGCRead_Close);
        if (num == pCallback.m_nSizeInBytes)
        {
            SteamRemoteStorage.FileForget(pCallback.m_pchFileName);
            SaveRequest(pCallback.m_pchFileName, savedata, delegate (bool success)
            {
                if (success)
                {
                    request_callback_[pCallback.m_pchFileName](savedata);
                }
                else
                {
                    request_callback_[pCallback.m_pchFileName](null);
                    SteamCtrl.IsShareFilesError = true;
                }
                request_callback_.Remove(pCallback.m_pchFileName);
            });
        }
        else
        {
            request_callback_[pCallback.m_pchFileName](null);
            request_callback_.Remove(pCallback.m_pchFileName);
            SteamCtrl.IsShareFilesError = true;
        }
    }
    */
}
