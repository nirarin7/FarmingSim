using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace GameServer
{
    public class Server
    {
        public static int MaxPlayers { get; set; }
        public static int Port { get; set; }
        public static Dictionary<int, Client> Clients = new Dictionary<int, Client>();

        public delegate void PacketHandler(int clientId, Packet packer);

        public static Dictionary<int, PacketHandler> packetHandlers = new Dictionary<int, PacketHandler>();

        private static TcpListener _tcpListener;
        private static UdpClient _udpListner;

        public static void Start(int maxPlayers, int port)
        {
            MaxPlayers = maxPlayers;
            Port = port;

            Console.WriteLine("Starting server...");
            InitializeServerData();

            _tcpListener = new TcpListener(IPAddress.Any, Port);
            _tcpListener.Start();
            _tcpListener.BeginAcceptTcpClient(TcpConnectCallback, null);

            _udpListner = new UdpClient(port);
            _udpListner.BeginReceive(UdpReceiveCallback, null);

            Console.WriteLine($"Server started on {port}");
        }

        private static void TcpConnectCallback(IAsyncResult result)
        {
            TcpClient tcpClient = _tcpListener.EndAcceptTcpClient(result);
            _tcpListener.BeginAcceptTcpClient(TcpConnectCallback, null);
            Console.WriteLine($"Incoming connection from {tcpClient.Client.RemoteEndPoint}...");

            for (int i = 1; i <= MaxPlayers; i++)
            {
                if (Clients[i].tcp.Socket == null)
                {
                    Clients[i].tcp.Connect(tcpClient);
                    return;
                }
            }

            Console.WriteLine($"{tcpClient.Client.RemoteEndPoint} failed to connect: Server full!");
        }

        private static void UdpReceiveCallback(IAsyncResult result)
        {
            try
            {
                IPEndPoint clientEndPoint = new IPEndPoint(IPAddress.Any, 0);
                byte[] data = _udpListner.EndReceive(result, ref clientEndPoint);
                _udpListner.BeginReceive(UdpReceiveCallback, null);

                if (data.Length < 4)
                {
                    return;
                }

                using (Packet packet = new Packet(data))
                {
                    int clientId = packet.ReadInt();

                    if (clientId == 0)
                    {
                        return;
                    }

                    if (Clients[clientId].udp.EndPoint == null)
                    {
                        Clients[clientId].udp.Connect(clientEndPoint);
                        return;
                    }

                    if (Clients[clientId].udp.EndPoint.ToString() == clientEndPoint.ToString())
                    {
                        Clients[clientId].udp.HandleData(packet);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"There was an error during UDP receive callback {e.Message}, {e.StackTrace}");
                throw;
            }
        }

        public static void SendUDPData(IPEndPoint clientEndPoint, Packet packet)
        {
            try
            {
                if (clientEndPoint != null)
                {
                    _udpListner.BeginSend(packet.ToArray(), packet.Length(), clientEndPoint, null, null);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error while sending udp data {e.Message}, {e.StackTrace}");
                throw;
            }
            
        }

        private static void InitializeServerData()
        {
            for (int i = 1; i <= MaxPlayers; i++)
            {
                Clients.Add(i, new Client(i));
            }

            packetHandlers = new Dictionary<int, PacketHandler>()
            {
                {(int) ClientPackets.welcomeReceived, ServerHandle.WelcomeReceived},
                {(int) ClientPackets.udpTestReceived, ServerHandle.UdpTestReceived}
            };
            Console.WriteLine("Initialized packets.");
        }
    }
}