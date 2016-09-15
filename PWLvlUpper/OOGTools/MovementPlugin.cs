using OOGLibrary.GameTypes;
using OOGLibrary.IO.PacketBase.ClientContainer;
using OOGLibrary.IO.PacketBase.ServerContainer;
using OOGLibrary.Network.Templates;
using System;

namespace PWLvlUpper.PWBot.OOGTools
{
	internal class MovementPlugin
	{
		public Point3F PlayerPosition;

		private OOGAccountHost oog;

		private PacketHolder packetHolder;

		private float flightSpeed;

		private ushort counter;

		private System.DateTime lastSelfTraceTime;

		public MovementPlugin(OOGAccountHost oog, PacketHolder packetHolder, float flightSpeed)
		{
			this.oog = oog;
			this.packetHolder = packetHolder;
			this.flightSpeed = flightSpeed;
		}

		private Point3F Lerp(Point3F start, Point3F end, float percent)
		{
			return new Point3F(start + (end - start) * percent);
		}

		private Point3F GetDirectionPoint(Point3F start, Point3F end, float range, out bool stopMove)
		{
			double num = start.Distance3D(end);
			float num2 = (float)((double)range / num);
			if (num2 >= 1f)
			{
				stopMove = true;
				return end;
			}
			stopMove = false;
			return this.Lerp(start, end, num2);
		}

		public void StartFly(Point3F destination)
		{
			float num = 5f + this.flightSpeed;
			int num2 = 500;
			bool flag;
			do
			{
				if (this.counter == 65535)
				{
					this.counter = 0;
				}
				try
				{
					SelfTraceCurPosCB1 selfTraceCurPosCB = this.packetHolder.Receive<SelfTraceCurPosCB1>(num2);
					if ((System.DateTime.Now - this.lastSelfTraceTime).TotalSeconds < 10.0)
					{
						throw new PacketException(selfTraceCurPosCB);
					}
					this.PlayerPosition = selfTraceCurPosCB.Point;
					this.counter = selfTraceCurPosCB.MovementCounter;
					this.lastSelfTraceTime = System.DateTime.Now;
				}
				catch
				{
				}
				float range = num / (1000f / (float)num2);
				Point3F directionPoint = this.GetDirectionPoint(this.PlayerPosition, destination, range, out flag);
				this.PlayerPosition = directionPoint;
				PlayerMoveC00 playerMoveC = new PlayerMoveC00(this.PlayerPosition, this.PlayerPosition, (ushort)num2, (ushort)(num * 256f), 97, this.counter);
				this.oog.Send(playerMoveC);
				this.counter += 1;
			}
			while (!flag);
			this.oog.Send(new PlayerStopC07(this.PlayerPosition));
			this.counter += 1;
		}
	}
}
