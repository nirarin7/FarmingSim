using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Inventory : MonoBehaviour {
    public static Inventory Instance;

    public List<ItemData> items = new List<ItemData>();
    private readonly Dictionary<string, InventoryItem> _itemLookUp = new Dictionary<string, InventoryItem>();

    private void Awake() {
        if (!Instance) {
            Instance = this;
            BuildItemLookUp();
        } else if (Instance) {
            Debug.Log("Instance already exist, destroying object");
            Destroy(this);
        }
    }

    private void BuildItemLookUp() {
        foreach (var item in items) {
            _itemLookUp.Add(item.name, new InventoryItem {count = 1, ItemData = item});
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
        if (_itemLookUp.ContainsKey(item.ItemData.name)) {
            _itemLookUp[item.ItemData.name].count += item.count;
        } else {
            _itemLookUp.Add(item.ItemData.name, item);
        }
    }

    public override string ToString() {
        StringBuilder sb = new StringBuilder();

        sb.AppendLine("Current Items");
        foreach (var item in _itemLookUp.Select(pair => pair.Value)) {
            sb.AppendLine($"Item: {item.ItemData.name}, Count: {item.count}");
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