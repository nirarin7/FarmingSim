using System.Net.Sockets;

namespace GameServer
{
    public class ServerSend
    {
        private static void SendTcpData(int tcpClient, Packet packet)
        {
            packet.WriteLength();
            Server.Clients[tcpClient].tcp.SendData(packet);
        }

        private static void SendUdpData(int udpClient, Packet packet)
        {
            packet.WriteLength();
            Server.Clients[udpClient].udp.SendData(packet);
        }

        private static void SendTCPDataToAll(Packet packet)
        {
            packet.WriteLength();
            for (int i = 1; i < Server.MaxPlayers; i++)
            {
                Server.Clients[i].tcp.SendData(packet);
            }
        }
        
        private static void SendTCPDataToAll(int expectClientId, Packet packet)
        {
            packet.WriteLength();
            for (int i = 1; i < Server.MaxPlayers; i++)
            {
                if (expectClientId != i)
                {
                    Server.Clients[i].tcp.SendData(packet);
                }
            }
        }

        private static void SendUDPDataToAll(Packet packet)
        {
            packet.WriteLength();
            for (int i = 1; i < Server.MaxPlayers; i++)
            {
                Server.Clients[i].udp.SendData(packet);
            }
        }
        
        private static void SendUDPDataToAll(int expectClientId, Packet packet)
        {
            packet.WriteLength();
            for (int i = 1; i < Server.MaxPlayers; i++)
            {
                if (expectClientId != i)
                {
                    Server.Clients[i].udp.SendData(packet);
                }
            }
        }
        
        public static void Welcome(int tcpClient, string msg)
        {
            using (Packet packet = new Packet((int)ServerPackets.welcome))
            {
                packet.Write(msg);
                packet.Write(tcpClient);
                SendTcpData(tcpClient, packet);
            }
        }


        public static void SpawnPlayer(int clientId, Player player)
        {
            using (Packet packet = new Packet((int)ServerPackets.spawnPlayer))
            {
                packet.Write(player.Id);
                packet.Write(player.Username);
                packet.Write(player.position);
                
                SendTcpData(clientId, packet);
            }
        }

        public static void PlayerPosition(Player player)
        {
            using (Packet packet = new Packet((int) ServerPackets.playerPosition))
            {
                packet.Write(player.Id);
                packet.Write(player.position);
                
                SendUDPDataToAll(packet);
            }
        }
        
    }
}