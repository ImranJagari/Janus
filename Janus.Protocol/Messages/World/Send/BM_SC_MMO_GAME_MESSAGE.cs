using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Janus.Core.IO;
using Janus.Protocol.Enums;

namespace Janus.Protocol.Messages.World.Send
{
    public class BM_SC_MMO_GAME_MESSAGE : NetworkMessage
    {
        public const ushort Id = 2018;

        public int unk0;

        public BM_SC_MMO_GAME_MESSAGE()
        {
        }

        public BM_SC_MMO_GAME_MESSAGE(int unk0)
        {
            this.unk0 = unk0;
        }

        public override ushort MessageId => Id;

        public override ServerEnum[] ServerEnum => new Enums.ServerEnum[] { Enums.ServerEnum.WorldServer };

        public override void Deserialize(IDataReader reader)
        {
            throw NetworkMessageException.DeserializeException;
        }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteInt(unk0);
        }
    }
}
