using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Janus.Core.IO;
using Janus.Protocol.Enums;

namespace Janus.Protocol.Messages.World.Send
{
    public class BM_SC_QUEST_UNKNOWN : NetworkMessage
    {
        public const ushort Id = 2361;

        public int unk0;

        public BM_SC_QUEST_UNKNOWN()
        {
        }

        public BM_SC_QUEST_UNKNOWN(int unk0)
        {
            this.unk0 = unk0;
        }

        public override ushort MessageId => Id;

        public override ServerEnum[] ServerEnum => throw new NotImplementedException();

        public override void Deserialize(IDataReader reader)
        {
            throw NetworkMessageException.DeserializeException;
        }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteUTFBytes(successString);
            writer.WriteBytes(new byte[17]);
            writer.WriteInt(unk0);
        }
    }
}
