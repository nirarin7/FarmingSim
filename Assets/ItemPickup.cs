using System;
using System.ComponentModel.Design.Serialization;
using UnityEngine;
using Random = UnityEngine.Random;

public class ItemPickup : MonoBehaviour {
    
    public float movementSpeed;
    public float inRangeOffset;
    public float dropOffSet;
    
    private GameObject _player;
    private Vector3 _itemPosition;
    private Vector2 _dropPosition;
    private bool _playerInRange = false;
    
    private void Awake() {
        _itemPosition = gameObject.transform.position;
        
        // fruit moves from plant to ground
        var xEnd = Random.Range(_itemPosition.x - dropOffSet, _itemPosition.x + dropOffSet);
        var yEnd = Random.Range(_itemPosition.y - dropOffSet, _itemPosition.y + dropOffSet);

        _dropPosition = new Vector2(xEnd, yEnd);
    }

    // Update is called once per frame
    void Update() {
        
        if (_playerInRange == true) {
            MoveTowardsPlayer();
        } else {
            MovetoGround();
        }
    }

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
            _itemPosition = gameObject.transform.position;
            var _playerPosition = _player.transform.position;
            var step = movementSpeed * Time.deltaTime;

            gameObject.transform.position =
                Vector2.MoveTowards(_itemPosition, _playerPosition, step);
            var inRangeMinX = _playerPosition.x - inRangeOffset;
            var inRangeMaxX = _playerPosition.x + inRangeOffset;
            var inRangeMinY = _playerPosition.y - inRangeOffset;
            var inRangeMaxY = _playerPosition.y + inRangeOffset;

            // fruit disappears when in player range
            if (_itemPosition.x <= inRangeMaxX && _itemPosition.x >= inRangeMinX && _itemPosition.y <= inRangeMaxY &&
                _itemPosition.y >= inRangeMinY) {
                AddToPlayer();
            }
        }
    }

    private void AddToPlayer() {
        Destroy(gameObject);
    }
}