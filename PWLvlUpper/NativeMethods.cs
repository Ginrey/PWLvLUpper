using System;
using System.Runtime.InteropServices;

namespace PWLvlUpper
{
	internal class NativeMethods
	{
		public const string DllName = "guardian1";

		[System.Runtime.InteropServices.DllImport("guardian1", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern bool initLicense(int type);

		[System.Runtime.InteropServices.DllImport("guardian1", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		[return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.LPStr)]
		public static extern string getStrings();

		[System.Runtime.InteropServices.DllImport("guardian1", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		[return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.LPStr)]
		public static extern string runScript([System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.LPStr)] string code);

		[System.Runtime.InteropServices.DllImport("guardian1", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		[return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.LPStr)]
		public static extern string processString([System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.LPStr)] string s);
	}
}
