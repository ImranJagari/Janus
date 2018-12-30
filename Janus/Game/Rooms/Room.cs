using Janus.Game.Characters;
using Janus.Protocol.Enums;
using Janus.Protocol.Types;
using Janus.Server.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Janus.Game.Rooms
{
    public class Room
    {
        public SimpleClientCollection Clients = new SimpleClientCollection();
        public List<Character> PlayersConnected = new List<Character>();
        public Room(int id, string name, string password, int maxPlayers, RoomStateEnum state, RankEnum minRank, RoomModeListEnum mode)
        {
            Id = id;
            Name = name;
            Password = password;
            MaxPlayers = maxPlayers;
            State = state;
            MinRank = minRank;
            Mode = mode;
        }

        public int Id { get; }
        public string Name { get; set; }
        public string Password { get; set; }
        public int MaxPlayers { get; set; }
        public RoomStateEnum State { get; set; }
        public RankEnum MinRank { get; set; }
        public RoomModeListEnum Mode { get; set; }
        public short MapId { get; set; }

        public bool AddPlayer(Character character)
        {
            if (character.Licence < MinRank)
            {
                return false;
            }
            else if (PlayersConnected.Count >= MaxPlayers)
            {
                return false;
            }

            character.IsLeader = PlayersConnected.Count == 0;

            character.RoomConnected = this;
            Clients.Add(character.Client);
            PlayersConnected.Add(character);

            return true;
        }
        public bool RemovePlayer(Character character)
        {
            character.RoomConnected = null;
            Clients.Remove(character.Client);
            PlayersConnected.Remove(character);
            if (character.IsLeader && PlayersConnected.Count > 0)
                PlayersConnected[new Random().Next(0, PlayersConnected.Count - 1)].IsLeader = true;
            if (PlayersConnected.Count > 0)
                SimpleServer.Rooms.Remove(Id);
            return true;
        }

        public bool IsAllReady()
        {
            return !PlayersConnected.Exists(x => !x.IsReady && !x.IsLeader);
        }

        public RoomInfoType GetRoomInfoType() => new RoomInfoType(Id, Name, (int)Mode, (byte)PlayersConnected.Count, (byte)MaxPlayers, (byte)State, (byte)MinRank);

        public byte GetPosition(Character character)
        {
            return (byte)PlayersConnected.IndexOf(character);
        }

        public static int GetRoomId()
        {
            for(int i = 1; i < int.MaxValue; i++)
            {
                if (!SimpleServer.Rooms.ContainsKey(i))
                {
                    return i;
                }
            }
            return -1;
        }

        public IEnumerable<SimpleClient> GetRelayClients()
        {
            foreach(var client in Clients)
            {
                yield return SimpleServer.ConnectedClients.FirstOrDefault(x => x.Account == client.Account);
            }
        }
    }
}
