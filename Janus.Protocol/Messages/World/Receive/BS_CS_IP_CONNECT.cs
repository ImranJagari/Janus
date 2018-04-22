using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Janus.Core.IO;
using Janus.Protocol.Enums;

namespace Janus.Protocol.Messages.World.Receive
{
    public class BS_CS_IP_CONNECT : NetworkMessage
    {
        public const ushort Id = 2162;

        public string playerIP;
        //4 bytes away
        public short unk0;
        public short unk1;

        public override ushort MessageId => Id;

        public override ServerEnum[] ServerEnum => new Enums.ServerEnum[] { Enums.ServerEnum.WorldServer };

        public override void Deserialize(IDataReader reader)
        {
            playerIP = reader.ReadUTFBytes(19);
            reader.ReadInt();
        }

        public override void Serialize(IDataWriter writer)
        {
            throw new NotImplementedException();
        }
    }
}
