public class SubWindowCursor
{
	private delegate void CursorProc(SubWindowCursor cursor);

	private const byte ANIMATION_FRAME = 4;

	private short x;

	private short y;

	private short w;

	private short h;

	private short now_x;

	private short now_y;

	private short now_w;

	private short now_h;

	private short add_x0;

	private short add_y0;

	private short add_x1;

	private short add_y1;

	public byte disp_off;

	public byte Rno_0;

	public byte Rno_1;

	public byte timer;

	private static readonly CursorProc[] proc_table;

	static SubWindowCursor()
	{
		proc_table = new CursorProc[4] { Init, Appear, Main, Exit };
	}

	public void Proc()
	{
		proc_table[Rno_0](this);
	}

	public void Set(ushort x, ushort y, ushort w, ushort h, byte in_flag)
	{
		if (in_flag == 0)
		{
			this.x = (short)x;
			this.y = (short)y;
			this.w = (short)w;
			this.h = (short)h;
			now_x = 0;
			now_y = 0;
			now_w = 256;
			now_h = 192;
			short num = now_x;
			short num2 = now_y;
			short num3 = (short)(now_x + now_w);
			short num4 = (short)(now_y + now_h);
			short num5 = this.x;
			short num6 = this.y;
			short num7 = (short)(this.x + this.w);
			short num8 = (short)(this.y + this.h);
			add_x0 = (short)((num5 - num) / 4);
			add_y0 = (short)((num6 - num2) / 4);
			add_x1 = (short)((num7 - num3) / 4);
			add_y1 = (short)((num8 - num4) / 4);
		}
		else
		{
			now_x = (short)x;
			now_y = (short)y;
			now_w = (short)w;
			now_h = (short)h;
		}
	}

	private bool CalcPosition()
	{
		short num = now_x;
		short num2 = now_y;
		short num3 = (short)(now_x + now_w);
		short num4 = (short)(now_y + now_h);
		num += add_x0;
		num2 += add_y0;
		num3 += add_x1;
		num4 += add_y1;
		now_x = num;
		now_y = num2;
		now_w = (short)(num3 - num);
		now_h = (short)(num4 - num2);
		timer++;
		if (timer >= 4)
		{
			now_x = x;
			now_y = y;
			now_w = w;
			now_h = h;
			return true;
		}
		return false;
	}

	private static void Init(SubWindowCursor cursor)
	{
		cursor.now_x = 0;
		cursor.now_y = 0;
		cursor.now_w = 256;
		cursor.now_h = 192;
		cursor.disp_off = 1;
		cursor.timer = 0;
		cursor.Rno_0 = 1;
		cursor.Rno_1 = 0;
	}

	private static void Appear(SubWindowCursor cursor)
	{
		byte rno_ = cursor.Rno_1;
		if (rno_ != 0 && rno_ == 1)
		{
			cursor.disp_off = 0;
			if (cursor.CalcPosition())
			{
				cursor.Rno_0 = 2;
				cursor.Rno_1 = 0;
			}
		}
	}

	private static void Main(SubWindowCursor cursor)
	{
	}

	private static void Exit(SubWindowCursor cursor)
	{
		cursor.disp_off = 1;
	}
}
