public class piceDetailData
{
	public uint bg_id;

	public uint page_num;

	public uint scroll;

	public string name = string.Empty;

	public piceDetailData(uint in_bg_id, uint in_page_num, uint in_scroll, string in_name)
	{
		bg_id = in_bg_id;
		page_num = in_page_num;
		scroll = in_scroll;
		name = in_name;
	}
}
