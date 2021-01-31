using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class ClientHandle : MonoBehaviour
{
    public static void Welcome(Packet packet)
    {
        string msg = packet.ReadString();
        int id = packet.ReadInt();
        Debug.Log($"Message from server: {msg}");
        Client.Instance.id = id;
        ClientSend.WelcomeReceived();

        Client.Instance.udp.Connect(((IPEndPoint) Client.Instance.tcp.socket.Client.LocalEndPoint).Port);
    }

    public static void UdpTest(Packet packet)
    {
        string msg = packet.ReadString();
        Debug.Log($"Received packet via UDP. Contains message: {msg}");
        ClientSend.UdpTestRecieve();
    }
}