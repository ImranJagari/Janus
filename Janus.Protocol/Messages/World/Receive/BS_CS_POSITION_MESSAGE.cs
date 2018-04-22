using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Janus.Core.IO;
using Janus.Protocol.Enums;

namespace Janus.Protocol.Messages.World.Receive
{
    public class BS_CS_POSITION_MESSAGE : NetworkMessage
    {
        public const ushort Id = 2015;

        public float coord_x;
        public float coord_y;
        public float coord_angle_z;
        public float coord_z;


        public override ushort MessageId => Id;

        public override ServerEnum[] ServerEnum => new Enums.ServerEnum[] { Enums.ServerEnum.WorldServer };

        public override void Deserialize(IDataReader reader)
        {
            coord_x = reader.ReadFloat();
            coord_y = reader.ReadFloat();
            coord_z = reader.ReadFloat();
            coord_angle_z = reader.ReadFloat();
        }

        public override void Serialize(IDataWriter writer)
        {
            throw NetworkMessageException.SerializeException;
        }
    }
}
