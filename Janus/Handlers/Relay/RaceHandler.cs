using Janus.Core.Extensions;
using Janus.Game.Accounts;
using Janus.Game.Characters;
using Janus.Manager.Accounts;
using Janus.Protocol.Messages.Relay.Receive;
using Janus.Protocol.Messages.Relay.Send;
using Janus.Server.Handlers;
using Janus.Server.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Janus.Handlers.Relay
{
    public class RaceHandler
    {
        [PacketHandler(NS_CS_LOGIN.Id)]
        public static void HandleNS_CS_LOGIN(SimpleClient client, NS_CS_LOGIN message)
        {
            Account account = AccountManager.Instance.GetTicketAccount(message.sessionKey);

            if (account == null)
            {
                client.Disconnect();
                return;
            }

            client.Account = account;
            Character leader = client.Character.RoomConnected.PlayersConnected.FirstOrDefault(x => x.IsLeader);

            
            //client.Send(new NM_SC_LOGIN2());

            if (leader != null)
            {
                leader.RelayClient = client;
                client.Send(new NM_SC_LOGIN(leader.Client.IP, 5000));
            }
            else
            {
                client.Send(new NM_SC_LOGIN(SimpleServer.AuthHost, 5000));
            }
        }

        [PacketHandler(NS_CS_SYNCHRO.Id)]
        public static void HandleNS_CS_SYNCHRO(SimpleClient client, NS_CS_SYNCHRO message)
        {
            if (client.Account.SessionKey == message.sessionKey)
                client.Send(new NM_SC_SYNCHRO(11));
        }
        [PacketHandler(NS_CS_KEEP_ALIVE.Id)]
        public static void HandleNS_CS_KEEP_ALIVE(SimpleClient client, NS_CS_KEEP_ALIVE message)
        {
            client.Send(new NM_SC_KEEP_ALIVE());
            //client.Send(new NM_SC_GET_SERVER_TICK());
            //client.Send(new NM_SC_GAME_READY(client.Account.Id));
            //Thread.Sleep(5000);
            //client.Send(new NM_SC_START_GAME(DateTime.Now.GetUnixTimeStamp(), DateTime.Now.AddSeconds(4).GetUnixTimeStamp()));
        }
        [PacketHandler(NS_CS_EXPIRE.Id)]
        public static void HandleNS_CS_EXPIRE(SimpleClient client, NS_CS_EXPIRE message)
        {
            client.Send(new NM_SC_EXPIRE());
            client.Send(new NM_SC_GET_SERVER_TICK());

            Thread.Sleep(2000);

            client.Send(new NM_SC_GAME_READY(client.Account.Id));
            Thread.Sleep(5000);
            client.Send(new NM_SC_START_GAME(DateTime.Now.GetUnixTimeStamp(), DateTime.Now.AddSeconds(4).GetUnixTimeStamp()));
        }
    }
}
