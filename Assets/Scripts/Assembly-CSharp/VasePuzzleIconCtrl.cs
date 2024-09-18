using UnityEngine;

public class VasePuzzleIconCtrl : MonoBehaviour
{
	private enum icon_type
	{
		noused = 0,
		enable_on = 1,
		enable_off = 2,
		Current = 3
	}

	private Vector2 icon_offset_position_ = new Vector2(100f, 28f);

	private float icon_size_x_ = 200f;

	private GameObject iconbase_object_;

	private GameObject[] icon_board_list_;

	private GameObject[] icon_list_;

	public static VasePuzzleIconCtrl instance { get; private set; }

	private void Awake()
	{
		instance = this;
	}

	public void init(PiecesStatus[] in_pieces_status)
	{
		_loadAssetBundle(in_pieces_status);
		_settingIconPosition();
		setIconStatus(in_pieces_status, -1);
	}

	public void exit()
	{
		_freeAssetBundle();
	}

	public void setIconStatus(PiecesStatus[] in_pieces_status, int in_current_index)
	{
		for (int i = 0; i < icon_board_list_.Length; i++)
		{
			VasePuzzleSprite component = icon_board_list_[i].GetComponent<VasePuzzleSprite>();
			if (i == in_current_index)
			{
				component.sprite.spriteNo(3);
			}
			else if (in_pieces_status[i].used)
			{
				component.sprite.spriteNo(2);
			}
			else
			{
				component.sprite.spriteNo(1);
			}
		}
		for (int j = 0; j < icon_list_.Length; j++)
		{
			VasePuzzleSprite component2 = icon_list_[j].GetComponent<VasePuzzleSprite>();
			if (in_pieces_status[j].used)
			{
				component2.sprite.sprite_renderer_.enabled = false;
				continue;
			}
			component2.sprite.sprite_renderer_.enabled = true;
			icon_list_[j].transform.localEulerAngles = new Vector3(0f, 0f, in_pieces_status[j].angle_id * 90);
		}
	}

	private void _loadAssetBundle(PiecesStatus[] in_pieces_status)
	{
		iconbase_object_ = VasePuzzleUtil.instance.createAssetBundle("/GS1/minigame/", "record02", VasePuzzleUtil.SortOrder.IconWindow);
		icon_board_list_ = new GameObject[in_pieces_status.Length];
		for (int i = 0; i < icon_board_list_.Length; i++)
		{
			icon_board_list_[i] = VasePuzzleUtil.instance.createAssetBundle("/menu/common/", "record_icon", VasePuzzleUtil.SortOrder.IconBase);
			icon_board_list_[i].AddComponent<BoxCollider2D>();
			InputTouch inputTouch = icon_board_list_[i].AddComponent<InputTouch>();
			inputTouch.argument_parameter = i;
			inputTouch.touch_event = delegate(TouchParameter p)
			{
				VasePuzzleMiniGame.instance.SetCursorIndex((int)p.argument_parameter);
			};
		}
		icon_list_ = new GameObject[in_pieces_status.Length];
		for (int j = 0; j < icon_list_.Length; j++)
		{
			icon_list_[j] = VasePuzzleUtil.instance.createAssetBundle("/GS1/minigame/", in_pieces_status[j].icon_name, VasePuzzleUtil.SortOrder.Icon);
		}
	}

	private void _freeAssetBundle()
	{
		Object.Destroy(iconbase_object_);
		GameObject[] array = icon_list_;
		foreach (GameObject obj in array)
		{
			Object.Destroy(obj);
		}
		icon_list_ = null;
		GameObject[] array2 = icon_board_list_;
		foreach (GameObject obj2 in array2)
		{
			Object.Destroy(obj2);
		}
		icon_board_list_ = null;
	}

	private void _settingIconPosition()
	{
		iconbase_object_.transform.localPosition = new Vector3(0f, -386f, 0f);
		float num = icon_size_x_ * (float)icon_board_list_.Length / -2f;
		for (int i = 0; i < icon_board_list_.Length; i++)
		{
			icon_board_list_[i].transform.parent = iconbase_object_.transform;
			icon_board_list_[i].transform.localPosition = new Vector3(icon_offset_position_.x + num, icon_offset_position_.y, 0f);
			num += icon_size_x_;
		}
		for (int j = 0; j < icon_list_.Length; j++)
		{
			icon_list_[j].transform.parent = icon_board_list_[j].transform;
			icon_list_[j].transform.localPosition = new Vector3(0f, -8f, 0f);
		}
	}
}
