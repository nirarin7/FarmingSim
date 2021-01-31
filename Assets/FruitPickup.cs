using UnityEngine;

public class FruitPickup : MonoBehaviour {
    private GameObject _player;
    public float movementSpeed;
    public float inRangeOffset;

    // Update is called once per frame
    void Update() {
        MoveTowardsPlayer();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        //TODO: make sure only Player collider
        _player = other.gameObject;
    }

    private void MoveTowardsPlayer() {
        if (_player != null) {
            var _fruitPosition = gameObject.transform.position;
            var _playerPosition = _player.transform.position;

            var step = movementSpeed * Time.deltaTime;

            gameObject.transform.position =
                Vector2.MoveTowards(_fruitPosition, _playerPosition, step);
            var inRangeMinX = _playerPosition.x - inRangeOffset;
            var inRangeMaxX = _playerPosition.x + inRangeOffset;
            var inRangeMinY = _playerPosition.y - inRangeOffset;
            var inRangeMaxY = _playerPosition.y + inRangeOffset;

            if (_fruitPosition.x <= inRangeMaxX && _fruitPosition.x >= inRangeMinX && _fruitPosition.y <= inRangeMaxY &&
                _fruitPosition.y >= inRangeMinY) {
                AddToPlayer();
            }
        }
    }

    private void AddToPlayer() {
        Destroy(gameObject);
        Debug.Log("Tomato was deestroyed");
    }
}