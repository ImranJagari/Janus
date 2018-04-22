using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Janus.Core.IO;
using Janus.Protocol.Enums;

namespace Janus.Protocol.Messages.Auth.Receive
{
    public class TS_CS_ACCOUNT : NetworkMessage
    {
        public const ushort Id = 1003;

        public string login;
        public string password;

        public TS_CS_ACCOUNT()
        {
        }

        public TS_CS_ACCOUNT(string login, string password)
        {
            this.login = login;
            this.password = password;
        }

        public override ushort MessageId => Id;

        public override ServerEnum[] ServerEnum => new Enums.ServerEnum[] { Enums.ServerEnum.AuthServer };

        public override void Deserialize(IDataReader reader)
        {
            login = reader.ReadUTFBytes(19);
            password = reader.ReadUTFBytes(32);
        }

        public override void Serialize(IDataWriter writer)
        {
            throw NetworkMessageException.SerializeException;
        }
    }
}
