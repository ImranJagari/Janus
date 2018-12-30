using Janus.Core.Extensions;
using Janus.Core.Utils;
using Janus.Game.Characters;
using Janus.Game.Rooms;
using Janus.Protocol.Enums;
using Janus.Protocol.Messages;
using Janus.Protocol.Messages.World.Receive;
using Janus.Protocol.Messages.World.Send;
using Janus.Protocol.Types;
using Janus.Server.Handlers;
using Janus.Server.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Janus.Handlers.World
{
    public class LobbyHandler
    {
        [PacketHandler(BS_CS_ROOM_LIST.Id)]
        public static void HandleBS_CS_ROOM_LIST(SimpleClient client, BS_CS_ROOM_LIST message)
        {
            client.Send(new BM_SC_ROOM_LIST(2, 3, SimpleServer.Rooms.Select(x => x.Value.GetRoomInfoType())));
        }
        [PacketHandler(BS_CS_CREATE_ROOM.Id)]
        [PacketHandler(BS_CS_CREATE_ROOM_SHORTCUT.Id)]
        public static void HandleBS_CS_CREATE_ROOM(SimpleClient client, NetworkMessage message)
        {
            if(message is BS_CS_CREATE_ROOM_SHORTCUT)
            {
                BS_CS_CREATE_ROOM_SHORTCUT messageReceived = message as BS_CS_CREATE_ROOM_SHORTCUT;
                int id = Room.GetRoomId();
                if (id > 0)
                {
                    if(messageReceived.mode == 6)
                    {
                        messageReceived.mode = (byte)new Random().Next(0, 3);
                    }

                    switch ((RoomModeEnum)messageReceived.mode)
                    {
                        case RoomModeEnum.Item_Solo:
                            messageReceived.mode = (byte)RoomModeListEnum.Item_Solo;
                            break;
                        case RoomModeEnum.Speed_Solo:
                            messageReceived.mode = (byte)RoomModeListEnum.Speed_Solo;
                            break;
                        case RoomModeEnum.Item_Team:
                            messageReceived.mode = (byte)RoomModeListEnum.Item_Team;
                            break;
                        case RoomModeEnum.Speed_Team:
                            messageReceived.mode = (byte)RoomModeListEnum.Speed_Team;
                            break;
                    }

                    Room room = new Room(id, "Let's go SG !", "", 8, RoomStateEnum.Opened, RankEnum.SpecialPro, (RoomModeListEnum)(messageReceived.mode));
                    SimpleServer.Rooms.Add(room.Id, room);

                    
                    byte team = GetTeam(room.Mode);

                    var clients = SimpleServer.ConnectedClients.Where(x => x.Account != null && x.Character != null && x.Character.RoomConnected == null);

                    clients.ForEach(x => x.Send(new BM_SC_ROOM_LIST(2, 3, SimpleServer.Rooms.Select(y => y.Value.GetRoomInfoType()))));

                    //Thread.Sleep(3000);

                    //client.Send(new BM_SC_CREATE_ROOM("SUCCESS\0", room.Id, SimpleServer.RelayHost, SimpleServer.RelayPort, team, 1, 5000));
                    //if (room.AddPlayer(client.Character))
                    //{
                    //    client.Send(new BM_SC_ENTER_ROOM("SUCCESS\0", SimpleServer.RelayHost, SimpleServer.RelayPort, team, 5, 5, 5, 5000));

                    //    room.Clients.Send(new BM_SC_USER_INFO(client.IP, client.Character.Name,
                    //        room.GetPosition(client.Character), (byte)client.Character.Type,
                    //         client.Character.IsLeader ? (byte)UserEntryEnum.USER_LEADER : (byte)UserEntryEnum.USER_ENTER,
                    //        (byte)client.Account.Role, client.Character.IsLeader ? (byte)1 : client.Character.IsReady ? (byte)1 : (byte)0,
                    //        room.GetPosition(client.Character), (byte)UserStatusEnum.USER_NORMAL, 1));

                    //    room.Clients.ForEach(entry => client.Send(new BM_SC_USER_INFO(entry.IP, entry.Character.Name,
                    //        room.GetPosition(entry.Character), (byte)entry.Character.Type,
                    //         entry.Character.IsLeader ? (byte)UserEntryEnum.USER_LEADER : (byte)UserEntryEnum.USER_ENTER,
                    //        (byte)entry.Account.Role, room.GetPosition(entry.Character),
                    //        entry.Character.IsLeader ? (byte)1 : entry.Character.IsReady ? (byte)1 : (byte)0, (byte)UserStatusEnum.USER_NORMAL, 1)));
                    //}
                    //else
                    //{
                    //    client.Send(new BM_SC_ENTER_ROOM("INVALID_REQUEST\0", "", 0, 0, 0, 0, 0, 0));
                    //}
                }
                else
                {
                    client.Send(new BM_SC_CREATE_ROOM("ALREADY_ENTERED_ROOM\0", 0, "", 0, 0, 1, 0));
                }
            }
            else if(message is BS_CS_CREATE_ROOM)
            {
                BS_CS_CREATE_ROOM messageReceived = message as BS_CS_CREATE_ROOM;
                int id = Room.GetRoomId();
                if (id > 0)
                {
                    Room room = new Room(id, messageReceived.name, messageReceived.password, messageReceived.maxPlayers, RoomStateEnum.Opened, (RankEnum)messageReceived.rank, (RoomModeListEnum)messageReceived.mode);
                    SimpleServer.Rooms.Add(room.Id, room);

                    byte team = GetTeam(room.Mode);

                    var clients = SimpleServer.ConnectedClients.Where(x => x.Account != null && x.Character != null && x.Character.RoomConnected == null);

                    clients.ForEach(x => x.Send(new BM_SC_ROOM_LIST(0, 0, SimpleServer.Rooms.Select(y => y.Value.GetRoomInfoType()))));

                    //Thread.Sleep(3000);

                    //client.Send(new BM_SC_CREATE_ROOM("SUCCESS\0", room.Id, SimpleServer.RelayHost, SimpleServer.RelayPort, room.Mode > RoomModeEnum.Speed_Solo ? (byte)1 : (byte)0, 1, 5000));
                    //if (room.AddPlayer(client.Character))
                    //{
                    //    client.Send(new BM_SC_ENTER_ROOM("SUCCESS\0", SimpleServer.RelayHost, SimpleServer.RelayPort, team, 5, 5, 5, 5000));

                    //    room.Clients.Send(new BM_SC_USER_INFO(client.IP, client.Character.Name,
                    //        room.GetPosition(client.Character), (byte)client.Character.Type,
                    //         client.Character.IsLeader ? (byte)UserEntryEnum.USER_LEADER : (byte)UserEntryEnum.USER_ENTER,
                    //        (byte)client.Account.Role, client.Character.IsLeader ? (byte)1 : client.Character.IsReady ? (byte)1 : (byte)0,
                    //        room.GetPosition(client.Character), (byte)UserStatusEnum.USER_NORMAL, 1));

                    //    room.Clients.ForEach(entry => client.Send(new BM_SC_USER_INFO(entry.IP, entry.Character.Name,
                    //        room.GetPosition(entry.Character), (byte)entry.Character.Type,
                    //         entry.Character.IsLeader ? (byte)UserEntryEnum.USER_LEADER : (byte)UserEntryEnum.USER_ENTER,
                    //        (byte)entry.Account.Role, room.GetPosition(entry.Character),
                    //        entry.Character.IsLeader ? (byte)1 : entry.Character.IsReady ? (byte)1 : (byte)0, (byte)UserStatusEnum.USER_NORMAL, 1)));
                    //}
                    //else
                    //{
                    //    client.Send(new BM_SC_ENTER_ROOM("INVALID_REQUEST\0", "", 0, 0, 0, 0, 0, 0));
                    //}

                }
                else
                {
                    client.Send(new BM_SC_CREATE_ROOM("ALREADY_ENTERED_ROOM\0", 0, "", 0, 0, 1, 0));
                }
            }
        }
        [PacketHandler(BS_CS_ENTER_ROOM.Id)]
        public static void HandleBS_CS_ENTER_ROOM(SimpleClient client, BS_CS_ENTER_ROOM message)
        {
            SimpleServer.Rooms.TryGetValue(message.roomId, out Room room);

            if(room != null)
            {
                byte team = GetTeam(room.Mode);

                if (room.AddPlayer(client.Character))
                {
                    client.Send(new BM_SC_ENTER_ROOM("SUCCESS\0", SimpleServer.RelayHost, SimpleServer.RelayPort, team, room.GetPosition(client.Character), 0, 0, 5000));

                    room.Clients.Send(new BM_SC_USER_INFO(client.IP, client.Character.Name,
                         room.GetPosition(client.Character), (byte)client.Character.Type,
                          client.Character.IsLeader ? (byte)UserEntryEnum.USER_LEADER : (byte)UserEntryEnum.USER_ENTER,
                         (byte)client.Account.Role, client.Character.IsLeader ? (byte)1 : client.Character.IsReady ? (byte)1 : (byte)0,
                         room.GetPosition(client.Character), (byte)UserStatusEnum.USER_NORMAL, 1));

                    room.Clients.ForEach(entry => client.Send(new BM_SC_USER_INFO(entry.IP, entry.Character.Name,
                        room.GetPosition(entry.Character), (byte)entry.Character.Type,
                         entry.Character.IsLeader ? (byte)UserEntryEnum.USER_LEADER : (byte)UserEntryEnum.USER_ENTER,
                        (byte)entry.Account.Role, room.GetPosition(entry.Character),
                         entry.Character.IsLeader ? (byte)1 : entry.Character.IsReady ? (byte)1 : (byte)0, (byte)UserStatusEnum.USER_NORMAL, 1)));

                    Character character = client.Character;

                    //if (character != null)
                    //{
                    //    client.Character.RoomConnected.Clients.Send(new BM_SC_CHARACTER_INFO(character.Name, 1,
                    //        (byte)character.Type, 1, character.Level, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                    //        character.GetTricks()));
                    //}

                    

                    if (room.MapId == 0)
                    {
                        room.MapId = (short)(Enum.GetValues(typeof(MapIdEnum)) as MapIdEnum[]).Shuffle().First();
                    }
                    client.Send(new BM_SC_SELECT_MAP());
                    client.Send(new BM_SC_MAP_INFO(room.MapId));

                    client.Send(new BM_SC_ROOM_INFO2(room.Name, (int)room.Mode, room.MapId, 1));
                   
                }
                else
                {
                    client.Send(new BM_SC_ENTER_ROOM("INVALID_REQUEST\0", "", 0, 0, 0, 0, 0, 0));
                }
            }
        }
        [PacketHandler(BS_CS_PLAYER_READY.Id)]
        public static void HandleBS_CS_PLAYER_READY(SimpleClient client, BS_CS_PLAYER_READY message)
        {
            client.Character.IsReady = !client.Character.IsReady;

            Room room = client.Character.RoomConnected;

            room.Clients.Send(new BM_SC_USER_INFO(client.IP, client.Character.Name,
                        room.GetPosition(client.Character), (byte)client.Character.Type,
                         client.Character.IsLeader ? (byte)UserEntryEnum.USER_LEADER : (byte)UserEntryEnum.USER_ENTER,
                        (byte)client.Account.Role, room.GetPosition(client.Character),
                         client.Character.IsReady ? (byte)1 : (byte)0, (byte)UserStatusEnum.USER_NORMAL, 0));

        }
        [PacketHandler(BS_CS_START_GAME.Id)]
        public static void HandleBS_CS_START_GAME(SimpleClient client, BS_CS_START_GAME message)
        {
            if (client.Character.RoomConnected.IsAllReady())
            {
                Room room = client.Character.RoomConnected;
                if (room.MapId == 0)
                {
                    room.MapId = (short)(Enum.GetValues(typeof(MapIdEnum)) as MapIdEnum[]).Shuffle().First();
                }
                room.Clients.Send(new BM_SC_SELECT_MAP());
                room.Clients.Send(new BM_SC_MAP_INFO(client.Character.RoomConnected.MapId));


                string encryptionKey = new AsyncRandom().RandomString(16);
                room.Clients.Send(new BM_SC_START_GAME(encryptionKey, room.PlayersConnected.Select(x => x.GetPlayerGameInfoType())));

               //Thread.Sleep(6000);

                client.Send(new BM_SC_PSB_LEADER_NAME(client.Character.RoomConnected.PlayersConnected.FirstOrDefault(x => x.IsLeader).Name));

            }
        }
        [PacketHandler(BS_CS_CHARACTER_INFO.Id)]
        public static void HandleBS_CS_CHARACTER_INFO(SimpleClient client, BS_CS_CHARACTER_INFO message)
        {
            Character character = client.Character?.RoomConnected?.PlayersConnected.FirstOrDefault(x => message.name.Contains(x.Name));

            if(character != null)
            {
                client.Character.RoomConnected.Clients.Send(new BM_SC_CHARACTER_INFO(character.Name, 1, 
                    (byte)character.Type, 1, character.Level, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                    character.GetTricks()));
            }
        }
        [PacketHandler(BS_CS_SELECT_MAP.Id)]
        public static void HandleBS_CS_SELECT_MAP(SimpleClient client, BS_CS_SELECT_MAP message)
        {
            if(message.mapId == 0)
            {
                message.mapId = (short)(Enum.GetValues(typeof(MapIdEnum)) as MapIdEnum[]).Shuffle().First();
            }
            client.Character.RoomConnected.MapId = message.mapId;
            client.Character.RoomConnected.Clients.Send(new BM_SC_SELECT_MAP());
            client.Character.RoomConnected.Clients.Send(new BM_SC_MAP_INFO(message.mapId));

            string encryptionKey = new AsyncRandom().RandomString(16);
            client.Character.RoomConnected.Clients.Send(new BM_SC_START_GAME(encryptionKey, client.Character.RoomConnected.PlayersConnected.Select(x => x.GetPlayerGameInfoType())));

            //Thread.Sleep(6000);

            client.Send(new BM_SC_PSB_LEADER_NAME(client.Character.RoomConnected.PlayersConnected.FirstOrDefault(x => x.IsLeader).Name));

        }
        [PacketHandler(BS_CS_UNKNOWN_INFO.Id)]
        public static void HandleBS_CS_UNKNOWN_INFO(SimpleClient client, BS_CS_UNKNOWN_INFO message)
        {
            Room room = client.Character.RoomConnected;
            room?.Clients.Send(new BM_SC_PSB_LEADER_NAME(client.Character.Name));
        }
        [PacketHandler(BS_CS_IP_CONNECT.Id)]
        public static void HandleBS_CS_IP_CONNECT(SimpleClient client, BS_CS_IP_CONNECT message)
        {
            client.Send(new BM_SC_IP_CONNECT(client.IP, (short)(5000 + client.Account.Id)));
        }
        [PacketHandler(BS_CS_LEAVE_ROOM.Id)]
        public static void HandleBS_CS_LEAVE_ROOM(SimpleClient client, BS_CS_LEAVE_ROOM message)
        {
            client.Send(new BM_SC_LEAVE_ROOM());
            client.Character.RoomConnected.RemovePlayer(client.Character);
            client.Character.RoomConnected = null;
        }
        [PacketHandler(BS_CS_END_GAME.Id)]//2191
        public static void HandleBS_CS_END_GAME(SimpleClient client, BS_CS_END_GAME message)
        {
            client.Send(new BM_SC_END_GAME());

            Thread.Sleep(10000);
            //client.Send(new BM_SC_LEAVE_ROOM());
            //Thread.Sleep(3000);
            var room = client.Character.RoomConnected;
            if (room != null)
            {
                client.Character.RoomConnected = room;
                client.Send(new BM_SC_ENTER_ROOM("SUCCESS\0", SimpleServer.RelayHost, SimpleServer.RelayPort, 0,
                    room.GetPosition(client.Character), 0, 0, 5000));

                room.Clients.Send(new BM_SC_USER_INFO(client.IP, client.Character.Name,
                    room.GetPosition(client.Character), (byte) client.Character.Type,
                    client.Character.IsLeader ? (byte) UserEntryEnum.USER_LEADER : (byte) UserEntryEnum.USER_ENTER,
                    (byte) client.Account.Role,
                    client.Character.IsLeader ? (byte) 1 : client.Character.IsReady ? (byte) 1 : (byte) 0,
                    room.GetPosition(client.Character), (byte) UserStatusEnum.USER_NORMAL, 1));

                room.Clients.ForEach(entry => client.Send(new BM_SC_USER_INFO(entry.IP, entry.Character.Name,
                    room.GetPosition(entry.Character), (byte) entry.Character.Type,
                    entry.Character.IsLeader ? (byte) UserEntryEnum.USER_LEADER : (byte) UserEntryEnum.USER_ENTER,
                    (byte) entry.Account.Role, room.GetPosition(entry.Character),
                    entry.Character.IsLeader ? (byte) 1 : entry.Character.IsReady ? (byte) 1 : (byte) 0,
                    (byte) UserStatusEnum.USER_NORMAL, 1)));
            }
        }
        [PacketHandler(BS_CS_FINISH_LAP.Id)]
        public static void HandleBS_CS_FINISH_LAP(SimpleClient client, BS_CS_FINISH_LAP message)
        {
            //client.Send(new BM_SC_FINISH_LAP());
        }

        public static byte GetTeam(RoomModeListEnum roomMode)
        {
            return  roomMode == RoomModeListEnum.Speed_Team
                               || roomMode == RoomModeListEnum.Item_Team
                               || roomMode == RoomModeListEnum.Item_Team_private
                               || roomMode == RoomModeListEnum.Speed_Team_private
                ? (byte)new Random().Next(0, 1) : (byte)0;
        }
    }
}
