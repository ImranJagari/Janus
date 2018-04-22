using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Janus.Core.IO;
using Janus.Protocol.Enums;

namespace Janus.Protocol.Messages.World.Receive
{
    public class BS_CS_MMO_TICK_MESSAGE : NetworkMessage
    {
        public const ushort Id = 2019;

        public override ushort MessageId => Id;

        public override ServerEnum[] ServerEnum => new Enums.ServerEnum[] { Enums.ServerEnum.WorldServer, Enums.ServerEnum.RelayServer, Enums.ServerEnum.LobbyServer };

        public override void Deserialize(IDataReader reader)
        {
        }

        public override void Serialize(IDataWriter writer)
        {
            throw NetworkMessageException.SerializeException;
        }
    }
}
