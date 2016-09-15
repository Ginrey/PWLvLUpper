using OOGLibrary.IO.PacketBase.Server;
using System;

namespace PWLvlUpper.PWStuct
{
	internal class PWRoleForbid
	{
		public int Type;

		public int Time;

		public System.DateTime CreateTime;

		public string Message;

		private static string descriptionFormat = "Бан: {0}\nДата получения: {1}\nВремя до окончания: {2} мин.";

		private string description;

		public PWRoleForbid(AnnounceForbidInfoS7B Announce)
		{
			this.Type = Announce.Forbid.Type;
			this.Time = Announce.Forbid.Time;
			this.CreateTime = Announce.Forbid.CreateTime;
			this.Message = Announce.Forbid.Message;
		}

		public override string ToString()
		{
			if (this.description == null)
			{
				this.description = string.Format(PWRoleForbid.descriptionFormat, this.Message, this.CreateTime.ToLocalTime(), this.Time / 60);
			}
			return this.description;
		}
	}
}
