using Janus.Core.IO;
using Janus.Protocol.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Janus.Protocol.Messages.World.Send
{
    public class MM_SC_MSN : NetworkMessage
    {
        public const ushort Id = 5002;

        public int unk0;
        public int unk1;

        public MM_SC_MSN()
        {
        }

        public MM_SC_MSN(int unk0, int unk1)
        {
            this.unk0 = unk0;
            this.unk1 = unk1;
        }

        public override ushort MessageId => Id;

        public override ServerEnum[] ServerEnum => new Enums.ServerEnum[] { Enums.ServerEnum.WorldServer, Enums.ServerEnum.MsgServer };

        public override void Deserialize(IDataReader reader)
        {
            throw NetworkMessageException.DeserializeException;
        }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteUTFBytes(successString);
            writer.WriteBytes(new byte[16]);
            writer.WriteInt(unk0);
            writer.WriteInt(unk1);
        }
    }
}
