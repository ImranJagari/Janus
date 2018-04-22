using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Janus.Core.IO;
using Janus.Protocol.Enums;

namespace Janus.Protocol.Messages.World.Send
{
    public class BM_SC_CHANNEL_LEAVE : NetworkMessage
    {
        public const ushort Id = 2010;

        public int channelId;

        public BM_SC_CHANNEL_LEAVE()
        {
        }

        public BM_SC_CHANNEL_LEAVE(int channelId)
        {
            this.channelId = channelId;
        }

        public override ushort MessageId => Id;

        public override ServerEnum[] ServerEnum => new Enums.ServerEnum[] { Enums.ServerEnum.WorldServer };

        public override void Deserialize(IDataReader reader)
        {
            throw NetworkMessageException.DeserializeException;
        }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteInt(channelId);
        }
    }
}
