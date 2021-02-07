
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

        for (int i = 0; i < _inventory.items.Count; i++) {
            var item = _inventory.items[i];
            var inventorySlot = _inventorySlots[i];
            
            inventorySlot.AddItem(item);
            
        }



    }
    
}
