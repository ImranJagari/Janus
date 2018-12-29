using Janus.Core.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Janus.Protocol.MessageReceiver;

namespace Janus.Protocol.Types
{
    public class ListCharacterInfoType
    {

        public byte psb_top;//maybe the top clothes
        public int characterType;
        public byte psb_bottom;//maybe the bottom clothes
        //14 bytes vide
        public int unk2 = 0;

        public int unk3 = 0;
        public int unk4 = 0;
        public int unk5 = 0;
        public int unk6 = 0;
        public int unk7 = 0;
        public int unk8 = 0;
        public int unk9 = 0;
        public int unk10 = 0;

        public ListCharacterInfoType(byte psb_top, int characterType, byte psb_bottom)
        {
            this.psb_top = psb_top;
            this.characterType = characterType;
            this.psb_bottom = psb_bottom;
        }

        public  void Deserialize(IDataReader reader)
        {
            throw NetworkMessageException.DeserializeException;
        }

        public  void Serialize(IDataWriter writer)
        {
            writer.WriteByte(psb_top);
            writer.WriteInt(characterType);
            writer.WriteByte(psb_bottom);

            writer.WriteBytes(new byte[14]);
            writer.WriteInt(unk2);
            writer.WriteInt(unk3);
            writer.WriteInt(unk4);
            writer.WriteInt(unk5);
            writer.WriteInt(unk6);
            writer.WriteInt(unk7);
            writer.WriteInt(unk8);
            writer.WriteInt(unk9);
            writer.WriteInt(unk10);
        }
    }
}
