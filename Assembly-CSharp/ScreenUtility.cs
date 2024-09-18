using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class ScreenUtility
{
	private struct RECT
	{
		public int left;

		public int top;

		public int right;

		public int bottom;

		public RECT(int in_left, int in_top, int in_right, int in_bottom)
		{
			left = in_left;
			top = in_top;
			right = in_right;
			bottom = in_bottom;
		}
	}

	private struct POINT
	{
		public int x;

		public int y;
	}

	private const int kDefaultResolutionWidth = 1280;

	private const int kDefaultResolutionHeight = 720;

	private const int HWND_TOP = 0;

	private const int HWND_BOTTOM = 1;

	private const int SWP_NOSIZE = 1;

	private const int SWP_NOMOVE = 2;

	private const int SWP_NOZORDER = 4;

	private const int SWP_NOREDRAW = 8;

	private const int SWP_NOACTIVATE = 16;

	private const int SWP_FRAMECHANGED = 32;

	private const int SWP_SHOWWINDOW = 64;

	private const int SWP_HIDEWINDOW = 128;

	private const int SWP_NOCOPYBITS = 256;

	private const int SWP_NOOWNERZORDER = 512;

	private const int SWP_NOSENDCHANGING = 1024;

	private const int SWP_DEFERERASE = 8192;

	private const int SWP_ASYNCWINDOWPOS = 16384;

	private const int SW_SHOWNORMAL = 1;

	private const int SW_SHOWMINIMIZED = 2;

	private const int SW_SHOWMAXIMIZED = 3;

	private static int winpos_x_;

	private static int winpos_y_;

	private static bool window_mode_;

	private const int GWL_STYLE = -16;

	public static int winpos_x
	{
		get
		{
			return winpos_x_;
		}
	}

	public static int winpos_y
	{
		get
		{
			return winpos_y_;
		}
	}

	[DllImport("User32.dll")]
	private static extern bool GetCursorPos(ref POINT point);

	[DllImport("user32.dll")]
	private static extern bool GetWindowRect(IntPtr hWnd, ref RECT rect);

	[DllImport("user32.dll")]
	private static extern bool GetClientRect(IntPtr hWnd, ref RECT rect);

	[DllImport("user32.dll")]
	private static extern IntPtr SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int y, int w, int h, int wFlags);

	[DllImport("user32.dll")]
	private static extern int MoveWindow(IntPtr hWnd, int x, int y, int nWidth, int nHeight, int bRepaint);

	[DllImport("user32.dll")]
	private static extern int ShowWindow(IntPtr hWnd, int command);

	[DllImport("user32")]
	private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

	[DllImport("user32")]
	private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwLong);

	public static void SetVsync(int count)
	{
		if (count < 0 || count > 1)
		{
			count = 0;
		}
		QualitySettings.vSyncCount = count;
	}

	public static void SetResolution(int resolution_w, int resolution_h, bool in_window_mode)
	{
		Screen.SetResolution(resolution_w, resolution_h, in_window_mode);
		window_mode_ = in_window_mode;
		if (!window_mode_)
		{
			IntPtr windowHandle = WindowAPI.WindowHandle;
			if (windowHandle == IntPtr.Zero)
			{
				windowHandle = WindowAPI.GetWindowHandle();
			}
			RECT rect = new RECT(0, 0, 1280, 720);
			RECT rect2 = new RECT(0, 0, 1280, 720);
			GetWindowRect(windowHandle, ref rect);
			GetClientRect(windowHandle, ref rect2);
			int nWidth = rect.right - rect.left - (rect2.right - rect2.left) + resolution_w;
			int nHeight = rect.bottom - rect.top - (rect2.bottom - rect2.top) + resolution_h;
			winpos_x_ = rect.left;
			winpos_y_ = rect.top;
			MoveWindow(windowHandle, winpos_x_, winpos_y_, nWidth, nHeight, 1);
		}
	}

	public static void MoveWindow(int resolution_w, int resolution_h, bool in_window_mode)
	{
		IntPtr windowHandle = WindowAPI.WindowHandle;
		if (windowHandle == IntPtr.Zero)
		{
			windowHandle = WindowAPI.GetWindowHandle();
		}
		RECT rect = new RECT(0, 0, 1280, 720);
		RECT rect2 = new RECT(0, 0, 1280, 720);
		GetWindowRect(windowHandle, ref rect);
		GetClientRect(windowHandle, ref rect2);
		int nWidth = rect.right - rect.left - (rect2.right - rect2.left) + resolution_w;
		int nHeight = rect.bottom - rect.top - (rect2.bottom - rect2.top) + resolution_h;
		winpos_x_ = rect.left;
		winpos_y_ = rect.top;
		if (rect2.right != resolution_w)
		{
			POINT point = default(POINT);
			GetCursorPos(ref point);
			int num = rect.right - (rect.right - rect.left) / 2;
			winpos_x_ += (rect2.right - resolution_w - (num - point.x)) / 2;
		}
		MoveWindow(windowHandle, winpos_x_, winpos_y_, nWidth, nHeight, 1);
	}

	public static void Init()
	{
		IntPtr windowHandle = WindowAPI.WindowHandle;
		if (windowHandle == IntPtr.Zero)
		{
			windowHandle = WindowAPI.GetWindowHandle();
		}
		RECT rect = new RECT(0, 0, 1280, 720);
		if (GetWindowRect(windowHandle, ref rect))
		{
			Debug.Log("GetWindowRect Success " + rect.left + " " + rect.top + " " + rect.right + " " + rect.bottom);
		}
		else
		{
			Debug.Log("GetWindowRect Error!! " + rect.left + " " + rect.top + " " + rect.right + " " + rect.bottom);
		}
		winpos_x_ = rect.left;
		winpos_y_ = rect.top;
	}
}
