using System;
using System.Collections;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using UnityEngine;

public class Window : MonoBehaviour
{
	public struct RECT
	{
		public int x;

		public int y;

		public int z;

		public int w;
	}

	private struct Flash
	{
		public uint a;

		public int b;

		public int c;

		public int d;

		public int e;
	}

	public bool QuickAutoBorderless = true;

	public bool FullyAutoBorderless;

	public bool SilenceWarnings;

	public bool AutoFixAfterResizing = true;

	public bool FocusResetOnClick;

	public bool StabilizeQuickChanges = true;

	public bool AllowSizeResettingBeforeExit;

	public Vector2 SizeReset = new Vector2(120f, 90f);

	public static bool DoneCalculating;

	public static Window Local;

	public static Vector4 Limitations = new Vector4(0f, 4096f, 0f, 4096f);

	public static RECT Position;

	public static bool MoveWindow;

	public static int ID;

	public static int[] ChildId = new int[0];

	public static Rect[] ChildOffset = new Rect[0];

	public static Rect Maximized;

	private static bool Borders;

	private static Vector4 MoveOffSet;

	private static Vector3 OldOffSet;

	private static int Action;

	private static Vector2 CursorUpdate = Vector3.zero;

	private static Vector2 ClientPosition;

	private static float AspectRation;

	private static Rect ResetSize;

	private static Resolution LastResolution;

	private static int Loop;

	private static Rect StoredRect;

	private static bool StoredBorders;

	private static Vector2 StoredBorderSize;

	private static Vector2 PermanentBorderSize;

	private bool FocusReset = true;

	private bool MinimizeReset = true;

	private bool Once;

	private bool Once1;

	[DllImport("USER32.DLL")]
	public static extern int SetWindowLong(IntPtr hWnd, int nIndex, uint dwNewLong);

	[DllImport("USER32.DLL")]
	public static extern long GetWindowLong(IntPtr hWnd, int nIndex);

	[DllImport("user32.dll")]
	private static extern bool ShowWindow(int hwnd, int nCmdShow);

	[DllImport("user32.dll")]
	private static extern int GetActiveWindow();

	[DllImport("user32.dll")]
	private static extern int SetWindowPos(int hwnd, int hwndInsertAfter, int x, int y, int cx, int cy, int uFlags);

	[DllImport("user32.dll")]
	private static extern bool GetWindowRect(int hWnd, ref RECT NewRect);

	[DllImport("user32.dll")]
	[return: MarshalAs(UnmanagedType.Bool)]
	private static extern bool FlashWindowEx(ref Flash flash);

	[DllImport("user32.dll")]
	private static extern long GetWindowText(int hWnd, StringBuilder text, int count);

	[DllImport("user32.dll")]
	private static extern bool SetWindowText(int hwnd, string lpString);

	[DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
	private static extern IntPtr FindWindowByCaption(IntPtr ZeroOnly, string lpWindowName);

	[DllImport("user32.dll")]
	public static extern bool SetFocus(int hWnd);

	[DllImport("user32.dll")]
	public static extern IntPtr GetDesktopWindow();

	[DllImport("user32.dll", SetLastError = true)]
	private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

	[DllImport("user32.dll", CharSet = CharSet.Unicode)]
	public static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, string lclassName, string windowTitle);

	[DllImport("user32.dll", SetLastError = true)]
	public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

	[DllImport("user32.dll")]
	public static extern int SetParent(int child, int parent);

	[DllImport("user32.dll")]
	[return: MarshalAs(UnmanagedType.Bool)]
	public static extern bool IsWindowVisible(IntPtr hWnd);

	[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
	public static extern int GetForegroundWindow();

	[DllImport("user32.dll")]
	public static extern int GetFocus();

	[DllImport("user32.dll")]
	[return: MarshalAs(UnmanagedType.Bool)]
	public static extern bool IsIconic(IntPtr hWnd);

	[DllImport("user32.dll", SetLastError = true)]
	public static extern IntPtr SetActiveWindow(IntPtr hWnd);

	[DllImport("user32.dll")]
	[return: MarshalAs(UnmanagedType.Bool)]
	public static extern bool SetForegroundWindow(IntPtr hWnd);

	[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
	public static extern IntPtr GetParent(IntPtr hWnd);

	private static void FlashWindow(int handle, int flags, int count, int timeout)
	{
		Flash flash = default(Flash);
		flash.a = Convert.ToUInt32(Marshal.SizeOf(flash));
		flash.b = handle;
		flash.c = flags;
		flash.d = count;
		flash.e = timeout;
		FlashWindowEx(ref flash);
	}

	private void Awake()
	{
		if (GetActiveWindow() != 0)
		{
			ID = GetActiveWindow();
		}
		else
		{
			ID = ProcessIdByName(UnityEngine.Application.productName);
		}
		Local = this;
		PermanentBorderSize = Vector2.zero;
	}

	private void Start()
	{
		if (!Local)
		{
			Local = this;
		}
		Once = true;
		if (ID == 0)
		{
			if (GetActiveWindow() != 0)
			{
				ID = GetActiveWindow();
			}
			else
			{
				ID = ProcessIdByName(UnityEngine.Application.productName);
			}
		}
		UnityEngine.Screen.fullScreen = false;
		LastResolution = UnityEngine.Screen.currentResolution;
	}

	private void Update()
	{
		if (ID == 0)
		{
			if (GetActiveWindow() != 0)
			{
				ID = GetActiveWindow();
			}
			else
			{
				ID = ProcessIdByName(UnityEngine.Application.productName);
			}
		}
		if (!Local)
		{
			Local = this;
		}
		if (Once && (QuickAutoBorderless || FullyAutoBorderless) && DoneCalculating && Borders && !UnityEngine.Application.isEditor)
		{
			if (QuickAutoBorderless)
			{
				if (ID != 0)
				{
					Once = false;
				}
			}
			else
			{
				Once = false;
			}
			if (QuickAutoBorderless)
			{
				SetWindowLong((IntPtr)ID, -16, 524288u);
				SetWindowPosition(ID, -2, (int)GetRect().x, (int)GetRect().y, (int)GetRect().width, (int)GetRect().height, 64);
				SetWindowLong((IntPtr)ID, -16, 524288u);
				SetWindowPosition(ID, -2, (int)GetRect().x, (int)GetRect().y, (int)GetRect().width, (int)GetRect().height, 64);
			}
			else
			{
				try
				{
					Process.Start(Regex.Replace(UnityEngine.Application.dataPath.ToString().Remove(UnityEngine.Application.dataPath.ToString().Length - 5) + ".exe", "/", "\\"), "-popupwindow");
					if (ID != Process.GetCurrentProcess().Id)
					{
						UnityEngine.Debug.LogWarning("Misleaded processes : " + ID + " and " + Process.GetCurrentProcess().Id);
					}
					Process.GetCurrentProcess().Kill();
				}
				catch (Exception ex)
				{
					UnityEngine.Debug.LogError("Could not reboot, reason : " + ex.ToString());
				}
			}
		}
		OnMonitorResolutionChanged();
		if (PermanentBorderSize == Vector2.zero)
		{
			PermanentBorderSize = GetBorderSize();
		}
		if (FocusResetOnClick)
		{
			if (IsMinimized())
			{
				MinimizeReset = true;
			}
			else if (MinimizeReset)
			{
				SetWindowPosition(ID, 0, (int)ResetSize.x, (int)ResetSize.y, (int)ResetSize.width, (int)ResetSize.height, 96);
				MinimizeReset = false;
			}
			else
			{
				ResetSize = GetRect();
			}
			if (GetForegroundWindow() == ID)
			{
				if (FocusReset)
				{
					SetWindowPosition(ID, 0, (int)GetRect().x, (int)GetRect().y, (int)GetRect().width, (int)GetRect().height, 96);
					FocusReset = false;
				}
			}
			else
			{
				FocusReset = true;
			}
		}
		if (MoveWindow && DoneCalculating)
		{
			SetFocus(ID);
			if (Action == 1)
			{
				SetWindowPosition(ID, 0, (int)((float)System.Windows.Forms.Cursor.Position.X + MoveOffSet.x), (int)((float)System.Windows.Forms.Cursor.Position.Y + MoveOffSet.y) + 1, (int)MoveOffSet.z, (int)MoveOffSet.w, 96);
			}
			if (Action == 2)
			{
				if ((float)(int)(MoveOffSet.z - ((float)System.Windows.Forms.Cursor.Position.X - OldOffSet.x)) > Limitations.y)
				{
					SetWindowPosition(ID, 0, (int)(OldOffSet.x + MoveOffSet.x - (Limitations.y - MoveOffSet.z)), (int)MoveOffSet.y + 1, (int)Limitations.y, (int)MoveOffSet.w, 96);
				}
				else if ((float)(int)(MoveOffSet.z - ((float)System.Windows.Forms.Cursor.Position.X - OldOffSet.x)) < Limitations.x)
				{
					SetWindowPosition(ID, 0, (int)(OldOffSet.x + MoveOffSet.x - (Limitations.x - MoveOffSet.z)), (int)MoveOffSet.y + 1, (int)Limitations.x, (int)MoveOffSet.w, 96);
				}
				else
				{
					SetWindowPosition(ID, 0, (int)((float)System.Windows.Forms.Cursor.Position.X + MoveOffSet.x), (int)MoveOffSet.y + 1, (int)(MoveOffSet.z - ((float)System.Windows.Forms.Cursor.Position.X - OldOffSet.x)), (int)MoveOffSet.w, 96);
				}
			}
			if (Action == 3)
			{
				if ((float)(int)(MoveOffSet.z - ((float)System.Windows.Forms.Cursor.Position.X - OldOffSet.x)) > Limitations.y)
				{
					if ((float)(int)((float)System.Windows.Forms.Cursor.Position.Y - OldOffSet.y + MoveOffSet.w) > Limitations.w)
					{
						SetWindowPosition(ID, 0, (int)(OldOffSet.x + MoveOffSet.x - (Limitations.y - MoveOffSet.z)), (int)MoveOffSet.y + 1, (int)Limitations.y, (int)Limitations.w, 96);
					}
					else if ((float)(int)((float)System.Windows.Forms.Cursor.Position.Y - OldOffSet.y + MoveOffSet.w) < Limitations.z)
					{
						SetWindowPosition(ID, 0, (int)(OldOffSet.x + MoveOffSet.x - (Limitations.y - MoveOffSet.z)), (int)MoveOffSet.y + 1, (int)Limitations.y, (int)Limitations.z, 96);
					}
					else
					{
						SetWindowPosition(ID, 0, (int)(OldOffSet.x + MoveOffSet.x - (Limitations.y - MoveOffSet.z)), (int)MoveOffSet.y + 1, (int)Limitations.y, (int)((float)System.Windows.Forms.Cursor.Position.Y - OldOffSet.y + MoveOffSet.w), 96);
					}
				}
				else if ((float)(int)(MoveOffSet.z - ((float)System.Windows.Forms.Cursor.Position.X - OldOffSet.x)) < Limitations.x)
				{
					if ((float)(int)((float)System.Windows.Forms.Cursor.Position.Y - OldOffSet.y + MoveOffSet.w) > Limitations.w)
					{
						SetWindowPosition(ID, 0, (int)(OldOffSet.x + MoveOffSet.x - (Limitations.x - MoveOffSet.z)), (int)MoveOffSet.y + 1, (int)Limitations.x, (int)Limitations.w, 96);
					}
					else if ((float)(int)((float)System.Windows.Forms.Cursor.Position.Y - OldOffSet.y + MoveOffSet.w) < Limitations.z)
					{
						SetWindowPosition(ID, 0, (int)(OldOffSet.x + MoveOffSet.x - (Limitations.x - MoveOffSet.z)), (int)MoveOffSet.y + 1, (int)Limitations.x, (int)Limitations.z, 96);
					}
					else
					{
						SetWindowPosition(ID, 0, (int)(OldOffSet.x + MoveOffSet.x - (Limitations.x - MoveOffSet.z)), (int)MoveOffSet.y + 1, (int)Limitations.x, (int)((float)System.Windows.Forms.Cursor.Position.Y - OldOffSet.y + MoveOffSet.w), 96);
					}
				}
				else if ((float)(int)((float)System.Windows.Forms.Cursor.Position.Y - OldOffSet.y + MoveOffSet.w) > Limitations.w)
				{
					SetWindowPosition(ID, 0, (int)((float)System.Windows.Forms.Cursor.Position.X + MoveOffSet.x), (int)MoveOffSet.y + 1, (int)(MoveOffSet.z - ((float)System.Windows.Forms.Cursor.Position.X - OldOffSet.x)), (int)Limitations.w, 96);
				}
				else if ((float)(int)((float)System.Windows.Forms.Cursor.Position.Y - OldOffSet.y + MoveOffSet.w) < Limitations.z)
				{
					SetWindowPosition(ID, 0, (int)((float)System.Windows.Forms.Cursor.Position.X + MoveOffSet.x), (int)MoveOffSet.y + 1, (int)(MoveOffSet.z - ((float)System.Windows.Forms.Cursor.Position.X - OldOffSet.x)), (int)Limitations.z, 96);
				}
				else
				{
					SetWindowPosition(ID, 0, (int)((float)System.Windows.Forms.Cursor.Position.X + MoveOffSet.x), (int)MoveOffSet.y + 1, (int)(MoveOffSet.z - ((float)System.Windows.Forms.Cursor.Position.X - OldOffSet.x)), (int)((float)System.Windows.Forms.Cursor.Position.Y - OldOffSet.y + MoveOffSet.w), 96);
				}
			}
			if (Action == 4)
			{
				if ((float)(int)((float)System.Windows.Forms.Cursor.Position.Y - OldOffSet.y + MoveOffSet.w) > Limitations.w)
				{
					SetWindowPosition(ID, 0, (int)MoveOffSet.x, (int)MoveOffSet.y + 1, (int)MoveOffSet.z, (int)Limitations.w, 96);
				}
				else if ((float)(int)((float)System.Windows.Forms.Cursor.Position.Y - OldOffSet.y + MoveOffSet.w) < Limitations.z)
				{
					SetWindowPosition(ID, 0, (int)MoveOffSet.x, (int)MoveOffSet.y + 1, (int)MoveOffSet.z, (int)Limitations.z, 96);
				}
				else
				{
					SetWindowPosition(ID, 0, (int)MoveOffSet.x, (int)MoveOffSet.y + 1, (int)MoveOffSet.z, (int)((float)System.Windows.Forms.Cursor.Position.Y - OldOffSet.y + MoveOffSet.w), 96);
				}
			}
			if (Action == 5)
			{
				if ((float)(int)((float)System.Windows.Forms.Cursor.Position.X - OldOffSet.x + MoveOffSet.z) > Limitations.y)
				{
					if ((float)(int)((float)System.Windows.Forms.Cursor.Position.Y - OldOffSet.y + MoveOffSet.w) > Limitations.w)
					{
						SetWindowPosition(ID, 0, (int)MoveOffSet.x, (int)MoveOffSet.y + 1, (int)Limitations.y, (int)Limitations.w, 96);
					}
					else if ((float)(int)((float)System.Windows.Forms.Cursor.Position.Y - OldOffSet.y + MoveOffSet.w) < Limitations.z)
					{
						SetWindowPosition(ID, 0, (int)MoveOffSet.x, (int)MoveOffSet.y + 1, (int)Limitations.y, (int)Limitations.z, 96);
					}
					else
					{
						SetWindowPosition(ID, 0, (int)MoveOffSet.x, (int)MoveOffSet.y + 1, (int)Limitations.y, (int)((float)System.Windows.Forms.Cursor.Position.Y - OldOffSet.y + MoveOffSet.w), 96);
					}
				}
				else if ((float)(int)((float)System.Windows.Forms.Cursor.Position.X - OldOffSet.x + MoveOffSet.z) < Limitations.x)
				{
					if ((float)(int)((float)System.Windows.Forms.Cursor.Position.Y - OldOffSet.y + MoveOffSet.w) > Limitations.w)
					{
						SetWindowPosition(ID, 0, (int)MoveOffSet.x, (int)MoveOffSet.y + 1, (int)Limitations.x, (int)Limitations.w, 96);
					}
					else if ((float)(int)((float)System.Windows.Forms.Cursor.Position.Y - OldOffSet.y + MoveOffSet.w) < Limitations.z)
					{
						SetWindowPosition(ID, 0, (int)MoveOffSet.x, (int)MoveOffSet.y + 1, (int)Limitations.x, (int)Limitations.z, 96);
					}
					else
					{
						SetWindowPosition(ID, 0, (int)MoveOffSet.x, (int)MoveOffSet.y + 1, (int)Limitations.x, (int)((float)System.Windows.Forms.Cursor.Position.Y - OldOffSet.y + MoveOffSet.w), 96);
					}
				}
				else if ((float)(int)((float)System.Windows.Forms.Cursor.Position.Y - OldOffSet.y + MoveOffSet.w) > Limitations.w)
				{
					SetWindowPosition(ID, 0, (int)MoveOffSet.x, (int)MoveOffSet.y + 1, (int)((float)System.Windows.Forms.Cursor.Position.X - OldOffSet.x + MoveOffSet.z), (int)Limitations.w, 96);
				}
				else if ((float)(int)((float)System.Windows.Forms.Cursor.Position.Y - OldOffSet.y + MoveOffSet.w) < Limitations.z)
				{
					SetWindowPosition(ID, 0, (int)MoveOffSet.x, (int)MoveOffSet.y + 1, (int)((float)System.Windows.Forms.Cursor.Position.X - OldOffSet.x + MoveOffSet.z), (int)Limitations.z, 96);
				}
				else
				{
					SetWindowPosition(ID, 0, (int)MoveOffSet.x, (int)MoveOffSet.y + 1, (int)((float)System.Windows.Forms.Cursor.Position.X - OldOffSet.x + MoveOffSet.z), (int)((float)System.Windows.Forms.Cursor.Position.Y - OldOffSet.y + MoveOffSet.w), 96);
				}
			}
			if (Action == 6)
			{
				if ((float)(int)((float)System.Windows.Forms.Cursor.Position.X - OldOffSet.x + MoveOffSet.z) > Limitations.y)
				{
					SetWindowPosition(ID, 0, (int)MoveOffSet.x, (int)MoveOffSet.y + 1, (int)Limitations.y, (int)MoveOffSet.w, 96);
				}
				else if ((float)(int)((float)System.Windows.Forms.Cursor.Position.X - OldOffSet.x + MoveOffSet.z) < Limitations.x)
				{
					SetWindowPosition(ID, 0, (int)MoveOffSet.x, (int)MoveOffSet.y + 1, (int)Limitations.x, (int)MoveOffSet.w, 96);
				}
				else
				{
					SetWindowPosition(ID, 0, (int)MoveOffSet.x, (int)MoveOffSet.y + 1, (int)((float)System.Windows.Forms.Cursor.Position.X - OldOffSet.x + MoveOffSet.z), (int)MoveOffSet.w, 96);
				}
			}
			if (Action == 7)
			{
				if ((float)(int)((float)System.Windows.Forms.Cursor.Position.X - OldOffSet.x + MoveOffSet.z) > Limitations.y)
				{
					if ((float)(int)(MoveOffSet.w - ((float)System.Windows.Forms.Cursor.Position.Y - OldOffSet.y)) > Limitations.w)
					{
						SetWindowPosition(ID, 0, (int)MoveOffSet.x, (int)(OldOffSet.y + MoveOffSet.y - (Limitations.w - MoveOffSet.w)) + 1, (int)Limitations.y, (int)Limitations.w, 96);
					}
					else if ((float)(int)(MoveOffSet.w - ((float)System.Windows.Forms.Cursor.Position.Y - OldOffSet.y)) < Limitations.z)
					{
						SetWindowPosition(ID, 0, (int)MoveOffSet.x, (int)(OldOffSet.y + MoveOffSet.y - (Limitations.z - MoveOffSet.w)) + 1, (int)Limitations.y, (int)Limitations.z, 96);
					}
					else
					{
						SetWindowPosition(ID, 0, (int)MoveOffSet.x, (int)((float)System.Windows.Forms.Cursor.Position.Y + MoveOffSet.y) + 1, (int)Limitations.y, (int)(MoveOffSet.w - ((float)System.Windows.Forms.Cursor.Position.Y - OldOffSet.y)), 96);
					}
				}
				else if ((float)(int)((float)System.Windows.Forms.Cursor.Position.X - OldOffSet.x + MoveOffSet.z) < Limitations.x)
				{
					if ((float)(int)(MoveOffSet.w - ((float)System.Windows.Forms.Cursor.Position.Y - OldOffSet.y)) > Limitations.w)
					{
						SetWindowPosition(ID, 0, (int)MoveOffSet.x, (int)(OldOffSet.y + MoveOffSet.y - (Limitations.w - MoveOffSet.w)) + 1, (int)Limitations.x, (int)Limitations.w, 96);
					}
					else if ((float)(int)(MoveOffSet.w - ((float)System.Windows.Forms.Cursor.Position.Y - OldOffSet.y)) < Limitations.z)
					{
						SetWindowPosition(ID, 0, (int)MoveOffSet.x, (int)(OldOffSet.y + MoveOffSet.y - (Limitations.z - MoveOffSet.w)) + 1, (int)Limitations.x, (int)Limitations.z, 96);
					}
					else
					{
						SetWindowPosition(ID, 0, (int)MoveOffSet.x, (int)((float)System.Windows.Forms.Cursor.Position.Y + MoveOffSet.y) + 1, (int)Limitations.x, (int)(MoveOffSet.w - ((float)System.Windows.Forms.Cursor.Position.Y - OldOffSet.y)), 96);
					}
				}
				else if ((float)(int)(MoveOffSet.w - ((float)System.Windows.Forms.Cursor.Position.Y - OldOffSet.y)) > Limitations.w)
				{
					SetWindowPosition(ID, 0, (int)MoveOffSet.x, (int)(OldOffSet.y + MoveOffSet.y - (Limitations.w - MoveOffSet.w)) + 1, (int)((float)System.Windows.Forms.Cursor.Position.X - OldOffSet.x + MoveOffSet.z), (int)Limitations.w, 96);
				}
				else if ((float)(int)(MoveOffSet.w - ((float)System.Windows.Forms.Cursor.Position.Y - OldOffSet.y)) < Limitations.z)
				{
					SetWindowPosition(ID, 0, (int)MoveOffSet.x, (int)(OldOffSet.y + MoveOffSet.y - (Limitations.z - MoveOffSet.w)) + 1, (int)((float)System.Windows.Forms.Cursor.Position.X - OldOffSet.x + MoveOffSet.z), (int)Limitations.z, 96);
				}
				else
				{
					SetWindowPosition(ID, 0, (int)MoveOffSet.x, (int)((float)System.Windows.Forms.Cursor.Position.Y + MoveOffSet.y) + 1, (int)((float)System.Windows.Forms.Cursor.Position.X - OldOffSet.x + MoveOffSet.z), (int)(MoveOffSet.w - ((float)System.Windows.Forms.Cursor.Position.Y - OldOffSet.y)), 96);
				}
			}
			if (Action == 8)
			{
				if ((float)(int)(MoveOffSet.w - ((float)System.Windows.Forms.Cursor.Position.Y - OldOffSet.y)) > Limitations.w)
				{
					SetWindowPosition(ID, 0, (int)MoveOffSet.x, (int)(OldOffSet.y + MoveOffSet.y - (Limitations.w - MoveOffSet.w)) + 1, (int)MoveOffSet.z, (int)Limitations.w, 96);
				}
				else if ((float)(int)(MoveOffSet.w - ((float)System.Windows.Forms.Cursor.Position.Y - OldOffSet.y)) < Limitations.z)
				{
					SetWindowPosition(ID, 0, (int)MoveOffSet.x, (int)(OldOffSet.y + MoveOffSet.y - (Limitations.z - MoveOffSet.w)) + 1, (int)MoveOffSet.z, (int)Limitations.z, 96);
				}
				else
				{
					SetWindowPosition(ID, 0, (int)MoveOffSet.x, (int)((float)System.Windows.Forms.Cursor.Position.Y + MoveOffSet.y) + 1, (int)MoveOffSet.z, (int)(MoveOffSet.w - ((float)System.Windows.Forms.Cursor.Position.Y - OldOffSet.y)), 96);
				}
			}
			if (Action == 9)
			{
				if ((float)(int)(MoveOffSet.z - ((float)System.Windows.Forms.Cursor.Position.X - OldOffSet.x)) > Limitations.y)
				{
					if ((float)(int)(MoveOffSet.w - ((float)System.Windows.Forms.Cursor.Position.Y - OldOffSet.y)) > Limitations.w)
					{
						SetWindowPosition(ID, 0, (int)(OldOffSet.x + MoveOffSet.x - (Limitations.y - MoveOffSet.z)), (int)(OldOffSet.y + MoveOffSet.y - (Limitations.w - MoveOffSet.w)) + 1, (int)Limitations.y, (int)Limitations.w, 96);
					}
					else if ((float)(int)(MoveOffSet.w - ((float)System.Windows.Forms.Cursor.Position.Y - OldOffSet.y)) < Limitations.z)
					{
						SetWindowPosition(ID, 0, (int)(OldOffSet.x + MoveOffSet.x - (Limitations.y - MoveOffSet.z)), (int)(OldOffSet.y + MoveOffSet.y - (Limitations.z - MoveOffSet.w)) + 1, (int)Limitations.y, (int)Limitations.z, 96);
					}
					else
					{
						SetWindowPosition(ID, 0, (int)(OldOffSet.x + MoveOffSet.x - (Limitations.y - MoveOffSet.z)), (int)((float)System.Windows.Forms.Cursor.Position.Y + MoveOffSet.y) + 1, (int)Limitations.y, (int)(MoveOffSet.w - ((float)System.Windows.Forms.Cursor.Position.Y - OldOffSet.y)), 96);
					}
				}
				else if ((float)(int)(MoveOffSet.z - ((float)System.Windows.Forms.Cursor.Position.X - OldOffSet.x)) < Limitations.x)
				{
					if ((float)(int)(MoveOffSet.w - ((float)System.Windows.Forms.Cursor.Position.Y - OldOffSet.y)) > Limitations.w)
					{
						SetWindowPosition(ID, 0, (int)(OldOffSet.x + MoveOffSet.x - (Limitations.x - MoveOffSet.z)), (int)(OldOffSet.y + MoveOffSet.y - (Limitations.w - MoveOffSet.w)) + 1, (int)Limitations.x, (int)Limitations.w, 96);
					}
					else if ((float)(int)(MoveOffSet.w - ((float)System.Windows.Forms.Cursor.Position.Y - OldOffSet.y)) < Limitations.z)
					{
						SetWindowPosition(ID, 0, (int)(OldOffSet.x + MoveOffSet.x - (Limitations.x - MoveOffSet.z)), (int)(OldOffSet.y + MoveOffSet.y - (Limitations.z - MoveOffSet.w)) + 1, (int)Limitations.x, (int)Limitations.z, 96);
					}
					else
					{
						SetWindowPosition(ID, 0, (int)(OldOffSet.x + MoveOffSet.x - (Limitations.x - MoveOffSet.z)), (int)((float)System.Windows.Forms.Cursor.Position.Y + MoveOffSet.y) + 1, (int)Limitations.x, (int)(MoveOffSet.w - ((float)System.Windows.Forms.Cursor.Position.Y - OldOffSet.y)), 96);
					}
				}
				else if ((float)(int)(MoveOffSet.w - ((float)System.Windows.Forms.Cursor.Position.Y - OldOffSet.y)) > Limitations.w)
				{
					SetWindowPosition(ID, 0, (int)((float)System.Windows.Forms.Cursor.Position.X + MoveOffSet.x), (int)(OldOffSet.y + MoveOffSet.y - (Limitations.w - MoveOffSet.w)) + 1, (int)(MoveOffSet.z - ((float)System.Windows.Forms.Cursor.Position.X - OldOffSet.x)), (int)Limitations.w, 96);
				}
				else if ((float)(int)(MoveOffSet.w - ((float)System.Windows.Forms.Cursor.Position.Y - OldOffSet.y)) < Limitations.z)
				{
					SetWindowPosition(ID, 0, (int)((float)System.Windows.Forms.Cursor.Position.X + MoveOffSet.x), (int)(OldOffSet.y + MoveOffSet.y - (Limitations.z - MoveOffSet.w)) + 1, (int)(MoveOffSet.z - ((float)System.Windows.Forms.Cursor.Position.X - OldOffSet.x)), (int)Limitations.z, 96);
				}
				else
				{
					SetWindowPosition(ID, 0, (int)((float)System.Windows.Forms.Cursor.Position.X + MoveOffSet.x), (int)((float)System.Windows.Forms.Cursor.Position.Y + MoveOffSet.y) + 1, (int)(MoveOffSet.z - ((float)System.Windows.Forms.Cursor.Position.X - OldOffSet.x)), (int)(MoveOffSet.w - ((float)System.Windows.Forms.Cursor.Position.Y - OldOffSet.y)), 96);
				}
			}
			if (Action == 12)
			{
				int num = (int)(MoveOffSet.z - ((float)System.Windows.Forms.Cursor.Position.X - OldOffSet.x));
				if ((float)num > Limitations.y)
				{
					SetWindowPosition(ID, 0, (int)(OldOffSet.z + MoveOffSet.z - Limitations.y), (int)MoveOffSet.y + 1, (int)Limitations.y, (int)(Limitations.y / AspectRation), 96);
				}
				else if ((float)num < Limitations.x)
				{
					SetWindowPosition(ID, 0, (int)(OldOffSet.z + MoveOffSet.z - Limitations.x), (int)MoveOffSet.y + 1, (int)Limitations.x, (int)(Limitations.x / AspectRation), 96);
				}
				else
				{
					SetWindowPosition(ID, 0, (int)(OldOffSet.z + MoveOffSet.z - (float)num), (int)MoveOffSet.y + 1, num, (int)((float)num / AspectRation), 96);
				}
			}
			if (Action == 13)
			{
				int num = (int)(MoveOffSet.z - ((float)System.Windows.Forms.Cursor.Position.X - OldOffSet.x));
				if ((float)num / ((float)System.Windows.Forms.Cursor.Position.Y - OldOffSet.y + MoveOffSet.w) >= AspectRation)
				{
					if ((float)num > Limitations.y)
					{
						SetWindowPosition(ID, 0, (int)(OldOffSet.z + MoveOffSet.z - Limitations.y), (int)MoveOffSet.y + 1, (int)Limitations.y, (int)(Limitations.y / AspectRation), 96);
					}
					else if ((float)num < Limitations.x)
					{
						SetWindowPosition(ID, 0, (int)(OldOffSet.z + MoveOffSet.z - Limitations.x), (int)MoveOffSet.y + 1, (int)Limitations.x, (int)(Limitations.x / AspectRation), 96);
					}
					else
					{
						SetWindowPosition(ID, 0, (int)(OldOffSet.z + MoveOffSet.z - (float)num), (int)MoveOffSet.y + 1, num, (int)((float)num / AspectRation), 96);
					}
				}
				if ((float)num / ((float)System.Windows.Forms.Cursor.Position.Y - OldOffSet.y + MoveOffSet.w) < AspectRation)
				{
					num = (int)((float)System.Windows.Forms.Cursor.Position.Y - OldOffSet.y + MoveOffSet.w);
					if ((float)num > Limitations.w)
					{
						SetWindowPosition(ID, 0, (int)(OldOffSet.z + MoveOffSet.z - Limitations.w * AspectRation), (int)MoveOffSet.y + 1, (int)(Limitations.w * AspectRation), (int)Limitations.w, 96);
					}
					else if ((float)num < Limitations.z)
					{
						SetWindowPosition(ID, 0, (int)(OldOffSet.z + MoveOffSet.z - Limitations.z * AspectRation), (int)MoveOffSet.y + 1, (int)(Limitations.z * AspectRation), (int)Limitations.z, 96);
					}
					else
					{
						SetWindowPosition(ID, 0, (int)(OldOffSet.z + MoveOffSet.z - (float)num * AspectRation), (int)MoveOffSet.y + 1, (int)((float)num * AspectRation), num, 96);
					}
				}
			}
			if (Action == 14)
			{
				int num = (int)((float)System.Windows.Forms.Cursor.Position.Y - OldOffSet.y + MoveOffSet.w);
				if ((float)num > Limitations.w)
				{
					SetWindowPosition(ID, 0, (int)MoveOffSet.x, (int)MoveOffSet.y + 1, (int)(Limitations.w * AspectRation), (int)Limitations.w, 96);
				}
				else if ((float)num < Limitations.z)
				{
					SetWindowPosition(ID, 0, (int)MoveOffSet.x, (int)MoveOffSet.y + 1, (int)(Limitations.z * AspectRation), (int)Limitations.z, 96);
				}
				else
				{
					SetWindowPosition(ID, 0, (int)MoveOffSet.x, (int)MoveOffSet.y + 1, (int)((float)num * AspectRation), num, 96);
				}
			}
			if (Action == 15)
			{
				int num = (int)((float)System.Windows.Forms.Cursor.Position.X - OldOffSet.x + MoveOffSet.z);
				if ((float)num / ((float)System.Windows.Forms.Cursor.Position.Y - OldOffSet.y + MoveOffSet.w) >= AspectRation)
				{
					if ((float)num > Limitations.y)
					{
						SetWindowPosition(ID, 0, (int)MoveOffSet.x, (int)MoveOffSet.y + 1, (int)Limitations.y, (int)(Limitations.y / AspectRation), 96);
					}
					else if ((float)num < Limitations.x)
					{
						SetWindowPosition(ID, 0, (int)MoveOffSet.x, (int)MoveOffSet.y + 1, (int)Limitations.x, (int)(Limitations.x / AspectRation), 96);
					}
					else
					{
						SetWindowPosition(ID, 0, (int)MoveOffSet.x, (int)MoveOffSet.y + 1, num, (int)((float)num / AspectRation), 96);
					}
				}
				if ((float)num / ((float)System.Windows.Forms.Cursor.Position.Y - OldOffSet.y + MoveOffSet.w) < AspectRation)
				{
					num = (int)((float)System.Windows.Forms.Cursor.Position.Y - OldOffSet.y + MoveOffSet.w);
					if ((float)num > Limitations.w)
					{
						SetWindowPosition(ID, 0, (int)MoveOffSet.x, (int)MoveOffSet.y + 1, (int)(Limitations.w * AspectRation), (int)Limitations.w, 96);
					}
					else if ((float)num < Limitations.z)
					{
						SetWindowPosition(ID, 0, (int)MoveOffSet.x, (int)MoveOffSet.y + 1, (int)(Limitations.z * AspectRation), (int)Limitations.z, 96);
					}
					else
					{
						SetWindowPosition(ID, 0, (int)MoveOffSet.x, (int)MoveOffSet.y + 1, (int)((float)num * AspectRation), num, 96);
					}
				}
			}
			if (Action == 16)
			{
				int num = (int)((float)System.Windows.Forms.Cursor.Position.X - OldOffSet.x + MoveOffSet.z);
				if ((float)num > Limitations.y)
				{
					SetWindowPosition(ID, 0, (int)MoveOffSet.x, (int)MoveOffSet.y + 1, (int)Limitations.y, (int)(Limitations.y / AspectRation), 96);
				}
				else if ((float)num < Limitations.x)
				{
					SetWindowPosition(ID, 0, (int)MoveOffSet.x, (int)MoveOffSet.y + 1, (int)Limitations.x, (int)(Limitations.x / AspectRation), 96);
				}
				else
				{
					SetWindowPosition(ID, 0, (int)MoveOffSet.x, (int)MoveOffSet.y + 1, num, (int)((float)num / AspectRation), 96);
				}
			}
			if (Action == 17)
			{
				int num = (int)((float)System.Windows.Forms.Cursor.Position.X - OldOffSet.x + MoveOffSet.z);
				if ((float)num / (MoveOffSet.w - ((float)System.Windows.Forms.Cursor.Position.Y - OldOffSet.y)) >= AspectRation)
				{
					if ((float)num > Limitations.y)
					{
						SetWindowPosition(ID, 0, (int)MoveOffSet.x, (int)(OldOffSet.z + MoveOffSet.w - Limitations.y / AspectRation), (int)Limitations.y, (int)(Limitations.y / AspectRation), 96);
					}
					else if ((float)num < Limitations.x)
					{
						SetWindowPosition(ID, 0, (int)MoveOffSet.x, (int)(OldOffSet.z + MoveOffSet.w - Limitations.x / AspectRation), (int)Limitations.x, (int)(Limitations.x / AspectRation), 96);
					}
					else
					{
						SetWindowPosition(ID, 0, (int)MoveOffSet.x, (int)(OldOffSet.z + MoveOffSet.w - (float)num / AspectRation), num, (int)((float)num / AspectRation), 96);
					}
				}
				if ((float)num / (MoveOffSet.w - ((float)System.Windows.Forms.Cursor.Position.Y - OldOffSet.y)) < AspectRation)
				{
					num = (int)(MoveOffSet.w - ((float)System.Windows.Forms.Cursor.Position.Y - OldOffSet.y));
					if ((float)num > Limitations.w)
					{
						SetWindowPosition(ID, 0, (int)MoveOffSet.x, (int)(OldOffSet.z + MoveOffSet.w - Limitations.w), (int)(Limitations.w * AspectRation), (int)Limitations.w, 96);
					}
					else if ((float)num < Limitations.z)
					{
						SetWindowPosition(ID, 0, (int)MoveOffSet.x, (int)(OldOffSet.z + MoveOffSet.w - Limitations.z), (int)(Limitations.z * AspectRation), (int)Limitations.z, 96);
					}
					else
					{
						SetWindowPosition(ID, 0, (int)MoveOffSet.x, (int)(OldOffSet.z + MoveOffSet.w - (float)num), (int)((float)num * AspectRation), num, 96);
					}
				}
			}
			if (Action == 18)
			{
				int num = (int)(MoveOffSet.w - ((float)System.Windows.Forms.Cursor.Position.Y - OldOffSet.y));
				if ((float)num > Limitations.w)
				{
					SetWindowPosition(ID, 0, (int)MoveOffSet.x, (int)((float)Position.y + MoveOffSet.w - Limitations.w), (int)(Limitations.w * AspectRation), (int)Limitations.w, 96);
				}
				else if ((float)num < Limitations.z)
				{
					SetWindowPosition(ID, 0, (int)MoveOffSet.x, (int)((float)Position.y + MoveOffSet.w - Limitations.z), (int)(Limitations.z * AspectRation), (int)Limitations.z, 96);
				}
				else
				{
					SetWindowPosition(ID, 0, (int)MoveOffSet.x, (int)((float)Position.y + MoveOffSet.w - (float)num), (int)((float)num * AspectRation), num, 96);
				}
			}
			if (Action == 19)
			{
				int num = (int)(MoveOffSet.z - ((float)System.Windows.Forms.Cursor.Position.X - OldOffSet.x));
				if ((float)num / (MoveOffSet.w - ((float)System.Windows.Forms.Cursor.Position.Y - OldOffSet.y)) >= AspectRation)
				{
					if ((float)num > Limitations.y)
					{
						SetWindowPosition(ID, 0, (int)(MoveOffSet.x + MoveOffSet.z - Limitations.y), (int)(OldOffSet.z + MoveOffSet.w - Limitations.y / AspectRation), (int)Limitations.y, (int)(Limitations.y / AspectRation), 96);
					}
					else if ((float)num < Limitations.x)
					{
						SetWindowPosition(ID, 0, (int)(MoveOffSet.x + MoveOffSet.z - Limitations.x), (int)(OldOffSet.z + MoveOffSet.w - Limitations.x / AspectRation), (int)Limitations.x, (int)(Limitations.x / AspectRation), 96);
					}
					else
					{
						SetWindowPosition(ID, 0, (int)(MoveOffSet.x + MoveOffSet.z - (float)num), (int)(OldOffSet.z + MoveOffSet.w - (float)num / AspectRation), num, (int)((float)num / AspectRation), 96);
					}
				}
				if ((float)num / (MoveOffSet.w - ((float)System.Windows.Forms.Cursor.Position.Y - OldOffSet.y)) < AspectRation)
				{
					num = (int)(MoveOffSet.w - ((float)System.Windows.Forms.Cursor.Position.Y - OldOffSet.y));
					if ((float)num > Limitations.w)
					{
						SetWindowPosition(ID, 0, (int)(MoveOffSet.x + MoveOffSet.z - Limitations.w * AspectRation), (int)(OldOffSet.z + MoveOffSet.w - Limitations.w), (int)(Limitations.w * AspectRation), (int)Limitations.w, 96);
					}
					else if ((float)num < Limitations.z)
					{
						SetWindowPosition(ID, 0, (int)(MoveOffSet.x + MoveOffSet.z - Limitations.z * AspectRation), (int)(OldOffSet.z + MoveOffSet.w - Limitations.z), (int)(Limitations.z * AspectRation), (int)Limitations.z, 96);
					}
					else
					{
						SetWindowPosition(ID, 0, (int)(MoveOffSet.x + MoveOffSet.z - (float)num * AspectRation), (int)(OldOffSet.z + MoveOffSet.w - (float)num), (int)((float)num * AspectRation), num, 96);
					}
				}
			}
		}
		GetWindowRect(ID, ref Position);
		Vector2 vector = new Vector2(Input.mousePosition.x - CursorUpdate.x, Input.mousePosition.y - CursorUpdate.y);
		if (vector == Vector2.zero)
		{
			ClientPosition = new Vector2((float)System.Windows.Forms.Cursor.Position.X - Input.mousePosition.x - 1f, (float)System.Windows.Forms.Cursor.Position.Y - ((float)UnityEngine.Screen.height - Input.mousePosition.y));
		}
		CursorUpdate = Input.mousePosition;
		if (UnityEngine.Screen.height != Position.w - Position.y)
		{
			Borders = true;
		}
		else
		{
			Borders = false;
		}
		if (ID != 0)
		{
			DoneCalculating = true;
		}
	}

	public static bool IsDoneLoading()
	{
		return DoneCalculating;
	}

	public static bool IsBorderless()
	{
		return !Borders;
	}

	public static void Border()
	{
		Border(!Borders);
	}

	public static void Border(bool Active)
	{
		if (!UnityEngine.Application.isEditor)
		{
			try
			{
				if (Active)
				{
					Process.Start(Regex.Replace(UnityEngine.Application.dataPath.ToString().Remove(UnityEngine.Application.dataPath.ToString().Length - 5) + ".exe", "/", "\\"));
					if (ID != Process.GetCurrentProcess().Id)
					{
						UnityEngine.Debug.LogWarning("Misleaded processes : " + ID + " and " + Process.GetCurrentProcess().Id);
					}
					Process.GetCurrentProcess().Kill();
				}
				else
				{
					Process.Start(Regex.Replace(UnityEngine.Application.dataPath.ToString().Remove(UnityEngine.Application.dataPath.ToString().Length - 5) + ".exe", "/", "\\"), "-popupwindow");
					if (ID != Process.GetCurrentProcess().Id)
					{
						UnityEngine.Debug.LogWarning("Misleaded processes : " + ID + " and " + Process.GetCurrentProcess().Id);
					}
					Process.GetCurrentProcess().Kill();
				}
				return;
			}
			catch (Exception ex)
			{
				UnityEngine.Debug.LogError("Could not set borders, reason : " + ex.ToString());
				return;
			}
		}
		if (!Local.SilenceWarnings)
		{
			UnityEngine.Debug.LogWarning("Border function is not recommended to be called within the editor, please use it only in the standlone build.");
		}
	}

	public static void SetRect(Rect Source)
	{
		SetRect(ID, (int)Source.x, (int)Source.y, (int)Source.width, (int)Source.height);
	}

	public static void SetRect(int LeftCorner, int TopCorner, int width, int height)
	{
		SetRect(ID, LeftCorner, TopCorner, width, height);
	}

	public static void SetRect(int windowId, Rect Source)
	{
		SetRect(windowId, (int)Source.x, (int)Source.y, (int)Source.width, (int)Source.height);
	}

	public static void SetRect(int windowId, int LeftCorner, int TopCorner, int width, int height)
	{
		if (!UnityEngine.Application.isEditor)
		{
			if (!MoveWindow || ID != windowId)
			{
				if ((float)width > Limitations.y && ID == windowId)
				{
					if ((float)height > Limitations.w && ID == windowId)
					{
						SetWindowPosition(windowId, 0, LeftCorner, TopCorner + 1, (int)Limitations.y, (int)Limitations.w, 96);
					}
					else if ((float)height < Limitations.z && ID == windowId)
					{
						SetWindowPosition(windowId, 0, LeftCorner, TopCorner + 1, (int)Limitations.y, (int)Limitations.z, 96);
					}
					else
					{
						SetWindowPosition(windowId, 0, LeftCorner, TopCorner + 1, (int)Limitations.y, height, 96);
					}
				}
				else if ((float)width < Limitations.x && ID == windowId)
				{
					if ((float)height > Limitations.w && ID == windowId)
					{
						SetWindowPosition(windowId, 0, LeftCorner, TopCorner + 1, (int)Limitations.x, (int)Limitations.w, 96);
					}
					else if ((float)height < Limitations.z && ID == windowId)
					{
						SetWindowPosition(windowId, 0, LeftCorner, TopCorner + 1, (int)Limitations.x, (int)Limitations.z, 96);
					}
					else
					{
						SetWindowPosition(windowId, 0, LeftCorner, TopCorner + 1, (int)Limitations.x, height, 96);
					}
				}
				else if ((float)height > Limitations.w && ID == windowId)
				{
					SetWindowPosition(windowId, 0, LeftCorner, TopCorner + 1, width, (int)Limitations.w, 96);
				}
				else if ((float)height < Limitations.z && ID == windowId)
				{
					SetWindowPosition(windowId, 0, LeftCorner, TopCorner + 1, width, (int)Limitations.z, 96);
				}
				else
				{
					SetWindowPosition(windowId, 0, LeftCorner, TopCorner + 1, width, height, 96);
				}
			}
			else if (!Local.SilenceWarnings)
			{
				UnityEngine.Debug.LogWarning("SetRect function cant be called while GrabStart has been called and GrabEnd hasn't.");
			}
		}
		else if (!Local.SilenceWarnings)
		{
			UnityEngine.Debug.LogWarning("SetRect function is not recommended to be called within the editor, please use it only in the standlone build.");
		}
	}

	public static void SetPosition(int LeftCorner, int TopCorner)
	{
		SetPosition(ID, new Vector2(LeftCorner, TopCorner));
	}

	public static void SetPosition(Vector2 Source)
	{
		SetPosition(ID, Source);
	}

	public static void SetPosition(int windowId, int LeftCorner, int TopCorner)
	{
		SetPosition(windowId, new Vector2(LeftCorner, TopCorner));
	}

	public static void SetPosition(int windowId, Vector2 Source)
	{
		if (!UnityEngine.Application.isEditor)
		{
			if (!MoveWindow || ID != windowId)
			{
				int width = UnityEngine.Screen.width;
				int height = UnityEngine.Screen.height;
				SetWindowPosition(windowId, 0, (int)Source.x, (int)Source.y + 1, width, height, 96);
			}
			else if (!Local.SilenceWarnings)
			{
				UnityEngine.Debug.LogWarning("SetPosition function cant be called while GrabStart has been called and GrabEnd hasn't.");
			}
		}
		else if (!Local.SilenceWarnings)
		{
			UnityEngine.Debug.LogWarning("SetPosition function is not recommended to be called within the editor, please use it only in the standlone build.");
		}
	}

	public static void SetSize(int Width, int Height)
	{
		SetSize(new Vector2(Width, Height));
	}

	public static void SetSize(Vector2 Source)
	{
		SetSize(ID, Source);
	}

	public static void SetSize(int windowId, int Width, int Height)
	{
		SetSize(windowId, new Vector2(Width, Height));
	}

	public static void SetSize(int windowId, Vector2 Source)
	{
		if (!UnityEngine.Application.isEditor)
		{
			if (!MoveWindow || ID != windowId)
			{
				if ((float)(int)Source.x > Limitations.y && ID == windowId)
				{
					if ((float)(int)Source.y > Limitations.w && ID == windowId)
					{
						SetWindowPosition(windowId, 0, Position.x, Position.y, (int)Limitations.y, (int)Limitations.w, 96);
					}
					else if ((float)(int)Source.y < Limitations.z && ID == windowId)
					{
						SetWindowPosition(windowId, 0, Position.x, Position.y, (int)Limitations.y, (int)Limitations.z, 96);
					}
					else
					{
						SetWindowPosition(windowId, 0, Position.x, Position.y, (int)Limitations.y, (int)Source.y, 96);
					}
				}
				else if ((float)(int)Source.x < Limitations.x && ID == windowId)
				{
					if ((float)(int)Source.y > Limitations.w && ID == windowId)
					{
						SetWindowPosition(windowId, 0, Position.x, Position.y, (int)Limitations.x, (int)Limitations.w, 96);
					}
					else if ((float)(int)Source.y < Limitations.z && ID == windowId)
					{
						SetWindowPosition(windowId, 0, Position.x, Position.y, (int)Limitations.x, (int)Limitations.z, 96);
					}
					else
					{
						SetWindowPosition(windowId, 0, Position.x, Position.y, (int)Limitations.x, (int)Source.y, 96);
					}
				}
				else if ((float)(int)Source.y > Limitations.w && ID == windowId)
				{
					SetWindowPosition(windowId, 0, Position.x, Position.y, (int)Source.x, (int)Limitations.w, 96);
				}
				else if ((float)(int)Source.y < Limitations.z && ID == windowId)
				{
					SetWindowPosition(windowId, 0, Position.x, Position.y, (int)Source.x, (int)Limitations.z, 96);
				}
				else
				{
					SetWindowPosition(windowId, 0, Position.x, Position.y, (int)Source.x, (int)Source.y, 96);
				}
			}
			else if (!Local.SilenceWarnings)
			{
				UnityEngine.Debug.LogWarning("SetSize function cant be called while GrabStart has been called and GrabEnd hasn't.");
			}
		}
		else if (!Local.SilenceWarnings)
		{
			UnityEngine.Debug.LogWarning("SetSize function is not recommended to be called within the editor, please use it only in the standlone build.");
		}
	}

	public static Rect GetBorderRect()
	{
		return new Rect(Position.x, Position.y, Position.z - Position.x, Position.w - Position.y);
	}

	public static Vector2 GetBorderPosition()
	{
		return new Vector2(Position.x, Position.y);
	}

	public static Vector2 GetBorderSize()
	{
		if (Borders)
		{
			return new Vector2((Position.z - Position.x - UnityEngine.Screen.width) / 2, (Position.w - Position.y - UnityEngine.Screen.height) / 2);
		}
		return Vector2.zero;
	}

	public static Vector2 GetPermnentBorderSize()
	{
		return PermanentBorderSize;
	}

	public static Rect GetRect()
	{
		return new Rect((float)Position.x - GetBorderSize().x, (float)Position.y - GetBorderSize().y, UnityEngine.Screen.width, UnityEngine.Screen.height);
	}

	public static Vector2 GetPosition()
	{
		if (Borders)
		{
			return ClientPosition;
		}
		return new Vector2(Position.x, Position.y);
	}

	public static Vector2 GetSize()
	{
		return new Vector2(UnityEngine.Screen.width, UnityEngine.Screen.height);
	}

	public static void GrabStart()
	{
		if (!UnityEngine.Application.isEditor)
		{
			if (!MoveWindow && Action == 0)
			{
				MoveOffSet = new Vector4(0f - Input.mousePosition.x, Input.mousePosition.y - (float)UnityEngine.Screen.height, UnityEngine.Screen.width, UnityEngine.Screen.height);
				MoveWindow = true;
				Action = 1;
				if (Local.AutoFixAfterResizing)
				{
					SetWindowLong((IntPtr)ID, -16, 524288u);
					SetWindowPosition(ID, -2, (int)GetRect().x, (int)GetRect().y, (int)GetRect().width, (int)GetRect().height, 64);
					SetWindowLong((IntPtr)ID, -16, 524288u);
					SetWindowPosition(ID, -2, (int)GetRect().x, (int)GetRect().y, (int)GetRect().width, (int)GetRect().height, 64);
				}
			}
			else if (!Local.SilenceWarnings)
			{
				UnityEngine.Debug.LogWarning("You cant start a Grab function while another Grab or Resize function hasn't ended.");
			}
		}
		else if (!Local.SilenceWarnings)
		{
			UnityEngine.Debug.LogWarning("GrabStart function is not recommended to be called within the editor, please use it only in the standlone build.");
		}
	}

	public static void GrabEnd()
	{
		if (!UnityEngine.Application.isEditor)
		{
			if (MoveWindow && Action == 1)
			{
				MoveOffSet = new Vector4(0f, 0f, 0f, 0f);
				MoveWindow = false;
				Action = 0;
			}
			else if (!Local.SilenceWarnings)
			{
				UnityEngine.Debug.LogWarning("You cant end a Grab function while you haven't started a Grab function or another Resize function hasn't ended.");
			}
		}
		else if (!Local.SilenceWarnings)
		{
			UnityEngine.Debug.LogWarning("GrabEnd function is not recommended to be called within the editor, please use it only in the standlone build.");
		}
	}

	public static void ResizeLeftStart(float aspectRatio)
	{
		if (!UnityEngine.Application.isEditor)
		{
			if (!MoveWindow && Action == 0)
			{
				MoveOffSet = new Vector4(0f - Input.mousePosition.x, Position.y - 1, UnityEngine.Screen.width, UnityEngine.Screen.height);
				OldOffSet.x = System.Windows.Forms.Cursor.Position.X;
				OldOffSet.y = System.Windows.Forms.Cursor.Position.Y;
				OldOffSet.z = Position.x;
				MoveWindow = true;
				Action = 12;
				AspectRation = aspectRatio;
			}
			else if (!Local.SilenceWarnings)
			{
				UnityEngine.Debug.LogWarning("You cant start a ResizeLeft function while another Grab or Resize function hasn't ended.");
			}
		}
		else if (!Local.SilenceWarnings)
		{
			UnityEngine.Debug.LogWarning("ResizeLeftStart function is not recommended to be called within the editor, please use it only in the standlone build.");
		}
	}

	public static void ResizeLeftStart()
	{
		if (!UnityEngine.Application.isEditor)
		{
			if (!MoveWindow && Action == 0)
			{
				MoveOffSet = new Vector4(0f - Input.mousePosition.x, Position.y - 1, UnityEngine.Screen.width, UnityEngine.Screen.height);
				OldOffSet.x = System.Windows.Forms.Cursor.Position.X;
				OldOffSet.y = System.Windows.Forms.Cursor.Position.Y;
				MoveWindow = true;
				Action = 2;
			}
			else if (!Local.SilenceWarnings)
			{
				UnityEngine.Debug.LogWarning("You cant start a ResizeLeft function while another Grab or Resize function hasn't ended.");
			}
		}
		else if (!Local.SilenceWarnings)
		{
			UnityEngine.Debug.LogWarning("ResizeLeftStart function is not recommended to be called within the editor, please use it only in the standlone build.");
		}
	}

	public static void ResizeLeftEnd()
	{
		if (!UnityEngine.Application.isEditor)
		{
			if (MoveWindow && (Action == 2 || Action == 12))
			{
				MoveOffSet = new Vector4(0f, 0f, 0f, 0f);
				MoveWindow = false;
				Action = 0;
				if (Local.AutoFixAfterResizing)
				{
					SetWindowLong((IntPtr)ID, -16, 524288u);
					SetWindowPosition(ID, -2, (int)GetRect().x, (int)GetRect().y, (int)GetRect().width, (int)GetRect().height, 64);
					SetWindowLong((IntPtr)ID, -16, 524288u);
					SetWindowPosition(ID, -2, (int)GetRect().x, (int)GetRect().y, (int)GetRect().width, (int)GetRect().height, 64);
				}
			}
			else if (!Local.SilenceWarnings)
			{
				UnityEngine.Debug.LogWarning("You cant end a ResizeLeft function while you haven't started a ResizeLeft function or a Grab function hasn't ended.");
			}
		}
		else if (!Local.SilenceWarnings)
		{
			UnityEngine.Debug.LogWarning("ResizeLeftEnd function is not recommended to be called within the editor, please use it only in the standlone build.");
		}
	}

	public static void ResizeDownLeftStart(float aspectRatio)
	{
		if (!UnityEngine.Application.isEditor)
		{
			if (!MoveWindow && Action == 0)
			{
				MoveOffSet = new Vector4(0f - Input.mousePosition.x, Position.y - 1, UnityEngine.Screen.width, UnityEngine.Screen.height);
				OldOffSet.x = System.Windows.Forms.Cursor.Position.X;
				OldOffSet.y = System.Windows.Forms.Cursor.Position.Y;
				OldOffSet.z = Position.x;
				MoveWindow = true;
				Action = 13;
				AspectRation = aspectRatio;
			}
			else if (!Local.SilenceWarnings)
			{
				UnityEngine.Debug.LogWarning("You cant start a ResizeDownLeft function while another Grab or Resize function hasn't ended.");
			}
		}
		else if (!Local.SilenceWarnings)
		{
			UnityEngine.Debug.LogWarning("ResizeDownLeftStart function is not recommended to be called within the editor, please use it only in the standlone build.");
		}
	}

	public static void ResizeDownLeftStart()
	{
		if (!UnityEngine.Application.isEditor)
		{
			if (!MoveWindow && Action == 0)
			{
				MoveOffSet = new Vector4(0f - Input.mousePosition.x, Position.y - 1, UnityEngine.Screen.width, UnityEngine.Screen.height);
				OldOffSet.x = System.Windows.Forms.Cursor.Position.X;
				OldOffSet.y = System.Windows.Forms.Cursor.Position.Y;
				MoveWindow = true;
				Action = 3;
			}
			else if (!Local.SilenceWarnings)
			{
				UnityEngine.Debug.LogWarning("You cant start a ResizeDownLeft function while another Grab or Resize function hasn't ended.");
			}
		}
		else if (!Local.SilenceWarnings)
		{
			UnityEngine.Debug.LogWarning("ResizeDownLeftStart function is not recommended to be called within the editor, please use it only in the standlone build.");
		}
	}

	public static void ResizeDownLeftEnd()
	{
		if (!UnityEngine.Application.isEditor)
		{
			if (MoveWindow && (Action == 3 || Action == 13))
			{
				MoveOffSet = new Vector4(0f, 0f, 0f, 0f);
				MoveWindow = false;
				Action = 0;
				if (Local.AutoFixAfterResizing)
				{
					SetWindowLong((IntPtr)ID, -16, 524288u);
					SetWindowPosition(ID, -2, (int)GetRect().x, (int)GetRect().y, (int)GetRect().width, (int)GetRect().height, 64);
					SetWindowLong((IntPtr)ID, -16, 524288u);
					SetWindowPosition(ID, -2, (int)GetRect().x, (int)GetRect().y, (int)GetRect().width, (int)GetRect().height, 64);
				}
			}
			else if (!Local.SilenceWarnings)
			{
				UnityEngine.Debug.LogWarning("You cant end a ResizeDownLeft function while you haven't started a ResizeDownLeft function or a Grab function hasn't ended.");
			}
		}
		else if (!Local.SilenceWarnings)
		{
			UnityEngine.Debug.LogWarning("ResizeDownLeftEnd function is not recommended to be called within the editor, please use it only in the standlone build.");
		}
	}

	public static void ResizeDownStart(float aspectRatio)
	{
		if (!UnityEngine.Application.isEditor)
		{
			if (!MoveWindow && Action == 0)
			{
				MoveOffSet = new Vector4(Position.x, Position.y - 1, UnityEngine.Screen.width, UnityEngine.Screen.height);
				OldOffSet.x = System.Windows.Forms.Cursor.Position.X;
				OldOffSet.y = System.Windows.Forms.Cursor.Position.Y;
				MoveWindow = true;
				Action = 14;
				AspectRation = aspectRatio;
			}
			else if (!Local.SilenceWarnings)
			{
				UnityEngine.Debug.LogWarning("You cant start a ResizeDown function while another Grab or Resize function hasnt ended.");
			}
		}
		else if (!Local.SilenceWarnings)
		{
			UnityEngine.Debug.LogWarning("ResizeDownStart function is not recommended to be called within the editor, please use it only in the standlone build.");
		}
	}

	public static void ResizeDownStart()
	{
		if (!UnityEngine.Application.isEditor)
		{
			if (!MoveWindow && Action == 0)
			{
				MoveOffSet = new Vector4(Position.x, Position.y - 1, UnityEngine.Screen.width, UnityEngine.Screen.height);
				OldOffSet.x = System.Windows.Forms.Cursor.Position.X;
				OldOffSet.y = System.Windows.Forms.Cursor.Position.Y;
				MoveWindow = true;
				Action = 4;
			}
			else if (!Local.SilenceWarnings)
			{
				UnityEngine.Debug.LogWarning("You cant start a ResizeDown function while another Grab or Resize function hasn't ended.");
			}
		}
		else if (!Local.SilenceWarnings)
		{
			UnityEngine.Debug.LogWarning("ResizeDownStart function is not recommended to be called within the editor, please use it only in the standlone build.");
		}
	}

	public static void ResizeDownEnd()
	{
		if (!UnityEngine.Application.isEditor)
		{
			if (MoveWindow && (Action == 4 || Action == 14))
			{
				MoveOffSet = new Vector4(0f, 0f, 0f, 0f);
				MoveWindow = false;
				Action = 0;
				if (Local.AutoFixAfterResizing)
				{
					SetWindowLong((IntPtr)ID, -16, 524288u);
					SetWindowPosition(ID, -2, (int)GetRect().x, (int)GetRect().y, (int)GetRect().width, (int)GetRect().height, 64);
					SetWindowLong((IntPtr)ID, -16, 524288u);
					SetWindowPosition(ID, -2, (int)GetRect().x, (int)GetRect().y, (int)GetRect().width, (int)GetRect().height, 64);
				}
			}
			else if (!Local.SilenceWarnings)
			{
				UnityEngine.Debug.LogWarning("You cant end a ResizeDown function while you haven't started a ResizeDown function or a Grab function hasn't ended.");
			}
		}
		else if (!Local.SilenceWarnings)
		{
			UnityEngine.Debug.LogWarning("ResizeDownEnd function is not recommended to be called within the editor, please use it only in the standlone build.");
		}
	}

	public static void ResizeDownRightStart(float aspectRatio)
	{
		if (!UnityEngine.Application.isEditor)
		{
			if (!MoveWindow && Action == 0)
			{
				MoveOffSet = new Vector4(Position.x, Position.y - 1, UnityEngine.Screen.width, UnityEngine.Screen.height);
				OldOffSet.x = System.Windows.Forms.Cursor.Position.X;
				OldOffSet.y = System.Windows.Forms.Cursor.Position.Y;
				MoveWindow = true;
				Action = 15;
				AspectRation = aspectRatio;
			}
			else if (!Local.SilenceWarnings)
			{
				UnityEngine.Debug.LogWarning("You cant start a ResizeDownRight function while another Grab or Resize function hasnt ended.");
			}
		}
		else if (!Local.SilenceWarnings)
		{
			UnityEngine.Debug.LogWarning("ResizeDownRightStart function is not recommended to be called within the editor, please use it only in the standlone build.");
		}
	}

	public static void ResizeDownRightStart()
	{
		if (!UnityEngine.Application.isEditor)
		{
			if (!MoveWindow && Action == 0)
			{
				MoveOffSet = new Vector4(Position.x, Position.y - 1, UnityEngine.Screen.width, UnityEngine.Screen.height);
				OldOffSet.x = System.Windows.Forms.Cursor.Position.X;
				OldOffSet.y = System.Windows.Forms.Cursor.Position.Y;
				MoveWindow = true;
				Action = 5;
			}
			else if (!Local.SilenceWarnings)
			{
				UnityEngine.Debug.LogWarning("You cant start a ResizeDownRight function while another Grab or Resize function hasn't ended.");
			}
		}
		else if (!Local.SilenceWarnings)
		{
			UnityEngine.Debug.LogWarning("ResizeDownRightStart function is not recommended to be called within the editor, please use it only in the standlone build.");
		}
	}

	public static void ResizeDownRightEnd()
	{
		if (!UnityEngine.Application.isEditor)
		{
			if (MoveWindow && (Action == 5 || Action == 15))
			{
				MoveOffSet = new Vector4(0f, 0f, 0f, 0f);
				MoveWindow = false;
				Action = 0;
				if (Local.AutoFixAfterResizing)
				{
					SetWindowLong((IntPtr)ID, -16, 524288u);
					SetWindowPosition(ID, -2, (int)GetRect().x, (int)GetRect().y, (int)GetRect().width, (int)GetRect().height, 64);
					SetWindowLong((IntPtr)ID, -16, 524288u);
					SetWindowPosition(ID, -2, (int)GetRect().x, (int)GetRect().y, (int)GetRect().width, (int)GetRect().height, 64);
				}
			}
			else if (!Local.SilenceWarnings)
			{
				UnityEngine.Debug.LogWarning("You cant end a ResizeDownRight function while you haven't started a ResizeDownRight function or a Grab function hasn't ended.");
			}
		}
		else if (!Local.SilenceWarnings)
		{
			UnityEngine.Debug.LogWarning("ResizeDownRightEnd function is not recommended to be called within the editor, please use it only in the standlone build.");
		}
	}

	public static void ResizeRightStart(float aspectRatio)
	{
		if (!UnityEngine.Application.isEditor)
		{
			if (!MoveWindow && Action == 0)
			{
				MoveOffSet = new Vector4(Position.x, Position.y - 1, UnityEngine.Screen.width, UnityEngine.Screen.height);
				OldOffSet.x = System.Windows.Forms.Cursor.Position.X;
				OldOffSet.y = System.Windows.Forms.Cursor.Position.Y;
				MoveWindow = true;
				Action = 16;
				AspectRation = aspectRatio;
			}
			else if (!Local.SilenceWarnings)
			{
				UnityEngine.Debug.LogWarning("You cant start a ResizeRight function while another Grab or Resize function hasnt ended.");
			}
		}
		else if (!Local.SilenceWarnings)
		{
			UnityEngine.Debug.LogWarning("ResizeRightStart function is not recommended to be called within the editor, please use it only in the standlone build.");
		}
	}

	public static void ResizeRightStart()
	{
		if (!UnityEngine.Application.isEditor)
		{
			if (!MoveWindow && Action == 0)
			{
				MoveOffSet = new Vector4(Position.x, Position.y - 1, UnityEngine.Screen.width, UnityEngine.Screen.height);
				OldOffSet.x = System.Windows.Forms.Cursor.Position.X;
				OldOffSet.y = System.Windows.Forms.Cursor.Position.Y;
				MoveWindow = true;
				Action = 6;
			}
			else if (!Local.SilenceWarnings)
			{
				UnityEngine.Debug.LogWarning("You cant start a ResizeRight function while another Grab or Resize function hasn't ended.");
			}
		}
		else if (!Local.SilenceWarnings)
		{
			UnityEngine.Debug.LogWarning("ResizeRightStart function is not recommended to be called within the editor, please use it only in the standlone build.");
		}
	}

	public static void ResizeRightEnd()
	{
		if (!UnityEngine.Application.isEditor)
		{
			if (MoveWindow && (Action == 6 || Action == 16))
			{
				MoveOffSet = new Vector4(0f, 0f, 0f, 0f);
				MoveWindow = false;
				Action = 0;
				if (Local.AutoFixAfterResizing)
				{
					SetWindowLong((IntPtr)ID, -16, 524288u);
					SetWindowPosition(ID, -2, (int)GetRect().x, (int)GetRect().y, (int)GetRect().width, (int)GetRect().height, 64);
					SetWindowLong((IntPtr)ID, -16, 524288u);
					SetWindowPosition(ID, -2, (int)GetRect().x, (int)GetRect().y, (int)GetRect().width, (int)GetRect().height, 64);
				}
			}
			else if (!Local.SilenceWarnings)
			{
				UnityEngine.Debug.LogWarning("You cant end a ResizeRight function while you haven't started a ResizeRight function or a Grab function hasn't ended.");
			}
		}
		else if (!Local.SilenceWarnings)
		{
			UnityEngine.Debug.LogWarning("ResizeRightEnd function is not recommended to be called within the editor, please use it only in the standlone build.");
		}
	}

	public static void ResizeRightTopStart(float aspectRatio)
	{
		if (!UnityEngine.Application.isEditor)
		{
			if (!MoveWindow && Action == 0)
			{
				MoveOffSet = new Vector4(Position.x, Input.mousePosition.y - (float)UnityEngine.Screen.height, UnityEngine.Screen.width, UnityEngine.Screen.height);
				OldOffSet.x = System.Windows.Forms.Cursor.Position.X;
				OldOffSet.y = System.Windows.Forms.Cursor.Position.Y;
				OldOffSet.z = Position.y - 1;
				MoveWindow = true;
				Action = 17;
				AspectRation = aspectRatio;
			}
			else if (!Local.SilenceWarnings)
			{
				UnityEngine.Debug.LogWarning("You cant start a ResizeRightTop function while another Grab or Resize function hasn't ended.");
			}
		}
		else if (!Local.SilenceWarnings)
		{
			UnityEngine.Debug.LogWarning("ResizeRightTopStart function is not recommended to be called within the editor, please use it only in the standlone build.");
		}
	}

	public static void ResizeRightTopStart()
	{
		if (!UnityEngine.Application.isEditor)
		{
			if (!MoveWindow && Action == 0)
			{
				MoveOffSet = new Vector4(Position.x, Input.mousePosition.y - (float)UnityEngine.Screen.height, UnityEngine.Screen.width, UnityEngine.Screen.height);
				OldOffSet.x = System.Windows.Forms.Cursor.Position.X;
				OldOffSet.y = System.Windows.Forms.Cursor.Position.Y;
				MoveWindow = true;
				Action = 7;
			}
			else if (!Local.SilenceWarnings)
			{
				UnityEngine.Debug.LogWarning("You cant start a ResizeRightTop function while another Grab or Resize function hasn't ended.");
			}
		}
		else if (!Local.SilenceWarnings)
		{
			UnityEngine.Debug.LogWarning("ResizeRightTopStart function is not recommended to be called within the editor, please use it only in the standlone build.");
		}
	}

	public static void ResizeRightTopEnd()
	{
		if (!UnityEngine.Application.isEditor)
		{
			if (MoveWindow && (Action == 7 || Action == 17))
			{
				MoveOffSet = new Vector4(0f, 0f, 0f, 0f);
				MoveWindow = false;
				Action = 0;
				if (Local.AutoFixAfterResizing)
				{
					SetWindowLong((IntPtr)ID, -16, 524288u);
					SetWindowPosition(ID, -2, (int)GetRect().x, (int)GetRect().y, (int)GetRect().width, (int)GetRect().height, 64);
					SetWindowLong((IntPtr)ID, -16, 524288u);
					SetWindowPosition(ID, -2, (int)GetRect().x, (int)GetRect().y, (int)GetRect().width, (int)GetRect().height, 64);
				}
			}
			else if (!Local.SilenceWarnings)
			{
				UnityEngine.Debug.LogWarning("You cant end a ResizeRightTop function while you haven't started a ResizeRightTop function or a Grab function hasn't ended.");
			}
		}
		else if (!Local.SilenceWarnings)
		{
			UnityEngine.Debug.LogWarning("ResizeRightTopEnd function is not recommended to be called within the editor, please use it only in the standlone build.");
		}
	}

	public static void ResizeTopStart(float aspectRatio)
	{
		if (!UnityEngine.Application.isEditor)
		{
			if (!MoveWindow && Action == 0)
			{
				MoveOffSet = new Vector4(Position.x, Input.mousePosition.y - (float)UnityEngine.Screen.height, UnityEngine.Screen.width, UnityEngine.Screen.height);
				OldOffSet.x = System.Windows.Forms.Cursor.Position.X;
				OldOffSet.y = System.Windows.Forms.Cursor.Position.Y;
				OldOffSet.z = Position.y;
				MoveWindow = true;
				Action = 18;
				AspectRation = aspectRatio;
			}
			else if (!Local.SilenceWarnings)
			{
				UnityEngine.Debug.LogWarning("You cant start a ResizeTop function while another Grab or Resize function hasn't ended.");
			}
		}
		else if (!Local.SilenceWarnings)
		{
			UnityEngine.Debug.LogWarning("ResizeTopStart function is not recommended to be called within the editor, please use it only in the standlone build.");
		}
	}

	public static void ResizeTopStart()
	{
		if (!UnityEngine.Application.isEditor)
		{
			if (!MoveWindow && Action == 0)
			{
				MoveOffSet = new Vector4(Position.x, Input.mousePosition.y - (float)UnityEngine.Screen.height, UnityEngine.Screen.width, UnityEngine.Screen.height);
				OldOffSet.x = System.Windows.Forms.Cursor.Position.X;
				OldOffSet.y = System.Windows.Forms.Cursor.Position.Y;
				MoveWindow = true;
				Action = 8;
			}
			else if (!Local.SilenceWarnings)
			{
				UnityEngine.Debug.LogWarning("You cant start a ResizeTop function while another Grab or Resize function hasn't ended.");
			}
		}
		else if (!Local.SilenceWarnings)
		{
			UnityEngine.Debug.LogWarning("ResizeTopStart function is not recommended to be called within the editor, please use it only in the standlone build.");
		}
	}

	public static void ResizeTopEnd()
	{
		if (!UnityEngine.Application.isEditor)
		{
			if (MoveWindow && (Action == 8 || Action == 18))
			{
				MoveOffSet = new Vector4(0f, 0f, 0f, 0f);
				MoveWindow = false;
				Action = 0;
				if (Local.AutoFixAfterResizing)
				{
					SetWindowLong((IntPtr)ID, -16, 524288u);
					SetWindowPosition(ID, -2, (int)GetRect().x, (int)GetRect().y, (int)GetRect().width, (int)GetRect().height, 64);
					SetWindowLong((IntPtr)ID, -16, 524288u);
					SetWindowPosition(ID, -2, (int)GetRect().x, (int)GetRect().y, (int)GetRect().width, (int)GetRect().height, 64);
				}
			}
			else if (!Local.SilenceWarnings)
			{
				UnityEngine.Debug.LogWarning("You cant end a ResizeTop function while you haven't started a ResizeTop function or a Grab function hasn't ended.");
			}
		}
		else if (!Local.SilenceWarnings)
		{
			UnityEngine.Debug.LogWarning("ResizeTop function is not recommended to be called within the editor, please use it only in the standlone build.");
		}
	}

	public static void ResizeTopLeftStart(float aspectRatio)
	{
		if (!UnityEngine.Application.isEditor)
		{
			if (!MoveWindow && Action == 0)
			{
				MoveOffSet = new Vector4(Position.x, Input.mousePosition.y - (float)UnityEngine.Screen.height, UnityEngine.Screen.width, UnityEngine.Screen.height);
				OldOffSet.x = System.Windows.Forms.Cursor.Position.X;
				OldOffSet.y = System.Windows.Forms.Cursor.Position.Y;
				OldOffSet.z = Position.y;
				MoveWindow = true;
				Action = 19;
				AspectRation = aspectRatio;
			}
			else if (!Local.SilenceWarnings)
			{
				UnityEngine.Debug.LogWarning("You cant start a ResizeTopLeft function while another Grab or Resize function hasn't ended.");
			}
		}
		else if (!Local.SilenceWarnings)
		{
			UnityEngine.Debug.LogWarning("ResizeTopLeftStart function is not recommended to be called within the editor, please use it only in the standlone build.");
		}
	}

	public static void ResizeTopLeftStart()
	{
		if (!UnityEngine.Application.isEditor)
		{
			if (!MoveWindow && Action == 0)
			{
				MoveOffSet = new Vector4(0f - Input.mousePosition.x, Input.mousePosition.y - (float)UnityEngine.Screen.height, UnityEngine.Screen.width, UnityEngine.Screen.height);
				OldOffSet.x = System.Windows.Forms.Cursor.Position.X;
				OldOffSet.y = System.Windows.Forms.Cursor.Position.Y;
				MoveWindow = true;
				Action = 9;
			}
			else if (!Local.SilenceWarnings)
			{
				UnityEngine.Debug.LogWarning("You cant start a ResizeTopLeft function while another Grab or Resize function hasn't ended.");
			}
		}
		else if (!Local.SilenceWarnings)
		{
			UnityEngine.Debug.LogWarning("ResizeTopLeftStart function is not recommended to be called within the editor, please use it only in the standlone build.");
		}
	}

	public static void ResizeTopLeftEnd()
	{
		if (!UnityEngine.Application.isEditor)
		{
			if (MoveWindow && (Action == 9 || Action == 19))
			{
				MoveOffSet = new Vector4(0f, 0f, 0f, 0f);
				MoveWindow = false;
				Action = 0;
				if (Local.AutoFixAfterResizing)
				{
					SetWindowLong((IntPtr)ID, -16, 524288u);
					SetWindowPosition(ID, -2, (int)GetRect().x, (int)GetRect().y, (int)GetRect().width, (int)GetRect().height, 64);
					SetWindowLong((IntPtr)ID, -16, 524288u);
					SetWindowPosition(ID, -2, (int)GetRect().x, (int)GetRect().y, (int)GetRect().width, (int)GetRect().height, 64);
				}
			}
			else if (!Local.SilenceWarnings)
			{
				UnityEngine.Debug.LogWarning("You cant end a ResizeTopLeft function while you haven't started a ResizeTopLeft function or a Grab function hasn't ended.");
			}
		}
		else if (!Local.SilenceWarnings)
		{
			UnityEngine.Debug.LogWarning("ResizeTopLeftEnd function is not recommended to be called within the editor, please use it only in the standlone build.");
		}
	}

	public static void SetMinWidth(int Minimum)
	{
		if (!UnityEngine.Application.isEditor)
		{
			Limitations.x = Minimum;
		}
		else if (!Local.SilenceWarnings)
		{
			UnityEngine.Debug.LogWarning("SetMinWidth function is not recommended to be called within the editor, please use it only in the standlone build.");
		}
	}

	public static void SetMaxWidth(int Maximum)
	{
		if (!UnityEngine.Application.isEditor)
		{
			Limitations.y = Maximum;
		}
		else if (!Local.SilenceWarnings)
		{
			UnityEngine.Debug.LogWarning("SetMaxWidth function is not recommended to be called within the editor, please use it only in the standlone build.");
		}
	}

	public static void SetMinHeight(int Minimum)
	{
		if (!UnityEngine.Application.isEditor)
		{
			Limitations.z = Minimum;
		}
		else if (!Local.SilenceWarnings)
		{
			UnityEngine.Debug.LogWarning("SetMinHeight function is not recommended to be called within the editor, please use it only in the standlone build.");
		}
	}

	public static void SetMaxHeight(int Maximum)
	{
		if (!UnityEngine.Application.isEditor)
		{
			Limitations.w = Maximum;
		}
		else if (!Local.SilenceWarnings)
		{
			UnityEngine.Debug.LogWarning("SetMaxHeight function is not recommended to be called within the editor, please use it only in the standlone build.");
		}
	}

	public static int GetMinWidth()
	{
		return (int)Limitations.x;
	}

	public static int GetMaxWidth()
	{
		return (int)Limitations.y;
	}

	public static int GetMinHeight()
	{
		return (int)Limitations.z;
	}

	public static int GetMaxHeight()
	{
		return (int)Limitations.w;
	}

	public static void QuickDisableBorders()
	{
		QuickDisableBorders(ID);
	}

	public static void QuickDisableBorders(int windowId)
	{
		if (!UnityEngine.Application.isEditor)
		{
			UnityEngine.Debug.Log(GetPermnentBorderSize());
			if (!IsBorderless())
			{
				if (Local.StabilizeQuickChanges)
				{
					SetWindowLong((IntPtr)windowId, -16, 524288u);
					SetWindowPosition(windowId, -2, (int)GetRect().x + (int)GetPermnentBorderSize().x, (int)GetRect().y + (int)GetPermnentBorderSize().y, (int)GetRect().width - (int)GetPermnentBorderSize().x * 2, (int)GetRect().height - (int)GetPermnentBorderSize().y * 2, 64);
					SetWindowLong((IntPtr)windowId, -16, 524288u);
					SetWindowPosition(windowId, -2, (int)GetRect().x + (int)GetPermnentBorderSize().x, (int)GetRect().y + (int)GetPermnentBorderSize().y, (int)GetRect().width, (int)GetRect().height, 64);
				}
				else
				{
					SetWindowLong((IntPtr)windowId, -16, 524288u);
					SetWindowPosition(windowId, -2, (int)GetRect().x, (int)GetRect().y, (int)GetRect().width, (int)GetRect().height, 64);
					SetWindowLong((IntPtr)windowId, -16, 524288u);
					SetWindowPosition(windowId, -2, (int)GetRect().x, (int)GetRect().y, (int)GetRect().width, (int)GetRect().height, 64);
				}
			}
		}
		else if (!Local.SilenceWarnings)
		{
			UnityEngine.Debug.LogWarning("QuickDisableBorders function is not recommended to be called within the editor, please use it only in the standlone build.");
		}
	}

	public static void QuickEnableBorders()
	{
		QuickEnableBorders(ID);
	}

	public static void QuickEnableBorders(int windowId)
	{
		if (!UnityEngine.Application.isEditor)
		{
			if (Local.StabilizeQuickChanges)
			{
				SetWindowLong((IntPtr)windowId, -16, 349110272u);
				SetWindowPosition(windowId, -2, (int)GetRect().x, (int)GetRect().y, (int)GetRect().width + (int)GetPermnentBorderSize().x * 4, (int)GetRect().height + (int)GetPermnentBorderSize().y * 4, 64);
			}
			else
			{
				SetWindowLong((IntPtr)windowId, -16, 349110272u);
				SetWindowPosition(windowId, -2, (int)GetRect().x, (int)GetRect().y, (int)GetRect().width, (int)GetRect().height, 64);
			}
			SetWindowLong((IntPtr)windowId, -16, 349110272u);
			SetWindowPosition(windowId, -2, (int)GetRect().x, (int)GetRect().y, (int)GetRect().width, (int)GetRect().height, 64);
		}
		else if (!Local.SilenceWarnings)
		{
			UnityEngine.Debug.LogWarning("QuickEnableBorders function is not recommended to be called within the editor, please use it only in the standlone build.");
		}
	}

	public static void Minimize()
	{
		Minimize(ID, true);
	}

	public static void Minimize(bool TurnAutoBorderlessOff)
	{
		Minimize(ID, TurnAutoBorderlessOff);
	}

	public static void Minimize(int windowId)
	{
		Minimize(windowId, true);
	}

	public static void Minimize(int windowId, bool TurnAutoBorderlessOff)
	{
		if (!UnityEngine.Application.isEditor)
		{
			Local.QuickAutoBorderless = !TurnAutoBorderlessOff;
			if (!MoveWindow || ID != windowId)
			{
				ShowWindow(windowId, 2);
				for (int i = 0; i < ChildId.Length; i++)
				{
					SetWindowPosition(ChildId[i], -2, 0, 0, 0, 0, 64);
				}
			}
			else if (!Local.SilenceWarnings)
			{
				UnityEngine.Debug.LogWarning("Minimize function cant be called while GrabStart has been called and GrabEnd hasn't.");
			}
		}
		else if (!Local.SilenceWarnings)
		{
			UnityEngine.Debug.LogWarning("Minimize function is not recommended to be called within the editor, please use it only in the standlone build.");
		}
	}

	public static void Fullscreen()
	{
		Fullscreen(UnityEngine.Screen.width, UnityEngine.Screen.height);
	}

	public static void Fullscreen(Vector2 Quality)
	{
		Fullscreen((int)Quality.x, (int)Quality.y);
	}

	public static void Fullscreen(int Width, int Height)
	{
		if (!UnityEngine.Application.isEditor)
		{
			if (!MoveWindow)
			{
				UnityEngine.Screen.SetResolution(Width, Height, !UnityEngine.Screen.fullScreen);
			}
			else if (!Local.SilenceWarnings)
			{
				UnityEngine.Debug.LogWarning("Fullscreen function cant be called while GrabStart has been called and GrabEnd hasn't.");
			}
		}
		else if (!Local.SilenceWarnings)
		{
			UnityEngine.Debug.LogWarning("Fullscreen function doesnt work in the editor.");
		}
	}

	public static IEnumerator StoreMaximized(int windowId, bool IgnoreLimitations)
	{
		yield return 0;
		Maximized = GetRect();
		if (!IgnoreLimitations)
		{
			ShowWindow(windowId, 1);
			yield return 0;
			yield return 0;
			SetWindowPos(windowId, 0, (int)(Maximized.x + Maximized.width / 2f - (float)(int)(Limitations.y / 2f)), (int)(Maximized.y + Maximized.height / 2f - (float)(int)(Limitations.w / 2f) + 1f), (int)Limitations.y, (int)Limitations.w, 96);
			yield return 0;
		}
	}

	public static void UnMaximize()
	{
		UnMaximize(ID);
	}

	public static void UnMaximize(int windowId)
	{
		if (!UnityEngine.Application.isEditor)
		{
			if (!MoveWindow || ID != windowId)
			{
				ShowWindow(windowId, 1);
			}
			else if (!Local.SilenceWarnings)
			{
				UnityEngine.Debug.LogWarning("UnMaximize function cant be called while GrabStart has been called and GrabEnd hasn't.");
			}
		}
		else if (!Local.SilenceWarnings)
		{
			UnityEngine.Debug.LogWarning("UnMaximize function is not recommended to be called within the editor, please use it only in the standlone build.");
		}
	}

	public static void Maximize()
	{
		Maximize(ID, true);
	}

	public static void Maximize(bool IgnoreLimitations)
	{
		Maximize(ID, IgnoreLimitations);
	}

	public static void Maximize(int windowId)
	{
		Maximize(windowId, true);
	}

	public static void Maximize(int windowId, bool IgnoreLimitations)
	{
		if (!UnityEngine.Application.isEditor)
		{
			if (!MoveWindow || ID != windowId)
			{
				ShowWindow(windowId, 3);
				Local.StartCoroutine(StoreMaximized(windowId, IgnoreLimitations));
			}
			else if (!Local.SilenceWarnings)
			{
				UnityEngine.Debug.LogWarning("Maximize function cant be called while GrabStart has been called and GrabEnd hasn't.");
			}
		}
		else if (!Local.SilenceWarnings)
		{
			UnityEngine.Debug.LogWarning("Maximize function is not designed to work in the editor.");
		}
	}

	public static bool IsMaximized()
	{
		return GetRect() == Maximized;
	}

	public static void Exit()
	{
		if (!UnityEngine.Application.isEditor)
		{
			if (!MoveWindow)
			{
				UnityEngine.Application.Quit();
			}
			else if (!Local.SilenceWarnings)
			{
				UnityEngine.Debug.LogWarning("Exit function cant be called while GrabStart has been called and GrabEnd hasn't.");
			}
		}
		else if (!Local.SilenceWarnings)
		{
			UnityEngine.Debug.LogWarning("Exit function is not designed to work in the editor.");
		}
	}

	public static void ForceExit()
	{
		ForceExit(ID);
	}

	public static void ForceExit(int windowId)
	{
		if (!UnityEngine.Application.isEditor)
		{
			try
			{
				Process.GetProcessById(PidByHwnd(windowId)).Kill();
				return;
			}
			catch (Exception ex)
			{
				UnityEngine.Debug.LogError("Could not kill process, reason : " + ex.ToString());
				return;
			}
		}
		if (!Local.SilenceWarnings)
		{
			UnityEngine.Debug.LogWarning("ForceExit function is not recommended to be called within the editor because it can cause data loss.");
		}
	}

	private void OnApplicationQuit()
	{
		for (int i = 0; i < ChildId.Length; i++)
		{
			ForceExit(ChildId[i]);
		}
		if (AllowSizeResettingBeforeExit)
		{
			PlayerPrefs.SetInt("Screenmanager Resolution Width", (int)SizeReset.x);
			PlayerPrefs.SetInt("Screenmanager Resolution Height", (int)SizeReset.y);
		}
	}

	public static void FlashEnd()
	{
		FlashEnd(ID);
	}

	public static void FlashEnd(int WindowId)
	{
		if (!UnityEngine.Application.isEditor)
		{
			if (!MoveWindow)
			{
				FlashWindow(WindowId, 0, 0, 0);
			}
			else if (!Local.SilenceWarnings)
			{
				UnityEngine.Debug.LogWarning("FlashEnd function cant be called while GrabStart has been called and GrabEnd hasn't.");
			}
		}
		else if (!Local.SilenceWarnings)
		{
			UnityEngine.Debug.LogWarning("FlashEnd function is not designed to work in the editor.");
		}
	}

	public static void FlashPause()
	{
		FlashPause(ID);
	}

	public static void FlashPause(int WindowId)
	{
		if (!UnityEngine.Application.isEditor)
		{
			if (!MoveWindow)
			{
				FlashWindow(WindowId, 0, int.MaxValue, 0);
			}
			else if (!Local.SilenceWarnings)
			{
				UnityEngine.Debug.LogWarning("FlashPause function cant be called while GrabStart has been called and GrabEnd hasn't.");
			}
		}
		else if (!Local.SilenceWarnings)
		{
			UnityEngine.Debug.LogWarning("FlashPause function is not designed to work in the editor.");
		}
	}

	public static void FlashStart()
	{
		FlashStart(ID, 0f, int.MaxValue, string.Empty);
	}

	public static void FlashStart(float MilisecSpeed)
	{
		FlashStart(ID, MilisecSpeed, int.MaxValue, string.Empty);
	}

	public static void FlashStart(int FlashTimes)
	{
		FlashStart(ID, 0f, FlashTimes, string.Empty);
	}

	public static void FlashStart(float MilisecSpeed, int FlashTimes)
	{
		FlashStart(ID, MilisecSpeed, FlashTimes, string.Empty);
	}

	public static void FlashStart(int WindowId, float MilisecSpeed)
	{
		FlashStart(WindowId, MilisecSpeed, int.MaxValue, string.Empty);
	}

	public static void FlashStart(int WindowId, float MilisecSpeed, int FlashTimes)
	{
		FlashStart(WindowId, MilisecSpeed, FlashTimes, string.Empty);
	}

	public static void FlashStart(string Mode)
	{
		FlashStart(ID, 0f, int.MaxValue, Mode);
	}

	public static void FlashStart(float MilisecSpeed, string Mode)
	{
		FlashStart(ID, MilisecSpeed, int.MaxValue, Mode);
	}

	public static void FlashStart(int FlashTimes, string Mode)
	{
		FlashStart(ID, 0f, FlashTimes, Mode);
	}

	public static void FlashStart(float MilisecSpeed, int FlashTimes, string Mode)
	{
		FlashStart(ID, MilisecSpeed, FlashTimes, Mode);
	}

	public static void FlashStart(int WindowId, float MilisecSpeed, string Mode)
	{
		FlashStart(WindowId, MilisecSpeed, int.MaxValue, Mode);
	}

	public static void FlashStart(int WindowId, float MilisecSpeed, int FlashTimes, string Mode)
	{
		if (!UnityEngine.Application.isEditor)
		{
			if (!MoveWindow)
			{
				if (Mode == "Taskbar")
				{
					FlashWindow(WindowId, 2, FlashTimes, (int)MilisecSpeed);
				}
				else if (Mode == "Caption")
				{
					FlashWindow(WindowId, 1, FlashTimes, (int)MilisecSpeed);
				}
				else
				{
					FlashWindow(WindowId, 3, FlashTimes, (int)MilisecSpeed);
				}
			}
			else if (!Local.SilenceWarnings)
			{
				UnityEngine.Debug.LogWarning("FlashStart function cant be called while GrabStart has been called and GrabEnd hasn't.");
			}
		}
		else if (!Local.SilenceWarnings)
		{
			UnityEngine.Debug.LogWarning("FlashStart function is not designed to work in the editor.");
		}
	}

	public static void FlashUntilFocus()
	{
		FlashUntilFocus(ID, 0f, int.MaxValue, string.Empty);
	}

	public static void FlashUntilFocus(float MilisecSpeed)
	{
		FlashUntilFocus(ID, MilisecSpeed, int.MaxValue, string.Empty);
	}

	public static void FlashUntilFocus(int FlashTimes)
	{
		FlashUntilFocus(ID, 0f, FlashTimes, string.Empty);
	}

	public static void FlashUntilFocus(float MilisecSpeed, int FlashTimes)
	{
		FlashUntilFocus(ID, MilisecSpeed, FlashTimes, string.Empty);
	}

	public static void FlashUntilFocus(int WindowId, float MilisecSpeed)
	{
		FlashUntilFocus(WindowId, MilisecSpeed, int.MaxValue, string.Empty);
	}

	public static void FlashUntilFocus(int WindowId, float MilisecSpeed, int FlashTimes)
	{
		FlashUntilFocus(WindowId, MilisecSpeed, FlashTimes, string.Empty);
	}

	public static void FlashUntilFocus(string Mode)
	{
		FlashUntilFocus(ID, 0f, int.MaxValue, Mode);
	}

	public static void FlashUntilFocus(float MilisecSpeed, string Mode)
	{
		FlashUntilFocus(ID, MilisecSpeed, int.MaxValue, Mode);
	}

	public static void FlashUntilFocus(int FlashTimes, string Mode)
	{
		FlashUntilFocus(ID, 0f, FlashTimes, Mode);
	}

	public static void FlashUntilFocus(float MilisecSpeed, int FlashTimes, string Mode)
	{
		FlashUntilFocus(ID, MilisecSpeed, FlashTimes, Mode);
	}

	public static void FlashUntilFocus(int WindowId, float MilisecSpeed, string Mode)
	{
		FlashUntilFocus(WindowId, MilisecSpeed, int.MaxValue, Mode);
	}

	public static void FlashUntilFocus(int WindowId, float MilisecSpeed, int FlashTimes, string Mode)
	{
		if (!UnityEngine.Application.isEditor)
		{
			if (!MoveWindow)
			{
				if (Mode == "Taskbar")
				{
					FlashWindow(WindowId, 15, FlashTimes, (int)MilisecSpeed);
				}
				else if (Mode == "Caption")
				{
					FlashWindow(WindowId, 13, FlashTimes, (int)MilisecSpeed);
				}
				else
				{
					FlashWindow(WindowId, 15, FlashTimes, (int)MilisecSpeed);
				}
			}
			else if (!Local.SilenceWarnings)
			{
				UnityEngine.Debug.LogWarning("FlashUntilFocus function cant be called while GrabStart has been called and GrabEnd hasn't.");
			}
		}
		else if (!Local.SilenceWarnings)
		{
			UnityEngine.Debug.LogWarning("FlashUntilFocus function is not designed to work in the editor.");
		}
	}

	public static string GetTitle()
	{
		return GetTitle(ID);
	}

	public static string GetTitle(int windowId)
	{
		StringBuilder stringBuilder = new StringBuilder(256);
		if (!UnityEngine.Application.isEditor)
		{
			if (GetWindowText(windowId, stringBuilder, 256) > 0)
			{
				return stringBuilder.ToString();
			}
			if (!Local.SilenceWarnings)
			{
				UnityEngine.Debug.LogWarning("GetTitle function should not be called in the editor.");
			}
		}
		return null;
	}

	public static void SetTitle(string newTitle)
	{
		SetTitle(ID, newTitle);
	}

	public static void SetTitle(int windowId, string newTitle)
	{
		if (!UnityEngine.Application.isEditor)
		{
			SetWindowText(ID, newTitle);
		}
		else if (!Local.SilenceWarnings)
		{
			UnityEngine.Debug.LogWarning("SetTitle function should not be called in the editor.");
		}
	}

	public static void SetOwner(int ownerId)
	{
		SetOwner(ID, ownerId);
	}

	public static void SetOwner(int windowId, int ownerId)
	{
		if (!UnityEngine.Application.isEditor)
		{
			SetWindowLong((IntPtr)windowId, -8, (uint)ownerId);
		}
		else if (!Local.SilenceWarnings)
		{
			UnityEngine.Debug.LogWarning("SetOwner function should not be called in the editor.");
		}
	}

	public static void SetChild(int childId)
	{
		SetChild(childId, ID);
	}

	public static void SetChild(int childId, int parentId)
	{
		if (!UnityEngine.Application.isEditor)
		{
			SetParent(childId, parentId);
		}
		else if (!Local.SilenceWarnings)
		{
			UnityEngine.Debug.LogWarning("SetChild function should not be called in the editor.");
		}
	}

	public static void Hide()
	{
		Hide(ID);
	}

	public static void Hide(int windowId)
	{
		if (!UnityEngine.Application.isEditor)
		{
			ShowWindow(windowId, 0);
		}
		else if (!Local.SilenceWarnings)
		{
			UnityEngine.Debug.LogWarning("Hide function should not be called in the editor.");
		}
	}

	public static void Show()
	{
		Show(ID);
	}

	public static void Show(int windowId)
	{
		if (!UnityEngine.Application.isEditor)
		{
			ShowWindow(windowId, 5);
		}
		else if (!Local.SilenceWarnings)
		{
			UnityEngine.Debug.LogWarning("Show function should not be called in the editor.");
		}
	}

	public static int FindWindowByName(string windowName)
	{
		return (int)FindWindowByCaption(IntPtr.Zero, windowName);
	}

	public static void AboveRendering()
	{
		AboveRendering(ID, true);
	}

	public static void AboveRendering(bool Active)
	{
		AboveRendering(ID, Active);
	}

	public static void AboveRendering(int windowId)
	{
		AboveRendering(windowId, true);
	}

	public static void AboveRendering(int windowId, bool Active)
	{
		if (!UnityEngine.Application.isEditor)
		{
			if (Active)
			{
				SetWindowPosition(windowId, -1, 0, 0, 0, 0, 3);
			}
			else
			{
				SetWindowPosition(windowId, -2, 0, 0, 0, 0, 3);
			}
		}
		else if (!Local.SilenceWarnings)
		{
			UnityEngine.Debug.LogWarning("AboveRendering function should not be called in the editor.");
		}
	}

	public static void Clickthrough(bool Active)
	{
		Clickthrough(ID, Active);
	}

	public static void Clickthrough(int windowId, bool Active)
	{
		if (!UnityEngine.Application.isEditor)
		{
			if (Active)
			{
				SetFocus(windowId);
				SetWindowLong((IntPtr)windowId, -20, 2148007968u);
			}
			else
			{
				SetWindowLong((IntPtr)windowId, -20, 2415919104u);
			}
		}
		else if (!Local.SilenceWarnings)
		{
			UnityEngine.Debug.LogWarning("Clickthrough function should not be called in the editor.");
		}
	}

	public static void BootByName(string Location)
	{
		BootByName(Location, string.Empty);
	}

	public static void BootByName(string Location, string Parameters)
	{
		if (!UnityEngine.Application.isEditor)
		{
			try
			{
				Process.Start(Regex.Replace(Location, "/", "\\"), Parameters);
				return;
			}
			catch (Exception ex)
			{
				UnityEngine.Debug.LogError("Could not boot process, reason : " + ex.ToString());
				return;
			}
		}
		if (!Local.SilenceWarnings)
		{
			UnityEngine.Debug.LogWarning("BootByName function should not be called in the editor.");
		}
	}

	public static int ProcessIdByName(string Name)
	{
		Process[] processesByName = Process.GetProcessesByName(Name);
		if (processesByName.Length > 1)
		{
			UnityEngine.Debug.LogWarning("More than one process with name : " + Name + " found.");
		}
		else if (processesByName.Length == 0)
		{
			UnityEngine.Debug.LogError("No processes with name : " + Name + " found.");
			return 0;
		}
		return HwndByPid(processesByName[0].Id);
	}

	public static string ProcessNameById(int windowId)
	{
		Process[] processes = Process.GetProcesses();
		for (int i = 0; i < processes.Length; i++)
		{
			try
			{
				if (HwndByPid(processes[i].Id) == windowId)
				{
					return processes[i].ProcessName;
				}
			}
			catch
			{
			}
		}
		return "Error-Not-Found";
	}

	internal static int HwndByPid(int processId)
	{
		int num = 0;
		while (true)
		{
			IntPtr desktopWindow = GetDesktopWindow();
			if (desktopWindow == IntPtr.Zero)
			{
				break;
			}
			IntPtr intPtr = FindWindowEx(desktopWindow, (IntPtr)num, null, null);
			if ((int)intPtr == 0)
			{
				break;
			}
			uint lpdwProcessId = 0u;
			GetWindowThreadProcessId(intPtr, out lpdwProcessId);
			if (lpdwProcessId == processId && IsWindowVisible(intPtr) && (int)GetParent(intPtr) == 0)
			{
				return (int)intPtr;
			}
			num = (int)intPtr;
		}
		return 0;
	}

	internal static int PidByHwnd(int windowId)
	{
		uint lpdwProcessId = 0u;
		GetWindowThreadProcessId((IntPtr)windowId, out lpdwProcessId);
		return (int)lpdwProcessId;
	}

	public static void SetWindowPosition(int hwnd, int hwndInsertAfter, int x, int y, int cx, int cy, int uFlags)
	{
		if (UnityEngine.Application.isEditor)
		{
			return;
		}
		if (hwnd == ID && ChildId.Length > 0 && DoneCalculating)
		{
			for (int i = 0; i < ChildId.Length; i++)
			{
				SetWindowPos(ChildId[i], ID, (int)((float)x + ChildOffset[i].x), (int)((float)y + ChildOffset[i].y), (int)((float)cx + ChildOffset[i].width), (int)((float)cy + ChildOffset[i].height), 0x10 | uFlags);
			}
		}
		SetWindowPos(hwnd, hwndInsertAfter, x, y, cx, cy, uFlags);
		Maximized = new Rect(0f, 0f, UnityEngine.Screen.currentResolution.width, UnityEngine.Screen.currentResolution.height);
	}

	public static void AddSyncChild(int windowId, Rect offset)
	{
		if (!UnityEngine.Application.isEditor)
		{
			int[] array = new int[ChildId.Length + 1];
			Rect[] array2 = new Rect[ChildId.Length + 1];
			for (int i = 0; i < ChildId.Length; i++)
			{
				array[i] = ChildId[i];
				array2[i] = ChildOffset[i];
			}
			ChildId = new int[ChildId.Length + 1];
			ChildOffset = new Rect[ChildId.Length + 1];
			for (int j = 0; j < ChildId.Length - 1; j++)
			{
				ChildId[j] = array[j];
				ChildOffset[j] = array2[j];
			}
			ChildId[ChildId.Length - 1] = windowId;
			ChildOffset[ChildId.Length - 1] = offset;
		}
		else if (!Local.SilenceWarnings)
		{
			UnityEngine.Debug.LogWarning("AddSyncChild function should not be called in the editor.");
		}
	}

	public static void EditSyncChild(int windowId, Rect offset)
	{
		if (!UnityEngine.Application.isEditor)
		{
			if (ChildId.Length < windowId)
			{
				ChildOffset[windowId] = offset;
			}
			else
			{
				UnityEngine.Debug.LogError("No sync child with such big id exists.");
			}
		}
		else if (!Local.SilenceWarnings)
		{
			UnityEngine.Debug.LogWarning("EditSyncChild function should not be called in the editor.");
		}
	}

	public static void RemoveAllSyncChilds()
	{
		ChildId = new int[0];
		ChildOffset = new Rect[0];
	}

	public static void RemoveLastSyncChild()
	{
		if (!UnityEngine.Application.isEditor)
		{
			if (ChildId.Length != 0)
			{
				int[] array = new int[ChildId.Length - 1];
				Rect[] array2 = new Rect[ChildId.Length - 1];
				for (int i = 0; i < ChildId.Length - 1; i++)
				{
					array[i] = ChildId[i];
					array2[i] = ChildOffset[i];
				}
				ChildId = new int[ChildId.Length - 1];
				ChildOffset = new Rect[ChildId.Length - 1];
				for (int j = 0; j < ChildId.Length - 1; j++)
				{
					ChildId[j] = array[j];
					ChildOffset[j] = array2[j];
				}
			}
			else
			{
				UnityEngine.Debug.LogError("RemoveLastSyncChild failed because there are no more childs to remove.");
			}
		}
		else if (!Local.SilenceWarnings)
		{
			UnityEngine.Debug.LogWarning("RemoveLastSyncChild function should not be called in the editor.");
		}
	}

	public static void RemoveSyncChild(int windowId)
	{
		if (!UnityEngine.Application.isEditor)
		{
			bool flag = false;
			for (int i = 0; i < ChildId.Length; i++)
			{
				if (ChildId[i] == windowId)
				{
					ChildId[i] = ChildId[ChildId.Length - 1];
					ChildOffset[i] = ChildOffset[ChildId.Length - 1];
					flag = true;
					break;
				}
			}
			if (flag)
			{
				RemoveLastSyncChild();
			}
			else
			{
				UnityEngine.Debug.LogError("RemoveSyncChild failed because no child with id : " + windowId + " exists.");
			}
		}
		else if (!Local.SilenceWarnings)
		{
			UnityEngine.Debug.LogWarning("RemoveSyncChild function should not be called in the editor.");
		}
	}

	public static int GetCurrentActive()
	{
		return GetActiveWindow();
	}

	public static void SetActive()
	{
		SetActive(ID);
	}

	public static void SetActive(int windowId)
	{
		SetActiveWindow((IntPtr)windowId);
	}

	public static void SetForeground()
	{
		SetForeground(ID);
	}

	public static void SetForeground(int windowId)
	{
		SetForegroundWindow((IntPtr)windowId);
	}

	public static void HideAltTab()
	{
		HideAltTab(ID);
	}

	public static void HideAltTab(int windowId)
	{
		SetWindowLong((IntPtr)windowId, -20, (uint)(GetWindowLong((IntPtr)windowId, -20) | 0x80));
	}

	public static bool IsMinimized()
	{
		return IsMinimized(ID);
	}

	public static bool IsMinimized(int windowId)
	{
		return IsIconic((IntPtr)windowId);
	}

	public static bool OnMonitorResolutionChanged()
	{
		if (!UnityEngine.Application.isEditor && (UnityEngine.Screen.currentResolution.width != LastResolution.width || UnityEngine.Screen.currentResolution.height != LastResolution.height))
		{
			if (Local.AutoFixAfterResizing)
			{
				Loop = 10;
			}
			LastResolution = UnityEngine.Screen.currentResolution;
			return true;
		}
		if (Loop <= 0)
		{
			StoredRect = GetRect();
			StoredBorders = Borders;
			StoredBorderSize = GetBorderSize();
		}
		else if (!StoredBorders)
		{
			if (UnityEngine.Screen.height != Position.w - Position.y)
			{
				SetWindowLong((IntPtr)ID, -16, 524288u);
				SetWindowPosition(ID, -2, (int)StoredRect.x, (int)StoredRect.y, (int)StoredRect.width, (int)StoredRect.height, 64);
				SetWindowLong((IntPtr)ID, -16, 524288u);
				SetWindowPosition(ID, -2, (int)StoredRect.x, (int)StoredRect.y, (int)StoredRect.width, (int)StoredRect.height, 64);
				Loop--;
			}
			else
			{
				Loop = 0;
			}
		}
		else if (GetRect().width != StoredRect.width || GetRect().height != StoredRect.height)
		{
			SetWindowPosition(ID, 0, (int)StoredRect.x, (int)StoredRect.y, (int)(StoredRect.width + StoredBorderSize.x * 2f), (int)(StoredRect.height + StoredBorderSize.y * 2f), 96);
			Loop--;
		}
		else
		{
			Loop = 0;
		}
		return false;
	}

	public static int ReturnParent(int ChildId)
	{
		return (int)GetParent((IntPtr)ChildId);
	}

	public static int Find(string ClassName, string WindowName)
	{
		return (int)FindWindow(ClassName, WindowName);
	}

	public static int FindChild(int ParentId, int NextChild, string ClassName, string WindowName)
	{
		return (int)FindWindowEx((IntPtr)ParentId, (IntPtr)NextChild, ClassName, WindowName);
	}
}
