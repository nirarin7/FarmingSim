using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Cow : MonoBehaviour, IMaturable, IPlayerInteractable {
    public string cowName;
    public int cowAge;
    public Sprite matureSprite;
    public int currentEnergy;
    public int currentHappiness;
    public int matureAge = 5;
    public int maxEnergy = 100;


    private SpriteRenderer _spriteRenderer;


    // Start is called before the first frame update
    void Start() {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    // void Update()
    // {
    //     Matures();
    // }


    public void Matures() {
        
        GrazingIncreaseEnergy();
        cowAge++;
        if (cowAge >= matureAge) {
            _spriteRenderer.sprite = matureSprite;
            
        }
    }

    public void GrazingIncreaseEnergy() {
        if (currentEnergy < maxEnergy)
            currentEnergy++;
    }


    private void FeedingIncreaseEnergy(int energyAmount) {
        if (currentEnergy + energyAmount < maxEnergy) {
            currentEnergy += energyAmount;

            if (currentEnergy < maxEnergy)
                currentEnergy += (maxEnergy - currentEnergy);
        } else
            Debug.Log("the cow is full");
    }

    private void IncreaseHappiness() {
        currentHappiness++;
    }

    public void PlayerInteract(GameObject equippedItem) {
        var consumable = equippedItem.GetComponent<ConsumableItem>();

        if (consumable != null)
            FeedingIncreaseEnergy(consumable.energyAmount);
    }

    public void PlayerInteract() {
        Debug.Log("Player pets the cow");
        IncreaseHappiness();
    }
}