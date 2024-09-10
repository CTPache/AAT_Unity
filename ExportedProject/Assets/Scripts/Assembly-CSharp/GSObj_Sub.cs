public class GSObj_Sub
{
	private static GSObj_Sub instance_;

	public static GSObj_Sub Instance
	{
		get
		{
			return instance_ ?? (instance_ = new GSObj_Sub());
		}
	}

	private TitleId CurrentTitle
	{
		get
		{
			return GSStatic.global_work_.title;
		}
	}

	public void Obj_main()
	{
		if (!(bgCtrl.instance == null))
		{
		}
	}
}
