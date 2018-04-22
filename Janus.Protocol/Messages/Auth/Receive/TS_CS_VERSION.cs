using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Janus.Core.IO;
using Janus.Protocol.Enums;

namespace Janus.Protocol.Messages.Auth.Receive
{
    public class TS_CS_VERSION : NetworkMessage
    {
        public const ushort Id = 1002;

        public string version;
        public string lang;

        public TS_CS_VERSION(string version, string lang)
        {
            this.version = version;
            this.lang = lang;
        }

        public TS_CS_VERSION()
        {
        }

        public override ushort MessageId => Id;

        public override ServerEnum[] ServerEnum => new Enums.ServerEnum[] { Enums.ServerEnum.AuthServer };

        public override void Deserialize(IDataReader reader)
        {
            version = reader.ReadUTFBytes(20);
            lang = reader.ReadUTFBytes(3);
        }

        public override void Serialize(IDataWriter writer)
        {
            throw NetworkMessageException.SerializeException;
        }
    }
}
