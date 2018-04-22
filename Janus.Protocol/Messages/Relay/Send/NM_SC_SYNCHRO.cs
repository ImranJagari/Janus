using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Janus.Core.IO;
using Janus.Protocol.Enums;

namespace Janus.Protocol.Messages.Relay.Send
{
    public class NM_SC_SYNCHRO : NetworkMessage
    {
        public const ushort Id = 6021;

        public short unk0;//must be set to 11

        public NM_SC_SYNCHRO()
        {
        }

        public NM_SC_SYNCHRO(short unk0)
        {
            this.unk0 = unk0;
        }

        public override ushort MessageId => Id;

        public override ServerEnum[] ServerEnum => new Enums.ServerEnum[] { Enums.ServerEnum.RelayServer };

        public override void Deserialize(IDataReader reader)
        {
            throw NetworkMessageException.DeserializeException;
        }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort(unk0);
        }
    }
}
