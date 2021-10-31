using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Inventory : MonoBehaviour {
    public static Inventory Instance;
    public static int HotBarCapacity = 10;
    
    public List<InventoryItem> items = new List<InventoryItem>();
    
    private void Awake() {
        if (!Instance) {
            Instance = this;
        } else if (Instance) {
            Debug.Log("Instance already exist, destroying object");
            Destroy(this);
        }
    }

    public void AddItem(Item item) {
        if (!item) {
            Debug.Log("Cannot get item date from item, item is null.");
            return;
        }

        if (!item.itemData) return;

        var inventoryItem = new InventoryItem {itemData = item.itemData, count = item.count};
        if (item.itemData.CanStack) {
            var duplicateItem = items.FirstOrDefault(x => x.itemData.Name == item.itemData.Name);
            if (duplicateItem != null) {
                duplicateItem.count += item.count;
            } else {
                items.Add(inventoryItem);
            }
        } else {
            items.Add(inventoryItem);
        }
    }

    public override string ToString() {
        StringBuilder sb = new StringBuilder();

        sb.AppendLine("Current Items");
        foreach (var item in items) {
            sb.AppendLine($"Item: {item.itemData.Name}, Count: {item.count}");
        }

        return sb.ToString();
    }

    public GameObject GetItem(int index) {
        if (index < items.Count) {
            return PrefabManager.Instance.GetItem(items[index].itemData);
        }
        return null;
    }

    public bool RemoveItem(ItemData itemData) {
        if (!itemData.CanStack) return false;
        var item = items.FirstOrDefault(x => x.itemData.Name == itemData.Name);
        
        if (item == null) return false;
        item.count--;
        
        if (item.count <= 0) {
            items.Remove(item);
            return true;
        }

        return false;
    }
}

public class InventoryItem {
    public ItemData itemData;
    public int count;
}