using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Janus.Core.IO;
using Janus.Protocol.Enums;

namespace Janus.Protocol.Messages.World.Send
{
    public class BM_SC_QUEST_COIN : NetworkMessage
    {
        public const ushort Id = 2296;

        public string result;
        public int amount;

        public BM_SC_QUEST_COIN()
        {
        }

        public BM_SC_QUEST_COIN(string result, int amount)
        {
            this.result = result;
            this.amount = amount;
        }

        public override ushort MessageId => Id;

        public override ServerEnum[] ServerEnum => new Enums.ServerEnum[] { Enums.ServerEnum.WorldServer };

        public override void Deserialize(IDataReader reader)
        {
            throw NetworkMessageException.DeserializeException;
        }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteUTFBytes(result.PadRight(18,'\0'));
            writer.WriteBytes(new byte[6]);
            writer.WriteInt(amount);
        }
    }
}
