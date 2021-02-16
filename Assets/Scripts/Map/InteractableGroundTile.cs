using UnityEngine;

public class InteractableGroundTile : MonoBehaviour, IPlayerInteractable {
    public GameObject plant;
    public Sprite plowedGroundSprite;

    private SpriteRenderer _spriteRenderer;
    private Sprite _baseGround;
    private Color _highlightedColor = Color.red;
    private Color _defaultColor;
    public bool isPlowed = false;

    // Start is called before the first frame update
    void Start() {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        if (_spriteRenderer) {
            _defaultColor = _spriteRenderer.color;
            _baseGround = _spriteRenderer.sprite;
        }
    }

    // TODO: Clean this shit up, Taylor!
    public bool PlayerInteract(GameObject item) {
        // if (item == null) {
            // Debug.Log("Interacting with null item");
        // }

        if (item) {
            var equipItem = item.GetComponent<Item>();
            if (equipItem != null) {
                if (!isPlowed && equipItem.itemData.GetType() == typeof(EquipmentData) &&
                    ((EquipmentData) equipItem.itemData).Roles.Contains(EquipmentRoles.Shovel)) {
                    _spriteRenderer.sprite = plowedGroundSprite;
                    isPlowed = true;
                    return true;
                }
                
                if (isPlowed && !plant && equipItem.itemData.GetType() == typeof(SeedData)) {
                    var seedData = (SeedData) equipItem.itemData;
                    plant = PrefabManager.Instance.GetPlant(seedData.PlantData, gameObject);
                    plant.transform.position = gameObject.transform.position;
                    return true;
                }
            }
        }

        if (plant != null) {
            var plantScript = plant.GetComponent<Plant>();
            if (plantScript != null && plantScript.IsReadyToHarvest()) {
                plantScript.Harvest();

                if (plantScript.HasBeenDestroyed) {
                    plant = null;
                }

                return true;
            }
        }

        return false;
    }


    public bool PlayerInteract() {
        return false;
    }
}