using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Janus.Core.IO;
using Janus.Protocol.Enums;
using Janus.Protocol.Types;

namespace Janus.Protocol.Messages.World.Receive
{
    public class BM_SC_CHARACTER_TYPE_LIST : NetworkMessage
    {
        public const ushort Id = 2063;

        public short count;
        public IEnumerable<CharacterTypeInfoType> characterTypeInfos;

        public BM_SC_CHARACTER_TYPE_LIST(short count, IEnumerable<CharacterTypeInfoType> characterTypeInfos)
        {
            this.count = count;
            this.characterTypeInfos = characterTypeInfos;
        }

        public BM_SC_CHARACTER_TYPE_LIST()
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
            writer.WriteBytes(new byte[24]);
            writer.WriteShort(count);

            foreach(var infoType in characterTypeInfos)
            {
                infoType.Serialize(writer);
            }
        }
    }
}
