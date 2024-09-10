using System;

namespace SaveStruct
{
	[Serializable]
	public struct CinemaWork
	{
		public ushort set_type;

		public ushort film_no;

		public byte bg_no;

		public byte sw;

		public byte step0;

		public byte step1;

		public byte plt;

		public byte win_type;

		public short frame_add;

		public short frame_top;

		public short frame_end;

		public float frame_now;

		public short frame_set;

		public uint status;

		public int movie_type;

		public static void New(out CinemaWork cinema_work)
		{
			cinema_work = default(CinemaWork);
		}

		public void CopyFrom(global::CinemaWork src)
		{
			set_type = src.set_type;
			film_no = src.film_no;
			bg_no = src.bg_no;
			sw = src.sw;
			step0 = src.step0;
			step1 = src.step1;
			plt = src.plt;
			win_type = src.win_type;
			frame_add = src.frame_add;
			frame_top = src.frame_top;
			frame_end = src.frame_end;
			frame_now = src.frame_now;
			frame_set = src.frame_set;
			status = src.status;
			movie_type = src.movie_type;
		}

		public void CopyTo(global::CinemaWork dest)
		{
			dest.set_type = set_type;
			dest.film_no = film_no;
			dest.bg_no = bg_no;
			dest.sw = sw;
			dest.step0 = step0;
			dest.step1 = step1;
			dest.plt = plt;
			dest.win_type = win_type;
			dest.frame_add = frame_add;
			dest.frame_top = frame_top;
			dest.frame_end = frame_end;
			dest.frame_now = frame_now;
			dest.frame_set = frame_set;
			dest.status = status;
			dest.movie_type = movie_type;
		}
	}
}
