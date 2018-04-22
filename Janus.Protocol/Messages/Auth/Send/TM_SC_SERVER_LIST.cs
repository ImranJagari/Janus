using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Janus.Core.IO;
using Janus.Protocol.Enums;

namespace Janus.Protocol.Messages.Auth.Send
{
    public class TM_SC_SERVER_LIST : NetworkMessage
    {
        public const ushort Id = 1012;

        public short isValid;
        public string mmo_ip;
        public int mmo_port;
        public short mmo_id;
        public string msg_ip;
        public int msg_port;
        public short msg_id;
        public string lobby_ip;
        public int lobby_port;
        public short lobby_id;
        public short idx;
        public short usersCount;
        public short usersMax;

        public TM_SC_SERVER_LIST(short isValid, string mmo_ip, int mmo_port, short mmo_id, string msg_ip, int msg_port, short msg_id, string lobby_ip, int lobby_port, short lobby_id, short idx, short usersCount, short usersMax)
        {
            this.isValid = isValid;
            this.mmo_ip = mmo_ip;
            this.mmo_port = mmo_port;
            this.mmo_id = mmo_id;
            this.msg_ip = msg_ip;
            this.msg_port = msg_port;
            this.msg_id = msg_id;
            this.lobby_ip = lobby_ip;
            this.lobby_port = lobby_port;
            this.lobby_id = lobby_id;
            this.idx = idx;
            this.usersCount = usersCount;
            this.usersMax = usersMax;
        }

        public TM_SC_SERVER_LIST()
        {
        }

        public override ushort MessageId => Id;

        public override ServerEnum[] ServerEnum => new Enums.ServerEnum[] { Enums.ServerEnum.AuthServer };

        public override void Deserialize(IDataReader reader)
        {
            throw new NotImplementedException();
        }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort(isValid);
            writer.WriteShort(idx);
            writer.WriteShort(msg_id);
            writer.WriteShort(lobby_id);
            writer.WriteShort(mmo_id);
            writer.WriteUTFBytes(msg_ip.PadRight(16, '\0'));
            writer.WriteUTFBytes(lobby_ip.PadRight(16, '\0'));
            writer.WriteUTFBytes(mmo_ip.PadRight(16, '\0'));
            writer.WriteInt(msg_port);
            writer.WriteInt(lobby_port);
            writer.WriteInt(mmo_port);
            writer.WriteShort(usersCount);
            writer.WriteShort(usersMax);
        }
    }
}
