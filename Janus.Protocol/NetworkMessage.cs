using Janus.Core.IO;
using Janus.Protocol.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Janus.Protocol.Messages
{
    public class NetworkMessageException : Exception
    {
        public static Exception DeserializeException => new Exception("Packet non deserializable, it's a packet sended by the server !");
        public static Exception SerializeException => new Exception("Packet non serializable, it's a packet sended by the client !");
    }
    public abstract class NetworkMessage
    {
        public string successString = "SUCCESS\0";

        public abstract ushort MessageId { get; }
        public abstract ServerEnum[] ServerEnum { get; }
        /// <summary>
        /// Serialize the data of the packet
        /// </summary>
        /// <param name="writer">Writer for the buffer</param>
        public abstract void Serialize(IDataWriter writer);
        /// <summary>
        /// Deserialize the data of the packet
        /// </summary>
        /// <param name="reader"></param>
        public abstract void Deserialize(IDataReader reader);
        public void Pack(IDataWriter writer)
        {
            //Write Lenght + Id + Hash(???)
            writer.WriteUShort(0);

            writer.WriteUShort(MessageId);

            writer.WriteByte(0);

            //Write packet body
            Serialize(writer);

            //Write the exact lenght of the packet
            writer.Seek(0);
            writer.WriteUShort((ushort)writer.Data.Length);

            byte value = 0;
            value += (byte)(writer.Data.Length & 0xFF);
            value += (byte)((writer.Data.Length >> 8) & 0xFF);
            value += (byte)((writer.Data.Length >> 16) & 0xFF);
            value += (byte)((writer.Data.Length >> 24) & 0xFF);

            value += (byte)(MessageId & 0xFF);
            value += (byte)((MessageId >> 8) & 0xFF);
            writer.Seek(4);
            writer.WriteByte(value);
        }
        public void Unpack(IDataReader reader)
        {
            Deserialize(reader);
        }

        public override string ToString()
        {
            return base.ToString().Split('.').Last();
        }
    }
}
