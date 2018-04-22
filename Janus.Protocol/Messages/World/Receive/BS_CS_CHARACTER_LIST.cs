using Janus.Core.IO;
using Janus.Protocol.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Janus.Protocol.Messages.World.Receive
{
    public class BS_CS_CHARACTER_LIST : NetworkMessage
    {
        public const ushort Id = 2317;
        public string sessionKey;

        public BS_CS_CHARACTER_LIST(string sessionKey)
        {
            this.sessionKey = sessionKey;
        }

        public BS_CS_CHARACTER_LIST()
        {
        }

        public override ushort MessageId => Id;

        public override ServerEnum[] ServerEnum => new Enums.ServerEnum[] { Enums.ServerEnum.WorldServer };

        public override void Deserialize(IDataReader reader)
        {
            sessionKey = reader.ReadUTFBytes(33);
        }

        public override void Serialize(IDataWriter writer)
        {
            throw NetworkMessageException.SerializeException;
        }
    }
}
