using System;
using Sony.PS4.SaveData;

public class WriteFilesResponse : FileOps.FileOperationResponse
{
	public DateTime lastWriteTime;

	public long totalFileSizeWritten;
}
