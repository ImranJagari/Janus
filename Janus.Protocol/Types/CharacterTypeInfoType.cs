using Janus.Core.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Janus.Protocol.Types
{
    public class CharacterTypeInfoType
    {
        public byte unk0;
        public byte unk1;

        public CharacterTypeInfoType(byte unk0, byte unk1)
        {
            this.unk0 = unk0;
            this.unk1 = unk1;
        }

        public void Deserialize(IDataReader reader)
        {
            this.unk0 = reader.ReadByte();
            this.unk1 = reader.ReadByte();
        }

        public void Serialize(IDataWriter writer)
        {
            writer.WriteByte(unk0);
            writer.WriteByte(unk1);
            writer.WriteBytes(new byte[3]);
        }
    }
}
