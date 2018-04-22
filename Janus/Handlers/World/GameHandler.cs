using Janus.Core.Extensions;
using Janus.Protocol.Enums;
using Janus.Protocol.Messages.World.Receive;
using Janus.Protocol.Messages.World.Send;
using Janus.Protocol.Types;
using Janus.Server.Handlers;
using Janus.Server.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Janus.Handlers.World
{
    public class GameHandler
    {

        [PacketHandler(BS_CS_SET_SESSION_MESSAGE.Id)]//Find the real utility, it sends select-slot=0 but why ?
        public static void HandleBS_CS_SET_SESSION_MESSAGE(SimpleClient client, BS_CS_SET_SESSION_MESSAGE message)
        {
        }
        [PacketHandler(BS_CS_CHANGE_SCENE.Id)]//Find the utility
        public static void HandleBS_CS_CHANGE_SCENE(SimpleClient client, BS_CS_CHANGE_SCENE message)
        {
        }

        [PacketHandler(BS_CS_MISSION_LIST.Id)] //Create the data for the missions, and send it
        public static void HandleBS_CS_MISSION_LIST(SimpleClient client, BS_CS_MISSION_LIST message)
        {
            client.Send(new BM_SC_MISSION_LIST(0, new short[0]));
        }

        [PacketHandler(BS_CS_POSITION_MESSAGE.Id)] //TODO : Finish this 
        public static void HandleBS_CS_STATUS_MESSAGE(SimpleClient client, BS_CS_POSITION_MESSAGE message)
        {
        }

        [PacketHandler(BS_CS_MMO_GAME_MESSAGE.Id)]//TODO : Find the utility of this
        public static void HandleBS_CS_MMO_GAME_MESSAGE(SimpleClient client, BS_CS_MMO_GAME_MESSAGE message)
        {
            client.Send(new BM_SC_MMO_GAME_MESSAGE(1));
        }
        [PacketHandler(BS_CS_QUEST_DAY_COIN.Id)]//TODO : Handle this correctly
        public static void HandleBS_CS_QUEST_DAY_COIN(SimpleClient client, BS_CS_QUEST_DAY_COIN message)
        {
            client.Send(new BM_SC_QUEST_COIN(DailyQuestCoinResultEnum.Success, 5));
            client.Send(new BM_SC_QUEST_COIN2(DailyQuestCoinResultEnum.Success));
        }
        [PacketHandler(BS_CS_MSG_UNKNOWN.Id)]
        public static void HandleBS_CS_MSG_UNKNOWN(SimpleClient client, BS_CS_MSG_UNKNOWN message)
        {
            client.Send(new BM_SC_MSG_UNKONWN());
            client.Send(new BM_SC_MSG_UNKONWN2());
        }
        [PacketHandler(BS_CS_AOI_UNKNOWN.Id)]
        public static void HandleBS_CS_AOI_UNKNOWN(SimpleClient client, BS_CS_AOI_UNKNOWN message)
        {
            client.Send(new BM_SC_AOI_UNKNOWN(1));
        }
        [PacketHandler(BS_CS_QUEST_UNKNOWN.Id)]
        public static void HandleBS_CS_QUEST_UNKNOWN(SimpleClient client, BS_CS_QUEST_UNKNOWN message)
        {
            client.Send(new BM_SC_QUEST_UNKNOWN(message.unk0));
        }
        [PacketHandler(BS_CS_SPEEDSTAR_RECORD.Id)]
        public static void HandleBM_SC_SPEEDSTAR_RECORD(SimpleClient client, BS_CS_SPEEDSTAR_RECORD message)
        {
            List<SpeedStarRecordType> recordTypes = new List<SpeedStarRecordType>();
            client.Send(new BM_SC_SPEEDSTAR_RECORD(recordTypes.Populate(new SpeedStarRecordType(), 12)));
            //client.Send(new BM_SC_ROOM_INFO(0, 0, new List<RoomListInfoType>() { new RoomListInfoType(0, 0, DateTime.Now.GetUnixTimeStampLong(), 0, 0) }));

        }
        [PacketHandler(BS_CS_QUEST_LIST.Id)]
        public static void HandleBS_CS_QUEST_LIST(SimpleClient client, BS_CS_QUEST_LIST message)
        {
            client.Send(new BM_SC_QUEST_LIST(new List<QuestInfoType>()));
        }
        [PacketHandler(BS_CS_PLAYER_DISGUISE.Id)]
        public static void HandleBS_CS_PLAYER_DISGUISE(SimpleClient client, BS_CS_PLAYER_DISGUISE message)
        {
            client.Send(new BM_SC_PLAYER_DISGUISE(1, 1, 10, 10));
        }
        [PacketHandler(BS_CS_MMO_TICK_MESSAGE.Id)]
        public static void HandleBS_CS_MMO_TICK_MESSAGE(SimpleClient client, BS_CS_MMO_TICK_MESSAGE message)
        {
            client.Send(new BM_SC_MMO_TICK_MESSAGE());
        }
    }
}
