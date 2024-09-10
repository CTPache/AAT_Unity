using System;

[Serializable]
public class MessageKeyIconSaveData
{
	public bool key_icon_visible;

	public float key_icon_pos_x;

	public float key_icon_pos_y;

	public ushort key_icon_type;

	public void init()
	{
		key_icon_visible = false;
		key_icon_pos_x = 0f;
		key_icon_pos_y = 0f;
		key_icon_type = 0;
	}
}
