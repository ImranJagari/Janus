using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Janus.Core.Extensions;
using Janus.Core.IO;
using Janus.Protocol.Enums;

namespace Janus.Protocol.Messages.Relay.Send
{
    public class NM_SC_GET_SERVER_TICK : NetworkMessage
    {
        public const ushort Id = 6029;

        //success string
        public string leaderIP;
        public byte quiet;

        public NM_SC_GET_SERVER_TICK()
        {
        }


        public override ushort MessageId => Id;

        public override ServerEnum[] ServerEnum => new Enums.ServerEnum[] { Enums.ServerEnum.RelayServer };

        public override void Deserialize(IDataReader reader)
        {
            throw NetworkMessageException.DeserializeException;
        }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteInt(DateTime.Now.GetUnixTimeStamp());
        }
    }
}
