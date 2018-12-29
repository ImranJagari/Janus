using Janus.Databases.Characters;
using Janus.Game.Rooms;
using Janus.Protocol.Enums;
using Janus.Protocol.Types;
using Janus.Server.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Janus.Game.Characters
{
    public class Character
    {
        public Character(CharacterRecord record, SimpleClient client)
        {
            Record = record;
            Client = client;
        }

        public CharacterRecord Record { get; }
        public SimpleClient Client { get; }
        public SimpleClient RelayClient { get; set; }
        public string Name
        {
            get
            {
                return Record.Name;
            }
            set
            {
                Record.Name = value;
            }
        }
        public string Rank
        {
            get
            {
                return Record.Rank;
            }
            set
            {
                Record.Rank = value;
            }
        }
        public string ClanName
        {
            get
            {
                return Record.ClanName;
            }
            set
            {
                Record.ClanName = value;
            }
        }
        public string Bio
        {
            get
            {
                return Record.Bio;
            }
            set
            {
                Record.Bio = value;
            }
        }
        public string Zone
        {
            get
            {
                return Record.Zone;
            }
            set
            {
                Record.Zone = value;
            }
        }
        public int Country
        {
            get
            {
                return Record.Country;
            }
            set
            {
                Record.Country = value;
            }
        }
        public int Age
        {
            get
            {
                return Record.Age;
            }
            set
            {
                Record.Age = value;
            }
        }
        public int FirstLogin
        {
            get
            {
                return Record.FirstLogin;
            }
            set
            {
                Record.FirstLogin = value;
            }
        }
        public int Type
        {
            get
            {
                return Record.Type;
            }
            set
            {
                Record.Type = value;
            }
        }
        public int Sex
        {
            get
            {
                return Record.Sex;
            }
            set
            {
                Record.Sex = value;
            }
        }
        public int Level
        {
            get
            {
                return Record.Level;
            }
            set
            {
                Record.Level = value;
            }
        }
        public int Exp
        {
            get
            {
                return Record.Exp;
            }
            set
            {
                Record.Exp = value;
            }
        }
        public RankEnum Licence
        {
            get
            {
                return (RankEnum)Record.Licence;
            }
            set
            {
                Record.Licence = (int)value;
            }
        }
        public int Cash
        {
            get
            {
                return Record.Cash;
            }
            set
            {
                Record.Cash = value;
            }
        }
        public int Gamecash
        {
            get
            {
                return Record.Gamecash;
            }
            set
            {
                Record.Gamecash = value;
            }
        }
        public int Coin
        {
            get
            {
                return Record.Coin;
            }
            set
            {
                Record.Coin = value;
            }
        }
        public int Questpoint
        {
            get
            {
                return Record.Questpoint;
            }
            set
            {
                Record.Questpoint = value;
            }
        }

        public bool IsLeader
        {
            get;
            set;
        }
        public bool IsReady
        {
            get;
            set;
        }
        public Room RoomConnected
        {
            get;
            set;
        }

        public void LogOut(SimpleClient client)
        {
            if (client != RelayClient)
                RoomConnected?.RemovePlayer(this);
            else
                RelayClient = null;
        }

        public ListCharacterInfoType GetListCharacterInfoType()
        {
            return new ListCharacterInfoType(1, Type, 1);
        }
        public ListCharacterInfoType2 GetListCharacterInfoType2()
        {
            return new ListCharacterInfoType2(0, Type, 0);
        }
        public PlayerGameInfoType GetPlayerGameInfoType()
        {
            return new PlayerGameInfoType(Name, Client.IP, (short)RoomConnected.GetPosition(this), IsLeader ? (byte)1 : (byte)0);
        }
        public IEnumerable<TrickInfoType> GetTricks()
        {
            string tricksHash = "1000,5,1|1100,4,1|1200,5,1|1300,5,1|1400,4,1|1500,3,1|1600,3,1|1700,3,1|1800,3,1|1900,5,1|2000,0,1|2200,3,1|5000,4,1";
            string[] tricks = tricksHash.Split('|');

            foreach(var trick in tricks)
            {
                string[] trickInfo = trick.Split(',');
                yield return new TrickInfoType(int.Parse(trickInfo[0]), int.Parse(trickInfo[1]), byte.Parse(trickInfo[2]));
            }
        }
    }
}
