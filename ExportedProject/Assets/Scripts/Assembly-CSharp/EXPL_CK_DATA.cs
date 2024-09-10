public class EXPL_CK_DATA
{
	public GSPoint4[] point = new GSPoint4[2];

	public uint trueMes;

	public uint falseMes1;

	public uint falseMes2;

	public uint dm00;

	public ushort[] p = new ushort[4];

	public EXPL_CK_DATA(GSPoint4 in_point, GSPoint4 in_point2, uint in_trueMes, uint in_falseMes1, uint in_falseMes2, uint in_dm00, ushort[] in_p)
	{
		point[0] = in_point;
		point[1] = in_point2;
		trueMes = in_trueMes;
		falseMes1 = in_falseMes1;
		falseMes2 = in_falseMes2;
		dm00 = in_dm00;
		p = in_p;
	}
}
