using Janus.Core.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Janus.Protocol.Types
{
    public class ItemInventoryType
    {
        public int numberId;
        public int itemId;
        public byte tradeStatus;
        //5 bytes empty
        public int duration;
        //4 bytes empty
        public short unk7;
        public byte equiped;
        public int unk8;

        public ItemInventoryType(int numberId, int itemId, byte tradeStatus, int duration, short unk7, byte equiped, int unk8)
        {
            this.numberId = numberId;
            this.itemId = itemId;
            this.tradeStatus = tradeStatus;
            this.duration = duration;
            this.unk7 = unk7;
            this.equiped = equiped;
            this.unk8 = unk8;
        }
        public void Serialize(IDataWriter writer)
        {
            writer.WriteInt(numberId);
            writer.WriteInt(itemId);
            writer.WriteByte(tradeStatus);
            writer.WriteBytes(new byte[5]);
            writer.WriteInt(duration);
            writer.WriteBytes(new byte[4]);
            writer.WriteShort(unk7);
            writer.WriteByte(equiped);
            writer.WriteInt(unk8);
        }
    }
}
