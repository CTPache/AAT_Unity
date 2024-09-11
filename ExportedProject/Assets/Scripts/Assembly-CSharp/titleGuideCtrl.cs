public class titleGuideCtrl : keyGuideBoard
{
	private static titleGuideCtrl instance_;

	public static titleGuideCtrl instance
	{
		get
		{
			return instance_;
		}
	}

	protected override void Awake()
	{
		if (instance_ == null)
		{
			instance_ = this;
		}
		base.Awake();
	}
}
