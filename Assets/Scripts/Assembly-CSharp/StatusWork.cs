using System;

[Serializable]
public class StatusWork
{
	[Flags]
	public enum PageStatus
	{
		PAGE_ITEM = 1,
		PAGE_CHAR_DISP = 2,
		ARROW_DISP = 4,
		MAGADAMA_TUKITUKE = 8,
		PSYLOCK_TUKITUKE = 0x10,
		PHONE_KURAE = 0x20
	}

	public byte page_status;

	public byte page_now;

	public byte page_now_max;

	public byte item_page_max;

	public byte name_page_max;

	public byte[] now_file;

	public byte[] item_file = new byte[32];

	public byte[] name_file = new byte[32];

	public void init()
	{
		page_status = 0;
		page_now = 0;
		page_now_max = 0;
		item_page_max = 0;
		name_page_max = 0;
		for (int i = 0; i < item_file.Length; i++)
		{
			item_file[i] = 0;
		}
		for (int j = 0; j < name_file.Length; j++)
		{
			name_file[j] = 0;
		}
	}
}
