using System;

[Serializable]
public class TALK_DATA
{
	public uint room;

	public uint pl_id;

	public uint dm;

	public uint sw;

	public uint[] tag = new uint[4];

	public uint[] flag = new uint[4];

	public uint[] mess = new uint[4];

	public TALK_DATA()
	{
		room = uint.MaxValue;
		pl_id = uint.MaxValue;
		dm = uint.MaxValue;
		sw = uint.MaxValue;
		tag[0] = uint.MaxValue;
		tag[1] = uint.MaxValue;
		tag[2] = uint.MaxValue;
		tag[3] = uint.MaxValue;
		flag[0] = uint.MaxValue;
		flag[1] = uint.MaxValue;
		flag[2] = uint.MaxValue;
		flag[3] = uint.MaxValue;
		mess[0] = uint.MaxValue;
		mess[1] = uint.MaxValue;
		mess[2] = uint.MaxValue;
		mess[3] = uint.MaxValue;
	}

	public TALK_DATA(uint in_room, uint in_pl_id, uint in_dm, uint in_sw, uint in_tag_0, uint in_tag_1, uint in_tag_2, uint in_tag_3, uint in_flag_0, uint in_flag_1, uint in_flag_2, uint in_flag_3, uint in_mess_0, uint in_mess_1, uint in_mess_2, uint in_mess_3)
	{
		room = in_room;
		pl_id = in_pl_id;
		dm = in_dm;
		sw = in_sw;
		tag[0] = in_tag_0;
		tag[1] = in_tag_1;
		tag[2] = in_tag_2;
		tag[3] = in_tag_3;
		flag[0] = in_flag_0;
		flag[1] = in_flag_1;
		flag[2] = in_flag_2;
		flag[3] = in_flag_3;
		mess[0] = in_mess_0;
		mess[1] = in_mess_1;
		mess[2] = in_mess_2;
		mess[3] = in_mess_3;
	}
}
