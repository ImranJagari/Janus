using Janus.ORM;
using Janus.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Janus.Core.Utils;
using Janus.Server.Initialization;
using Janus.Core.Reflection;
using Janus.Server.Manager;
using Janus.Protocol;
using Janus.Core.Attributes;
using System.IO;
using Janus.Core.Xml.Config;
using Janus.Server.Handlers;
using Janus.Protocol.Enums;
using NLog;
using Janus.Game.Rooms;

namespace Janus.Server.Network
{
    public class SimpleServer : Singleton<SimpleServer>
    {

        #region Variables
        private ServerEnum serverType;

        private Socket socketListener;
        private bool runing = false;
        private const string configFilePath = ".//config.xml";

        public static readonly Logger logger = LogManager.GetCurrentClassLogger();



        [Variable]
        public static int ServerId = 27;

        [Variable]
        public static string AuthHost = "127.0.0.1";

        [Variable]
        public static short AuthPort = 82;

        [Variable]
        public static string WorldHost = "127.0.0.1";

        [Variable]
        public static short WorldPort = 83;

        [Variable]
        public static string MsgHost = "127.0.0.1";

        [Variable]
        public static short MsgPort = 84;

        [Variable]
        public static string LobbyHost = "127.0.0.1";

        [Variable]
        public static short LobbyPort = 85;

        [Variable]
        public static string RelayHost = "127.0.0.1";

        [Variable]
        public static short RelayPort = 86;

        [Variable]
        public static string P2PHost = "127.0.0.1";

        [Variable]
        public static short P2PPort = 5000;

        [Variable]
        public static DatabaseConfiguration DatabaseConfiguration = new DatabaseConfiguration
        {
            Host = "localhost",
            DbName = "janus",
            User = "root",
            Password = "",
            ProviderName = "MySql.Data.MySqlClient",
        };


        [Variable]
        public static int[] PacketsSkipToShow = new int[] { 2017, 2019, 2015 };
        public static DatabaseAccessor DBAccessor
        {
            get;
            protected set;
        }
        public static InitializationManager InitManager
        {
            get;
            set;
        }
        public static XmlConfig Config
        {
            get;
            protected set;
        }

        public static List<SimpleClient> ConnectedClients;

        public static Dictionary<int, Room> Rooms = new Dictionary<int, Room>();


        public UdpClient P2PServer = new UdpClient(P2PHost, P2PPort);

        public SimpleServer(ServerEnum serverType)
        {
            socketListener = new Socket(AddressFamily.InterNetwork, serverType == ServerEnum.P2PServer ? SocketType.Dgram : SocketType.Stream, serverType == ServerEnum.P2PServer ? ProtocolType.Udp : ProtocolType.Tcp);
            this.serverType = serverType;
        }

        #endregion

        #region Methods
        public static void DrawAscii()
        {
            string[] Logo = {
            "      _                          ",
            "     | | __ _ _ __  _   _ ___    ",
            "  _  | |/ _` | '_ \\| | | / __|  ",
            " | |_| | (_| | | | | |_| \\__ \\ ",
            "  \\___/ \\__,_|_| |_|\\__,_|___/",
            "                                 "
            };
            foreach (var lines in Logo)
            {
                Console.WriteLine(lines);
            }
        }
        public void Start(string ip, short listenPort)
        {

            runing = true;
            if (serverType != ServerEnum.P2PServer)
            {
                socketListener.Bind(new IPEndPoint(IPAddress.Parse(ip), listenPort));
                socketListener.Listen(5);
                socketListener.BeginAccept(BeginAcceptCallBack, socketListener);
            }
            else
            {
                P2PServer.BeginReceive(BeginAcceptCallBack, socketListener);
            }
            ConsoleUtils.WriteMessageInfo($"Server {serverType} listening on ip {ip} and on port {listenPort} !");

        }

        public void Stop()
        {
            runing = false;
            socketListener.Shutdown(SocketShutdown.Both);
        }
        public static void Initialize()
        {
            NLogHelper.DefineLogProfile(true, true);
            NLogHelper.EnableLogging();

            ConnectedClients = new List<SimpleClient>();

            logger.Info("Initializing Configuration !");

            Config = new XmlConfig(configFilePath);
            Config.AddAssemblies(AppDomain.CurrentDomain.GetAssemblies().ToDictionary(entry => entry.GetName().Name).Values.ToArray());

            if (!File.Exists(configFilePath))
            {
                Config.Create();
                logger.Info("Config file created !");
            }
            else
            {
                Config.Load();
                //Config.Save();
            }
            logger.Info("Loading protocol messages !");
            MessageReceiver.Initialize();

            logger.Info("Loading handlers !");
            PacketManager.Initialize(Assembly.GetExecutingAssembly());

            logger.Info("Loading Database !");
            DBAccessor = new DatabaseAccessor(DatabaseConfiguration);
            DBAccessor.RegisterMappingAssembly(Assembly.GetExecutingAssembly());
            DBAccessor.Initialize();
            DBAccessor.OpenConnection();
            DataManagerAllocator.Assembly = System.Reflection.Assembly.GetExecutingAssembly();
            DatabaseManager.DefaultDatabase = DBAccessor.Database;

            logger.Info("Loading database features !");
            InitManager = Singleton<InitializationManager>.Instance;
            InitManager.AddAssemblies(AppDomain.CurrentDomain.GetAssemblies());
            InitManager.Initialize(InitializationPass.Database);
            InitManager.InitializeAll();
        }
        private void BeginAcceptCallBack(IAsyncResult result)
        {
            if (runing)
            {
                Socket listener = (Socket)result.AsyncState;
                Socket acceptedSocket = listener.EndAccept(result);

                SimpleClient client = new SimpleClient(acceptedSocket, serverType);
                AddClient(client);

                ConsoleUtils.WriteSuccess($"Client <{client.IP}> is connected to {serverType}!");
                OnConnectionAccepted(acceptedSocket);
                socketListener.BeginAccept(BeginAcceptCallBack, socketListener);
            }
        }

        public static void AddClient(SimpleClient client)
        {
            ConnectedClients.Add(client);
        }
        public static void RemoveClient(SimpleClient client)
        {
            ConnectedClients.Remove(client);
        }
        #endregion

        #region Events

        public delegate Socket ConnectionAcceptedDelegate(Socket acceptedSocket);
        public event ConnectionAcceptedDelegate ConnectionAccepted;
        private void OnConnectionAccepted(Socket client)
        {
            if (ConnectionAccepted != null)
                ConnectionAccepted(client);
        }

        #endregion

    }
}
