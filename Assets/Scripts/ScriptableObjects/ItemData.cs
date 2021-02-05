using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item", order = 51)]
public class ItemData : ScriptableObject {
    
    [SerializeField] private new string name;
    [SerializeField] private string description;
    [SerializeField] private Sprite sprite;
    [SerializeField] private bool canStack;

    public string Name => name;
    public string Description => description;
    public Sprite Sprite => sprite;
    public bool CanStack => canStack;
}
