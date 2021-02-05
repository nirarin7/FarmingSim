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

    public void PlayerInteract(GameObject item) {
        if (item == null) {
            Debug.Log("Interacting with null item");
            return;
        }


        var playerPlant = item.GetComponent<Plant>();
        if (playerPlant != null) {
            if (isPlowed && plant == null)
                plant = Instantiate(item, gameObject.transform.position, Quaternion.identity);
        }

        var shovel = item.GetComponent<Shovel>();
        if (!isPlowed && shovel != null) {
            _spriteRenderer.sprite = plowedGroundSprite;
            isPlowed = true;
        }

        if (plant != null) {
            var plantScript = plant.GetComponent<Plant>();
            if (plantScript != null && plantScript.IsReadyToHarvest()) {
                plantScript.Harvest();

                if (plantScript.HasBeenDestroyed) {
                    plant = null;
                }
            }
        }
    }

    
    public void PlayerInteract() {
    }
}