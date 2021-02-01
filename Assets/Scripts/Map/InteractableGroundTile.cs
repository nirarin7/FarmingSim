using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableGroundTile : MonoBehaviour {
    public GameObject plant;
    public Sprite plowedGroundSprite;

    private SpriteRenderer _spriteRenderer;
    private Sprite _baseGround;
    private Color _highlightedColor = Color.red;
    private Color _defaultColor;
    private bool isPlowed = false;

    // Start is called before the first frame update
    void Start() {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        if (_spriteRenderer) {
            _defaultColor = _spriteRenderer.color;
            _baseGround = _spriteRenderer.sprite;
        }
    }

    // Update is called once per frame
    void Update() { }

    void OnMouseEnter() {
        _spriteRenderer.color = _highlightedColor;
    }

    void OnMouseExit() {
        _spriteRenderer.color = _defaultColor;
    }

    private void OnMouseDown() {
        var playerGameObject = GameObject.FindWithTag("Player");
        if (playerGameObject == null) return;
        
        var player = playerGameObject.GetComponent<Player>();
        if (player != null) {
            //todo: remove debug 
            if (player.getPlant() == null) 
                Debug.Log("Player is missing a plant gameobject.");

            if (isPlowed && player.getPlant() != null && plant == null)
                plant = Instantiate(player.getPlant(), gameObject.transform.position, Quaternion.identity);
        }

        if (player.getTool() == "Plow") {
            _spriteRenderer.sprite = plowedGroundSprite;
            isPlowed = true;
        }
    }
}