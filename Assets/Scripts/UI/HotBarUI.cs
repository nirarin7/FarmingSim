using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HotBarUI : MonoBehaviour {
    private HotBarSlotUI[] hotbarSlots = new HotBarSlotUI[Inventory.HotBarCapacity];

    // Start is called before the first frame update
    void Start() {
        hotbarSlots = GetComponentsInChildren<HotBarSlotUI>();
    }

    // Update is called once per frame
    void Update() {
        Refresh();
    }

    void Refresh() {
        var hotbarItems = Inventory.Instance.items;
        for (int i = 0; i < hotbarSlots.Length; i++) {
            if (i < hotbarItems.Count && hotbarItems[i] != null) {
                hotbarSlots[i].AddItem(hotbarItems[i]);
            } else {
                hotbarSlots[i].ResetSlot();
            }
        }
    }
}