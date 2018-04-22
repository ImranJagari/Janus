using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Janus.Core.IO;
using Janus.Protocol.Enums;

namespace Janus.Protocol.Messages.Relay.Send
{
    public class NM_SC_START_GAME : NetworkMessage
    {
        public const ushort Id = 6013;

        public int startTick;
        public int delayedTicks;

        public NM_SC_START_GAME()
        {
        }

        public NM_SC_START_GAME(int startTick, int delayedTicks)
        {
            this.startTick = startTick;
            this.delayedTicks = delayedTicks;
        }

        public override ushort MessageId => Id;

        public override ServerEnum[] ServerEnum => new Enums.ServerEnum[] { Enums.ServerEnum.RelayServer };

        public override void Deserialize(IDataReader reader)
        {
            throw NetworkMessageException.DeserializeException;
        }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteInt(startTick);
            writer.WriteInt(delayedTicks);
        }
    }
}
