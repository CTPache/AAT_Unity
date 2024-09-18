using System;
using System.Runtime.InteropServices;

namespace SaveStruct
{
	[Serializable]
	public struct MessageWork
	{
		public int status;

		public int status2;

		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
		public string mdt_path;

		public ushort mdt_datas_index;

		public uint mdt_index;

		public uint mdt_index_top;

		public ushort code;

		public ushort code_no;

		public ushort work;

		public byte message_trans_flag;

		public byte message_se_character_count;

		public byte message_se;

		public ushort now_no;

		public ushort next_no;

		public int message_color;

		public ushort message_line;

		public byte message_time;

		public byte message_timer;

		public ushort tukkomi_no;

		public byte tukkomi_flag;

		public byte text_flag;

		public byte rt_wait_timer;

		public byte desk_attack;

		public byte speaker_id;

		public byte mess_win_rno;

		public byte cursor;

		public byte op_no;

		public byte op_workno;

		public byte op_flg;

		public byte op_para;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
		public ushort[] op_work;

		public ushort sc_no;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
		public ushort[] all_work;

		public short Item_open_type;

		public byte Item_id;

		public short Item_face_open_type;

		public byte Item_face_id;

		public byte enable_message_trophy;

		public static void New(out MessageWork message_work)
		{
			message_work = default(MessageWork);
			message_work.op_work = new ushort[8];
			message_work.all_work = new ushort[3];
		}

		public void CopyFrom(global::MessageWork src)
		{
			status = (int)src.status;
			status2 = (int)src.status2;
			mdt_path = src.mdt_path;
			mdt_datas_index = src.mdt_datas_index_;
			mdt_index = src.mdt_index;
			mdt_index_top = src.mdt_index_top;
			code = src.code;
			code_no = src.code_no;
			work = src.work;
			message_trans_flag = src.message_trans_flag;
			message_se_character_count = src.message_se_character_count;
			message_se = src.message_se;
			now_no = src.now_no;
			next_no = src.next_no;
			message_color = src.message_text.color;
			message_line = src.message_line;
			message_time = src.message_time;
			message_timer = src.message_timer;
			tukkomi_no = src.tukkomi_no;
			tukkomi_flag = src.tukkomi_flag;
			text_flag = (byte)src.text_flag;
			rt_wait_timer = src.rt_wait_timer;
			desk_attack = src.desk_attack;
			speaker_id = src.speaker_id;
			mess_win_rno = src.mess_win_rno;
			cursor = src.cursor;
			op_no = src.op_no;
			op_workno = src.op_workno;
			op_flg = src.op_flg;
			op_para = src.op_para;
			Array.Copy(src.op_work, op_work, op_work.Length);
			sc_no = src.sc_no;
			Array.Copy(src.all_work, all_work, all_work.Length);
			Item_open_type = src.Item_open_type;
			Item_id = src.Item_id;
			Item_face_open_type = src.Item_face_open_type;
			Item_face_id = src.Item_face_id;
			enable_message_trophy = (byte)(src.enable_message_trophy ? 1u : 0u);
		}

		public void CopyTo(global::MessageWork dest)
		{
			dest.status = (MessageSystem.Status)status;
			dest.status2 = (MessageSystem.Status2)status2;
			dest.mdt_path = mdt_path;
			dest.mdt_datas_index_ = mdt_datas_index;
			dest.mdt_index = mdt_index;
			dest.mdt_index_top = mdt_index_top;
			dest.code = code;
			dest.code_no = code_no;
			dest.work = work;
			dest.message_trans_flag = message_trans_flag;
			dest.message_se_character_count = message_se_character_count;
			dest.message_se = message_se;
			dest.now_no = now_no;
			dest.next_no = next_no;
			dest.message_text.SetColor(message_color);
			dest.message_line = message_line;
			dest.message_time = message_time;
			dest.message_timer = message_timer;
			dest.tukkomi_no = tukkomi_no;
			dest.tukkomi_flag = tukkomi_flag;
			dest.text_flag = (MessageSystem.TextFlag)text_flag;
			dest.rt_wait_timer = rt_wait_timer;
			dest.desk_attack = desk_attack;
			dest.speaker_id = speaker_id;
			dest.mess_win_rno = mess_win_rno;
			dest.cursor = cursor;
			dest.op_no = op_no;
			dest.op_workno = op_workno;
			dest.op_flg = op_flg;
			dest.op_para = op_para;
			if (op_work == null)
			{
				op_work = new ushort[8];
			}
			Array.Copy(op_work, dest.op_work, op_work.Length);
			dest.sc_no = sc_no;
			if (all_work == null)
			{
				all_work = new ushort[3];
			}
			Array.Copy(all_work, dest.all_work, all_work.Length);
			dest.Item_open_type = Item_open_type;
			dest.Item_id = Item_id;
			dest.Item_face_open_type = Item_face_open_type;
			dest.Item_face_id = Item_face_id;
			dest.enable_message_trophy = enable_message_trophy != 0;
		}
	}
}
