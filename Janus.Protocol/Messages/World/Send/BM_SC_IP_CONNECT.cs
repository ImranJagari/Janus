using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Janus.Core.IO;
using Janus.Protocol.Enums;

namespace Janus.Protocol.Messages.World.Send
{
    public class BM_SC_IP_CONNECT : NetworkMessage //TODO: We must find where it's handled in the code
    {
        public const ushort Id = 2163;

        //successString
        //16 bytes
        public string playerIP;
        public short port;

        public BM_SC_IP_CONNECT()
        {
        }

        public BM_SC_IP_CONNECT(string playerIP, short port)
        {
            this.playerIP = playerIP;
            this.port = port;
        }

        public override ushort MessageId => Id;

        public override ServerEnum[] ServerEnum => new Enums.ServerEnum[] { Enums.ServerEnum.WorldServer };

        public override void Deserialize(IDataReader reader)
        {
            throw NetworkMessageException.DeserializeException;
        }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteUTFBytes(successString);
            writer.WriteBytes(new byte[16]);
            writer.WriteUTFBytes(playerIP, 16);
            writer.WriteShort(port);
        }
    }
}
