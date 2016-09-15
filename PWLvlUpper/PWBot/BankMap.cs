using OOGLibrary.GameTypes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace PWLvlUpper.PWBot
{
	internal class BankMap
	{
		private static BankMap instance;

		private System.Collections.Generic.List<Point3F> bankPoints;

		private System.Collections.Generic.List<uint> bankIds;

		public static BankMap Instance
		{
			get
			{
				if (BankMap.instance == null)
				{
					BankMap.instance = new BankMap();
				}
				return BankMap.instance;
			}
		}

		private BankMap()
		{
			this.bankPoints = new System.Collections.Generic.List<Point3F>();
			System.Collections.Generic.List<string> list = (from x in System.IO.File.ReadAllLines("config\\bankPoints.txt")
			where !string.IsNullOrWhiteSpace(x)
			select x).Distinct<string>().ToList<string>();
			foreach (string current in list)
			{
				string[] array = current.Split(new char[]
				{
					' '
				});
				Point3F item = new Point3F(float.Parse(array[0], System.Globalization.CultureInfo.InvariantCulture), float.Parse(array[1], System.Globalization.CultureInfo.InvariantCulture), float.Parse(array[2], System.Globalization.CultureInfo.InvariantCulture));
				this.bankPoints.Add(item);
			}
			this.bankIds = (from x in System.IO.File.ReadAllLines("config\\bankIds.txt")
			where !string.IsNullOrWhiteSpace(x)
			select uint.Parse(x)).ToList<uint>();
		}

		public bool ContainsId(uint id)
		{
			return this.bankIds.Contains(id);
		}

		public Point3F GetNearest(Point3F position)
		{
			return (from x in this.bankPoints
			orderby x.Distance3D(position)
			select x).First<Point3F>();
		}
	}
}
