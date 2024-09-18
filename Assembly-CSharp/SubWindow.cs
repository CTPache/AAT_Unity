using System;
using UnityEngine;

[Serializable]
public class SubWindow
{
	public enum State
	{
		IDLE = 0,
		TITLE = 1,
		HOUTEI = 2,
		SELECT = 3,
		QUESTIONING = 4,
		TANTEI = 5,
		INSPECT = 6,
		MOVE = 7,
		TALK = 8,
		ATTACK = 9,
		KAIWA = 10,
		STATUS = 11,
		STATUS_DETAIL = 12,
		STATUS_3D = 13,
		POINT = 14,
		SAVE = 15,
		HUMAN = 16,
		HUMAN_DETAIL = 17,
		GBA_CARTRIDGE = 18,
		STAFF = 19,
		BLANK = 20,
		FORMAT = 21,
		TANCHIKI = 22,
		LUMINOL = 23,
		FINGER = 24,
		DIE_MES = 25,
		VASE_PUZZLE = 26,
		VASE_SHOW = 27,
		SAFE_CRACKING = 28,
		MOVIE_THRUST = 29,
		OPTION = 30,
		MAX = 31
	}

	public enum Req
	{
		NONE = 0,
		IDLE = 1,
		TITLE = 2,
		TITLE_START = 3,
		TITLE_EXIT = 4,
		HOUTEI = 5,
		HOUTEI_EXIT = 6,
		SELECT = 7,
		SELECT_EXIT = 8,
		QUESTIONING_FIRST = 9,
		QUESTIONING = 10,
		QUESTIONING_EXIT = 11,
		QUESTIONING_YUSABURU = 12,
		TANTEI = 13,
		TANTEI_EXIT = 14,
		INSPECT = 15,
		MOVE = 16,
		TALK = 17,
		ATTACK = 18,
		INSPECT_EXIT = 19,
		MOVE_EXIT = 20,
		MOVE_GO = 21,
		TALK_EXIT = 22,
		ATTACK_EXIT = 23,
		STATUS = 24,
		STATUS_EXIT = 25,
		STATUS_SETU = 26,
		STATUS_TUKITUKERU = 27,
		STATUS_3D_EXIT = 28,
		POINT = 29,
		POINT_EXIT = 30,
		STATUS_SETU_EXIT = 31,
		RT_TMAIN = 32,
		MESS_EXIT = 33,
		SAVE = 34,
		SAVE_EXIT = 35,
		SAVE_EXIT2 = 36,
		SAVE_DECIDE = 37,
		LUMINOL = 38,
		LUMINOL_EXIT = 39,
		LUMINOL_ANM = 40,
		FINGER = 41,
		FINGER_EXIT = 42,
		JUDGMENT = 43,
		JUDGMENT_EXIT = 44,
		OPENING = 45,
		STATUS_3D = 46,
		HUMAN = 47,
		_3D_POINT = 48,
		EPISODE_CLEAR = 49,
		VASE_PUZZLE = 50,
		VASE_SHOW = 51,
		SAFE_CRACKING = 52,
		MOVIE_THRUST = 53,
		SAVE_EXIT3 = 54,
		FINGER_EFF0 = 55,
		FINGER_EFF1 = 56,
		FINGER_EFF2 = 57,
		FINGER_EFF3 = 58,
		FINGER_EFF4 = 59,
		FINGER_EFF5 = 60,
		FINGER_EFF6 = 61,
		DIE_MES = 62,
		LUMINOL_SCENARIO = 63,
		STAFF_MASK_SET = 64,
		STAFF_FORCE_OUT = 65,
		BLANK = 66,
		FORMAT = 67,
		FORMAT_EXIT = 68,
		FORMAT_DECIDE = 69,
		QUESTIONING_PREV = 70,
		QUESTIONING_NEXT = 71,
		GBA_STAFF = 72,
		LAST_OBJECTION = 73,
		MAGATAMA_TUKITUKE = 74,
		MAGATAMA_TUKITUKE2 = 75,
		MAGATAMA_MENU_ON = 76,
		POINT_TANTEI_EXIT = 77,
		MAX = 78
	}

	public enum BarReq
	{
		NONE = 0,
		NONE_D = 1,
		IDLE = 2,
		IDLE_D = 3,
		PLANE = 4,
		PLANE_D = 5,
		MESS = 6,
		MESS_D = 7,
		QUESTIONING = 8,
		QUESTIONING_D = 9,
		QUESTIONING_2 = 10,
		QUESTIONING_2_D = 11,
		QUEST_EFFECT = 12,
		QUEST_EFFECT_D = 13,
		STATUS = 14,
		STATUS_D = 15,
		STATUS_DETAIL_0 = 16,
		STATUS_DETAIL_0_D = 17,
		STATUS_DETAIL_1 = 18,
		STATUS_DETAIL_1_D = 19,
		STATUS_ADD = 20,
		STATUS_ADD_D = 21,
		STATUS_FORCE = 22,
		STATUS_FORCE_D = 23,
		THRUST_0 = 24,
		THRUST_0_D = 25,
		THRUST_1 = 26,
		THRUST_1_D = 27,
		THRUST_2 = 28,
		THRUST_2_D = 29,
		THRUST_3 = 30,
		THRUST_3_D = 31,
		STATUS_SPECIAL_0 = 32,
		STATUS_SPECIAL_0_D = 33,
		STATUS_SPECIAL_1 = 34,
		STATUS_SPECIAL_1_D = 35,
		INSPECT_0 = 36,
		INSPECT_0_D = 37,
		INSPECT_0_LR = 38,
		INSPECT_0_LR_D = 39,
		INSPECT_1 = 40,
		INSPECT_1_D = 41,
		INSPECT_1_LR = 42,
		INSPECT_1_LR_D = 43,
		STATUS_3D = 44,
		STATUS_3D_D = 45,
		STATUS_3D_1 = 46,
		STATUS_3D_D_1 = 47,
		TUTORIAL_3D = 48,
		TUTORIAL_3D_D = 49,
		TUTORIAL_3D_1 = 50,
		TUTORIAL_3D_D_1 = 51,
		_3D_POINT = 52,
		_3D_POINT_D = 53,
		POINT_0 = 54,
		POINT_0_D = 55,
		POINT_1 = 56,
		POINT_1_D = 57,
		TALK = 58,
		TALK_D = 59,
		TANTEI = 60,
		TANTEI_D = 61,
		TANTEI_S = 62,
		TANTEI_S_D = 63,
		SELECT = 64,
		SELECT_D = 65,
		LUMINOL = 66,
		LUMINOL_D = 67,
		FINGER_SELECT_0 = 68,
		FINGER_SELECT_0_D = 69,
		FINGER_SELECT_1 = 70,
		FINGER_SELECT_1_D = 71,
		FINGER_SELECT_2 = 72,
		FINGER_SELECT_2_D = 73,
		FINGER_SELECT_3 = 74,
		FINGER_SELECT_3_D = 75,
		FINGER_MAIN = 76,
		FINGER_MAIN_D = 77,
		FINGER_MAN_SELECT = 78,
		FINGER_MAN_SELECT_D = 79,
		FINGER_MAN_SELECT_DETAIL = 80,
		FINGER_MAN_SELECT_DETAIL_D = 81,
		SHOW_POT = 82,
		SHOW_POT_D = 83,
		MAKE_POT = 84,
		MAKE_POT_D = 85,
		MOVIE_THRUST = 86,
		MOVIE_THRUST_D = 87,
		DIE_MES = 88,
		DIE_MES_D = 89,
		LAST_OBJECTION = 90,
		LAST_OBJECTION_D = 91,
		TANCHIKI_LR = 92,
		TANCHIKI_LR_D = 93,
		MAX = 94,
		HOUTEI = 60,
		EFF000 = 60,
		TANTEI_1 = 4,
		TANTEI_2 = 58,
		TANTEI_3 = 58,
		TANTEI_4 = 24,
		KAIWA = 6
	}

	public enum BtnReq
	{
		IDLE = 0,
		TITLE = 1,
		HOUTEI = 2,
		SELECT = 3,
		QUESTIONING = 4,
		TANTEI = 5,
		INSPECT = 6,
		MOVE = 7,
		TALK = 8,
		ATTACK = 9,
		KAIWA = 10,
		STATUS = 11,
		STATUS_DETAIL = 12,
		STATUS_3D = 13,
		POINT = 14,
		SAVE = 15,
		LUMINOL = 16,
		FINGER = 17,
		FINGER_SELECT = 18,
		EPISODE = 19,
		CONTINUE = 20,
		QUESTIONING_VOICE = 21,
		ATTACK_VOICE_0 = 22,
		ATTACK_VOICE_1 = 23,
		HUMAN = 24,
		_3D_POINT = 25,
		VASE_PUZZLE = 26,
		VASE_SHOW = 27,
		NOTE_SPECIAL = 28,
		NOTE_SPECIAL_2 = 29,
		MOVIE_THRUST_PLAY = 30,
		MOVIE_THRUST_STOP = 31,
		HUMAN2 = 32,
		DIE_MES = 33,
		LAST_OBJECTION = 34,
		ATTACK_PSY = 35,
		TANCHIKI = 36,
		DISP_OFF = 37
	}

	[Flags]
	public enum ObjFlag
	{
		DISP = 1,
		DISP_PREV = 2,
		OUT = 4,
		IN = 8
	}

	public enum ObjId
	{
		BTN01 = 0,
		BTN02 = 1,
		BTN03 = 2,
		BTN04 = 3,
		BTN04_2 = 4,
		BTN05 = 5,
		BTN06 = 6,
		BTN07 = 7,
		BTN08 = 8,
		BTN09 = 9,
		BTN0A = 10,
		BTN0B = 11,
		BTN0C = 12,
		BTN0D = 13,
		BTN0E = 14,
		BTN0E_2 = 15,
		BTN0F = 16,
		BTN10 = 17,
		BTN11 = 18,
		BTN12 = 19,
		BTN13 = 20,
		BTN14 = 21,
		BTN15 = 22,
		BTN18 = 23,
		BTN1E = 24,
		BTN1F = 25,
		BTN21 = 26,
		BTN22 = 27,
		BTN25 = 28,
		BTN26 = 29,
		BTN23 = 30,
		BTN24 = 31,
		BTN28 = 32,
		BTN29 = 33,
		BTN2A = 34,
		BTN2B = 35,
		BTN2C = 36,
		S2D002 = 37,
		S2D003 = 38,
		S2D011 = 39,
		S2D01F = 40,
		S2D00C = 41,
		S2D020 = 42,
		S2D004_0 = 43,
		S2D004_1 = 44,
		S2D004_2 = 45,
		S2D00D = 46,
		S2D013_0 = 47,
		S2D013_1 = 48,
		S2D014 = 49,
		S2D015 = 50,
		S2D016 = 51,
		S2D017 = 52,
		S2D017_1 = 53,
		S2D018 = 54,
		S2D019 = 55,
		S2D01A = 56,
		S2D01A_1 = 57,
		S2D01B_0 = 58,
		S2D01B_1 = 59,
		S2D021 = 60,
		S2D022 = 61,
		S2D023 = 62,
		S2D024 = 63,
		S2D025 = 64,
		S2D026 = 65,
		S2D027 = 66,
		S2D028 = 67,
		S2D02A = 68,
		BTN2D = 69,
		BTN2E = 70,
		INDICATOR_K_ = 71,
		INDICATOR_K = 72,
		INDICATOR_C2 = 73,
		INDICATOR_E = 74,
		INDICATOR_H = 75,
		INDICATOR_C = 76,
		INDICATOR = 77,
		BTN40 = 78,
		BTN41 = 79,
		MAX = 80
	}

	public enum ObjMove
	{
		U = 0,
		D = 1,
		L = 2,
		R = 3,
		NONE = 4
	}

	private delegate void Proc(SubWindow sub_window);

	public const int TEX_COM_BASE = 4;

	private const int BAR_MOVE_X = 4;

	private const int BAR_MOVE_Y = 4;

	private const int BTN_MOVE_X = 4;

	private const int BTN_MOVE_Y = 4;

	private const int MENU_OFF_Y = -1;

	public const int SRTATUS_SROLL_SPEED = 8;

	public Routine[] routine_ = new Routine[8];

	public Routine scan_ = new Routine();

	public ObjRoutine[] sprite_routine_ = new ObjRoutine[96];

	public SubWindowNote note_ = new SubWindowNote();

	public InspectBG inspect_ = new InspectBG();

	public SubWindowCursor cursor_ = new SubWindowCursor();

	[SerializeField]
	private Req req;

	[SerializeField]
	private byte stack;

	public BarReq bar_req_;

	public byte cursor_change_;

	[SerializeField]
	private uint busy;

	public ObjFlag[] obj_flag_ = new ObjFlag[96];

	public uint sw_bg_no_;

	public byte tantei_tukituke_;

	public byte tutorial_;

	public byte status_force_;

	public byte point_3d_;

	public sbyte sw_expl_rtn_no_;

	public short sw_expl_pos_;

	public byte bg_return_;

	public byte sprite_set_state_;

	public byte real_fast_mes_;

	public byte note_add_;

	private static readonly Proc[] sub_window_proc_table;

	private static readonly SubWindowObjInfo[] obj_info_table;

	public Req req_
	{
		get
		{
			return req;
		}
		set
		{
			req = value;
		}
	}

	public byte stack_
	{
		get
		{
			return stack;
		}
		set
		{
			stack = value;
		}
	}

	public uint busy_
	{
		get
		{
			return busy;
		}
		set
		{
			busy = value;
		}
	}

	static SubWindow()
	{
		sub_window_proc_table = new Proc[31]
		{
			Proc_Idle,
			SubWindow_Title.Proc,
			SubWindow_Houtei.Proc,
			SubWindow_Select.Proc,
			SubWindow_Questioning.Proc,
			SubWindow_Tantei.Proc,
			SubWindow_Inspect.Proc,
			SubWindow_Move.Proc,
			SubWindow_Talk.Proc,
			Proc_Attack,
			SubWindow_Kaiwa.Proc,
			SubWindow_Status.Proc,
			SubWindow_Detail.Proc,
			SubWindow_Info.Proc,
			Proc_Point,
			Proc_Save,
			SubWindow_Human.Proc,
			SubWindow_HumanDetail.Proc,
			Proc_Dummy,
			Proc_Staffroll,
			Proc_Blank,
			Proc_Format,
			SubWindow_Tanchiki.Proc,
			SubWindow_Luminol.Proc,
			Proc_Finger,
			Proc_DieMes,
			Proc_VasePuzzle,
			Proc_VaseShow,
			Proc_Safecracking,
			ConfrontWithMovie.Proc,
			Proc_Option
		};
		obj_info_table = new SubWindowObjInfo[80]
		{
			new SubWindowObjInfo(SpriteDataTable.MenuSpriteNo.BTN01, 2, 88, 160, new GSRect(88, 172, 80, 20), ObjMove.U, 4, 6, GSTouch.ButtonB, GSTouch.Check.ON, 2),
			new SubWindowObjInfo(SpriteDataTable.MenuSpriteNo.BTN02, 2, 176, 160, new GSRect(176, 172, 80, 20), ObjMove.U, 4, 8, GSTouch.ButtonA, GSTouch.Check.CLICK, 1),
			new SubWindowObjInfo(SpriteDataTable.MenuSpriteNo.BTN03, 2, 0, 160, new GSRect(0, 162, 80, 30), ObjMove.U, 4, 8, TouchBackButton, GSTouch.Check.CLICK, 2),
			new SubWindowObjInfo(SpriteDataTable.MenuSpriteNo.BTN04, 2, 88, 0, new GSRect(88, 0, 80, 20), ObjMove.D, 4, 8, GSTouch.ButtonX, GSTouch.Check.CLICK, 1024),
			new SubWindowObjInfo(SpriteDataTable.MenuSpriteNo.BTN04, 2, 88, 0, new GSRect(88, 0, 80, 20), ObjMove.D, 4, 8, GSTouch.ButtonX, GSTouch.Check.CLICK, 1024),
			new SubWindowObjInfo(SpriteDataTable.MenuSpriteNo.BTN05, 2, 176, 0, new GSRect(176, 0, 80, 30), ObjMove.D, 4, 8, GSTouch.ButtonR, GSTouch.Check.CLICK, 256),
			new SubWindowObjInfo(SpriteDataTable.MenuSpriteNo.BTN06, 2, 176, 0, new GSRect(176, 0, 80, 20), ObjMove.D, 4, 6, GSTouch.ButtonR, GSTouch.Check.CLICK, 256),
			new SubWindowObjInfo(SpriteDataTable.MenuSpriteNo.BTN07, 2, 176, 0, new GSRect(200, 0, 56, 30), ObjMove.D, 4, 8, GSTouch.ButtonR, GSTouch.Check.CLICK, 256),
			new SubWindowObjInfo(SpriteDataTable.MenuSpriteNo.BTN08, 2, 104, 160, new GSRect(104, 172, 48, 20), ObjMove.U, 4, 5, GSTouch.ButtonL, GSTouch.Check.CLICK, 512),
			new SubWindowObjInfo(SpriteDataTable.MenuSpriteNo.BTN09, 2, 192, 160, new GSRect(192, 172, 64, 20), ObjMove.U, 4, 6, GSTouch.ButtonA, GSTouch.Check.ON, 1),
			new SubWindowObjInfo(SpriteDataTable.MenuSpriteNo.BTN0A, 2, 104, 160, new GSRect(104, 172, 48, 20), ObjMove.U, 4, 5, GSTouch.ButtonL, GSTouch.Check.CLICK, 512),
			new SubWindowObjInfo(SpriteDataTable.MenuSpriteNo.BTN0B, 2, 0, 160, new GSRect(0, 172, 64, 20), ObjMove.U, 4, 6, GSTouch.ButtonY, GSTouch.Check.ON, 2048),
			new SubWindowObjInfo(SpriteDataTable.MenuSpriteNo.BTN0C, 2, 176, 160, new GSRect(176, 162, 80, 30), ObjMove.U, 4, 5, GSTouch.ButtonA, GSTouch.Check.CLICK, 1),
			new SubWindowObjInfo(SpriteDataTable.MenuSpriteNo.BTN0D, 2, 176, 160, new GSRect(176, 172, 80, 20), ObjMove.U, 4, 5, GSTouch.ButtonA, GSTouch.Check.CLICK, 1),
			new SubWindowObjInfo(SpriteDataTable.MenuSpriteNo.BTN0E, 2, 88, 0, new GSRect(88, 0, 80, 30), ObjMove.D, 4, 8, GSTouch.ButtonX, GSTouch.Check.CLICK, 1024),
			new SubWindowObjInfo(SpriteDataTable.MenuSpriteNo.BTN0E, 2, 88, 0, new GSRect(88, 0, 80, 24), ObjMove.D, 4, 8, GSTouch.ButtonX, GSTouch.Check.CLICK, 1024),
			new SubWindowObjInfo(SpriteDataTable.MenuSpriteNo.BTN0F, 2, 176, 0, new GSRect(176, 0, 80, 30), ObjMove.D, 4, 8, GSTouch.ButtonR, GSTouch.Check.CLICK, 256),
			new SubWindowObjInfo(SpriteDataTable.MenuSpriteNo.BTN10, 2, 0, 0, new GSRect(0, 0, 80, 30), ObjMove.D, 4, 8, GSTouch.ButtonL, GSTouch.Check.CLICK, 512),
			new SubWindowObjInfo(SpriteDataTable.MenuSpriteNo.BTN11, 2, 88, 0, new GSRect(88, 0, 80, 30), ObjMove.D, 4, 8, GSTouch.ButtonX, GSTouch.Check.CLICK, 1024),
			new SubWindowObjInfo(SpriteDataTable.MenuSpriteNo.BTN12, 2, 88, 160, new GSRect(88, 172, 80, 20), ObjMove.U, 4, 6, GSTouch.ButtonB, GSTouch.Check.ON, 2),
			new SubWindowObjInfo(SpriteDataTable.MenuSpriteNo.BTN13, 2, 176, 0, new GSRect(200, 0, 56, 30), ObjMove.D, 4, 8, GSTouch.ButtonR, GSTouch.Check.CLICK, 256),
			new SubWindowObjInfo(SpriteDataTable.MenuSpriteNo.BTN14, 2, 0, 160, new GSRect(0, 172, 80, 20), ObjMove.U, 4, 8, TouchBackButton, GSTouch.Check.CLICK, 2),
			new SubWindowObjInfo(SpriteDataTable.MenuSpriteNo.BTN15, 2, 88, 0, new GSRect(88, 0, 80, 30), ObjMove.D, 4, 8, GSTouch.ButtonX, GSTouch.Check.CLICK, 1024),
			new SubWindowObjInfo(SpriteDataTable.MenuSpriteNo.BTN18, 2, 176, 160, new GSRect(176, 160, 80, 30), ObjMove.U, 4, 8, GSTouch.ButtonA, GSTouch.Check.CLICK, 1),
			new SubWindowObjInfo(SpriteDataTable.MenuSpriteNo.BTN1E, 2, 43, 46, new GSRect(43, 46, 21, 37), ObjMove.U, 4, 0, null, GSTouch.Check.ON, 256),
			new SubWindowObjInfo(SpriteDataTable.MenuSpriteNo.BTN1F, 2, 43, 85, new GSRect(43, 85, 21, 37), ObjMove.U, 4, 0, null, GSTouch.Check.ON, 512),
			new SubWindowObjInfo(SpriteDataTable.MenuSpriteNo.BTN21, 2, 43, 76, new GSRect(43, 76, 32, 40), ObjMove.U, 4, 0, GSTouch.ButtonL, GSTouch.Check.CLICK, 512),
			new SubWindowObjInfo(SpriteDataTable.MenuSpriteNo.BTN22, 2, 189, 76, new GSRect(189, 76, 32, 40), ObjMove.U, 4, 0, GSTouch.ButtonR, GSTouch.Check.CLICK, 256),
			new SubWindowObjInfo(SpriteDataTable.MenuSpriteNo.BTN25, 2, 0, 92, new GSRect(0, 92, 16, 16), ObjMove.U, 4, 0, null, GSTouch.Check.CLICK, 0),
			new SubWindowObjInfo(SpriteDataTable.MenuSpriteNo.BTN26, 2, 240, 92, new GSRect(240, 92, 16, 16), ObjMove.U, 4, 0, null, GSTouch.Check.CLICK, 0),
			new SubWindowObjInfo(SpriteDataTable.MenuSpriteNo.BTN23, 2, 0, 52, new GSRect(0, 52, 16, 96), ObjMove.U, 4, 0, GSTouch.KeyLeft, GSTouch.Check.ON, 16),
			new SubWindowObjInfo(SpriteDataTable.MenuSpriteNo.BTN24, 2, 240, 52, new GSRect(240, 52, 16, 96), ObjMove.U, 4, 0, GSTouch.KeyRight, GSTouch.Check.ON, 32),
			new SubWindowObjInfo(SpriteDataTable.MenuSpriteNo.BTN28, 2, 88, 0, new GSRect(88, 0, 80, 30), ObjMove.D, 4, 8, GSTouch.ButtonX, GSTouch.Check.CLICK, 1024),
			new SubWindowObjInfo(SpriteDataTable.MenuSpriteNo.BTN29, 2, 176, 160, new GSRect(176, 172, 80, 30), ObjMove.U, 4, 8, GSTouch.ButtonA, GSTouch.Check.CLICK, 1),
			new SubWindowObjInfo(SpriteDataTable.MenuSpriteNo.BTN2A, 2, 176, 160, new GSRect(176, 160, 80, 30), ObjMove.U, 4, 8, TouchBackButton, GSTouch.Check.CLICK, 2),
			new SubWindowObjInfo(SpriteDataTable.MenuSpriteNo.BTN2B, 2, 192, 160, new GSRect(192, 172, 64, 20), ObjMove.U, 4, 6, GSTouch.ButtonA, GSTouch.Check.ON, 1),
			new SubWindowObjInfo(SpriteDataTable.MenuSpriteNo.BTN2C, 2, 0, 160, new GSRect(0, 172, 64, 20), ObjMove.U, 4, 6, GSTouch.ButtonY, GSTouch.Check.ON, 2048),
			new SubWindowObjInfo(SpriteDataTable.MenuSpriteNo.S2D002, 6, 0, -1, new GSRect(0, 0, 32, 32), ObjMove.D, 4, 8, null, GSTouch.Check.CLICK, 0),
			new SubWindowObjInfo(SpriteDataTable.MenuSpriteNo.S2D003, 6, 0, -1, new GSRect(0, 0, 32, 32), ObjMove.D, 4, 8, null, GSTouch.Check.CLICK, 0),
			new SubWindowObjInfo(SpriteDataTable.MenuSpriteNo.S2D011, 6, 0, -1, new GSRect(0, 0, 32, 32), ObjMove.D, 4, 8, null, GSTouch.Check.CLICK, 0),
			new SubWindowObjInfo(SpriteDataTable.MenuSpriteNo.S2D01F, 6, 32, -1, new GSRect(32, 0, 32, 32), ObjMove.D, 4, 8, null, GSTouch.Check.CLICK, 0),
			new SubWindowObjInfo(SpriteDataTable.MenuSpriteNo.S2D00C, 6, 32, -1, new GSRect(32, 0, 32, 32), ObjMove.D, 4, 8, null, GSTouch.Check.CLICK, 0),
			new SubWindowObjInfo(SpriteDataTable.MenuSpriteNo.S2D020, 6, 32, -1, new GSRect(32, 0, 32, 32), ObjMove.D, 4, 8, null, GSTouch.Check.CLICK, 0),
			new SubWindowObjInfo(SpriteDataTable.MenuSpriteNo.S2D004, 6, 80, 19, new GSRect(80, 16, 32, 16), ObjMove.D, 5, 8, null, GSTouch.Check.CLICK, 0),
			new SubWindowObjInfo(SpriteDataTable.MenuSpriteNo.S2D004, 6, 144, 19, new GSRect(144, 16, 32, 16), ObjMove.D, 5, 8, null, GSTouch.Check.CLICK, 0),
			new SubWindowObjInfo(SpriteDataTable.MenuSpriteNo.S2D004, 6, 208, 19, new GSRect(208, 16, 32, 16), ObjMove.D, 5, 8, null, GSTouch.Check.CLICK, 0),
			new SubWindowObjInfo(SpriteDataTable.MenuSpriteNo.S2D00D, 6, 64, -1, new GSRect(64, 0, 16, 32), ObjMove.D, 4, 8, null, GSTouch.Check.CLICK, 0),
			new SubWindowObjInfo(SpriteDataTable.MenuSpriteNo.S2D013, 8, 80, 0, new GSRect(80, 0, 32, 16), ObjMove.D, 4, 0, null, GSTouch.Check.CLICK, 0),
			new SubWindowObjInfo(SpriteDataTable.MenuSpriteNo.S2D013, 9, 168, 0, new GSRect(168, 0, 32, 16), ObjMove.D, 4, 0, null, GSTouch.Check.CLICK, 0),
			new SubWindowObjInfo(SpriteDataTable.MenuSpriteNo.S2D014, 8, 0, 160, new GSRect(0, 160, 80, 32), ObjMove.U, 4, 8, null, GSTouch.Check.CLICK, 0),
			new SubWindowObjInfo(SpriteDataTable.MenuSpriteNo.S2D015, 8, 176, 160, new GSRect(176, 160, 80, 32), ObjMove.U, 4, 8, null, GSTouch.Check.CLICK, 0),
			new SubWindowObjInfo(SpriteDataTable.MenuSpriteNo.S2D016, 9, 0, 0, new GSRect(0, 0, 80, 32), ObjMove.D, 4, 0, null, GSTouch.Check.CLICK, 0),
			new SubWindowObjInfo(SpriteDataTable.MenuSpriteNo.S2D017, 10, 88, 0, new GSRect(88, 0, 80, 32), ObjMove.D, 4, 0, null, GSTouch.Check.CLICK, 0),
			new SubWindowObjInfo(SpriteDataTable.MenuSpriteNo.S2D017, 10, 88, 0, new GSRect(88, 0, 80, 32), ObjMove.D, 4, 8, null, GSTouch.Check.CLICK, 0),
			new SubWindowObjInfo(SpriteDataTable.MenuSpriteNo.S2D018, 10, 88, 0, new GSRect(88, 0, 80, 32), ObjMove.D, 4, 0, null, GSTouch.Check.CLICK, 0),
			new SubWindowObjInfo(SpriteDataTable.MenuSpriteNo.S2D019, 9, 0, 0, new GSRect(0, 0, 80, 32), ObjMove.D, 4, 0, null, GSTouch.Check.CLICK, 0),
			new SubWindowObjInfo(SpriteDataTable.MenuSpriteNo.S2D01A, 10, 88, 0, new GSRect(88, 0, 80, 32), ObjMove.D, 4, 0, null, GSTouch.Check.CLICK, 0),
			new SubWindowObjInfo(SpriteDataTable.MenuSpriteNo.S2D01A, 10, 88, 0, new GSRect(88, 0, 80, 32), ObjMove.D, 4, 8, null, GSTouch.Check.CLICK, 0),
			new SubWindowObjInfo(SpriteDataTable.MenuSpriteNo.S2D01B, 9, 80, 0, new GSRect(80, 0, 32, 16), ObjMove.D, 4, 0, null, GSTouch.Check.CLICK, 0),
			new SubWindowObjInfo(SpriteDataTable.MenuSpriteNo.S2D01B, 10, 168, 0, new GSRect(168, 0, 32, 16), ObjMove.D, 4, 0, null, GSTouch.Check.CLICK, 0),
			new SubWindowObjInfo(SpriteDataTable.MenuSpriteNo.S2D021, 7, 72, 24, new GSRect(72, 24, 16, 32), ObjMove.D, 4, 0, null, GSTouch.Check.CLICK, 0),
			new SubWindowObjInfo(SpriteDataTable.MenuSpriteNo.S2D022, 7, 72, 24, new GSRect(72, 24, 16, 32), ObjMove.D, 4, 0, null, GSTouch.Check.CLICK, 0),
			new SubWindowObjInfo(SpriteDataTable.MenuSpriteNo.S2D023, 7, 72, 24, new GSRect(72, 24, 16, 32), ObjMove.D, 4, 0, null, GSTouch.Check.CLICK, 0),
			new SubWindowObjInfo(SpriteDataTable.MenuSpriteNo.S2D024, 7, 72, 24, new GSRect(72, 24, 16, 32), ObjMove.D, 4, 0, null, GSTouch.Check.CLICK, 0),
			new SubWindowObjInfo(SpriteDataTable.MenuSpriteNo.S2D025, 7, 72, 24, new GSRect(72, 24, 16, 32), ObjMove.D, 4, 0, null, GSTouch.Check.CLICK, 0),
			new SubWindowObjInfo(SpriteDataTable.MenuSpriteNo.S2D026, 7, 72, 24, new GSRect(72, 24, 16, 32), ObjMove.D, 4, 0, null, GSTouch.Check.CLICK, 0),
			new SubWindowObjInfo(SpriteDataTable.MenuSpriteNo.S2D027, 7, 72, 24, new GSRect(72, 24, 16, 32), ObjMove.D, 4, 0, null, GSTouch.Check.CLICK, 0),
			new SubWindowObjInfo(SpriteDataTable.MenuSpriteNo.S2D028, 7, 72, 24, new GSRect(72, 24, 16, 32), ObjMove.D, 4, 0, null, GSTouch.Check.CLICK, 0),
			new SubWindowObjInfo(SpriteDataTable.MenuSpriteNo.S2D02A, 7, 8, 24, new GSRect(8, 24, 64, 32), ObjMove.D, 4, 0, null, GSTouch.Check.CLICK, 0),
			new SubWindowObjInfo(SpriteDataTable.MenuSpriteNo.BTN2D, 2, 0, 160, new GSRect(0, 162, 80, 30), ObjMove.U, 4, 8, TouchBackButton, GSTouch.Check.CLICK, 2),
			new SubWindowObjInfo(SpriteDataTable.MenuSpriteNo.BTN2E, 2, 0, 160, new GSRect(0, 172, 80, 20), ObjMove.U, 4, 8, TouchBackButton, GSTouch.Check.CLICK, 2),
			new SubWindowObjInfo(SpriteDataTable.MenuSpriteNo.BTN34, 13, 88, 0, new GSRect(88, 0, 80, 20), ObjMove.NONE, 4, 0, GSTouch.ButtonA, GSTouch.Check.CLICK, 1),
			new SubWindowObjInfo(SpriteDataTable.MenuSpriteNo.BTN33, 13, 135, 0, new GSRect(88, 0, 80, 20), ObjMove.NONE, 4, 0, GSTouch.ButtonA, GSTouch.Check.CLICK, 1),
			new SubWindowObjInfo(SpriteDataTable.MenuSpriteNo.BTN32, 13, 121, 0, new GSRect(88, 0, 80, 20), ObjMove.NONE, 4, 0, GSTouch.ButtonA, GSTouch.Check.CLICK, 1),
			new SubWindowObjInfo(SpriteDataTable.MenuSpriteNo.BTN31, 13, 107, 0, new GSRect(88, 0, 80, 20), ObjMove.NONE, 4, 0, GSTouch.ButtonA, GSTouch.Check.CLICK, 1),
			new SubWindowObjInfo(SpriteDataTable.MenuSpriteNo.BTN30, 13, 90, 0, new GSRect(88, 0, 80, 20), ObjMove.NONE, 4, 0, GSTouch.ButtonA, GSTouch.Check.CLICK, 1),
			new SubWindowObjInfo(SpriteDataTable.MenuSpriteNo.BTN2F, 13, 88, 0, new GSRect(88, 0, 80, 20), ObjMove.NONE, 4, 0, GSTouch.ButtonA, GSTouch.Check.CLICK, 1),
			new SubWindowObjInfo(SpriteDataTable.MenuSpriteNo.BTN40, 2, 176, 0, new GSRect(200, 0, 56, 30), ObjMove.D, 4, 8, GSTouch.ButtonR, GSTouch.Check.CLICK, 256),
			new SubWindowObjInfo(SpriteDataTable.MenuSpriteNo.BTN41, 2, 176, 0, new GSRect(200, 0, 56, 30), ObjMove.D, 4, 8, GSTouch.ButtonR, GSTouch.Check.CLICK, 256),
			default(SubWindowObjInfo)
		};
	}

	public SubWindow()
	{
		GSStructUtility.FillArrayNewInstance(routine_);
		GSStructUtility.FillArrayNewInstance(sprite_routine_);
	}

	public void Process()
	{
		Routine routine = routine_[stack_];
		sub_window_proc_table[routine.r.no_0](this);
		for (int i = 0; i < 80; i++)
		{
			ObjProc(i);
		}
		BarMain();
		real_fast_mes_ = 0;
		if (routine_[stack_].r.no_0 != 24)
		{
		}
	}

	public bool SetReq(Req req)
	{
		debugLogger.instance.Log("SubWindowReq", "SetReq:" + req);
		Routine routine = routine_[stack_];
		switch (req)
		{
		case Req.QUESTIONING_EXIT:
			if (routine.r.no_0 == 4)
			{
				req_ = req;
				return true;
			}
			break;
		case Req.SELECT_EXIT:
			if (routine.r.no_0 == 3)
			{
				req_ = req;
				return true;
			}
			break;
		case Req.HOUTEI_EXIT:
			if (routine.r.no_0 == 2)
			{
				req_ = req;
				return true;
			}
			break;
		case Req.TITLE_EXIT:
			if (routine.r.no_0 == 1)
			{
				req_ = req;
				return true;
			}
			break;
		case Req.TITLE:
		case Req.TITLE_START:
			req_ = req;
			return true;
		case Req.HOUTEI:
		case Req.TANTEI:
			if (routine.r.no_0 == 0 || routine.r.no_0 == 20 || routine.r.no_0 == 23)
			{
				req_ = req;
				return true;
			}
			break;
		case Req.SELECT:
			if (routine.r.no_0 == 2 || routine.r.no_0 == 5 || routine.r.no_0 == 10 || routine.r.no_0 == 4 || routine.r.no_0 == 23)
			{
				req_ = req;
				return true;
			}
			break;
		case Req.QUESTIONING_FIRST:
		case Req.QUESTIONING:
			if (GSStatic.global_work_.title == TitleId.GS1)
			{
				if (routine.r.no_0 == 2 && GSStatic.global_work_.rest > 0)
				{
					req_ = req;
					return true;
				}
			}
			else if (routine.r.no_0 == 2 && GSStatic.global_work_.gauge_hp > 0)
			{
				req_ = req;
				return true;
			}
			break;
		case Req.QUESTIONING_YUSABURU:
			if (routine.r.no_0 == 4)
			{
				req_ = req;
				return true;
			}
			break;
		case Req.IDLE:
			if (stack_ == 0)
			{
				req_ = req;
				return true;
			}
			break;
		case Req.INSPECT_EXIT:
			if (routine.r.no_0 == 6)
			{
				req_ = req;
				return true;
			}
			break;
		case Req.MOVE_EXIT:
			if (routine.r.no_0 == 7)
			{
				req_ = req;
				return true;
			}
			break;
		case Req.MOVE_GO:
			req_ = req;
			return true;
		case Req.TALK_EXIT:
			if (routine.r.no_0 == 8)
			{
				req_ = req;
				return true;
			}
			break;
		case Req.ATTACK_EXIT:
			if (routine.r.no_0 == 9)
			{
				req_ = req;
				return true;
			}
			break;
		case Req.INSPECT:
		case Req.MOVE:
		case Req.TALK:
			if (routine.r.no_0 == 5)
			{
				req_ = req;
				return true;
			}
			break;
		case Req.STATUS:
			if (routine.r.no_0 != 11)
			{
				req_ = req;
				return true;
			}
			break;
		case Req.STATUS_EXIT:
		case Req.STATUS_SETU:
		case Req.STATUS_TUKITUKERU:
			if (routine.r.no_0 == 11 || routine.r.no_0 == 12 || routine.r.no_0 == 13)
			{
				req_ = req;
				return true;
			}
			break;
		case Req.STATUS_SETU_EXIT:
			if (routine.r.no_0 == 12)
			{
				req_ = req;
				return true;
			}
			break;
		case Req.STATUS_3D:
			req_ = req;
			return true;
		case Req.STATUS_3D_EXIT:
			if (routine.r.no_0 == 13)
			{
				req_ = req;
				return true;
			}
			break;
		case Req.POINT:
			if (routine.r.no_0 == 2 || routine.r.no_0 == 14 || (GSStatic.global_work_.r.no_0 == 5 && GSStatic.global_work_.r.no_1 == 11))
			{
				req_ = req;
				return true;
			}
			break;
		case Req.POINT_EXIT:
			req_ = req;
			return true;
		case Req.RT_TMAIN:
			req_ = req;
			return true;
		case Req.MESS_EXIT:
		case Req.SAVE:
		case Req.SAVE_EXIT:
		case Req.SAVE_EXIT2:
		case Req.SAVE_DECIDE:
		case Req.LUMINOL:
		case Req.LUMINOL_EXIT:
		case Req.LUMINOL_ANM:
		case Req.FINGER:
		case Req.FINGER_EXIT:
		case Req.JUDGMENT:
		case Req.JUDGMENT_EXIT:
		case Req.OPENING:
		case Req.HUMAN:
		case Req._3D_POINT:
		case Req.EPISODE_CLEAR:
		case Req.VASE_PUZZLE:
		case Req.VASE_SHOW:
		case Req.SAFE_CRACKING:
		case Req.MOVIE_THRUST:
		case Req.SAVE_EXIT3:
		case Req.FINGER_EFF0:
		case Req.FINGER_EFF1:
		case Req.FINGER_EFF2:
		case Req.FINGER_EFF3:
		case Req.FINGER_EFF4:
		case Req.FINGER_EFF5:
		case Req.FINGER_EFF6:
		case Req.DIE_MES:
		case Req.LUMINOL_SCENARIO:
		case Req.STAFF_MASK_SET:
		case Req.STAFF_FORCE_OUT:
		case Req.FORMAT:
		case Req.FORMAT_EXIT:
		case Req.FORMAT_DECIDE:
		case Req.QUESTIONING_PREV:
		case Req.QUESTIONING_NEXT:
		case Req.GBA_STAFF:
		case Req.LAST_OBJECTION:
		case Req.MAGATAMA_TUKITUKE:
		case Req.MAGATAMA_TUKITUKE2:
		case Req.MAGATAMA_MENU_ON:
		case Req.POINT_TANTEI_EXIT:
			req_ = req;
			return true;
		case Req.BLANK:
			req_ = req;
			break;
		default:
			return false;
		}
		return false;
	}

	public bool IsBusy()
	{
		if (busy_ == 0 && req_ == Req.NONE && bar_req_ == BarReq.NONE)
		{
			return false;
		}
		return true;
	}

	private void ObjProc(int obj_num)
	{
		ObjRoutine objRoutine = sprite_routine_[obj_num];
		SubWindowObjInfo subWindowObjInfo = obj_info_table[obj_num];
		switch (objRoutine.r.no_0)
		{
		case 0:
			if ((obj_flag_[obj_num] & ObjFlag.DISP) != 0 && (CheckObjOut() || obj_num == 51 || obj_num == 52 || obj_num == 54 || obj_num == 55 || obj_num == 56))
			{
				if (obj_num == 5 && (GSStatic.global_work_.status_flag & 0x10u) != 0)
				{
					bar_req_ = BarReq.PLANE;
					ObjFlag[] array;
					(array = obj_flag_)[5] = array[5] & ~ObjFlag.DISP;
					objRoutine.r.no_0 = 7;
				}
				else
				{
					ObjFlag[] array;
					int num4;
					(array = obj_flag_)[num4 = obj_num] = array[num4] & ~ObjFlag.OUT;
					objRoutine.r.no_0++;
				}
			}
			break;
		case 1:
			if (objRoutine.r.no_1 == 0)
			{
				objRoutine.x = subWindowObjInfo.x;
				objRoutine.y = subWindowObjInfo.y;
				ObjFlag[] array;
				int num7;
				(array = obj_flag_)[num7 = obj_num] = array[num7] | ObjFlag.IN;
				if (subWindowObjInfo.proc != null)
				{
					objRoutine.r.no_0++;
				}
				else
				{
					objRoutine.r.no_0 = 4;
				}
			}
			else
			{
				objRoutine.x += objRoutine.ratex;
				objRoutine.y += objRoutine.ratey;
				objRoutine.r.no_1--;
			}
			break;
		case 2:
			if ((obj_flag_[obj_num] & ObjFlag.DISP) == 0)
			{
				if ((obj_num == 17 && (obj_flag_[51] & ObjFlag.DISP) != 0) || (obj_num == 14 && (obj_flag_[52] & ObjFlag.DISP) != 0) || (obj_num == 14 && (obj_flag_[54] & ObjFlag.DISP) != 0))
				{
					int num5;
					ObjFlag[] array;
					(array = obj_flag_)[num5 = obj_num] = array[num5] & ~ObjFlag.IN;
					int num6;
					(array = obj_flag_)[num6 = obj_num] = array[num6] | ObjFlag.OUT;
					objRoutine.r.no_0 = 0;
					objRoutine.r.no_2 = 0;
					switch ((ObjId)obj_num)
					{
					case ObjId.BTN04:
						sprite_routine_[4].r.no_2 = 0;
						break;
					case ObjId.BTN04_2:
						sprite_routine_[3].r.no_2 = 0;
						break;
					case ObjId.BTN0E:
						sprite_routine_[15].r.no_2 = 0;
						break;
					case ObjId.BTN0E_2:
						sprite_routine_[14].r.no_2 = 0;
						break;
					}
				}
				else
				{
					if (objRoutine.r.no_3 != byte.MaxValue)
					{
						objRoutine.r.no_3 = byte.MaxValue;
					}
					objRoutine.r.no_0++;
				}
			}
			if (objRoutine.r.no_2 == 1)
			{
				if (subWindowObjInfo.proc_trg != 1)
				{
					if (objRoutine.r.no_1 == 0)
					{
						objRoutine.r.no_2 = 0;
						switch ((ObjId)obj_num)
						{
						case ObjId.BTN04:
							sprite_routine_[4].r.no_2 = 0;
							break;
						case ObjId.BTN04_2:
							sprite_routine_[3].r.no_2 = 0;
							break;
						case ObjId.BTN0E:
							sprite_routine_[15].r.no_2 = 0;
							break;
						case ObjId.BTN0E_2:
							sprite_routine_[14].r.no_2 = 0;
							break;
						}
					}
					else
					{
						objRoutine.r.no_1--;
					}
				}
				if (objRoutine.reaction_flag == 1)
				{
					objRoutine.r.no_2 = 0;
					switch ((ObjId)obj_num)
					{
					case ObjId.BTN04:
						sprite_routine_[4].r.no_2 = 0;
						break;
					case ObjId.BTN04_2:
						sprite_routine_[3].r.no_2 = 0;
						break;
					case ObjId.BTN0E:
						sprite_routine_[15].r.no_2 = 0;
						break;
					case ObjId.BTN0E_2:
						sprite_routine_[14].r.no_2 = 0;
						break;
					}
				}
			}
			if (objRoutine.reaction_flag != 0 || !CheckObjIn() || !CheckObjOut() || (obj_flag_[obj_num] & ObjFlag.DISP) == 0 || obj_num != 5)
			{
				break;
			}
			if ((GSStatic.global_work_.status_flag & 0x10u) != 0)
			{
				bar_req_ = BarReq.PLANE;
				ObjFlag[] array;
				(array = obj_flag_)[5] = array[5] & ~ObjFlag.DISP;
				objRoutine.r.no_0 = 6;
			}
			else if (routine_[0].r.no_0 == 5)
			{
				if ((GSStatic.message_work_.status & (MessageSystem.Status.RT_WAIT | MessageSystem.Status.LOOP)) == 0)
				{
					objRoutine.r.no_2 = 0;
				}
			}
			else if ((GSStatic.message_work_.status & (MessageSystem.Status.RT_WAIT | MessageSystem.Status.SELECT)) == 0)
			{
				objRoutine.r.no_2 = 0;
			}
			break;
		case 3:
			if (objRoutine.r.no_1 == 0)
			{
				int num11;
				ObjFlag[] array;
				(array = obj_flag_)[num11 = obj_num] = array[num11] & ~ObjFlag.IN;
				int num12;
				(array = obj_flag_)[num12 = obj_num] = array[num12] | ObjFlag.OUT;
				objRoutine.r.no_0 = 0;
				objRoutine.r.no_2 = 0;
				switch ((ObjId)obj_num)
				{
				case ObjId.BTN04:
					sprite_routine_[4].r.no_2 = 0;
					break;
				case ObjId.BTN04_2:
					sprite_routine_[3].r.no_2 = 0;
					break;
				case ObjId.BTN0E:
					sprite_routine_[15].r.no_2 = 0;
					break;
				case ObjId.BTN0E_2:
					sprite_routine_[14].r.no_2 = 0;
					break;
				}
			}
			else
			{
				objRoutine.x += objRoutine.ratex;
				objRoutine.y += objRoutine.ratey;
				objRoutine.r.no_1--;
			}
			break;
		case 4:
			if ((obj_flag_[obj_num] & ObjFlag.DISP) == 0)
			{
				objRoutine.r.no_0--;
			}
			break;
		case 5:
			if (objRoutine.r.no_1 == 0)
			{
				objRoutine.r.no_0 = 2;
			}
			objRoutine.r.no_1--;
			break;
		case 6:
			if (objRoutine.r.no_1 == 0)
			{
				int num8;
				ObjFlag[] array;
				(array = obj_flag_)[num8 = obj_num] = array[num8] & ~ObjFlag.IN;
				int num9;
				(array = obj_flag_)[num9 = obj_num] = array[num9] | ObjFlag.OUT;
				int num10;
				(array = obj_flag_)[num10 = obj_num] = array[num10] & ~ObjFlag.DISP;
				objRoutine.r.no_0++;
			}
			else
			{
				objRoutine.x += objRoutine.ratex;
				objRoutine.y += objRoutine.ratey;
			}
			objRoutine.r.no_1--;
			break;
		case 7:
		{
			Routine currentRoutine = GetCurrentRoutine();
			if (currentRoutine.r.no_0 != 2 && currentRoutine.r.no_0 != 10 && currentRoutine.r.no_0 != 5)
			{
				objRoutine.r.no_0 = 0;
			}
			else if ((GSStatic.global_work_.status_flag & 0x10) == 0)
			{
				if (routine_[0].r.no_0 == 5 && (currentRoutine.r.no_0 == 5 || currentRoutine.r.no_0 == 10))
				{
					if ((GSStatic.message_work_.status & (MessageSystem.Status.RT_WAIT | MessageSystem.Status.RT_GO | MessageSystem.Status.LOOP)) != 0)
					{
						bar_req_ = BarReq.TANTEI;
						ObjFlag[] array;
						int num;
						(array = obj_flag_)[num = obj_num] = array[num] | ObjFlag.DISP;
						objRoutine.r.no_0 = 0;
					}
				}
				else if (routine_[0].r.no_0 == 2 && (GSStatic.message_work_.status & MessageSystem.Status.RT_WAIT) != 0)
				{
					bar_req_ = BarReq.TANTEI;
					ObjFlag[] array;
					int num2;
					(array = obj_flag_)[num2 = obj_num] = array[num2] | ObjFlag.DISP;
					objRoutine.r.no_0 = 0;
				}
			}
			else
			{
				ObjFlag[] array;
				int num3;
				(array = obj_flag_)[num3 = obj_num] = array[num3] & ~ObjFlag.DISP;
				if (bar_req_ == BarReq.TANTEI)
				{
					bar_req_ = BarReq.PLANE;
				}
			}
			break;
		}
		}
	}

	private void BarMain()
	{
		BarReq barReq = BarReq.NONE;
		if (bar_req_ != 0)
		{
			barReq = bar_req_;
			bar_req_ = BarReq.NONE;
		}
		uint num = 0u;
		for (ushort num2 = 0; num2 < 2; num2++)
		{
			int num3 = (int)(barReq + num2);
			BarInfo[] array = SubWindow_Bar.bar_info_table[num3];
			for (int i = 0; i < array.Length; i++)
			{
				num++;
			}
			for (int j = 0; j < array.Length; j++)
			{
			}
		}
		if (num != 0)
		{
		}
	}

	private static void TouchBackButton()
	{
		GlobalWork global_work_ = GSStatic.global_work_;
		SubWindow sub_window_ = advCtrl.instance.sub_window_;
		Routine currentRoutine = sub_window_.GetCurrentRoutine();
		switch ((State)currentRoutine.r.no_0)
		{
		case State.STATUS:
			if ((global_work_.r.no_3 != 1 || (global_work_.status_flag & 0x100) == 0) && global_work_.r.no_3 != 3)
			{
				sub_window_.SetReq(Req.STATUS_EXIT);
			}
			break;
		case State.STATUS_DETAIL:
			sub_window_.SetReq(Req.STATUS_SETU_EXIT);
			break;
		case State.STATUS_3D:
			sub_window_.SetReq(Req.STATUS_3D_EXIT);
			break;
		case State.MOVE:
			sub_window_.SetReq(Req.MOVE_EXIT);
			break;
		case State.TALK:
			sub_window_.SetReq(Req.TALK_EXIT);
			break;
		case State.LUMINOL:
			sub_window_.SetReq(Req.LUMINOL_EXIT);
			break;
		case State.FINGER:
			sub_window_.SetReq(Req.FINGER_EXIT);
			break;
		}
	}

	public void SetObjDispFlag(int btn_req)
	{
		switch ((BtnReq)btn_req)
		{
		case BtnReq.HOUTEI:
			SetObjDispPrevFlag();
			ClearObjDispFlag();
			SetObjFlagOne(5, 1, 0);
			break;
		case BtnReq.SELECT:
			SetObjDispPrevFlag();
			ClearObjDispFlag();
			if (routine_[stack_ - 1].r.no_0 != 23)
			{
				SetObjFlagOne(5, 1, 0);
			}
			break;
		case BtnReq.QUESTIONING:
			SetObjDispPrevFlag();
			ClearObjDispFlag();
			SetObjFlagOne(17, 1, 0);
			SetObjFlagOne(16, 1, 0);
			SetObjFlagOne(47, 1, 0);
			break;
		case BtnReq.QUESTIONING_VOICE:
			SetObjDispPrevFlag();
			ClearObjDispFlag();
			SetObjFlagOne(51, 1, 0);
			SetObjFlagOne(55, 1, 0);
			SetObjFlagOne(58, 1, 0);
			break;
		case BtnReq.TANTEI:
			SetObjDispPrevFlag();
			ClearObjDispFlag();
			SetObjFlagOne(5, 1, 0);
			break;
		case BtnReq.INSPECT:
			SetObjDispPrevFlag();
			ClearObjDispFlag();
			SetObjFlagOne(21, 1, 0);
			SetObjFlagOne(6, 1, 0);
			break;
		case BtnReq.MOVE:
			SetObjDispPrevFlag();
			ClearObjDispFlag();
			SetObjFlagOne(5, 1, 0);
			SetObjFlagOne(2, 1, 0);
			break;
		case BtnReq.TALK:
			SetObjDispPrevFlag();
			ClearObjDispFlag();
			SetObjFlagOne(5, 1, 0);
			SetObjFlagOne(2, 1, 0);
			break;
		case BtnReq.ATTACK:
			SetObjDispPrevFlag();
			ClearObjDispFlag();
			SetObjFlagOne(2, 1, 0);
			if (note_.current_mode == 0)
			{
				if (GSStatic.global_work_.title == TitleId.GS1)
				{
					SetObjFlagOne(7, 1, 1);
				}
				else
				{
					SetObjFlagOne(7, 1, 0);
				}
				SetObjFlagOne(20, 1, 1);
				SetObjFlagOne(14, 1, 0);
				SetObjFlagOne(38, 1, 0);
				SetObjFlagOne(41, 1, 0);
				SetObjFlagOne(46, 1, 0);
				SetObjFlagOne(43, 1, 0);
				SetObjFlagOne(44, 1, 0);
				SetObjFlagOne(45, 1, 0);
			}
			else
			{
				SetObjFlagOne(7, 1, 1);
				SetObjFlagOne(20, 1, 0);
				SetObjFlagOne(14, 1, 0);
				SetObjFlagOne(37, 1, 0);
				SetObjFlagOne(40, 1, 0);
				SetObjFlagOne(46, 1, 0);
				SetObjFlagOne(43, 1, 0);
				SetObjFlagOne(44, 1, 0);
				SetObjFlagOne(45, 1, 0);
			}
			if (GSStatic.global_work_.r.no_3 == 1)
			{
				SetObjFlagOne(48, 1, 0);
			}
			break;
		case BtnReq.ATTACK_VOICE_0:
			SetObjDispPrevFlag();
			SetObjFlagOne(14, 1, 1);
			SetObjFlagOne(48, 1, 1);
			SetObjFlagOne(52, 1, 0);
			SetObjFlagOne(56, 1, 0);
			SetObjFlagOne(59, 1, 0);
			break;
		case BtnReq.ATTACK_VOICE_1:
			SetObjDispPrevFlag();
			SetObjFlagOne(14, 1, 1);
			SetObjFlagOne(48, 1, 1);
			SetObjFlagOne(54, 1, 0);
			SetObjFlagOne(56, 1, 0);
			SetObjFlagOne(59, 1, 0);
			break;
		case BtnReq.KAIWA:
			SetObjDispPrevFlag();
			ClearObjDispFlag();
			if (routine_[stack_ - 1].r.no_0 != 23)
			{
				SetObjFlagOne(5, 1, 0);
			}
			break;
		case BtnReq.STATUS:
			SetObjDispPrevFlag();
			ClearObjDispFlag();
			SetObjFlagOne(2, 1, 0);
			if (note_.current_mode == 0)
			{
				SetObjFlagOne(7, 1, 0);
				SetObjFlagOne(20, 1, 1);
				SetObjFlagOne(38, 1, 0);
				SetObjFlagOne(41, 1, 0);
				SetObjFlagOne(46, 1, 0);
				SetObjFlagOne(43, 1, 0);
				SetObjFlagOne(44, 1, 0);
				SetObjFlagOne(45, 1, 0);
			}
			else
			{
				SetObjFlagOne(7, 1, 1);
				SetObjFlagOne(20, 1, 0);
				SetObjFlagOne(37, 1, 0);
				SetObjFlagOne(40, 1, 0);
				SetObjFlagOne(46, 1, 0);
				SetObjFlagOne(43, 1, 0);
				SetObjFlagOne(44, 1, 0);
				SetObjFlagOne(45, 1, 0);
			}
			break;
		case BtnReq.STATUS_3D:
			SetObjDispPrevFlag();
			ClearObjDispFlag();
			SetObjFlagOne(21, 1, 0);
			break;
		case BtnReq.POINT:
			SetObjDispPrevFlag();
			ClearObjDispFlag();
			SetObjFlagOne(3, 1, 0);
			if (GSStatic.global_work_.r.no_0 == 5)
			{
				SetObjFlagOne(70, 1, 0);
			}
			break;
		case BtnReq.LUMINOL:
			SetObjDispPrevFlag();
			ClearObjDispFlag();
			SetObjFlagOne(21, 1, 0);
			break;
		case BtnReq.FINGER:
			SetObjDispPrevFlag();
			ClearObjDispFlag();
			SetObjFlagOne(49, 1, 0);
			break;
		case BtnReq.FINGER_SELECT:
			SetObjDispPrevFlag();
			ClearObjDispFlag();
			SetObjFlagOne(21, 1, 0);
			break;
		case BtnReq.EPISODE:
			SetObjDispPrevFlag();
			ClearObjDispFlag();
			SetObjFlagOne(2, 1, 0);
			SetObjFlagOne(23, 1, 0);
			break;
		case BtnReq.CONTINUE:
			SetObjDispPrevFlag();
			ClearObjDispFlag();
			SetObjFlagOne(21, 1, 0);
			break;
		case BtnReq.HUMAN:
			SetObjDispPrevFlag();
			ClearObjDispFlag();
			SetObjFlagOne(32, 1, 0);
			SetObjFlagOne(39, 1, 0);
			SetObjFlagOne(42, 1, 0);
			SetObjFlagOne(46, 1, 0);
			SetObjFlagOne(43, 1, 0);
			SetObjFlagOne(44, 1, 0);
			SetObjFlagOne(45, 1, 0);
			break;
		case BtnReq.HUMAN2:
			SetObjDispPrevFlag();
			ClearObjDispFlag();
			SetObjFlagOne(14, 1, 0);
			SetObjFlagOne(37, 1, 0);
			SetObjFlagOne(40, 1, 0);
			SetObjFlagOne(46, 1, 0);
			SetObjFlagOne(43, 1, 0);
			SetObjFlagOne(44, 1, 0);
			SetObjFlagOne(45, 1, 0);
			break;
		case BtnReq._3D_POINT:
			SetObjDispPrevFlag();
			ClearObjDispFlag();
			SetObjFlagOne(3, 1, 0);
			break;
		case BtnReq.VASE_PUZZLE:
			SetObjDispPrevFlag();
			ClearObjDispFlag();
			SetObjFlagOne(18, 1, 0);
			SetObjFlagOne(21, 1, 0);
			SetObjFlagOne(26, 1, 0);
			SetObjFlagOne(27, 1, 0);
			SetObjFlagOne(30, 1, 0);
			SetObjFlagOne(31, 1, 0);
			SetObjFlagOne(28, 1, 0);
			SetObjFlagOne(29, 1, 0);
			SetObjFlagOne(60, 1, 0);
			SetObjFlagOne(68, 1, 0);
			break;
		case BtnReq.VASE_SHOW:
			SetObjDispPrevFlag();
			ClearObjDispFlag();
			SetObjFlagOne(15, 1, 0);
			SetObjFlagOne(24, 1, 0);
			SetObjFlagOne(25, 1, 0);
			break;
		case BtnReq.NOTE_SPECIAL:
			SetObjDispPrevFlag();
			ClearObjDispFlag();
			SetObjFlagOne(21, 1, 0);
			break;
		case BtnReq.NOTE_SPECIAL_2:
			SetObjDispPrevFlag();
			ClearObjDispFlag();
			SetObjFlagOne(21, 1, 0);
			break;
		case BtnReq.MOVIE_THRUST_PLAY:
			SetObjDispPrevFlag();
			ClearObjDispFlag();
			SetObjFlagOne(3, 1, 0);
			SetObjFlagOne(6, 1, 0);
			SetObjFlagOne(19, 1, 0);
			SetObjFlagOne(9, 1, 0);
			SetObjFlagOne(11, 1, 0);
			break;
		case BtnReq.MOVIE_THRUST_STOP:
			SetObjDispPrevFlag();
			ClearObjDispFlag();
			SetObjFlagOne(3, 1, 0);
			SetObjFlagOne(6, 1, 0);
			SetObjFlagOne(0, 1, 0);
			SetObjFlagOne(35, 1, 0);
			SetObjFlagOne(36, 1, 0);
			break;
		case BtnReq.DIE_MES:
			SetObjDispPrevFlag();
			ClearObjDispFlag();
			SetObjFlagOne(4, 1, 0);
			SetObjFlagOne(6, 1, 0);
			SetObjFlagOne(21, 1, 0);
			break;
		case BtnReq.LAST_OBJECTION:
			SetObjDispPrevFlag();
			ClearObjDispFlag();
			SetObjFlagOne(48, 1, 0);
			SetObjFlagOne(53, 1, 0);
			SetObjFlagOne(57, 1, 0);
			break;
		case BtnReq.ATTACK_PSY:
			SetObjDispPrevFlag();
			ClearObjDispFlag();
			SetObjFlagOne(69, 1, 0);
			if (note_.current_mode == 0)
			{
				SetObjFlagOne(7, 1, 0);
				SetObjFlagOne(20, 1, 1);
				SetObjFlagOne(14, 1, 0);
				SetObjFlagOne(38, 1, 0);
				SetObjFlagOne(41, 1, 0);
				SetObjFlagOne(46, 1, 0);
				SetObjFlagOne(43, 1, 0);
				SetObjFlagOne(44, 1, 0);
				SetObjFlagOne(45, 1, 0);
			}
			else
			{
				SetObjFlagOne(7, 1, 1);
				SetObjFlagOne(20, 1, 0);
				SetObjFlagOne(14, 1, 0);
				SetObjFlagOne(37, 1, 0);
				SetObjFlagOne(40, 1, 0);
				SetObjFlagOne(46, 1, 0);
				SetObjFlagOne(43, 1, 0);
				SetObjFlagOne(44, 1, 0);
				SetObjFlagOne(45, 1, 0);
			}
			if (GSStatic.global_work_.r.no_3 == 1)
			{
				SetObjFlagOne(48, 1, 0);
			}
			break;
		case BtnReq.TANCHIKI:
			SetObjDispPrevFlag();
			ClearObjDispFlag();
			SetObjFlagOne(5, 1, 1);
			SetObjFlagOne(72, 1, 1);
			SetObjFlagOne(73, 1, 1);
			SetObjFlagOne(74, 1, 1);
			SetObjFlagOne(75, 1, 1);
			SetObjFlagOne(76, 1, 1);
			SetObjFlagOne(77, 1, 0);
			break;
		case BtnReq.DISP_OFF:
		case (BtnReq)240:
			SetObjDispPrevFlag();
			ClearObjDispFlag();
			break;
		}
	}

	public bool CheckObjOut()
	{
		return true;
	}

	public bool CheckObjIn()
	{
		return true;
	}

	public void SetObjDispPrevFlag()
	{
		for (ushort num = 0; num < 80; num++)
		{
			if ((obj_flag_[num] & ObjFlag.DISP) != 0)
			{
				ObjFlag[] array;
				int num2;
				(array = obj_flag_)[num2 = num] = array[num2] | ObjFlag.DISP_PREV;
			}
			else
			{
				ObjFlag[] array;
				int num3;
				(array = obj_flag_)[num3 = num] = array[num3] & ~ObjFlag.DISP_PREV;
			}
		}
	}

	public void ClearObjDispFlag()
	{
		for (ushort num = 0; num < 80; num++)
		{
			ObjFlag[] array;
			int num2;
			(array = obj_flag_)[num2 = num] = array[num2] & ~ObjFlag.DISP;
		}
	}

	public void SetObjFlagOne(int obj_id, byte flag, byte off)
	{
		switch (off)
		{
		case 0:
		{
			ObjFlag[] array;
			int num2;
			(array = obj_flag_)[num2 = obj_id] = (ObjFlag)((int)array[num2] | (int)flag);
			break;
		}
		case 1:
		{
			ObjFlag[] array;
			int num;
			(array = obj_flag_)[num = obj_id] = (ObjFlag)((int)array[num] & ~flag);
			break;
		}
		}
	}

	private static void Proc_Dummy(SubWindow sub_window)
	{
	}

	private static void Proc_Idle(SubWindow sub_window)
	{
		Routine routine = sub_window.routine_[sub_window.stack_];
		switch (sub_window.req_)
		{
		case Req.TITLE:
			routine.r.Set(1, 0, 0, 0);
			break;
		case Req.HOUTEI:
			routine.r.Set(2, 0, 0, 0);
			break;
		case Req.TANTEI:
			routine.r.Set(5, 0, 0, 0);
			break;
		case Req.BLANK:
			routine.r.Set(20, 0, 0, 0);
			break;
		case Req.FORMAT:
			routine.r.Set(21, 0, 0, 0);
			break;
		}
		if (routine.r.no_0 != 0)
		{
			sub_window.busy_ = 1u;
			for (ushort num = 0; num < 64; num++)
			{
				sub_window.obj_flag_[num] = (ObjFlag)0;
			}
			for (ushort num2 = 0; num2 < routine.tp_cnt; num2++)
			{
			}
			routine.tp_cnt = 0;
		}
	}

	private static void Proc_Title(SubWindow sub_window)
	{
	}

	private static void Proc_Tantei(SubWindow sub_window)
	{
		Routine routine = sub_window.routine_[sub_window.stack_];
		switch (routine.r.no_1)
		{
		case 0:
			switch (routine.r.no_2)
			{
			case 0:
				Debug.Log("SetObjDispFlag SW_HOUTEI");
				break;
			}
			break;
		case 1:
			break;
		case 2:
			switch (routine.r.no_2)
			{
			case 0:
				break;
			case 1:
				break;
			case 2:
				Debug.Log("SetObjDispFlag SW_TANTEI");
				break;
			case 3:
				break;
			}
			break;
		case 3:
			switch (routine.r.no_2)
			{
			}
			break;
		case 4:
			break;
		case 5:
			switch (routine.r.no_2)
			{
			}
			break;
		case 6:
			break;
		case 7:
			switch (routine.r.no_2)
			{
			}
			break;
		case 8:
			break;
		}
	}

	private static void Proc_Inspect(SubWindow sub_window)
	{
		Routine routine = sub_window.routine_[sub_window.stack_];
		switch (routine.r.no_1)
		{
		case 0:
			inspectCtrl.instance.play();
			routine.r.no_1++;
			break;
		case 1:
			if (!inspectCtrl.instance.is_play)
			{
				if (inspectCtrl.instance.is_cancel)
				{
					sub_window.routine_[sub_window.stack_].r.Set(5, 3, 0, 0);
				}
				else
				{
					routine.r.no_1++;
				}
			}
			break;
		}
	}

	private static void Proc_Move(SubWindow sub_window)
	{
		GlobalWork global_work_ = GSStatic.global_work_;
		Routine routine = sub_window.routine_[sub_window.stack_];
		switch (routine.r.no_1)
		{
		case 0:
		{
			int num = 0;
			for (int i = 0; i < 4; i++)
			{
				uint num2 = global_work_.Map_data[global_work_.Room][4 + i];
				if (num2 != 255)
				{
					moveCtrl.instance.select_list[num].bg_no_ = global_work_.Map_data[num2][0];
					num++;
				}
			}
			moveCtrl.instance.play(num, 0);
			routine.r.no_1++;
			break;
		}
		case 1:
			if (!moveCtrl.instance.is_play)
			{
				if (moveCtrl.instance.is_cancel)
				{
					sub_window.routine_[sub_window.stack_].r.Set(5, 3, 0, 0);
				}
				else
				{
					GSStatic.global_work_.r.Set(5, 5, 0, 0);
				}
			}
			break;
		}
	}

	private static void Proc_Talk(SubWindow sub_window)
	{
	}

	private static void Proc_Attack(SubWindow sub_window)
	{
	}

	private static void Proc_Kaiwa(SubWindow sub_window)
	{
	}

	private static void Proc_Status(SubWindow sub_window)
	{
	}

	private static void Proc_StatusDetail(SubWindow sub_window)
	{
	}

	private static void Proc_Status3d(SubWindow sub_window)
	{
	}

	private static void Proc_Point(SubWindow sub_window)
	{
		Routine currentRoutine = sub_window.GetCurrentRoutine();
		switch (currentRoutine.r.no_1)
		{
		case 0:
			PointMiniGame.instance.Init(GSStatic.tantei_work_.siteki_no);
			currentRoutine.r.no_1++;
			break;
		case 1:
		{
			PointMiniGame instance = PointMiniGame.instance;
			if (!instance.is_running)
			{
				GlobalWork global_work_ = GSStatic.global_work_;
				if (!instance.is_canceled)
				{
					advCtrl.instance.message_system_.SetMessage(instance.GetResultMessage());
				}
				else
				{
					global_work_.r.no_2 = 8;
					global_work_.r.no_3 = 0;
					sub_window.tantei_tukituke_ = 0;
				}
				global_work_.Mess_move_flag = 1;
				sub_window.stack_--;
			}
			break;
		}
		}
	}

	private static void Proc_Save(SubWindow sub_window)
	{
	}

	private static void Proc_Human(SubWindow sub_window)
	{
	}

	private static void Proc_HumanDetail(SubWindow sub_window)
	{
	}

	private static void Proc_Staffroll(SubWindow sub_window)
	{
		Routine routine = sub_window.routine_[sub_window.stack_];
		Req req = sub_window.req_;
		if (req == Req.STATUS_3D)
		{
			sub_window.tutorial_ = 30;
			routine.r.Set(13, 0, 0, 0);
		}
	}

	private static void Proc_Blank(SubWindow sub_window)
	{
		Routine routine = sub_window.routine_[sub_window.stack_];
		byte no_ = routine.r.no_1;
		if (no_ == 0 || no_ == 1)
		{
			switch (sub_window.req_)
			{
			case Req.TITLE:
				routine.r.Set(1, 0, 0, 0);
				break;
			case Req.HOUTEI:
				routine.r.Set(2, 0, 0, 0);
				break;
			case Req.TANTEI:
				routine.r.Set(5, 0, 0, 0);
				break;
			}
		}
	}

	private static void Proc_Format(SubWindow sub_window)
	{
	}

	private static void Proc_Tanchiki(SubWindow sub_window)
	{
	}

	private static void Proc_Luminol(SubWindow sub_window)
	{
	}

	private static void Proc_Finger(SubWindow sub_window)
	{
		Routine currentRoutine = sub_window.GetCurrentRoutine();
		if (currentRoutine.r.no_1 == 0)
		{
			FingerMiniGame.instance.Init(GSStatic.global_work_.r.no_3);
			currentRoutine.r.no_1++;
		}
	}

	private static void Proc_DieMes(SubWindow sub_window)
	{
	}

	private static void Proc_VasePuzzle(SubWindow sub_window)
	{
	}

	private static void Proc_VaseShow(SubWindow sub_window)
	{
	}

	private static void Proc_Safecracking(SubWindow sub_window)
	{
	}

	private static void Proc_MovieThrust(SubWindow sub_window)
	{
	}

	private static void Proc_Option(SubWindow sub_window)
	{
	}

	public bool CheckFastMessage()
	{
		if (real_fast_mes_ == 1)
		{
			return true;
		}
		return false;
	}

	public Routine GetCurrentRoutine()
	{
		return routine_[stack_];
	}

	public static byte GS1_GetMovieProcNo(ushort mess_no)
	{
		switch (mess_no)
		{
		case 156:
			return 0;
		case 163:
			return 1;
		case 157:
			return 2;
		case 167:
			return 3;
		default:
			return 0;
		}
	}
}
