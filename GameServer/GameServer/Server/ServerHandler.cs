using System;
using GameServer.GameModels;
using GameServerLib.DataModels;
using GameServerLib.Packet;

namespace GameServer.Server
{
    public static class ServerHandler
    {
        public static void WelcomeReceived(int clientId, Packet packet)
        {
            int clientIdCheck = packet.ReadInt();
            string username = packet.ReadString();
            
            Console.WriteLine($"{GameServer.Clients[clientId].tcp.Socket.Client.RemoteEndPoint} connected successfully with id: {clientId}");
            if (clientId != clientIdCheck)
            {
                Console.WriteLine($"Player {username}, ID: {clientId} has assumed the wrong client ID {clientIdCheck}");
            }
            
            GameServer.Clients[clientId].SendIntoGame(username);
        }
        
        
        public static void PlayerMovement(int clientId, Packet packet)
        {
            Vector2 position = new Vector2(packet.ReadFloat(), packet.ReadFloat());
            Player player = GameServer.Clients[clientId].Player;
            player?.SetPosition(position);
        }
    }
}