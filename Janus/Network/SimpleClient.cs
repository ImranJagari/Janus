using Janus.Core.IO;
using Janus.Core.Utils;
using Janus.Game.Accounts;
using Janus.Game.Characters;
using Janus.Manager.Accounts;
using Janus.Protocol;
using Janus.Protocol.Enums;
using Janus.Protocol.Messages;
using Janus.Protocol.Messages.World.Send;
using Janus.Server.Handlers;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Janus.Server.Manager;

namespace Janus.Server.Network
{
    public class SimpleClient : IPacketInterceptor
    {

        #region Variables

        public Logger logger = LogManager.GetCurrentClassLogger();

        private byte[] sendBuffer, receiveBuffer;
        const int bufferLength = 8192;

        public ServerEnum serverConnected;

        public Account Account { get; set; }
        public Character Character => Account.Character;
        public MessagePart currentMessage;
        public BigEndianReader buffer;
        public Socket Socket;
        public bool Runing { get; private set; }
        public static int BufferSize
        {
            get
            {
                return bufferLength;
            }
        }
        public string IP
        {
            get
            {
                return ((IPEndPoint)this.Socket.RemoteEndPoint)?.Address?.ToString();
            }
        }

        #endregion

        #region Builder

        public SimpleClient(Socket socket, ServerEnum serverType)
        {
            Init();
            serverConnected = serverType;
            Start(socket);

        }

        #endregion

        #region Methods

        public void Start(Socket socket)
        {
            try
            {
                Runing = true;
                Socket = socket;
                Socket.BeginReceive(receiveBuffer, 0, bufferLength, SocketFlags.None, new AsyncCallback(ReceiveCallBack), Socket);
            }
            catch (System.Exception ex)
            {
                OnError(new ErrorEventArgs(ex));
            }
        }
        /// <summary>
        /// Disconnect the player
        /// </summary>
        public void Disconnect(bool transitionServer = false)
        {
            try
            {
                //if (!transitionServer && Account != null && !string.IsNullOrEmpty(Account.SessionKey))
                //{
                //    AccountManager.Instance.RemoveTicketAccount(Account.SessionKey);
                //}
                    Socket.BeginDisconnect(false, DisconectedCallBack, Socket);
            }
            catch (System.Exception ex)
            {
                OnError(new ErrorEventArgs(ex));
            }
        }
        /// <summary>
        /// Send the packet to the client
        /// </summary>
        /// <param name="message">packet structure</param>
        /// <param name="encrypt">(Not used atm) to force or not the encryption</param>
        public void Send(NetworkMessage message, bool encrypt = true)
        {
            BigEndianWriter writer = new BigEndianWriter();

            message.Pack(writer);
            byte[] data = writer.Data;
           
            Send(writer.Data);
             Console.WriteLine(string.Format("[SND] {0} -> {1}", IP, message));
        }
        public void Send(byte[] data)
        {
            try
            {
                if (Socket.Connected == false)
                    Runing = false;
                if (Runing)
                {
                    if (data.Length == 0)
                        return;
                    sendBuffer = data;
                    Socket.BeginSend(sendBuffer, 0, sendBuffer.Length, SocketFlags.None, new AsyncCallback(SendCallBack), Socket);
                }
                else
                    Console.WriteLine("Send " + data.Length + " bytes but not runing");
            }
            catch (System.Exception ex)
            {
                OnError(new ErrorEventArgs(ex));
            }
        }
        public void Dispose()
        {
            // Dispose

            if(Socket != null)
                Socket.Dispose();

            //if (buffer != null)
            //    buffer.Dispose();

            // Clean

            Socket = null;
            sendBuffer = null;
            receiveBuffer = null;

        }

        #endregion

        #region Private Methods

        private void Init()
        {
            try
            {
                buffer = new BigEndianReader();
                receiveBuffer = new byte[bufferLength];
                Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            }
            catch (System.Exception ex)
            {
                OnError(new ErrorEventArgs(ex));
            }
        }

        public void ThreatBuffer()
        {
            if (this.currentMessage == null)
                this.currentMessage = new MessagePart();
            if (!this.currentMessage.Build(ref this.buffer))
                return;
            this.OnDataReceived(new DataReceivedEventArgs(this.currentMessage));
        }

        #endregion

        #region CallBack

        private void ConnectionCallBack(IAsyncResult asyncResult)
        {
            try
            {
                Runing = true;
                Socket client = (Socket)asyncResult.AsyncState;
                client.EndConnect(asyncResult);
                client.BeginReceive(receiveBuffer, 0, bufferLength, SocketFlags.None, new AsyncCallback(ReceiveCallBack), client);

                OnConnected(new ConnectedEventArgs());
            }
            catch (System.Exception ex)
            {
                OnError(new ErrorEventArgs(ex));
            }
        }

        private void DisconectedCallBack(IAsyncResult asyncResult)
        {
            try
            {
                ConsoleUtils.WriteMessageInfo($"{this} is disconnected from {serverConnected} !");
               
                Runing = false;
                Socket client = (Socket)asyncResult.AsyncState;

                this.Character?.LogOut(this);
                DatabaseManager.DefaultDatabase.Update(this.Character?.Record);

                client.EndDisconnect(asyncResult);
                OnDisconnected(new DisconnectedEventArgs(Socket));

                SimpleServer.RemoveClient(this);

                Dispose();
            }
            catch (System.Exception ex)
            {
                OnError(new ErrorEventArgs(ex));
            }
        }

        private void ReceiveCallBack(IAsyncResult asyncResult)
        {
            Socket client = (Socket)asyncResult.AsyncState;
            if (!client.Connected)
            {
                Runing = false;
                return;
            }
            if (Runing)
            {
                int bytesRead = 0;
                try
                {
                    bytesRead = client.EndReceive(asyncResult);


                    if (bytesRead == 0)
                    {
                        Runing = false;
                        this.Disconnect();
                        return;
                    }
                    List<Tuple<MessagePart, NetworkMessage>> messages = new List<Tuple<MessagePart, NetworkMessage>>();

                    byte[] data = new byte[bytesRead];
                    Array.Copy(receiveBuffer, data, bytesRead);
                    buffer = new BigEndianReader(data);

                REDO_PACKET:

                    ThreatBuffer();
                    var messagePart = new MessagePart(DataReceivedEventArgs.Data);
                    NetworkMessage message = MessageReceiver.BuildMessage(messagePart.MessageId.Value, new BigEndianReader(messagePart.Data));
                    messages.Add(new Tuple<MessagePart, NetworkMessage>(messagePart, message));

                    if (buffer.BytesAvailable > 4)
                        goto REDO_PACKET;

                    foreach (var packet in messages)
                    {
                        if (packet.Item1.MessageId == 2000)
                        {
                            //Keep alive the connection if the player
                            continue;
                        }

                        if (packet.Item2 == null)
                        {
                            if (!SimpleServer.PacketsSkipToShow.Contains(packet.Item1.MessageId.Value) && packet.Item1.Length.Value < 1000)
                            {
                                logger.Warn(string.Format($"[{serverConnected}] Received Unknown PacketId : {IP} -> {packet.Item1.MessageId}"));
                                Console.WriteLine($" Body [len:{packet.Item1.Length - 5}] : [{BitConverter.ToString(packet.Item1.Data)}] {Environment.NewLine} [{Encoding.UTF8.GetString(packet.Item1.Data)}]");
                            }
                        }
                        else
                        {
                            if (!packet.Item2.ServerEnum.Contains(serverConnected))
                                throw new Exception($"Sended Packet (Id:{packet.Item1.MessageId}) for server {packet.Item2.ServerEnum} but {serverConnected} received it !");
                            if (!SimpleServer.PacketsSkipToShow.Contains(packet.Item1.MessageId.Value))
                            {
                                ConsoleUtils.WriteMessageInfo(string.Format($"[RCV : {serverConnected}] {IP} -> {packet.Item2}"));
                                Console.WriteLine($" Body [len:{packet.Item1.Length - 5}] : [{BitConverter.ToString(packet.Item1.Data)}] {Environment.NewLine} [{Encoding.UTF8.GetString(packet.Item1.Data)}]");
                            }
                            PacketManager.ParseHandler(this, packet.Item2);
                        }
                    }
                    if (!client.Connected)
                    {
                        Runing = false;
                        return;
                    }
                    client.BeginReceive(receiveBuffer, 0, bufferLength, SocketFlags.None, new AsyncCallback(ReceiveCallBack), client);
                }
                catch (System.Exception ex)
                {
                    logger.Error($"Message : {ex.Message} {Environment.NewLine} StackTrace : {ex.StackTrace}");
                    this.Disconnect();
                }
            }
            else
                logger.Warn("Receive data but not running");
        }

        private void SendCallBack(IAsyncResult asyncResult)
        {
            try
            {
                if (Runing == true)
                {
                    Socket client = (Socket)asyncResult.AsyncState;
                    client.EndSend(asyncResult);
                    OnDataSended(new DataSendedEventArgs());
                }
                else
                    Console.WriteLine("Send data but not runing !");
            }
            catch (System.Exception ex)
            {
                OnError(new ErrorEventArgs(ex));
            }
        }

        #endregion

        #region Events

        public event EventHandler<ConnectedEventArgs> Connected;
        public event EventHandler<DisconnectedEventArgs> Disconnected;
        public event EventHandler<DataReceivedEventArgs> DataReceived;
        public event EventHandler<DataSendedEventArgs> DataSended;
        public event EventHandler<ErrorEventArgs> Error;

        private void OnConnected(ConnectedEventArgs e)
        {
            ConsoleUtils.WriteSuccess(this.ToString() + " is connected !");
            if (Connected != null)
                Connected(this, e);
        }

        private void OnDisconnected(DisconnectedEventArgs e)
        {
            if (Disconnected != null)
                Disconnected(this, e);
        }

        private void OnDataReceived(DataReceivedEventArgs e)
        {
            if (DataReceived != null)
                DataReceived(this, e);
        }

        private void OnDataSended(DataSendedEventArgs e)
        {
            if (DataSended != null)
                DataSended(this, e);
        }

        private void OnError(ErrorEventArgs e)
        {
            if (Error != null)
                Error(this, e);
        }

        #endregion

        #region EventArgs

        public class ConnectedEventArgs : EventArgs
        {
        }

        public class DisconnectedEventArgs : EventArgs
        {
            public Socket Socket { get; private set; }

            public DisconnectedEventArgs(Socket socket)
            {
                
                Socket = socket;
            }
        }

        public class DataSendedEventArgs : EventArgs
        {
        }

        public class DataReceivedEventArgs : EventArgs
        {
            public static MessagePart Data { get; private set; }

            public DataReceivedEventArgs(MessagePart data)
            {
                Data = data;
            }
        }

        public class ErrorEventArgs : EventArgs
        {
            public System.Exception Ex { get; private set; }

            public ErrorEventArgs(System.Exception ex)
            {
                Ex = ex;
            }
        }
        #endregion
        public override string ToString()
        {
            return $"client <{this.IP}>";
        }
    }
}
