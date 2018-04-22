using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Janus.Core.IO;
using Janus.Protocol.Enums;

namespace Janus.Protocol.Messages.World.Receive
{
    public class BS_CS_CHANGE_SCENE : NetworkMessage
    {
        public const ushort Id = 2358;

        public int psb_sceneId;

        public override ushort MessageId => Id;

        public override ServerEnum[] ServerEnum => new Enums.ServerEnum[] { Enums.ServerEnum.WorldServer, Enums.ServerEnum.LobbyServer, Enums.ServerEnum.RelayServer };

        public override void Deserialize(IDataReader reader)
        {
            psb_sceneId = reader.ReadInt();
        }

        public override void Serialize(IDataWriter writer)
        {
            throw NetworkMessageException.SerializeException;
        }
    }
}
