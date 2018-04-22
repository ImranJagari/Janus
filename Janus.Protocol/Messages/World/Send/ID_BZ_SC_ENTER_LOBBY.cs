using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Janus.Core.IO;
using Janus.Protocol.Enums;

namespace Janus.Protocol.Messages.World.Send
{
    public class ID_BZ_SC_ENTER_LOBBY : NetworkMessage
    {
        public const ushort Id = 19019;

        public int unk0; // must not be 0

        public ID_BZ_SC_ENTER_LOBBY()
        {
        }

        public ID_BZ_SC_ENTER_LOBBY(int unk0)
        {
            this.unk0 = unk0;
        }

        public override ushort MessageId => Id;

        public override ServerEnum[] ServerEnum => new Enums.ServerEnum[] { Enums.ServerEnum.LobbyServer };

        public override void Deserialize(IDataReader reader)
        {
            throw NetworkMessageException.DeserializeException;
        }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteUTFBytes(successString);
            writer.WriteBytes(new byte[16]);
            writer.WriteInt(unk0);
            writer.WriteInt(unk0);
            writer.WriteInt(unk0);
            writer.WriteInt(unk0);
            writer.WriteInt(unk0);
            writer.WriteInt(unk0);
            writer.WriteInt(unk0);
            writer.WriteInt(unk0);
            writer.WriteInt(unk0);
        }
    }
}
