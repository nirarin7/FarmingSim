using UnityEngine;
using Random = UnityEngine.Random;

public class MoveToDestination : MonoBehaviour {
    public float movementSpeed;

    public bool randomDestinationWithInRange = true;
    public float range;
    public Vector2 destination;

    private float _destinationThreshold = .1f;
    private bool _isWithInThreshold;
    private PlayerPickup _playerPickup;


    private void Awake() {
        _playerPickup = GetComponent<PlayerPickup>();
        SetDestination();
    }

    void Update() {
        if (_isWithInThreshold || HasBeenPickedUpByPlayer()) return;

        Move();
        _isWithInThreshold = Vector2.Distance(transform.position, destination) <= _destinationThreshold;
    }

    private bool HasBeenPickedUpByPlayer() {
        return _playerPickup != null && _playerPickup.HasBeenPickedUpByPlayer();
    }

    private void Move() {
        var step = movementSpeed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, destination, step);
    }

    private void SetDestination() {
        var itemPosition = gameObject.transform.position;
        float xEnd;
        float yEnd;
        if (randomDestinationWithInRange) {
            xEnd = Random.Range(itemPosition.x - range, itemPosition.x + range);
            yEnd = Random.Range(itemPosition.y - range, itemPosition.y + range);
        } else {
            xEnd = destination.x;
            yEnd = destination.y;
        }

        destination = new Vector2(xEnd, yEnd);
    }
}