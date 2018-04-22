using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Janus.Core.IO;
using Janus.Protocol.Enums;
using Janus.Protocol.Types;

namespace Janus.Protocol.Messages.World.Send
{
    public class BM_SC_QUEST_LIST : NetworkMessage
    {
        public const ushort Id = 2107;

        public IEnumerable<QuestInfoType> questInfoTypes;

        public BM_SC_QUEST_LIST()
        {
        }

        public BM_SC_QUEST_LIST(IEnumerable<QuestInfoType> questInfoTypes)
        {
            this.questInfoTypes = questInfoTypes;
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
            writer.WriteShort((short)questInfoTypes.Count());
            foreach(QuestInfoType questInfo in questInfoTypes)
            {
                questInfo.Serialize(writer);
            }
        }
    }
}
