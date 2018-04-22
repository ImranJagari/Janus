using Janus.Core.IO;
using Janus.Protocol.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Janus.Protocol.Messages.World.Send
{
    public class BM_SC_QUEST_COIN2 : NetworkMessage
    {
        public const ushort Id = 2298;

        public string result;

        public BM_SC_QUEST_COIN2()
        {
        }

        public BM_SC_QUEST_COIN2(string result)
        {
            this.result = result;
        }

        public override ushort MessageId => Id;

        public override ServerEnum[] ServerEnum => new Enums.ServerEnum[] { Enums.ServerEnum.WorldServer };

        public override void Deserialize(IDataReader reader)
        {
            throw NetworkMessageException.DeserializeException;
        }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteUTFBytes(result);
        }
    }
}
