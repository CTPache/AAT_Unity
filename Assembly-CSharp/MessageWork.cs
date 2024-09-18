using System;
using UnityEngine;

[Serializable]
public class MessageWork
{
	[SerializeField]
	private MessageSystem.Status status_;

	public MessageSystem.Status2 status2;

	public string mdt_path;

	public ushort mdt_datas_index_;

	public uint mdt_index_;

	public uint mdt_index_top_;

	public ushort code;

	public ushort code_no;

	public ushort work;

	public WindowType message_type;

	[SerializeField]
	private byte message_trans_flag_;

	public byte message_se_character_count;

	public byte message_se;

	public ushort now_no;

	public ushort next_no;

	public ushort ev_temp;

	public ushort[] now_no_bak = new ushort[5];

	public uint[] mdt_index_bak = new uint[5];

	public MessageText message_text = new MessageText();

	public ushort message_line;

	public byte message_time;

	public byte message_timer;

	public ushort tukkomi_no;

	public byte tukkomi_flag;

	public MessageSystem.TextFlag text_flag;

	public byte rt_wait_timer;

	public byte desk_attack;

	public byte speaker_id;

	[SerializeField]
	private byte mess_win_rno_;

	public byte cursor;

	public byte op_no;

	public byte op_workno;

	public byte op_flg;

	public byte op_para;

	public ushort[] op_work = new ushort[8];

	public ushort sc_no;

	public ushort[] all_work = new ushort[3];

	public ushort choustateBK;

	public ushort choustate;

	public byte chou_no;

	public byte chou_st;

	public ushort chou_cnt;

	public ChouWork[] chou = new ChouWork[3]
	{
		new ChouWork(),
		new ChouWork(),
		new ChouWork()
	};

	public MessageSystem.SoundFlag sound_flag;

	public short Item_open_type;

	public byte Item_id;

	public byte Item_rno_0;

	public short Item_face_open_type;

	public byte Item_face_id;

	public short Item_zoom;

	public bool game_over;

	public bool enable_message_trophy;

	public int questioning_message_wait;

	public MessageSystem.Status status
	{
		get
		{
			return status_;
		}
		set
		{
			status_ = value;
		}
	}

	public byte message_trans_flag
	{
		get
		{
			return message_trans_flag_;
		}
		set
		{
			message_trans_flag_ = value;
		}
	}

	public byte mess_win_rno
	{
		get
		{
			return mess_win_rno_;
		}
		set
		{
			mess_win_rno_ = value;
		}
	}

	public MdtData mdt_data
	{
		get
		{
			return GSStatic.mdt_datas_[mdt_datas_index_];
		}
	}

	public uint mdt_index
	{
		get
		{
			return mdt_index_;
		}
		set
		{
			mdt_index_ = value;
		}
	}

	public uint mdt_index_top
	{
		get
		{
			return mdt_index_top_;
		}
		set
		{
			mdt_index_top_ = value;
		}
	}

	public void init()
	{
		status2 = (MessageSystem.Status2)0;
		mdt_path = string.Empty;
		mdt_datas_index_ = 0;
		mdt_index_ = 0u;
		mdt_index_top_ = 0u;
		code = 0;
		code_no = 0;
		work = 0;
		message_type = WindowType.MAIN;
		message_trans_flag = 0;
		message_se_character_count = 0;
		message_se = 0;
		now_no = 0;
		next_no = 0;
		ev_temp = 0;
		message_line = 0;
		message_time = 0;
		message_timer = 0;
		tukkomi_no = 0;
		tukkomi_flag = 0;
		text_flag = (MessageSystem.TextFlag)0;
		rt_wait_timer = 0;
		desk_attack = 0;
		speaker_id = 0;
		mess_win_rno = 0;
		cursor = 0;
		op_no = 0;
		op_workno = 0;
		op_flg = 0;
		op_para = 0;
		sc_no = 0;
		choustateBK = 0;
		choustate = 0;
		chou_no = 0;
		chou_st = 0;
		chou_cnt = 0;
		sound_flag = (MessageSystem.SoundFlag)0;
		Item_open_type = -1;
		Item_id = 0;
		Item_rno_0 = 0;
		Item_face_open_type = -1;
		Item_face_id = 0;
		Item_zoom = 0;
		game_over = false;
		for (int i = 0; i < now_no_bak.Length; i++)
		{
			now_no_bak[i] = 0;
		}
		for (int j = 0; j < mdt_index_bak.Length; j++)
		{
			mdt_index_bak[j] = 0u;
		}
		for (int k = 0; k < op_work.Length; k++)
		{
			op_work[k] = 0;
		}
		for (int l = 0; l < all_work.Length; l++)
		{
			all_work[l] = 0;
		}
		for (int m = 0; m < chou.Length; m++)
		{
			chou[m].init();
		}
		enable_message_trophy = false;
		questioning_message_wait = 0;
	}
}
