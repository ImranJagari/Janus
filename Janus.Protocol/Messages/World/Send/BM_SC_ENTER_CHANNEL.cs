using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Janus.Core.IO;
using Janus.Protocol.Enums;

namespace Janus.Protocol.Messages.World.Send
{
    public class BM_SC_ENTER_CHANNEL : NetworkMessage
    {
        public const ushort Id = 2008;
        public static Byte channel_id;

        public BM_SC_ENTER_CHANNEL(byte _channel_id)
        {
            channel_id = _channel_id;
        }

        public BM_SC_ENTER_CHANNEL()
        {
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
            writer.WriteInt(0);
            writer.WriteInt(0);
            writer.WriteInt(0);
            writer.WriteInt(0);
            writer.WriteByte(channel_id);
            writer.WriteShort(channel_id);
        }
    }
}
