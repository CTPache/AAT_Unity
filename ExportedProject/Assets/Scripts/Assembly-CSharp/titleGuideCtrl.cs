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

	private void Awake()
	{
		if (instance_ == null)
		{
			instance_ = this;
		}
	}
}
