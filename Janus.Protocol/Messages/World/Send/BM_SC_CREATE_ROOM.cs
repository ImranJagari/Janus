using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Janus.Core.IO;
using Janus.Protocol.Enums;

namespace Janus.Protocol.Messages.World.Send
{
    public class BM_SC_CREATE_ROOM : NetworkMessage
    {
        public const ushort Id = 2174;

        public string result;
        //16 bytes empty
        public int unk0;
        public string relayIP;//20 bytes
        public short relayPort;
        public byte team;//0 = blue or 1 = red
        public byte unk1;
        //1 byte empty
        public int udpPort;//not sure ???

        public BM_SC_CREATE_ROOM()
        {
        }

        public BM_SC_CREATE_ROOM(string result, int unk0, string relayIP, short relayPort, byte team, byte unk1, int udpPort)
        {
            this.result = result;
            this.unk0 = unk0;
            this.relayIP = relayIP;
            this.relayPort = relayPort;
            this.team = team;
            this.unk1 = unk1;
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
            writer.WriteInt(unk0);
            writer.WriteUTFBytes(relayIP, 20);
            writer.WriteShort(relayPort);
            writer.WriteByte(team);
            writer.WriteByte(unk1);
            writer.WriteByte(unk1);
            writer.WriteByte(0);
            writer.WriteInt(udpPort);
        }
    }
}
