using UnityEngine;

public class Cinema
{
	public class PAC_LIST
	{
		private uint offset;

		private uint size;
	}

	public struct FILM_TBL
	{
		public uint top_addr;

		public int x;

		public int y;

		public int film_num;

		public uint comp_type;

		public uint film_size;

		public FILM_TBL(uint in_top_addr, int in_x, int in_y, int in_film_num, uint in_comp_type, uint in_film_size)
		{
			top_addr = in_top_addr;
			x = in_x;
			y = in_y;
			film_num = in_film_num;
			comp_type = in_comp_type;
			film_size = in_film_size;
		}
	}

	public const uint FILM00_PAC_ADRS = 0u;

	public const uint FILM01_PAC_ADRS = 0u;

	public const uint FILM02_PAC_ADRS = 0u;

	public const uint FILM03_PAC_ADRS = 0u;

	public const uint FILM04_PAC_ADRS = 0u;

	public const uint MAX_CINEMA_FRAME_NUM = 1430u;

	public const uint CI_COMP_LZ = 0u;

	public const uint CI_COMP_RL = 1u;

	public const uint C256X192_P16_SIZE = 24576u;

	public const uint C256X160_P16_SIZE = 20480u;

	public const uint C256X192_P256_SIZE = 49152u;

	public const uint CINEMA_ST_OFF = 0u;

	public const uint CINEMA_ST_PLAY = 1u;

	public const uint CINEMA_ST_END = 2u;

	public const uint CINEMA_ST_STOP = 3u;

	public const uint CINEMA_ST_REVERSE = 4u;

	public const uint CINEMA_ST_ESTOP = 5u;

	public const uint CINEMA_ST_ONE_STOP = 8192u;

	public const uint CINEMA_ST_END_STOP = 16384u;

	public const uint CINEMA_ST_LOOP = 32768u;

	public const uint CINEMA_ST_JMP_MASK = 15u;

	public const uint CINEMA_ST_BIT_MASK = 65520u;

	public static PAC_LIST[] Filme_info = new PAC_LIST[1430];

	public static FILM_TBL[] film_tbl = new FILM_TBL[5]
	{
		new FILM_TBL(0u, -16, 0, 450, 1u, 20480u),
		new FILM_TBL(0u, 0, 0, 329, 1u, 24576u),
		new FILM_TBL(0u, 0, 0, 1420, 0u, 24576u),
		new FILM_TBL(0u, 0, 0, 270, 0u, 24576u),
		new FILM_TBL(0u, 0, 0, 900, 0u, 49152u)
	};

	public static void Cinema_init(uint set_type, uint film_no, uint status, uint start_frame = 65535u)
	{
		Debug.Log("Cinema_init  set_type;" + set_type + " film_no:" + film_no + " status:" + status + " start_frame:" + start_frame);
		GSStatic.cinema_work_.set_type = (ushort)set_type;
		GSStatic.cinema_work_.film_no = (ushort)film_no;
		GSStatic.cinema_work_.sw = 0;
		GSStatic.cinema_work_.frame_add = 100;
		FILM_TBL fILM_TBL = film_tbl[GSStatic.cinema_work_.film_no];
		GSStatic.cinema_work_.status = (ushort)status;
		GSStatic.cinema_work_.frame_top = 0;
		GSStatic.cinema_work_.frame_end = (short)(fILM_TBL.film_num - 1);
		switch (GSStatic.cinema_work_.set_type)
		{
		case 0:
			GSStatic.cinema_work_.win_type = 0;
			GSStatic.cinema_work_.bg_no = 2;
			GSStatic.cinema_work_.plt = 3;
			break;
		case 1:
			GSStatic.cinema_work_.win_type = 1;
			GSStatic.cinema_work_.bg_no = 3;
			GSStatic.cinema_work_.plt = 0;
			break;
		case 2:
			GSStatic.cinema_work_.win_type = 0;
			GSStatic.cinema_work_.bg_no = 3;
			GSStatic.cinema_work_.plt = 3;
			break;
		case 3:
			GSStatic.cinema_work_.win_type = 1;
			GSStatic.cinema_work_.bg_no = 3;
			GSStatic.cinema_work_.plt = 3;
			break;
		case 4:
		case 5:
			GSStatic.cinema_work_.win_type = 1;
			GSStatic.cinema_work_.bg_no = 2;
			GSStatic.cinema_work_.plt = 3;
			break;
		}
	}

	public static void Cinema_set_status(uint status)
	{
		Debug.Log("Cinema_set_status  status:" + status);
		if ((status & 0xFu) != 0)
		{
			GSStatic.cinema_work_.status &= 4294967280u;
			GSStatic.cinema_work_.step0 = 0;
		}
		GSStatic.cinema_work_.status |= status;
	}

	public static uint Cinema_get_status()
	{
		Debug.Log("Cinema_get_status");
		return GSStatic.cinema_work_.status;
	}

	public static void Cinema_clear_status(uint status)
	{
		Debug.Log("Cinema_clear_status  status:" + status);
		GSStatic.cinema_work_.status &= ~status;
	}

	public static void Cinema_set_frame_top(uint frame_top)
	{
		Debug.Log("Cinema_set_frame_top  frame_top:" + frame_top);
		if (frame_top == 65535)
		{
			frame_top = 0u;
		}
		GSStatic.cinema_work_.frame_top = (short)frame_top;
	}

	public static void Cinema_set_frame_end(uint frame_end)
	{
		Debug.Log("Cinema_set_frame_end  frame_end:" + frame_end);
		FILM_TBL fILM_TBL = film_tbl[GSStatic.cinema_work_.film_no];
		if (frame_end >= fILM_TBL.film_num - 1)
		{
			frame_end = (uint)(fILM_TBL.film_num - 1);
		}
		GSStatic.cinema_work_.frame_end = (short)frame_end;
	}

	public static void Cinema_set_frame(uint frame_set)
	{
		Debug.Log("Cinema_set_frame  frame_set:" + frame_set);
		if (frame_set > GSStatic.cinema_work_.frame_end)
		{
			frame_set = (uint)GSStatic.cinema_work_.frame_end;
		}
		GSStatic.cinema_work_.frame_set = (short)frame_set;
	}

	public static uint Cinema_get_frame()
	{
		Debug.Log("Cinema_get_frame ");
		return 0u;
	}

	public static void Cinema_set_frame_add(uint add)
	{
		Debug.Log("Cinema_set_frame_add  add:" + add);
		GSStatic.cinema_work_.frame_add = (short)add;
	}

	public static void Cinema_set_parframe(uint par)
	{
		Debug.Log("Cinema_set_parframe  par:" + par);
		FILM_TBL fILM_TBL = film_tbl[GSStatic.cinema_work_.film_no];
		uint film_num = (uint)fILM_TBL.film_num;
		switch (par)
		{
		case 0u:
			GSStatic.cinema_work_.frame_set = 0;
			break;
		default:
			GSStatic.cinema_work_.frame_set = (short)film_num;
			break;
		case 1u:
		case 2u:
		case 3u:
		case 4u:
		case 5u:
		case 6u:
		case 7u:
		case 8u:
		case 9u:
		case 10u:
		case 11u:
		case 12u:
		case 13u:
		case 14u:
		case 15u:
		case 16u:
		case 17u:
		case 18u:
		case 19u:
		case 20u:
		case 21u:
		case 22u:
		case 23u:
		case 24u:
		case 25u:
		case 26u:
		case 27u:
		case 28u:
		case 29u:
		case 30u:
		case 31u:
		case 32u:
		case 33u:
		case 34u:
		case 35u:
		case 36u:
		case 37u:
		case 38u:
		case 39u:
		case 40u:
		case 41u:
		case 42u:
		case 43u:
		case 44u:
		case 45u:
		case 46u:
		case 47u:
		case 48u:
		case 49u:
		case 50u:
		case 51u:
		case 52u:
		case 53u:
		case 54u:
		case 55u:
		case 56u:
		case 57u:
		case 58u:
		case 59u:
		case 60u:
		case 61u:
		case 62u:
		case 63u:
		case 64u:
		case 65u:
		case 66u:
		case 67u:
		case 68u:
		case 69u:
		case 70u:
		case 71u:
		case 72u:
		case 73u:
		case 74u:
		case 75u:
		case 76u:
		case 77u:
		case 78u:
		case 79u:
		case 80u:
		case 81u:
		case 82u:
		case 83u:
		case 84u:
		case 85u:
		case 86u:
		case 87u:
		case 88u:
		case 89u:
		case 90u:
		case 91u:
		case 92u:
		case 93u:
		case 94u:
		case 95u:
		case 96u:
		case 97u:
		case 98u:
		case 99u:
			GSStatic.cinema_work_.frame_set = (short)(film_num * 100 / par);
			break;
		}
	}

	public static void cinema_end()
	{
		ConfrontWithMovie.instance.controller.auto_unload = true;
		ConfrontWithMovie.instance.controller.one_step = false;
		GSStatic.cinema_work_.set_type = 0;
		GSStatic.cinema_work_.film_no = 0;
		GSStatic.cinema_work_.bg_no = 0;
		GSStatic.cinema_work_.sw = 0;
		GSStatic.cinema_work_.step0 = 0;
		GSStatic.cinema_work_.step1 = 0;
		GSStatic.cinema_work_.plt = 0;
		GSStatic.cinema_work_.win_type = 0;
		GSStatic.cinema_work_.frame_add = 0;
		GSStatic.cinema_work_.frame_top = 0;
		GSStatic.cinema_work_.frame_end = 0;
		GSStatic.cinema_work_.frame_now = 0f;
		GSStatic.cinema_work_.frame_set = 0;
		GSStatic.cinema_work_.status = 0u;
		GSStatic.cinema_work_.movie_type = 0;
	}
}
