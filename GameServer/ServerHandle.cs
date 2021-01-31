using System;
using System.Numerics;

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
            
            Server.Clients[clientId].SendIntoGame(username);
        }
        
        
        public static void PlayerMovement(int clientId, Packet packet)
        {
            Vector2 position = new Vector2(packet.ReadFloat(), packet.ReadFloat());
            Server.Clients[clientId].Player.SetPosition(position);
        }
    }
}