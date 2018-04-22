using Janus.Core.IO;
using Janus.Core.Reflection;
using Janus.Protocol.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Janus.Protocol
{
    public class MessageReceiver
    {
        private static readonly Dictionary<ushort, Type> Messages = new Dictionary<ushort, Type>();
        private static readonly Dictionary<ushort, Func<NetworkMessage>> Constructors = new Dictionary<ushort, Func<NetworkMessage>>();

        public static void Initialize()
        {
            var assembly = Assembly.GetAssembly(typeof(MessageReceiver));
            foreach (var type in assembly.GetTypes().Where(x => x.IsSubclassOf(typeof(NetworkMessage))))
            {
                var field = type.GetField("Id");
                if (field != null)
                {
                    var num = (ushort)field.GetValue(type);
                    if (type.Name.Contains("_SC_"))
                        continue;
                    if (Messages.ContainsKey(num))
                    {
                        throw new AmbiguousMatchException(
                            $"MessageReceiver() => {num} item is already in the dictionary, old type is : {Messages[num]}, new type is  {type}");
                    }
                    Messages.Add(num, type);
                    var constructor = type.GetConstructor(Type.EmptyTypes);
                    if (constructor == null)
                    {
                        throw new Exception($"'{type}' doesn't implemented a parameterless constructor");
                    }
                    Constructors.Add(num, constructor.CreateDelegate<Func<NetworkMessage>>());
                }
            }
            

        }

        public static NetworkMessage BuildMessage(ushort id, IDataReader reader)
        {
            if (!Messages.ContainsKey(id))
            {
                return null;
            }
            var message = Constructors[id]();
            if (message == null)
            {
                return null;
            }
            message.Unpack(reader);
            return message;
        }

      

        public static Type GetMessageType(ushort id)
        {
            if (!Messages.ContainsKey(id))
            {
                return null;
            }
            return Messages[id];
        }

        [Serializable]
        public class MessageNotFoundException : Exception
        {
            public MessageNotFoundException()
            {
            }

            public MessageNotFoundException(string message)
                : base(message)
            {
            }

            public MessageNotFoundException(string message, Exception inner)
                : base(message, inner)
            {
            }

            protected MessageNotFoundException(SerializationInfo info, StreamingContext context)
                : base(info, context)
            {
            }
        }
        public class NetworkMessageException : Exception
        {
            public static Exception DeserializeException => new Exception("Packet non deserializable, it's a packet sended by the server !");
            public static Exception SerializeException => new Exception("Packet non serializable, it's a packet sended by the client !");
        }
    }
}