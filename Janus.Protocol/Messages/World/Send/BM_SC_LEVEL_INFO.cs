using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Janus.Core.IO;
using Janus.Protocol.Enums;

namespace Janus.Protocol.Messages.World.Send
{
    public class BM_SC_LEVEL_INFO : NetworkMessage
    {
        public const ushort Id = 2097;

        public uint exp;
        public uint level;
        public uint license;

        public BM_SC_LEVEL_INFO(uint exp, uint level, uint license)
        {
            this.exp = exp;
            this.level = level;
            this.license = license;
        }

        public BM_SC_LEVEL_INFO()
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
            writer.WriteUTFBytes(successString);
            writer.WriteBytes(new byte[16]);
            writer.WriteUInt(exp);
            writer.WriteUInt(level);
            writer.WriteUInt(license);
        }
    }
}
