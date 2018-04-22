using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Janus.Core.IO;
using Janus.Protocol.Enums;

namespace Janus.Protocol.Messages.World.Send
{
    public class BM_SC_MAP_INFO : NetworkMessage
    {
        public const ushort Id = 2164;

        public int mapId;

        public BM_SC_MAP_INFO()
        {
        }

        public BM_SC_MAP_INFO(int mapId)
        {
            this.mapId = mapId;
        }

        public override ushort MessageId => Id;

        public override ServerEnum[] ServerEnum => new Enums.ServerEnum[] { Enums.ServerEnum.WorldServer };

        public override void Deserialize(IDataReader reader)
        {
            throw NetworkMessageException.DeserializeException;
        }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteInt(mapId);
        }
    }
}
