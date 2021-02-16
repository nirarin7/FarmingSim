using ScriptableObjects;
using UnityEngine;

[CreateAssetMenu(fileName = "New Seed", menuName = "Scriptable Objects/Items/Seed Item")]
public class SeedData : ItemData {
    [SerializeField] private PlantData _plantData;

    public PlantData PlantData => _plantData;
}