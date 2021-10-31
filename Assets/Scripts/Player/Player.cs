using System;
using UnityEngine;

public class Player : MonoBehaviour {
    public float speed = .5f;
    public GameObject equippedItem;

    private Camera _mainCamera;

    protected Animator Animator;
    protected float MoveHorizontal;
    protected float MoveVertical;
    public int Id;
    public string username;
    public bool isLocal;

    protected static readonly int HorizontalDirection = Animator.StringToHash("HorizontalDirection");
    protected static readonly int VerticalDirection = Animator.StringToHash("VerticalDirection");
    private int equippedIndex = -1;

    // Start is called before the first frame update
    void Start() {
        Animator = GetComponent<Animator>();
        _mainCamera = GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    protected virtual void Update() {
        var invIndex = InventoryIndex();
        if (invIndex >= 0 && equippedIndex != invIndex) {
            EquipItem(invIndex);
        }

        MouseOverObjects();
        if (equippedItem != null)
            equippedItem.transform.position = gameObject.transform.position + new Vector3(.6f, 0f);
    }

    private void EquipItem(int invIndex) {
        RemovedEquippedItem();
        var newItem = Inventory.Instance.GetItem(invIndex);
        if (newItem) equippedItem = newItem;
    }

    protected virtual void FixedUpdate() {
        gameObject.transform.position = Move();
    }

    private int InventoryIndex() {
        var index = -1;
        if (Input.GetKeyDown(KeyCode.Alpha1))
            index = 0;
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            index = 1;
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            index = 2;
        else if (Input.GetKeyDown(KeyCode.Alpha4))
            index = 3;
        else if (Input.GetKeyDown(KeyCode.Alpha5))
            index = 4;
        else if (Input.GetKeyDown(KeyCode.Alpha6))
            index = 5;
        else if (Input.GetKeyDown(KeyCode.Alpha7))
            index = 6;
        else if (Input.GetKeyDown(KeyCode.Alpha8))
            index = 7;
        else if (Input.GetKeyDown(KeyCode.Alpha9))
            index = 8;
        else if (Input.GetKeyDown(KeyCode.Alpha0))
            index = 9;
        return index;
    }

    private void MouseOverObjects() {
        if (_mainCamera == null) {
            Debug.Log("Camera is not attached to player.");
            return;
        }

        LayerMask mask = LayerMask.GetMask("Player");
        RaycastHit2D hit = Physics2D.GetRayIntersection(_mainCamera.ScreenPointToRay(Input.mousePosition), 15F, mask);
        var isPointNearPlayer = IsPointNearPlayer(hit.point.x, hit.point.y, 4.5f);
        if (isPointNearPlayer && hit.collider != null) {
            var hitObject = hit.collider.gameObject;
            var playerInteractable = hitObject.GetComponent<IPlayerInteractable>();

            if (playerInteractable != null && Input.GetMouseButtonDown(0)) {
                // move this logic to the ground tile and generalize into an Item class

                bool isSuccessfull;
                // if (equippedItem != null)
                    isSuccessfull = playerInteractable.PlayerInteract(equippedItem);
                // else
                    // isSuccessfull = playerInteractable.PlayerInteract();

               if (isSuccessfull) {
                    if (equippedItem && Inventory.Instance.RemoveItem(equippedItem.GetComponent<Item>().itemData)) {
                        RemovedEquippedItem();
                    }
                }
            }
        }
    }

    private void RemovedEquippedItem() {
        if (!equippedItem) return;
        
        equippedItem.SetActive(false);
        Destroy(equippedItem);
        equippedItem = null;
    }

    private bool IsPointNearPlayer(float pointX, float pointY, float offset) {
        var distance = Vector3.Distance(gameObject.transform.position, new Vector3(pointX, pointY, 0f));
        return distance < offset;
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


    public virtual void SetPosition(Vector2 position) { }

    public virtual void SetDirection(Vector2 direction) { }
}