using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Janus.Core.IO;
using Janus.Protocol.Enums;

namespace Janus.Protocol.Messages.World.Send
{
    public class BM_SC_MISSION_LIST : NetworkMessage
    {
        public const ushort Id = 2073;


        public byte active;
        public IEnumerable<short> missionList;

        public BM_SC_MISSION_LIST(byte active, IEnumerable<short> missionList)
        {
            this.active = active;
            this.missionList = missionList;
        }

        public BM_SC_MISSION_LIST()
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
            writer.WriteShort((short)missionList.Count());
            writer.WriteByte(active);

            foreach(short mission in missionList)
            {
                writer.WriteShort(mission);
            }
        }
    }
}
