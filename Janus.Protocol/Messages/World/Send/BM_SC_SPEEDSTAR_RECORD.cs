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
    public class BM_SC_SPEEDSTAR_RECORD : NetworkMessage
    {
        public const ushort Id = 2330;

        public IEnumerable<SpeedStarRecordType> speedStarRecordTypes;

        public BM_SC_SPEEDSTAR_RECORD()
        {
        }

        public BM_SC_SPEEDSTAR_RECORD(IEnumerable<SpeedStarRecordType> speedStarRecordTypes)
        {
            this.speedStarRecordTypes = speedStarRecordTypes;
        }

        public override ushort MessageId => Id;

        public override ServerEnum[] ServerEnum => new Enums.ServerEnum[] { Enums.ServerEnum.WorldServer };

        public override void Deserialize(IDataReader reader)
        {
            throw NetworkMessageException.DeserializeException;
        }

        public override void Serialize(IDataWriter writer)
        {
            foreach(SpeedStarRecordType record in speedStarRecordTypes)
            {
                record.Serialize(writer);
            }
        }
    }
}
