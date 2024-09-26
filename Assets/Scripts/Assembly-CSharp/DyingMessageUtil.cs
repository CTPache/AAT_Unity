using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DyingMessageUtil : MonoBehaviour
{
	public struct tagMG_CHECK_LINEPOINT
	{
		public ushort x;

		public ushort y;

		public ushort w;

		public ushort h;

		public ushort lx;

		public ushort ly;

		public tagMG_CHECK_LINEPOINT(ushort in_x, ushort in_y, ushort in_w, ushort in_h, ushort in_lx, ushort in_ly)
		{
			x = in_x;
			y = in_y;
			w = in_w;
			h = in_h;
			lx = in_lx;
			ly = in_ly;
		}
	}

	public struct LINE_DRAW_POINT
	{
		public ushort lx;

		public ushort ly;

		public LINE_DRAW_POINT(ushort in_lx, ushort in_ly, Vector2 offset, Vector2 scale)
		{
			lx = (ushort)Mathf.FloorToInt(offset.x + (float)(int)in_lx * scale.x);
			ly = (ushort)Mathf.FloorToInt(offset.y + (float)(int)in_ly * scale.y);
		}
	}

	public struct tagLINE_LIST
	{
		public int pt1;

		public int pt2;

		public tagLINE_LIST(int in_pt1, int in_pt2)
		{
			if (in_pt2 > in_pt1)
			{
				pt1 = in_pt1;
				pt2 = in_pt2;
			}
			else
			{
				pt1 = in_pt2;
				pt2 = in_pt1;
			}
		}
	}

	private enum State
	{
		Set = 0,
		Free = 1,
		Line = 2
	}

	private tagMG_CHECK_LINEPOINT[] mg_chk_die_jp = new tagMG_CHECK_LINEPOINT[15]
	{
		new tagMG_CHECK_LINEPOINT(112, 22, 16, 32, 118, 33),
		new tagMG_CHECK_LINEPOINT(64, 35, 16, 32, 70, 44),
		new tagMG_CHECK_LINEPOINT(136, 35, 32, 16, 157, 46),
		new tagMG_CHECK_LINEPOINT(197, 43, 32, 16, 212, 46),
		new tagMG_CHECK_LINEPOINT(27, 67, 32, 16, 39, 77),
		new tagMG_CHECK_LINEPOINT(104, 67, 16, 16, 108, 74),
		new tagMG_CHECK_LINEPOINT(72, 75, 32, 16, 79, 86),
		new tagMG_CHECK_LINEPOINT(96, 91, 16, 16, 102, 101),
		new tagMG_CHECK_LINEPOINT(112, 104, 16, 16, 118, 113),
		new tagMG_CHECK_LINEPOINT(29, 120, 32, 16, 35, 131),
		new tagMG_CHECK_LINEPOINT(84, 117, 16, 32, 89, 127),
		new tagMG_CHECK_LINEPOINT(189, 128, 16, 16, 195, 136),
		new tagMG_CHECK_LINEPOINT(104, 136, 16, 16, 112, 146),
		new tagMG_CHECK_LINEPOINT(205, 152, 16, 32, 211, 169),
		new tagMG_CHECK_LINEPOINT(96, 160, 32, 16, 102, 169)
	};

	private tagMG_CHECK_LINEPOINT[] mg_chk_die_us = new tagMG_CHECK_LINEPOINT[12]
	{
		new tagMG_CHECK_LINEPOINT(44, 27, 32, 32, 55, 34),
		new tagMG_CHECK_LINEPOINT(96, 35, 32, 16, 100, 41),
		new tagMG_CHECK_LINEPOINT(184, 80, 16, 16, 190, 87),
		new tagMG_CHECK_LINEPOINT(144, 83, 16, 16, 150, 90),
		new tagMG_CHECK_LINEPOINT(203, 99, 16, 16, 212, 105),
		new tagMG_CHECK_LINEPOINT(19, 123, 16, 32, 23, 137),
		new tagMG_CHECK_LINEPOINT(80, 123, 16, 16, 88, 131),
		new tagMG_CHECK_LINEPOINT(181, 128, 16, 16, 187, 139),
		new tagMG_CHECK_LINEPOINT(213, 128, 16, 16, 219, 134),
		new tagMG_CHECK_LINEPOINT(144, 136, 16, 16, 149, 143),
		new tagMG_CHECK_LINEPOINT(160, 152, 16, 16, 170, 159),
		new tagMG_CHECK_LINEPOINT(219, 152, 16, 16, 225, 157)
	};

	private tagMG_CHECK_LINEPOINT[] mg_chk_die_korea = new tagMG_CHECK_LINEPOINT[15]
	{
		new tagMG_CHECK_LINEPOINT(29, 41, 16, 24, 37, 53),
		new tagMG_CHECK_LINEPOINT(30, 116, 16, 24, 38, 128),
		new tagMG_CHECK_LINEPOINT(50, 53, 16, 20, 58, 63),
		new tagMG_CHECK_LINEPOINT(49, 105, 16, 20, 57, 115),
		new tagMG_CHECK_LINEPOINT(74, 47, 20, 28, 85, 63),
		new tagMG_CHECK_LINEPOINT(79, 97, 16, 20, 87, 107),
		new tagMG_CHECK_LINEPOINT(106, 77, 16, 24, 114, 89),
		new tagMG_CHECK_LINEPOINT(105, 157, 16, 24, 113, 169),
		new tagMG_CHECK_LINEPOINT(132, 94, 16, 16, 140, 103),
		new tagMG_CHECK_LINEPOINT(128, 144, 20, 20, 137, 156),
		new tagMG_CHECK_LINEPOINT(157, 144, 16, 16, 164, 153),
		new tagMG_CHECK_LINEPOINT(181, 53, 16, 20, 189, 63),
		new tagMG_CHECK_LINEPOINT(179, 85, 16, 14, 186, 92),
		new tagMG_CHECK_LINEPOINT(180, 128, 16, 14, 187, 135),
		new tagMG_CHECK_LINEPOINT(180, 142, 16, 16, 188, 150)
	};

	[SerializeField]
	private MiniGameCursor cursor_;

	[SerializeField]
	private Transform line_point_base_;

	[SerializeField]
	private DyingMessageMiniGame dyingmessage;

	private DebugMiniGameGSPoint4Hit debug_hit_;

	private GSPoint4[] converted_point_;

	private LINE_DRAW_POINT[] draw_point_;

	private List<tagLINE_LIST> line_list_ = new List<tagLINE_LIST>();

	private List<GameObject> line_object_list_ = new List<GameObject>();

	private State stete_;

	private Sprite[] icon_sprites_;

	private int linepoint_start_index_;

	private LineRenderer select_line_;

	public static DyingMessageUtil instance { get; private set; }

	private tagMG_CHECK_LINEPOINT[] mg_chk_die_
	{
		get
        {
            string lang = Language.langFallback[GSStatic.global_work_.language].ToUpper();
            switch (lang)
            {
			case "JAPAN":
			case "CHINA_S":
			case "CHINA_T":
				return mg_chk_die_jp;
			case "KOREA":
				return mg_chk_die_korea;
			default:
				return mg_chk_die_us;
			}
		}
	}

	public MiniGameCursor cursor
	{
		get
		{
			return cursor_;
		}
		set
		{
			cursor_ = value;
		}
	}

	public void init_die_message()
	{
		line_list_.Clear();
		GameObject gameObject = new GameObject("SelectLine");
		gameObject.layer = base.gameObject.layer;
		gameObject.transform.SetParent(line_point_base_, false);
		gameObject.transform.localPosition = new Vector3(0f, 0f, -11f);
		select_line_ = gameObject.AddComponent<LineRenderer>();
		select_line_.useWorldSpace = false;
		select_line_.positionCount = 2;
		Color color = new Color(0.42f, 0.734f, 0.905f);
		select_line_.startColor = color;
		select_line_.endColor = color;
		select_line_.widthMultiplier = 100f;
		select_line_.numCapVertices = 10;
		select_line_.material = new Material(Shader.Find("Sprites/Default"));
		select_line_.enabled = false;
		debugLogger.instance.Log("DyingMessage", "_updateDyingMessageMain :State.Set.");
		AssetBundleCtrl assetBundleCtrl = AssetBundleCtrl.instance;
		AssetBundle assetBundle = assetBundleCtrl.load("/menu/common/", "s2d051");
		icon_sprites_ = assetBundle.LoadAssetWithSubAssets<Sprite>("s2d051");
		cursor_.icon_offset = new Vector3(48f, -48f, 0f);
		cursor_.icon_sprite = icon_sprites_[0];
		cursor_.icon_visible = true;
		cursor_.cursor_position = new Vector3(0f, cursor_.cursor_area_size.y * 0.5f, 0f);
		MiniGameCursor.instance.ActiveCursorCanvas(true);
	}

	public void body_die_message()
	{
		_updateDyingMessageMain();
	}

	public void end_die_message()
	{
		MiniGameCursor.instance.ActiveCursorCanvas(false);
		cursor_.cursor_exception_limit = Vector2.zero;
		cursor_.icon_visible = false;
		cursor_.icon_sprite = null;
		icon_sprites_ = null;
		coroutineCtrl.instance.Play(keyGuideCtrl.instance.open(keyGuideBase.Type.NO_GUIDE));
		foreach (GameObject item in line_object_list_)
		{
			Object.Destroy(item);
		}
		line_object_list_.Clear();
		line_list_.Clear();
		select_line_.enabled = false;
		stete_ = State.Set;
	}

	public bool checkDieMessage_jp()
	{
		int num = 1;
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		int num5 = 0;
		int num6 = 0;
		for (int i = 0; i < line_list_.Count; i++)
		{
			switch (line_list_[i].pt1)
			{
			case 0:
				if (line_list_[i].pt2 != 5)
				{
					num = 0;
				}
				else
				{
					num2++;
				}
				break;
			case 1:
				if (line_list_[i].pt2 != 6)
				{
					num = 0;
				}
				else
				{
					num2++;
				}
				break;
			case 2:
				if (line_list_[i].pt2 != 4)
				{
					num = 0;
				}
				else
				{
					num2++;
				}
				break;
			case 3:
				switch (line_list_[i].pt2)
				{
				case 7:
					num3 = 1;
					break;
				case 9:
					num4 = 1;
					break;
				default:
					num = 0;
					break;
				}
				break;
			case 4:
				num = 0;
				break;
			case 5:
				num = 0;
				break;
			case 6:
				num = 0;
				break;
			case 7:
				switch (line_list_[i].pt2)
				{
				case 9:
					num5 = 1;
					break;
				case 12:
					num6 = 1;
					break;
				default:
					num = 0;
					break;
				}
				break;
			case 8:
				if (line_list_[i].pt2 != 10)
				{
					num = 0;
				}
				else
				{
					num2++;
				}
				break;
			case 9:
				num = 0;
				break;
			case 10:
				if (line_list_[i].pt2 != 14)
				{
					num = 0;
				}
				else
				{
					num2++;
				}
				break;
			case 11:
				if (line_list_[i].pt2 != 13)
				{
					num = 0;
				}
				else
				{
					num2++;
				}
				break;
			case 12:
				if (line_list_[i].pt2 != 14)
				{
					num = 0;
				}
				break;
			case 13:
				if (line_list_[i].pt2 != 14)
				{
					num = 0;
				}
				else
				{
					num2++;
				}
				break;
			case 14:
				num = 0;
				break;
			}
			if (num == 0)
			{
				break;
			}
		}
		if (num == 1)
		{
			if (num2 != 7)
			{
				num = 0;
			}
			else if (num6 == 0)
			{
				num = 0;
			}
			else if (num4 == 0 && (num3 == 0 || num5 == 0))
			{
				num = 0;
			}
		}
		return num == 1;
	}

	public bool checkDieMessage_us()
	{
		int num = 1;
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		int num5 = 0;
		int num6 = 0;
		int num7 = 0;
		int num8 = 0;
		for (int i = 0; i < line_list_.Count; i++)
		{
			switch (line_list_[i].pt1)
			{
			case 0:
				switch (line_list_[i].pt2)
				{
				case 1:
					num2++;
					break;
				case 5:
					num2++;
					break;
				default:
					num = 0;
					break;
				}
				break;
			case 1:
				num = 0;
				break;
			case 2:
				switch (line_list_[i].pt2)
				{
				case 3:
					num2++;
					break;
				case 9:
					num2++;
					break;
				default:
					num = 0;
					break;
				}
				break;
			case 3:
				num = 0;
				break;
			case 4:
				switch (line_list_[i].pt2)
				{
				case 7:
					num3++;
					break;
				case 8:
					num4++;
					break;
				case 10:
					num5++;
					break;
				case 11:
					num6++;
					break;
				default:
					num = 0;
					break;
				}
				break;
			case 5:
				if (line_list_[i].pt2 != 6)
				{
					num = 0;
				}
				else
				{
					num2++;
				}
				break;
			case 6:
				num = 0;
				break;
			case 7:
				switch (line_list_[i].pt2)
				{
				case 8:
					num2++;
					break;
				case 10:
					num7++;
					break;
				default:
					num = 0;
					break;
				}
				break;
			case 8:
			{
				int pt = line_list_[i].pt2;
				if (pt == 11)
				{
					num8 = 1;
				}
				else
				{
					num = 0;
				}
				break;
			}
			case 9:
				num = 0;
				break;
			case 10:
				num = 0;
				break;
			case 11:
				num = 0;
				break;
			}
			if (num == 0)
			{
				break;
			}
		}
		if (num != 0)
		{
			if (num2 != 6)
			{
				num = 0;
			}
			else
			{
				if (num5 == 0 && (num3 == 0 || num7 == 0))
				{
					num = 0;
				}
				if (num6 == 0 && (num4 == 0 || num8 == 0))
				{
					num = 0;
				}
			}
		}
		return num == 1;
	}

	public bool checkDieMessage_korea()
	{
		bool flag = true;
		int num = 7;
		int num2 = 0;
		for (int i = 0; i < line_list_.Count; i++)
		{
			Debug.Log(i + " : pt1=" + line_list_[i].pt1 + ", pt2=" + line_list_[i].pt2);
		}
		for (int j = 0; j < line_list_.Count; j++)
		{
			switch (line_list_[j].pt1)
			{
			case 0:
			{
				int pt8 = line_list_[j].pt2;
				if (pt8 == 1)
				{
					num2++;
				}
				else
				{
					flag = false;
				}
				break;
			}
			case 1:
				flag = false;
				break;
			case 2:
			{
				int pt4 = line_list_[j].pt2;
				if (pt4 == 3)
				{
					num2++;
				}
				else
				{
					flag = false;
				}
				break;
			}
			case 3:
				flag = false;
				break;
			case 4:
			{
				int pt6 = line_list_[j].pt2;
				if (pt6 == 5)
				{
					num2++;
				}
				else
				{
					flag = false;
				}
				break;
			}
			case 5:
				flag = false;
				break;
			case 6:
			{
				int pt2 = line_list_[j].pt2;
				if (pt2 == 7)
				{
					num2++;
				}
				else
				{
					flag = false;
				}
				break;
			}
			case 7:
				flag = false;
				break;
			case 8:
			{
				int pt7 = line_list_[j].pt2;
				if (pt7 == 9)
				{
					num2++;
				}
				else
				{
					flag = false;
				}
				break;
			}
			case 9:
			{
				int pt5 = line_list_[j].pt2;
				if (pt5 == 10)
				{
					num2++;
				}
				else
				{
					flag = false;
				}
				break;
			}
			case 10:
				flag = false;
				break;
			case 11:
			{
				int pt3 = line_list_[j].pt2;
				if (pt3 == 12)
				{
					num2++;
				}
				else
				{
					flag = false;
				}
				break;
			}
			case 12:
				flag = false;
				break;
			case 13:
			{
				int pt = line_list_[j].pt2;
				if (pt != 14)
				{
					flag = false;
				}
				break;
			}
			case 14:
				flag = false;
				break;
			}
			if (!flag)
			{
				break;
			}
		}
		if (flag && num2 < num)
		{
			flag = false;
		}
		return flag;
	}

	private void Awake()
	{
		instance = this;
	}

	public void CreatePoint()
	{
		GSPoint4[] array = new GSPoint4[mg_chk_die_.Length];
		draw_point_ = new LINE_DRAW_POINT[mg_chk_die_.Length];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = new GSPoint4(scenario.BATD_X + mg_chk_die_[i].x, scenario.BATD_Y + mg_chk_die_[i].y, scenario.BATD_X + mg_chk_die_[i].x + mg_chk_die_[i].w, scenario.BATD_Y + mg_chk_die_[i].y, scenario.BATD_X + mg_chk_die_[i].x + mg_chk_die_[i].w, scenario.BATD_Y + mg_chk_die_[i].y + mg_chk_die_[i].h, scenario.BATD_X + mg_chk_die_[i].x, scenario.BATD_Y + mg_chk_die_[i].y + mg_chk_die_[i].h);
			draw_point_[i] = new LINE_DRAW_POINT((ushort)(scenario.BATD_X + mg_chk_die_[i].lx), (ushort)(scenario.BATD_Y + mg_chk_die_[i].ly), new Vector2(100f, -230f), new Vector2(6.4f, 6.4f));
		}
		converted_point_ = MiniGameGSPoint4Hit.ConvertPoint(array, new Vector2(100f, -230f), new Vector2(6.4f, 6.4f)).ToArray();
		for (int j = 0; j < array.Length; j++)
		{
			float num = converted_point_[j].x1 - converted_point_[j].x0;
			float num2 = converted_point_[j].x2 - converted_point_[j].x0;
			float num3 = converted_point_[j].x1 - converted_point_[j].x3;
			float num4 = converted_point_[j].x2 - converted_point_[j].x3;
			float num5 = converted_point_[j].y2 - converted_point_[j].y0;
			float num6 = converted_point_[j].y3 - converted_point_[j].y0;
			float num7 = converted_point_[j].y2 - converted_point_[j].y1;
			float num8 = converted_point_[j].y3 - converted_point_[j].y1;
			float x = Mathf.Min(num, num2, num3, num4);
			float y = Mathf.Min(num5, num6, num7, num8);
			dyingmessage.AddOffsetList(j, x, y);
		}
	}

	private void _updateDyingMessageMain()
	{
		int num = MiniGameGSPoint4Hit.CheckHit(GetCursorRect(), converted_point_);
		if (stete_ == State.Set)
		{
			stete_ = State.Free;
			return;
		}
		cursor_.Process();
		select_line_.SetPosition(0, new Vector3(cursor_.cursor_position.x, 0f - cursor_.cursor_position.y, cursor_.cursor_position.z));
		if (0 <= num)
		{
			cursor_.icon_sprite = icon_sprites_[1];
			if (padCtrl.instance.GetKeyDown(KeyType.A))
			{
				if (stete_ == State.Free)
				{
					debugLogger.instance.Log("DyingMessage", "LinePoint:start[" + num + "]");
					linepoint_start_index_ = num;
					stete_ = State.Line;
					LINE_DRAW_POINT lINE_DRAW_POINT = draw_point_[num];
					select_line_.SetPosition(1, new Vector3((int)lINE_DRAW_POINT.lx, -lINE_DRAW_POINT.ly, cursor_.cursor_position.z));
					select_line_.enabled = true;
				}
				else if (stete_ == State.Line)
				{
					debugLogger.instance.Log("DyingMessage", "LinePoint:start[" + linepoint_start_index_ + "] end[" + num + "]");
					if (!_checkOverrlapLine(linepoint_start_index_, num) && linepoint_start_index_ != num)
					{
						tagLINE_LIST in_line = new tagLINE_LIST(linepoint_start_index_, num);
						_createLine(in_line);
						stete_ = State.Free;
						soundCtrl.instance.PlaySE(52);
						select_line_.enabled = false;
					}
				}
			}
		}
		else
		{
			cursor_.icon_sprite = icon_sprites_[0];
		}
		if (padCtrl.instance.GetKeyDown(KeyType.B))
		{
			if (stete_ == State.Free)
			{
				_deleteLine();
			}
			else if (stete_ == State.Line)
			{
				soundCtrl.instance.PlaySE(51);
				linepoint_start_index_ = 0;
				stete_ = State.Free;
				select_line_.enabled = false;
			}
		}
	}

	private GSRect GetCursorRect()
	{
		Vector3 cursor_position = MiniGameCursor.instance.cursor_position;
		return new GSRect((short)(cursor_position.x - 8f), (short)(cursor_position.y - 8f), 16, 16);
	}

	private bool _checkOverrlapLine(int in_start, int in_end)
	{
		foreach (tagLINE_LIST item in line_list_)
		{
			if (item.pt1 == in_start && item.pt2 == in_end)
			{
				return true;
			}
			if (item.pt1 == in_end && item.pt2 == in_start)
			{
				return true;
			}
		}
		return false;
	}

	private void _createLine(tagLINE_LIST in_line)
	{
		int count = line_list_.Count;
		debugLogger.instance.Log("DyingMessage", "createLine index:" + count);
		GameObject gameObject = new GameObject("Line" + count);
		gameObject.layer = base.gameObject.layer;
		gameObject.transform.SetParent(line_point_base_, false);
		gameObject.transform.localPosition = new Vector3(0f, 0f, -9f);
		LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
		lineRenderer.useWorldSpace = false;
		lineRenderer.positionCount = 2;
		Color endColor = (lineRenderer.startColor = new Color(0.588f, 0.141f, 0.141f));
		lineRenderer.endColor = endColor;
		lineRenderer.widthMultiplier = 100f;
		lineRenderer.numCapVertices = 10;
		lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
		LINE_DRAW_POINT lINE_DRAW_POINT = draw_point_[in_line.pt1];
		LINE_DRAW_POINT lINE_DRAW_POINT2 = draw_point_[in_line.pt2];
		lineRenderer.SetPositions(new Vector3[2]
		{
			new Vector3((int)lINE_DRAW_POINT.lx, -lINE_DRAW_POINT.ly, 0f),
			new Vector3((int)lINE_DRAW_POINT2.lx, -lINE_DRAW_POINT2.ly, 0f)
		});
		line_object_list_.Add(gameObject);
		line_list_.Add(in_line);
	}

	private void _deleteLine()
	{
		if (line_object_list_.Count != 0)
		{
			soundCtrl.instance.PlaySE(51);
			GameObject obj = line_object_list_[line_object_list_.Count - 1];
			line_object_list_.RemoveAt(line_object_list_.Count - 1);
			Object.Destroy(obj);
		}
		if (line_list_.Count != 0)
		{
			line_list_.RemoveAt(line_list_.Count - 1);
		}
	}
}
