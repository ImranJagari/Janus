using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Janus.Core.IO;
using Janus.Protocol.Enums;

namespace Janus.Protocol.Messages.World.Send
{
    public class BM_SC_LOGIN : NetworkMessage
    {
        public const ushort Id = 2145;

        public int permission;
        public int unk0;


        public BM_SC_LOGIN(int permission, int unk0)
        {
            this.permission = permission;
            this.unk0 = unk0;
        }

        public BM_SC_LOGIN()
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
            writer.WriteInt(permission);
            writer.WriteBytes(new byte[278]);
            writer.WriteInt(unk0);
        }
    }
}
