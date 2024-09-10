using UnityEngine;

public class keyGuideCtrl : keyGuideBoard
{
	private static keyGuideCtrl instance_;

	[SerializeField]
	protected float board_width_j_ = 200f;

	[SerializeField]
	protected float board_width_u_ = 250f;

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
			return (GSStatic.global_work_.language != 0) ? board_width_u_ : board_width_j_;
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
