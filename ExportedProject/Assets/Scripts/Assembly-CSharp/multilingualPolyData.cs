using System.Collections.Generic;

public class multilingualPolyData
{
	public string jp_name = string.Empty;

	public string us_name = string.Empty;

	public string fr_name = string.Empty;

	public string ge_name = string.Empty;

	public string ko_name = string.Empty;

	public string ch_s_name = string.Empty;

	public string ch_t_name = string.Empty;

	public multilingualPolyData(string in_jp_name, string in_us_name, string in_fr_name, string in_ge_name, string in_ko_name, string in_ch_s_name, string in_ch_t_name)
	{
		jp_name = in_jp_name;
		us_name = in_us_name;
		fr_name = in_fr_name;
		ge_name = in_ge_name;
		ko_name = in_ko_name;
		ch_s_name = in_ch_s_name;
		ch_t_name = in_ch_t_name;
	}

	public List<string> GetNameList()
	{
		List<string> list = new List<string>();
		list.Add(jp_name);
		list.Add(us_name);
		list.Add(fr_name);
		list.Add(ge_name);
		list.Add(ko_name);
		list.Add(ch_s_name);
		list.Add(ch_t_name);
		return list;
	}
}
