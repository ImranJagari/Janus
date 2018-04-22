using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Janus.Core.IO;
using Janus.Protocol.Enums;

namespace Janus.Protocol.Messages.World.Receive
{
    public class BS_CS_CREATE_ROOM : NetworkMessage
    {
        public const ushort Id = 2173;

        public string name; //24 bytes
        public string password;//4 bytes
        //48 bytes vide
        public byte maxPlayers;
        public int mode;
        public byte rank;

        public override ushort MessageId => Id;

        public override ServerEnum[] ServerEnum => new Enums.ServerEnum[] { Enums.ServerEnum.WorldServer };

        public override void Deserialize(IDataReader reader)
        {
            name = reader.ReadUTFBytes(24);
            password = reader.ReadUTFBytes(4);
            reader.ReadBytes(48);
            maxPlayers = reader.ReadByte();
            mode = reader.ReadInt();
            rank = reader.ReadByte();
        }

        public override void Serialize(IDataWriter writer)
        {
            throw new NotImplementedException();
        }
    }
}
