using Janus.Core.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Janus.Protocol.Types
{
    public class TrickInfoType
    {
        public int id;
        public int level;
        public byte apply;

        public TrickInfoType(int id, int level, byte apply)
        {
            this.id = id;
            this.level = level;
            this.apply = apply;
        }
        public void Serialize(IDataWriter writer)
        {
            writer.WriteInt(id);
            writer.WriteInt(level);
            writer.WriteByte(apply);
        }
    }
}
