using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using Unity.Mathematics;
using UnityEngine;

public class PrefabManager : MonoBehaviour {
    public static PrefabManager Instance;
    public GameObject baseItem;
    public GameObject consumableItem;

    public GameObject plantPrefab;

    private void Awake() {
        if (!Instance) {
            Instance = this;
        } else if (Instance) {
            Debug.Log("Instance already exist, destroying object");
            Destroy(this);
        }
    }

    public GameObject GetItem(ItemData itemData) {
        var prefab = GetPrefab(itemData.Type);
        var newItem = CreateGameObject(prefab);

        var item = newItem.GetComponent<Item>();
        if (!item) 
            item = newItem.AddComponent<Item>();

        item.SetItemData(itemData);
        return newItem;
    }

    public GameObject GetPlant(PlantData plantData, GameObject parent = null) {
        var newObject = parent ? CreateGameObject(plantPrefab, parent) : CreateGameObject(plantPrefab);
        var plant = newObject.GetComponent<Plant>();
        if (!plant)
            plant = newObject.AddComponent<Plant>();
        plant.Init(plantData);
        return newObject;
    }

    private GameObject CreateGameObject(GameObject prefab, GameObject parent) {
        return Instantiate(prefab, parent.transform);
    }

    private GameObject CreateGameObject(GameObject prefab) {
        return Instantiate(prefab, Vector3.zero, quaternion.identity);
    }

    private GameObject GetPrefab(ItemType itemType) {
        if (itemType == ItemType.Consumable) {
            return consumableItem;
        }

        return baseItem;
    }
}