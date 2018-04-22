using Janus.Core.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Janus.Protocol.Types
{
    public class ListCharacterInfoType2
    {
        public int unk0;
        public int characterType;
        public int unk1;

        public ListCharacterInfoType2(int unk0, int characterType, int unk1)
        {
            this.unk0 = unk0;
            this.characterType = characterType;
            this.unk1 = unk1;
        }
        public void Deserialize(IDataReader reader)
        {
            unk0 = reader.ReadInt();
            characterType = reader.ReadInt();
            unk1 = reader.ReadInt();
        }

        public void Serialize(IDataWriter writer)
        {
            writer.WriteInt(unk0);
            writer.WriteInt(characterType);
            writer.WriteInt(unk1);
        }
    }
}
