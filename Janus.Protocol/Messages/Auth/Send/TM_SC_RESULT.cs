using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Janus.Core.IO;
using Janus.Protocol.Enums;

namespace Janus.Protocol.Messages.Auth.Send
{
    /// <summary>
    /// In the ASM, if error is 0 and the psb_packetId is 1003, the client send the packet with the id 1004
    /// </summary>
    public class TM_SC_RESULT : NetworkMessage
    {
        public const ushort Id = 1001;

        public ushort psb_packetId;
        public ushort error;

        public TM_SC_RESULT()
        {
        }

        public TM_SC_RESULT(ushort psb_packetId, ushort error)
        {
            this.psb_packetId = psb_packetId;
            this.error = error;
        }



        public override ushort MessageId => Id;

        public override ServerEnum[] ServerEnum => new Enums.ServerEnum[] { Enums.ServerEnum.AuthServer };

        public override void Deserialize(IDataReader reader)
        {
            throw NetworkMessageException.DeserializeException;
        }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteUShort(psb_packetId);
            writer.WriteUShort(error);
        }
    }
}
