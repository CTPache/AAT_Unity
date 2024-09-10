using System;

namespace SaveStruct
{
	[Serializable]
	public struct BgWork
	{
		public ushort bg_no;

		public ushort bg_no_old;

		public byte bg_flag;

		public float bg_pos_x;

		public float bg_pos_y;

		public bool bg_black;

		public bool negaposi;

		public bool negaposi_sub;

		public GSColor color;

		public ushort bg_parts;

		public bool bg_parts_enabled;

		public bool reverse;

		public static void New(out BgWork bg_work)
		{
			bg_work = default(BgWork);
		}

		public void CopyFrom(BgSaveData src)
		{
			bg_no = (ushort)bgCtrl.instance.bg_no;
			bg_no_old = (ushort)bgCtrl.instance.bg_no_old;
			bg_flag = bgCtrl.instance.Bg256_SP_Flag;
			bg_pos_x = bgCtrl.instance.bg_pos_x;
			bg_pos_y = bgCtrl.instance.bg_pos_y;
			bg_black = src.bg_black;
			negaposi = src.negaposi;
			negaposi_sub = src.negaposi_sub;
			color.CopyFrom(bgCtrl.instance.getColor());
			bg_parts = src.bg_parts;
			bg_parts_enabled = src.bg_parts_enabled;
			reverse = bgCtrl.instance.is_reverse;
		}

		public void CopyTo(BgSaveData dest)
		{
			dest.bg_no = bg_no;
			dest.bg_flag = bg_flag;
			dest.bg_no_old = bg_no_old;
			dest.bg_pos_x = bg_pos_x;
			dest.bg_pos_y = bg_pos_y;
			dest.bg_black = bg_black;
			dest.negaposi = negaposi;
			dest.negaposi_sub = negaposi_sub;
			color.CopyTo(ref dest.color);
			dest.bg_parts = bg_parts;
			dest.bg_parts_enabled = bg_parts_enabled;
			dest.reverse = reverse;
		}
	}
}
