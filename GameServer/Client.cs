using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace GameServer
{
    public class Client
    {
        public static int _dataBufferSize = 4096;

        public int id;
        public TCP tcp;
        public UDP udp;

        public Client(int id)
        {
            this.id = id;
            this.tcp = new TCP(id);
            this.udp = new UDP(id);
        }

        public class TCP
        {
            public TcpClient Socket { get; private set; }
            
            private readonly int _id;
            private NetworkStream _stream;
            private Packet _receiveData;
            private byte[] _receiveBuffer;

            public TCP(int id)
            {
                this._id = id;
            }

            public void Connect(TcpClient tcpSocket)
            {
                Socket = tcpSocket;
                Socket.ReceiveBufferSize = _dataBufferSize;
                Socket.SendBufferSize = _dataBufferSize;

                _stream = Socket.GetStream();
                _receiveData = new Packet();
                _receiveBuffer = new byte[_dataBufferSize];

                _stream.BeginRead(_receiveBuffer, 0, _dataBufferSize, ReceiveCallback, null);

                ServerSend.Welcome(_id, "Welcome to the server!");
            }

            public void SendData(Packet packet)
            {
                try
                {
                    if (Socket != null)
                    {
                        _stream.BeginWrite(packet.ToArray(), 0, packet.Length(), null, null);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error when sending data to player {_id} via TCP.");
                    throw;
                }
            }

            private void ReceiveCallback(IAsyncResult result)
            {
                try
                {
                    int byteLength = _stream.EndRead(result);
                    if (byteLength <= 0)
                    {
                        return;
                    }

                    byte[] data = new byte[byteLength];
                    Array.Copy(_receiveBuffer, data, byteLength);

                    _receiveData.Reset(HandleData(data));
                    _stream.BeginRead(_receiveBuffer, 0, _dataBufferSize, ReceiveCallback, null);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"There was an error: {e.Message}, {e.StackTrace}");
                }
            } 

            private bool HandleData(byte[] data)
            {
                int packetLength = 0;
                _receiveData.SetBytes(data);
                if (_receiveData.UnreadLength() >= 4)
                {
                    packetLength = _receiveData.ReadInt();
                    if (packetLength <= 0)
                    {
                        return true;
                    }
                }

                while (packetLength > 0 && packetLength <= _receiveData.UnreadLength())
                {
                    byte[] packetBytes = _receiveData.ReadBytes(packetLength);
                    ThreadManager.ExecuteOnMainThread(() =>
                    {
                        using (Packet packet = new Packet(packetBytes))
                        {
                            int packetId = packet.ReadInt();
                            Server.packetHandlers[packetId](_id, packet);
                        }
                    });
                    
                    packetLength = 0;
                    if (_receiveData.UnreadLength() >= 4)
                    {
                        packetLength = _receiveData.ReadInt();
                        if (packetLength <= 0)
                        {
                            return true;
                        }
                    }
                }

                if (packetLength <= 1)
                {
                    return true;
                }

                return false;
            }
        }

        public class UDP
        {
            public IPEndPoint EndPoint;

            private int id;

            public UDP(int id)
            {
                this.id = id;
            }

            public void Connect(IPEndPoint endPoint)
            {
                EndPoint = endPoint;
                ServerSend.UdpTest(id);
            }
            
            public void SendData(Packet packet)
            {
                Server.SendUDPData(EndPoint, packet);
            }
            
            public void HandleData(Packet packetData)
            {
                int packetLength = packetData.ReadInt();
                byte[] packetBytes = packetData.ReadBytes(packetLength);
                
                ThreadManager.ExecuteOnMainThread(() =>
                {
                    using (Packet packet = new Packet(packetBytes))
                    {
                        int packetId = packet.ReadInt();
                        Server.packetHandlers[packetId](id, packet);
                    }
                });
                
            }

        }
    }
}