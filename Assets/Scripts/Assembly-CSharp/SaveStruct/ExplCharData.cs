using System;

namespace SaveStruct
{
	[Serializable]
	public struct ExplCharData
	{
		public byte id;

		public byte blink;

		public byte timer;

		public byte speed;

		public byte move;

		public byte status;

		public byte dot;

		public byte dot_now;

		public ushort para0;

		public ushort para1;

		public float move_x;

		public float move_y;

		public ushort para2;

		public ushort oam;

		public uint vram_addr;

		public static void New(out ExplCharData expl_char_data)
		{
			expl_char_data = default(ExplCharData);
		}

		public void CopyFrom(ref global::ExplCharData src)
		{
			id = src.id;
			blink = src.blink;
			timer = src.timer;
			speed = src.speed;
			move = src.move;
			status = src.status;
			dot = src.dot;
			dot_now = src.dot_now;
			para0 = src.para0;
			para1 = src.para1;
			move_x = src.move_x;
			move_y = src.move_y;
			para2 = src.para2;
			oam = src.oam;
			vram_addr = src.vram_addr;
		}

		public void CopyTo(ref global::ExplCharData dest)
		{
			dest.id = id;
			dest.blink = blink;
			dest.timer = timer;
			dest.speed = speed;
			dest.move = move;
			dest.status = status;
			dest.dot = dot;
			dest.dot_now = dot_now;
			dest.para0 = para0;
			dest.para1 = para1;
			dest.move_x = move_x;
			dest.move_y = move_y;
			dest.para2 = para2;
			dest.oam = oam;
			dest.vram_addr = vram_addr;
		}
	}
}
