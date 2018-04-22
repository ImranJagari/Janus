using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Janus.Core.IO;
using Janus.Protocol.Enums;

namespace Janus.Protocol.Messages.Auth.Send
{
    public class TM_SC_SERVER_SELECT : NetworkMessage
    {
        public const ushort Id = 1007;

        public short status;
        public int unknown0;
        public int unknown1;
        public int serverId;

        public TM_SC_SERVER_SELECT(short status, int unknown0, int unknown1, int serverId)
        {
            this.status = status;
            this.unknown0 = unknown0;
            this.unknown1 = unknown1;
            this.serverId = serverId;
        }

        public TM_SC_SERVER_SELECT()
        {
        }

        public override ushort MessageId => Id;

        public override ServerEnum[] ServerEnum => new Enums.ServerEnum[] { Enums.ServerEnum.AuthServer };

        public override void Deserialize(IDataReader reader)
        {
            throw NetworkMessageException.DeserializeException;
        }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort(status);
            writer.WriteInt(unknown0);
            writer.WriteInt(unknown1);
            writer.WriteInt(serverId);
        }
    }
}
