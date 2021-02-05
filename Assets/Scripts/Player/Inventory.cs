using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Inventory : MonoBehaviour {
    public static Inventory Instance;

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

        AddItem(new InventoryItem {ItemData = item.itemData, count = item.count});
    }

    public void AddItem(InventoryItem item) {
        if (item.ItemData && item.ItemData.CanStack) {
            var inventoryItem = items.FirstOrDefault(x => x.ItemData.Name == item.ItemData.Name);
            if (inventoryItem != null) {
                inventoryItem.count += item.count;
            } else {
                items.Add(item);
            }
        } else {
            items.Add(item);
        }
    }

    public override string ToString() {
        StringBuilder sb = new StringBuilder();

        sb.AppendLine("Current Items");
        foreach (var item in items){
            sb.AppendLine($"Item: {item.ItemData.Name}, Count: {item.count}");
        }

        return sb.ToString();
    }

    // public void RemoveItem() {

    // }
}

public class InventoryItem {
    public int count = 0;
    public ItemData ItemData;
}