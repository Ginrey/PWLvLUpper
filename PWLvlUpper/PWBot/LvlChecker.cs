using OOGLibrary.GameTypes;
using OOGLibrary.IO;
using OOGLibrary.IO.PacketBase.ClientContainer;
using OOGLibrary.IO.PacketBase.Server;
using OOGLibrary.IO.PacketBase.ServerContainer;
using OOGLibrary.Network;
using OOGLibrary.Network.Templates;
using PWLvlUpper.PWBot.OOGTools;
using PWLvlUpper.PWStuct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;


namespace PWLvlUpper.PWBot
{
	internal class LvlChecker
	{
		private OOGAccountHost oog;

		private PacketHolder packetHolder;

		private Point3F playerPosition;

		private int minLvl;

		private System.Collections.Generic.List<InventoryInfoS2B> inventories;

		private System.Collections.Generic.List<NpcInfo> npcs = new System.Collections.Generic.List<NpcInfo>();

		private System.DateTime lastSelfTraceTime;

		private float flightSpeed;

		private ushort counter;

		private GameServer server;

		private PWGameAccount pwAccount;

		private InventoryInfoS2B GetInventory(int type)
		{
			InventoryInfoS2B inventoryInfoS2B = this.inventories.FirstOrDefault((InventoryInfoS2B x) => (int)x.Type == type);
			if (inventoryInfoS2B == null)
			{
				do
				{
					inventoryInfoS2B = this.packetHolder.Receive<InventoryInfoS2B>(15000);
					this.inventories.Add(inventoryInfoS2B);
				}
				while ((int)inventoryInfoS2B.Type != type);
			}
			return inventoryInfoS2B;
		}

		private void InitPacketHolder()
		{
			this.packetHolder = new PacketHolder(this.oog);
			this.packetHolder.AddException<ErrorInfoS05>();
			this.packetHolder.AddException<AnnounceForbidInfoS7B>();
			this.packetHolder.AddException<ErrorMessageS19>();
			this.packetHolder.AddHandle<SMKeyS02>();
			this.packetHolder.AddHandle<RoleList_ReS53>();
			this.packetHolder.AddHandle<RoleInfoUpdateS26>();
			this.packetHolder.AddHandle<LockInfoS105>();
			this.packetHolder.AddHandle<TrashboxPasswordStateS81>();
			this.packetHolder.AddHandle<TrashboxWealthS84>();
			this.packetHolder.AddHandle<Logout_ReS45>();
			this.packetHolder.AddHandle<InventoryInfoS2B>();
			this.packetHolder.AddHandle<PlayerUseItem_ReS5B>();
			this.packetHolder.AddHandle<ObjectTakeoffS60>();
			this.packetHolder.AddHandle<SelectTarget_ReS34>();
			this.packetHolder.AddHandle<OpenNpcDialog_ReS46>();
			this.packetHolder.AddHandle<TrashboxOpenS82>();
			this.packetHolder.AddHandle<TrashboxClose_ReS83>();
			this.packetHolder.AddHandle<NPCsAroundS09>();
			this.packetHolder.AddHandle<SelfTraceCurPosCB1>();
			this.packetHolder.AddHandle<GetMailList_ReS106B>();
			this.packetHolder.AddHandle<PlayerSendMail_ReS1075>();
			this.packetHolder.AddHandle<GetPlayerIDByName_ReS77>();
			this.packetHolder.AddHandle<MoneyInfoS52>();
			this.packetHolder.AddHandle<TrashboxGetMoney_ReS8A>();
			this.packetHolder.AddHandle<NotifyPosS0E>();
			this.packetHolder.AddHandle<ItemObtainedS9C>();
			this.packetHolder.AddHandle<MeditationEnabledS14A>();
			this.packetHolder.AddHandle<MeditationInfoS149>();
			this.packetHolder.AddHandle<LvlUpS25>();
			this.packetHolder.AddHandle<PlayerWaypointListSB4>();
		}

		private void ClearPacketHolder()
		{
			this.packetHolder.Clear();
		}

		private System.Collections.Generic.List<PWCharacter> LoadCharacters()
		{
			System.Collections.Generic.List<PWCharacter> list = new System.Collections.Generic.List<PWCharacter>();
			RoleList_ReS53 roleList_ReS;
			while ((roleList_ReS = this.packetHolder.Receive<RoleList_ReS53>(15000)).IsChar)
			{
				PWCharacter item = new PWCharacter(roleList_ReS.Name, roleList_ReS.Level, roleList_ReS.LastOnline.Time, roleList_ReS.Reincarnation);
				list.Add(item);
			}
			return list;
		}

		private void Login(string host, int port, string login, string password, WebProxy proxy = null)
		{
			if (login.Contains("@"))
			{
				PWGameCenter pWGameCenter = new PWGameCenter();
				pWGameCenter.Init(proxy);
				string[] array = pWGameCenter.Auth(login, password, false);
				this.oog = new OOGAccountHost(new GameServer(host, port), array[1], array[2], true, true);
			}
			else
			{
				this.oog = new OOGAccountHost(new GameServer(host, port), login, password, false, true);
			}
			this.InitPacketHolder();
			if (proxy != null)
			{
				this.oog.Client.ConnectFunction = delegate(System.Net.IPEndPoint endpoint)
				{
					return proxy.CreateConnection(endpoint.Address.ToString(), endpoint.Port, null).Client;
				};
              

            }
			if (!this.oog.BeginWork())
			{
				throw new System.Exception("Connect");
			}
			this.packetHolder.Receive<SMKeyS02>(15000);
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

		private bool FlyTo(Point3F destination)
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
					this.counter = selfTraceCurPosCB.MovementCounter;
					if ((System.DateTime.Now - this.lastSelfTraceTime).TotalSeconds < 10.0)
					{
						return false;
					}
					this.playerPosition = selfTraceCurPosCB.Point;
					this.counter = selfTraceCurPosCB.MovementCounter;
					this.lastSelfTraceTime = System.DateTime.Now;
				}
				catch (System.TimeoutException)
				{
				}
				float range = num / (1000f / (float)num2);
				Point3F directionPoint = this.GetDirectionPoint(this.playerPosition, destination, range, out flag);
				this.playerPosition = directionPoint;
				PlayerMoveC00 playerMoveC = new PlayerMoveC00(this.playerPosition, this.playerPosition, (ushort)num2, (ushort)(num * 256f), 97, this.counter);
				this.oog.Send(playerMoveC);
				this.counter += 1;
			}
			while (!flag);
			this.oog.Send(new PlayerStopC07(this.playerPosition));
			this.counter += 1;
			return true;
		}

		private bool Fly()
		{
			InventoryItem flightMount = this.GetFlightMount();
			if (flightMount == null)
			{
				return false;
			}
			if (flightMount.ID == 2096)
			{
				this.flightSpeed = 0f;
			}
			else
			{
				this.flightSpeed = System.BitConverter.ToSingle(flightMount.Body, 20);
			}
			this.oog.Send(new PlayerUseItemC28(1, 1, 12, (uint)flightMount.ID));
			this.packetHolder.Receive<PlayerUseItem_ReS5B>(15000);
			this.packetHolder.Receive<ObjectTakeoffS60>(15000);
			return true;
		}

		private InventoryItem GetFlightMount()
		{
			InventoryInfoS2B inventory = this.GetInventory(1);
			return (from x in inventory.Items
			where x.Slot == 12
			select x).FirstOrDefault<InventoryItem>();
		}

		private uint GetNearestBankWID()
		{
			try
			{
				while (true)
				{
					NPCsAroundS09 nPCsAroundS = this.packetHolder.Receive<NPCsAroundS09>(500);
					this.npcs.AddRange(nPCsAroundS.NPCs);
				}
			}
			catch
			{
			}
			foreach (NpcInfo current in this.npcs)
			{
				if (BankMap.Instance.ContainsId(current.ID) && this.playerPosition.Distance3D(current.Location) < 6.0)
				{
					return current.WID;
				}
			}
			return 0u;
		}

		private System.Collections.Generic.List<InventoryInfoS2B> ObtainBank(uint wid)
		{
			System.Collections.Generic.List<InventoryInfoS2B> list = new System.Collections.Generic.List<InventoryInfoS2B>();
			this.oog.Send(new OpenNpcDialogC23(wid));
			this.packetHolder.Receive<OpenNpcDialog_ReS46>(15000);
			this.oog.Send(new DoActionsNpcC25(15));
			this.packetHolder.Receive<TrashboxOpenS82>(15000);
			this.oog.Send(new GetStorageC37(0));
			list.Add(this.GetInventory(3));
			list.Add(this.GetInventory(4));
			return list;
		}

		private InventoryInfoS2B ObtainBankAccount(uint wid)
		{
			this.oog.Send(new OpenNpcDialogC23(wid));
			this.packetHolder.Receive<OpenNpcDialog_ReS46>(15000);
			this.oog.Send(new DoActionsNpcC25(59));
			this.packetHolder.Receive<TrashboxOpenS82>(15000);
			this.oog.Send(new GetStorageC37(1));
			return this.GetInventory(6);
		}

		public LvlChecker(GameServer server, PWGameAccount pwAccount, int minLvl)
		{
			this.server = server;
			this.pwAccount = pwAccount;
			this.minLvl = minLvl;
		}

		public void Start(ProxyClient proxy)
		{
			this.Login(this.server.Host, this.server.Port, this.pwAccount.Login, this.pwAccount.Password, proxy);
			this.pwAccount.Characters = this.LoadCharacters();
			if (this.pwAccount.Characters.Count((PWCharacter x) => x.Level >= 40) != 0)
			{
				return;
			}
			System.Collections.Generic.List<PWCharacter> list = (from x in this.pwAccount.Characters
			where x.Level >= this.minLvl && !this.pwAccount.ProblemChars.Contains(x.Name)
			orderby x.Level descending
			select x).ToList<PWCharacter>();
			for (int i = 0; i < list.Count; i++)
			{
				try
				{
					if (this.LoadCharacter(list[i], this.pwAccount.Characters.IndexOf(list[i])))
					{
						return;
					}
					if (i != list.Count - 1)
					{
						this.pwAccount.ProblemChars.Add(list[i].Name);
						this.oog.Logout();
						this.packetHolder.Receive<Logout_ReS45>(15000);
						this.LoadCharacters();
					}
				}
				catch (PacketException ex)
				{
					if (ex.ErrorPacket is AnnounceForbidInfoS7B)
					{
						this.pwAccount.ProblemChars.Add(list[i].Name);
					}
					else
					{
						if (ex.ErrorPacket is ErrorMessageS19)
						{
							DataStream arg_175_0 = ex.ErrorPacket;
							throw new System.Exception("wrong");
						}
						throw ex;
					}
				}
			}
			throw new System.Exception("wrong");
		}

		private bool LoadCharacter(PWCharacter character, int index)
		{
			this.inventories = new System.Collections.Generic.List<InventoryInfoS2B>();
			this.ClearPacketHolder();
			this.oog.Enter(index);
			RoleInfoUpdateS26 roleInfoUpdateS = this.packetHolder.Receive<RoleInfoUpdateS26>(15000);
			if (roleInfoUpdateS.HP == 0)
			{
				return false;
			}
			this.GetInventory(0);
			this.oog.Send(new UnkC31(4, 30549));
			System.Threading.Thread.Sleep(300);
			this.oog.Send(new UnkC31(4, 30551));
			System.Threading.Thread.Sleep(300);
			this.oog.Send(new UnkC31(4, 30552));
			System.Threading.Thread.Sleep(300);
			this.oog.Send(new UnkC31(1, 30549));
			System.Threading.Thread.Sleep(300);
			this.oog.Send(new UnkC31(1, 30551));
			System.Threading.Thread.Sleep(300);
			this.oog.Send(new UnkC31(1, 30552));
			System.Threading.Thread.Sleep(300);
			try
			{
				while (true)
				{
					this.packetHolder.Receive<ItemObtainedS9C>(15000);
				}
			}
			catch (System.TimeoutException)
			{
			}
			this.inventories.Remove((from x in this.inventories
			where x.Type == 0
			select x).FirstOrDefault<InventoryInfoS2B>());
			this.oog.Send(new GetInventoryC27());
			InventoryInfoS2B inventory = this.GetInventory(0);
			InventoryItem inventoryItem = (from x in inventory.Items
			where x.ID == 36636
			select x).FirstOrDefault<InventoryItem>();
			MeditationInfoS149 meditationInfoS = this.packetHolder.Receive<MeditationInfoS149>(15000);
			if (inventoryItem == null && meditationInfoS.Today2 <= 0)
			{
				return false;
			}
			if (meditationInfoS.Today <= 0)
			{
				return false;
			}
			this.packetHolder.Receive<NotifyPosS0E>(15000);
			if (this.oog.SelectedPlayer.WorldID != 150u)
			{
				this.playerPosition = this.oog.SelectedPlayer.WorldPos;
				Point3F nearest = BankMap.Instance.GetNearest(this.playerPosition);
				if (this.oog.SelectedPlayer.WorldID != 1u || this.playerPosition.Distance3D(nearest) > 5.0)
				{
					float num = 650f;
					bool flag = false;
					InventoryItem inventoryItem2 = (from x in inventory.Items
					where x.ID == 36905
					select x).FirstOrDefault<InventoryItem>();
					if (inventoryItem2 != null)
					{
						PlayerWaypointListSB4 playerWaypointListSB = this.packetHolder.Receive<PlayerWaypointListSB4>(15000);
						if (playerWaypointListSB.Points.Contains(3801))
						{
							this.oog.Send(new UseTeleportStoneC6D((byte)inventoryItem2.Slot, inventoryItem2.ID, 3801));
							this.playerPosition = this.packetHolder.Receive<NotifyPosS0E>(15000).Location;
							num = 240f;
							flag = true;
						}
					}
					if (!flag && this.oog.SelectedPlayer.WorldID != 1u)
					{
						return false;
					}
					if (!this.Fly())
					{
						return false;
					}
					nearest.Y=(nearest.Y + 3f);
					if (!this.FlyTo(new Point3F(this.playerPosition.X, num, this.playerPosition.Z)) || !this.FlyTo(new Point3F(nearest.X, this.playerPosition.Y, nearest.Z)) || !this.FlyTo(nearest))
					{
						return false;
					}
				}
				uint nearestBankWID = this.GetNearestBankWID();
				if (nearestBankWID == 0u)
				{
					return false;
				}
				this.oog.Send(new SelectTargetC02(nearestBankWID));
				this.packetHolder.Receive<SelectTarget_ReS34>(15000);
				this.oog.Send(new OpenNpcDialogC23(nearestBankWID));
				this.packetHolder.Receive<OpenNpcDialog_ReS46>(15000);
				this.oog.Send(new DoActionsNpcC25(7, new byte[]
				{
					0,
					0,
					0,
					12,
					0,
					0,
					0,
					92,
					105,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0
				}));
				System.Threading.Thread.Sleep(3000);
				this.oog.Send(new UnkC31(1, 26979));
				System.Threading.Thread.Sleep(300);
				this.oog.Send(new UnkC31(1, 26980));
				System.Threading.Thread.Sleep(300);
				this.oog.Send(new UnkC31(1, 26981));
				System.Threading.Thread.Sleep(300);
				this.oog.Send(new UnkC31(1, 26982));
				System.Threading.Thread.Sleep(300);
				this.oog.Send(new UnkC31(1, 26983));
				System.Threading.Thread.Sleep(300);
				this.oog.Send(new UnkC31(1, 26984));
				System.Threading.Thread.Sleep(300);
				this.packetHolder.Receive<NotifyPosS0E>(60000);
			}
			if (inventoryItem != null)
			{
				this.oog.Send(new MeditationPayC85(inventoryItem.Slot, inventoryItem.Count));
				meditationInfoS = this.packetHolder.Receive<MeditationInfoS149>(15000);
			}
			this.oog.Send(new MeditationSetC86(1, true));
			this.packetHolder.Receive<MeditationEnabledS14A>(15000);
			do
			{
				try
				{
					while (this.packetHolder.Receive<LvlUpS25>(100).ID != this.oog.SelectedPlayer.WorldID)
					{
					}
					character.Level++;
					if (character.Level >= 40)
					{
						return true;
					}
				}
				catch (System.TimeoutException)
				{
				}
			}
			while (this.packetHolder.Receive<MeditationInfoS149>(120000).Today2 > 0);
			return true;
		}

		public void Disconnect()
		{
			if (this.oog != null)
			{
				try
				{
					this.oog.Client.Close();
				}
				catch
				{
				}
				this.oog = null;
			}
		}
	}
}
