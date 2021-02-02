using UnityEngine;

public class Player : MonoBehaviour {
    public float speed = .5f;
    public bool isOnline = false;
    public GameObject plant;
    public GameObject equippedItem;
    public GameObject item1;
    public GameObject item2;
    public GameObject item3;

    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private Camera _mainCamera;

    // Start is called before the first frame update
    void Start() {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _mainCamera = GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update() {
        EquipItem();
        MouseOverObjects();
        if(equippedItem != null)
            equippedItem.transform.position = gameObject.transform.position + new Vector3(.6f, 0f);
    }

    private void EquipItem() {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            
            if(equippedItem != null)
                equippedItem.SetActive(false);
            equippedItem = Instantiate(item1, gameObject.transform.position + new Vector3(.6f,0f), Quaternion.identity);
            equippedItem.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            if(equippedItem != null)
                equippedItem.SetActive(false);
            equippedItem = Instantiate(item2, gameObject.transform.position + new Vector3(.6f,0f), Quaternion.identity);
            equippedItem.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3)) {
            if(equippedItem != null)
                equippedItem.SetActive(false);
            equippedItem = Instantiate(item3, gameObject.transform.position + new Vector3(.6f,0f), Quaternion.identity);
            equippedItem.SetActive(true);
        }
    }

    private void MouseOverObjects() {
        if (_mainCamera == null) {
            Debug.Log("Camera is not attached to player.");
            return;
        }

        RaycastHit2D hit = Physics2D.GetRayIntersection(_mainCamera.ScreenPointToRay(Input.mousePosition));
        var isPointNearPlayer = IsPointNearPlayer(hit.point.x, hit.point.y, 4.5f);
        if (isPointNearPlayer) {
            if (hit.collider != null) {
                var groundTileGameObject = hit.collider.gameObject;
                var groundTile = groundTileGameObject.GetComponent<InteractableGroundTile>();

                if (groundTile != null && Input.GetMouseButtonDown(0)) {
                    // move this logic to the ground tile and generalize into an Item class
                    groundTile.Interact(equippedItem);
                }
            }
        }
    }

    private bool IsPointNearPlayer(float pointX, float pointY, float offset) {
        var distance = Vector3.Distance(gameObject.transform.position, new Vector3(pointX, pointY, 0f));
        return distance < offset;
    }

    private void FixedUpdate() {
        gameObject.transform.position = Move();
        if (isOnline) {
            SendDataToServer();
        }
    }

    private void SendDataToServer() {
        SendPositionToServer(gameObject.transform.position);
    }

    private Vector2 Move() {
        var moveHorizontal = Input.GetAxis("Horizontal");
        var moveVertical = Input.GetAxis("Vertical");

        _animator.SetFloat("HorizontalDirection", moveHorizontal);
        _animator.SetFloat("VerticalDirection", moveVertical);

        var movement = new Vector2(moveHorizontal, moveVertical) * speed;
        var position = transform.position;
        return new Vector2(position.x + movement.x, position.y + movement.y);
    }

    private void SendPositionToServer(Vector2 position) {
        ClientSend.PlayerPosition(position);
    }

    public string getTool() {
        return "Plow";
    }

    public GameObject getPlant() {
        return plant;
    }
}