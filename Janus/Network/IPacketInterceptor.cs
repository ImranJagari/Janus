using Janus.Protocol.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Janus.Server.Network
{
    public interface IPacketInterceptor
    {
        void Send(NetworkMessage message, bool encrypt = true);
    }
}
