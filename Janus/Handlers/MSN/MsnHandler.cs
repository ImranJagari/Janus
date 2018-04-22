using Janus.Protocol.Messages.World.Receive;
using Janus.Protocol.Messages.World.Send;
using Janus.Server.Handlers;
using Janus.Server.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Janus.Handlers.MSN
{
    public class MsnHandler
    {
        [PacketHandler(MS_CS_MSN.Id)]//TODO : Find the utility of the variables and complete it
        public static void HandleMS_CS_MSN(SimpleClient client, MS_CS_MSN message)
        {
            client.Send(new MM_SC_MSN(1, 1));
        }
    }
}
