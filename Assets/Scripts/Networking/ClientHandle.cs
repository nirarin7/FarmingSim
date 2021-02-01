using System.Collections;
using System.Net;
using GameServerLib.Packet;
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

    public static void SpawnPlayer(Packet packet)
    {
        int id = packet.ReadInt();
        string username = packet.ReadString();
        GameServerLib.DataModels.Vector2 pVector = packet.ReadVector2();
        Vector2 position = new Vector2(pVector.X, pVector.Y);
        
        GameManager.Instance.SpawnPlayer(id, username, position);
    }
    
    public static void PlayerPosition(Packet packet)
    {
        int id = packet.ReadInt();
        GameServerLib.DataModels.Vector2 pVector = packet.ReadVector2();
        Vector2 position = new Vector2(pVector.X, pVector.Y);
        // Vector2 position = packet.ReadVector2();


        if (!GameManager.players[id].isLocal)
        {
            GameManager.players[id].transform.position = position;
        }
    }
}