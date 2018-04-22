using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Janus.Core.IO;
using Janus.Protocol.Enums;

namespace Janus.Protocol.Messages.Relay.Receive
{
    public class NS_CS_SYNCHRO : NetworkMessage
    {
        public const ushort Id = 6020;

        public string sessionKey;

        public override ushort MessageId => Id;

        public override ServerEnum[] ServerEnum => new Enums.ServerEnum[] { Enums.ServerEnum.RelayServer };

        public override void Deserialize(IDataReader reader)
        {
            reader.ReadInt();
            reader.ReadShort();
            sessionKey = reader.ReadUTFBytes(33);
        }

        public override void Serialize(IDataWriter writer)
        {
            throw NetworkMessageException.SerializeException;
        }
    }
}
