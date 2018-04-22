using Janus.Core.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Janus.Protocol.Types
{
    public class RoomInfoType
    {
        public int id;
        public string name; // 24 bytes
        public int mode;
        public byte currentplayers;
        public byte maxplayers;
        public byte state;
        public byte level;

        public RoomInfoType(int id, string name, int mode, byte currentplayers, byte maxplayers, byte state, byte level)
        {
            this.id = id;
            this.name = name;
            this.mode = mode;
            this.currentplayers = currentplayers;
            this.maxplayers = maxplayers;
            this.state = state;
            this.level = level;
        }

        public void Serialize(IDataWriter writer)
        {
            writer.WriteInt(id);
            writer.WriteUTFBytes(name.PadRight(24, '\0'), 24);
            writer.WriteInt(mode);
            writer.WriteByte(currentplayers);
            writer.WriteByte(maxplayers);
            writer.WriteByte(state);
            writer.WriteByte(level);
        }

    }
}
