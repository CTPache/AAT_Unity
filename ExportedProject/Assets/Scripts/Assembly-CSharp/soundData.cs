public class soundData
{
	public string name;

	public string path;

	public ushort no;

	public bool loop;

	public soundData(string in_name, string in_path, ushort in_no, bool in_loop)
	{
		name = in_name;
		path = in_path;
		no = in_no;
		loop = in_loop;
	}
}
