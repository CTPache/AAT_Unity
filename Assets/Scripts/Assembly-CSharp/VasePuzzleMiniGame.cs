using System.Collections.Generic;
using UnityEngine;

public class VasePuzzleMiniGame : MonoBehaviour
{
	private enum Proc
	{
		none = 0,
		init = 1,
		start = 2,
		free = 3,
		union_success = 4,
		union_failure = 5,
		rotate = 6,
		end = 7,
		exit = 8
	}

	[SerializeField]
	private PiecesStatus[] pieces_status_;

	private PiecesStatus[] pieces_noramal_ = new PiecesStatus[8]
	{
		new PiecesStatus("parts04", "itm05a7", 340f, 0f),
		new PiecesStatus("parts07", "itm05aa", 505f, 0.5f),
		new PiecesStatus("parts06", "itm05a9", 420f, 1f),
		new PiecesStatus("parts02", "itm05a5", 150f, 0f),
		new PiecesStatus("parts01", "itm05a4", 60f, 0f),
		new PiecesStatus("parts03", "itm05a6", 230f, 0f),
		new PiecesStatus("parts08", "itm05ab", 540f, 1f),
		new PiecesStatus("parts05", "itm05a8", 360f, 1f)
	};

	private PiecesStatus[] pieces_noramal_u_ = new PiecesStatus[8]
	{
		new PiecesStatus("parts04", "itm05a7u", 340f, 0f),
		new PiecesStatus("parts07", "itm05aau", 505f, 0.5f),
		new PiecesStatus("parts06", "itm05a9u", 420f, 1f),
		new PiecesStatus("parts02", "itm05a5u", 150f, 0f),
		new PiecesStatus("parts01", "itm05a4u", 60f, 0f),
		new PiecesStatus("parts03u", "itm05a6u", 230f, 0f),
		new PiecesStatus("parts08u", "itm05abu", 540f, 1f),
		new PiecesStatus("parts05", "itm05a8u", 360f, 1f)
	};

	private PiecesStatus[] pieces_noramal_k_ = new PiecesStatus[8]
	{
		new PiecesStatus("parts04", "itm05a7k", 340f, 0f),
		new PiecesStatus("parts07", "itm05aak", 505f, 0.5f),
		new PiecesStatus("parts06", "itm05a9k", 420f, 1f),
		new PiecesStatus("parts02", "itm05a5k", 150f, 0f),
		new PiecesStatus("parts01", "itm05a4k", 60f, 0f),
		new PiecesStatus("parts03", "itm05a6k", 230f, 0f),
		new PiecesStatus("parts08", "itm05abk", 540f, 1f),
		new PiecesStatus("parts05", "itm05a8k", 360f, 1f)
	};

	private PiecesStatus[] pieces_clear_ = new PiecesStatus[1]
	{
		new PiecesStatus("parts09", "itm05ac", 580f, 1f)
	};

	private PiecesStatus[] pieces_clear_u_ = new PiecesStatus[1]
	{
		new PiecesStatus("parts09u", "itm05acu", 580f, 1f)
	};

	private PiecesStatus[] pieces_clear_k_ = new PiecesStatus[1]
	{
		new PiecesStatus("parts09", "itm05ack", 580f, 1f)
	};

	[SerializeField]
	private bool force_clear_;

	[SerializeField]
	private bool SCE4_FLAG_JAR_PUZZZLE_on;

	[SerializeField]
	private bool rondom_rotate_off_;

	private Proc proc_id_;

	private bool clear_flag_;

	private int start_rotate_y_;

	private int icon_cursor_;

	private int puzzle_step_;

	private int restert_step_;

	private int local_proc_step_;

	private bool input_touch_step_next_flag_;

	private GameObject background_object_;

	private bool is_down_;

	private InputTouch input_;

	private Vector3 old_position_ = default(Vector3);

	private Vector3 down_position_ = default(Vector3);

	private const float PICE_FLICK_POWER = 10f;

	private Dictionary<string, PiecesStatus[]> pieces_noramal_map = new Dictionary<string, PiecesStatus[]>();

	private Dictionary<string, PiecesStatus[]> pieces_clear_map = new Dictionary<string, PiecesStatus[]>();

	public static VasePuzzleMiniGame instance { get; private set; }

	private void Awake()
	{
		instance = this;
		pieces_noramal_map.Add("JAPAN", pieces_noramal_);
		pieces_noramal_map.Add("USA", pieces_noramal_u_);
		pieces_noramal_map.Add("FRANCE", pieces_noramal_u_);
		pieces_noramal_map.Add("GERMAN", pieces_noramal_u_);
		pieces_noramal_map.Add("KOREA", pieces_noramal_k_);
		pieces_noramal_map.Add("CHINA_S", pieces_noramal_);
		pieces_noramal_map.Add("CHINA_T", pieces_noramal_);
		pieces_clear_map.Add("JAPAN", pieces_clear_);
		pieces_clear_map.Add("USA", pieces_clear_u_);
		pieces_clear_map.Add("FRANCE", pieces_clear_u_);
		pieces_clear_map.Add("GERMAN", pieces_clear_u_);
		pieces_clear_map.Add("KOREA", pieces_clear_k_);
		pieces_clear_map.Add("CHINA_S", pieces_clear_);
		pieces_clear_map.Add("CHINA_T", pieces_clear_);
	}

	public void startVasePuzzle(bool in_clear_flag)
	{
		TouchSystem.TouchInActive();
		clear_flag_ = in_clear_flag;
		if (SCE4_FLAG_JAR_PUZZZLE_on)
		{
			clear_flag_ = true;
		}
		if (!clear_flag_)
		{
			pieces_status_ = pieces_noramal_map[GSStatic.global_work_.language];
			restert_step_ = 0;
			start_rotate_y_ = 60;
		}
		else
		{
			pieces_status_ = pieces_clear_map[GSStatic.global_work_.language];
			restert_step_ = 8;
			start_rotate_y_ = -140;
		}
		VasePuzzleModelCtrl.instance.BaseRotate(0f);
		PiecesStatus[] array = pieces_status_;
		foreach (PiecesStatus piecesStatus in array)
		{
			piecesStatus.reset();
			if (!rondom_rotate_off_)
			{
				piecesStatus.angle_id = Random.Range(0, 4);
			}
		}
		debugLogger.instance.Log("Puzzle", "startVasePuzzle.");
		_chengeStaete(Proc.init);
		base.enabled = true;
		_create_vase_piece_assemble_area();
	}

	public void SetCursorIndex(int i)
	{
		if (proc_id_ == Proc.free && local_proc_step_ == 1 && !pieces_status_[i].used)
		{
			if (icon_cursor_ == i)
			{
				input_touch_step_next_flag_ = true;
				soundCtrl.instance.PlaySE(52);
			}
			else
			{
				soundCtrl.instance.PlaySE(54);
			}
			icon_cursor_ = i;
			VasePuzzleIconCtrl.instance.setIconStatus(pieces_status_, icon_cursor_);
			VasePuzzleModelCtrl.instance.setPiecesStatus(pieces_status_, icon_cursor_);
		}
	}

	private void FixedUpdate()
	{
		Process();
	}

	private void Process()
	{
		switch (proc_id_)
		{
		case Proc.none:
			base.enabled = false;
			break;
		case Proc.init:
			_proc_init();
			break;
		case Proc.start:
			_proc_start();
			break;
		case Proc.free:
			_proc_free();
			break;
		case Proc.union_success:
			_proc_union_success();
			break;
		case Proc.union_failure:
			_proc_union_failure();
			break;
		case Proc.rotate:
			_proc_rotate();
			break;
		case Proc.end:
			_proc_end();
			break;
		case Proc.exit:
			_proc_exit();
			break;
		}
	}

	private void _chengeStaete(Proc in_proc_id)
	{
		debugLogger.instance.Log("Puzzle", string.Concat("_chengeStaete:", proc_id_, " > ", in_proc_id));
		proc_id_ = in_proc_id;
		local_proc_step_ = 0;
	}

	private void _proc_init()
	{
		switch (local_proc_step_)
		{
		case 0:
		{
			SubWindow sub_window_ = advCtrl.instance.sub_window_;
			sub_window_.stack_ = 1;
			sub_window_.GetCurrentRoutine().r.Set(26, 0, 0, 0);
			GSStatic.global_work_.r.Set(5, 10, 0, GSStatic.global_work_.r.no_3);
			fadeCtrl.instance.play(2u, 3u, 1u, 31u);
			local_proc_step_++;
			break;
		}
		case 1:
			if (fadeCtrl.instance.is_end)
			{
				AnimationSystem.Instance.StopCharacters();
				VasePuzzleIconCtrl.instance.init(pieces_status_);
				VasePuzzleModelCtrl.instance.init(pieces_status_);
				VasePuzzleModelCtrl.instance.BaseRotate(start_rotate_y_);
				background_object_ = VasePuzzleUtil.instance.createAssetBundle("/GS1/minigame/", "frame05", VasePuzzleUtil.SortOrder.Background);
				background_object_.transform.localPosition = new Vector3(0f, 0f, 0f);
				messageBoardCtrl.instance.board(false, false);
				coroutineCtrl.instance.Play(keyGuideCtrl.instance.open(keyGuideBase.Type.POT_PAZZLE));
				icon_cursor_ = 0;
				puzzle_step_ = 0;
				VasePuzzleIconCtrl.instance.setIconStatus(pieces_status_, -1);
				VasePuzzleModelCtrl.instance.setPiecesStatus(pieces_status_, -1);
				VasePuzzleModelCtrl.instance.setFlashing(false);
				VasePuzzleModelCtrl.instance.setVaseStatus(restert_step_ + puzzle_step_);
				local_proc_step_++;
			}
			break;
		case 2:
			fadeCtrl.instance.play(1u, 1u, 1u, 31u);
			local_proc_step_++;
			break;
		case 3:
			if (fadeCtrl.instance.is_end)
			{
				_chengeStaete(Proc.start);
			}
			break;
		}
		if (input_ != null)
		{
			input_.ActiveCollider();
			input_.SetEnableCollider(true);
		}
	}

	private void _proc_start()
	{
		switch (local_proc_step_)
		{
		case 0:
			VasePuzzleModelCtrl.instance.rotateVase(0.5f, 360, 6f);
			local_proc_step_++;
			break;
		case 1:
			if (VasePuzzleModelCtrl.instance.isRotateEnd())
			{
				_chengeStaete(Proc.free);
			}
			break;
		}
	}

	private void _proc_free()
	{
		switch (local_proc_step_)
		{
		case 0:
			VasePuzzleIconCtrl.instance.setIconStatus(pieces_status_, icon_cursor_);
			VasePuzzleModelCtrl.instance.setPiecesStatus(pieces_status_, icon_cursor_);
			VasePuzzleModelCtrl.instance.setFlashing(true);
			if (messageBoardCtrl.instance != null)
			{
				messageBoardCtrl.instance.InActiveNormalMessageNextTouch();
			}
			input_touch_step_next_flag_ = false;
			local_proc_step_++;
			break;
		case 1:
			if (padCtrl.instance.IsNextMove())
			{
				if (padCtrl.instance.GetKeyDown(KeyType.Left) || padCtrl.instance.GetKeyDown(KeyType.StickL_Left) || padCtrl.instance.GetWheelMoveUp())
				{
					debugLogger.instance.Log("Puzzle", "push KeyType.Left.");
					soundCtrl.instance.PlaySE(54);
					_prevCursor();
					VasePuzzleIconCtrl.instance.setIconStatus(pieces_status_, icon_cursor_);
					VasePuzzleModelCtrl.instance.setPiecesStatus(pieces_status_, icon_cursor_);
				}
				if (padCtrl.instance.GetKeyDown(KeyType.Right) || padCtrl.instance.GetKeyDown(KeyType.StickL_Right) || padCtrl.instance.GetWheelMoveDown())
				{
					debugLogger.instance.Log("Puzzle", "push KeyType.Right.");
					soundCtrl.instance.PlaySE(54);
					_nextCursor();
					VasePuzzleIconCtrl.instance.setIconStatus(pieces_status_, icon_cursor_);
					VasePuzzleModelCtrl.instance.setPiecesStatus(pieces_status_, icon_cursor_);
				}
			}
			padCtrl.instance.WheelMoveValUpdate();
			if (padCtrl.instance.GetKeyDown(KeyType.L))
			{
				debugLogger.instance.Log("Puzzle", "push KeyType.L.");
				pieces_status_[icon_cursor_].rotateLeft();
				VasePuzzleIconCtrl.instance.setIconStatus(pieces_status_, icon_cursor_);
				VasePuzzleModelCtrl.instance.setPiecesStatus(pieces_status_, icon_cursor_);
				soundCtrl.instance.PlaySE(429);
			}
			if (padCtrl.instance.GetKeyDown(KeyType.Record, 2, true, KeyType.R))
			{
				debugLogger.instance.Log("Puzzle", "push KeyType.R.");
				pieces_status_[icon_cursor_].rotateRight();
				VasePuzzleIconCtrl.instance.setIconStatus(pieces_status_, icon_cursor_);
				VasePuzzleModelCtrl.instance.setPiecesStatus(pieces_status_, icon_cursor_);
				soundCtrl.instance.PlaySE(429);
			}
			if (padCtrl.instance.GetKeyDown(KeyType.X) || input_touch_step_next_flag_)
			{
				_assemble_vase();
			}
			if (padCtrl.instance.GetKeyDown(KeyType.B))
			{
				VasePuzzleIconCtrl.instance.setIconStatus(pieces_status_, -1);
				VasePuzzleModelCtrl.instance.setPiecesStatus(pieces_status_, -1);
				VasePuzzleModelCtrl.instance.setFlashing(false);
				_chengeStaete(Proc.exit);
			}
			if (padCtrl.instance.GetKeyDown(KeyType.A) && force_clear_)
			{
				debugLogger.instance.Log("Puzzle", "push KeyType.A ... debug skip!");
				_chengeStaete(Proc.end);
			}
			if (is_down_ && padCtrl.instance.InputGetMouseButtonUp(0))
			{
				if ((old_position_ - down_position_).x > 10f)
				{
					_assemble_vase();
				}
				is_down_ = false;
			}
			break;
		}
	}

	private void _assemble_vase()
	{
		debugLogger.instance.Log("Puzzle", "push KeyType.B.");
		if (pieces_status_[icon_cursor_].used)
		{
			debugLogger.instance.Log("Puzzle", "使用済みのアイコンを選択.");
			return;
		}
		soundCtrl.instance.PlaySE(52);
		VasePuzzleIconCtrl.instance.setIconStatus(pieces_status_, -1);
		VasePuzzleModelCtrl.instance.setFlashing(false);
		if (VasePuzzleUtil.instance.checkPuzzle(restert_step_ + puzzle_step_, icon_cursor_, pieces_status_[icon_cursor_].angle_id))
		{
			_chengeStaete(Proc.union_success);
		}
		else
		{
			_chengeStaete(Proc.union_failure);
		}
	}

	private void _proc_union_failure()
	{
		switch (local_proc_step_)
		{
		case 0:
			VasePuzzleModelCtrl.instance.UnionVase(icon_cursor_, false);
			local_proc_step_++;
			break;
		case 1:
			if (VasePuzzleModelCtrl.instance.isRotateEnd())
			{
				_chengeStaete(Proc.free);
			}
			break;
		}
	}

	private void _proc_union_success()
	{
		switch (local_proc_step_)
		{
		case 0:
			VasePuzzleModelCtrl.instance.UnionVase(icon_cursor_, true);
			pieces_status_[icon_cursor_].used = true;
			local_proc_step_++;
			break;
		case 1:
			if (VasePuzzleModelCtrl.instance.isRotateEnd())
			{
				_chengeStaete(Proc.rotate);
			}
			break;
		}
	}

	private void _proc_rotate()
	{
		switch (local_proc_step_)
		{
		case 0:
			puzzle_step_++;
			VasePuzzleModelCtrl.instance.setVaseStatus(restert_step_ + puzzle_step_);
			VasePuzzleIconCtrl.instance.setIconStatus(pieces_status_, -1);
			VasePuzzleModelCtrl.instance.setPiecesStatus(pieces_status_, -1);
			if (pieces_status_.Length <= puzzle_step_)
			{
				if ((uint)GSStatic.global_work_.scenario >= 28u)
				{
					VasePuzzleModelCtrl.instance.rotateVase(2f, -40, -2f);
				}
				else
				{
					VasePuzzleModelCtrl.instance.rotateVase(2f, 65, 2f);
				}
			}
			else
			{
				VasePuzzleModelCtrl.instance.rotateVase(1f, VasePuzzleUtil.instance.GetVaseRotateY(restert_step_ + puzzle_step_), 1f);
			}
			local_proc_step_++;
			break;
		case 1:
			if (!VasePuzzleModelCtrl.instance.isRotateEnd())
			{
				break;
			}
			if (pieces_status_.Length <= puzzle_step_)
			{
				if ((uint)GSStatic.global_work_.scenario >= 28u)
				{
					soundCtrl.instance.PlaySE(416);
				}
				_chengeStaete(Proc.end);
			}
			else
			{
				_nextCursor();
				_chengeStaete(Proc.free);
			}
			break;
		}
	}

	private void _proc_end()
	{
		switch (local_proc_step_)
		{
		default:
			local_proc_step_++;
			break;
		case 60:
			coroutineCtrl.instance.Play(keyGuideCtrl.instance.open(keyGuideBase.Type.NO_GUIDE));
			VasePuzzleIconCtrl.instance.exit();
			if (!clear_flag_)
			{
				debugLogger.instance.Log("Puzzle", "壷パズル初クリア.");
				advCtrl.instance.message_system_.SetMessage(scenario.SC4_64450);
			}
			else
			{
				debugLogger.instance.Log("Puzzle", "クリア二回目以降.");
				advCtrl.instance.message_system_.SetMessage(scenario.SC4_67760);
			}
			GSFlag.Set(0u, scenario.SCE4_FLAG_JAR_PUZZZLE, 1u);
			local_proc_step_++;
			break;
		case 61:
			break;
		}
		if (input_ != null)
		{
			input_.SetEnableCollider(false);
		}
	}

	public void DeleteEndBackGround()
	{
		if (proc_id_ != 0 && background_object_ != null)
		{
			SubWindow sub_window_ = advCtrl.instance.sub_window_;
			sub_window_.stack_ = 1;
			sub_window_.GetCurrentRoutine().r.Set(10, 0, 0, 0);
			GSStatic.global_work_.r.Set(5, 1, 0, 0);
			VasePuzzleModelCtrl.instance.exit();
			Object.Destroy(background_object_);
			background_object_ = null;
			_chengeStaete(Proc.none);
		}
	}

	private void _proc_exit()
	{
		switch (local_proc_step_)
		{
		case 0:
			coroutineCtrl.instance.Play(keyGuideCtrl.instance.open(keyGuideBase.Type.NO_GUIDE));
			fadeCtrl.instance.play(2u, 1u, 1u, 31u);
			local_proc_step_++;
			break;
		case 1:
			if (fadeCtrl.instance.is_end)
			{
				VasePuzzleModelCtrl.instance.exit();
				VasePuzzleIconCtrl.instance.exit();
				Object.Destroy(background_object_);
				MessageSystem.GetActiveMessageWork().message_trans_flag = 1;
				GSStatic.global_work_.Mess_move_flag = 0;
				if (!clear_flag_)
				{
					debugLogger.instance.Log("Puzzle", "壷パズル初クリア.");
					advCtrl.instance.message_system_.SetMessage(scenario.SC4_64440);
				}
				else
				{
					debugLogger.instance.Log("Puzzle", "クリア二回目以降.");
					advCtrl.instance.message_system_.SetMessage(scenario.SC4_67755);
				}
				local_proc_step_++;
			}
			break;
		case 2:
			fadeCtrl.instance.play(1u, 1u, 1u, 31u);
			local_proc_step_++;
			break;
		case 3:
			if (fadeCtrl.instance.is_end)
			{
				GSStatic.global_work_.Mess_move_flag = 1;
				SubWindow sub_window_ = advCtrl.instance.sub_window_;
				sub_window_.stack_ = 0;
				sub_window_.GetCurrentRoutine().r.Set(5, 0, 0, 0);
				GSStatic.global_work_.r.Set(5, 1, 0, 0);
				_chengeStaete(Proc.none);
			}
			break;
		}
		if (input_ != null)
		{
			input_.SetEnableCollider(false);
		}
	}

	private void _prevCursor()
	{
		int num = icon_cursor_;
		do
		{
			icon_cursor_--;
			if (icon_cursor_ < 0)
			{
				icon_cursor_ = pieces_status_.Length - 1;
			}
		}
		while (num != icon_cursor_ && pieces_status_[icon_cursor_].used);
	}

	private void _nextCursor()
	{
		int num = icon_cursor_;
		do
		{
			icon_cursor_++;
			if (pieces_status_.Length <= icon_cursor_)
			{
				icon_cursor_ = 0;
			}
		}
		while (num != icon_cursor_ && pieces_status_[icon_cursor_].used);
	}

	private void _create_vase_piece_assemble_area()
	{
		if (!(input_ != null))
		{
			base.gameObject.AddComponent<BoxCollider2D>();
			input_ = base.gameObject.AddComponent<InputTouch>();
			input_.SetColliderSize(new Vector2(500f, 500f));
			input_.SetColliderOffset(new Vector2(500f, 250f));
			input_.down_event = delegate
			{
				is_down_ = true;
				down_position_ = TouchUtility.GetTouchPosition();
				old_position_ = down_position_;
				input_.touch_key_type = KeyType.None;
			};
			input_.drag_event = delegate
			{
				old_position_ = down_position_;
				down_position_ = TouchUtility.GetTouchPosition();
			};
			input_.up_event = delegate
			{
				Vector3 vector = old_position_ - down_position_;
				input_.touch_key_type = ((!(vector.x > 10f)) ? KeyType.Record : KeyType.None);
			};
			input_.touch_key_type = KeyType.None;
			base.gameObject.layer = 8;
		}
	}
}
