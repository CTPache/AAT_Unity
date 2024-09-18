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
			switch (GSStatic.global_work_.language)
			{
			case Language.JAPAN:
			case Language.KOREA:
			case Language.CHINA_S:
			case Language.CHINA_T:
				return board_width_j_;
			case Language.FRANCE:
			case Language.GERMAN:
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
