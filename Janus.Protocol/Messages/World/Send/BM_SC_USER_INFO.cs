using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Janus.Core.IO;
using Janus.Protocol.Enums;

namespace Janus.Protocol.Messages.World.Send
{
    public class BM_SC_USER_INFO : NetworkMessage
    {
        public const ushort Id = 2165;

        public string playerIP;//33 bytes
        public string playerName;//40 bytes
        public byte lobbyPosition;
        public byte charType;
        public byte entryInfo;
        public byte isAdmin;
        public byte slotDisplay;
        public byte isReady;
        public byte status;
        //3 bytes empty
        public byte unk0;//Must be set to 1, IDK why ?

        public BM_SC_USER_INFO()
        {
        }

        public BM_SC_USER_INFO(string playerIP, string playerName, byte lobbyPosition, byte charType, byte entryInfo, byte isAdmin, byte slotDisplay, byte isReady, byte status, byte unk0)
        {
            this.playerIP = playerIP;
            this.playerName = playerName;
            this.lobbyPosition = lobbyPosition;
            this.charType = charType;
            this.entryInfo = entryInfo;
            this.isAdmin = isAdmin;
            this.slotDisplay = slotDisplay;
            this.isReady = isReady;
            this.status = status;
            this.unk0 = unk0;
        }

        public override ushort MessageId => Id;

        public override ServerEnum[] ServerEnum => new Enums.ServerEnum[] { Enums.ServerEnum.WorldServer };

        public override void Deserialize(IDataReader reader)
        {
            throw NetworkMessageException.DeserializeException;
        }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteUTFBytes(playerIP, 33);
            writer.WriteUTFBytes(playerName, 40);
            writer.WriteByte(lobbyPosition);
            writer.WriteByte(charType);
            writer.WriteByte(entryInfo);
            writer.WriteByte(isAdmin);
            writer.WriteByte(slotDisplay);
            writer.WriteByte(isReady);
            writer.WriteByte(status);
            writer.WriteBytes(new byte[3]);
            writer.WriteByte(unk0);
        }
    }
}
