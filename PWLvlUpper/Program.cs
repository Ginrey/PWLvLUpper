using System;
using System.IO;
using System.Windows.Forms;
using UtilsLib;

namespace PWLvlUpper
{
	internal static class Program
	{
		[System.STAThread]
		private static void Main()
		{
            GlobalConfig.ServerStrings = NativeMethods.getStrings().Split(new string[]
              {
                "_SEP12_"
              }, System.StringSplitOptions.None);
            int num = 23;
            if (UpdateManager.CheckForUpdate(GlobalConfig.Version, GlobalConfig.ServerStrings[num], GlobalConfig.ServerStrings[num + 1], GlobalConfig.ServerStrings[num + 2]))
            {
                return;
            }

            GlobalConfig.ServerNumbers = new int[12];
			for (int i = 0; i < 11; i++)
			{
				GlobalConfig.ServerNumbers[i] = System.Convert.ToInt32(GlobalConfig.ServerStrings[12 + i]);
			}
			GlobalConfig.ServerNumbers[11] = System.Convert.ToInt32(GlobalConfig.ServerStrings[27]);
			ItemsManager.Init();
			System.Windows.Forms.Application.EnableVisualStyles();
			System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
			System.Windows.Forms.Application.Run(new Form1());
		}
	}
}
