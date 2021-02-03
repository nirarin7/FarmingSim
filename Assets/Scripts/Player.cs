using UnityEngine;

public class Player : MonoBehaviour {
    
    public float speed = .5f;
    public GameObject plant;
    public GameObject equippedItem;
    public GameObject item1;
    public GameObject item2;
    public GameObject item3;

    private Camera _mainCamera;

    protected Animator Animator;
    protected float MoveHorizontal;
    protected float MoveVertical;
    public int Id;
    public string username;
    public bool isLocal;

    protected static readonly int HorizontalDirection = Animator.StringToHash("HorizontalDirection");
    protected static readonly int VerticalDirection = Animator.StringToHash("VerticalDirection");

    // Start is called before the first frame update
    void Start() {
        Animator = GetComponent<Animator>();
        _mainCamera = GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    protected virtual void Update() {
        EquipItem();
        MouseOverObjects();
        if (equippedItem != null)
            equippedItem.transform.position = gameObject.transform.position + new Vector3(.6f, 0f);
    }

    private void EquipItem() {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            if (equippedItem != null)
                equippedItem.SetActive(false);
            equippedItem = Instantiate(item1, gameObject.transform.position + new Vector3(.6f, 0f),
                Quaternion.identity);
            equippedItem.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            if (equippedItem != null)
                equippedItem.SetActive(false);
            equippedItem = Instantiate(item2, gameObject.transform.position + new Vector3(.6f, 0f),
                Quaternion.identity);
            equippedItem.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3)) {
            if (equippedItem != null)
                equippedItem.SetActive(false);
            equippedItem = Instantiate(item3, gameObject.transform.position + new Vector3(.6f, 0f),
                Quaternion.identity);
            equippedItem.SetActive(true);
        }
    }

    private void MouseOverObjects() {
        if (_mainCamera == null) {
            Debug.Log("Camera is not attached to player.");
            return;
        }
        
        LayerMask mask = LayerMask.GetMask("Player");
        RaycastHit2D hit = Physics2D.GetRayIntersection(_mainCamera.ScreenPointToRay(Input.mousePosition),15F, mask);
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

    protected virtual void FixedUpdate() {
        gameObject.transform.position = Move();
    }



    private Vector2 Move() {
        MoveHorizontal = Input.GetAxis("Horizontal");
        MoveVertical = Input.GetAxis("Vertical");

        Animator.SetFloat(HorizontalDirection, MoveHorizontal);
        Animator.SetFloat(VerticalDirection, MoveVertical);

        var movement = new Vector2(MoveHorizontal, MoveVertical) * speed;
        var position = transform.position;
        return new Vector2(position.x + movement.x, position.y + movement.y);
    }


    public virtual void SetPosition(Vector2 position) {
    }

    public virtual void SetDirection(Vector2 direction) {
    }
}