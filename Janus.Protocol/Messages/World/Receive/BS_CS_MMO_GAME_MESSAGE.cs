using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Janus.Core.IO;
using Janus.Protocol.Enums;

namespace Janus.Protocol.Messages.World.Receive
{
    public class BS_CS_MMO_GAME_MESSAGE : NetworkMessage
    {
        public const ushort Id = 2017;
        public uint unk1;
        public ushort counter;
        public uint keystate;

        public char uk;

        //coords
        public int unk2;
        public int unk3;
        public int unk4;
        public int unk5;
        public override ushort MessageId => Id;

        public override ServerEnum[] ServerEnum => new Enums.ServerEnum[] { Enums.ServerEnum.WorldServer };

        public override void Deserialize(IDataReader reader)
        {
            if (reader.BytesAvailable >= 38)
            {
                unk1 = reader.ReadUInt();
                counter = reader.ReadUShort();
                keystate = reader.ReadUInt();
                uk = reader.ReadChar();
                unk2 = reader.ReadInt();
                unk3 = reader.ReadInt();
                unk4 = reader.ReadInt();
                unk5 = reader.ReadInt();
            }
            else
            {
                var bytes = reader.ReadBytes((int)reader.BytesAvailable);
                Console.WriteLine($"BytesAvailable : {reader.BytesAvailable} bytes [{BitConverter.ToString(bytes)}] utf8 [{Encoding.UTF8.GetString(bytes)}]");
            }
        }

        public override void Serialize(IDataWriter writer)
        {
            throw new NotImplementedException();
        }
    }
}
