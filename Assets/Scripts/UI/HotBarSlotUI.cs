using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HotBarSlotUI : MonoBehaviour {
    public Image icon;
    public TextMeshProUGUI itemCounter;

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update() { }

    public void AddItem(InventoryItem item) {
        icon.sprite = item.itemData.Sprite;
        icon.enabled = true;
        if (item.count > 0 && item.itemData.CanStack) {
            itemCounter.text = item.count.ToString();
            itemCounter.enabled = true;
        } else {
            itemCounter.enabled = false;
        }
    }

    public void ResetSlot() {
        icon.sprite = null;
        icon.enabled = false;
        itemCounter.enabled = false;
    }
}