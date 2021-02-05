using System.Net;
using GameServerLib.Packet;
using UnityEngine;

public class ClientHandle : MonoBehaviour {
    public static void Welcome(Packet packet) {
        string msg = packet.ReadString();
        int id = packet.ReadInt();
        Debug.Log($"Message from server: {msg}");
        Client.Instance.id = id;
        ClientSend.WelcomeReceived();

        Client.Instance.udp.Connect(((IPEndPoint) Client.Instance.tcp.socket.Client.LocalEndPoint).Port);
    }

    public static void SpawnPlayer(Packet packet) {
        int id = packet.ReadInt();
        string username = packet.ReadString();
        GameServerLib.DataModels.Vector2 pVector = packet.ReadVector2();
        Vector2 position = new Vector2(pVector.X, pVector.Y);

        OnlineGameManager.Instance.SpawnPlayer(id, username, position);
    }

    public static void PlayerPosition(Packet packet) {
        int id = packet.ReadInt();
        Player player = OnlineGameManager.players[id];

        // TODO: have the server not send packet to the player who sent the packet
        if (player.GetType() == typeof(LocalPlayer)) return;

        GameServerLib.DataModels.Vector2 pVector = packet.ReadVector2();
        GameServerLib.DataModels.Vector2 dVector = packet.ReadVector2();

        Vector2 position = new Vector2(pVector.X, pVector.Y);
        Vector2 direction = new Vector2(dVector.X, dVector.Y);


        player.SetPosition(position);
        player.SetDirection(direction);
    }
}