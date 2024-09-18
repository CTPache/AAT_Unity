using System;
using UnityEngine;

[Serializable]
public class luminolTable
{
	public uint flag;

	public uint x;

	public uint y;

	public uint w;

	public uint h;

	public uint curW;

	public uint curH;

	public uint min_cnt;

	public uint sce_flag;

	public uint message_id;

	public float pos_x;

	public float pos_y;

	public string sprite_name;

	public luminolBGTable bg;

	public luminolTable(uint in_flag, uint in_x, uint in_y, uint in_w, uint in_h, uint in_curW, uint in_curH, uint in_min_cnt, uint in_sce_flag, uint in_message_id, float in_pos_x, float in_pos_y, luminolBGTable in_bg, string in_sprite_name)
	{
		bg = in_bg;
		flag = in_flag;
		x = in_x;
		y = in_y;
		w = in_w;
		h = in_h;
		pos_x = (int)(ushort)Mathf.FloorToInt(luminolMiniGame.offset.x + (float)in_x * luminolMiniGame.scale.x + (float)in_w * luminolMiniGame.scale.x / 2f);
		pos_y = -(ushort)Mathf.FloorToInt(luminolMiniGame.offset.y + (float)in_y * luminolMiniGame.scale.y + (float)in_h * luminolMiniGame.scale.y / 2f);
		curW = in_curW;
		curH = in_curH;
		min_cnt = in_min_cnt;
		sce_flag = in_sce_flag;
		message_id = in_message_id;
		sprite_name = in_sprite_name;
	}
}
