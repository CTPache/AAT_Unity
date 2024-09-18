public struct MG_CHECK_TBL
{
	public int flag;

	public int x;

	public int y;

	public int w;

	public int h;

	public int curW;

	public int curH;

	public int min_cnt;

	public uint sce_flag;

	public uint message_id;

	public string file_name;

	public MG_CHECK_TBL(int flag, int x, int y, int w, int h, int curW, int curH, int min_cnt, uint sce_flag, uint message_id, string file_name)
	{
		this.flag = flag;
		this.x = x;
		this.y = y;
		this.w = w;
		this.h = h;
		this.curW = curW;
		this.curH = curH;
		this.min_cnt = min_cnt;
		this.sce_flag = sce_flag;
		this.message_id = message_id;
		this.file_name = file_name;
	}

	public MG_CHECK_TBL(int flag, int x, int y, int w, int h, int curW, int curH, float min_cnt, uint sce_flag, uint message_id, string file_name)
	{
		this.flag = flag;
		this.x = x;
		this.y = y;
		this.w = w;
		this.h = h;
		this.curW = curW;
		this.curH = curH;
		this.min_cnt = (int)min_cnt;
		this.sce_flag = sce_flag;
		this.message_id = message_id;
		this.file_name = file_name;
	}
}
