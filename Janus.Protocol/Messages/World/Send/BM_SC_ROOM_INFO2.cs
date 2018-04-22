using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Janus.Core.IO;
using Janus.Protocol.Enums;

namespace Janus.Protocol.Messages.World.Send
{
    public class BM_SC_ROOM_INFO2 : NetworkMessage
    {
        public const ushort Id = 2170;

        public string roomName;
        public int unk0;
        public int unk1;
        public byte isSingleTestMode;

        public BM_SC_ROOM_INFO2()
        {
        }

        public BM_SC_ROOM_INFO2(string roomName, int unk0, int unk1, byte isSingleTestMode)
        {
            this.roomName = roomName;
            this.unk0 = unk0;
            this.unk1 = unk1;
            this.isSingleTestMode = isSingleTestMode;
        }

        public override ushort MessageId => Id;

        public override ServerEnum[] ServerEnum => new Enums.ServerEnum[] { Enums.ServerEnum.WorldServer };

        public override void Deserialize(IDataReader reader)
        {
            throw NetworkMessageException.DeserializeException;
        }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteUTFBytes(roomName, 16);
            writer.WriteBytes(new byte[61]);
            writer.WriteInt(unk0);
            writer.WriteInt(unk1);
            writer.WriteByte(isSingleTestMode);

        }
    }
}
