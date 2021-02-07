using UnityEngine;
using UnityEngine.UI;

public class HotBarSlotUI : MonoBehaviour {
    public Image icon;

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update() { }

    public void AddItem(InventoryItem item) {
        icon.sprite = item.itemData.Sprite;
        icon.enabled = true;
    }

    public void ResetSlot() {
        icon.sprite = null;
        icon.enabled = false;
    }
}