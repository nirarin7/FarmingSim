using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Scriptable Objects/Items/Item")]
public class ItemData : ScriptableObject {
    
    [SerializeField] private new string name;
    [SerializeField] private string description;
    [SerializeField] private Sprite sprite;
    [SerializeField] private bool canStack;
    [SerializeField] private ItemType type;
    [SerializeField] private int salePrice;
    [SerializeField] private Quality _baseQuality;

    public string Name => name;
    public string Description => description;
    public Sprite Sprite => sprite;
    public bool CanStack => canStack;
    public ItemType Type => type;
    public int SalePrice => salePrice;
    public Quality BaseQuality => _baseQuality;
}


