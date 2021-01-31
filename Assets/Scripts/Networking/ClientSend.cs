using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientSend : MonoBehaviour
{
    private static void SendTCPData(Packet packet)
    {
        packet.WriteLength();
        Client.Instance.tcp.SendData(packet);
    }

    private static void SendUDPData(Packet packet)
    {
        packet.WriteLength();
        Client.Instance.udp.SendData(packet);
    }

    public static void WelcomeReceived()
    {
        using (Packet packet = new Packet((int)ClientPackets.WelcomeReceived))
        {
            packet.Write(Client.Instance.id);
            packet.Write(UiManager.instance.username.text);
            SendTCPData(packet);
        }
    }

    public static void PlayerPosition(Vector2 position)
    {
        using (Packet packet = new Packet((int) ClientPackets.PlayerPosition))
        {
            packet.Write(position);
            SendUDPData(packet);
        }
    }
}
