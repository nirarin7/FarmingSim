using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour {
    // Plant attributes (instance variables)

    private SpriteRenderer _spriteRenderer;

    public String plantName;
    public String type;


    private int _totalSprites;
    public List<Sprite> growthPhase;
    public Sprite harvestSprite;


    public int _currentDay;
    public int harvestDay;

    // Start is called before the first frame update
    void Start() {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _totalSprites = growthPhase.Count;
    }

    // Update is called once per frame
    void Update() {
    }


    public void Grow() {
        
        _currentDay++;
        Debug.Log(CalculateSprite());
        // change sprite
        
        if (_currentDay >= harvestDay) {
            _spriteRenderer.sprite = harvestSprite;
        } else {
            _spriteRenderer.sprite = growthPhase[CalculateSprite()];
        }
    }

    private int CalculateSprite() {
        float spriteIndex = (_totalSprites / (float)harvestDay) * _currentDay;
        return (int)spriteIndex;
    }
}