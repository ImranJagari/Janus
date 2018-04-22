using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Janus.Core.IO;
using Janus.Protocol.Enums;

namespace Janus.Protocol.Messages.Auth.Receive
{
    public class TS_CS_SERVER_SELECT : NetworkMessage
    {
        public const ushort Id = 1006;

        public short idx;
        public short msgId;
        public short lobbyId;
        public short mmoId;

        public TS_CS_SERVER_SELECT()
        {
        }

        public TS_CS_SERVER_SELECT(short idx, short msgId, short lobbyId, short mmoId)
        {
            this.idx = idx;
            this.msgId = msgId;
            this.lobbyId = lobbyId;
            this.mmoId = mmoId;
        }

        public override ushort MessageId => Id;

        public override ServerEnum[] ServerEnum => new Enums.ServerEnum[] { Enums.ServerEnum.AuthServer };

        public override void Deserialize(IDataReader reader)
        {
            idx = reader.ReadShort();
            msgId = reader.ReadShort();
            lobbyId = reader.ReadShort();
            mmoId = reader.ReadShort();
        }

        public override void Serialize(IDataWriter writer)
        {
            throw NetworkMessageException.SerializeException;
        }
    }
}
