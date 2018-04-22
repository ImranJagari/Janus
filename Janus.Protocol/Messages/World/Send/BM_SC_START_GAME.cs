using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Janus.Core.IO;
using Janus.Protocol.Enums;
using Janus.Protocol.Types;

namespace Janus.Protocol.Messages.World.Send
{
    public class BM_SC_START_GAME : NetworkMessage
    {
        public const ushort Id = 2190;

        //success string
        //16 bytes empty
        public string encryptionKey;//16 bytes with \0
        //short countPlayer
        public IEnumerable<PlayerGameInfoType> playerGameInfoTypes;

        public BM_SC_START_GAME()
        {
        }

        public BM_SC_START_GAME(string encryptionKey, IEnumerable<PlayerGameInfoType> playerGameInfoTypes)
        {
            this.encryptionKey = encryptionKey;
            this.playerGameInfoTypes = playerGameInfoTypes;
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
            writer.WriteUTFBytes(encryptionKey, 16);
            writer.WriteShort((short)playerGameInfoTypes.Count());
            foreach(PlayerGameInfoType playerInfo in playerGameInfoTypes)
            {
                playerInfo.Serialize(writer);
            }
        }
    }
}
