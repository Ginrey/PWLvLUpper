using OOGLibrary.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PWLvlUpper
{
	internal static class ItemsManager
	{
		private static string pathConfig = "config//";

		private static HashSet<int> equipIds;

		private static HashSet<int> refineIds;

		public static void Init()
		{
			System.Collections.Generic.List<int> list = new System.Collections.Generic.List<int>(ItemsManager.loadIDs(ItemsManager.pathConfig + "003_weapons.txt"));
			list.AddRange(ItemsManager.loadIDs(ItemsManager.pathConfig + "006_armor.txt"));
			list.AddRange(ItemsManager.loadIDs(ItemsManager.pathConfig + "009_decoration.txt"));
			ItemsManager.equipIds = new HashSet<int>(list);
			ItemsManager.refineIds = new HashSet<int>(ItemsManager.loadIDs(ItemsManager.pathConfig + "000_refine.txt"));
		}

		private static HashSet<int> loadIDs(string path)
		{
			return new HashSet<int>(from x in System.IO.File.ReadAllLines(path)
			where !string.IsNullOrWhiteSpace(x)
			select System.Convert.ToInt32(x));
		}

		public static bool IsEquip(int id)
		{
			return ItemsManager.equipIds.Contains(id);
		}

		public static bool IsRefine(int bonusId)
		{
			return ItemsManager.refineIds.Contains(bonusId);
		}

		public static int GetRefineLevel(int ID, byte[] Body)
		{
			int result;
			try
			{
				if (Body.Length == 0 || !ItemsManager.IsEquip(ID))
				{
					result = 0;
				}
				else
				{
					DataStream dataStream = new DataStream();
					dataStream.Write(Body);
					dataStream.ReadWord();
					dataStream.ReadWord();
					dataStream.ReadWord();
					dataStream.ReadWord();
					dataStream.ReadWord();
					dataStream.ReadWord();
					dataStream.ReadInt32();
					dataStream.ReadInt32();
					ushort num = dataStream.ReadWord();
					dataStream.ReadByte();
					dataStream.ReadArray((int)dataStream.ReadByte());
					if (num == 36)
					{
						dataStream.ReadInt32();
						dataStream.ReadInt32();
						dataStream.ReadInt32();
						dataStream.ReadInt32();
						dataStream.ReadInt32();
						dataStream.ReadInt32();
						dataStream.ReadInt32();
						dataStream.ReadInt32();
						dataStream.ReadInt32();
					}
					else if (num == 44)
					{
						dataStream.ReadInt32();
						dataStream.ReadInt32();
						dataStream.ReadInt32();
						dataStream.ReadInt32();
						dataStream.ReadInt32();
						dataStream.ReadInt32();
						dataStream.ReadInt32();
						dataStream.ReadInt32();
						dataStream.ReadInt32();
						dataStream.ReadFloat();
						dataStream.ReadFloat();
					}
					ushort num2 = dataStream.ReadWord();
					dataStream.ReadWord();
					dataStream.ReadArray((int)(num2 * 4));
					int num3 = dataStream.ReadInt32();
					for (int i = 0; i < num3; i++)
					{
						int num4 = dataStream.ReadInt32();
						if (num4 >= 40960 && (num4 & 40960) != 0)
						{
							dataStream.Skip(4);
						}
						else if (num4 >= 24576 && (num4 & 24576) != 0)
						{
							dataStream.Skip(12);
						}
						else if (num4 >= GlobalConfig.ServerNumbers[11] && (num4 & GlobalConfig.ServerNumbers[11]) != 0)
						{
							dataStream.Skip(4);
							num4 ^= 16384;
							int num5 = dataStream.ReadInt32();
							if (ItemsManager.IsRefine(num4))
							{
								result = num5;
								return result;
							}
						}
						else if (num4 >= 8192 && (num4 & 8192) != 0)
						{
							dataStream.Skip(4);
						}
						else
						{
							dataStream.Skip(0);
						}
					}
					result = 0;
				}
			}
			catch
			{
				result = -1;
			}
			return result;
		}
	}
}
