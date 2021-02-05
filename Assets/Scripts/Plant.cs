using System.Linq;
using ScriptableObjects;
using UnityEngine;
using Random = UnityEngine.Random;

public class Plant : MonoBehaviour, IDestroyable, IMaturable {
    public GameObject harvestItem;
    public PlantData plantData;

    public bool HasBeenDestroyed { get; set; }

    private int _numberOfDaysGrown;
    private int _totalSpritesCount;
    private SpriteRenderer _spriteRenderer;

    private void Awake() {
        if (!plantData) {
            Debug.Log($"There is no plant data attached to the plant: {gameObject.name}.");
            return;
        }

        _spriteRenderer = GetComponent<SpriteRenderer>();
        if (_spriteRenderer)
            _spriteRenderer.sprite = plantData.SeedSprite;


        _totalSpritesCount = plantData.GrowingSprites.Count;
    }

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update() { }

    public void Matures() {
        _numberOfDaysGrown++;

        if (IsReadyToHarvest())
            _spriteRenderer.sprite = plantData.HarvestSprite;
        else
            _spriteRenderer.sprite = plantData.GrowingSprites[CalculateSpriteIndex()];
    }

    private int CalculateSpriteIndex() {
        return (int) ((_totalSpritesCount / (float) plantData.GrowthTimeDays) * _numberOfDaysGrown);
    }

    public void Harvest() {
        Debug.Log("Item dropped");

        var dropAmount = Random.Range(plantData.MinDrop, plantData.MaxDrop); // is this already an int?

        var plantPosition = gameObject.transform.position;

        // drops in range of plant
        for (int numberDropped = 0; numberDropped < dropAmount; numberDropped++) {
            // fruit appears on plant
            var itemDrop = Instantiate(harvestItem, new Vector2(plantPosition.x, plantPosition.y), Quaternion.identity);
            var item = itemDrop.GetComponent<Item>();
            
            if(item)
                item.SetItemData(plantData.Drop);
        }

        if (plantData.HarvestType == HarvestType.SingleHarvest) {
            Debug.Log("this is a single harvest plant");
            RemoveFromGame();
        } else if (plantData.HarvestType == HarvestType.MultipleHarvest) {
            // TODO: Extract this into a class or data, not which one yet.
            _numberOfDaysGrown = (plantData.GrowthTimeDays / 2) + 3;
            _spriteRenderer.sprite = plantData.GrowingSprites.Last();
            // after 'season' ends, destroy the plant?
        }
    }

    public bool IsReadyToHarvest() {
        return _numberOfDaysGrown >= plantData.GrowthTimeDays;
    }

    public void RemoveFromGame() {
        HasBeenDestroyed = true;
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}


public enum HarvestType {
    SingleHarvest,
    MultipleHarvest,
    IndefiniteHarvest,
}