using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Janus.Core.IO;
using Janus.Protocol.Enums;

namespace Janus.Protocol.Messages.World.Send
{
    public class BM_SC_GET_CHANNEL_LIST : NetworkMessage
    {
        public const ushort Id = 2006;

        public static Int16 channel_count;
        public static Byte channel_id;
        public static String channel_name;
        public static Int32 unk4;
        public static Int32 unk5;
        public static Int32 unk6;
        public static Int32 channel_players;
        public static Int32 channel_maximumPlayers;

        public BM_SC_GET_CHANNEL_LIST(short _channel_count, byte _channel_id, string _channel_name, int _channel_players, int _channel_maximumPlayers)
        {
            channel_count = _channel_count;
            channel_id = _channel_id;
            channel_name = _channel_name;
            channel_players = _channel_players;
            channel_maximumPlayers = _channel_maximumPlayers;
        }

        public BM_SC_GET_CHANNEL_LIST()
        {
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
            writer.WriteBytes(new byte[16]);
            writer.WriteShort(channel_count);
            writer.WriteByte(channel_id);
            writer.WriteUTFBytes(channel_name.PadRight(12, '\0'));
            writer.WriteInt(unk4);
            writer.WriteInt(unk5);
            writer.WriteInt(unk6);
            writer.WriteInt(channel_players);
            writer.WriteInt(channel_maximumPlayers);
        }
    }
}
