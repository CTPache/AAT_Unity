using System;
using System.Collections.Generic;

[Serializable]
public class luminolBGTable
{
	public uint base_bg;

	public List<uint> bg_anime_ = new List<uint>();

	public luminolBGTable(uint in_base_bg_, uint bg_anim_1, uint bg_anim_2, uint bg_anim_3)
	{
		base_bg = in_base_bg_;
		bg_anime_.Clear();
		bg_anime_.Add(bg_anim_1);
		bg_anime_.Add(bg_anim_2);
		bg_anime_.Add(bg_anim_3);
	}
}
