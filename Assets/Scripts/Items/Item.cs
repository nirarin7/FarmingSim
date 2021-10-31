using UnityEditor;
using UnityEngine;

public class Item : MonoBehaviour {
    public ItemData itemData;
    public int count = 1;
    
    private SpriteRenderer _spriteRenderer;

    void Awake() {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        SetItemData(itemData);
    }

    public void InitItem() {
         if (!itemData || !_spriteRenderer) return;
            _spriteRenderer.sprite = itemData.Sprite;
    }

     public void SetItemData(ItemData dropItem) {
         itemData = dropItem;
         InitItem();
     }

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update() { }
}