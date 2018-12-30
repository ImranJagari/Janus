using Janus.Databases.Characters;
using Janus.Game.Characters;
using Janus.Manager.Accounts;
using Janus.Manager.Characters;
using Janus.Protocol.Enums;
using Janus.Protocol.Messages;
using Janus.Protocol.Messages.World.Receive;
using Janus.Protocol.Messages.World.Send;
using Janus.Protocol.Types;
using Janus.Server.Handlers;
using Janus.Server.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Janus.Handlers.World
{
    public class TransitionHandler
    {
        [PacketHandler(BS_CS_LOGIN.Id)]
        public static void HandleBS_CS_LOGIN(SimpleClient client, BS_CS_LOGIN message)
        {
            if ((client.Account = AccountManager.Instance.GetTicketAccount(message.sessionKey)) != null)
            {
                //Thread.Sleep(7000);
                client.Send(new BM_SC_LOGIN((int)0, 999));
                //client.Send(new BM_SC_CHARACTER_TYPE_LIST(5, new List<CharacterTypeInfoType>() { new CharacterTypeInfoType(0, 1), new CharacterTypeInfoType(1, 2), new CharacterTypeInfoType(2, 2), new CharacterTypeInfoType(3, 1), new CharacterTypeInfoType(4, 2) }));

                //if (client.Account.SessionKey != message.sessionKey)
                //{
                //    client.Disconnect();
                //    return;
                //}

                //CharacterRecord character = CharacterManager.Instance.GetCharacterByAccountId(client.Account.Id);

                //if (character == null)
                //    client.Send(new BM_SC_CHARACTER_LIST("", 1, 0, new List<ListCharacterInfoType>()));
                //else
                //{
                //    client.Account.Character = new Character(character);
                //    client.Send(new BM_SC_CHARACTER_LIST(client.Character.Name, client.Character.Level, 1, new List<ListCharacterInfoType>() { client.Character.GetListCharacterInfoType() }));
                //    Thread.Sleep(4000);
                //    client.Send(new BM_SC_CHARACTER_LIST2(client.Character.Name, 1, 1, new List<ListCharacterInfoType2>() { client.Character.GetListCharacterInfoType2() }));
                //    //client.Send(new BM_SC_SELECT_CHARACTER());
                //}
                //SimpleServer.ConnectedClients.FirstOrDefault(x => x.Account.SessionKey == client.Account.SessionKey).Disconnect(true);
            }
            else
                client.Disconnect();
        }
        [PacketHandler(BS_CS_CHARACTER_TYPE_LIST.Id)]
        public static void HandleBM_CS_CHARACTER_TYPE_LIST(SimpleClient client, BS_CS_CHARACTER_TYPE_LIST message)
        {
            client.Send(new BM_SC_CHARACTER_TYPE_LIST(6, new List<CharacterTypeInfoType>() { new CharacterTypeInfoType(0, 1), new CharacterTypeInfoType(1, 2), new CharacterTypeInfoType(2, 2), new CharacterTypeInfoType(3, 1), new CharacterTypeInfoType(4, 2), new CharacterTypeInfoType(5, 2) }));
        }
        [PacketHandler(BS_CS_CHARACTER_LIST.Id)]
        public static void HandleBS_CS_CHARACTER_LIST(SimpleClient client, BS_CS_CHARACTER_LIST message)
        {
            if (client.Account.SessionKey != message.sessionKey)
            {
                client.Disconnect();
                return;
            }

            CharacterRecord character = CharacterManager.Instance.GetCharacterByAccountId(client.Account.Id);

            if (character == null)
                client.Send(new BM_SC_CHARACTER_LIST("", 1, 0, new List<ListCharacterInfoType>()));
            else
            {
                client.Account.Character = new Character(character, client);
                client.Send(new BM_SC_CHARACTER_LIST(client.Character.Name, client.Character.Level, 6, new List<ListCharacterInfoType>() { new ListCharacterInfoType(1, 0, 1), new ListCharacterInfoType(1, 1, 1), new ListCharacterInfoType(1, 2, 1), new ListCharacterInfoType(1, 3, 1), new ListCharacterInfoType(1, 4, 1), new ListCharacterInfoType(1, 5, 1) }));
                Thread.Sleep(4000);
                client.Send(new BM_SC_CHARACTER_LIST2(client.Character.Name, 1, 6, new List<ListCharacterInfoType2>() { new ListCharacterInfoType2(0, 0, 0), new ListCharacterInfoType2(1, 1, 1), new ListCharacterInfoType2(2, 2, 2), new ListCharacterInfoType2(3, 3, 3), new ListCharacterInfoType2(4, 4, 4), new ListCharacterInfoType2(5, 5, 5) }));
                //client.Send(new BM_SC_SELECT_CHARACTER());
            }
        }

        [PacketHandler(BS_CS_SELECT_CHARACTER.Id)]
        public static void HandleBS_CS_SELECT_CHARACTER(SimpleClient client, BS_CS_SELECT_CHARACTER message)
        {
            client.Send(new BM_SC_SELECT_CHARACTER());
        }
        [PacketHandler(BS_CS_PLAYER_INFO.Id)]
        public static void HandleBS_CS_PLAYER_INFO(SimpleClient client, BS_CS_PLAYER_INFO message)
        {
            client.Send(new BM_SC_PLAYER_INFO(client.Character.Level));
        }

        [PacketHandler(BS_CS_TRICK_LIST.Id)]
        public static void HandleBS_CS_TRICK_LIST(SimpleClient client, BS_CS_TRICK_LIST message)
        {
            client.Send(new BM_SC_TRICK_LIST(client.Character.GetTricks()));
        }
        [PacketHandler(BS_CS_BALANCE_INFO.Id)]
        public static void HandleBS_CS_BALANCE_INFO(SimpleClient client, BS_CS_BALANCE_INFO message)
        {
            client.Send(new BM_SC_BALANCE_INFO(client.Character.Gamecash, client.Character.Coin, client.Character.Cash, client.Character.Questpoint));
            client.Send(new BM_SC_LEVEL_INFO((uint)client.Character.Exp, (uint)client.Character.Level, (uint)client.Character.Licence));
        }

        [PacketHandler(BS_CS_CASH_BALANCE_INFO.Id)]
        public static void HandleBS_CS_CASH_BALANCE_INFO(SimpleClient client, BS_CS_CASH_BALANCE_INFO message)
        {
            client.Send(new BM_SC_CASH_BALANCE_INFO(client.Character.Coin));
        }
        [PacketHandler(BS_CS_GET_CHANNEL_LIST.Id)]
        public static void HandleBS_CS_GET_CHANNEL_LIST(SimpleClient client, BS_CS_GET_CHANNEL_LIST message)
        {
            client.Send(new BM_SC_GET_CHANNEL_LIST(1, 1, "CHANNEL 1", SimpleServer.ConnectedClients.Count, ushort.MaxValue));
            //client.Send(new BM_SC_ENTER_CHANNEL(1));
        }
        [PacketHandler(BS_CS_ENTER_CHANNEL.Id)]
        public static void HandleBS_CS_ENTER_CHANNEL(SimpleClient client, BS_CS_ENTER_CHANNEL message)
        {
            client.Send(new BM_SC_ENTER_CHANNEL(1));
        }
        [PacketHandler(BS_CS_CHANNEL_LEAVE.Id)]
        public static void HandleBS_CS_CHANNEL_LEAVE(SimpleClient client, BS_CS_CHANNEL_LEAVE message)
        {
            client.Send(new BM_SC_CHANNEL_LEAVE(1));
            client.Send(new ID_BZ_SC_ENTER_LOBBY(0));
            
        }
    }
}
