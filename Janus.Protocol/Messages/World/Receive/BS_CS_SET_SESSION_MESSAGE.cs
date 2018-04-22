using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Janus.Core.IO;
using Janus.Protocol.Enums;

namespace Janus.Protocol.Messages.World.Receive
{
    public class BS_CS_SET_SESSION_MESSAGE : NetworkMessage
    {
        public const ushort Id = 2120;
        public string sessionMessage;

        public override ushort MessageId => Id;

        public override ServerEnum[] ServerEnum => new Enums.ServerEnum[] { Enums.ServerEnum.WorldServer, Enums.ServerEnum.LobbyServer };

        public override void Deserialize(IDataReader reader)
        {
            sessionMessage = reader.ReadUTFBytes((ushort)reader.BytesAvailable);
        }

        public override void Serialize(IDataWriter writer)
        {
            throw NetworkMessageException.DeserializeException;
        }
    }
}
