using UnityEngine;
using UnityEngine.UI;

public class TanchikiRadioWave : MonoBehaviour
{
	[SerializeField]
	private RectTransform rect_transform_;

	[SerializeField]
	private Image icon_;

	[SerializeField]
	private InputTouch touch_wave_;

	private Sprite[] icon_sprites_;

	private int time_;

	private int loop_time_ = 30;

	private int se_time_;

	private int sprite_level_;

	private int level_ = 1;

	private bool is_turn_;

	private bool is_drag_;

	private Vector3 old_pos_ = Vector3.zero;

	private const float DRAG_POWER = 30f;

	private const int LEVEL_0 = 0;

	public const int LEVEL_1 = 1;

	public const int LEVEL_2 = 2;

	public const int LEVEL_3 = 3;

	public const int LEVEL_4 = 4;

	public const int LEVEL_5 = 5;

	private const int LEVEL_5_1 = 6;

	private const int LEVEL_5_2 = 7;

	private const int LEVEL_5_3 = 8;

	private const int LEVEL_5_4 = 9;

	private const int LEVEL_5_5 = 10;

	private const int LEVEL_5_LAST = 11;

	public bool active
	{
		get
		{
			return icon_.enabled;
		}
		set
		{
			icon_.enabled = value;
			if (value)
			{
				icon_.sprite = icon_sprites_[level_];
				time_ = 0;
			}
			else
			{
				reset();
			}
		}
	}

	public int level
	{
		get
		{
			return level_;
		}
		set
		{
			if ((value == 5 && level_ < 5) || (value < 5 && level_ == 5))
			{
				sprite_level_ = value;
				icon_.sprite = icon_sprites_[sprite_level_];
				time_ = 0;
				is_turn_ = true;
			}
			level_ = value;
		}
	}

	public Vector2 position
	{
		get
		{
			return rect_transform_.localPosition;
		}
		set
		{
			rect_transform_.localPosition = value;
		}
	}

	private int sprite_time
	{
		get
		{
			if (level_ <= 4)
			{
				return loop_time_ / (level_ + 1);
			}
			return loop_time_ / 7;
		}
	}

	private int se_interval_time
	{
		get
		{
			switch (level_)
			{
			case 1:
				return 90;
			case 2:
				return 75;
			case 3:
				return 60;
			case 4:
				return 45;
			case 5:
				return 20;
			default:
				return 255;
			}
		}
	}

	private void load()
	{
		string in_path = "/menu/common/";
		string in_name = "s2d040";
		AssetBundle assetBundle = AssetBundleCtrl.instance.load(in_path, in_name);
		icon_sprites_ = assetBundle.LoadAllAssets<Sprite>();
	}

	public void init()
	{
		load();
		active = false;
		touch_wave_.ActiveCollider();
		touch_wave_.down_event = delegate
		{
			is_drag_ = false;
			old_pos_ = MiniGameCursor.instance.cursor_position;
		};
		touch_wave_.touch_event = delegate
		{
			touch_wave_.touch_key_type = ((!is_drag_) ? KeyType.A : KeyType.None);
		};
		touch_wave_.drag_event = delegate
		{
			Vector3 cursor_position = MiniGameCursor.instance.cursor_position;
			if ((Mathf.Abs(old_pos_.x - cursor_position.x) >= 30f && !is_drag_) || (Mathf.Abs(old_pos_.y - cursor_position.y) >= 30f && !is_drag_))
			{
				is_drag_ = true;
			}
		};
	}

	public void end()
	{
		active = false;
	}

	public void update()
	{
		time_++;
		se_time_++;
		if (se_time_ >= se_interval_time)
		{
			soundCtrl.instance.PlaySE(172);
			se_time_ = 0;
		}
		if (level_ <= 4)
		{
			if (!is_turn_)
			{
				if (time_ >= (sprite_level_ + 1) * sprite_time)
				{
					if (sprite_level_ < level_ - 1)
					{
						sprite_level_++;
					}
					else
					{
						is_turn_ = true;
						time_ = 0;
						sprite_level_ = level_;
					}
					icon_.sprite = icon_sprites_[sprite_level_];
				}
			}
			else if (time_ >= (level_ - sprite_level_ + 1) * sprite_time)
			{
				if (sprite_level_ > 0)
				{
					sprite_level_--;
				}
				else
				{
					is_turn_ = false;
					time_ = 0;
					sprite_level_ = 0;
				}
				icon_.sprite = icon_sprites_[sprite_level_];
			}
		}
		else if (time_ >= (sprite_level_ - 5 + 1) * sprite_time)
		{
			if (sprite_level_ < 11)
			{
				sprite_level_++;
			}
			else
			{
				time_ = 0;
				sprite_level_ = 5;
			}
			icon_.sprite = icon_sprites_[sprite_level_];
		}
	}

	private void reset()
	{
		icon_.sprite = null;
		level_ = 1;
		sprite_level_ = 0;
		time_ = 0;
		is_turn_ = false;
	}
}
