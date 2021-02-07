
using UnityEngine;

public class InventoryUI : MonoBehaviour {

    private InventorySlotUI [] _inventorySlots;
    private Inventory _inventory;
    
    
    // Start is called before the first frame update
    void Start() {
        _inventory = Inventory.Instance;
        _inventorySlots = GetComponentsInChildren<InventorySlotUI>();
    }

    // Update is called once per frame
    void Update() {
        
        UpdateUI();
    }
   
    
    public void InventoryToggle() {
        gameObject.SetActive(!gameObject.activeSelf);
        
        
    }

    public void UpdateUI() {
        var items = Inventory.Instance.items;
        for (int i = 0; i < _inventorySlots.Length; i++) {
            if (i < items.Count && items[i] != null) {
                _inventorySlots[i].AddItem(items[i]);
            } else {
                _inventorySlots[i].ResetSlot();
            }
        }

    }
    
}
