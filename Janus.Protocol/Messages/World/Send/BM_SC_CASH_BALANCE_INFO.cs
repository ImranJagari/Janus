using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Janus.Core.IO;
using Janus.Protocol.Enums;

namespace Janus.Protocol.Messages.World.Send
{
    public class BM_SC_CASH_BALANCE_INFO : NetworkMessage
    {
        public const ushort Id = 2272;
        public int coin;

        public BM_SC_CASH_BALANCE_INFO(int _coin)
        {
            coin = _coin;
        }

        public BM_SC_CASH_BALANCE_INFO()
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
            writer.WriteInt(coin);
        }
    }
}
