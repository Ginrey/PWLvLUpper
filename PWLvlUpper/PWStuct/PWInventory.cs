using OOGLibrary.GameTypes;
using OOGLibrary.IO.PacketBase.ServerContainer;
using System;
using System.Collections.Generic;
using System.IO;

namespace PWLvlUpper.PWStuct
{
	internal class PWInventory
	{
		public System.Collections.Generic.List<PWItem> Items;

		public PWInventory()
		{
		}

		public PWInventory(InventoryInfoS2B Inventory)
		{
			this.Items = new System.Collections.Generic.List<PWItem>();
			InventoryItem[] items = Inventory.Items;
			for (int i = 0; i < items.Length; i++)
			{
				InventoryItem item = items[i];
				this.Items.Add(new PWItem(item));
			}
		}

		public PWInventory(System.IO.BinaryReader br, int version)
		{
			this.Read(br, version);
		}

		public void Read(System.IO.BinaryReader br, int version)
		{
			int num = br.ReadInt32();
			this.Items = new System.Collections.Generic.List<PWItem>();
			for (int i = 0; i < num; i++)
			{
				this.Items.Add(new PWItem(br, version));
			}
		}

		public void Write(System.IO.BinaryWriter bw, int version)
		{
			if (this.Items == null)
			{
				bw.Write(0);
				return;
			}
			bw.Write(this.Items.Count);
			foreach (PWItem current in this.Items)
			{
				current.Write(bw, version);
			}
		}
	}
}
