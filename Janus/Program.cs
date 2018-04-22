using Janus.Core.Utils;
using Janus.Protocol.Enums;
using Janus.Server.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Janus
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleUtils.ChangeTitle("Chronos 1.1.0");

            SimpleServer.DrawAscii();

            Console.WriteLine(" ");
            ConsoleUtils.WriteMessageInfo("Initializing all stuffs !");

            SimpleServer.Initialize();

            Console.WriteLine(" ");

            SimpleServer authServer = new SimpleServer(ServerEnum.AuthServer);
            authServer.Start(SimpleServer.AuthHost, SimpleServer.AuthPort);

            SimpleServer worldServer = new SimpleServer(ServerEnum.WorldServer);
            worldServer.Start(SimpleServer.WorldHost, SimpleServer.WorldPort);

            SimpleServer msgServer = new SimpleServer(ServerEnum.MsgServer);
            msgServer.Start(SimpleServer.MsgHost, SimpleServer.MsgPort);

            SimpleServer lobbyServer = new SimpleServer(ServerEnum.LobbyServer);
            lobbyServer.Start(SimpleServer.LobbyHost, SimpleServer.LobbyPort);

            SimpleServer relayServer = new SimpleServer(ServerEnum.RelayServer);
            relayServer.Start(SimpleServer.RelayHost, SimpleServer.RelayPort);

            SimpleServer p2pServer = new SimpleServer(ServerEnum.P2PServer);
            p2pServer.Start(SimpleServer.P2PHost, SimpleServer.P2PPort);

            Console.ReadLine();
        }
    }
}
