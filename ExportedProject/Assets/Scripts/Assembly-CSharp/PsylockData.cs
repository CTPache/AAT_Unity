using System;

[Serializable]
public class PsylockData
{
	public uint status;

	public ushort room;

	public ushort pl_id;

	public byte level;

	public byte size;

	public ushort start_message;

	public ushort cancel_message;

	public ushort correct_message;

	public ushort wrong_message;

	public ushort die_message;

	public ushort cancel_bgm;

	public ushort unlock_bgm;

	public const int PSYLOCK_MULTI_ANSWER_SIZE = 4;

	public uint item_size;

	public byte[] item_no = new byte[4];

	public ushort[] item_correct_message = new ushort[4];

	public void Clear()
	{
		status = 0u;
		room = 0;
		pl_id = 0;
		level = 0;
		size = 0;
		start_message = 0;
		cancel_message = 0;
		correct_message = 0;
		wrong_message = 0;
		die_message = 0;
		cancel_bgm = 0;
		unlock_bgm = 0;
		item_size = 0u;
		for (int i = 0; i < item_no.Length; i++)
		{
			item_no[i] = 0;
		}
		for (int j = 0; j < item_correct_message.Length; j++)
		{
			item_correct_message[j] = 0;
		}
	}

	public void init()
	{
		status = 0u;
		room = 0;
		pl_id = 0;
		level = 0;
		size = 0;
		start_message = 0;
		cancel_message = 0;
		correct_message = 0;
		wrong_message = 0;
		die_message = 0;
		cancel_bgm = 0;
		unlock_bgm = 0;
		item_size = 0u;
		for (int i = 0; i < item_no.Length; i++)
		{
			item_no[i] = 0;
		}
		for (int j = 0; j < item_correct_message.Length; j++)
		{
			item_correct_message[j] = 0;
		}
	}
}
