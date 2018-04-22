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
    public class BM_SC_CHARACTER_LIST : NetworkMessage //TODO : find utility of the unknow data
    {
        public const ushort Id = 2065;//2318

        //17 bytes vide
        public int unk1 = 1;
        public string charname; // Charname, you can use some html code like: <#ff0000> and <glow> 41 bytes
        public byte unk2 = 2;
        public int unk3 = 3;
        public int unk4 = 4;
        public int level;
        public int unk5 = 5;
        //byte vide
        public short count;
        public IEnumerable<ListCharacterInfoType> listCharacterInfos;

        public BM_SC_CHARACTER_LIST()
        {
        }

        public BM_SC_CHARACTER_LIST(string charname, int level, short count, IEnumerable<ListCharacterInfoType> listCharacterInfos)
        {
            this.charname = charname;
            this.level = level;
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
            writer.WriteBytes(new byte[17]);
            writer.WriteInt(unk1);
            writer.WriteUTFBytes(charname.PadRight(41, '\0'), 41);
            writer.WriteByte(unk2);
            writer.WriteInt(unk3);
            writer.WriteInt(unk4);
            writer.WriteInt(level);
            writer.WriteInt(unk5);
            writer.WriteByte(0);
            writer.WriteShort(count);
            foreach(ListCharacterInfoType info in listCharacterInfos)
            {
                info.Serialize(writer);
            }
        }
    }
}
