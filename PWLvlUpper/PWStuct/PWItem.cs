using OOGLibrary.GameTypes;
using System;
using System.IO;

namespace PWLvlUpper.PWStuct
{
	internal class PWItem
	{
		public int Slot;

		public int ID;

		public int Count;

		public uint Proctype;

		public int RefineLevel;

		public byte[] Body;

		public PWItem(InventoryItem item)
		{
			this.Slot = item.Slot;
			this.ID = item.ID;
			this.Proctype = item.Proctype;
			this.Count = item.Count;
			this.Body = item.Body;
			this.RefineLevel = ItemsManager.GetRefineLevel(this.ID, item.Body);
		}

		public PWItem(System.IO.BinaryReader br, int version)
		{
			this.Read(br, version);
		}

		public void Read(System.IO.BinaryReader br, int version)
		{
			this.Slot = br.ReadInt32();
			this.ID = br.ReadInt32();
			this.Count = br.ReadInt32();
			if (version >= 8)
			{
				this.Proctype = br.ReadUInt32();
			}
			else
			{
				this.Proctype = 0u;
			}
			if (version >= 9)
			{
				int count = br.ReadInt32();
				this.Body = br.ReadBytes(count);
			}
			else
			{
				this.Body = new byte[0];
			}
			if (version >= 10)
			{
				this.RefineLevel = br.ReadInt32();
				return;
			}
			this.RefineLevel = -1;
		}

		public void Write(System.IO.BinaryWriter bw, int version)
		{
			bw.Write(this.Slot);
			bw.Write(this.ID);
			bw.Write(this.Count);
			if (version >= 8)
			{
				bw.Write(this.Proctype);
			}
			if (version >= 9)
			{
				bw.Write(this.Body.Length);
				bw.Write(this.Body);
			}
			if (version >= 10)
			{
				bw.Write(this.RefineLevel);
			}
		}
	}
}
