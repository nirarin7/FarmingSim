using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Plant : MonoBehaviour {
    // Plant attributes (instance variables)
    public string plantName;
    public string type;
    public int numberOfDaysGrown;
    public int harvestTimeDays;
    public List<Sprite> growingSprites;
    public Sprite harvestSprite;
    public GameObject HarvestItem;

    private int _totalSpritesCount;
    private SpriteRenderer _spriteRenderer;
    
    // Start is called before the first frame update
    void Start() {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _totalSpritesCount = growingSprites.Count;
    }

    // Update is called once per frame
    void Update() {
    }
    
    public void Grow() {
        numberOfDaysGrown++;

        if (numberOfDaysGrown >= harvestTimeDays)
            _spriteRenderer.sprite = harvestSprite;
        else
            _spriteRenderer.sprite = growingSprites[CalculateSpriteIndex()];
    }

    private int CalculateSpriteIndex() {
        return (int) ((_totalSpritesCount / (float) harvestTimeDays) * numberOfDaysGrown);
    }
    
    public void Harvest() {
        Debug.Log("Item dropped" );
        Instantiate(HarvestItem);
    }
}