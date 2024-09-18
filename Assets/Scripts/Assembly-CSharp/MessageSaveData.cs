using System;

[Serializable]
public class MessageSaveData
{
	public string msg_line1;

	public string msg_line2;

	public string msg_line3;

	public int[] line_x = new int[3];

	public ushort name_no;

	public bool name_visible;

	public bool window_visible;

	public MessageKeyIconSaveData[] key_icon = new MessageKeyIconSaveData[3]
	{
		new MessageKeyIconSaveData(),
		new MessageKeyIconSaveData(),
		new MessageKeyIconSaveData()
	};

	public void init()
	{
		msg_line1 = string.Empty;
		msg_line2 = string.Empty;
		msg_line3 = string.Empty;
		line_x[0] = 0;
		line_x[1] = 0;
		line_x[2] = 0;
		name_no = 0;
		name_visible = false;
		window_visible = false;
		key_icon[0].init();
		key_icon[1].init();
		key_icon[2].init();
	}
}
