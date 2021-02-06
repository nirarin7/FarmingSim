using System.Text;
using UnityEngine;

public class Animal : MonoBehaviour, IMaturable, IPlayerInteractable {
    public string NickName;

    public int age;
    public int currentEnergy;
    public int currentHappiness;

    public AnimalData animalData;

    private SpriteRenderer _spriteRenderer;

    // Start is called before the first frame update
    void Start() {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        if (animalData && _spriteRenderer)
            _spriteRenderer.sprite = IsAnAdult() ? animalData.MatureSprite : animalData.YouthSprite;
    }

    public void Matures() {
        GrazingIncreaseEnergy();
        age++;
        if (IsAnAdult()) {
            _spriteRenderer.sprite = animalData.MatureSprite;
        }
    }

    private bool IsAnAdult() {
        return age >= animalData.MatureAge;
    }

    public void GrazingIncreaseEnergy() {
        if (currentEnergy < animalData.MaxEnergy)
            currentEnergy++;
    }


    private void FeedingIncreaseEnergy(int energyAmount) {
        currentEnergy += energyAmount;
        if (currentEnergy > animalData.MaxEnergy)
            currentEnergy = animalData.MaxEnergy;
    }

    private void IncreaseHappiness() {
        currentHappiness++;
    }

    public void PlayerInteract(GameObject equippedItem) {
        var consumable = equippedItem.GetComponent<Item>();

        //TODO: Refactor this due to the change in how Items work
        // FeedingIncreaseEnergy(consumable.energyAmount);
        Debug.Log($"Player tries to feed {animalData.name}");
        if (consumable != null && consumable.itemData.GetType() == typeof(ConsumableData))
            FeedingIncreaseEnergy(((ConsumableData) consumable.itemData).EnergyAmount);
    }

    public void PlayerInteract() {
        Debug.Log("Player pets the cow");
        IncreaseHappiness();
    }

    // for debugging purposes.
    public override string ToString() {
        StringBuilder sb = new StringBuilder();

        sb.AppendLine($"{animalData.name}: {NickName}").AppendLine($"Age: {age}")
          .AppendLine($"Current Energy: {currentEnergy}/{animalData.MaxEnergy}")
          .AppendLine($"Current Happiness: {currentHappiness}/{animalData.MaxHappiness}");
        return sb.ToString();
    }
}