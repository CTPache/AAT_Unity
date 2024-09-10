using System;
using System.Runtime.InteropServices;

namespace SaveStruct
{
	[Serializable]
	public struct MessageData
	{
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 512)]
		public string msg_line01;

		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 512)]
		public string msg_line02;

		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 512)]
		public string msg_line03;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
		public int[] line_x;

		public ushort name_no;

		public bool name_visible;

		public bool window_visible;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
		public MessageKeyIconSaveData[] msg_icon;

		public static void New(out MessageData msg_data)
		{
			msg_data = default(MessageData);
			msg_data.line_x = new int[3];
		}

		public void CopyFrom(MessageSaveData src)
		{
			messageBoardCtrl.instance.SaveMsgSet();
			msg_line01 = src.msg_line1;
			msg_line02 = src.msg_line2;
			msg_line03 = src.msg_line3;
			Array.Copy(src.line_x, line_x, line_x.Length);
			name_no = src.name_no;
			name_visible = src.name_visible;
			window_visible = src.window_visible;
			msg_icon = new MessageKeyIconSaveData[3];
			for (int i = 0; i < 3; i++)
			{
				msg_icon[i] = default(MessageKeyIconSaveData);
				msg_icon[i].key_icon_visible = src.key_icon[i].key_icon_visible;
				msg_icon[i].key_icon_pos_x = src.key_icon[i].key_icon_pos_x;
				msg_icon[i].key_icon_pos_y = src.key_icon[i].key_icon_pos_y;
				msg_icon[i].key_icon_type = src.key_icon[i].key_icon_type;
			}
		}

		public void CopyTo(MessageSaveData dest)
		{
			dest.msg_line1 = msg_line01;
			dest.msg_line2 = msg_line02;
			dest.msg_line3 = msg_line03;
			if (line_x == null)
			{
				line_x = new int[3];
			}
			Array.Copy(line_x, dest.line_x, line_x.Length);
			dest.name_no = name_no;
			dest.name_visible = name_visible;
			dest.window_visible = window_visible;
			for (int i = 0; i < 3; i++)
			{
				dest.key_icon[i].key_icon_visible = msg_icon[i].key_icon_visible;
				dest.key_icon[i].key_icon_pos_x = msg_icon[i].key_icon_pos_x;
				dest.key_icon[i].key_icon_pos_y = msg_icon[i].key_icon_pos_y;
				dest.key_icon[i].key_icon_type = msg_icon[i].key_icon_type;
			}
		}
	}
}
