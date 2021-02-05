using UnityEngine;

public class Item : MonoBehaviour {
    public ItemData itemData;
    public int count = 1;
    
    private SpriteRenderer _spriteRenderer;

    void Awake() {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        SetItemData(itemData);
    }

     public void SetItemData(ItemData dropItem) {
         itemData = dropItem;

         if (!dropItem || !_spriteRenderer) return;
         
         _spriteRenderer.sprite = dropItem.Sprite;
     }

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update() { }
}