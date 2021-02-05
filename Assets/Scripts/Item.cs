using UnityEngine;

public class Item : MonoBehaviour {
    public ItemData itemData;
    public int count = 1;
    
    private SpriteRenderer _spriteRenderer;

    void Awake() {
        if (_spriteRenderer == null) _spriteRenderer = GetComponent<SpriteRenderer>();
        SetItemData();
    }

    private void SetItemData() {
        if (itemData) {
            if (_spriteRenderer) {
                _spriteRenderer.sprite = itemData.Sprite;
            }
        }
    }

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update() { }
}