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
    public class BM_SC_INVENTORY : NetworkMessage
    {
        public const ushort Id = 2099;

        public IEnumerable<ItemInventoryType> items;

        public BM_SC_INVENTORY(IEnumerable<ItemInventoryType> items)
        {
            this.items = items;
        }

        public BM_SC_INVENTORY()
        {
        }

        public override ushort MessageId => Id;

        public override ServerEnum[] ServerEnum => new ServerEnum[] { Enums.ServerEnum.WorldServer };

        public override void Deserialize(IDataReader reader)
        {
            throw NetworkMessageException.DeserializeException;
        }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteUTFBytes(successString);
            writer.WriteBytes(new byte[17]);
            writer.WriteShort((short)items.Count());
            foreach(ItemInventoryType item in items)
            {
                item.Serialize(writer);
            }
        }
    }
}
