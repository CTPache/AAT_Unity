using System;
using UnityEngine;

[Serializable]
public class BgSaveData
{
	public ushort bg_no;

	public ushort bg_no_old;

	public byte bg_flag;

	public float bg_pos_x;

	public float bg_pos_y;

	public bool bg_black;

	public bool negaposi;

	public bool negaposi_sub;

	public Color color;

	public ushort bg_parts;

	public bool bg_parts_enabled;

	public bool reverse;

	public void init()
	{
		bg_no = 0;
		bg_no_old = 0;
		bg_flag = 0;
		bg_pos_x = 0f;
		bg_pos_y = 0f;
		bg_black = false;
		negaposi = false;
		negaposi_sub = false;
		color.r = 0f;
		color.g = 0f;
		color.b = 0f;
		color.a = 0f;
		bg_parts = 0;
		bg_parts_enabled = false;
		reverse = false;
	}
}
