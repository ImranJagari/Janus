using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Janus.Core.IO;
using Janus.Protocol.Enums;

namespace Janus.Protocol.Messages.World.Send
{
    public class BM_SC_BALANCE_INFO : NetworkMessage
    {
        public const ushort Id = 2095;
        public static Int32 gamecash;
        public static Int32 coin;
        public static Int32 cash;
        public static Int32 questpoints;

        public BM_SC_BALANCE_INFO(int _gamecash, int _coin, int _cash, int _questpoints)
        {
            gamecash = _gamecash;
            coin = _coin;
            cash = _cash;
            questpoints = _questpoints;
        }

        public BM_SC_BALANCE_INFO()
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
            writer.WriteInt(gamecash);
            writer.WriteInt(coin);
            writer.WriteInt(cash);
            writer.WriteInt(questpoints);
        }
    }
}
