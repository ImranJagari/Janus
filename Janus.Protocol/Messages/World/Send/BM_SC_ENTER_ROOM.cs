using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Janus.Core.IO;
using Janus.Protocol.Enums;

namespace Janus.Protocol.Messages.World.Send
{
    public class BM_SC_ENTER_ROOM : NetworkMessage
    {
        public const ushort Id = 2176;

        public string result;
        //16 bytes empty
        public string relayIP;//20 bytes
        public short relayPort;
        public byte team;
        public byte unk0;
        public byte unk1;
        public byte unk2;
        //1 byte empty
        public int udpPort;

        public BM_SC_ENTER_ROOM()
        {
        }

        public BM_SC_ENTER_ROOM(string result, string relayIP, short relayPort, byte team, byte unk0, byte unk1, byte unk2, int udpPort)
        {
            this.result = result;
            this.relayIP = relayIP;
            this.relayPort = relayPort;
            this.team = team;
            this.unk0 = unk0;
            this.unk1 = unk1;
            this.unk2 = unk2;
            this.udpPort = udpPort;
        }

        public override ushort MessageId => Id;

        public override ServerEnum[] ServerEnum => new Enums.ServerEnum[] { Enums.ServerEnum.WorldServer };

        public override void Deserialize(IDataReader reader)
        {
            throw NetworkMessageException.DeserializeException;
        }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteUTFBytes(result);
            writer.WriteBytes(new byte[16]);
            writer.WriteUTFBytes(relayIP, 20);
            writer.WriteShort(relayPort);
            writer.WriteByte(team);
            writer.WriteByte(unk0);
            writer.WriteByte(unk1);
            writer.WriteByte(unk2);
            writer.WriteByte(0);
            writer.WriteInt(udpPort);
        }
    }
}
