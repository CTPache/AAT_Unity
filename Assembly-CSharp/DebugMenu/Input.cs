using System;

namespace DebugMenu
{
	[Flags]
	public enum Input
	{
		None = 0,
		Left = 1,
		Right = 2,
		Up = 4,
		Down = 8,
		Decide = 0x10,
		Cancel = 0x20,
		BitSize = 6
	}
}
