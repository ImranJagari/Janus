using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Janus.Core.IO;
using Janus.Protocol.Enums;

namespace Janus.Protocol.Messages.World.Send
{
    public class BM_SC_PLAYER_DISGUISE : NetworkMessage
    {
        public const ushort Id = 2341;

        //SuccessString
        //17 bytes empty
        public float unk0; //try 1
        public float unk1;//try 1
        public int unk2; //more than 0 to active unk3
        public int unk3; //try 1

        public BM_SC_PLAYER_DISGUISE()
        {
        }

        public BM_SC_PLAYER_DISGUISE(float unk0, float unk1, int unk2, int unk3)
        {
            this.unk0 = unk0;
            this.unk1 = unk1;
            this.unk2 = unk2;
            this.unk3 = unk3;
        }

        public override ushort MessageId => Id;

        public override ServerEnum[] ServerEnum => new Enums.ServerEnum[] { Enums.ServerEnum.WorldServer };

        public override void Deserialize(IDataReader reader)
        {
            throw NetworkMessageException.DeserializeException;
        }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteUTFBytes(successString);
            writer.WriteBytes(new byte[17]);
            writer.WriteFloat(unk0);
            writer.WriteFloat(unk1);
            writer.WriteInt(unk2);
            writer.WriteInt(unk3);
        }
    }
}
