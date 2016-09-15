using System;

namespace PWLvlUpper.PWStuct
{
	internal class PWCharacter
	{
		public string Name;

		public int Level;

		public int ReincarnationCount;

		public PWCharacter(string Name, int Level, System.DateTime LastOnline, int ReincarnationCount)
		{
			this.Name = Name;
			this.Level = Level;
			this.ReincarnationCount = ReincarnationCount;
		}

		public override string ToString()
		{
			return null;
		}
	}
}
