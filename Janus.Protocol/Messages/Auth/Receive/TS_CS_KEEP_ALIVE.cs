using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Janus.Core.IO;
using Janus.Protocol.Enums;

namespace Janus.Protocol.Messages.Auth.Receive
{
    public class TS_CS_KEEP_ALIVE : NetworkMessage
    {
        public const ushort Id = 2000;
        public TS_CS_KEEP_ALIVE()
        {
        }

        public override ushort MessageId => Id;

        public override ServerEnum[] ServerEnum => new Enums.ServerEnum[] { Enums.ServerEnum.AuthServer, Enums.ServerEnum.LobbyServer, Enums.ServerEnum.MsgServer, Enums.ServerEnum.RelayServer, Enums.ServerEnum.WorldServer };

        public override void Serialize(IDataWriter writer)
        {
        }

        public override void Deserialize(IDataReader reader)
        {
        }

        
    }
}
