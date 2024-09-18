public struct MG_CHECK_INFO
{
	public int chk_num;

	public int chk_type;

	public int chk_min_val;

	public MG_CHECK_TBL[] ptbl;

	public MG_CHECK_INFO(int chk_num, int chk_type, int chk_min_val, MG_CHECK_TBL[] ptbl)
	{
		this.chk_num = chk_num;
		this.chk_type = chk_type;
		this.chk_min_val = chk_min_val;
		this.ptbl = ptbl;
	}
}
