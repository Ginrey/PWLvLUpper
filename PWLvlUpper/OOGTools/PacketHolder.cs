using OOGLibrary.IO;
using OOGLibrary.Network.Templates;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace PWLvlUpper.PWBot.OOGTools
{
	internal class PacketHolder
	{
		private OOGAccountHost oog;

		private System.Collections.Generic.Dictionary<System.Type, BlockingCollection<DataStream>> packets;

		private HashSet<System.Type> exceptions;

		private DataStream exception;

		public PacketHolder(OOGAccountHost oog)
		{
			this.oog = oog;
			oog.Receive+=(new ReceiveEventHandler(this.oog_Receive));
			this.packets = new System.Collections.Generic.Dictionary<System.Type, BlockingCollection<DataStream>>();
			this.exceptions = new HashSet<System.Type>();
			this.exception = null;
		}

		public void CheckErros()
		{
			if (this.exception != null)
			{
				throw new PacketException(this.exception);
			}
			if (this.oog == null || !this.oog.Work)
			{
				throw new System.Exception();
			}
		}

		public void AddException<T>() where T : DataStream
		{
			System.Type typeFromHandle = typeof(T);
			if (!this.exceptions.Contains(typeFromHandle))
			{
				this.exceptions.Add(typeof(T));
			}
		}

		public void AddHandle<T>() where T : DataStream
		{
			System.Type typeFromHandle = typeof(T);
			if (!this.packets.ContainsKey(typeFromHandle))
			{
				this.packets.Add(typeFromHandle, new BlockingCollection<DataStream>());
			}
		}

		public T Receive<T>(int timeout = 15000) where T : DataStream
		{
			BlockingCollection<DataStream> blockingCollection = this.packets[typeof(T)];
			int num = 0;
			int num2 = 50;
			DataStream dataStream;
			while (!blockingCollection.TryTake(out dataStream, num2))
			{
				this.CheckErros();
				num += num2;
				if (num >= timeout)
				{
					throw new System.TimeoutException();
				}
			}
			return (T)((object)dataStream);
		}

		private void oog_Receive(object sender, ReceiveEventArgs e)
		{
			System.Type type = e.Stream.GetType();
			BlockingCollection<DataStream> blockingCollection;
			if (this.packets.TryGetValue(type, out blockingCollection))
			{
				blockingCollection.Add(e.Stream);
				return;
			}
			if (this.exceptions.Contains(type))
			{
				this.exception = e.Stream;
			}
		}

		public void Clear()
		{
			foreach (System.Collections.Generic.KeyValuePair<System.Type, BlockingCollection<DataStream>> current in this.packets)
			{
				while (current.Value.Count > 0)
				{
					DataStream dataStream;
					current.Value.TryTake(out dataStream);
				}
			}
		}
	}
}
