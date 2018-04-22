using Janus.Core.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Janus.Protocol.Types
{
    public class QuestInfoType
    {
        public int questId;
        public byte unk; //Must be to 1

        public QuestInfoType(int questId, byte unk)
        {
            this.questId = questId;
            this.unk = unk;
        }
        public void Serialize(IDataWriter writer)
        {
            writer.WriteInt(questId);
            writer.WriteBytes(new byte[4]);
            writer.WriteByte(unk);
            writer.WriteBytes(new byte[5]);
        }
    }
}
