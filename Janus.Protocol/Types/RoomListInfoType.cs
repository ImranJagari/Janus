using Janus.Core.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Janus.Protocol.Types
{
    public class RoomListInfoType
    {
        public int unk0;
        public int unk1;
        public long timestamp;
        public int unk2;
        public byte unk3;

        public RoomListInfoType(int unk0, int unk1, long timestamp, int unk2, byte unk3)
        {
            this.unk0 = unk0;
            this.unk1 = unk1;
            this.timestamp = timestamp;
            this.unk2 = unk2;
            this.unk3 = unk3;
        }
        public void Serialize(IDataWriter writer)
        {
            writer.WriteInt(unk0);
            writer.WriteInt(unk1);
            writer.WriteLong(timestamp);
            writer.WriteInt(unk2);
            writer.WriteByte(unk3);
        }
    }
}
