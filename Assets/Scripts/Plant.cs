﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.Serialization;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;
using Vector2 = UnityEngine.Vector2;

public class Plant : MonoBehaviour {
    // Plant attributes (instance variables)
    public string plantName;
    public string type;
    public int numberOfDaysGrown;
    public int harvestTimeDays;
    public List<Sprite> growingSprites;
    public Sprite harvestSprite;
    public GameObject harvestItem;
    public int maxHarvestItemNumber;
    public int minHarvestItemNumber;
    public int totalHarvestNumber;
    public float dropOffSetX;
    public float dropOffSetY;
    //public Vector<> playerPosition; 
    

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
        Debug.Log("Item dropped");

        totalHarvestNumber = Random.Range(minHarvestItemNumber, maxHarvestItemNumber); // is this already an int?

        var dropPosition = gameObject.transform.position;
        
        // drops in range of plant
        for (int numberDropped = 0; numberDropped < totalHarvestNumber; numberDropped++) {
            var x = Random.Range(dropPosition.x - dropOffSetX, dropPosition.x + dropOffSetX);
            var y = Random.Range(dropPosition.y - dropOffSetY, dropPosition.y + dropOffSetY);
            Instantiate(harvestItem, new Vector2(x,y),Quaternion.identity);
        }
    }
    
    
}