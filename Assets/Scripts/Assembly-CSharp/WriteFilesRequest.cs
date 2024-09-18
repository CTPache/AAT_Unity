using System.IO;
using Sony.PS4.SaveData;

public class WriteFilesRequest : FileOps.FileOperationRequest
{
	public byte[] largeData = new byte[2097152];

	public override void DoFileOperations(Mounting.MountPoint mp, FileOps.FileOperationResponse response)
	{
		WriteFilesResponse writeFilesResponse = response as WriteFilesResponse;
		string text = mp.PathName.Data + "/Data.dat";
		using (FileStream fileStream = File.OpenWrite(text))
		{
			fileStream.Write(largeData, 0, largeData.Length);
		}
		FileInfo fileInfo = new FileInfo(text);
		writeFilesResponse.lastWriteTime = fileInfo.LastWriteTime;
		writeFilesResponse.totalFileSizeWritten += fileInfo.Length;
	}
}
