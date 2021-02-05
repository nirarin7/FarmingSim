using UnityEngine;

[CreateAssetMenu(fileName = "New Consumable", menuName = "Scriptable Objects/Items/Consumable Item")]
public class ConsumableData : ItemData {
    [SerializeField] private int energyAmount;

    public int EnergyAmount => energyAmount;
}