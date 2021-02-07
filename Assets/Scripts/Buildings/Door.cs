using UnityEngine;

public class Door : MonoBehaviour {
    public Door door;
    public GameObject spawnPoint;

    private Player _player;


    private void Update() {
        if (_player && spawnPoint && door.spawnPoint && Input.GetKeyDown(KeyCode.E)) {
            _player.gameObject.transform.position = door.spawnPoint.gameObject.transform.position;
            _player = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        var player = other.gameObject.GetComponent<Player>();
        if (player) {
            _player = player;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.GetComponent<Player>()) {
            _player = null;
        }
    }
}