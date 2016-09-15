using OOGLibrary.IO;
using System;

namespace PWLvlUpper.PWBot.OOGTools
{
	internal class PacketException : System.Exception
	{
		public DataStream ErrorPacket;

		public PacketException(DataStream ErrorPacket)
		{
			this.ErrorPacket = ErrorPacket;
		}
	}
}
