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
    public class BM_SC_ROOM_LIST : NetworkMessage
    {
        public const ushort Id = 2304;

        //short compteur
        public short unk0;//must be to 0
        public short unk1;// must be to 0

        public IEnumerable<RoomInfoType> roomInfoTypes;

        public BM_SC_ROOM_LIST()
        {
        }

        public BM_SC_ROOM_LIST(short unk0, short unk1, IEnumerable<RoomInfoType> roomInfoTypes)
        {
            this.unk0 = unk0;
            this.unk1 = unk1;
            this.roomInfoTypes = roomInfoTypes;
        }

        public override ushort MessageId => Id;

        public override ServerEnum[] ServerEnum => new Enums.ServerEnum[] { Enums.ServerEnum.WorldServer };

        public override void Deserialize(IDataReader reader)
        {
            throw NetworkMessageException.DeserializeException;
        }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)roomInfoTypes.Count());
            writer.WriteShort(unk0);
            writer.WriteShort(unk1);

            foreach (RoomInfoType room in roomInfoTypes)
            {
                room.Serialize(writer);
            }
        }
    }
}
