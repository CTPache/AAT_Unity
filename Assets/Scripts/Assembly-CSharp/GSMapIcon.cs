using UnityEngine;

public class GSMapIcon : MonoBehaviour
{
	private static GSMapIcon instance_;

	private SpriteRenderer[] icons_;

	private Vector2 BGsize_DS = new Vector2(256f, 192f);

	private Vector2 BGsize_HD = new Vector2(1920f, 1080f);

	private float expantionScale = 6.75f;

	private bool is_expl_higaisha_move_;

	public static GSMapIcon instance
	{
		get
		{
			return instance_;
		}
	}

	public bool is_expl_higaisha_move
	{
		get
		{
			return is_expl_higaisha_move_;
		}
		set
		{
			is_expl_higaisha_move_ = value;
		}
	}

	private void Awake()
	{
		instance_ = this;
	}

	public void Initialize()
	{
		if (icons_ == null)
		{
			icons_ = new SpriteRenderer[GSStatic.expl_char_work_.expl_char_data_.Length];
			for (int i = 0; i < icons_.Length; i++)
			{
				GameObject gameObject = new GameObject("Icon" + i);
				gameObject.layer = base.gameObject.layer;
				gameObject.transform.SetParent(base.transform, false);
				SpriteRenderer spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
				spriteRenderer.enabled = false;
				icons_[i] = spriteRenderer;
			}
		}
	}

	public void Terminate()
	{
		if (icons_ == null)
		{
			Initialize();
		}
		for (int i = 0; i < icons_.Length; i++)
		{
			icons_[i].enabled = false;
			icons_[i].sprite = null;
		}
		ResetExplCharWork();
	}

	public void SetPosition(int id, int x, int y)
	{
		int explCharWorkIndexById = GetExplCharWorkIndexById(id);
		if (explCharWorkIndexById != 255)
		{
			SetPositionByIndex(id, explCharWorkIndexById, x, y);
		}
	}

	public void SetPositionByIndex(int id, int index, int x, int y)
	{
		Vector3 offset = GetOffset(index);
		float scale = GetScale();
		Vector3 vector = Vector3.zero;
		Sprite sprite = icons_[index].sprite;
		if (sprite != null)
		{
			vector = new Vector3(sprite.rect.width * 0.5f, sprite.rect.height * 0.5f);
		}
		if (GSStatic.global_work_.title == TitleId.GS3 && (id == 11 || id == 12))
		{
			vector.x += 27f;
			vector.y += 54f;
		}
		IconSetChangeUV2(id, index);
		ExplCharData explCharData = GSStatic.expl_char_work_.expl_char_data_[index];
		Vector3 localPosition = new Vector3(offset.x + ((float)x + explCharData.move_x) * scale + vector.x, offset.y + ((float)y + explCharData.move_y) * scale + vector.y, 75f);
		byte b = 0;
		switch (GSStatic.global_work_.title)
		{
		case TitleId.GS3:
			if (id == 7)
			{
				b = 1;
			}
			break;
		case TitleId.GS2:
			if (id == 3)
			{
				b = 1;
			}
			break;
		case TitleId.GS1:
			if (id == 4 || id == 5 || id == 6)
			{
				b = 1;
			}
			break;
		}
		if (b == 1)
		{
			localPosition.z += 0.5f;
		}
		if (GSStatic.global_work_.title == TitleId.GS1 && id == 1 && is_expl_higaisha_move_)
		{
			localPosition.y -= 28f;
			is_expl_higaisha_move_ = false;
		}
		localPosition.y = 0f - localPosition.y;
		icons_[index].transform.localPosition = localPosition;
	}

	public void SetVisible(int id, bool on_off)
	{
		int explCharWorkIndexById = GetExplCharWorkIndexById(id);
		if (explCharWorkIndexById != 255)
		{
			SetVisibleByIndex(explCharWorkIndexById, on_off);
		}
	}

	public void SetVisibleByIndex(int index, bool on_off)
	{
		icons_[index].enabled = on_off;
	}

	public void LoadSprite(int id)
	{
		int explCharWorkIndexById = GetExplCharWorkIndexById(id);
		if (explCharWorkIndexById == 255)
		{
			explCharWorkIndexById = GetExplCharWorkIndexById(255);
			if (explCharWorkIndexById != 255)
			{
				LoadSpriteByIndex(explCharWorkIndexById, id);
				ExplCharData[] expl_char_data_ = GSStatic.expl_char_work_.expl_char_data_;
				expl_char_data_[explCharWorkIndexById].id = (byte)id;
				GSExplDataTable.EXPL_DATA explCharData = GSExplDataTable.GetExplCharData((uint)id);
				expl_char_data_[explCharWorkIndexById].para0 = explCharData.para0;
				expl_char_data_[explCharWorkIndexById].para1 = explCharData.para1;
				expl_char_data_[explCharWorkIndexById].move_x = 0f;
				expl_char_data_[explCharWorkIndexById].move_y = 0f;
				expl_char_data_[explCharWorkIndexById].blink = 0;
				icons_[explCharWorkIndexById].flipX = false;
				icons_[explCharWorkIndexById].flipY = false;
				int x = expl_char_data_[explCharWorkIndexById].para1 & 0x1FF;
				int y = expl_char_data_[explCharWorkIndexById].para0 & 0xFF;
				SetPositionByIndex(id, explCharWorkIndexById, x, y);
			}
		}
		else
		{
			ExplCharData[] expl_char_data_2 = GSStatic.expl_char_work_.expl_char_data_;
			expl_char_data_2[explCharWorkIndexById].status &= 251;
			int x2 = expl_char_data_2[explCharWorkIndexById].para1 & 0x1FF;
			int y2 = expl_char_data_2[explCharWorkIndexById].para0 & 0xFF;
			SetVisibleByIndex(explCharWorkIndexById, true);
			SetPositionByIndex(id, explCharWorkIndexById, x2, y2);
		}
	}

	public void LoadSpriteByIndex(int index, int id)
	{
		int title = (int)GSStatic.global_work_.title;
		string explCharFilename = GSExplDataTable.GetExplCharFilename((uint)id);
		AssetBundle assetBundle = AssetBundleCtrl.instance.load("/GS" + (1 + title) + "/itb/", explCharFilename);
		Sprite sprite = null;
		if (assetBundle != null)
		{
			Sprite[] array = assetBundle.LoadAllAssets<Sprite>();
			sprite = ((array.Length <= 0) ? null : array[0]);
		}
		icons_[index].sprite = sprite;
		icons_[index].enabled = true;
		SetPositionByIndex(id, index, 0, 0);
	}

	public void UnloadSprite(int id)
	{
		int explCharWorkIndexById = GetExplCharWorkIndexById(id);
		if (explCharWorkIndexById != 255)
		{
			UnloadSpriteByIndex(explCharWorkIndexById);
			GSStatic.expl_char_work_.expl_char_data_[explCharWorkIndexById].id = byte.MaxValue;
		}
	}

	private void UnloadSpriteByIndex(int index)
	{
		SetVisibleByIndex(index, false);
		icons_[index].sprite = null;
	}

	public static void ResetExplCharWork()
	{
		ExplCharData[] expl_char_data_ = GSStatic.expl_char_work_.expl_char_data_;
		for (int i = 0; i < expl_char_data_.Length; i++)
		{
			expl_char_data_[i].Reset();
		}
	}

	public static int GetExplCharWorkIndexById(int id)
	{
		ExplCharData[] expl_char_data_ = GSStatic.expl_char_work_.expl_char_data_;
		for (int i = 0; i < expl_char_data_.Length; i++)
		{
			if (expl_char_data_[i].id == id)
			{
				return i;
			}
		}
		return 255;
	}

	private Vector3 GetOffset(int _index)
	{
		Vector3 result = new Vector3((BGsize_HD.x - BGsize_DS.x * expantionScale) / 2f, (BGsize_HD.y - BGsize_DS.y * expantionScale) / 2f, 0f);
		ExplCharData[] expl_char_data_ = GSStatic.expl_char_work_.expl_char_data_;
		byte b = GSStatic.global_work_.scenario;
		switch (GSStatic.global_work_.title)
		{
		case TitleId.GS1:
			switch (b)
			{
			case 2:
			case 4:
				result = new Vector3((BGsize_HD.x - BGsize_DS.x * 4.65625f) / 2f - 26f, -93f, 0f);
				if (expl_char_data_[_index].id == 2 || expl_char_data_[_index].id == 3)
				{
					result.y -= 67f;
				}
				break;
			case 6:
				if ((long)bgCtrl.instance.bg_no == 61)
				{
					Vector3 vector5 = new Vector3(result.x, result.y + 28f, 0f);
					result = vector5;
				}
				break;
			default:
				if ((long)bgCtrl.instance.bg_no == 119)
				{
					result = new Vector3(150f, (BGsize_HD.y - BGsize_DS.y * expantionScale) / 2f, 0f);
				}
				break;
			}
			break;
		case TitleId.GS2:
			if ((long)bgCtrl.instance.bg_no == 42)
			{
				Vector3 vector4 = new Vector3(result.x, result.y - 12f, 0f);
				result = vector4;
			}
			break;
		case TitleId.GS3:
			if ((long)bgCtrl.instance.bg_no == 93)
			{
				Vector3 vector = new Vector3((BGsize_HD.x - BGsize_DS.x * expantionScale) / 2f, (BGsize_HD.y - BGsize_DS.y * 5.625f) / 2f, 0f);
				vector += new Vector3(99f, -26f, 0f);
				result = vector;
			}
			if ((long)bgCtrl.instance.bg_no == 112 || (long)bgCtrl.instance.bg_no == 113 || (long)bgCtrl.instance.bg_no == 114 || (long)bgCtrl.instance.bg_no == 115 || (long)bgCtrl.instance.bg_no == 116)
			{
				Vector3 vector2 = new Vector3((BGsize_HD.x - BGsize_DS.x * expantionScale) / 2f, (BGsize_HD.y - BGsize_DS.y * 5.625f) / 2f, 0f);
				vector2 += new Vector3(53f, 0f, 0f);
				result = vector2;
			}
			if ((long)bgCtrl.instance.bg_no == 124 || (long)bgCtrl.instance.bg_no == 125 || (long)bgCtrl.instance.bg_no == 126 || (long)bgCtrl.instance.bg_no == 127 || (long)bgCtrl.instance.bg_no == 128 || (long)bgCtrl.instance.bg_no == 129)
			{
				Vector3 vector3 = new Vector3((BGsize_HD.x - BGsize_DS.x * expantionScale) / 2f, (BGsize_HD.y - BGsize_DS.y * 5.625f) / 2f, 0f);
				vector3 += new Vector3(53f, 0f, 0f);
				result = vector3;
			}
			break;
		}
		return result;
	}

	private float GetScale()
	{
		float result = expantionScale;
		switch (GSStatic.global_work_.title)
		{
		case TitleId.GS1:
			if (GSStatic.global_work_.scenario == 2 || GSStatic.global_work_.scenario == 4)
			{
				result = 4.65625f;
			}
			break;
		}
		return result;
	}

	public void ExplScroll(Vector3 _csPos)
	{
		ExplCharData[] expl_char_data_ = GSStatic.expl_char_work_.expl_char_data_;
		for (int i = 0; i < expl_char_data_.Length; i++)
		{
			if (expl_char_data_[i].id != byte.MaxValue)
			{
				expl_char_data_[i].move_x += _csPos.x;
				expl_char_data_[i].move_y += _csPos.y;
				int num = (int)expl_char_data_[i].move_x;
				int num2 = (int)expl_char_data_[i].move_y;
				int num3 = (expl_char_data_[i].para1 & 0x1FF) + num;
				int num4 = (expl_char_data_[i].para0 & 0xFF) + num2;
				expl_char_data_[i].move_x -= num;
				expl_char_data_[i].move_y -= num2;
				expl_char_data_[i].para0 = (ushort)((expl_char_data_[i].para0 & 0xFF00u) | ((uint)num4 & 0xFFu));
				expl_char_data_[i].para1 = (ushort)((expl_char_data_[i].para1 & 0xFE00u) | ((uint)num3 & 0x1FFu));
				SetPositionByIndex(expl_char_data_[i].id, i, num3, num4);
			}
		}
	}

	public void IconSetChangeUV(int iconID, int mode)
	{
		switch (mode)
		{
		case 0:
			icons_[iconID].flipX = false;
			icons_[iconID].flipY = false;
			break;
		case 1:
			icons_[iconID].flipX = true;
			icons_[iconID].flipY = false;
			break;
		case 2:
			icons_[iconID].flipX = false;
			icons_[iconID].flipY = true;
			break;
		}
	}

	public void IconSetChangeUV2(int iconID, int index)
	{
		if (GSStatic.global_work_.title == TitleId.GS3)
		{
			if (iconID == 12)
			{
				icons_[index].flipX = true;
			}
			icons_[index].flipY = false;
		}
	}
}
