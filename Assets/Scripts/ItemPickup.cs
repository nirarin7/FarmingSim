using UnityEngine;
using UnityEngine.Networking;

public class ItemPickup : MonoBehaviour, IDestroyable {
    public float movementSpeed;
    public float inRangeOffset;
    public float dropOffSet;
    public bool HasBeenDestroyed { get; set; }

    private GameObject _player;
    private Vector2 _dropPosition;
    private bool _playerInRange = false;

    
    ///this might need to be separated into a HarvestItem class or something.
    private void Awake() {
        var itemPosition = gameObject.transform.position;

        // moves from source to ground
        var xEnd = Random.Range(itemPosition.x - dropOffSet, itemPosition.x + dropOffSet);
        var yEnd = Random.Range(itemPosition.y - dropOffSet, itemPosition.y + dropOffSet);

        _dropPosition = new Vector2(xEnd, yEnd);
    }

    // Update is called once per frame
    void Update() {
        if (_playerInRange) {
            MoveTowardsPlayer();
        } else {
            MovetoGround();
        }
    }
    
    //items move 
    private void MovetoGround() {
        var step = movementSpeed * Time.deltaTime;
        gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, _dropPosition, step);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        //TODO: make sure only Player collider
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
        RemoveFromGame();
    }

    public void RemoveFromGame() {
        HasBeenDestroyed = true;
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}