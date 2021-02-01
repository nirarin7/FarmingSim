using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public static Dictionary<int, PlayerManager> players = new Dictionary<int, PlayerManager>();

    public GameObject localPlayerPrefab;
    public GameObject playerPrefab;
    
    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        } else if (Instance != this)
        {
            Debug.Log("GameManager Instance already exists, destorying object.");
            Destroy(this);
        }
    }

    public void SpawnPlayer(int id, string username, Vector2 position)
    {
        GameObject player;
        var isLocal = false;
        if (id == Client.Instance.id)
        {
            player = Instantiate(localPlayerPrefab, position, Quaternion.identity);
            isLocal = true;
        }
        else
        {
            player = Instantiate(playerPrefab, position, Quaternion.identity);
        }
        PlayerManager playerManager = player.GetComponent<PlayerManager>();
        playerManager.id = id;
        playerManager.username = username;
        playerManager.isLocal = isLocal;
        players.Add(id, playerManager);
    }
}
