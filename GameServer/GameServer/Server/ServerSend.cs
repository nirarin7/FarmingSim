using GameServer.GameModels;
using GameServerLib.Enums;
using GameServerLib.Packet;

namespace GameServer.Server
{
    public static class ServerSend
    {
        private static void SendTcpData(int tcpClient, Packet packet)
        {
            packet.WriteLength();
            GameServer.Clients[tcpClient].tcp.SendData(packet);
        }

        private static void SendUdpData(int udpClient, Packet packet)
        {
            packet.WriteLength();
            GameServer.Clients[udpClient].udp.SendData(packet);
        }

        private static void SendTCPDataToAll(Packet packet)
        {
            packet.WriteLength();
            for (int i = 1; i < GameServer.MaxPlayers; i++)
            {
                GameServer.Clients[i].tcp.SendData(packet);
            }
        }
        
        private static void SendTCPDataToAll(int expectClientId, Packet packet)
        {
            packet.WriteLength();
            for (int i = 1; i < GameServer.MaxPlayers; i++)
            {
                if (expectClientId != i)
                {
                    GameServer.Clients[i].tcp.SendData(packet);
                }
            }
        }

        private static void SendUDPDataToAll(Packet packet)
        {
            packet.WriteLength();
            for (int i = 1; i < GameServer.MaxPlayers; i++)
            {
                GameServer.Clients[i].udp.SendData(packet);
            }
        }
        
        private static void SendUDPDataToAll(int expectClientId, Packet packet)
        {
            packet.WriteLength();
            for (int i = 1; i < GameServer.MaxPlayers; i++)
            {
                if (expectClientId != i)
                {
                    GameServer.Clients[i].udp.SendData(packet);
                }
            }
        }
        
        public static void Welcome(int tcpClient, string msg)
        {
            using (Packet packet = new Packet((int)ServerPackets.Welcome))
            {
                packet.Write(msg);
                packet.Write(tcpClient);
                SendTcpData(tcpClient, packet);
            }
        }


        public static void SpawnPlayer(int clientId, Player player)
        {
            using (Packet packet = new Packet((int)ServerPackets.SpawnPlayer))
            {
                packet.Write(player.Id);
                packet.Write(player.Username);
                packet.Write(player.Position);
                
                SendTcpData(clientId, packet);
            }
        }

        public static void PlayerPosition(Player player)
        {
            using (Packet packet = new Packet((int) ServerPackets.PlayerPosition))
            {
                packet.Write(player.Id);
                packet.Write(player.Position);
                
                SendUDPDataToAll(packet);
            }
        }
        
    }
}