using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class WindowAPI
{
	private static HandleRef WindowRef_;

	private static IntPtr WindowHandle_ = IntPtr.Zero;

	private static IntPtr oldWndProcPtr_ = IntPtr.Zero;

	private static IntPtr newWndProcPtr_ = IntPtr.Zero;

	private static WndProcDelegate newWndProc_;

	private static bool is_init_ = false;

	private static bool is_proc_ = false;

	private static bool is_mini_ = false;

	private static bool is_close_ = false;

	private static bool is_paint_ = false;

	private static bool is_move_ = false;

	private const int WH_GETMESSAGE = 3;

	private const int WM_NCDESTROY = 130;

	private const int WM_DESTROY = 2;

	private const int WM_CLOSE = 16;

	private const int WM_PAINT = 15;

	private const int WM_NCLBUTTONDOWN = 161;

	private const int WM_NCRBUTTONDOWN = 164;

	private const int WM_SIZE = 5;

	private const int WM_MOVE = 3;

	private const int SIZE_MAXHIDE = 4;

	private const int SIZE_MAXIMIZED = 2;

	private const int SIZE_MAXSHOW = 3;

	private const int SIZE_MINIMIZED = 1;

	private const int SIZE_RESTORED = 0;

	private const int GWL_WNDPROC = -4;

	public static IntPtr oldWndProcPtr
	{
		get
		{
			return oldWndProcPtr_;
		}
	}

	public static bool is_proc
	{
		get
		{
			return is_proc_;
		}
	}

	public static bool is_init
	{
		get
		{
			return is_init_;
		}
	}

	public static bool is_mini
	{
		get
		{
			return is_mini_;
		}
	}

	public static bool is_close
	{
		get
		{
			return is_close_;
		}
	}

	public static bool is_paint
	{
		get
		{
			return is_paint_;
		}
	}

	public static bool is_move
	{
		get
		{
			return is_move_;
		}
	}

	public static IntPtr WindowHandle
	{
		get
		{
			return WindowHandle_;
		}
	}

	[DllImport("user32.dll")]
	private static extern IntPtr GetWindowLong(HandleRef hWnd, int nIndex);

	[DllImport("user32.dll")]
	private static extern IntPtr GetWindowLongPtr(HandleRef hWnd, int nIndex);

	[DllImport("user32.dll")]
	private static extern IntPtr SetWindowLong(HandleRef hWnd, int nIndex, IntPtr dwNewLong);

	[DllImport("user32.dll")]
	private static extern IntPtr SetWindowLongPtr(HandleRef hWnd, int nIndex, IntPtr dwNewLong);

	[DllImport("user32.dll")]
	private static extern IntPtr DefWindowProc(IntPtr hWnd, uint wMsg, IntPtr wParam, IntPtr lParam);

	[DllImport("user32.dll")]
	private static extern IntPtr CallWindowProc(IntPtr lpPrevWndFunc, IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

	[DllImport("user32.dll")]
	private static extern IntPtr GetActiveWindow();

	[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
	private static extern IntPtr GetForegroundWindow();

	public static IntPtr GetWindowLong86_64(HandleRef hWnd, int nIndex)
	{
		if (IntPtr.Size == 8)
		{
			return GetWindowLongPtr(hWnd, nIndex);
		}
		return GetWindowLong(hWnd, nIndex);
	}

	public static IntPtr SetWindowLong86_64(HandleRef hWnd, int nIndex, IntPtr dwNewLong)
	{
		if (IntPtr.Size == 8)
		{
			return SetWindowLongPtr(hWnd, nIndex, dwNewLong);
		}
		return SetWindowLong(hWnd, nIndex, dwNewLong);
	}

	private static IntPtr wndProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam)
	{
		is_proc_ = true;
		if (msg == 16)
		{
			is_close_ = true;
			return IntPtr.Zero;
		}
		is_move_ = false;
		if (msg == 3)
		{
			is_move_ = true;
		}
		is_paint_ = false;
		if (msg == 15)
		{
			is_paint_ = true;
		}
		if (is_move_)
		{
			ScreenUtility.MoveWindow((int)GSStatic.option_work.resolution_w, (int)GSStatic.option_work.resolution_h, (GSStatic.option_work.window_mode != 0) ? true : false);
		}
		if (msg == 161 || msg == 164)
		{
			padCtrl.instance.InputResetInputAxis();
		}
		if (msg == 5)
		{
			is_mini_ = false;
			Debug.Log("WindowAPI.wndProc WM_SIZE wParam:" + wParam.ToInt32());
			if (wParam.ToInt32() == 1)
			{
				Debug.Log("WindowAPI.wndProc SIZE_MINIMIZED");
				is_mini_ = true;
			}
		}
		return CallWindowProc(oldWndProcPtr_, hWnd, msg, wParam, lParam);
	}

	public static IntPtr GetWindowHandle()
	{
		return GetActiveWindow();
	}

	public static void Init()
	{
		if (!is_init_)
		{
			is_init_ = true;
			WindowHandle_ = GetWindowHandle();
			WindowRef_ = new HandleRef(null, WindowHandle_);
			newWndProc_ = wndProc;
			newWndProcPtr_ = Marshal.GetFunctionPointerForDelegate(newWndProc_);
			oldWndProcPtr_ = SetWindowLong86_64(WindowRef_, -4, newWndProcPtr_);
		}
	}

	public static void Term()
	{
		if (is_init_)
		{
			is_init_ = false;
			is_proc_ = false;
			SetWindowLong86_64(WindowRef_, -4, oldWndProcPtr_);
			WindowRef_ = new HandleRef(null, IntPtr.Zero);
			oldWndProcPtr_ = IntPtr.Zero;
			newWndProcPtr_ = IntPtr.Zero;
			newWndProc_ = null;
		}
	}
}
