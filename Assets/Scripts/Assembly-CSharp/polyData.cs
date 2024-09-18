public class polyData
{
	public string common_name = string.Empty;

	public string prefab_name = string.Empty;

	public string sub_prefab_name = string.Empty;

	public string hit_prefab_name = string.Empty;

	public int[] event_tbl;

	public string[] col_obj_names;

	public polyData(string in_common_name, string in_prefab_name, string in_sub_prefab_name, string in_hit_prefab_name, int[] in_event_tbl, string[] col_obj = null)
	{
		common_name = in_common_name;
		prefab_name = in_prefab_name;
		sub_prefab_name = in_sub_prefab_name;
		hit_prefab_name = in_hit_prefab_name;
		event_tbl = in_event_tbl;
		col_obj_names = col_obj;
	}
}
