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
    public class BM_SC_CHARACTER_LIST2 : NetworkMessage
    {
        public const ushort Id = 2318;

        public byte[] unk0 = new byte[17];
        public string charname; // Charname, you can use some html code like: <#ff0000> and <glow> 41 bytes
        public short unk1;
        public short count;
        public IEnumerable<ListCharacterInfoType2> listCharacterInfos;

        public BM_SC_CHARACTER_LIST2()
        {
        }

        public BM_SC_CHARACTER_LIST2( string charname, short unk1, short count, IEnumerable<ListCharacterInfoType2> listCharacterInfos)
        {
            this.charname = charname;
            this.unk1 = unk1;
            this.count = count;
            this.listCharacterInfos = listCharacterInfos;
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
            writer.WriteBytes(unk0);
            writer.WriteUTFBytes(charname.PadRight(41, '\0'), 41);
            writer.WriteShort(unk1);
            writer.WriteShort(count);
            foreach (ListCharacterInfoType2 info in listCharacterInfos)
            {
                info.Serialize(writer);
            }
        }
    }
}
