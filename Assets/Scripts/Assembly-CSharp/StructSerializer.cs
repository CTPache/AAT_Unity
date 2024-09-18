using System;
using System.Runtime.InteropServices;

public static class StructSerializer
{
	public static byte[] Serialize<T>(ref T str) where T : struct
	{
		int num = Marshal.SizeOf(str);
		byte[] array = new byte[num];
		IntPtr intPtr = Marshal.AllocHGlobal(num);
		Marshal.StructureToPtr(str, intPtr, true);
		Marshal.Copy(intPtr, array, 0, num);
		Marshal.FreeHGlobal(intPtr);
		return array;
	}

	public static void Deserialize<T>(ref T str, byte[] bytes) where T : struct
	{
		int num = Marshal.SizeOf(str);
		IntPtr intPtr = Marshal.AllocHGlobal(num);
		Marshal.Copy(bytes, 0, intPtr, num);
		str = (T)Marshal.PtrToStructure(intPtr, str.GetType());
		Marshal.FreeHGlobal(intPtr);
	}
}
