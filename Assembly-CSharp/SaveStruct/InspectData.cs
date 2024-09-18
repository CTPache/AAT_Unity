using System;

namespace SaveStruct
{
	[Serializable]
	public struct InspectData
	{
		public uint message;

		public uint place;

		public uint item;

		public uint x0;

		public uint y0;

		public uint x1;

		public uint y1;

		public uint x2;

		public uint y2;

		public uint x3;

		public uint y3;

		public static void New(out InspectData inspect_data)
		{
			inspect_data = default(InspectData);
		}

		public void CopyFrom(ref INSPECT_DATA src)
		{
			message = src.message;
			place = src.place;
			item = src.item;
			x0 = src.x0;
			y0 = src.y0;
			x1 = src.x1;
			y1 = src.y1;
			x2 = src.x2;
			y2 = src.y2;
			x3 = src.x3;
			y3 = src.y3;
		}

		public void CopyTo(ref INSPECT_DATA dest)
		{
			dest.message = message;
			dest.place = place;
			dest.item = item;
			dest.x0 = x0;
			dest.y0 = y0;
			dest.x1 = x1;
			dest.y1 = y1;
			dest.x2 = x2;
			dest.y2 = y2;
			dest.x3 = x3;
			dest.y3 = y3;
		}
	}
}
