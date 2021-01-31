using System;

namespace GameServer
{
    public class ServerHandle
    {
        public static void WelcomeReceived(int clientId, Packet packet)
        {
            int clientIdCheck = packet.ReadInt();
            string username = packet.ReadString();
            
            Console.WriteLine($"{Server.Clients[clientId].tcp.Socket.Client.RemoteEndPoint} connected successfully with id: {clientId}");
            if (clientId != clientIdCheck)
            {
                Console.WriteLine($"Player {username}, ID: {clientId} has assumed the wrong client ID {clientIdCheck}");
            }
        }

        public static void UdpTestReceived(int clientid, Packet packet)
        {
            string msg = packet.ReadString();
            Console.WriteLine($"Received UDP Packet from {clientid} message: {msg}");
        }
    }
}