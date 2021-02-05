using System;
using UnityEngine;

public class PlayerPickup : MonoBehaviour, IDestroyable {
    public float movementSpeed;
    public float inRangeOffset;
    public bool HasBeenDestroyed { get; set; }

    private GameObject _player;
    private bool _playerInRange = false;
    private Item _item;

    private void Awake() {
        _item = GetComponent<Item>();
    }

    // Update is called once per frame
    void Update() {
        if (_playerInRange) {
            MoveTowardsPlayer();
        }

    }

    private void OnTriggerEnter2D(Collider2D other) {
        
        if (_playerInRange) return;
        
        _player = other.gameObject;
        _playerInRange = true;
    }

    private void MoveTowardsPlayer() {
        if (_player != null) {
            var playerPosition = _player.transform.position;
            var itemPosition = gameObject.transform.position;
            var step = movementSpeed * Time.deltaTime;

            gameObject.transform.position = Vector2.MoveTowards(itemPosition, playerPosition, step);

            // item disappears when in player range
            var distance = Vector3.Distance(itemPosition, playerPosition);
            if (distance <= inRangeOffset) {
                AddToPlayer();
            }
        } else {
            Debug.Log("Player is null.");
        }
    }

    private void AddToPlayer() {
        Inventory.Instance.AddItem(_item);
        RemoveFromGame();
    }

    public void RemoveFromGame() {
        HasBeenDestroyed = true;
        gameObject.SetActive(false);
        Destroy(gameObject);
    }

    public bool HasBeenPickedUpByPlayer() {
        return _playerInRange;
    }
}