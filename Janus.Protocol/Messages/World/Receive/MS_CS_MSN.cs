using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Janus.Core.IO;
using Janus.Protocol.Enums;

namespace Janus.Protocol.Messages.World.Receive
{
    public class MS_CS_MSN : NetworkMessage
    {
        public const ushort Id = 5001;

        public string sessionKey;
        public byte unk0;

        public override ushort MessageId => Id;

        public override ServerEnum[] ServerEnum => new Enums.ServerEnum[] { Enums.ServerEnum.WorldServer, Enums.ServerEnum.MsgServer };

        public override void Deserialize(IDataReader reader)
        {
            sessionKey = reader.ReadUTFBytes(33);
            unk0 = reader.ReadByte();
        }

        public override void Serialize(IDataWriter writer)
        {
            throw NetworkMessageException.SerializeException;
        }
    }
}
