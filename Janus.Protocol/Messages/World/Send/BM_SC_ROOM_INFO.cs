using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Janus.Core.IO;
using Janus.Protocol.Enums;
using Janus.Protocol.Types;

namespace Janus.Protocol.Messages.World.Send
{
    public class BM_SC_ROOM_INFO : NetworkMessage
    {
        public const ushort Id = 2334;

        //2 bytes counter
        public short unk0;
        public short unk1;
        public IEnumerable<RoomListInfoType> roomListInfoTypes;

        public BM_SC_ROOM_INFO()
        {
        }

        public BM_SC_ROOM_INFO(short unk0, short unk1, IEnumerable<RoomListInfoType> roomListInfoTypes)
        {
            this.unk0 = unk0;
            this.unk1 = unk1;
            this.roomListInfoTypes = roomListInfoTypes;
        }

        public override ushort MessageId => Id;

        public override ServerEnum[] ServerEnum => new Enums.ServerEnum[] { Enums.ServerEnum.WorldServer };

        public override void Deserialize(IDataReader reader)
        {
            throw NetworkMessageException.DeserializeException;
        }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)roomListInfoTypes.Count());
            writer.WriteShort(unk0);
            writer.WriteShort(unk1);

            foreach(RoomListInfoType info in roomListInfoTypes)
            {
                info.Serialize(writer);
            }
        }
    }
}
