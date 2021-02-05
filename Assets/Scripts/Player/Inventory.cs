using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Inventory {
    private readonly Dictionary<int, Item> _items = new Dictionary<int, Item>();

    public Inventory() {
        AddItem(new Item {Id = 1, Count = 1, DisplayName = "Tomato"});
        AddItem(new Item {Id = 2, Count = 1, DisplayName = "Potato"});
        AddItem(new Item {Id = 3, Count = 3, DisplayName = "Cat"});
        AddItem(new Item {Id = 4, Count = 5, DisplayName = "Tomato Seeds"});
        AddItem(new Item {Id = 1, Count = 1, DisplayName = "Tomato"});
    }

    public void AddItem(Item item) {
        if (_items.ContainsKey(item.Id)) {
            _items[item.Id].Count += item.Count;
        } else {
            _items.Add(item.Id, item);
        }
    }

    public override string ToString() {
        StringBuilder sb = new StringBuilder();

        sb.AppendLine("Current Items");
        foreach (var item in _items.Select(pair => pair.Value)) {
            sb.AppendLine($"Item: {item.DisplayName}, Count: {item.Count}");
        }

        return sb.ToString();
    }

    // public void RemoveItem() {

    // }
}