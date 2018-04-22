using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Janus.Core.IO;
using Janus.Protocol.Enums;

namespace Janus.Protocol.Messages.Relay.Send
{
    public class NM_SC_LOGIN : NetworkMessage
    {
        public const ushort Id = 6002;

        //success string
        public string leaderIP;
        public short leaderPort;

        public NM_SC_LOGIN()
        {
        }

        public NM_SC_LOGIN(string leaderIP, short leaderPort)
        {
            this.leaderIP = leaderIP;
            this.leaderPort = leaderPort;
        }

        public override ushort MessageId => Id;

        public override ServerEnum[] ServerEnum => new Enums.ServerEnum[] { Enums.ServerEnum.RelayServer };

        public override void Deserialize(IDataReader reader)
        {
            throw NetworkMessageException.DeserializeException;
        }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteUTFBytes(successString);
            writer.WriteBytes(new byte[16]);

            writer.WriteUTFBytes(leaderIP, 20);
            writer.WriteShort(leaderPort);
        }
    }
}
