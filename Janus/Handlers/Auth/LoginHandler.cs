using Janus.Core.Extensions;
using Janus.Core.Utils;
using Janus.Databases.Accounts;
using Janus.Game.Accounts;
using Janus.Manager.Accounts;
using Janus.Protocol.Enums;
using Janus.Protocol.Messages.Auth.Receive;
using Janus.Protocol.Messages.Auth.Send;
using Janus.Server.Handlers;
using Janus.Server.Network;
using System;
using System.Linq;

namespace Janus.Handlers.Auth
{
    public class LoginHandler
    {
        [PacketHandler(TS_CS_VERSION.Id)]
        public static void HandleTS_CS_VERSION(SimpleClient client, TS_CS_VERSION message) { }

        [PacketHandler(TS_CS_ACCOUNT.Id)]
        public static void HandleTS_CS_ACCOUNT(SimpleClient client, TS_CS_ACCOUNT message)
        {
            string login = message.login.Replace("\0", "");
            string password = message.password.Replace("\0", "");

            AccountRecord record = AccountManager.Instance.GetAccountRecordByLogin(login);

            if(record == null)
            {
                client.Send(new TM_SC_RESULT(0, (ushort)ErrorLoginEnum.MSG_SERVER_NOT_EXIST));
                return;
            }

            if(record.BanTime.HasValue && record.BanTime.Value > DateTime.Now)
            {
                client.Send(new TM_SC_RESULT(0, (ushort)ErrorLoginEnum.MSG_SERVER_DENIED));
                return;
            }

            if(record.PasswordHash != password)
            {
                client.Send(new TM_SC_RESULT(0, (ushort)ErrorLoginEnum.MSG_SERVER_DENIED));
                return;
            }

            SimpleClient clientAlreadyConnected = SimpleServer.ConnectedClients.FirstOrDefault(x => x.Account != null && x.Account.Username == login);
            if (clientAlreadyConnected != null)
            {
                client.Send(new TM_SC_RESULT(0, (ushort)ErrorLoginEnum.MSG_SERVER_ALREADY_EXIST));
                clientAlreadyConnected.Disconnect();
                return;
            }

            string sessionKey = new AsyncRandom().RandomString(32) + '\0';
            client.Account = new Account(record, sessionKey);

            client.Send(new TM_SC_WE_LOGIN((int)ValidKeyEnum.IS_VALID, sessionKey, (int)client.Account.Role, 1));

            AccountManager.Instance.AddTicketAccount(sessionKey, client.Account);
        }

        [PacketHandler(TS_CS_SERVER_LIST.Id)]
        public static void HandleTS_CS_SERVER_LIST(SimpleClient client, TS_CS_SERVER_LIST message)
        {
            client.Send(new TM_SC_SERVER_LIST((short)ValidKeyEnum.IS_VALID, SimpleServer.WorldHost, SimpleServer.WorldPort, 1, SimpleServer.MsgHost, SimpleServer.MsgPort, 2, SimpleServer.LobbyHost, SimpleServer.LobbyPort, 3, (short)SimpleServer.ServerId, (short)SimpleServer.ConnectedClients.Count, short.MaxValue));
        }

        [PacketHandler(TS_CS_SERVER_SELECT.Id)]
        public static void HandleTS_CS_SERVER_SELECT(SimpleClient client, TS_CS_SERVER_SELECT message)
        {
            client.Send(new TM_SC_SERVER_SELECT((short)ServerStatusEnum.CONNECTION_SUCCESS, 1, 1, SimpleServer.ServerId));
        }
    }
}
