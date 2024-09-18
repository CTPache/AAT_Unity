public class SHOW_DATA
{
	public uint room;

	public uint roomseq;

	public uint item;

	public uint pl_id;

	public uint end;

	public uint mess_true;

	public uint mess_false;

	public SHOW_DATA(uint in_room, uint in_roomseq, uint in_item, uint in_pl_id, uint in_end, uint in_mess_true, uint in_mess_false)
	{
		room = in_room;
		roomseq = in_roomseq;
		item = in_item;
		pl_id = in_pl_id;
		end = in_end;
		mess_true = in_mess_true;
		mess_false = in_mess_false;
	}
}
