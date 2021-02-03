using System.Collections.Generic;
using UnityEngine;

public class OnlineGameManager : MonoBehaviour {
    public static OnlineGameManager Instance;

    public static Dictionary<int, Player> players = new Dictionary<int, Player>();

    public GameObject localPlayerPrefab;
    public GameObject playerPrefab;

    public void Awake() {
        if (Instance == null) {
            Instance = this;
        } else if (Instance != this) {
            Debug.Log("GameManager Instance already exists, destorying object.");
            Destroy(this);
        }
    }

    public void SpawnPlayer(int id, string username, Vector2 position) {
        GameObject player;
        var isLocal = false;
        if (id == Client.Instance.id) {
            player = Instantiate(localPlayerPrefab, position, Quaternion.identity);
            isLocal = true;
        } else {
            player = Instantiate(playerPrefab, position, Quaternion.identity);
        }

        Player serverPlayer = player.GetComponent<Player>();
        serverPlayer.Id = id;
        serverPlayer.username = username;
        serverPlayer.isLocal = isLocal;
        players.Add(id, serverPlayer);
    }
}