using Janus.Core.Encryption;
using Janus.Core.IO;
using System;
namespace Janus.Protocol
{
    public class MessagePart
    {
        private byte[] m_data;
        private byte[] m_completeData;

        public ushort? Header { get; private set; }

        public ushort? MessageId
        {
            get
            {
                if (!this.Header.HasValue)
                    return new ushort?();
                ushort? header = this.Header;
                return header.HasValue ? header.Value : new ushort?();
            }
        }

        public uint? Length { get; private set; }

        public byte[] Data
        {
            get
            {
                return this.m_data;
            }
            private set
            {
                this.m_data = value;
            }
        }

        public byte[] CompleteData
        {
            get
            {
                return this.m_completeData;
            }
            private set
            {
                this.m_completeData = value;
            }
        }
        public MessagePart() { }
        public MessagePart(MessagePart messagePart)
        {
            this.CompleteData = messagePart.CompleteData;
            this.Data = messagePart.Data;
            this.Header = messagePart.Header;
            this.Length = messagePart.Length;
        }
        public bool Build(ref BigEndianReader reader, bool decrypt = true)
        {
            if(reader.Data.Length < 5)
            {
                return false;
            }

            byte[] data = reader.Data;

            CompleteData = data;

            long position = reader.Position;
            reader = new BigEndianReader(data);
            reader.Seek((int)position, System.IO.SeekOrigin.Begin);

            Length = reader.ReadUShort();
            Header = reader.ReadUShort();
            reader.ReadByte(); // Hash
            
            Data = reader.ReadBytes((int)Length < 5 ? (int)reader.BytesAvailable : (int)Length - 5);
            //reader.Data.CopyTo(Data, sizeof(uint) + sizeof(ushort) - 1);
            return true;
        }
    }
}
