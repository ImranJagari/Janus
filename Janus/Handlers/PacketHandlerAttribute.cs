using Janus.Protocol.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Janus.Server.Handlers
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class PacketHandlerAttribute : Attribute
    {
        public ushort MessageId
        {
            get;
            set;
        }
        public PacketHandlerAttribute(ushort messageId)
        {
            MessageId = messageId;
        }
    }
}
