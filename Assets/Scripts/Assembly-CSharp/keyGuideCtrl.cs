using UnityEngine;

public class keyGuideCtrl : keyGuideBoard
{
	private static keyGuideCtrl instance_;

	[SerializeField]
	protected float board_width_j_ = 200f;

	[SerializeField]
	protected float board_width_u_ = 250f;

	[SerializeField]
	protected float board_width_f_ = 320f;

	public static keyGuideCtrl instance
	{
		get
		{
			return instance_;
		}
	}

	protected override bool is_long_guide
	{
		get
		{
			return board_width_ > board_width_j_;
		}
	}

	protected override float board_width_
	{
		get
        {
            string lang = Language.langFallback[GSStatic.global_work_.language].ToUpper();
            switch (lang)
            {
			case "JAPAN":
			case "KOREA":
			case "CHINA_S":
			case "CHINA_T":
				return board_width_j_;
			case "FRANCE":
			case "GERMAN":
				return board_width_u_;
			default:
				return board_width_u_;
			}
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
