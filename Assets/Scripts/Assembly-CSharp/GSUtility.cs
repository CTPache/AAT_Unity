using System;
using System.Globalization;

public static class GSUtility
{
	private struct hl
	{
		public short val;

		public byte l
		{
			get
			{
				return (byte)((uint)val & 0xFFu);
			}
			set
			{
				val &= -256;
				val |= value;
			}
		}

		public sbyte h
		{
			get
			{
				return (sbyte)(val >> 8);
			}
			set
			{
				val &= 255;
				val |= (short)(value << 8);
			}
		}
	}

	private struct POS_W
	{
		public hl b;

		public short l
		{
			get
			{
				return b.val;
			}
			set
			{
				b.val = value;
			}
		}
	}

	public static bool CkTwoLine(GSPoint pt0, GSPoint pt1, GSPoint pt2, GSPoint pt3)
	{
		int num = pt0.x - pt1.x;
		int num2 = pt0.y - pt1.y;
		int num3 = pt2.x - pt3.x;
		int num4 = pt2.y - pt3.y;
		int num5 = num * num4 - num2 * num3;
		if (num5 == 0)
		{
			return false;
		}
		int num6 = pt1.x - pt3.x;
		int num7 = pt1.y - pt3.y;
		int num8 = num7 * num3 - num6 * num4;
		int num9 = num7 * num - num6 * num2;
		if (((num5 > 0 && num8 >= 0 && num8 <= num5) || (num5 < 0 && num8 >= num5 && num8 <= 0)) && ((num5 > 0 && num9 >= 0 && num9 <= num5) || (num5 < 0 && num9 >= num5 && num9 <= 0)))
		{
			return true;
		}
		return false;
	}

	public static bool ObjHitCheck2(GSRect rect, GSPoint4 point)
	{
		if (Hit_ck_point4(new GSPoint((ushort)rect.x, (ushort)rect.y), point))
		{
			return true;
		}
		GSPoint4 gSPoint = default(GSPoint4);
		gSPoint.x0 = (gSPoint.x3 = (ushort)rect.x);
		gSPoint.y0 = (gSPoint.y1 = (ushort)rect.y);
		gSPoint.x1 = (gSPoint.x2 = (ushort)(rect.x + rect.w));
		gSPoint.y2 = (gSPoint.y3 = (ushort)(rect.y + rect.h));
		for (int i = 0; i < 4; i++)
		{
			GSPoint point2 = point.GetPoint(i);
			GSPoint point3 = point.GetPoint((i + 1) % 4);
			for (int j = 0; j < 4; j++)
			{
				GSPoint point4 = gSPoint.GetPoint(j);
				GSPoint point5 = gSPoint.GetPoint((j + 1) % 4);
				if (CkTwoLine(point2, point3, point4, point5))
				{
					return true;
				}
			}
		}
		return false;
	}

	public static bool Hit_ck_point4(GSPoint p, GSPoint4 cp)
	{
		int x = cp.x0;
		int y = cp.y0;
		int num = p.x - x;
		int num2 = p.y - y;
		int num3 = cp.x1 - x;
		int num4 = cp.y1 - y;
		int num5 = cp.x3 - x;
		int num6 = cp.y3 - y;
		if (num3 * num2 < num4 * num || num5 * num2 > num6 * num)
		{
			return false;
		}
		num -= cp.x2 - x;
		num2 -= cp.y2 - y;
		num3 -= cp.x2 - x;
		num4 -= cp.y2 - y;
		num5 -= cp.x2 - x;
		num6 -= cp.y2 - y;
		if (num3 * num2 > num4 * num || num5 * num2 < num6 * num)
		{
			return false;
		}
		return true;
	}

	public static byte Rnd()
	{
		POS_W pOS_W = default(POS_W);
		POS_W pOS_W2 = default(POS_W);
		pOS_W.l = (short)GSStatic.global_work_.Random_seed;
		pOS_W2.l = (short)(pOS_W.l * 3);
		pOS_W.b.l += (byte)pOS_W2.b.h;
		pOS_W.b.h = pOS_W2.b.h;
		GSStatic.global_work_.Random_seed = (ushort)pOS_W.l;
		return pOS_W.b.l;
	}

	public static string GetPlatformResourceName()
	{
		return "_pc";
	}

	public static string GetResourceNameLanguage(Language language)
	{
		switch (language)
		{
		default:
			return string.Empty;
		case Language.USA:
			return "u";
		}
	}

	public static Language GetLanguageLayoutType(Language language)
	{
		switch (language)
		{
		case Language.JAPAN:
		case Language.KOREA:
		case Language.CHINA_S:
		case Language.CHINA_T:
			return Language.JAPAN;
		default:
			return Language.USA;
		}
	}

	public static int GetLanguageSlotNum(int slot_num, Language language)
	{
		return slot_num + 10 * (int)language;
	}

	public static string GetScenarioLanguage(Language language)
	{
		switch (language)
		{
		case Language.JAPAN:
			return string.Empty;
		case Language.USA:
			return "_u";
		case Language.FRANCE:
			return "_f";
		case Language.GERMAN:
			return "_g";
		case Language.KOREA:
			return "_k";
		case Language.CHINA_S:
			return "_s";
		case Language.CHINA_T:
			return "_t";
		default:
			return "_u";
		}
	}

	public static DateTime DateTimeParse(string time, Language language)
	{
		string s = time.Replace("\n", " ");
		try
		{
			switch (language)
			{
			case Language.FRANCE:
				return DateTime.Parse(s, new CultureInfo("fr-FR"), DateTimeStyles.None);
			case Language.GERMAN:
				return DateTime.Parse(s, new CultureInfo("de-DE"), DateTimeStyles.None);
			default:
				return DateTime.ParseExact(s, "yyyy/MM/dd HH:mm:ss", null);
			}
		}
		catch
		{
			return DateTime.Parse("1900/01/01 00:00:00");
		}
	}
}
