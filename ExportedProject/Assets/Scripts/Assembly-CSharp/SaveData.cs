using System;

[Serializable]
public struct SaveData
{
	public string time;

	public ushort title;

	public ushort scenario;

	public ushort progress;

	public ushort in_data;

	public void init()
	{
		time = string.Empty;
		title = 0;
		scenario = 0;
		progress = 0;
		in_data = 0;
	}
}
