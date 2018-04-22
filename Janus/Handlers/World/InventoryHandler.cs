using Janus.Protocol.Messages.World.Receive;
using Janus.Protocol.Messages.World.Send;
using Janus.Protocol.Types;
using Janus.Server.Handlers;
using Janus.Server.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Janus.Handlers.World
{
    public class InventoryHandler
    {
        [PacketHandler(BS_CS_INVENTORY.Id)]//TODO : Create the data for the inventory and handle it here
        public static void HandleBS_CS_INVENTORY(SimpleClient client, BS_CS_INVENTORY message)
        {
            client.Send(new BM_SC_INVENTORY(new ItemInventoryType[0]));
        }
    }
}
