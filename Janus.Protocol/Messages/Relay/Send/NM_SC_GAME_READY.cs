using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Janus.Core.IO;
using Janus.Protocol.Enums;

namespace Janus.Protocol.Messages.Relay.Send
{
    public class NM_SC_GAME_READY : NetworkMessage
    {
        public const ushort Id = 6012;

        public int idx;

        public NM_SC_GAME_READY()
        {
        }

        public NM_SC_GAME_READY(int idx)
        {
            this.idx = idx;
        }

        public override ushort MessageId => Id;

        public override ServerEnum[] ServerEnum => new Enums.ServerEnum[] { Enums.ServerEnum.RelayServer };

        public override void Deserialize(IDataReader reader)
        {
            throw NetworkMessageException.DeserializeException;
        }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteInt(idx);
        }
    }
}
