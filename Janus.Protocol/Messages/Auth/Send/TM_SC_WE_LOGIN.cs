using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Janus.Core.IO;
using Janus.Protocol.Enums;

namespace Janus.Protocol.Messages.Auth.Send
{
    public class TM_SC_WE_LOGIN : NetworkMessage
    {
        public const ushort Id = 1009;

        public int psb_isKeyValid;
        public string sessionKey;
        public int permission;
        public byte psb_isPCRoom;

        public TM_SC_WE_LOGIN(int psb_isKeyValid, string sessionKey, int permission, byte psb_isPCRoom)
        {
            this.psb_isKeyValid = psb_isKeyValid;
            this.sessionKey = sessionKey;
            this.permission = permission;
            this.psb_isPCRoom = psb_isPCRoom;
        }

        public TM_SC_WE_LOGIN()
        {
        }

        public override ushort MessageId => Id;

        public override ServerEnum[] ServerEnum => new Enums.ServerEnum[] { Enums.ServerEnum.AuthServer };

        public override void Deserialize(IDataReader reader)
        {
            throw NetworkMessageException.DeserializeException;
        }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteInt(psb_isKeyValid);
            writer.WriteUTFBytes(sessionKey.PadRight(33, '\0'), 33);
            writer.WriteInt(permission);
            writer.WriteByte(psb_isPCRoom);
        }
    }
}
