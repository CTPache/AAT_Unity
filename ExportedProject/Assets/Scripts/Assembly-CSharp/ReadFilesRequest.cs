using System.IO;
using Sony.PS4.SaveData;

public class ReadFilesRequest : FileOps.FileOperationRequest
{
	public override void DoFileOperations(Mounting.MountPoint mp, FileOps.FileOperationResponse response)
	{
		ReadFilesResponse readFilesResponse = response as ReadFilesResponse;
		string text = mp.PathName.Data + "/Data.dat";
		FileInfo fileInfo = new FileInfo(text);
		readFilesResponse.largeData = new byte[fileInfo.Length];
		using (FileStream fileStream = File.OpenRead(text))
		{
			fileStream.Read(readFilesResponse.largeData, 0, (int)fileInfo.Length);
		}
	}
}
