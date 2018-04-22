using Janus.Core.IO;
using Janus.Protocol.Enums;
using Janus.Protocol.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Janus.Protocol.Messages.World.Send
{
    public class BM_SC_TRICK_LIST : NetworkMessage
    {
        public const ushort Id = 2105;
        
        public Int16 trickCount = 13;
        public IEnumerable<TrickInfoType> trickInfos;

        public BM_SC_TRICK_LIST(IEnumerable<TrickInfoType> trickInfos)
        {
            this.trickInfos = trickInfos;
        }

        public BM_SC_TRICK_LIST()
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
            writer.WriteBytes(new byte[17]);
            writer.WriteShort((short)trickInfos.Count());

            foreach (TrickInfoType trick in trickInfos)
            {
                trick.Serialize(writer);
            }
        }
    }
}
