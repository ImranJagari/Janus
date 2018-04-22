using Janus.Core.Reflection;
using Janus.Server.Network;
using Janus.Protocol.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Janus.Server.Handlers
{
    public class PacketManager
    {
        public static Dictionary<ushort, Action<object, SimpleClient, NetworkMessage>> MethodHandlers = new Dictionary<ushort, Action<object, SimpleClient, NetworkMessage>>();
        public static void Initialize(Assembly asm)
        {
            var methods = asm.GetTypes()
                      .SelectMany(t => t.GetMethods())
                      .Where(m => m.GetCustomAttributes(typeof(PacketHandlerAttribute), false).Length > 0)
                      .ToArray();
            foreach (var method in methods)
            {
                var action =  DynamicExtension.CreateDelegate(method, typeof(SimpleClient), typeof(NetworkMessage)) as Action<object, SimpleClient, NetworkMessage>;
                foreach (var customAttribute in method.CustomAttributes)
                    MethodHandlers.Add((ushort)customAttribute.ConstructorArguments[0].Value, action);
            }
        }
        public static void ParseHandler(SimpleClient client, NetworkMessage message)
        {
            Action<object, SimpleClient, NetworkMessage> methodToInvok;
            if (message != null)
            {
                if (MethodHandlers.TryGetValue((ushort)message.MessageId, out methodToInvok))
                {
                    methodToInvok.Invoke(null, client, message);
                }
                else
                {
                    client.logger.Warn(string.Format("Received non handled Packet : id = {0} -> {1}", message.MessageId, message));
                }
            }
            else
            {
                client.logger.Error("Received empty packet !");
                client.Disconnect();
            }
        }
    }
}
