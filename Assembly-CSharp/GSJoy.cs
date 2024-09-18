public static class GSJoy
{
	public const int NA_BUTTON_A = 1;

	public const int NA_BUTTON_B = 2;

	public const int NA_BUTTON_START = 4;

	public const int NA_BUTTON_SELECT = 8;

	public const int NA_BUTTON_LEFT = 16;

	public const int NA_BUTTON_RIGHT = 32;

	public const int NA_BUTTON_UP = 64;

	public const int NA_BUTTON_DOWN = 128;

	public const int NA_BUTTON_R = 256;

	public const int NA_BUTTON_L = 512;

	public const int NA_BUTTON_X = 1024;

	public const int NA_BUTTON_Y = 2048;

	public const int PAD_BUTTON_A = 1;

	public const int A_BUTTON = 1;

	public const int PAD_BUTTON_B = 2;

	public const int B_BUTTON = 2;

	public const int PAD_BUTTON_X = 1024;

	public const int PAD_BUTTON_Y = 2048;

	public const int PAD_BUTTON_R = 256;

	public const int PAD_BUTTON_L = 512;

	public const int START_BUTTON = 4;

	public const int SELECT_BUTTON = 8;

	public const int PAD_KEY_LEFT = 16;

	public const int PAD_KEY_RIGHT = 32;

	public const int PAD_KEY_UP = 64;

	public const int PAD_KEY_DOWN = 128;

	public const int R_KEY = 32;

	public const int L_KEY = 16;

	public const int U_KEY = 64;

	public const int D_KEY = 128;

	public const int R_BUTTON = 256;

	public const int L_BUTTON = 512;

	public const int X_BUTTON = 1024;

	public const int Y_BUTTON = 2048;

	private static readonly KeyType[] key_table;

	static GSJoy()
	{
		key_table = new KeyType[12]
		{
			KeyType.A,
			KeyType.B,
			KeyType.Start,
			KeyType.Select,
			KeyType.Left,
			KeyType.Right,
			KeyType.Up,
			KeyType.Down,
			KeyType.R,
			KeyType.L,
			KeyType.X,
			KeyType.Y
		};
	}

	public static bool Trg(int flags)
	{
		int num = 0;
		while (flags != 0 && num < key_table.Length)
		{
			if (((uint)flags & (true ? 1u : 0u)) != 0 && padCtrl.instance.GetKeyDown(key_table[num]))
			{
				return true;
			}
			flags >>= 1;
			num++;
		}
		return false;
	}

	public static bool On(int flags)
	{
		int num = 0;
		while (flags != 0 && num < key_table.Length)
		{
			if (((uint)flags & (true ? 1u : 0u)) != 0 && padCtrl.instance.GetKeyDown(key_table[num]))
			{
				return true;
			}
			flags >>= 1;
			num++;
		}
		return false;
	}
}
